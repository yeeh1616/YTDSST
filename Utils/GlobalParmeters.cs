using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using YTDSSTGenII.Service.Enums;

namespace YTDSSTGenII.Utils
{
    class GlobalParmeters
    {
        public static List<string> AddLotteryTime = null;
        public static string BillBoxStatusStr = "";
        public static int[] BillMount = new int[] { 5, 10, 20 };
        public static int BuyLotteryCount = 1;
        public static double CardinalNumber = 1.0;
        public static int[] CashAcceptorStatus = new int[2];
        public static bool CashBoxFlag = false;
        public static int CashCount = 0;
        public static int CashFaultCount = 0;
        public static int CashType = 10;
        public static bool ChackMotorStatus1 = true;
        public static bool ChackMotorStatus2 = true;
        public static bool ChackMotorStatus3 = true;
        public static bool ChackMotorStatus4 = true;
        public static bool ChackMotorStatus5 = true;
        public static bool ChackMotorStatus6 = true;
        public static bool ChackMotorStatus7 = true;
        public static bool ChackMotorStatus8 = true;
        public static bool ChackMotorStatus9 = true;
        public static int CoinCount = 0;
        public static int CoinFaultCount = 0;
        public static double CoinType = 1.0;
        public static string ComposeKey = "0103";
        public static int ComposeKeyLength = 2;
        public static bool downtime = false;
        public static int DrawMoney = 0;
        public static string Drawphono = "";
        public static int DrawSerialNumbe = 0;
        public static bool ForbitInputFlag = false;
        public static string frmShowDialog = "";
        public static int GotMoney = 0;
        public static bool heartbeatFlag = false;
        public static bool iscansend = false;
        public static bool isChangeLottLength = false;
        public static bool isJieSuanFlag = false;
        public static bool isLoadDataAgin = false;
        public static bool IsOpenScreenKey = false;
        public static bool isScanCoded = false;
        public static bool IsStartHook = false;
        public static bool isUnzip = false;
        public static int KeyErrorStatusCount = 0;
        public static int[] KeyStatus = new int[3];
        public static int LoginFailedCount = 0;
        public static bool LongRangeDisableCashBox = false;
        public static int[] lotteryLength = new int[9];
        public static string LotterySerialNumber = "";
        public static string machineNumber = "";
        public static int MaxPrize = 0;
        public static int MinCashCount = 0;
        public static int MinCoinCount = 0;
        public static int[] MotorStatus = new int[9];
        public static int num1 = 0;
        public static int num2 = 0;
        public static int num3 = 0;
        public static int num4 = 0;
        public static int num5 = 0;
        public static int num6 = 0;
        public static int num7 = 0;
        public static int num8 = 0;
        public static int num9 = 0;
        public static bool PayoutFlag = false;
        public static bool payoutSure = false;
        public static int PollFailTimes = 0;
        public static int price1 = 0;
        public static int price2 = 0;
        public static int price3 = 0;
        public static int price4 = 0;
        public static int price5 = 0;
        public static int price6 = 0;
        public static int price7 = 0;
        public static int price8 = 0;
        public static int price9 = 0;
        public static string PrizeTime = "";
        public static int prizeType = 0;
        public static bool ReadInputtedMountFlag = false;
        public static bool ReadKeyFlag = true;
        public static string SecurityNumber = "";
        public static int SelectCount = 1;
        public static string ShowMessage = "请选择彩种,点击结算购买";
        public static int ThisTimeGotMoney = 0;
        public static int xiaofei = 0;

        public static string ServicePhoneNumber = "400-";

        private static List<string> CopyToList(string sql)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in SQLiteHelper.Query(sql).Tables[0].Rows)
            {
                list.Add(row[0].ToString());
            }
            return list;
        }


        //SG兑奖
        public static int pingtime = 60;
        public static string sgtelephone = "";
        public static string sgversion;


        //兑奖模式
        //public static EnumDuiJiangMode DuiJiangMode = EnumDuiJiangMode.INTRADAK_MANUL;
        public static EnumDuiJiangMode DuiJiangMode = EnumDuiJiangMode.INTRADAK_SG;

        public static string no;
        public static string port;
        public static string provinceid;
        public static string logonid;
        public static string logonpwd;
        public static string techpasswd;

    }
}
