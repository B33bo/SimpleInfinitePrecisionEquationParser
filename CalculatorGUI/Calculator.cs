using SIPEP;
using SIPEP.Functions;

namespace CalculatorGUI;

public partial class Calculator : Form
{
    public static Calculator Instance { get; set; }
    public static Equation currentEquation;
    public static int Version = 2;

    public Calculator()
    {
        InitializeComponent();
        currentEquation = new Equation("");
        Instance = this;
    }

    private void EquationChanged(object sender, EventArgs e)
    {
        UpdateIndentCount();
    }

    private void GetAnswer(object sender, EventArgs e)
    {
        try
        {
            currentEquation.LoadString(equationTextBox.Text);
        }
        catch (Exception)
        {
            AnswerLabel.Text = "Error";
            AnswerLabel.ForeColor = Color.Red;
            return;
        }

        //equationTextBox.Text = currentEquation.ToString();
        BigComplex answer;

        try
        {
            answer = currentEquation.Solve();
        }
        catch (Exception)
        {
            AnswerLabel.Text = "Error";
            AnswerLabel.ForeColor = Color.Red;
            return;
        }

        AnswerLabel.Text = answer.ToString();
        AnswerLabel.ForeColor = Color.White;
        precision.Value = Equation.DecimalPrecision;
    }

    private void CopyToClipboard(object sender, EventArgs e)
    {
        Clipboard.SetText(AnswerLabel.Text);
    }

    private void LoadFunctions(object sender, EventArgs e)
    {
        new FunctionsDialog().Show();
    }

    public void AppendString(string s)
    {
        equationTextBox.Text += s;
    }

    private void LoadVars(object sender, EventArgs e)
    {
        new VariablesDialog().Show();
    }

    private void ChangePrecision(object sender, EventArgs e)
    {
        Equation.DecimalPrecision = (int)precision.Value;
    }

    private void Clear(object sender, EventArgs e)
    {
        equationTextBox.Text = "";
        AnswerLabel.Text = "";
    }

    private void UpdateIndentCount()
    {
        var equationText = equationTextBox.Text;
        int indentCount = 0;
        for (int i = 0; i < equationText.Length; i++)
        {
            if (equationText[i] == '(')
                indentCount++;
            else if (equationText[i] == ')')
                indentCount--;
        }

        if (indentCount < 0)
            IndentCountLabel.ForeColor = Color.Red;
        else if (indentCount > 0)
            IndentCountLabel.ForeColor = Color.Orange;
        else
            IndentCountLabel.ForeColor = Color.White;
        IndentCountLabel.Text = $"Indent Count: {indentCount}";
    }

    private void OpenAbout(object sender, EventArgs e)
    {
        new About().Show();
    }

    private void OnCalcLoad(object sender, EventArgs e)
    {
        if (FunctionLoader.loadedFunctions is null)
            throw new Exception();
#nullable disable
        FunctionLoader.loadedFunctions.Add((new FunctionAttribute("Close"), typeof(CustomFunctions).GetMethod("Close")));

        FunctionAttribute ChangeColor = new FunctionAttribute("ChangeColor")
        {
            Args = "ChangeColor(R, G, B)",
        };

        FunctionLoader.loadedFunctions.Add((ChangeColor, typeof(CustomFunctions).GetMethod("ChangeColor")));

        FunctionAttribute ChangeTextColor = new FunctionAttribute("ChangeTextColor")
        {
            Args = "ChangeTextColor(R, G, B)",
        };

        FunctionLoader.loadedFunctions.Add((ChangeTextColor, typeof(CustomFunctions).GetMethod("ChangeTextColor")));
#nullable enable
    }

    public void SetColor(Color c)
    {
        equationTextBox.BackColor = c;
    }

    public void SetTextColor(Color c)
    {
        equationTextBox.ForeColor = c;
    }

    private void Plot(object sender, EventArgs e)
    {
        new Graph().Show();
    }
}