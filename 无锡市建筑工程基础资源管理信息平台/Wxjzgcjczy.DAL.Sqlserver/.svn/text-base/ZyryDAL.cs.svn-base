using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class ZyryDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveZyryJbxx(string rylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " ryID  ";
           
            string ryzyzglxID = "";
            switch (rylx)
            {
                case "zczyry":
                    ryzyzglxID = "1,2,21,41,51,61";
                    break;
                case "aqscglry":
                    ryzyzglxID = "4,5,6";
                    break;
                case "qyjjry":
                    ryzyzglxID = "20";
                  
                    break;
                case "zygwglry":
                    ryzyzglxID = "7,8,9,11,12,14,15,16,17,18,22,42";
                    break;
                default:
                    ryzyzglxID = "''";
                    break;
            }
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

//            string sql = @" select  *,(case when SbToStState=1 then '上报失败' when SbToStState=0 then  '上报成功'  else '未上报' end) SbState from ( 
//select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
//case when (a.AJ_EXISTINIDCARDS is null or a.AJ_EXISTINIDCARDS='0') then '作废' when a.AJ_EXISTINIDCARDS='-1' then '冲突' when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
//c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
// ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState
// from uepp_ryjbxx a inner join (select distinct ryid,qyid,ryzyzglxid
// from uepp_qyry 
// where ryzyzglxid in ( " + ryzyzglxID + @" )) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid  
// where  1=1  ";

//            string sql = @" select  *,(case when SbToStState=1 then '上报失败' when SbToStState=0 then  '上报成功'  else '未上报' end) SbState from ( 
//select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
//case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
//c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
// ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState
// from uepp_ryjbxx a inner join (select distinct  ryid,qyid
// from uepp_qyry 
// where ryzyzglxid in ( " + ryzyzglxID + @" ) ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid  
// where  1=1 ) AA ";


            string sql = @"   select  *,(case when SbToStState=1 then '上报失败' when SbToStState=0 then  '上报成功'  else '未上报' end) SbState from ( 
select   a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,
case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
 ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState

 from uepp_ryjbxx a left join (select distinct ryid,qyid from uepp_qyry 
 where ryzyzglxid in ( " + ryzyzglxID + " ) ";

            string _ryzyzglxID = ft.GetValue("ryzyzglxID");
            if (!string.IsNullOrEmpty(_ryzyzglxID.Trim()))

            {
                sql += " and ryzyzglxid in (" + _ryzyzglxID + ")";
                ft.Remove("ryzyzglxid");
            }

            sql += " ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid where  1=1 ";

            string zsbh = ft.GetValue("zsbh");
            if (!string.IsNullOrEmpty(zsbh))
            {
                sql += " and a.ryid in (select ryid from uepp_ryzs where zsbh like'%" + zsbh + "%') ";
                ft.Remove("zsbh");
                //ft.Translate();
            }


            sql += ") ryxx where 1=1 and  ";




            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText.Trim();
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable GetRyzyzg(string ryid, string rylx)
        {
            string ryzyzglxID = "";

           
            switch (rylx)
            {
                case "zczyry":
                    ryzyzglxID = "1,2,21,41,51,61";
                    break;
                case "aqscglry":
                    ryzyzglxID = "4,5,6";
                    break;
                case "qyjjry":
                    ryzyzglxID = "11,12";
                    break;
                case "zygwglry":
                    ryzyzglxID = "7,8,9,11,12,14,15,16,17,18,22,42";
                    break;
                default:
                    ryzyzglxID = "' '";
                    break;
            }
            //            string sql = @" select a.zsbh,b.zyzgdj from uepp_ryzs a left join UEPP_Ryzymx b on a.ryid=b.ryid and a.ryzyzglxID=b.ryzyzglxID 
            //  where  a.ryid=:ryid and a.ryzyzglxid in (" + ryzyzglxID + ")";
            string sql = "select distinct a.ryzyzglx,a.zsbh from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid=@ryid and b.ryzyzglxid in (" + ryzyzglxID + ")";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryid", ryid);
            //sp.Add(":ryzyzglxID", ryzyzglxID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable GetRyzyzgByRyids(string ryid, string rylx)
        {
            string ryzyzglxID = "";
            string sql = "";
            switch (rylx)
            {
                case "zczyry":
                    ryzyzglxID = "1,2,21,41,51,61";

                    sql = @"select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a  
inner join uepp_ryzyzg b on a.ryid=b.ryid and  a.ryzyzglxID=b.ryzyzglxID where b.ryid in " + ryid + " and a.ryzslxID in (11,12,21,22,91,131,151,511,161) and b.ryzyzglxid in (" + ryzyzglxID + ") ";

                    break;
                case "aqscglry":
                    ryzyzglxID = "4,5,6";
                    sql = "select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid in " + ryid + " and a.ryzslxID in (41,42,43) and b.ryzyzglxid in (" + ryzyzglxID + ")";
                    break;
                case "qyjjry":
                    ryzyzglxID = "11,12";
                    sql = "select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid in " + ryid + " and a.ryzslxID in (82,83)  and b.ryzyzglxid in (" + ryzyzglxID + ")";
                    break;
                case "zygwglry":
                    ryzyzglxID = "7,8,9,11,12,14,15,16,17,18,22,42";
                    sql = "select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid in " + ryid + " and a.ryzslxID in (82,83,84,85,89,86,87,88,101,141,51,61,71) and b.ryzyzglxid in (" + ryzyzglxID + ")";
                    break;
                default:
                    ryzyzglxID = "' '";
                    sql = "select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid in " + ryid + " and b.ryzyzglxid in ('" + ryzyzglxID + "')";
                    break;
            }

            //            string sql = @" select a.zsbh,b.zyzgdj from uepp_ryzs a left join UEPP_Ryzymx b on a.ryid=b.ryid and a.ryzyzglxID=b.ryzyzglxID 
            //  where  a.ryid=:ryid and a.ryzyzglxid in (" + ryzyzglxID + ")";
           
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //sp.Add(":ryid", ryid);
            //sp.Add(":ryzyzglxID", ryzyzglxID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable ReadRyxx(string ryID)
        {
            string sql = @"  select * from uepp_ryjbxx where ryID=@ryID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadRyxxView(string ryID)
        {
            string sql = @"select c.*,a.ryzslx,a.ryzyzglx,a.zyzgdj,a.zyzglb,b.zsbh,b.zsyxqrq,b.zsyxzrq,b.fzdw,b.fzrq  from uepp_ryjbxx c
  left join UEPP_Ryzymx a on c.ryID=a.ryID and a.zzbz='主项'
  left join  UEPP_Ryzs b on c.ryID=b.ryID and b.sfzzz=1 
   where c.ryID=@ryID  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable RetrieveCjxm(string ryID)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            string sql = @"select  a.*,b.xm,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,
 c.PKID as LxPKID,c.PrjName as LxPrjName,e.qyID,ISNULL(d.jsdwID,'') jsdwID
 from  TBContractRecordManage  a 
 left join Uepp_Ryjbxx b on a.IDCard=b.zjhm and b.DataState<>-1
 inner  join TBProjectInfo c on c.PrjNum=a.PrjNum
 left join UEPP_Jsdw d on d.zzjgdm=a.PropietorCorpCode  
 inner join UEPP_Qyjbxx e on e.zzjgdm=a.ContractorCorpCode 
 where  a.UpdateFlag='U' and b.ryID=@ryID ";

            return DB.ExeSqlForDataTable(sql, sp, "t");
            
//            string ryid = "", ryzyzglx = "", zjhm = "";
//            sql = @"select distinct a.ryid,a.zjhm,b.ryzyzglxid,b.ryzyzglx from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid  where a.ryID=@ryID order by ryzyzglxid ";
//       c
//            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t1");
//            DataTable modle = new DataTable();
//            if (dt.Rows.Count > 0)
//            {
//                //if (dt.Rows.Count == 1)
//                //{
//                ryid = dt.Rows[0]["ryid"].ToString();
//                zjhm = dt.Rows[0]["zjhm"].ToString();
//                ryzyzglx = dt.Rows[0]["ryzyzglx"].ToString();
//                //}
//                //else
//                //{
//                //sp.RemoveAt(0);
//                //SqlParameterCollection spc = DB.CreateSqlParameterCollection();
//                //foreach (DataRow dr in dt.Rows)
//                //{

//                //ryid = dr["ryid"].ToString();
//                //zjhm = dr["zjhm"].ToString();
//                //ryzyzglx = dr["ryzyzglx"].ToString();

//                switch (ryzyzglx)
//                {
//                    case "注册建造师":
//                    case "小型项目管理师":
//                        sql = @"
//select a.*,(select CodeInfo from tbPrjStructureTypeDic where Code=a.gcgkJglx) as jglx,ISNULL(b.jsdwID,'') as jsdwID
//,ISNULL(c.ryID,'') as ryID ,ISNULL(d.qyID,'') as qyID
// from aj_gcjbxx a
// left join Uepp_Jsdw b on a.zbdwDwdm=b.zzjgdm and b.DataState<>-1
// inner join Uepp_Ryjbxx c on a.zbdwZcjzsdm=c.zjhm and c.DataState<>-1
// inner join Uepp_Qyjbxx d on a.zbdwDwdm=d.zzjgdm and d.DataState<>-1 
//  where UpdateFlag='U'  and c.ryID=@ryID";

//                        break;
//                    //case "A类安全生产考核证":
//                    //case "B类安全生产考核证":
//                    //case "安全员(C类人员)":
//                    case "企业A类人员":
//                    case "企业B类人员":
//                    case "企业C类人员":
//                        sql = @" select a.*,(select CodeInfo from tbPrjStructureTypeDic where Code=a.gcgkJglx) as jglx
// ,ISNULL(b.jsdwID,'') as jsdwID,ISNULL(c.ryID,'') as ryID ,ISNULL(d.qyID,'') as qyID from aj_gcjbxx a
// left join Uepp_Jsdw b on a.zbdwDwdm=b.zzjgdm and b.DataState<>-1
//  inner join Uepp_Ryjbxx c on a.zbdwZcjzsdm=c.zjhm and c.DataState<>-1
//  inner join Uepp_Qyjbxx d on a.zbdwDwdm=d.zzjgdm and d.DataState<>-1 
//  where a.UpdateFlag='U' and a.aqjdbm in (
// select aqjdbm  from  TBProjectBuilderUserInfo a
// inner join UEPP_Ryjbxx b on a.IDCard=b.zjhm 
// where  a.UpdateFlag='U' and b.ryID=@ryID)";

//                        break;

//                    case "注册监理师":
//                        sql = @"select xmid,xmmc,ssdq,sgdw,jsdw,xmjl,cblx,jsdwid,qyid,ryid from(
// select distinct a.xmid,a.xmmc,a.ssdq,a.sgdw,a.jsdw,a.xmjl,a.cblx,b.jsdwid,c.qyid,d.ryid
// from uepp_xmjbxx a inner join UEPP_Sgzcy e on a.sgxmtybh=e.sgxmtybh 
// inner join uepp_ryzyzg f on e.ryid=f.ryid and f.ryzyzglx='注册监理工程师'
// left join uepp_jsdw b on a.jsdwid=b.jsdwid  and b.DataState<>-1
// left join uepp_qyjbxx c on a.sgdwid=c.qyid and c.DataState<>-1 
// left join uepp_ryjbxx d on a.xmjlid=d.ryid and d.DataState<>-1
// where e.ryid=@ryID) ryxx";
//                        break;

//                    default:
//                        sql = @"select a.xmid,xmmc,ssdq,sgdw,jsdw,xmjl,cblx,jsdwid,qyid,ryid from(
// select distinct a.xmid,a.xmmc,a.ssdq,a.sgdw,a.jsdw,a.xmjl,a.cblx,b.jsdwid,c.qyid,d.ryid
// from uepp_xmjbxx a inner join UEPP_Sgzcy e on a.sgxmtybh=e.sgxmtybh 
// inner join uepp_ryzyzg f on e.ryid=f.ryid and f.ryzyzglx=@pRyzyzglx 
// left join uepp_jsdw b on a.jsdwid=b.jsdwid 
// left join uepp_qyjbxx c on a.sgdwid=c.qyid  left join uepp_ryjbxx d on a.xmjlid=d.ryid 
// where e.ryid=@ryID) ryxx";
//                        sp.Add("@pRyzyzglx", ryzyzglx);
//                        break;

                    //}


                    //DataTable dtTemp = DB.ExeSqlForDataTable(sql, spc, "t");
                    //spc.RemoveAt(0);
                    //if (modle.Rows.Count == 0)
                    //    modle = dtTemp;
                    //else
                    //{
                    //foreach (DataRow drow in dtTemp.Rows)
                    //    modle.Rows.Add(drow.ItemArray);
                    //}
                //}
                //sp.Add("@ryID", ryid);
                //}
            //}

            //            string sql = @"select rownum,xmmc,dd,jsdw,sgdw,jzmj,zj,gm,jsdwrowid,qyrowid from( select distinct b.xmmc,b.dd,b.jsdw,b.sgdw,b.jzmj,b.zj,b.gm,c.rowid jsdwrowid,d.rowid qyrowid from uepp_ryjbxx a inner join uepp_xmjbxx b on a.zjhm=b.xmjlsfzh
            //left join uepp_jsdw c on b.jsdwzzjgdm=c.zzjgdm left join uepp_qyjbxx d on b.sgdwzzjgdm=d.zzjgdm
            //where a.rowid=:pRyID ) xm where 1=1";

            //return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveZyryJbxxViewList(string ryid, string rylx)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            string ryzyzglxID = "";
            switch (rylx)
            {
                case "zczyry":
                    ryzyzglxID = "1,2,21,41,51,61";
                    break;
                case "aqscglry":
                    ryzyzglxID = "4,5,6";
                    break;
                case "qyjjry":
                    ryzyzglxID = "11,12";
                    break;
                case "zygwglry":
                    ryzyzglxID = "7,8,9,11,12,14,15,16,17,18,22,42";
                    break;
                default:
                    ryzyzglxID = "''";
                    break;
            }
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @" select distinct * from (
select  a.Id,a.ryid,a.ryzyzglxID,a.ryzyzglx,b.ryzslxid,b.ryzslx,b.zsbh,b.zsyxqrq,b.zsyxzrq,e.qyid,e.qymc,'查看明细' ckmx ,f.tag  
from UEPP_Ryzyzg a inner join uepp_ryzs b on a.ryid=b.ryid and a.ryzyzglxID=b.ryzyzglxID  
 inner join uepp_ryjbxx d on a.ryid=d.ryid
 inner join (select  distinct qyid,ryid,ryzyzglxID,tag from UEPP_QyRy ) f  on d.ryID=f.ryID 
 left join uepp_qyjbxx e on f.qyid=e.qyid
where a.ryid=@ryID and a.ryzyzglxid in (" + ryzyzglxID + ")) ryzs  order by Id, ryzyzglxID   ";

            sp.Add("@ryID", ryid);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        /// <summary>
        /// 获取人员专业明细信息
        /// </summary>
        /// <param name="ryid"></param>
        /// <param name="ryzyzglxid"></param>
        /// <param name="ryzslxid"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public DataTable RetrieveRyzymx(string ryid, string ryzyzglxid, string ryzslxid, FilterTranslator ft)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @" select id,ryzyzglx,ryzslx,zzbz,zyzglb,zyzgdj,gzfw from UEPP_Ryzymx  where ryid=@ryID and ryzyzglxID=@RyzyzglxID and ryzslxid=@RyzslxID ";

            sp.Add("@ryID", ryid);
            sp.Add("@RyzyzglxID", ryzyzglxid);
            sp.Add("@RyzslxID", ryzslxid);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable RetrieveRyzsmx(string rowid)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @" select * from uepp_ryzymx where rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadRyzs(string rowid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = "select ryzyzglx,ryzslx,zsbh,zsyxqrq,zsyxzrq,fzdw,fzrq,bz,case when sfzzz=0 then '否' else '是' end sfzzz from uepp_ryzs where rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public string GetRyzglxid(string ryID)
        {
            string sql = "select a.ryzyzglxID from uepp_ryzyzg a inner join uepp_ryjbxx b on a.ryid=b.ryid where b.ryID=@ryID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DB.ExeSqlForString(sql, sp);
        }
    }

}
