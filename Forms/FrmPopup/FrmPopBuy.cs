using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using YTDSSTGenII.Forms.model;
using YTDSSTGenII.Service.Context;
using YTDSSTGenII.Service.Exception;
using YTDSSTGenII.Service.Wexin;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms.Forms.FrmPopup
{
    public partial class FrmPopBuy : Form
    {
        //支付方式：Wechat - 微信, Alipay - 支付宝
        private enum pay_type {Cash = 1, Wechat, Alipay }
        private int pay_type_current = 0;

        private bool is_need_request_qrcode = false;//是否需要请求二维码
        private bool is_can_close = false;//是否需要去关闭窗口
        private bool is_need_check_money = false;//是否需要检查余额
        private bool is_window_alive = true;//窗口是否活着
        private bool is_need_check_payresult = false;//是否需要检查支付结果

        private bool qrcode_is_alive = false;//二维码在有效时间内

        int lastbalance = 0; //上一次的余额
        private int pay_cut_down = 120;//支付剩余时间倒计时

        private Label[] LabelArray = new Label[8];

        private WeXinSocketClient.RemoteCommandDelegate rdelegate;

        public FrmPopBuy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPopBuy_Load(object sender, EventArgs e)
        {
            this.Size = new Size(601, 980);

            CommonUtils.WritePayLogInfo("进入支付界面");
            //设置当前是在支付界面上
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_IN_PAY_FORM = true;
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = true;//用来作为是否情况购物车的条件之一

            LabelArray = new Label[8] { this.lab_lottery01_tip, this.lab_lottery02_tip, this.lab_lottery03_tip, this.lab_lottery04_tip, this.lab_lottery05_tip, this.lab_lottery06_tip, this.lab_lottery07_tip, this.lab_lottery08_tip };

            initFormInfo();

            //如果是没有余额，默认显示微信支付
            /*
            if (UserContext.getInstance().Balance == 0)
            {
                CommonUtils.WriteWeiXinLogInfo("当前为微信支付");
                refreshWeixinInfo();
            }
            else//显示现金支付
            {
                CommonUtils.WriteWeiXinLogInfo("当前为现金支付");
                is_need_request_qrcode = false;
                refreshCashInfo();
            }*/

            //默认为现金支付
            CommonUtils.WritePayLogInfo("当前为现金支付");
            is_need_request_qrcode = false;
            refreshCashInfo();

            rdelegate = new WeXinSocketClient.RemoteCommandDelegate(OnRemoteCommand);
            WeXinSocketClient.getInstance().RegistDelegate(rdelegate);

            //请求二维码的线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(requestQRCode));
            //检查关闭的线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(checkClose));
            //检查余额线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(checkMoney));
            //请求支付结果信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(requestPayResult));

            //请求支付结果信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(checkPayCutDown));

            CommonUtils.WritePayLogInfo("初始化支付界面完成");
        }

        /// <summary>
        /// 支付时间倒计时
        /// </summary>
        /// <param name="state"></param>
        private void checkPayCutDown(object state)
        {
            while (is_window_alive)
            {
                this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                {
                    if (pay_cut_down > 0)
                    {
                        pay_cut_down--;
                    }
                    else
                    {
                        is_can_close = true;
                    }

                    this.labPayCutDown.Text = String.Format("请在 {0} 秒内完成支付", pay_cut_down.ToString());
                }));
                Thread.Sleep(1000);
            }
        }

        //TODO 120秒才检查一次？
        private void requestPayResult(object state)
        {
            //5秒后开始检查
            Thread.Sleep(5 * 1000);
            CommonUtils.WritePayLogInfo("开始查询微信支付结果");
            while (is_window_alive)
            {
                if (is_need_check_payresult)
                {
                    try
                    {
                        RemoteData remoteData = new RemoteData();
                        //远程命令参数设置
                        //1.机器编号
                        remoteData.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
                                                                                //2.协议类型，即命令类型
                        remoteData.ProtocolId = C009ContentReq.PROTOCOL_TYPE;
                        //3.请求的参数
                        C009ContentReq req = new C009ContentReq();


                        req.OrderId = UserContext.getInstance().UserOrder.OutTradeNo;



                        remoteData.Content = req;

                        WeXinSocketClient.getInstance().WriteSocketData(remoteData);
                        CommonUtils.WritePayLogInfo("发送订单支付状态请求信息");
                    }
                    catch (Exception)
                    {
                        CommonUtils.WritePayLogInfo("发送订单支付状态请求信息异常");
                    }
                }

                Thread.Sleep(3 * 1000);
            }
        }

        /// <summary>
        /// 当前现金支付——显示现金相关信息
        /// </summary>
        private void refreshCashInfo()
        {
            pay_cut_down = 120;
            this.btnCash.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.xianjin_on;
            this.btnWechat.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_off;
            this.btnAlipay.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.alipay_off;
            this.lab_money_tip01.Visible = true;
            this.lab_money_tip02.Visible = true;
            this.lab_money_tip03.Visible = true;
            this.lab_money_tip04.Visible = true;
            this.lab_money_tip05.Visible = true;
            this.lab_money_tip06.Visible = true;

            if (UserContext.getInstance().Balance >= UserContext.getInstance().getBuyMoney())
            {
                this.pBoxSurePay.Visible = true;
            }
            else
            {
                this.pBoxSurePay.Visible = false;
            }

            this.lab_tip_weixin.Visible = false;
            this.pBox_QRCode.Visible = false;
            this.lab_loading_tips.Visible = false;

            this.pBox_zflc.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.xianjin_liucheng;

            //钱箱打开
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_NEED_CHECK_MONEYBOX = true;//去轮询收币器
            is_need_check_money = true;//检查余额

            //如果余额已经大于0，不能再切换到微信支付了
            if (UserContext.getInstance().Balance > 0)
            {
                this.btnWechat.Enabled = false;
                this.btnAlipay.Enabled = false;
            }
        }

        Image QRCode_temp_wechat = null;//微信二维码缓存
        Image QRCode_temp_alipay = null;//支付宝二维码缓存

        public int Pay_type_current
        {
            get
            {
                return pay_type_current;
            }

            set
            {
                if (this.pay_type_current != value)
                {
                    this.pay_type_current = value;
                    this.pBox_QRCode.Image = YTDSSTGenII.Forms.Properties.Resources.loading;
                    this.pBox_QRCode.BackgroundImage = null;

                    if (this.Pay_type_current == (int)pay_type.Wechat)
                    {
                        if (this.QRCode_temp_wechat != null && this.pay_cut_down > 0)
                        {
                            this.pBox_QRCode.Image = null;
                            this.pBox_QRCode.BackgroundImage = this.QRCode_temp_wechat;
                        }
                        else
                        {
                            //如果二维码不在有效时间内，可以再次请求二维码
                            //if (!qrcode_is_alive)
                            {
                                is_need_request_qrcode = true;
                            }
                        }
                    }
                    else if (this.Pay_type_current == (int)pay_type.Alipay)
                    {
                        if (this.QRCode_temp_alipay != null && this.pay_cut_down > 0)
                        {
                            this.pBox_QRCode.Image = null;
                            this.pBox_QRCode.BackgroundImage = this.QRCode_temp_alipay;
                        }
                        else
                        {
                            //如果二维码不在有效时间内，可以再次请求二维码
                            //if (!qrcode_is_alive)
                            {
                                is_need_request_qrcode = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当前微信支付——显示微信相关信息
        /// </summary>
        private void refreshWeixinInfo()
        {
            this.Pay_type_current = (int)pay_type.Wechat;//取支付类型
            this.pay_cut_down = 120;
            this.btnCash.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.xianjin_off;
            this.btnWechat.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_on;
            this.btnAlipay.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.alipay_off;
            this.lab_money_tip01.Visible = false;
            this.lab_money_tip02.Visible = false;
            this.lab_money_tip03.Visible = false;
            this.lab_money_tip04.Visible = false;
            this.lab_money_tip05.Visible = false;
            this.lab_money_tip06.Visible = false;

            this.pBoxSurePay.Visible = false;

            this.lab_tip_weixin.Visible = true;
            this.pBox_QRCode.Visible = true;
            this.lab_loading_tips.Visible = true;

            this.pBox_zflc.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_liucheng;

            //钱箱关闭
            Service.Context.ApplicationContext.getInstance().IS_NEED_CHECK_MONEYBOX = false;//停止去轮询收币器

            is_need_check_money = false;//停止检查余额
        }

        /// <summary>
        /// 当前支付宝支付——显示支付宝相关信息
        /// </summary>
        private void refreshAlipayInfo()
        {
            this.Pay_type_current = (int)pay_type.Alipay;//取支付类型
            this.pay_cut_down = 120;
            this.btnAlipay.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.alipay_on;
            this.btnWechat.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.weixin_off;
            this.btnCash.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.xianjin_off;
            this.lab_money_tip01.Visible = false;
            this.lab_money_tip02.Visible = false;
            this.lab_money_tip03.Visible = false;
            this.lab_money_tip04.Visible = false;
            this.lab_money_tip05.Visible = false;
            this.lab_money_tip06.Visible = false;
            this.pBoxSurePay.Visible = false;
            this.lab_tip_weixin.Visible = true;
            this.pBox_QRCode.Visible = true;
            this.lab_loading_tips.Visible = true;

            //替换为支付宝支付流程图片
            this.pBox_zflc.BackgroundImage = global::YTDSSTGenII.Forms.Properties.Resources.alipay_liucheng;

            //钱箱关闭,停止去轮询收币器
            Service.Context.ApplicationContext.getInstance().IS_NEED_CHECK_MONEYBOX = false;

            is_need_check_money = false;//停止检查余额
        }

        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initFormInfo()
        {
            CommonUtils.WritePayLogInfo("开始初始化界面显示");
            lastbalance = UserContext.getInstance().Balance;
            this.lbPrice.Text = "总金额: " + UserContext.getInstance().getBuyMoney().ToString() + " 元";
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                if (UserContext.getInstance().UserMotorArray[i].BuyLotteryNum > 0)
                {
                    LabelArray[j].Text = String.Format("{0} x {1}({2}元) {3}元",
                       UserContext.getInstance().UserMotorArray[i].BuyLotteryNum.ToString(),
                        UserContext.getInstance().UserMotorArray[i].Lottery.LotteryName,
                        UserContext.getInstance().UserMotorArray[i].Lottery.UnitPrice.ToString(),
                        (UserContext.getInstance().UserMotorArray[i].BuyLotteryNum * UserContext.getInstance().UserMotorArray[i].Lottery.UnitPrice).ToString()
                        );
                    j++;
                }
            }
            for (int i = j; i < 8; i++)
            {
                LabelArray[i].Visible = false;
            }

            CommonUtils.WritePayLogInfo("初始化界面显示完成");
        }

        /// <summary>
        /// 检查余额线程
        /// </summary>
        /// <param name="state"></param>
        private void checkMoney(object state)
        {
            while (is_window_alive)
            {
                if (is_need_check_money)
                {
                    this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                    {
                        this.lab_money_tip02.Text = UserContext.getInstance().Balance.ToString();
                        if ((UserContext.getInstance().getBuyMoney() - UserContext.getInstance().Balance) <= 0)
                        {
                            this.lab_money_tip05.Text = "0";
                        }
                        else
                        {
                            this.lab_money_tip05.Text = (UserContext.getInstance().getBuyMoney() - UserContext.getInstance().Balance).ToString();
                        }

                        if (UserContext.getInstance().Balance > 0) //余额大于0
                        {
                            this.btnWechat.Enabled = false;
                            this.btnAlipay.Enabled = false;
                            if (lastbalance != UserContext.getInstance().Balance)
                            {
                                pay_cut_down = 120;
                                lastbalance = UserContext.getInstance().Balance;
                            }
                        }

                    }));

                    //如果余额已经足够支付，支付成功去出票
                    if (UserContext.getInstance().Balance >= UserContext.getInstance().getBuyMoney())
                    {
                        CommonUtils.WritePayLogInfo("余额足够，完成支付去出票");
                        this.pBoxSurePay.Visible = true;
                    }
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 检查是否能关闭窗口
        /// </summary>
        /// <param name="state"></param>
        private void checkClose(object state)
        {
            while (is_window_alive)
            {
                if (is_can_close)
                {
                    this.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                    {
                        CommonUtils.WritePayLogInfo("关闭界面");
                        this.Close();
                    }));
                    is_window_alive = false;
                    //设置当前已经不是在支付界面上
                    YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_IN_PAY_FORM = false;
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 请求二维码线程
        /// </summary>
        /// <param name="state"></param>
        private void requestQRCode(object state)
        {
            while (is_window_alive)
            {
                if (is_need_request_qrcode)
                {
                    requestQRCodeHandler();
                }
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// 请求二维码操作
        /// </summary>
        private void requestQRCodeHandler()
        {
            try
            {
                RemoteData remoteData = new RemoteData();
                //远程命令参数设置
                //1.机器编号
                remoteData.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
                //2.协议类型，即命令类型
                remoteData.ProtocolId = C006ContentReq.PROTOCOL_TYPE;
                //3.请求的参数
                C006ContentReq req = new C006ContentReq();
                List<ticket> lts = new List<ticket>();
                foreach (UserMotor item in UserContext.getInstance().UserMotorArray)
                {
                    if (item.BuyLotteryNum > 0)
                    {
                        ticket t = new ticket();
                        t.HeadNo = item.MotorId;
                        t.LotteryTypeId = item.Lottery.LotteryId;
                        t.Nums = item.BuyLotteryNum;
                        lts.Add(t);
                    }
                }
                req.pay_type = this.Pay_type_current;// 设置支付方式
                req.tickets = lts;
                remoteData.Content = req;

                WeXinSocketClient.getInstance().WriteSocketData(remoteData);
                CommonUtils.WritePayLogInfo("发送二维码请求信息");
                is_need_request_qrcode = false;
                is_need_check_payresult = true;
            }
            catch (ServiceException exp)
            {
                //重新打开链接
                CommonUtils.WritePayLogInfo("发送二维码请求信息异常");
                this.lab_loading_tips.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                {
                    this.lab_loading_tips.Text = "当前网络已断开，请使用现金支付";
                }));
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageDialog md = new MessageDialog();
            md.setMessage("取消当前支付订单?");

            md.ShowDialog();

            if (md.Result == true)
            {
                this.btnClose.Enabled = false;
                is_can_close = true;
            }
        }

        /// <summary>
        /// 切换到支付宝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlipay_Click(object sender, EventArgs e)
        {
            CommonUtils.WritePayLogInfo("切换到支付宝支付");
            refreshAlipayInfo();
        }

        /// <summary>
        /// 切换到微信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWechat_Click(object sender, EventArgs e)
        {
            CommonUtils.WritePayLogInfo("切换到微信支付");
            refreshWeixinInfo();
        }

        /// <summary>
        /// 切换到现金支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCash_Click(object sender, EventArgs e)
        {
            CommonUtils.WritePayLogInfo("切换到现金支付");
            refreshCashInfo();
        }


        /**
         * 处理远程下发的出票请求,非UI线程调用
         */
        private void OnRemoteCommand(RemoteData remoteData)
        {
            CommonUtils.WritePayLogInfo("接受到反馈信息" + remoteData.ToString());
            try
            {
                if (remoteData.ProtocolId.Equals(C006ContentReq.PROTOCOL_TYPE))
                {
                    CommonUtils.WritePayLogInfo("收到请求二维码回馈信息");
                    //二维码请求响应
                    C006ContentAck ack = (C006ContentAck)remoteData.Content;
                    CommonUtils.WritePayLogInfo("转换为对象正确");
                    try
                    {
                        if (!ack.ErrorMsg.Equals(""))
                        {
                            this.lab_loading_tips.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                            {
                                this.lab_loading_tips.Text = "生成二维码失败,请选择现金支付!";
                            }));
                            return;
                        }
                    }
                    catch
                    {
                    }

                    string qrcode = ack.QRCodeUrl;
                    CommonUtils.WritePayLogInfo("二维码数据为：" + ack.QRCodeUrl);
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeScale = 4;
                    qrCodeEncoder.QRCodeVersion = 7;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    Image image = qrCodeEncoder.Encode(qrcode);

                    pBox_QRCode.Image = null;
                    pBox_QRCode.BackgroundImage = image;
                    //pBox_QRCode.Image = image;

                    //二维码缓存
                    if (this.Pay_type_current == (int)pay_type.Wechat)
                    {
                        this.QRCode_temp_wechat = image;
                    }
                    else if (this.Pay_type_current == (int)pay_type.Alipay)
                    {
                        this.QRCode_temp_alipay = image;
                    }

                    this.lab_loading_tips.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                    {
                        this.lab_loading_tips.Text = "请在120秒内完成支付";
                    }));

                    //解析订单
                    Order o = new Order(ack);
                    o.PayType = this.Pay_type_current;
                    UserContext.getInstance().UserOrder = o;
                    if (this.Pay_type_current == (int)pay_type.Wechat)
                    {
                        UserContext.getInstance().OrderIdTempWechat = o.OutTradeNo;
                    }
                    else if (this.Pay_type_current == (int)pay_type.Alipay)
                    {
                        UserContext.getInstance().OrderIdTempAlipay = o.OutTradeNo;
                    }
                    //向数据库插入一条订单
                    addOrderInfo2db(o);
                    //is_need_wait = true;//等待扫码支付，不能关闭窗口

                    //二维码在有效时间内
                    this.qrcode_is_alive = true;

                    return;
                }
            }
            catch (Exception)
            {
                this.lab_loading_tips.Invoke(new EventHandler(delegate (object o2, EventArgs e)
                {
                    this.lab_loading_tips.Text = "生成二维码失败,请选择现金支付!";
                }));
                CommonUtils.WritePayLogInfo("解析二维码出现异常");
                return;
            }


            try
            {
                if (remoteData.ProtocolId.Equals(C007ContentReq.PROTOCOL_TYPE))
                {
                    CommonUtils.WritePayLogInfo("收到支付结果回馈信息");
                    //支付结果
                    C007ContentAck ack = (C007ContentAck)remoteData.Content;
                    //判断回来的订单号是不是当前的订单号，不是得话不做处理
                    //---加支付类型判断
                    //---加两个订单号（微信，支付宝）的判断
                    //if (!ack.OrderId.Equals(UserContext.getInstance().UserOrder.OutTradeNo))
                    //{
                    //    CommonUtils.WritePayLogInfo(String.Format("(C007ContentReq)错误的订单号,当前订单号{0},服务器反馈订单号{1}", UserContext.getInstance().UserOrder.OutTradeNo, ack.OrderId));
                    //    return;
                    //}
                    if (ack.OrderId.Equals(UserContext.getInstance().OrderIdTempWechat))
                    {
                        UserContext.getInstance().UserOrder.OutTradeNo = UserContext.getInstance().OrderIdTempWechat;
                        UserContext.getInstance().UserOrder.PayType = (int)pay_type.Wechat;
                    }
                    else if (ack.OrderId.Equals(UserContext.getInstance().OrderIdTempAlipay))
                    {
                        UserContext.getInstance().UserOrder.OutTradeNo = UserContext.getInstance().OrderIdTempAlipay;
                        UserContext.getInstance().UserOrder.PayType = (int)pay_type.Alipay;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Wechat:{0},Ali:{1},OrderId:{2}", UserContext.getInstance().OrderIdTempWechat , UserContext.getInstance().OrderIdTempAlipay , ack.OrderId));/////////////////
                        CommonUtils.WritePayLogInfo(String.Format("(C007ContentReq)错误的订单号,当前订单号{0},服务器反馈订单号{1}", UserContext.getInstance().UserOrder.OutTradeNo, ack.OrderId));
                        return;
                    }
                    
                    //todo 判断订单状态
                    //非待出票初始状态，不处理
                    if (UserContext.getInstance().UserOrder.TicketOutState != 0)
                    {
                        return;
                    }

                    if (ack.OrderState != "1")
                    {
                        return;
                    }

                    //改变数据库的订单和票的状态
                    if ( UpdateOrderState ( ack ) == 0 )
                    {
                        return;
                    }

                    UserContext.getInstance().UserOrder.OrderState = Convert.ToInt32(ack.OrderState);
                    UserContext.getInstance().UserOrder.PayTime = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    UserContext.getInstance().UserOrder.TicketOutState = 1; //出票中

                    //UserContext.getInstance().UserOrder.TicketOutState = ack.OrderState == "1" ? 1 : 2;

                    //向服务器回传一条信息
                    RemoteData rdata = new RemoteData();
                    //远程命令参数设置
                    //1.机器编号
                    rdata.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
                                                                       //2.协议类型，即命令类型
                    rdata.ProtocolId = C007ContentReq.PROTOCOL_TYPE;
                    //3.请求的参数
                    C007ContentReq req = new C007ContentReq();
                    req.OrderId = ack.OrderId;
                    remoteData.Content = req;
                    WeXinSocketClient.getInstance().WriteSocketData(remoteData);
                    is_need_check_payresult = false;
                    //可以关闭窗口
                    is_can_close = true;
                }

                if (remoteData.ProtocolId.Equals(C009ContentReq.PROTOCOL_TYPE))
                {
                    CommonUtils.WritePayLogInfo("收到支付结果回馈信息（单独请求）");
                    //支付结果
                    C009ContentAck ack = (C009ContentAck)remoteData.Content;
                    //判断回来的订单号是不是当前的订单号，不是得话不做处理
                    //if (!ack.OrderId.Equals(UserContext.getInstance().UserOrder.OutTradeNo))
                    if (ack.OrderId.Equals(UserContext.getInstance().OrderIdTempWechat))
                    {
                        UserContext.getInstance().UserOrder.OutTradeNo = UserContext.getInstance().OrderIdTempWechat;
                        UserContext.getInstance().UserOrder.PayType = (int)pay_type.Wechat;
                    }
                    else if (ack.OrderId.Equals(UserContext.getInstance().OrderIdTempAlipay))
                    {
                        UserContext.getInstance().UserOrder.OutTradeNo = UserContext.getInstance().OrderIdTempAlipay;
                        UserContext.getInstance().UserOrder.PayType = (int)pay_type.Alipay;
                    }
                    else
                    {
                        CommonUtils.WritePayLogInfo(String.Format("(C009ContentReq)错误的订单号,当前订单号{0},服务器反馈订单号{1}", UserContext.getInstance().UserOrder.OutTradeNo, ack.OrderId));
                        return;
                    }

                    if (!ack.OrderState.Equals("1") && !ack.OrderState.Equals("2"))
                    {
                        CommonUtils.WritePayLogInfo(String.Format("订单{0},错误的支付状态{1}", ack.OrderId, ack.OrderState));
                        return;
                    }

                    //改变数据库的订单和票的状态
                    //TODO 检查数据库的订单状态，如果订单是已支付,待出票,add by Bean.Long

                    //非待出票初始状态，不处理
                    if (UserContext.getInstance().UserOrder.TicketOutState != 0)
                    {
                        return;
                    }

                    if (ack.OrderState != "1")
                    {
                        return;
                    }

                    if ( UpdateOrderState ( ack ) == 0 )
                    {
                        return;
                    }

                    UserContext.getInstance().UserOrder.OrderState = Convert.ToInt32(ack.OrderState);
                    UserContext.getInstance().UserOrder.PayTime = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    //UserContext.getInstance().UserOrder.TicketOutState = ack.OrderState == "1" ? 1 : 2;

                    //向服务器回传一条信息
                    RemoteData rdata = new RemoteData();
                    //远程命令参数设置
                    //1.机器编号
                    rdata.MachineCode = GlobalParmeters.machineNumber; // GlobalParmeters.machineNumber
                                                                       //2.协议类型，即命令类型
                    rdata.ProtocolId = C007ContentReq.PROTOCOL_TYPE;
                    //3.请求的参数
                    C007ContentReq req = new C007ContentReq();
                    req.OrderId = ack.OrderId;
                    remoteData.Content = req;
                    WeXinSocketClient.getInstance().WriteSocketData(remoteData);
                    is_need_check_payresult = false;
                    //可以关闭窗口
                    is_can_close = true;
                }
            }
            catch (Exception e)
            {
                CommonUtils.WritePayLogInfo("解析回馈信息异常：" + e.Message);
            }
        }

        /// <summary>
        /// 支付成功，修改订单相应的状态
        /// </summary>
        /// <param name="oid"></param>
        private int UpdateOrderState(C007ContentAck ack)
        {
            return SQLiteHelper.ExecuteSql(String.Format("update p_terminal_orders set order_state={0},pay_time='{1}',ticket_out_state={2} where out_trade_no='{3}';",
                       ack.OrderState, System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), (ack.OrderState == "1" ? "1" : "0"), ack.OrderId
                ));
        }

        /// <summary>
        /// 支付成功，修改订单相应的状态
        /// </summary>
        /// <param name="oid"></param>
        private int UpdateOrderState(C009ContentAck ack)
        {
           return SQLiteHelper.ExecuteSql(String.Format("update p_terminal_orders set order_state={0},pay_time='{1}',ticket_out_state={2} where out_trade_no='{3}';",
                       ack.OrderState, System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), (ack.OrderState == "1" ? "1" : "0"), ack.OrderId
                ));
        }

        /// <summary>
        /// 向数据库插入订单信息
        /// </summary>
        /// <param name="o"></param>
        private void addOrderInfo2db(Order o)
        {
            List<String> sqllist = new List<string>();
            //插入订单sql
            String addordersql = String.Format(@"INSERT INTO p_terminal_orders(
                out_trade_no,
                terminal_code,
                pay_type,
                total_num,
                total_fee,
                order_state,
                create_time,
                end_time,
                pay_time,
                upload_time,
                ticket_out_state,
                out_success_num,
                out_success_money,
                out_error_num,
                out_error_money,
                deleted
                ) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                o.OutTradeNo, o.TerminalCode, o.PayType, o.TotalNum, o.TotalMoney, o.OrderState, o.CreateTime, o.EndTime, o.PayTime, o.UploadTime,
                o.TicketOutState, o.OutSuccNum, o.OutSuccMoney, o.OutErrorNum, o.OutErrorMoney, o.deleted);
            sqllist.Add(addordersql);

            //插入票信息——多条
            foreach (model.OrderDetails t in o.Tickets)
            {
                String addticketsql = String.Format(@"INSERT INTO p_terminal_order_details(
                    terminal_order_id,
                    out_trade_no,
                    self_terminal_id,
                    terminal_code,
                    head_no,
                    ticket_id,
                    ticket_out_time,
                    lottery_type_id,
                    lottery_name,
                    unit_price,
                    ticket_num,
                    ticket_out_state,
                    deleted
                    ) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                    t.TerminalOrderId, t.OutTradeNo, t.SelfTerminalId, t.TerminalCode, t.getHeadNo_String(), t.TicketId, t.TicketOutTime, t.LotteryTypeId, t.LotteryName, t.UnitPrice, t.Ticket_Num, t.TicketOutState, t.Deleted
                    );
                sqllist.Add(addticketsql);
            }

            SQLiteHelper.ExecuteSqlTran(sqllist);
        }

        //关闭的时候，要把注册的代理取消掉
        private void FrmPopBuy_FormClosing(object sender, FormClosingEventArgs e)
        {
            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;//用来作为是否情况购物车的条件之一
            WeXinSocketClient.getInstance().UnregistDeletegate(rdelegate);
        }

        private void pBoxSurePay_Click(object sender, EventArgs e)
        {
            //生成一个不插库的订单
            Order o = new Order(UserContext.getInstance().UserMotorArray);
            UserContext.getInstance().UserOrder = o;
            is_can_close = true;
            this.pBoxSurePay.Enabled = false;
        }

        private void plUP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lab_lottery01_tip_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    //class QRCodeObj
    //{
    //    Image qrcode_img = null;//二维码缓存
    //    private int pay_type = 0;//支付类型：微信，支付宝
    //    private int time = 120;//二维码有效时间
    //    public QRCodeObj()
    //    {
    //        //请求支付结果信息
    //        ThreadPool.QueueUserWorkItem(new WaitCallback(timeClick));
    //    }

    //    /// <summary>
    //    /// 支付时间倒计时
    //    /// </summary>
    //    /// <param name="state"></param>
    //    private void timeClick(object state)
    //    {
    //        while (time > 0)
    //        {
    //            time--;
    //            Thread.Sleep(1000);
    //        }
    //    }

    //    public int Pay_type
    //    {
    //        get
    //        {
    //            return pay_type;
    //        }

    //        set
    //        {
    //            pay_type = value;
    //        }
    //    }

    //    public Image Qrcode_img
    //    {
    //        get
    //        {
    //            return qrcode_img;
    //        }

    //        set
    //        {
    //            qrcode_img = value;
    //        }
    //    }

    //    public int Time
    //    {
    //        get
    //        {
    //            return time;
    //        }

    //        set
    //        {
    //            time = value;
    //        }
    //    }
    //}
}
