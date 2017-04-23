using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL
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
                    ryzyzglxID = "71,72";
                    break;
                case "zygwglry":
                    ryzyzglxID = "7,8,9,11,12,14,15,16,17,18,22,42";
                    break;
                default:
                    ryzyzglxID = "''";
                    break;
            }
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @"  select  * from ( 
select   a.rowid row_id, a.ryid,a.xm,a.zjlx,a.zjhm,a.AJ_EXISTINIDCARDS,
case when (a.AJ_EXISTINIDCARDS is null or a.AJ_EXISTINIDCARDS='0') then '作废' when a.AJ_EXISTINIDCARDS='-1' then '冲突' when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,a.sylx,nvl(a.zcjb,'无') zcjb,nvl(nvl(a.lxdh,a.yddh),'0') lxdh,a.datastate,nvl(c.county,'无') county,c.provinceid,c.province,c.rowid qyrowid     
 from uepp_ryjbxx a inner join (select distinct ryid,qyid,ryzyzglxid
 from uepp_qyry 
 where ryzyzglxid in ( " + ryzyzglxID + @" ) ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid  
 where  1=1  ";
            string _ryzyzglxID = ft.GetValue("ryzyzglxID");
            if (!string.IsNullOrEmpty(_ryzyzglxID))
            {
                sql += " and ryzyzglxid in (" + _ryzyzglxID + ")";
                ft.Remove("ryzyzglxid");
            }

            
            string zsbh = ft.GetValue("zsbh");
            if (!string.IsNullOrEmpty(zsbh))
            {
                sql += " and a.ryid =(select ryid from uepp_ryzs where zsbh like'%" + zsbh + "%') ";
                ft.Remove("zsbh");
                //ft.Translate();
            }


            sql += ") ryxx where 1=1 and ";


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
            string sql = "select distinct a.ryzyzglx,a.zsbh from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid=:ryid and b.ryzyzglxid in (" + ryzyzglxID + ")";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":ryid", ryid);
            //sp.Add(":ryzyzglxID", ryzyzglxID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable GetRyzyzgByRyids(string ryid, string rylx)
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
            string sql = "select distinct a.ryzyzglx,a.zsbh,a.ryid from uepp_ryzs a inner join uepp_ryzyzg b on a.ryid=b.ryid where b.ryid in " + ryid + " and b.ryzyzglxid in (" + ryzyzglxID + ")";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //sp.Add(":ryid", ryid);
            //sp.Add(":ryzyzglxID", ryzyzglxID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable ReadRyxx(string rowID)
        {
            string sql = @"  select * from uepp_ryjbxx where rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveCjxm(string rowID)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            string ryid = "", ryzyzglx = "", zjhm = "";
            sql = @"select distinct a.ryid,a.zjhm,b.ryzyzglxid,b.ryzyzglx from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid  where a.rowid=:pRowID order by ryzyzglxid ";
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t1");
            DataTable modle = new DataTable();
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows.Count == 1)
                //{
                ryid = dt.Rows[0]["ryid"].ToString();
                zjhm = dt.Rows[0]["zjhm"].ToString();
                ryzyzglx = dt.Rows[0]["ryzyzglx"].ToString();
                //}
                //else
                //{
                sp.RemoveAt(0);
                //SqlParameterCollection spc = DB.CreateSqlParameterCollection();
                //foreach (DataRow dr in dt.Rows)
                //{

                //ryid = dr["ryid"].ToString();
                //zjhm = dr["zjhm"].ToString();
                //ryzyzglx = dr["ryzyzglx"].ToString();

                switch (ryzyzglx)
                {
                    case "注册建造师":
                        sql = @"select rownum,row_id,xmmc,ssdq,sgdw,jsdw,xmjl,cblx,jsdwrowid,qyrowid,ryrowid from ( 
 select distinct a.rowid row_id,a.xmmc,a.ssdq,a.sgdw,a.jsdw,a.xmjl,a.cblx,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
 from uepp_xmjbxx a left join uepp_jsdw b on a.jsdwid=b.jsdwid 
 left join uepp_qyjbxx c on a.sgdwid=c.qyid  left join uepp_ryjbxx d on a.xmjlid=d.ryid 
 where a.xmjlid=:pRyID) ryxx";

                        break;
                    case "注册监理师":
                        sql = @"select rownum,row_id,xmmc,ssdq,sgdw,jsdw,xmjl,cblx,jsdwrowid,qyrowid,ryrowid from(
 select distinct a.rowid row_id,a.xmmc,a.ssdq,a.sgdw,a.jsdw,a.xmjl,a.cblx,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
 from uepp_xmjbxx a inner join UEPP_Sgzcy e on a.sgxmtybh=e.sgxmtybh 
 inner join uepp_ryzyzg f on e.ryid=f.ryid and f.ryzyzglx='注册监理工程师'
 left join uepp_jsdw b on a.jsdwid=b.jsdwid 
 left join uepp_qyjbxx c on a.sgdwid=c.qyid  left join uepp_ryjbxx d on a.xmjlid=d.ryid 
 where e.ryid=:pRyID) ryxx";
                        break;

                    default:
                        sql = @"select rownum,row_id,xmmc,ssdq,sgdw,jsdw,xmjl,cblx,jsdwrowid,qyrowid,ryrowid from(
 select distinct a.rowid row_id,a.xmmc,a.ssdq,a.sgdw,a.jsdw,a.xmjl,a.cblx,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
 from uepp_xmjbxx a inner join UEPP_Sgzcy e on a.sgxmtybh=e.sgxmtybh 
 inner join uepp_ryzyzg f on e.ryid=f.ryid and f.ryzyzglx=:pRyzyzglx 
 left join uepp_jsdw b on a.jsdwid=b.jsdwid 
 left join uepp_qyjbxx c on a.sgdwid=c.qyid  left join uepp_ryjbxx d on a.xmjlid=d.ryid 
 where e.ryid=:pRyID) ryxx";
                        sp.Add(":pRyzyzglx", ryzyzglx);
                        break;

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
                }
                sp.Add(":pRyID", ryid);
                //}
            }

            //            string sql = @"select rownum,xmmc,dd,jsdw,sgdw,jzmj,zj,gm,jsdwrowid,qyrowid from( select distinct b.xmmc,b.dd,b.jsdw,b.sgdw,b.jzmj,b.zj,b.gm,c.rowid jsdwrowid,d.rowid qyrowid from uepp_ryjbxx a inner join uepp_xmjbxx b on a.zjhm=b.xmjlsfzh
            //left join uepp_jsdw c on b.jsdwzzjgdm=c.zzjgdm left join uepp_qyjbxx d on b.sgdwzzjgdm=d.zzjgdm
            //where a.rowid=:pRyID ) xm where 1=1";

            return DB.ExeSqlForDataTable(sql, sp, "t");
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

            string sql = @" select rownum,ryid,ryzyzglxid,ryzyzglx,ryzslxid,ryzslx,zsbh,zsyxqrq,zsyxzrq,qyid,qyrowid,qymc,ckmx from (
