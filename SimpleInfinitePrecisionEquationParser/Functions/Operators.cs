using System.Numerics;

namespace SIPEP.Functions;

public static class Operators
{
    [Function("Add", Operator = '+', InverseFunctionName = "Subtract", Priority = 3)]
    public static BigComplex Add(params BigComplex[] args)
    {
        BigComplex val = 0;
        for (int i = 0; i < args.Length; i++)
            val += args[i];
        return val;
    }

    [Function("Subtract", Operator = '-', InverseFunctionName = "Add", Priority = 3, OperatorStyle = OperatorStyle.LeftAndRight | OperatorStyle.Right)]
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

    [Function("Multiply", Operator = '*', InverseFunctionName = "Divide", Priority = 2)]
    public static BigComplex Multiply(params BigComplex[] args)
    {
        BigComplex val = 1;
        for (int i = 0; i < args.Length; i++)
            val *= args[i];
        return val;
    }

    [Function("Divide", Operator = '/', InverseFunctionName = "Multiply", Priority = 2)]
    public static BigComplex Divide(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        BigComplex val = args[0];
        for (int i = 1; i < args.Length; i++)
            val /= args[i];
        return val;
    }

    [Function("Pow", Operator = '^', InverseFunctionName = "Root", Args = "Pow(base, exponent)", Priority = 1)]
    public static BigComplex Pow(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];

        BigComplex current = args[0];
        for (int i = 1; i < args.Length; i++)
            current = Power(current, args[i]);

        return current;

        static BigComplex Power(BigComplex value, BigComplex exponent)
        {
            if (exponent == 0)
                return 1;

            if (value == 0)
                return value; //zero

            int precistion = Equation.DecimalPrecision;

            BigRational valueReal = value.Real;
            BigRational valueImaginary = value.Imaginary;
            BigRational exponentReal = exponent.Real;
            BigRational exponentImaginary = exponent.Imaginary;

            BigRational rho = Misc.Abs(value).Real;
            BigRational theta = BigRational.Atan2(valueImaginary, valueReal, Equation.DecimalPrecision);
            BigRational newRho = exponentReal * theta + exponentImaginary * BigRational.Log(rho, precistion);

            BigRational t = BigRational.Pow(rho, exponentReal, precistion) * BigRational.Pow(Constants.E, -exponentImaginary * theta, Equation.DecimalPrecision);

            return new BigComplex(t * BigRational.Cos(newRho, Equation.DecimalPrecision), t * BigRational.Sin(newRho, Equation.DecimalPrecision));
        }
    }

    [Function("Root", Operator = '\\', InverseFunctionName = "Power", Args = "Root(root, number)", Priority = 1)]
    public static BigComplex Root(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return Pow(args[0], BigRational.Parse(".5")); //square root
        // 3 | 56 = cube root 56 (56 ^ 1 /3)
        return Pow(args[1], 1 / args[0]);
    }

    [Function("Mod", Operator = '%', Priority = 4)]
    public static BigComplex Mod(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1)
            return args[0];

        var number = args[0];
        for (int i = 1; i < args.Length; i++)
        {
            var real = number.Real;
            var imag = number.Imaginary;

            real %= args[i].Real;
            imag %= args[i].Imaginary;
            number = new BigComplex(real, imag);
        }
        return number;
    }
}
