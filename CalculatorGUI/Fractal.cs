using SIPEP;
using SIPEP.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorGUI;

public partial class Fractal : Form
{
    public Fractal()
    {
        InitializeComponent();
    }

    private void DrawFractal(object sender, EventArgs e)
    {
        TryDrawFractal();
    }

    private bool TryDrawFractal()
    {
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
        if (!BigComplex.TryParse(epsilonText.Text, out epsilon))
            return false;

        graphImageBox.BackgroundImage?.Dispose();
        graphImageBox.BackgroundImage = GetJulia();
        return true;
    }

    private static Color[,] currentBMP;
    private static int threadsRunning;

    private int pixelScale, iterations;
    private BigComplex cutoff, scale, startPoint, epsilon;
    private BigRational UnitsPerPixel;
    private Bitmap GetJulia()
    {
        loadingBar.Value = 0;
        loadingBar.Maximum = pixelScale;
        startPoint -= scale / 2;
        BigComplex position = startPoint;
        UnitsPerPixel = scale.Real / pixelScale;
        currentBMP = new Color[pixelScale, pixelScale];

        /*
        for (int x = 0; x < pixelScale; x++)
        {
            position.Real = x * unitsPerPixel + startPoint.Real;
            for (int y = 0; y < pixelScale; y++)
            {
                position.Imaginary = y * unitsPerPixel + startPoint.Imaginary;
                bmp.SetPixel(x, pixelScale - y - 1, GetColor(position, iterations, cutoff, epsilon));
                loadingBar.Value++;
            }
        }*/

        Thread[] threads = new Thread[pixelScale];
        for (int i = 0; i < pixelScale; i++)
        {
#nullable disable
            ParameterizedThreadStart start = new(GetStrip);
#nullable enable
            threads[i] = new Thread(start);
            threadsRunning++;
        }

        for (int i = 0; i < threads.Length; i++)
        {
            Equation eq = new Equation(equationBox.Text);
            eq.Variables.Add("z", 0);
            threads[i].Start((i, eq));
        }

        while (threadsRunning != 0) {
            loadingBar.Value = threads.Length - threadsRunning - 1;
        }
        Bitmap bmp = new Bitmap(pixelScale, pixelScale);

        for (int x = 0; x < currentBMP.GetLength(0); x++)
        {
            for (int y = 0; y < currentBMP.GetLength(1); y++)
            {
                bmp.SetPixel(x, y, currentBMP[x, y]);
            }
        }
        return bmp;
    }

    private void GetStrip(object index)
    {
        if (index is not ValueTuple<int, Equation> data)
        {
            threadsRunning--;
            return;
        }

        int xIndex = data.Item1;
        Equation eq = data.Item2;

        BigComplex position = new(xIndex * UnitsPerPixel + startPoint.Real, 0);
        //eq.Variables = Calculator.currentEquation.Variables;

        for (int i = 0; i < pixelScale; i++)
        {
            position.Imaginary = i * UnitsPerPixel + startPoint.Imaginary;
            Color c = GetColor(position, iterations, cutoff.Real, epsilon.Real, eq);
            lock (currentBMP)
            {
                currentBMP[xIndex, pixelScale - i - 1] = c;
            }
        }

        threadsRunning--;
    }

    private static Color GetColor(BigComplex position, int iterations, BigRational cutoff, BigRational epsilon, Equation eq)
    {
        BigComplex z = position;
        for (int i = 0; i < iterations; i++)
        {
            eq.Variables["z"] = z;
            z = eq.Solve();

            z.Real -= z.Real % epsilon;
            z.Imaginary -= z.Imaginary % epsilon;

            if (z == 0)
                return Color.Red;
        }

        bool realInRange = z.Real < cutoff && z.Real > -cutoff;
        bool imaginaryInRange = z.Imaginary < cutoff && z.Imaginary > -cutoff;
        return realInRange && imaginaryInRange ? Color.Red : Color.Blue;
    }
}
