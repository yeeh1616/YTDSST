namespace YTDSSTGenII.Forms.Sg
{
    partial class FormSGSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInterval = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClaimAmount = new System.Windows.Forms.Button();
            this.lbl_sgGetclaimAmount = new System.Windows.Forms.Label();
            this.txtclaimAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInterval);
            this.groupBox1.Controls.Add(this.txtInterval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "心跳设置";
            // 
            // btnInterval
            // 
            this.btnInterval.Location = new System.Drawing.Point(459, 61);
            this.btnInterval.Name = "btnInterval";
            this.btnInterval.Size = new System.Drawing.Size(161, 45);
            this.btnInterval.TabIndex = 2;
            this.btnInterval.Text = "执行";
            this.btnInterval.UseVisualStyleBackColor = true;
            this.btnInterval.Click += new System.EventHandler(this.btnInterval_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(164, 70);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(260, 30);
            this.txtInterval.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "超时时间：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClaimAmount);
            this.groupBox2.Controls.Add(this.lbl_sgGetclaimAmount);
            this.groupBox2.Controls.Add(this.txtclaimAmount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(11, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(709, 195);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "兑奖确认金额设置";
            // 
            // btnClaimAmount
            // 
            this.btnClaimAmount.Location = new System.Drawing.Point(468, 57);
            this.btnClaimAmount.Name = "btnClaimAmount";
            this.btnClaimAmount.Size = new System.Drawing.Size(161, 45);
            this.btnClaimAmount.TabIndex = 3;
            this.btnClaimAmount.Text = "设置";
            this.btnClaimAmount.UseVisualStyleBackColor = true;
            this.btnClaimAmount.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_sgGetclaimAmount
            // 
            this.lbl_sgGetclaimAmount.AutoSize = true;
            this.lbl_sgGetclaimAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_sgGetclaimAmount.Location = new System.Drawing.Point(33, 141);
            this.lbl_sgGetclaimAmount.Name = "lbl_sgGetclaimAmount";
            this.lbl_sgGetclaimAmount.Size = new System.Drawing.Size(219, 20);
            this.lbl_sgGetclaimAmount.TabIndex = 4;
            this.lbl_sgGetclaimAmount.Text = "当前兑奖确认额度500元";
            // 
            // txtclaimAmount
            // 
            this.txtclaimAmount.Location = new System.Drawing.Point(173, 66);
            this.txtclaimAmount.Name = "txtclaimAmount";
            this.txtclaimAmount.Size = new System.Drawing.Size(260, 30);
            this.txtclaimAmount.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "兑奖确认金额：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(114, 477);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(169, 20);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "兑奖绑定设置提示";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::YTDSSTGenII.Forms.Properties.Resources.key;
            this.pictureBox1.Location = new System.Drawing.Point(506, 445);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 129);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 609);
            this.panel1.TabIndex = 4;
            // 
            // FormSGSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(750, 633);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormSGSet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "兑奖绑定设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormSGSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnInterval;
        private System.Windows.Forms.TextBox txtclaimAmount;
        private System.Windows.Forms.Label lbl_sgGetclaimAmount;
        private System.Windows.Forms.Button btnClaimAmount;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}