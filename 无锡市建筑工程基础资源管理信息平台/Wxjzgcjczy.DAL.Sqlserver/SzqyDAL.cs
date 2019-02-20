using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class SzqyDAL
    {
        public DBOperator DB { get; set; }

        #region 获取表结构
        #endregion

        #region 读取


        public DataTable ReadQyxx(string qyID)
        {
            string sql = @" select a.*,b.zsyxzrq  jgdmyxq,c.zsbh  zzzsbh,c.fzdw  zzzsfzdw,c.zsyxzrq  zzzsyxq ,c.bz  zzzsbz,
              d.zsbh  yyzzzch, d.zsyxzrq  yyzzzch,e.zsbh  aqscxkzbh,e.fzdw  aqscxkzfzdw ,e.fzrq  aqscxkzfzrq,
              e.zsyxzrq  aqscxkzyxq, f.zsbh  scbh, f.zsyxzrq  xyscyxq,f.fzdw  xyscfzdw, g.zc  frdbzc ,h.zc  qyjlzc,i.zc  jsfzrzc,a.tyshxydm 
               from UEPP_Qyjbxx a left join UEPP_Qyzs b on a.qyid=b.qyid and b.DataState<>-1  and b.zslxID=1 
               left join UEPP_Qyzs c on a.qyid=c.qyid and c.DataState<>-1 and c.zslxID=10
               left join UEPP_Qyzs d on a.qyid=d.qyid and d.DataState<>-1 and d.zslxID=2
               left join UEPP_Qyzs e on a.qyid=e.qyid and e.DataState<>-1 and e.zslxID=11
               left join UEPP_Qyzs f on a.qyid=f.qyid and f.DataState<>-1 and f.zslxID=12
               left join Uepp_Ryjbxx g on a.fddbr_ryid=g.ryid and g.datastate<>-1 
               left join Uepp_Ryjbxx h on a.qyfzr_ryid=h.ryid and h.datastate<>-1 
               left join Uepp_Ryjbxx i on a.jsfzr_ryid=i.ryid and i.datastate<>-1 
              where a.qyID=@qyID";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadJsdw(string jsdwID)
        {
            string sql = @"select * from uepp_jsdw where jsdwID=@jsdwID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        #endregion

        #region 读取列表
        public DataTable RetrieveJsdwSsgc(string jsdwID)
        {

//            string sql = @"select b.zj,b.gm,b.jzmj,b.dd,b.xmmc,b.xmid,b.sgdwid,b.sgdw,b.xmjl,b.xmjlid,d.rowid qyrowid,e.rowid ryrowid 
//from uepp_jsdw a inner join uepp_xmjbxx b on a.jsdwid=b.jsdwid   
// left join uepp_qyjbxx d on b.sgdwid=d.qyid left join uepp_ryjbxx e on b.xmjlid=e.ryid   
//where a.jsdwID=@jsdwID";
//            string sql = @"select b.xmid,b.jsdwid,b.zj,b.gm,b.jzmj,b.dd,b.xmmc,b.xmid,b.sgdwid,b.sgdw,b.xmjl,b.xmjlid,d.qyid,e.ryid
//from uepp_jsdw a inner join uepp_xmjbxx b on a.jsdwid=b.jsdwid   
// left join uepp_qyjbxx d on b.sgdwid=d.qyid left join uepp_ryjbxx e on b.xmjlid=e.ryid   
//where a.jsdwID=@jsdwID ";
            string sql = @"select *,ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=ProvinceNum),'')
+ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=CityNum),'')
+ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=CountyNum),'') as xmdd
,(select CodeInfo from tbPrjTypeDic where Code=PrjTypeNum) as PrjType from TBProjectInfo where UpdateFlag='U' and  BuildCorpCode=(select zzjgdm from UEPP_Jsdw where jsdwID=@jsdwID) ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "t");

        }

        public DataTable RetrieveZyry(string qyID, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            //            string sql = @" select rownum,a.qymc,b.*,c.xm,c.zjhm,d.ryzslx,d.zsbh,to_char(d.zsyxqrq,'yyyy-MM-dd') zsyxqrq,to_char(d.zsyxzrq,'yyyy-MM-dd') zsyxzrq 
            //from uepp_qyjbxx a inner join UEPP_QyRy b on a.qyid=b.qyid inner join uepp_ryjbxx c on b.ryid=c.ryid
            //left join uepp_ryzs d on b.ryid=d.ryid and b.ryzyzglxID=d.ryzyzglxID 
            //where a.rowid=:pRowID";
            /*
            string sql = @"select qymc,qyid,ryid,xm,zjhm,lxdh,ryzyzglxid,ryzyzglx,zsjlId,ryzslxid,ryzslx,zsbh,zsyxqrq,zsyxzrq,zsmx from(
select distinct a.qymc,b.qyid,b.ryid,c.xm,c.zjhm,ISNULL(c.lxdh,c.yddh) lxdh,b.ryzyzglxid,b.ryzyzglx,d.zsjlId,d.ryzslxid,d.ryzslx,d.zsbh,  
CONVERT(varchar(10),d.zsyxqrq,120) zsyxqrq,CONVERT(varchar(10),d.zsyxzrq,120) zsyxzrq,'查看明细' zsmx
from uepp_qyjbxx a 
inner join UEPP_QyRy b on a.qyid=b.qyid and b.DataState<>-1
inner join uepp_ryjbxx c on b.ryid=c.ryid and c.DataState<>-1
left join uepp_ryzs d on c.ryid=d.ryid and d.DataState<>-1 and b.ryzyzglxID=d.ryzyzglxID  where a.qyID=@qyID 
) ryxx where 1=1 and ";
             */

            //and d.zsyxzrq>GETDATE() 20171109 从SQL移除

            string sql = @"select 
ROW_NUMBER() over(order by ryid,zsbh) as rowno, 
qymc,qyid,ryid,xm,zjhm,lxdh,ryzyzglxid,ryzyzglx,zsjlId,ryzslxid,ryzslx,zsbh,zsyxqrq,zsyxzrq,zsmx from(
select distinct *
from
(
select a.qymc,b.qyid,b.ryid,c.xm,c.zjhm,ISNULL(c.lxdh,c.yddh) lxdh,b.ryzyzglxid,b.ryzyzglx,d.zsjlId,d.ryzslxid,d.ryzslx,d.zsbh,  
CONVERT(varchar(10),d.zsyxqrq,120) zsyxqrq,CONVERT(varchar(10),d.zsyxzrq,120) zsyxzrq,'查看明细' zsmx
from uepp_qyjbxx a 
inner join UEPP_QyRy b on a.qyid=b.qyid and b.DataState<>-1
inner join uepp_ryjbxx c on b.ryid=c.ryid and c.DataState<>-1
left join uepp_ryzs d on c.ryid=d.ryid and d.DataState<>-1 and b.ryzyzglxID=d.ryzyzglxID  
where 1=1
and a.qyID=@qyID
) mid

) ryxx where 1=1 and ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText.Trim();
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);

        }

        public DataTable RetrieveRyzs(string ryid)
        {
            string sql = "select rowid,ryzyzglx,ryzslx,zsbh,CONVERT(varchar(10),zsyxqrq,120) zsyxqrq,CONVERT(varchar(10),zsyxzrq,120) zsyxzrq,'查看明细' zsmx  from uepp_ryzs where ryid=@ryid ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryid", ryid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        /// <summary>
        ///勘察设计企业承揽工程
        /// </summary>
        /// <param name="qyID"></param>
        /// <param name="befrom"></param>
        /// <param name="dwlx"></param>
        /// <returns></returns>
        public DataTable RetrieveQyclgc(string qyID, string befrom, string dwlx)
        {
            string sql = "";
            sql = @"select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID,ISNULL(d.jsdwID,'') as jsdwID from TBContractRecordManage a
inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
left join UEPP_Jsdw d on d.zzjgdm=a.PropietorCorpCode 
 where a.UpdateFlag='U' and b.UpdateFlag='U' and c.qyID=@qyID ";
//            if (befrom.Equals("sgdw", StringComparison.CurrentCultureIgnoreCase))
//            {
//                sql = @"select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID,ISNULL(d.jsdwID,'') from TBContractRecordManage a
//inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
//inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
//left join UEPP_Jsdw d on d.zzjgdm=a.PropietorCorpCode 
// where a.UpdateFlag='U' and b.UpdateFlag='U' and ContractTypeNum in ('301','302','304') and c.qyID=@qyID ";

//            }
//            else
//            if (befrom.Equals("kcdw", StringComparison.CurrentCultureIgnoreCase))
//            {
//                sql = @"  select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID from TBContractRecordManage a
//inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
//inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
// where a.UpdateFlag='U' and b.UpdateFlag='U' and ContractTypeNum ='100' and c.qyID=@qyID";
//            }
//            else if (befrom.Equals("sjdw", StringComparison.CurrentCultureIgnoreCase))
//            {
//                sql = @"  select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID from TBContractRecordManage a
//inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
//inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
// where a.UpdateFlag='U' and b.UpdateFlag='U' and ContractTypeNum ='200' and c.qyID=@qyID";

//            }
//            else
//            {
//                sql="";
//            }
//            if (dwlx == "sgdw")
//            {
////                sql = @"select qymc,qyid,ryid,xm,zjhm,lxdh,ryzyzglx,ryzslx,zsbh,zsyxqrq,zsyxzrq,zsmx from(
////select distinct a.qymc,b.qyid,b.ryid,c.xm,c.zjhm,ISNULL(ISNULL(c.lxdh,c.yddh),'0') lxdh,d.ryzyzglx,d.ryzslx,d.zsbh,  
////CONVERT(varchar(10),d.zsyxqrq,120) zsyxqrq,CONVERT(varchar(10),d.zsyxzrq,120) zsyxzrq,'查看明细' zsmx
////from uepp_qyjbxx a inner join UEPP_QyRy b on a.qyid=b.qyid inner join uepp_ryjbxx c on b.ryid=c.ryid
////inner join uepp_ryzs d on c.ryid=d.ryid 
////where a.qyID=@qyID ) aaa where 1=1 and qymc is not null ";
//                sql = @"select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID,ISNULL(d.jsdwID,'') from TBContractRecordManage a
//inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
//inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
//left join UEPP_Jsdw d on d.zzjgdm=a.PropietorCorpCode 
// where a.UpdateFlag='U' and b.UpdateFlag='U' and ContractTypeNum in ('301','302','304') and c.qyID=@qyID ";

//            }
//            else
//            {
//                string csywlxid = "''";

//                switch (befrom)
//                {
//                    case "sgdw":
//                        csywlxid = "1,2,3";
//                        sql = @"select xmid ,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,qyid,jsdwid,ryid  from (
//select c.xmid ,c.xmmc,c.jsdw,c.sgdw,c.xmjl,c.cblx,c.ssdq,f.qyid,d.jsdwid,e.ryid 
//from uepp_qyjbxx a inner join UEPP_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxid in (" + csywlxid + ")";
//                        sql += @" inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh left join uepp_qyjbxx f on c.sgdwid=f.qyid  
//left join uepp_jsdw d on c.jsdwid=d.jsdwid left join uepp_ryjbxx e on c.xmjlid=e.ryid 
//where a.qyid=@qyID ) aaa where 1=1  and qymc is not null ";
//                        break;
//                    case "sjdw":
//                        csywlxid = "5,6";
//                        sql = @"  select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID from TBContractRecordManage a
//inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
//inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
// where a.UpdateFlag='U' and b.UpdateFlag='U' and ContractTypeNum in ('100','200') and c.qyID=@qyID";
//                        break;
//                    case "jldw":
//                        csywlxid = "4";
//                        sql = @"select xmid ,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,qyid,jsdwid,ryid  from (
//select c.xmid ,c.xmmc,c.jsdw,c.sgdw,c.xmjl,c.cblx,c.ssdq,f.qyid,d.jsdwid,e.ryid 
//from uepp_qyjbxx a inner join UEPP_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxid in (" + csywlxid + ")";
//                        sql += @" inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh left join uepp_qyjbxx f on c.sgdwid=f.qyid  
//left join uepp_jsdw d on c.jsdwid=d.jsdwid left join uepp_ryjbxx e on c.xmjlid=e.ryid 
//where a.qyid=@qyID ) aaa where 1=1  and qymc is not null ";
//                        break;
//                    case "zjjg":
//                        csywlxid = "7,8,9";
//                        sql = @"select xmid ,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,qyid,jsdwid,ryid  from (
//select c.xmid ,c.xmmc,c.jsdw,c.sgdw,c.xmjl,c.cblx,c.ssdq,f.qyid,d.jsdwid,e.ryid 
//from uepp_qyjbxx a inner join UEPP_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxid in (" + csywlxid + ")";
//                        sql += @" inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh left join uepp_qyjbxx f on c.sgdwid=f.qyid  
//left join uepp_jsdw d on c.jsdwid=d.jsdwid left join uepp_ryjbxx e on c.xmjlid=e.ryid 
//where a.qyid=@qyID ) aaa where 1=1  and qymc is not null ";
//                        break;
//                    case "qtdw":
//                        csywlxid = "''";
//                        sql = @"select xmid ,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,qyid,jsdwid,ryid  from (
//select c.xmid ,c.xmmc,c.jsdw,c.sgdw,c.xmjl,c.cblx,c.ssdq,f.qyid,d.jsdwid,e.ryid 
//from uepp_qyjbxx a inner join UEPP_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxid in (" + csywlxid + ")";
//                        sql += @" inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh left join uepp_qyjbxx f on c.sgdwid=f.qyid  
//left join uepp_jsdw d on c.jsdwid=d.jsdwid left join uepp_ryjbxx e on c.xmjlid=e.ryid 
//where a.qyid=@qyID ) aaa where 1=1  and qymc is not null ";
//                        break;
//                }
               
 //           }
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //sp.Add(":pCsywlxID", csywlxid);
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "t");

        }

        public DataTable RetrieveQyzs(string qyID, string befrom)
        {
            string sql = "";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
//            sql = @"select b.zsjlId,b.zslx,b.zsbh,b.fzdw,Convert(varchar(10),b.fzrq,120) fzrq,Convert(varchar(10),b.zsyxzrq,120) zsyxzrq
//from uepp_qyjbxx a  inner join uepp_qyzs b on a.qyid=b.qyid 
//where a.qyID=@qyID";
            sql = @"select zsjlId,zslx,zsbh,fzdw,Convert(varchar(10),fzrq,120) fzrq,Convert(varchar(10),zsyxzrq,120) zsyxzrq,tag,xgrqsj
from uepp_qyzs where qyID=@qyID and DataState<>-1 ";
            sp.Add("@qyID", qyID);

            return DB.ExeSqlForDataTable(sql, sp, "t");

        }


        public DataTable RetrieveQyxxList(string qylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            //string tagValue = "";
            if (qylx == "jsdw")
            {
                sql = @" select * from (
 select jsdwid,zzjgdm,jsdw,ISNULL(fddbr,'无') fddbr,dwflid,dwfl,ISNULL(dwdz,'无') dwdz,ISNULL(lxr,'无') lxr,lxdh,a.datastate,CONVERT(varchar(10),a.xgrqsj,120) xgrqsj,a.tag 
 from uepp_jsdw a
 ) as aaa  where 1=1 and ";

                //tagValue = ft.GetValue("tag");
                //ft.Remove("tag");


            }
            else
            {
                //if (string.IsNullOrEmpty(orderby.Trim()))
                //    orderby = " qyid  ";
                string csywlxID = "";
                switch (qylx)
                {
                    case "sgdw":
                        csywlxID = "1,3,2,13,14";
                        break;
                    case "kcdw":
                        csywlxID = "5";
                        break;
                    case "sjdw":
                        csywlxID = "6,2";
                        break;
                    //case "jsdw":
                    //    csywlxID = "11,12";
                    //    break;
                    case "zjjg":
                        csywlxID = "7,4,8,9,15,16,17";
                        break;
                    case "qtdw":
                        csywlxID = "''";
                        break;
                }



                //                sql = @"  select * from (
                // select a.rowid row_id,a.qyid,a.qymc,a.xxdd,ISNULL(a.lxdh,'0') lxdh,ISNULL(a.lxr,'无') lxr,a.sylxid,a.sylx,ISNULL(a.county,'无') county,a.datastate,b.csywlxid,b.csywlx 
                //from uepp_qyjbxx a inner join uepp_qycsyw b on a.qyid=b.qyid and b.csywlxid in(" + csywlxID + ")";

                sql = @"  select *,(province+','+city+','+county) as pcc,(case when SbToStState=0 then '已上报' when SbToStState=-1 then '未上报' when SbToStState=2 then '未更新'  else '上报出错' end) as SbState from (
 select a.qyid,a.zzjgdm,a.yyzzzch,a.qymc,a.zcdd,a.xxdd,a.lxdh lxdh,a.lxr lxr,a.sylxid,a.sylx,a.province, a.city, a.county ,a.datastate,a.tag,a.CountyID,CONVERT(varchar(10),xgrqsj,120) xgrqsj 
,ISNULL((select  SbToStState from SaveToStLog2 where TableName='uepp_qyjbxx' and PKID=a.qyid ),-1) as SbToStState
,(select  SbToStMsg from SaveToStLog2 where TableName='uepp_qyjbxx' and PKID=a.qyid ) as SbToStMsg
from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid in(" + csywlxID + ") and DataState != -1";

                string _csywlxid = ft.GetValue("csywlxid");
                if (!string.IsNullOrEmpty(_csywlxid))
                {
                    sql += " and csywlxid in (" + _csywlxid + ")  ";
                    ft.Remove("csywlxid");
                    //ft.Translate();
                }

                sql += "))  qyxx where 1=1  and ";   //and xxdd is not null

                string zhuxzz = ft.GetValue("zhuxzz");
                if (!string.IsNullOrEmpty(zhuxzz))
                {
                    sql += " qyid in (select qyid from uepp_qyzzmx where zzbz=@zzbz and zzlb=@zzlb and DataState<>-1 )  and ";
                    sp.Add("@zzbz", "主项");
                    sp.Add("@zzlb", zhuxzz);
                    ft.Remove("zhuxzz");
                    //ft.Translate();
                }

                string zengxzz = ft.GetValue("zengxzz");
                if (!string.IsNullOrEmpty(zengxzz))
                {
                    sql += " qyid in (select qyid from uepp_qyzzmx where zzbz=@zzbz1 and zzlb=@zzlb1 and DataState<>-1)  and ";
                    sp.Add("@zzbz1", "增项");
                    sp.Add("@zzlb1", zengxzz);
                    ft.Remove("zengxzz");
                    //ft.Translate();
                }

                string countyID = ft.GetValue("CountyID");
                if (!string.IsNullOrEmpty(countyID))
                {
                    if (string.Equals(countyID, "320213"))
                    {
                        sql += " CountyID in (320202, 320203, 320204, 320213) and ";
                    }
                    else if (string.Equals(countyID, "省内企业"))
                    {
                        sql += " province='江苏省' and city!='无锡市' and";
                    }
                    else if (string.Equals(countyID, "省外企业"))
                    {
                        sql += " province!='江苏省' and ";
                    }
                    else
                    {
                        sql += " CountyID =@countyID and ";
                        sp.Add("@countyID", countyID);
                    }
                    ft.Remove("CountyID");
                }

                string county = ft.GetValue("county");
                if (!string.IsNullOrEmpty(county))
                {
                    if (string.Equals(county, "省内企业"))
                    {
                        sql += " province='江苏省' and city!='无锡市' and";
                    }
                    else if (string.Equals(county, "省外企业"))
                    {
                        sql += " province!='江苏省' and ";
                    }
                    else
                    {
                        sql += " county =@county and ";
                        sp.Add("@county", county);
                    }
                    ft.Remove("county");
                }


                //if (!string.IsNullOrEmpty(zhuxzz) || !string.IsNullOrEmpty(zengxzz))
                //    ft.Translate();
            }
            DALHelper.GetSearchClause(ref sp, ft);
            
            sql += ft.CommandText.Trim();

            //if (!string.IsNullOrEmpty(tagValue))
            //{
            //    string[] tagArr = tagValue.Split(',');
            //    string tagSql = "";

            //    for (int i = 0; i < tagArr.Length; i++)
            //    {
            //        if (i == 0)
            //        {
            //            tagSql += " tag = :pam" + (i + 1);
            //            sp.Add(":pam" + (i + 1), tagArr[i]);
            //        }
            //        else
            //        {
            //            tagSql += " or tag = :pam" + (i + 1);
            //            sp.Add(":pam" + (i + 1), tagArr[i]);
            //        }
            //    }

            //    sql += " and (" + tagSql + ") ";
            //}

            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveQyxxViewList(string qyid)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @"SELECT ROW_NUMBER() OVER (
		ORDER BY zzlb
			,zsbh
		) AS rowno
	,*
