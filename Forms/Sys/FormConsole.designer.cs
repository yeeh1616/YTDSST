namespace YTDSSTGenII.Forms.Sys
{
    partial class FormConsole
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
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.timerReadLogs = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.BackColor = System.Drawing.Color.Black;
            this.textBoxLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLogs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxLogs.ForeColor = System.Drawing.SystemColors.Window;
            this.textBoxLogs.Location = new System.Drawing.Point(0, 0);
            this.textBoxLogs.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(1623, 868);
            this.textBoxLogs.TabIndex = 0;
            this.textBoxLogs.Text = "启动控制台...";
            // 
            // FormConsole
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1623, 868);
            this.Controls.Add(this.textBoxLogs);
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Name = "FormConsole";
            this.Text = "控制台";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConsole_FormClosed);
            this.Load += new System.EventHandler(this.FormConsole_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Timer timerReadLogs;
    }
}