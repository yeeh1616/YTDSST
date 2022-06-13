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
using YTDSSTGenII.Service.Exception;
using YTDSSTGenII.Service;

namespace YTDSSTGenII.Forms
{
    public partial class FormCashDeposit : Form
    {
        private int i = 5;

        private int CashFaceValue;
        private double CoinFacValue;
        
        private IniFile ini;

        public FormCashDeposit()
        {
            InitializeComponent();
        }

        private void btnCleanCashBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (SQLiteHelper.GetSingle("select sql from sqlite_master where tbl_name='tblEmptyCash' ").ToString().IndexOf("CashTypeFlag") == -1)
                {
                    SQLiteHelper.ExecuteSql("ALTER TABLE tblEmptyCash ADD COLUMN 'CashTypeFlag' INTEGER DEFAULT 1; ");
                }
                SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblEmptyCash(CashMount,EmptyCount,ActualEmptyCount,time,UpLoadFlag,UserName,CashTypeFlag) VALUES(", GlobalParmeters.CashType, ",", GlobalParmeters.CashCount, ",", 0, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false','", FormLogin.UserName, "',1)" }));
                GlobalParmeters.CashCount = 0;
                CommonUtils.WriteCashCount(0);
                this.lblTips.Text = string.Concat(new object[] { "退币箱中有：纸币", GlobalParmeters.CashCount, "张；硬币", GlobalParmeters.CoinCount, "枚" });
                this.txtCashCount.Enabled = true;
                this.txtCashCount.Focus();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lblMessage.Text == "")
            {
                int num = 0;
                int num2 = 0;
                if (this.txtCashCount.Text != "")
                {
                    num = int.Parse(this.txtCashCount.Text);
                }
                if (this.txtCoinCount.Text != "")
                {
                    num2 = int.Parse(this.txtCoinCount.Text);
                }
                if ((num != 0) || (num2 != 0))
                {
                    FormShowDialog dialog = new FormShowDialog();
                    GlobalParmeters.frmShowDialog = "确定预存吗？";
                    dialog.ShowDialog();
                    if (GlobalParmeters.frmShowDialog == "是")
                    {
                        if (num > 0)
                        {
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblCashDeposit(CashCount,CoinCount,time,UploadFlag,CashFaceValue,CoinFaceValue,UserName) VALUES (", num, ",", 0, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false',", this.CashFaceValue, ",", this.CoinFacValue, ",'", FormLogin.UserName, "')" }));
                            CommonUtils.WriteCashCount(num);
                            GlobalParmeters.CashCount = num;
                        }
                        if (num2 > 0)
                        {
                            SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblCashDeposit(CashCount,CoinCount,time,UploadFlag,CashFaceValue,CoinFaceValue,UserName) VALUES (", 0, ",", num2, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false',", this.CashFaceValue, ",", this.CoinFacValue, ",'", FormLogin.UserName, "')" }));
                            int num3 = CommonUtils.ReadCoinCount();
                            CommonUtils.WriteCoinCount(num3 + num2);
                            GlobalParmeters.CoinCount = num3 + num2;
                        }
                        this.lblTips.Text = string.Concat(new object[] { "退币箱中有：纸币", GlobalParmeters.CashCount, "张；硬币", GlobalParmeters.CoinCount, "枚" });
                        this.lblMessage.Text = "预存成功，5秒后自动关闭页面";
                        this.txtCashCount.Text = "0";
                        this.txtCoinCount.Text = "0";
                        this.timer1.Enabled = true;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.i--;
            this.lblMessage.Text = "预存成功，" + this.i + "秒后自动关闭页面";
            if (this.i == 0)
            {
                this.i = 5;
                this.timer1.Enabled = false;
                base.Close();
                base.Dispose();
            }
        }

        private void btnEmptyCoinBox_Click(object sender, EventArgs e)
        {
            CoinClass class2 = null;
            try
            {
                class2 = new CoinClass();
                if (SQLiteHelper.GetSingle("select sql from sqlite_master where tbl_name='tblEmptyCash' ").ToString().IndexOf("CashTypeFlag") == -1)
                {
                    SQLiteHelper.ExecuteSql("ALTER TABLE tblEmptyCash ADD COLUMN 'CashTypeFlag' INTEGER DEFAULT 1; ");
                }

                //class2.CoinPayout(20);
                SQLiteHelper.ExecuteSql(string.Concat(new object[] { "INSERT INTO tblEmptyCash(CashMount,EmptyCount,ActualEmptyCount,time,UpLoadFlag,UserName,CashTypeFlag) VALUES(", GlobalParmeters.CoinType, ",", GlobalParmeters.CoinCount, ",", 0, ",'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false','", FormLogin.UserName, "',0)" }));

                GlobalParmeters.CoinCount = 0;
                CommonUtils.WriteCoinCount(0);

                this.lblTips.Text = string.Concat(new object[] { "退币箱中有：纸币", GlobalParmeters.CashCount, "张；硬币", GlobalParmeters.CoinCount, "枚" });

                this.txtCoinCount.Enabled = true;
                this.txtCoinCount.Focus();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            finally
            {
                class2.ClosePort();
                class2 = null;
            }
        }

        private void txtCashCount_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCashCount.Text != "")
            {
                int num;
                if (int.TryParse(this.txtCashCount.Text, out num))
                {
                    this.lblMessage.Text = "";
                }
                else
                {
                    this.lblMessage.Text = "必须输入数字";
                    return;
                }
                if (int.Parse(this.txtCashCount.Text) > 500)
                {
                    this.lblMessage.Text = "最多预存500张纸币";
                }
            }
            else
            {
                this.lblMessage.Text = "";
            }
        }

        private void txtCoinCount_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCoinCount.Text != "")
            {
                int num;
                if (int.TryParse(this.txtCoinCount.Text, out num))
                {
                    this.lblMessage.Text = "";
                }
                else
                {
                    this.lblMessage.Text = "必须输入数字";
                    return;
                }
                if (int.Parse(this.txtCoinCount.Text) > 750)
                {
                    this.lblMessage.Text = "最多预存750枚硬币";
                }
            }
            else
            {
                this.lblMessage.Text = "";
            }
        }

        private void picSoftKey_Click(object sender, EventArgs e)
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

        private void FormCashDeposit_Load(object sender, EventArgs e)
        {
            this.txtCashCount.Enabled = false;
            this.txtCoinCount.Enabled = false;

            this.ini = new IniFile("D://advitise//1.ini");
            this.label1.Text = "*注：纸币面值为" + this.ini.ReadIniValue("配置", "纸币币种") + "元，硬币面值为" + this.ini.ReadIniValue("配置", "硬币币种") + "元；若要预存纸币，请先清空纸币退币箱";
            this.CashFaceValue = int.Parse(this.ini.ReadIniValue("配置", "纸币币种"));
            this.CoinFacValue = Convert.ToDouble(this.ini.ReadIniValue("配置", "硬币币种"));
            this.lblTips.Text = string.Concat(new object[] { "退币箱中有：纸币", GlobalParmeters.CashCount, "张；硬币", GlobalParmeters.CoinCount, "枚" });
        }

        private void FormCashDeposit_FormClosing(object sender, FormClosingEventArgs e)
        {
            CashClass.ClosePort();
            CashClass.MyPort = null;
            base.Enabled = false;
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            GlobalParmeters.IsOpenScreenKey = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
