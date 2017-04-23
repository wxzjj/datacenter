using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;

namespace Wxjzgcjczy.DAL
{
    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换数据访问类
    /// 作者：孙刚
    /// 时间：2015-03-31
    /// </summary>
    public class DataExchangeDAL
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
        #endregion

        #region  读取列表
        /// <summary>
        /// 获取无锡数据中心库里的立项项目数据集
        /// </summary>
        /// <param name="conditions">查询条件集合</param>
        /// <returns>返回立项项目数据集</returns>
        public DataTable GetTBData_TBProjectInfo(List<IDataItem> conditions)
        {
            string sql = @"  select * from (
                select PKID   ,PrjNum ,PrjInnerNum ,PrjName ,PrjTypeNum ,
	            BuildCorpName ,BuildCorpCode ,ProvinceNum ,CityNum ,CountyNum ,PrjApprovalNum ,PrjApprovalLevelNum ,BuldPlanNum,ProjectPlanNum 
                ,AllInvest ,AllArea  ,PrjSize ,PrjPropertyNum ,PrjFunctionNum , to_char(BDate,'yyyy-MM-dd') BDate ,to_char(EDate,'yyyy-MM-dd') EDate ,
	            to_char(CREATEDATE,'yyyy-MM-dd') CREATEDATE ,UpdateFlag ,sbdqbm from TBProjectInfo
	        ) aaa where 1=1 ";

//            string sql = @"select * from (
//                select PKID   ,PrjNum ,PrjInnerNum ,PrjName ,PrjTypeNum ,
//	            BuildCorpName ,BuildCorpCode ,ProvinceNum ,CityNum ,CountyNum ,PrjApprovalNum ,PrjApprovalLevelNum ,BuldPlanNum,ProjectPlanNum 
//                ,AllInvest ,AllArea  ,PrjSize ,PrjPropertyNum ,PrjFunctionNum , convert(varchar(30),BDate,111) BDate ,convert(varchar(30),EDate,111) EDate ,
//	            convert(varchar(30),CREATEDATE,111) CREATEDATE ,UpdateFlag ,sbdqbm from TBProjectInfo
//	        ) aaa where 1=1 ";
 
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
         
