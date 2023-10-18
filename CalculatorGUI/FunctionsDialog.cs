using SIPEP;
using System.Media;

namespace CalculatorGUI;

public partial class FunctionsDialog : Form
{
    private int selected = -1;
    private List<string> functionNames;

    public FunctionsDialog()
    {
        InitializeComponent();

        functionNames = new (FunctionLoader.customFunctions.Count);
        foreach (var item in FunctionLoader.customFunctions)
        {
            funcs.Items.Add($"{item.Key}({item.Value.VarNameArgs})");
            functionNames.Add(item.Key);
        }

        if (FunctionLoader.loadedFunctions is null)
            FunctionLoader.ReloadFunctions();

        if (FunctionLoader.loadedFunctions is null)
            return;

        foreach (var item in FunctionLoader.loadedFunctions)
        {
            funcs.Items.Add(item.FunctionInfo.ToString());
        }
    }

    private void AddFunction(object sender, EventArgs e)
    {
        AddDialog add = new()
        {
            Text = "Add Function",
            DummyText = "f(x) = x + 1"
        };

        var result = add.ShowDialog();
        if (result != DialogResult.OK)
            return;

        try
        {
            if (add.Result == "")
                return;
            Calculator.currentEquation.Parse($"let {add.Result}");

            if (!Calculator.currentEquation.SolveBoolean())
                SystemSounds.Beep.Play();

            funcs.Items.Insert(0, add.Result);

            functionNames.Insert(0, add.Result.Split('(')[0]);
        }
        catch (Exception)
        {
            SystemSounds.Beep.Play();
        }
    }

    private void DeleteFunction(object sender, EventArgs e)
    {
        if (funcs.Items[selected] is null)
            return;
        if (funcs.Items[selected].ToString() is not string name)
            return;
        if (selected < 0 || funcs.Items.Count >= selected)
            return;

        if (!FunctionLoader.customFunctions.ContainsKey(name))
        {
            SystemSounds.Beep.Play();
            return;
        }

        funcs.Items.RemoveAt(selected);
        FunctionLoader.customFunctions.Remove(name);
    }

    private void SelectFunction(object sender, EventArgs e)
    {
        if (selected < 0)
            return;

        string str;
        if (selected >= FunctionLoader.customFunctions.Count)
        {
            selected -= FunctionLoader.customFunctions.Count;
            if (FunctionLoader.loadedFunctions is null)
                return;
            str = FunctionLoader.loadedFunctions[selected].FunctionInfo.FunctionName + "(";
        }
        else
            str = functionNames[selected] + "(";

        Calculator.Instance.AppendString(str);
        Close();
    }

    private void SelectedChanged(object sender, EventArgs e)
    {
        selected = funcs.SelectedIndex;
    }

    private void RemoveFunction(object sender, EventArgs e)
    {
        if (selected >= FunctionLoader.customFunctions.Count || selected < 0)
        {
            SystemSounds.Beep.Play();
            return;
        }

        FunctionLoader.customFunctions.Remove(functionNames[selected]);
        functionNames.RemoveAt(selected);
        funcs.Items.RemoveAt(selected);
    }
}
