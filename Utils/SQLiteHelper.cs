using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace YTDSSTGenII.Utils
{

    class SQLiteHelper
    {
        private static string connectionString = @"Data Source=D:\Db\cashmachineclientdb";//D:\Db

        public static List<string> CopyToList(string sql)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in Query(sql).Tables[0].Rows)
            {
                list.Add(row[0].ToString());
            }
            return list;
        }

        public static SQLiteDataReader ExecuteReader(string sql)
        {
            SQLiteDataReader reader2;
                SQLiteConnection connection = new SQLiteConnection ( connectionString );
                SQLiteCommand command = new SQLiteCommand ( sql, connection );
                try
                {
                    connection.Open ( );
                    reader2 = command.ExecuteReader ( CommandBehavior.CloseConnection );
                }
                catch ( SQLiteException exception )
                {
                    throw exception;
                }
                return reader2;   
        }

        public static int ExecuteSql(string sql)
        {
            int num2;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SQLiteException exception)
                {
                    connection.Close();
                    throw new SQLiteException(exception.Message);
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSql(string sql, params SQLiteParameter[] cmdParms)
        {
            int num2;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, sql, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (SQLiteException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSqlInsertImg(string Sql, byte[] Img)
        {
            int num2;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(Sql, connection);
                SQLiteParameter parameter = new SQLiteParameter("@Img", DbType.Binary)
                {
                    Value = Img
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SQLiteException exception)
                {
                    throw new SQLiteException(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSqlInsertImg(string Sql, byte[] Img, byte[] Imges)
        {
            int num2;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(Sql, connection);
                SQLiteParameter parameter = new SQLiteParameter("@Img");
                SQLiteParameter parameter2 = new SQLiteParameter("@Imges");
                parameter.Value = Img;
                parameter2.Value = Imges;
                command.Parameters.Add(parameter);
                command.Parameters.Add(parameter2);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SQLiteException exception)
                {
                    throw new SQLiteException(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static void ExecuteSqlTran(List<SqlInfo> sqlList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    try
                    {
                        foreach (SqlInfo info in sqlList)
                        {
                            string sqlString = info.SqlString;
                            SQLiteParameter[] parameters = (SQLiteParameter[])info.Parameters;
                            PrepareCommand(cmd, connection, transaction, sqlString, parameters);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("执行错误,事务回滚");
                    }
                    finally
                    {
                        cmd.Dispose ( );
                        connection.Close ( );
                    }
                }
            }
        }

        public static int ExecuteSqlTran(List<string> sqlList)
        {
            int num3;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection
                };
                SQLiteTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    int num = 0;
                    for (int i = 0; i <= (sqlList.Count - 1); i++)
                    {
                        string str = sqlList[i];
                        if (str.Trim().Length > 1)
                        {
                            command.CommandText = str;
                            num += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    num3 = num;
                }
                catch
                {
                    transaction.Rollback();
                    num3 = 0;
                }
                finally
                {
                    command.Dispose ( );
                    connection.Close ( );
                }
            }
            return num3;
        }

        public static object GetSingle(string SQLString)
        {
            object obj3;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return "";
                    }
                    obj3 = objA;
                }
                catch (SQLiteException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return obj3;
        }

        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parameter in cmdParms)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static DataSet Query(string sql)
        {
            DataSet dataSet = new DataSet ( );
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {               
                try
                {
                    connection.Open();
                    new SQLiteDataAdapter(sql, connection).Fill(dataSet, "ds");
                }
                catch (SQLiteException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    connection.Close ( );
                }               
            }
            return dataSet;
        }
    }
}
