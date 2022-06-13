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
    public partial class FormReceCash : Form
    {
        public FormReceCash()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT id AS 流水号,billMount AS 面值,CASE billflag WHEN 1 THEN '纸币' WHEN 0 THEN '硬币' END AS 币种,Time AS 时间 FROM tblComeCash WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
            this.dgv.DataSource = SQLiteHelper.Query(sql).Tables[0];
            string s = SQLiteHelper.GetSingle("SELECT sum(BillMount*CashCount)  FROM tblComeCash  where BillFlag=0 and Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            string str3 = SQLiteHelper.GetSingle("SELECT sum(BillMount)  FROM tblComeCash  where BillFlag=1 and Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            if (s == "")
            {
                s = "0";
            }
            if (str3 == "")
            {
                str3 = "0";
            }
            string[] strArray4 = new string[] { "收入：纸币", str3, "元,硬币", s, "元，共计：", (int.Parse(str3) + int.Parse(s)).ToString(), "元" };
            this.groupBox1.Text = string.Concat(strArray4);
        }

        private void FormReceCash_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
            this.groupBox1.Text = "请选择日期搜索";
        }
    }
}
