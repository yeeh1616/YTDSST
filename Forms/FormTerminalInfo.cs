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
    public partial class FormTerminalInfo : Form
    {
        private String CleanCashTime = "";
        private string ReceCash = "";
        private IniFile ini;

        public FormTerminalInfo()
        {
            InitializeComponent();

            this.ini = new IniFile("D://advitise//1.ini");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.label3.Text = "终端编号：" + GlobalParmeters.machineNumber;
            this.CleanCashTime = SQLiteHelper.GetSingle("select CleanCashTime from tblCleanCashTime where id=(select Max(id) from tblCleanCashTime) ").ToString();
            if (this.CleanCashTime == "")
            {
                this.ReceCash = SQLiteHelper.GetSingle("SELECT sum(billMount) FROM tblComeCash WHERE BillFlag=1").ToString();
            }
            else
            {
                this.ReceCash = SQLiteHelper.GetSingle("SELECT sum(billMount) FROM tblComeCash where Time BETWEEN'" + this.CleanCashTime + "' and '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND BillFlag=1").ToString();
            }
            string sql = "SELECT MotorId AS 机头号,LotteryName AS 彩种名称,LotteryPrice AS 单价,LotteryCount AS 余票量,LotteryPrice*LotteryCount AS 余票金额 FROM tblMotorInfo where motorId<9";
            this.dgv.DataSource = SQLiteHelper.Query(sql).Tables[0];
            string str2 = SQLiteHelper.GetSingle("SELECT sum(lotteryCount*LotteryPrice) FROM tblMotorInfo where motorId<9").ToString();
            string[] values = new string[] { "合计：", "", "", "", str2 };
            ((DataTable)this.dgv.DataSource).Rows.Add(values);
            double num = Convert.ToDouble(this.ini.ReadIniValue("配置", "纸币币种"));
            double num2 = Convert.ToDouble(this.ini.ReadIniValue("配置", "硬币币种"));
            double num3 = (GlobalParmeters.CashCount * num) + (GlobalParmeters.CoinCount * num2);
            if (str2 == "")
            {
                str2 = "0";
            }
            if (this.ReceCash == "")
            {
                this.ReceCash = "0";
            }
            this.groupBox1.Text = "各机头票种合计金额" + str2 + "元";
            this.label1.Text = "收币钱箱中共有纸币" + this.ReceCash + "元";
            this.label2.Text = string.Concat(new object[] { "退币箱中有：纸币", GlobalParmeters.CashCount, "张,", GlobalParmeters.CashCount * num, "元，硬币", GlobalParmeters.CoinCount, "枚,", GlobalParmeters.CoinCount * num2, "元,合计：", num3, "元" });
        }


        public void AddRow(DataGridView dg, string value)
        {
            if (dg.Rows.Count > 1)
            {
                this.DelRow(dg, dg.Rows.Count - 1);
            }
            dg.Rows.Add();
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                dg.Rows[dg.Rows.Count - 1].Cells[i].Value = value;
            }
            dg.CurrentCell = dg.Rows[dg.Rows.Count - 1].Cells[0];
            this.TotalRow(dg);
        }

        public void DelRow(DataGridView dg, int index)
        {
            dg.Rows.Remove(dg.Rows[index]);
        }


        public void TotalRow(DataGridView dg)
        {
            dg.Rows.Add();
            DataGridViewRow row = dg.Rows[dg.Rows.Count - 1];
            row.ReadOnly = true;
            row.DefaultCellStyle.BackColor = Color.Khaki;
            row.Cells[0].Value = "合计";
            for (int i = 0; i < (dg.Rows.Count - 1); i++)
            {
                row.Cells[3].Value = Convert.ToSingle(row.Cells[3].Value) + Convert.ToSingle(dg.Rows[i].Cells[3].Value);
            }
        }

        private void FormTerminalInfo_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void FormTerminalInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
        }
    }
}
