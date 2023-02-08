using SIPEP;
using System.Numerics;

namespace CalculatorGUI;

public partial class Graph : Form
{
    private int truewidth = 128, trueheight = 128;
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
        loadingBar.Value = 0;
        graph = new byte[truewidth, trueheight];

        // midPointX = (width / 2 - xoffset) / (width / truewidth) therefore:
        int midPointX = (int)((truewidth * (width / 2 - xoffset)) / width);
        int midPointY = (int)((truewidth * (width / 2 - xoffset)) / height);

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

        return image;
    }

    private void Draw(object sender, EventArgs e)
    {
        loadingBar.Show();
        graph = new byte[truewidth, trueheight];

        var img = Render();

        loadingBar.Hide();

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
        int widthForThread = truewidth / threads;

        int threadsWaiting = threads;

        for (int i = 0; i < threads; i++)
        {
            int startX = i * widthForThread, endX = widthForThread * (i + 1);
            if (i == threads - 1)
                endX = truewidth; // so the slice at the end doesn't get shaved off

            new Thread(() =>
            {
                Equation eq = new(equation, Calculator.currentEquation.Variables);
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

        Equation eq = new(equation, Calculator.currentEquation.Variables);

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
            BigRational output = eq.Solve().Real;

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
        midY = trueheight - midY - 1;
        Bitmap bmp = new(truewidth, trueheight);

        for (int i = 0; i < truewidth; i++)
        {
            for (int j = 0; j < trueheight; j++)
            {
                if (i == midX || j == midY)
                    bmp.SetPixel(i, j, Color.Gray);
                else
                    bmp.SetPixel(i, j, Color.White);
            }
        }
        return bmp;
    }

    private void ChangeRes(object sender, EventArgs e)
    {
        if (!int.TryParse(resBox.Text, out int newRes))
            return;
        if (newRes <= 0)
            return;

        truewidth = newRes;
        trueheight = newRes;
        lineWidthSlider.Maximum = newRes;
    }

    private void SaveAsPNG(object sender, EventArgs e)
    {
        var dialog = new SaveFileDialog()
        {
            Filter = "JPG|*.jpg|PNG|*.png|BITMAP|*.bmp",
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
        UpdatePlotInformation(sender, e);
    }

    private void ToggleSweep(object sender, EventArgs e)
    {
        lineWidthSlider.Visible = sweep.Checked;
        lineWidthText.Visible = sweep.Checked;
        UpdatePlotInformation(sender, e);
    }

    private void UpdatePlotInformation(object sender, EventArgs e)
    {
        if (equationBox.SelectedIndex >= 0)
            return;
        equationBox.Items[currentPlot] = equationBox.Text;
        plots[currentPlot] = new Plot(equationBox.Text, sweep.Checked, (int)lineWidthSlider.Value, colorBox.BackColor);
    }

    private void ChangeEquationNumber(object sender, EventArgs e)
    {
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
        if (!BigComplex.TryParse(XOffset.Text, out BigComplex xoffset))
            return;
        if (!BigComplex.TryParse(YOffset.Text, out BigComplex yoffset))
            return;

        if (!BigComplex.TryParse(widthText.Text, out BigComplex width))
            return;
        if (!BigComplex.TryParse(heightText.Text, out BigComplex height))
            return;

        this.width = width.Real;
        this.height = height.Real;
        this.xoffset = xoffset.Real;
        this.yoffset = yoffset.Real;

        var left = xoffset - (width / 2);
        var right = xoffset + (width / 2);
        var up = yoffset + (height / 2);
        var down = yoffset - (height / 2);

        topLeft.Text = $"({left.Real}, {up.Real})";
        bottomLeft.Text = $"({left.Real}, {down.Real})";
        topRight.Text = $"({right.Real}, {up.Real})";
        bottomRight.Text = $"({right.Real}, {down.Real})";
        middle.Text = $"({xoffset.Real}, {yoffset.Real})";
    }

    private struct Plot
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
