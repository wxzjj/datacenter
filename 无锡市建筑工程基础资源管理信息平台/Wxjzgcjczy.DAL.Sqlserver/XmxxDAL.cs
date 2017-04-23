using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8.Security;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换数据访问类
    /// 作者：孙刚
    /// 时间：2015-03-31
    /// </summary>
    public class XmxxDAL
    {
        public DBOperator DB { get; set; }

        #region 表结构

        public DataTable GetSchema_aj_gcjbxx()
        {
            string sql = "select * from aj_gcjbxx where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetSchema_zj_gcjbxx()
        {
            string sql = "select * from zj_gcjbxx where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetSchema_TBProjectBuilderUserInfo()
        {
            string sql = "select * from TBProjectBuilderUserInfo where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetSchema_zj_gcjbxx_zrdw()
        {
            string sql = "select * from zj_gcjbxx_zrdw where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        #endregion


        #region 读取单条记录
 


        public DataTable Read_TBProjectInfo(string prjNum)
        {
            string sql = @"select  * from TBProjectInfo  where UpdateFlag='U' and PrjNum=@prjNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@prjNum", prjNum);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_aj_gcjbxx(string pkId)
        {
            string sql = @"select  * from aj_gcjbxx  where UpdateFlag='U' and PKID=@PKID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_aj_gcjbxxWithRights(string pkId, AppUser workUser)
        {
            string sql = @"select  * from aj_gcjbxx  where UpdateFlag='U' and PKID=@PKID and JGID=@JGID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            sp.Add("@JGID", workUser.qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxx(string pkId)
        {
            string sql = @"select  * from zj_gcjbxx  where UpdateFlag='U' and PKID=@PKID   ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxxWithRights(string pkId,AppUser workUser)
        {
            string sql = @"select  * from zj_gcjbxx  where UpdateFlag='U' and PKID=@PKID and JGID=@JGID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            sp.Add("@JGID", workUser.qyID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxx_zrdw(string pkId)
        {
            string sql = @"select  * from zj_gcjbxx_zrdw  where PKID=@PKID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_aj_gcjbxxByAqjdbm(string aqjdbm)
        {
            string sql = @"select  * from aj_gcjbxx  where UpdateFlag='U' and aqjdbm=@aqjdbm ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@aqjdbm", aqjdbm);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable Read_zj_gcjbxxByZljdbm(string zljdbm)
        {
            string sql = @"select  * from zj_gcjbxx  where UpdateFlag='U' and zljdbm=@zljdbm ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@zljdbm", zljdbm);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable Read_TBProjectBuilderUserInfo(string pkId)
        {
            string sql = @"select  * from TBProjectBuilderUserInfo  where  PKID=@PKID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", pkId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }



        #endregion 


        #region 获取列表


        public DataTable GettbXzqdmDic()
        {
            string sql = "select * from tbXzqdmDic where parentCode='320200' order by sortOrder ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable GetQySsdq()
        {
            string sql = "select distinct County from uepp_qyjbxx a where a.DataState<>-1 and  a.County <>'' and a.County is not null ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable GetRysd()
        {
            string sql = "select distinct County from  uepp_qyjbxx where DataState<>-1 and County <>'' and County is not null ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }



        public DataTable GetXmxx(string ssdq, string xmdjrq, string xmmc)
        {
            //            string sql = @"select * from (select b.*,a.zbdw_dwmc,a.zbdw_dwdm,a.jldw_dwmc,a.jldw_dwdm,a.gcgk_kgrq,a.gcgk_jhjgrq,a.gis_jd,a.gis_wd  
            //,(select EconCorpName+';' from TBProjectCensorInfo  where PrjNum=b.PrjNum for xml path('')) as 'EconCorpNames'
            //,(select DesignCorpName+';' from TBProjectCensorInfo  where PrjNum=b.PrjNum for xml path('')) as 'DesignCorpNames' 
            //from aj_gcjbxx  a
            //inner join TBProjectInfo b on a.PrjNum=b.PrjNum 
            //where gis_jd<>0 or gis_wd<>0
            //) as aaa where 1=1  ";
            string sql = @"select  a.*
,ISNULL((select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='301' and UpdateFlag='U' and PropietorCorpCode=a.BuildCorpCode and  PrjNum=a.PrjNum for xml path('')),'')  as SgzcbCorpNames
,ISNULL((select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='100' and UpdateFlag='U' and  PrjNum=a.PrjNum for xml path('')),'')  as EconCorpNames
,ISNULL((select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='200' and UpdateFlag='U' and  PrjNum=a.PrjNum for xml path('')),'')  as DesignCorpNames
,ISNULL((select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='400' and UpdateFlag='U' and  PrjNum=a.PrjNum for xml path('')),'')  as JLCorpNames
,ISNULL(isSgbz,0) isSgbz
 from  TBProjectInfo a where a.UpdateFlag='U' and a.jd is not null and a.wd is not null and a.jd<>0 and a.wd <>0 ";


            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(ssdq))
            {
                sql += " and CountyNum=@ssdq ";
                sp.Add("@ssdq", ssdq);

            }

            if (!string.IsNullOrEmpty(xmdjrq))
            {
                sql += " and CreateDate like @CreateDate ";
                sp.Add("@CreateDate", "%" + xmdjrq + "%");

            }

            if (!string.IsNullOrEmpty(xmmc))
            {
                sql += " and PrjName like @PrjName ";
                sp.Add("@PrjName", "%" + xmmc + "%");

            }
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetXmxx(string PKID)
        {
            string sql = @"select  a.* from  TBProjectInfo a where a.UpdateFlag='U' and a.PKID=@PKID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@PKID", PKID);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable GetXmxx(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select  PKID, PrjNum, PrjInnerNum, PrjName, PrjTypeNum, BuildCorpName, BuildCorpCode, ProvinceNum, CityNum, CountyNum, PrjApprovalNum, PrjApprovalLevelNum, BuldPlanNum, ProjectPlanNum, AllInvest, AllArea, PrjSize, PrjPropertyNum, PrjFunctionNum, BDate, EDate, CreateDate, UpdateFlag, sbdqbm,  jd, wd
, (case when isSgbz is null then 0 when isSgbz=1 then 1 else -1  end) as isSgbz
,(case when isSgbz is null then '未标注' when isSgbz=1 then '已标注' else '未设置经纬度'  end) as isSgbzState
,(select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='301' and PropietorCorpCode=a.BuildCorpCode and  PrjNum=a.PrjNum for xml path(''))  as SgzcbCorpNames
,(select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='100' and  PrjNum=a.PrjNum for xml path(''))  as EconCorpNames
,(select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='200' and  PrjNum=a.PrjNum for xml path(''))  as DesignCorpNames
,(select ContractorCorpName+';' from TBContractRecordManage where  ContractTypeNum='400' and  PrjNum=a.PrjNum for xml path(''))  as JLCorpNames
 from  TBProjectInfo a where a.UpdateFlag='U' and ";



            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

     
        /// <summary>
        /// 获取安监信息数据列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveAjxxList( AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from (
		                   SELECT 
                            a.PKID,
                            a.aqjdbm,
                            a.gcmc,
                            a.Aqjdjgmc,a.zbdw_dwmc,
                            a.jldw_dwmc,
                            convert(char(10),a.bjrq,20) as  bjrq,
                            convert(char(10),a.gcgk_kgrq,20) as  gcgk_kgrq,
                            a.PrjNum,b.PrjName,b.PKID as LxPKID ,c.qyID
                            ,(select count(*) from SaveToStLog where TableName='aj_gcjbxx' and PKID=a.PKID and OperateState=0)  as HasSbToSt
                            ,a.CreateDate
                            FROM aj_gcjbxx a  
                            left join TBProjectInfo b on a.PrjNum=b.PrjNum and b.UpdateFlag='U'
							left join Uepp_Qyjbxx c on c.zzjgdm=a.zbdw_dwdm 
							where a.UpdateFlag='U' and a.JGID=@JGID
                          ) as aaa WHERE 1=1 and ";

            sp.Add("@JGID", userInfo.qyID);
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 获取质监信息数据列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveZjxxList(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from (
		                   SELECT 
                            a.PKID, zljdbm, gcmc, a.PrjNum, sgzbbm, zljdjgmc, zjzbm, gczj, jzmj, dlcd, jglx, cc, jzgm, xxjd, bz, a.CreateDate, a.UpdateFlag, a.sbdqbm,
                              convert(char(10),a.sbrq,20) as  sbrq,
                            convert(char(10),a.kgrq,20) as  kgrq,
                            convert(char(10),a.jhjgrq,20) as  jhjgrq,
                            (select CodeInfo from tbPrjStructureTypeDic where Code=a.jglx) as StructureType,
                            b.PrjName,b.PKID as LxPKID 
                            ,(select count(*) from SaveToStLog where TableName='aj_gcjbxx' and PKID=a.PKID and OperateState=0)  as HasSbToSt
                            FROM zj_gcjbxx a  
                            left join TBProjectInfo b on a.PrjNum=b.PrjNum and b.UpdateFlag='U'
							where a.UpdateFlag='U' and a.JGID=@JGID
                          ) as aaa WHERE 1=1 and ";


            sp.Add("@JGID", userInfo.qyID);
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aqjdbm"></param>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveAjxx_RyxxList(string aqjdbm ,AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"SELECT
a.PKID,	/*业务编码 guid值*/ 
a.PrjNum,	/*项目编号*/
a.BuilderLicenceNum,	/*施工许可证编号 按住建部编码规则统一编号*/
a.CorpName,	/*所属单位名称*/
a.CorpCode,		/*所属单位组织机构代码*/
a.SafetyCerID,		/*安全生产许可证编号*/
a.UserName,	/*人员姓名*/
a.IDCardTypeNum,	/*证件类型  见代码表*/
b.CodeInfo as IDCardType,
a.IDCard,	/*人员证件号码*/
a.UserPhone,		/*人员电话*/
a.CertID,	/*安全生产考核合格证书编号*/
(CASE UserType WHEN 1 THEN '主要负责人' WHEN 2 THEN '主要负责人' WHEN '3' THEN '安全员' END ) as UserType
FROM TBProjectBuilderUserInfo as a
LEFT JOIN tbIDCardTypeDic AS b ON a.IDCardTypeNum = b.Code 
LEFT JOIN TBBuilderLicenceManage AS c ON a.BuilderLicenceNum = c.BuilderLicenceNum
WHERE a.UpdateFlag='U' and a.aqjdbm = @aqjdbm and  ";
            sp.Add("@aqjdbm", aqjdbm);

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 获取质量监督编码
        /// </summary>
        /// <param name="zljdbm"></param>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveZjxx_RyxxList(string zljdbm, AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"SELECT
a.PKID, a.zljdbm, a.sbdqbm, dwlx, xh, dwmc, dwdm, xmfzrxm, xmfzrdm, xmfzr_lxdh, jsfzr, jsfzr_lxdh, zly, zly_lxdh, qyy, qyy_lxdh
FROM zj_gcjbxx_zrdw  as a
left join  zj_gcjbxx b on a.zljdbm=b.zljdbm 
WHERE a.zljdbm=@zljdbm and  ";
            sp.Add("@zljdbm", zljdbm);

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
        }




//        /// <summary>
//        /// 质监数据列表
//        /// </summary>
//        /// <param name="userInfo"></param>
//        /// <param name="ft"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="pageIndex"></param>
//        /// <param name="orderby"></param>
//        /// <param name="allRecordCount"></param>
//        /// <returns></returns>
//        public DataTable RetrieveZjxxList(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
//        {
//            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
//            string sql = @"select * from (
//                        SELECT 
//                        a.PKID,
//                        a.gcmc,
//                        a.zljdbm,
//                        a.zljdjgmc,a.zjzbm,a.sgzbbm,a.jglx,
//                        a.gczj,
//                        convert(char(10),a.sbrq,20) as sbrq,
//                        (select CodeInfo from tbPrjStructureTypeDic where Code=a.jglx) as StructureType,
//                        b.PrjName,a.PrjNum,b.PKID as LxPKID
//                        ,(select count(*) from SaveToStLog where TableName='zj_gcjbxx' and PKID=a.PKID and OperateState=0)  as HasSbToSt
//                        ,a.CreateDate
//                        FROM zj_gcjbxx a 
//                        left join TBProjectInfo b on a.PrjNum=b.PrjNum 
//                        where  a.UpdateFlag='U'  ) as aaa  WHERE 1=1 and ";

//            DALHelper.GetSearchClause(ref sp, ft);
//            sql += ft.CommandText;

//            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
//        }


        /// <summary>
        /// 立项项目信息列表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="ft"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveLxxmList(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from (
SELECT 
a.PKID,
a.PrjNum,a.PrjInnerNum,
a.PrjName,
a.PrjApprovalNum,
a.BuildCorpName,
a.BuildCorpCode,
convert(varchar(10),a.BDate,120) as BDate,
convert(varchar(10),a.EDate,120) as EDate
,convert(varchar(19),a.CreateDate,120) as CreateDate,a.PrjTypeNum,
(select codeinfo from tbPrjTypeDic where code=a.PrjTypeNum ) as PrjType
FROM TBProjectInfo a 
where a.UpdateFlag='U' ) as aaa WHERE 1=1 and ";


            //string zzlb = ft.GetValue("zzlb");
            //if (!string.IsNullOrEmpty(zzlb))
            //{
            //    sql += "  zzlb in (" + AntiSqlInjection.ParameterizeInClause(zzlb, "@para", ref sp) + ") and ";
            //    ft.Remove("zzlb");
            //}

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;

            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pageSize, pageIndex, out allRecordCount);
        }

#endregion 


        #region 更新
        public bool SubmitXmxx(DataTable dt)
        {
            string sql = @"select  a.* from  TBProjectInfo a where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_aj_gcjbxx(DataTable dt)
        {
            string sql = @"select  a.* from  aj_gcjbxx a where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_zj_gcjbxx(DataTable dt)
        {
            string sql = @"select  a.* from  zj_gcjbxx a where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_TBProjectBuilderUserInfo(DataTable dt)
        {
            string sql = @"select * from  TBProjectBuilderUserInfo  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool Submit_zj_gcjbxx_zrdw(DataTable dt)
        {
            string sql = @"select * from  zj_gcjbxx_zrdw  where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        #endregion
    }
}
