using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Threading.Tasks;

using System.Threading;

using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Utils.Network.Http;
using YTDSSTGenII.Service;
using YTDSSTGenII.Service.Wexin;
using YTDSSTGenII.Service.Context;

using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Forms.Properties;

using YTDSSTGenII.Service.Enums;

namespace YTDSSTGenII.Forms.Pay
{
    /**
     * 二维码扫码窗口
     */
    public partial class FormLotteryQRCode : Form
    {
        private WexinService wexinService;

        private delegate void SwithPanelStatus();

        private WeXinSocketClient wexinSocketClient = WeXinSocketClient.getInstance();
        private WeXinSocketClient.RemoteCommandDelegate remoteCommandDelegate;

        /**
         * 机头操作类，来自于购彩主程序
         */ 
        private MotorOperate motorOperator;

        /**
         * 用户选择的机头上的票，即商品
         */ 
        private int selectedMotor = 0;

        //彩票的ID
        private int lotteryTypeId = 0;

        //付款状态
        private EnumPayStatus payStatus;
        //出票状态
        private EnumCutPaperStatus cutPaperStatus;
        //购彩金额
        private int money = 0;

        //倒计时秒数
        private int seconds = 60;

        //系统配置文件
        private IniFile ini;

        private String orderId;

        public FormLotteryQRCode(IniFile ini, MotorOperate motorOperator, int selectedMotor, int lotteryTypeId, int money)
        {
            InitializeComponent();

            this.Size = new Size(606, 696);
            
            //初始状态
            payStatus = EnumPayStatus.PAY_INIT;
            cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_INIT;

            this.money = money;
            this.selectedMotor = selectedMotor;
            this.lotteryTypeId = lotteryTypeId;
            this.motorOperator = motorOperator;

            //初始化
            wexinService = new WexinService();
            //wexinService.WexinRequestDeletageMethod = new WexinService.WexinRequestDeletage(OnWexinLoaded);
            remoteCommandDelegate = new WeXinSocketClient.RemoteCommandDelegate(OnRemoteCommand);

            this.ini = ini;
        }

        private void FormLotteryQRCode_Load(object sender, EventArgs e)
        {
            wexinSocketClient.RegistDelegate(remoteCommandDelegate);

            //For test.
            //ShowWaitGenerateQRCodePanel();
            //ShowWaitUserScanQRCodePanel();
            //ShowNotSupportWeixinBuyLotteryPanel();
            //ShowCutPaperFailed();
            //ShowCutPaperSuccess();
            //ShowCuttingPaperPanel();

            ShowConnectServerPanel();

            //连接网络服务器
            var t = new Task(loadInitData);
            t.ContinueWith(p =>
            {   
                Console.WriteLine("Conect to Server end.");
            });

            t.Start();
        }

        private void loadInitData()
        {
            //启动TCP长连接
            try
            {
                wexinSocketClient.ConnectToServer();
            }
            catch
            { }
            
            if (wexinSocketClient.Connected == false)
            {
                //网络未接通，本机暂时不支持微信购买，提示用户投入现金购彩
                ShowNotSupportWeixinBuyLotteryPanel();
            }
            else
            {
                ShowWaitGenerateQRCodePanel();

                RemoteData remoteData = new RemoteData();
                //远程命令参数设置
                //1.机器编号
                remoteData.MachineCode = GlobalParmeters.machineNumber;
                //2.协议类型，即命令类型
                remoteData.ProtocolId = C001ContentReq.PROTOCOL_TYPE;
                //3.请求的参数
                C001ContentReq req = new C001ContentReq();
                //3.1机头号
                req.HeadNo = this.selectedMotor + "";
                //3.2彩票的ID
                req.LotteryTypeId = string.Format("{0:D3}", this.lotteryTypeId); 
                //3.3终端机代码
                req.TerminalCode = remoteData.MachineCode;
                
                remoteData.Content = req;

                try
                {
                    wexinSocketClient.WriteSocketData(remoteData);
                }
                catch (Exception exp) {
                    CommonUtils.WriteExceptionInfo(exp);
                }
            }
        }

        private void FormLotteryQRCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            wexinSocketClient.UnregistDeletegate(remoteCommandDelegate);
            wexinSocketClient.Close();
        }

