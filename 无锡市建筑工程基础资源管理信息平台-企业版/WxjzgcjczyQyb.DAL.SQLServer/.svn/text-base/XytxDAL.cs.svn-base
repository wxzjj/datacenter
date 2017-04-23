using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace WxjzgcjczyQyb.DAL
{
    public class XytxDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveQyxykp( List<IDataItem> list,int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from (select a.*,b.qyID from XykpImport a
                           left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
                           where 1=1 and qyID is not null ";
            IDataItem zzlbdt = list.GetDataItem("zzlb");
            if (zzlbdt != null)
            {
                sql += " and zzlb in (@zzlb)";
                sp.Add("@zzlb", zzlbdt.ItemData);
                list.Remove(zzlbdt);
            }
            IDataItem qysddt = list.GetDataItem("qysd");
            if (qysddt != null)
            {
                sql += " and qysd in (@qysd)";
                sp.Add("@qysd", qysddt.ItemData);
                list.Remove(qysddt);
            }

            list.GetSearchClause(sp,ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }
    }
}