FROM (
    SELECT  distinct *
    FROM
    (
	SELECT zzmx.csywlx zzlb
		,zs.zsbh
		,(zzmx.zzlb + zzmx.zzxl + zzmx.zzdj) zzmc
		,CONVERT(VARCHAR(12), zs.zsyxqrq, 23) AS fzrq
		,CONVERT(VARCHAR(12), zs.zsyxzrq, 23) AS yxq
		,zs.fzdw
	FROM UEPP_Qyzzmx zzmx
	LEFT JOIN UEPP_Qyzs zs ON zzmx.zsbh = zs.zsbh
	LEFT JOIN Uepp_Qyjbxx jbxx ON zzmx.qyID = jbxx.qyID
	WHERE zsyxzrq > GETDATE() AND zzmx.DataState <> -1
		AND zzmx.qyID=@pQyID
    ) aaa
	UNION ALL
	SELECT qyzs.csywlx
		,qyzs.zsbh
		,qyzs.zslx
		,CONVERT(VARCHAR(12), qyzs.zsyxqrq, 23) AS fzrq
		,CONVERT(VARCHAR(12), qyzs.zsyxzrq, 23) AS yxq
		,qyzs.fzdw
	FROM UEPP_Qyzs qyzs
	WHERE  qyzs.DataState <> -1
		AND qyID=@pQyID
		AND zslxID = 140
	) t
