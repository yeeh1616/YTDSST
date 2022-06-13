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
    public partial class FormAddLotteryCount : Form
    {
        public FormAddLotteryCount()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.DgvTradeList.DataSource = SQLiteHelper.Query("SELECT MotorId as 机头号,LotteryName as 彩种名称, sum(LotteryCount) as 数量  FROM tblAddLotteryInfo WHERE TradeTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY MotorId,LotteryName ").Tables[0];
        }

        private void FormAddLotteryCount_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }
    }
}
