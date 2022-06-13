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
    public partial class FrmPopPaySuccessAllFailure : Form
    {
        int countdown_s = 60;
        private WeXinSocketClient.RemoteCommandDelegate rdelegate;
        private bool is_can_countdown = false;
        public FrmPopPaySuccessAllFailure()
        {
            InitializeComponent();
        }

        private void FrmPopPaySuccessAllFailure_Load(object sender, EventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一
            this.btnClose.Enabled = false;
            this.lbOrderId.Text = UserContext.getInstance().UserOrder.OutTradeNo;

            this.lbFailNum.Text = String.Format("失败出票{0}张", UserContext.getInstance().UserOrder.OutErrorNum.ToString());
            this.lblServiceNumber.Text = GlobalParmeters.ServicePhoneNumber;

            rdelegate = new WeXinSocketClient.RemoteCommandDelegate(OnRemoteCommand);
            WeXinSocketClient.getInstance().RegistDelegate(rdelegate);

            //提交出票结果的线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(uploadTicketResult));
            //倒计时
            ThreadPool.QueueUserWorkItem(new WaitCallback(countdown));
        }

        /// <summary>
        /// 倒计时
        /// </summary>
        /// <param name="state"></param>
        private void countdown(object state)
        {
            while (countdown_s > 0)
            {
                if (is_can_countdown)
                {
                    countdown_s--;
                    this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                    {
                        this.lbTiming.Text = countdown_s.ToString();
                    }));
                    Thread.Sleep(1000);
                }
            }

            this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
            {
                this.Close();
                //this.Dispose();
            }));
        }

        private void OnRemoteCommand(RemoteData remoteData)
        {
            if (remoteData.ProtocolId.Equals(C008ContentReq.PROTOCOL_TYPE))
            {
                //出票结果反馈已收到
                C008ContentAck ack = (C008ContentAck)remoteData.Content;
                is_can_countdown = true;
            }
        }

        /// <summary>
        /// 提交反馈数据
        /// </summary>
        /// <param name="state"></param>
        private void uploadTicketResult(object state)
        {
            RemoteData remoteData = new RemoteData();
            //远程命令参数设置
            //1.机器编号
            remoteData.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
            //2.协议类型，即命令类型
            remoteData.ProtocolId = C008ContentReq.PROTOCOL_TYPE;
            //3.请求的参数
            C008ContentReq req = new C008ContentReq();
            req.OrderId = UserContext.getInstance().UserOrder.OutTradeNo;
            req.OrderCutpaperStatus = UserContext.getInstance().UserOrder.getFeedbackTicketOutState();
            List<ticket008> lts = new List<ticket008>();
            foreach (OrderDetails item in UserContext.getInstance().UserOrder.Tickets)
            {
                ticket008 t = new ticket008();
                t.HeadNo = item.getHeadNo_String();
                t.LotteryTypeId = item.LotteryTypeId;
                t.TicketCutpaperStatus = item.getFeedBackStatus();
                t.TicketId = item.TicketId;
                lts.Add(t);
            }

            req.tickets = lts;
            remoteData.Content = req;

            try
            {
                WeXinSocketClient.getInstance().WriteSocketData(remoteData);
            }
            catch (Exception exp) {
                CommonUtils.WriteExceptionInfo(exp);
            }

            this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
            {
                this.btnClose.Enabled = true;//必须发送结果以后才能关闭窗口
            }));

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            countdown_s = 0;
        }

        private void FrmPopPaySuccessAllFailure_FormClosed(object sender, FormClosedEventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
            WeXinSocketClient.getInstance().UnregistDeletegate(rdelegate);
        }
    }
}
