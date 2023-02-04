using SIPEP;
using System.Media;
using System.Numerics;

namespace CalculatorGUI;

public partial class Graph : Form
{
    private int truewidth = 128, trueheight = 128;
    private BigRational xoffset = 0, yoffset = 0;
    private BigRational width = 2, height = 2;

    private enum Mode
    {
        Coordinate,
        Imaginary,
    }

    private Mode mode = Mode.Coordinate;
    private bool cancel = false;
    private int lineWidth;

    public Graph()
    {
        InitializeComponent();
    }

    private void Draw(object sender, EventArgs e)
    {
        loadingBar.Show();

        try
        {
            Calculator.currentEquation.LoadString(equationBox.Text);
        }
        catch (Exception)
        {
            SystemSounds.Beep.Play();
        }

        lineWidth = (int)lineWidthSlider.Value;

        var img = sweep.Checked ? Sweep() : Draw();

        loadingBar.Hide();

        if (img is null)
            return;

        graphImageBox.BackgroundImage?.Dispose();
        graphImageBox.BackgroundImage = img;
    }

    private Bitmap? Draw()
    {
        loadingBar.Maximum = truewidth * trueheight;
        cancel = false;
        loadingBar.Value = 0;
        if (!BigComplex.TryParse(XOffset.Text, out BigComplex xoffset))
            return null;
        if (!BigComplex.TryParse(YOffset.Text, out BigComplex yoffset))
            return null;

        if (!BigComplex.TryParse(widthText.Text, out BigComplex width))
            return null;
        if (!BigComplex.TryParse(heightText.Text, out BigComplex height))
            return null;

        BigRational trueWidthToUnit = (width / truewidth).Real;
        BigRational trueHeightToUnit = (height / truewidth).Real;

        var midPointXunit = (-xoffset + width / 2) / width * truewidth;
        var midPointYunit = (-yoffset + height / 2) / height * trueheight;

        var baseMap = GetBase((int)midPointXunit.Real, (int)midPointYunit.Real);
        if (baseMap is null)
            return null;
        Bitmap bitmap = baseMap;

        if (!Calculator.currentEquation.Variables.ContainsKey("x"))
            Calculator.currentEquation.Variables.Add("x", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("y"))
            Calculator.currentEquation.Variables.Add("y", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("pos"))
            Calculator.currentEquation.Variables.Add("pos", 0);

        var left = (xoffset - (width / 2)).Real;
        var down = (yoffset - (height / 2)).Real;

        for (int trueX = 0; trueX < truewidth; trueX++)
        {
            for (int trueY = 0; trueY < trueheight; trueY++)
            {
                if (cancel)
                    return null;

                loadingBar.Value++;

                BigRational x = left + trueX * trueWidthToUnit;
                BigRational y = down + (trueheight - trueY - 1) * trueHeightToUnit;

                Calculator.currentEquation.Variables["x"] = x;
                Calculator.currentEquation.Variables["y"] = y;
                Calculator.currentEquation.Variables["pos"] = new BigComplex(x, y);

                try
                {
                    if (mode == Mode.Coordinate)
                    {
                        if (Calculator.currentEquation.SolveBoolean())
                            bitmap.SetPixel(trueX, trueY, Color.Red);
                    }
                    else
                    {
                        var pos = Calculator.currentEquation.Solve();
                        int xPos = (int)((pos.Real - left) / width.Real * truewidth);
                        int yPos = (int)((pos.Imaginary - down) / height.Real * truewidth);

                        if (xPos < 0 || xPos >= truewidth || yPos < 0 || yPos > trueheight)
                            continue;
                        bitmap.SetPixel(xPos, trueY - yPos - 1, Color.Red);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        return bitmap;
    }

    private Bitmap? GetBase(int midX, int midY)
    {
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

    private Bitmap? Sweep()
    {
        loadingBar.Value = 0;
        loadingBar.Maximum = truewidth;

        cancel = false;
        loadingBar.Value = 0;
        if (!BigComplex.TryParse(XOffset.Text, out BigComplex xoffset))
            return null;
        if (!BigComplex.TryParse(YOffset.Text, out BigComplex yoffset))
            return null;

        if (!BigComplex.TryParse(widthText.Text, out BigComplex width))
            return null;
        if (!BigComplex.TryParse(heightText.Text, out BigComplex height))
            return null;

        BigRational trueWidthToUnit = (width / truewidth).Real;
        BigRational trueHeightToUnit = (height / truewidth).Real;

        var midPointXunit = (-xoffset + width / 2) / width * truewidth;
        var midPointYunit = (-yoffset + height / 2) / height * trueheight;
        var baseImage = GetBase((int)midPointXunit.Real, (int)midPointYunit.Real);
        if (baseImage is null)
            return null;
        Bitmap bitmap = baseImage;

        if (!Calculator.currentEquation.Variables.ContainsKey("x"))
            Calculator.currentEquation.Variables.Add("x", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("y"))
            Calculator.currentEquation.Variables.Add("y", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("pos"))
            Calculator.currentEquation.Variables.Add("pos", 0);

        var left = (xoffset - (width / 2)).Real;
        var down = (yoffset - (height / 2)).Real;

        for (int trueX = 0; trueX < truewidth; trueX++)
        {
            loadingBar.Value++;
            if (cancel)
                return null;

            BigRational x = left + trueX * trueWidthToUnit;
            Calculator.currentEquation.Variables["x"] = x;
            Calculator.currentEquation.Variables["pos"] = new BigComplex(x, 0);
            BigRational output = Calculator.currentEquation.Solve().Real;

            if (output > int.MaxValue || output < int.MinValue)
                continue;

            // output = down + trueY * trueHeightToUnit
            // output - down / trueHeightToUnit = trueY
            int trueY = (int)((output - down) / trueHeightToUnit);

            DrawAtPoint(trueX, trueheight - trueY - 1);
        }
        return bitmap;

        void DrawAtPoint(int x, int y)
        {
            int top = y - lineWidth / 2;
            int bottom = y + lineWidth / 2;

            for (int i = top; i <= bottom; i++)
            {
                if (i < 0 || i >= bitmap.Height)
                    continue;
                bitmap.SetPixel(x, i, Color.Red);
            }
        }
    }

    private void CycleMode(object sender, EventArgs e)
    {
        if (mode == Mode.Imaginary)
            mode = Mode.Coordinate;
        else
            mode = Mode.Imaginary;
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

        var drawing = sweep.Checked ? Sweep() : Draw();
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

    private void SwitchToFractal(object sender, EventArgs e)
    {
        Close();
        new Fractal().Show();
    }

    private void ToggleSweep(object sender, EventArgs e)
    {
        lineWidthSlider.Visible = sweep.Checked;
        lineWidthText.Visible = sweep.Checked;
    }

    private void ResetMPos(object sender, MouseEventArgs e)
    {
        (double x, double y) mousePosPercent = ((double)e.Location.X / graphImageBox.Size.Width, (double)e.Location.Y / graphImageBox.Size.Height);
        mousePosPercent.y = 1 - mousePosPercent.y;

        BigComplex position = new BigComplex(mousePosPercent.x * width, mousePosPercent.y * height);
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
}
