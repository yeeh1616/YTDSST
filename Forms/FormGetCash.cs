using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YTDSSTGenII.Utils;
using YTDSSTGenII.Service;

namespace YTDSSTGenII.Forms
{
    public partial class FormGetCash : Form
    {
        private string caozuo = "";
        private int i = 5;
        private IniFile ini;

        public FormGetCash()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormShowDialog dialog = new FormShowDialog();
            GlobalParmeters.frmShowDialog = "确定清钞吗？";
            dialog.ShowDialog();
            dialog.Dispose();
            if (GlobalParmeters.frmShowDialog == "是")
            {
                JCMBillAcceptorClass.Reset();
                JCMBillAcceptorClass.UsefulAllBillType();
                JCMBillAcceptorClass.SetAllSecurityNormal();
                JCMBillAcceptorClass.SetRehibit();
                JCMBillAcceptorClass.SetDirection();
                JCMBillAcceptorClass.OptionFunction();
                JCMBillAcceptorClass.SetCommunitToPoll();

                this.caozuo = "清钞";
                GlobalParmeters.CashBoxFlag = false;
                this.ini.WriteIniValue("配置", "钞箱状态", "0");
                SQLiteHelper.ExecuteSql("insert into tblCleanCashTime(CleanCashTime,UploadFlag,UserName) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','false','" + FormLogin.UserName + "')");
                this.lblMessage.Text = "清钞成功，5秒后自动关闭页面";
                this.timer1.Enabled = true;
            }
        }

        private void btnResolveFault_Click(object sender, EventArgs e)
        {
            JCMBillAcceptorClass.Reset();
            JCMBillAcceptorClass.UsefulAllBillType();
            JCMBillAcceptorClass.SetAllSecurityNormal();
            JCMBillAcceptorClass.SetRehibit();
            JCMBillAcceptorClass.SetDirection();
            JCMBillAcceptorClass.OptionFunction();
            JCMBillAcceptorClass.SetCommunitToPoll();

            GlobalParmeters.CashBoxFlag = false;

            this.ini.WriteIniValue("配置", "钞箱状态", "0");

            this.caozuo = "解除故障";
            this.lblMessage.Text = "解除故障成功，5秒后自动关闭页面";
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.i--;
            this.lblMessage.Text = string.Concat(new object[] { this.caozuo, "成功，", this.i, "秒后自动关闭页面" });
            if (this.i <= 0)
            {
                this.i = 5;
                this.timer1.Enabled = false;
                base.Close();
                base.Dispose();
            }
        }

        private void FormGetCash_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");
        }
    }
}
