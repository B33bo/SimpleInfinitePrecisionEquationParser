using SIPEP;
using System.Numerics;

namespace CalculatorGUI.MiscFeatures;

internal class Factors
{
    public static List<string> GetFactors(string input)
    {
        if (!BigComplex.TryParse(input, out BigComplex num))
            return new List<string>();

        var numInt = (BigInteger)num.Real;
        var factors = GetFactors(numInt);
        List<string> output = new(1)
        {
            $"Count: {factors.Length}"
        };

        for (int i = 0; i < factors.Length; i++)
            output.Add($"{factors[i]} * {numInt / factors[i]}");

        return output;
    }

    private static BigInteger[] GetFactors(BigRational number)
    {
        List<BigInteger> bigIntegers = new();
        BigInteger max = (BigInteger)BigRational.Sqrt(number, Equation.DecimalPrecision);

        for (BigRational i = 1; i <= max; i++)
        {
            BigInteger num = (BigInteger)i;
            if (number % num != 0)
                continue;
            bigIntegers.Add(num);
        }
        return bigIntegers.ToArray();
    }
}
