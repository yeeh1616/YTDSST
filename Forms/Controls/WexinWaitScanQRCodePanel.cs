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
    public partial class WexinWaitScanQRCodePanel : UserControl
    {
        public WexinWaitScanQRCodePanel()
        {
            InitializeComponent();
        }

        private void WexinWaitScanQRCodePanel_Load(object sender, EventArgs e)
        {

        }

        internal void SetMoney(int p)
        {
            lblMoney.Text = p + "";
        }


        public void SetTimerCountDown(int seconds)
        {
            lblSeconds.Text = seconds + "";
        }

        public void SetQRCodeImage(Image image) {
            picBoxQRCode.BackgroundImageLayout = ImageLayout.Stretch;
            picBoxQRCode.Image = image;
            Application.DoEvents();
        }
    }
}
