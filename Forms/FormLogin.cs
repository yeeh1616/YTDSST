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

/**
 * 终端机屏幕尺寸 768*1366
 */
namespace YTDSSTGenII.Forms
{
    public partial class FormLogin : Form
    {
        private static String userName;
        private IniFile ini;

        public static String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try {
                Process[] processes = Process.GetProcesses();
                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].ProcessName == "ScreenKey")
                    {
                        processes[i].Kill();
                    }
                }
                base.Close();
                base.Dispose();
            }
            catch (Exception exp)
            {
                CommonUtils.WriteExceptionInfo(exp);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try {
                if (this.txtName.Text.Trim() == "") 
                {
                    this.lblMessage.Text = "请输入用户名!";
                    this.txtName.Focus();
                }
                else if (this.txtPassWord.Text.Trim() == "")
                {
                    this.lblMessage.Text = "请输入密码！";
                    this.txtPassWord.Focus();
                }
                else
                {
                    if (Convert.ToInt32(SQLiteHelper.GetSingle("select count(*) from Users where name='" + this.txtName.Text.Trim() + "'")) > 0)
                    {
                        string str = SQLiteHelper.GetSingle("select password from Users where name='" + this.txtName.Text.Trim() + "'").ToString();
                        if (this.txtPassWord.Text.Trim() == str)
                        {
                            UserName = this.txtName.Text.Trim();
                            GlobalParmeters.LoginFailedCount = 0;
                            this.lblMessage.Text = "登录成功.";
                            base.Hide();
                            Process[] processes = Process.GetProcesses();
                            for (int i = 0; i < processes.Length; i++)
                            {
                                if (processes[i].ProcessName == "ScreenKey")
                                {
                                    processes[i].Kill();
                                }
                            }

                            FormAdminMain adminMain = new FormAdminMain();

                            adminMain.ShowDialog();
                            adminMain.Dispose();
                            base.Close();
                            base.Dispose();
                        }
                        else
                        {
                            this.lblMessage.Text = "您输入的密码错误!";
                        }
                    }
                    else 
                    {
                        this.lblMessage.Text = "你输入用户名不存在！";
                        this.txtName.SelectAll();
                        this.txtName.Focus();
                    } 
                }
            } catch (Exception exp) {
                Console.WriteLine(exp.StackTrace);
                MessageBox.Show(exp.Message);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            this.txtName.Focus();
            this.ini = new IniFile("D://advitise//1.ini");
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
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
                txtName.Focus();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }

        }

        private void timerMouseKey_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseKeyBoardOperate.GetLastInputTime() >= 20L)
                {
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        if (processes[i].ProcessName == "ScreenKey")
                        {
                            processes[i].Kill();
                        }
                    }
                    base.Close();
                    base.Dispose();
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        
    }
}
