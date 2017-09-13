using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Bigdesk8.Data;
using System.Data;
using Bigdesk8.Security;
using WxjzgcjczyTimerService;
using Bigdesk8;

namespace WxsjzxTimerService
{

    public class DataService
    {
        /// <summary>
        /// 无锡数据中心与无锡建筑工程基础资源管理平台（新库）数据访问DBOperator
        /// </summary>
        public DBOperator DB = DBOperatorFactory.GetDBOperator(ConfigurationManager.AppSettings["ConnectionString"],
   ConfigurationManager.AppSettings["SQLSERVER"]);
        /// <summary>
        /// 无锡局一号通系统数据访问DBOperator
        /// </summary>
        public DBOperator DB_db_adminexam = DBOperatorFactory.GetDBOperator(
            ConfigurationManager.AppSettings["db_adminexam_ConnString"], ConfigurationManager.AppSettings["SQLSERVER"]);
        /// <summary>
        /// 无锡建筑工程基础资源管理平台（老库）数据访问DBOperator
        /// </summary>
        //public DBOperator DB_oracl =new OracleOperator(ConfigurationManager.AppSettings["oracl_ConnString"],DataBaseType.ORACLE11G);

        /// <summary>
        /// 无锡勘察设计系统数据访问DBOperator
        /// </summary>
        public DBOperator DB_Epoint_WXkcsj = DBOperatorFactory.GetDBOperator(ConfigurationManager.AppSettings["Epoint_WXkcsjConnectionString"],
   ConfigurationManager.AppSettings["SQLSERVER"]);
        /// <summary>
        /// 无锡安监站前置机数据库数据访问DBOperator
        /// </summary>
        public DBOperator DB_WXJGC_SSIP = DBOperatorFactory.GetDBOperator(ConfigurationManager.AppSettings["WXJGC_SSIP_ConnectionString"],
        ConfigurationManager.AppSettings["SQLSERVER"]);

  /// <summary>
        /// 省勘察设计系统数据库数据访问DBOperator
        /// </summary>
        public DBOperator DB_Epoint_Jskcsj = DBOperatorFactory.GetDBOperator(ConfigurationManager.AppSettings["Epoint_Jskcsj_ConnectionString"],
        ConfigurationManager.AppSettings["SQLSERVER"], ConfigurationManager.AppSettings["timeout_Epoint_Jskcsj"].ToInt32(1)*1000*60);


        public SqlParameterCollection CreateSqlParameterCollection()
        {
            return this.DB.CreateSqlParameterCollection();
        }

