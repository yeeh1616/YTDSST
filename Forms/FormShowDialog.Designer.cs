namespace YTDSSTGenII.Forms
{
    partial class FormShowDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblYes = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.信息提示;
            this.panel1.Controls.Add(this.lblNo);
            this.panel1.Controls.Add(this.lblYes);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 292);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblNo
            // 
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Location = new System.Drawing.Point(389, 196);
            this.lblNo.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(121, 54);
            this.lblNo.TabIndex = 2;
            this.lblNo.Click += new System.EventHandler(this.lblNo_Click);
            // 
            // lblYes
            // 
            this.lblYes.BackColor = System.Drawing.Color.Transparent;
            this.lblYes.Location = new System.Drawing.Point(209, 196);
            this.lblYes.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblYes.Name = "lblYes";
            this.lblYes.Size = new System.Drawing.Size(116, 54);
            this.lblYes.TabIndex = 1;
            this.lblYes.Click += new System.EventHandler(this.lblYes_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(208, 116);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(156, 45);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "提示信息";
            // 
            // FormShowDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(585, 292);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormShowDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "请选择";
            this.Text = "请选择";
            this.Load += new System.EventHandler(this.FormShowDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblYes;
    }
}