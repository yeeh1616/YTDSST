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
    public partial class FormCleanCashTime : Form
    {
        public FormCleanCashTime()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.dgv.DataSource = SQLiteHelper.Query("select id as 编号, CleanCashTime as 清钞时间,UserName as 操作人 from tblCleanCashTime  WHERE CleanCashTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' order by CleanCashTime desc").Tables[0];
        }

        private void FormCleanCashTime_Load(object sender, EventArgs e)
        {
             this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }
    }
}
