namespace CalculatorGUI
{
    partial class Fractal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fractal));
            this.loadingBar = new System.Windows.Forms.ProgressBar();
            this.topLeft = new System.Windows.Forms.Label();
            this.topRight = new System.Windows.Forms.Label();
            this.middle = new System.Windows.Forms.Label();
            this.bottomRight = new System.Windows.Forms.Label();
            this.bottomLeft = new System.Windows.Forms.Label();
            this.resBox = new System.Windows.Forms.TextBox();
            this.YOffset = new System.Windows.Forms.TextBox();
            this.XOffset = new System.Windows.Forms.TextBox();
            this.scaleText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.equationBox = new System.Windows.Forms.TextBox();
            this.graphImageBox = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cutoffText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.iterationsText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.epsilonText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingBar
            // 
            this.loadingBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBar.Location = new System.Drawing.Point(530, 495);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(258, 23);
            this.loadingBar.TabIndex = 27;
            // 
            // topLeft
            // 
            this.topLeft.AutoSize = true;
            this.topLeft.Location = new System.Drawing.Point(12, 10);
            this.topLeft.Name = "topLeft";
            this.topLeft.Size = new System.Drawing.Size(30, 15);
            this.topLeft.TabIndex = 24;
            this.topLeft.Text = "(0,1)";
            // 
            // topRight
            // 
            this.topRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topRight.AutoSize = true;
            this.topRight.Location = new System.Drawing.Point(417, 10);
            this.topRight.Name = "topRight";
            this.topRight.Size = new System.Drawing.Size(30, 15);
            this.topRight.TabIndex = 23;
            this.topRight.Text = "(1,1)";
            // 
            // middle
            // 
            this.middle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.middle.AutoSize = true;
            this.middle.Location = new System.Drawing.Point(243, 257);
            this.middle.Name = "middle";
            this.middle.Size = new System.Drawing.Size(30, 15);
            this.middle.TabIndex = 22;
            this.middle.Text = "(1,0)";
            // 
            // bottomRight
            // 
            this.bottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomRight.AutoSize = true;
            this.bottomRight.Location = new System.Drawing.Point(494, 507);
            this.bottomRight.Name = "bottomRight";
            this.bottomRight.Size = new System.Drawing.Size(30, 15);
            this.bottomRight.TabIndex = 25;
            this.bottomRight.Text = "(1,0)";
            // 
            // bottomLeft
            // 
            this.bottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bottomLeft.AutoSize = true;
            this.bottomLeft.Location = new System.Drawing.Point(12, 507);
            this.bottomLeft.Name = "bottomLeft";
            this.bottomLeft.Size = new System.Drawing.Size(30, 15);
            this.bottomLeft.TabIndex = 20;
            this.bottomLeft.Text = "(0,0)";
            // 
            // resBox
            // 
            this.resBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resBox.Location = new System.Drawing.Point(620, 156);
            this.resBox.Name = "resBox";
            this.resBox.Size = new System.Drawing.Size(168, 23);
            this.resBox.TabIndex = 21;
            this.resBox.Text = "128";
            // 
            // YOffset
            // 
            this.YOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.YOffset.Location = new System.Drawing.Point(601, 126);
            this.YOffset.Name = "YOffset";
            this.YOffset.Size = new System.Drawing.Size(187, 23);
            this.YOffset.TabIndex = 19;
            this.YOffset.Text = "0";
            // 
            // XOffset
            // 
            this.XOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XOffset.Location = new System.Drawing.Point(601, 97);
            this.XOffset.Name = "XOffset";
            this.XOffset.Size = new System.Drawing.Size(187, 23);
            this.XOffset.TabIndex = 13;
            this.XOffset.Text = "0";
            // 
            // scaleText
            // 
            this.scaleText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleText.Location = new System.Drawing.Point(588, 68);
            this.scaleText.Name = "scaleText";
            this.scaleText.Size = new System.Drawing.Size(200, 23);
            this.scaleText.TabIndex = 11;
            this.scaleText.Text = "1";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.CausesValidation = false;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(530, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "Resolution";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.CausesValidation = false;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(530, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 15;
            this.label4.Text = "Y Offset";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(530, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "X Offset";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(530, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 18;
            this.label1.Text = "Scale";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(530, 272);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(258, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Save as Image";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(530, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DrawFractal);
            // 
            // equationBox
            // 
            this.equationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.equationBox.Location = new System.Drawing.Point(530, 10);
            this.equationBox.Name = "equationBox";
            this.equationBox.Size = new System.Drawing.Size(258, 23);
            this.equationBox.TabIndex = 9;
            this.equationBox.Text = "z^2";
            // 
            // graphImageBox
            // 
            this.graphImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphImageBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("graphImageBox.InitialImage")));
            this.graphImageBox.Location = new System.Drawing.Point(12, 10);
            this.graphImageBox.Name = "graphImageBox";
            this.graphImageBox.Size = new System.Drawing.Size(512, 512);
            this.graphImageBox.TabIndex = 8;
            this.graphImageBox.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Julia",
            "Mandelbrot"});
            this.comboBox1.Location = new System.Drawing.Point(530, 301);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(258, 23);
            this.comboBox1.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.CausesValidation = false;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(530, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "Cutoff";
            // 
            // cutoffText
            // 
            this.cutoffText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cutoffText.Location = new System.Drawing.Point(589, 185);
            this.cutoffText.Name = "cutoffText";
            this.cutoffText.Size = new System.Drawing.Size(199, 23);
            this.cutoffText.TabIndex = 21;
            this.cutoffText.Text = "2000";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.CausesValidation = false;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(530, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "Iterations";
            // 
            // iterationsText
            // 
            this.iterationsText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iterationsText.Location = new System.Drawing.Point(611, 214);
            this.iterationsText.Name = "iterationsText";
            this.iterationsText.Size = new System.Drawing.Size(177, 23);
            this.iterationsText.TabIndex = 21;
            this.iterationsText.Text = "20";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.CausesValidation = false;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(530, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "Epsilon";
            // 
            // epsilonText
            // 
            this.epsilonText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.epsilonText.Location = new System.Drawing.Point(596, 243);
            this.epsilonText.Name = "epsilonText";
            this.epsilonText.Size = new System.Drawing.Size(192, 23);
            this.epsilonText.TabIndex = 21;
            this.epsilonText.Text = "0.0001";
            // 
            // Fractal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.topLeft);
            this.Controls.Add(this.topRight);
            this.Controls.Add(this.middle);
            this.Controls.Add(this.bottomRight);
            this.Controls.Add(this.bottomLeft);
            this.Controls.Add(this.epsilonText);
            this.Controls.Add(this.iterationsText);
            this.Controls.Add(this.cutoffText);
            this.Controls.Add(this.resBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.YOffset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.XOffset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scaleText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.equationBox);
            this.Controls.Add(this.graphImageBox);
            this.Name = "Fractal";
            this.Text = "Fractal";
            ((System.ComponentModel.ISupportInitialize)(this.graphImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar loadingBar;
        private Label topLeft;
        private Label topRight;
        private Label middle;
        private Label bottomRight;
        private Label bottomLeft;
        private TextBox resBox;
        private TextBox YOffset;
        private TextBox XOffset;
        private TextBox scaleText;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private Button button2;
        private Button button1;
        private TextBox equationBox;
        private PictureBox graphImageBox;
        private ComboBox comboBox1;
        private Label label2;
        private TextBox cutoffText;
        private Label label6;
        private TextBox iterationsText;
        private Label label7;
        private TextBox epsilonText;
    }
}