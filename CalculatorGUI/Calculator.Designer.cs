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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.equationTextBox = new System.Windows.Forms.TextBox();
            this.AnswerLabel = new System.Windows.Forms.Label();
            this.GetAnswerButton = new System.Windows.Forms.Button();
            this.IndentCountLabel = new System.Windows.Forms.Label();
            this.CopyTextButton = new System.Windows.Forms.Button();
            this.funcsBtn = new System.Windows.Forms.Button();
            this.varBtn = new System.Windows.Forms.Button();
            this.precision = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.plotBtn = new System.Windows.Forms.Button();
            this.answerPrev = new System.Windows.Forms.CheckBox();
            this.keepOnTopToggle = new System.Windows.Forms.CheckBox();
            this.moreBtn = new System.Windows.Forms.Button();
            this.secretBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputDPVar = new System.Windows.Forms.NumericUpDown();
            this.debugModeText = new System.Windows.Forms.Label();
            this.degreesRadians = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.precision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secretBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputDPVar)).BeginInit();
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
            this.equationTextBox.Size = new System.Drawing.Size(551, 52);
            this.equationTextBox.TabIndex = 0;
            this.equationTextBox.Text = "1+1";
            this.equationTextBox.TextChanged += new System.EventHandler(this.EquationChanged);
            // 
            // AnswerLabel
            // 
            this.AnswerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerLabel.AutoSize = true;
            this.AnswerLabel.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AnswerLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AnswerLabel.Location = new System.Drawing.Point(12, 200);
            this.AnswerLabel.MaximumSize = new System.Drawing.Size(550, 0);
            this.AnswerLabel.Name = "AnswerLabel";
            this.AnswerLabel.Size = new System.Drawing.Size(45, 54);
            this.AnswerLabel.TabIndex = 1;
            this.AnswerLabel.Text = "2";
            this.AnswerLabel.UseMnemonic = false;
            // 
            // GetAnswerButton
            // 
            this.GetAnswerButton.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetAnswerButton.Location = new System.Drawing.Point(12, 99);
            this.GetAnswerButton.Name = "GetAnswerButton";
            this.GetAnswerButton.Size = new System.Drawing.Size(84, 89);
            this.GetAnswerButton.TabIndex = 1;
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
            this.IndentCountLabel.TabIndex = 12;
            this.IndentCountLabel.Text = "Indent Count:  0";
            // 
            // CopyTextButton
            // 
            this.CopyTextButton.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CopyTextButton.Location = new System.Drawing.Point(102, 99);
            this.CopyTextButton.Name = "CopyTextButton";
            this.CopyTextButton.Size = new System.Drawing.Size(150, 89);
            this.CopyTextButton.TabIndex = 2;
            this.CopyTextButton.Text = "Copy";
            this.CopyTextButton.UseVisualStyleBackColor = true;
            this.CopyTextButton.Click += new System.EventHandler(this.CopyToClipboard);
            // 
            // funcsBtn
            // 
            this.funcsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.funcsBtn.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.funcsBtn.Location = new System.Drawing.Point(12, 291);
            this.funcsBtn.Name = "funcsBtn";
            this.funcsBtn.Size = new System.Drawing.Size(115, 114);
            this.funcsBtn.TabIndex = 5;
            this.funcsBtn.Text = "F(x)";
            this.funcsBtn.UseVisualStyleBackColor = true;
            this.funcsBtn.Click += new System.EventHandler(this.LoadFunctions);
            // 
            // varBtn
            // 
            this.varBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.varBtn.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.varBtn.Location = new System.Drawing.Point(133, 291);
            this.varBtn.Name = "varBtn";
            this.varBtn.Size = new System.Drawing.Size(115, 114);
            this.varBtn.TabIndex = 6;
            this.varBtn.Text = "n";
            this.varBtn.UseVisualStyleBackColor = true;
            this.varBtn.Click += new System.EventHandler(this.LoadVars);
            // 
            // precision
            // 
            this.precision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.precision.Location = new System.Drawing.Point(367, 353);
            this.precision.Name = "precision";
            this.precision.Size = new System.Drawing.Size(53, 23);
            this.precision.TabIndex = 11;
            this.precision.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.precision.ValueChanged += new System.EventHandler(this.RefreshSettings);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(426, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Precision";
            // 
            // clearBtn
            // 
            this.clearBtn.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearBtn.Location = new System.Drawing.Point(258, 99);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(150, 89);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.Clear);
            // 
            // plotBtn
            // 
            this.plotBtn.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.plotBtn.Location = new System.Drawing.Point(414, 99);
            this.plotBtn.Name = "plotBtn";
            this.plotBtn.Size = new System.Drawing.Size(150, 89);
            this.plotBtn.TabIndex = 4;
            this.plotBtn.Text = "Plot";
            this.plotBtn.UseVisualStyleBackColor = true;
            this.plotBtn.Click += new System.EventHandler(this.Plot);
            // 
            // answerPrev
            // 
            this.answerPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.answerPrev.AutoSize = true;
            this.answerPrev.Checked = true;
            this.answerPrev.CheckState = System.Windows.Forms.CheckState.Checked;
            this.answerPrev.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.answerPrev.ForeColor = System.Drawing.Color.White;
            this.answerPrev.Location = new System.Drawing.Point(367, 291);
            this.answerPrev.Name = "answerPrev";
            this.answerPrev.Size = new System.Drawing.Size(140, 25);
            this.answerPrev.TabIndex = 9;
            this.answerPrev.Text = "Answer Preview";
            this.answerPrev.UseVisualStyleBackColor = true;
            this.answerPrev.CheckedChanged += new System.EventHandler(this.RefreshSettings);
            // 
            // keepOnTopToggle
            // 
            this.keepOnTopToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.keepOnTopToggle.AutoSize = true;
            this.keepOnTopToggle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.keepOnTopToggle.ForeColor = System.Drawing.Color.White;
            this.keepOnTopToggle.Location = new System.Drawing.Point(367, 322);
            this.keepOnTopToggle.Name = "keepOnTopToggle";
            this.keepOnTopToggle.Size = new System.Drawing.Size(116, 25);
            this.keepOnTopToggle.TabIndex = 10;
            this.keepOnTopToggle.Text = "Keep On Top";
            this.keepOnTopToggle.UseVisualStyleBackColor = true;
            this.keepOnTopToggle.CheckedChanged += new System.EventHandler(this.RefreshSettings);
            // 
            // moreBtn
            // 
            this.moreBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moreBtn.AutoSize = true;
            this.moreBtn.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moreBtn.Location = new System.Drawing.Point(254, 291);
            this.moreBtn.Name = "moreBtn";
            this.moreBtn.Size = new System.Drawing.Size(107, 114);
            this.moreBtn.TabIndex = 7;
            this.moreBtn.Text = "...";
            this.moreBtn.UseVisualStyleBackColor = true;
            this.moreBtn.Click += new System.EventHandler(this.LoadMisc);
            // 
            // secretBox
            // 
            this.secretBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secretBox.Location = new System.Drawing.Point(555, 0);
            this.secretBox.Name = "secretBox";
            this.secretBox.Size = new System.Drawing.Size(22, 17);
            this.secretBox.TabIndex = 8;
            this.secretBox.TabStop = false;
            this.secretBox.Click += new System.EventHandler(this.DebugBoxClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(426, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Output DP";
            // 
            // outputDPVar
            // 
            this.outputDPVar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.outputDPVar.Location = new System.Drawing.Point(367, 382);
            this.outputDPVar.Name = "outputDPVar";
            this.outputDPVar.Size = new System.Drawing.Size(53, 23);
            this.outputDPVar.TabIndex = 12;
            this.outputDPVar.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.outputDPVar.ValueChanged += new System.EventHandler(this.RefreshSettings);
            // 
            // debugModeText
            // 
            this.debugModeText.AutoSize = true;
            this.debugModeText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.debugModeText.ForeColor = System.Drawing.Color.Red;
            this.debugModeText.Location = new System.Drawing.Point(468, 2);
            this.debugModeText.Name = "debugModeText";
            this.debugModeText.Size = new System.Drawing.Size(86, 15);
            this.debugModeText.TabIndex = 13;
            this.debugModeText.Text = "DEBUG MODE";
            this.debugModeText.Visible = false;
            // 
            // degreesRadians
            // 
            this.degreesRadians.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.degreesRadians.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.degreesRadians.FormattingEnabled = true;
            this.degreesRadians.Items.AddRange(new object[] {
            "Radians",
            "Degrees"});
            this.degreesRadians.Location = new System.Drawing.Point(367, 262);
            this.degreesRadians.MaxDropDownItems = 2;
            this.degreesRadians.Name = "degreesRadians";
            this.degreesRadians.Size = new System.Drawing.Size(187, 23);
            this.degreesRadians.TabIndex = 8;
            this.degreesRadians.SelectedIndexChanged += new System.EventHandler(this.RefreshSettings);
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(575, 417);
            this.Controls.Add(this.degreesRadians);
            this.Controls.Add(this.debugModeText);
            this.Controls.Add(this.secretBox);
            this.Controls.Add(this.keepOnTopToggle);
            this.Controls.Add(this.answerPrev);
            this.Controls.Add(this.outputDPVar);
            this.Controls.Add(this.precision);
            this.Controls.Add(this.moreBtn);
            this.Controls.Add(this.varBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.funcsBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IndentCountLabel);
            this.Controls.Add(this.plotBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.CopyTextButton);
            this.Controls.Add(this.GetAnswerButton);
            this.Controls.Add(this.AnswerLabel);
            this.Controls.Add(this.equationTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Calculator";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.OnCalcLoad);
            this.ClientSizeChanged += new System.EventHandler(this.ResetAnswerWidth);
            ((System.ComponentModel.ISupportInitialize)(this.precision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secretBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputDPVar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox equationTextBox;
        private Button GetAnswerButton;
        private Button CopyTextButton;
        private Button clearBtn;
        private Label AnswerLabel;
        private Label IndentCountLabel;
        private Button funcsBtn;
        private Button varBtn;
        private NumericUpDown precision;
        private Label label1;
        private Button plotBtn;
        private CheckBox answerPrev;
        private CheckBox keepOnTopToggle;
        private Button moreBtn;
        private PictureBox secretBox;
        private Label label2;
        private NumericUpDown outputDPVar;
        private Label debugModeText;
        private ComboBox degreesRadians;
    }
}