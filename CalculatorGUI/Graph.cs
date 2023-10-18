using SIPEP;
using System.Diagnostics;
using System.Numerics;

namespace CalculatorGUI;

public partial class Graph : Form
{
    private int truewidth = 512, trueheight = 512;
    private BigRational xoffset = 0, yoffset = 0;
    private BigRational width = 2, height = 2;

    private readonly List<Plot> plots;
    private int currentPlot;
    private static byte[,]? graph;

    private static int pendingPlots;
    private static int progress;

    public Graph()
    {
        InitializeComponent();
        plots = new List<Plot>() { new Plot("", true, 1, Color.Red) };
        graphImageBox.BackgroundImage = GetBase(truewidth / 2, trueheight / 2);
    }

    private Bitmap? Render()
    {
        loadingBar.Show();
        loadingBar.Value = 0;
        graph = new byte[truewidth, trueheight];

        // midPointX = (width / 2 - xoffset) / (width / truewidth) therefore:
        int midPointX = (int)((truewidth * (width / 2 - xoffset)) / width);
        int midPointY = (int)((trueheight * (height / 2 - yoffset)) / height);

        var image = GetBase(midPointX, midPointY);

        int area = truewidth * trueheight;
        for (int i = 0; i < plots.Count; i++)
            loadingBar.Maximum += plots[i].Sweep ? truewidth : area;

        if (image is null)
            return null;
        progress = 0;

        for (int i = 0; i < plots.Count; i++)
        {
            pendingPlots++;
            Plot plot = plots[i];
            byte index = (byte)(i + 1);

            if (plots[i].Sweep)
            {
                new Thread(() =>
                {
                    Sweep(plot.Equation, index, plot.LineWidth);
                    pendingPlots--;
                }).Start();
            }
            else
            {
                new Thread(() =>
                {
                    BooleanDraw(plot.Equation, index);
                    pendingPlots--;
                }).Start();
            }
        }

        while (pendingPlots > 0)
        {
            loadingBar.Value = progress;
        }

        for (int x = 0; x < graph.GetLength(0); x++)
        {
            for (int y = 0; y < graph.GetLength(1); y++)
            {
                var index = graph[x, y] - 1;
                if (index < 0)
                    continue;
                image.SetPixel(x, y, plots[index].LineColor);
            }
        }

        loadingBar.Hide();

        return image;
    }

    private void Draw(object sender, EventArgs e)
    {
        graph = new byte[truewidth, trueheight];

        var img = Render();

        if (img is null)
            return;

        graphImageBox.BackgroundImage?.Dispose();
        graphImageBox.BackgroundImage = img;
    }

