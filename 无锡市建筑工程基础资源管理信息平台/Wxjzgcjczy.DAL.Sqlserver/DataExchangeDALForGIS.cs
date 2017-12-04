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
        /// <param name="buildCorpCode">建设单位编号</param>
        /// <param name="buildCorpName">建设单位名称</param>
        /// <param name="location">项目地点</param>
        /// <param name="countyNum">地区码</param>
        /// <param name="beginDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetProject(string prjNum, string prjName, String buildCorpCode, String buildCorpName, string location, string countyNum, string beginDate, string endDate)
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
                sb.Append(" and a.PrjName like CONCAT('%',@prjName,'%')");
            }

            if (!string.IsNullOrEmpty(buildCorpCode))
            {
                sp.Add("@buildCorpCode", buildCorpCode);
                sb.Append(" and a.BuildCorpCode=@buildCorpCode");
            }

            if (!string.IsNullOrEmpty(buildCorpName))
            {
                sp.Add("@buildCorpName", buildCorpName);
                sb.Append(" and a.BuildCorpName like CONCAT('%',@buildCorpName,'%')");
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
            //获取档案馆数据
            DataTable rst = this.GetSubProjectFromXm_gcdjb_dtxm_doc(prjNum, sbdqbm, beginDate, endDate);

            DataTable dt = null;

            //获取四库旧数据
            dt = this.GetSubProjectFromXm_gcdjb_dtxm(prjNum, sbdqbm, beginDate, endDate);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if(!this.exists(rst, dr))
                    {
                        rst.Rows.Add(dr.ItemArray);
                    }
                }
            }

            //获取四库新数据
            dt = this.GetSubProjectFromAp_zjsbb(prjNum, sbdqbm, beginDate, endDate);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!this.exists(rst, dr))
                    {
                        rst.Rows.Add(dr.ItemArray);
                    }
                }
            }


            return rst;
        }

        /// <summary>
        /// 从档案馆上传的表中获取
        /// </summary>
        /// <param name="prjNum"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private DataTable GetSubProjectFromXm_gcdjb_dtxm_doc(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();
            sb.Append(" select a.PrjNum,a.fxbm,a.xmmc,'' AS sbdqbm, a.docNum");
            sb.Append(" from xm_gcdjb_dtxm_doc a");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and a.PrjNum=@prjNum");
            }

            /*
            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }
             * */

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.CreateDate, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), a.CreateDate, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "xm_gcdjb_dtxm");
        }

        /// <summary>
        /// 从四库旧的的表中获取
        /// </summary>
        /// <param name="prjNum"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>

        private DataTable GetSubProjectFromXm_gcdjb_dtxm(string prjNum, string sbdqbm, string beginDate, string endDate)
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
        /// 从四库新的表中获取
        /// </summary>
        /// <param name="prjNum"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private DataTable GetSubProjectFromAp_zjsbb(string prjNum, string sbdqbm, string beginDate, string endDate)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            StringBuilder sb = new StringBuilder();

            sb.Append(" select b.PrjNum,a.dwgcbm AS fxbm, a.dwgcmc AS xmmc, '' AS sbdqbm,c.docNum");
            sb.Append(" from Ap_zjsbb_dwgc a");
            sb.Append(" left join Ap_zjsbb b on b.uuid=a.uuid");
            sb.Append(" left join xm_gcdjb_dtxm_doc c on c.fxbm=a.dwgcbm");
            sb.Append(" where 1=1");

            if (!string.IsNullOrEmpty(prjNum))
            {
                sp.Add("@prjNum", prjNum);
                sb.Append(" and b.PrjNum=@prjNum");
            }

            /*
            if (!string.IsNullOrEmpty(sbdqbm))
            {
                sp.Add("@sbdqbm", sbdqbm);
                sb.Append(" and a.sbdqbm=@sbdqbm");
            }
             */

            if (!string.IsNullOrEmpty(beginDate))
            {
                sp.Add("@beginDate", beginDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), b.CreateDate, 120), 1, 10)>=@beginDate");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sp.Add("@endDate", endDate);
                sb.Append(" and SUBSTRING(convert(VARCHAR(30), b.CreateDate, 120), 1, 10)<=@endDate");
            }

            return DB.ExeSqlForDataTable(sb.ToString(), sp, "xm_gcdjb_dtxm");
        }

        private bool exists(DataTable dt, DataRow dr)
        {
            bool flag = false;

            foreach (DataRow item in dt.Rows)
            {
                if (dr["fxbm"].Equals(item["fxbm"]))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
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

        #region 保存项目位置相关信息
        public int SaveProjectPosition(DataRow row)
        {

            SqlParameterCollection paramCol = DB.CreateSqlParameterCollection();

            paramCol.Add("@prjNum", row["PrjNum"].ToString2());
            paramCol.Add("@wd", row["Wd"].ToString2());
            paramCol.Add("@jd", row["Jd"].ToString2());

            int effects = this.UpdateProjectPosition(paramCol);
            if (effects == 0)
            {
                Guid id = Guid.NewGuid();
                paramCol.Add("@id", id);
                effects = this.InsertProjectPosition(paramCol);
            }

            return effects;
        }

        public int UpdateProjectPosition(SqlParameterCollection paramCol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update TBProjectInfoDoc");
            sb.Append(" set wd=@wd,jd=@jd,");
            sb.Append(" UpdateDate=SYSDATETIME()");
            sb.Append(" where prjNum=@prjNum");

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        public int InsertProjectPosition(SqlParameterCollection paramCol)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into TBProjectInfoDoc(PKID, prjNum, ");
            sb.Append(" wd, jd,");
            sb.Append(" CreateDate, UpdateDate)");
            sb.Append(" values(@id, @prjNum, ");
            sb.Append(" @wd, @jd,");
            sb.Append(" SYSDATETIME(), SYSDATETIME())");

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        #endregion

        #region 保存项目档案相关信息



        public int SaveProjectDoc(DataRow row)
        {

            SqlParameterCollection paramCol = DB.CreateSqlParameterCollection();

            paramCol.Add("@prjNum", row["PrjNum"].ToString2());
            paramCol.Add("@prjName", row["PrjName"].ToString2());
            paramCol.Add("@prjAddress", row["PrjAddress"].ToString2());
            paramCol.Add("@lxpzwh", row["Lxpzwh"].ToString2());
            paramCol.Add("@ydghxkzh", row["Ydghxkzh"].ToString2());
            paramCol.Add("@ghxkzh", row["Ghxkzh"].ToString2());
            paramCol.Add("@gytdsyzh", row["Gytdsyzh"].ToString2());

            int effects = this.UpdateProjectDoc(paramCol);
            if (effects == 0)
            {
                Guid id = Guid.NewGuid();
                paramCol.Add("@id", id);
                effects = this.InsertProjectDoc(paramCol);
            }

            return effects;
        }

        public int UpdateProjectDoc(SqlParameterCollection paramCol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update TBProjectInfoDoc");
            sb.Append(" set prjName=@prjName,prjAddress=@prjAddress,lxpzwh=@lxpzwh,");
            sb.Append(" ydghxkzh=@ydghxkzh,ghxkzh=@ghxkzh,gytdsyzh=@gytdsyzh,");
            sb.Append(" UpdateDate=SYSDATETIME()");
            sb.Append(" where prjNum=@prjNum");

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        public int InsertProjectDoc(SqlParameterCollection paramCol)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into TBProjectInfoDoc(PKID, prjNum, ");
            sb.Append(" prjName, prjAddress, lxpzwh, ydghxkzh, ghxkzh, gytdsyzh,");
            sb.Append(" CreateDate, UpdateDate)");
            sb.Append(" values(@id, @prjNum, ");
            sb.Append(" @prjName, @prjAddress, @lxpzwh, @ydghxkzh, @ghxkzh, @gytdsyzh,");
            sb.Append(" SYSDATETIME(), SYSDATETIME())");

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        public int SaveSubProjectDoc(DataRow row)
        {

            SqlParameterCollection paramCol = DB.CreateSqlParameterCollection();

            string fxbm = row["Fxbm"].ToString2();
            string docNum = row["DocNum"].ToString2();
            string gd = row["Gd"].ToString2();
            string dscs = row["Dscs"].ToString2();
            string dxcs = row["Dxcs"].ToString2();
            string jclx = row["Jclx"].ToString2();
            string jzmj = row["Jzmj"].ToString2();
            string ydmj = row["Ydmj"].ToString2();
            string jglx = row["Jglx"].ToString2();

            paramCol.Add("@fxbm", fxbm);
            paramCol.Add("@docNum", docNum);
            paramCol.Add("@gd", gd);
            paramCol.Add("@dscs", dscs);
            paramCol.Add("@dxcs", dxcs);
            paramCol.Add("@jclx", jclx);
            paramCol.Add("@jzmj", jzmj);
            paramCol.Add("@ydmj", ydmj);
            paramCol.Add("@jglx", jglx);

            int effects = this.UpdateSubProjectDoc(paramCol);
            if (effects == 0)
            {
                Guid id = Guid.NewGuid();
                paramCol.Add("@id", id);
                effects = this.InsertSubProjectDoc(paramCol);
            }

            return effects;
        }

        public int UpdateSubProjectDoc(SqlParameterCollection paramCol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" update xm_gcdjb_dtxm_doc");
            sb.Append(" set docNum=@docNum,");
            sb.Append(" gd=@gd,");
            sb.Append(" dscs=@dscs,");
            sb.Append(" dxcs=@dxcs,");
            sb.Append(" jclx=@jclx,");
            sb.Append(" jzmj=@jzmj,");
            sb.Append(" ydmj=@ydmj,");
            sb.Append(" jglx=@jglx,");
            sb.Append(" UpdateDate=SYSDATETIME()");
            sb.Append(" where fxbm=@fxbm");

            return DB.ExecuteNonQuerySql(sb.ToString(), paramCol);
        }

        public int InsertSubProjectDoc(SqlParameterCollection paramCol)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into xm_gcdjb_dtxm_doc(PKID, fxbm, docNum, ");
            sb.Append(" gd, dscs,dxcs, jclx,");
            sb.Append(" jzmj, ydmj,jglx,");
            sb.Append(" CreateDate, UpdateDate)");
            sb.Append(" values(@id, @fxbm, @docNum,");
            sb.Append(" @gd, @dscs, @dxcs, @jclx,");
            sb.Append(" @jzmj, @ydmj, @jglx,");
            sb.Append(" SYSDATETIME(), SYSDATETIME())");

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
