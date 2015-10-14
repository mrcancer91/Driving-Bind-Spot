namespace Driving_Blind_Spot_Rebuild
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSobel = new System.Windows.Forms.Button();
            this.btnMorphologicalClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBinary = new System.Windows.Forms.Button();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbRect = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCols = new System.Windows.Forms.TextBox();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbShape = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIter = new System.Windows.Forms.TextBox();
            this.txtAppetureSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtXorder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtYorder = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(617, 515);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(638, 325);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(99, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSobel
            // 
            this.btnSobel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSobel.Location = new System.Drawing.Point(638, 267);
            this.btnSobel.Name = "btnSobel";
            this.btnSobel.Size = new System.Drawing.Size(99, 23);
            this.btnSobel.TabIndex = 2;
            this.btnSobel.Text = "Sobel mask";
            this.btnSobel.UseVisualStyleBackColor = true;
            this.btnSobel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnMorphologicalClose
            // 
            this.btnMorphologicalClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMorphologicalClose.Location = new System.Drawing.Point(743, 267);
            this.btnMorphologicalClose.Name = "btnMorphologicalClose";
            this.btnMorphologicalClose.Size = new System.Drawing.Size(99, 23);
            this.btnMorphologicalClose.TabIndex = 3;
            this.btnMorphologicalClose.Text = "Close - Horizontal";
            this.btnMorphologicalClose.UseVisualStyleBackColor = true;
            this.btnMorphologicalClose.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Projection";
            // 
            // btnBinary
            // 
            this.btnBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBinary.Location = new System.Drawing.Point(794, 296);
            this.btnBinary.Name = "btnBinary";
            this.btnBinary.Size = new System.Drawing.Size(48, 23);
            this.btnBinary.TabIndex = 7;
            this.btnBinary.Text = "Binary";
            this.btnBinary.UseVisualStyleBackColor = true;
            this.btnBinary.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtThreshold
            // 
            this.txtThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThreshold.Location = new System.Drawing.Point(744, 298);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(44, 20);
            this.txtThreshold.TabIndex = 8;
            this.txtThreshold.Text = "120";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(638, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Close - Vertical";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(744, 325);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Process";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // lbRect
            // 
            this.lbRect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRect.AutoSize = true;
            this.lbRect.Location = new System.Drawing.Point(650, 517);
            this.lbRect.Name = "lbRect";
            this.lbRect.Size = new System.Drawing.Size(0, 13);
            this.lbRect.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(636, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Cols: ";
            // 
            // txtCols
            // 
            this.txtCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCols.Location = new System.Drawing.Point(666, 354);
            this.txtCols.Name = "txtCols";
            this.txtCols.Size = new System.Drawing.Size(44, 20);
            this.txtCols.TabIndex = 15;
            this.txtCols.Text = "3";
            // 
            // txtRows
            // 
            this.txtRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRows.Location = new System.Drawing.Point(754, 354);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(44, 20);
            this.txtRows.TabIndex = 17;
            this.txtRows.Text = "3";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(714, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Rows";
            // 
            // txtY
            // 
            this.txtY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtY.Location = new System.Drawing.Point(754, 380);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(44, 20);
            this.txtY.TabIndex = 21;
            this.txtY.Text = "1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(716, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Y";
            // 
            // txtX
            // 
            this.txtX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtX.Location = new System.Drawing.Point(666, 380);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(44, 20);
            this.txtX.TabIndex = 19;
            this.txtX.Text = "1";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(636, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "X";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(690, 459);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 24);
            this.button3.TabIndex = 22;
            this.button3.Text = "Test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // cmbShape
            // 
            this.cmbShape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShape.FormattingEnabled = true;
            this.cmbShape.Items.AddRange(new object[] {
            "RECTANGLE",
            "CROSS",
            "ELLIPSE"});
            this.cmbShape.Location = new System.Drawing.Point(679, 406);
            this.cmbShape.Name = "cmbShape";
            this.cmbShape.Size = new System.Drawing.Size(109, 21);
            this.cmbShape.TabIndex = 23;
            this.cmbShape.Text = "RECTANGLE";
            this.cmbShape.SelectedIndexChanged += new System.EventHandler(this.cmbShape_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(635, 409);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Shape";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(691, 489);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 25);
            this.button4.TabIndex = 25;
            this.button4.Text = "Detect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(635, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Closing Iteration";
            // 
            // txtIter
            // 
            this.txtIter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIter.Location = new System.Drawing.Point(723, 433);
            this.txtIter.Name = "txtIter";
            this.txtIter.Size = new System.Drawing.Size(44, 20);
            this.txtIter.TabIndex = 29;
            this.txtIter.Text = "8";
            // 
            // txtAppetureSize
            // 
            this.txtAppetureSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppetureSize.Location = new System.Drawing.Point(731, 226);
            this.txtAppetureSize.Name = "txtAppetureSize";
            this.txtAppetureSize.Size = new System.Drawing.Size(44, 20);
            this.txtAppetureSize.TabIndex = 33;
            this.txtAppetureSize.Text = "5";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(655, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "AppetureSize";
            // 
            // txtXorder
            // 
            this.txtXorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXorder.Location = new System.Drawing.Point(693, 200);
            this.txtXorder.Name = "txtXorder";
            this.txtXorder.Size = new System.Drawing.Size(44, 20);
            this.txtXorder.TabIndex = 31;
            this.txtXorder.Text = "1";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(653, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "xOrder";
            // 
            // txtYorder
            // 
            this.txtYorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYorder.Location = new System.Drawing.Point(790, 200);
            this.txtYorder.Name = "txtYorder";
            this.txtYorder.Size = new System.Drawing.Size(44, 20);
            this.txtYorder.TabIndex = 35;
            this.txtYorder.Text = "2";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(750, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "yOrder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 539);
            this.Controls.Add(this.txtYorder);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAppetureSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtXorder);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtIter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbShape);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCols);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbRect);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.btnBinary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMorphologicalClose);
            this.Controls.Add(this.btnSobel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSobel;
        private System.Windows.Forms.Button btnMorphologicalClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBinary;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbRect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCols;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cmbShape;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIter;
        private System.Windows.Forms.TextBox txtAppetureSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtXorder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtYorder;
        private System.Windows.Forms.Label label10;
    }
}

