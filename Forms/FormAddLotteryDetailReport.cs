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
    public partial class FormAddLotteryDetailReport : Form
    {
        public FormAddLotteryDetailReport()
        {
            InitializeComponent();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                this.DgvTradeList.DataSource = SQLiteHelper.Query("SELECT MotorId as 机头号,LotteryName as 彩种名称, LotteryCount as 数量, TradeTime as 加票时间,UserName as 操作人 FROM tblAddLotteryInfo WHERE  TradeTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' order by TradeTime desc").Tables[0];
            }
            else
            {
                this.DgvTradeList.DataSource = SQLiteHelper.Query("SELECT MotorId as 机头号,LotteryName as 彩种名称, LotteryCount as 数量, TradeTime as 加票时间,UserName as 操作人 FROM tblAddLotteryInfo WHERE  MotorId='" + this.comboBox1.Text + "' and TradeTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'  order by TradeTime desc").Tables[0];
            }
        }

        private void FormAddLotteryDetailReport_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
