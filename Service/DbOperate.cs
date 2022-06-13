using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using YTDSSTGenII.Utils;

using YTDSSTGenII.Forms;

namespace YTDSSTGenII.Utils
{
    class DbOperate
    {
        private SQLiteConnection conn;
        private static readonly string connectionString = @"Data Source=D:\Db\cashmachineclientdb";
        private int id;
        private int id1;

        internal bool AddLottery(int MotorNumber, string p, bool ChangedLotteryNameFlag, int AddedLotteryRemain, bool ChangedLotteryRemainFlag, int LotteryCount)
        {
            try
            {
                string str = "";
                string str2 = "";
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                if (ChangedLotteryNameFlag)
                {
                    DataSet dataSet = new DataSet();
                    DataTable table = new DataTable();
                    new SQLiteDataAdapter("select * from tblLotteryInfo where LotteryName ='" + p + "'", this.Connection).Fill(dataSet);
                    table = dataSet.Tables[0];
                    if (table.Rows.Count < 1)
                    {
                        return false;
                    }
                    str = table.Rows[0]["LotteryLength"].ToString();
                    str2 = table.Rows[0]["LotteryPrice"].ToString();
                    SQLiteCommand command = new SQLiteCommand("update tblMotorInfo set LotteryName='" + p.ToString() + "',LotteryLength='" + str + "',LotteryPrice='" + str2 + "' where MotorId =" + MotorNumber.ToString(), this.Connection);
                    SQLiteTransaction transaction = this.Connection.BeginTransaction();
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
                if (ChangedLotteryRemainFlag)
                {
                    SQLiteCommand command2 = new SQLiteCommand("update tblMotorInfo set LotteryCount='" + AddedLotteryRemain.ToString() + "' where MotorId =" + MotorNumber.ToString(), this.Connection);
                    SQLiteTransaction transaction2 = this.Connection.BeginTransaction();
                    command2.Transaction = transaction2;
                    command2.ExecuteNonQuery();
                    command2.Transaction.Commit();
                }
                if (LotteryCount > 0)
                {
                    
                    SQLiteCommand command3 = new SQLiteCommand(string.Concat(new object[] { "insert into tblException(MotorId,LotteryName,LotteryCount,ChageLotteryName,Time,UploadFlag) values ('", MotorNumber, "','", p, "',", LotteryCount, ",'", FormAddLottery.lName, "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','false')" }), this.Connection);
                    SQLiteTransaction transaction3 = this.Connection.BeginTransaction();
                    command3.Transaction = transaction3;
                    command3.ExecuteNonQuery();
                    command3.Transaction.Commit();
                }
                
                SQLiteCommand command4 = new SQLiteCommand(string.Concat(new object[] { "insert into tblAddLotteryInfo (MotorId,TradeTime,LotteryName,LotteryCount,UploadFlag,UserName)values('", MotorNumber.ToString(), "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','", p, "','", AddedLotteryRemain, "','false','", FormLogin.UserName, "')" }), this.Connection);
                SQLiteTransaction transaction4 = this.Connection.BeginTransaction();
                command4.Transaction = transaction4;
                command4.ExecuteNonQuery();
                command4.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
            return true;
        }

        internal void AddLotteryInfo(string p, string p_2, string p_3, string p_4)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("insert into tblLotteryInfo (LotteryId,LotteryName,LotteryLength,LotteryPrice)values('" + p + "','" + p_2 + "','" + p_3 + "','" + p_4 + "')", this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        public void CloseConn()
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                this.Connection.Close();
                this.conn.Close();
                this.conn.Dispose();
                this.conn = null;
            }
        }

        internal void Decrease(int CutPaperMotorNumber, int p)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("update tblMotorInfo set LotteryCount='" + p.ToString() + "' where MotorId =" + CutPaperMotorNumber.ToString(), this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            this.Connection.Close();
        }

        internal List<string> GetAllLotteryId()
        {
            List<string> list = new List<string>();
            try
            {
                DataSet dataSet = new DataSet();
                DataTable table = new DataTable();
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                string commandText = "select * from tblLotteryInfo";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(table.Rows[i]["LotteryId"].ToString());
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return list;
        }

        internal string GetAllLotteryNumber()
        {
            string str = "";
            try
            {
                DataSet dataSet = new DataSet();
                DataTable table = new DataTable();
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                string commandText = "select * from tblLotteryInfo";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return str;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    str = str + table.Rows[i]["LotteryId"].ToString() + ",";
                }
                str = str.TrimEnd(new char[] { ',' });
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return str;
        }

        internal DataTable GetAllMotorInfo()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                string commandText = "select MotorId,LotteryName,LotteryLength,LotteryPrice,LotteryCount from tblMotorInfo";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return table;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return table;
            }
            return table;
        }

