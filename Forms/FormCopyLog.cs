using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Service;

namespace YTDSSTGenII.Forms
{
    public partial class FormCopyLog : Form
    {
        private static int totalSize;
        private string DriveName = "";
        private IniFile ini;

        public FormCopyLog()
        {
            InitializeComponent();
            this.ini = new IniFile("D://advitise//1.ini");
        }

        private void FormCopyLog_Load(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            if (GlobalParmeters.LongRangeDisableCashBox)
            {
                this.listBox1.Items.Add("远程禁用纸币器");
            }
            if (GlobalParmeters.CashAcceptorStatus[0] == 1)
            {
                this.listBox1.Items.Add("纸币接收器通信故障");
            }
            if (GlobalParmeters.CashAcceptorStatus[1] == 1)
            {
                this.listBox1.Items.Add("硬币接收器通信故障");
            }
            for (int i = 0; i < 8; i++)
            {
                if (GlobalParmeters.MotorStatus[i] == 1)
                {
                    this.listBox1.Items.Add((i + 1) + "号机头故障或无票");
                }
            }
            if ((GlobalParmeters.CashFaultCount >= 3) || GlobalParmeters.downtime)
            {
                this.listBox1.Items.Add("纸币找零器故障");
            }
            if (GlobalParmeters.CoinFaultCount >= 3)
            {
                this.listBox1.Items.Add("硬币找零器故障");
            }
            if (GlobalParmeters.CashBoxFlag)
            {
                this.listBox1.Items.Add("钞箱被取下");
            }
            if (GlobalParmeters.CashCount < GlobalParmeters.MinCashCount)
            {
                this.listBox1.Items.Add("退币箱纸币少于" + GlobalParmeters.MinCashCount + "张");
            }
            if (GlobalParmeters.CoinCount < GlobalParmeters.MinCoinCount)
            {
                this.listBox1.Items.Add("退币箱硬币小于" + GlobalParmeters.MinCoinCount + "枚");
            }
        }

        private void btnCopyLog_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DriveInfo info in DriveInfo.GetDrives())
                {
                    if (info.DriveType == DriveType.Removable)
                    {
                        this.DriveName = info.Name;
                        this.lblMessage.Text = "正在拷贝,请稍候...";
                        Application.DoEvents();
                        break;
                    }
                }
                if (this.DriveName == "")
                {
                    this.lblMessage.Text = "对不起,未检测到U盘";
                }
                else
                {
                    GlobalParmeters.isUnzip = true;
                    long num = 0L;
                    string str = this.ini.ReadIniValue("配置", "本机编号");
                    string str2 = this.DriveName + str + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    List<string> list = new List<string> { 
                        @"E:\Excetionlog",
                        @"E:\MotorStatus",
                        @"E:\Cashlog",
                        @"E:\Keylog",
                        @"E:\uploadlog",
                        @"E:\PowerControl",
                        @"E:\MaxScreenFlag",
                        @"E:\Payout",
                        @"E:\ServiceSuspended",
                        @"E:\LotteryPrize",
                        @"D:\db"
                    };
                    foreach (string str3 in list)
                    {
                        if (Directory.Exists(str3))
                        {
                            DirectoryInfo info2 = new DirectoryInfo(str3);
                            foreach (FileInfo info3 in info2.GetFiles())
                            {
                                num += info3.Length;
                            }
                            CopyFolder(str3, str2 + @"\");
                        }
                    }
                    if (totalSize >= num)
                    {
                        this.lblMessage.Text = "拷贝完成";
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                this.lblMessage.Text = "拷贝过程中出现错误，请重试";
                Application.DoEvents();
            }
            finally
            {
                GlobalParmeters.isUnzip = false;
            }
        }

        private void btnResolveFault_Click(object sender, EventArgs e)
        {
            GlobalParmeters.downtime = false;
            GlobalParmeters.CashFaultCount = 0;
            this.ini.WriteIniValue("配置", "纸币找零器状态", "0");
            CommonUtils.WriteCashFaultCount(GlobalParmeters.CashFaultCount);
            GlobalParmeters.CoinFaultCount = 0;
            this.ini.WriteIniValue("配置", "硬币找零器状态", "0");
            CommonUtils.WriteCoinFaultCount(GlobalParmeters.CoinFaultCount);
            for (int i = 1; i <= 9; i++)
            {
                GlobalParmeters.MotorStatus[i - 1] = 0;
                SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=0 WHERE MotorId=" + i);
                this.ini.WriteIniValue("配置", "机头" + i + "状态", "0");
            }

            //收币器故障,重置收币器
            JCMBillAcceptorClass.ResetJCMBillAcceptor();
            GlobalParmeters.CashBoxFlag = false;
            this.ini.WriteIniValue("配置", "钞箱状态", "0");

            this.listBox1.Items.Clear();
            GlobalParmeters.isLoadDataAgin = true;
            this.lblMessage.Text = "故障已解除！";
        }

        private void btnCopyVideo_Click(object sender, EventArgs e)
        {
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                if (info.DriveType == DriveType.Removable)
                {
                    this.DriveName = info.Name;
                    this.lblMessage.Text = "正在拷贝,请稍候...";
                    Application.DoEvents();
                    break;
                }
            }
            if (this.DriveName == "")
            {
                this.lblMessage.Text = "对不起,未检测到U盘";
            }
            else
            {
                try
                {
                    string str = this.ini.ReadIniValue("配置", "本机编号");
                    string str2 = this.dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    this.CopyDir(@"E:\Camera\" + str2, this.DriveName + @"\" + str + @"-VideoLogs\" + str2);
                    this.lblMessage.Text = "拷贝完成";
                    Application.DoEvents();
                }
                catch
                {
                }
                finally
                {
                    this.lblMessage.Text = "拷贝完成";
                    Application.DoEvents();
                }
            }
        }

        private void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    aimPath = aimPath + Path.DirectorySeparatorChar;
                }
                if (!Directory.Exists(aimPath))
                {
                    Directory.CreateDirectory(aimPath);
                }
                foreach (string str in Directory.GetFileSystemEntries(srcPath))
                {
                    if (Directory.Exists(str))
                    {
                        this.CopyDir(str, aimPath + Path.GetFileName(str));
                    }
                    else
                    {
                        File.Copy(str, aimPath + Path.GetFileName(str), true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CopyFolder(string from, string to)
        {
            try
            {
                if (!Directory.Exists(to))
                {
                    Directory.CreateDirectory(to);
                }
                foreach (string str in Directory.GetDirectories(from))
                {
                    CopyFolder(str + @"\", to + Path.GetFileName(str) + @"\");
                    FileStream stream = new FileStream(str, FileMode.Open, FileAccess.Read);
                    totalSize += (int)stream.Length;
                }
                foreach (string str2 in Directory.GetFiles(from))
                {
                    File.Copy(str2, to + Path.GetFileName(str2), true);
                    FileStream stream2 = new FileStream(str2, FileMode.Open, FileAccess.Read);
                    totalSize += (int)stream2.Length;
                }
            }
            catch
            {
            }
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
