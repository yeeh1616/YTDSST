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
using YTDSSTGenII.Service.Context;
using YTDSSTGenII.Service.Enums;

namespace YTDSSTGenII.Forms.Sys
{
    public partial class FormSetSysMode : Form
    {            
        private IniFile ini;

        public FormSetSysMode()
        {
            InitializeComponent();
        }

        private void FormSetSysMode_Load(object sender, EventArgs e)
        {
            this.ini = new IniFile("D://advitise//1.ini");

            string runMode = ini.ReadIniValue("系统", "运行模式");
            if (runMode == "生成")
            {
                //MachineContext.RunMode = EnumRunMode.PRODUCT;
                rbProduct.Checked = true;
            }
            else
            {
                //MachineContext.RunMode = EnumRunMode.DEBUG;
                rbDebug.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbDebug.Checked == true)
            {
                //MachineContext.RunMode = EnumRunMode.DEBUG;
            }
            else
            {
                //MachineContext.RunMode = EnumRunMode.PRODUCT;
            }

            if (MachineContext.RunMode == EnumRunMode.DEBUG)
            {
                ini.WriteIniValue("系统", "运行模式", "调试");
            }
            else
            {
                ini.WriteIniValue("系统", "运行模式", "生产");
            }

            base.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rbProduct_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbDebug_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
