using SIPEP;
using System.Diagnostics;
using System.Numerics;

namespace CalculatorGUI;

public partial class Fractal : Form
{
    private static Color[,] currentBMP;
    private static int threadsRunning;
    private static int sectionsDone;

    private int pixelScale, iterations, threads, rowsDone;
    private BigComplex cutoff, scale, startPoint, epsilon, offset;
    private bool juliaMode;

    private BigRational UnitsPerPixel;
    private BigComplex lastMousePos;

    public Fractal()
    {
        InitializeComponent();
        juliaOrMandelbrot.SelectedIndex = 0;
    }

    private void DrawFractal(object sender, EventArgs e)
    {
        TryDrawFractal(out Bitmap? drawing);
        graphImageBox.BackgroundImage?.Dispose();
        graphImageBox.BackgroundImage = drawing;
    }

    private bool TryDrawFractal(out Bitmap? drawing)
    {
        juliaMode = juliaOrMandelbrot.SelectedIndex == 1;
        drawing = null;
        if (!BigComplex.TryParse(scaleText.Text, out scale))
            return false;
        if (!BigComplex.TryParse(XOffset.Text, out BigComplex xOffset))
            return false;
        if (!BigComplex.TryParse(YOffset.Text, out BigComplex yOffset))
            return false;
        if (!int.TryParse(resBox.Text, out pixelScale))
            return false;
        if (!BigComplex.TryParse(cutoffText.Text, out cutoff))
            return false;
        if (!int.TryParse(iterationsText.Text, out iterations))
            return false;
        if (!int.TryParse(threadBox.Text, out threads))
            return false;
        if (!BigComplex.TryParse(epsilonText.Text, out epsilon))
            return false;

        offset = new BigComplex(xOffset.Real, yOffset.Real);
        drawing = GetJulia();
        return true;
    }

    private void ResetMPos(object sender, MouseEventArgs e)
    {
        (double x, double y) mousePosPercent = ((double)e.Location.X / graphImageBox.Size.Width, (double)e.Location.Y / graphImageBox.Size.Height);
        mousePosPercent.y = 1 - mousePosPercent.y;

        BigComplex position = new(mousePosPercent.x * scale.Real, mousePosPercent.y * scale.Real);
        lastMousePos = position;
        position -= new BigComplex(scale.Real / 2, scale.Real / 2);
        position += new BigComplex(offset.Real, offset.Imaginary);

        string xAxis = position.Real.ToString("0.00");
        string yAxis = position.Imaginary.ToString("0.00");
        posText.Text = $"({xAxis},{yAxis})";
    }

    private void CopyMpos(object sender, EventArgs e)
    {
        Clipboard.SetText($"{lastMousePos.Real} + i*{lastMousePos.Imaginary}");
    }

