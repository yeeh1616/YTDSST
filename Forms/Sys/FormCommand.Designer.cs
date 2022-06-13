namespace YTDSSTGenII.Forms.Sys
{
    partial class FormCommand
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
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPowerControl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblAdvertise = new System.Windows.Forms.Label();
            this.lblDataUpload = new System.Windows.Forms.Label();
            this.lblPowerControl = new System.Windows.Forms.Label();
            this.btnAdvertise = new System.Windows.Forms.Button();
            this.btnDataUpload = new System.Windows.Forms.Button();
            this.timerCheckProcess = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMainExit = new System.Windows.Forms.Button();
            this.btnShowTaskbar = new System.Windows.Forms.Button();
            this.btnHideTaskbar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(63, 47);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(156, 54);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "重启机器";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(301, 47);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(156, 54);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭机器";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPowerControl
            // 
            this.btnPowerControl.Location = new System.Drawing.Point(55, 67);
            this.btnPowerControl.Name = "btnPowerControl";
            this.btnPowerControl.Size = new System.Drawing.Size(174, 54);
            this.btnPowerControl.TabIndex = 2;
            this.btnPowerControl.Text = "退出电控程序";
            this.btnPowerControl.UseVisualStyleBackColor = true;
            this.btnPowerControl.Click += new System.EventHandler(this.btnPowerControl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnHideTaskbar);
            this.groupBox1.Controls.Add(this.btnShowTaskbar);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnRestart);
            this.groupBox1.Location = new System.Drawing.Point(26, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 243);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作系统";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblAdvertise);
            this.groupBox2.Controls.Add(this.lblDataUpload);
            this.groupBox2.Controls.Add(this.lblPowerControl);
            this.groupBox2.Controls.Add(this.btnAdvertise);
            this.groupBox2.Controls.Add(this.btnDataUpload);
            this.groupBox2.Controls.Add(this.btnPowerControl);
            this.groupBox2.Location = new System.Drawing.Point(26, 492);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 359);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "后台程序";
            // 
            // lblAdvertise
            // 
            this.lblAdvertise.AutoSize = true;
            this.lblAdvertise.Location = new System.Drawing.Point(281, 274);
            this.lblAdvertise.Name = "lblAdvertise";
            this.lblAdvertise.Size = new System.Drawing.Size(130, 24);
            this.lblAdvertise.TabIndex = 7;
            this.lblAdvertise.Text = "程序已启动";
            // 
            // lblDataUpload
            // 
            this.lblDataUpload.AutoSize = true;
            this.lblDataUpload.Location = new System.Drawing.Point(279, 183);
            this.lblDataUpload.Name = "lblDataUpload";
            this.lblDataUpload.Size = new System.Drawing.Size(178, 24);
            this.lblDataUpload.TabIndex = 6;
            this.lblDataUpload.Text = "数据上传已启动";
            // 
            // lblPowerControl
            // 
            this.lblPowerControl.AutoSize = true;
            this.lblPowerControl.Location = new System.Drawing.Point(279, 82);
            this.lblPowerControl.Name = "lblPowerControl";
            this.lblPowerControl.Size = new System.Drawing.Size(130, 24);
            this.lblPowerControl.TabIndex = 5;
            this.lblPowerControl.Text = "程序已启动";
            // 
            // btnAdvertise
            // 
            this.btnAdvertise.Location = new System.Drawing.Point(55, 259);
            this.btnAdvertise.Name = "btnAdvertise";
            this.btnAdvertise.Size = new System.Drawing.Size(174, 54);
            this.btnAdvertise.TabIndex = 4;
            this.btnAdvertise.Text = "退出广告程序";
            this.btnAdvertise.UseVisualStyleBackColor = true;
            this.btnAdvertise.Click += new System.EventHandler(this.btnAdvertise_Click);
            // 
            // btnDataUpload
            // 
            this.btnDataUpload.Location = new System.Drawing.Point(55, 168);
            this.btnDataUpload.Name = "btnDataUpload";
            this.btnDataUpload.Size = new System.Drawing.Size(174, 54);
            this.btnDataUpload.TabIndex = 3;
            this.btnDataUpload.Text = "退出数据上传";
            this.btnDataUpload.UseVisualStyleBackColor = true;
            this.btnDataUpload.Click += new System.EventHandler(this.btnDataUpload_Click);
            // 
            // timerCheckProcess
            // 
            this.timerCheckProcess.Enabled = true;
            this.timerCheckProcess.Interval = 500;
            this.timerCheckProcess.Tick += new System.EventHandler(this.timerCheckProcess_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMainExit);
            this.groupBox3.Location = new System.Drawing.Point(26, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 137);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "购彩程序";
            // 
            // btnMainExit
            // 
            this.btnMainExit.Location = new System.Drawing.Point(63, 53);
            this.btnMainExit.Name = "btnMainExit";
            this.btnMainExit.Size = new System.Drawing.Size(272, 54);
            this.btnMainExit.TabIndex = 2;
            this.btnMainExit.Text = "退出购彩程序";
            this.btnMainExit.UseVisualStyleBackColor = true;
            this.btnMainExit.Click += new System.EventHandler(this.btnMainExit_Click);
            // 
            // btnShowTaskbar
            // 
            this.btnShowTaskbar.Location = new System.Drawing.Point(64, 146);
            this.btnShowTaskbar.Name = "btnShowTaskbar";
            this.btnShowTaskbar.Size = new System.Drawing.Size(155, 54);
            this.btnShowTaskbar.TabIndex = 2;
            this.btnShowTaskbar.Text = "显示任务栏";
            this.btnShowTaskbar.UseVisualStyleBackColor = true;
            this.btnShowTaskbar.Click += new System.EventHandler(this.btnTaskbar_Click);
            // 
            // btnHideTaskbar
            // 
            this.btnHideTaskbar.Location = new System.Drawing.Point(301, 146);
            this.btnHideTaskbar.Name = "btnHideTaskbar";
            this.btnHideTaskbar.Size = new System.Drawing.Size(155, 54);
            this.btnHideTaskbar.TabIndex = 3;
            this.btnHideTaskbar.Text = "隐藏任务栏";
            this.btnHideTaskbar.UseVisualStyleBackColor = true;
            this.btnHideTaskbar.Click += new System.EventHandler(this.btnHideTaskbar_Click);
            // 
            // FormCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(761, 890);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCommand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统命令";
            this.Load += new System.EventHandler(this.FormCommand_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPowerControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAdvertise;
        private System.Windows.Forms.Button btnDataUpload;
        private System.Windows.Forms.Label lblAdvertise;
        private System.Windows.Forms.Label lblDataUpload;
        private System.Windows.Forms.Label lblPowerControl;
        private System.Windows.Forms.Timer timerCheckProcess;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMainExit;
        private System.Windows.Forms.Button btnShowTaskbar;
        private System.Windows.Forms.Button btnHideTaskbar;
    }
}