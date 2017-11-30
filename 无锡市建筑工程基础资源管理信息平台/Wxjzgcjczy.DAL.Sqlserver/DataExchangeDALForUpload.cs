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
    /// 功能：手动往省厅上传
    /// 作者：黄正宇
    /// 时间：2017-09-10
    /// </summary>
    public class DataExchangeDALForUpload
    {
        public DBOperator DB { get; set; }


        #region 读取数据

        /// <summary>
        /// 根据PKID获取立项项目信息
        /// </summary>
        /// <param name="prjNum"></param>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectInfoByPKID(string pKID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select PKID, PrjNum, PrjInnerNum, PrjName, PrjTypeNum, BuildCorpName, BuildCorpCode, ProvinceNum, CityNum, CountyNum, PrjApprovalNum, PrjApprovalLevelNum, BuldPlanNum, ProjectPlanNum, AllInvest, AllArea, PrjSize, PrjPropertyNum, PrjFunctionNum, BDate, EDate, CreateDate, UpdateFlag, sbdqbm,xgrqsj ,updateUser from TBProjectInfo where PKID =@PKID";
            sp.Add("@PKID", pKID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 根据项目编号获取立项项目信息
        /// </summary>
        /// <param name="prjNum"></param>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectInfoByPrjNum(string prjNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select PKID, PrjNum, PrjInnerNum, PrjName, PrjTypeNum, BuildCorpName, BuildCorpCode, ProvinceNum, CityNum, CountyNum, PrjApprovalNum, PrjApprovalLevelNum, BuldPlanNum, ProjectPlanNum, AllInvest, AllArea, PrjSize, PrjPropertyNum, PrjFunctionNum, BDate, EDate, CreateDate, UpdateFlag, sbdqbm,xgrqsj ,updateUser from TBProjectInfo where PrjNum =@PrjNum";
            sp.Add("@PrjNum", prjNum);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 根据项目编号获取项目等级补充信息
        /// </summary>
        /// <patenderInnerNumram name="projNum"></param>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectAdditionalInfo(string prjNum)
        {
            string sql = "select * from TBProjectAdditionalInfo where prjnum  ='" + prjNum + "' ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
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
