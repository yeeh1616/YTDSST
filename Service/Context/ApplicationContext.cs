using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using YTDSSTGenII.Service;
using YTDSSTGenII.Service.Wexin;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service.Context
{
    public class ApplicationContext
    {
        private static bool isInit = false;//是否调用过初始化方法

        public static String MachineCode { get; set; }

        private static ApplicationContext instance;
        private ApplicationContext() { }
        public static ApplicationContext getInstance() {
            if (null == instance)
            {
                instance = new ApplicationContext();
            }
            return instance;
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public static void init() {
            if (!isInit)
            {
                MachineCode = GlobalParmeters.machineNumber; //GlobalParmeters.machineNumber
                //启用微信服务器长链接检查线程
                ThreadPool.QueueUserWorkItem(new WaitCallback(weiXin_ConnectToServer));
                isInit = true;
            }            
        }

        private static void weiXin_ConnectToServer(object state)
        {
            while (true)
            {
                try
                {
                    WeXinSocketClient.getInstance().ConnectToServer();
                }
                catch
                { }

                Thread.Sleep(1000*10);
            }
        }

        //微信服务器
        public String WEIXIN_SERVER_IP = "";
        public String WEIXIN_SERVER_PORT = "";

        private static WexinService wexinService;
        public static WexinService GetWexinService()
        {
            if (wexinService == null)
            {
                wexinService = new WexinService();
            }

            return wexinService;
        }


        //设置两个变量用来在弹出框中向主界面传送信号来控制对钱箱的控制
        public Boolean IS_IN_PAY_FORM { get; set; } //是否在支付界面上
        public Boolean IS_NEED_CHECK_MONEYBOX = false;//是否需要去检查钱箱的余额

        //是否有弹框
        public Boolean IS_HAS_POPUP_FORM = false;
    }
}
