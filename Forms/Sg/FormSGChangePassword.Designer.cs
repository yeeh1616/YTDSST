namespace YTDSSTGenII.Forms.Sg
{
    partial class FormSGChangePassword
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.btn_sgChangePassword = new System.Windows.Forms.Button();
            this.btn_changesave = new System.Windows.Forms.Button();
            this.cbnewpwd2 = new System.Windows.Forms.CheckBox();
            this.cbnewpwd = new System.Windows.Forms.CheckBox();
            this.cboldPwd = new System.Windows.Forms.CheckBox();
            this.txtnewpwd2 = new System.Windows.Forms.TextBox();
            this.txtnewpwd = new System.Windows.Forms.TextBox();
            this.txtoldPwd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.linkLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.btn_sgChangePassword);
            this.groupBox1.Controls.Add(this.btn_changesave);
            this.groupBox1.Controls.Add(this.cbnewpwd2);
            this.groupBox1.Controls.Add(this.cbnewpwd);
            this.groupBox1.Controls.Add(this.cboldPwd);
            this.groupBox1.Controls.Add(this.txtnewpwd2);
            this.groupBox1.Controls.Add(this.txtnewpwd);
            this.groupBox1.Controls.Add(this.txtoldPwd);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Location = new System.Drawing.Point(63, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 499);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "修改密码";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(235, 459);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 20);
            this.lblMessage.TabIndex = 15;
            // 
            // btn_sgChangePassword
            // 
            this.btn_sgChangePassword.Location = new System.Drawing.Point(314, 368);
            this.btn_sgChangePassword.Name = "btn_sgChangePassword";
            this.btn_sgChangePassword.Size = new System.Drawing.Size(146, 66);
            this.btn_sgChangePassword.TabIndex = 14;
            this.btn_sgChangePassword.Text = "确认修改";
            this.btn_sgChangePassword.UseVisualStyleBackColor = true;
            this.btn_sgChangePassword.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_changesave
            // 
            this.btn_changesave.Location = new System.Drawing.Point(114, 368);
            this.btn_changesave.Name = "btn_changesave";
            this.btn_changesave.Size = new System.Drawing.Size(146, 66);
            this.btn_changesave.TabIndex = 13;
            this.btn_changesave.Text = "保存";
            this.btn_changesave.UseVisualStyleBackColor = true;
            this.btn_changesave.Click += new System.EventHandler(this.btn_changesave_Click);
            // 
            // cbnewpwd2
            // 
            this.cbnewpwd2.AutoSize = true;
            this.cbnewpwd2.Location = new System.Drawing.Point(349, 288);
            this.cbnewpwd2.Name = "cbnewpwd2";
            this.cbnewpwd2.Size = new System.Drawing.Size(111, 24);
            this.cbnewpwd2.TabIndex = 12;
            this.cbnewpwd2.Text = "显示密码";
            this.cbnewpwd2.UseVisualStyleBackColor = true;
            this.cbnewpwd2.CheckedChanged += new System.EventHandler(this.cbnewpwd2_CheckedChanged);
            // 
            // cbnewpwd
            // 
            this.cbnewpwd.AutoSize = true;
            this.cbnewpwd.Location = new System.Drawing.Point(349, 219);
            this.cbnewpwd.Name = "cbnewpwd";
            this.cbnewpwd.Size = new System.Drawing.Size(111, 24);
            this.cbnewpwd.TabIndex = 11;
            this.cbnewpwd.Text = "显示密码";
            this.cbnewpwd.UseVisualStyleBackColor = true;
            this.cbnewpwd.CheckedChanged += new System.EventHandler(this.cbnewpwd_CheckedChanged);
            // 
            // cboldPwd
            // 
            this.cboldPwd.AutoSize = true;
            this.cboldPwd.Location = new System.Drawing.Point(349, 150);
            this.cboldPwd.Name = "cboldPwd";
            this.cboldPwd.Size = new System.Drawing.Size(111, 24);
            this.cboldPwd.TabIndex = 10;
            this.cboldPwd.Text = "显示密码";
            this.cboldPwd.UseVisualStyleBackColor = true;
            this.cboldPwd.CheckedChanged += new System.EventHandler(this.cboldPwd_CheckedChanged);
            // 
            // txtnewpwd2
            // 
            this.txtnewpwd2.Location = new System.Drawing.Point(170, 286);
            this.txtnewpwd2.Name = "txtnewpwd2";
            this.txtnewpwd2.Size = new System.Drawing.Size(164, 30);
            this.txtnewpwd2.TabIndex = 8;
            // 
            // txtnewpwd
            // 
            this.txtnewpwd.Location = new System.Drawing.Point(170, 213);
            this.txtnewpwd.Name = "txtnewpwd";
            this.txtnewpwd.Size = new System.Drawing.Size(164, 30);
            this.txtnewpwd.TabIndex = 7;
            // 
            // txtoldPwd
            // 
            this.txtoldPwd.Location = new System.Drawing.Point(170, 144);
            this.txtoldPwd.Name = "txtoldPwd";
            this.txtoldPwd.Size = new System.Drawing.Size(164, 30);
            this.txtoldPwd.TabIndex = 6;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(170, 69);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(164, 30);
            this.txtUserName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "再次输入新密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "新密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "旧密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Image = global::YTDSSTGenII.Forms.Properties.Resources.key;
            this.linkLabel1.Location = new System.Drawing.Point(507, 340);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 109);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = false;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // FormSGChangePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(750, 548);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormSGChangePassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSGChangePassword_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.linkLabel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnewpwd2;
        private System.Windows.Forms.TextBox txtnewpwd;
        private System.Windows.Forms.TextBox txtoldPwd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.CheckBox cboldPwd;
        private System.Windows.Forms.CheckBox cbnewpwd2;
        private System.Windows.Forms.CheckBox cbnewpwd;
        private System.Windows.Forms.Button btn_changesave;
        private System.Windows.Forms.Button btn_sgChangePassword;
        private System.Windows.Forms.Label lblMessage;
    }
}