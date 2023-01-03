﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Functions;

public static class Misc
{
    [Function("Abs")]
    public static BigComplex Abs(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        BigComplex val = args[0];
        return BigRational.Sqrt(val.Real * val.Real + val.Imaginary * val.Imaginary, Equation.DecimalPrecision);
    }

    [Function("Log", Args = "Log(number, ?Base)")]
    public static BigComplex Log(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Log(args[0], 10);

        return logQuick(args[0]) / logQuick(args[1]);

        static BigComplex logQuick(BigComplex a)
        {
            return new BigComplex(BigRational.Log(Abs(a).Real, Equation.DecimalPrecision), BigRational.Atan2(a.Imaginary, a.Real, Equation.DecimalPrecision));
        }
    }

    [Function("Exp")]
    public static BigComplex Exp(params BigComplex[] args)
    {
        if (args.Length == 0)
            return Constants.E;
        return Operators.Pow(Constants.E, args[0]);
    }

    [Function("Rand", Args = "Rand(min, max, ?Seed)")]
    public static BigComplex Rand(params BigComplex[] args)
    {
        if (args.Length == 0)
            return new BigComplex(new Random((int)DateTime.Now.Ticks).NextDouble(), 0);
        int seed;
        if (args.Length >= 3)
        {
            seed = args[2].GetHashCode();
            for (int i = 3; i < args.Length; i++)
                seed ^= args[i].GetHashCode(); //rare compound bitwise :O
        }
        else
        {
            seed = (int)DateTime.Now.Ticks;
        }

        var real = new Random(seed).NextDouble();
        var imaginary = new Random(seed).NextDouble();
        BigComplex min, max;

        if (args.Length == 1)
        {
            min = 0;
            max = args[0];
        }
        else
        {
            min = args[0];
            max = args[1];
        }

        var realDiff = max.Real - min.Real;
        var imagDiff = max.Imaginary - min.Imaginary;

        realDiff *= real;
        imagDiff *= imaginary;

        realDiff += min.Real;
        imagDiff += min.Imaginary;

        return new BigComplex(realDiff, imagDiff);
    }

    [Function("Crash")]
    public static void Crash() { } // no arguments so it crashes

    [Function("MaxDigits")]
    public static BigComplex MaxDigits(params BigComplex[] args)
    {
        if (args.Length == 1)
            Equation.DecimalPrecision = (int)args[0].Real;
        return Equation.DecimalPrecision;
    }

    [Function("Echo")]
    public static BigComplex Echo(params BigComplex[] args) => args[0];

    [Function("Min")]
    public static BigComplex Min(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        var min = args[0];
        BigRational minLength = Abs(min).Real;

        for (int i = 1; i < args.Length; i++)
        {
            var current = Abs(args[i]).Real;
            if (current > minLength)
                continue;

            min = args[i];
            minLength = current;
        }
        return min;
    }

    [Function("Max")]
    public static BigComplex Max(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        var max = args[0];
        BigRational maxLength = Abs(max).Real;

        for (int i = 1; i < args.Length; i++)
        {
            var current = Abs(args[i]).Real;
            if (current < maxLength)
                continue;

            max = args[i];
            maxLength = current;
        }
        return max;
    }

    [Function("Mean")]
    public static BigComplex Mean(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        var total = Operators.Add(args);
        return total / args.Length;
    }

    [Function("Len")]
    public static BigComplex Len(params BigComplex[] args) => args.Length;

    [Function("Real")]
    public static BigComplex Real(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return args[0].Real;
    }

    [Function("Imaginary")]
    public static BigComplex Imaginary(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return new(0, args[0].Imaginary);
    }

    [Function("Round", Args = "Round(number, nearest)")]
    public static BigComplex Round(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Round(args[0], 1);

        var real = BigRational.Round(args[0].Real / args[1].Real) * args[1].Real;
        var imag = BigRational.Round(args[0].Imaginary / args[1].Real) * args[1].Real;

        if (args[0].Real == args[1].Real / 2)
            real = args[1].Real;
        if (args[0].Imaginary == args[1].Real / 2)
            imag = args[1].Real;

        return new BigComplex(real, imag);
    }

    [Function("Floor", Args = "Floor(number, nearest)")]
    public static BigComplex Floor(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Floor(args[0], 1);

        var real = BigRational.Floor(args[0].Real / args[1].Real) * args[1].Real;
        var imag = BigRational.Floor(args[0].Imaginary / args[1].Real) * args[1].Real;
        return new BigComplex(real, imag);
    }

    [Function("Ceiling", Args = "Ceiling(number, nearest)")]
    public static BigComplex Ceiling(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Ceiling(args[0], 1);

        var real = BigRational.Ceiling(args[0].Real / args[1].Real) * args[1].Real;
        var imag = BigRational.Ceiling(args[0].Imaginary / args[1].Real) * args[1].Real;
        return new BigComplex(real, imag);
    }

    //fun fact this used to have the operator "|" because I didn't implement operator order yet
    [Function("Halfway")]
    public static BigComplex Halfway(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0; //halfway between all numbers ;)
        if (args.Length == 1)
            return args[0] / 2;

        BigRational minReal = args[0].Real, maxReal = args[0].Real, minImaginary = args[0].Imaginary, maxImaginary = args[0].Imaginary;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].Real < minReal)
                minReal = args[i].Real;
            else if (args[i].Real > maxReal)
                maxReal = args[i].Real;

            if (args[i].Imaginary < minImaginary)
                minImaginary = args[i].Imaginary;
            else if (args[i].Imaginary > maxImaginary)
                maxImaginary = args[i].Imaginary;
        }

        var middleReal = (maxReal + minReal) / 2;
        var middleImag = (maxImaginary + minImaginary) / 2;

        return new BigComplex(middleReal, middleImag);
    }

    [Function("Lerp", Args = "Lerp(a, b, t)")]
    public static BigComplex Lerp(params BigComplex[] args)
    {
        // (b - a) * t + a

        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];
        if (args.Length == 2)
            return Lerp(args[0], args[1], BigRational.Parse(".5"));

        var a = args[0];
        var b = args[1];
        var t = args[2];

        return (b - a) * t + a;
    }

    [Function("Convert", Args = "Convert(num, fromUnit, toUnit)")]
    public static BigComplex Convert(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];
        if (args.Length == 2)
            return args[0] / args[1];

        return (args[0] * args[1]) / args[2];
    }

    [Function("ConvertTemperature", Args = "ConvertTemperature(num, fromUnit, toUnit)")]
    public static BigComplex ConvertTemperature(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];
        if (args.Length == 2)
            return CelsiusTo(args[0], args[1]);

        var celsius = ToCelsius(args[0], args[1]);
        return CelsiusTo(celsius, args[2]);

        static BigComplex ToCelsius(BigComplex val, BigComplex unit)
        {
            return (int)unit.Real switch
            {
                Conversions.Celsius => val,
                Conversions.Fahrenheit => (val - 32) * 5 / 9,
                Conversions.Kelvin => val - BigRational.Parse("273.15"),
                Conversions.Rankine => (val - BigRational.Parse("491.67")) * 5 / 9,
                _ => val,
            };
        }

        static BigComplex CelsiusTo(BigComplex val, BigComplex unit)
        {
            return (int)unit.Real switch
            {
                Conversions.Celsius => val,
                Conversions.Fahrenheit => (val * 9 / 5) + 32,
                Conversions.Kelvin => val + BigRational.Parse("273.15"),
                Conversions.Rankine => (val * 9 / 5) + BigRational.Parse("491.67"),
                _ => val,
            };
        }
    }
}
