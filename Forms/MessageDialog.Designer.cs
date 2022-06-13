namespace YTDSSTGenII.Forms
{
    partial class MessageDialog 
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
            this.lblYes = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblYes
            // 
            this.lblYes.BackColor = System.Drawing.Color.Transparent;
            this.lblYes.Location = new System.Drawing.Point(198, 191);
            this.lblYes.Name = "lblYes";
            this.lblYes.Size = new System.Drawing.Size(135, 56);
            this.lblYes.TabIndex = 0;
            this.lblYes.Click += new System.EventHandler(this.lblYes_Click);
            // 
            // lblNo
            // 
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Location = new System.Drawing.Point(382, 191);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(135, 56);
            this.lblNo.TabIndex = 1;
            this.lblNo.Click += new System.EventHandler(this.lblNo_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Location = new System.Drawing.Point(198, 110);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(137, 39);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "信息提示";
            // 
            // MessageDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.信息提示;
            this.ClientSize = new System.Drawing.Size(585, 292);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.lblYes);
            this.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblYes;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblMessage;
    }
}