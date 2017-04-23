using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using System.Configuration;

namespace Wxjzgcjczy.DAL
{
    public class DatabaseOperator :  OracleOperator 
    {
        public DatabaseOperator()
            : base(ConfigManager.GetConnectionString(), (DataBaseType)Enum.Parse(typeof(DataBaseType), ConfigManager.GetDatabaseType().ToUpper()))
        {
        }
       
    }
}
