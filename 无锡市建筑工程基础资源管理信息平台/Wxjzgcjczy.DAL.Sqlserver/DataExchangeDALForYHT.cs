using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy.Common;
using Bigdesk8.Security;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    /// <summary>
    /// 功能： 一号通数据交换
    /// 作者：
    /// 时间：2018-10-17
    /// </summary>
    public class DataExchangeDALForYHT
    {

        public DBOperator DB { get; set; }

        public DataTable GetTBProjectInfo_PrjApprovalNum(string prjNum)
        {
            string sql = @"select (lxwh_type+'['+fb_year+']'+num+'号') FROM DG_Programme_Info where PrjNum=@prjNum		  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@prjNum", prjNum);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        
    }
}
