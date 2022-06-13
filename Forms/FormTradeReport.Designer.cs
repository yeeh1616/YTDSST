namespace YTDSSTGenII.Forms
{
    partial class FormTradeReport
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
            this.lblLocalNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoQingChao = new System.Windows.Forms.RadioButton();
            this.labelDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.linLblPrize = new System.Windows.Forms.LinkLabel();
            this.lblReceCash = new System.Windows.Forms.LinkLabel();
            this.lblLotteryPrize = new System.Windows.Forms.LinkLabel();
            this.lblTrade = new System.Windows.Forms.LinkLabel();
            this.lblPayout = new System.Windows.Forms.LinkLabel();
            this.lblEatCash = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvTradeList = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTradeCash = new System.Windows.Forms.LinkLabel();
            this.lblTradeWeiXin = new System.Windows.Forms.LinkLabel();
            this.lblTradeZhifubao = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTradeList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLocalNumber
            // 
            this.lblLocalNumber.AutoSize = true;
            this.lblLocalNumber.Location = new System.Drawing.Point(26, 35);
            this.lblLocalNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocalNumber.Name = "lblLocalNumber";
            this.lblLocalNumber.Size = new System.Drawing.Size(171, 35);
            this.lblLocalNumber.TabIndex = 0;
            this.lblLocalNumber.Text = "终端机编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(356, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = "上次清钞时间：";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(59, 79);
            this.rdoAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(222, 39);
            this.rdoAll.TabIndex = 2;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "上次清钞到现在";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.CheckedChanged += new System.EventHandler(this.rdoAll_CheckedChanged);
            // 
            // rdoQingChao
            // 
            this.rdoQingChao.AutoSize = true;
            this.rdoQingChao.Location = new System.Drawing.Point(320, 79);
            this.rdoQingChao.Margin = new System.Windows.Forms.Padding(4);
            this.rdoQingChao.Name = "rdoQingChao";
            this.rdoQingChao.Size = new System.Drawing.Size(170, 39);
            this.rdoQingChao.TabIndex = 3;
            this.rdoQingChao.TabStop = true;
            this.rdoQingChao.Text = "按时间搜索";
            this.rdoQingChao.UseVisualStyleBackColor = true;
            this.rdoQingChao.CheckedChanged += new System.EventHandler(this.rdoQingChao_CheckedChanged);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(31, 136);
            this.labelDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(93, 35);
            this.labelDate.TabIndex = 4;
            this.labelDate.Text = "日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 133);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(108, 132);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(207, 41);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(363, 131);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(204, 41);
            this.dateTimePicker2.TabIndex = 7;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // linLblPrize
            // 
            this.linLblPrize.AutoSize = true;
            this.linLblPrize.Location = new System.Drawing.Point(15, 207);
            this.linLblPrize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linLblPrize.Name = "linLblPrize";
            this.linLblPrize.Size = new System.Drawing.Size(186, 35);
            this.linLblPrize.TabIndex = 8;
            this.linLblPrize.TabStop = true;
            this.linLblPrize.Text = "待兑奖金额0元";
            this.linLblPrize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linLblPrize_LinkClicked);
            // 
            // lblReceCash
            // 
            this.lblReceCash.AutoSize = true;
            this.lblReceCash.Location = new System.Drawing.Point(26, 252);
            this.lblReceCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceCash.Name = "lblReceCash";
            this.lblReceCash.Size = new System.Drawing.Size(160, 35);
            this.lblReceCash.TabIndex = 9;
            this.lblReceCash.TabStop = true;
            this.lblReceCash.Text = "收入金额0元";
            this.lblReceCash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblReceCash_LinkClicked);
            // 
            // lblLotteryPrize
            // 
            this.lblLotteryPrize.AutoSize = true;
            this.lblLotteryPrize.Location = new System.Drawing.Point(26, 301);
            this.lblLotteryPrize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLotteryPrize.Name = "lblLotteryPrize";
            this.lblLotteryPrize.Size = new System.Drawing.Size(160, 35);
            this.lblLotteryPrize.TabIndex = 10;
            this.lblLotteryPrize.TabStop = true;
            this.lblLotteryPrize.Text = "兑奖金额0元";
            this.lblLotteryPrize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLotteryPrize_LinkClicked);
            // 
            // lblTrade
            // 
            this.lblTrade.AutoSize = true;
            this.lblTrade.Location = new System.Drawing.Point(26, 353);
            this.lblTrade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrade.Name = "lblTrade";
            this.lblTrade.Size = new System.Drawing.Size(160, 35);
            this.lblTrade.TabIndex = 11;
            this.lblTrade.TabStop = true;
            this.lblTrade.Text = "购彩交易0元";
            this.lblTrade.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblTrade_LinkClicked);
            // 
            // lblPayout
            // 
            this.lblPayout.AutoSize = true;
            this.lblPayout.Location = new System.Drawing.Point(27, 548);
            this.lblPayout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPayout.Name = "lblPayout";
            this.lblPayout.Size = new System.Drawing.Size(160, 35);
            this.lblPayout.TabIndex = 12;
            this.lblPayout.TabStop = true;
            this.lblPayout.Text = "找零金额0元";
            this.lblPayout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblPayout_LinkClicked);
            // 
            // lblEatCash
            // 
            this.lblEatCash.AutoSize = true;
            this.lblEatCash.Location = new System.Drawing.Point(27, 593);
            this.lblEatCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEatCash.Name = "lblEatCash";
            this.lblEatCash.Size = new System.Drawing.Size(160, 35);
            this.lblEatCash.TabIndex = 13;
            this.lblEatCash.TabStop = true;
            this.lblEatCash.Text = "吞钞金额0元";
            this.lblEatCash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblEatCash_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DgvTradeList);
            this.groupBox1.Location = new System.Drawing.Point(218, 180);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(519, 454);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索结果";
            // 
            // DgvTradeList
            // 
            this.DgvTradeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvTradeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvTradeList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvTradeList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvTradeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvTradeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTradeList.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.DgvTradeList.Location = new System.Drawing.Point(8, 28);
            this.DgvTradeList.Margin = new System.Windows.Forms.Padding(4);
            this.DgvTradeList.MultiSelect = false;
            this.DgvTradeList.Name = "DgvTradeList";
            this.DgvTradeList.ReadOnly = true;
            this.DgvTradeList.RowTemplate.Height = 23;
            this.DgvTradeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTradeList.Size = new System.Drawing.Size(507, 422);
            this.DgvTradeList.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTradeCash
            // 
            this.lblTradeCash.AutoSize = true;
            this.lblTradeCash.Location = new System.Drawing.Point(27, 402);
            this.lblTradeCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTradeCash.Name = "lblTradeCash";
            this.lblTradeCash.Size = new System.Drawing.Size(160, 35);
            this.lblTradeCash.TabIndex = 15;
            this.lblTradeCash.TabStop = true;
            this.lblTradeCash.Text = "现金交易0元";
            this.lblTradeCash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblTradeCash_LinkClicked);
            // 
            // lblTradeWeiXin
            // 
            this.lblTradeWeiXin.AutoSize = true;
            this.lblTradeWeiXin.Location = new System.Drawing.Point(26, 450);
            this.lblTradeWeiXin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTradeWeiXin.Name = "lblTradeWeiXin";
            this.lblTradeWeiXin.Size = new System.Drawing.Size(160, 35);
            this.lblTradeWeiXin.TabIndex = 16;
            this.lblTradeWeiXin.TabStop = true;
            this.lblTradeWeiXin.Text = "微信交易0元";
            this.lblTradeWeiXin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblTradeWeiXin_LinkClicked);
            // 
            // lblTradeZhifubao
            // 
            this.lblTradeZhifubao.AutoSize = true;
            this.lblTradeZhifubao.Location = new System.Drawing.Point(24, 498);
            this.lblTradeZhifubao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTradeZhifubao.Name = "lblTradeZhifubao";
            this.lblTradeZhifubao.Size = new System.Drawing.Size(186, 35);
            this.lblTradeZhifubao.TabIndex = 17;
            this.lblTradeZhifubao.TabStop = true;
            this.lblTradeZhifubao.Text = "支付宝交易0元";
            this.lblTradeZhifubao.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblTradeZhifubao_LinkClicked);
            // 
            // FormTradeReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(750, 647);
            this.Controls.Add(this.lblTradeZhifubao);
            this.Controls.Add(this.lblTradeWeiXin);
            this.Controls.Add(this.lblTradeCash);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblEatCash);
            this.Controls.Add(this.lblPayout);
            this.Controls.Add(this.lblTrade);
            this.Controls.Add(this.lblLotteryPrize);
            this.Controls.Add(this.lblReceCash);
            this.Controls.Add(this.linLblPrize);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.rdoQingChao);
            this.Controls.Add(this.rdoAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLocalNumber);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormTradeReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "销售报表->销售报表";
            this.Load += new System.EventHandler(this.FormTradeReport_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTradeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLocalNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoQingChao;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.LinkLabel linLblPrize;
        private System.Windows.Forms.LinkLabel lblReceCash;
        private System.Windows.Forms.LinkLabel lblLotteryPrize;
        private System.Windows.Forms.LinkLabel lblTrade;
        private System.Windows.Forms.LinkLabel lblPayout;
        private System.Windows.Forms.LinkLabel lblEatCash;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvTradeList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel lblTradeCash;
        private System.Windows.Forms.LinkLabel lblTradeWeiXin;
        private System.Windows.Forms.LinkLabel lblTradeZhifubao;
    }
}