using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YTDSSTGenII.Forms.Controls
{
    public partial class WexinCutPaperSuccessPanel : UserControl
    {
        public WexinCutPaperSuccessPanel()
        {
            InitializeComponent();
        }

        private void WexinCutPaperSuccessPanel_Load(object sender, EventArgs e)
        {

        }

        public void SetTimerCountDown(int seconds)
        {
            this.lblSeconds.Text = seconds + "";
        }
    }
}
