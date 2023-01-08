using SIPEP;
using System.Numerics;

namespace CalculatorGUI;

public partial class Misc : Form
{
    private bool mouseOverNumber;
    private bool mouseOverFactors;

    public Misc()
    {
        InitializeComponent();
    }

    private void PrimeFactors_ChangedNumber(object sender, EventArgs e)
    {
        if (!BigComplex.TryParse(numberText.Text, out BigComplex bc))
            return;
        if (bc.Imaginary != 0)
            return;
        if (bc.Real % 1 != 0)
            return;
        if (!mouseOverNumber)
            return;

        var primeFactors = GetPrimeFactors((BigInteger)bc.Real);

        if (primeFactors is null)
        {
            primeFactorsText.Text = "Error";
            return;
        }

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

        primeFactorsText.Text = s[3..];

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

    private void PrimeFactors_ChangedFactors(object sender, EventArgs e)
    {
        if (!mouseOverFactors)
            return;
        try
        {
            Calculator.currentEquation.LoadString(primeFactorsText.Text);
            numberText.Text = Calculator.currentEquation.Solve().ToString();
        }
        catch (Exception)
        {
            numberText.Text = "NaN";
        }
    }

    private BigInteger[] GetFactors(BigRational number)
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

    private void MouseEnterText(object sender, EventArgs e)
    {
        if (sender == numberText)
            mouseOverNumber = true;
        else if (sender == primeFactorsText)
            mouseOverFactors = true;
    }

    private void MouseExitText(object sender, EventArgs e)
    {
        if (sender == numberText)
            mouseOverNumber = false;
        else if (sender == primeFactorsText)
            mouseOverFactors = false;
    }

    private void Factors_FactorChange(object sender, EventArgs e)
    {
        if (!BigComplex.TryParse(factor_number.Text, out BigComplex num))
            return;

        var numInt = (BigInteger)num.Real;
        var factors = GetFactors(numInt);
        FactorsList.Items.Clear();

        for (int i = 0; i < factors.Length; i++)
        {
            FactorsList.Items.Add($"{factors[i]} * {numInt / factors[i]}");
        }
    }

    private static Dictionary<string, Dictionary<string, BigComplex>> conversions = new();
    private void Misc_Load(object sender, EventArgs e)
    {
        conversionType.Items.Clear();
        var fields = typeof(Conversions).GetFields();
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].FieldType != typeof(Dictionary<string, BigComplex>))
                continue;

            conversionType.Items.Add(fields[i].Name);

            var conversionObj = fields[i].GetValue(null);

            if (conversionObj is not Dictionary<string, BigComplex> conversion)
                continue;

            conversions.Add(fields[i].Name, conversion);
        }

        conversionType.SelectedIndex = 0;
        ReloadAnswer();
    }

    private void ReloadAnswer()
    {
        try
        {
            string equationStr;
            if (conversionType.Text == "Temperature")
                equationStr = $"ConvertTemperature({conversionInput.Text},{conversionFromUnit.Text},{conversionToUnit.Text})";
            else
                equationStr = $"Convert({conversionInput.Text},{conversionFromUnit.Text},{conversionToUnit.Text})";

            Calculator.currentEquation.LoadString(equationStr);
            conversionOutput.Text = Calculator.currentEquation.Solve().ToString();
        }
        catch (Exception)
        {
            conversionOutput.Text = "";
        }
    }

    private int _fromVarName;
    private int _toVarName;

    private void ChangedVariableOrText(object sender, EventArgs e)
    {
        if (_fromVarName != _toVarName)
        {
            if (conversionFromUnit.SelectedIndex == _toVarName)
                conversionToUnit.SelectedIndex = _fromVarName;
            else if (conversionToUnit.SelectedIndex == _fromVarName)
                conversionFromUnit.SelectedIndex = _toVarName;
        }

        _fromVarName = conversionFromUnit.SelectedIndex;
        _toVarName = conversionToUnit.SelectedIndex;

        ReloadAnswer();
    }

    private void ChangedConversionType(object sender, EventArgs e)
    {
        var units = conversions[conversionType.Text];

        conversionFromUnit.Items.Clear();
        conversionToUnit.Items.Clear();

        _fromVarName = 0;
        _toVarName = 1;

        foreach (var item in units)
        {
            if (item.ToString().Contains("metre"))
                continue;
            conversionFromUnit.Items.Add(item.Key);
            conversionToUnit.Items.Add(item.Key);
        }

        conversionFromUnit.SelectedIndex = _fromVarName;
        conversionToUnit.SelectedIndex = _toVarName;

        ReloadAnswer();
    }
}
