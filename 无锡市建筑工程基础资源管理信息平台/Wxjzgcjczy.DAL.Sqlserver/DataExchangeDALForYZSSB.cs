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
    /// 功能： 无锡数据中心与各县市系统数据交换数据访问类之一站式申报数据访问
    /// 作者：黄正宇
    /// 时间：2017-09-10
    /// </summary>
    public class DataExchangeDALForYZSSB
    {
        public DBOperator DB { get; set; }

     

        #region 安监申报数据

        /// <summary>
        /// 获取安监申报表
        /// </summary>
        /// <param name="date">申报日期</param>
        /// <param name="countryCodes">区划代码</param>
        /// <returns></returns>
        public DataTable GetAp_ajsbb(string date, string countryCodes)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"SELECT  b.uuid,b.xmmc
                        ,b.PrjNum
                        ,b.PrjName
                        ,b.Ajjgmc
                        ,b.AjCorpCode
                        ,b.PrjSize
                        ,b.EconCorpName
                        ,b.EconCorpCode
                        ,b.PrjApprovalNum
                        ,b.BuldPlanNum
      ,b.ProjectPlanNum
      ,b.CityNum
      ,b.CountyNum
      ,b.PrjTypeNum
      ,b.sPrjTypeNum
      ,b.PrjFunctionNum
      ,b.sbr
      ,b.sbryddh
      ,b.CreateDate
      ,b.sfzps
      ,b.sfbz
      ,b.jdz
      ,b.wdz
      ,b.mj
      ,b.zj
      ,b.jgcc
      ,b.sbmb
      ,b.sfjk
      ,b.sgxkz
      ,b.UpdateFlag
      ,b.UpdateTime
      ,b.UpdateUser
	  ,u.countryCode
    FROM dbo.Ap_ajsbb b 
    LEFT JOIN dbo.Ap_api_user u ON u.deptCode = b.UpdateUser
    WHERE SUBSTRING(convert(VARCHAR(30), b.FetchDate, 120), 1, 10)=@date
    AND countryCode in (" + AntiSqlInjection.ParameterizeInClause(countryCodes, "@para", ref sp) + ")";

            sp.Add("@date", date);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ap_ajsbb");

        }

        public DataTable GetAp_ajsbb_ht(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_ht where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_ht");
        }

        public DataTable GetAp_ajsbb_dwry(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_dwry where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_dwry");
        }

        public DataTable GetAp_ajsbb_clqd(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_clqd where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_clqd");
        }

        public DataTable GetAp_ajsbb_hjssjd(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_hjssjd where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_hjssjd");
        }

        public DataTable GetAp_ajsbb_wxyjdgcqd(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_wxyjdgcqd where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_wxyjdgcqd");
        }

        public DataTable GetAp_ajsbb_cgmgcqd(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_ajsbb_cgmgcqd where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "ajsbb_cgmgcqd");
        }

        #endregion


        #region 质监申报数据

        /// <summary>
        /// 获取质监申报表
        /// </summary>
        /// <param name="date">申报日期</param>
        /// <param name="countryCodes">区划代码</param>
        /// <returns></returns>
        public DataTable GetAp_zjsbb(string date, string countryCodes)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"SELECT  b.uuid,b.xmmc
                        ,b.PrjNum
                        ,b.PrjName
                        ,b.gcdz
                        ,b.zjjgmc
                        ,b.zjCorpCode
                        ,b.PrjSize
                        ,b.EconCorpName
                        ,b.EconCorpCode
                        ,b.PrjApprovalNum
                        ,b.BuldPlanNum
      ,b.ProjectPlanNum
      ,b.CityNum
      ,b.CountyNum
      ,b.PrjTypeNum
      ,b.PrjFunctionNum
      ,b.sbr
      ,b.sbryddh
      ,b.CreateDate
      ,b.sfzps
      ,b.UpdateFlag
      ,b.UpdateTime
      ,b.UpdateUser
	  ,u.countryCode
    FROM dbo.Ap_zjsbb b 
    LEFT JOIN dbo.Ap_api_user u ON u.deptCode = b.UpdateUser
    WHERE SUBSTRING(convert(VARCHAR(30), b.FetchDate, 120), 1, 10)=@date
    AND countryCode in (" + AntiSqlInjection.ParameterizeInClause(countryCodes, "@para", ref sp) + ")";

            sp.Add("@date", date);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ap_zjsbb");

        }

        public DataTable GetAp_zjsbb_ht(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_zjsbb_ht where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "zjsbb_ht");
        }

        public DataTable GetAp_zjsbb_dwry(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_zjsbb_dwry where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "zjsbb_dwry");
        }

        public DataTable GetAp_zjsbb_schgs(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_zjsbb_schgs where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "zjsbb_schgs");
        }

        public DataTable GetAp_zjsbb_dwgc(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_zjsbb_dwgc where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "zjsbb_dwgc");
        }

        public DataTable GetAp_zjsbb_clqd(string uuid)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_zjsbb_clqd where uuid=@uuid";
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "zjsbb_clqd");
        }

        #endregion

    }
}
