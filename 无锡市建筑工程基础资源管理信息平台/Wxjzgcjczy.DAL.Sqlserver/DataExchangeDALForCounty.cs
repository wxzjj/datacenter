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
    /// 功能： 无锡数据中心与各县市系统数据交换数据访问类
    /// 作者：孙刚
    /// 时间：2015-03-31
    /// </summary>
    public class DataExchangeDALForCounty
    {
        public DBOperator DB { get; set; }

        #region 表结构
        public DataTable GetSchema_TBProjectInfo()
        {
            string sql = "select * from TBProjectInfo where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_xm_gcdjb_dtxm()
        {
            string sql = "select * from xm_gcdjb_dtxm where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_TBTenderInfo()
        {
            string sql = "select * from TBTenderInfo where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_TBContractRecordManage()
        {
            string sql = "select * from TBContractRecordManage where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_TBProjectCensorInfo()
        {
            string sql = "select * from TBProjectCensorInfo where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_TBProjectDesignEconUserInfo()
        {
            string sql = "select * from TBProjectDesignEconUserInfo where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_TBBuilderLicenceManage()
        {
            string sql = "select * from TBBuilderLicenceManage where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_TBProjectBuilderUserInfo()
        {
            string sql = "select * from TBProjectBuilderUserInfo where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_TBProjectFinishManage()
        {
            string sql = "select * from TBProjectFinishManage where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_aj_gcjbxx()
        {
            string sql = "select * from aj_gcjbxx where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_zj_gcjbxx()
        {
            string sql = "select * from zj_gcjbxx where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetSchema_zj_gcjbxx_zrdw()
        {
            string sql = "select * from zj_gcjbxx_zrdw where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_SaveToStLog()
        {
            string sql = "select * from SaveToStLog where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }


        #endregion

        #region  读取列表
        /// <summary>
        /// 获取无锡数据中心库里的立项项目数据集
        /// </summary>
        /// <param name="conditions">查询条件集合</param>
        /// <returns>返回立项项目数据集</returns>
        public DataTable GetTBData_TBProjectInfo(List<IDataItem> conditions)
        {


            string sql = @"select * from (
                            select p.PKID   ,p.PrjNum ,p.PrjInnerNum ,p.PrjName ,p.PrjTypeNum ,
            	            p.BuildCorpName ,p.BuildCorpCode ,p.ProvinceNum ,p.CityNum ,p.CountyNum ,p.PrjApprovalNum ,p.PrjApprovalLevelNum ,p.BuldPlanNum,p.ProjectPlanNum 
                            ,p.AllInvest ,p.AllArea  ,p.PrjSize ,p.PrjPropertyNum ,p.PrjFunctionNum , SUBSTRING(convert(varchar(30),p.BDate,120),1,10) BDate 
                            ,SUBSTRING(convert(varchar(30),p.EDate,120),1,10) EDate ,SUBSTRING(convert(varchar(30),p.CREATEDATE,120),1,10) CREATEDATE ,p.UpdateFlag ,p.sbdqbm
                            , pa.prjpassword,pa.gyzzpl,pa.dzyx,pa.lxr,pa.yddh,pa.xmtz ,pa.gytze ,pa.gytzbl ,pa.lxtzze, pa.programme_address
                            from TBProjectInfo p LEFT JOIN TBProjectAdditionalInfo pa ON p.PrjNum = pa.prjnum ) aaa where 1=1 ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();


            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_xm_gcdjb_dtxm(List<IDataItem> conditions)
        {
            string sql = @" select * from (
 select PKID, PrjNum, fxbm, fxnbbm, xmmc, gclb, gczj, jzmj, jsgm, jglx, jsyt, dscs, dxcs, gd, kd, jhkgrq, jhjgrq,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, sbdqbm from xm_gcdjb_dtxm
) aaa where 1=1 ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            //string sql = "select * from xm_gcdjb_dtxm ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBTenderInfo(List<IDataItem> conditions)
        {
            string sql = @"SELECT * FROM (
	SELECT a.PKID
		,a.TenderName
		,a.TenderNum
		,a.TenderInnerNum
		,a.PrjNum
		,a.TenderClassNum
		,a.TenderTypeNum
		,a.TenderResultDate
		,a.TenderMoney
		,a.PrjSize
		,a.Area
		,a.AgencyCorpName
		,a.AgencyCorpCode
		,a.TenderCorpName
		,a.TenderCorpCode
		,a.ConstructorName
		,a.ConstructorPhone
		,a.IDCardTypeNum
		,a.ConstructorIDCard
		,a.shypbf
		,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
		,a.UpdateFlag
		,a.sbdqbm
		,i.CountyNum
	FROM TBTenderInfo a
	INNER JOIN TBProjectInfo i ON a.PrjNum = i.PrjNum
	) aaa
WHERE 1 = 1";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBContractRecordManage(List<IDataItem> conditions)
        {


//            string sql = @"select * from (
//select PKID, RecordName, RecordNum, RecordInnerNum, PrjNum, ContractNum, ContractTypeNum, ContractMoney, PrjSize, ContractDate, PropietorCorpName, PropietorCorpCode, ContractorCorpName, ContractorCorpCode, UnionCorpName, UnionCorpCode,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, PrjHead, PrjHeadPhone, IDCard, sbdqbm
//from TBContractRecordManage
//) aaa where 1=1 ";
            string sql = @" SELECT * FROM( SELECT a.PKID
	,a.RecordName
	,a.RecordNum
	,a.RecordInnerNum
	,a.PrjNum
	,a.ContractNum
	,a.ContractTypeNum
	,a.ContractMoney
	,a.PrjSize
	,a.ContractDate
	,a.PropietorCorpName
	,a.PropietorCorpCode
	,a.ContractorCorpName
	,a.ContractorCorpCode
	,a.UnionCorpName
	,a.UnionCorpCode
	,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
	,a.UpdateFlag
	,a.PrjHead
	,a.PrjHeadPhone
	,a.IDCard
	,a.sbdqbm
    ,i.CountyNum
FROM TBContractRecordManage a
left join TBProjectInfo i on i.PrjNum = a.PrjNum ) aaa
WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBProjectCensorInfo(List<IDataItem> conditions)
        {
            //string sql = "select * from TBProjectCensorInfo ";
            //            string sql = @"select * from (
            //select PKID, CensorNum, CensorInnerNum, PrjNum, CensorCorpName, CensorCorpCode, CensorEDate, PrjSize, EconCorpName, EconCorpCode, DesignCorpName, DesignCorpCode, EconCorpNum, DesignCorpNum, OneCensorIsPass, OneCensorWfqtCount, OneCensorWfqtContent,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, sbdqbm
            //from TBProjectCensorInfo 
            //) aaa where 1=1 ";
            string sql = @"SELECT *
FROM (
	SELECT a.PKID
		,a.CensorNum
		,a.CensorInnerNum
		,a.PrjNum
		,a.CensorCorpName
		,a.CensorCorpCode
		,a.CensorEDate
		,a.PrjSize
		,a.EconCorpName
		,a.EconCorpCode
		,a.DesignCorpName
		,a.DesignCorpCode
		,a.EconCorpNum
		,a.DesignCorpNum
		,a.OneCensorIsPass
		,a.OneCensorWfqtCount
		,a.OneCensorWfqtContent
		,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
		,a.UpdateFlag
		,a.sbdqbm
		,i.CountyNum
	FROM TBProjectCensorInfo a
	left join TBProjectInfo i on i.PrjNum = a.PrjNum
	) aaa
WHERE 1 = 1 ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectCensorInfoByCensorInnerNum(string censorInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@CensorInnerNum", censorInnerNum);
            string sql = " select *  from  TBProjectCensorInfo where UpdateFlag='U' and CensorInnerNum=@CensorInnerNum ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }



        public DataTable GetNumAndSbState_TBProjectCensorInfo(List<IDataItem> conditions)
        {

            string sql = @"
select PKID, CensorNum, CensorInnerNum, PrjNum,
,(select OperateState from dbo.SaveToStLog  where tableName='TBProjectCensorInfo' and PKID=a.PKID) as OperateState
from TBProjectCensorInfo  a where 1=1  ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectDesignEconUserInfo(List<IDataItem> conditions)
        {
            string sql = "select * from TBProjectDesignEconUserInfo ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBProjectDesignEconUserInfo(string censorNum)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@censorNum", censorNum);
            string sql = "select * from TBProjectDesignEconUserInfo  where CensorNum = @censorNum";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManage(List<IDataItem> conditions)
        {
//            string sql = @"select * from (
//select PKID, BuilderLicenceName, BuilderLicenceNum, BuilderLicenceInnerNum, PrjNum, BuldPlanNum, ProjectPlanNum, CensorNum, ContractMoney, Area, PrjSize, IssueCertDate, EconCorpName, EconCorpCode, DesignCorpName, DesignCorpCode, ConsCorpName, SafetyCerID, SuperCorpName, SuperCorpCode, ConstructorName, CIDCardTypeNum, ConstructorIDCard, ConstructorPhone, SupervisionName, SIDCardTypeNum, SupervisionIDCard, SupervisionPhone, SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, sbdqbm
//from dbo.TBBuilderLicenceManage
//) as aaa where 1=1 ";
            string sql = @"SELECT *
FROM (
	SELECT a.PKID
		,a.BuilderLicenceName
		,a.BuilderLicenceNum
		,a.BuilderLicenceInnerNum
		,a.PrjNum
		,a.BuldPlanNum
		,a.ProjectPlanNum
		,a.CensorNum
		,a.ContractMoney
		,a.Area
		,a.PrjSize
		,a.IssueCertDate
		,a.EconCorpName
		,a.EconCorpCode
		,a.DesignCorpName
		,a.DesignCorpCode
		,a.ConsCorpName
		,a.SafetyCerID
		,a.SuperCorpName
		,a.SuperCorpCode
		,a.ConstructorName
		,a.CIDCardTypeNum
		,a.ConstructorIDCard
		,a.ConstructorPhone
		,a.SupervisionName
		,a.SIDCardTypeNum
		,a.SupervisionIDCard
		,a.SupervisionPhone
		,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
		,a.UpdateFlag
		,a.sbdqbm
		,i.CountyNum
	FROM dbo.TBBuilderLicenceManage a
	LEFT JOIN TBProjectInfo i ON i.PrjNum = a.PrjNum
	) AS aaa
WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_ap_ajsbb(List<IDataItem> conditions)
        {
            string sql = @"SELECT *
FROM ( select * from dbo.Ap_ajsbb) AS aaa
WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_ap_ajsbb_jg(string uuid)
        {
            string sql = @"SELECT jg.slry AS TUser
	,(
		CASE jg.success
			WHEN 'Yes'
				THEN '受理通过'
			WHEN 'No'
				THEN '退回:' + jg.thyy
			ELSE ''
			END
		) AS Content
	,jg.UpdateTime
FROM dbo.Ap_ajsbjg jg
WHERE uuid = @uuid
UNION ALL
SELECT tzs.tzrq AS Tuser
	,'推送监督通知书-监督注册号（' + tzs.jdzch + ')' AS Content
	,tzs.UpdateTime
FROM dbo.Ap_ajtzs tzs
WHERE uuid = @uuid order by UpdateTime asc";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@uuid", uuid);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_ap_zjsbb(List<IDataItem> conditions)
        {
            string sql = @"SELECT * FROM ( select * from dbo.Ap_zjsbb) AS aaa WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManageCanJianDanW(string builderLicenceNum)
        {
            string sql = @"select * from TBBuilderLicenceManageCanJianDanW  where BuilderLicenceNum=@builderLicenceNum ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@builderLicenceNum", builderLicenceNum);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectBuilderUserInfo(string aqjdbmString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from TBProjectBuilderUserInfo where aqjdbm in (" + AntiSqlInjection.ParameterizeInClause(aqjdbmString, "@para", ref sp) + ")";
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable GetTBData_TBProjectBuilderUserInfoByPKIDs(string PKIDs)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from TBProjectBuilderUserInfo where PKID in (" + AntiSqlInjection.ParameterizeInClause(PKIDs, "@para", ref sp) + ")";
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable GetTBData_TBProjectBuilderUserInfo(List<IDataItem> conditions)
        {
            string sql = "select * from TBProjectBuilderUserInfo ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_TBProjectFinishManage(List<IDataItem> conditions)
        {
            //string sql = "select * from TBProjectFinishManage ";
            string sql = @"SELECT *
                FROM (
	                SELECT a.PKID
		                ,PrjFinishName
		                ,PrjFinishNum
		                ,PrjFinishInnerNum
		                ,a.PrjNum
		                ,BuilderLicenceNum
		                ,QCCorpName
		                ,QCCorpCode
		                ,FactCost
		                ,FactArea
		                ,FactSize
		                ,PrjStructureTypeNum
		                ,a.BDate
		                ,a.EDate
		                ,Mark
		                ,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
		                ,a.UpdateFlag
		                ,a.sbdqbm
		                ,i.CountyNum
	                FROM TBProjectFinishManage a
	                LEFT JOIN TBProjectInfo i ON i.PrjNum = a.PrjNum
	                ) AS aaa
                WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_aj_gcjbxx(List<IDataItem> conditions)
        {
            //string sql = "select * from aj_gcjbxx ";
            string sql = @"SELECT *
FROM (
	SELECT a.[PKID]
		,a.[aqjdbm]
		,a.[gcmc]
		,a.[xmbm]
		,a.[sgzbbm]
		,a.[aqjdjgmc]
		,a.[sdcode]
		,a.[gcgkYszj]
		,a.[gcgkJzmj]
		,a.[gcgkJglx]
		,a.[gcgkCc]
		,a.[gis_jd]
		,a.[gis_wd]
		,a.[bjrq]
		,a.[gcgkKgrq]
		,a.[gcgkJhjgrq]
		,a.[zbdwDwdm]
		,a.[zbdwDwmc]
		,a.[zbdwAqxkzh]
		,a.[zbdwZcjzs]
		,a.[zbdwZcjzsdm]
		,a.[zbdwZcjzslxdh]
		,a.[zbdwAqy1]
		,a.[zbdwAqyzh1]
		,a.[zbdwAqy2]
		,a.[zbdwAqyzh2]
		,a.[zbdwAqy3]
		,a.[zbdwAqyzh3]
		,a.[jldwDwdm]
		,a.[jldwDwmc]
		,a.[jldwXmzj]
		,a.[jldwZjdm]
		,a.[jldwJlgcs1]
		,a.[jldwJlgcs2]
		,a.[jldwJlgcs3]
		,a.[bz]
		,a.[createDate]
		,a.[updateFlag]
		,a.[sbdqbm]
		,i.CountyNum
	FROM aj_gcjbxx a
	INNER JOIN TBProjectInfo i ON i.PrjNum = a.xmbm
	) AS aaa
WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_zj_gcjbxx(List<IDataItem> conditions)
        {
            //string sql = "select * from zj_gcjbxx ";
            string sql = @"SELECT *
FROM (
	SELECT a.PKID
		,zljdbm
		,gcmc
		,a.PrjNum
		,sgzbbm
		,zljdjgmc
		,zjzbm
		,gczj
		,jzmj
		,dlcd
		,jglx
		,cc
		,jzgm
		,sbrq
		,kgrq
		,jhjgrq
		,xxjd
		,bz
		,SUBSTRING(convert(VARCHAR(30), a.CREATEDATE, 120), 1, 10) CreateDate
		,a.UpdateFlag
		,a.sbdqbm
		,i.CountyNum
	FROM dbo.zj_gcjbxx a
	INNER JOIN dbo.TBProjectInfo i ON i.PrjNum = a.prjNum
	) AS aaa
WHERE 1 = 1";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_zj_gcjbxx_zrdw(List<IDataItem> conditions)
        {
            string sql = "select * from zj_gcjbxx_zrdw ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_zj_gcjbxx_zrdw(string zljdbm)
        {
            string sql = @"select * from zj_gcjbxx_zrdw where zljdbm=@zljdbm ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@zljdbm", zljdbm);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_aj_zj_sgxk_relation(List<IDataItem> conditions)
        {
            string sql = "select * from aj_zj_sgxk_relation ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_SaveToStLog(List<IDataItem> conditions)
        {
            string sql = "select * from SaveToStLog  ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 根据单位分类获取建设单位信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdwByDwfl(string dwfl)
        {
            string sql = @"select * from uepp_jsdw  where  DataState=0 and dwfl=@dwfl ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@dwfl", dwfl);
            return DB.ExeSqlForDataTable(sql, sp, "uepp_jsdw");
        }

        /// <summary>
        /// 按组织机构代码获取建设单位
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_by_qyid(string jsdwID)
        {
            string sql = "";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            if (jsdwID.Length == 9)
            {
                sql = @"select top 1 * from uepp_jsdw  where jsdwID=@jsdwID or substring(jsdwID,1,8)+substring(jsdwID,10,1)=@jsdwID or (substring(jsdwID,9,9))=@jsdwID order by len(jsdwID) desc ";
            }
            else
                if (jsdwID.Length == 10)
                {
                    sql = @"select top 1 * from uepp_jsdw  where jsdwID=@jsdwID or substring(jsdwID,1,8)+'-'+substring(jsdwID,9,1)=@jsdwID or substring(jsdwID,9,8)+'-'+substring(jsdwID,17,1) =@jsdwID  order by len(jsdwID) desc  ";
                }
                else
                {
                    sql = @"select top 1  * from uepp_jsdw  where jsdwID=@jsdwID  order by len(jsdwID) desc  ";
                }

            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "uepp_jsdw");
        }


        /// <summary>
        /// 获取江阴项目建设单位列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_bycounty(string countyNum,string startDate, string endDate)
        {
            string sql = "";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sql = @"SELECT * FROM WJSJZX.dbo.UEPP_Jsdw
                    WHERE OperateDate > @startDate and OperateDate < @endDate
                    AND WJSJZX.dbo.GetCorpZZJGDM(jsdwID) IN (
		                    SELECT WJSJZX.dbo.GetCorpZZJGDM(BuildCorpCode)
		                    FROM WJSJZX.dbo.tbprojectinfo
		                    WHERE CountyNum = @countyNum
		                    )";
            sp.Add("@countyNum", countyNum);
            sp.Add("@startDate", startDate);
            sp.Add("@endDate", endDate);
            return DB.ExeSqlForDataTable(sql, sp, "buildCorp");
        }

        /// <summary>
        /// 获取江阴项目施工单位列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_sgdw_bycounty(string countyNum, List<IDataItem> conditions)
        {
            string sql = @"SELECT *
                FROM (
                    SELECT *, SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10) CreateDate
FROM WJSJZX.dbo.UEPP_Qyjbxx a
WHERE CountyID = @countyNum  AND (a.qyID in (select qyid from uepp_qycsyw where csywlxid in (1,3,2,13,14)))
	 ) AS aaa
                WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@countyNum", countyNum);

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取江阴项目勘察单位列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_kcdw_bycounty(string countyNum, List<IDataItem> conditions)
        {
            string sql = @"SELECT *
                FROM (
                    SELECT *, SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10) CreateDate
FROM WJSJZX.dbo.UEPP_Qyjbxx a
WHERE CountyID = @countyNum  AND (a.qyID in (select qyid from uepp_qycsyw where csywlxid in (5)))
	OR (
		qyID IN (
			SELECT SUBSTRING(replace(EconCorpCode, '-', ''), 1, 8) + '-' + SUBSTRING(replace(EconCorpCode, '-', ''), 9, 1)
			FROM [dbo].[TBProjectCensorInfo] b
			LEFT JOIN .dbo.TBProjectInfo i ON b.PrjNum = i.PrjNum
			WHERE i.CountyNum = @countyNum
				AND EconCorpCode != ''
			)
		)
	                ) AS aaa
                WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@countyNum", countyNum);

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 获取江阴项目设计单位列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_sjdw_bycounty(string countyNum, List<IDataItem> conditions)
        {
            string sql = @"SELECT *
FROM WJSJZX.dbo.UEPP_Qyjbxx a
WHERE CountyID = '320281' AND (a.qyID in (select qyid from uepp_qycsyw where csywlxid in (6,2)))
	OR (
		qyID IN (
			SELECT SUBSTRING(replace(DesignCorpCode, '-', ''), 1, 8) + '-' + SUBSTRING(replace(DesignCorpCode, '-', ''), 9, 1)
			FROM [WJSJZX].[dbo].[TBProjectCensorInfo] b
			LEFT JOIN WJSJZX.dbo.TBProjectInfo i ON b.PrjNum = i.PrjNum
			WHERE i.CountyNum = @countyNum
				AND EconCorpCode != ''
			)
		)
ORDER BY xgrqsj ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@countyNum", countyNum);

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 获取江阴项目中介机构列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_zjjg_bycounty(string countyNum, List<IDataItem> conditions)
        {
            string sql = @"SELECT *
                FROM (
                    SELECT *, SUBSTRING(convert(VARCHAR(30), a.xgrqsj, 120), 1, 10) CreateDate
FROM WJSJZX.dbo.UEPP_Qyjbxx a
WHERE CountyID = @countyNum  AND (a.qyID in (select qyid from uepp_qycsyw where csywlxid in (7,4,8,9,15,16,17))) ) AS aaa
                WHERE 1 = 1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@countyNum", countyNum);

            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        /// <summary>
        /// 获取企业基本信息（建设单位除外）
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable GetQyjbxx(string qyID)
        {
            string sql = "";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sql = @"select top 1 * from UEPP_Qyjbxx  where qyID=@qyID";
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "qyjbxx");
        }

        /// <summary>
        /// 获取江阴项目建设单位代码列表
        /// </summary>
        /// <param name="countyNum"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_code_bycounty(string countyNum)
        {
            string sql = "";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sql = @"SELECT BuildCorpName
	                    ,BuildCorpCode
                    FROM WJSJZX.dbo.tbprojectinfo
                    WHERE CountyNum = @countyNum
                    ORDER BY CreateDate DESC";
            sp.Add("@countyNum", countyNum);
            return DB.ExeSqlForDataTable(sql, sp, "buildCorp");
        }

        /// <summary>
        /// 获取企业证书信息
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public DataTable GetCorpCert(string qyid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @"SELECT ROW_NUMBER() OVER (
		ORDER BY zzlb
			,zsbh
		) AS rowno
	,*
FROM (
	SELECT DISTINCT *
	FROM (
		SELECT zzmx.csywlx zzlb
			,zs.zsbh
			,zzmx.zzlb AS zylb
			,zzmx.zzlbID AS zylbID
			,zzmx.zzxl
			,zzmx.zzxlID AS zzxlID
			,zzmx.zzdj
			,CONVERT(VARCHAR(12), zs.zsyxqrq, 23) AS fzrq
			,CONVERT(VARCHAR(12), zs.zsyxzrq, 23) AS yxq
			,zs.fzdw
		FROM UEPP_Qyzzmx zzmx
		LEFT JOIN UEPP_Qyzs zs ON zzmx.zsbh = zs.zsbh
		LEFT JOIN Uepp_Qyjbxx jbxx ON zzmx.qyID = jbxx.qyID
		WHERE zsyxzrq > GETDATE()
			AND zzmx.DataState <> - 1
			AND zzmx.qyID = @pQyID
		) aaa
	
	UNION ALL
	
	SELECT qyzs.csywlx
		,qyzs.zsbh
		,qyzs.zslx AS zylb
		,''
		,''
		,''
		,''
		,CONVERT(VARCHAR(12), qyzs.zsyxqrq, 23) AS fzrq
		,CONVERT(VARCHAR(12), qyzs.zsyxzrq, 23) AS yxq
		,qyzs.fzdw
	FROM UEPP_Qyzs qyzs
	WHERE qyzs.DataState <> - 1
		AND qyID = @pQyID
		AND zslxID = 140
	) t
ORDER BY zzlb
	,zsbh";
            sp.Add("@pQyID", qyid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 获取企业人员信息
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public DataTable GetCorpStaff(string qyid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @"SELECT ROW_NUMBER() OVER (
		ORDER BY ryid
			,zsbh
		) AS rowno
	,qymc
	,qyid
	,ryid
	,xm
	,zjhm
	,lxdh
	,ryzyzglxid
	,ryzyzglx
	,zsjlId
	,ryzslxid
	,ryzslx
	,zsbh
	,zsyxqrq
	,zsyxzrq
FROM (
	SELECT DISTINCT *
	FROM (
		SELECT a.qymc
			,b.qyid
			,b.ryid
			,c.xm
			,c.zjhm
			,ISNULL(c.lxdh, c.yddh) lxdh
			,b.ryzyzglxid
			,b.ryzyzglx
			,d.zsjlId
			,d.ryzslxid
			,d.ryzslx
			,d.zsbh
			,CONVERT(VARCHAR(10), d.zsyxqrq, 120) zsyxqrq
			,CONVERT(VARCHAR(10), d.zsyxzrq, 120) zsyxzrq
		FROM uepp_qyjbxx a
		INNER JOIN UEPP_QyRy b ON a.qyid = b.qyid
			AND b.DataState <> - 1
		INNER JOIN uepp_ryjbxx c ON b.ryid = c.ryid
			AND c.DataState <> - 1
		LEFT JOIN uepp_ryzs d ON c.ryid = d.ryid
			AND d.DataState <> - 1
			AND b.ryzyzglxID = d.ryzyzglxID
		WHERE 1 = 1
			AND a.qyID = @qyID
		) mid
	) ryxx
WHERE 1 = 1";
            sp.Add("@qyID", qyid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 获取企业从事业务类型
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qycsywlx()
        {
            string sql = @"select * from UEPP_Code where CodeType='企业从事业务类型' ";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "dt_qycsywlx");
            return dt;
        }

        /// <summary>
        /// 根据单位从事业务类型获取施工单位信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Sgdw(string dwfl,string clrqS,string clrqE)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(dwfl, "@para", ref sp);

            string sql = @" select qyID, qymc, zzjgdm, yyzzzch, khyh, yhzh, sfsyq, gcjsry_zs, gcjsry_gjzcrs, gcjsry_zjzcrs, sylxID, sylx, ProvinceID, Province, CityID, City, CountyID, County, zcdd, jjxzID, jjxz, zczb, zyfw, jyfw, clrq, qyjj, xxdd, yzbm, cz, email, webAddress, lxr, lxdh, fddbr_ryid, fddbr, qyfzr_ryid, qyfzr, cwfzr_ryid, cwfzr, jsfzr_ryid, jsfzr, aqfzr_ryid, aqfzr, xgr, xgrqsj, tag
 from UEPP_Qyjbxx where DataState=0 and qyID in (
  select qyID from UEPP_Qycsyw where csywlxID in (1,3,2,13,14) and  DataState=0  ";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and csywlx in (" + sqlPara + "))";
            sql += " and  YEAR(clrq) between YEAR('" + clrqS + "') and YEAR('" + clrqE + "')";

            
            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 根据企业组织机构代码获取施工单位信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_sgdw_single(string corpCode)
        {
            string sql = "";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sql = @" SELECT a.* FROM uepp_qyjbxx a WHERE DataState != - 1 and  qyID=@qyID";

            sp.Add("@qyID", corpCode);
            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");
        }


        /// <summary>
        /// 获取勘察单位基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Kcdw()
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @" select qyID, qymc, zzjgdm, yyzzzch, khyh, yhzh, sfsyq, gcjsry_zs, gcjsry_gjzcrs, gcjsry_zjzcrs, sylxID, sylx, ProvinceID, Province, CityID, City, CountyID, County, zcdd, jjxzID, jjxz, zczb, zyfw, jyfw, clrq, qyjj, xxdd, yzbm, cz, email, webAddress, lxr, lxdh, fddbr_ryid, fddbr, qyfzr_ryid, qyfzr, cwfzr_ryid, cwfzr, jsfzr_ryid, jsfzr, aqfzr_ryid, aqfzr, tag, xgr, xgrqsj
	 from  UEPP_Qyjbxx where DataState=0 and  qyID in (
	 select qyID from UEPP_Qycsyw where DataState=0 and csywlxID=5
	)";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_kcdw");

        }


        public DataTable Get_uepp_Sjdw(string dwfl)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(dwfl, "@para", ref sp);

            string sql = @" select qyID, qymc, zzjgdm, yyzzzch, khyh, yhzh, sfsyq, gcjsry_zs, gcjsry_gjzcrs, gcjsry_zjzcrs, sylxID, sylx, ProvinceID, Province, CityID, City, CountyID, County, zcdd, jjxzID, jjxz, zczb, zyfw, jyfw, clrq, qyjj, xxdd, yzbm, cz, email, webAddress, lxr, lxdh, fddbr_ryid, fddbr, qyfzr_ryid, qyfzr, cwfzr_ryid, cwfzr, jsfzr_ryid, jsfzr, aqfzr_ryid, aqfzr, tag, xgr, xgrqsj
	 from  UEPP_Qyjbxx where DataState=0 and  qyID in (
	 select qyID from UEPP_Qycsyw where DataState=0 and csywlxID in (6,2) ";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and csywlx in (" + sqlPara + "))";
            return DB.ExeSqlForDataTable(sql, sp, "dt_sjdw");

        }
        /// <summary>
        /// 获取中介机构信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Zjjg(string dwfl)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(dwfl, "@para", ref sp);

            string sql = @" select qyID, qymc, zzjgdm, yyzzzch, khyh, yhzh, sfsyq, gcjsry_zs, gcjsry_gjzcrs, gcjsry_zjzcrs, sylxID, sylx, ProvinceID, Province, CityID, City, CountyID, County, zcdd, jjxzID, jjxz, zczb, zyfw, jyfw, clrq, qyjj, xxdd, yzbm, cz, email, webAddress, lxr, lxdh, fddbr_ryid, fddbr, qyfzr_ryid, qyfzr, cwfzr_ryid, cwfzr, jsfzr_ryid, jsfzr, aqfzr_ryid, aqfzr, tag, xgr, xgrqsj
	 from  UEPP_Qyjbxx where DataState=0 and  qyID in (
	 select qyID from UEPP_Qycsyw where DataState=0 and csywlxID in (7,4,8,9,15,16,17) ";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and csywlx in (" + sqlPara + "))";
            return DB.ExeSqlForDataTable(sql, sp, "dt_zjjg");

        }

        /// <summary>
        /// 获取人员执业资格类型
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzyzglx()
        {
            string sql = @"select * from UEPP_Code where CodeType='人员执业资格类型' ";
            return DB.ExeSqlForDataTable(sql, null, "dt_ryzyzglx");

        }


        /// <summary>
        /// 获取注册执业人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Zczyry(string ryzyzglx)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(ryzyzglx, "@p", ref sp);

            string sql = @"select ryID, UserID, xm, zjlxID, zjlx, zjhm, xb, csrq, mz, xlID, xl, sxzy, byyx, byny, zcID, zc, zczh, zcjbID, zcjb, zczyID, zczy, zwID, zw, csgzjsnx, lxdh, yddh, gzjl, bz, DataState, tag, xgr, xgrqsj, sfzsmj, ryzz, fzjg, sfzyxqs, sfzyxqz, AJ_IsRefuse, AJ_EXISTINIDCARDS
 from UEPP_Ryjbxx where DataState=0 and  ryID in (
select  ryID from UEPP_Ryzyzg where  ryzyzglxID in (1,2,21,41,51,61) and DataState<>-1 ";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and ryzyzglx in (" + sqlPara + "))";

            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zczyry");
        }

        /// <summary>
        /// 获取安全生产管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Aqscglry(string ryzyzglx)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(ryzyzglx, "@para", ref sp);

            string sql = @"select ryID, UserID, xm, zjlxID, zjlx, zjhm, xb, csrq, mz, xlID, xl, sxzy, byyx, byny, zcID, zc, zczh, zcjbID, zcjb, zczyID, zczy, zwID, zw, csgzjsnx, lxdh, yddh, gzjl, bz, DataState, tag, xgr, xgrqsj, sfzsmj, ryzz, fzjg, sfzyxqs, sfzyxqz, AJ_IsRefuse, AJ_EXISTINIDCARDS
 from UEPP_Ryjbxx where DataState=0 and  ryID in (
select  ryID from UEPP_Ryzyzg where  ryzyzglxID in (4,5,6) and DataState<>-1
";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and ryzyzglx in (" + sqlPara + "))";

            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zczyry");
        }

        /// <summary>
        /// 获取专业岗位管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Zygwglry(string ryzyzglx)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(ryzyzglx, "@para", ref sp);

            string sql = @"select ryID, UserID, xm, zjlxID, zjlx, zjhm, xb, csrq, mz, xlID, xl, sxzy, byyx, byny, zcID, zc, zczh, zcjbID, zcjb, zczyID, zczy, zwID, zw, csgzjsnx, lxdh, yddh, gzjl, bz, DataState, tag, xgr, xgrqsj, sfzsmj, ryzz, fzjg, sfzyxqs, sfzyxqz, AJ_IsRefuse, AJ_EXISTINIDCARDS
 from UEPP_Ryjbxx where DataState=0 and  ryID in (
select  ryID from UEPP_Ryzyzg where  ryzyzglxID in (9,11,7,12,8,91,16,17,18,22,42) and DataState<>-1
";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and ryzyzglx in (" + sqlPara + "))";

            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zczyry");
        }

        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Aqscglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select zsjlId, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj, DataState 
from UEPP_Ryzs where DataState=0 and ryID = @ryID and ryzyzglxID in (4,5,6) ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
        }


        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Aqscglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select ID, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, zzbz, zyzglbID, zyzglb, zyzgdjID, zyzgdj, gzfw, bz, tag, xgr, xgrqsj, DataState
 from UEPP_Ryzymx where DataState=0 and  ryzyzglxID in (4,5,6) and ryID=@ryID ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
        }

        /// <summary>
        /// 获取施工监理合同备案信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable Get_TBContractRecordManage_SgJlHtba()
        {

            string sql = @"select * from (
select PKID, RecordName, RecordNum, RecordInnerNum, PrjNum, ContractNum, ContractTypeNum, ContractMoney, PrjSize, ContractDate, PropietorCorpName, PropietorCorpCode, ContractorCorpName, ContractorCorpCode, UnionCorpName, UnionCorpCode,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, PrjHead, PrjHeadPhone, IDCard, sbdqbm from TBContractRecordManage where ContractTypeNum in (301,302,304)
) aaa where 1=1 ";
            //UpdateFlag='U' 
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }



        /// <summary>
        /// 获取施工图审查信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable Get_TBProjectCensorInfoByPrjNum(string PrjNum)
        {

            string sql = @" 
              select       
                a.CensorNum,
                convert(char(10),a.CensorEDate,20) as CensorEDate 
                from TBProjectCensorInfo as a        
				where a.UpdateFlag='U' and PrjNum=@PrjNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PrjNum", PrjNum);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        /// <summary>
        /// 获取企业资质信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SgQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID in (1,3,2,13,14) and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取企业所有资质信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_all_qyzz(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select * from UEPP_Qyzzmx where DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        public DataTable Get_uepp_KcQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID=5  and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取设计企业证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SjQyzsByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"  select zsjlId, qyID, csywlxID, csywlx, zslxID, zslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj
    from UEPP_Qyzs where csywlxID in (6,2) and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取施工企业证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SgQyzsByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"  select zsjlId, qyID, csywlxID, csywlx, zslxID, zslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj
    from UEPP_Qyzs where csywlxID in (1,3,2,13,14) and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }


        public DataTable Get_uepp_ZjjgQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID in (7,4,8,9,15,16,17)  and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "dt_qyzz");

        }

        /// <summary>
        /// 获取企业所有证书
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_all_qyzs(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"  select * from UEPP_Qyzs where DataState=0 and qyID=@qyID ";
            return DB.ExeSqlForDataTable(sql, sp, "dt_qyzz");

        }

        /// <summary>
        /// 获取中介机构信息的证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_ZjjgQyzsByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"  select zsjlId, qyID, csywlxID, csywlx, zslxID, zslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj
    from UEPP_Qyzs where csywlxID in (7,4,8,9,15,16,17) and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取勘察企业证书基本信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_KcQyzsByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"  select zsjlId, qyID, csywlxID, csywlx, zslxID, zslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj
    from UEPP_Qyzs where csywlxID=5 and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Zczyry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select ID, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, zzbz, zyzglbID, zyzglb, zyzgdjID, zyzgdj, gzfw, bz, tag, xgr, xgrqsj, DataState
 from UEPP_Ryzymx where DataState=0 and  ryzyzglxID in (1,2,21,41,51,61) and ryID=@ryID ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zczyry");
        }

        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Zygwglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select ID, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, zzbz, zyzglbID, zyzglb, zyzgdjID, zyzgdj, gzfw, bz, tag, xgr, xgrqsj, DataState
 from UEPP_Ryzymx where DataState=0 and  ryzyzglxID in (9,11,7,12,8,91,16,17,18,22,42) and ryID=@ryID ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
        }
        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Zczyry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select zsjlId, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj, DataState 
from UEPP_Ryzs where DataState=0 and ryID = @ryID and ryzyzglxID in (1,2,21,41,51,61) ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zczyry");
        }
        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Zygwglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select zsjlId, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj, DataState 
from UEPP_Ryzs where DataState=0 and ryID = @ryID and ryzyzglxID in (9,11,7,12,8,91,16,17,18,22,42) ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
        }


        public DataTable Get_uepp_SjQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID in (6,2)  and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        /// <summary>
        /// 获取企业信用考评信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_QyXykp(List<IDataItem> list)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select a.*,b.qyID from XykpImport a
left join UEPP_Qyjbxx b on a.zzjgdm=b.zzjgdm 
where 1=1 ";

            if (list.Exists(p => p.ItemName == "csywlx"))
            {
                IDataItem item = list.GetDataItem("csywlx");
                if (!string.IsNullOrEmpty(item.ItemData))
                {
                    string sqlPara = AntiSqlInjection.ParameterizeInClause(item.ItemData, "@para", ref sp);
                    sql += " and b.qyID in (select qyID from UEPP_Qycsyw where csywlx in (" + sqlPara + ")) ";
                }
                list.Remove(item);
            }
            list.GetSearchClause(sp, ref sql);

            return DB.ExeSqlForDataTable(sql, sp, "dt_QyXykp");
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 根据立项项目的内部编号获取立项项目信息
        /// </summary>
        /// <patenderInnerNumram name="prjInnerNum"></param>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectInfo(string prjInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select PKID, PrjNum, PrjInnerNum, PrjName, PrjTypeNum, BuildCorpName, BuildCorpCode, ProvinceNum, CityNum, CountyNum, PrjApprovalNum, PrjApprovalLevelNum, BuldPlanNum, ProjectPlanNum, AllInvest, AllArea, PrjSize, PrjPropertyNum, PrjFunctionNum, BDate, EDate, CreateDate, UpdateFlag, sbdqbm,xgrqsj ,updateUser from TBProjectInfo where PrjInnerNum in (" + AntiSqlInjection.ParameterizeInClause(prjInnerNum, "@para", ref sp) + ") ";

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
        /// <summary>
        /// 根据企业组织机构代码、年份、季度、考评类型获取考评
        /// </summary>
        /// <param name="zzjgdm"></param>
        /// <param name="kpnf"></param>
        /// <param name="kpjd"></param>
        /// <returns></returns>
        public DataTable GetTBData_Xykp(string zzjgdm, int kpnf, int kpjd, int kptype)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@zzjgdm", zzjgdm);
            sp.Add("@kpnf", kpnf);
            sp.Add("@kpjd", kpjd);
            sp.Add("@kptype", kptype);

            string sql = @" SELECT id,kp_nf, kp_jd,kp_type,kpqymc, kpqy_zzjgdm, zhdf, khnf, khyf,oper,updateUser,createTime,updateTime FROM Xypj_kpjlhz WHERE kpqy_zzjgdm=@zzjgdm and kp_nf=@kpnf and kp_jd = @kpjd and kp_type = @kptype ";

            return DB.ExeSqlForDataTable(sql, sp, "dt_Xykp");
        }


        public DataTable GetTBData_xm_gcdjb_dtxm(string fxnbbm)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from xm_gcdjb_dtxm where fxnbbm in (" + AntiSqlInjection.ParameterizeInClause(fxnbbm, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable GetTBData_TBTenderInfo(string tenderInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //if (tenderInnerNum.IndexOf(",") < 0)
            //    tenderInnerNum = "'" + tenderInnerNum + "'";
            string sql = "select * from TBTenderInfo where TenderInnerNum in (" + AntiSqlInjection.ParameterizeInClause(tenderInnerNum, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBContractRecordManage(string recordInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //if (recordInnerNum.IndexOf(",") < 0)
            //    recordInnerNum = "'" + recordInnerNum + "'";
            string sql = "select * from TBContractRecordManage where RecordInnerNum in ("
                + AntiSqlInjection.ParameterizeInClause(recordInnerNum, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectCensorInfo(string censorInnerNum)
        {

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //if (censorInnerNum.IndexOf(",") < 0)
            //    censorInnerNum = "'" + censorInnerNum + "'";
            string sql = "select * from TBProjectCensorInfo where CensorInnerNum in ("
                + AntiSqlInjection.ParameterizeInClause(censorInnerNum, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManage(string builderLicenceInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //if (builderLicenceInnerNum.IndexOf(",") < 0)
            //    builderLicenceInnerNum = "'" + builderLicenceInnerNum + "'";
            string sql = "select * from TBBuilderLicenceManage where BuilderLicenceInnerNum in ("
                 + AntiSqlInjection.ParameterizeInClause(builderLicenceInnerNum, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectFinishManage(string prjFinishInnerNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //if (prjFinishInnerNum.IndexOf(",") < 0)
            //    prjFinishInnerNum = "'" + prjFinishInnerNum + "'";
            string sql = "select * from TBProjectFinishManage where PrjFinishInnerNum in ("
                + AntiSqlInjection.ParameterizeInClause(prjFinishInnerNum, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_zj_gcjbxx(string zljdbmString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from zj_gcjbxx where zljdbm in ("
                + AntiSqlInjection.ParameterizeInClause(zljdbmString, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_zj_gcjbxxByPKIDs(string pkIdsString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from zj_gcjbxx where pKID in ("
                + AntiSqlInjection.ParameterizeInClause(pkIdsString, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable GetTBData_aj_gcjbxx(string aqjdbmString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from aj_gcjbxx where aqjdbm in ("
                + AntiSqlInjection.ParameterizeInClause(aqjdbmString, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_aj_gcjbxxByPKIDs(string pkidsString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from aj_gcjbxx where pKID in ("
                + AntiSqlInjection.ParameterizeInClause(pkidsString, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_aj_zj_sgxk_relation(string aqjdbmString)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from aj_zj_sgxk_relation where aqjdbm in ("
                + AntiSqlInjection.ParameterizeInClause(aqjdbmString, "@para", ref sp) + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetBuilderLicenceNumByAqjdbm(string aqjdbm)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@aqjdbm", aqjdbm);
            string sql = "select a.BuilderLicenceNum,b.BuilderLicenceNum BuilderLicenceNum2 from aj_zj_sgxk_relation a left join TBBuilderLicenceManage  b  on a.BuilderLicenceNum =b.BuilderLicenceInnerNum  where aqjdbm =@aqjdbm ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable GetTBData_SaveToStLog(string tableName, string PKID)
        {
            string sql = "select * from SaveToStLog where TableName=@TableName and PKID =@PKID  ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            sp.Add("@TableName", tableName);
            sp.Add("@PKID", PKID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_SaveToStLog2(string tableName, string PKIDs)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from SaveToStLog where TableName=@TableName and PKID in ("
                + AntiSqlInjection.ParameterizeInClause(PKIDs, "@para", ref sp) + ")";

            sp.Add("@TableName", tableName);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        //public DataTable GetTBData_SaveToStLog(string tableName, string pkId)
        //{
        //    string sql = String.Empty;
        //    pkId = pkId.Trim(',');
        //    if (pkId.IndexOf(",") >= 0)
        //    {
        //        sql = "select * from SaveToStLog where tableName='" + tableName + "' and PKID in (" + pkId + ") ";
        //        return DB.ExeSqlForDataTable(sql, null, "dt");
        //    }
        //    else
        //    {
        //        sql = "select * from SaveToStLog where tableName='" + tableName + "' and PKID ='" + pkId + "' ";
        //        return DB.ExeSqlForDataTable(sql, null, "dt");
        //    }



        //}

        public DataTable GetPrjType()
        {
            string sql = "select * from tbPrjTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetPrjProperty()
        {
            string sql = "select * from tbPrjPropertyDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetPrjFunction()
        {
            string sql = "select * from tbPrjFunctionDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetTenderClass()
        {
            string sql = "select * from tbTenderClassDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetTenderType()
        {
            string sql = "select * from tbTenderTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetIDCardType()
        {
            string sql = "select * from tbIDCardTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetContractType()
        {
            string sql = "select * from tbContractTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }

        public DataTable GetWorkDuty()
        {
            string sql = "select * from tbWorkDutyDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetSpecialtyType()
        {
            string sql = "select * from tbSpecialtyTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetStructureType()
        {
            string sql = "select * from tbPrjStructureTypeDic ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable Get_tbXzqdmDic()
        {
            string sql = " select Code,CodeInfo from 	tbXzqdmDic where parentCode='320200' ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetCountryCodes(string deptType)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = " select countryCode from Ap_api_user where deptType=@deptType";
            sp.Add("@deptType", deptType);
            return DB.ExeSqlForDataTable(sql, sp, "dt");  
        }
        public bool IsExistsSgxkxx(string builderLicenceNum)
        {
            string sql = "select count(*) from TBBuilderLicenceManage where BuilderLicenceNum=@BuilderLicenceNum  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@BuilderLicenceNum", builderLicenceNum);
            return DB.ExeSqlForString(sql, sp).ToInt32(0) > 0;
        }

        /// <summary>
        /// 企业信用考评数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetData_QyXykp(string ids)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from XykpImport where ID in (" + AntiSqlInjection.ParameterizeInClause(ids, "@para", ref sp) + ") ";

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 获取行政处罚
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetData_Xzcf(string ids)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from Xzcf where ajNo in (" + AntiSqlInjection.ParameterizeInClause(ids, "@para", ref sp) + ") ";

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        /// <summary>
        /// 获取保障房源
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetData_Bzfy(string CJHOUSENO)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from Syzxspt_Bzfy where CJHOUSENO=@CJHOUSENO";
            sp.Add("@CJHOUSENO", CJHOUSENO);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取保障对象
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetData_Bzdx(string SOAPPLYIDCARDNO)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from Syzxspt_Bzdxxx where SOAPPLYIDCARDNO=@SOAPPLYIDCARDNO";
            sp.Add("@SOAPPLYIDCARDNO", SOAPPLYIDCARDNO);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 获取保障对象家庭成员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetData_Bzdxjtcy(string IDCARDNO)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select * from Syzxspt_Bzdxjtcy where IDCARDNO=@IDCARDNO";
            sp.Add("@IDCARDNO", IDCARDNO);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }




        #endregion

        #region
        public string GetMaxXmhjNumber_TBTenderInfo(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(SUBSTRING(TenderNum,charindex('-',TenderNum,1)+4,3)) from TBTenderInfo where prjNum=@PrjNum and SUBSTRING(TenderNum,charindex('-',TenderNum,1)+1,2)=@Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@PrjNum", prjNum);
            spc.Add("@Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBContractRecordManage(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(SUBSTRING(RecordNum,charindex('-',RecordNum,1)+4,3)) from TBContractRecordManage where prjNum=@PrjNum and SUBSTRING(RecordNum,charindex('-',RecordNum,1)+1,2)=@Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@PrjNum", prjNum);
            spc.Add("@Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBProjectCensorInfo(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(SUBSTRING(CensorNum,charindex('-',CensorNum,1)+4,3)) from TBProjectCensorInfo where prjNum=@PrjNum and SUBSTRING(CensorNum,charindex('-',CensorNum,1)+1,2)=@Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@PrjNum", prjNum);
            spc.Add("@Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBBuilderLicenceManage(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(SUBSTRING(BuilderLicenceNum,charindex('-',BuilderLicenceNum,1)+4,3)) from TBBuilderLicenceManage where prjNum=@PrjNum and SUBSTRING(BuilderLicenceNum,charindex('-',BuilderLicenceNum,1)+1,2)=@Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@PrjNum", prjNum);
            spc.Add("@Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBProjectFinishManage(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(SUBSTRING(PrjFinishNum,charindex('-',PrjFinishNum,1)+4,3)) from TBProjectFinishManage where prjNum=@PrjNum and SUBSTRING(PrjFinishNum,charindex('-',PrjFinishNum,1)+1,2)=@Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@PrjNum", prjNum);
            spc.Add("@Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        public bool IsExists_TBProjectInfo(string prjNum)
        {
            string sql = "select * from TBProjectInfo where PrjNum=@PrjNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PrjNum", prjNum);
            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "dt");
            return (dt == null || dt.Rows.Count == 0) ? false : true;
        }

        public DataTable GetPrjNums_TBProjectInfo()
        {
            string sql = " select PrjNum  from  TBProjectInfo where UpdateFlag='U' ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        public DataTable GetCensorNums_TBProjectCensorInfo()
        {
            string sql = " select CensorNum  from  TBProjectCensorInfo where UpdateFlag='U' ";
            return DB.ExeSqlForDataTable(sql, null, "dt");

        }
        /// <summary>
        /// 获取接口用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable GetInterfaceUserInfo(string userName, string pwd)
        {
            string sql = " select * from uepp_InterfaceUser where UserName=@userName and Pwd=@pwd and DataState=0 ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@userName", userName);
            sp.Add("@pwd", pwd);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable Read_zj_gcjbxx(string zljdbm)
        {
            string sql = " select * from zj_gcjbxx where zljdbm=@zljdbm and UpdateFlag='U' ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@zljdbm", zljdbm);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxxByPKID(string pKID)
        {
            string sql = " select * from zj_gcjbxx where pKID=@pKID and UpdateFlag='U' ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@pKID", pKID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxx_zrdw(string pkid, string xh)
        {
            string sql = " select * from zj_gcjbxx_zrdw where pKID=@PKID and xh=@xh ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkid);
            sp.Add("@xh", xh);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        #endregion

        public DataTable Get_API_zb_apiFlow(string apiFlow)
        {
            string sql = @" select apiControl from API_zb where apiFlow=@apiFlow		  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiFlow", apiFlow);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetSchema_API_cb()
        {
            string sql = "select * from API_cb  where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public DataTable GetAPIUnable()
        {
            string sql = "select '接口关闭' as jkgb ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public string Get_apiCbNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,apiCbID)),0)+1 from API_cb ";
            return DB.ExeSqlForString(sql, null);
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
        
        public bool Submit_API_zb(DataTable dt)
        {
            string sql = @"select * from API_zb   where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public bool Submit_API_cb(DataTable dt)
        {
            string sql = @"select * from API_cb   where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }


    }
}
