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
    public partial class FormPayoutReport : Form
    {
        public FormPayoutReport()
        {
            InitializeComponent();
        }

        private void FormPayoutReport_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
            this.groupBox1.Text = "请选择日期后搜索";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT cashCount AS 纸币张数,CoinCount AS 硬币个数,CashId AS 现金流水号,CashType AS 纸币面值,CoinType AS 硬币面值,time AS 时间 FROM tblPayout WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
            this.dgv.DataSource = SQLiteHelper.Query(sql).Tables[0];
            string s = SQLiteHelper.GetSingle("SELECT sum(cashCount*CashType)  FROM tblPayout WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            string str3 = SQLiteHelper.GetSingle("SELECT sum(CoinCount)  FROM tblPayout WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            if (s == "")
            {
                s = "0";
            }
            if (str3 == "")
            {
                str3 = "0";
            }
            string[] strArray4 = new string[] { "退纸币", s, "元，硬币", str3, "元，共计：", (int.Parse(s) + int.Parse(str3)).ToString(), "元" };
            this.groupBox1.Text = string.Concat(strArray4);
        }
    }
}
