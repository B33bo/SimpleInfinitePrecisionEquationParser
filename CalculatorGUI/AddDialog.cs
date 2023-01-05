using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorGUI
{
    public partial class AddDialog : Form
    {
        public string DummyText { get; set; } = "";
        public string Result = "";
        public AddDialog()
        {
            InitializeComponent();
        }

        private void WhenLoaded(object sender, EventArgs e)
        {
            text.Text = DummyText;
        }

        private void Cancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Ok(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ChangeResult(object sender, EventArgs e)
        {
            Result = text.Text;
        }
    }
}