        internal int[] GetDayTradeData()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            int[] numArray = new int[0x18];
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblTradeLog where TradeDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return numArray;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int num2 = int.Parse(table.Rows[i]["MotorId"].ToString());
                    numArray[num2 - 1] += int.Parse(table.Rows[i]["Money"].ToString());
                    numArray[num2 + 11]++;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return numArray;
        }

        private string GetLotteryIdByLotteryName(string p)
        {
            string str = "";
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblLotteryInfo where LotteryName ='" + p + "'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return str;
                }
                str = table.Rows[0]["LotteryId"].ToString();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return str;
            }
            return str;
        }

        internal List<string> GetLotteryInfo()
        {
            List<string> list = new List<string>();
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                string commandText = "select * from tblLotteryInfo";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(table.Rows[i]["LotteryName"].ToString());
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return list;
            }
            return list;
        }

        internal List<string> GetLotteryInfo(string LotteryId)
        {
            List<string> list = new List<string>();
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblLotteryInfo where gamecode like '%" + int.Parse(LotteryId) + "%'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(table.Rows[i]["LotteryName"].ToString());
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;
        }

        private string GetLotteryLength(string p)
        {
            if ("2" != p)
            {
                if ("4" == p)
                {
                    return "102";
                }
                if ("6" == p)
                {
                    return "153";
                }
                if ("8" == p)
                {
                    return "203";
                }
            }
            return "51";
        }

        internal int[] GetMonthTradeData()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            int[] numArray = new int[0x18];
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblTradeLog where TradeDate like '%" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "%'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return numArray;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int num2 = int.Parse(table.Rows[i]["MotorId"].ToString());
                    numArray[num2 - 1] += int.Parse(table.Rows[i]["Money"].ToString());
                    numArray[num2 + 11]++;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return numArray;
        }

        internal bool GetMotorInfoByMotorNumber(int CutPaperMotorNumber, ref int MotorLength, ref int LotteryPrice, ref int MotorLotteryRemain, ref string LotteryName)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblMotorInfo where MotorId =" + CutPaperMotorNumber.ToString(), this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return false;
                }
                MotorLength = int.Parse(table.Rows[0]["LotteryLength"].ToString());
                LotteryPrice = int.Parse(table.Rows[0]["LotteryPrice"].ToString());
                MotorLotteryRemain = int.Parse(table.Rows[0]["LotteryCount"].ToString());
                LotteryName = table.Rows[0]["LotteryName"].ToString();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
            this.Connection.Close();
            return true;
        }

        internal void GetNameAndRemainByMotorNumber(int MotorNumber, ref string LotteryName, ref int LotteryRemain)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblMotorInfo where MotorId =" + MotorNumber.ToString(), this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count >= 1)
                {
                    LotteryRemain = int.Parse(table.Rows[0]["LotteryCount"].ToString());
                    LotteryName = table.Rows[0]["LotteryName"].ToString();
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        internal int[] GetWeekTradeData()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            int[] numArray = new int[0x18];
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                DateTime now = DateTime.Now;
                int dayOfWeek = (int)now.DayOfWeek;
                string str = now.AddDays((double)(-dayOfWeek + 1)).ToString("yyyy-MM-dd HH:mm:ss");
                string str2 = now.AddDays((double)(7 - dayOfWeek)).ToString("yyyy-MM-dd HH:mm:ss");
                if (dayOfWeek == 0)
                {
                    str = now.AddDays(-6.0).ToString("yyyy-MM-dd HH:mm:ss");
                    str2 = now.AddDays(0.0).ToString("yyyy-MM-dd HH:mm:ss");
                }
                new SQLiteDataAdapter("select * from tblTradeLog where TradeDate>='" + str + "' and TradeDate<='" + str2 + "'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return numArray;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int num3 = int.Parse(table.Rows[i]["MotorId"].ToString());
                    numArray[num3 - 1] += int.Parse(table.Rows[i]["Money"].ToString());
                    numArray[num3 + 11]++;
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            return numArray;
        }

        public void InsertTrade(string motorId, string money)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("insert into tblTradeLog (MotorId,Money,TradeDate,TradeTime)values('" + motorId + "','" + money + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToShortTimeString() + "')", this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        internal void InsertTrade(string p, string p_2, string LotteryName)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                DataSet dataSet = new DataSet();
                DataTable table = new DataTable();
                string commandText = "select Max(Id) AS a from (select tblComeCash.id as Id from tblComeCash Union select tbllotteryprize.id as Id from tbllotteryprize ) t";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.id = Convert.ToInt32(table.Rows[0]["a"]);
                }
                SQLiteCommand command = new SQLiteCommand(string.Concat(new object[] { "insert into tblTradeInfo (MotorId,TradeId,LotteryName,TradeMoney,UploadFlag,Time,CashId)values('", p, "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','", LotteryName, "','", p_2, "','false','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "',", this.id, ")" }), this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            this.Connection.Close();
        }

        internal void InsertTradeFailInfo(string p, string p_2, string LotteryName)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                DataSet dataSet = new DataSet();
                DataTable table = new DataTable();
                string commandText = "select max(id) as a from tblComeCash";
                new SQLiteDataAdapter(commandText, this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.id1 = Convert.ToInt32(table.Rows[0]["a"]);
                }
                SQLiteCommand command = new SQLiteCommand(string.Concat(new object[] { "insert into tblTradeFailInfo (MotorId,TradeId,LotteryName,TradeMoney,UploadFlag,Time,CashId)values('", p, "','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','", LotteryName, "','", p_2, "','false','", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "',", this.id1, ")" }), this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
            this.Connection.Close();
        }

        internal void UpDateAddLotteryUploadFlag(string p)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("update tblAddLotteryInfo set UploadFlag='true' where TradeId ='" + p + "'", this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        internal void UpDateLotteryInfo(string LotteryInfos)
        {
            try
            {
                foreach (string str in LotteryInfos.Split(new char[] { ';' }))
                {
                    string[] strArray2 = str.Split(new char[] { ',' });
                    DataSet dataSet = new DataSet();
                    DataTable table = new DataTable();
                    if (this.Connection.State == ConnectionState.Closed)
                    {
                        this.Connection.Open();
                    }
                    new SQLiteDataAdapter("select * from tblLotteryInfo where LotteryName='" + strArray2[1] + "'", this.Connection).Fill(dataSet);
                    table = dataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        SQLiteCommand command = new SQLiteCommand("delete from tblLotteryInfo  where LotteryName='" + strArray2[1] + "'", this.Connection);
                        SQLiteTransaction transaction = this.Connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        command.Transaction.Commit();
                    }
                    SQLiteCommand command2 = new SQLiteCommand("insert into tblLotteryInfo(LotteryId,LotteryName,LotteryLength,LotteryPrice) values ('" + strArray2[0] + "','" + strArray2[1] + "','" + this.GetLotteryLength(strArray2[2]) + "','" + strArray2[3] + "')", this.Connection);
                    SQLiteTransaction transaction2 = this.Connection.BeginTransaction();
                    command2.Transaction = transaction2;
                    command2.ExecuteNonQuery();
                    command2.Transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        internal void UpDateUploadFlag(string p)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("update tblTradeInfo set UploadFlag='true' where TradeId ='" + p + "'", this.Connection);
                SQLiteTransaction transaction = this.Connection.BeginTransaction();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
            }
        }

        public SQLiteConnection Connection
        {
            get
            {
                try
                {
                    if (this.conn == null)
                    {
                        this.conn = new SQLiteConnection();
                        this.conn.ConnectionString = connectionString;
                    }
                }
                catch (Exception exception)
                {
                    CommonUtils.WriteExceptionInfo(exception);
                    throw new Exception("实例化Connection出错!错误编码sqlite0001" + exception.Message);
                }
                return this.conn;
            }
        }
    }
}
