using System;
using System.Threading;
using System.Windows.Forms;
using YTDSSTGenII.Forms;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII
{
    static class Program
    {
        //private static ILog logger = LogManager.GetLogger(typeof(Program));
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = false;
            Mutex mutex = null;
            mutex = new Mutex(true, "SportsLotterySelfHelpyMachine", out createdNew);
            if (createdNew)
            {
                RuntimeLogUtils.WriteLog("启动购彩程序...");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += new EventHandler(OnApplictionExit);
                //根据配置文件加载不同的购彩程序
                Application.Run(new FormBuyLotteryMain());
                RuntimeLogUtils.WriteLog("退出购彩程序...");
            }
            else
            {
                MessageBox.Show("购彩程序己启动");
                Application.Exit();
            }
        }

        private static void OnApplictionExit(object sender, EventArgs e)
        {
            RuntimeLogUtils.WriteLog("退出购彩程序...");
        }
    }
}
