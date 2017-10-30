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

        public static string PROJECTINFO_FIELDS = "a.PrjNum,a.PrjName,a.PrjTypeNum,a.BuildCorpName,a.BuildCorpCode,a.ProvinceNum,a.CityNum,a.CountyNum,a.BDate,a.EDate,a.jd,a.wd,b.programme_address";
     

        #region 获取项目数据

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="prjName">项目名称</param>
        /// <param name="location">项目地点</param>
        /// <param name="countyNum">地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetProject(string prjNum, string prjName, string location, string countyNum, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(PROJECTINFO_FIELDS);
            sb.Append(" from TBProjectInfo a");
            sb.Append(" left join TBProjectAdditionalInfo b on b.PrjNum=a.PrjNum");
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
                sb.Append(" and b.programme_address like CONCAT('%',@location,'%')");
            }

            if (!string.IsNullOrEmpty(countyNum))
            {
                sp.Add("@countyNum", countyNum);
                if ("320000".Equals(countyNum))
                {
                    sb.Append(" and a.ProvinceNum=@countyNum");
                }
                else
                if ("320200".Equals(countyNum))
                {
                    sb.Append(" and a.CityNum=@countyNum");
                }
                else
                {
                    sb.Append(" and a.CountyNum=@countyNum");
                }
                
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)<=@endDate");
            }
            
            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectInfo");

        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="range">经纬度范围</range>
        /// <returns></returns>
        public DataTable GetProjectByRange(string range)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            //parse range
            double jd1, jd2, wd1, wd2;

            int indexOfSep = range.IndexOf(";");
            string part1 = range.Substring(0, indexOfSep);
            string part2 = range.Substring(indexOfSep + 1);

            indexOfSep = part1.IndexOf(":");
            string part11 = part1.Substring(0, indexOfSep);
            string part12 = part1.Substring(indexOfSep + 1);

            indexOfSep = part2.IndexOf(":");
            string part21 = part2.Substring(0, indexOfSep);
            string part22 = part2.Substring(indexOfSep + 1);

            if (double.Parse(part11) < double.Parse(part21))
            {
                jd1 = double.Parse(part11);
                jd2 = double.Parse(part21);
            }
            else
            {
                jd2 = double.Parse(part11);
                jd1 = double.Parse(part21);
            }

            if (double.Parse(part12) < double.Parse(part22))
            {
                wd1 = double.Parse(part12);
                wd2 = double.Parse(part22);
            }
            else
            {
                wd2 = double.Parse(part12);
                wd1 = double.Parse(part22);
            }

            //parse range finish

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(PROJECTINFO_FIELDS);
            sb.Append(" from TBProjectInfo a");
            sb.Append(" left join TBProjectAdditionalInfo b on b.PrjNum=a.PrjNum");
            sb.Append(" where 1=1");

            sp.Add("@jd1", jd1);
            sb.Append(" and a.jd>=@jd1");

            sp.Add("@jd2", jd2);
            sb.Append(" and a.jd<=@jd2");

            sp.Add("@wd1", wd1);
            sb.Append(" and a.wd>=@wd1");

            sp.Add("@wd2", wd2);
            sb.Append(" and a.wd<=@wd2");


            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectInfo");

        }

        /// <summary>
        /// 获取子项目
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="sbdqbm">上报地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetSubProject(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select a.PrjNum,a.fxbm,a.xmmc,a.sbdqbm, b.docNum");
            sb.Append(" from xm_gcdjb_dtxm a");
            sb.Append(" left join xm_gcdjb_dtxm_doc b on b.fxbm=a.fxbm");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "xm_gcdjb_dtxm");
        }


        /// <summary>
        /// 获取施工许可证
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="sbdqbm">上报地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetBuildingLicense(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select BuilderLicenceName,BuilderLicenceNum,PrjNum,CensorNum,EconCorpName,EconCorpCode,");
            sb.Append(" DesignCorpName,DesignCorpCode,ConsCorpName,ConsCorpCode,SuperCorpName,SuperCorpCode,ConstructorName,");
            sb.Append(" CIDCardTypeNum,ConstructorIDCard,ConstructorPhone,sbdqbm");
            sb.Append(" from TBBuilderLicenceManage a");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBBuilderLicenceManage");

        }


        /// <summary>
        /// 获取竣工备案
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="sbdqbm">上报地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetProjectFinish(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select PrjFinishName,PrjFinishNum,PrjNum,BuilderLicenceNum,BDate,EDate,sbdqbm");
            sb.Append(" from TBProjectFinishManage a");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectFinishManage");

        }

        /// <summary>
        /// 获取施工图审查信息
        /// </summary>
        /// <param name="prjNum">项目编号</param>
        /// <param name="sbdqbm">上报地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetProjectCensorInfo(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select CensorNum,CensorInnerNum,PrjNum,CensorCorpName,CensorCorpCode,CensorEDate,EconCorpName,");
            sb.Append(" EconCorpCode,DesignCorpName,DesignCorpCode,EconCorpNum,DesignCorpNum,sbdqbm");
            sb.Append(" from TBProjectCensorInfo a");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "TBProjectCensorInfo");

        }

        #endregion

        #region 保存项目档案相关信息

        public int SaveProjectDoc(string fxbm, string docNum)
        {
            int effects = this.UpdateProjectDoc(fxbm, docNum);
            if (effects == 0)
            {
                effects = this.InsertProjectDoc(fxbm, docNum);
            }

            return effects;
        }

        public int UpdateProjectDoc(string fxbm, string docNum)
        {
            string sql = "update xm_gcdjb_dtxm_doc set docNum=@docNum where fxbm=@fxbm";

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
