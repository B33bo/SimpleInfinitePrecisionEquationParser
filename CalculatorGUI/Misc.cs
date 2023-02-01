using CalculatorGUI.MiscFeatures;
using SIPEP;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace CalculatorGUI;

public partial class Misc : Form
{
    public Misc()
    {
        InitializeComponent();
    }

    private void PrimeFactors_ChangedNumber(object sender, EventArgs e)
    {
        if (numberText.Text.Length > 5)
        {
            manualPrimeFactors.Show();
            return;
        }
        manualPrimeFactors.Hide();

        primeFactorsText.Text = PrimeFactors.GetPrimeFactors(numberText.Text);
    }

    private void PrimeFactors_ForceUpdate(object sender, EventArgs e) =>
        primeFactorsText.Text = PrimeFactors.GetPrimeFactors(numberText.Text);

    private void Factors_FactorChange(object sender, EventArgs e)
    {
        if (factor_number.Text.Length > 5)
        {
            manualListFactors.Show();
            return;
        }
        manualListFactors.Hide();

        var factors = Factors.GetFactors(factor_number.Text);
        FactorsList.Items.Clear();
        for (int i = 0; i < factors.Count; i++)
            FactorsList.Items.Add(factors[i]);
    }

    private void Factors_ForceUpdate(object sender, EventArgs e)
    {
        var factors = Factors.GetFactors(factor_number.Text);
        FactorsList.Items.Clear();
        for (int i = 0; i < factors.Count; i++)
            FactorsList.Items.Add(factors[i]);
    }

    private static Dictionary<string, Dictionary<string, BigComplex>?> conversions = new();
    private void Misc_Load(object sender, EventArgs e)
    {
        conversionType.Items.Clear();
        conversions.Clear();
        conversions.Add("Number", null);
        conversionType.Items.Add("Number");

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

        if (numberText.Text.Length > 5)
            manualPrimeFactors.Show();
        else
            manualPrimeFactors.Hide();

        if (factor_number.Text.Length > 5)
            manualListFactors.Show();
        else
            manualListFactors.Hide();
    }

    private void ReloadAnswer()
    {
        if (conversionType.Text == "Number")
        {
            if (!int.TryParse(conversionFromUnit.Text, out int from))
                return;
            if (!int.TryParse(conversionToUnit.Text, out int to))
                return;
            conversionOutput.Text = Base.ConvertBase(conversionInput.Text, from, to);
            return;
        }

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
        if (conversionType.Text == "Number")
        {
            ReloadAnswer();
            return;
        }

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

        if (units is null)
        {
            conversionFromUnit.DropDownStyle = ComboBoxStyle.DropDown;
            conversionToUnit.DropDownStyle = ComboBoxStyle.DropDown;

            if (conversionType.Text == "Number")
            {
                conversionFromUnit.Text = "10";
                conversionToUnit.Text = "2";

                conversionFromUnit.Items.Clear();
                conversionFromUnit.Items.Add("2");
                conversionFromUnit.Items.Add("10");
                conversionFromUnit.Items.Add("16");

                conversionToUnit.Items.Clear();
                conversionToUnit.Items.Add("2");
                conversionToUnit.Items.Add("10");
                conversionToUnit.Items.Add("16");

                ReloadAnswer();
            }

            return;
        }

        conversionFromUnit.DropDownStyle = ComboBoxStyle.DropDownList;
        conversionToUnit.DropDownStyle = ComboBoxStyle.DropDownList;

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