ORDER BY zzlb
	,zsbh";
            sp.Add("@pQyID", qyid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveQyxxViewListForKcsj(string qyid)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @"SELECT ROW_NUMBER() OVER (
		ORDER BY zzlb
			,zsbh
		) AS rowno
	,*
FROM (
	SELECT c.cert_type AS zzlb
		,c.cert_no AS zsbh
		,c.cert_name AS zzmc
		,c.issue_date AS fzrq
		,c.valid_date AS yxq
		,c.issue_authority AS fzdw
	FROM WJSJZX.dbo.Corp_Cert c
	WHERE c.corp_id = @pQyID
   AND (c.STATUS IS NULL
	OR c.STATUS <> - 1)
	) t
ORDER BY zzlb
	,zsbh
";
            sp.Add("@pQyID", qyid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadZzmx(string ID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select zslx,zsbh,zsyxqrq,zsyxzrq,fzdw,fzrq,bz,case when sfzzz=0 then '否' else '是' end sfzzz from uepp_qyzs where rowid=@pRowID";
            sp.Add("@pRowID", ID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        public DataTable ReadZsxx(string zsjlId)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select zslx,zsbh,zsyxqrq,zsyxzrq,fzdw,fzrq,bz,case when sfzzz=0 then '否' else '是' end sfzzz from uepp_qyzs where zsjlId=@zsjlId";
            sp.Add("@zsjlId", zsjlId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 资质名称树结构
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string getZTreeOfZzmc()
        {

            string sql = @"select codetype,CODE,codeinfo from uepp_code where codetype=@codetype and CODE in ('10','11','12','20')  order by CODE  ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@codetype", "企业资质序列");
            DataTable dtZzxl = DB.ExeSqlForDataTable(sql, spc, "t");
            spc.Clear();
            string sql2 = @"select codetype,code,codeinfo,parentcode from uepp_code where parentcodetype=@parentcodetype and codetype=@codetype and parentcode  in  ('10','11','12','20') order by parentcode, orderid";
            spc.Add("@parentcodetype", "企业资质序列");
            spc.Add("@codetype", "企业资质类别");
            DataTable dtZzlb = DB.ExeSqlForDataTable(sql2, spc, "t2");
            DataRow[] zzxlRows = dtZzxl.Select("1=1");

            string resultJosn = string.Empty;

            if (zzxlRows.Length > 0)
            {
                resultJosn = @"[";
                for (int i = 0; i < dtZzxl.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultJosn += @"{""id"":" + dtZzxl.Rows[i]["code"] + @",""name"":""" + dtZzxl.Rows[i]["codeinfo"] + @""",""open"":""false"",""isParent"":true";
                    }
                    else
                    {
                        resultJosn += @",{""id"":" + dtZzxl.Rows[i]["code"] + @",""name"":""" + dtZzxl.Rows[i]["codeinfo"] + @""",""open"":""false"",""isParent"":true";
                    }
                    DataTable tempdtZzlb = dtZzlb.Select("parentcode=" + dtZzxl.Rows[i]["code"]).CopyToDataTable();
                    if (tempdtZzlb.Rows.Count > 0)
                    {
                        resultJosn += @",""children"":[";
                        for (int j = 0; j < tempdtZzlb.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                resultJosn += @"{""id"":" + tempdtZzlb.Rows[j]["code"] + @",""name"":""" + tempdtZzlb.Rows[j]["codeinfo"] + @""",""isLeaf"":true }";
                            }
                            else
                            {
                                resultJosn += @",{""id"":" + tempdtZzlb.Rows[j]["code"] + @",""name"":""" + tempdtZzlb.Rows[j]["codeinfo"] + @""",""isLeaf"":true }";
                            }
                        }
                        resultJosn += "]";
                    }
                    resultJosn += "}";
                }
                resultJosn += "]";
            }

            return resultJosn;
        }


        public string GetCsywlx(string qyID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            string csywlx = "";
            string sql = "select distinct csywlx from uepp_qycsyw where qyid=@qyID and DataState != -1";
            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    csywlx += dt.Rows[i]["csywlx"].ToString() + "<br />";
                }
            }

            return csywlx;
        }


        #endregion

        public DataTable  getCsywlxid(string qyID)
        {
            string sql = "select a.csywlxid from uepp_qycsyw a inner join uepp_qyjbxx b on a.qyid=b.qyid where b.qyid=@qyid ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyid", qyID);
            return DB.ExeSqlForDataTable(sql, sp,"dt");
        }

        public void UpdateRegArea(string qyID, string city, string county, string tyshxydm)
        {
            string sql = " update [dbo].[UEPP_Qyjbxx] set City=@city,County=@county ,tyshxydm=@tyshxydm WHERE qyID=@qyID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@city", city);
            sp.Add("@county", county);
            sp.Add("@qyID", qyID);
            sp.Add("@tyshxydm", tyshxydm);
            this.DB.ExecuteNonQuerySql(sql, sp);
        }

    }
}
