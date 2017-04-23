using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Bigdesk8;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class GzyjDAL
    {

        public DBOperator DB { get; set; }

        public DataTable Retrieve(string fromwhere, AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            switch (fromwhere)
            {

                case "qyzsgq"://三个月内即将过期和已过期的企业证书
                    orderby = "zsyxzrq desc";

                    sql = @"select  qyID,qymc,xxdd,lxr,lxdh,fddbr,zsyxqrq,zsyxzrq,zslx,zsbh  from ( 
select distinct a.qyID ,a.qymc,a.xxdd,a.lxr,a.lxdh,a.fddbr,b.zslx,b.zsbh,convert(varchar(10),b.zsyxqrq,120) zsyxqrq,
convert(varchar(10),b.zsyxzrq,120) zsyxzrq from uepp_qyjbxx a inner join uepp_qyzs b on a.qyid=b.qyid 
) qy where 1=1 and (convert(varchar(10),zsyxzrq,120) between convert(varchar(10),GETDATE(),120) and CONVERT(char(10), DATEADD(MM,3,GETDATE()), 120) 
or convert(varchar(10),zsyxzrq,120)<=convert(varchar(10),GETDATE(),120)) and ";
                    break;

                case "ryzsgq"://三个月内即将过期和已过期的人员证书

                    orderby = "zsyxzrq desc";

                    sql = @"select ryID,xm,zjhm,zcjb,ryzyzglx,lxdh,ryzslx,zsbh,zsyxqrq,zsyxzrq from (
select distinct a.ryID, a.xm,a.zjhm,a.zcjb,b.ryzyzglx,a.lxdh,b.ryzslx,b.zsbh,convert(varchar(10),b.zsyxqrq,120) zsyxqrq,
convert(varchar(10),b.zsyxzrq,120) zsyxzrq from uepp_ryjbxx a inner join uepp_ryzs b on a.ryid=b.ryid )  
ry where 1=1 and    (convert(varchar(10),zsyxzrq,120) between convert(varchar(10),GETDATE(),120) and  CONVERT(char(10), DATEADD(MM,3,GETDATE()), 120)
or convert(varchar(10),zsyxzrq,120)<=convert(varchar(10),GETDATE(),120))  and ";

                    break;
                case "zjgyxm"://造价过亿项目
//                    sql = @"select rownum,row_id,xmmc,jsdw,sgdw,xmjl,zj,ssdqid,ssdq,jsdwrowid,qyrowid,ryrowid from (
//select distinct a.rowid row_id,a.xmmc,a.jsdw,a.sgdw,a.xmjl,a.zj,a.ssdqid,a.ssdq,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
//from uepp_xmjbxx a inner join uepp_jsdw b on a.jsdwid=b.jsdwid left join uepp_qyjbxx c on a.sgdwid=c.qyid left join uepp_ryjbxx d on a.xmjlid=d.ryid 
//where a.zj>10000) xm where 1=1 and ";
                    orderby = "CreateDate desc";
                    sql = @"select PKID,PrjName,BuildCorpCode,BuildCorpName,sgdw,xmjl,CountyNum,AllInvest,AllArea,County,CreateDate,BDate,EDate from (
select distinct a.PKID ,a.PrjName,a.BuildCorpCode,a.BuildCorpName,a.AllArea,
stuff((select ',' +ContractorCorpName from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as sgdw
,stuff((select ',' +PrjHead from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as xmjl
,a.AllInvest,a.CountyNum,(select CodeInfo from tbXzqdmDic where Code =a.CountyNum) County,convert(varchar(10),a.CreateDate,120) CreateDate,convert(varchar(10),a.BDate,120) BDate,convert(varchar(10),a.EDate,120) EDate 
from TBProjectInfo a 
inner join uepp_jsdw b on a.BuildCorpCode=b.jsdwID 
where a.AllInvest>10000) xm where 1=1 and ";

                    break;

                case "wzljd"://未质监
                    orderby = "CreateDate desc";
                    sql = @"select PKID,PrjName,BuildCorpCode,BuildCorpName,sgdw,xmjl,CountyNum,AllInvest,County,CreateDate,BDate,EDate,PrjNum from (
select distinct a.PKID ,a.PrjName,a.BuildCorpCode,a.BuildCorpName,a.PrjNum,
stuff((select ',' +ContractorCorpName from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as sgdw
,stuff((select ',' +PrjHead from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as xmjl
,a.AllInvest,a.CountyNum,(select CodeInfo from tbXzqdmDic where Code =a.CountyNum) County,convert(varchar(10),a.CreateDate,120) CreateDate,convert(varchar(10),a.BDate,120) BDate,convert(varchar(10),a.EDate,120) EDate 
from TBProjectInfo a 
inner join uepp_jsdw b on a.BuildCorpCode=b.jsdwID 
where a.UpdateFlag='U' and a.PrjNum not in (
   select prjNum from  zj_gcjbxx where UpdateFlag='U'
)) xm where 1=1 and  ";

                    break;
                case "waqjd"://未安监
                    orderby = "CreateDate desc";
                    sql = @"select PKID,PrjName,BuildCorpCode,BuildCorpName,sgdw,xmjl,CountyNum,AllInvest,County,CreateDate,BDate,EDate,PrjNum from (
select distinct a.PKID ,a.PrjName,a.BuildCorpCode,a.BuildCorpName,a.PrjNum,
stuff((select ',' +ContractorCorpName from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as sgdw
,stuff((select ',' +PrjHead from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as xmjl
,a.AllInvest,a.CountyNum,(select CodeInfo from tbXzqdmDic where Code =a.CountyNum) County,convert(varchar(10),a.CreateDate,120) CreateDate,convert(varchar(10),a.BDate,120) BDate,convert(varchar(10),a.EDate,120) EDate 
from TBProjectInfo a 
inner join uepp_jsdw b on a.BuildCorpCode=b.jsdwID 
where a.UpdateFlag='U' and a.PrjNum not in (
   select xmbm from  aj_gcjbxx where UpdateFlag='U'
)) xm where 1=1 and  ";

                    break;


                case "jgbaLcyj"://竣工备案流程预警
                    orderby = "CreateDate desc";
                    sql = @"select  * from (
select   b.PKID,a.CreateDate,  b.PrjName,b.PrjNum,a.PrjFinishName,a.PrjFinishNum,(select CodeInfo from tbXzqdmDic where Code =b.CountyNum) County,
case when (select count(*) from TBContractRecordManage where PrjNum=a.PrjNum)=0 then '否' else '是' end  as sfhtba,
case when (select count(*) from TBProjectCensorInfo where PrjNum=a.PrjNum)=0 then '否' else '是' end    as sfsgtsc,
case when (select count(*) from TBBuilderLicenceManage where PrjNum=a.PrjNum)=0 then '否' else '是' end    as sfsgxk,
case when (select count(*) from aj_gcjbxx where xmbm=a.PrjNum)=0 then '否' else '是' end    as sfaj,
case when (select count(*) from zj_gcjbxx where PrjNum=a.PrjNum)=0 then '否' else '是' end    as sfzj
from TBProjectFinishManage a 
left join TBProjectInfo b on a.PrjNum=b.PrjNum 
 where a.UpdateFlag='U' ) xm where ( sfhtba='否' or  sfsgtsc='否' or  sfsgxk='否' or  sfaj='否' or  sfzj='否' ) and  ";

                    break;

                case "wbsgxkz"://未办施工许可证项目
//                    sql = @"select rownum,row_id,xmmc,ssdq,jsdw,sgdw,xmjl,sgxmtybh,kgrq,jsdwrowid,qyrowid,ryrowid from (
//select distinct a.rowid row_id,a.xmmc,a.ssdq,a.jsdw,a.sgdw,a.xmjl, a.sgxmtybh,to_char(b.kgrq,'yyyy-mm-dd') kgrq,e.rowid jsdwrowid,f.rowid qyrowid,g.rowid ryrowid from uepp_xmjbxx a 
//inner join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh and to_char(b.kgrq,'yyyy-mm-dd') <=to_char(sysdate,'yyyy-mm-dd')
//left join uepp_jsdw e on a.jsdwid=e.jsdwid left join uepp_qyjbxx f on a.sgdwid=f.qyid left join uepp_ryjbxx g on a.xmjlid=g.ryid 
//where a.sgxmtybh not in (select distinct d.sgxmtybh from uepp_sgxkxx c inner join UEPP_SgxkAndBdRelation d on c.sgxkid=d.sgxkid )
//) xm where 1=1 and ";
                    orderby = "BDate desc";
                    sql = @"select PKID,PrjName,BuildCorpCode,BuildCorpName,sgdw,xmjl,CountyNum,AllInvest,County,CreateDate,BDate,EDate,PrjNum from (
select distinct a.PKID ,a.PrjName,a.BuildCorpCode,a.BuildCorpName,a.PrjNum,
stuff((select ',' +ContractorCorpName from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as sgdw
,stuff((select ',' +PrjHead from TBContractRecordManage where ContractNum in ('301','302','303','304','500','600','700') for xml path('')), 1 , 1 , '') as xmjl
,a.AllInvest,a.CountyNum,(select CodeInfo from tbXzqdmDic where Code =a.CountyNum) County,convert(varchar(10),a.CreateDate,120) CreateDate,convert(varchar(10),a.BDate,120) BDate,convert(varchar(10),a.EDate,120) EDate 
from TBProjectInfo a 
inner join uepp_jsdw b on a.BuildCorpCode=b.jsdwID 
where a.UpdateFlag='U' and a.PrjNum not in (
   select PrjNum from  TBBuilderLicenceManage where UpdateFlag='U'
)) xm where 1=1 and ";

                    string kgrq1 = ft.GetValue("BDate");
                    string kgrq2 = ft.GetValue("EDate");
                    if (!string.IsNullOrEmpty(kgrq1))
                    {
                        sql += " convert(varchar(10),BDate,120)  >= '" + kgrq1 + "' and ";
                        ft.Remove("BDate");
                        //ft.Translate();
                    }
                    if (!string.IsNullOrEmpty(kgrq2))
                    {
                        sql += "convert(varchar(10),EDate,120)  <= '" + kgrq2 + "' and ";
                        ft.Remove("EDate");
                        //ft.Translate();
                    }

                    break;
                case "gcbgxm":
                    sql = @"select rownum, row_id,xmmc,jsdw,sgdw,xmbgmc,xmbgbw,bggsje,jsdwrowid,qyrowid from (
select distinct b.rowid row_id,a.xmmc,a.jsdw,a.sgdw,b.xmbgmc,b.xmbgbw,b.bggsje,c.rowid jsdwrowid,
d.rowid qyrowid from uepp_xmjbxx a inner join  UEPP_xmBgjl b  on a.sgxmtybh =b.sgxmtybh 
left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid
) xm where 1=1 and ";

                    break;

            }

            string ssdq = ft.GetValue("County");
            if (!string.IsNullOrEmpty(ssdq))
            {

                if (ssdq.Contains(','))
                {
                    string[] strSsdq = ssdq.Split(',');
                    sql += "(";
                    for (int i = 0; i < strSsdq.Length; i++)
                    {
                        if (i != 0)
                            sql += " or ";
                        if (strSsdq[i] == "市区")
                        {
                            sql += " County like @pp1 or County like @pp2";
                            sp.Add("@pp1", "%市区%");
                            sp.Add("@pp2", "%市辖区%");
                        }
                        else
                        {
                            sql += "  County like @pp3";
                            sp.Add("@pp3", "%" + strSsdq[i] + "%");
                        }
                    }
                    sql += " ) and ";

                }
                else
                {
                    if (ssdq == "市区")
                    {
                        sql += "  (County like @pp1 or County like @pp2)  ";
                        sp.Add("@pp1", "%市区%");
                        sp.Add("@pp2", "%市辖区%");

                    }
                    else
                    {
                        sql += " County like @pp3";
                        sp.Add("@pp3", "%" + ssdq + "%");

                    }
                    sql += " and ";
                }

                ft.Remove("County");
            }
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);

        }

        public DataTable Read(string rowID)
        {
            string sql = @"select a.rowid,a.*,b.xmmc,b.jsdw,b.sgdw,b.cblx from UEPP_xmBgjl a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add(":pRowID", rowID);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }
    }
}
