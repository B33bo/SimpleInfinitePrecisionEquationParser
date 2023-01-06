using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Functions;

public static class Trig
{
    [Function("Sin")]
    public static BigComplex Sin(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;

        if (args[0].Real < 0)
        {
            var res = Sin(args[0].Real * -1);
            return new BigComplex(-res.Real, res.Imaginary);
        }

        if (args[0].Imaginary < 0)
        {
            var res = Sin(args[0].Imaginary * -1);
            return new BigComplex(res.Real, -res.Imaginary);
        }

        var p = BigRational.Exp(args[0].Imaginary, Equation.DecimalPrecision);
        var q = (BigRational)1 / p;
        var sinh = (p - q) / 2;
        var cosh = (p + q) / 2;
        return new BigComplex(BigRational.Sin(args[0].Real, Equation.DecimalPrecision) * cosh, BigRational.Cos(args[0].Real, Equation.DecimalPrecision) * sinh);
    }

    [Function("Sinh")]
    public static BigComplex Sinh(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        var a = Misc.Exp(new BigComplex(-args[0].Real, -args[0].Imaginary));
        var exp1 = new BigComplex(-a.Real, -a.Imaginary);
        var exp2 = Misc.Exp(args[0]);

        return Operators.Divide(exp1 + exp2, new BigComplex(2, 0));
    }

    [Function("Asin")]
    public static BigComplex Asin(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        Complex normalPrecisionComplex = new((double)args[0].Real, (double)args[0].Imaginary);
        normalPrecisionComplex = Complex.Asin(normalPrecisionComplex);
        return new BigComplex(normalPrecisionComplex.Real, normalPrecisionComplex.Imaginary);
    }

    [Function("Cos")]
    public static BigComplex Cos(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;

        if (args[0].Real < 0)
        {
            var res = Cos(args[0].Real * -1);
            return new BigComplex(res.Real, res.Imaginary);
        }

        if (args[0].Imaginary < 0)
        {
            var res = Cos(args[0].Imaginary * -1);
            return new BigComplex(res.Real, res.Imaginary);
        }

        var p = BigRational.Exp(args[0].Imaginary, Equation.DecimalPrecision);
        var q = (BigRational)1 / p;
        BigRational sinh = (p - q) / 2;
        BigRational cosh = (p + q) / 2;
        return new BigComplex(BigRational.Cos(args[0].Real, Equation.DecimalPrecision) * cosh, -BigRational.Sin(args[0].Real, Equation.DecimalPrecision) * sinh);
    }

    [Function("Cosh")]
    public static BigComplex Cosh(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        var exp1 = Misc.Exp(new BigComplex(-args[0].Real, -args[0].Imaginary));
        var exp2 = Misc.Exp(args[0]);

        return Operators.Divide(exp1 + exp2, new BigComplex(2, 0));
    }

    [Function("Acos")]
    public static BigComplex Acos(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        Complex normalPrecisionComplex = new((double)args[0].Real, (double)args[0].Imaginary);
        normalPrecisionComplex = Complex.Acos(normalPrecisionComplex);
        return new BigComplex(normalPrecisionComplex.Real, normalPrecisionComplex.Imaginary);
    }

    [Function("Tan")]
    public static BigComplex Tan(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        BigRational x2 = 2.0 * args[0].Real;
        BigRational y2 = 2.0 * args[0].Imaginary;
        BigRational p = BigRational.Exp(y2, Equation.DecimalPrecision);
        BigRational q = 1.0 / p;
        BigRational cosh = (p + q) * 0.5;

        if (BigRational.Abs(args[0].Imaginary) <= 4.0)
        {
            BigRational sinh = (p - q) / 2;
            BigRational D = BigRational.Cos(x2, Equation.DecimalPrecision) + cosh;
            return new BigComplex(BigRational.Sin(x2, Equation.DecimalPrecision) / D, sinh / D);
        }

        BigRational D2 = 1.0 + BigRational.Cos(x2, Equation.DecimalPrecision) / cosh;
        return new BigComplex(BigRational.Sin(x2, Equation.DecimalPrecision) / cosh / D2, BigRational.Tanh(y2, Equation.DecimalPrecision) / D2);
    }

    [Function("Tanh")]
    public static BigComplex Tanh(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        BigComplex tan = Tan(new BigComplex(-args[0].Imaginary, args[0].Real));
        return new BigComplex(tan.Imaginary, -tan.Real);
    }

    [Function("Atan")]
    public static BigComplex Atan(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.Zero;
        Complex asNormalComplex = new((double)args[0].Real, (double)args[0].Imaginary);
        asNormalComplex = Complex.Atan(asNormalComplex);
        return new BigComplex(asNormalComplex.Real, asNormalComplex.Imaginary);
    }
}
