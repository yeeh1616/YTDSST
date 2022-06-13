using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using YTDSSTGenII.Utils;
using PointingDevice;

namespace YTDSSTGenII.Forms
{
    public partial class FormAddLottery : Form
    {
        public static string lName = "";
        public static int LotteryCount = 0;
        private string LotteryName = "";
        private int LotteryRemain;
        public static int MotorNumber = 0;
        private IniFile ini;


        public FormAddLottery()
        {
            InitializeComponent();
        }

        private void FormAddLottery_Load(object sender, EventArgs e)
        {
            this.GetLotteryInfo();
            this.GetMotorInfo();
            this.ini = new IniFile("D://advitise//1.ini");
        }

        private void picSoftKeyBoard_Click(object sender, EventArgs e)
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
            this.txbLotteryRemain.SelectAll();
            this.txbLotteryRemain.Focus();
            GlobalParmeters.IsOpenScreenKey = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                bool changedLotteryNameFlag = false;
                bool changedLotteryRemainFlag = false;
                if (this.cmbLotteryType.Text != this.LotteryName)
                {
                    changedLotteryNameFlag = true;
                }
                if (int.TryParse(this.txbLotteryRemain.Text, out num) && (num != this.LotteryRemain))
                {
                    changedLotteryRemainFlag = true;
                }
                if ((changedLotteryNameFlag || changedLotteryRemainFlag) && (int.Parse(this.txbLotteryRemain.Text) > 0))
                {
                    if (changedLotteryNameFlag)
                    {
                        FormShowDialog dialog = new FormShowDialog();
                        GlobalParmeters.frmShowDialog = "是否要改变彩票信息？";
                        dialog.ShowDialog();
                        dialog.Dispose();
                        if (GlobalParmeters.frmShowDialog == "是")
                        {
                            DbOperate operate = new DbOperate();
                            bool flag3 = operate.AddLottery(MotorNumber, this.cmbLotteryType.Text, changedLotteryNameFlag, num, changedLotteryRemainFlag, LotteryCount);
                            operate.CloseConn();
                            if (flag3)
                            {
                                GlobalParmeters.isLoadDataAgin = true;
                                GlobalParmeters.isChangeLottLength = true;
                                string text = this.lblMotorNumber.Text;
                                if (this.txbLotteryRemain.Text != "0")
                                {
                                    GlobalParmeters.MotorStatus[int.Parse(text) - 1] = 0;
                                    SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=0 WHERE MotorId=" + text);
                                    this.ini.WriteIniValue("配置", "机头" + int.Parse(text) + "状态", "0");
                                }
                                base.Close();
                            }
                            else
                            {
                                this.lblMessage.Text = "彩票添加失败";
                            }
                        }
                    }
                    else if (!changedLotteryNameFlag && changedLotteryRemainFlag)
                    {
                        FormShowDialog dialog2 = new FormShowDialog();
                        GlobalParmeters.frmShowDialog = "确定要加票吗？";
                        dialog2.ShowDialog();
                        dialog2.Dispose();
                        if (GlobalParmeters.frmShowDialog == "是")
                        {
                            DbOperate operate2 = new DbOperate();
                            bool flag4 = operate2.AddLottery(MotorNumber, this.cmbLotteryType.Text, changedLotteryNameFlag, num, changedLotteryRemainFlag, LotteryCount);
                            operate2.CloseConn();
                            if (flag4)
                            {
                                GlobalParmeters.isLoadDataAgin = true;
                                string s = this.lblMotorNumber.Text;
                                if (this.txbLotteryRemain.Text != "0")
                                {
                                    this.ini.ReadIniValue("配置", "机头" + s + "状态");
                                    GlobalParmeters.MotorStatus[int.Parse(s) - 1] = 0;
                                    SQLiteHelper.ExecuteSql("UPDATE tblMotorInfo SET MotorStatus=0 WHERE MotorId=" + s);
                                    this.ini.WriteIniValue("配置", "机头" + int.Parse(s) + "状态", "0");
                                }
                                base.Close();
                            }
                            else
                            {
                                this.lblMessage.Text = "彩票添加失败";
                            }
                        }
                    }
                    Application.DoEvents();
                }
            }
            catch(Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }

        private void GetLotteryInfo()
        {
            try
            {
                this.cmbLotteryType.Items.Clear();
                DbOperate operate = new DbOperate();
                List<string> lotteryInfo = operate.GetLotteryInfo();
                if (lotteryInfo.Count > 0)
                {
                    foreach (string str in lotteryInfo)
                    {
                        this.cmbLotteryType.Items.Add(str);
                    }
                }
                operate.CloseConn();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        private void GetMotorInfo()
        {
            try
            {
                DbOperate operate = new DbOperate();
                operate.GetNameAndRemainByMotorNumber(MotorNumber, ref this.LotteryName, ref this.LotteryRemain);
                this.lblMotorNumber.Text = MotorNumber.ToString();
                this.cmbLotteryType.Text = this.LotteryName;
                this.txbLotteryRemain.Text = this.LotteryRemain.ToString();
                operate.CloseConn();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.label4.Text = "";
        }

        private void cmbLotteryType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.txbLotteryRemain.SelectAll();
            this.txbLotteryRemain.Focus();
        }

        private void txtGameCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbLotteryType.Text = "";
                this.cmbLotteryType.Items.Clear();
                DbOperate operate = new DbOperate();
                List<string> lotteryInfo = operate.GetLotteryInfo(this.txtGameCode.Text.Trim());
                if (lotteryInfo.Count > 0)
                {
                    foreach (string str in lotteryInfo)
                    {
                        this.cmbLotteryType.Items.Add(str);
                    }
                }
                else
                {
                    this.GetLotteryInfo();
                }
                this.cmbLotteryType.DroppedDown = true;
                this.cmbLotteryType.Select(this.cmbLotteryType.Text.Length, 0);
                operate.CloseConn();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }
    }
}
