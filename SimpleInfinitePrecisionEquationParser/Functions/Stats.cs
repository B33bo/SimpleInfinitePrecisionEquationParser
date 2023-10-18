using System.Numerics;

namespace SIPEP.Functions;

public static class Stats
{
    public static BigComplex[] Order(params BigComplex[] args)
    {
        bool didSwap;
        do
        {
            didSwap = false;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].ModulusSquared >= args[i - 1].ModulusSquared)
                    continue;
                (args[i - 1], args[i]) = (args[i], args[i - 1]);
                didSwap = true;
            }

        } while (didSwap);

        return args;
    }

    [Function("Min", HandlesInfinity = true)]
    public static BigComplex Min(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        var min = args[0];

        if (min.IsInfinity && min.Real < 0)
            return min;

        BigRational minLength = Misc.AbsSigned(min).Real;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].IsInfinity && args[i].Real < 0)
                return args[i];
            var current = Misc.AbsSigned(args[i]).Real;
            if (current > minLength)
                continue;

            min = args[i];
            minLength = current;
        }
        return min;
    }

    [Function("Max", HandlesInfinity = true)]
    public static BigComplex Max(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;

        var max = args[0];

        if (max.IsInfinity && max.Real > 0)
            return max;

        BigRational maxLength = Misc.AbsSigned(max).Real;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].IsInfinity && args[i].Real > 0)
                return args[i];

            var current = Misc.AbsSigned(args[i]).Real;
            if (current < maxLength)
                continue;

            max = args[i];
            maxLength = current;
        }
        return max;
    }

    [Function("Mean", HandlesInfinity = true)]
    public static BigComplex Mean(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        var total = Operators.Add(args);
        return total / args.Length;
    }

    [Function("GeometricMean", HandlesInfinity = true)]
    public static BigComplex GeometricMean(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        var total = Operators.Multiply(args);
        return Operators.Pow(total, 1 / (BigRational)args.Length);
    }

    [Function("Median", HandlesInfinity = true)]
    public static BigComplex Median(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        args = Order(args);

        if (args.Length % 2 == 0)
            return Mean(args[(args.Length - 1) / 2], args[(args.Length - 1) / 2 + 1]);
        return args[args.Length / 2];
    }

    [Function("Mode", HandlesInfinity = true)]
    public static BigComplex Mode(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        Dictionary<BigComplex, int> mostUsed = new();

        for (int i = 0; i < args.Length; i++)
        {
            if (mostUsed.ContainsKey(args[i]))
                mostUsed[args[i]]++;
            else
                mostUsed.Add(args[i], 1);
        }

        int currentMostUsed = 0;
        BigComplex mostUsedSum = 0;
        int mostUsedLength = 0;

        foreach (var data in mostUsed)
        {
            if (data.Value < currentMostUsed)
                continue;

            if (data.Value == currentMostUsed)
            {
                mostUsedSum += data.Key;
                mostUsedLength++;
                continue;
            }

            // data.Value > currentMostUsed

            mostUsedSum = data.Key;
            mostUsedLength = 1;
            currentMostUsed = data.Value;
        }

        return mostUsedSum / mostUsedLength;
    }

    [Function("Range", HandlesInfinity = true)]
    public static BigComplex Range(params BigComplex[] args)
    {
        return Max(args) - Min(args);
    }

    [Function("Variance", HandlesInfinity = true)]
    public static BigComplex Variance(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        BigComplex varience = 0;
        BigComplex mean = Mean(args);

        for (int i = 0; i < args.Length; i++)
        {
            var current = args[i] - mean;
            varience += current * current;
        }

        return varience / args.Length;
    }

    [Function("VarianceSample", HandlesInfinity = true)]
    public static BigComplex VarianceSample(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        if (args.Length == 1) 
            return 0;
        BigComplex varience = 0;
        BigComplex mean = Mean(args);

        for (int i = 0; i < args.Length; i++)
        {
            var current = args[i] - mean;
            varience += current * current;
        }

        return varience / (args.Length - 1);
    }

    [Function("StandardDeviation", HandlesInfinity = true)]
    public static BigComplex StandardDeviation(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return Operators.Pow(Variance(args), BigComplex.One / 2);
    }

    [Function("StandardDeviationSample", HandlesInfinity = true)]
    public static BigComplex StandardDeviationSample(params BigComplex[] args)
    {
        if (args.Length == 0)
            return 0;
        return Operators.Pow(VarianceSample(args), BigComplex.One / 2);
    }
}
