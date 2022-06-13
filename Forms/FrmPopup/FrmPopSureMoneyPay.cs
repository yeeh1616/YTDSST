using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YTDSSTGenII.Forms.model;
using YTDSSTGenII.Service.Context;
using YTDSSTGenII.Service.Wexin;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    public partial class FrmPopSureMoneyPay : Form
    {
        int countdown_s = 20;
        private bool issure = false;

        public FrmPopSureMoneyPay()
        {
            InitializeComponent();
        }

        private void FrmPopPrintSucces_Load(object sender, EventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一


            //倒计时
            ThreadPool.QueueUserWorkItem(new WaitCallback(countdown));
        }

        private void countdown(object state)
        {
            while (countdown_s > 0)
            {
                countdown_s--;
                this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                {
                    this.lbTiming.Text = countdown_s.ToString();
                }));
                Thread.Sleep(1000);
            }

            this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
            {
                if (this.issure)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.btnSure.Enabled = false;
            this.btnCancel.Enabled = false;
            issure = true;
            countdown_s = 0;            
        }

        private void FrmPopPrintSucces_FormClosed(object sender, FormClosedEventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnSure.Enabled = false;
            this.btnCancel.Enabled = false;
            issure = false;
            countdown_s = 0;            
        }
    }
}