    private void BooleanDraw(string equation, byte index)
    {
        if (graph is null)
            return;

        BigRational trueWidthToUnit = (width / truewidth);
        BigRational trueHeightToUnit = (height / truewidth);

        var left = xoffset - (width / 2);
        var down = yoffset - (height / 2);

        int threads = (int)threadsPerPlot.Value;

        if (threads > truewidth)
            threads = truewidth;

        int widthForThread = truewidth / threads;

        int threadsWaiting = threads;

        for (int i = 0; i < threads; i++)
        {
            int startX = i * widthForThread, endX = widthForThread * (i + 1);
            if (i == threads - 1)
                endX = truewidth; // so the slice at the end doesn't get shaved off

            new Thread(() =>
            {
                Equation eq;
                try
                {
                    eq = new(equation, Calculator.currentEquation.Variables);
                    eq.Simplify();
                }
                catch (Exception)
                {
                    threadsWaiting--;
                    return;
                }
                eq.SetVariable("x", 0);
                eq.SetVariable("y", 0);
                eq.SetVariable("pos", 0);

                for (int trueX = startX; trueX < endX; trueX++)
                {
                    RenderYCoordinate(trueX, eq);
                }
                threadsWaiting--;
            }).Start();
        }

        while (threadsWaiting > 0) { }

        void RenderYCoordinate(int trueX, Equation eq)
        {
            if (graph is null)
                return;
            for (int trueY = 0; trueY < trueheight; trueY++)
            {
                progress++;

                BigRational x = left + trueX * trueWidthToUnit;
                BigRational y = down + (trueheight - trueY - 1) * trueHeightToUnit;

                eq.Variables["x"] = x;
                eq.Variables["y"] = y;
                eq.Variables["pos"] = new BigComplex(x, y);

                try
                {
                    if (!eq.SolveBoolean())
                        continue;
                    if (graph[trueX, trueY] > index)
                        continue;
                    graph[trueX, trueY] = index;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }

    private void Sweep(string equation, byte index, int lineHeight)
    {
        if (graph is null)
            return;
        BigRational trueWidthToUnit = (width / truewidth);
        BigRational trueHeightToUnit = (height / truewidth);

        Equation eq;
        try
        {
            eq = new(equation, Calculator.currentEquation.Variables);
            eq.Simplify();
        }
        catch (Exception)
        {
            return;
        }

        eq.SetVariable("x", 0);
        eq.SetVariable("y", 0);
        eq.SetVariable("pos", 0);

        var left = xoffset - (width / 2);
        var down = yoffset - (height / 2);

        for (int trueX = 0; trueX < truewidth; trueX++)
        {
            progress++;

            BigRational x = left + trueX * trueWidthToUnit;
            eq.Variables["x"] = x;
            eq.Variables["pos"] = new BigComplex(x, 0);

            BigRational output;
            try
            {
                output = eq.Solve().Real;
            }
            catch (Exception)
            {
                return;
            }

            if (output > int.MaxValue || output < int.MinValue)
                continue;

            // output = down + trueY * trueHeightToUnit
            // output - down / trueHeightToUnit = trueY
            int trueY = (int)((output - down) / trueHeightToUnit);
            trueY = trueheight - trueY - 1;

            DrawAtPoint(trueX, trueY, lineHeight, index);
        }

        static void DrawAtPoint(int x, int y, int height, byte index)
        {
            if (graph is null)
                return;
            int startY = y - height / 2;
            for (int i = 0; i < height; i++)
            {
                int trueY = i + startY;

                if (trueY < 0 || trueY >= graph.GetLength(1))
                    continue;

                if (graph[x, trueY] > index)
                    continue;
                graph[x, trueY] = index;
            }
        }
    }

    private Bitmap? GetBase(int midX, int midY)
    {
        bool renderAxis = this.renderAxis.Checked;
        midY = trueheight - midY - 1;
        Bitmap bmp = new(truewidth, trueheight);

        for (int i = 0; i < truewidth; i++)
        {
            for (int j = 0; j < trueheight; j++)
            {
                if ((i == midX || j == midY) && renderAxis)
                    bmp.SetPixel(i, j, Color.Gray);
                else
                    bmp.SetPixel(i, j, Color.White);
            }
        }
        return bmp;
    }

    private void ChangeRes(object sender, EventArgs e)
    {
        truewidth = (int)resBox.Value;
        trueheight = (int)resBox.Value;
        lineWidthSlider.Maximum = (int)resBox.Value;
    }

    private void SaveAsPNG(object sender, EventArgs e)
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

        var drawing = Render();
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

    private void ChangeColor(object sender, EventArgs e)
    {
        var colorDialog = new ColorDialog();
        if (colorDialog.ShowDialog() != DialogResult.OK)
            return;

        colorBox.BackColor = colorDialog.Color;
        plots[currentPlot].LineColor = colorDialog.Color;
    }

    private void ToggleSweep(object sender, EventArgs e)
    {
        lineWidthSlider.Visible = sweep.Checked;
        lineWidthText.Visible = sweep.Checked;
        plots[currentPlot].Sweep = sweep.Checked;
    }

    private int oldIndex = -1;

    private void ChangeEquationNumber(object sender, EventArgs e)
    {
        // I don't know what I did, but I fixed the weird combobox issue
        // fuck comboboxes
        oldIndex = equationBox.SelectedIndex;

        if (equationBox.SelectedIndex < 0)
            return;

        currentPlot = equationBox.SelectedIndex;
        if (currentPlot >= plots.Count)
        {
            plots.Add(new Plot("", true, 1, Color.Red));
            equationBox.Items.Insert(plots.Count - 1, "");
            equationBox.Text = "";
        }

        sweep.Checked = plots[currentPlot].Sweep;
        lineWidthSlider.Value = plots[currentPlot].LineWidth;
        colorBox.BackColor = plots[currentPlot].LineColor;
    }

    private void EquationTextChanged(object sender, EventArgs e)
    {
        if (oldIndex != equationBox.SelectedIndex)
        {
            oldIndex = equationBox.SelectedIndex;
            return;
        }

        equationBox.Items[currentPlot] = equationBox.Text;
        plots[currentPlot] = new Plot(equationBox.Text, plots[currentPlot].Sweep, plots[currentPlot].LineWidth, plots[currentPlot].LineColor);
    }

    private void ChangeLineWidth(object sender, EventArgs e)
    {
        plots[currentPlot].LineWidth = (int)lineWidthSlider.Value;
    }

    private void SwitchToFractal(object sender, EventArgs e)
    {
        new Fractal().Show();
        Close();
    }

    private void ResetMPos(object sender, MouseEventArgs e)
    {
        (double x, double y) mousePosPercent = ((double)e.Location.X / graphImageBox.Size.Width, (double)e.Location.Y / graphImageBox.Size.Height);
        mousePosPercent.y = 1 - mousePosPercent.y;

        BigComplex position = new(mousePosPercent.x * width, mousePosPercent.y * height);
        position -= new BigComplex(width / 2, height / 2);
        position += new BigComplex(xoffset, yoffset);

        string xAxis = position.Real.ToString("0.00");
        string yAxis = position.Imaginary.ToString("0.00");
        pointerPos.Text = $"({xAxis},{yAxis})";
    }

    private void ChangeAxis(object sender, EventArgs e)
    {
        try
        {
            Calculator.currentEquation.Parse(XOffset.Text);
            xoffset = Calculator.currentEquation.Solve().Real;

            Calculator.currentEquation.Parse(YOffset.Text);
            yoffset = Calculator.currentEquation.Solve().Real;

            Calculator.currentEquation.Parse(widthText.Text);
            width = Calculator.currentEquation.Solve().Real;

            Calculator.currentEquation.Parse(heightText.Text);
            height = Calculator.currentEquation.Solve().Real;
        }
        catch (Exception)
        {
            xoffset = BigComplex.NaN.Real;
            yoffset = BigComplex.NaN.Real;
            width = BigComplex.NaN.Real;
            height = BigComplex.NaN.Real;
        }

        var left = xoffset - (width / 2);
        var right = xoffset + (width / 2);
        var up = yoffset + (height / 2);
        var down = yoffset - (height / 2);

        topLeft.Text = $"({left}, {up})";
        bottomLeft.Text = $"({left}, {down})";
        topRight.Text = $"({right}, {up})";
        bottomRight.Text = $"({right}, {down})";
        middle.Text = $"({xoffset}, {yoffset})";
    }

    private class Plot
    {
        public string Equation { get; set; }
        public bool Sweep { get; set; }
        public int LineWidth { get; set; }
        public Color LineColor { get; set; }

        public Plot(string equation, bool sweep, int lineWidth, Color color)
        {
            Equation = equation;
            Sweep = sweep;
            LineWidth = lineWidth;
            LineColor = color;
        }
    }
}
