namespace CalculatorGUI
{
    partial class Graph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graph));
            this.graphImageBox = new System.Windows.Forms.PictureBox();
            this.equationBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.widthText = new System.Windows.Forms.TextBox();
            this.heightText = new System.Windows.Forms.TextBox();
            this.XOffset = new System.Windows.Forms.TextBox();
            this.YOffset = new System.Windows.Forms.TextBox();
            this.bottomLeft = new System.Windows.Forms.Label();
            this.bottomRight = new System.Windows.Forms.Label();
            this.topRight = new System.Windows.Forms.Label();
            this.topLeft = new System.Windows.Forms.Label();
            this.middle = new System.Windows.Forms.Label();
            this.loadingBar = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.resBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.graphImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // graphImageBox
            // 
            this.graphImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphImageBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("graphImageBox.InitialImage")));
            this.graphImageBox.Location = new System.Drawing.Point(12, 12);
            this.graphImageBox.Name = "graphImageBox";
            this.graphImageBox.Size = new System.Drawing.Size(512, 512);
            this.graphImageBox.TabIndex = 0;
            this.graphImageBox.TabStop = false;
            // 
            // equationBox
            // 
            this.equationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.equationBox.Location = new System.Drawing.Point(530, 12);
            this.equationBox.Name = "equationBox";
            this.equationBox.Size = new System.Drawing.Size(258, 23);
            this.equationBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(530, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Draw);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(530, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(530, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(530, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "X Offset";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.CausesValidation = false;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(530, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y Offset";
            // 
            // widthText
            // 
            this.widthText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.widthText.Location = new System.Drawing.Point(588, 70);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(200, 23);
            this.widthText.TabIndex = 5;
            this.widthText.Text = "1";
            this.widthText.TextChanged += new System.EventHandler(this.ChangeAxis);
            // 
            // heightText
            // 
            this.heightText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.heightText.Location = new System.Drawing.Point(588, 97);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(200, 23);
            this.heightText.TabIndex = 5;
            this.heightText.Text = "1";
            this.heightText.TextChanged += new System.EventHandler(this.ChangeAxis);
            // 
            // XOffset
            // 
            this.XOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XOffset.Location = new System.Drawing.Point(601, 126);
            this.XOffset.Name = "XOffset";
            this.XOffset.Size = new System.Drawing.Size(187, 23);
            this.XOffset.TabIndex = 5;
            this.XOffset.Text = "0";
            this.XOffset.TextChanged += new System.EventHandler(this.ChangeAxis);
            // 
            // YOffset
            // 
            this.YOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.YOffset.Location = new System.Drawing.Point(601, 155);
            this.YOffset.Name = "YOffset";
            this.YOffset.Size = new System.Drawing.Size(187, 23);
            this.YOffset.TabIndex = 5;
            this.YOffset.Text = "0";
            this.YOffset.TextChanged += new System.EventHandler(this.ChangeAxis);
            // 
            // bottomLeft
            // 
            this.bottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bottomLeft.AutoSize = true;
            this.bottomLeft.Location = new System.Drawing.Point(12, 509);
            this.bottomLeft.Name = "bottomLeft";
            this.bottomLeft.Size = new System.Drawing.Size(30, 15);
            this.bottomLeft.TabIndex = 6;
            this.bottomLeft.Text = "(0,0)";
            // 
            // bottomRight
            // 
            this.bottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomRight.AutoSize = true;
            this.bottomRight.Location = new System.Drawing.Point(494, 509);
            this.bottomRight.Name = "bottomRight";
            this.bottomRight.Size = new System.Drawing.Size(30, 15);
            this.bottomRight.TabIndex = 6;
            this.bottomRight.Text = "(1,0)";
            // 
            // topRight
            // 
            this.topRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topRight.AutoSize = true;
            this.topRight.Location = new System.Drawing.Point(417, 12);
            this.topRight.Name = "topRight";
            this.topRight.Size = new System.Drawing.Size(30, 15);
            this.topRight.TabIndex = 6;
            this.topRight.Text = "(1,1)";
            // 
            // topLeft
            // 
            this.topLeft.AutoSize = true;
            this.topLeft.Location = new System.Drawing.Point(12, 12);
            this.topLeft.Name = "topLeft";
            this.topLeft.Size = new System.Drawing.Size(30, 15);
            this.topLeft.TabIndex = 6;
            this.topLeft.Text = "(0,1)";
            // 
            // middle
            // 
            this.middle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.middle.AutoSize = true;
            this.middle.Location = new System.Drawing.Point(243, 259);
            this.middle.Name = "middle";
            this.middle.Size = new System.Drawing.Size(30, 15);
            this.middle.TabIndex = 6;
            this.middle.Text = "(1,0)";
            // 
            // loadingBar
            // 
            this.loadingBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingBar.Location = new System.Drawing.Point(530, 497);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(258, 23);
            this.loadingBar.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.CausesValidation = false;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(530, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Resolution";
            // 
            // resBox
            // 
            this.resBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resBox.Location = new System.Drawing.Point(620, 185);
            this.resBox.Name = "resBox";
            this.resBox.Size = new System.Drawing.Size(168, 23);
            this.resBox.TabIndex = 5;
            this.resBox.Text = "128";
            this.resBox.TextChanged += new System.EventHandler(this.ChangeRes);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(530, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(258, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Export to PNG";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SaveAsPNG);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.topLeft);
            this.Controls.Add(this.topRight);
            this.Controls.Add(this.middle);
            this.Controls.Add(this.bottomRight);
            this.Controls.Add(this.bottomLeft);
            this.Controls.Add(this.resBox);
            this.Controls.Add(this.YOffset);
            this.Controls.Add(this.XOffset);
            this.Controls.Add(this.heightText);
            this.Controls.Add(this.widthText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.equationBox);
            this.Controls.Add(this.graphImageBox);
            this.Name = "Graph";
            this.Text = "Graph";
            ((System.ComponentModel.ISupportInitialize)(this.graphImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox graphImageBox;
        private TextBox equationBox;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox widthText;
        private TextBox heightText;
        private TextBox XOffset;
        private TextBox YOffset;
        private Label bottomLeft;
        private Label bottomRight;
        private Label topRight;
        private Label topLeft;
        private Label middle;
        private ProgressBar loadingBar;
        private Label label5;
        private TextBox resBox;
        private Button button2;
    }
}