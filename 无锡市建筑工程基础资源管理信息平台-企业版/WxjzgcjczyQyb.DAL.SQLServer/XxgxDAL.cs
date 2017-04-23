using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace WxjzgcjczyQyb.DAL
{
    public class XxgxDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveApiZb(List<IDataItem> list, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from  API_zb
                           where  apiUrl is not null";
            list.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }


        public void UpdateApiZbApiControl(string apiFlow, string apiControl)
        {
            string sql = @" update API_zb set apiControl=@apiControl  where apiFlow=@apiFlow ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiControl", apiControl);
            sp.Add("@apiFlow", apiFlow);
            DB.ExecuteNonQuerySql(sql, sp);
        }



        public DataTable GetApizbByApiFlow(string apiFlow)
        {
            string sql = @"select * from  API_zb
                           where  apiFlow=@apiFlow";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiFlow", apiFlow);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable RetrieveApiCb(List<IDataItem> list, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from ( select a.*,b.apiName
                           from  API_cb a left join API_zb  b on a.apiFlow=b.apiFlow) as a where 1=1
                              ";
            list.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        public DataTable GetStLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string sfsccg, string orderby)
        {
            string sql = @"select * from (  select *,case when OperateState='0' then '是' else '否' end as OperateStateMsg from SaveToStLog 
                           where     1=1) as a where 1=1 and  datediff(day,UpdateDate,GETDATE())=0 and  Msg!='007'";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(sfsccg))
            {
                sp.Add("@OperateStateMsg", sfsccg);
                sql = sql + " and OperateStateMsg=@OperateStateMsg ";
            }

            condition.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }


        public DataTable GetSyzxspt(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string orderby, string tableName)
        {
            string sql = "select * from "+tableName+" where 1=1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            condition.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }
    }
}
