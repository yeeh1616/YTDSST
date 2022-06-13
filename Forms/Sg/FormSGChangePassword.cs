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
    public partial class FormSGChangePassword : Form
    {
        private string strmessage = "";

        public FormSGChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.txtnewpwd.Text.Trim().Equals(this.txtnewpwd2.Text.Trim()))
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
                if (dr == DialogResult.Yes)
                {
                    new FormWaitingBox(delegate(object obj, EventArgs args)
                    {
                        try
                        {
                            int mode = 1;
                            string opindex = this.txtUserName.Text.Trim();
                            string oldpasswd = this.txtoldPwd.Text.Trim();
                            string newpasswd = this.txtnewpwd.Text.Trim();
                            if (SGBentDll.sgChangePassword(mode, opindex, oldpasswd, newpasswd) == 0)
                            {
                                this.strmessage = "密码修改成功";
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
                            this.strmessage = "修改过程中出现异常";
                            CommonUtils.showTime("修改密码过程中出现异常：" + exception.ToString());
                        }
                    }).ShowDialog(this);
                    this.lblMessage.Text = this.strmessage;
                }
            }
        }

        private void btn_changesave_Click(object sender, EventArgs e)
        {
            if (!this.txtnewpwd.Text.Trim().Equals(this.txtnewpwd2.Text.Trim()))
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

                if (dr == DialogResult.Yes)
                {
                    new FormWaitingBox(delegate(object obj, EventArgs args)
                    {
                        try
                        {
                            int mode = 0;
                            string opindex = this.txtUserName.Text.Trim();
                            string oldpasswd = this.txtoldPwd.Text.Trim();
                            string newpasswd = this.txtnewpwd.Text.Trim();
                            if (SGBentDll.sgChangePassword(mode, opindex, oldpasswd, newpasswd) == 0)
                            {
                                this.strmessage = "密码保存成功";
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
                            this.strmessage = "修改过程中出现异常";
                            CommonUtils.showTime("修改失败：" + exception.ToString());
                        }
                    }).ShowDialog(this);
                    this.lblMessage.Text = this.strmessage;
                }
            }
        }

        private void cbnewpwd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbnewpwd.Checked)
            {
                this.txtnewpwd.PasswordChar = '\0';
            }
            else
            {
                this.txtnewpwd.PasswordChar = '*';
            }
        }

        private void cbnewpwd2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbnewpwd2.Checked)
            {
                this.txtnewpwd2.PasswordChar = '\0';
            }
            else
            {
                this.txtnewpwd2.PasswordChar = '*';
            }
        }

        private void cboldPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cboldPwd.Checked)
            {
                this.txtoldPwd.PasswordChar = '\0';
            }
            else
            {
                this.txtoldPwd.PasswordChar = '*';
            }
        }

        private void FormSGChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
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
    }
}
