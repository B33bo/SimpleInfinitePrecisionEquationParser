using CalculatorGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Functions;

public static class CustomFunctions
{
    [Function("Close")]
    public static BigComplex Close(params BigComplex[] args)
    {
        if (args.Length == 0)
        {
            Calculator.Instance.Close();
            return 0;
        }

        if (args[1].IsBoolean && args[1].BoolValue)
            throw new Exception();
        Calculator.Instance.Close();
        return 0;
    }

    [Function("ChangeColor")]
    public static BigComplex ChangeColor(params BigComplex[] args)
    {
        if (args.Length < 3)
            return false;
        int r = (int)args[0].Real, g = (int)args[1].Real, b = (int)args[2].Real;

        Color color;
        if (args.Length > 3)
            color = Color.FromArgb((int)args[3].Real, r, g, b);
        else
            color = Color.FromArgb(r, g, b);

        Calculator.Instance.SetColor(color);
        return true;
    }

    [Function("ChangeTextColor")]
    public static BigComplex ChangeTextColor(params BigComplex[] args)
    {
        if (args.Length < 3)
            return false;
        int r = (int)args[0].Real, g = (int)args[1].Real, b = (int)args[2].Real;

        Color color;
        if (args.Length > 3)
            color = Color.FromArgb((int)args[3].Real, r, g, b);
        else
            color = Color.FromArgb(r, g, b);

        Calculator.Instance.SetTextColor(color);
        return true;
    }
}
