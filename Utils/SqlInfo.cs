using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace YTDSSTGenII.Utils
{
    class SqlInfo
    {
        public DbParameter[] Parameters;
        public string SqlString;

        public SqlInfo(string sqlString, DbParameter[] parameters)
        {
            this.SqlString = sqlString;
            this.Parameters = parameters;
        }
    }
}