select distinct * from (
select  a.ryid,a.ryzyzglxID,a.ryzyzglx,b.ryzslxid,b.ryzslx,b.zsbh,b.zsyxqrq,b.zsyxzrq,d.qyid,e.rowid qyrowid,d.qymc,:ckmx ckmx  
from UEPP_Ryzyzg a inner join uepp_ryzs b on a.ryid=b.ryid  
 inner join uepp_ryjbxx d on a.ryid=d.ryid  
 left join uepp_qyjbxx e on d.qyid=e.qyid 
where a.ryid=:pRyID and a.ryzyzglxid in (" + ryzyzglxID + ") order by rownum, a.ryzyzglxID ) ryzs ) where 1=1    ";

            sp.Add(":pRyID", ryid);
            sp.Add(":ckmx", "查看明细");
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveRyzymx(string ryid, string ryzyzglxid, string ryzslxid, FilterTranslator ft)
        {
            //if (string.IsNullOrEmpty(orderby.Trim()))
            //    orderby = " zzbz  ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @" select rownum,ryzyzglx,ryzslx,zzbz,zyzglb,zyzgdj,gzfw from UEPP_Ryzymx  where ryid=:pRyID and ryzyzglxID=:pRyzyzglxID and ryzslxid=:pRyzslxID ";

            sp.Add(":pRyID", ryid);
            sp.Add(":pRyzyzglxID", ryzyzglxid);
            sp.Add(":pRyzslxID", ryzslxid);
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

        public string GetRyzglxid(string rowid)
        {
            string sql = "select a.ryzyzglxID from uepp_ryzyzg a inner join uepp_ryjbxx b on a.ryid=b.ryid where b.rowid=:pRowID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForString(sql, sp);
        }
    }
}
