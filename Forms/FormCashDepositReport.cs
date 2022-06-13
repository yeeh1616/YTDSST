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
    public partial class FormCashDepositReport : Form
    {
        public FormCashDepositReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT CashCount AS 纸币张数,CoinCount AS 硬币个数, CashFaceValue AS 纸币面值,CoinFaceValue AS 硬币面值,time AS 时间,UserName as 操作人 FROM tblCashDeposit WHERE time  BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' ";
            this.dgv.DataSource = SQLiteHelper.Query(sql).Tables[0];
        }

        private void FormCashDepositReport_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }
    }
}
