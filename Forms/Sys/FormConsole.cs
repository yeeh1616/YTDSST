using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms.Sys
{
    public partial class FormConsole : Form
    {
        public FormConsole()
        {
            InitializeComponent();
        }

        private void FormConsole_Load(object sender, EventArgs e)
        {
            String logs = RuntimeLogUtils.ReadLog(null);
            textBoxLogs.Focus();

            textBoxLogs.Text = "";
            textBoxLogs.AppendText(logs);
            textBoxLogs.SelectionStart = textBoxLogs.Text.Length;

            textBoxLogs.ScrollToCaret();
        }

        private void FormConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
