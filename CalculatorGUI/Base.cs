using SIPEP;
using System.Numerics;
using System.Text;

namespace CalculatorGUI;

public static class Base
{
    public static string ConvertBase(string num, int fromBase, int toBase)
    {
        if (toBase == 1)
        {
            if (num.Length == 0)
                return "0";
            if (num[0] == '-')
                return ConvertBase(num[1..], fromBase, toBase);

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '0')
                    continue;
                if (num[i] == '.')
                    continue;
                return "?";
            }
            return "0";
        }

        string[] parts = num.Split('.');
        string intPartInDecimal = ConvertToDecimal(parts[0], fromBase);

        string decimalPartInDecimal = ".0";
        if (parts.Length > 1)
            decimalPartInDecimal = ConvertDecimalToDecimal(parts[1], fromBase);

        if (decimalPartInDecimal == "0")
            decimalPartInDecimal = "";

        if (decimalPartInDecimal.StartsWith("0."))
            decimalPartInDecimal = decimalPartInDecimal[1..];

        string decimalInDecimal = intPartInDecimal + "." + decimalPartInDecimal;
        return ConvertDecimalToBase(decimalInDecimal, toBase);
    }

    static string ConvertToDecimal(string num, int baseNum)
    {
        int decimalNum = 0;
        int exponent = num.Length - 1;
        for (int i = 0; i < num.Length; i++)
        {
            int digit = GetDigit(num[i]);
            decimalNum += digit * Pow(baseNum, exponent);
            exponent--;
        }
        return decimalNum.ToString();
    }

    static string ConvertDecimalToDecimal(string num, int baseNum)
    {
        BigRational decimalNum = 0;
        int exponent = -1;
        for (int i = 0; i < num.Length; i++)
        {
            int digit = GetDigit(num[i]);
            decimalNum += digit * Pow(baseNum, exponent);
            exponent--;
        }
        return decimalNum.ToString();
    }

    static string ConvertDecimalToBase(string num, int baseNum)
    {
        string[] parts = num.Split('.');
        if (parts.Length != 1)
            parts[1] = "." + parts[1];

        int intPart = int.Parse(parts[0]);
        BigRational decimalPart = parts.Length > 1 ? BigRational.Parse(parts[1]) : 0;
        StringBuilder result = new StringBuilder();
        result.Append(ConvertIntToBase(intPart, baseNum));
        result.Append(".");
        result.Append(ConvertDecimalToBase(decimalPart, baseNum));
        return result.ToString();
    }

    static string ConvertIntToBase(int num, int baseNum)
    {
        StringBuilder result = new StringBuilder();
        while (num > 0)
        {
            int digit = num % baseNum;
            result.Insert(0, GetChar(digit));
            num = num / baseNum;
        }
        return result.ToString();
    }

    static string ConvertDecimalToBase(BigRational num, int baseNum)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < Equation.DecimalPrecision; i++)
        {
            num *= baseNum;
            int digit = (int)num;
            result.Append(GetChar(digit));
            num -= digit;

            if (num == 0) break;
        }
        
        return result.ToString();
    }

    static int GetDigit(char c)
    {
        if (char.IsDigit(c))
            return (int)char.GetNumericValue(c);
        else
            return (int)c - (int)'A' + 10;
    }

    static char GetChar(int digit)
    {
        if (digit < 10)
            return (char)(digit + '0');
        else
            return (char)(digit - 10 + 'A');
    }

    private static int Pow(int a, int b)
    {
        int val = 1;
        for (int i = 0; i < b; i++)
            val *= a;
        return val;
    }
}
