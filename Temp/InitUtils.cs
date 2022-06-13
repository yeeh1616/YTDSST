using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLibrary
{
    /// <summary>
    /// 用来初始化时调用的
    /// </summary>
    public class InitUtils
    {
        /// <summary>
        /// 初始化数据库——升级过程中会增加或是删除一些表
        /// </summary>
        public static void InitDB()
        {
            int result = 0;

            Object o = SQLiteHelper.GetSingle(
                "SELECT COUNT(*) FROM sqlite_master WHERE type='table' and name='tblRejectInfo';");
            int.TryParse(o.ToString(), out result);

            if (result == 0)//创建表 ——富雷的表
            {
                SQLiteHelper.ExecuteSql(@"CREATE TABLE 'tblRejectInfo'('id' INTEGER PRIMARY KEY,'RejectCount' INTEGER,'PayoutCount' INTEGER,'BillType' INTEGER,'Time' TEXT(12) DEFAULT '',UploadFlag TEXT(12) DEFAULT;");
            }


            result = 0;
            Object oorder = SQLiteHelper.GetSingle(
                "SELECT COUNT(*) FROM sqlite_master WHERE type='table' and name='p_terminal_orders';");
            int.TryParse(oorder.ToString(), out result);
            if (result == 0)//创建表 ——订单表
            {
                SQLiteHelper.ExecuteSql(@"CREATE TABLE 'p_terminal_orders' (
                    'id'  INTEGER NOT NULL,
                    'out_trade_no'  TEXT(32) NOT NULL,
                    'terminal_code'  TEXT(32) NOT NULL,
                    'pay_type'  INTEGER NOT NULL,
                    'total_num'  INTEGER NOT NULL,
                    'total_fee'  INTEGER NOT NULL,
                    'order_state'  INTEGER NOT NULL,
                    'create_time'  TEXT NOT NULL,
                    'end_time'  TEXT,
                    'pay_time'  TEXT,
                     'upload_time'  TEXT,
                    'ticket_out_state'  INTEGER NOT NULL,
                    'out_success_num'  INTEGER NOT NULL,
                    'out_success_money'  INTEGER NOT NULL,
                    'out_error_num'  INTEGER NOT NULL,
                    'out_error_money'  INTEGER NOT NULL,
                    'deleted'  INTEGER NOT NULL,
                    'upload_flag' TEXT DEFAULT false,
                    PRIMARY KEY ('id' ASC)
                    );");
            }

            result = 0;
            Object ooorderdetail = SQLiteHelper.GetSingle(
                "SELECT COUNT(*) FROM sqlite_master WHERE type='table' and name='p_terminal_order_details';");
            int.TryParse(ooorderdetail.ToString(), out result);
            if (result == 0)//创建表 ——订单详情表
            {
                SQLiteHelper.ExecuteSql(@"CREATE TABLE 'p_terminal_order_details' (
                                'id'  INTEGER NOT NULL,
                                'terminal_order_id'  INTEGER NOT NULL,
                                'out_trade_no'  TEXT(32),
                                'self_terminal_id'  INTEGER,
                                'terminal_code'  TEXT NOT NULL,
                                'head_no'  TEXT NOT NULL,
                                'ticket_id'  TEXT NOT NULL,
                                'ticket_out_time'  TEXT,
                                'lottery_type_id'  INTEGER NOT NULL,
                                'lottery_name'  TEXT NOT NULL,
                                'unit_price'  INTEGER NOT NULL,
                                'ticket_num'  INTEGER NOT NULL,
                                'ticket_out_state'  INTEGER NOT NULL,
                                'deleted'  INTEGER NOT NULL,
                                'upload_flag' TEXT DEFAULT false,
                                PRIMARY KEY ('id')
                    );");
            }
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        public static void initContext()
        {
            //微信长链接
            Service.Context.ApplicationContext.init();
            UserContext.getInstance().Balance = GlobalParmeters.GotMoney;//用户余额

            for (int i = 0; i < 8; i++)
            {
                //读取机头1的状态和票
                String sql = String.Format("select m.lotteryName, m.lotteryLength, m.LotteryPrice, m.LotteryCount, m.MotorId, l.LotteryId from tblMotorInfo m, tblLotteryInfo l where m.LotteryName = l.LotteryName and m.motorId={0}", i + 1);
                SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sql);
                if (reader.Read())
                {
                    lottery l = new lottery(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]));

                    l.LotteryId = reader[5].ToString();

                    ServerMotor sm = new ServerMotor(Convert.ToInt32(reader[4]).ToString("00"), l, Convert.ToInt32(reader[3]));

                    UserMotor um = new UserMotor(Convert.ToInt32(reader[4]).ToString("00"), l, 0);
                    MachineContext.getInstance().ServerMotorArray[i] = sm;

                    UserContext.getInstance().UserMotorArray[i] = um;
                }
            }
        }

        /// <summary>
        /// 修正数据
        /// </summary>
        public static void correctData()
        {
            //1、先查询所有的出票中的订单;2、查询各个订单下所有的彩票数量；3、统计然后修改订单对应的状态
            try
            {
                String sql = "SELECT * FROM p_terminal_orders WHERE ticket_out_state='1';";

                DataSet ds = SQLiteHelper.Query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<String> sqllist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        String s = String.Format(@"select sum(case when ticket_out_state='0' then 1 else 0 end) as ing,
			            sum(case when ticket_out_state='0' then unit_price else 0 end) as ingprice,
                        sum(case when ticket_out_state='1' then 1 else 0 end) as succ,
			            sum(case when ticket_out_state='1' then unit_price else 0 end) as succprice,
                        sum(case when ticket_out_state='2' then 1 else 0 end) as error,
                        sum(case when ticket_out_state='2' then unit_price else 0 end) as errorprice
                        from p_terminal_order_details WHERE out_trade_no='{0}' group by out_trade_no", ds.Tables[0].Rows[i][1].ToString());

                        DataSet dsone = SQLiteHelper.Query(s);
                        // SQLiteDataReader reader = SQLiteHelper.ExecuteReader(s);
                        if (dsone.Tables[0].Rows.Count > 0)
                        {
                            if (dsone.Tables[0].Rows[0][0].ToString().Equals("0"))//如果正在出票的票数量不为0，那当前订单状态是正确的，不需要修正
                            {
                                String ticketOutState = "4";
                                if (dsone.Tables[0].Rows[0][2].ToString().Equals("0"))//没有出票成功的，全部出票失败
                                {
                                    ticketOutState = "3";
                                }
                                else if (dsone.Tables[0].Rows[0][4].ToString().Equals("0"))//没有出票失败的，全部出票成功
                                {
                                    ticketOutState = "2";
                                }

                                String ordersql = String.Format(@"Update p_terminal_orders set out_success_num = {0},
                out_success_money = {1},out_error_num = {2},out_error_money = {3},ticket_out_state={4} where out_trade_no='{5}'",
                dsone.Tables[0].Rows[0][2].ToString(), dsone.Tables[0].Rows[0][3].ToString(), dsone.Tables[0].Rows[0][4].ToString(), dsone.Tables[0].Rows[0][5].ToString(), ticketOutState, ds.Tables[0].Rows[i][1].ToString());
                                sqllist.Add(ordersql);
                            }
                        }
                    }

                    int r = SQLiteHelper.ExecuteSqlTran(sqllist);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
