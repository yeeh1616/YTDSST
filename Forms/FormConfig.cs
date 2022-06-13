using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

using System.IO;
using System.IO.Ports;

using YTDSSTGenII.Utils;


namespace YTDSSTGenII.Forms
{
    public partial class FormConfig : Form
    {
        public string Coin = "";
        public string drawPort = "";
        public string DuiJiang = "";
        private int i = 5;
        private IniFile ini;
        public string LotteryPrizeIp = "";
        public string Motor = "";
        public string MotorCount = "";
        public string Panel = "";
        public string PayoutCash = "";
        public string phono = "";
        public string PrizePunch = "";
        public string ReceCash = "";
        public string ServerIP = "";
        public string ServerPort = "";
        public string ServiceTel = "";
        public string ShutTime = "";
        public string StartTime = "";
        public string UPS = "";

        public FormConfig()
        {
            InitializeComponent();
            this.ini = new IniFile("D://advitise//1.ini");
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void IsRestart()
        {
            if (((this.ServerIP != this.txtServerIp.Text.Trim()) || (this.ServerPort != this.txtServerPort.Text.Trim())) || (this.StartTime != this.txtStartTime.Text.Trim()) || (((this.ShutTime != this.txtShutTime.Text.Trim()) || (this.UPS != this.cmbUps.Text.Trim())) || ((this.LotteryPrizeIp != this.txtLotteryPrizeIp.Text.Trim()) || (this.drawPort != this.txtDraw.Text.Trim()))))
            {
                FormShowDialog dialog = new FormShowDialog();
                GlobalParmeters.frmShowDialog = "必须重启计算机,立刻重启？";
                dialog.ShowDialog();
                if (GlobalParmeters.frmShowDialog == "是")
                {
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        if (((processes[i].ProcessName == "UpLoadBySocket") || (processes[i].ProcessName == "TestMeidiaPlayWithDirectX")) || ((processes[i].ProcessName == "WinPowerContorlForUPS") || (processes[i].ProcessName == "WinFormCameraDemo")))
                        {
                            processes[i].Kill();
                        }
                    }
                    Process.Start("shutdown", "-r -t 0");
                    Environment.Exit(0);
                }
            }
            if ((((this.ReceCash != this.cmbReceCash.Text.Trim()) || (this.PayoutCash != this.cmbPayoutCash.Text.Trim())) || ((this.Coin != this.cmbCoin.Text.Trim()) || (this.Motor != this.cmbMotor.Text.Trim()))) || (((this.Panel != this.cmbPanel.Text.Trim()) || (this.DuiJiang != this.cmbDuiJiang.Text.Trim())) || (((this.ServiceTel != this.txtServiceTel.Text.Trim()) || (this.phono != this.txtPhono.Text.Trim())) || (this.PrizePunch != this.cmbPrizePunch.Text.Trim()))))
            {
                FormShowDialog dialog2 = new FormShowDialog();
                GlobalParmeters.frmShowDialog = "必须重启购彩程序,立刻重启？";
                dialog2.ShowDialog();
                if (GlobalParmeters.frmShowDialog == "是")
                {
                    Application.Restart();
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            Process.Start(Application.StartupPath + @"\ScreenKey.exe");
            GlobalParmeters.IsOpenScreenKey = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();
            GlobalParmeters.frmShowDialog = "确定要恢复出厂设置吗？";
            dialog.ShowDialog();
            if (GlobalParmeters.frmShowDialog == "是")
            {
                this.ini.WriteIniValue("配置", "服务器IP", "114.242.9.32");
                this.ini.WriteIniValue("配置", "服务器端口", "6499");
                this.ini.WriteIniValue("配置", "自动开机时间", "09:00:00");
                this.ini.WriteIniValue("配置", "自动关机时间", "21:00:00");
                this.ini.WriteIniValue("配置", "纸币接收器", "COM5");
                this.ini.WriteIniValue("配置", "纸币找零器", "COM1");
                this.ini.WriteIniValue("配置", "硬币器", "COM3");
                this.ini.WriteIniValue("配置", "机头", "COM4");
                this.ini.WriteIniValue("配置", "控制板", "COM2");
                this.ini.WriteIniValue("配置", "UPS", "COM8");
                this.ini.WriteIniValue("配置", "兑奖", "COM7");
                this.ini.WriteIniValue("配置", "日志清除", "60");
                this.ini.WriteIniValue("配置", "视频清除", "7");
                this.ini.WriteIniValue("配置", "纸币币种", "10");
                this.ini.WriteIniValue("配置", "超时间隔", "120");
                this.ini.WriteIniValue("配置", "机头数量", "8");
                this.ini.WriteIniValue("配置", "客服电话", "4008-123335");
                this.ini.WriteIniValue("配置", "兑奖网点电话", "4008-123335");
                this.ini.WriteIniValue("配置", "兑奖端口", "6490");
                this.ini.WriteIniValue("配置", "兑奖打孔", "COM9");
                this.timer1.Enabled = true;
            }
        }

        private void RefreshCom()
        {
            this.cmbReceCash.Items.Clear();
            this.cmbPayoutCash.Items.Clear();
            this.cmbCoin.Items.Clear();
            this.cmbMotor.Items.Clear();
            this.cmbPanel.Items.Clear();
            this.cmbUps.Items.Clear();
            this.cmbDuiJiang.Items.Clear();
            foreach (string str in SerialPort.GetPortNames())
            {
                this.cmbReceCash.Items.Add(str);
                this.cmbPayoutCash.Items.Add(str);
                this.cmbCoin.Items.Add(str);
                this.cmbMotor.Items.Add(str);
                this.cmbPanel.Items.Add(str);
                this.cmbUps.Items.Add(str);
                this.cmbDuiJiang.Items.Add(str);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.txtServerIp.Text = this.ServerIP = this.ini.ReadIniValue("配置", "服务器IP");
            this.txtServerPort.Text = this.ServerPort = this.ini.ReadIniValue("配置", "服务器端口");
            this.StartTime = this.txtStartTime.Text = this.ini.ReadIniValue("配置", "自动开机时间");
            this.ShutTime = this.txtShutTime.Text = this.ini.ReadIniValue("配置", "自动关机时间");
            this.cmbReceCash.Text = this.ReceCash = this.ini.ReadIniValue("配置", "纸币接收器");
            this.cmbPayoutCash.Text = this.PayoutCash = this.ini.ReadIniValue("配置", "纸币找零器");
            this.cmbCoin.Text = this.Coin = this.ini.ReadIniValue("配置", "硬币器");
            this.cmbMotor.Text = this.Motor = this.ini.ReadIniValue("配置", "机头");
            this.cmbPanel.Text = this.Panel = this.ini.ReadIniValue("配置", "控制板");
            this.cmbUps.Text = this.UPS = this.ini.ReadIniValue("配置", "UPS");
            this.cmbDuiJiang.Text = this.DuiJiang = this.ini.ReadIniValue("配置", "兑奖");
            this.txtLog.Text = this.ini.ReadIniValue("配置", "日志清除");
            this.txtVideo.Text = this.ini.ReadIniValue("配置", "视频清除");
            this.cmbCashType.Text = this.ini.ReadIniValue("配置", "纸币币种");
            //this.txtTimeOut.Text = this.ini.ReadIniValue("配置", "超时间隔");
            //this.cmbMoterCount.Text = this.MotorCount = this.ini.ReadIniValue("配置", "机头数量");
            this.txtServiceTel.Text = this.ServiceTel = this.ini.ReadIniValue("配置", "客服电话");
            this.cmbPrizeType.Text = this.ini.ReadIniValue("配置", "兑奖类型");
            if (this.ini.ValueExists("配置", "兑奖服务器IP"))
            {
                this.LotteryPrizeIp = this.txtLotteryPrizeIp.Text = this.ini.ReadIniValue("配置", "兑奖服务器IP");
            }
            else
            {
                this.ini.WriteIniValue("配置", "兑奖服务器IP", "114.242.9.32");
                this.LotteryPrizeIp = this.txtLotteryPrizeIp.Text = "114.242.9.32";
            }
            this.txtDraw.Text = this.drawPort = this.ini.ReadIniValue("配置", "兑奖端口");
            this.txtPhono.Text = this.phono = this.ini.ReadIniValue("配置", "兑奖网点电话");
            this.cmbPrizePunch.Text = this.PrizePunch = this.ini.ReadIniValue("配置", "兑奖打孔");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.i--;
            this.lblMessage.Text = "保存成功，" + this.i + "秒后自动关闭页面";
            if (this.i <= 0)
            {
                this.i = 5;
                this.timer2.Enabled = false;
                base.Close();
                base.Dispose();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void FormConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
            GlobalParmeters.IsOpenScreenKey = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Regexlib.IsValidTel(this.txtPhono.Text.Trim()) && !Regexlib.IsHandset(this.txtPhono.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“兑奖联系电话”不合法！";
                }
                else if (!Regexlib.IsValidTel(this.txtServiceTel.Text.Trim()) && !Regexlib.IsHandset(this.txtServiceTel.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“客服电话”不合法！";
                }
                /*
                else if (!Regexlib.IsValidIp(this.txtServerIp.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“通讯地址”不合法！";
                }
                else if (!Regexlib.IsValidIp(this.txtLotteryPrizeIp.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“兑奖地址”不合法！";
                }
                */
                else if (!Regexlib.IsNumber(this.txtServerPort.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“通讯端口”不合法！";
                }
                else if (!Regexlib.IsNumber(this.txtDraw.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“兑奖端口”不合法！";
                }
                else if (!Regexlib.IsNumber(this.txtLog.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“日志（天）”不合法！";
                }
                else if (!Regexlib.IsNumber(this.txtVideo.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“视频（天）”不合法！";
                }
                else if (!Regexlib.IsTime(this.txtStartTime.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“自动开机时间”不合法！";
                }
                else if (!Regexlib.IsTime(this.txtShutTime.Text.Trim()))
                {
                    this.lblMessage.Text = "对不起，您输入的“自动关机时间”不合法！";
                }
                else
                {
                    this.ini.WriteIniValue("配置", "服务器IP", this.txtServerIp.Text.Trim());
                    this.ini.WriteIniValue("配置", "服务器端口", this.txtServerPort.Text.Trim());
                    this.ini.WriteIniValue("配置", "自动开机时间", this.txtStartTime.Text.Trim().PadLeft(8, '0'));
                    this.ini.WriteIniValue("配置", "自动关机时间", this.txtShutTime.Text.Trim().PadLeft(8, '0'));
                    this.ini.WriteIniValue("配置", "纸币接收器", this.cmbReceCash.Text.Trim());
                    this.ini.WriteIniValue("配置", "纸币找零器", this.cmbPayoutCash.Text.Trim());
                    this.ini.WriteIniValue("配置", "硬币器", this.cmbCoin.Text.Trim());
                    this.ini.WriteIniValue("配置", "机头", this.cmbMotor.Text.Trim());
                    this.ini.WriteIniValue("配置", "控制板", this.cmbPanel.Text.Trim());
                    this.ini.WriteIniValue("配置", "UPS", this.cmbUps.Text.Trim());
                    this.ini.WriteIniValue("配置", "兑奖", this.cmbDuiJiang.Text.Trim());
                    this.ini.WriteIniValue("配置", "日志清除", this.txtLog.Text.Trim());
                    this.ini.WriteIniValue("配置", "视频清除", this.txtVideo.Text.Trim());
                    this.ini.WriteIniValue("配置", "纸币币种", this.cmbCashType.Text.Trim());
                    GlobalParmeters.CashType = int.Parse(this.cmbCashType.Text.Trim());
                    //this.ini.WriteIniValue("配置", "超时间隔", this.txtTimeOut.Text.Trim());
                    //this.ini.WriteIniValue("配置", "机头数量", this.cmbMoterCount.Text.Trim());
                    this.ini.WriteIniValue("配置", "客服电话", this.txtServiceTel.Text.Trim());
                    this.ini.WriteIniValue("配置", "兑奖端口", this.txtDraw.Text.Trim());
                    this.ini.WriteIniValue("配置", "兑奖网点电话", this.txtPhono.Text.Trim());
                    this.ini.WriteIniValue("配置", "兑奖打孔", this.cmbPrizePunch.Text.Trim());
                    this.ini.WriteIniValue("配置", "兑奖服务器IP", this.txtLotteryPrizeIp.Text.Trim());
                    string str = this.cmbPrizeType.Text.Trim();
                    if (str == "单码")
                    {
                        GlobalParmeters.prizeType = 1;
                    }
                    else
                    {
                        GlobalParmeters.prizeType = 2;
                    }
                    this.ini.WriteIniValue("配置", "兑奖类型", str);
                    this.IsRestart();
                    this.lblMessage.Text = "保存成功，5秒后自动关闭页面";
                    this.timer2.Enabled = true;
                }
            }
            catch
            {
                this.lblMessage.Text = "保存失败";
            }
        }
    }
}
