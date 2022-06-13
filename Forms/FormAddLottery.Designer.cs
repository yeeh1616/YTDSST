namespace YTDSSTGenII.Forms
{
    partial class FormAddLottery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddLottery));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGameCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLotteryType = new System.Windows.Forms.ComboBox();
            this.txbLotteryRemain = new System.Windows.Forms.TextBox();
            this.lblMotorNumber = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.picSoftKeyBoard = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSoftKeyBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "机头号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "游戏编码：";
            // 
            // txtGameCode
            // 
            this.txtGameCode.Location = new System.Drawing.Point(148, 81);
            this.txtGameCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGameCode.Name = "txtGameCode";
            this.txtGameCode.Size = new System.Drawing.Size(253, 30);
            this.txtGameCode.TabIndex = 2;
            this.txtGameCode.TextChanged += new System.EventHandler(this.txtGameCode_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "彩种名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 195);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "彩票数量：";
            // 
            // cmbLotteryType
            // 
            this.cmbLotteryType.FormattingEnabled = true;
            this.cmbLotteryType.Location = new System.Drawing.Point(148, 138);
            this.cmbLotteryType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLotteryType.Name = "cmbLotteryType";
            this.cmbLotteryType.Size = new System.Drawing.Size(253, 31);
            this.cmbLotteryType.TabIndex = 5;
            this.cmbLotteryType.SelectionChangeCommitted += new System.EventHandler(this.cmbLotteryType_SelectionChangeCommitted);
            // 
            // txbLotteryRemain
            // 
            this.txbLotteryRemain.Location = new System.Drawing.Point(148, 191);
            this.txbLotteryRemain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbLotteryRemain.Name = "txbLotteryRemain";
            this.txbLotteryRemain.Size = new System.Drawing.Size(253, 30);
            this.txbLotteryRemain.TabIndex = 6;
            // 
            // lblMotorNumber
            // 
            this.lblMotorNumber.AutoSize = true;
            this.lblMotorNumber.Location = new System.Drawing.Point(148, 40);
            this.lblMotorNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMotorNumber.Name = "lblMotorNumber";
            this.lblMotorNumber.Size = new System.Drawing.Size(20, 23);
            this.lblMotorNumber.TabIndex = 7;
            this.lblMotorNumber.Text = "1";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(96, 295);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 48);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(252, 295);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 48);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picSoftKeyBoard
            // 
            this.picSoftKeyBoard.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.key;
            this.picSoftKeyBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picSoftKeyBoard.Location = new System.Drawing.Point(452, 61);
            this.picSoftKeyBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picSoftKeyBoard.Name = "picSoftKeyBoard";
            this.picSoftKeyBoard.Size = new System.Drawing.Size(156, 191);
            this.picSoftKeyBoard.TabIndex = 10;
            this.picSoftKeyBoard.TabStop = false;
            this.picSoftKeyBoard.Click += new System.EventHandler(this.picSoftKeyBoard_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(148, 249);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 23);
            this.lblMessage.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(427, 308);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "注：游戏编码仅用于搜索彩种名称";
            // 
            // FormAddLottery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(733, 390);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.picSoftKeyBoard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblMotorNumber);
            this.Controls.Add(this.txbLotteryRemain);
            this.Controls.Add(this.cmbLotteryType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGameCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddLottery";
            this.ShowInTaskbar = false;
            this.Text = "更新机头彩票信息";
            this.Load += new System.EventHandler(this.FormAddLottery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSoftKeyBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGameCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLotteryType;
        private System.Windows.Forms.TextBox txbLotteryRemain;
        private System.Windows.Forms.Label lblMotorNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picSoftKeyBoard;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label5;
    }
}