        /**
         * 处理远程下发的出票请求,非UI线程调用
         */ 
        private void OnRemoteCommand(RemoteData remoteData) {
            
            if(remoteData.ProtocolId.Equals(C001ContentReq.PROTOCOL_TYPE)) {
                //二维码请求响应
                C001ContentAck ack = (C001ContentAck)remoteData.Content;
                String qrcode = ack.QRCodeUrl;

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 8;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(qrcode);
               
                wexinWaitScanQRCodePanel.SetQRCodeImage(image);
                
                ShowWaitUserScanQRCodePanel();

                return;
            }

            if(remoteData.ProtocolId.Equals(C002ContentReq.PROTOCOL_TYPE)) 
            {
                C002ContentReq req = (C002ContentReq)remoteData.Content;
                //扫码通知
                //ShowWaitGenerateQRCodePanel();
                RemoteData c002RemoteData = new RemoteData();
                c002RemoteData.MachineCode = remoteData.MachineCode;
                c002RemoteData.ProtocolId = remoteData.ProtocolId;
                C002ContentAck c002Ack = new C002ContentAck();
                
                c002RemoteData.Content = c002Ack;
                c002Ack.OrderId = req.OrderId;

                try
                {
                    wexinSocketClient.WriteSocketData(c002RemoteData);
                }
                catch (Exception exp) {
                    CommonUtils.WriteExceptionInfo(exp);
                }

                return;
            }

            if(remoteData.ProtocolId.Equals(C003ContentReq.PROTOCOL_TYPE))
            {
                C003ContentReq c003Req = (C003ContentReq)remoteData.Content;

                //支付成功通知
                ShowCuttingPaperPanel();
                OnWexinCutPaper((C003ContentReq)remoteData.Content);

                //ack
                RemoteData c003RemoteData = new RemoteData();

                c003RemoteData.ProtocolId = remoteData.ProtocolId;
                c003RemoteData.MachineCode = remoteData.MachineCode;

                C003ContentAck c003Ack = new C003ContentAck();

                c003Ack.OrderId = c003Req.OrderId;
                c003Ack.CutpaperState = 0; //1
                c003RemoteData.Content = c003Ack;

                try
                {
                    wexinSocketClient.WriteSocketData(c003RemoteData);
                }
                catch (Exception exp) {
                    CommonUtils.WriteExceptionInfo(exp);
                }

                return;
            }

            if(remoteData.ProtocolId.Equals(C004ContentReq.PROTOCOL_TYPE))
            {
                //重连响应, Do nothing.
                return;
            }

            if (remoteData.ProtocolId.Equals(C005ContentReq.PROTOCOL_TYPE))
            { 
                //出票通知应答 Do nothing.
                return;
            }
        }

