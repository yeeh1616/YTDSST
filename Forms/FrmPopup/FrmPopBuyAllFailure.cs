using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YTDSSTGenII;
using YTDSSTGenII.Service.Context;

namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    public partial class FrmPopBuyAllFailure : Form
    {
        int countdown_s = 60;
        public FrmPopBuyAllFailure()
        {
            InitializeComponent();
        }

        private void FrmPopBuyAllFailure_Load(object sender, EventArgs e)
        {
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一

            this.lbFailNum.Text = String.Format("失败出票{0}张", UserContext.getInstance().UserOrder.OutErrorNum.ToString());
            this.lbTiming.Text = countdown_s.ToString();
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
                this.Close();
               // this.Dispose();
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.countdown_s = 0;
        }

        private void FrmPopBuyAllFailure_FormClosed(object sender, FormClosedEventArgs e)
        {
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
        }
    }
}
 