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
    public partial class FormEmptyCashReport : Form
    {
        public FormEmptyCashReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SQLiteHelper.GetSingle("SELECT sum(CashMount*actualEmptyCount) FROM tblEmptyCash WHERE time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            string sql = "SELECT cashMount AS 币种,actualEmptyCount AS 清空数量,time AS 时间,UserName AS 操作人 FROM tblEmptyCash WHERE time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
            this.dgv.DataSource = SQLiteHelper.Query(sql).Tables[0];
            int num = 0;
            for (int i = 0; i < this.dgv.Columns.Count; i++)
            {
                this.dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                num += this.dgv.Columns[i].Width;
            }
            if (num > this.dgv.Size.Width)
            {
                this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void FormEmptyCashReport_Load(object sender, EventArgs e)
        {
            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }
    }
}