        private void OnWexinCutPaper(C003ContentReq req) {
            this.orderId = req.OrderId;

            //执行减票逻辑
            int motorLength = 0;
            int lotteryPrice = 0;
            int motorLotteryRemain = 0;
            string lotteryName = "";
           
            DbOperate operate = new DbOperate();
            operate.GetMotorInfoByMotorNumber(this.selectedMotor, ref motorLength, ref lotteryPrice, ref motorLotteryRemain, ref lotteryName);
             //无余量
            if (motorLotteryRemain < 1)
            {
                GlobalParmeters.MotorStatus[this.selectedMotor - 1] = 1;
                ShowCutPaperFailed();
                return;
            }

            bool cupaperSuccess = CutPaper(this.selectedMotor);

            if (cupaperSuccess)
            {  
                //出票成功
                operate.Decrease(this.selectedMotor, motorLotteryRemain - 1);
                motorLotteryRemain--;
                operate.InsertTrade(this.selectedMotor + "", lotteryPrice.ToString(), lotteryName);
                            
                ShowCutPaperSuccess();
            }
            else
            {
                //记录到交易表
                operate.InsertTradeFailInfo(this.selectedMotor + "", this.money + "", lotteryName);
                ShowCutPaperFailed();
            }

            //通知服务器出票状态
            RemoteData c005RemoteData = new RemoteData();
            c005RemoteData.ProtocolId = C005ContentReq.PROTOCOL_TYPE;
            c005RemoteData.MachineCode = GlobalParmeters.machineNumber;

            C005ContentReq c005Req = new C005ContentReq();
            c005Req.OrderId = req.OrderId;
            c005Req.Tickets = new List<Ticket>();
            if (cupaperSuccess)
            {
                c005Req.OrderCutpaperStatus = (int)EnumOrderCutpaperStatus.SUCCESS;
            }
            else 
            {
                c005Req.OrderCutpaperStatus = (int)EnumOrderCutpaperStatus.FAILED;
            }

            c005RemoteData.Content = c005Req;

            try
            {
                wexinSocketClient.WriteSocketData(c005RemoteData);
            }
            catch (Exception exp) {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }

        //从指定的机器上出一张票
        private bool CutPaper(int p)
        {
            try
            {
                //出票
                byte[] buffer = this.motorOperator.CutCardOperate(p);

                //机头返回数据出错
                if ((buffer == null) || (buffer.Length < 10))
                {
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    return false;
                }

                //机头出票不成功
                if (buffer[2] != 0x55)
                {
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

        private void OnWexinCutPaperResult(CutPaperResult result) 
        {
            //TODO 出票结果处理
            //回调服务器
        }

        /**
         * 根据当前的操作流程，显示不同的panel
         **/

        private void ShowConnectServerPanel() {
            if (wexinConnectServerPanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowConnectServerPanel));
            }
            else
            {
                btnClose.Show();
                wexinConnectServerPanel.Location = new Point(25, 125);
                wexinConnectServerPanel.Show();

                wexinWaitScanQRCodePanel.Hide();
                wexinCutPaperFailedPanel.Hide();
                wexinCutPaperSuccessPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
            }
        }

        private void SetWindowTitle(EnumTitleLogoType logoType, String msg) 
        {
            if(msg != null)
                lblTitle.Text = msg;

           if(logoType == EnumTitleLogoType.TITLE_LOGO_TYPE_CRY)
           {
                picBoxLogo.Image = Resources.标题红色哭脸Logo;
           }
           else if (logoType == EnumTitleLogoType.TITLE_LOGO_TYPE_ERROR)
           {
               picBoxLogo.Image = Resources.标题警告Logo;
           }
           else if(logoType == EnumTitleLogoType.TITLE_LOGO_TYPE_SMILE)
           {
                picBoxLogo.Image = Resources.标题绿色笑脸Logo;
           }
           else if(logoType == EnumTitleLogoType.TITLE_LOGO_TYPE_WARNING)
           {
                picBoxLogo.Image = Resources.标题警告Logo;
           }
           else if(logoType == EnumTitleLogoType.TITLE_LOGO_TYPE_YES)
           {
                picBoxLogo.Image = Resources.标题绿色勾logo;
           }
        }

        private void ShowWaitUserScanQRCodePanel() {
            if (wexinWaitScanQRCodePanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowWaitUserScanQRCodePanel));
            }
            else
            {
                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_SMILE, "微信支付，立即出票");

                btnClose.Show();

                wexinWaitScanQRCodePanel.Location = new Point(25, 125);
                wexinWaitScanQRCodePanel.SetMoney(10);
                wexinWaitScanQRCodePanel.Show();

                payStatus = EnumPayStatus.PAY_WAIT_SCAN_QRCODE;
                cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_INIT;

                //开始倒计时
                BeginToCounDown(120);

                //hide other panel
                wexinCutPaperFailedPanel.Hide();
                wexinCutPaperSuccessPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        /**
         * 显示不支持微信购彩
         */ 
        private void ShowNotSupportWeixinBuyLotteryPanel() {
            if (wexinNotSupportedPanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowNotSupportWeixinBuyLotteryPanel));
            }
            else
            {
                btnClose.Show();

                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_CRY, "提示信息");

                wexinNotSupportedPanel.Location = new Point(25, 125);
                wexinNotSupportedPanel.Show();

                this.payStatus =EnumPayStatus.PAY_NOT_SUPPPRT;
                this.cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_INIT;

                BeginToCounDown(10);

