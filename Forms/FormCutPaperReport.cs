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
    public partial class FormCutPaperReport : Form
    {
        public FormCutPaperReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MotorId AS 机头号,lotteryName AS 彩种名称,trademoney AS 金额,time AS 时间 FROM tblTradeInfo  WHERE time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
            this.DGV.DataSource = SQLiteHelper.Query(sql).Tables[0];
        }

        private void FormCutPaperReport_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }

        private void lblLocalNumber_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
