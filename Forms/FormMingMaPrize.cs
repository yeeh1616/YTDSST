using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using YTDSSTGenII.Service;

namespace YTDSSTGenII.Forms
{
    public partial class FormMingMaPrize : Form
    {
        public FormMingMaPrize()
        {
            InitializeComponent();
        }

        private void FormMingMaPrize_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            Process.Start(Application.StartupPath + @"\ScreenKey.exe");
            this.txtSN.Focus();
            //GlobalParmeters.IsOpenScreenKey = true;
        }

        private void btnPrize_Click(object sender, EventArgs e)
        {

        }
    }
}
