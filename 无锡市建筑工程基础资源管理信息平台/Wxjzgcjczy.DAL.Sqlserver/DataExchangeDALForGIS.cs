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
    /// 功能： 无锡数据中心与GIS接口相关数据库访问
    /// 作者：顾立强
    /// 时间：2017-10-10
    /// </summary>
    public class DataExchangeDALForGIS
    {
        public DBOperator DB { get; set; }

     

        #region 获取项目数据

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="prjName">项目名称</param>
        /// <param name="location">项目地点</param>
        /// <returns></returns>
        public DataTable GetProject(string prjNum, string prjName, string location)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select *");
            sb.Append(" from TBProjectInfo a");
            sb.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            if (!string.IsNullOrEmpty(prjName))
            {
                sp.Add("@prjName", prjName);
                sb.Append(" and a.PrjName=@prjName");
            }

            if (!string.IsNullOrEmpty(location))
            {
                sp.Add("@location", location);
                //sb.Append(" and a.PrjName=@location");
            }
            
            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectInfo");

        }

        /// <summary>
        /// 获取子项目
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <returns></returns>
        public DataTable GetSubProject(string prjNum)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select *");
            sb.Append(" from xm_gcdjb_dtxm a");
            sb.Append(" where 1=1");

            sp.Add("@prjNum", prjNum);
            sb.Append(" and a.PrjNum=@prjNum");

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "xm_gcdjb_dtxm");
        }


        /// <summary>
        /// 获取施工许可证
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <returns></returns>
        public DataTable GetBuildingLicense(string prjNum)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select *");
            sb.Append(" from TBBuilderLicenceManage a");
            sb.Append(" where 1=1");

            sp.Add("@prjNum", prjNum);
            sb.Append(" and a.PrjNum=@prjNum");

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBBuilderLicenceManage");

        }


        /// <summary>
        /// 获取竣工备案
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <returns></returns>
        public DataTable GetProjectFinish(string prjNum)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select *");
            sb.Append(" from TBProjectFinishManage a");
            sb.Append(" where 1=1");

            sp.Add("@prjNum", prjNum);
            sb.Append(" and a.PrjNum=@prjNum");

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectFinishManage");

        }

        #endregion

        #region 保存项目档案相关信息

        public int SaveProjectDoc(string fxbm, string docNum)
        {
            int effects = this.UpdateProjectDoc(fxbm, docNum);
            if (effects < 1)
            {
                effects = this.InsertProjectDoc(fxbm, docNum);
            }

            return effects;
        }

        public int UpdateProjectDoc(string fxbm, string docNum)
        {
            string sql = "update xm_gcdjb_dtxm_doc set docNum=@fxbm where fxbm=@docNum";

            SqlParameterCollection paramCol = DB.CreateSqlParameterCollection();

            paramCol.Add("@fxbm", fxbm);
            paramCol.Add("@docNum", docNum);

            return DB.ExecuteNonQuerySql(sql, paramCol);
        }

        public int InsertProjectDoc(string fxbm, string docNum)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into xm_gcdjb_dtxm_doc(PKID, fxbm, docNum, CreateDate, UpdateDate)");
            sb.Append(" values(@id, @fxbm, @docNum, SYSDATETIME(), SYSDATETIME())");

            SqlParameterCollection paramCol = DB.CreateSqlParameterCollection();
            Guid id = Guid.NewGuid();
            paramCol.Add("@id", id);
            paramCol.Add("@fxbm", fxbm);
            paramCol.Add("@docNum", docNum);

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        #endregion


        #region 日志表操作

        public DataTable GetTBData_SaveToStLog(string tableName, string PKID)
        {
            string sql = "select * from SaveToStLog where TableName=@TableName and PKID =@PKID  ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            sp.Add("@TableName", tableName);
            sp.Add("@PKID", PKID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public bool SaveTBData_SaveToStLog(DataTable dt)
        {
            string sql = "select * from SaveToStLog where 1=2";
            return DB.Update(sql, null, dt);
        }

        #endregion


        #region 通用表查询
        public DataTable GetApTable(string tableName)
        {
            string sql = "select * from " + tableName + " where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        #endregion

        #region 通用表保存
        public bool SaveApTable(string tableName , DataTable dt)
        {
            string sql = "select *  from " + tableName + " where 1=2";
            return DB.Update(sql, null, dt);
        }
        #endregion

    }
}