    private void SaveAsImage(object sender, EventArgs e)
    {
        var dialog = new SaveFileDialog()
        {
            Filter = "PNG|*.png|JPG|*.jpg|BITMAP|*.bmp",
            Title = "Save graph",
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        if (dialog.FileName == "")
            return;

        if (!TryDrawFractal(out Bitmap? drawing))
            return;
        switch (dialog.FilterIndex)
        {
            case 1:
                drawing?.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
            case 2:
                drawing?.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                break;
            case 3:
                drawing?.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                break;
        }
        drawing?.Dispose();
    }

    private void Fractal_Load(object sender, EventArgs e)
    {
        TryDrawFractal(out Bitmap? drawing);
        graphImageBox.BackgroundImage = drawing;
    }

    private void RecalcNums(object sender, EventArgs e)
    {
        if (!BigComplex.TryParse(scaleText.Text, out scale))
            return;
        if (!BigComplex.TryParse(XOffset.Text, out BigComplex xOffset))
            return;
        if (!BigComplex.TryParse(YOffset.Text, out BigComplex yOffset))
            return;

        BigRational left = -scale.Real / 2 + xOffset.Real;
        BigRational right = scale.Real / 2 + xOffset.Real;
        BigRational up = scale.Real / 2 + yOffset.Real;
        BigRational down = -scale.Real / 2 + yOffset.Real;

        topLeft.Text = $"({left},{up})";
        bottomLeft.Text = $"({left},{down})";
        topRight.Text = $"({right},{up})";
        bottomRight.Text = $"({right},{down})";
        middle.Text = $"({xOffset},{yOffset})";
    }

    private Bitmap GetJulia()
    {
        rowsDone = 0;
        loadingBar.Value = 0;
        loadingBar.Maximum = pixelScale;

        startPoint = -scale / 2;
        startPoint.Imaginary -= scale.Real / 2; // why
        startPoint += offset;

        BigComplex position = startPoint;
        UnitsPerPixel = scale.Real / pixelScale;
        currentBMP = new Color[pixelScale, pixelScale];

        Thread[] loadedThreads = new Thread[threads];

        Stopwatch stopwatch = new();
        stopwatch.Start();

        threadsRunning = 1;
        sectionsDone = 0;

        Equation eq = new Equation(equationBox.Text);
        eq.Simplify();
        eq.SetVariable("z", 0);

        int lengthOfThread = pixelScale / threads;

        for (int i = 0; i < threads; i++)
        {
            ParameterizedThreadStart start = new(DoThread);
            loadedThreads[i] = new Thread(start);
            threadsRunning++;

            if (i == threads - 1)
            {
                //int remainingLength = pixelScale - );
                loadedThreads[i].Start((i * lengthOfThread, new Equation(eq), lengthOfThread));
            }
            else
                loadedThreads[i].Start((i * lengthOfThread, new Equation(eq), lengthOfThread));
        }

        threadsRunning--;

        while (threadsRunning > 0)
        {
            renderTime.Text = stopwatch.Elapsed.ToString();
            loadingBar.Value = rowsDone;
            Update();
        }

        stopwatch.Stop();
        loadingBar.Value = loadingBar.Maximum;

        Bitmap bmp = new Bitmap(pixelScale, pixelScale);

        for (int x = 0; x < currentBMP.GetLength(0); x++)
        {
            for (int y = 0; y < currentBMP.GetLength(1); y++)
                bmp.SetPixel(x, pixelScale - y - 1, currentBMP[x, y]);
        }

        return bmp;
    }

    private void DoThread(object? input) // has to be an object because it's threaded
    {
        if (input is not ValueTuple<int, Equation, int> tuple)
            return;

        int xIndex = tuple.Item1;
        Equation eq = tuple.Item2;
        int length = tuple.Item3;

        for (int i = 0; i < length; i++)
        {
            GetStrip(xIndex + i, eq);
        }

        threadsRunning--;
    }

    private void GetStrip(int xIndex, Equation eq)
    {
        BigComplex position = new(xIndex * UnitsPerPixel + startPoint.Real, 0);

        for (int i = 0; i < pixelScale; i++)
        {
            position.Imaginary = i * UnitsPerPixel + startPoint.Imaginary;
            Color c = GetColor(position, iterations, cutoff.Real, epsilon.Real, juliaMode, eq);
            currentBMP[xIndex, pixelScale - i - 1] = c;
        }

        rowsDone++;
    }

    private static Color GetColor(BigComplex position, int iterations, BigRational cutoff, BigRational epsilon, bool julia, Equation eq)
    {
        BigComplex z = julia ? position : 0;
        eq.SetVariable("c", position);
        eq.SetVariable("z", z);

        for (int i = 0; i < iterations; i++)
        {
            eq.Variables["z"] = z;
            z = eq.Solve();

            z.Real -= z.Real % epsilon;
            z.Imaginary -= z.Imaginary % epsilon;

            if (z == 0)
                return Color.Black;

            if (BigRational.Abs(z.Real) + BigRational.Abs(z.Imaginary) > cutoff)
            {
                int darkness = (int)(i / (float)iterations * 255);
                return Color.FromArgb(0, darkness, 255 - darkness);
            }
        }

        return Color.Black;
    }
}
