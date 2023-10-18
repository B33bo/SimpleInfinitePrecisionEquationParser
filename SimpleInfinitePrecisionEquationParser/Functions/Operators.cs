using System.Numerics;

namespace SIPEP.Functions;

public static class Operators
{
    [Function("Add", Operator = '+', Priority = 3, HandlesInfinity = true)]
    public static BigComplex Add(params BigComplex[] args)
    {
        BigComplex val = 0;
        for (int i = 0; i < args.Length; i++)
            val += args[i];
        return val;
    }

    [Function("Subtract", Operator = '-', Priority = 3, OperatorStyle = OperatorStyle.LeftAndRight | OperatorStyle.Right, HandlesInfinity = true)]
    public static BigComplex Subtract(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Subtract(0, args[0]);

        BigComplex val = args[0];
        for (int i = 1; i < args.Length; i++)
            val -= args[i];
        return val;
    }

    [Function("Multiply", HandlesInfinity = true)]
    public static BigComplex Multiply(params BigComplex[] args)
    {
        BigComplex val = 1;
        for (int i = 0; i < args.Length; i++)
            val *= args[i];
        return val;
    }

    // multiplyOperator because one input means to radians
    [Function("MultiplyOperator", Operator = '*', Priority = 2, OperatorStyle = OperatorStyle.LeftAndRight | OperatorStyle.Left, HandlesInfinity = true)]
    public static BigComplex MultiplyOperator(params BigComplex[] args)
    {
        if (args.Length == 1)
            return args[0].Conjugate; //to radians
        return Multiply(args);
    }

    [Function("Conjugate")]
    public static BigComplex Conjugate(params BigComplex[] args) => args.Length == 0 ? 0 : args[0].Conjugate;

    [Function("Divide", Operator = '/', Priority = 2, HandlesInfinity = true)]
    public static BigComplex Divide(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        BigComplex val = args[0];
        for (int i = 1; i < args.Length; i++)
            val /= args[i];
        return val;
    }

    [Function("Pow", Operator = '^', Args = "Pow(base, exponent)", Priority = 1, HandlesInfinity = true)]
    public static BigComplex Pow(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 1;
        if (args.Length == 1)
            return args[0];

        BigComplex current = args[0];
        for (int i = 1; i < args.Length; i++)
        {
            bool isInt = args[i].Real - args[i].Real % 1 == args[i].Real;
            if (args[i].Imaginary == 0 && isInt && args[i].Real > 0)
            {
                if (args[i].IsInfinity)
                    current = new(true, args[i].Real, 0);
                current = IntPow(current, (BigInteger)args[i].Real);
                continue;
            }
            current = Power(current, args[i]);
        }

        return current;

        static BigComplex Power(BigComplex value, BigComplex exponent)
        {
            if (exponent == 0)
                return 1;

            if (value == 0)
                return value; //zero

            if (value.IsInfinity)
                return new(true, value.Real * exponent.Real, 0);
            if (exponent.IsInfinity)
                return new(true, exponent.Real, 0);

            int precistion = Equation.DecimalPrecision;

            BigRational valueReal = value.Real;
            BigRational valueImaginary = value.Imaginary;
            BigRational exponentReal = exponent.Real;
            BigRational exponentImaginary = exponent.Imaginary;

            BigRational rho = Misc.Abs(value).Real;
            BigRational theta = BigRational.Atan2(valueImaginary, valueReal, Equation.DecimalPrecision);
            BigRational newRho = exponentReal * theta + exponentImaginary * BigRational.Log(rho, precistion);

            BigRational t = BigRational.Pow(rho, exponentReal, precistion) * BigRational.Pow(Constants.E.Real, -exponentImaginary * theta, Equation.DecimalPrecision);

            return new BigComplex(t * BigRational.Cos(newRho, Equation.DecimalPrecision), t * BigRational.Sin(newRho, Equation.DecimalPrecision));
        }

        static BigComplex IntPow(BigComplex value, BigInteger exponent)
        {
            BigComplex currVal = 1;
            for (int i = 0; i < exponent; i++)
                currVal *= value;
            return currVal;
        }
    }

    [Function("Root", Operator = '\\', Args = "Root(root, number)", Priority = 1, OperatorStyle = OperatorStyle.LeftAndRight | OperatorStyle.Right, HandlesInfinity = true)]
    public static BigComplex Root(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Pow(args[0], BigRational.Parse(".5")); //square root

        if (args[1].IsInfinity)
            return new BigComplex(true, args[1].Real * args[0].Real, 0);
        // 3 | 56 = cube root 56 (56 ^ 1 /3)
        return Pow(args[1], 1 / args[0]);
    }

    [Function("Mod", HandlesInfinity = true)]
    public static BigComplex Mod(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];

        if (args[0].IsInfinity)
            return 0;

        var number = args[0];
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].IsInfinity)
                return number;
            var divide = Misc.Floor(number / args[i]);
            number -= (divide * args[i]);
        }
        return number;
    }

    [Function("Percent", HandlesInfinity = true)]
    public static BigComplex Percent(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return args[0] / 100;
    }

    [Function("ModOrPercent", Operator = '%', Priority = 4, OperatorStyle = OperatorStyle.Left | OperatorStyle.LeftAndRight, HandlesInfinity = true)]
    public static BigComplex ModOrPercent(params BigComplex[] args)
    {
        if (args.Length == 1)
            return Percent(args);
        return Mod(args);
    }

    [Function("Factorial", Operator = '!', Priority = 4, OperatorStyle = OperatorStyle.Left, HandlesInfinity = true)]
    public static BigComplex Factorial(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        if (args[0].IsInfinity)
            return args[0];

        if (args[0].IsInteger)
        {
            if (args[0].Real >= 0)
                return (BigRational)IntFactorial((BigInteger)args[0].Real);
        }

        // use function N^x * Prod(N, k=1, k / (x + k))
        // where N = precision

        BigInteger N;
        if (args.Length == 2)
            N = (BigInteger)args[1].Real;
        else
            N = Equation.DecimalPrecision * 20;

        var Nx = Pow(new BigComplex(N, 0), args[0]);

        BigComplex prod = 1;
        for (BigInteger k = 1; k <= N; k++)
            prod *= (BigRational)k / (args[0] + new BigComplex(k, 0)); // k / (x + k)

        return prod * Nx;

        static BigInteger IntFactorial(BigInteger num)
        {
            BigInteger current = 1;
            for (int i = 1; i <= num; i++)
                current *= i;
            return current;
        }
    }
}
