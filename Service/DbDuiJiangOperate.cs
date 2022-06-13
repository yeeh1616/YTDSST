using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SQLite;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Utils
{
    class DbDuiJiangOperate
    {
        private SQLiteConnection conn;
        private static readonly string connectionString = @"Data Source=D:\Db\LotteryPrizeTest";


        internal void AlredayDrawUpdate(string securityNumber)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("update tblLotteryPrize set PrizeDrawFlag='1' where SecurityNumber ='" + securityNumber + "'", this.Connection);
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

        internal bool GetPrizeBySecurityNumber(string securityNumber, ref string prizeDrawFlag, ref string prizeQuota)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                new SQLiteDataAdapter("select * from tblLotteryPrize where SecurityNumber ='" + securityNumber + "'", this.Connection).Fill(dataSet);
                table = dataSet.Tables[0];
                if (table.Rows.Count < 1)
                {
                    return false;
                }
                prizeDrawFlag = table.Rows[0]["PrizeDrawFlag"].ToString();
                prizeQuota = table.Rows[0]["PrizeQuota"].ToString();
            }
            catch (Exception exception)
            {
                CommonUtils.WriteExceptionInfo(exception);
                return false;
            }
            return true;
        }

        public void InsertTrade(string motorId, string money)
        {
            try
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
                SQLiteCommand command = new SQLiteCommand("insert into tblTradeLog (MotorId,Money,TradeDate,TradeTime)values('" + motorId + "','" + money + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToShortTimeString() + "')", this.Connection);
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
                    throw new Exception("实例化Connection出错!错误编码sqlite0001" + exception.Message);
                }
                return this.conn;
            }
        }
    }
}