        public string ExecuteSql(string sql)
        {
            return this.DB.ExeSqlForString(sql, null);
        }
        public string ExecuteSql(string sql, SqlParameterCollection sp)
        {
            return this.DB.ExeSqlForString(sql, sp);
        }
        public DataTable GetTable(string sql, SqlParameterCollection sp)
        {
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public int ExecuteNonQuerySql(string sql, SqlParameterCollection sp)
        {
            return this.DB.ExecuteNonQuerySql(sql, sp);
        }
        public int ExecuteNonQuerySql2(string sql, SqlParameterCollection sp)
        {
            return this.DB.ExecuteNonQuerySql2(sql, sp);
        }

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


        public DataTable GetSchema_DataJkLog()
        {
            string sql = "select * from DataJkLog where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_DataJkDataDetail()
        {
            string sql = "select * from DataJkDataDetail where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_SaveDataLog()
        {
            string sql = "select * from SaveDataLog where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        //public DataTable GetSchema_uepp_qycsyw()
        //{
        //    string sql = @" select * from uepp_qycsyw where 1=2 ";
        //    return DB_oracl.ExeSqlForDataTable(sql, null, "dt");
        //}

        public DataTable GetSchema_API_cb()
        {
            string sql = "select * from API_cb  where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }


        #endregion

        #region  读取列表
        public DataTable Get_tbXzqdmDic()
        {
            string sql = " select Code,CodeInfo from 	tbXzqdmDic where parentCode='320200' ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable Get_uepp_qycsyw_kcsj(string qyID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            string sql = @" select * from uepp_qycsyw where qyID=@qyID and csywlxID in ('5','6') ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_AllTBProjectInfo()
        {
            string sql = @"select * from (
                            select PKID   ,PrjNum ,PrjInnerNum ,PrjName ,PrjTypeNum ,
            	            BuildCorpName ,BuildCorpCode ,ProvinceNum ,CityNum ,CountyNum ,PrjApprovalNum ,PrjApprovalLevelNum ,BuldPlanNum,ProjectPlanNum 
                            ,AllInvest ,AllArea  ,PrjSize ,PrjPropertyNum ,PrjFunctionNum , SUBSTRING(convert(varchar(30),BDate,120),1,10) BDate 
                            ,SUBSTRING(convert(varchar(30),EDate,120),1,10) EDate ,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CREATEDATE , UpdateFlag ,sbdqbm 
                            from TBProjectInfo ) aaa where 1=1 ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取无锡数据中心需要上传至省厅的数据
        /// </summary>
        /// <returns>返回立项项目数据集</returns>
        public DataTable GetTBData_TBProjectInfo()
        {
            string sql = @"select * from (
                            select PKID   ,PrjNum ,PrjInnerNum ,PrjName ,PrjTypeNum ,
            	            BuildCorpName ,BuildCorpCode ,ProvinceNum ,CityNum ,CountyNum ,PrjApprovalNum ,PrjApprovalLevelNum ,BuldPlanNum,ProjectPlanNum 
                            ,AllInvest ,AllArea  ,PrjSize ,PrjPropertyNum ,PrjFunctionNum , SUBSTRING(convert(varchar(30),BDate,120),1,10) BDate 
                            ,SUBSTRING(convert(varchar(30),EDate,120),1,10) EDate ,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate ,UpdateFlag ,sbdqbm 
                            from TBProjectInfo ) aaa where 1=1  and PKID not in (
							  select  PKID from SaveToStLog where TableName='TBProjectInfo' and  OperateState in (0,2)
							)  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取无锡数据中心需要上传至省厅的项目补充数据
        /// </summary>
        /// <returns>返回立项项目补充数据集</returns>
        public DataTable GetTBData_TBProjectAdditionalInfo()
        {
            string sql = @"SELECT PKID,prjnum,prjpassword,gyzzpl,dzyx,lxr,yddh, xmtz, gytze, gytzbl, lxtzze, sbdqbm
                            FROM TBProjectAdditionalInfo
                            WHERE PKID IN ( SELECT PKID FROM SaveToStLog WHERE TableName = 'TBProjectAdditionalInfo' AND OperateState NOT IN (0 ,2 ) )";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBProjectInfo(string PKID)
        {
            string sql = @"select * from TBProjectInfo where PKID='" + PKID + "' ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBProjectInfoByPrjNum(string PrjNum)
        {
            string sql = @"select * from TBProjectInfo where PrjNum='" + PrjNum + "' ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取无锡数据中心需要上传至省厅的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_xm_gcdjb_dtxm()
        {
            string sql = @" select * from (
 select PKID, PrjNum, fxbm, fxnbbm, xmmc, gclb, gczj, jzmj, jsgm, jglx, jsyt, dscs, dxcs, gd, kd, jhkgrq, jhjgrq,convert(varchar(30),CREATEDATE,20) CreateDate, UpdateFlag, sbdqbm 
 from xm_gcdjb_dtxm ) aaa where  PKID not in (
	select  PKID from SaveToStLog where TableName='xm_gcdjb_dtxm' and  OperateState=0
) ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取无锡数据中心需要上传至省厅的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBTenderInfo()
        {
            string sql = @"select * from (
 select PKID, TenderName, TenderNum, TenderInnerNum, PrjNum, TenderClassNum, TenderTypeNum, TenderResultDate, TenderMoney, PrjSize, Area, AgencyCorpName, AgencyCorpCode, TenderCorpName, TenderCorpCode, ConstructorName, ConstructorPhone, IDCardTypeNum, ConstructorIDCard, shypbf,convert(varchar(30),CREATEDATE,120) CreateDate,  UpdateFlag, sbdqbm 
 from TBTenderInfo
) aaa where  PKID not in (
	select  PKID from SaveToStLog where TableName='TBTenderInfo' and  OperateState=0
) ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 要送往省信用平台的合同备案信息（只包括勘察设计合同备案）
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBContractRecordManage()
        {
            string sql = @"select * from (
select PKID, RecordName, RecordNum, RecordInnerNum, PrjNum, ContractNum, ContractTypeNum, ContractMoney, PrjSize, ContractDate, PropietorCorpName, PropietorCorpCode, ContractorCorpName, ContractorCorpCode, UnionCorpName, UnionCorpCode,convert(varchar(30),CREATEDATE,120) CreateDate, UpdateFlag, PrjHead, PrjHeadPhone, IDCard, sbdqbm
from TBContractRecordManage
) aaa where ContractTypeNum in ('100','200') and PKID not in (
	select  PKID from SaveToStLog where TableName='TBContractRecordManage' and PKID=aaa.PKID and  OperateState=0
)  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }


        public DataTable GetTBData_TBProjectCensorInfo()
        {

            string sql = @"select * from (
select PKID, CensorNum, CensorInnerNum, PrjNum, CensorCorpName, CensorCorpCode, CensorEDate, PrjSize, EconCorpName, EconCorpCode, DesignCorpName, DesignCorpCode, EconCorpNum, DesignCorpNum, OneCensorIsPass, OneCensorWfqtCount, OneCensorWfqtContent,convert(varchar(30),CREATEDATE,120) CreateDate, UpdateFlag, sbdqbm
,(select OperateState from dbo.SaveToStLog  where tableName='TBProjectCensorInfo' and PKID=a.PKID) as OperateState
from TBProjectCensorInfo  a
) aaa where   PKID not in (
	select  PKID from SaveToStLog where TableName='TBProjectCensorInfo' and PKID=aaa.PKID and  OperateState=0
)  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }


        public DataTable GetTBData_TBProjectCensorInfoByCensorNum(string censorNum)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from TBProjectCensorInfo where PKID=@censorNum ";
            sp.Add("@censorNum", censorNum);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectCensorInfoByPkId(string pkId)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from TBProjectCensorInfo where PKID=@pkId ";
            sp.Add("@pkId", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable GetTBData_TBProjectDesignEconUserInfoByPkId(string pkId)
        {
            string sql = @"select * from TBProjectDesignEconUserInfo where PKID=@pkId ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@pkId", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable GetTBData_TBProjectDesignEconUserInfo()
        {
            string sql = @"select * from (
select  PKID, PrjNum, CensorNum, CorpName, CorpCode, UserName, IDCardTypeNum, IDCard, SpecialtyTypNum, UserPhone, PrjDuty,convert(varchar(30),GETDATE(),120) UpdateDate, UpdateFlag, sbdqbm 
from TBProjectDesignEconUserInfo ) as aaa where SpecialtyTypNum is not null and  PKID not in (
	select  PKID from SaveToStLog where TableName='TBProjectDesignEconUserInfo' and PKID=aaa.PKID and  OperateState=0
) ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManage()
        {

            string sql = @"select * from (
select PKID, BuilderLicenceName, BuilderLicenceNum, BuilderLicenceInnerNum, PrjNum, BuldPlanNum, ProjectPlanNum, CensorNum, ContractMoney, Area, PrjSize, IssueCertDate, EconCorpName, EconCorpCode, DesignCorpName, DesignCorpCode, ConsCorpName, ConsCorpCode, SafetyCerID, SuperCorpName, SuperCorpCode, ConstructorName, CIDCardTypeNum, ConstructorIDCard, ConstructorPhone, SupervisionName, SIDCardTypeNum, SupervisionIDCard, SupervisionPhone, convert(varchar(30),CREATEDATE,120) CreateDate,convert(varchar(30),GETDATE(),120) UpdateDate, UpdateFlag, sbdqbm
from TBBuilderLicenceManage
) as aaa where  PKID not in (
	select  PKID from SaveToStLog where TableName='TBBuilderLicenceManage' and PKID=aaa.PKID and  OperateState=0
)  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }




        public DataTable GetTBData_TBProjectBuilderUserInfo()
        {
            string sql = @"select * from (
     select PKID, PrjNum, BuilderLicenceNum, CorpName, CorpCode, SafetyCerID, UserName, IDCardTypeNum, IDCard, UserPhone, CertID, UserType, UpdateFlag, sbdqbm,
	 convert(varchar(30),GETDATE(),120) UpdateDate from TBProjectBuilderUserInfo
) as aaa where PKID not in (
	select  PKID from SaveToStLog where TableName='TBProjectBuilderUserInfo' and PKID=aaa.PKID and  OperateState=0
) and aaa.BuilderLicenceNum is not null ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }



        public DataTable GetTBData_TBProjectFinishManage()
        {
            //string sql = "select * from TBProjectFinishManage ";
            string sql = @"select * from (
select PKID, PrjFinishName, PrjFinishNum, PrjFinishInnerNum, PrjNum, BuilderLicenceNum, QCCorpName, QCCorpCode, FactCost, FactArea, FactSize, PrjStructureTypeNum, BDate, EDate, Mark, convert(varchar(30),CREATEDATE,120) CreateDate,convert(varchar(30),GETDATE(),120) UpdateDate, UpdateFlag, sbdqbm
from TBProjectFinishManage ) as aaa where  PKID not in (
	select  PKID from SaveToStLog where TableName='TBProjectFinishManage' and PKID=aaa.PKID and  OperateState=0
)  ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }


        public DataTable GetTBData_SaveToStLog(string tableName, string pkId)
        {
            string sql = String.Empty;
            pkId = pkId.Trim(',');
            if (pkId.IndexOf(',') >= 0)
            {
                sql = "select * from SaveToStLog where tableName='" + tableName + "' and PKID in (" + pkId + ") ";
                return DB.ExeSqlForDataTable(sql, null, "dt");
            }
            else
            {
                sql = "select * from SaveToStLog where tableName='" + tableName + "' and PKID ='" + pkId.Trim('\'') + "' ";
                return DB.ExeSqlForDataTable(sql, null, "dt");
            }



        }


        public DataTable Get_SaveToStLog2(string tableName, string pkId)
        {
            string sql = String.Empty;
            pkId = pkId.Trim(',');
            if (pkId.IndexOf(',') >= 0)
            {
                sql = "select * from SaveToStLog2 where tableName='" + tableName + "' and PKID in (" + pkId + ") ";
                return DB.ExeSqlForDataTable(sql, null, "dt");
            }
            else
            {
                sql = "select * from SaveToStLog2 where tableName='" + tableName + "' and PKID ='" + pkId.Trim('\'') + "' ";
                return DB.ExeSqlForDataTable(sql, null, "dt");
            }

        }


        public bool SaveTBData_SaveToStLog(DataTable dt)
        {
            string sql = "select * from SaveToStLog where 1=2 ";
            return DB.Update(sql, null, dt);
        }




        public DataTable GetTBData_aj_gcjbxx()
        {
            //string sql = "select * from aj_gcjbxx ";
            string sql = @"select * from (
select pKID, aqjdbm, gcmc, xmbm, sgzbbm, aqjdjgmc, sdcode, gcgkYszj, gcgkJzmj, gcgkJglx, gcgkCc, bjrq, gcgkKgrq, gcgkJhjgrq, zbdwDwdm, zbdwDwmc, zbdwAqxkzh, zbdwZcjzs, zbdwZcjzsdm, zbdwZcjzslxdh, zbdwAqy1, zbdwAqyzh1, zbdwAqy2, zbdwAqyzh2, zbdwAqy3, zbdwAqyzh3, jldwDwdm, jldwDwmc, jldwXmzj, jldwZjdm, jldwJlgcs1, jldwJlgcs2, jldwJlgcs3, bz, updateFlag, sbdqbm,convert(varchar(30),createDate,120) createDate
 from aj_gcjbxx
) as aaa where  PKID not in (
	select  PKID from SaveToStLog where TableName='aj_gcjbxx' and OperateState=0
) ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_zj_gcjbxx()
        {
            //string sql = "select * from zj_gcjbxx ";
            string sql = @"select * from (
    select  pKID, zljdbm, gcmc, prjNum, sgzbbm, zljdjgmc, zjzbm, gczj, jzmj, dlcd, jglx, cc, jzgm, sbrq, kgrq, jhjgrq, xxjd, bz,convert(varchar(30),CREATEDATE,120) createDate, updateFlag, sbdqbm from dbo.zj_gcjbxx
) as aaa where PKID not in (
	select  PKID from SaveToStLog where TableName='zj_gcjbxx' and  OperateState=0 )  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_zj_gcjbxx_zrdw()
        {
            //string sql = "select * from zj_gcjbxx ";
            string sql = @"select * from (
	select pKID, zljdbm, sbdqbm, dwlx, xh, dwmc, dwdm, xmfzrxm, xmfzrdm, xmfzr_lxdh, jsfzr, jsfzr_lxdh, zly, zly_lxdh, qyy, qyy_lxdh
    from zj_gcjbxx_zrdw
) as aaa where  xh in (1,2,3,4,5) and pKID+'-'+convert(varchar(10),xh) not in (
	select  PKID from SaveToStLog where TableName='zj_gcjbxx_zrdw' and OperateState=0) ";

      
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取一号通系统建设单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_DG_QY_JBXX()
        {
            string sql = @" select * from DG_QY_JBXX where zt='已审核' and convert(varchar(10),shenhe_time,120)=convert(varchar(10),GETDATE(),120) ";
            
            return DB_db_adminexam.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取勘察设计系统里的勘察设计企业人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_RY_JBXX_Tab()
        {
            string sql = @"select * from RY_JBXX_Tab where  IsValid=1 and convert(varchar(10),shtgsj,120)=convert(varchar(10),GETDATE(),120)
 and DWRowGuid in (select RowGuid from (
select RowGuid,Is_KCSJ,(select count(*) from Enterprise_ZZ_Tab_ZS where IsValid=1 and  ZZName like '勘察-%' and DWRowGuid=a.RowGuid) as 'IsKc'
,(select count(*) from Enterprise_ZZ_Tab_ZS where IsValid=1 and  ZZName like '设计-%' and DWRowGuid=a.RowGuid) as 'IsSj'
 from Enterprise_Tab  a where a.IsValid=1 and GSZCSZD_XZQHDM is not null and LEN(GSZCSZD_XZQHDM)>1 and SUBSTRING(GSZCSZD_XZQHDM,1,2)<>'32' 
and convert(varchar(10),a.UpdateTime ,120)=convert(varchar(10),GETDATE(),120)
 ) aaa where IsKc>0 or IsSj >0 )";
        
            return DB_Epoint_WXkcsj.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取勘察设计系统里的勘察设计企业信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_Tab()
        {

            string sql = @"select * from (
select *,(select RowGuid from RY_JBXX_Tab where IsValid=1 and ZJNO=a.FRDB_ZJNO  ) as 'fddbr_ryid'
,(select count(*) from Enterprise_ZZ_Tab_ZS where IsValid=1 and  ZZName like '勘察-%' and DWRowGuid=a.RowGuid) as 'IsKc'
,(select count(*) from Enterprise_ZZ_Tab_ZS where IsValid=1 and  ZZName like '设计-%' and DWRowGuid=a.RowGuid) as 'IsSj'
 from Enterprise_Tab  a where a.IsValid=1 and GSZCSZD_XZQHDM is not null and LEN(GSZCSZD_XZQHDM)>1 and SUBSTRING(GSZCSZD_XZQHDM,1,2)<>'32' 
and convert(varchar(10),a.UpdateTime ,120)=convert(varchar(10),GETDATE() ,120)
 ) aaa where IsKc>0 or IsSj >0 and ZZJGDM is not null  ";


            return DB_Epoint_WXkcsj.ExeSqlForDataTable(sql, null, "dt");
        }



        /// <summary>
        /// 获取资勘察设计企业质信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_ZZ_Tab()
        {
            string sql = @" select * from Enterprise_ZZ_Tab_ZS where IsValid=1 ";
            return DB_Epoint_WXkcsj.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取资勘察设计企业拥有资质的企业ID
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_ZZ_Tab_QYID()
        {
            string sql = @" select distinct DWRowGuid from Enterprise_ZZ_Tab_ZS where IsValid=1 ";
            return DB_Epoint_WXkcsj.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取企业资质信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_ZZ_Tab(string qyID)
        {
            string sql = @" select * from Enterprise_ZZ_Tab_ZS where IsValid=1 and (ZZName like '勘察-%' or ZZName like '设计-%') and DWRowGuid=@qyID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            return DB_Epoint_WXkcsj.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 获取安监站前置机里的施工单位信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Contractors()
        {
            string sql = @"   select * from Contractors where Status=1 and CreditStatus=1 and FullNOC is not null and RegionName='省外企业' and convert(varchar(10),Modified,120)>=convert(varchar(10),GETDATE()-10,120) ";

            //string sql = @"   select * from Contractors where Status=1 and CreditStatus=1 and FullNOC is not null  and convert(varchar(10),Modified,120)>='2015-09-01' ";
            //string sql = @"     select * from Contractors where Status=1 and CreditStatus=1 and FullNOC is not null and CONVERT(varchar(10),Modified,120)=>=@date  order by Modified desc ";
            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取安监站前置机里的建造师信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Constructors()
        {
            string sql = @"select a.*,b.GUID, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from Constructors a 
  inner join  Contractors b on a.ITCode=b.ITCode
  inner join IDCards c on a.id=c.ID 
  where a.grade in ('一级','二级','小型') AND a.RegStatus = 2 and EXISTINIDCARDS=2
  AND a.ID is not null and c.PhotoBytes IS NOT NULL and c.Status=1 and b.FullNoc IS NOT NULL and  b.Status=1 and b.CreditStatus=1 
and  b.Status=1 and b.CreditStatus=1 and b.FullNOC is not null and b.RegionName='省外企业' and convert(varchar(10),a.Modified,120)>=convert(varchar(10),GETDATE()-10,120)  ";

            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取安监站前置机里的安全人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SafetyOfficers()
        {
            string sql = @"select a.*,b.GUID, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from SafetyOfficers a 
  inner join  Contractors b on a.ITCode=b.ITCode 
  inner join IDCards c on a.id=c.ID 
  where a.RegStatus = 2 and EXISTINIDCARDS=2
  AND a.ID is not null and c.PhotoBytes IS NOT NULL and c.Status=1 and b.FullNoc IS NOT NULL and  a.CateGory is not null  and convert(varchar(10),a.Modified,120)=convert(varchar(10),GETDATE(),120) and  b.Status=1 and b.CreditStatus=1 and b.FullNOC is not null and b.RegionName='省外企业'  ";

            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取安监站前置机里的技经人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Employees()
        {
            string sql = @"select a.*,b.GUID, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from Employees a 
  inner join  Contractors b on a.ITCode=b.ITCode 
  inner join IDCards c on a.id=c.ID 
  where a.RegStatus = 2 and EXISTINIDCARDS=2
  AND a.ID is not null and c.PhotoBytes IS NOT NULL and c.Status=1 and b.FullNoc IS NOT NULL  and convert(varchar(10),a.Modified,120)=convert(varchar(10),GETDATE(),120)
and  b.Status=1 and b.CreditStatus=1 and b.FullNOC is not null and b.RegionName='省外企业' ";

            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取安监站前置机里的特种作业人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SpecialOperators()
        {
            string sql = @"select a.*,b.GUID, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from SpecialOperators a 
  inner join  Contractors b on a.ITCode=b.ITCode 
  inner join IDCards c on a.id=c.ID 
  where a.RegStatus = 2 and EXISTINIDCARDS=2
  AND a.ID is not null and c.PhotoBytes IS NOT NULL and c.Status=1 and b.FullNoc IS NOT NULL  and convert(varchar(10),a.Modified,120)=convert(varchar(10),GETDATE(),120)
 and  b.Status=1 and b.CreditStatus=1 and b.FullNOC is not null and b.RegionName='省外企业' ";

            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(注册执业人员)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CertifPerson()
        {
            string sql = @"select Id as PKID ,ryxx.xm as RYXM,ryxx.zjhm as SFZH,zs.zsbh as ZSBM,zs.ryzyzglx as ZYZGLX, ryxx.qymc as DWMC  from ( 
select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
 ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState

 from uepp_ryjbxx a left join (select distinct ryid,qyid from uepp_qyry 
 where ryzyzglxid in ( 1,2,21,41,51,61 )  ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid where  1=1 ) ryxx  
 left join 
 
  ( select distinct a.ryzyzglx,a.zsbh,a.ryid,b.Id from  uepp_ryzs a  
inner join uepp_ryzyzg b on a.ryid=b.ryid and  a.ryzyzglxID=b.ryzyzglxID where a.ryzslxID in (11,12,21,22,91,131,151,511,161) and b.ryzyzglxid in ( 1,2,21,41,51,61) )

zs on ryxx.ryid=zs.ryid  	where ryzyzglx is not null";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(竣工验收备案)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CompleteAcc()
        {
            string sql = @"select  PKID,PrjFinishName as XMMC,PrjFinishNum as JGBABH,PrjNum as QSTYXMBH,BuilderLicenceInnerNum as XKZNBBH,FactCost as SJZJ,EDate as SJJGYSRQ      from (
SELECT 
a.PKID,
a.PrjFinishNum,
a.PrjFinishName,
a.QCCorpName,
convert(char(10),a.BDate,20) as BDate,
convert(char(10),a.EDate,20) as EDate,
a.PrjStructureTypeNum,a.BuilderLicenceNum,a.FactCost,a.FactSize,FactArea,
(select CodeInfo from tbPrjStructureTypeDic where Code=a.PrjStructureTypeNum ) as PrjStructureType,
a.PrjNum,b.PrjName,b.PKID as LxPKID,c.BuilderLicenceInnerNum
FROM TBProjectFinishManage a  
left join TBProjectInfo b on a.PrjNum=b.PrjNum 
left join TBBuilderLicenceManage c on a.BuilderLicenceNum=c.BuilderLicenceNum
where a.UpdateFlag='U'
) as aaa WHERE 1=1";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(建设工程施工许可证)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Constrlicence()
        {
            string sql = @"select PKID,PrjNum as QSTYXMBH,BuilderLicenceInnerNum as XKZNBBH,BuilderLicenceName as GCXMMC,IssueCertDate as FZRQ,PrjSize as JSGM,
                        ConsCorpName as SGDWMC,SuperCorpName as JLDWMC,EconCorpName as KCDWMC,DesignCorpName as SJDWMC
                        from (
                         SELECT 
                        a.PKID,
                        a.BuilderLicenceName,a.BuilderLicenceInnerNum,a.PrjSize,
                        a.BuilderLicenceNum,a.CensorNum,
                        convert(char(10),a.IssueCertDate,20) as IssueCertDate,
                        a.EconCorpName,a.DesignCorpName,a.ConsCorpName,a.SuperCorpName,
                        a.PrjNum, b.PrjName,b.PKID as LxPKID,c.qyID as kcqyID,d.qyID as sjqyID,e.qyID as sgqyID,b.CountyNum
                        FROM TBBuilderLicenceManage a 
                        left join TBProjectInfo b on a.PrjNum=b.PrjNum 
						left join UEPP_Qyjbxx c on c.zzjgdm=a.EconCorpCode  
						left join UEPP_Qyjbxx d on d.zzjgdm=a.DesignCorpCode  
						left join UEPP_Qyjbxx e on e.zzjgdm=a.ConsCorpCode 
                        where a.UpdateFlag='U'
                        ) as aa WHERE 1=1 ";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(工程项目基本信息)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ConstrProjectInfo()
        {
            string sql = @"select PKID,PrjName as XMMC,PrjNum as QSTYXMBH,PrjType as XMFL,BuildCorpName as JSDWMC,BuildCorpCode as ZZJGDM,
                          '江苏省' as XMSZS,'无锡市' as SZDS,(select CodeInfo from tbXzqdmDic where parentCode='320200' and Code=aaa.CountyNum)   as SZQX,AllInvest as ZTZ,
                          AllArea as ZMJ,PrjSize as JSGM,(select CodeInfo from  tbPrjPropertyDic where Code=aaa.PrjPropertyNum ) as JSXZ,(select CodeInfo from tbPrjFunctionDic where Code=aaa.PrjFunctionNum ) as GCYT,
                          BDate as SJKGRQ,EDate as SJJGRQ,PrjApprovalNum as LXPWH,BuldPlanNum as JSYDGHXKZH,ProjectPlanNum as JSGCGHXKZH

from (
SELECT 
a.PKID,
a.PrjNum,a.PrjInnerNum,
a.PrjName,
a.PrjApprovalNum,
a.BuildCorpName,a.BuildCorpCode,a.CountyNum,a.PrjPropertyNum,a.AllArea,a.PrjSize,
a.BuldPlanNum,a.ProjectPlanNum,a.AllInvest,a.PrjFunctionNum,
convert(varchar(10),a.BDate,20) as BDate,
convert(varchar(10),a.EDate,20) as EDate,convert(varchar(10),a.CreateDate,20) as CreateDate,a.PrjTypeNum,c.jsdwID,
(select codeinfo from tbPrjTypeDic where code=a.PrjTypeNum) as PrjType

FROM TBProjectInfo a left join SaveToStLog b on b.TableName='TBProjectinfo' and a.PKID=b.PKID 
left join UEPP_Jsdw c on a.BuildCorpCode=c.zzjgdm 
where a.UpdateFlag='U' ) as aaa WHERE 1=1 ";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(工程招标中标信息)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_EngBidInfo()
        {
            string sql = @"SELECT PKID,TenderName as ZBXMMC,PrjName as XMMC,PrjNum as QSTYXMBH,TenderCorpName as JSDWMC,TenderCorpCode as ZZJGDM,TenderClass as ZBLX,
                        TenderType as ZBFS,PrjSize as JSGM,TenderMoney as ZTZ,Area as ZMJ,TenderResultDate as SJKGRQ   
                        FROM (
                        select  distinct
                        a.PKID,a.TenderCorpCode,a.PrjSize,a.TenderMoney,a.Area,
                        a.TenderName,
                        a.TenderNum,
                        a.TenderClassNum,
                        a.TenderTypeNum,
                        a.AgencyCorpName,a.TenderCorpName,
                        convert(char(10),a.TenderResultDate,20) as TenderResultDate,
                        b1.CodeInfo as TenderClass,
                        b2.CodeInfo as TenderType,
                        c.PrjName,
                        c.PrjNum,c.PKID as LxPKID,d.qyID,c.CountyNum
,(select count(*) from SaveToStLog where TableName='TBTenderInfo' and  PKID=a.pKID and OperateState=0) OperateState
                        from TBTenderInfo as a
                        left join tbTenderClassDic as b1 on a.TenderClassNum = b1.Code 
                        left join tbTenderTypeDic as b2 on a.TenderTypeNum = b2.Code 
                        left join TBProjectInfo c on a.PrjNum=c.PrjNum 
                        left join UEPP_Qyjbxx  d on a.TenderCorpCode=d.zzjgdm and a.TenderClassNum in ('001,002','003','006')
                        where  a.UpdateFlag='U'
                        ) AS TEMP WHERE 1=1 ";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(专业岗位管理人员)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ProFadmper()
        {
            string sql = @" select Id as PKID,xm as RYXM,zjhm as SFZH,ryzyzglx as ZYZGLX,qymc as DWMC   from ( 
select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
 ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState
 from uepp_ryjbxx a left join (select distinct ryid,qyid from uepp_qyry 
 where ryzyzglxid in ( 7,8,9,11,12,14,15,16,17,18,22,42 )  ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid where  1=1 ) ryxx  left join 
( select distinct a.ryzyzglx,a.zsbh,a.ryid ,b.Id
 from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where   
   a.ryzslxID in (82,83,84,85,89,86,87,88,101,141,51,61,71) and
    b.ryzyzglxid in (7,8,9,11,12,14,15,16,17,18,22,42)) zs on ryxx.ryid=zs.ryid 	where ryzyzglx is not null";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取无锡数据中心需要上传至市一中心四平台的数据(安全生产管理人员)---是讲uepp_ryzyzg中的ID作为PKID
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SafeProdadmper()
        {
            string sql = @"  select   Id as PKID, xm as RYXM,zjhm as SFZH,ryzyzglx as ZYZGLX,zsbh as ZSBM,qymc as DWMC   
                            from ( 
select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
 ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState
 from uepp_ryjbxx a left join (select distinct ryid,qyid from uepp_qyry 
 where ryzyzglxid in ( 4,5,6 )  ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid where  1=1 ) ryxx  left join 
 (
 select distinct a.ryzyzglx,a.zsbh,a.ryid,b.Id from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where  a.ryzslxID in (41,42,43) and b.ryzyzglxid in (4,5,6)) zs 
 on ryxx.ryid=zs.ryid 	where ryzyzglx is not null";

            return this.DB.ExeSqlForDataTable(sql, null, "dt");
        }

        #endregion

        #region  读取单条


        public DataTable GetTBData_TBContractRecordManage(string PKID)
        {
            string sql = @"select * from  TBContractRecordManage where PKID=@PKID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", PKID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable GetTBData_TBProjectFinishManage(string PKID)
        {
            string sql = @"select * from  TBProjectFinishManage where PKID=@PKID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", PKID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable GetTBData_TBBuilderLicenceManage(string builderLicenceInnerNum, string xuKeZhengNum)
        {

            string sql = @"select * from TBBuilderLicenceManage  where (BuilderLicenceInnerNum=@BuilderLicenceInnerNum or BuilderLicenceInnerNum=@xuKeZhengNum) ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@BuilderLicenceInnerNum", builderLicenceInnerNum);
            sp.Add("@xuKeZhengNum", xuKeZhengNum);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 获取企业从事业务类型
        /// </summary>
        /// <returns></returns>
        public List<string> Get_uepp_Qycsywlx()
        {
            string sql = @"select * from UEPP_Code where CodeType='企业从事业务类型' ";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "dt_qycsywlx");
            List<string> list = new List<string>();

            foreach (DataRow row_csywlx in dt.Rows)
            {
                list.Add(row_csywlx["CodeInfo"].ToString());
            }
            return list;
        }
        /// <summary>
        /// 获取人员执业资格类型
        /// </summary>
        /// <returns></returns>
        public List<string> Get_uepp_Ryzyzglx()
        {
            string sql = @"select * from UEPP_Code where CodeType='人员执业资格类型' ";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "dt_ryzyzglx");
            List<string> list = new List<string>();

            foreach (DataRow row_ryzyzglx in dt.Rows)
            {
                list.Add(row_ryzyzglx["CodeInfo"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取建设单位信息
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw(string jsdwID)
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

        ///// <summary>
        ///// 获取一号通的建设单位信息
        ///// </summary>
        ///// <param name="jsdwID"></param>
        ///// <returns></returns>
        //public DataTable Get_uepp_jsdw_FromYht(string jsdwID)
        //{
        //    SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
        //    string sql = @"select top 1  * from uepp_jsdw  where jsdwID=@jsdwID order by len(jsdwID) desc  ";
        //    sp.Add("@jsdwID", jsdwID);
        //    return DB.ExeSqlForDataTable(sql, sp, "uepp_jsdw");
        //}
       
        /// <summary>
        /// 根据单位从事业务类型获取施工单位信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Sgdw(string dwfl)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            string sqlPara = AntiSqlInjection.ParameterizeInClause(dwfl, "@para", ref sp);

            string sql = @" select qyID, qymc, zzjgdm, yyzzzch, khyh, yhzh, sfsyq, gcjsry_zs, gcjsry_gjzcrs, gcjsry_zjzcrs, sylxID, sylx, ProvinceID, Province, CityID, City, CountyID, County, zcdd, jjxzID, jjxz, zczb, zyfw, jyfw, clrq, qyjj, xxdd, yzbm, cz, email, webAddress, lxr, lxdh, fddbr_ryid, fddbr, qyfzr_ryid, qyfzr, cwfzr_ryid, cwfzr, jsfzr_ryid, jsfzr, aqfzr_ryid, aqfzr, xgr, xgrqsj, tag
 from UEPP_Qyjbxx where DataState=0 and qyID in (
  select qyID from UEPP_Qycsyw where csywlxID in (1,3,2,13,14) and  DataState=0  ";
            sql += string.IsNullOrEmpty(sqlPara) ? ")" : " and csywlx in (" + sqlPara + "))";
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
        public DataTable Get_uepp_KcQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID=5  and DataState=0 and qyID=@qyID ";

            return DB.ExeSqlForDataTable(sql, sp, "uepp_sgdw");

        }

        public DataTable Get_uepp_SjQyzzByQyID(string qyID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            string sql = @"select ID, qyID, csywlxID, csywlx, zslxID, zslx, zzbz, zzxlID, zzxl, zzhyID, zzhy, zzlbID, zzlb, zzdjID, zzdj, cjywfw, bz, tag, xgr, xgrqsj from UEPP_Qyzzmx where csywlxID in (6,2)  and DataState=0 and qyID=@qyID ";

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
        public DataTable Get_Ryzy_Aqscglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select ID, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, zzbz, zyzglbID, zyzglb, zyzgdjID, zyzgdj, gzfw, bz, tag, xgr, xgrqsj, DataState
 from UEPP_Ryzymx where DataState=0 and  ryzyzglxID in (4,5,6) and ryID=@ryID ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
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
        public DataTable Get_Ryzs_Aqscglry(string ryID)
        {
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();

            string sql = @"select zsjlId, ryID, ryzyzglxID, ryzyzglx, ryzslxID, ryzslx, sfzzz, zsbh, zsyxqrq, zsyxzrq, fzdw, fzrq, bz, tag, xgr, xgrqsj, DataState 
from UEPP_Ryzs where DataState=0 and ryID = @ryID and ryzyzglxID in (4,5,6) ";
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt_Ryxx_Zygwglry");
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
            list.GetSearchClause(sp,ref sql);

            return DB.ExeSqlForDataTable(sql, sp, "dt_QyXykp");
        }

        /// <summary>
        /// 获取施工监理合同备案信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable Get_TBContractRecordManage_SgJlHtba()
        {

            string sql = @"select * from (
select PKID, RecordName, RecordNum, RecordInnerNum, PrjNum, ContractNum, ContractTypeNum, ContractMoney, PrjSize, ContractDate, PropietorCorpName, PropietorCorpCode, ContractorCorpName, ContractorCorpCode, UnionCorpName, UnionCorpCode,SUBSTRING(convert(varchar(30),CREATEDATE,120),1,10) CreateDate, UpdateFlag, PrjHead, PrjHeadPhone, IDCard, sbdqbm from TBContractRecordManage where ContractTypeNum in (301,302,304,400)
) aaa where 1=1 ";
            //UpdateFlag='U' 
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }


        /// <summary>
        /// 获取所有从信用平台传过来的建设单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_FromSxypt()
        {
            string sql = @"select * from uepp_jsdw where DataState<>-1 and tag like '%省信用平台%' ";

            return DB.ExeSqlForDataTable(sql, null, "uepp_jsdw");
        }
        /// <summary>
        /// 获取建设单位主要负责人
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_zyfzr(string jsdwID)
        {
            string sql = @"select * from  TCorpChiefPerson  where CorpCode=@jsdwID";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "TCorpChiefPerson");
        }
        /// <summary>
        /// 建设单位资质明细
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_zzmxxx_jsdw(string jsdwID)
        {
            string sql = @"select * from  UEPP_Qyzzmx  where DataState=0 and qyID=@jsdwID ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 建设单位资质明细
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_zzmxxx_qyxx(string qyID, string csywlxid)
        {
            string sql = @"select * from  UEPP_Qyzzmx  where DataState=0 and qyID=@qyID and csywlxid=@csywlxid ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@csywlxid", csywlxid);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取建设单位资质证书信息
        /// </summary>
        /// <param name="jsdwID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdw_zzzsxx(string jsdwID)
        {
            string sql = @" select * from  UEPP_Qyzs where  csywlxID=11 and DataState=0 and qyID=@jsdwID ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable Get_uepp_zzzsxx_qyxx(string qyID)
        {
            string sql = @" select * from  UEPP_Qyzs where   DataState=0 and qyID=@qyID ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        ///// <summary>
        ///// 获取无锡建筑工程基础资源管理系统某企业的人员基本信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable Get_uepp_Ryjbxx(string rowGuid)
        //{
        //    string sql = @" select * from  UEPP_Ryjbxx where ryID=:ryID  ";
        //    SqlParameterCollection sp = DB_oracl.CreateSqlParameterCollection();
        //    sp.Add(":ryID", rowGuid);
        //    return DB_oracl.ExeSqlForDataTable(sql, sp, "dt");
        //}


        /// <summary>
        /// 获取无锡建筑工程基础资源管理系统某企业的企业基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyjbxx(string qyID)
        {
            //string sql = @" select * from UEPP_Qyjbxx where QyID=@qyID ";
            string sql = "";
            if (qyID.Length == 9)
            {
                sql = @"select top 1 * from UEPP_Qyjbxx  where QyID=@qyID or len(QyID)=10 and substring(QyID,1,8)+substring(QyID,10,1)=@qyID or len(QyID)=18 and (substring(QyID,9,9))=@qyID order by len(QyID) desc ";
            }
            else
                if (qyID.Length == 10)
                {
                    sql = @"select top 1 * from UEPP_Qyjbxx  where QyID=@qyID or len(QyID)=9 and substring(QyID,1,8)+'-'+substring(QyID,9,1)=@qyID or len(QyID)=18 and substring(QyID,9,8)+'-'+substring(QyID,17,1) =@qyID  order by len(QyID) desc  ";
                }
                else
                {
                    sql = @"select top 1  * from UEPP_Qyjbxx  where QyID=@qyID  order by len(QyID) desc  ";
                }
          

            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable Get_uepp_Ryjbxx(string ryID)
        {
            string sql = @" select * from UEPP_Ryjbxx where ryID=@ryID   ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 获取无锡建筑工程基础资源管理系统施工企业的企业从事业务类型
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qycsyw_sgqy(string qyID)
        {
            string sql = @" select * from UEPP_Qycsyw where qyID=@qyID and csywlxid in ('1')";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable Get_uepp_Qycsyw_sjsgyth(string qyID, string csywlxid)
        {
            string sql = @" select * from UEPP_Qycsyw where qyID=@qyID and csywlxid =@csywlxid ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@csywlxid", csywlxid);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取无锡建筑工程基础资源管理系统建设单位的企业从事业务类型
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qycsyw_jsdw(string qyID)
        {
            string sql = @" select * from UEPP_Qycsyw where qyID=@qyID and csywlxid in (11,12)   ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable Get_uepp_Ryzyzg_zcjzs(string ryID)
        {
            string sql = @" select * from UEPP_Ryzyzg where ryID=@ryID and  ryzyzglxID in ('1','2') ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Ryzyzg(string ryID)
        {
            string sql = @" select * from UEPP_Ryzyzg where ryID=@ryID  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable Get_uepp_Ryzyzg_aqry(string ryID)
        {
            string sql = @" select * from UEPP_Ryzyzg where ryID=@ryID and  ryzyzglxID in ('4','5','6') ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Ryzyzg_jjry(string ryID)
        {
            string sql = @" select * from UEPP_Ryzyzg where ryID=@ryID and  ryzyzglxID ='20' ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Ryzyzg_tzzy(string ryID)
        {
            string sql = @" select * from UEPP_Ryzyzg where ryID=@ryID and  ryzyzglxID ='17' ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable Get_uepp_Ryzs(string ryID)
        {
            string sql = @" select * from UEPP_Ryzs where ryID=@ryID ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        ///获取安全人员的安全证号
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzs_aqry(string ryID)
        {
            string sql = @" select * from UEPP_Ryzs where ryID=@ryID and ryzyzglxID in (4,5,6) and ryzslxID in (41,42,43) ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        ///获取特种作业人员的上岗证证号
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzs_tzzy(string ryID)
        {
            string sql = @" select * from UEPP_Ryzs where ryID=@ryID and ryzyzglxID ='17' and ryzslxID ='87' ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取注册建造师与小型项目管理师B类安全生产考核证
        /// </summary>
        /// <param name="ryID"></param>
        /// <param name="ryzyzglxID"></param>
        /// <param name="ryzslxID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzs_zcjzs_blaq(string ryID)
        {
            string sql = @" select * from UEPP_Ryzs where ryID=@ryID and (ryzyzglxID='1' and ryzslxID =12 or ryzyzglxID='2' and ryzslxID ='22')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 获取注册建造师与小型项目管理师资格证
        /// </summary>
        /// <param name="ryID"></param>
        /// <param name="ryzslxID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzs_zcjzs_zgz(string ryID)
        {
            string sql = @" select * from UEPP_Ryzs where ryID=@ryID and ryzyzglxID in ('1','2') and ryzslxID in (11,21) ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        ///  A类安全员的专业明细
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzymx_AqyA(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID='4'  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }
        /// <summary>
        /// B类安全员的专业明细
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzymx_AqyB(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID='5'  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// C类安全员的专业明细
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzymx_AqyC(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID='6'  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Ryzymx(string ryID)
        {
            //为避免重复人员数据
            //string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('1','2')  ";
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }
        /// <summary>
        /// 质量员专业
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Ryzym_Zly(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('8')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }
        public DataTable Get_uepp_Ryzymx_Sgy(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('7')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }

        public DataTable Get_uepp_Ryzymx_zjsXxxmgls(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('1','2')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }
        public DataTable Get_uepp_Ryzymx_zcjlgcs(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('21')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }

        //注册机构师专业明细
        public DataTable Get_uepp_Ryzymx_Zcjgs(string ryID)
        {
            string sql = @" select * from UEPP_Ryzymx where ryID=@ryID and ryzyzglxID in ('61')  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable Get_uepp_qyryzyzg_zcjzs(string qyID, string ryID)
        {
            string sql = @" select * from UEPP_QyRy where qyID=@qyID and ryID=@ryID and ryzyzglxID in ('1','2') ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_qyryzyzg_aqry(string qyID, string ryID)
        {
            string sql = @" select * from UEPP_QyRy where qyID=@qyID and ryID=@ryID and ryzyzglxID in ('4','5','6') ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_qyryzyzg_jjry(string qyID, string ryID)
        {
            string sql = @" select * from UEPP_QyRy where qyID=@qyID and ryID=@ryID and ryzyzglxID ='20' ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_qyryzyzg_tzzy(string qyID, string ryID)
        {
            string sql = @" select * from UEPP_QyRy where qyID=@qyID and ryID=@ryID and ryzyzglxID ='17' ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Code(string codeType, string parentCode, string codeInfo)
        {
            string sql = @" select  *  from  uepp_code where codetype=@codeType  and ParentCode=@parentCode
and (CodeInfo=@codeInfo or CodeInfo=@codeInfo2 or CodeInfo=@codeInfo3) ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@codeType", codeType);
            sp.Add("@parentCode", parentCode);
            sp.Add("@codeInfo3", codeInfo);
            if (codeInfo.IndexOf("工程") >= 0)
            {
                sp.Add("@codeInfo", codeInfo.Replace("工程", ""));
                sp.Add("@codeInfo2", codeInfo);
            }
            else
            {
                sp.Add("@codeInfo", codeInfo);
                sp.Add("@codeInfo2", codeInfo + "工程");
            }

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_Code_zzdj(string codeType, string parentCode, string codeInfo)
        {
            string sql = @" select  *  from  uepp_code where codetype=@codeType  and ParentCode=@parentCode
and CodeInfo=@codeInfo ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@codeType", codeType);
            sp.Add("@parentCode", parentCode);
            sp.Add("@codeInfo", codeInfo);

            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public string Get_uepp_Code_NewCode(string codeType)
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,code)),0)+1 from  uepp_code where codetype=@codeType  ";
            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@codeType", codeType);

            return DB.ExeSqlForString(sql, sp);
        }


        public string Get_uepp_qyxxmx_NewID()
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,ID)),0)+1 from  UEPP_Qyzzmx ";
            return DB.ExeSqlForString(sql, null);
        }
        /// <summary>
        /// 企业资质证书新纪录ID
        /// </summary>
        /// <returns></returns>
        public string Get_uepp_qyQyzs_NewID()
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,zsjlId)),0)+1 from  UEPP_Qyzs ";
            return DB.ExeSqlForString(sql, null);
        }

        public string Get_uepp_qyzs_NewID()
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,zsjlId)),0)+1 from  UEPP_Qyzs ";
            return DB.ExeSqlForString(sql, null);
        }

        public string Get_uepp_Qycsyw_NewID()
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,ID)),0)+1 from  UEPP_Qycsyw ";
            return DB.ExeSqlForString(sql, null);
        }

        public string Get_uepp_RyzsNewID()
        {
            string sql = @" select ISNULL(MAX(CONVERT(int ,zsjlId)),0)+1 from  UEPP_Ryzs  ";
            return DB.ExeSqlForString(sql, null);
        }

        public string Get_DataJkLogNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,ID)),0)+1 from DataJkLog ";
            return DB.ExeSqlForString(sql, null);
        }

        public string Get_DataJkDataDetailNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,ID)),0)+1 from DataJkDataDetail ";
            return DB.ExeSqlForString(sql, null);
        }


        public string Get_apiCbNewID()
        {
            string sql = "select ISNULL(MAX(CONVERT(int ,apiCbID)),0)+1 from API_cb ";
            return DB.ExeSqlForString(sql, null);
        }

        /// <summary>
        /// 获取注册建造师信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyry_zcjzs()
        {
            //            string sql = @" select  a.*,c.qyID as zydwID,c.qymc as zydw ,c.zzjgdm,d.zsyxqrq,d.zsyxzrq,d.zsbh,d.fzdw,d.fzrq,b.ryzyzglxid
            //from uepp_ryjbxx a 
            //inner join (select distinct ryid,qyid,ryzyzglxid from uepp_qyry  where ryzyzglxid in (1,2) ) b on a.ryid=b.ryid 
            //inner join uepp_qyjbxx c on b.qyid=c.qyid and c.County='省外企业'
            //inner join uepp_Ryzs d on a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)
            //where  a.ryid  not in (
            // select  PKID from SaveToStLog2 where TableName='uepp_ryjbxx' and SbToStState=0
            //) ";
            string sql = @"
select  a.*,c.qyID as zydwID,c.qymc as zydw ,c.zzjgdm,b.ryzyzglxid,
(select zsyxqrq from  uepp_Ryzs d where a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)) as zsyxqrq
,(select zsyxzrq from  uepp_Ryzs d where a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)) as zsyxzrq
,(select zsbh from  uepp_Ryzs d where a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)) as zsbh
,(select fzdw from  uepp_Ryzs d where a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)) as fzdw
,(select fzrq from  uepp_Ryzs d where a.ryID=d.ryID and d.ryzyzglxID in (1,2) and d.ryzslxID in (11,21)) as fzrq
from uepp_ryjbxx a 
inner join (select distinct ryid,qyid,ryzyzglxid from uepp_qyry  where ryzyzglxid in (1,2) ) b on a.ryid=b.ryid 
inner join uepp_qyjbxx c on b.qyid=c.qyid and c.County='省外企业'
where  a.tag='无锡市建设工程安全监督站'and a.datastate<>-1 and a.ryid  not in (
 select  PKID from SaveToStLog2 where TableName='uepp_ryjbxx' and SbToStState=0
) ";


            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            return DB.ExeSqlForDataTable(sql, sp, "dt");

        }

        /// <summary>
        /// 获取注册建造师信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyry_zcjzs2()
        {
            string sql = "select  a.*, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from Constructors a inner join  Contractors b on a.ITCode=b.ITCode inner join IDCards c on a.id=c.ID where a.grade in ('一级','二级')  AND a.RegStatus = 2  and EXISTINIDCARDS =2  AND a.ID is not null and  a.ID <>''  and c.PhotoBytes IS NOT NULL  and b.FullNoc IS NOT NULL ";

            return this.DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取小型项目管理师信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyry_xxxmgls2()
        {
            string sql = "select  a.*, b.fullname,b.FullNoc,c.PhotoBytes,c.birthdate,c.Photo,c.Address,c.Agency,c.Nation,c.ValidFrom as 'IDValidFrom',c.ValidTo as 'IDValidTo'  from Constructors a inner join  Contractors b on a.ITCode=b.ITCode  inner join IDCards c on a.id=c.ID  where a.grade='小型' and a.RegStatus = 2  and EXISTINIDCARDS =2  AND a.ID is not null and  a.ID <>'' and c.PhotoBytes IS NOT NULL  and b.FullNoc IS NOT NULL  ";

            return DB_WXJGC_SSIP.ExeSqlForDataTable(sql, null, "dt");
        }


        public DataTable Get_uepp_Qyry(string ryID, string qyID, string ryzyzglxID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            sp.Add("@qyID", qyID);
            sp.Add("@ryzyzglxID", ryzyzglxID);
            string sql = @" select * from  UEPP_QyRy where ryID=@ryID and qyID=@qyID and ryzyzglxID=@ryzyzglxID ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_uepp_QyryNoRyzyzg(string ryID, string qyID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            sp.Add("@qyID", qyID);
            string sql = @" select * from  UEPP_QyRy where ryID=@ryID and qyID=@qyID ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        /// <summary>
        /// 获取施工单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyjbxx_Sgqy()
        {
            string sql = @" select  a.* from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid=1)  ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取待上报的施工单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyjbxx_Sgqy_ToSb()
        {
            string sql = @"select  a.* from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid=1) 
and a.qyID not in (select PKID from SaveToStLog2 where TableName='uepp_qyjbxx' and SbToStState=0)   
 and a.County='省外企业' and tag='无锡市建设工程安全监督站' and a.DataState<>-1 ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取施工单位资质信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyzz()
        {
            string sql = @"select a.*,b.zzjgdm,c.zsbh,c.zsyxqrq,c.zsyxzrq,c.fzrq,c.fzdw,c.sfzzz  from UEPP_Qyzzmx a 
 inner join  UEPP_Qyzs c on  a.qyID= c.qyID and c.csywlxid=1 and c.zslxID='1' and a.csywlxid=1 and a.zslxID='1'
 inner join uepp_qyjbxx b on a.qyid=b.qyid   
 where a.DataState=0 and b.County='省外企业' and b.tag='无锡市建设工程安全监督站' and b.qyid in (select qyid from uepp_qycsyw where csywlxid ='1') ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }



        ///// <summary>
        ///// 获取施工单位信息
        ///// </summary>
        ///// <returns></returns>
        //public DataTable Get_uepp_Qyjbxx_Sgqy2()
        //{
        //   // string sql = @" select a.* from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid in(1,2,3))  ";
        //   // string sql = "select * from  \"Contractors\" a  where  a.IsRefuse <> 1 and FullNOC is not NULL and (length(FullNOC)=10 or length(FullNOC)=9) ";
        //    string sql = " select * from Contractors where Status=1 and CreditStatus=1 and FullNOC is not null ";
        //    return DB_oracl.ExeSqlForDataTable(sql, null, "dt");
        //}


        /// <summary>
        /// 获取已经保存的勘察设计企业信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyjbxx_kcsj()
        {
            string sql = @" select top 100 a.*,(select count(*)  from uepp_qycsyw where csywlxid ='5' and qyid=a.qyid ) as iskc
,(select count(*)  from uepp_qycsyw where csywlxid ='6' and qyid=a.qyid ) as issj from UEPP_Qyjbxx a 
where a.qyID in (select qyID from uepp_qycsyw where csywlxid in ('5','6')) ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable Get_UEPP_Qyzzmx_kcsj(string qyID)
        {
            string sql = @"  select * from UEPP_Qyzzmx where qyID=@qyID and csywlxid in ('5','6') ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 获取无锡建筑工程基础资源管理系统某企业的资质信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_UEPP_Qyzzmx_zhux(string qyID)
        {
            string sql = @" select * from UEPP_Qyzzmx where qyID=@qyID and csywlxid=1 and zzbz='主项' ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        public DataTable Get_UEPP_Qyzzmx_zx(string qyID)
        {
            string sql = @"  select * from UEPP_Qyzzmx where qyID=@qyID and csywlxid=1 and zzbz='增项'   ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable Get_UEPP_Qyzzzs(string qyID, string zslxID)
        {
            string sql = @"  select * from UEPP_Qyzs where qyID=@qyID and csywlxid in (1,2,3) and zslxID=@zslxID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@zslxID", zslxID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Get_UEPP_Qyzzzs_kcsj(string qyID, string csywlxid, string zslxID, string zsbh)
        {
            string sql = @"  select * from UEPP_Qyzs where qyID=@qyID and csywlxid=@csywlxid and zslxID=@zslxID and zsbh=@zsbh  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            sp.Add("@csywlxid", csywlxid);
            sp.Add("@zslxID", zslxID);
            sp.Add("@zsbh", zsbh);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_UEPP_Ryjbxx(string ryID)
        {
            string sql = @"  select * from UEPP_Ryjbxx where ryID=@ryID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        /// <summary>
        /// 根据建设单位十位组织机构代码获取对应的社会信用代码
        /// </summary>
        /// <param name="jsdwZzjgdm"></param>
        /// <returns></returns>
        public string Get_UEPP_Jsdw_Shxydm(string jsdwZzjgdm)
        {
            string sql = @" select top 1 jsdwID from UEPP_Jsdw where len(jsdwID)=18 and substring(jsdwID,9,8)+'-'+substring(jsdwID,17,1)=@jsdwZzjgdm  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@jsdwZzjgdm", jsdwZzjgdm);
            return this.DB.ExeSqlForString(sql, sp);
        }
        /// <summary>
        /// 根据企业十位组织机构代码获取对应的社会信用代码
        /// </summary>
        /// <param name="qyZzjgdm"></param>
        /// <returns></returns>
        public string Get_UEPP_Qyjbxx_Shxydm(string qyZzjgdm)
        {
            string sql = @" select top 1 qyID from UEPP_Qyjbxx where len(qyID)=18 and substring(qyID,9,8)+'-'+substring(qyID,17,1)=@qyZzjgdm  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyZzjgdm", qyZzjgdm);
            return this.DB.ExeSqlForString(sql, sp);
        }


        /// <summary>
        /// 根据apiFlow获取Api信息
        /// </summary>
        /// <param name="qyZzjgdm"></param>
        /// <returns></returns>
        public DataTable Get_API_zb_apiFlow(string apiFlow)
        {
            string sql = @" select apiControl from API_zb where apiFlow=@apiFlow		  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@apiFlow", apiFlow);
            return this.DB.ExeSqlForDataTable(sql, sp,"dt");
        }

       

        //public DataTable Get_UEPP_SaveToStLog(string PKID)
        //{
        //    string sql = @"select * from  SaveToStLog where PKID=:PKID  ";
        //    SqlParameterCollection sp = this.DB_oracl.CreateSqlParameterCollection();
        //    sp.Add(":PKID", PKID);
        //    return DB_oracl.ExeSqlForDataTable(sql, sp, "dt");
        //}


        #endregion

        #region  更新
        public bool Submit_TBProjectInfo(DataTable dt)
        {
            string sql = "select * from  TBProjectInfo where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_TBContractRecordManage(DataTable dt)
        {
            string sql = "select * from  TBContractRecordManage where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        public bool Submit_TBBuilderLicenceManage(DataTable dt)
        {
            string sql = "select * from  TBBuilderLicenceManage where 1=2 ";
            return DB.Update(sql, null, dt);
        }


        public bool Submit_TBProjectFinishManage(DataTable dt)
        {
            string sql = "select * from  TBProjectFinishManage where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        //public bool Submit_SaveToStLog_Oracl(DataTable dt)
        //{
        //    string sql = "select * from  SaveToStLog where 1=2 ";
        //    return this.DB_oracl.Update(sql, null, dt);
        //}
        public bool Submit_SaveToStLog(DataTable dt)
        {
            string sql = "select * from  SaveToStLog where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }


        public void Update_TBProjectBuilderUserInfo_Num()
        {
            string sql = @"update a
set a.BuilderLicenceNum=b.BuilderLicenceNum
from TBProjectBuilderUserInfo a , aj_zj_sgxk_relation b 
where a.aqjdbm=b.aqjdbm  and b.BuilderLicenceNum is null ";
            DB.ExecuteNonQuerySql(sql, null);

        }


        public void Update_TBProjectBuilderUserInfo_Num2()
        {
            string sql = @"	update a
set a.BuilderLicenceNum=c.BuilderLicenceNum 
from TBProjectBuilderUserInfo a , aj_zj_sgxk_relation b ,TBBuilderLicenceManage c 
where a.aqjdbm=b.aqjdbm and b.BuilderLicenceNum=c.BuilderLicenceInnerNum  and a.BuilderLicenceNum is null ";
            DB.ExecuteNonQuerySql(sql, null);

        }



        /// <summary>
        /// 提交建设单位信息修改到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Submit_uepp_jsdw(DataTable dt)
        {
            string sql = @"select * from uepp_jsdw  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_TCorpChiefPerson(DataTable dt)
        {
            string sql = @"select * from TCorpChiefPerson  where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        /// <summary>
        /// 提交企业信息修改到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Submit_uepp_qyjbxx(DataTable dt)
        {
            string sql = @"select * from UEPP_Qyjbxx  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }
        /// <summary>
        /// 提交人员信息修改到数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Submit_uepp_ryjbxx(DataTable dt)
        {
            string sql = @"select * from UEPP_Ryjbxx  where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        public bool Submit_uepp_qycsyw(DataTable dt)
        {
            string sql = @"select * from uepp_qycsyw  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }
        public bool Submit_uepp_qyzzmx(DataTable dt)
        {
            string sql = @"select * from UEPP_Qyzzmx  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }
        public bool Submit_uepp_qyzzzs(DataTable dt)
        {
            string sql = @"select * from UEPP_Qyzs  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }
        public bool Submit_uepp_qyry(DataTable dt)
        {
            string sql = @"select * from UEPP_QyRy  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public bool Submit_uepp_code(DataTable dt)
        {
            string sql = @"select * from UEPP_Code  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public bool Submit_uepp_Ryzyzg(DataTable dt)
        {
            string sql = @"select * from UEPP_Ryzyzg  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public bool Submit_uepp_Ryzs(DataTable dt)
        {
            string sql = @"select * from UEPP_Ryzs  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }
        public bool Submit_uepp_Ryzymx(DataTable dt)
        {
            string sql = @"select * from UEPP_Ryzymx  where 1=2 ";
            return this.DB.Update(sql, null, dt);
        }

        public bool Submit_SaveToStLog2(DataTable dt)
        {
            string sql = @"select * from SaveToStLog2  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_DataJkLog(DataTable dt)
        {
            string sql = @"select * from DataJkLog  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_DataJkDataDetail(DataTable dt)
        {
            string sql = @"select * from DataJkDataDetail  where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        public bool Submit_SaveDataLog(DataTable dt)
        {
            string sql = @"select * from SaveDataLog  where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBProjectCensorInfo(DataTable dt)
        {
            string sql = "select * from TBProjectCensorInfo where 1=2 ";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBProjectDesignEconUserInfo(DataTable dt)
        {
            string sql = "select * from TBProjectDesignEconUserInfo where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        //public bool Submit_uepp_SaveToStLog_Oracl(DataTable dt)
        //{
        //    string sql = @"select * from SaveToStLog  where 1=2 ";
        //    return DB_oracl.Update(sql, null, dt);
        //}

        //public bool Submit_uepp_SaveToStLog(DataTable dt)
        //{
        //    string sql = @"select * from SaveToStLog  where 1=2 ";
        //    return DB.Update(sql, null, dt);
        //}

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





        #endregion

        #region  删除

        public bool Delete_SaveToStLog(string tableName, string PKID)
        {
            string sql = "delete from SaveToStLog where tableName=@tableName and PKID =@PKID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@tableName", tableName);
            sp.Add("@PKID", PKID);
            return DB.ExecuteNonQuerySql(sql, sp) > 0;
        }

        public bool Delete_SaveToStLog2(string tableName, string PKID)
        {
            string sql = "delete from SaveToStLog2 where tableName=@tableName and PKID =@PKID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@tableName", tableName);
            sp.Add("@PKID", PKID);
            return DB.ExecuteNonQuerySql(sql, sp) > 0;
        }



        //public bool Delete_uepp_qycsyw(string qyID)
        //{
        //    string sql = @" delete from uepp_qycsyw where qyID=:qyID ";
        //    SqlParameterCollection sp = DB_oracl.CreateSqlParameterCollection();
        //    sp.Add(":qyID", qyID);
        //    return DB_oracl.ExecuteNonQuerySql(sql, sp)>0;
        //}

        //public bool Delete_UEPP_Qyzzmx(string qyID)
        //{
        //    string sql = @" delete from UEPP_Qyzzmx where qyID=:qyID ";
        //    SqlParameterCollection sp = DB.CreateSqlParameterCollection();
        //    sp.Add(":qyID", qyID);
        //    return DB_oracl.ExecuteNonQuerySql(sql, sp) > 0;
        //}

        #endregion


        #region 省勘察设计系统数据访问

        /// <summary>
        /// 获取省勘察设计系统勘察设计企业信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_Tab_Skcsj()
        {

            string sql = @"  select * from (
select *
,(select count(*) from Enterprise_ZZ_Tab where IsValid=1 and  ZZName like '勘察-%' and DWRowGuid=a.RowGuid) as 'IsKc'
,(select count(*) from Enterprise_ZZ_Tab where IsValid=1 and  ZZName like '设计-%' and DWRowGuid=a.RowGuid) as 'IsSj'
 from Enterprise_Tab a where a.IsValid=1 and LEN(a.ZZJGDM)>=9 and a.RowGuid in (
    select DWRowGuid from Epoint_Jskcsj..Enterprise_ZZ_Tab where IsValid=1 and (ZZName like '勘察-%' or  ZZName like '设计-%'))
 ) aaa where (IsKc>0 or IsSj >0) ";

            //--and convert(varchar(10),a.OperateDate ,120)=convert(varchar(10),GETDATE() ,120)

            return DB_Epoint_Jskcsj.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取省勘察设计系统勘察设计企业资质信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Enterprise_ZZ_Tab_Skcsj(string DWRowGuid)
        {
            string sql = @"select * from Enterprise_ZZ_Tab where IsValid=1 and (ZZName like '勘察-%' or ZZName like '设计-%')	and DWRowGuid=@DWRowGuid ";
  
            SqlParameterCollection sp = this.DB_Epoint_Jskcsj.CreateSqlParameterCollection();
            sp.Add("@DWRowGuid", DWRowGuid);
            return this.DB_Epoint_Jskcsj.ExeSqlForDataTable(sql, sp, "dt");

        }

        #endregion 



    }
}
