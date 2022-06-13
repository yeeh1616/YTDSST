namespace YTDSSTGenII.Forms.Controls
{
    partial class WexinGeneratingQRCodePanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMainMessage = new System.Windows.Forms.Label();
            this.lblQRCodeMessage = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.picBoxQRCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(9, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "金额：";
            // 
            // lblMoney
            // 
            this.lblMoney.Font = new System.Drawing.Font("微软雅黑", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMoney.ForeColor = System.Drawing.Color.Red;
            this.lblMoney.Location = new System.Drawing.Point(107, 283);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(107, 57);
            this.lblMoney.TabIndex = 3;
            this.lblMoney.Text = "5";
            this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(201, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "元";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblMainMessage
            // 
            this.lblMainMessage.AutoSize = true;
            this.lblMainMessage.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMainMessage.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMainMessage.Location = new System.Drawing.Point(96, 376);
            this.lblMainMessage.Name = "lblMainMessage";
            this.lblMainMessage.Size = new System.Drawing.Size(377, 39);
            this.lblMainMessage.TabIndex = 6;
            this.lblMainMessage.Text = "二维码生成中，请耐心等待";
            // 
            // lblQRCodeMessage
            // 
            this.lblQRCodeMessage.AutoSize = true;
            this.lblQRCodeMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblQRCodeMessage.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQRCodeMessage.Location = new System.Drawing.Point(18, 104);
            this.lblQRCodeMessage.Name = "lblQRCodeMessage";
            this.lblQRCodeMessage.Size = new System.Drawing.Size(227, 39);
            this.lblQRCodeMessage.TabIndex = 7;
            this.lblQRCodeMessage.Text = "正在生成二维码";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.手机扫码提示图;
            this.pictureBox2.Location = new System.Drawing.Point(292, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 248);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // picBoxQRCode
            // 
            this.picBoxQRCode.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.二维码占位符;
            this.picBoxQRCode.Location = new System.Drawing.Point(17, 16);
            this.picBoxQRCode.Name = "picBoxQRCode";
            this.picBoxQRCode.Size = new System.Drawing.Size(228, 228);
            this.picBoxQRCode.TabIndex = 0;
            this.picBoxQRCode.TabStop = false;
            // 
            // WexinGeneratingQRCodePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblQRCodeMessage);
            this.Controls.Add(this.lblMainMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.picBoxQRCode);
            this.Name = "WexinGeneratingQRCodePanel";
            this.Size = new System.Drawing.Size(556, 486);
            this.Load += new System.EventHandler(this.WexinPayPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxQRCode;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMainMessage;
        private System.Windows.Forms.Label lblQRCodeMessage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
