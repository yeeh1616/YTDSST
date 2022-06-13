using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms
{
    public partial class FormShowDialog : Form
    {
        public FormShowDialog()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNo_Click(object sender, EventArgs e)
        {
            if (this.lblMessage.Text == "出票后是否直接退币？")
            {
                GlobalParmeters.PayoutFlag = false;
            }
            else if (this.lblMessage.Text == "真的要退币吗？")
            {
                GlobalParmeters.payoutSure = false;
            }
            else if (this.lblMessage.Text == "出票失败，是否退币？")
            {
                GlobalParmeters.PayoutFlag = false;
            }
            else if (GlobalParmeters.frmShowDialog == "确定预存吗？")
            {
                GlobalParmeters.frmShowDialog = "";
            }
            else if (GlobalParmeters.frmShowDialog == "是否要改变彩票信息？")
            {
                GlobalParmeters.frmShowDialog = "";
            }
            else if (GlobalParmeters.frmShowDialog == "确定要加票吗？")
            {
                GlobalParmeters.frmShowDialog = "";
            }
            else if (GlobalParmeters.frmShowDialog == "确定清钞吗？")
            {
                GlobalParmeters.frmShowDialog = "";
            }
            else if (GlobalParmeters.frmShowDialog == "真的要退出吗？")
            {
                GlobalParmeters.frmShowDialog = "";
            }
            else if (((GlobalParmeters.frmShowDialog == "必须重启购彩程序,立刻重启？") || (GlobalParmeters.frmShowDialog == "必须重启计算机,立刻重启？")) || (GlobalParmeters.frmShowDialog == "确定要恢复出场设置吗？"))
            {
                GlobalParmeters.frmShowDialog = "";
            }
            base.Close();
        }

        private void lblYes_Click(object sender, EventArgs e)
        {
            if (this.lblMessage.Text == "出票后是否直接退币？")
            {
                GlobalParmeters.PayoutFlag = true;
            }
            else if (this.lblMessage.Text == "真的要退币吗？")
            {
                GlobalParmeters.payoutSure = true;
            }
            else if (this.lblMessage.Text == "出票失败，是否退币？")
            {
                GlobalParmeters.PayoutFlag = true;
            }
            else if (GlobalParmeters.frmShowDialog == "确定预存吗？")
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            else if (GlobalParmeters.frmShowDialog == "是否要改变彩票信息？")
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            else if (GlobalParmeters.frmShowDialog == "确定要加票吗？")
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            else if (GlobalParmeters.frmShowDialog == "确定清钞吗？")
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            else if (GlobalParmeters.frmShowDialog == "真的要退出吗？")
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            else if (((GlobalParmeters.frmShowDialog == "必须重启购彩程序,立刻重启？") || (GlobalParmeters.frmShowDialog == "必须重启计算机,立刻重启？")) || (GlobalParmeters.frmShowDialog == "确定要恢复出场设置吗？"))
            {
                GlobalParmeters.frmShowDialog = "是";
            }
            base.Close();
        }

        private void FormShowDialog_Load(object sender, EventArgs e)
        {
            this.lblMessage.Text = GlobalParmeters.frmShowDialog;
            if (GlobalParmeters.frmShowDialog == "确定预存吗？")
            {
                base.Location = new Point(100, 130);
            }
            else if (GlobalParmeters.frmShowDialog == "是否要改变彩票信息？")
            {
                base.Location = new Point(0x69, 0x91);
            }
            else if (GlobalParmeters.frmShowDialog == "确定要加票吗？")
            {
                base.Location = new Point(0x69, 0x91);
            }
            else if (GlobalParmeters.frmShowDialog == "确定清钞吗？")
            {
                base.Location = new Point(60, 0x87);
            }
            else if (GlobalParmeters.frmShowDialog == "真的要退出吗？")
            {
                base.Location = new Point(60, 0x87);
            }
            else if (((GlobalParmeters.frmShowDialog == "必须重启购彩程序,立刻重启？") || (GlobalParmeters.frmShowDialog == "必须重启计算机,立刻重启？")) || (GlobalParmeters.frmShowDialog == "确定要恢复出场设置吗？"))
            {
                base.Location = new Point(60, 0x87);
            }
        }
    }
}
