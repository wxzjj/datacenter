using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Bigdesk8.Security;
using System.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class XytxDAL
    {
        public DBOperator DB { get; set; }

        #region 数据列表

        public DataTable RetrieveQyxykp(string qylx, AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {


            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";

            switch (qylx)
            {
                case "sgdw":

                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=1 and ";
                    break;
                case "jldw":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                case "kcdw":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                case "sjdw":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                case "zbdljg":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                case "zjzxjg":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                case "jcjg":
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=2 and ";
                    break;
                default :
                    sql = @"select * from (select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm) as aaa
 where 1=1 and ";
                    break;

            }





            string zzlb = ft.GetValue("zzlb");
            if (!string.IsNullOrEmpty(zzlb))
            {
                sql += "  zzlb in (" + AntiSqlInjection.ParameterizeInClause(zzlb, "@para", ref sp) + ") and ";
                ft.Remove("zzlb");
            }

            string qysd = ft.GetValue("qysd");
            if (!string.IsNullOrEmpty(qysd))
            {
                sql += "  qysd in (" + AntiSqlInjection.ParameterizeInClause(qysd, "@pam", ref sp) + ") and ";
                ft.Remove("qysd");
            }

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }
        /// <summary>
        /// 行政处罚列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveXzcf(string type, AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from  xzcf where updateFlag='U' and  ";

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion
    }
}
