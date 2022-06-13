using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YTDSSTGenII.Forms.Forms.Controls
{
    public partial class WexinCutPaperFailedPanel : UserControl
    {
        public WexinCutPaperFailedPanel()
        {
            InitializeComponent();
        }

        private void WexinCutPaperFailedPanel_Load(object sender, EventArgs e)
        {

        }

        public void SetOrderId(String orderId) 
        {
            lblOrderId.Text = orderId;
        }

        public void SetServicePhoneNumber(String phoneNumber)
        {
            lblServicePhoneNumber.Text = phoneNumber;
        }

        public void SetTimerCountDown(int seconds) 
        {
            lblSeconds.Text = seconds + "";    
        }
    }
}
