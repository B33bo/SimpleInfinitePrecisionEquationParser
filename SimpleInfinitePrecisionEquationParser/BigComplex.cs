using SIPEP.Functions;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SIPEP;

public struct BigComplex
{
    public static readonly BigComplex Zero = 0;
    public static readonly BigComplex One = 1;
    public static readonly BigComplex ImaginaryOne = new(0, 1);
    public static readonly BigComplex True = new(true);
    public static readonly BigComplex False = new(false);
    public static readonly BigComplex Infinity = new(true, 1, 0);
    public static readonly BigComplex NaN = new((BigRational)0 / 0, 0);

    public BigRational Real { get; set; }
    public BigRational Imaginary { get; set; }
    public bool BoolValue { get => Real > 0; set => Real = value ? 1 : -1; }
    public bool IsInfinity { get; set; }
    public bool IsBoolean = false;
    public bool IsInteger => Imaginary == 0 && Real % 1 == 0;

    public BigComplex(BigRational real, BigRational imaginary)
    {
        Real = real;
        Imaginary = imaginary;
        IsBoolean = false;
        IsInfinity = false;
    }

    public BigComplex(bool value)
    {
        IsBoolean = true;
        Real = new BigRational(value ? 1 : -1);
        Imaginary = new BigRational(0);
        IsInfinity = false;
    }

    public BigComplex(bool isInfinity, BigRational real, BigRational imag)
    {
        Real = real;
        Imaginary = imag;
        IsInfinity = isInfinity;
    }

    public static BigComplex operator +(BigComplex left, BigComplex right)
    {
        if (left.IsInfinity || right.IsInfinity)
        {
            if (left.IsInfinity && right.IsInfinity)
                return new(true, left.Real * right.Real, 0);
            if (left.IsInfinity)
                return left;
            return right;
        }
        return new(left.Real + right.Real, left.Imaginary + right.Imaginary);
    }

    public static BigComplex operator -(BigComplex left, BigComplex right)
    {
        if (left.IsInfinity || right.IsInfinity)
        {
            if (left.IsInfinity && right.IsInfinity)
                return new(true, left.Real * right.Real * -1, 0);
            if (left.IsInfinity)
                return left;
            return -right;
        }
        return new(left.Real - right.Real, left.Imaginary - right.Imaginary);
    }

    public static BigComplex operator -(BigComplex num) =>
        new(num.IsInfinity, -num.Real, -num.Imaginary);

    public static BigComplex operator *(BigComplex left, BigComplex right)
    {
        if (left.IsInfinity || right.IsInfinity)
        {
            if (left.IsInfinity && right == 0 || right.IsInfinity && left == 0)
                return 0;
            return new BigComplex(true, left.Real * right.Real, 0);
        }
        if (left == 0 || right == 0)
            return 0;
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
        if (left.IsInfinity)
            return new BigComplex(true, left.Real * right.Real, left.Imaginary);

        if (right.IsInfinity)
            return 0;

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

    public static bool operator ==(BigComplex left, BigComplex right)
    {
        if (left.IsInfinity)
        {
            if (!right.IsInfinity)
                return false;
            if (left.Real >= 0 && right.Real >= 0)
                return true;
            if (left.Real < 0 && right.Real < 0)
                return true;
            return false;
        }
        if (right.IsInfinity)
            return false;
        return left.Real == right.Real && left.Imaginary == right.Imaginary;
    }

    public static bool operator !=(BigComplex left, BigComplex right) =>
        !(left == right);

    public static BigComplex operator %(BigComplex left, BigComplex right)
    {
        if (left.IsInfinity)
            return 0;
        if (right.IsInfinity)
            return 0;

        var div = left / right;
        BigRational real = div.Real, imag;

        if (real < 0)
            real = Misc.Ceiling(div).Real;
        else
            real = Misc.Floor(div).Real;

        if (real < 0)
            imag = Misc.Ceiling(div).Imaginary;
        else
            imag = Misc.Floor(div).Imaginary;

        div = new BigComplex(real, imag);
        return left - right * div;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is int intVal)
            return this == intVal;
        if (obj is bool boolVal)
            return this == boolVal;
        if (obj is BigRational rationalVal)
            return this == rationalVal;
        if (obj is BigComplex other)
            return this == other;
        return false;
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

            imaginary = BigRational.Parse(splitByI[1]);
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
        if (BigRational.IsNaN(Real) || BigRational.IsNaN(Imaginary))
            return "NaN";

        if (IsBoolean)
            return (Real > 0).ToString();

        if (IsInfinity)
            return Real >= 0 ? "infinity" : "-infinity";

        if (Imaginary == 0)
            return Real.ToString();
        return $"{Real} + {Imaginary}i";
    }

    public string ToString(int precision)
    {
        if (Real == NaN.Real || Imaginary == NaN.Real)
            // BigRational.IsNan doesn't work
            return "NaN";

        if (IsBoolean)
            return (Real > 0).ToString();

        string precisionText = "0.";
        for (int i = 0; i < precision; i++)
            precisionText += '#';

        if (IsInfinity)
            return Real >= 0 ? "infinity" : "-infinity";

        if (this == 0)
            return "0";

        if (Imaginary == 0)
            return Real.ToString(precisionText);
        if (Real == 0)
            return $"{Imaginary.ToString(precisionText)}i";

        return $"{Real.ToString(precisionText)} + {Imaginary.ToString(precisionText)}i";
    }
}
