namespace YTDSSTGenII.Forms
{
    partial class FormCashDeposit
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
            this.lblTips = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEmptyCoinBox = new System.Windows.Forms.Button();
            this.btnCleanCashBox = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picSoftKey = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtCoinCount = new System.Windows.Forms.TextBox();
            this.txtCashCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSoftKey)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(4, 13);
            this.lblTips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(35, 38);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "  ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEmptyCoinBox);
            this.groupBox1.Controls.Add(this.btnCleanCashBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(268, 386);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "清空退币箱";
            // 
            // btnEmptyCoinBox
            // 
            this.btnEmptyCoinBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEmptyCoinBox.Location = new System.Drawing.Point(8, 207);
            this.btnEmptyCoinBox.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmptyCoinBox.Name = "btnEmptyCoinBox";
            this.btnEmptyCoinBox.Size = new System.Drawing.Size(245, 61);
            this.btnEmptyCoinBox.TabIndex = 1;
            this.btnEmptyCoinBox.Text = "清空硬币退币箱";
            this.btnEmptyCoinBox.UseVisualStyleBackColor = true;
            this.btnEmptyCoinBox.Click += new System.EventHandler(this.btnEmptyCoinBox_Click);
            // 
            // btnCleanCashBox
            // 
            this.btnCleanCashBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCleanCashBox.Location = new System.Drawing.Point(8, 101);
            this.btnCleanCashBox.Margin = new System.Windows.Forms.Padding(4);
            this.btnCleanCashBox.Name = "btnCleanCashBox";
            this.btnCleanCashBox.Size = new System.Drawing.Size(245, 61);
            this.btnCleanCashBox.TabIndex = 0;
            this.btnCleanCashBox.Text = "清空纸币退币箱";
            this.btnCleanCashBox.UseVisualStyleBackColor = true;
            this.btnCleanCashBox.Click += new System.EventHandler(this.btnCleanCashBox_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picSoftKey);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtCoinCount);
            this.groupBox2.Controls.Add(this.txtCashCount);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(296, 60);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(423, 386);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "现金预存";
            // 
            // picSoftKey
            // 
            this.picSoftKey.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.key;
            this.picSoftKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picSoftKey.Location = new System.Drawing.Point(37, 231);
            this.picSoftKey.Margin = new System.Windows.Forms.Padding(4);
            this.picSoftKey.Name = "picSoftKey";
            this.picSoftKey.Size = new System.Drawing.Size(136, 116);
            this.picSoftKey.TabIndex = 6;
            this.picSoftKey.TabStop = false;
            this.picSoftKey.Click += new System.EventHandler(this.picSoftKey_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(181, 259);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(217, 61);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "确定添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtCoinCount
            // 
            this.txtCoinCount.Location = new System.Drawing.Point(195, 166);
            this.txtCoinCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtCoinCount.Name = "txtCoinCount";
            this.txtCoinCount.Size = new System.Drawing.Size(132, 46);
            this.txtCoinCount.TabIndex = 4;
            this.txtCoinCount.Text = "0";
            this.txtCoinCount.TextChanged += new System.EventHandler(this.txtCoinCount_TextChanged);
            // 
            // txtCashCount
            // 
            this.txtCashCount.Location = new System.Drawing.Point(195, 99);
            this.txtCashCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtCashCount.Name = "txtCashCount";
            this.txtCashCount.Size = new System.Drawing.Size(132, 46);
            this.txtCashCount.TabIndex = 3;
            this.txtCashCount.Text = "0";
            this.txtCashCount.TextChanged += new System.EventHandler(this.txtCashCount_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 172);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 38);
            this.label4.TabIndex = 2;
            this.label4.Text = "硬币个数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 38);
            this.label3.TabIndex = 1;
            this.label3.Text = "纸币个数：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "注：本次加多少就输入多少";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(342, 13);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(62, 38);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "     ";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(204, 472);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 38);
            this.labelStatus.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTips);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(13, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 543);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 38);
            this.label1.TabIndex = 4;
            this.label1.Text = "    ";
            // 
            // FormCashDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(750, 576);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelStatus);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCashDeposit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "找币器管理->现金预存";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCashDeposit_FormClosing);
            this.Load += new System.EventHandler(this.FormCashDeposit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSoftKey)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCleanCashBox;
        private System.Windows.Forms.Button btnEmptyCoinBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCashCount;
        private System.Windows.Forms.TextBox txtCoinCount;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.PictureBox picSoftKey;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}