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
    public partial class WexinGeneratingQRCodePanel : UserControl
    {
        public WexinGeneratingQRCodePanel()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void WexinPayPanel_Load(object sender, EventArgs e)
        {

        }

        public void SetMoney(int money) {
            this.lblMoney.Text = money + "";
        }

        public void SetQRCodeGenerating() {
            lblQRCodeMessage.Text = "二维码生成中...";
            lblMainMessage.Text = "二维码生成中, 请耐心等待...";
        }
    }
}
