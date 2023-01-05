namespace CalculatorGUI
{
    partial class Calculator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.equationTextBox = new System.Windows.Forms.TextBox();
            this.AnswerLabel = new System.Windows.Forms.Label();
            this.GetAnswerButton = new System.Windows.Forms.Button();
            this.IndentCountLabel = new System.Windows.Forms.Label();
            this.CopyTextButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.precision = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.precision)).BeginInit();
            this.SuspendLayout();
            // 
            // equationTextBox
            // 
            this.equationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.equationTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.equationTextBox.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.equationTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.equationTextBox.Location = new System.Drawing.Point(12, 41);
            this.equationTextBox.Name = "equationTextBox";
            this.equationTextBox.Size = new System.Drawing.Size(776, 52);
            this.equationTextBox.TabIndex = 0;
            this.equationTextBox.Text = "1+1";
            this.equationTextBox.TextChanged += new System.EventHandler(this.EquationChanged);
            // 
            // AnswerLabel
            // 
            this.AnswerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerLabel.AutoSize = true;
            this.AnswerLabel.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AnswerLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AnswerLabel.Location = new System.Drawing.Point(12, 200);
            this.AnswerLabel.Name = "AnswerLabel";
            this.AnswerLabel.Size = new System.Drawing.Size(153, 54);
            this.AnswerLabel.TabIndex = 1;
            this.AnswerLabel.Text = "Answer";
            // 
            // GetAnswerButton
            // 
            this.GetAnswerButton.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetAnswerButton.Location = new System.Drawing.Point(12, 99);
            this.GetAnswerButton.Name = "GetAnswerButton";
            this.GetAnswerButton.Size = new System.Drawing.Size(84, 89);
            this.GetAnswerButton.TabIndex = 3;
            this.GetAnswerButton.Text = "=";
            this.GetAnswerButton.UseVisualStyleBackColor = true;
            this.GetAnswerButton.Click += new System.EventHandler(this.GetAnswer);
            // 
            // IndentCountLabel
            // 
            this.IndentCountLabel.AutoSize = true;
            this.IndentCountLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IndentCountLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.IndentCountLabel.Location = new System.Drawing.Point(12, 9);
            this.IndentCountLabel.Name = "IndentCountLabel";
            this.IndentCountLabel.Size = new System.Drawing.Size(151, 28);
            this.IndentCountLabel.TabIndex = 4;
            this.IndentCountLabel.Text = "Indent Count:  0";
            // 
            // CopyTextButton
            // 
            this.CopyTextButton.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CopyTextButton.Location = new System.Drawing.Point(102, 99);
            this.CopyTextButton.Name = "CopyTextButton";
            this.CopyTextButton.Size = new System.Drawing.Size(150, 89);
            this.CopyTextButton.TabIndex = 3;
            this.CopyTextButton.Text = "Copy";
            this.CopyTextButton.UseVisualStyleBackColor = true;
            this.CopyTextButton.Click += new System.EventHandler(this.CopyToClipboard);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(12, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 99);
            this.button1.TabIndex = 5;
            this.button1.Text = "F(x)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadFunctions);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(133, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 99);
            this.button2.TabIndex = 5;
            this.button2.Text = "n";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.LoadVars);
            // 
            // precision
            // 
            this.precision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.precision.Location = new System.Drawing.Point(735, 14);
            this.precision.Name = "precision";
            this.precision.Size = new System.Drawing.Size(53, 23);
            this.precision.TabIndex = 6;
            this.precision.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.precision.ValueChanged += new System.EventHandler(this.ChangePrecision);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(564, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Decimal Precision";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(258, 99);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 89);
            this.button3.TabIndex = 3;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Clear);
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 378);
            this.Controls.Add(this.precision);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IndentCountLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.CopyTextButton);
            this.Controls.Add(this.GetAnswerButton);
            this.Controls.Add(this.AnswerLabel);
            this.Controls.Add(this.equationTextBox);
            this.Name = "Calculator";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.OnCalcLoad);
            ((System.ComponentModel.ISupportInitialize)(this.precision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox equationTextBox;
        private Label AnswerLabel;
        private Button GetAnswerButton;
        private Label IndentCountLabel;
        private Button CopyTextButton;
        private Button button1;
        private Button button2;
        private NumericUpDown precision;
        private Label label1;
        private Button button3;
    }
}