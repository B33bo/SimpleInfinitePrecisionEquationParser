﻿using SIPEP;
using System.Media;
using System.Numerics;

namespace CalculatorGUI;

public partial class Graph : Form
{
    private int truewidth = 128, trueheight = 128;
    private enum Mode
    {
        Coordinate,
        Imaginary,
    }

    private Mode mode = Mode.Coordinate;
    private bool cancel = false;

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

        loadingBar.Maximum = truewidth * trueheight;
        var img = Draw();

        loadingBar.Hide();
        //cancelButton.Hide();

        if (img is null)
            return;

        graphImageBox.BackgroundImage?.Dispose();
        graphImageBox.BackgroundImage = img;
    }

    private Bitmap? Draw()
    {
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

        Bitmap bitmap = new(truewidth, trueheight);

        if (!Calculator.currentEquation.Variables.ContainsKey("x"))
            Calculator.currentEquation.Variables.Add("x", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("y"))
            Calculator.currentEquation.Variables.Add("y", 0);

        if (!Calculator.currentEquation.Variables.ContainsKey("pos"))
            Calculator.currentEquation.Variables.Add("pos", 0);

        var left = (xoffset - (width / 2)).Real;
        var down = (yoffset - (height / 2)).Real;

        int halfWayX = truewidth / 2;
        int halfWayY = trueheight / 2;

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
                        else if (trueX == halfWayX || trueY == halfWayY)
                            bitmap.SetPixel(trueX, trueY, Color.Gray);
                        else
                            bitmap.SetPixel(trueX, trueY, Color.White);
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

        var drawing = Draw();
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

    private void ComplexModeToggle(object sender, EventArgs e)
    {
        //mode = complexMode.Checked ? Mode.Imaginary : Mode.Coordinate;
    }

    private void CancelLoading(object sender, EventArgs e)
    {
        cancel = true;
    }

    private void SwitchToFractal(object sender, EventArgs e)
    {
        Close();
        new Fractal().Show();
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

        var left = xoffset - (width / 2);
        var right = xoffset + (width / 2);
        var up = yoffset + (height / 2);
        var down = yoffset - (height / 2);

        topLeft.Text = $"({left.Real}, {up.Real})";
        bottomLeft.Text = $"({left.Real}, {down.Real})";
        topRight.Text = $"({right.Real}, {up.Real})";
        bottomRight.Text = $"({right.Real}, {down.Real})";
        middle.Text = $"({xoffset}, {yoffset})";
    }
}
