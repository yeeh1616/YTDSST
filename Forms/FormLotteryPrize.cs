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
    public partial class FormLotteryPrize : Form
    {
        public FormLotteryPrize()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.cmbState.Text == "已兑奖")
            {
                this.DgvTradeList.DataSource = SQLiteHelper.Query("SELECT id AS 编号, LotterySerialNumber AS 彩票序列号,SecurityNumber AS 保安区号,PrizeQuota AS 金额, GetCashTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=1 AND GetCashTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' ").Tables[0];
            }
            else
            {
                this.DgvTradeList.DataSource = SQLiteHelper.Query("SELECT id AS 编号, LotterySerialNumber AS 彩票序列号,SecurityNumber AS 保安区号,PrizeQuota AS 金额, DrawTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=99 AND DrawTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' ").Tables[0];
            }
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormLotteryPrize_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
            this.cmbState.SelectedIndex = 0;
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
