using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using YTDSSTGenII.Service;
using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Sg;

namespace YTDSSTGenII.Forms.Sg
{
    public partial class FormSGSet : Form
    {
        private int claimAmount = -1;
        private IniFile ini;
        private string strgetclaimAmount = "";
        private string strmessage = "";

        public FormSGSet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();
            GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
            dialog.StartPosition = FormStartPosition.CenterParent;
            
            DialogResult dr = dialog.ShowDialog();
            
            dialog.Dispose();

            if (dr  == DialogResult.Yes)
            {
                new FormWaitingBox(delegate(object obj, EventArgs args)
                {
                    try
                    {
                        if ((SGBentDll.sgSetclaimAmount(int.Parse(this.txtclaimAmount.Text.Trim())) % 0x100) == 0)
                        {
                            int num3 = SGBentDll.sgGetclaimAmount();
                            this.strmessage = "设置兑奖确认额度成功，当前兑奖确认额度：" + num3 + " 元";
                            this.setclaimAmout();
                        }
                        else
                        {
                            string errocode = "";
                            string erromessage = "";
                            CommonUtils.ErroMessage(ref errocode, ref erromessage);
                            this.strmessage = "操作不成功：(" + errocode + ")\r\n" + erromessage;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.strmessage = "设置兑奖金额异常 ";
                        CommonUtils.showTime(this.strmessage + exception.ToString());
                    }
                }).ShowDialog(this);
                this.lblMessage.Text = this.strmessage;
                this.lbl_sgGetclaimAmount.Text = this.strgetclaimAmount;
            }
        }

        private void btnInterval_Click(object sender, EventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();
            GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
            dialog.StartPosition = FormStartPosition.CenterParent;
            
            DialogResult dr = dialog.ShowDialog();

            dialog.Dispose();
            if (dr == DialogResult.Yes)
            {
                new FormWaitingBox(delegate(object obj, EventArgs args)
                {
                    try
                    {
                        if (SGBentDll.sgPingHost(int.Parse(this.txtInterval.Text.Trim())) == 0)
                        {
                            this.strmessage = "心跳设置成功";
                        }
                        else
                        {
                            string errocode = "";
                            string erromessage = "";
                            CommonUtils.ErroMessage(ref errocode, ref erromessage);
                            this.strmessage = "操作不成功：(" + errocode + ")\r\n" + erromessage;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.strmessage = "心跳设置过程中出现异常";
                        CommonUtils.showTime(this.strmessage + ": " + exception.ToString());
                    }
                }).ShowDialog(this);
                this.ini.WriteIniValue("配置", "心跳间隔", this.txtInterval.Text.Trim());
                GlobalParmeters.pingtime = int.Parse(this.txtInterval.Text.Trim());
                this.lblMessage.Text = this.strmessage;
            }
        }

        private void FormSGSet_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");
            this.setclaimAmout();
            this.lbl_sgGetclaimAmount.Text = this.strgetclaimAmount;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        public void setclaimAmout()
        {
            this.claimAmount = SGBentDll.sgGetclaimAmount();
            if (this.claimAmount != -1)
            {
                this.strgetclaimAmount = "当前兑奖确认额度" + this.claimAmount + "元";
            }
            else
            {
                this.strgetclaimAmount = "当前兑奖确认额度-元";
                CommonUtils.showTime("兑奖确认金额获取失败：" + CommonUtils.ErroMessage());
            }
        }
    }
}
