using SIPEP;
using System.Numerics;

namespace CalculatorGUI.MiscFeatures;

internal class PrimeFactors
{
    public static string GetPrimeFactors(string val)
    {
        if (!BigComplex.TryParse(val, out BigComplex bc))
            return "Error";
        if (bc.Imaginary != 0)
            return "Error";
        if (bc.Real % 1 != 0)
            return "Error";

        var primeFactors = GetPrimeFactors((BigInteger)bc.Real);

        if (primeFactors is null)
            return "Error";

        Sort(ref primeFactors);
        Dictionary<BigInteger, int> primeFactorsDict = new();

        for (int i = 0; i < primeFactors.Length; i++)
        {
            if (primeFactorsDict.ContainsKey(primeFactors[i]))
                primeFactorsDict[primeFactors[i]]++;
            else
                primeFactorsDict.Add(primeFactors[i], 1);
        }

        string s = "";
        foreach (var primeFactor in primeFactorsDict)
        {
            if (primeFactor.Value == 1)
                s += " * " + primeFactor.Key;
            else
                s += " * " + primeFactor.Key + "^" + primeFactor.Value;
        }

        return s[3..];

        static void Sort(ref BigInteger[] primeFactors)
        {
            bool didSwap;
            do
            {
                didSwap = false;
                for (int i = 1; i < primeFactors.Length; i++)
                {
                    if (primeFactors[i] >= primeFactors[i - 1])
                        continue;

                    (primeFactors[i - 1], primeFactors[i]) = (primeFactors[i], primeFactors[i - 1]);
                    didSwap = true;
                }
            } while (didSwap);
        }
    }

    private static BigInteger[]? GetPrimeFactors(BigInteger n)
    {
        if (n == 0)
            return new BigInteger[] { 0 };
        if (n == 1)
            return new BigInteger[] { 1 };
        if (n < 0)
            return null;

        List<BigInteger> primeFactors = new();
        while (n % 2 == 0)
        {
            primeFactors.Add(2);
            n /= 2;
        }

        var max = (BigInteger)BigRational.Sqrt(n, Equation.DecimalPrecision);
        for (BigInteger i = 3; i <= max; i += 2)
        {
            while (n % i == 0)
            {
                primeFactors.Add(i);
                n /= i;
            }
        }
        if (n > 2)
        {
            primeFactors.Add(n);
        }
        return primeFactors.ToArray();
    }
}
