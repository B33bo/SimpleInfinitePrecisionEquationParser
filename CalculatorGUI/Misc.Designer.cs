namespace CalculatorGUI
{
    partial class Misc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabs = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.primeFactorsText = new System.Windows.Forms.TextBox();
            this.numberText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FactorsList = new System.Windows.Forms.ListBox();
            this.factor_number = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.conversionType = new System.Windows.Forms.ComboBox();
            this.conversionToUnit = new System.Windows.Forms.ComboBox();
            this.conversionFromUnit = new System.Windows.Forms.ComboBox();
            this.conversionOutput = new System.Windows.Forms.TextBox();
            this.conversionInput = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabs);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 445);
            this.tabControl1.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabs.Controls.Add(this.label2);
            this.tabs.Controls.Add(this.label1);
            this.tabs.Controls.Add(this.primeFactorsText);
            this.tabs.Controls.Add(this.numberText);
            this.tabs.Location = new System.Drawing.Point(4, 24);
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Windows.Forms.Padding(3);
            this.tabs.Size = new System.Drawing.Size(792, 417);
            this.tabs.TabIndex = 0;
            this.tabs.Text = "Prime Factors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 46);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prime Factors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number";
            // 
            // primeFactorsText
            // 
            this.primeFactorsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primeFactorsText.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.primeFactorsText.Location = new System.Drawing.Point(6, 156);
            this.primeFactorsText.Name = "primeFactorsText";
            this.primeFactorsText.Size = new System.Drawing.Size(776, 52);
            this.primeFactorsText.TabIndex = 1;
            this.primeFactorsText.Text = "1";
            this.primeFactorsText.TextChanged += new System.EventHandler(this.PrimeFactors_ChangedFactors);
            this.primeFactorsText.Enter += new System.EventHandler(this.MouseEnterText);
            this.primeFactorsText.Leave += new System.EventHandler(this.MouseExitText);
            // 
            // numberText
            // 
            this.numberText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberText.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberText.Location = new System.Drawing.Point(6, 52);
            this.numberText.Name = "numberText";
            this.numberText.Size = new System.Drawing.Size(776, 52);
            this.numberText.TabIndex = 0;
            this.numberText.Text = "1";
            this.numberText.TextChanged += new System.EventHandler(this.PrimeFactors_ChangedNumber);
            this.numberText.Enter += new System.EventHandler(this.MouseEnterText);
            this.numberText.Leave += new System.EventHandler(this.MouseExitText);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPage2.Controls.Add(this.FactorsList);
            this.tabPage2.Controls.Add(this.factor_number);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 417);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Factors";
            // 
            // FactorsList
            // 
            this.FactorsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FactorsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FactorsList.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FactorsList.ForeColor = System.Drawing.Color.White;
            this.FactorsList.FormattingEnabled = true;
            this.FactorsList.ItemHeight = 45;
            this.FactorsList.Location = new System.Drawing.Point(8, 64);
            this.FactorsList.Name = "FactorsList";
            this.FactorsList.Size = new System.Drawing.Size(776, 319);
            this.FactorsList.TabIndex = 2;
            // 
            // factor_number
            // 
            this.factor_number.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.factor_number.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.factor_number.Location = new System.Drawing.Point(8, 6);
            this.factor_number.Name = "factor_number";
            this.factor_number.Size = new System.Drawing.Size(776, 52);
            this.factor_number.TabIndex = 1;
            this.factor_number.Text = "1";
            this.factor_number.TextChanged += new System.EventHandler(this.Factors_FactorChange);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPage1.Controls.Add(this.conversionType);
            this.tabPage1.Controls.Add(this.conversionToUnit);
            this.tabPage1.Controls.Add(this.conversionFromUnit);
            this.tabPage1.Controls.Add(this.conversionOutput);
            this.tabPage1.Controls.Add(this.conversionInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 417);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Conversion";
            // 
            // conversionType
            // 
            this.conversionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conversionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conversionType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.conversionType.FormattingEnabled = true;
            this.conversionType.Location = new System.Drawing.Point(8, 138);
            this.conversionType.Name = "conversionType";
            this.conversionType.Size = new System.Drawing.Size(776, 29);
            this.conversionType.TabIndex = 1;
            this.conversionType.SelectedIndexChanged += new System.EventHandler(this.ChangedConversionType);
            // 
            // conversionToUnit
            // 
            this.conversionToUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.conversionToUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conversionToUnit.FormattingEnabled = true;
            this.conversionToUnit.Location = new System.Drawing.Point(434, 98);
            this.conversionToUnit.Name = "conversionToUnit";
            this.conversionToUnit.Size = new System.Drawing.Size(350, 23);
            this.conversionToUnit.TabIndex = 1;
            this.conversionToUnit.SelectedIndexChanged += new System.EventHandler(this.ChangedVariableOrText);
            // 
            // conversionFromUnit
            // 
            this.conversionFromUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conversionFromUnit.FormattingEnabled = true;
            this.conversionFromUnit.Location = new System.Drawing.Point(8, 98);
            this.conversionFromUnit.Name = "conversionFromUnit";
            this.conversionFromUnit.Size = new System.Drawing.Size(350, 23);
            this.conversionFromUnit.TabIndex = 1;
            this.conversionFromUnit.SelectedIndexChanged += new System.EventHandler(this.ChangedVariableOrText);
            // 
            // conversionOutput
            // 
            this.conversionOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.conversionOutput.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.conversionOutput.Location = new System.Drawing.Point(434, 40);
            this.conversionOutput.Name = "conversionOutput";
            this.conversionOutput.ReadOnly = true;
            this.conversionOutput.Size = new System.Drawing.Size(350, 52);
            this.conversionOutput.TabIndex = 0;
            // 
            // conversionInput
            // 
            this.conversionInput.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.conversionInput.Location = new System.Drawing.Point(8, 40);
            this.conversionInput.Name = "conversionInput";
            this.conversionInput.Size = new System.Drawing.Size(350, 52);
            this.conversionInput.TabIndex = 0;
            this.conversionInput.TextChanged += new System.EventHandler(this.ChangedVariableOrText);
            // 
            // Misc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Misc";
            this.ShowIcon = false;
            this.Text = "Misc";
            this.Load += new System.EventHandler(this.Misc_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabs.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabs;
        private TabPage tabPage2;
        private Label label1;
        private TextBox primeFactorsText;
        private TextBox numberText;
        private Label label2;
        private TextBox factor_number;
        private ListBox FactorsList;
        private TabPage tabPage1;
        private TextBox conversionOutput;
        private TextBox conversionInput;
        private ComboBox conversionFromUnit;
        private ComboBox conversionToUnit;
        private ComboBox conversionType;
    }
}