using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    public partial class FrmPopCoinReturn : Form
    {
        int countdown_s = 60;
        bool is_ok = false;
        public FrmPopCoinReturn()
        {
            InitializeComponent();
        }

        private void FrmPopCoinReturn_Load(object sender, EventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一
            this.lbTiming.Text = countdown_s.ToString();
            //倒计时
            ThreadPool.QueueUserWorkItem(new WaitCallback(countdownCoinReturn));
        }

        private void countdownCoinReturn(object state)
        {
            //this.DialogResult = DialogResult.Cancel;
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
                this.Close();
                //this.Dispose();
                this.DialogResult = is_ok?DialogResult.OK: DialogResult.Cancel;
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            is_ok = true;
            this.btnClose.Enabled = false;
            this.btn_cancel.Enabled = false;
            this.countdown_s = 0;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.btn_cancel.Enabled = false;
            this.countdown_s = 0;
        }

        private void FrmPopCoinReturn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
        }
    }
}
