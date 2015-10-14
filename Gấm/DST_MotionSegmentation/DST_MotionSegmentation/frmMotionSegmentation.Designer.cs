namespace DST_MotionSegmentation
{
    partial class frmMotionSegmentation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMotionSegmentation));
            this.picOrigin = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.picAvg = new System.Windows.Forms.PictureBox();
            this.lblOriginFrame = new System.Windows.Forms.Label();
            this.lblSegmentedFrame = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvg)).BeginInit();
            this.SuspendLayout();
            // 
            // picOrigin
            // 
            this.picOrigin.Location = new System.Drawing.Point(8, 30);
            this.picOrigin.Name = "picOrigin";
            this.picOrigin.Size = new System.Drawing.Size(640, 480);
            this.picOrigin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOrigin.TabIndex = 0;
            this.picOrigin.TabStop = false;
            this.picOrigin.Click += new System.EventHandler(this.picOrigin_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(332, 536);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(292, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Power by DST group - Hanoi University of Industry";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(333, 554);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Email: dst06@googlegroups.com";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 553);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Email: dst06@googlegroups.com";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Thực hiện bởi nhóm DST - Trường ĐH Công nghiệp Hà Nội";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(332, 516);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Motion Segmentation Technique";
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(5, 516);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(292, 20);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "Kỹ thuật phân đoạn chuyển động";
            // 
            // picAvg
            // 
            this.picAvg.Location = new System.Drawing.Point(654, 30);
            this.picAvg.Name = "picAvg";
            this.picAvg.Size = new System.Drawing.Size(640, 480);
            this.picAvg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvg.TabIndex = 0;
            this.picAvg.TabStop = false;
            // 
            // lblOriginFrame
            // 
            this.lblOriginFrame.AutoSize = true;
            this.lblOriginFrame.Location = new System.Drawing.Point(7, 11);
            this.lblOriginFrame.Name = "lblOriginFrame";
            this.lblOriginFrame.Size = new System.Drawing.Size(73, 13);
            this.lblOriginFrame.TabIndex = 10;
            this.lblOriginFrame.Text = "Current Frame";
            // 
            // lblSegmentedFrame
            // 
            this.lblSegmentedFrame.AutoSize = true;
            this.lblSegmentedFrame.Location = new System.Drawing.Point(655, 9);
            this.lblSegmentedFrame.Name = "lblSegmentedFrame";
            this.lblSegmentedFrame.Size = new System.Drawing.Size(93, 13);
            this.lblSegmentedFrame.TabIndex = 10;
            this.lblSegmentedFrame.Text = "Segmented Frame";
            // 
            // frmMotionSegmentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 582);
            this.Controls.Add(this.lblSegmentedFrame);
            this.Controls.Add(this.lblOriginFrame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.picAvg);
            this.Controls.Add(this.picOrigin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMotionSegmentation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[DST] Motion Segmentation";
            this.Load += new System.EventHandler(this.frmMotionSegmentation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picOrigin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox picAvg;
        private System.Windows.Forms.Label lblOriginFrame;
        private System.Windows.Forms.Label lblSegmentedFrame;
    }
}

