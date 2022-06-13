namespace YTDSSTGenII.Forms.Pay
{
    partial class FormLotteryQRCode
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
            this.components = new System.ComponentModel.Container();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.wexinGenerateQRCodePanel = new YTDSSTGenII.Forms.Controls.WexinGeneratingQRCodePanel();
            this.wexinCutPaperSuccessPanel = new YTDSSTGenII.Forms.Controls.WexinCutPaperSuccessPanel();
            this.wexinCutPaperFailedPanel = new YTDSSTGenII.Forms.Forms.Controls.WexinCutPaperFailedPanel();
            this.wexinCutingPaperPanel = new YTDSSTGenII.Forms.Forms.Controls.WexinCutingPaperPanel();
            this.wexinNotSupportedPanel = new YTDSSTGenII.Forms.Forms.Controls.WexinNotSupportedPanel();
            this.wexinConnectServerPanel = new YTDSSTGenII.Forms.Forms.Controls.WexinConnectServerPanel();
            this.wexinWaitScanQRCodePanel = new YTDSSTGenII.Forms.Controls.WexinWaitScanQRCodePanel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxLogo
            // 
            this.picBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.picBoxLogo.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.微信Logo;
            this.picBoxLogo.Location = new System.Drawing.Point(141, 32);
            this.picBoxLogo.Name = "picBoxLogo";
            this.picBoxLogo.Size = new System.Drawing.Size(36, 35);
            this.picBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxLogo.TabIndex = 1;
            this.picBoxLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(196, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(266, 39);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "微信支付 立即出票";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.关闭窗口;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(177, 588);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(229, 74);
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // wexinGenerateQRCodePanel
            // 
            this.wexinGenerateQRCodePanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinGenerateQRCodePanel.Location = new System.Drawing.Point(26, 122);
            this.wexinGenerateQRCodePanel.Name = "wexinGenerateQRCodePanel";
            this.wexinGenerateQRCodePanel.Size = new System.Drawing.Size(556, 486);
            this.wexinGenerateQRCodePanel.TabIndex = 0;
            // 
            // wexinCutPaperSuccessPanel
            // 
            this.wexinCutPaperSuccessPanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinCutPaperSuccessPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wexinCutPaperSuccessPanel.Location = new System.Drawing.Point(741, 122);
            this.wexinCutPaperSuccessPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wexinCutPaperSuccessPanel.Name = "wexinCutPaperSuccessPanel";
            this.wexinCutPaperSuccessPanel.Size = new System.Drawing.Size(556, 486);
            this.wexinCutPaperSuccessPanel.TabIndex = 4;
            // 
            // wexinCutPaperFailedPanel
            // 
            this.wexinCutPaperFailedPanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinCutPaperFailedPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wexinCutPaperFailedPanel.Location = new System.Drawing.Point(1497, 122);
            this.wexinCutPaperFailedPanel.Name = "wexinCutPaperFailedPanel";
            this.wexinCutPaperFailedPanel.Size = new System.Drawing.Size(556, 486);
            this.wexinCutPaperFailedPanel.TabIndex = 7;
            // 
            // wexinCutingPaperPanel
            // 
            this.wexinCutingPaperPanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinCutingPaperPanel.Location = new System.Drawing.Point(26, 727);
            this.wexinCutingPaperPanel.Name = "wexinCutingPaperPanel";
            this.wexinCutingPaperPanel.Size = new System.Drawing.Size(556, 486);
            this.wexinCutingPaperPanel.TabIndex = 6;
            // 
            // wexinNotSupportedPanel
            // 
            this.wexinNotSupportedPanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinNotSupportedPanel.Location = new System.Drawing.Point(1486, 662);
            this.wexinNotSupportedPanel.Name = "wexinNotSupportedPanel";
            this.wexinNotSupportedPanel.Size = new System.Drawing.Size(556, 486);
            this.wexinNotSupportedPanel.TabIndex = 8;
            // 
            // wexinConnectServerPanel
            // 
            this.wexinConnectServerPanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinConnectServerPanel.Location = new System.Drawing.Point(1511, 1101);
            this.wexinConnectServerPanel.Name = "wexinConnectServerPanel";
            this.wexinConnectServerPanel.Size = new System.Drawing.Size(556, 486);
            this.wexinConnectServerPanel.TabIndex = 9;
            // 
            // wexinWaitScanQRCodePanel
            // 
            this.wexinWaitScanQRCodePanel.BackColor = System.Drawing.Color.Transparent;
            this.wexinWaitScanQRCodePanel.Location = new System.Drawing.Point(741, 677);
            this.wexinWaitScanQRCodePanel.Name = "wexinWaitScanQRCodePanel";
            this.wexinWaitScanQRCodePanel.Size = new System.Drawing.Size(556, 486);
            this.wexinWaitScanQRCodePanel.TabIndex = 11;
            this.wexinWaitScanQRCodePanel.Load += new System.EventHandler(this.wexinWaitScanQRCodePanel_Load);
            // 
            // FormLotteryQRCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.支付弹出框背景;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(2065, 1446);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picBoxLogo);
            this.Controls.Add(this.wexinGenerateQRCodePanel);
            this.Controls.Add(this.wexinCutPaperSuccessPanel);
            this.Controls.Add(this.wexinCutPaperFailedPanel);
            this.Controls.Add(this.wexinCutingPaperPanel);
            this.Controls.Add(this.wexinNotSupportedPanel);
            this.Controls.Add(this.wexinConnectServerPanel);
            this.Controls.Add(this.wexinWaitScanQRCodePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLotteryQRCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormLotteryQRCode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLotteryQRCode_FormClosing);
            this.Load += new System.EventHandler(this.FormLotteryQRCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.WexinGeneratingQRCodePanel wexinGenerateQRCodePanel;
        private System.Windows.Forms.PictureBox picBoxLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timerCountDown;
        private Controls.WexinCutPaperSuccessPanel wexinCutPaperSuccessPanel;
        private Forms.Controls.WexinCutingPaperPanel wexinCutingPaperPanel;
        private Forms.Controls.WexinCutPaperFailedPanel wexinCutPaperFailedPanel;
        private Forms.Controls.WexinNotSupportedPanel wexinNotSupportedPanel;
        private Forms.Controls.WexinConnectServerPanel wexinConnectServerPanel;
        private Controls.WexinWaitScanQRCodePanel wexinWaitScanQRCodePanel;


    }
}