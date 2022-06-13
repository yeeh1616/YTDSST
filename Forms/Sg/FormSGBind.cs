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

using YTDSSTGenII.Service;
using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Sg;

namespace YTDSSTGenII.Forms.Sg
{
    public partial class FormSGBind : Form
    {
        private IniFile ini;
        private string strmessage = "";

        public FormSGBind()
        {
            InitializeComponent();
        }

        private void FormSGBind_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");
            this.txtMachineNumber.Text = GlobalParmeters.machineNumber;
            this.txtBranchNumber.Text = GlobalParmeters.no;
            this.txtPort.Text = GlobalParmeters.port;
            this.txtprovinceid.Text = GlobalParmeters.provinceid;
            this.txtoperusername.Text = GlobalParmeters.logonid;
            this.txtPwd.Text = GlobalParmeters.logonpwd;
            this.txttechpswd.Text = GlobalParmeters.techpasswd;
        }

        private void btnUnbind_Click(object sender, EventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();

            GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
            dialog.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dialog.ShowDialog();
            dialog.Dispose();
            
            if (dr == DialogResult.Yes)
            {
                this.strmessage = "";
                new FormWaitingBox(delegate(object obj, EventArgs args)
                {
                    try
                    {
                        string no = this.txtBranchNumber.Text.Trim() + this.txtPort.Text.Trim();
                        if (SGBentDll.sgBind(0, this.txttechpswd.Text.Trim(), int.Parse(this.txtprovinceid.Text.Trim()), no, this.txtMachineNumber.Text.Trim(), this.txtoperusername.Text.Trim(), this.txtPwd.Text.Trim()) == 0)
                        {
                            this.strmessage = "恭喜，解绑成功";
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
                        this.strmessage = "解绑过程中出现异常";
                        CommonUtils.showTime(this.strmessage + exception.ToString());
                    }
                }).ShowDialog(this);
                this.lblMessage.Text = this.strmessage;
            }
        }

        private void btnBind_Click(object sender, EventArgs e)
        {
            string branch;
            string techpswd;
            string provinceid;
            string MachineNumber;
            string operusername;
            string pwd;
            int result;

            if (!this.txtPwd.Text.Trim().Equals(this.txtpwd2.Text.Trim()))
            {
                this.strmessage = "两次输入的操作员密码不一致，请重新输入";
                this.lblMessage.Text = this.strmessage;
            }
            else
            {
                FormShowDialog dialog = new FormShowDialog();
                GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
                dialog.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = dialog.ShowDialog();
                dialog.Dispose();

                //开始绑定
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    branch = this.txtBranchNumber.Text.Trim() + this.txtPort.Text.Trim();
                    string str = this.txtPort.Text.Trim();
                    string str2 = this.txtBranchNumber.Text.Trim();
                    techpswd = this.txttechpswd.Text.Trim();
                    provinceid = this.txtprovinceid.Text.Trim();
                    MachineNumber = this.txtMachineNumber.Text.Trim();
                    operusername = this.txtoperusername.Text.Trim();
                    pwd = this.txtPwd.Text.Trim();
                    result = -1;

                    //等待绑定
                    new FormWaitingBox(delegate(object obj, EventArgs args) 
                        {
                            this.strmessage = "";
                            try
                            {
                                result = SGBentDll.sgBind(1, techpswd, int.Parse(provinceid), branch, MachineNumber, operusername, pwd);
                                if (result == 0)
                                {
                                    this.strmessage = "恭喜，绑定成功";
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
                                this.strmessage = "绑定过程中出现异常";
                                CommonUtils.showTime("绑定失败：" + exception.ToString());
                            }

                        }
                    ).ShowDialog(this);

                    this.lblMessage.Text = this.strmessage;
                    this.ini.WriteIniValue("配置", "网店编号", str2);
                    this.ini.WriteIniValue("配置", "网店端口", str);
                    this.ini.WriteIniValue("配置", "绑定密码", techpswd);
                    this.ini.WriteIniValue("配置", "省ID", provinceid);
                    this.ini.WriteIniValue("配置", "操作员编号", operusername);
                    this.ini.WriteIniValue("配置", "操作员密码", pwd);

                    GlobalParmeters.no = str2;
                    GlobalParmeters.port = str;
                    GlobalParmeters.provinceid = provinceid;
                    GlobalParmeters.logonid = operusername;
                    GlobalParmeters.logonpwd = pwd;
                    GlobalParmeters.techpasswd = techpswd;
                }

            }

        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();

            GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
            dialog.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dialog.ShowDialog();
            dialog.Dispose();
            
            if (dr == DialogResult.Yes)
            {
                this.strmessage = "";
                new FormWaitingBox(delegate(object obj, EventArgs args)
                {
                    try
                    {
                        string no = this.txtBranchNumber.Text.Trim() + this.txtPort.Text.Trim();
                        if (SGBentDll.sgBind(2, this.txttechpswd.Text.Trim(), int.Parse(this.txtprovinceid.Text.Trim()), no, this.txtMachineNumber.Text.Trim(), this.txtoperusername.Text.Trim(), this.txtPwd.Text.Trim()) == 0)
                        {
                            this.strmessage = "恭喜，恢复成功";
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
                        this.strmessage = "恢复过程中出现异常";
                        CommonUtils.showTime(this.strmessage + exception.ToString());
                    }
                }).ShowDialog(this);
                this.lblMessage.Text = this.strmessage;
            }
        }

        private void ckpwd2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckpwd2.Checked)
            {
                this.txtpwd2.PasswordChar = '\0';
            }
            else
            {
                this.txtpwd2.PasswordChar = '*';
            }
        }

        private void ckPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckPwd.Checked)
            {
                this.txtPwd.PasswordChar = '\0';
            }
            else
            {
                this.txtPwd.PasswordChar = '*';
            }
        }

        private void cktechpswd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cktechpswd.Checked)
            {
                this.txttechpswd.PasswordChar = '\0';
            }
            else
            {
                this.txttechpswd.PasswordChar = '*';
            }
        }

        private void FormSGBind_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
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

        private void txtBranchNumber_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBranchNumber.Text.Length > 2)
            {
                this.txtprovinceid.Text = this.txtBranchNumber.Text.Substring(0, 2);
            }
        }
    }
}
