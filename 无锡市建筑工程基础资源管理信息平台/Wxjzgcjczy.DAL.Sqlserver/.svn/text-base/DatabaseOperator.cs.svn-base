using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using System.Configuration;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class DatabaseOperator : SqlServerDbOperator
    {
        public DatabaseOperator()
            : base(ConfigManager.GetConnectionString_Sqlserver(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType_Sqlserver().ToUpper()))
        {
        }
        public DatabaseOperator(int cmdTimeOut)
            : base(ConfigManager.GetConnectionString_Sqlserver(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType_Sqlserver().ToUpper()), cmdTimeOut)
        {
        }

    }
}
