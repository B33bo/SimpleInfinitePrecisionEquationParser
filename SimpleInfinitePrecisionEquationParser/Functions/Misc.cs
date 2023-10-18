using System.Numerics;

namespace SIPEP.Functions;

public static class Misc
{
    [Function("Abs", HandlesInfinity = true)]
    public static BigComplex Abs(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args[0].IsInfinity)
            return new BigComplex(true, 1, 0);
        BigComplex val = args[0];
        return BigRational.Sqrt(val.Real * val.Real + val.Imaginary * val.Imaginary, Equation.DecimalPrecision);
    }

    [Function("Remainder", HandlesInfinity = true)]
    public static BigComplex Remainder(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];

        var number = args[0];
        for (int i = 1; i < args.Length; i++)
            number %= args[i];
        return number;
    }

    [Function("AbsSigned", HandlesInfinity = true)]
    public static BigComplex AbsSigned(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        var num = Abs(args[0]);
        var abs1 = BigRational.Sign(args[0].Real);
        var abs2 = BigRational.Sign(args[0].Imaginary);

        if (abs1 != 0)
            num *= abs1;
        if (abs2 != 0)
            num *= abs2;
        return num;
    }

    [Function("Sign", HandlesInfinity = true)]
    public static BigComplex Sign(params BigComplex[] args)
    {
        //sign = abs(x) / x
        if (args.Length == 0)
            return 0;
        if (args[0] == 0)
            return 0;
        if (args[0].IsInfinity)
            return args[0].Real >= 0 ? 1 : -1;

        return Abs(args[0]) / args[0];
    }

    [Function("Log", Args = "Log(number, ?Base)", HandlesInfinity = true)]
    public static BigComplex Log(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Log(args[0], 10);

        if (args[0].IsInfinity)
            return args[0];
        if (args[1].IsInfinity)
            return BigComplex.NaN;

        return logQuick(args[0]) / logQuick(args[1]);

        static BigComplex logQuick(BigComplex a)
        {
            return new BigComplex(BigRational.Log(Abs(a).Real, Equation.DecimalPrecision), BigRational.Atan2(a.Imaginary, a.Real, Equation.DecimalPrecision));
        }
    }

    [Function("Exp", HandlesInfinity = true)]
    public static BigComplex Exp(params BigComplex[] args)
    {
        if (args.Length == 0)
            return Constants.E;
        return Operators.Pow(Constants.E, args[0]);
    }

    [Function("Rand", Args = "Rand(min, max, ?Seed)", StaticFunction = false)]
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

    [Function("Crash", StaticFunction = false)]
    public static void Crash() { } // no arguments so it crashes

    [Function("MaxDigits", StaticFunction = false)]
    public static BigComplex MaxDigits(params BigComplex[] args)
    {
        if (args.Length == 1)
            Equation.DecimalPrecision = (int)args[0].Real;
        return Equation.DecimalPrecision;
    }

    [Function("Echo", HandlesInfinity = true)]
    public static BigComplex Echo(params BigComplex[] args) => args.Length > 0 ? args[0] : 0;

    [Function("Length", HandlesInfinity = true)]
    public static BigComplex Len(params BigComplex[] args) => args.Length;

    [Function("Re")]
    public static BigComplex Real(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return args[0].Real;
    }

    [Function("Im")]
    public static BigComplex Imaginary(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return args[0].Imaginary;
    }

    [Function("Round", Args = "Round(number, nearest)", HandlesInfinity = true)]
    public static BigComplex Round(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Round(args[0], 1);

        if (args[0].IsInfinity)
            return args[0];
        if (args[1].IsInfinity)
            return 0;

        var real = BigRational.Round(args[0].Real / args[1].Real) * args[1].Real;
        var imag = BigRational.Round(args[0].Imaginary / args[1].Real) * args[1].Real;

        if (args[0].Real == args[1].Real / 2)
            real = args[1].Real;
        if (args[0].Imaginary == args[1].Real / 2)
            imag = args[1].Real;

        return new BigComplex(real, imag);
    }

    [Function("Floor", Args = "Floor(number, nearest)", HandlesInfinity = true)]
    public static BigComplex Floor(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Floor(args[0], 1);

        if (args[0].IsInfinity)
            return args[0];

        if (args[1].IsInfinity)
            return args[0].Real < 0 ? new BigComplex(true, -1, 0) : 0;

        var real = BigRational.Floor(args[0].Real / args[1].Real) * args[1].Real;
        var imag = BigRational.Floor(args[0].Imaginary / args[1].Real) * args[1].Real;
        return new BigComplex(real, imag);
    }

    [Function("Ceiling", Args = "Ceiling(number, nearest)", HandlesInfinity = true)]
    public static BigComplex Ceiling(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Ceiling(args[0], 1);

        if (args[0].IsInfinity)
            return args[0];

        if (args[1].IsInfinity)
            return args[0].Real > 0 ? new BigComplex(true, 1, 0) : 0;

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

    [Function("Lerp", Args = "Lerp(a, b, t)", HandlesInfinity = true)]
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

    [Function("Convert", Args = "Convert(num, fromUnit, toUnit)", HandlesInfinity = true)]
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

    [Function("ConvertTemperature", Args = "ConvertTemperature(num, fromUnit, toUnit)", StringArguments = true)]
    public static BigComplex ConvertTemperature(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length == 0)
            return 0;

        var number = new Equation(args[0]).Solve();
        if (args.Length == 1)
            return number;
        if (args.Length == 2)
            return CelsiusTo(number, args[1]);

        var celsius = ToCelsius(number, args[1]);
        return CelsiusTo(celsius, args[2]);

        static BigComplex ToCelsius(BigComplex val, string unit)
        {
            return unit.ToLower().Trim(' ') switch
            {
                "celsius" => val,
                "centigrade" => val,
                "fahrenheit" => (val - 32) * 5 / 9,
                "kelvin" => val - BigRational.Parse("273.15"),
                "rankine" => (val - BigRational.Parse("491.67")) * 5 / 9,
                _ => BigComplex.NaN,
            };
        }

        static BigComplex CelsiusTo(BigComplex val, string unit)
        {
            return unit.ToLower().Trim(' ') switch
            {
                "celsius" => val,
                "centigrade" => val,
                "fahrenheit" => (val * 9 / 5) + 32,
                "kelvin" => val + BigRational.Parse("273.15"),
                "rankine" => (val * 9 / 5) + BigRational.Parse("491.67"),
                _ => BigComplex.NaN,
            };
        }
    }

    [Function("Time", Args = "Time(unit)", HandlesInfinity = true, StaticFunction = false)]
    public static BigComplex Time(params BigComplex[] args)
    {
        if (args.Length == 0)
            return new BigComplex(DateTime.Now.Ticks / TimeSpan.TicksPerSecond, 0);
        return new BigComplex(DateTime.Now.Ticks / TimeSpan.TicksPerSecond / args[0].Real, 0);
    }

    [Function("TimeUTC", Args = "TimeUTC(unit)", HandlesInfinity = true)]
    public static BigComplex TimeUTC(params BigComplex[] args)
    {
        if (args.Length == 0)
            return new BigComplex(DateTime.UtcNow.Ticks, 0);
        return new BigComplex(DateTime.UtcNow.Ticks / args[0].Real, 0);
    }

    [Function("Int")]
    public static BigComplex Int(params BigComplex[] args)
    {
        if (args.Length == 0)
            return new BigComplex(0, 0);
        return new BigComplex(args[0].Real - args[0].Real % 1, args[0].Imaginary - args[0].Imaginary % 1);
    }

    [Function("Frac")]
    public static BigComplex Frac(params BigComplex[] args)
    {
        if (args.Length == 0)
            return new BigComplex(0, 0);
        return new BigComplex(args[0].Real % 1, args[0].Imaginary % 1);
    }

    [Function("If", HandlesInfinity = true, StringArguments = true)]
    public static BigComplex If(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length == 0)
            return 0;

        bool istrue = new Equation(args[0], vars).SolveBoolean();
        if (args.Length == 1)
            return istrue;

        if (args.Length == 2)
        {
            if (istrue)
                return new Equation(args[1], vars).Solve();
            return istrue;
        }

        return istrue ? new Equation(args[1], vars).Solve() : new Equation(args[2], vars).Solve();
    }

    [Function("DataVal", Args = "DataVal(Real, Imaginary, Is Boolean, Is Infinity)", HandlesInfinity = true)]
    public static BigComplex DataVal(params BigComplex[] args)
    {
        BigComplex data = new();

        if (args.Length >= 1)
            data.Real = args[0].Real;

        if (args.Length >= 2)
            data.Imaginary = args[1].Real;

        if (args.Length >= 3)
            data.IsBoolean = args[2].BoolValue;

        if (args.Length >= 4)
            data.IsInfinity = args[3].BoolValue;

        return data;
    }

    [Function("StringLength", StringArguments = true)]
    public static BigComplex StringLength(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length == 0)
            return -1;
        return args[0].Length;
    }

    [Function("Sum", Args = "Sum(i = start, end, equation)", StringArguments = true)]
    public static BigComplex Sum(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length != 3)
            return BigComplex.NaN;
        //sum (i = start, end, equation)
        string[] startText = args[0].Replace(" ", "").Split('=');

        if (startText.Length != 2)
            return BigComplex.NaN;

        string varName = startText[0];
        BigInteger start = (BigInteger)new Equation(startText[1], vars).Solve().Real;
        BigInteger end = (BigInteger)new Equation(args[1], vars).Solve().Real;

        Equation eq = new(args[2], vars);

        if (!eq.Variables.ContainsKey(varName))
            eq.Variables.Add(varName, (BigComplex)0);

        BigComplex value = 0;

        for (BigInteger i = start; i <= end; i++)
        {
            eq.Variables[varName] = (BigComplex)i;
            value += eq.Solve();
        }
        return value;
    }

    [Function("Product", Args = "Product(i = start, end, equation)", StringArguments = true)]
    public static BigComplex Product(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length != 3)
            return BigComplex.NaN;
        //product (i = start, end, equation)
        string[] startText = args[0].Replace(" ", "").Split('=');

        if (startText.Length != 2)
            return BigComplex.NaN;

        string varName = startText[0];
        BigInteger start = (BigInteger)new Equation(startText[1], vars).Solve().Real;
        BigInteger end = (BigInteger)new Equation(args[1], vars).Solve().Real;

        Equation eq = new(args[2], vars);

        if (!eq.Variables.ContainsKey(varName))
            eq.Variables.Add(varName, (BigComplex)0);

        BigComplex value = 1;

        for (BigInteger i = start; i <= end; i++)
        {
            eq.Variables[varName] = (BigComplex)i;
            value *= eq.Solve();
        }

        return value;
    }

    [Function("Integral", Args = "Integral(start, end, intervals, equation, varname?)", StringArguments = true)]
    public static BigComplex Integral(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length < 4)
            return BigComplex.NaN;

        string variableName = args.Length > 4 ? args[4].Trim(' ') : "x";

        BigComplex start = new Equation(args[0], vars).Solve();
        BigComplex end = new Equation(args[1], vars).Solve();
        BigInteger intervals = (BigInteger)new Equation(args[2], vars).Solve().Real;
        Equation equation = new(args[3], vars);

        BigComplex sum = 0;
        BigComplex dx = (end - start) / new BigComplex(intervals, 0);

        var half = BigComplex.One / 2;

        if (!equation.Variables.ContainsKey(variableName))
            equation.Variables.Add(variableName, (BigComplex)0);

        for (BigInteger i = 0; i < intervals; i++)
        {
            var t = start + (half + new BigComplex(i, 0)) * dx;

            equation.Variables[variableName] = t;
            sum += equation.Solve();
        }

        return sum * dx;
    }

    [Function("Derivative", Args = "Derivative(equation, epsilon, initial Variable, varname?)", StringArguments = true)]
    public static BigComplex Derivative(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length < 3)
            return BigComplex.NaN;

        string varname = "x";

        if (args.Length > 3)
            varname = args[3].Trim(' ');

        BigComplex initial = new Equation(args[2], vars).Solve();

        if (vars.ContainsKey(varname))
            vars[varname] = initial;
        else
            vars.Add(varname, initial);

        Equation eq = new(args[0], vars);
        BigComplex epsilon = new Equation(args[1], vars).Solve();

        var first = eq.Solve();
        eq.Variables[varname] += epsilon;
        var second = eq.Solve();

        return (second - first) / epsilon;
    }

    [Function("For", Args = "For(start, keep running, after iteration, equation, varname?, initial value?)", StringArguments = true)]
    public static BigComplex For(Dictionary<string, Variable> vars, params string[] args)
    {
        string varname = args.Length > 4 ? args[4].Trim(' ') : "current";

        string keepRunning = args[1];
        string afterIteration = args[2];
        string equation = args[3];

        BigComplex current = args.Length > 5 ? new Equation(args[5], vars).Solve() : 0;
        if (vars.ContainsKey(varname))
            vars[varname] = current;
        else
            vars.Add(varname, current);

        Equation eq = new(args[0], vars); //let i = 0
        eq.Solve();
        eq.Parse(keepRunning);

        while (eq.SolveBoolean())
        {
            eq.Parse(equation);
            current = eq.Solve();
            eq.Variables[varname] = current;

            eq.Parse(afterIteration);
            eq.Solve();
            eq.Parse(keepRunning);
        }

        return current;
    }

    [Function("Repeat", Args = "Repeat(equation, iterations, varname, initial value)", StringArguments = true)]
    public static BigComplex Repeat(Dictionary<string, Variable> vars, params string[] args)
    {
        if (args.Length < 4)
            return BigComplex.NaN;
        Equation eq = new(args[0], vars);

        var initialVal = new Equation(args[3], vars).Solve();
        args[2] = args[2].Replace(" ", "");
        eq.SetVariable(args[2], initialVal);

        BigInteger iterations = (BigInteger)new Equation(args[1], vars).Solve().Real;

        for (BigInteger i = 0; i < iterations - 1; i++)
        {
            eq.SetVariable(args[2], eq.Solve());
        }

        return eq.Solve();
    }

    [Function("lcm", Args = "LCM(numbers)")]
    public static BigComplex LCM(params BigComplex[] args)
    {
        if (args.Length == 1)
            return args[0];
        if (args.Length == 0)
            return 1;

        var lcm = CalculateLCM(args[0], args[1]);
        for (int i = 2; i < args.Length; i++)
            lcm = CalculateLCM(lcm, args[i]);

        return lcm;

        static BigComplex CalculateLCM(BigComplex num1, BigComplex num2)
        {
            return (num1 * num2) / GCD(num1, num2);
        }
    }

    [Function("gcd", Args = "GCD(numbers)")]
    public static BigComplex GCD(params BigComplex[] args)
    {
        if (args.Length == 1)
            return args[0];
        if (args.Length == 0)
            return 1;

        var gcd = CalculateGCD(args[0], args[1]);

        for (int i = 2; i < args.Length; i++)
            gcd = CalculateGCD(gcd, args[i]);

        return gcd;

        static BigComplex CalculateGCD(BigComplex num1, BigComplex num2)
        {
            if (num2 == 0)
                return num1;
            return CalculateGCD(num2, num1 % num2);
        }
    }

    [Function("nCr", Args = "NCR(n, r)")]
    public static BigComplex NCR(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return 0;
        return NPR(args[0], args[1]) / Operators.Factorial(args[1]);
    }

    [Function("nPr", Args = "NPR(n, r)")]
    public static BigComplex NPR(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return 0;
        return Operators.Factorial(args[0]) / Operators.Factorial(args[0] - args[1]);
    }
}
