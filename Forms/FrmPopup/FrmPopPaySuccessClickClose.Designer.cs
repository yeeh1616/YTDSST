namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    partial class FrmPopPaySuccessClickClose
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
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.lbTiming = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.kulian;
            this.pictureBox3.Location = new System.Drawing.Point(156, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(49, 49);
            this.pictureBox3.TabIndex = 27;
            this.pictureBox3.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(215, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(306, 41);
            this.label9.TabIndex = 26;
            this.label9.Text = "哎呀，出票失败了！";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(181, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 41);
            this.label3.TabIndex = 25;
            this.label3.Text = "是否确认关闭？";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(561, 35);
            this.label2.TabIndex = 24;
            this.label2.Text = "请您确定已记录出票失败的交易订单号和出票码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(234, 595);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 35);
            this.label1.TabIndex = 23;
            this.label1.Text = "秒后窗口自动关闭";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.quxiao;
            this.pictureBox2.Location = new System.Drawing.Point(340, 476);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(167, 74);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.queding;
            this.btnClose.Location = new System.Drawing.Point(96, 476);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(167, 74);
            this.btnClose.TabIndex = 21;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbTiming
            // 
            this.lbTiming.BackColor = System.Drawing.Color.Transparent;
            this.lbTiming.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lbTiming.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbTiming.Location = new System.Drawing.Point(174, 582);
            this.lbTiming.Name = "lbTiming";
            this.lbTiming.Size = new System.Drawing.Size(70, 53);
            this.lbTiming.TabIndex = 20;
            this.lbTiming.Text = "59";
            this.lbTiming.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPopPaySuccessClickClose
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.di;
            this.ClientSize = new System.Drawing.Size(606, 696);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbTiming);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPopPaySuccessClickClose";
            this.Text = "FrmPopBuyPartialFailure";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPopPaySuccessClickClose_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label lbTiming;
    }
}