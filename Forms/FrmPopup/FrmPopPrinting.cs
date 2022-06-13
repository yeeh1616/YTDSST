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
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    public partial class FrmPopPrinting : Form
    {

        MotorOperate mo;
        private IniFile ini;
        private Boolean isFlashLights;
        Dictionary<String, int> errorMap;
        public FrmPopPrinting(MotorOperate mop, IniFile inifile,Boolean isflights, Dictionary<String, int> emap)
        {
            InitializeComponent();
            this.mo = mop;
            this.ini = inifile;
            this.isFlashLights = isflights;
            this.errorMap = emap; 
        }

        private void FrmPopPrinting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
        }

        private void doCutPaper(object state)
        {
            CommonUtils.WritePayLogInfo("线程开始执行");
            DbOperate operate = null;
            try
            {
                operate = new DbOperate();
                this.mo = this.mo == null ? new MotorOperate() : this.mo;
                foreach (OrderDetails item in UserContext.getInstance().UserOrder.Tickets)
                {
                    if (errorMap.ContainsKey(item.getHeadNo_String()))
                    {
                        item.TicketOutState = 3;
                        errorMap[item.getHeadNo_String()] += 1;
                        item.TicketOutState = 3;
                        UserContext.getInstance().UserOrder.OutErrorNum++;
                        UserContext.getInstance().UserOrder.OutErrorMoney += item.UnitPrice;
                        updateDBTicketError(item);
                        //TODO 微信支付和现金支付交易记录会有所不同         
                        //UserContext.getInstance().UserOrder.OutTradeNo;
                        operate.InsertTradeFailInfo(item.getHeadNo_Int().ToString(), item.UnitPrice.ToString(), item.LotteryName);
                    }
                    else
                    {
                        int motorLength = 0, lotteryPrice = 0, motorLotteryRemain = 0;
                        string lotteryName = "";
                        operate.GetMotorInfoByMotorNumber(item.getHeadNo_Int(), ref motorLength, ref lotteryPrice, ref motorLotteryRemain, ref lotteryName);
                        if (this.CutPaper(item.getHeadNo_Int()))
                        {
                            //出票成功
                            operate.Decrease(item.getHeadNo_Int(), motorLotteryRemain - 1);
                            motorLotteryRemain--;
                            //TODO 微信支付和现金支付交易记录会有所不同
                            operate.InsertTrade(item.getHeadNo_Int().ToString(), lotteryPrice.ToString(), lotteryName);

                            this.isFlashLights = true;

                            if (GlobalParmeters.GotMoney > 0)//走现金
                            {
                                GlobalParmeters.GotMoney -= lotteryPrice;
                                UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                                CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                            }

                            item.TicketOutState = 2;
                            UserContext.getInstance().UserOrder.OutSuccNum++;
                            UserContext.getInstance().UserOrder.OutSuccMoney += item.UnitPrice;

                            MachineContext.getInstance().ServerMotorArray[item.getHeadNo_Int() - 1].TraceLotteryNum--;

                            updateDBTicketSucc(item);
                        }
                        else
                        {
                            item.TicketOutState = 3;
                            errorMap.Add(item.getHeadNo_String(), 1);
                            item.TicketOutState = 3;
                            UserContext.getInstance().UserOrder.OutErrorNum++;
                            UserContext.getInstance().UserOrder.OutErrorMoney += item.UnitPrice;
                            updateDBTicketError(item);

                            //TODO 失败                                
                            operate.InsertTradeFailInfo(item.getHeadNo_Int().ToString(), item.UnitPrice.ToString(), item.LotteryName);
                        }
                    }
                }

                //关闭连接
                operate.CloseConn();
                this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                {
                    this.Close();
                    CommonUtils.WritePayLogInfo("关闭当前窗口");
                }));
            }
            catch {
                CommonUtils.WritePayLogInfo("出异常了");
            }
                        
        }

        private void FrmPopPrinting_Load(object sender, EventArgs e)
        {
            Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一 
                                                                                      //请求支付结果信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(doCutPaper));
        }

        /**
         * 出票
         * 只要一出错，就禁用机头，每次一张票
         * @param int p 机头 1-8
         */
        private bool CutPaper(int p)
        {
            try
            {
                //出票
                byte[] buffer = this.mo.CutCardOperate(p);

                CommonUtils.WritePayLogInfo(System.Text.Encoding.UTF8.GetString(buffer));

                //机头返回数据出错
                if ((buffer == null) || (buffer.Length < 10))
                {
                    CommonUtils.WritePayLogInfo("机头返回数据出错");
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    return false;
                }

                //机头出票不成功
                if (buffer[2] != 0x55)
                {
                    CommonUtils.WritePayLogInfo("机头出票不成功"+ buffer[9].ToString());
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    //机头的各种状态
                    if (buffer[9] == 5)
                    {
                        SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=1 WHERE MotorId=" + p);
                        this.ini.WriteIniValue("配置", "机头" + p + "状态", "4");
                    }
                    if (buffer[9] == 3)
                    {
                        this.ini.WriteIniValue("配置", "机头" + p + "状态", "2");
                    }
                    else
                    {
                        this.ini.WriteIniValue("配置", "机头" + p + "状态", "3");
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }

            //出票成功
            GlobalParmeters.MotorStatus[p - 1] = 0;
            this.ini.WriteIniValue("配置", "机头" + p + "状态", "0");
            SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=0 WHERE MotorId=" + p);

            return true;
        }

        /// <summary>
        /// 成功一张票时修改
        /// </summary>
        /// <param name="item"></param>
        private void updateDBTicketSucc(OrderDetails t)
        {
            List<String> sqllist = new List<string>();
            String addticketsql = String.Format(@"Update p_terminal_order_details set ticket_out_time='{0}',ticket_out_state={1} where ticket_id='{2}'",
                   System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), 1, t.TicketId
                   );
            sqllist.Add(addticketsql);

            //插入订单sql
            String addordersql = String.Format(@"Update p_terminal_orders set out_success_num = out_success_num+1,
                out_success_money = out_success_money+{0} where out_trade_no='{1}'",
                t.UnitPrice, t.OutTradeNo );

            sqllist.Add(addordersql);

            SQLiteHelper.ExecuteSqlTran(sqllist);
        }

        /// <summary>
        /// 错误一张票时修改
        /// </summary>
        /// <param name="item"></param>
        private void updateDBTicketError(OrderDetails t)
        {
            List<String> sqllist = new List<string>();
            String addticketsql = String.Format(@"Update p_terminal_order_details set ticket_out_time='{0}',ticket_out_state={1} where ticket_id='{2}'",
                   System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), 2, t.TicketId
                   );
            sqllist.Add(addticketsql);

            //插入订单sql
            String addordersql = String.Format(@"Update p_terminal_orders set out_error_num = out_error_num+1,
                out_error_money = out_error_money+{0} where out_trade_no='{1}'",
                t.UnitPrice, t.OutTradeNo);
            sqllist.Add(addordersql);

            SQLiteHelper.ExecuteSqlTran(sqllist);
        }

        //public void formClose() {
        //    this.Close();
        //}
    }
}
