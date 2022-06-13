using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using YTDSSTGenII.Forms.Forms.FrmPopup;
using YTDSSTGenII.Forms.model;
using YTDSSTGenII.Forms.Properties;
using YTDSSTGenII.Service;
using YTDSSTGenII.Service.Context;
using YTDSSTGenII.Service.Enums;
using YTDSSTGenII.Service.Sg;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms
{
    public partial class FormBuyLotteryMain : Form
    {
        private Form tipForm = null;//弹框窗口

        public static int num;

        //220V 电源提示
        private bool breakSource;
        private int breakSourceTime = 30;

        //硬币器
        private CoinClass coin;

        //广告路径
        private string FileAddress = ConfigurationManager.AppSettings["advitisepath"];

        //扫码器
        private GetSecurityNumber Gsn;

        //托盘图标
        private IntPtr hTray;

        //是否禁用纸币器
        private bool InhibitCashBox;

        //配置文件 1.ini
        private IniFile ini;

        //消息是否初始化
        public bool InitSetShowMessageFlag;

        //Socket是否连接
        public bool isConnected;

        //是否有消费行为
        public bool isConsumption;

        //是否正在兑奖
        public bool isDuiJiang;

        //是否正在闪灯
        private Boolean isFlashLights;

        //是否曾在结算
        public bool isJieSuan;

        //钱箱门是否打开
        private bool isOpenCashBoxDoor;

        //机头门是否打开
        private bool isOpenMotorDoor;

        //是否正在发送数据
        public bool isSendData;

        //是否到关机时间
        private bool IsShutDown;

        //是否震动
        private bool isVibrationed;

        //钱箱是否移除
        private bool RemoveCashBox;

        //Socket封装，兑奖socket和MySocket
        private SocketClient sc;

        //数据编码区段
        public int segment;
        //偏移码，编码用
        public int shiftCode;

        //声音播放器
        private SoundPlayer simpleSound;

        //当前总票数
        private int sum;

        //关机的倒计时
        //异常断电，到计数5次，关机
        private int time5;
        //关机倒计时，180次后进入关机提示，到激活素120次
        private int timerShutdownCount;
        //关机倒计时
        private int ShutdownTimeCount = 120;

        //升级文件的xml路径
        private string upgradePath = "";

        //机头控制类
        private MotorOperate mo;
        //机头中最小的彩票面值
        private int MotorMixPrice;

        //红外传感器的倒计时
        private int leaveCount;

        //彩票编号
        public string[] LotteryIdArray = new String[8];
        //彩种图片控件数组
        public PictureBox[] LotteryTypePBoxArray = new PictureBox[8];
        //彩票剩余数量显示label数组
        public Label[] LotteryRemainLabArray = new Label[8];

        private Boolean IS_CHECK_RECEIVECOIN = false; //是否需要去检查硬币器
        private Boolean IS_CHECK_RECEIVECASH = false; //是否需要去检查纸币器

        //清空购物车的倒计时
        private int clearShoppingCartCount = 120;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowA(string lp1, string lp2);
        [DllImport("user32.dll")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int _value);
        [DllImport("winmm.dll")]
        public static extern bool sndPlaySound(string FileName, int fuSound);

        private static String SELL_TIPS = "请选择彩票，点击结算购买";

        public FormBuyLotteryMain()
        {
            InitializeComponent();

            this.mo = new MotorOperate();
            this.coin = new CoinClass();
            this.Gsn = new GetSecurityNumber();
            this.Gsn.OpenPort();
        }

        private void AddRejectTable()
        {
            CommonUtils.WriteExceptionInfo("判断表是否存在");
            if (!SQLiteHelper.CopyToList("select name from sqlite_master WHERE type = 'table'").Contains("tblRejectInfo"))
            {
                CommonUtils.WriteExceptionInfo("不存在表，将要生成tblRejectInfo");
                string sql = "CREATE TABLE 'tblRejectInfo'('id' INTEGER PRIMARY KEY,'RejectCount' INTEGER,'PayoutCount' INTEGER,'BillType' INTEGER,'Time' TEXT(12) DEFAULT '',UploadFlag TEXT(12) DEFAULT '');";
                SQLiteHelper.ExecuteSql(sql);
            }
        }

        //加密
        private byte[] AssemblyAgain(byte[] Date, byte[] PiecewiseContentData)
        {
            byte[] buffer2;
            try
            {
                byte[] array = new byte[Date.Length];
                array[0] = Date[0];
                array[1] = Date[1];
                array[2] = Date[2];
                array[3] = Date[3];
                array[4] = Date[4];
                array[5] = Date[5];
                PiecewiseContentData.CopyTo(array, 6);
                array[array.Length - 2] = GetBCC(array, array.Length - 2);
                array[array.Length - 1] = Date[Date.Length - 1];
                buffer2 = array;
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                throw exp;
            }
            return buffer2;
        }

        //二进制数组 to 16进制字符串
        public static string ByteArray2HexString(byte[] inp, int offset, int len)
        {
            string str2;
            try
            {
                string str = "";
                for (int i = 0; i < len; i++)
                {
                    str = str + Convert.ToString(inp[i + offset], 0x10).PadLeft(2, '0');
                }
                str2 = str;
            }
            catch
            {
                throw;
            }
            return str2;
        }

        //二进制 to 16进制
        public static string byteToHexStr(byte[] bytes)
        {
            string str2;
            try
            {
                string str = "";
                if (bytes != null)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        str = str + bytes[i].ToString("X2");
                    }
                }
                str2 = str;
            }
            catch
            {
                throw;
            }
            return str2;
        }

        //检查马达的状态
        private void CheckMotor()
        {
            for (int i = 1; i <= 8; i++)
            {
                if (SQLiteHelper.GetSingle("SELECT motorStatus FROM tblMotorInfo WHERE  MotorId=" + i).ToString() != "1")
                {
                    int cardLength = Convert.ToInt32(SQLiteHelper.GetSingle("select lotteryLength from tblMotorInfo where MotorId= " + i));
                    byte[] buffer = this.mo.CompositeQuery(i);
                    if ((buffer == null) || (buffer.Length < 10))
                    {
                        //机头状态返回异常,重试
                        byte[] buffer2 = this.mo.CompositeQuery(i);
                        if ((buffer2 == null) || (buffer2.Length < 10))
                        {
                            GlobalParmeters.MotorStatus[i - 1] = 1;
                            CommonUtils.WriteMootrInfo("程序启动，" + i + "号机头状态异常");
                            this.ini.WriteIniValue("配置", "机头" + i + "状态", "1");
                        }
                        else if (buffer2[2] == 0x55)
                        {
                            //物理硬件正常
                            GlobalParmeters.MotorStatus[i - 1] = 0;
                            this.ini.WriteIniValue("配置", "机头" + i + "状态", "0");
                            int num3 = (buffer2[7] * 0x80) + buffer2[8];
                            //机头的返回的票的数量和数据库中记录的票的数量不一致，需要人工处理
                            if (((num3 != cardLength) && !this.mo.SetCardLengthOperate(cardLength, i)) && !this.mo.SetCardLengthOperate(cardLength, i))
                            {
                                GlobalParmeters.MotorStatus[i - 1] = 1;
                                this.ini.WriteIniValue("配置", "机头" + i + "状态", "3");
                            }
                            //校验位不对
                            int num4 = buffer2[9] & 3;
                            if (num4 > 0)
                            {
                                this.ini.WriteIniValue("配置", "机头" + i + "状态", "2");
                                GlobalParmeters.MotorStatus[i - 1] = 1;
                            }
                        }
                    }
                    else if (buffer[2] == 0x55)
                    {
                        //同上
                        GlobalParmeters.MotorStatus[i - 1] = 0;
                        this.ini.WriteIniValue("配置", "机头" + i + "状态", "0");
                        int num5 = (buffer[7] * 0x80) + buffer[8];
                        if (((num5 != cardLength) && !this.mo.SetCardLengthOperate(cardLength, i)) && !this.mo.SetCardLengthOperate(cardLength, i))
                        {
                            GlobalParmeters.MotorStatus[i - 1] = 1;
                            this.ini.WriteIniValue("配置", "机头" + i + "状态", "3");
                        }

                        //校验位不对
                        int num6 = buffer[9] & 3;
                        if (num6 > 0)
                        {
                            this.ini.WriteIniValue("配置", "机头" + i + "状态", "2");
                            GlobalParmeters.MotorStatus[i - 1] = 1;
                        }
                    }
                }
                else
                {
                    //数据库中无此机头，置为不可用
                    GlobalParmeters.MotorStatus[i - 1] = 1;
                }
            }
        }

        /**
         * 检测兑奖Socket状态是否正常，会阻塞当前线程
         */
        private void checkSocket()
        {
            CommonUtils.showTime("socket是否连接：" + this.isConnected);

            //Socket未连接
            if (!this.isConnected)
            {
                this.sc = new SocketClient();
                //循环检查，心跳发现网络断开则立刻重连
                while (true)
                {
                    try
                    {
                        if (this.isConnected == false)
                        {
                            CommonUtils.showTime("准备打开socket");
                            if (this.sc.OpenDraw())
                            {
                                CommonUtils.showTime("打开socket成功");
                                this.isConnected = true;
                            }
                            else
                            {
                                CommonUtils.showTime("打开socket失败");
                                //Add by Bean.
                                Thread.Sleep(10 * 1000);
                                continue;
                            }
                        }

                        CommonUtils.showTime("checkSocket() 是否正在兑奖：isDuiJiang：" + this.isDuiJiang);
                        if (!this.isDuiJiang)
                        {
                            GlobalParmeters.heartbeatFlag = true;
                            //心跳检测兑奖Socket是否已连接
                            if (this.sc.IsSocketConnected(this.sc.DrawSocket))
                            {
                                this.isConnected = true;
                            }
                            else
                            {
                                this.isConnected = false;
                            }
                        }

                        //20秒重试一次
                        Thread.Sleep(20 * 1000);
                    }
                    catch (Exception exception)
                    {
                        CommonUtils.WriteExceptionInfo(exception);
                    }
                } //end while
            }
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
        
        private void FormBuyLotteryMain_Load(object sender, EventArgs e)
        {
            //设置窗体初始位置为：屏幕具中
            this.StartPosition = FormStartPosition.CenterScreen;
           
            //获取屏幕的尺寸
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);

            String str = rect.Width + "," + rect.Height + "," + this.Size.Width + "," + this.Size.Height;

            //把屏幕尺寸写入log
            CommonUtils.WriteExceptionInfo(str);

            //设置窗体的尺寸为屏幕尺寸
            this.Size = new Size(768, 1366);

            try
            {
                LotteryTypePBoxArray = new PictureBox[8] { this.picLotteryType1, this.picLotteryType2, this.picLotteryType3, this.picLotteryType4, this.picLotteryType5, this.picLotteryType6, this.picLotteryType7, this.picLotteryType8 };
                LotteryRemainLabArray = new Label[8] { this.lblLotteryRemain1, this.lblLotteryRemain2, this.lblLotteryRemain3, this.lblLotteryRemain4, this.lblLotteryRemain5, this.lblLotteryRemain6, this.lblLotteryRemain7, this.lblLotteryRemain8 };

                if (MachineContext.AppEnvMode == EnumAppEnvMode.TEST || MachineContext.RunMode == EnumRunMode.DEBUG)
                {
                    //用户发布前提醒
                    MessageBox.Show("当前是测试环境或调试模式");
                }
                else {
                    Cursor.Hide();
                }
                
                RuntimeLogUtils.WriteLog("读取配置信息...");

                RuntimeLogUtils.WriteLog("更新远程主机信息...");
                HostLoader.loadHostInfo();

                //TODO
                this.ini = new IniFile("D://advitise/1.ini");
                
                GlobalParmeters.machineNumber = this.ini.ReadIniValue("配置", "本机编号");
                string machSoftNo = "终端编号：" + GlobalParmeters.machineNumber;
                if (MachineContext.AppEnvMode == EnumAppEnvMode.TEST)
                {
                    machSoftNo += "(测试环境)";
                }
                this.lblMachsoftNumber.Text = machSoftNo;

                this.lblPhono.Text = "客服电话:" + this.ini.ReadIniValue("配置", "客服电话");

                RuntimeLogUtils.WriteLog("初始化机头彩种信息...");
                this.initDb();

                //获取后台服务器地址和端口

                GlobalParmeters.CashType = int.Parse(this.ini.ReadIniValue("配置", "纸币币种"));
                GlobalParmeters.CoinType = Convert.ToDouble(this.ini.ReadIniValue("配置", "硬币币种"));

                RuntimeLogUtils.WriteLog("读取现金信息...");
                GlobalParmeters.CoinCount = CommonUtils.ReadCoinCount();
                GlobalParmeters.CashCount = CommonUtils.ReadCashCount();

                GlobalParmeters.GotMoney = CommonUtils.ReadMoneyCount();
                GlobalParmeters.CashFaultCount = CommonUtils.ReadCashFaultCount();
                GlobalParmeters.CoinFaultCount = CommonUtils.ReadCoinFaultCount();

                if (GlobalParmeters.GotMoney > 0) {
                    this.isConsumption = true;
                    this.btnReturnMoney.Visible = true;
                }

                RuntimeLogUtils.WriteLog("硬币个数:" + GlobalParmeters.CoinCount + ",纸币张数:" + GlobalParmeters.CashCount + ",购买余额:" + GlobalParmeters.GotMoney);

                this.lblInputedMoney.Text = GlobalParmeters.GotMoney.ToString();


                GlobalParmeters.ServicePhoneNumber = this.ini.ReadIniValue("配置", "客服电话");
                GlobalParmeters.Drawphono = this.ini.ReadIniValue("配置", "兑奖网点电话");

                if (!this.ini.ValueExists("配置", "远程升级配置文件路径"))
                {
                    this.upgradePath = "D:/terminalupgrade/parameterSet.xml";
                    this.ini.WriteIniValue("配置", "远程升级配置文件路径", this.upgradePath);
                }
                else
                {
                    this.upgradePath = this.ini.ReadIniValue("配置", "远程升级配置文件路径");
                }
                
                this.ini.WriteIniValue("配置", "显示广告", "否");
                
                //微信服务器
                if (!this.ini.ValueExists("配置", "微信支付服务器"))
                {
                    this.ini.WriteIniValue("配置", "微信支付服务器", this.ini.ReadIniValue("配置", "服务器IP")); 
                }

                YTDSSTGenII.Service.Context.ApplicationContext.getInstance().WEIXIN_SERVER_IP = this.ini.ReadIniValue("配置", "微信支付服务器");

                //微信端口
                if (!this.ini.ValueExists("配置", "微信支付端口"))
                {
                    this.ini.WriteIniValue("配置", "微信支付端口", "6650");
                }
                YTDSSTGenII.Service.Context.ApplicationContext.getInstance().WEIXIN_SERVER_PORT = this.ini.ReadIniValue("配置", "微信支付端口");
                

                RuntimeLogUtils.WriteLog("加载机头彩种图片...");
                for (int i = 0; i < 8; i++)
                {
                    LotteryTypePBoxArray[i].BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[i] + "_8.png");
                }
                RuntimeLogUtils.WriteLog("加载机头彩种图片成功!");

                /**********************************新加配置-zlp,2016-10-08——begin****************************************/
                InitUtils.InitDB();//初始化数据库相关操作
                InitUtils.initContext();//初始化上下文
                /********************数据修正-第一个版本彩票出票状态和订单出票状态数据有问题，需要修正，可以在下一个版本去掉***************************/                                        //做数据修正操作
                InitUtils.correctData();
                /********************数据修正-第一个版本彩票出票状态和订单出票状态数据有问题，需要修正，可以在下一个版本去掉***************************/

                //请求二维码的线程 ReceiveCoin
                ThreadPool.QueueUserWorkItem(new WaitCallback(checkPayFormCommand));
                ThreadPool.QueueUserWorkItem(new WaitCallback(threadReceiveCoin));
                ThreadPool.QueueUserWorkItem(new WaitCallback(threadReceiveCash));
                //没有弹出框、不在结算中、不在兑奖中，购物车有票——两分钟内无操作，清空购物车
                ThreadPool.QueueUserWorkItem(new WaitCallback(clearShoppingCart));
                /**********************************新加配置-zlp,2016-10-08——end****************************************/

                this.timerLotteryCount.Enabled = true;
                this.timerInitSet.Enabled = true;
                this.timerShutDown.Enabled = true;
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                RuntimeLogUtils.WriteLog("初始化异常:" + exp.Message);
            }
        }

        /// <summary>
        /// 没有弹出框、不在结算中、不在兑奖中，购物车有票——两分钟内无操作，清空购物车
        /// </summary>
        /// <param name="state"></param>
        private void clearShoppingCart(object state)
        {
            while (true)
            {
                if (!YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM && UserContext.getInstance().getBuyLotteryNum() > 0)
                {
                    clearShoppingCartCount--;
                    if (clearShoppingCartCount == 0)
                    {
                        UserContext.getInstance().clearBuyInfo();
                        refreshMainInfo();
                        clearShoppingCartCount = 120;
                    }
                }
                else
                {
                    clearShoppingCartCount = 120;
                }               
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 检查纸币器线程
        /// </summary>
        /// <param name="state"></param>
        private void threadReceiveCash(object state) 
        {
            while (true)
            {
                if (IS_CHECK_RECEIVECASH)
                {
                    this.check_receivecash();
                }
               Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 检查纸币器操作方法
        /// </summary>
        private void check_receivecash()
        {
            try
            {
                int num = 0;
                //检查有几个马达不可用
                foreach (int num2 in GlobalParmeters.MotorStatus)
                {
                    num += num2;
                }

                //所有马达都不可用
                if (num == 8)
                {
                    //禁用硬币和纸币器
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        StopService();
                        GlobalParmeters.ShowMessage = "暂停服务";
                    }
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：所有机头状态异常");
                    return;
                }

                //总余票为0
                if (this.sum == 0)
                {
                    //禁用硬币和纸币器
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();

                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        StopService();
                        GlobalParmeters.ShowMessage = "暂停服务";
                    }
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：所有机头无票");
                    return;
                }

                //用户余额和待消费大于200
                if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) >= 200)
                {
                    //禁用硬币和纸币器
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：现金余额超过200元");
                    return;
                }

                //纸币器故障次数3次以后，或者到关机时间
                if ((GlobalParmeters.CashFaultCount >= 3) || GlobalParmeters.downtime)
                {
                    //禁用硬币和纸币器
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        //当前用户没有消费
                        StopService();
                        GlobalParmeters.ShowMessage = "暂停服务";
                    }
                    else
                    {
                        //还有用户正在操作
                    }

                    this.ini.WriteIniValue("配置", "纸币找零器状态", "1");
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：纸币找零器故障");
                    return;
                }

                //硬币器三次故障
                if (GlobalParmeters.CoinFaultCount >= 3)
                {
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        GlobalParmeters.ShowMessage = "暂停服务";
                        StopService();
                    }
                    else
                    {
                        //还有用户正在操作
                    }

                    this.ini.WriteIniValue("配置", "硬币找零器状态", "1");
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：硬币找零器故障");
                    return;
                }

                //远程关闭纸币器
                if (GlobalParmeters.LongRangeDisableCashBox)
                {
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        GlobalParmeters.ShowMessage = "暂停服务";
                        StopService();
                    }
                    else
                    {
                        //还有用户正在操作
                    }

                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：远程禁用纸币器");
                    return;
                }

                //纸币器状态
                if (GlobalParmeters.CashBoxFlag)
                {
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        GlobalParmeters.ShowMessage = "暂停服务";
                        StopService();
                    }
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：钞箱被取下");
                    return;
                }

                //纸币器和硬币器金额过低
                if ((GlobalParmeters.CashCount < GlobalParmeters.MinCashCount) || (GlobalParmeters.CoinCount < GlobalParmeters.MinCoinCount))
                {
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        GlobalParmeters.ShowMessage = "暂停服务";
                        StopService();
                    }
                    CommonUtils.WriteServiceSuspendedInfo(string.Concat(new object[] { "禁用所有纸币：退币箱纸币少于", GlobalParmeters.MinCashCount, "张或硬币小于", GlobalParmeters.MinCoinCount, "枚" }));
                    return;
                }

                //收币器业务正常
                //IS_CHECK_RECEIVECASH = false;

                //禁用输入标志位true，已禁用，则打开
                if (GlobalParmeters.ForbitInputFlag)
                {
                    CommonUtils.WriteServiceSuspendedInfo("启用所有纸币");
                    this.coin.UseOnePipe();
                    JCMBillAcceptorClass.UsefulAllBillType();
                    IS_CHECK_RECEIVECOIN = true;
                    GlobalParmeters.ForbitInputFlag = false;
                    
                    if (GlobalParmeters.GotMoney < this.MotorMixPrice)
                    {
                        GlobalParmeters.ShowMessage = SELL_TIPS;
                    }
                    else
                    {
                        GlobalParmeters.ShowMessage = SELL_TIPS;
                    }
                    //暂停服务的panel2
                    StartService();
                }

                //轮询纸币器
                int num3 = JCMBillAcceptorClass.Poll();

                //返回钱数
                if (num3 < 0)
                {
                    //状态错误，暂停，硬件出现问题
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    GlobalParmeters.ForbitInputFlag = true;
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) == 0)
                    {
                        GlobalParmeters.ShowMessage = "暂停服务";
                        StopService();
                    }
                    return;
                }
                //正确
                if (num3 > 0)
                {
                    this.isConsumption = false;
                    GlobalParmeters.GotMoney += num3;
                    UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                    CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                    SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblComeCash(BillMount,CashCount,Time,BillFlag,UploadFlag) values ('", num3, "',1,'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "',1,'false')" }));
                    if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                    {
                        GlobalParmeters.ShowMessage = SELL_TIPS;
                    }
                    //this.ini.WriteIniValue("配置", "是否全屏", "否");
                    //暂停检查硬件的状态
                    this.timerQuery.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException)
                {
                    //端口未打开，忽略这个错误
                    CommonUtils.WriteExceptionInfo("纸币收币器端口已经被关闭.");
                }
                else
                {
                    GlobalParmeters.ShowMessage = "暂停服务";
                    CommonUtils.WriteServiceSuspendedInfo("暂停服务：timerReceiveCash_Tick异常");
                    CommonUtils.WriteExceptionInfo(exception);
                    StopService();
                }
            }
        }

        private void threadReceiveCoin(object state)
        {
            while (true)
            {
                if (IS_CHECK_RECEIVECOIN)
                {
                    try
                    {
                        this.Poll();
                    }
                    catch (Exception exp)
                    {
                        if (exp is InvalidOperationException)
                        {
                            //端口未打开，忽略这个错误
                            CommonUtils.WriteExceptionInfo("硬币器端口已经被关闭.");
                        }
                    }
                }
                Thread.Sleep(500);
            }            
        }

        /// <summary>
        /// 不停的去检查是否有从支付界面传过来的操作指令并进行相关操作
        /// </summary>
        /// <param name="state"></param>
        private void checkPayFormCommand(object state)
        {
            while (true)
            {
                if (YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_IN_PAY_FORM) //当前在支付界面上
                {
                    if (YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_NEED_CHECK_MONEYBOX)
                    {
                        IS_CHECK_RECEIVECASH = true;
                        IS_CHECK_RECEIVECOIN = true;
                        //this.timerAdvertiseSwitch.Enabled = true;
                        //this.coin.UseOnePipe();
                    }
                    else
                    {
                        IS_CHECK_RECEIVECASH = false;
                        IS_CHECK_RECEIVECOIN = false;
                        //this.timerAdvertiseSwitch.Enabled = false;
                    }
                     
                }
                Thread.Sleep(100);
            }
        }

        private void FormBuyLotteryMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO 增加托盘程序
            this.hTray = FindWindowA("Shell_TrayWnd", string.Empty);
            if (this.hTray != null)
            {
                ShowWindow(this.hTray, 5);
            }

            Environment.Exit(0);
        }

        //异或校验 TODO
        public static byte GetBCC(byte[] temp, int len)
        {
            byte num = 0;
            for (int i = 0; i < len; i++)
            {
                num = (byte)(num ^ temp[i]);
            }
            return num;
        }

        /**
         * 根据分段获取变数
         */
        private static byte[] getBytesBySegment(byte[] bts, byte segment)
        {
            byte[] buffer2;
            try
            {
                byte[] buffer = new byte[bts.Length];
                int num = bts.Length / segment;
                for (int i = 0; i < segment; i++)
                {
                    int num3 = i * num;
                    int num4 = (num3 + num) - 1;
                    if (i == (segment - 1))
                    {
                        num4 = bts.Length - 1;
                    }
                    int num5 = (num4 - num3) + 1;
                    int num6 = ((num5 % 2) == 0) ? (num5 / 2) : ((num5 / 2) + 1);
                    for (int j = 0; j < num6; j++)
                    {
                        byte num8 = bts[num3 + j];
                        byte num9 = bts[num4 - j];
                        buffer[num3 + j] = num9;
                        buffer[num4 - j] = num8;
                    }
                }
                buffer2 = buffer;
            }
            catch
            {
                throw;
            }
            return buffer2;
        }

        //根据偏移码获取数据，每个字节加上一个偏移码
        private static byte[] getBytesByShiftCode(byte[] bts, byte shiftCode)
        {
            byte[] buffer2;
            try
            {
                byte[] buffer = new byte[bts.Length];
                for (int i = 0; i < bts.Length; i++)
                {
                    int num2 = bts[i] + shiftCode;
                    buffer[i] = (byte)num2;
                }
                buffer2 = buffer;
            }
            catch
            {
                throw;
            }
            return buffer2;
        }

        /**
         * 获取数据区，从第6个字节开始
         */
        private byte[] GetData(byte[] data)
        {
            byte[] buffer2;
            try
            {
                byte[] dst = new byte[data.Length - 8];
                Buffer.BlockCopy(data, 6, dst, 0, dst.Length);
                buffer2 = dst;
            }
            catch
            {
                throw;
            }
            return buffer2;
        }

        /**
         * 计算投入的硬币的数量
         */
        private static int GetEventCount(byte[] ReadData, int count)
        {
            if (count == 1)
            {
                count = ReadData[10];
                return count;
            }
            if (count == 2)
            {
                count = ReadData[10] + ReadData[12];
                return count;
            }
            if (count == 3)
            {
                count = (ReadData[10] + ReadData[12]) + ReadData[14];
                return count;
            }
            if (count == 4)
            {
                count = ((ReadData[10] + ReadData[12]) + ReadData[14]) + ReadData[0x10];
                return count;
            }
            count = (((ReadData[10] + ReadData[12]) + ReadData[14]) + ReadData[0x10]) + ReadData[0x12];

            return count;
        }

        private void initDb()
        {
            List<String> list = SQLiteHelper.CopyToList("SELECT tblLotteryInfo.LotteryId FROM tblLotteryInfo JOIN tblMotorInfo ON tblLotteryInfo.LotteryName=tblMotorInfo.LotteryName ORDER BY tblMotorInfo.MotorId");

            this.LotteryIdArray[0] = list[0];
            this.LotteryIdArray[1] = list[1];
            this.LotteryIdArray[2] = list[2];
            this.LotteryIdArray[3] = list[3];
            this.LotteryIdArray[4] = list[4];
            this.LotteryIdArray[5] = list[5];
            this.LotteryIdArray[6] = list[6];
            this.LotteryIdArray[7] = list[7];
            
            List<string> list2 = SQLiteHelper.CopyToList("SELECT LotteryName FROM tblMotorInfo");
            
            this.lblName1.Text = list2[0];
            this.lblName2.Text = list2[1];
            this.lblName3.Text = list2[2];
            this.lblName4.Text = list2[3];
            this.lblName5.Text = list2[4];
            this.lblName6.Text = list2[5];
            this.lblName7.Text = list2[6];
            this.lblName8.Text = list2[7];

            this.MotorMixPrice = Convert.ToInt32(SQLiteHelper.GetSingle("SELECT min(Cast(LotteryPrice AS INSTEAD)) FROM tblMotorInfo"));
        }

        /*
         * 纸币器找零
         * @param int CashMoney 需要找零的纸币金额
         */
        private void payoutCash(int CashMoney)
        {
            try
            {
                //吐钱成功次数
                int num = 0;
                //循环次数
                int num2 = 0;

                int[] numArray = new int[2];
                //纸币金额吐钱，循环正常次数1-5次
                while ((num < (CashMoney / GlobalParmeters.CashType)) && (num2 < 6))
                {
                    num2++;
                    Thread.Sleep(200);

                    //计算需要吐钱的次数 金额/币种。如果大于20次，先吐20次纸币
                    int cashCount = GlobalParmeters.GotMoney / GlobalParmeters.CashType;
                    if (cashCount >= 20)
                    {
                        numArray = CashClass.Execute("20");
                    }
                    else if (cashCount > 0 && cashCount < 20)
                    {
                        numArray = CashClass.Execute(cashCount.ToString());
                    }
                    num += numArray[0];
                    //计算已经成功扣掉的钱，从总额里面减掉
                    GlobalParmeters.GotMoney -= numArray[0] * GlobalParmeters.CashType;
                    UserContext.getInstance().Balance = GlobalParmeters.GotMoney;

                    //记录本次循环吐钱的记录
                    if (numArray[1] > 0)
                    {
                        SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblRejectInfo(RejectCount,PayoutCount,BillType,Time,UploadFlag) VALUES(", numArray[1], ",", CashMoney / GlobalParmeters.CashType, ",", GlobalParmeters.CashType, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false')" }));
                    }
                }

                CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                //修改总金额，检查各设备是否继续可用
                //退币次数大于0
                if (num > 0)
                {
                    //扣减纸币总金额
                    GlobalParmeters.CashCount -= num;
                    CommonUtils.WriteCashCount(GlobalParmeters.CashCount);
                    SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblPayout(CashType,CoinType,CashId,CashCount,CoinCount,time,UploadFlag) VALUES (", GlobalParmeters.CashType, ",", GlobalParmeters.CoinType, ",(select Max(Id) from(select tblComeCash.ID as Id from tblComeCash Union select tbllotteryprize.id as Id from tbllotteryprize ) t),", num, ",", 0, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false')" }));

                    //吐钱已结束
                    if (GlobalParmeters.GotMoney == 0)
                    {
                        GlobalParmeters.CashFaultCount = 0;
                        CommonUtils.WriteCashFaultCount(GlobalParmeters.CashFaultCount);

                        this.ini.WriteIniValue("配置", "纸币找零器状态", "0");
                        //提示用户取走纸币
                        sndPlaySound(this.FileAddress + "/yuyin/GetyourCash.WAV", 1);
                        GlobalParmeters.ShowMessage = "请取走纸币";
                    }
                    //还有余额，继续吐硬币
                    else if ((GlobalParmeters.GotMoney > 0) && (GlobalParmeters.GotMoney < GlobalParmeters.CashType))
                    {
                        GlobalParmeters.CashFaultCount = 0;
                        CommonUtils.WriteCashFaultCount(GlobalParmeters.CashFaultCount);
                        this.ini.WriteIniValue("配置", "纸币找零器状态", "0");
                        //继续找零硬币
                        payoutCoin(GlobalParmeters.GotMoney, true);
                    }
                    //给用户多退钱了，公司亏本
                    else if (GlobalParmeters.GotMoney < 0)
                    {
                        CommonUtils.WritePayoutInfo("退币器出现问题,返回成功数可能大于要退的张数");
                        GlobalParmeters.GotMoney = 0;
                        UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                        CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                        GlobalParmeters.ShowMessage = "退币中出现问题,请联系客服";
                    }
                    //退币的循环超过5次，仍不能完成退避，说明退币器出现问题，或者正常，用户还有余额
                    else if (GlobalParmeters.GotMoney > GlobalParmeters.CashType)
                    {
                        GlobalParmeters.CashFaultCount++;
                        CommonUtils.WriteCashFaultCount(GlobalParmeters.CashFaultCount);
                        GlobalParmeters.ShowMessage = "请取走纸币后，继续购彩或联系客服";
                        sndPlaySound(this.FileAddress + "/yuyin/GetyourCashAndGoonBuy.WAV", 1);
                    }

                    ControlPanel.SendCommand(2, 1);
                    this.timerInitInfoMessage.Enabled = true;
                }
                else
                {
                    GlobalParmeters.CashFaultCount++;
                    CommonUtils.WriteCashFaultCount(GlobalParmeters.CashFaultCount);
                    GlobalParmeters.ShowMessage = "退币失败，请联系客服或继续购彩";
                    this.timerInitInfoMessage.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                throw exp;
            }
        }

        /**
         * 硬币找零 
         * @param int CointMoneyCount 需要找零的硬币数量
         * @param bool IsPayoutCashSuccess 是否找零成功
         */
        private void payoutCoin(int CoinMoneyCount, bool IsPayoutCashSuccess)
        {
            try
            {
                //支持成功的钱数
                int num = 0;
                //重试次数
                int num2 = 0;
                //单次支付成功的金额
                int num3 = 0;
                //重试次数3次
                while ((num < CoinMoneyCount) && (num2) < 3)
                {
                    num2++;
                    Thread.Sleep(100);

                    num3 = coin.CoinPayout(GlobalParmeters.GotMoney);
                    num += num3;
                    GlobalParmeters.GotMoney -= num3;
                    UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                }

                CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                //硬币找零后，扣减全局硬币的总金额
                if (num > 0)
                {
                    GlobalParmeters.CoinCount -= num;
                    CommonUtils.WriteCoinCount(GlobalParmeters.CoinCount);
                    SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblPayout(CashType,CoinType,CashId,CashCount,CoinCount,time,UploadFlag) VALUES (", GlobalParmeters.CashType, ",", GlobalParmeters.CoinType, ",(select Max(Id) from(select tblComeCash.ID as Id from tblComeCash Union select tbllotteryprize.id as Id from tbllotteryprize ) t),0,", num, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false')" }));

                    //已经完成找零
                    if (GlobalParmeters.GotMoney == 0)
                    {
                        GlobalParmeters.CoinFaultCount = 0;
                        CommonUtils.WriteCoinFaultCount(GlobalParmeters.CoinFaultCount);
                        this.ini.WriteIniValue("配置", "硬币找零器状态", "0");
                        if (IsPayoutCashSuccess)
                        {
                            GlobalParmeters.ShowMessage = "请取走纸币和硬币";
                            sndPlaySound(this.FileAddress + "/yuyin/GetyourCashAndCoin.WAV", 1);
                        }
                        else
                        {
                            GlobalParmeters.ShowMessage = "请取走硬币";
                            sndPlaySound(this.FileAddress + "/yuyin/GetyourCoin.WAV", 1);
                        }
                    }
                    //找多
                    else if (GlobalParmeters.GotMoney < 0)
                    {
                        CommonUtils.WritePayoutInfo("退硬币出现问题,返回成功数可能大于要退的个数");
                        GlobalParmeters.GotMoney = 0;
                        UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                        CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                        GlobalParmeters.ShowMessage = "退币中出现问题,请联系客服";
                    }
                    //用户还有余额
                    else if (GlobalParmeters.GotMoney > GlobalParmeters.CoinType)
                    {
                        GlobalParmeters.CoinFaultCount++;
                        CommonUtils.WriteCoinFaultCount(GlobalParmeters.CoinFaultCount);
                        //纸币正常
                        if (IsPayoutCashSuccess)
                        {
                            GlobalParmeters.ShowMessage = "请取走纸币和硬币后，继续购彩";
                            sndPlaySound(this.FileAddress + "/yuyin/GetyourCashAndCoinGoonBuy.WAV", 1);
                        }
                        else
                        {
                            GlobalParmeters.ShowMessage = "请取走硬币后，继续购彩或联系客服";
                            sndPlaySound(this.FileAddress + "/yuyin/GetyourCoinAndGoonBuy.WAV", 1);
                            this.coin.TestHOPPER();
                        }
                    }
                    ControlPanel.SendCommand(3, 1);
                    this.timerInitInfoMessage.Enabled = true;
                }
                //硬币找零失败
                else
                {
                    GlobalParmeters.CoinFaultCount++;
                    CommonUtils.WriteCoinFaultCount(GlobalParmeters.CoinFaultCount);
                    this.coin.TestHOPPER();
                    //找纸币成功
                    if (IsPayoutCashSuccess)
                    {
                        GlobalParmeters.ShowMessage = "请取走纸币后，继续购彩或联系客服";
                        sndPlaySound(this.FileAddress + "/yuyin/GetyourCashAndGoonBuy.WAV", 1);
                    }
                    //都失败
                    else
                    {
                        GlobalParmeters.ShowMessage = "退币失败,请联系客服或继续购彩";
                    }
                    this.timerInitInfoMessage.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                throw exp;
            }
        }

        /**
         * 扫描硬币收币器
         */
        private void Poll()
        {
            try
            {
                this.coin.OpenPort();
                CommonUtils.WriteReceiveCoinInfo("Poll Start");

                //重置缓冲区
                this.coin.MyPort.DiscardOutBuffer();
                this.coin.MyPort.DiscardInBuffer();

                //指令，硬币器的状态
                byte[] buffer = new byte[] { 2, 0, 1, 0xe5, 0x18 };
                this.coin.MyPort.Write(buffer, 0, buffer.Length);

                CommonUtils.WriteReceiveCoinInfo("Send " + CommonUtils.ByteArray2HexString(buffer, 0, buffer.Length));
                //等待250ms
                for (int i = 0; i < 50; i++)
                {
                    if (this.coin.MyPort.BytesToRead > 0)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }

                if (this.coin.MyPort.BytesToRead < 1)
                {
                    CommonUtils.WriteReceiveCoinInfo("无数据返回");
                    GlobalParmeters.CashAcceptorStatus[1] = 1;
                    this.ini.WriteIniValue("配置", "硬币器状态", "1");
                    this.coin.ClosePort();
                }
                else
                {
                    GlobalParmeters.CashAcceptorStatus[1] = 0;
                    //长度不够，继续等待
                    if (this.coin.MyPort.BytesToRead < 0x15)
                    {
                        Thread.Sleep(100);
                    }

                    int bytesToRead = this.coin.MyPort.BytesToRead;
                    byte[] buffer2 = new byte[bytesToRead];
                    this.coin.MyPort.Read(buffer2, 0, buffer2.Length);
                    if (buffer2 != null)
                    {
                        CommonUtils.WriteReceiveCoinInfo("Read " + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                        //解析返回值的状态

                        //校验
                        byte num3 = 0;
                        for (int j = 0; j < (buffer2.Length - 1); j++)
                        {
                            num3 = (byte)(num3 + buffer2[j]);
                        }

                        byte num5 = (byte)((0xff - num3) + 1);
                        //OK
                        if (num5 == buffer2[buffer2.Length - 1])
                        {
                            CommonUtils.WriteReceiveCoinInfo("Read" + CommonUtils.ByteArray2HexString(buffer2, 0, buffer2.Length));
                            if (buffer2[9] != this.coin.EventCount)
                            {
                                if (buffer2[9] == 0)
                                {
                                    //第10个字节
                                    this.coin.EventCount = 0;
                                }
                                else if (buffer2[9] <= this.coin.EventCount)
                                {
                                    //计算硬币箱硬币的个数
                                    int count = (buffer2[9] + 0xff) - this.coin.EventCount;
                                    this.coin.EventCount = buffer2[9];
                                    //???
                                    count = GetEventCount(buffer2, count);
                                    if (count > 0)
                                    {
                                        this.ini.WriteIniValue("配置", "硬币器状态", "0");
                                        GlobalParmeters.CoinCount += count;
                                        CommonUtils.WriteCoinCount(GlobalParmeters.CoinCount);
                                        GlobalParmeters.GotMoney += count;
                                        UserContext.getInstance().Balance = GlobalParmeters.GotMoney;

                                        CommonUtils.WriteMoney(GlobalParmeters.GotMoney);

                                        for (int k = 1; k <= count; k++)
                                        {
                                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblComeCash(BillMount,CashCount,Time,BillFlag,UploadFlag) values ('", 1, "',", 1, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "',0,'false')" }));
                                        }
                                        if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                                        {
                                            GlobalParmeters.ShowMessage = SELL_TIPS;
                                        }
                                    }
                                }
                                else if (buffer2[9] > this.coin.EventCount)
                                {
                                    if (GlobalParmeters.GotMoney == 0)
                                    {
                                        this.ini.WriteIniValue("配置", "是否全屏", "否");
                                        this.timerQuery.Enabled = false;
                                    }

                                    int eventCount = buffer2[9] - this.coin.EventCount;
                                    this.coin.EventCount = buffer2[9];
                                    eventCount = GetEventCount(buffer2, eventCount);
                                    if (eventCount > 0)
                                    {
                                        this.ini.WriteIniValue("配置", "硬币器状态", "0");
                                        GlobalParmeters.CoinCount += eventCount;
                                        this.isConsumption = false;
                                        CommonUtils.WriteCoinCount(GlobalParmeters.CoinCount);
                                        GlobalParmeters.GotMoney += eventCount;
                                        UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                                        CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
                                        for (int m = 1; m <= eventCount; m++)
                                        {
                                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblComeCash(BillMount,CashCount,Time,BillFlag,UploadFlag) values ('", 1, "',", 1, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "',0,'false')" }));
                                        }
                                        if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                                        {
                                            GlobalParmeters.ShowMessage = SELL_TIPS;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }

        /**
         * 请求服务器是否可以开始兑奖
         **/
        private void QueryDrawInfo(string time)
        {
            try
            {
                //够造兑奖的请求协议头
                //0-5字节
                byte[] array = new byte[0x33]; //51 byte
                array[0] = 0x7e;
                array[1] = 1;

                byte[] bytes = BitConverter.GetBytes(0x33);
                array[2] = bytes[1];
                array[3] = bytes[0];

                Random random = new Random();
                this.shiftCode = random.Next(1, 0x80);
                this.segment = random.Next(1, 0x80);

                array[4] = (byte)this.shiftCode;
                array[5] = (byte)this.segment;

                Encoding encoding = Encoding.GetEncoding("GB18030");
                //6-19字节
                GlobalParmeters.machineNumber = GlobalParmeters.machineNumber.PadLeft(13, '0');
                byte[] buffer3 = encoding.GetBytes(GlobalParmeters.machineNumber);
                SocketClient.WriteCheckExcetionlog(GlobalParmeters.machineNumber);
                buffer3.CopyTo(array, 6);

                //兑奖的类型
                array[0x13] = (byte)GlobalParmeters.prizeType;
                if (array[0x13] == 1)
                {
                    encoding.GetBytes(GlobalParmeters.SecurityNumber).CopyTo(array, 20);
                }
                else
                {
                    encoding.GetBytes(GlobalParmeters.LotterySerialNumber).CopyTo(array, 20);
                    encoding.GetBytes(GlobalParmeters.SecurityNumber).CopyTo(array, 0x26);
                }

                array[0x31] = GetBCC(array, 0x31);
                //结束位
                array[50] = 0xed;

                //数据编码
                byte[] piecewiseContentData = getBytesBySegment(getBytesByShiftCode(this.GetData(array), array[4]), array[5]);
                byte[] inp = this.AssemblyAgain(array, piecewiseContentData);
                SocketClient.WriteCheckExcetionlog(ByteArray2HexString(inp, 0, inp.Length));

                //发送兑奖请求
                byte[] buffer9 = this.sc.SendDrawData(inp);
                if (buffer9 != null)
                {
                    SocketClient.WriteCheckExcetionlog(ByteArray2HexString(buffer9, 0, buffer9.Length));

                    //有效的返回数据
                    if (((buffer9.Length > 0x24) && (buffer9[1] == 0x81)) && (buffer9[buffer9.Length - 2] == GetBCC(buffer9, buffer9.Length - 2)))
                    {
                        //解码
                        byte[] buffer10 = getBytesBySegment(getBytesByShiftCode(this.GetData(buffer9), (byte)(buffer9[4] * -1)), buffer9[5]);
                        byte[] buffer11 = this.AssemblyAgain(buffer9, buffer10);
                        //解码之后
                        if (buffer11[0x24] == 1)
                        {
                            //彩票已兑奖
                            CommonUtils.showTime("兑奖结束---此彩票已兑过奖了");
                            GlobalParmeters.ShowMessage = "此彩票已兑过奖了";
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','0',1,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'true','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                            this.timerInitInfoMessage.Enabled = true;
                        }
                        else if (buffer11[0x24] == 2)
                        {
                            //彩票未中奖
                            CommonUtils.showTime("兑奖结束---未中奖");
                            GlobalParmeters.ShowMessage = "您未中奖，请继续努力";
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','0',2,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'true','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                            this.timerInitInfoMessage.Enabled = true;
                        }
                        else if (buffer11[0x24] == 3)
                        {
                            //金额大？只能到体彩中心兑奖
                            CommonUtils.showTime("兑奖结束---请到体彩中心兑奖");
                            GlobalParmeters.ShowMessage = "请到体彩中心兑奖";
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','0',3,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'true','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                            this.timerInitInfoMessage.Enabled = true;
                        }
                        else if (buffer11[0x24] == 0)
                        {
                            //彩票验证合法，请求兑奖
                            CommonUtils.showTime("开始请求兑奖");
                            this.requestDraw(time);
                        }
                        else if (buffer11[0x24] == 6)
                        {
                            //服务器超时
                            CommonUtils.showTime("兑奖结束---服务器返回超时");
                            SocketClient.WriteCheckExcetionlog("兑奖信息查询，服务器返回超时");
                            GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                        else if (buffer11[0x24] == 4)
                        {
                            //还在处理中，继续请求
                            CommonUtils.showTime("正在处理.....");
                            this.QueryDrawInfo(time);
                        }
                        else if (buffer11[0x24] == 7)
                        {
                            //不是本省的票
                            CommonUtils.showTime("兑奖结束---异地票");
                            SocketClient.WriteCheckExcetionlog("兑奖信息查询，服务器返回异地票");
                            GlobalParmeters.ShowMessage = "对不起,本机不支持外地票种";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                        else
                        {
                            //保安区无效
                            CommonUtils.showTime("兑奖结束---服务器拒绝兑奖或保安区无效");
                            SocketClient.WriteCheckExcetionlog("兑奖信息查询，服务器拒绝兑奖或保安区无效");
                            GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                    else
                    {
                        //数据返回过程中验证不正确，可能被篡改
                        CommonUtils.showTime("兑奖结束---服务器返回数据不正确");
                        SocketClient.WriteCheckExcetionlog("兑奖信息查询，返回数据不正确");
                        GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                        this.timerInitInfoMessage.Enabled = true;
                    }
                }
                else
                {
                    //服务器没有返回数据
                    CommonUtils.showTime("兑奖结束---服务器返回为空");
                    SocketClient.WriteCheckExcetionlog("兑奖信息查询，返回为空");
                    GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                    this.timerInitInfoMessage.Enabled = true;
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                throw exp;
            }

        }

        /**
        * 请求兑奖信息
        */
        private void requestDraw(string time)
        {
            try
            {
                //构造请求兑奖
                //协议头，编码
                byte[] array = new byte[0x41];
                array[0] = 0x7e;
                array[1] = 2;
                byte[] bytes = BitConverter.GetBytes(0x41);
                array[2] = bytes[1];
                array[3] = bytes[0];
                Random random = new Random();
                this.shiftCode = random.Next(1, 0x80);
                this.segment = random.Next(1, 0x80);
                array[4] = (byte)this.shiftCode;
                array[5] = (byte)this.segment;

                //机器编码
                Encoding encoding = Encoding.GetEncoding("GB18030");
                GlobalParmeters.machineNumber = GlobalParmeters.machineNumber.PadLeft(13, '0');
                encoding.GetBytes(GlobalParmeters.machineNumber).CopyTo(array, 6);

                //兑奖类型
                array[0x13] = (byte)GlobalParmeters.prizeType;
                if (array[0x13] == 1)
                {
                    encoding.GetBytes(GlobalParmeters.SecurityNumber).CopyTo(array, 20);
                }
                else
                {
                    encoding.GetBytes(GlobalParmeters.LotterySerialNumber).CopyTo(array, 20);
                    encoding.GetBytes(GlobalParmeters.SecurityNumber).CopyTo(array, 0x26);
                }

                //兑奖序列号
                byte[] buffer7 = BitConverter.GetBytes(GlobalParmeters.DrawSerialNumbe);
                //高低字节反序
                array[0x31] = buffer7[3];
                array[50] = buffer7[2];
                array[0x33] = buffer7[1];
                array[0x34] = buffer7[0];

                //兑奖时间
                string str = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", new CultureInfo("zh-CN", true), DateTimeStyles.AllowInnerWhite).ToString("yyyyMMddHHmmss").ToString();
                array[0x35] = (byte)int.Parse(str.Substring(2, 2));
                array[0x36] = (byte)int.Parse(str.Substring(4, 2));
                array[0x37] = (byte)int.Parse(str.Substring(6, 2));
                array[0x38] = (byte)int.Parse(str.Substring(8, 2));
                array[0x39] = (byte)int.Parse(str.Substring(10, 2));
                array[0x3a] = (byte)int.Parse(str.Substring(12, 2));

                //硬币数量???
                int coinCount = GlobalParmeters.CoinCount;
                if (coinCount > 9)
                {
                    coinCount = 9;
                }
                //可以找零的钱数大于奖品的金额，才可以兑奖
                byte[] buffer8 = BitConverter.GetBytes((int)(((GlobalParmeters.CashCount * GlobalParmeters.CashType) + coinCount) - GlobalParmeters.GotMoney));
                array[0x3b] = buffer8[3];
                array[60] = buffer8[2];
                array[0x3d] = buffer8[1];
                array[0x3e] = buffer8[0];
                array[0x3f] = GetBCC(array, 0x3f);
                //结束标志位
                array[0x40] = 0xed;

                //编码
                byte[] piecewiseContentData = getBytesBySegment(getBytesByShiftCode(this.GetData(array), array[4]), array[5]);
                byte[] inp = this.AssemblyAgain(array, piecewiseContentData);
                SocketClient.WriteCheckExcetionlog(ByteArray2HexString(inp, 0, inp.Length));

                //请求兑奖
                byte[] buffer11 = this.sc.SendDrawData(inp);
                if (buffer11 != null)
                {
                    SocketClient.WriteCheckExcetionlog(ByteArray2HexString(buffer11, 0, buffer11.Length));
                    //合法请求
                    if (((buffer11.Length > 0x27) && (buffer11[1] == 130)) && (buffer11[buffer11.Length - 2] == GetBCC(buffer11, buffer11.Length - 2)))
                    {
                        byte[] buffer12 = getBytesBySegment(getBytesByShiftCode(this.GetData(buffer11), (byte)(buffer11[4] * -1)), buffer11[5]);
                        byte[] buffer13 = this.AssemblyAgain(buffer11, buffer12);

                        if (buffer13[0x24] == 1)
                        {
                            //服务器接收兑奖信息，准备二次扫码
                            GlobalParmeters.ShowMessage = "正在检测彩票,请放回或微调彩票位置";
                            GlobalParmeters.DrawMoney = Convert.ToInt32(byteToHexStr(new byte[] { buffer13[0x25], buffer13[0x26], buffer13[0x27], buffer13[40] }), 0x10);
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','", GlobalParmeters.DrawMoney.ToString(), "',99,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'true','", GlobalParmeters.LotterySerialNumber, "','true')" }));
                            CommonUtils.showTime("服务器已同意兑奖，准备再次扫码");
                            GlobalParmeters.isScanCoded = true;
                            this.timerReadSecurityLottery.Enabled = true;
                        }
                        else if (buffer13[0x24] == 2)
                        {
                            //服务器拒绝兑奖
                            SocketClient.WriteCheckExcetionlog("请求兑奖，服务器不同意兑奖");
                            GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                            this.timerInitInfoMessage.Enabled = true;
                            CommonUtils.showTime("服务器已返回，兑奖失败");
                        }
                        else if (buffer13[0x24] == 3)
                        {
                            //本机零钱不足，不让兑奖
                            CommonUtils.showTime("服务器已返回，本机零钱不足");
                            SocketClient.WriteCheckExcetionlog("请求兑奖，本机零钱不足");
                            GlobalParmeters.ShowMessage = "本机零钱不足,请电询" + GlobalParmeters.Drawphono;
                            this.lblMessage.Text = GlobalParmeters.ShowMessage;
                            Application.DoEvents();
                            Thread.Sleep(0x1388);
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                    else
                    {
                        //协议内容不正确
                        CommonUtils.showTime("服务器已返回，码不正确");
                        SocketClient.WriteCheckExcetionlog("请求兑奖，服务器返回错误");
                        GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                        this.timerInitInfoMessage.Enabled = true;
                        SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','", 0, "',0,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'false','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                    }
                }
                else
                {
                    //服务器未返回兑奖请求
                    CommonUtils.showTime("服务器未返回兑奖请求");
                    SocketClient.WriteCheckExcetionlog("请求兑奖，服务器返回为空");
                    SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','", 0, "',0,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'false','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                    GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                    this.timerInitInfoMessage.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.showTime("请求兑奖出现异常：" + exception.ToString());
                SocketClient.WriteCheckExcetionlog("请求兑奖，服务器返回为空");
                SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','", 0, "',0,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'false','", GlobalParmeters.LotterySerialNumber, "','false')" }));
                SocketClient.WriteCheckExcetionlog("请求兑奖异常:" + exception.Message);
                GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                this.timerInitInfoMessage.Enabled = true;
            }
        }

        /**
         * 显示兑奖信息
         */
        private void ShowPrizeInfo()
        {
            sndPlaySound(this.FileAddress + "/yuyin/LotteryPrize.WAV", 1);
            this.isConsumption = true;
            int num = Convert.ToInt32(SQLiteHelper.GetSingle("SELECT prizeQuota FROM tbllotteryprize WHERE SecurityNumber='" + GlobalParmeters.SecurityNumber + "'"));
            SQLiteHelper.ExecuteSql("UPDATE tbllotteryprize SET prizeDrawFlag=1,Uploadflag='false',GetCashTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE SecurityNumber='" + GlobalParmeters.SecurityNumber + "'");
            GlobalParmeters.GotMoney += num;
            UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
            CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
            GlobalParmeters.ShowMessage = "恭喜您中得彩金" + num + "元";
            CommonUtils.showTime("兑奖完成");
            try
            {
                //打孔
                PrizePunch.Punch();
                Thread.Sleep(100);
                ControlPanel.SendCommand(5, 1);
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }

            this.timerInitInfoMessage.Enabled = true;
        }

        /**
         * 查询马达状态 
         * @param int p 第几个机头，从1开始
         * @param int p_2 机头余票
         */
        private bool QueryMotorStatus(int p, int p_2)
        {
            try
            {
                CommonUtils.WritePayLogInfo("开始检查机头:p="+p.ToString()+",p_2="+p_2.ToString());
                byte[] buffer = this.mo.CompositeQuery(p);
                if ((buffer == null) || (buffer.Length < 10))
                {
                    CommonUtils.WritePayLogInfo("机头出现故障，没有正确返回数据");
                    //机头出现故障，没有正确返回数据
                    GlobalParmeters.ShowMessage = "此票已售完,请选择其他彩票";
                    this.timerInitInfoMessage.Enabled = true;
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    this.simpleSound = new SoundPlayer(this.FileAddress + "/yuyin/lotteryHasSaled.WAV");
                    this.simpleSound.Play();
                    this.ini.WriteIniValue("配置", "机头" + p + "状态", "1");
                    return false;
                }

                //查询机头上的票的数量
                FormBuyLotteryMain.num = Convert.ToInt32(SQLiteHelper.GetSingle("select LotteryCount from tblMotorInfo where MotorId =" + p));

                //无票
                if (FormBuyLotteryMain.num == 0)
                {
                    CommonUtils.WritePayLogInfo("机头无票1670");
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    GlobalParmeters.ShowMessage = "此票已售完，请选择其他彩票";
                    this.timerInitInfoMessage.Enabled = true;
                    this.simpleSound = new SoundPlayer(this.FileAddress + "/yuyin/lotteryHasSaled.WAV");
                    this.simpleSound.Play();
                    return false;
                }


                if (buffer[2] == 0x55)
                {
                    GlobalParmeters.MotorStatus[p - 1] = 0;

                    //?????
                    int num = (buffer[7] * 0x80) + buffer[8];
                    //设置机头的余票
                    if ((num != p_2) && !this.mo.SetCardLengthOperate(p_2, p))
                    {
                        CommonUtils.WritePayLogInfo("设置机头余票异常1688");
                        //机头出现问题
                        GlobalParmeters.ShowMessage = "此票已售完，请选择其他彩票";
                        this.timerInitInfoMessage.Enabled = true;
                        GlobalParmeters.MotorStatus[p - 1] = 1;
                        this.simpleSound = new SoundPlayer(this.FileAddress + "/yuyin/lotteryHasSaled.WAV");
                        this.simpleSound.Play();

                        return false;
                    }

                    int num2 = buffer[9] & 3;
                    if (num2 > 0)
                    {
                        //机头出现问题
                        GlobalParmeters.MotorStatus[p - 1] = 1;
                        GlobalParmeters.ShowMessage = "此票已售完，请选择其他彩票";
                        this.timerInitInfoMessage.Enabled = true;
                        this.simpleSound = new SoundPlayer(this.FileAddress + "/yuyin/lotteryHasSaled.WAV");
                        this.simpleSound.Play();
                        this.ini.WriteIniValue("配置", "机头" + p + "状态", "2");
                        return false;
                    }
                }
                else if (buffer[6] == 6)
                {
                    CommonUtils.WritePayLogInfo("机头出现问题buffer[6] == 6");
                    //机头出现问题
                    GlobalParmeters.ShowMessage = "此票已售完，请选择其他彩票";
                    this.timerInitInfoMessage.Enabled = true;
                    GlobalParmeters.MotorStatus[p - 1] = 1;
                    this.simpleSound = new SoundPlayer(this.FileAddress + "/yuyin/lotteryHasSaled.WAV");
                    this.simpleSound.Play();
                    this.ini.WriteIniValue("配置", "机头" + p + "状态", "3");
                    return false;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                CommonUtils.WritePayLogInfo("检查机头状态异常");
                return false;
            }

            this.ini.WriteIniValue("配置", "机头" + p + "状态", "0");

            return true;
        }

        /**
         * 用来升级的配置文件
         */
        private string ReadAndWriteXml(string type)
        {
            try
            {
                string upgradePath = this.upgradePath;
                XmlDocument document = new XmlDocument();
                document.Load(upgradePath);
                XmlNodeList elementsByTagName = document.GetElementsByTagName("item");
                for (int i = 0; i < elementsByTagName.Count; i++)
                {
                    XmlAttribute attribute = elementsByTagName[i].Attributes["key"];
                    if (attribute.Value == "isRestart")
                    {
                        attribute = elementsByTagName[i].Attributes["value"];
                        if (type == "read")
                        {
                            return attribute.Value;
                        }
                        attribute.Value = "0";
                        document.Save(upgradePath);
                        return "true";
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return "0";
        }

        private void toolStripMenuItemSysteManager_Click(object sender, EventArgs e)
        {
            //进入系统管理

            IS_CHECK_RECEIVECOIN = false;
            IS_CHECK_RECEIVECASH = false;

            this.ini.WriteIniValue("配置", "显示广告", "否");
            this.ini.WriteIniValue("配置", "是否全屏", "否");

            this.timerQuery.Enabled = false;
            this.timerShutDown.Enabled = false;
            this.timerReadSecurityLottery.Enabled = false;
            this.timerShowMessage.Enabled = false;

            this.timerAdvertiseSwitch.Enabled = false;

            try
            {
                JCMBillAcceptorClass.ClosePort();
                JCMBillAcceptorClass.MyPort = null;
                
            }
            catch (Exception exp) {
                CommonUtils.WriteExceptionInfo(exp);
            }

            try
            {
                ControlPanel.ClosePort();
                ControlPanel.MyPort = null;
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }


            try
            {
                CashClass.ClosePort();
                CashClass.MyPort = null;
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }

            try
            {
                this.coin.ClosePort();
                this.coin = null;
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
            
            new FormLogin().ShowDialog();

            //硬币收币器
            try
            {
                this.coin = new CoinClass();
            }
            catch (Exception) { }

            //重启纸币器
            JCMBillAcceptorClass.ResetJCMBillAcceptor();
            
            if (GlobalParmeters.GotMoney == 0)
            {
                this.timerQuery.Enabled = true;
            }
            
            this.timerShutDown.Enabled = true;
            this.timerShowMessage.Enabled = true;
            this.timerAdvertiseSwitch.Enabled = true;
            IS_CHECK_RECEIVECOIN = true;
            IS_CHECK_RECEIVECASH = true;

            //this.ini.WriteIniValue("配置", "显示广告", "是");
            //this.ini.WriteIniValue("配置", "是否全屏", "是");

            try
            {
                List<string> list = new List<string>();
                Process[] processes = Process.GetProcesses();
                for (int i = 0; i < processes.Length; i++)
                {
                    list.Add(processes[i].ProcessName.ToString());
                }
                if (!list.Contains("WinVideoPlayByAPlater"))
                {
                    ProcessStartInfo info = new ProcessStartInfo
                    {
                        FileName = @"C:\Program Files\CashVersion\aplayer\WinVideoPlayByAPlater"
                    };
                    new Process { StartInfo = info }.Start();
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }
        /// <summary>
        /// 初始化ini文件属性的默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerInitSet_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timerInitSet.Enabled = false;
                if (!this.ini.ValueExists("配置", "远程解除纸币器"))
                {
                    this.ini.WriteIniValue("配置", "远程解除纸币器", "否");
                }

                if (this.ini.ValueExists("配置", "最小纸币张数"))
                {
                    GlobalParmeters.MinCashCount = int.Parse(this.ini.ReadIniValue("配置", "最小纸币张数"));
                }
                else
                {
                    this.ini.WriteIniValue("配置", "最小纸币张数", "10");
                    GlobalParmeters.MinCashCount = 10;
                }

                if (this.ini.ValueExists("配置", "最小硬币个数"))
                {
                    GlobalParmeters.MinCoinCount = int.Parse(this.ini.ReadIniValue("配置", "最小硬币个数"));
                }
                else
                {
                    this.ini.WriteIniValue("配置", "最小硬币个数", "5");
                    GlobalParmeters.MinCoinCount = 5;
                }

                if (this.ini.ValueExists("配置", "兑奖服务器IP"))
                {
                    SocketClient.DrawIp = this.ini.ReadIniValue("配置", "兑奖服务器IP");
                    SocketClient.DrawPort = this.ini.ReadIniValue("配置", "兑奖端口");
                }
                else
                {
                    this.ini.WriteIniValue("配置", "兑奖服务器IP", "114.242.9.32");
                    SocketClient.DrawIp = "114.242.9.32";
                    SocketClient.DrawPort = "6491";
                }

                if (!this.ini.ValueExists("配置", "机头操作"))
                {
                    this.ini.WriteIniValue("配置", "机头操作", "FF");
                }

                if (this.ini.ValueExists("配置", "兑奖类型"))
                {
                    if (this.ini.ReadIniValue("配置", "兑奖类型") == "单码")
                    {
                        GlobalParmeters.prizeType = 1;
                    }
                    else
                    {
                        GlobalParmeters.prizeType = 2;
                    }
                }
                else
                {
                    this.ini.WriteIniValue("配置", "兑奖类型", "双码");
                    GlobalParmeters.prizeType = 2;
                }


                GlobalParmeters.MaxPrize = Convert.ToInt32(this.ini.ReadIniValue("配置", "最高兑奖金额"));
                string str2 = SQLiteHelper.GetSingle("SELECT max(SerialNumber) FROM tbllotteryprize").ToString();
                if (str2 != "")
                {
                    GlobalParmeters.DrawSerialNumbe = Convert.ToInt32(str2);
                }

                try
                {
                    this.CheckMotor();
                }
                catch (Exception exp)
                {
                    CommonUtils.WriteExceptionInfo(exp);
                    RuntimeLogUtils.WriteLog("机头异常:" + exp.Message);
                }

                try
                {
                    RuntimeLogUtils.WriteLog("初始化找币器...");
                    JCMBillAcceptorClass.Reset();
                    JCMBillAcceptorClass.UsefulAllBillType();
                    JCMBillAcceptorClass.SetAllSecurityNormal();
                    JCMBillAcceptorClass.SetRehibit();
                    JCMBillAcceptorClass.SetDirection();
                    JCMBillAcceptorClass.OptionFunction();
                    JCMBillAcceptorClass.SetCommunitToPoll();
                    RuntimeLogUtils.WriteLog("初始化找币器结束!");
                }
                catch (Exception exp)
                {
                    CommonUtils.WriteExceptionInfo(exp);
                    RuntimeLogUtils.WriteLog("找币器:" + exp.Message);
                }

                try
                {
                    RuntimeLogUtils.WriteLog("启动控制板定时器: timerQuery");
                    this.timerQuery.Enabled = true;
                    ControlPanel.SendCommand(0, 1);
                }
                catch (Exception exp)
                {
                    RuntimeLogUtils.WriteLog("控制板:" + exp.Message);
                }


                try
                {
                    RuntimeLogUtils.WriteLog("初始化硬币器...");
                    this.coin.UseOnePipe();
                    RuntimeLogUtils.WriteLog("初始化硬币器成功!");
                }
                catch (Exception exp)
                {
                    RuntimeLogUtils.WriteLog("硬币器:" + exp.Message);
                }

                try
                {
                    RuntimeLogUtils.WriteLog("初始化纸币收币器...");
                    CashClass.Reset();
                    RuntimeLogUtils.WriteLog("初始化纸币收币器成功!");
                }
                catch (Exception exp)
                {
                    RuntimeLogUtils.WriteLog("纸币收币器:" + exp.Message);
                }

                RuntimeLogUtils.WriteLog("启动定时器: timerLotteryCount, timerReceiveCoin, timerReceiveCash, timerShowMessage");

                this.timerLotteryCount.Enabled = true;
                IS_CHECK_RECEIVECOIN = true;
                IS_CHECK_RECEIVECASH = true;
                this.timerShowMessage.Enabled = true;


                RuntimeLogUtils.WriteLog("当前兑奖模式:" + GlobalParmeters.DuiJiangMode);
                if (GlobalParmeters.DuiJiangMode == YTDSSTGenII.Service.Enums.EnumDuiJiangMode.INTRADAK_SG)
                {
                    try
                    {
                        CommonUtils.showTime("启动SG兑奖心跳包，开始发心跳");
                        RuntimeLogUtils.WriteLog("启动SG兑奖心跳包，开始发心跳");
                        new Thread(new ThreadStart(this.initSG)).Start();
                    }
                    catch (Exception exception)
                    {
                        CommonUtils.WriteExceptionInfo(exception);
                        RuntimeLogUtils.WriteLog("兑奖:" + exception.Message);
                    }
                }
                else
                {
                    try
                    {
                        CommonUtils.showTime("启动Intradak兑奖心跳包，开始发心跳");
                        RuntimeLogUtils.WriteLog("启动Intradak兑奖心跳包，开始发心跳");
                        new Thread(new ThreadStart(this.checkSocket)).Start();
                    }
                    catch (Exception exception)
                    {
                        CommonUtils.WriteExceptionInfo(exception);
                        RuntimeLogUtils.WriteLog("兑奖:" + exception.Message);
                    }
                }

            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
                RuntimeLogUtils.WriteLog("初始化硬件:" + exp.Message);
            }

            this.timerAdvertiseSwitch.Enabled = true;
        }

        /**
         * 彩票数量统计Timer回调
         * 统计所有的机头上的票的数量，根据数据库的状态设置机头对应的票的状态
         */
        private void timerLotteryCount_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timerLotteryCount.Enabled = false;
                //是否需要重新加载彩票信息
                if (GlobalParmeters.isLoadDataAgin)
                {
                    GlobalParmeters.isLoadDataAgin = false;
                    this.initDb();
                    //是否需要检查机头的状态
                    if (GlobalParmeters.isChangeLottLength)
                    {
                        GlobalParmeters.isChangeLottLength = false;
                        this.CheckMotor();
                    }
                }

                //每个机头票的数量
                List<string> list = SQLiteHelper.CopyToList("select LotteryCount from tblMotorinfo");
                this.sum = 0;
                //累计总数，遍历8个机头
                for (int i = 0; i <= 7; i++)
                {
                    // 剩余票量大于‘0’张 && 机头状态为‘可用’
                    if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    {
                        //有票且状态可用
                        LotteryRemainLabArray[i].Text = "有票    " + list[i].PadLeft(3, '0') + " 张";
                        LotteryTypePBoxArray[i].BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\" + this.LotteryIdArray[i] + "_8.png");
                    }
                    else
                    {
                        //无票
                        LotteryRemainLabArray[i].Text = "售完";
                        if (list[i] == "0")
                        {
                            //TODO:机头1状态? 机头i状态?
                            this.ini.WriteIniValue("配置", "机头1状态", "2");
                        }
                        GlobalParmeters.MotorStatus[i] = 1;
                        //图片变灰
                        LotteryTypePBoxArray[i].BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\" + this.LotteryIdArray[i] + "_no8.png");
                    }

                    
                    //switch (i)
                    //{
                    //    case 0:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            //有票且状态可用
                    //            this.lblLotteryRemain1.Text = "有票    " + list[0].PadLeft(3, '0') + " 张";
                    //            String fileName = Application.StartupPath + "\\Images\\" + this.LotteryIdArray[0] + "_8.png";
                    //            Console.WriteLine(fileName);
                    //            this.picLotteryType1.BackgroundImage = Image.FromFile(fileName);

                    //            break;
                    //        }

                    //        //无票
                    //        this.lblLotteryRemain1.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头1状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[0] = 1;
                    //        //图片变灰
                    //        this.picLotteryType1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\" + this.LotteryIdArray[0] + "_no8.png");
                    //        break;
                    //    case 1:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            //有票
                    //            this.lblLotteryRemain2.Text = "有票    " + list[1].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType2.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[1] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain2.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头2状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[1] = 1;
                    //        this.picLotteryType2.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[1] + "_no8.png");
                    //        break;
                    //    case 2:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain3.Text = "有票    " + list[2].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType3.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[1] + "_8.png");
                    //            break;
                    //        }

                    //        this.lblLotteryRemain3.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头3状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[2] = 1;
                    //        this.picLotteryType3.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[2] + "_no8.png");
                    //        break;
                    //    case 3:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain4.Text = "有票    " + list[3].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType4.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[3] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain4.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头4状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[3] = 1;
                    //        this.picLotteryType4.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[3] + "_no8.png");
                    //        break;
                    //    case 4:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain5.Text = "有票    " + list[4].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType5.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[4] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain5.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头5状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[4] = 1;
                    //        this.picLotteryType5.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[4] + "_no8.png");

                    //        break;
                    //    case 5:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain6.Text = "有票    " + list[5].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType6.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[5] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain6.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头6状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[5] = 1;
                    //        this.picLotteryType6.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[5] + "_no8.png");
                    //        break;
                    //    case 6:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain7.Text = "有票    " + list[6].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType7.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[6] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain7.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头7状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[6] = 1;
                    //        this.picLotteryType7.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[6] + "_no8.png");
                    //        break;
                    //    case 7:
                    //        if ((list[i] != "0") && (GlobalParmeters.MotorStatus[i] != 1))
                    //        {
                    //            this.lblLotteryRemain8.Text = "有票    " + list[7].PadLeft(3, '0') + " 张";
                    //            this.picLotteryType8.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[7] + "_8.png");
                    //            break;
                    //        }
                    //        this.lblLotteryRemain8.Text = "售完";
                    //        if (list[i] == "0")
                    //        {
                    //            this.ini.WriteIniValue("配置", "机头8状态", "2");
                    //        }
                    //        GlobalParmeters.MotorStatus[7] = 1;
                    //        this.picLotteryType8.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[7] + "_no8.png");
                    //        break;

                    //    default:
                    //        break;
                    //}
                    //累计，所有票的数量
                    this.sum += int.Parse(list[i]);
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        /**
         *Query控制板状态，检查各个门和振动器的状态
         */
        private void timerQuery_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timerQuery.Enabled = false;
                //查询控制板状态，位运算
                byte[] buffer = ControlPanel.Query();
                if (buffer != null)
                {
                    //纸币器是否被打开
                    if (((1 & buffer[6]) > 0) && !this.isOpenCashBoxDoor)
                    {
                        //开
                        this.isOpenCashBoxDoor = true;
                        SQLiteHelper.ExecuteSql("INSERT INTO tblAlarm(alarmMark,time,UploadFlag) VALUES(1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false')");
                    }
                    else if (((1 & buffer[6]) <= 0) && this.isOpenCashBoxDoor)
                    {
                        //关
                        this.isOpenCashBoxDoor = false;
                        SQLiteHelper.ExecuteSql("INSERT INTO tblAlarm(alarmMark,time,UploadFlag) VALUES(7,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false')");
                    }

                    //机头的门是否被打开
                    if (((2 & buffer[6]) > 0) && !this.isOpenMotorDoor)
                    {
                        //开
                        this.isOpenMotorDoor = true;
                        SQLiteHelper.ExecuteSql("INSERT INTO tblAlarm(alarmMark,time,UploadFlag) VALUES(2,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false')");
                    }
                    else if (((2 & buffer[6]) <= 0) && this.isOpenMotorDoor)
                    {
                        //关
                        this.isOpenMotorDoor = false;
                        SQLiteHelper.ExecuteSql("INSERT INTO tblAlarm(alarmMark,time,UploadFlag) VALUES(8,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false')");
                    }

                    //是否检测到震动感应器的状态
                    if (((8 & buffer[6]) > 0) && !this.isVibrationed)
                    {
                        //震动过
                        this.isVibrationed = true;
                        SQLiteHelper.ExecuteSql("INSERT INTO tblAlarm(alarmMark,time,UploadFlag) VALUES(4,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false')");
                    }
                    else if (((8 & buffer[6]) <= 0) && this.isVibrationed)
                    {
                        //没有
                        this.isVibrationed = false;
                    }

                    //是否全屏
                    if (buffer[7] == 0)
                    {
                        this.leaveCount++;
                        if (this.leaveCount >= 20)
                        {
                            this.leaveCount = 0;
                            this.picGame.BackgroundImage = null;
                            this.ini.WriteIniValue("配置", "是否全屏", "是");
                        }
                    }
                    else if (buffer[7] == 1)
                    {
                        this.leaveCount = 0;
                        this.ini.WriteIniValue("配置", "是否全屏", "否");
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }

            //等待下一次检查，轮询 默认设置 500ms
            this.timerQuery.Enabled = true;
        }

        /**
         * 消息提示Timer，一直启用
         */
        private void timerShowMessage_Tick(object sender, EventArgs e)
        {
            if ((GlobalParmeters.ShowMessage != null && GlobalParmeters.ShowMessage.Length > 13) || IsStopService())
            {
                this.picDuiJiang.Visible = false;
            }
            else
            {
                this.picDuiJiang.Visible = true;
            }

            this.lblMessage.Text = GlobalParmeters.ShowMessage;
            this.lblInputedMoney.Text = GlobalParmeters.GotMoney.ToString();

            Application.DoEvents();
        }

        /**
            * 广告切换Timer
            **/
        private void timerAdvertiseSwitch_Tick(object sender, EventArgs e)
        {
            if (IsIntializing())
            {
                return;
            }

            if (YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM == true)
            {
                bool flag = this.ini.ReadIniValue("配置", "显示广告") == "是";
                if(flag)
                {
                    this.ini.WriteIniValue("配置", "显示广告", "否");
                }
                return;
            }

            if (MouseKeyBoardOperate.GetLastInputTime() >= 10L)
            {
                bool flag = this.ini.ReadIniValue("配置", "显示广告") == "否";
                if (flag)
                {
                    this.ini.WriteIniValue("配置", "显示广告", "是");
                }
                this.picGame.BackgroundImage = null;
                //this.timerAdvertiseSwitch.Enabled = false;
            }
            else
            {
                bool flag = this.ini.ReadIniValue("配置", "显示广告") == "是";
                if (flag)
                {
                    this.ini.WriteIniValue("配置", "显示广告", "否");
                }
            }
        }

        /**
        * 读取彩票保安区Timer
        */
        private void timerReadSecurityLottery_Tick(object sender, EventArgs e)
        {
            //人工兑奖
            if (GlobalParmeters.DuiJiangMode == YTDSSTGenII.Service.Enums.EnumDuiJiangMode.INTRADAK_MANUL)
            {
                intrdakDuijiang();
            }
            //自动兑奖
            else if (GlobalParmeters.DuiJiangMode == YTDSSTGenII.Service.Enums.EnumDuiJiangMode.INTRADAK_SG)
            {
                sgDuijiang();
            }
            refreshMainInfo();
        }

        /**
         * 消息提醒控制Timer,初始化消息的状态
         */
        private void timerInitInfoMessage_Tick(object sender, EventArgs e)
        {
            this.timerInitInfoMessage.Enabled = false;
            this.picDuiJiang.Visible = true;

            String tips = "暂不支持兑奖，请选择彩票购彩";

            //纸币器最大金额大于 maxPrize，则允许兑奖的最大金额
            if (GlobalParmeters.CashCount * GlobalParmeters.CashType >= GlobalParmeters.MaxPrize)
            {
                //用户已经投入现金，并且现金数量大于票箱最小面值
                if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                {
                    GlobalParmeters.ShowMessage = SELL_TIPS;
                }
                else
                {
                    GlobalParmeters.ShowMessage = SELL_TIPS;
                }
            }
            //无法兑奖，但是允许购买彩票
            else if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
            {
                //本机暂无法兑奖，请选择彩种
                GlobalParmeters.ShowMessage = tips;
            }
            //投币金额不足
            else
            {
                //本机暂无法兑奖，请投入现金
                GlobalParmeters.ShowMessage = tips;
            }
            
            this.isDuiJiang = false;

            //处理所有的当前在消息队列中的Windows消息。
            //在不加的时候，因为优先级的问题，程序会执行主进程的代码，再执行别代码，而加了以后就可以同步执行。
            Application.DoEvents();
        }

        private void picDuiJiang_Click(object sender, EventArgs e)
        {   
            try
            {
                if ((!this.isDuiJiang && !this.isJieSuan) && (GlobalParmeters.ShowMessage != "暂停服务") && !IsIntializing())
                {
                    //this.picDuiJiang.BackgroundImage = Resources.jiesuan_down;
                    //纸币数量大余最大兑奖金额，则不提供兑奖服务
                    if ((GlobalParmeters.CashCount * GlobalParmeters.CashType) < GlobalParmeters.MaxPrize)
                    {
                        GlobalParmeters.ShowMessage = "本机零钱不足，暂时无法兑奖";
                        this.timerInitInfoMessage.Enabled = true;
                        return;
                    }

                    if (this.isConnected == true)
                    {
                        this.isDuiJiang = true;
                        this.picGame.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//ScanPrize.jpg");
                        GlobalParmeters.ShowMessage = "刮开彩票保安区,将保安区端插入兑奖口";
                        //禁用其他硬件
                        IS_CHECK_RECEIVECASH = false;
                        IS_CHECK_RECEIVECOIN = false;
                        this.timerQuery.Enabled = false;

                        CommonUtils.showTime("兑奖按键按下");
                        
                        //开始读取保安区二维码
                        this.timerReadSecurityLottery.Enabled = true;
                    }
                    else
                    {
                        GlobalParmeters.ShowMessage = "网络异常,暂时无法兑奖";
                        this.timerInitInfoMessage.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }



        /// <summary>
        /// 点击结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JieSuan_Click ( object sender, EventArgs e )
        {           
            if (( ( !this.isDuiJiang && !this.isJieSuan ) && ( GlobalParmeters.ShowMessage != "暂停服务" ) && !IsIntializing()) )
            {
                this.picGame.BackgroundImage = null;
                this.picJieSuan.BackgroundImage = Resources.jiesuan_up;

                if (UserContext.getInstance().getBuyMoney() == 0 )//还未选中彩票时点击
                {
                    GlobalParmeters.ShowMessage = "请选择彩票再点击“购买”";
                }
                //其他
                else
                {
                    //TODO 需要做二次确认 如果余额足够支付
                    if (GlobalParmeters.GotMoney >= UserContext.getInstance().getBuyMoney())
                    {
                        FrmPopSureMoneyPay frm = new FrmPopSureMoneyPay();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            this.isJieSuan = true;
                            //禁用
                            IS_CHECK_RECEIVECASH = false;
                            IS_CHECK_RECEIVECOIN = false;
                            //this.timerAdvertiseSwitch.Enabled = false;
                            this.timerCutPaper.Enabled = true;

                            //生成一个不插库的订单
                            Order o = new Order(UserContext.getInstance().UserMotorArray);
                            UserContext.getInstance().UserOrder = o;

                            //this.ini.WriteIniValue("配置", "显示广告", "否");
                            //this.timerAdvertiseSwitch.Enabled = true;
                            GlobalParmeters.PayoutFlag = false;
                            GlobalParmeters.ShowMessage = "正在出票，请稍候...";
                            this.timerInitInfoMessage.Enabled = true;
                        }

                        YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
                    }
                    else
                    {
                        //弹窗——走支付界面
                        tipForm = new FrmPopBuy();
                        DialogResult dr = tipForm.ShowDialog();
                        if (null != UserContext.getInstance().UserOrder && UserContext.getInstance().UserOrder.OrderState == 1)//支付成功——出票
                        {
                            this.isJieSuan = true;
                            //禁用
                            IS_CHECK_RECEIVECASH = false;
                            IS_CHECK_RECEIVECOIN = false;
                            
                            //this.ini.WriteIniValue("配置", "显示广告", "否");
                            //this.timerAdvertiseSwitch.Enabled = true;

                            GlobalParmeters.PayoutFlag = false;
                            GlobalParmeters.ShowMessage = "正在出票，请稍候...";
                            this.timerInitInfoMessage.Enabled = true;
                            this.timerCutPaper.Enabled = true;

                            //tipForm = new FrmPopPrinting(this.mo, this.ini);
                            //tipForm.ShowDialog();
                            //Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
                        }
                        else//支付失败 
                        {
                            UserContext.getInstance().UserOrder = null;
                            refreshMainInfo();

                            //收币器等的工作状态有可能在支付界面修改了，这里要重置
                            IS_CHECK_RECEIVECASH = true;
                            IS_CHECK_RECEIVECOIN = true;
                            
                            this.timerCutPaper.Enabled = false;

                            //this.ini.WriteIniValue("配置", "显示广告", "是");
                            //this.timerAdvertiseSwitch.Enabled = true;

                            YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
                        }
                    }                                        
                }
            }
        }

        private void picJieSuan_MouseDown(object sender, MouseEventArgs e)
        {
            if (((this.labbuylmoney.Text != "0") || (GlobalParmeters.GotMoney != 0)) && ((!this.isDuiJiang && !this.isJieSuan) && (GlobalParmeters.ShowMessage != "暂停服务")))
            {
                this.picJieSuan.BackgroundImage = Resources.jiesuan_down;
            }
        }

        private void picJieSuan_MouseUp(object sender, MouseEventArgs e)
        {
            if (((this.labbuylmoney.Text != "0") || (GlobalParmeters.GotMoney != 0)) && ((!this.isDuiJiang && !this.isJieSuan) && (GlobalParmeters.ShowMessage != "暂停服务")))
            {
                this.picJieSuan.BackgroundImage = Resources.jiesuan_up;
            }
        }

        /// <summary>
        /// 点击加号的公共处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plus_Click ( object sender, EventArgs e )
        {
            if ( ( !this.isDuiJiang && !this.isJieSuan ) && ( GlobalParmeters.ShowMessage != "暂停服务" ) && !IsIntializing())
            {
                if (UserContext.getInstance().getBuyMoney() > 500)
                {
                    GlobalParmeters.ShowMessage = "已超单笔最大购买金额上限，请理性购彩！";
                    return;
                }

                //取出看看是第几个加号
                int round = 0;
                if (((PictureBox)sender).Name.Contains("picAdd"))
                {
                    round = Convert.ToInt32(((PictureBox)sender).Name.Replace("picAdd", "")) - 1;
                }
                else
                {
                    round = Convert.ToInt32(((PictureBox)sender).Name.Replace("picLotteryType", "")) - 1;
                }

                if (GlobalParmeters.MotorStatus[round] == 0)//机头可用
                {
                    //1. 如果机头可用

                    //1.1 如果机头的剩余彩票数量 > 用户购物车的改机头的彩票购买数量
                    //1.1.1 设置广告图片
                    //1.1.2 设置配置文件的广告相关字段
                    //1.1.3 用户上下文的

                    //1.2 如果彩票数量不够
                    //1.2.1 提示：剩余数量不够
                    //1.2.2 this.timerInitInfoMessage.Enabled = true;

                    if (MachineContext.getInstance().ServerMotorArray[round].TraceLotteryNum -
                        UserContext.getInstance().UserMotorArray[round].BuyLotteryNum > 0)
                    {
                        this.picGame.BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[round] + ".png");
                        this.ini.WriteIniValue("配置", "显示广告", "否");
                        //this.timerAdvertiseSwitch.Enabled = true;
                        timerClearPicGameBackground.Enabled = true;
                        UserContext.getInstance().UserMotorArray[round].BuyLotteryNum++;
                        refreshMainInfo();
                    }
                    //票的数量不够
                    else
                    {
                        GlobalParmeters.ShowMessage = "此票种剩余数量不足,请选择其他彩票";
                        this.timerInitInfoMessage.Enabled = true;
                    }
                }
                else
                {
                    //图片变灰
                    LotteryTypePBoxArray[round].BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\" + this.LotteryIdArray[round] + "_no8.png");
                    LotteryRemainLabArray[round].Text = "售完";
                    GlobalParmeters.ShowMessage = "请选择其他彩票";
                    this.timerInitInfoMessage.Enabled = true;
                }                          
            }
        }

        /// <summary>
        /// 点击减号的公共处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minus_Click ( object sender, EventArgs e )
        {
            //取出看看是第几个减号
            int round = Convert.ToInt32 ( ( ( PictureBox ) sender ).Name.Replace ( "picReduction", "" ) ) - 1;

            if ( ( !this.isDuiJiang && !this.isJieSuan ) && ( GlobalParmeters.ShowMessage != "暂停服务" ) && !IsIntializing() 
                && UserContext.getInstance ( ).UserMotorArray [ round ].BuyLotteryNum > 0 )
            {
                UserContext.getInstance ( ).UserMotorArray [ round ].BuyLotteryNum--;
            }

            refreshMainInfo();
        }

        /**
         * 检测是否到关机时间，？？？开关机时间如何设计定
         */
        private void timerShutDown_Tick(object sender, EventArgs e)
        {

            try
            {
                this.timerShutDown.Enabled = false;
                //CommonUtils.showTime("timerShutDown_Tick:检查是否需要关机");


                string str = this.ini.ReadIniValue("配置", "机头操作");

                //配置机头
                if ((str.Substring(0, 1) == "A") || (str.Substring(0, 1) == "a"))
                {
                    GlobalParmeters.MotorStatus[int.Parse(str.Substring(1, 1)) - 1] = 1;
                    SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=1 WHERE MotorId=" + int.Parse(str.Substring(1, 1)));
                    this.ini.WriteIniValue("配置", "机头" + int.Parse(str.Substring(1, 1)) + "状态", "4");
                    this.ini.WriteIniValue("配置", "机头操作", "FF");
                    this.timerLotteryCount.Enabled = true;
                }
                else if ((str.Substring(0, 1) == "B") || (str.Substring(0, 1) == "b"))
                {
                    GlobalParmeters.MotorStatus[int.Parse(str.Substring(1, 1)) - 1] = 0;
                    SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=0 WHERE MotorId=" + int.Parse(str.Substring(1, 1)));
                    this.ini.WriteIniValue("配置", "机头" + int.Parse(str.Substring(1, 1)) + "状态", "0");
                    this.ini.WriteIniValue("配置", "机头操作", "FF");
                    this.timerLotteryCount.Enabled = true;
                }

                this.InhibitCashBox = this.ini.ReadIniValue("配置", "远程禁用纸币器") == "是";
                this.RemoveCashBox = this.ini.ReadIniValue("配置", "远程解除纸币器") == "是";

                if (this.InhibitCashBox)
                {
                    GlobalParmeters.LongRangeDisableCashBox = true;
                }

                if (this.RemoveCashBox)
                {
                    GlobalParmeters.LongRangeDisableCashBox = false;
                    GlobalParmeters.CashBoxFlag = false;
                    this.ini.WriteIniValue("配置", "远程解除纸币器", "否");

                    JCMBillAcceptorClass.ResetJCMBillAcceptor();
                }

                this.IsShutDown = this.ini.ReadIniValue("配置", "定时关机") == "是";
                //TODO
                if (this.IsShutDown)
                {
                    IS_CHECK_RECEIVECASH = false;
                    IS_CHECK_RECEIVECOIN = false;

                    JCMBillAcceptorClass.InhibitAllBillType();

                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：快到关机时间了");
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) > 0)
                    {
                        this.panel1.Visible = true;
                        this.lblShutDownMessage.Visible = true;
                        this.lblShutDownMessage.Text = "设备即将关闭,请尽快完成交易";
                        if (this.timerShutdownCount == 0)
                        {
                            this.ini.WriteIniValue("配置", "显示广告", "否");
                            sndPlaySound(this.FileAddress + "/yuyin/shutdown.WAV", 1);
                        }
                        //关机倒计时
                        this.timerShutdownCount++;
                        if (this.timerShutdownCount >= 180)
                        {
                            this.ShutdownTimeCount--;
                            if (this.ShutdownTimeCount <= 5)
                            {
                                this.timerQuery.Enabled = false;
                                Process[] processes = Process.GetProcesses();
                                for (int i = 0; i < processes.Length; i++)
                                {
                                    //关闭广告
                                    if ((processes[i].ProcessName == "TestMeidiaPlayWithDirectX") || (processes[i].ProcessName == "WinVideoPlayByAPlater") || (processes[i].ProcessName == "UpLoadBySocket"))
                                    {
                                        processes[i].Kill();
                                    }
                                }
                            }
                            this.lblShutDownMessage.Text = "剩余关机时间： " + this.ShutdownTimeCount + "秒";
                            if (this.ShutdownTimeCount <= 0)
                            {
                                if (SQLiteHelper.ExecuteSql(string.Concat(new object[] { "insert into EatCash(Money,Time,hint,UploadFlag) values ('", GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text), "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','定时关机','false')" })) > 0)
                                {
                                    CommonUtils.WriteMoney(0);
                                }
                                this.ini.WriteIniValue("配置", "定时关机", "否");
                                this.ini.WriteIniValue("配置", "切断电源", "是");

                                CommonUtils.showTime("购彩程序timerShutDown_Tick切断电源");
                                Environment.Exit(0);
                            }
                        }
                    }
                    else
                    {
                        this.timerShutdownCount++;
                        this.timerQuery.Enabled = false;
                        if (this.timerShutdownCount >= 5)
                        {
                            this.timerShutdownCount = 0;
                            this.ini.WriteIniValue("配置", "定时关机", "否");
                            this.ini.WriteIniValue("配置", "切断电源", "是");
                            CommonUtils.showTime("购彩程序timerShutDown_Tick切断电源");
                            Environment.Exit(0);
                        }
                    }
                }

                this.breakSource = this.ini.ReadIniValue("配置", "异常断电") == "是";
                if (this.breakSource)
                {
                    IS_CHECK_RECEIVECASH = false;
                    IS_CHECK_RECEIVECOIN = false;
                    JCMBillAcceptorClass.InhibitAllBillType();
                    CommonUtils.WriteServiceSuspendedInfo("禁用所有纸币：220V电源丢失，准备关机");
                    if ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) > 0)
                    {
                        this.panel1.Visible = true;
                        this.lblShutDownMessage.Visible = true;
                        if (this.breakSourceTime == 30)
                        {
                            this.ini.WriteIniValue("配置", "显示广告", "否");
                            sndPlaySound(this.FileAddress + "/yuyin/shutdown.WAV", 1);
                        }
                        this.breakSourceTime--;
                        if (this.breakSourceTime <= 5)
                        {
                            this.timerQuery.Enabled = false;
                            Process[] processArray2 = Process.GetProcesses();
                            for (int j = 0; j < processArray2.Length; j++)
                            {
                                if ((processArray2[j].ProcessName == "TestMeidiaPlayWithDirectX") || (processArray2[j].ProcessName == "WinVideoPlayByAPlater"))
                                {
                                    processArray2[j].Kill();
                                }
                            }
                        }
                        this.lblShutDownMessage.Text = "剩余关机时间： " + this.breakSourceTime + "秒";
                        if (this.breakSourceTime <= 0)
                        {
                            if (SQLiteHelper.ExecuteSql(string.Concat(new object[] { "insert into EatCash(Money,Time,hint,UploadFlag) values ('", GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text), "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','异常断电','false')" })) > 0)
                            {
                                CommonUtils.WriteMoney(0);
                            }
                            this.ini.WriteIniValue("配置", "异常断电", "否");
                            this.ini.WriteIniValue("配置", "关机", "是");
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        this.timerQuery.Enabled = false;
                        this.time5++;
                        if (this.time5 >= 5)
                        {
                            this.time5 = 0;
                            this.ini.WriteIniValue("配置", "异常断电", "否");
                            this.ini.WriteIniValue("配置", "关机", "是");
                            Environment.Exit(0);
                        }
                    }
                }

                if ((this.ReadAndWriteXml("read") == "1") && ((GlobalParmeters.GotMoney + int.Parse(this.labbuylmoney.Text)) <= 0))
                {
                    this.ReadAndWriteXml("write");
                    Process[] processArray3 = Process.GetProcesses();
                    for (int k = 0; k < processArray3.Length; k++)
                    {
                        if (((processArray3[k].ProcessName == "WinPowerContorlForUPS") || (processArray3[k].ProcessName == "TestMeidiaPlayWithDirectX")) || (((processArray3[k].ProcessName == "UpLoadBySocket") || (processArray3[k].ProcessName == "WinFormCameraDemo")) || (processArray3[k].ProcessName == "WinVideoPlayByAPlater")))
                        {
                            //关闭UPS
                            processArray3[k].Kill();
                        }
                    }
                    Process.Start("shutdown", "-r -t 0");
                    Environment.Exit(0);
                }

                if (GlobalParmeters.isLoadDataAgin)
                {
                    this.timerLotteryCount.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            finally
            {
                this.timerShutDown.Enabled = true;
                //CommonUtils.showTime("timerShutDown_Tick:不需要关机");
            }
        }

        /**
         * 机头出票
         */
        private void timerCutPaper_Tick(object sender, EventArgs e)
        {
            this.timerCutPaper.Enabled = false;
            Dictionary<String, int> errorMap = new Dictionary<string, int>();
            FrmPopPrinting tform = new FrmPopPrinting(this.mo, this.ini, this.isFlashLights,errorMap);
            tform.ShowDialog();
            tform.Dispose();
            YTDSSTGenII. Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
            
            //DbOperate operate = null;

            try
            {
            //    operate = new DbOperate();
            //    this.mo = this.mo == null ? new MotorOperate() : this.mo;
            //    foreach (OrderDetails item in UserContext.getInstance().UserOrder.Tickets)
            //    {
            //        if (errorMap.ContainsKey(item.getHeadNo_String()))
            //        {
            //            item.TicketOutState = 3;
            //            errorMap[item.getHeadNo_String()] += 1;
            //            item.TicketOutState = 3;
            //            UserContext.getInstance().UserOrder.OutErrorNum++;
            //            UserContext.getInstance().UserOrder.OutErrorMoney += item.UnitPrice;
            //            updateDBTicketError(item);
            //            //TODO 微信支付和现金支付交易记录会有所不同         
            //            //UserContext.getInstance().UserOrder.OutTradeNo;
            //            operate.InsertTradeFailInfo(item.getHeadNo_Int().ToString(), item.UnitPrice.ToString(), item.LotteryName);
            //        }
            //        else
            //        {
            //            int motorLength = 0, lotteryPrice = 0, motorLotteryRemain = 0;
            //            string lotteryName = "";
            //            operate.GetMotorInfoByMotorNumber(item.getHeadNo_Int(), ref motorLength, ref lotteryPrice, ref motorLotteryRemain, ref lotteryName);
            //            if (this.CutPaper(item.getHeadNo_Int()))
            //            {
            //                //出票成功
            //                operate.Decrease(item.getHeadNo_Int(), motorLotteryRemain - 1);
            //                motorLotteryRemain--;
            //                //TODO 微信支付和现金支付交易记录会有所不同
            //                operate.InsertTrade(item.getHeadNo_Int().ToString(), lotteryPrice.ToString(), lotteryName);

            //                this.isFlashLights = true;

            //                if (GlobalParmeters.GotMoney > 0)//走现金
            //                {
            //                    GlobalParmeters.GotMoney -= lotteryPrice;
            //                    UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
            //                    CommonUtils.WriteMoney(GlobalParmeters.GotMoney);
            //                }

            //                item.TicketOutState = 2;
            //                UserContext.getInstance().UserOrder.OutSuccNum++;
            //                UserContext.getInstance().UserOrder.OutSuccMoney += item.UnitPrice;

            //                MachineContext.getInstance().ServerMotorArray[item.getHeadNo_Int() - 1].TraceLotteryNum--;

            //                updateDBTicketSucc(item);
            //            }
            //            else
            //            {
            //                item.TicketOutState = 3;
            //                errorMap.Add(item.getHeadNo_String(), 1);
            //                item.TicketOutState = 3;
            //                UserContext.getInstance().UserOrder.OutErrorNum++;
            //                UserContext.getInstance().UserOrder.OutErrorMoney += item.UnitPrice;
            //                updateDBTicketError(item);

            //                //TODO 失败                                
            //                operate.InsertTradeFailInfo(item.getHeadNo_Int().ToString(), item.UnitPrice.ToString(), item.LotteryName);
            //            }
            //        }
            //    }

            //    //关闭连接
            //    operate.CloseConn();

                this.isConsumption = true;

                if (this.isFlashLights)//闪灯,停止闪灯
                {
                    this.isFlashLights = false;
                    ControlPanel.SendCommand(1, 1);
                }

                //((FrmPopPrinting)tipForm).formClose();
                //tipForm.Dispose();
                //Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
                
                //没有错漏票
                if (errorMap.Count == 0)
                {
                    UserContext.getInstance().UserOrder.TicketOutState = 2;
                    if (UserContext.getInstance().UserOrder.PayType == 1)//现金购买
                    {
                        tipForm = new FrmPopPrintSucces(1);
                    }
                    else if (UserContext.getInstance().UserOrder.PayType == 2)
                    {
                        tipForm = new FrmPopPrintSucces(2);
                    }
                    else if (UserContext.getInstance().UserOrder.PayType == 3)
                    {
                        tipForm = new FrmPopPrintSucces(3);
                    }
                }
                else //有错漏票
                {
                    if (UserContext.getInstance().UserOrder.PayType == 1)//现金购买
                    {
                        if (UserContext.getInstance().UserOrder.OutErrorNum == UserContext.getInstance().getBuyLotteryNum())//全部失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState = 3;
                            tipForm = new FrmPopBuyAllFailure();
                        }
                        else //部分失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState =4;
                            tipForm = new FrmPopBuyPartialFailure();
                        }
                    }
                    else if (UserContext.getInstance().UserOrder.PayType == 2) //微信购买
                    {
                        if (UserContext.getInstance().UserOrder.OutErrorNum == UserContext.getInstance().getBuyLotteryNum())//全部失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState = 3;
                            tipForm = new FrmPopPaySuccessAllFailure();
                        }
                        else //部分失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState = 4;
                            tipForm = new FrmPopPaySuccessPartialFailure();
                        }
                    }
                    else if (UserContext.getInstance().UserOrder.PayType == 3) //支付宝购买
                    {
                        if (UserContext.getInstance().UserOrder.OutErrorNum == UserContext.getInstance().getBuyLotteryNum())//全部失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState = 3;
                            tipForm = new FrmPopPaySuccessAllFailure();
                        }
                        else //部分失败
                        {
                            UserContext.getInstance().UserOrder.TicketOutState = 4;
                            tipForm = new FrmPopPaySuccessPartialFailure();
                        }
                    }
                }

                tipForm.ShowDialog();
                tipForm.Dispose();
                YTDSSTGenII.Service.Context.ApplicationContext.getInstance().IS_HAS_POPUP_FORM = false;
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            finally
            {
                //修改订单状态

                this.isJieSuan = false;

                GlobalParmeters.ShowMessage = SELL_TIPS;

                this.timerInitInfoMessage.Enabled = true;
                this.timerCutPaper.Enabled = false;

                UserContext.getInstance().Balance = GlobalParmeters.GotMoney;
                UserContext.getInstance().clearBuyInfo();
                UserContext.getInstance().UserOrder = null;
                CommonUtils.WriteMoney(GlobalParmeters.GotMoney);

                refreshMainInfo();

                //启用
                IS_CHECK_RECEIVECASH = true;
                IS_CHECK_RECEIVECOIN = true;

                //this.timerAdvertiseSwitch.Enabled = true;
                //this.ini.WriteIniValue("配置", "显示广告", "是");

                //GlobalParmeters.PayoutFlag = true;                
            }
        }

        /// <summary>
        /// 修改订单的出票结果
        /// </summary>
        /// <param name="item"></param>
        private void updateOrderTicketResultInfo(Order o)
        {
            List<String> sqllist = new List<string>();
            //插入订单sql
            String addordersql = String.Format(@"Update p_terminal_orders set 
                out_success_num = {0},
                out_success_money = {1},
                out_error_num = {2},
                out_error_money = {3} where out_trade_no='{4}'",
                o.OutSuccNum.ToString(),o.OutSuccMoney.ToString(),o.OutErrorNum.ToString(),o.OutErrorMoney.ToString(), o.OutTradeNo);

            sqllist.Add(addordersql);

            SQLiteHelper.ExecuteSqlTran(sqllist);
        }

        private void timerPayout_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timerPayout.Enabled = false;
                if (GlobalParmeters.downtime)
                {
                    GlobalParmeters.ShowMessage = "退币失败,请联系客服或继续购彩";
                    this.timerInitInfoMessage.Enabled = true;
                }
                else if (GlobalParmeters.GotMoney > 0)
                {
                    if (GlobalParmeters.GotMoney < GlobalParmeters.CashType)
                    {
                        if (GlobalParmeters.CoinCount >= Convert.ToInt32((double)(((double)GlobalParmeters.GotMoney) / GlobalParmeters.CoinType)))
                        {
                            this.payoutCoin(GlobalParmeters.GotMoney, false);
                        }
                        else
                        {
                            GlobalParmeters.ShowMessage = "本机零钱不足，请联系客服或继续购彩";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                    else if ((GlobalParmeters.GotMoney % GlobalParmeters.CashType) == 0)
                    {
                        if (GlobalParmeters.CashCount >= (GlobalParmeters.GotMoney / GlobalParmeters.CashType))
                        {
                            this.payoutCash(GlobalParmeters.GotMoney);
                        }
                        else
                        {
                            GlobalParmeters.ShowMessage = "本机零钱不足，请联系客服或继续购彩";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                    else if ((GlobalParmeters.CashCount >= (GlobalParmeters.GotMoney / GlobalParmeters.CashType)) 
                        && ((GlobalParmeters.CoinCount * GlobalParmeters.CoinType) >= (GlobalParmeters.GotMoney % GlobalParmeters.CashType)))
                    {
                        this.payoutCash(GlobalParmeters.GotMoney);
                    }
                    else
                    {
                        GlobalParmeters.ShowMessage = "本机零钱不足，请联系客服或继续购彩";
                        this.timerInitInfoMessage.Enabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                GlobalParmeters.ShowMessage = "退币中出现问题,请联系客服";
                this.timerInitInfoMessage.Enabled = true;
            }
            finally
            {
                //退币后进行下一次购彩初始化
                this.isJieSuan = false;

                IS_CHECK_RECEIVECASH = true;
                IS_CHECK_RECEIVECOIN = true;

                if (GlobalParmeters.GotMoney == 0)
                {
                    this.timerQuery.Enabled = true;
                }

                refreshMainInfo();
            }
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clearcart_Click ( object sender, EventArgs e )
        {
            if ( ( !this.isDuiJiang && !this.isJieSuan ) && ( GlobalParmeters.ShowMessage != "暂停服务" ) && !IsIntializing())
            {
                UserContext.getInstance().clearBuyInfo();
                refreshMainInfo();
            }
        }

        private bool IsIntializing() {
            return lblMessage.Text == "正在初始化，请稍后...";
        }

        /// <summary>
        /// 刷新主界面显示
        /// </summary>
        private void refreshMainInfo() {
            
            this.labbuylmoney.Text = UserContext.getInstance().getBuyMoney().ToString();
            this.labBuyNum.Text = UserContext.getInstance().getBuyLotteryNum().ToString(); 
             this.lblInputedMoney.Text = UserContext.getInstance().Balance.ToString();

            for (int i = 0; i < 8; i++)
            {
                LotteryRemainLabArray[i].Text = (GlobalParmeters.MotorStatus[i] != 0 || MachineContext.getInstance().ServerMotorArray[i].TraceLotteryNum == 0) ? "售完" : "有票    " + (MachineContext.getInstance().ServerMotorArray[i].TraceLotteryNum - UserContext.getInstance().UserMotorArray[i].BuyLotteryNum).ToString("000") + "张";
                LotteryTypePBoxArray[i].BackgroundImage = Image.FromFile(Application.StartupPath + "//Images//" + this.LotteryIdArray[i] + "_"+( (GlobalParmeters.MotorStatus[i] != 0 || MachineContext.getInstance().ServerMotorArray[i].TraceLotteryNum == 0) ?"no":"")+ "8.png");
            }

            this.lblSelectedNum1.Text = UserContext.getInstance().UserMotorArray[0].BuyLotteryNum.ToString();
            this.lblSelectedNum2.Text = UserContext.getInstance().UserMotorArray[1].BuyLotteryNum.ToString();
            this.lblSelectedNum3.Text = UserContext.getInstance().UserMotorArray[2].BuyLotteryNum.ToString();
            this.lblSelectedNum4.Text = UserContext.getInstance().UserMotorArray[3].BuyLotteryNum.ToString();
            this.lblSelectedNum5.Text = UserContext.getInstance().UserMotorArray[4].BuyLotteryNum.ToString();
            this.lblSelectedNum6.Text = UserContext.getInstance().UserMotorArray[5].BuyLotteryNum.ToString();
            this.lblSelectedNum7.Text = UserContext.getInstance().UserMotorArray[6].BuyLotteryNum.ToString();
            this.lblSelectedNum8.Text = UserContext.getInstance().UserMotorArray[7].BuyLotteryNum.ToString();

            //TODO 清空和退币是一个互斥操作
            if (UserContext.getInstance().getBuyMoney() == 0 
                    && this.isConsumption && GlobalParmeters.GotMoney > 0)
            {
                this.btnReturnMoney.Visible = true;
            }
            else {
                this.btnReturnMoney.Visible = false;
            }

        }
        
        //集成国家兑奖接口代码
        private int sgInitcount = 0;

        //初始化兑奖
        private void initSG()
        {
            try
            {
                while (true)
                {
                    while ((this.isDuiJiang || this.isJieSuan) || (GlobalParmeters.ShowMessage == "暂停服务"))
                    {
                        Thread.Sleep((int)(GlobalParmeters.pingtime * 1000));
                    }

                    string strversion = "";
                    int num = -1;
                    if (this.sgInitcount > 0)
                    {
                        num = SGBentDll.sgPingHost(GlobalParmeters.pingtime);
                        if (num != 0)
                        {
                            CommonUtils.showTime("心跳调用：sgPinghost(" + GlobalParmeters.pingtime + ")" + "兑奖状态：" + num);
                            this.isConnected = false;
                        }
                        else
                        {
                            this.isConnected = true;
                        }
                    }
                    else
                    {
                        num = CommonUtils.sgInit(GlobalParmeters.machineNumber, ref strversion);
                        if (num == 0)
                            this.isConnected = true;

                        CommonUtils.showTime("心跳调用：sgInit()");
                        this.ini.WriteIniValue("配置", "兑奖版本", strversion);

                        GlobalParmeters.sgversion = this.ini.ReadIniValue("配置", "兑奖版本");

                        this.sgInitcount++;

                        if (num == 0)
                        {
                            CommonUtils.showTime("SG初始化成功");
                            GlobalParmeters.ShowMessage = SELL_TIPS;
                            /**
                            if ("请选择彩种,点击结算购买".Equals(GlobalParmeters.ShowMessage))
                            {
                                if ((GlobalParmeters.CashCount * GlobalParmeters.CashType) >= GlobalParmeters.MaxPrize)
                                {
                                    if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                                    {
                                        GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                    }
                                    else if ((UserContext.getInstance().Balance == 0) && (UserContext.getInstance().getBuyMoney() == 0))
                                    {
                                        GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                    }
                                    else if ((UserContext.getInstance().Balance == 0) && (UserContext.getInstance().getBuyMoney() != 0))
                                    {
                                        GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                    }
                                    else if ((UserContext.getInstance().Balance != 0) && (UserContext.getInstance().getBuyMoney() != 0))
                                    {
                                        GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                    }
                                    else
                                    {
                                        GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                    }
                                }
                                else if (GlobalParmeters.GotMoney >= this.MotorMixPrice)
                                {
                                    GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                }
                                else
                                {
                                    GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                                }
                            }
                            */
                        }
                        else
                        {
                            /**
                            if ((UserContext.getInstance().Balance == 0) && (UserContext.getInstance().Balance == 0))
                            {
                                GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                            }
                            else if ((UserContext.getInstance().Balance == 0)  && (UserContext.getInstance().Balance != 0))
                            {
                                GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                            }
                            else if ((UserContext.getInstance().Balance != 0) && (UserContext.getInstance().Balance != 0))
                            {
                                GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                            }
                            else
                            {
                                GlobalParmeters.ShowMessage = "请选择彩种,点击结算购买";
                            }
                            */
                            GlobalParmeters.ShowMessage = SELL_TIPS;

                            CommonUtils.showTime("初始化失败，暂不支持兑奖，错误代码： " + num);

                            string errocode = "";
                            string erromessage = "";

                            CommonUtils.ErroMessage(ref errocode, ref erromessage);
                            CommonUtils.showTime("操作不成功：(" + errocode + ")\r\n" + erromessage);
                        }
                    }

                    Thread.Sleep((int)(GlobalParmeters.pingtime * 1000));
                }
            }
            catch (Exception exception)
            {
                CommonUtils.showTime("SG初始化失败:" + exception.ToString());
            }
        }

        private void mainsgClose()
        {
            try
            {
                CommonUtils.showTime("调用关机，发送SGCLOSE()指令");
                SGBentDll.sgClose();
                CommonUtils.showTime("sgclose()命令执行完毕");
                Thread.Sleep(0x7d0);
            }
            catch (Exception exception)
            {
                CommonUtils.showTime("执行sgclose() 过程中发生异常：" + exception.Message);
            }
        }
        private void PrizeMain(string time)
        {
            try
            {
                int key = 0;
                double prizeamount = 0.0;
                double acount = 0.0;
                key = SGBentDll.sgValidation(0, GlobalParmeters.LotterySerialNumber, GlobalParmeters.SecurityNumber, (double)(GlobalParmeters.CashCount * GlobalParmeters.CashType), ref prizeamount, ref acount);
                if (key == 0)
                {
                    GlobalParmeters.ShowMessage = "正在检测彩票,请放回或微调彩票位置";
                    GlobalParmeters.DrawMoney = Convert.ToInt32(prizeamount);
                    SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tbllotteryprize(securityNumber,prizeQuota,PrizeDrawFlag,DrawTime,SerialNumber,CanelSendFlag,LotterySerialNumber,UploadFlag) VALUES('", GlobalParmeters.SecurityNumber, "','", GlobalParmeters.DrawMoney.ToString(), "',99,'", time, "',", GlobalParmeters.DrawSerialNumbe, ",'true','", GlobalParmeters.LotterySerialNumber, "','true')" }));
                    CommonUtils.showTime("返回兑奖成功，准备再次扫码");
                    GlobalParmeters.isScanCoded = true;
                    this.timerReadSecurityLottery.Enabled = true;
                }
                else
                {
                    GlobalParmeters.ShowMessage = ResultMessage.getMean(key, ResultMessage.dic_sgValidation_a);
                    this.timerInitInfoMessage.Enabled = true;
                    CommonUtils.showTime(string.Concat(new object[] { "兑奖结束，返回码：", key, " 返回信息:", GlobalParmeters.ShowMessage, " 系统返回错误：", this.ErroMessage() }));
                }
            }
            catch (Exception exception)
            {
                CommonUtils.showTime(exception.ToString());
                GlobalParmeters.ShowMessage = "兑奖失败，请稍候再试....";
                this.timerInitInfoMessage.Enabled = true;
            }
        }

        public string ErroMessage()
        {
            string str = "";
            try
            {
                int type = 0;
                int size = 0x400;
                byte[] bytes = new byte[0x400];
                int num3 = SGBentDll.sgERRMsg(type, ref bytes[0], size);
                byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, bytes, 0, bytes.Length);
                string str2 = Encoding.Default.GetString(buffer2, 0, buffer2.Length);
                str = string.Concat(new object[] { "返回码： ", num3, " 错误信息：", str2 });
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            return str;
        }

        /**
         * 调用国家兑奖接口
         */
        public void sgDuijiang()
        {
            try
            {
                CommonUtils.showTime("开始扫码");

                this.timerReadSecurityLottery.Enabled = false;
                string time = "";
                string str2 = "";
                string str4 = "";
                int num = -1;
                for (int i = 0; i < 4; i++)
                {
                    str2 = this.Gsn.ReadSecurityNumber();
                    if (str2.Length > 0x1f)
                    {
                        int length = str2.Length;
                        for (int j = 0; j < length; j++)
                        {
                            if (char.IsDigit(str2[j]))
                            {
                                str4 = str4 + str2[j];
                            }
                        }
                        str2 = str4;
                        length = str2.Length;
                        for (int k = 0; k < (length - 1); k++)
                        {
                            if (str2.Substring(k, 2).Equals("35") || str2.Substring(k, 2).Equals("36"))
                            {
                                str4 = str2.Substring(k, length - k) + str2.Substring(0, k);
                                num = BarcodeCheckDLL.BarcodeCheck(str4.Substring(0, length - 11));
                                if (num == 0)
                                {
                                    break;
                                }
                            }
                        }
                        if (num == 0)
                        {
                            str2 = str4;
                            break;
                        }
                        CommonUtils.showTime("sg扫码校验失败 ");
                        GlobalParmeters.ShowMessage = "请确认保安区码刮开干净，稍后再兑 ";
                        this.timerInitInfoMessage.Enabled = true;
                        return;
                    }
                }
                CommonUtils.showTime("扫码结束");

                if ((str2.Length == 0x1d) || (str2.Length == 0x1f))
                {
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (GlobalParmeters.isScanCoded)
                    {
                        GlobalParmeters.isScanCoded = false;
                        if (GlobalParmeters.prizeType == 1)
                        {
                            if (str2.Equals(GlobalParmeters.LotterySerialNumber + GlobalParmeters.SecurityNumber))
                            {
                                CommonUtils.showTime("扫码校验成功");
                                this.ShowPrizeInfo();
                            }
                            else
                            {
                                CommonUtils.showTime("扫码校验失败 ");
                                GlobalParmeters.ShowMessage = "校验失败，请稍候再试... ";
                                this.timerInitInfoMessage.Enabled = true;
                            }
                        }
                        else
                        {
                            string str5 = "";
                            if (str2.Substring(11, 2) != "35")
                            {
                                str5 = str2.Substring(7, 11);
                            }
                            else
                            {
                                str5 = str2.Substring(0, 11);
                            }
                            if (str5 == GlobalParmeters.SecurityNumber)
                            {
                                CommonUtils.showTime("扫码校验成功");
                                this.ShowPrizeInfo();
                            }
                            else
                            {
                                CommonUtils.showTime("扫码校验失败");
                                GlobalParmeters.ShowMessage = "校验失败，请稍候再试...";
                                this.timerInitInfoMessage.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        GlobalParmeters.ShowMessage = "兑奖中，请勿移走彩票";
                        this.lblMessage.Text = GlobalParmeters.ShowMessage;
                        GlobalParmeters.LotterySerialNumber = str2.Substring(0, str2.Length - 11);
                        GlobalParmeters.SecurityNumber = str2.Substring(str2.Length - 11, 11);
                        Application.DoEvents();
                        GlobalParmeters.DrawSerialNumbe++;
                        string str6 = SQLiteHelper.GetSingle("select PrizeDrawFlag from tblLotteryPrize where LotterySerialNumber ='" + GlobalParmeters.LotterySerialNumber + "' or LotterySerialNumber ='" + GlobalParmeters.LotterySerialNumber.Substring(0, GlobalParmeters.LotterySerialNumber.Length - 2) + "'").ToString();
                        CommonUtils.showTime(string.Concat(new object[] { "标志位PrizeDrawFlag：", str6, "| isconnected:", this.isConnected }));
                        if (str6 == "")
                        {
                            this.PrizeMain(time);
                        }
                        else
                        {
                            CommonUtils.showTime("二次兑奖彩票");
                            if (str6.Trim().Equals("99"))
                            {
                                this.ShowPrizeInfo();
                            }
                            else
                            {
                                GlobalParmeters.ShowMessage = ResultMessage.getMean(Convert.ToInt32(str6), ResultMessage.dic_sgValidation_a);
                                this.timerInitInfoMessage.Enabled = true;
                            }
                        }
                    }
                }
                else if (GlobalParmeters.isScanCoded)
                {
                    CommonUtils.showTime("二次扫码不正确，准备第三次扫码");
                    GlobalParmeters.isScanCoded = false;
                    GlobalParmeters.ShowMessage = "未检测到彩票,请放回或微调彩票位置";
                    this.timerReadSecurityLottery.Enabled = true;
                }
                else
                {
                    CommonUtils.showTime("未扫到码或扫码不正确");
                    GlobalParmeters.ShowMessage = "请确认保安区已刮干净无损伤";
                    this.timerInitInfoMessage.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.showTime("异常：" + exception.ToString());
                SocketClient.WriteCheckExcetionlog(exception.ToString());
                GlobalParmeters.ShowMessage = "很抱歉,兑奖失败,请去网点兑奖";
                this.timerInitInfoMessage.Enabled = true;
            }
            finally
            {
                this.isDuiJiang = false;
                IS_CHECK_RECEIVECASH = true;
                IS_CHECK_RECEIVECOIN = true;
                if (GlobalParmeters.GotMoney == 0)
                {
                    this.timerQuery.Enabled = true;
                }
            }
        }

        /**
         * 英特达人工兑奖
         */
        public void intrdakDuijiang()
        {

            try
            {
                CommonUtils.showTime("开始扫码");
                this.timerReadSecurityLottery.Enabled = false;

                string time = "";
                string str2 = "";

                //重试最多8次
                for (int i = 0; i < 4; i++)
                {
                    str2 = this.Gsn.ReadSecurityNumber();
                    if (str2.Length > 0x1f)
                    {
                        str2 = str2.Substring(str2.Length - 0x1f, 0x1d);
                        CommonUtils.showTime(str2);
                        if ((GlobalParmeters.prizeType != 1) || (str2.Substring(0, 2) == "35"))
                        {
                            break;
                        }
                    }

                }

                CommonUtils.showTime("扫码结束");

                CommonUtils.showTime("扫码结果:" + str2);

                //扫码失败
                if (str2.Length != 0x1d)
                {
                    if (GlobalParmeters.isScanCoded)
                    {
                        CommonUtils.showTime("二次扫码不正确，准备第三次扫码");
                        GlobalParmeters.isScanCoded = false;
                        GlobalParmeters.ShowMessage = "未检测到彩票,请放回或微调彩票位置";
                        this.timerReadSecurityLottery.Enabled = true;
                        this.timerInitInfoMessage.Enabled = true;
                    }
                    else
                    {
                        CommonUtils.showTime("未扫到码或扫码不正确");
                        GlobalParmeters.ShowMessage = "请确认保安区已刮干净无损伤";
                        this.timerInitInfoMessage.Enabled = true;
                    }
                    return;
                }

                //扫码成功
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (GlobalParmeters.isScanCoded)
                {
                    //本次已经扫过了，重复扫
                    GlobalParmeters.isScanCoded = false;
                    if (GlobalParmeters.prizeType == 1)
                    {
                        if (str2 == GlobalParmeters.SecurityNumber)
                        {
                            CommonUtils.showTime("扫码校验成功 ");
                            this.ShowPrizeInfo();
                        }
                        else
                        {
                            CommonUtils.showTime("扫码校验失败 ");
                            GlobalParmeters.ShowMessage = "校验失败，请稍候再试... ";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                    else
                    {
                        string str3 = "";
                        if (str2.Substring(11, 2) != "35")
                        {
                            str3 = str2.Substring(7, 11);
                        }
                        else
                        {
                            str3 = str2.Substring(0, 11);
                        }
                        if (str3 == GlobalParmeters.SecurityNumber)
                        {
                            CommonUtils.showTime("扫码校验成功");
                            this.ShowPrizeInfo();
                        }
                        else
                        {
                            CommonUtils.showTime("扫码校验失败");
                            GlobalParmeters.ShowMessage = "校验失败，请稍候再试...";
                            this.timerInitInfoMessage.Enabled = true;
                        }
                    }
                }
                else
                {
                    //首次扫，连接服务器，进行兑奖
                    GlobalParmeters.ShowMessage = "兑奖中，请勿移走彩票";
                    this.lblMessage.Text = GlobalParmeters.ShowMessage;
                    if (GlobalParmeters.prizeType == 1)
                    {
                        GlobalParmeters.SecurityNumber = str2;
                    }
                    else if (str2.Substring(11, 2) != "35")
                    {
                        GlobalParmeters.SecurityNumber = str2.Substring(7, 11);
                        GlobalParmeters.LotterySerialNumber = str2.Substring(0x12, 11) + str2.Substring(0, 7);
                    }
                    else
                    {
                        GlobalParmeters.SecurityNumber = str2.Substring(0, 11);
                        GlobalParmeters.LotterySerialNumber = str2.Substring(11, 0x12);
                    }
                    Application.DoEvents();

                    GlobalParmeters.DrawSerialNumbe++;
                    string str4 = SQLiteHelper.GetSingle("select PrizeDrawFlag from tblLotteryPrize where SecurityNumber ='" + GlobalParmeters.SecurityNumber + "'").ToString();
                    CommonUtils.showTime(string.Concat(new object[] { "标志位PrizeDrawFlag：", str4, "| isconnected:", this.isConnected }));
                    //本地没有对兑换过该彩票
                    if (str4 == "")
                    {
                        if (!this.isConnected)
                        {
                            CommonUtils.showTime("准备打开连接");
                            if (this.sc.OpenDraw())
                            {
                                this.isConnected = true;
                                CommonUtils.showTime("打开连接成功");
                                this.QueryDrawInfo(time);
                            }
                            else
                            {
                                CommonUtils.showTime("打开连接失败");
                                GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";
                                this.timerInitInfoMessage.Enabled = true;
                            }
                        }
                        else
                        {
                            CommonUtils.showTime("无须再次打开，直接发送");
                            this.QueryDrawInfo(time);
                        }
                    }
                    else
                    {
                        //在本机上已经兑过
                        CommonUtils.showTime("二次兑奖彩票");
                        string str5 = str4;
                        if (str5 != null)
                        {
                            if (str5 != "0")
                            {
                                if (str5 == "1")
                                {
                                    GlobalParmeters.ShowMessage = "此彩票已兑过奖了";
                                    this.timerInitInfoMessage.Enabled = true;
                                    return;
                                }
                                if (str5 == "2")
                                {
                                    GlobalParmeters.ShowMessage = "对不起,您未中奖";
                                    this.timerInitInfoMessage.Enabled = true;
                                    return;
                                }
                                if (str5 == "3")
                                {
                                    GlobalParmeters.ShowMessage = "请到体彩中心兑奖";
                                    this.timerInitInfoMessage.Enabled = true;
                                    return;
                                }
                                if (str5 == "99")
                                {
                                    this.ShowPrizeInfo();
                                    return;
                                }
                            }
                            else
                            {
                                //需要在此处增加业务重做
                                //此处有BUG，如果彩票的状态为0，之后再无法重做该操作
                                GlobalParmeters.ShowMessage = "此彩票暂时无法兑奖，请稍候再试";
                                this.timerInitInfoMessage.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.showTime("异常：" + exception.ToString());
                SocketClient.WriteCheckExcetionlog(exception.ToString());
                GlobalParmeters.ShowMessage = "请确认彩票中奖，稍后再兑";

                this.timerInitInfoMessage.Enabled = true;
            }
            finally
            {
                IS_CHECK_RECEIVECASH = true;
                IS_CHECK_RECEIVECOIN = true;

                if (GlobalParmeters.GotMoney == 0)
                {
                    this.timerQuery.Enabled = true;
                }
            }
        }

        private void StopService()
        {
            if(panel2.InvokeRequired)
            {
                panel2.BeginInvoke(new MethodInvoker(StopService), null);
            } 
            else
            {
                panel2.Size = new Size(768, 690);
                panel2.Location = new Point(0, 630);
                panel2.BringToFront();
                panel2.Show();
                picDuiJiang.Visible = false;
            }
        }

        private void StartService()
        {
            if (panel2.InvokeRequired)
            {
                panel2.BeginInvoke(new MethodInvoker(StartService), null);
            }
            else
            {
                panel2.Location = new Point(-1500, -1500);
                panel2.Hide();
                picDuiJiang.Visible = false;
            }
        }

        private bool IsStopService()
        {
            if (GlobalParmeters.ShowMessage == "暂停服务")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        private void timerClearPicGameBackground_Tick(object sender, EventArgs e)
        {
            timerClearPicGameBackground.Enabled = false;
            this.picGame.BackgroundImage = null;
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

        private void lblPhono_Click(object sender, EventArgs e)
        {

        }

        private void btnReturnMoney_Click(object sender, EventArgs e)
        { 
            if(UserContext.getInstance().Balance > 0)
            {
                //有余额
                if (!this.isConsumption)
                {
                    GlobalParmeters.ShowMessage = "还未消费，必须消费才能退币";
                    this.timerInitInfoMessage.Enabled = true;
                }
                else
                {
                    //退币
                    tipForm = new FrmPopCoinReturn();
                    DialogResult dr = tipForm.ShowDialog();
                    tipForm.Dispose();

                    switch (dr)
                    {
                        case DialogResult.OK://确定退币
                            this.isJieSuan = true;
                            //禁用
                            IS_CHECK_RECEIVECASH = false;
                            IS_CHECK_RECEIVECOIN = false;


                            GlobalParmeters.ShowMessage = "退币中，请稍候...";
                            this.timerInitInfoMessage.Enabled = true;
                            this.lblMessage.Text = GlobalParmeters.ShowMessage;
                            this.timerPayout.Enabled = true;
                            break;
                        case DialogResult.Cancel://取消退币，啥也不做
                            break;
                    }
                }
            }
        }
    }
}
