using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using System.Configuration;

namespace WxjzgcjczyQyb.DAL
{
    public class DatabaseOperator : SqlServerDbOperator
    {
        public DatabaseOperator()
            : base(ConfigManager.GetConnectionString(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType().ToUpper()))
        { 
        }
    }

    public class DatabaseOperator_Webplat50 : SqlServerDbOperator
    {
        public DatabaseOperator_Webplat50()
            : base(ConfigManager.GetConnectionString_Webplat50(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType().ToUpper()))
        {
        }
    }
}
