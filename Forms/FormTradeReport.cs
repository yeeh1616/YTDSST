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
    public partial class FormTradeReport : Form
    {
        private string time = "";

        public FormTradeReport()
        {
            InitializeComponent();
        }

        private void FormTradeReport_Load(object sender, EventArgs e)
        {

            this.timer1.Enabled = true;
        }

        private void BindLinkLabel()
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";

            str = SQLiteHelper.GetSingle("SELECT sum(BillMount*CashCount) FROM  tblComeCash WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            str2 = SQLiteHelper.GetSingle("SELECT sum(tradeMoney) FROM tblTradeInfo where Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            str4 = SQLiteHelper.GetSingle("SELECT sum(cashCount*cashType)+sum(coinCount*cointype) FROM tblPayout where Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            str3 = SQLiteHelper.GetSingle("SELECT sum(Money) FROM EatCash where Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            str5 = SQLiteHelper.GetSingle("SELECT sum(PrizeQuota) FROM tbllotteryprize where GetCashTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' AND PrizeDrawFlag=1").ToString();
            str6 = SQLiteHelper.GetSingle("SELECT sum(PrizeQuota) FROM tbllotteryprize where DrawTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' AND PrizeDrawFlag=99").ToString();

            //微信支付交易金额
    
            string strWeiXin = SQLiteHelper.GetSingle("SELECT sum(d.unit_price) FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 AND o.pay_type = 2 AND d.ticket_out_time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            if (strWeiXin == "" || strWeiXin == null)
                strWeiXin = "0";

            //支付宝交易金额
            string strZhifubao = SQLiteHelper.GetSingle("SELECT sum(d.unit_price) FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 AND o.pay_type = 3 AND d.ticket_out_time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'").ToString();
            if (strZhifubao == "" || strZhifubao == null)
                strZhifubao = "0";

            //现金交易金额
            if (str2 == "" || str2 == null) {
                str2 = "0";
            }

            string strCash = (Int32.Parse(str2) - Int32.Parse(strWeiXin) - Int32.Parse(strZhifubao)) + "";

            if (str == "" || str == null )
            {
                this.lblReceCash.Text = "收入金额0元";
            }
            else
            {
                this.lblReceCash.Text = "收入金额" + str + "元";
            }
            if (str2 == "" || str2 == null)
            {
                this.lblTrade.Text = "购彩交易0元";
            }
            else
            {
                this.lblTrade.Text = "购彩交易" + str2 + "元";
            }
            if (str4 == "" || str4 == null)
            {
                this.lblPayout.Text = "找零金额" + 0 + "元";
            }
            else
            {
                this.lblPayout.Text = "找零金额" + str4 + "元";
            }
            if (str3 == "" || str3 == null)
            {
                this.lblEatCash.Text = "吞钞金额" + 0 + "元";
            }
            else
            {
                this.lblEatCash.Text = "吞钞金额" + str3 + "元";
            }
            if (str5 == "" || str5 == null)
            {
                this.lblLotteryPrize.Text = "兑奖金额" + 0 + "元";
            }
            else
            {
                this.lblLotteryPrize.Text = "兑奖金额" + str5 + "元";
            }
            if (str6 == "" || str6 == null)
            {
                this.linLblPrize.Text = "待兑奖金额" + 0 + "元";
            }
            else
            {
                this.linLblPrize.Text = "待兑奖金额" + str6 + "元";
            }
            
            this.lblTradeCash.Text = "现金交易" + strCash + "元";
            this.lblTradeWeiXin.Text = "微信交易" + strWeiXin + "元";
            this.lblTradeZhifubao.Text = "支付宝交易" + strZhifubao + "元";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindLinkLabel();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            BindLinkLabel();
        }

        private void linLblPrize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                sql = "SELECT LotterySerialNumber AS 彩票序列号,PrizeQuota AS 金额,DrawTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=99 AND DrawTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT LotterySerialNumber AS 彩票序列号, PrizeQuota AS 金额,DrawTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=99 AND DrawTime BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblLotteryPrize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                sql = "SELECT LotterySerialNumber AS 彩票序列号,PrizeQuota AS 金额,GetCashTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=1 AND GetCashTime BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT LotterySerialNumber AS 彩票序列号, PrizeQuota AS 金额,GetCashTime AS 兑奖时间 FROM tbllotteryprize WHERE PrizeQuota<>0 AND PrizeDrawFlag=1 AND GetCashTime BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblReceCash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                if (Convert.ToDateTime(this.dateTimePicker2.Value) > Convert.ToDateTime(this.dateTimePicker1.Value))
                {
                    sql = "SELECT BillMount AS 面值,CASE BillFlag WHEN 1 THEN '纸币' WHEN 0 THEN '硬币' END  as 币种 ,count(BillMount) as 数量,sum(CashCount) * BillMount as 金额 FROM tblComeCash WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY BillMount ORDER BY Cast(billmount AS INSTEAD)";
                }
                else
                {
                    sql = "SELECT BillMount AS 面值,CASE BillFlag WHEN 1 THEN '纸币' WHEN 0 THEN '硬币' END  as 币种 ,count(BillMount) as 数量,sum(CashCount) * BillMount as 金额 FROM tblComeCash WHERE Time BETWEEN '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY BillMount ORDER BY Cast(billmount AS INSTEAD)";
                }
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                this.groupBox1.Text = "";
                sql = "SELECT BillMount AS 面值,CASE BillFlag WHEN 1 THEN '纸币' WHEN 0 THEN '硬币' END  as 币种 ,count(BillMount) as 数量,sum(CashCount) * BillMount as 金额 FROM tblComeCash WHERE Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY BillMount ORDER BY Cast(billmount AS INSTEAD)";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblTrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                if (Convert.ToDateTime(this.dateTimePicker2.Value) > Convert.ToDateTime(this.dateTimePicker1.Value))
                {
                    sql = "SELECT tblMotorInfo.motorid as 机头号 ,tblTradeInfo.LotteryName AS 彩种名称,ifnull(count(tradeid),0) as 出票数量,ifnull(sum(trademoney) ,0) as 金额,tblMotorInfo.LotteryCount as 余票 from tblMotorInfo LEFT OUTER JOIN tblTradeInfo on tblTradeInfo.MotorId=tblMotorInfo.MotorId  WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY tblTradeInfo.MotorId,tblTradeInfo.LotteryName";
                }
                else
                {
                    sql = "SELECT tblMotorInfo.motorid as 机头号 ,tblTradeInfo.LotteryName AS 彩种名称,ifnull(count(tradeid),0) as 出票数量,ifnull(sum(trademoney) ,0) as 金额,tblMotorInfo.LotteryCount as 余票 from tblMotorInfo LEFT OUTER JOIN tblTradeInfo on tblTradeInfo.MotorId=tblMotorInfo.MotorId  WHERE Time BETWEEN '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY tblTradeInfo.MotorId,tblTradeInfo.LotteryName";
                }
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT tblMotorInfo.motorid as 机头号 ,tblTradeInfo.LotteryName AS 彩种名称,ifnull(count(tradeid),0) as 出票数量,ifnull(sum(trademoney) ,0) as 金额,tblMotorInfo.LotteryCount as 余票 from tblMotorInfo LEFT OUTER JOIN tblTradeInfo on tblTradeInfo.MotorId=tblMotorInfo.MotorId  WHERE Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY tblTradeInfo.MotorId,tblTradeInfo.LotteryName";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblPayout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                if (Convert.ToDateTime(this.dateTimePicker2.Value) > Convert.ToDateTime(this.dateTimePicker1.Value))
                {
                    sql = "SELECT CashType AS 纸币币种, sum(cashCount) AS 纸币张数,CoinType AS 硬币币种,sum(CoinCount) as 硬币个数 FROM tblPayout WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' group by cashType,CoinType";
                }
                else
                {
                    sql = "SELECT CashType AS 纸币币种, sum(cashCount) AS 纸币张数,CoinType AS 硬币币种,sum(CoinCount) as 硬币个数 FROM tblPayout WHERE Time BETWEEN '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59") + "' group by cashType,CoinType";
                }
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT CashType AS 纸币币种, sum(cashCount) AS 纸币张数,CoinType AS 硬币币种,sum(CoinCount) as 硬币个数 FROM tblPayout WHERE Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblEatCash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                if (Convert.ToDateTime(this.dateTimePicker2.Value) > Convert.ToDateTime(this.dateTimePicker1.Value))
                {
                    sql = "SELECT  Money AS 吞钞金额,time AS 时间 FROM EatCash WHERE Time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                else
                {
                    sql = "SELECT  Money AS 吞钞金额,time AS 时间 FROM EatCash WHERE Time BETWEEN '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT  Money AS 吞钞金额,time AS 时间 FROM EatCash WHERE Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoAll.Checked)
            {
                loadTradeDataFromClearCashToNow();
            }
        }

        private void loadTradeDataFromClearCashToNow()
        {
            this.groupBox1.Text = "";


            this.DgvTradeList.DataSource = null;
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker2.Enabled = false;

            string str = SQLiteHelper.GetSingle("SELECT sum(BillMount*CashCount) FROM  tblComeCash WHERE Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            string str2 = SQLiteHelper.GetSingle("SELECT sum(tradeMoney) FROM tblTradeInfo where Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            string str3 = SQLiteHelper.GetSingle("SELECT sum(cashCount*cashType)+sum(coinCount*cointype) FROM tblPayout where Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            string str4 = SQLiteHelper.GetSingle("SELECT sum(Money) FROM EatCash where Time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            string str5 = SQLiteHelper.GetSingle("SELECT sum(PrizeQuota) FROM tbllotteryprize where GetCashTime BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND PrizeDrawFlag=1").ToString();
            string str6 = SQLiteHelper.GetSingle("SELECT sum(PrizeQuota) FROM tbllotteryprize where DrawTime BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND PrizeDrawFlag=99").ToString();

            string strWeiXin = SQLiteHelper.GetSingle("SELECT sum(d.unit_price) FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 AND o.pay_type = 2 AND d.ticket_out_time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            if (strWeiXin == "")
                strWeiXin = "0";

            //支付宝交易金额
            string strZhifubao = SQLiteHelper.GetSingle("SELECT sum(d.unit_price) FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 AND o.pay_type = 3 AND d.ticket_out_time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
            if (strZhifubao == "")
                strZhifubao = "0";

            //现金交易金额
            string strCash = (Int32.Parse(str2) - Int32.Parse(strWeiXin) - Int32.Parse(strZhifubao)) + "";

            if (str == "")
            {
                str = "0";
            }
            if (str2 == "")
            {
                str2 = "0";
            }
            if (str3 == "")
            {
                str3 = "0";
            }
            if (str4 == "")
            {
                str4 = "0";
            }
            if (str5 == "")
            {
                str5 = "0";
            }
            if (str6 == "")
            {
                str6 = "0";
            }

            this.lblReceCash.Text = "收入金额" + str + "元";
            this.lblTrade.Text = "购彩交易" + str2 + "元";
            this.lblPayout.Text = "找零金额" + str3 + "元";
            this.lblEatCash.Text = "吞钞金额" + str4 + "元";
            this.lblLotteryPrize.Text = "兑奖金额" + str5 + "元";
            this.linLblPrize.Text = "待兑奖金额" + str6 + "元";

            this.lblTradeCash.Text = "现金交易" + strCash + "元";
            this.lblTradeWeiXin.Text = "微信交易" + strWeiXin + "元";
            this.lblTradeZhifubao.Text = "支付宝交易" + strZhifubao + "元";
        }

        private void rdoQingChao_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (this.rdoQingChao.Checked)
                {

                    this.dateTimePicker1.Enabled = true;
                    this.dateTimePicker2.Enabled = true;

                    this.BindLinkLabel();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.time = SQLiteHelper.GetSingle("select CleanCashTime from tblCleanCashTime where id=(select Max(id) from tblCleanCashTime) ").ToString();
            if (this.time == "")
            {
                this.label3.Text = "还未进行过清钞操作";
                this.rdoAll.Enabled = false;
            }
            else
            {
                this.label3.Text = "上次清钞时间:" + this.time;
            }

            this.lblLocalNumber.Text = "终端编号：" + GlobalParmeters.machineNumber;

            this.loadTradeDataFromClearCashToNow();
        }

        private void lblTradeWeiXin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sql = "";
            this.DgvTradeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (this.rdoQingChao.Checked)
            {
                if (Convert.ToDateTime(this.dateTimePicker2.Value) > Convert.ToDateTime(this.dateTimePicker1.Value))
                {
                    sql = "SELECT d.head_no as 机头, d.lottery_name 彩票名称, sum(d.unit_price) as '金额(元)' FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 and o.pay_type = 2 and d.ticket_out_time BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY d.head_no, d.lottery_name";
                }
                else
                {
                    sql = "SELECT d.head_no as 机头, d.lottery_name 彩票名称, sum(d.unit_price) as '金额(元)' FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 and o.pay_type = 2 and d.ticket_out_time BETWEEN '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY d.head_no, d.lottery_name";
                }
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
            else
            {
                sql = "SELECT d.head_no as 机头, d.lottery_name 彩票名称, sum(d.unit_price) as '金额(元)' FROM p_terminal_order_details d, p_terminal_orders o where d.out_trade_no = o.out_trade_no and d.ticket_out_state = 1 and o.pay_type = 2 and d.ticket_out_time BETWEEN '" + this.time + "' AND '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY d.head_no, d.lottery_name";
                this.DgvTradeList.DataSource = SQLiteHelper.Query(sql).Tables[0];
            }
        }

        private void lblTradeCash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("明细未实现");
        }

        private void lblTradeZhifubao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
