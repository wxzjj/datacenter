using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class XxgxDAL
    {
        public DBOperator DB { get; set; }


        #region  获取列表
        public DataTable GetXxgx_Csjk()
        {
            string sql = @" select  a.*,(select count(*) from  datajkdataDetail b where a.ID=dataJKLogID and IsOk=1) as IsOk from  datajklog  a 
where  CONVERT(varchar(10),a.csTime,120)= CONVERT(varchar(10),getdate(),120) ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        #endregion
    }
}