                wexinCutPaperFailedPanel.Hide();
                wexinCutPaperSuccessPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
                wexinWaitScanQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        /**
         * 生成二维码中
         */ 
        private void ShowWaitGenerateQRCodePanel() {

            if (wexinGenerateQRCodePanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowWaitGenerateQRCodePanel));
            }
            else
            {
                btnClose.Show();

                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_SMILE, "微信支付，立即出票");

                wexinGenerateQRCodePanel.Location = new Point(25, 125);
                wexinGenerateQRCodePanel.SetMoney(this.money);

                this.payStatus = EnumPayStatus.PAY_GENERATE_QRCODE;
                this.cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_INIT;

                wexinGenerateQRCodePanel.Show();

                //hide other
                wexinCutPaperFailedPanel.Hide();
                wexinCutPaperSuccessPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinWaitScanQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        /**
         * 正在出票
         */
        private void ShowCuttingPaperPanel() 
        {
            if (wexinCutingPaperPanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowCuttingPaperPanel));
            }
            else
            {
                //TODO
                //btnClose.Hide();
                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_YES, "支付成功, 出票中...");

                wexinCutingPaperPanel.Location = new Point(25, 125);
                wexinCutingPaperPanel.Show();
                this.payStatus = EnumPayStatus.PAY_SUCCESS;
                this.cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_ING;

                //hide other panel
                wexinCutPaperFailedPanel.Hide();
                wexinCutPaperSuccessPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
                wexinWaitScanQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        /**
         * 出票成功
         */
        private void ShowCutPaperSuccess() 
        {
            if (wexinCutPaperSuccessPanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowCutPaperSuccess));
            }
            else
            {
                btnClose.Show();
                
                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_YES, "出票成功了");

                wexinCutPaperSuccessPanel.Location = new Point(25, 125);
                wexinCutPaperSuccessPanel.Show();

                this.payStatus = EnumPayStatus.PAY_SUCCESS;
                this.cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_SUCCESS;

                BeginToCounDown(10);

                wexinCutPaperFailedPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
                wexinWaitScanQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        /**
         * 出票失败
         */ 
        private void ShowCutPaperFailed()
        {
            if (wexinCutPaperFailedPanel.InvokeRequired)
            {
                this.BeginInvoke(new SwithPanelStatus(ShowCutPaperFailed));
            }
            else
            {
                btnClose.Show();
                
                SetWindowTitle(EnumTitleLogoType.TITLE_LOGO_TYPE_CRY, "诶呀，出票失败了");

                wexinCutPaperFailedPanel.Location = new Point(25, 125);
                wexinCutPaperFailedPanel.Show();

                wexinCutPaperFailedPanel.SetOrderId(this.orderId);
                wexinCutPaperFailedPanel.SetServicePhoneNumber(GlobalParmeters.ServicePhoneNumber);


                this.payStatus = EnumPayStatus.PAY_SUCCESS;
                this.cutPaperStatus = EnumCutPaperStatus.CUT_PAPER_FAILED;

                BeginToCounDown(120);

                wexinCutPaperSuccessPanel.Hide();
                wexinNotSupportedPanel.Hide();
                wexinCutingPaperPanel.Hide();
                wexinGenerateQRCodePanel.Hide();
                wexinWaitScanQRCodePanel.Hide();
                wexinConnectServerPanel.Hide();
            }
        }

        private void BeginToCounDown(int seconds) 
        {
            this.seconds = seconds;
            timerCountDown.Enabled = true;
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            if (this.seconds > 0)
            {
                this.seconds = seconds - 1;
            }

            //更新到UI中
            if (payStatus == EnumPayStatus.PAY_WAIT_SCAN_QRCODE)
            {
                wexinWaitScanQRCodePanel.SetTimerCountDown(this.seconds);
            }
            else if (payStatus == EnumPayStatus.PAY_SUCCESS && cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_SUCCESS)
            {
                wexinCutPaperSuccessPanel.SetTimerCountDown(this.seconds);
            }
            else if (payStatus == EnumPayStatus.PAY_SUCCESS && cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_FAILED)
            {
                wexinCutPaperFailedPanel.SetTimerCountDown(this.seconds);
            }
            else if (payStatus == EnumPayStatus.PAY_NOT_SUPPPRT)
            { 
            }

            //超时处理
            if (this.seconds == 0)
            {
                this.Enabled = false;
                //执行倒计时结束逻辑

                //扫码超时后关闭或者系统不支持
                if (payStatus == EnumPayStatus.PAY_WAIT_SCAN_QRCODE ||  payStatus == EnumPayStatus.PAY_NOT_SUPPPRT)
                {
                    base.Close();
                    base.Dispose();
                    return;
                }

                //出票后自动关闭
                if(payStatus == EnumPayStatus.PAY_SUCCESS && (
                    cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_FAILED ||
                    cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_SUCCESS ||
                    cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_PART_FAILED))
                {
                    base.Close();
                    base.Dispose();
                    return;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ModelDialogHolder hoder;

            if (payStatus == EnumPayStatus.PAY_SUCCESS &&
                cutPaperStatus == EnumCutPaperStatus.CUT_PAPER_ING)
            {
                hoder = new ModelDialogHolder("正在交易中");
                hoder.ShowDialog();

                return;
            }
            else
            {
                hoder = new ModelDialogHolder("退出当前交易?");
                hoder.ShowDialog();
            }

            if (hoder.Result == true)
            {
                base.Close();
                base.Dispose();
            }
            else
            {
                return;
            }
            
        }

        private void wexinWaitScanQRCodePanel_Load(object sender, EventArgs e)
        {

        }
    }
}
