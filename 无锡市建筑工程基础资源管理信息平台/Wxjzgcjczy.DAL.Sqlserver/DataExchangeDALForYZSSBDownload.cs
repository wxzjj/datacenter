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
    public class DataExchangeDALForYZSSBDownload
    {
        public DBOperator DB { get; set; }

        #region  读取列表

        /// <summary>
        /// 获取一站式申报平台中无锡市帐号列表
        /// </summary>
        /// <param name="deptType"></param>
        /// <returns></returns>
        public DataTable GetApApiUsers(string deptType)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from Ap_api_user where deptType=@deptType ";
            sp.Add("@deptType", deptType);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetApApiUserByDeptCode(string deptCode)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from Ap_api_user where deptCode=@deptCode ";
            sp.Add("@deptCode", deptCode);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取需要重新抓取的日期列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAp_need_refetch()
        {
            string sql = @"select * from dbo.Ap_need_refetch where status = 0";
            return DB.ExeSqlForDataTable(sql, null, "Ap_need_refetch_dt");
        }

        public bool Submit_Ap_need_refetch(DataTable dt)
        {
            string sql = @"select * from Ap_need_refetch  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        /// <summary>
        /// 获取需要重新抓取的uuid列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAp_need_refetch_uuids(string deptType)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from dbo.Ap_need_refetch_uuid where deptType=@deptType ";
            sp.Add("@deptType", deptType);
            return DB.ExeSqlForDataTable(sql, sp, "Ap_need_refetch_uuid_dt");

        }

        public bool Delete_Ap_need_refetch_uuid(string uuid, string deptType)
        {
            string sql = "delete from dbo.Ap_need_refetch_uuid where uuid=@uuid and deptType=@deptType  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@deptType", deptType);
            return DB.ExecuteNonQuerySql(sql, sp) > 0;
        }


        /// <summary>
        /// 根据apiFlow获取Api详细信息
        /// </summary>
        /// <param name="qyZzjgdm"></param>
        /// <returns></returns>
        public DataTable Get_API_zb_apiFlowDetail(string apiFlow)
        {
            string sql = @" select apiControl, apiFlow, apiName, apiUrl from API_zb where apiFlow=@apiFlow		  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiFlow", apiFlow);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        #endregion

        #region  安监申报数据

        public DataTable Get_Ap_ajsbb(string uuid)
        {
            string sql = @"  select * from Ap_ajsbb where uuid=@uuid  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb where 1=2";
            return DB.Update(sql, null, dt);
        }

        public DataTable Get_Ap_ajsbb_ht()
        {
            string sql = "select * from Ap_ajsbb_ht where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable Get_Ap_ajsbb_ht(string uuid, string recordNum)
        {
            string sql = @"  select * from Ap_ajsbb_ht where uuid=@uuid and RecordNum=@recordNum";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@recordNum", recordNum);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_ht(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_ht where 1=2";
            return DB.Update(sql, null, dt);
        }


        public DataTable Get_Ap_ajsbb_dwry(string uuid, string idCard, string corpCode, string gw)
        {
            string sql = @"  select * from Ap_ajsbb_dwry where uuid=@uuid and idCard=@idCard and CorpCode=@corpCode and gw=@gw";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@idCard", idCard);
            sp.Add("@corpCode", corpCode);
            sp.Add("@gw", gw);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_dwry(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_dwry where 1=2";
            return DB.Update(sql, null, dt);
        }

        /**
        public bool Delete_Ap_ajsbb_dwry(string uuid)
        {
            string sql = "delete from dbo.Ap_ajsbb_dwry where uuid=@uuid  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid); 
            return DB.ExecuteNonQuerySql(sql, sp) > 0;
        }
         */

        public DataTable Get_Ap_ajsbb_clqd()
        {
            string sql = "select * from Ap_ajsbb_clqd where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable Get_Ap_ajsbb_clqd(string uuid, string xh, string sbzl)
        {
            string sql = @"  select * from Ap_ajsbb_clqd where uuid=@uuid and xh=@xh and sbzl=@sbzl";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@xh", xh);
            sp.Add("@sbzl", sbzl);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_clqd(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_clqd where 1=2";
            return DB.Update(sql, null, dt);
        }

        public DataTable Get_Ap_ajsbb_hjssjd()
        {
            string sql = "select * from Ap_ajsbb_hjssjd where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable Get_Ap_ajsbb_hjssjd(string uuid, string xh)
        {
            string sql = @"  select * from Ap_ajsbb_hjssjd where uuid=@uuid and xh=@xh";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@xh", xh);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_hjssjd(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_hjssjd where 1=2";
            return DB.Update(sql, null, dt);
        }


        public DataTable Get_Ap_ajsbb_wxyjdgcqd()
        {
            string sql = "select * from Ap_ajsbb_wxyjdgcqd where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable Get_Ap_ajsbb_wxyjdgcqd(string uuid, string fbfxgc)
        {
            string sql = @"  select * from Ap_ajsbb_wxyjdgcqd where uuid=@uuid and fbfxgc=@fbfxgc";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@fbfxgc", fbfxgc);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_wxyjdgcqd(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_wxyjdgcqd where 1=2";
            return DB.Update(sql, null, dt);
        }

        public DataTable Get_Ap_ajsbb_cgmgcqd()
        {
            string sql = "select * from Ap_ajsbb_cgmgcqd where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable Get_Ap_ajsbb_cgmgcqd(string uuid, string fbfxgc)
        {
            string sql = @"  select * from Ap_ajsbb_cgmgcqd where uuid=@uuid and fbfxgc=@fbfxgc";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@fbfxgc", fbfxgc);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool Save_Ap_ajsbb_cgmgcqd(DataTable dt)
        {
            string sql = "select *  from Ap_ajsbb_cgmgcqd where 1=2";
            return DB.Update(sql, null, dt);
        }

        #endregion

        #region  质监申报数据

        public bool Save_Ap_sbb(DataTable dt, string tableName)
        {
            string sql = "select *  from " + tableName + " where 1=2";
            return DB.Update(sql, null, dt);
        }

        public DataTable Get_Ap_zjsbb(string uuid)
        {
            string sql = @"  select * from Ap_zjsbb where uuid=@uuid  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_Ap_zjsbb_ht(string uuid, string recordNum)
        {
            string sql = @"  select * from Ap_zjsbb_ht where uuid=@uuid and RecordNum=@recordNum";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@recordNum", recordNum);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_Ap_zjsbb_dwry(string uuid, string idCard, string corpCode, string gw)
        {
            string sql = @"  select * from Ap_zjsbb_dwry where uuid=@uuid and idCard=@idCard and CorpCode=@corpCode and gw=@gw";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@idCard", idCard);
            sp.Add("@corpCode", corpCode);
            sp.Add("@gw", gw);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_Ap_zjsbb_schgs(string uuid, string CensorNum)
        {
            string sql = @"  select * from Ap_zjsbb_schgs where uuid=@uuid and CensorNum=@CensorNum";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@CensorNum", CensorNum);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_Ap_zjsbb_dwgc(string uuid, string dwgcbm)
        {
            string sql = @"  select * from Ap_zjsbb_dwgc where uuid=@uuid and dwgcbm=@dwgcbm";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@dwgcbm", dwgcbm);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_Ap_zjsbb_clqd(string uuid, string xh, string sbzl)
        {
            string sql = @"  select * from Ap_zjsbb_clqd where uuid=@uuid and xh=@xh and sbzl=@sbzl";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            sp.Add("@xh", xh);
            sp.Add("@sbzl", sbzl);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        #endregion


        #region 通用查询记录
        public DataTable Get_ApTable(string tableName)
        {
            string sql = "select * from " + tableName + " where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t_" + tableName);
        }
        #endregion

        #region 删除记录
        public bool Delete_ApTable(string tableName, string uuid)
        {
            string sql = "delete from dbo." + tableName + " where uuid=@uuid  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            return DB.ExecuteNonQuerySql(sql, sp) > 0;
        }
        #endregion

        #region 日志相关

        public DataTable GetSchema_DataJkLog()
        {
            string sql = "select * from DataJkLog where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public bool Submit_DataJkDataDetail(DataTable dt)
        {
            string sql = @"select * from DataJkDataDetail  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public DataTable GetSchema_API_cb()
        {
            string sql = "select * from API_cb  where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public string Get_apiCbNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,apiCbID)),0)+1 from API_cb ";
            return DB.ExeSqlForString(sql, null);
        }

        public bool Submit_API_cb(DataTable dt)
        {
            string sql = @"select * from API_cb   where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public void UpdateZbJkzt(string apiFlow, string apiRunState, string apiRunMessage)
        {
            string sql = "update  API_zb set apiRunState=@apiRunState , apiRunMessage=@apiRunMessage where apiFlow=@apiFlow ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiFlow", apiFlow);
            sp.Add("@apiRunState", apiRunState);
            sp.Add("@apiRunMessage", apiRunMessage);
            this.DB.ExecuteNonQuerySql(sql, sp);
        }

        public string Get_DataJkDataDetailNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,ID)),0)+1 from DataJkDataDetail ";
            return DB.ExeSqlForString(sql, null);
        }


        #endregion


    }
}
