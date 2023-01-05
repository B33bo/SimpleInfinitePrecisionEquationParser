using SIPEP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorGUI
{
    public partial class VariablesDialog : Form
    {
        private int selected = -1;
        public VariablesDialog()
        {
            InitializeComponent();

            foreach (var item in Calculator.currentEquation.Variables)
                vars.Items.Add(item.Key + " = " + item.Value);
        }

        private void SelectVariable(object sender, EventArgs e)
        {
            selected = vars.SelectedIndex;
        }

        private void AddVar(object sender, EventArgs e)
        {
            AddDialog add = new()
            {
                Text = "Add Variable",
                DummyText = "x = 14 * pi"
            };

            var result = add.ShowDialog();
            if (result != DialogResult.OK)
                return;

            try
            {
                if (add.Result == "")
                    return;
                Calculator.currentEquation.LoadString($"let {add.Result}");

                if (!Calculator.currentEquation.SolveBoolean())
                {
                    SystemSounds.Beep.Play();
                    return;
                }

                string variableName = add.Result.Replace(" ", "").Split('=')[0];
                if (Calculator.currentEquation.Variables.ContainsKey(variableName))
                    vars.Items.Insert(0, $"{variableName} = {Calculator.currentEquation.Variables[variableName]}");
            }
            catch (Exception)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void EditVar(object sender, EventArgs e)
        {
            if (selected < 0 || selected >= vars.Items.Count)
                return;

            AddDialog edit = new()
            {
                Text = "Edit Variable",
            };

            string fullEquation = (string)vars.Items[selected];
            fullEquation = fullEquation.Replace(" ", "");

            var dataValue = fullEquation.Split('=');

            string varname = dataValue[0];
            string vardata = dataValue[1];

            edit.DummyText = $"{varname} = {vardata}";

            var result = edit.ShowDialog();
            if (result != DialogResult.OK)
                return;

            try
            {
                if (edit.Result == "")
                    return;

                Calculator.currentEquation.LoadString(edit.Result);

                if (!Calculator.currentEquation.SolveBoolean())
                    return;

                string varName = edit.Result.Replace(" ", "").Split('=')[0];
                vars.Items[selected] = $"{varName} = {Calculator.currentEquation.Variables[varName]}";
            }
            catch (Exception)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void RemoveVar(object sender, EventArgs e)
        {
            if (selected < 0 || selected >= vars.Items.Count)
                return;

            var varname = ((string)vars.Items[selected]).Replace(" ", "").Split('=')[0];
            Calculator.currentEquation.Variables.Remove(varname);
            vars.Items.RemoveAt(selected);
        }

        private void UseVar(object sender, EventArgs e)
        {
            if (selected < 0 || selected >= vars.Items.Count)
                return;

            var varname = ((string)vars.Items[selected]).Replace(" ", "").Split('=')[0];
            Calculator.Instance.AppendString(varname);
            Close();
        }
    }
}