           conditions.GetSearchClause(DataBaseType.ORACLE11G,sp, ref sql);
           //  conditions.GetSearchClause( sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_xm_gcdjb_dtxm(List<IDataItem> conditions)
        {
            string sql = @" select * from (
 select PKID, PrjNum, fxbm, fxnbbm, xmmc, gclb, gczj, jzmj, jsgm, jglx, jsyt, dscs, dxcs, gd, kd, jhkgrq, jhjgrq,to_char(CREATEDATE,'yyyy-MM-dd') CreateDate, UpdateFlag, sbdqbm from xm_gcdjb_dtxm
) aaa where 1=1 ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            //string sql = "select * from xm_gcdjb_dtxm ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBTenderInfo(List<IDataItem> conditions)
        {
            string sql = @"select * from (
 select PKID, TenderName, TenderNum, TenderInnerNum, PrjNum, TenderClassNum, TenderTypeNum, TenderResultDate, TenderMoney, PrjSize, Area, AgencyCorpName, AgencyCorpCode, TenderCorpName, TenderCorpCode, ConstructorName, ConstructorPhone, IDCardTypeNum, ConstructorIDCard, shypbf,to_char(CREATEDATE,'yyyy-MM-dd')  CreateDate, UpdateFlag, sbdqbm 
 from TBTenderInfo
) aaa where 1=1 ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBContractRecordManage(List<IDataItem> conditions)
        {
            //string sql = "select * from TBContractRecordManage ";

            string sql = @"select * from (
select PKID, RecordName, RecordNum, RecordInnerNum, PrjNum, ContractNum, ContractTypeNum, ContractMoney, PrjSize, ContractDate, PropietorCorpName, PropietorCorpCode, ContractorCorpName, ContractorCorpCode, UnionCorpName, UnionCorpCode,to_char(CREATEDATE,'yyyy-MM-dd') CreateDate, UpdateFlag, PrjHead, PrjHeadPhone, IDCard, sbdqbm
from TBContractRecordManage
) aaa where 1=1 ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBProjectCensorInfo(List<IDataItem> conditions)
        {
            string sql = "select * from TBProjectCensorInfo ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBProjectDesignEconUserInfo(List<IDataItem> conditions)
        {
            string sql = "select * from TBProjectDesignEconUserInfo ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManage(List<IDataItem> conditions)
        {
           // string sql = "select * from TBBuilderLicenceManage ";

            string sql = @"select *  from (select PKID, BuilderLicenceName, BuilderLicenceNum, BuilderLicenceInnerNum, PrjNum, BuldPlanNum, ProjectPlanNum, CensorNum, ContractMoney, Area, PrjSize, IssueCertDate, EconCorpName, EconCorpCode, DesignCorpName, DesignCorpCode, ConsCorpName, ConsorpCode, SafetyCerID, SuperCorpName, SuperCorpCode, ConstructorName, CIDCardTypeNum, ConstructorIDCard, ConstructorPhone, SupervisionName, SIDCardTypeNum, SupervisionIDCard, SupervisionPhone,to_char(CREATEDATE,'yyyy-MM-dd') CreateDate, UpdateFlag, sbdqbm
from TBBuilderLicenceManage ) aaa where 1=1 ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_TBProjectBuilderUserInfo(List<IDataItem> conditions)
        {
            string sql = "select * from TBProjectBuilderUserInfo ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_TBProjectFinishManage(List<IDataItem> conditions)
        {
            //string sql = "select * from TBProjectFinishManage ";
            string sql = @"select *  from (select PKID, PrjFinishName, PrjFinishNum, PrjFinishInnerNum, PrjNum, BuilderLincenceNum, QCCorpName, QCCorpCode, FactCost, FactArea, FactSize, PrjStructureTypeNum, BDate, EDate, Mark,to_char(CREATEDATE,'yyyy-MM-dd') CreateDate, UpdateFlag, sbdqbm from TBProjectFinishManage ) aaa where 1=1 ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_aj_gcjbxx(List<IDataItem> conditions)
        {
            string sql = "select * from aj_gcjbxx ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_zj_gcjbxx(List<IDataItem> conditions)
        {
            string sql = "select * from zj_gcjbxx ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetTBData_zj_gcjbxx_zrdw(List<IDataItem> conditions)
        {
            string sql = "select * from zj_gcjbxx_zrdw ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable GetTBData_aj_zj_sgxk_relation(List<IDataItem> conditions)
        {
            string sql = "select * from aj_zj_sgxk_relation ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            conditions.GetSearchClause(DataBaseType.ORACLE11G, sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
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
            string sql = "select * from TBProjectInfo where PrjInnerNum in (" + prjInnerNum + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_xm_gcdjb_dtxm(string fxnbbm)
        {
            string sql = "select * from xm_gcdjb_dtxm where fxnbbm in (" + fxnbbm + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBTenderInfo(string tenderInnerNum)
        {
            if (tenderInnerNum.IndexOf(",") < 0)
                tenderInnerNum = "'" + tenderInnerNum + "'";
            string sql = "select * from TBTenderInfo where TenderInnerNum in ("+tenderInnerNum+") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBContractRecordManage(string recordInnerNum )
        {
            if (recordInnerNum.IndexOf(",") < 0)
                recordInnerNum = "'" + recordInnerNum + "'";
            string sql = "select * from TBContractRecordManage where RecordInnerNum in (" + recordInnerNum + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBProjectCensorInfo(string censorInnerNum )
        {
            if (censorInnerNum.IndexOf(",") < 0)
                censorInnerNum = "'" + censorInnerNum + "'";
            string sql = "select * from TBProjectCensorInfo where CensorInnerNum in (" + censorInnerNum + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBBuilderLicenceManage(string builderLicenceInnerNum )
        {
            if (builderLicenceInnerNum.IndexOf(",") < 0)
                builderLicenceInnerNum = "'" + builderLicenceInnerNum + "'";
            string sql = "select * from TBBuilderLicenceManage where BuilderLicenceInnerNum in (" + builderLicenceInnerNum + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_TBProjectFinishManage(string prjFinishInnerNum)
        {
            if (prjFinishInnerNum.IndexOf(",") < 0)
                prjFinishInnerNum = "'" + prjFinishInnerNum + "'";
            string sql = "select * from TBProjectFinishManage where PrjFinishInnerNum in (" + prjFinishInnerNum + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_zj_gcjbxx(string zljdbmString)
        {
            string sql = "select * from zj_gcjbxx where zljdbm in (" + zljdbmString + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetTBData_aj_gcjbxx(string aqjdbmString)
        {
            string sql = "select * from aj_gcjbxx where aqjdbm in (" + aqjdbmString + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetTBData_aj_zj_sgxk_relation(string aqjdbmString)
        {
          
            string sql = "select * from aj_zj_sgxk_relation where aqjdbm in (" + aqjdbmString + ") ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        #endregion

        #region 更新


        public bool SaveTBData_TBProjectInfo(DataTable dt)
        {
            string sql = "select * from TBProjectInfo where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_xm_gcdjb_dtxm(DataTable dt)
        {
            string sql = "select * from xm_gcdjb_dtxm where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_TBTenderInfo(DataTable dt)
        {
            string sql = "select * from TBTenderInfo where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBContractRecordManage(DataTable dt)
        {
            string sql = "select * from TBContractRecordManage where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_TBProjectCensorInfo(DataTable dt)
        {
            string sql = "select * from TBProjectCensorInfo where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBProjectDesignEconUserInfo(DataTable dt)
        {
            string sql = "select * from TBProjectDesignEconUserInfo where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_TBBuilderLicenceManage(DataTable dt)
        {
            string sql = "select * from TBBuilderLicenceManage where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBProjectBuilderUserInfo(DataTable dt)
        {
            string sql = "select * from TBProjectBuilderUserInfo where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_TBProjectFinishManage(DataTable dt)
        {
            string sql = "select * from TBProjectFinishManage where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_aj_gcjbxx(DataTable dt)
        {
            string sql = "select * from aj_gcjbxx where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SaveTBData_zj_gcjbxx(DataTable dt)
        {
            string sql = "select * from zj_gcjbxx where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_zj_gcjbxx_zrdw(DataTable dt)
        {
            string sql = "select * from zj_gcjbxx_zrdw where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SaveTBData_aj_zj_sgxk_relation(DataTable dt)
        {
            string sql = "select * from aj_zj_sgxk_relation where 1=2";
            return DB.Update(sql, null, dt);
        }
        #endregion


        #region 删除
        public bool Delete_ProjectBuilderUserInfo(string builderLicenceNumString)
        {
            string sql = "delete from TBProjectBuilderUserInfo where aqjdbm in (" + builderLicenceNumString + ")  ";
            return DB.ExecuteNonQuerySql(sql, null) > 0;
        }
        public bool Delete_ProjectDesignEconUserInfo(string censorNumString)
        {
            string sql = "delete from ProjectDesignEconUserInfo where CensorNum in (" + censorNumString + ")  ";
            return DB.ExecuteNonQuerySql(sql, null) > 0;
        }
        public bool Delete_zj_gcjbxx_zrdw(string zljdbmString)
        {
            string sql = "delete from zj_gcjbxx_zrdw where zljdbm in (" + zljdbmString + ")  ";
            return DB.ExecuteNonQuerySql(sql, null) > 0;
        }
        #endregion

        #region
        public string GetMaxXmhjNumber(string prjNum, string code)
        {
            string sql = "";
            string xmhjNumber = "";

            sql = @"select max(substr(TenderNum,length(PrjNum)+5,3)) from TBTenderInfo where prjNum=:PrjNum and substr(TenderNum,length(PrjNum)+2,2)=:Code ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add(":PrjNum", prjNum);
            spc.Add(":Code", code);
            xmhjNumber = DB.ExeSqlForString(sql, spc);

            if (!string.IsNullOrEmpty(xmhjNumber))
                xmhjNumber = (Int32.Parse(xmhjNumber) + 1).ToString().PadLeft(3, '0');
            else
                xmhjNumber = "001";
            return xmhjNumber;
        }

        #endregion
    }
}
