using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Business;
using Bigdesk8;
namespace Wxjzgcjczy.DAL
{
    public class FjxxDAL
    {
        public DBOperator DB { get; set; }
        #region 表结构
        public System.Data.DataTable GetSchemaFjxx()
        {
            string sql = "select * from Szgkjc_Jsxm_Fjxx where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        #endregion
    
        #region 读取

        public System.Data.DataTable ReadFjxx(string fjId)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select * from Szgkjc_Jsxm_Fjxx where FjId=@FjId ";
            p.Add("@FjId", fjId);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        #endregion

        #region 读取列表
        public DataTable RetrieveFjxx(AppUser userInfo, string lxxmid, string bdxmid, string ywlxId, string ywjd, string ywlx, string wjmc, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();

            string querySQL1 = @"select * from Szgkjc_Jsxm_Daxx where 1=1 ";
            string querySQL2 = @"select * from Szgkjc_Jsxm_Fjxx where 1=1 ";
            bool isNull = true;
            if (ywjd.ToString2().Length > 0)
            {
                querySQL1 += " and Ywjd=@Ywjd ";
                p.Add("@Ywjd", ywjd);
            }
            if (ywlx.ToString2().Length > 0)
            {
                querySQL1 += " and Ywlx=@Ywlx ";

                p.Add("@Ywlx", ywlx);
            }
            if (wjmc.ToString2().Length > 0)
            {
                querySQL1 += " and Wjmc=@Wjmc ";
                p.Add("@Wjmc", wjmc);
            }
            if (ywlxId.ToString2().Length > 0)
            {
                querySQL2 += " and ywlxId=@ywlxId ";
                p.Add("@ywlxId", ywlxId == "undefined" ? "0" : ywlxId);
                isNull = false;
            }
            if (lxxmid.ToString2().Length > 0)
            {
                querySQL2 += " and LxxmId=@LxxmId ";
                p.Add("@LxxmId", lxxmid);
                isNull = false;
            }
            if (bdxmid.ToString2().Length > 0)
            {
                querySQL2 += " and BdxmId=@BdxmId ";
                p.Add("@BdxmId", bdxmid);
                isNull = false;
            }
            if (isNull)
            {
                querySQL2 += " and 1<>1 ";
            }

            string querySQL = @"select a.*,b.FjId,b.LxxmId,b.BdxmId,b.YwlxId,b.Wjlx,b.Wjxxms,b.WjxxQm,b.Wjxxdz,b.Wjscr,b.EditDate,b.XxContent from (" + querySQL1 + @")  a left join (" + querySQL2 + @")  b on a.Wjbh=b.Wjbh  where 1=1 ";
            allRecordCount = 0;
            return DB.ExeSqlForDataTable(querySQL, p, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 获取立项项目附件信息
        /// </summary>
        /// <param name="lxxmid"></param>
        /// <param name="wjbh"></param>
        /// <returns></returns>
        public DataTable RetrieveFj(string lxxmid, string wjbh)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select  a.* from Szgkjc_Jsxm_Fjxx  a where a.Wjbh=@Wjbh and a.LxxmId=@LxxmId and BdxmId is null and YwlxId is null";
            p.Add("@Wjbh", wjbh);
            p.Add("@LxxmId", lxxmid);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }



        public DataTable RetrieveBdxmxx(string lxxmId)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select distinct * from Szgkjc_Jsxm_Bdxmxx where LxxmId=@lxxmId and zblx='施工招标' ";
            p.Add("@lxxmId", lxxmId);
            return DB.ExeSqlForDataTable(sql, p, "t");
        
        }


        /// <summary>
        /// 获取附件信息
        /// </summary>
        /// <param name="ywlx"></param>
        /// <returns></returns>
        public DataTable Retrievefj(string ywlx)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select a.* from Szgkjc_Jsxm_Daxx  a where a.Ywlx=@Ywlx  ";
            p.Add("@Ywlx", ywlx);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 获取标段项目附件信息
        /// </summary>
        /// <param name="bdxmid"></param>
        /// <param name="wjbh"></param>
        /// <returns></returns>
        public DataTable RetrieveBdxmFjBywjbh(string bdxmid, string wjbh)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select a.* from  Szgkjc_Jsxm_Fjxx  a where a.Wjbh=@Wjbh and a.BdxmId=@BdxmId ";
            p.Add("@Wjbh", wjbh);
            p.Add("@BdxmId", bdxmid);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        /// <summary>
        /// 获取业务类型
        /// </summary>
        /// <param name="ywjd"></param>
        /// <returns></returns>
        public DataTable RetrieveYwlx(string ywjd)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select distinct ywlx from Szgkjc_Jsxm_Daxx  a  where a.Ywjd=@Ywjd ";
            p.Add("@Ywjd", ywjd);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 获取附件名称
        /// </summary>
        /// <param name="ywlx"></param>
        /// <returns></returns>
        public DataTable RetrieveFjmc(string ywlx,string ywjd)
        {


            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string sql = @"select a.* from Szgkjc_Jsxm_Daxx  a  where a.Ywlx=@Ywlx and a.Ywjd=@ywjd ";
            p.Add("@Ywlx", ywlx);
            p.Add("@ywjd",ywjd);
            return DB.ExeSqlForDataTable(sql, p, "t");
        }


        #endregion
    }
}
