using SIPEP.Functions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SIPEP;

public struct BigComplex
{
    public static readonly BigComplex Zero = 0;
    public static readonly BigComplex One = 1;
    public static readonly BigComplex ImaginaryOne = new(0, 1);
    public static readonly BigComplex True = new(true);
    public static readonly BigComplex False = new(false);

    public BigRational Real { get; set; }
    public BigRational Imaginary { get; set; }
    public bool BoolValue { get => Real > 0; set => Real = value ? 1 : -1; }
    public bool IsBoolean = false;

    public BigComplex(BigRational real, BigRational imaginary)
    {
        Real = real;
        Imaginary = imaginary;
        IsBoolean = false;
    }

    public BigComplex(bool value)
    {
        IsBoolean = true;
        Real = new BigRational(value ? 1 : -1);
        Imaginary = new BigRational(0);
    }

    public static BigComplex operator +(BigComplex left, BigComplex right) =>
        new(left.Real + right.Real, left.Imaginary + right.Imaginary);

    public static BigComplex operator -(BigComplex left, BigComplex right) =>
        new(left.Real - right.Real, left.Imaginary - right.Imaginary);

    public static BigComplex operator -(BigComplex num) =>
        new(-num.Real, -num.Imaginary);

    public static BigComplex operator *(BigComplex left, BigComplex right)
    {
        // Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
        BigRational result_realpart = (left.Real * right.Real) - (left.Imaginary * right.Imaginary);
        BigRational result_imaginarypart = (left.Imaginary * right.Real) + (left.Real * right.Imaginary);
        return new BigComplex(result_realpart, result_imaginarypart);
    }

    public static implicit operator BigComplex(BigRational a) => new(a, 0);
    public static implicit operator BigComplex(int a) => new(a, 0);
    public static implicit operator BigComplex(bool a) => a ? True : False;
    public static explicit operator BigRational(BigComplex a) => a.Real;

    public static BigComplex operator /(BigComplex left, BigComplex right)
    {
        // Division : Smith's formula.
        BigRational a = left.Real;
        BigRational b = left.Imaginary;
        BigRational c = right.Real;
        BigRational d = right.Imaginary;

        // Computing c * c + d * d will overflow even in cases where the actual result of the division does not overflow.
        if (BigRational.Abs(d) < BigRational.Abs(c))
        {
            BigRational doc = d / c;
            return new BigComplex((a + b * doc) / (c + d * doc), (b - a * doc) / (c + d * doc));
        }
        else
        {
            BigRational cod = c / d;
            return new BigComplex((b + a * cod) / (d + c * cod), (-a + b * cod) / (d + c * cod));
        }
    }

    public static bool operator ==(BigComplex left, BigComplex right) =>
        left.Real == right.Real && left.Imaginary == right.Imaginary;

    public static bool operator !=(BigComplex left, BigComplex right) =>
        !(left == right);

    public static BigComplex operator %(BigComplex left, BigComplex right)
    {
        var div = left / right;
        BigRational real = div.Real, imag = div.Imaginary;

        if (real < 0)
            real = Misc.Ceiling(div).Real;
        else
            real = Misc.Floor(div).Real;

        if (real < 0)
            imag = Misc.Ceiling(div).Imaginary;
        else
            imag = Misc.Floor(div).Imaginary;

        div = new BigComplex(real, imag);
        return left - right * (BigComplex)(div);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginary.GetHashCode();
    }

    public static bool TryParse(string s, out BigComplex result)
    {
        result = Zero;

        var splitByI = s.Split('i');

        if (splitByI.Length == 0 || splitByI.Length >= 3)
            return false;

        if (!IsNumber(splitByI[0]))
            return false;

        BigRational real, imaginary = 0;

        real = BigRational.Parse(splitByI[0]);

        if (splitByI.Length == 2)
        {
            if (!IsNumber(splitByI[1]))
                return false;

            splitByI[1] = splitByI[1].Replace("m", "-");
            imaginary = BigRational.Parse(splitByI[1]);
            if (imaginary == 0 && splitByI[1] != "0")
                return false;
        }

        if (s == "e")
            return false;

        result = new BigComplex(real, imaginary);
        return true;
    }

    private static bool IsNumber(string s)
    {
        if (s.Length == 0)
            return false;

        bool foundPoint = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] >= '0' && s[i] <= '9')
                continue;
            if (s[i] == '.' && !foundPoint)
            {
                foundPoint = true;
                continue;
            }
            if (s[i] == '\'')
                continue;

            if (s[i] == '-' || s[i] == '+' || s[i] == 'e')
                continue;

            return false;
        }

        return true;
    }

    public override string ToString()
    {
        if (IsBoolean)
            return (Real > 0).ToString();

        if (Imaginary == 0)
            return Real.ToString();
        return $"{Real} + {Imaginary}i";
    }

    public string ToParsableString()
    {
        if (IsBoolean)
            return (Real > 0).ToString();

        if (Imaginary == 0)
            return Real.ToString();
        string imag = Imaginary.ToString().Replace("-", "m");
        return $"{Real}i{imag}";
    }
}
