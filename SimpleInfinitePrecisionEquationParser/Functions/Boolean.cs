using System.Numerics;

namespace SIPEP.Functions;

public static class Boolean
{
    [Function("And", Operator = '&', Priority = 2)]
    public static BigComplex And(params BigComplex[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (!args[i].BoolValue)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Or", Operator = '|', Priority = 3)]
    public static BigComplex Or(params BigComplex[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].BoolValue)
                return BigComplex.True;
        }
        return BigComplex.False;
    }

    [Function("Xor")]
    public static BigComplex Xor(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.False;
        bool checkingVal = args[0].BoolValue;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].BoolValue == checkingVal)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Not", Operator = '¬', OperatorStyle = OperatorStyle.Right, Priority = 1)]
    public static BigComplex Not(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.True;
        return new BigComplex(!args[0].BoolValue);
    }

    [Function("Equals", Operator = '=', Priority = 5, HandlesInfinity = true)]
    public static BigComplex Equals(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.True;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] != args[i - 1])
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Greater", Operator = '>', Priority = 5, HandlesInfinity = true)]
    public static BigComplex Greater(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.False;

        if (args[0].IsInfinity)
            return args[0].Real > 0;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].IsInfinity && args[i].Real > 0)
                return BigComplex.False;
            if (Misc.AbsSigned(args[i]).Real >= Misc.AbsSigned(args[i - 1]).Real)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Less", Operator = '<', Priority = 5, HandlesInfinity = true)]
    public static BigComplex Less(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.False;

        if (args[0].IsInfinity)
            return args[0].Real < 0;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].IsInfinity && args[i].Real > 0)
                return BigComplex.True;
            if (Misc.AbsSigned(args[i]).Real <= Misc.AbsSigned(args[i - 1]).Real)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Approx", Operator = '~', Priority = 5)]
    public static BigComplex Approx(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.True;
        if (args.Length == 2)
            return Approx(args[0], args[1], BigRational.Parse(".1"));

        if (args[0].IsInfinity)
        {
            if (args[1].IsInfinity)
                return args[0].Real == args[1].Real;
            return false;
        }
        if (args[1].IsInfinity)
            return false;

        var diffReal = BigRational.Abs(args[0].Real - args[1].Real);
        var diffImag = BigRational.Abs(args[0].Imaginary - args[1].Imaginary);

        bool real = diffReal <= args[2].Real;
        bool imag = diffImag <= args[2].Real;

        return real && imag;
    }

    [Function("Domain", StringArguments = true)]
    public static BigComplex Domain(Dictionary<string, Variable> vars, string[] args)
    {
        if (args.Length == 0)
            return false;
        if (args.Length == 1)
            return true;
        var numbers = new Equation(args[0], vars).SolveValues();

        for (int i = 0; i < numbers.Length; i++)
        {
            if (!IsInDomain(numbers[i], args[1]))
                return false;
        }
        return true;

        static bool IsInDomain(BigComplex number, string domain)
        {
            domain = domain.ToUpper().Trim();
            if (domain == "*")
                return true;

            if (number.IsInfinity)
                return domain == "INF" || domain == "#";
            if (number.IsBoolean)
                return domain == "BOOL" || domain == "B";
            // Bigrational.IsNan doesn't work
            if (number.Real.ToString() == "NaN" || number.Imaginary.ToString() == "NaN")
                return domain == "NAN" || domain == "?";

            return domain switch
            {
                "N" or "NATURAL" => number.Real >= 0 && number.Imaginary == 0 && number.Real % 1 == 0,
                "N1" or "NATURAL-1" => number.Real > 0 && number.Imaginary == 0 && number.Real % 1 == 0,
                "Z" or "INTEGER" => number % 1 == 0 && number.Imaginary == 0,
                "Q" or "RATIONAL" => number.Imaginary == 0 && !IsPiOrE(number.Real), // can't rlly find a good way to implement
                "R" or "REAL" => number.Imaginary == 0,
                "I" or "IMAGINARY" => number.Real == 0,
                "C" or "COMPLEX" => true,
                _ => false,
            };
        }

        static bool IsPiOrE(BigRational r)
        {
            if (r == 0) return false;
            if ((r / Constants.Pi).IsInteger) return true;
            if ((r * Constants.Pi).IsInteger) return true;
            if ((r / Constants.E).IsInteger) return true;
            if ((r * Constants.E).IsInteger) return true;
            return false;
        }
    }
}
