using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using System.Configuration;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class DatabaseOperatorYHT : SqlServerDbOperator
    {
        public DatabaseOperatorYHT()
            : base(ConfigManager.GetConnectionString_YHTSqlserver(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType_Sqlserver().ToUpper()))
        {
        }
        public DatabaseOperatorYHT(int cmdTimeOut)
            : base(ConfigManager.GetConnectionString_YHTSqlserver(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType_Sqlserver().ToUpper()), cmdTimeOut)
        {
        }

    }
}
