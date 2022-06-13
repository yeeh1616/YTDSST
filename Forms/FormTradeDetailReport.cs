using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms
{
    public partial class FormTradeDetailReport : Form
    {
        public FormTradeDetailReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string str = this.dateTimePicker1.Value.ToString("yyyy-MM-dd ") + this.dateTimePicker2.Value.ToString("HH:mm:ss");
            string str2 = this.dateTimePicker3.Value.ToString("yyyy-MM-dd ") + this.dateTimePicker4.Value.ToString("HH:mm:ss");
            string sql = "SELECT * FROM (SELECT '收钞' AS 类型, '' AS 机头号,'' AS 彩种名称,id AS 交易流水号, id AS 现金流水号, billMount as 收钞金额, '' AS 交易金额, Time as 时间 FROM tblComeCash WHERE Time BETWEEN'" + str + "' AND '" + str2 + "' UNION SELECT '出票' AS 类型,MotorId AS 机头号, LotteryName AS 彩种名称, Id AS 交易流水号,CashId AS 现金流水号,'' AS 收钞金额,TradeMoney AS 交易金额,Time AS 时间 FROM tblTradeInfo  WHERE Time BETWEEN'" + str + "' AND '" + str2 + "' UNION SELECT '找零' AS 类型, '' AS 机头号,'' AS 彩种名称,id AS 交易流水号,CashId AS 现金流水号, '' AS 收钞金额,(cashcount*CashType+coincount*CoinType) AS 交易金额,Time AS 时间 FROM tblPayout WHERE Time BETWEEN'" + str + "' AND '" + str2 + "' UNION SELECT '吞钞' AS 类型, '' AS 机头号,'' AS 彩种名称,id AS 交易流水号,'0' AS 现金流水号, '' AS 收钞金额,Money AS 交易金额,Time as 时间 FROM EatCash WHERE Time BETWEEN'" + str + "' AND '" + str2 + "'UNION SELECT '兑奖'AS 类型, '' AS 机头号,'' AS 彩种名称,id AS 交易流水号, '0' AS 现金流水号, '' AS 收钞金额, PrizeQuota AS 交易金额,GetCashTime AS 时间  FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=1 AND  GetCashTime BETWEEN'" + str + "' AND '" + str2 + "' ORDER BY Time)";
            this.DGV.DataSource = SQLiteHelper.Query(sql).Tables[0];
        }

        private void DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < this.DGV.Rows.Count)
            {
                DataGridViewRow row = this.DGV.Rows[e.RowIndex];
                try
                {
                    if (row.Cells["类型"].Value.ToString().Contains("收钞"))
                    {
                        row.DefaultCellStyle.BackColor = Color.SkyBlue;
                    }
                    else if (row.Cells["类型"].Value.ToString().Contains("出票"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Tan;
                    }
                    else if (row.Cells["类型"].Value.ToString().Contains("找零"))
                    {
                        row.DefaultCellStyle.BackColor = Color.YellowGreen;
                    }
                    else if (row.Cells["类型"].Value.ToString().Contains("吞钞"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                catch (Exception exception)
                {
                    CommonUtils.WriteExceptionInfo(exception);
                }
            }
        }

        private void FormTradeDetailReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.Enabled = false;
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            GlobalParmeters.IsOpenScreenKey = false;
        }

        private void FormTradeDetailReport_Load(object sender, EventArgs e)
        {
            Font font = new Font("方正准圆简体", 10f);
            this.DGV.Font = font;
            this.label1.Text = "终端编号：" + GlobalParmeters.machineNumber;
        }

        private void picSoftKey_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].ProcessName == "ScreenKey")
                {
                    processes[i].Kill();
                }
            }
            Process.Start(Application.StartupPath + @"\ScreenKey.exe");
            GlobalParmeters.IsOpenScreenKey = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
