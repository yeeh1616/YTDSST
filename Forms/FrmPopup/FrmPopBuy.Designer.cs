namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    partial class FrmPopBuy
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
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.plDown = new System.Windows.Forms.Panel();
            this.plMiddle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_lottery08_tip = new System.Windows.Forms.Label();
            this.lab_lottery06_tip = new System.Windows.Forms.Label();
            this.lab_lottery04_tip = new System.Windows.Forms.Label();
            this.lab_lottery02_tip = new System.Windows.Forms.Label();
            this.lab_lottery07_tip = new System.Windows.Forms.Label();
            this.lab_lottery05_tip = new System.Windows.Forms.Label();
            this.lab_lottery03_tip = new System.Windows.Forms.Label();
            this.lab_lottery01_tip = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.labPayCutDown = new System.Windows.Forms.Label();
            this.lab_loading_tips = new System.Windows.Forms.Label();
            this.pBox_QRCode = new System.Windows.Forms.PictureBox();
            this.lab_tip_weixin = new System.Windows.Forms.Label();
            this.btnWechat = new System.Windows.Forms.PictureBox();
            this.btnCash = new System.Windows.Forms.PictureBox();
            this.plUP = new System.Windows.Forms.Panel();
            this.pBox_zflc = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAlipay = new System.Windows.Forms.PictureBox();
            this.pBoxSurePay = new System.Windows.Forms.PictureBox();
            this.lab_money_tip06 = new System.Windows.Forms.Label();
            this.lab_money_tip05 = new System.Windows.Forms.Label();
            this.lab_money_tip04 = new System.Windows.Forms.Label();
            this.lab_money_tip03 = new System.Windows.Forms.Label();
            this.lab_money_tip02 = new System.Windows.Forms.Label();
            this.lab_money_tip01 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.plDown.SuspendLayout();
            this.plMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_QRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWechat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCash)).BeginInit();
            this.plUP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_zflc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlipay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxSurePay)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.btn_close1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Location = new System.Drawing.Point(534, 22);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 48);
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // plDown
            // 
            this.plDown.BackColor = System.Drawing.Color.Transparent;
            this.plDown.Controls.Add(this.plMiddle);
            this.plDown.Controls.Add(this.lbPrice);
            this.plDown.Location = new System.Drawing.Point(1, 672);
            this.plDown.Margin = new System.Windows.Forms.Padding(4);
            this.plDown.Name = "plDown";
            this.plDown.Size = new System.Drawing.Size(597, 292);
            this.plDown.TabIndex = 5;
            // 
            // plMiddle
            // 
            this.plMiddle.BackColor = System.Drawing.Color.Transparent;
            this.plMiddle.Controls.Add(this.label2);
            this.plMiddle.Controls.Add(this.lab_lottery08_tip);
            this.plMiddle.Controls.Add(this.lab_lottery06_tip);
            this.plMiddle.Controls.Add(this.lab_lottery04_tip);
            this.plMiddle.Controls.Add(this.lab_lottery02_tip);
            this.plMiddle.Controls.Add(this.lab_lottery07_tip);
            this.plMiddle.Controls.Add(this.lab_lottery05_tip);
            this.plMiddle.Controls.Add(this.lab_lottery03_tip);
            this.plMiddle.Controls.Add(this.lab_lottery01_tip);
            this.plMiddle.Controls.Add(this.label6);
            this.plMiddle.Controls.Add(this.label5);
            this.plMiddle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.plMiddle.Location = new System.Drawing.Point(2, 56);
            this.plMiddle.Margin = new System.Windows.Forms.Padding(4);
            this.plMiddle.Name = "plMiddle";
            this.plMiddle.Size = new System.Drawing.Size(594, 216);
            this.plMiddle.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(38, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(565, 32);
            this.label2.TabIndex = 15;
            this.label2.Text = "如订单有误，请关闭窗口后点“清空”重新选择。";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lab_lottery08_tip
            // 
            this.lab_lottery08_tip.AutoSize = true;
            this.lab_lottery08_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery08_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery08_tip.Location = new System.Drawing.Point(331, 130);
            this.lab_lottery08_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery08_tip.Name = "lab_lottery08_tip";
            this.lab_lottery08_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery08_tip.TabIndex = 14;
            this.lab_lottery08_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery06_tip
            // 
            this.lab_lottery06_tip.AutoSize = true;
            this.lab_lottery06_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery06_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery06_tip.Location = new System.Drawing.Point(331, 94);
            this.lab_lottery06_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery06_tip.Name = "lab_lottery06_tip";
            this.lab_lottery06_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery06_tip.TabIndex = 13;
            this.lab_lottery06_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery04_tip
            // 
            this.lab_lottery04_tip.AutoSize = true;
            this.lab_lottery04_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery04_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery04_tip.Location = new System.Drawing.Point(334, 57);
            this.lab_lottery04_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery04_tip.Name = "lab_lottery04_tip";
            this.lab_lottery04_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery04_tip.TabIndex = 12;
            this.lab_lottery04_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery02_tip
            // 
            this.lab_lottery02_tip.AutoSize = true;
            this.lab_lottery02_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery02_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery02_tip.Location = new System.Drawing.Point(334, 19);
            this.lab_lottery02_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery02_tip.Name = "lab_lottery02_tip";
            this.lab_lottery02_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery02_tip.TabIndex = 11;
            this.lab_lottery02_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery07_tip
            // 
            this.lab_lottery07_tip.AutoSize = true;
            this.lab_lottery07_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery07_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery07_tip.Location = new System.Drawing.Point(39, 130);
            this.lab_lottery07_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery07_tip.Name = "lab_lottery07_tip";
            this.lab_lottery07_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery07_tip.TabIndex = 10;
            this.lab_lottery07_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery05_tip
            // 
            this.lab_lottery05_tip.AutoSize = true;
            this.lab_lottery05_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery05_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery05_tip.Location = new System.Drawing.Point(39, 94);
            this.lab_lottery05_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery05_tip.Name = "lab_lottery05_tip";
            this.lab_lottery05_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery05_tip.TabIndex = 9;
            this.lab_lottery05_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery03_tip
            // 
            this.lab_lottery03_tip.AutoSize = true;
            this.lab_lottery03_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery03_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery03_tip.Location = new System.Drawing.Point(39, 57);
            this.lab_lottery03_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery03_tip.Name = "lab_lottery03_tip";
            this.lab_lottery03_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery03_tip.TabIndex = 8;
            this.lab_lottery03_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            // 
            // lab_lottery01_tip
            // 
            this.lab_lottery01_tip.AutoSize = true;
            this.lab_lottery01_tip.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_lottery01_tip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_lottery01_tip.Location = new System.Drawing.Point(39, 19);
            this.lab_lottery01_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_lottery01_tip.Name = "lab_lottery01_tip";
            this.lab_lottery01_tip.Size = new System.Drawing.Size(236, 28);
            this.lab_lottery01_tip.TabIndex = 7;
            this.lab_lottery01_tip.Text = "2 x 甜蜜蜜（5元） 10元";
            this.lab_lottery01_tip.Click += new System.EventHandler(this.lab_lottery01_tip_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label6.Location = new System.Drawing.Point(461, 281);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 48);
            this.label6.TabIndex = 6;
            this.label6.Text = "元";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label5.Location = new System.Drawing.Point(261, 278);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 48);
            this.label5.TabIndex = 5;
            this.label5.Text = "金额：";
            // 
            // lbPrice
            // 
            this.lbPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrice.AutoSize = true;
            this.lbPrice.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lbPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbPrice.Location = new System.Drawing.Point(172, 5);
            this.lbPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(249, 50);
            this.lbPrice.TabIndex = 0;
            this.lbPrice.Text = "总金额: 20 元";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labPayCutDown
            // 
            this.labPayCutDown.AutoSize = true;
            this.labPayCutDown.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.labPayCutDown.Location = new System.Drawing.Point(137, 350);
            this.labPayCutDown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPayCutDown.Name = "labPayCutDown";
            this.labPayCutDown.Size = new System.Drawing.Size(272, 36);
            this.labPayCutDown.TabIndex = 15;
            this.labPayCutDown.Text = "请在120s内完成支付";
            // 
            // lab_loading_tips
            // 
            this.lab_loading_tips.AutoSize = true;
            this.lab_loading_tips.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_loading_tips.ForeColor = System.Drawing.Color.Red;
            this.lab_loading_tips.Location = new System.Drawing.Point(137, 300);
            this.lab_loading_tips.Name = "lab_loading_tips";
            this.lab_loading_tips.Size = new System.Drawing.Size(289, 32);
            this.lab_loading_tips.TabIndex = 14;
            this.lab_loading_tips.Text = "正在加载二维码,请稍后...";
            // 
            // pBox_QRCode
            // 
            this.pBox_QRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pBox_QRCode.Image = global::YTDSSTGenII.Forms.Properties.Resources.loading;
            this.pBox_QRCode.Location = new System.Drawing.Point(143, 136);
            this.pBox_QRCode.Margin = new System.Windows.Forms.Padding(4);
            this.pBox_QRCode.Name = "pBox_QRCode";
            this.pBox_QRCode.Size = new System.Drawing.Size(140, 140);
            this.pBox_QRCode.TabIndex = 11;
            this.pBox_QRCode.TabStop = false;
            // 
            // lab_tip_weixin
            // 
            this.lab_tip_weixin.AutoSize = true;
            this.lab_tip_weixin.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_tip_weixin.Location = new System.Drawing.Point(311, 176);
            this.lab_tip_weixin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_tip_weixin.Name = "lab_tip_weixin";
            this.lab_tip_weixin.Size = new System.Drawing.Size(239, 36);
            this.lab_tip_weixin.TabIndex = 12;
            this.lab_tip_weixin.Text = "扫码支付无需找零";
            // 
            // btnWechat
            // 
            this.btnWechat.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_on;
            this.btnWechat.Location = new System.Drawing.Point(222, 25);
            this.btnWechat.Margin = new System.Windows.Forms.Padding(4);
            this.btnWechat.Name = "btnWechat";
            this.btnWechat.Size = new System.Drawing.Size(151, 77);
            this.btnWechat.TabIndex = 6;
            this.btnWechat.TabStop = false;
            this.btnWechat.Click += new System.EventHandler(this.btnWechat_Click);
            // 
            // btnCash
            // 
            this.btnCash.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.xianjin_off;
            this.btnCash.Location = new System.Drawing.Point(26, 25);
            this.btnCash.Margin = new System.Windows.Forms.Padding(4);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(151, 77);
            this.btnCash.TabIndex = 5;
            this.btnCash.TabStop = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // plUP
            // 
            this.plUP.BackColor = System.Drawing.Color.Transparent;
            this.plUP.Controls.Add(this.pBox_zflc);
            this.plUP.Controls.Add(this.btnClose);
            this.plUP.Controls.Add(this.label1);
            this.plUP.Controls.Add(this.pictureBox1);
            this.plUP.Dock = System.Windows.Forms.DockStyle.Top;
            this.plUP.Location = new System.Drawing.Point(0, 0);
            this.plUP.Margin = new System.Windows.Forms.Padding(4);
            this.plUP.Name = "plUP";
            this.plUP.Size = new System.Drawing.Size(601, 243);
            this.plUP.TabIndex = 6;
            this.plUP.Paint += new System.Windows.Forms.PaintEventHandler(this.plUP_Paint);
            // 
            // pBox_zflc
            // 
            this.pBox_zflc.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_liucheng;
            this.pBox_zflc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pBox_zflc.Location = new System.Drawing.Point(2, 93);
            this.pBox_zflc.Margin = new System.Windows.Forms.Padding(4);
            this.pBox_zflc.Name = "pBox_zflc";
            this.pBox_zflc.Size = new System.Drawing.Size(597, 149);
            this.pBox_zflc.TabIndex = 5;
            this.pBox_zflc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(260, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "已选彩票";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.tu;
            this.pictureBox1.Location = new System.Drawing.Point(207, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 49);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnAlipay);
            this.panel1.Controls.Add(this.pBoxSurePay);
            this.panel1.Controls.Add(this.lab_money_tip06);
            this.panel1.Controls.Add(this.lab_money_tip05);
            this.panel1.Controls.Add(this.lab_money_tip04);
            this.panel1.Controls.Add(this.lab_money_tip03);
            this.panel1.Controls.Add(this.lab_money_tip02);
            this.panel1.Controls.Add(this.lab_money_tip01);
            this.panel1.Controls.Add(this.btnWechat);
            this.panel1.Controls.Add(this.labPayCutDown);
            this.panel1.Controls.Add(this.btnCash);
            this.panel1.Controls.Add(this.lab_tip_weixin);
            this.panel1.Controls.Add(this.lab_loading_tips);
            this.panel1.Controls.Add(this.pBox_QRCode);
            this.panel1.Location = new System.Drawing.Point(3, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 424);
            this.panel1.TabIndex = 7;
            // 
            // btnAlipay
            // 
            this.btnAlipay.BackColor = System.Drawing.Color.Transparent;
            this.btnAlipay.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.alipay_off;
            this.btnAlipay.Location = new System.Drawing.Point(418, 25);
            this.btnAlipay.Margin = new System.Windows.Forms.Padding(4);
            this.btnAlipay.Name = "btnAlipay";
            this.btnAlipay.Size = new System.Drawing.Size(151, 77);
            this.btnAlipay.TabIndex = 22;
            this.btnAlipay.TabStop = false;
            this.btnAlipay.Click += new System.EventHandler(this.btnAlipay_Click);
            // 
            // pBoxSurePay
            // 
            this.pBoxSurePay.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.点击购买;
            this.pBoxSurePay.Location = new System.Drawing.Point(190, 269);
            this.pBoxSurePay.Name = "pBoxSurePay";
            this.pBoxSurePay.Size = new System.Drawing.Size(251, 62);
            this.pBoxSurePay.TabIndex = 21;
            this.pBoxSurePay.TabStop = false;
            this.pBoxSurePay.Click += new System.EventHandler(this.pBoxSurePay_Click);
            // 
            // lab_money_tip06
            // 
            this.lab_money_tip06.AutoSize = true;
            this.lab_money_tip06.Font = new System.Drawing.Font("微软雅黑", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip06.Location = new System.Drawing.Point(376, 207);
            this.lab_money_tip06.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip06.Name = "lab_money_tip06";
            this.lab_money_tip06.Size = new System.Drawing.Size(42, 35);
            this.lab_money_tip06.TabIndex = 20;
            this.lab_money_tip06.Text = "元";
            // 
            // lab_money_tip05
            // 
            this.lab_money_tip05.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_money_tip05.AutoSize = true;
            this.lab_money_tip05.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip05.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lab_money_tip05.Location = new System.Drawing.Point(303, 202);
            this.lab_money_tip05.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip05.Name = "lab_money_tip05";
            this.lab_money_tip05.Size = new System.Drawing.Size(60, 44);
            this.lab_money_tip05.TabIndex = 18;
            this.lab_money_tip05.Text = "20";
            this.lab_money_tip05.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lab_money_tip04
            // 
            this.lab_money_tip04.AutoSize = true;
            this.lab_money_tip04.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip04.Location = new System.Drawing.Point(120, 207);
            this.lab_money_tip04.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip04.Name = "lab_money_tip04";
            this.lab_money_tip04.Size = new System.Drawing.Size(183, 36);
            this.lab_money_tip04.TabIndex = 19;
            this.lab_money_tip04.Text = "还需继续投币";
            // 
            // lab_money_tip03
            // 
            this.lab_money_tip03.AutoSize = true;
            this.lab_money_tip03.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip03.Location = new System.Drawing.Point(376, 156);
            this.lab_money_tip03.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip03.Name = "lab_money_tip03";
            this.lab_money_tip03.Size = new System.Drawing.Size(43, 36);
            this.lab_money_tip03.TabIndex = 17;
            this.lab_money_tip03.Text = "元";
            // 
            // lab_money_tip02
            // 
            this.lab_money_tip02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_money_tip02.AutoSize = true;
            this.lab_money_tip02.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lab_money_tip02.Location = new System.Drawing.Point(303, 152);
            this.lab_money_tip02.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip02.Name = "lab_money_tip02";
            this.lab_money_tip02.Size = new System.Drawing.Size(60, 44);
            this.lab_money_tip02.TabIndex = 8;
            this.lab_money_tip02.Text = "20";
            this.lab_money_tip02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lab_money_tip01
            // 
            this.lab_money_tip01.AutoSize = true;
            this.lab_money_tip01.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lab_money_tip01.Location = new System.Drawing.Point(120, 156);
            this.lab_money_tip01.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_money_tip01.Name = "lab_money_tip01";
            this.lab_money_tip01.Size = new System.Drawing.Size(183, 36);
            this.lab_money_tip01.TabIndex = 16;
            this.lab_money_tip01.Text = "当前可用余额";
            // 
            // FrmPopBuy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.di;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(601, 980);
            this.Controls.Add(this.plDown);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.plUP);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPopBuy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmPopBuyPartialFailure";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPopBuy_FormClosing);
            this.Load += new System.EventHandler(this.FrmPopBuy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.plDown.ResumeLayout(false);
            this.plDown.PerformLayout();
            this.plMiddle.ResumeLayout(false);
            this.plMiddle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_QRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWechat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCash)).EndInit();
            this.plUP.ResumeLayout(false);
            this.plUP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBox_zflc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlipay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxSurePay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Panel plDown;
        private System.Windows.Forms.Panel plUP;
        private System.Windows.Forms.Panel plMiddle;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.PictureBox btnWechat;
        private System.Windows.Forms.PictureBox btnCash;
        private System.Windows.Forms.PictureBox pBox_zflc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_lottery07_tip;
        private System.Windows.Forms.Label lab_lottery05_tip;
        private System.Windows.Forms.Label lab_lottery03_tip;
        private System.Windows.Forms.Label lab_lottery01_tip;
        private System.Windows.Forms.Label lab_lottery08_tip;
        private System.Windows.Forms.Label lab_lottery06_tip;
        private System.Windows.Forms.Label lab_lottery04_tip;
        private System.Windows.Forms.Label lab_lottery02_tip;
        private System.Windows.Forms.PictureBox pBox_QRCode;
        private System.Windows.Forms.Label lab_tip_weixin;
        private System.Windows.Forms.Label lab_loading_tips;
        private System.Windows.Forms.Label labPayCutDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lab_money_tip06;
        private System.Windows.Forms.Label lab_money_tip05;
        private System.Windows.Forms.Label lab_money_tip04;
        private System.Windows.Forms.Label lab_money_tip03;
        private System.Windows.Forms.Label lab_money_tip02;
        private System.Windows.Forms.Label lab_money_tip01;
        private System.Windows.Forms.PictureBox pBoxSurePay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnAlipay;
    }
}