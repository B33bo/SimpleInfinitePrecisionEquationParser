using SIPEP;
using SIPEP.Functions;
using System.Numerics;
using System.Text;

namespace CalculatorGUI;

public partial class Calculator : Form
{
#nullable disable //it was annoying me
    public static Calculator Instance { get; set; }
    public static Equation currentEquation;
    public const int Version = 11;
    private static bool answerPreview = true;
    private bool debugMode;
    private int OutputDP;
#nullable enable

    public Calculator()
    {
        InitializeComponent();
        currentEquation = new Equation("");
        Instance = this;
        currentEquation.Variables.Add("calcversion", Version);
        OutputDP = (int)outputDPVar.Value;
        degreesRadians.SelectedIndex = 0;
    }

    private void EquationChanged(object sender, EventArgs e)
    {
        UpdateIndentCount();

        if (!answerPreview)
            return;
        try
        {
            currentEquation.LoadString(equationTextBox.Text);
            AnswerLabel.Text = currentEquation.Solve().ToString(OutputDP);
            precision.Value = Equation.DecimalPrecision;
        }
        catch (Exception)
        {
            AnswerLabel.Text = "";
        }
    }

    private void GetAnswer(object sender, EventArgs e)
    {
        BigComplex answer;

        try
        {
            AnswerLabel.ForeColor = Color.White;

            if (equationTextBox.Text.StartsWith(@"solve,"))
                AnswerLabel.Text = GetSolutionString(equationTextBox.Text, out answer);
            else
            {
                answer = GetAnswer(equationTextBox.Text);
                AnswerLabel.Text = answer.ToString(OutputDP);
            }
        }
        catch (Exception)
        {
            AnswerLabel.Text = "Error";
            AnswerLabel.ForeColor = Color.Red;

            if (debugMode)
                throw;

            return;
        }

        currentEquation.SetVariable("ans", answer);

        precision.Value = Equation.DecimalPrecision;
    }

    private string GetSolutionString(string s, out BigComplex FirstResult)
    {
        // format: solve,varName,max,depth:
        var argsString = s.Split(':')[0];
        var args = argsString.Split(',');

        currentEquation.LoadString(s.Substring(argsString.Length + 1));

        double max = 100;
        int depth = 20;
        BigRational cutoff = BigRational.Parse("0.1");
        bool realOnly = false;

        if (args.Length >= 3)
            max = double.Parse(args[2]);
        if (args.Length >= 4)
            depth = int.Parse(args[3]);
        if (args.Length >= 5)
            cutoff = BigRational.Parse(args[4]);
        if (args.Length >= 6)
            realOnly = bool.Parse(args[5]);

        var solutions = currentEquation.FindSolutions(args[1], max, depth, cutoff, realOnly);

        FirstResult = BigComplex.NaN;

        if (solutions.Length == 0)
            return "No Solutions";

        FirstResult = solutions[0];

        StringBuilder finalStr = new();
        for (int i = 0; i < solutions.Length; i++)
            finalStr.Append(solutions[i].ToString(OutputDP) + ", ");
        return finalStr.Remove(finalStr.Length - 2, 2).ToString();
    }

    private static BigComplex GetAnswer(string s)
    {
        currentEquation.LoadString(s);
        return currentEquation.Solve();
    }

    private void CopyToClipboard(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(AnswerLabel.Text))
            return;
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

    private void RefreshSettings(object sender, EventArgs e)
    {
        Equation.DecimalPrecision = (int)precision.Value;
        Equation.Radians = degreesRadians.SelectedIndex == 0;
        TopMost = keepOnTopToggle.Checked;
        answerPreview = answerPrev.Checked;
        OutputDP = (int)outputDPVar.Value;

        if (answerPreview)
        {
            try
            {
                currentEquation.LoadString(equationTextBox.Text);
                AnswerLabel.Text = currentEquation.Solve().ToString(OutputDP);
            }
            catch (Exception) { }
        }
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

    private void ResetAnswerWidth(object sender, EventArgs e)
    {
        AnswerLabel.MaximumSize = new Size(Width, 0);
    }

    private void LoadMisc(object sender, EventArgs e)
    {
        new Misc().Show();
    }

    private void DebugBoxClicked(object sender, EventArgs e)
    {
        debugMode = !debugMode;
        debugModeText.Visible = debugMode;
        if (debugMode)
            secretBox.BackColor = Color.Red;
        else
            secretBox.BackColor = Color.Transparent;
    }
}