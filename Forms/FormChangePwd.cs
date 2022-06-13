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

namespace YTDSSTGenII.Forms
{
    public partial class FormChangePwd : Form
    {
        private int Count = 5;


        private FormAdminMain mdiManage;

         public FormAdminMain MdiManage
        {
            get 
            {
                return this.mdiManage;
            }
               
            set
            {
                this.mdiManage = value;
            }
        }

        public FormChangePwd(FormAdminMain mdiManage)
        {
            InitializeComponent();

            this.mdiManage = mdiManage;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormChangePwd_Load(object sender, EventArgs e)
        {
            this.label1.Text = "当前用户：" + FormLogin.UserName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (this.txtOldPwd.Text.Trim() == "")
            {
                this.lblMessage.Text = "请输入旧密码";
            }
            else if (this.txtNewPwd.Text.Trim() == "")
            {
                this.lblMessage.Text = "请输入新密码";
            }
            else if (this.txtSureNewPwd.Text.Trim() == "")
            {
                this.lblMessage.Text = "请再次输入新密码";
            }
            else
            {
                string str = SQLiteHelper.GetSingle("select password from Users where name='" + FormLogin.UserName + "'").ToString();
                if (this.txtOldPwd.Text.Trim() != str)
                {
                    this.lblMessage.Text = "旧密码错误";
                }
                else if (this.txtNewPwd.Text.Trim() != this.txtSureNewPwd.Text.Trim())
                {
                    this.lblMessage.Text = "两次密码不一致";
                }
                else if (this.txtNewPwd.Text.Trim() == this.txtOldPwd.Text.Trim())
                {
                    this.lblMessage.Text = "新旧密码一致，无需修改!";
                }
                else if (SQLiteHelper.ExecuteSql("update users set password='" + this.txtSureNewPwd.Text.Trim() + "',oldPwd='" + this.txtOldPwd.Text.Trim() + "',UpLoadFlag='false' where name='" + FormLogin.UserName + "'") > 0)
                {
                    this.lblMessage.Text = "密码修改成功,页面将在5秒后自动关闭";
                    this.timer2.Enabled = true;
                }
            }
        }

        private void picSoftkey_Click(object sender, EventArgs e)
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
            this.txtOldPwd.Focus();
            GlobalParmeters.IsOpenScreenKey = true;
        }

        private void FormChangePwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
            GlobalParmeters.IsOpenScreenKey = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Count--;
            this.lblMessage.Text = "密码修改成功,页面将在" + this.Count + "秒后自动关闭";

            if (this.Count <= 0)
            {
                this.Count = 0;
                base.Close();
                base.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
