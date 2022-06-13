using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YTDSSTGenII.Service;
using YTDSSTGenII.Utils;
using YTDSSTGenII.Service.Sg;

namespace YTDSSTGenII.Forms.Sg
{
    public partial class FormSGFactoryTest : Form
    {
        private string strmessage = "";

        public FormSGFactoryTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            Application.DoEvents();
            FormShowDialog dialog = new FormShowDialog();

            GlobalParmeters.frmShowDialog = "请确认是否要进行此操作？";
            
            dialog.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dialog.ShowDialog();

            dialog.Dispose();
            
            if (GlobalParmeters.frmShowDialog == "是")
            {
                new FormWaitingBox(delegate(object obj, EventArgs args)
                {
                    try
                    {
                        if (SGBentDll.sgFactoryTest() == 0)
                        {
                            this.strmessage = "网络畅通";
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
                this.lblMessage.Text = this.strmessage;
            }
        }

        private void FormSGFactoryTest_Load(object sender, EventArgs e)
        {

        }

        private void FormSGFactoryTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
        }
    }
}
