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

namespace Wxjzgcjczy.DAL
{
    public class SzgcDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveLxbd(string rowid, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select b.rowid row_id,b.xmmc,b.jsdw,b.sgdw,b.xmjl,b.cblx,b.ssdq,c.rowid jsdwrowid,d.rowid qyrowid,e.rowid ryrowid
from uepp_lxxm a inner join uepp_xmjbxx b on a.LxxmTybh=b.LxxmTybh
left join uepp_jsdw c on b.jsdwid=c.jsdwid left join uepp_qyjbxx d on b.sgdwid=d.qyid left join uepp_ryjbxx e on b.xmjlid=e.ryid
where a.rowid=:pRowID";
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveSzgc(string xmlx, AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            switch (xmlx)
            {
                case "lxxm":
                    sql = @"select * from (
select a.rowid row_id,a.xmmc,a.jsdw,a.ssdqid, nvl(a.ssdq,'无') ssdq,a.tzlx,a.lxbm,  nvl(a.lxwh,'无') lxwh,
to_char(a.lxrq,'yyyy-MM-dd') lxrq,b.rowid jsdwrowid from uepp_lxxm a
left join uepp_jsdw b on a.jsdwid=b.jsdwid  
) lxxm where 1=1 and jsdw is not null  and ";
                    break;
                case "gcxm":
                    sql = @"select rownum,row_id,sgxmtybh,xmmc,dd,nvl(ssdq,'无') ssdq,jsdw,sgdw,cblx,xmjl,qymc,jsdwrowid,qyrowid,ryrowid,operatedate from (
select distinct a.rowid row_id,a.sgxmtybh,a.xmmc,a.dd,a.ssdq,a.jsdw,a.sgdw,a.cblx,a.xmjl,case when a.operatedate is null then to_date('1900-01-01','yyyy-MM-dd') else a.operatedate end operatedate,
c.qymc,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
from uepp_xmjbxx a left join uepp_jsdw b on a.jsdwid=b.jsdwid left join uepp_qyjbxx c on a.sgdwid=c.qyid 
left join uepp_ryjbxx d on a.xmjlid=d.ryid) xm where 1=1 and jsdw is not null and ";


                    string bhxm = ft.GetValue("bhxm");
                    if (!string.IsNullOrEmpty(bhxm))
                    {
                        if (bhxm.Contains(','))
                        {
                            sql += "(";
                            string querySQL = "";

                            if (bhxm.Contains("已安监"))
                                querySQL += " sgxmtybh in (select distinct sgxmtybh from uepp_aqjdxx ) ";
                            if (bhxm.Contains("已质监"))
                            {
                                if (!string.IsNullOrEmpty(querySQL.TrimString()))
                                    querySQL += " and ";
                                querySQL += " sgxmtybh in (select distinct sgxmtybh from uepp_zljdxx ) ";
                            }
                            if (bhxm.Contains("已施工许可"))
                            {
                                if (!string.IsNullOrEmpty(querySQL.TrimString()))
                                    querySQL += " and ";
                                querySQL += " sgxmtybh in (select distinct a.sgxmtybh from UEPP_SgxkAndBdRelation a inner join uepp_sgxkxx b on a.sgxkid=b.sgxkid ) ";
                            }
                            if (bhxm.Contains("已竣工验收"))
                            {
                                if (!string.IsNullOrEmpty(querySQL.TrimString()))
                                    querySQL += " and ";
                                querySQL += " sgxmtybh in (select distinct sgxmtybh from uepp_zljdxx where isjg='是' ) ";
                            }
                            sql += querySQL + " ) and ";

                        }
                        else
                        {
                            switch (bhxm)
                            {
                                case "已安监":
                                    sql += " sgxmtybh in (select distinct sgxmtybh from uepp_aqjdxx ) ";
                                    break;
                                case "已质监":
                                    sql += " sgxmtybh in (select distinct sgxmtybh from uepp_zljdxx ) ";
                                    break;
                                case "已施工许可":
                                    sql += " sgxmtybh in (select distinct a.sgxmtybh from UEPP_SgxkAndBdRelation a inner join uepp_sgxkxx b on a.sgxkid=b.sgxkid ) ";
                                    break;
                                case "已竣工验收":
                                    sql += " sgxmtybh in (select distinct sgxmtybh from uepp_zljdxx where isjg='是' ) ";
                                    break;
                            }
                            sql += " and ";
                        }

                        ft.Remove("bhxm");
                        //ft.Translate();
                    }

                    break;

                case "aqjd":
                    sql = @"select distinct * from( 
select a.cblx,a.rowid row_id,b.xmmc,b.jsdwid,b.sgdwid,b.sgdw,b.jsdw,a.ssdqid,nvl(a.ssdq,'无') ssdq,b.aqjdid,b.ryid,nvl(b.xmjl,'无') xmjl,b.aqjddabh,
to_char(case when b.aqjdslsj is null then to_date('1900-01-01','yyyy-MM-dd') else b.aqjdslsj end ,'yyyy-MM-dd') aqjdslsj,b.datastate aqjdstate,
b.aqjdflag ,b.aqjd_status,c.rowid jsdwrowid,d.rowid qyrowid,e.rowid ryrowid, 
case  b.aqjdflag when '4' then '新增待审' when '0' then '在建' when '3' then '竣工' when '2' then '完工未办手续' when '1' then '停工' end sfjg,
case b.aqjd_status when '-1' then '退回' when '0' then '未审' when '1' then '信用审核通过' when '2' then '报监待审' when '3' then '报监审核通过' end sfsh 
 from uepp_aqjdxx b left join uepp_xmjbxx a  on a.sgxmtybh=b.sgxmtybh 
left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid left join uepp_ryjbxx e on a.xmjlid=e.ryid   
) xmxx where 1=1 and jsdw is not null   and ";

                    //string aqbjsj1 = ft.GetValue("aqjdslsj1");
                    //string aqbjsj2 = ft.GetValue("aqjdslsj2");
                    //if (!string.IsNullOrEmpty(aqbjsj1))
                    //{
                    //    sql += " to_date(aqjdslsj,'yyyy-MM-dd')  >= to_date(" + aqbjsj1 + ",'yyyy-MM-dd') and ";
                    //    ft.Remove("aqjdslsj1");

                    //}
                    //if (!string.IsNullOrEmpty(aqbjsj2))
                    //{
                    //    sql += " to_date(aqjdslsj,'yyyy-MM-dd')  <= to_date(" + aqbjsj2 + ",'yyyy-MM-dd') and ";
                    //    ft.Remove("aqjdslsj2");

                    //}
                    string cblx = ft.GetValue("cblx");
                    if (!string.IsNullOrEmpty(cblx))
                    {
                        if (cblx == "总承包")
                            sql += " cblx =:cblx  ";
                        else
                            sql += " cblx <>:cblx ";
                        sql += " and ";
                        sp.Add(":cblx", "总承包");
                        ft.Remove("cblx");
                    }
                    break;
                case "zljd":
                    sql = @"select distinct * from( 
select a.rowid row_id,b.xmmc,b.jsdwid,b.sgdwid,b.sgdw,b.jsdw,b.ssdqid,nvl(a.ssdq,'无') ssdq,b.xmjlid,b.xmjl,b.zljdid,b.zljdbh,b.datastate zljdstate, 
to_char(b.zljdslsj, 'yyyy-MM-dd') zljdslsj,c.rowid jsdwrowid,d.rowid qyrowid,e.rowid ryrowid,b.isjg from uepp_zljdxx b left join uepp_xmjbxx a  on a.sgxmtybh=b.sgxmtybh  
left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid left join uepp_ryjbxx e on a.xmjlid=e.ryid   
) xmxx where 1=1 and jsdw is not null and  ";

                    string zlbjsj1 = ft.GetValue("zljdslsj1");
                    string zlbjsj2 = ft.GetValue("zljdslsj2");
                    if (!string.IsNullOrEmpty(zlbjsj1))
                    {
                        sql += " to_date(zljdslsj,'yyyy-MM-dd')  >= to_date('" + zlbjsj1 + "','yyyy-MM-dd') and ";
                        ft.Remove("zljdslsj1");
                        //ft.Translate();
                    }
                    if (!string.IsNullOrEmpty(zlbjsj2))
                    {
                        sql += " to_date(zljdslsj,'yyyy-MM-dd')  <= to_date('" + zlbjsj2 + "','yyyy-MM-dd') and ";
                        ft.Remove("zljdslsj2");
                        //ft.Translate();
                    }
                    break;
                case "sgxk":
                    //                    sql = @"select distinct * from( 
                    //select a.xmid,a.xmmc,a.jsdwid,a.sgdwid,a.sgdw,a.jsdw,a.ssdqid,a.ssdq,c.sgxkid,c.sgxkglbm,c.sgxkzbh,to_char(c.sgxkslsj,'yyyy-mm-dd') sgxkslsj 
                    //from UEPP_SgxkAndBdRelation b  inner join  uepp_sgxkxx c on b.sgxkid=c.sgxkid  and b.datastate=0 left join uepp_xmjbxx a  on a.sgxmtybh=b.sgxmtybh 
                    //) xmxx where 1=1 and  ";
                    sql = @"select distinct * from( 
select a.rowid row_id,c.xmmc,c.sgdw,c.jsdw,a.ssdqid,nvl(a.ssdq,'无') ssdq,c.sgxkid,c.sgxkglbm,c.sgxkzbh,
to_char(case when c.sgxkslsj is null then to_date('1900-01-01','yyyy-MM-dd') else c.sgxkslsj end ,'yyyy-MM-dd') sgxkslsj,c.datastate sgxkstate,
 d.rowid jsdwrowid,e.rowid qyrowid,f.rowid ryrowid  from 
 UEPP_SgxkAndBdRelation b  inner join  uepp_sgxkxx c on b.sgxkid=c.sgxkid and b.datastate=0  left join uepp_xmjbxx a  on a.sgxmtybh=b.sgxmtybh 
left join uepp_jsdw d on a.jsdwid=d.jsdwid left join uepp_qyjbxx e on a.sgdwid =e.qyid  left join uepp_ryjbxx f on a.xmjlid=f.ryid   
) xmxx where 1=1  and  ";

                    string bjsj1 = ft.GetValue("sgxkslsj1");
                    string bjsj2 = ft.GetValue("sgxkslsj2");
                    if (!string.IsNullOrEmpty(bjsj1))
                    {
                        sql += " to_date(sgxkslsj,'yyyy-MM-dd')  >= to_date('" + bjsj1 + "','yyyy-MM-dd') and ";
                        ft.Remove("sgxkslsj1");
                        //ft.Translate();
                    }
                    if (!string.IsNullOrEmpty(bjsj2))
                    {
                        sql += " to_date(sgxkslsj,'yyyy-MM-dd')  <= to_date('" + bjsj2 + "','yyyy-MM-dd') and ";
                        ft.Remove("sgxkslsj2");
                        //ft.Translate();
                    }

                    break;

            }

            string ssdq = ft.GetValue("ssdq");
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
                            sql += " ssdq like :pp1" + i.ToString() + " or ssdq like :pp2" + i.ToString() + "";
                            sp.Add(":pp1" + i.ToString(), "%市区%");
                            sp.Add(":pp2" + i.ToString(), "%市辖区%");
                        }
                        else
                        {
                            sql += "  ssdq like :pp3" + i.ToString();
                            sp.Add(":pp3" + i.ToString(), "%" + strSsdq[i] + "%");
                        }
                    }
                    sql += " ) and ";

                }
                else
                {
                    if (ssdq == "市区")
                    {
                        sql += "  (ssdq like :pp1 or ssdq like :pp2)  ";
                        sp.Add(":pp1", "%市区%");
                        sp.Add(":pp2", "%市辖区%");

                    }
                    else
                    {
                        sql += "  ssdq like :pp3";
                        sp.Add(":pp3", "%" + ssdq + "%");

                    }
                    sql += " and ";
                }
                ft.Remove("ssdq");
                //ft.Translate();
            }

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveGcxmAqjd(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select b.rowid row_id,b.aqjdglbm,b.aqjdslr,b.aqjddabh,b.aqjdflag,to_char(b.aqjdslsj,'yyyy-MM-dd') aqjdslsj
from uepp_xmjbxx a inner join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh 
where a.rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveGcxmHtba(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select b.rowid row_id,b.sghtbabh,b.sghtbaglbm,b.sghtbaslr,to_char(b.sghtbaslsj,'yyyy-MM-dd') sghtbaslsj,sghtjg,sghtgq from uepp_xmjbxx a inner join UEPP_sghtbaxx b on a.sgxmtybh=b.sgxmtybh 
where a.rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveGcxmZljd(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select b.rowid row_id,b.zljdglbm,b.zljdslr,b.zljdbh,to_char(b.zljdslsj,'yyyy-MM-dd') zljdslsj,b.isjg,:dwgc dwgc   
from uepp_xmjbxx  a  inner join uepp_zljdxx b on a.sgxmtybh=b.sgxmtybh
where a.rowid=:pRowID";
            sp.Add(":dwgc", "单位工程");
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveGcxmSgxk(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select c.rowid row_id,c.sgxkzbh,c.sgxkglbm,c.sgxkslr,to_char(c.sgxkslsj,'yyyy-MM-dd') sgxkslsj,
to_char(c.sghtkgrq,'yyyy-MM-dd') sghtkgrq,to_char(c.sghtjgrq,'yyyy-MM-dd') sghtjgrq 
from uepp_xmjbxx a inner join UEPP_SgxkAndBdRelation b on a.sgxmtybh=b.sgxmtybh
inner join uepp_sgxkxx c on b.sgxkid=c.sgxkid
where a.rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveGcxmJgys(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select b.rowid row_id,b.jgbaglbm,b.jgbaslr,b.jgbabh,to_char(b.jgbaslsj,'yyyy-MM-dd') jgbaslsj,b.isjg  
from uepp_xmjbxx  a  inner join uepp_zljdxx b on a.sgxmtybh=b.sgxmtybh
where a.rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveDwgcList(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select rownum,c.rowid row_id,c.mc,c.jdzch, c.dwgczj,c.jclx_com,c.mj,c.cs   
from uepp_zljdxx a inner join UEPP_ZljdAndDwgc b on a.zljdid=b.zljdid inner join uepp_dwgc c on b.dwgcid=c.dwgcid
where a.rowid=:pRowID";

            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable ReadDwgc(string rowID)
        {
            string sql = "select * from uepp_dwgc where rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadHtba(string rowID)
        {
            string sql = "select b.* from uepp_xmjbxx a inner join UEPP_sghtbaxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveAqbjxmzcy(string rowID, string befrom)
        {
            string sql = "";
            //if (befrom == "toolbar")
            sql = @"select  rownum,qymc,xm,zzlb,zzdj,zyzgzsh,rygw from(
select distinct d.rowid row_id, a.qymc,a.xm,a.zzlb,a.zzdj,a.zyzgzsh,c.codeinfo rygw from uepp_xmjbxx d inner join UEPP_Sgzcy a on a.sgxmtybh=d.sgxmtybh inner join uepp_aqjdxx b on a.sgxmtybh =b.sgxmtybh 
left join uepp_code c on a.gw=c.code and c.codetype=:pCodeType
) cyxx where  row_ID=:pRowID";
            //            else
            //                sql = @"select  rownum,qymc,xm,zzlb,zzdj,zyzgzsh,rygw from(
            //select distinct b.rowid row_id, a.qymc,a.xm,a.zzlb,a.zzdj,a.zyzgzsh,c.codeinfo rygw from UEPP_Sgzcy a inner join uepp_aqjdxx b on a.sgxmtybh =b.sgxmtybh 
            //left join uepp_code c on a.gw=c.code and c.codetype=:pCodeType
            //) cyxx where  row_ID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pCodeType", "项目组岗位类别");
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveAqjdZljd(string rowID)
        {
            string sql = @" select rownum,row_id,xmmc,jdzch,zljdglbm,zljdslr,zljdslsj from (
              select distinct a.rowid row_id,e.xmmc,c.jdzch,a.zljdglbm,a.zljdslr,a.zljdslsj  from uepp_zljdxx a inner join UEPP_ZljdAndDwgc b on a.zljdid=b.zljdid  inner join UEPP_dwgc c on b.dwgcid=c.dwgcid 
inner join uepp_aqjdxx d on a.sgxmtybh=d.sgxmtybh inner join uepp_xmjbxx e on a.sgxmtybh=e.sgxmtybh where d.rowID=:pRowID 
              ) zljdxm where 1=1";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveAqjdSgxk(string rowID)
        {
            string sql = "select rownum,a.rowid,a.*,d.xmmc from uepp_sgxkxx a inner join UEPP_SgxkAndBdRelation b on a.sgxkid=b.sgxkid inner join uepp_aqjdxx c on b.sgxmtybh=c.sgxmtybh inner join uepp_xmjbxx d on c.sgxmtybh=d.sgxmtybh where c.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveAqjdJgba(string rowID)
        {
            string sql = "select rownum,a.*,b.xmmc from uepp_zljdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh inner join uepp_aqjdxx c on b.sgxmtybh=c.sgxmtybh where c.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveZljdDtgc(string rowID, string befrom)
        {
            string sql = "";
            if (befrom == "toolbar")
                sql = "select rownum,a.* from  uepp_dwgc a inner join UEPP_ZljdAndDwgc b on a.dwgcid=b.dwgcid inner join uepp_zljdxx c on b.zljdid=c.zljdid inner join uepp_xmjbxx d on c.sgxmtybh=d.sgxmtybh  where d.rowID=:pRowID";
            else
                sql = "select rownum,a.* from uepp_dwgc a inner join UEPP_ZljdAndDwgc b on a.dwgcid=b.dwgcid inner join uepp_zljdxx c on b.zljdid=c.zljdid where c.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveZljdSgxk(string rowID)
        {
            string sql = "select rownum,a.*,d.xmmc from uepp_sgxkxx a inner join UEPP_SgxkAndBdRelation b on a.sgxkid=b.sgxkid inner join uepp_zljdxx c on b.sgxmtybh=c.sgxmtybh inner join uepp_xmjbxx d on c.sgxmtybh=d.sgxmtybh where c.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveZljdJgba(string rowID)
        {
            string sql = "select rownum,a.*,b.xmmc from uepp_zljdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveSgxk(string rowID)
        {
            string sql = "select rownum,a.*,c.xmmc from uepp_sgxkxx a inner join UEPP_SgxkAndBdRelation b on a.sgxkid=b.sgxkid  inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh where a.rowID=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveSgxkJgba(string rowID)
        {
            string sql = @"select rownum,a.*,b.xmmc from uepp_zljdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh inner join UEPP_SgxkAndBdRelation c on b.sgxmtybh=c.sgxmtybh
                           left join uepp_sgxkxx d on c.sgxkid=d.sgxkid   where d.rowID=:pRowID";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        public DataTable ReadXmxx(string rowID)
        {
            string sql = "select * from uepp_xmjbxx where rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadXmxx(string xmlx, string rowID, string befrom)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            switch (xmlx)
            {
                case "lxxm":
                    sql = "select a.*,b.rowid jsdwrowid from uepp_lxxm a left join uepp_jsdw b on a.jsdwid=b.jsdwid  where a.rowid=:pRowID";
                    break;
                case "gcxm":
                    sql = @"select  a.*,c.rowid jsdwrowid, c.dwdz,c.lxdh,c.fddbrdh,c.zzjgdm jsdwzzjgdm,d.rowid sgdwrowid, d.sylx,d.yyzzzch,d.county,d.zzzsh,d.xxdd,
d.lxdh sgdwlxdh,d.fddbr sgdwfddbr,d.fddbrdh sgdwfddbrdh,d.jjxz sgdwjjxz,d.zzjgdm sgdwzzjgdm1,
d.qyfzr sgdwqyjl,d.yzbm,d.zxwtfr,d.zxwtfrdh,d.zxjsfzr,d.zxjsfzrdh,d.zxaqfzr,d.zxaqfzrdh,
d.aqfzr,d.aqfzrdh,d.zyfw,d.jyfw,f.rowid ryrowid,e.jldwrowid
from  uepp_xmjbxx a  left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid 
left join uepp_ryjbxx f on a.xmjlid =f.ryid  
left join (select h.rowid jldwrowid, h.qymc,h.qyid,g.sgxmtybh from  uepp_qyjbxx h inner join UEPP_xmcjdwxx g on h.qyid=g.qyid and g.cjdwlxID= 4 )
 e on a.sgxmtybh=e.sgxmtybh 
where a.rowid=:pRowID";

                    //                    sql = @"select  a.*,b.kgrq,b.jgrq,c.rowid jsdwrowid, c.dwdz,c.lxdh,c.fddbrdh,d.rowid sgdwrowid, d.sylx,d.yyzzzch,d.county,d.zzzsh,d.xxdd,d.lxdh sgdwlxdh,d.fddbr sgdwfddbr,d.fddbrdh sgdwfddbrdh,d.jjxz sgdwjjxz,
                    //d.qyfzr sgdwqyjl,d.yzbm,d.zxwtfr,d.zxwtfrdh,d.zxjsfzr,d.zxjsfzrdh,d.zxaqfzr,d.zxaqfzrdh,d.aqfzr,d.aqfzrdh,d.zyfw,d.jyfw,b.jldw,e.zzjgdm jldwzzjgdm,b.xmjl,b.ryid,b.jldwid,
                    //e.fddbr jldwfddbr,e.fddbrdh jldwfddbrdh,e.xxdd jldwdz,e.lxdh jldwdh,b.xmzj,b.xmzjzzzsh,b.xmzjdh,b.zjdb,b.zjdbzsh,b.zjdbdh,b.aqjdglbm,b.aqjdslr,b.aqjdslsj,
                    //b.aqjdjar,b.aqjdjasj,  b.aqjddabh,b.aqjdflag,b.xgr,b.xgrqsj,b.ghxkz,b.Aqwmsg,b.Mtaqwm,b.Sbbab,b.Sgzt,f.lxdh xmjldh,f.rowid ryrowid,c.rowid jsdwrowid,e.rowid jldwrowid      
                    //from  uepp_xmjbxx a left join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid 
                    //left join uepp_qyjbxx  e on b.jldwid=e.qyid left join uepp_ryjbxx f on b.ryid =f.ryid  
                    //where a.rowid=:pRowID";

                    break;
                case "aqjd":
                    //if (befrom == "toolbar")
                    sql = @"select b.*,b.tag tag1 from uepp_xmjbxx a inner join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
                    //                    else
                    //                        sql = @"select  a.*,b.kgrq,b.jgrq,c.rowid jsdwrowid, c.dwdz,c.lxdh,c.fddbrdh,d.rowid sgdwrowid, d.sylx,d.yyzzzch,d.county,d.zzzsh,d.xxdd,d.lxdh sgdwlxdh,d.fddbr sgdwfddbr,d.fddbrdh sgdwfddbrdh,d.jjxz sgdwjjxz,
                    //d.qyfzr sgdwqyjl,d.yzbm,d.zxwtfr,d.zxwtfrdh,d.zxjsfzr,d.zxjsfzrdh,d.zxaqfzr,d.zxaqfzrdh,d.aqfzr,d.aqfzrdh,d.zyfw,d.jyfw,b.jldw,e.zzjgdm jldwzzjgdm,b.xmjl,b.ryid,b.jldwid,
                    //e.fddbr jldwfddbr,e.fddbrdh jldwfddbrdh,e.xxdd jldwdz,e.lxdh jldwdh,b.xmzj,b.xmzjzzzsh,b.xmzjdh,b.zjdb,b.zjdbzsh,b.zjdbdh,b.aqjdglbm,b.aqjdslr,b.aqjdslsj,
                    //b.aqjdjar,b.aqjdjasj,  b.aqjddabh,b.aqjdflag,b.xgr,b.xgrqsj,b.ghxkz,b.Aqwmsg,b.Mtaqwm,b.Sbbab,b.Sgzt,f.lxdh xmjldh,f.rowid ryrowid,c.rowid jsdwrowid,e.rowid jldwrowid      
                    //from uepp_aqjdxx b  inner join uepp_xmjbxx a on a.sgxmtybh=b.sgxmtybh left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid 
                    //left join uepp_qyjbxx  e on b.jldwid=e.qyid left join uepp_ryjbxx f on b.ryid =f.ryid where a.rowid=:pRowID";

                    break;

                case "zljd":
                    //if (befrom == "toolbar")
                    sql = @"select b.* from uepp_xmjbxx a inner join uepp_zljdxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
                    //                        else
                    //                        sql = @"
                    //select a.*,b.zljdglbm,b.zljdslr,b.zljdslsj,b.zljdbh,b.jsdw,b.jsdwid,b.kcdw,b.kcdwid,b.xgr,b.xgrqsj,b.sjdw,b.sjdwid,b.sgdw,b.sgdwid,b.jldw,b.jldwid,b.jcdw,b.jcdwid,b.cfb,
                    //b.bjfw,b.bjfb,c.rowid jsdwrowid,c.lxdh jsdwlxdh,d.lxdh kcdwlxdh,e.rowid sjdwrowid,e.lxdh sjdwlxdh,f.lxdh sgdwlxdh,g.lxdh jldwlxdh,h.lxdh jcdwlxdh ,k.rowid ryrowid,
                    // d.rowid kcdwrowid,f.rowid sgdwrowid,g.rowid jldwrowid,h.rowid jcdwrowid 
                    //from uepp_zljdxx b inner join uepp_xmjbxx a  on a.sgxmtybh=b.sgxmtybh left join uepp_jsdw c on b.jsdwid=c.jsdwid left join uepp_qyjbxx d on b.kcdwid=d.qyid 
                    //left join uepp_qyjbxx e on b.sjdwid=e.qyid left join uepp_qyjbxx f on a.sgdwid=f.qyid left join uepp_qyjbxx g on b.jldwid=g.qyid left join uepp_qyjbxx h
                    //on b.jcdwid=h.qyid left join uepp_ryjbxx k on a.xmjlid=k.ryid   
                    //where a.rowid=:pRowID";

                    break;

                case "sgxk":
                    //if (befrom == "toolbar")
                    sql = @"select c.* from uepp_xmjbxx a inner join   UEPP_SgxkAndBdRelation b on a.sgxmtybh=b.sgxmtybh inner join uepp_sgxkxx c on b.sgxkid=c.sgxkid 
where c.rowid=:pRowID";
                    //else
                    //    sql = @"select * from  uepp_sgxkxx where rowid=:pRowID";


                    break;
                case "jgys":

                    //if (befrom == "toolbar")
                    sql = @"select b.* from uepp_xmjbxx a inner join uepp_zljdxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
                    //else
                    //    sql = @"select * from  uepp_zljdxx where rowid=:pRowID";
                    break;
            }
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadXmjl(string ryid)
        {
            string sql = @"select distinct * from (
select ryzslxid,ryzslx,zsbh from uepp_ryzs where ryzslxid=11 and datastate=0 and ryid=:ryid 
union all 
select ryzslxid,ryzslx,zsbh from uepp_ryzs where ryzslxid=42  and datastate=0 and ryid=:ryid  
) xmjl where 1=1  order by ryzslxid ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":ryid", ryid);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadSgxkzs(string rowID, string befrom)
        {
            //            string sql = @"select a.sgxkglbm,a.sgxkslsj,a.sgxkzbh,a.sghtkgrq,a.sghtjgrq,a.sghtjg,b.xmmc,b.dd,b.jsdw,b.gm,a.sjdw,a.sgdw,a.jldw 
            //from uepp_sgxkxx a left join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID";
            string sql = "";
            if (string.IsNullOrEmpty(befrom))
                sql = "select * from uepp_sgxkxx where rowid=:pRowID";
            else sql = "select a.* from uepp_sgxkxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where b.rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable GetZljdzs(string rowid, string befrom)
        {
            string sql = "";
            if (string.IsNullOrEmpty(befrom))
                sql = "select a.rowid ,a.zljdbh,b.sgdw,b.xmmc from uepp_zljdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.rowid=:pRowID ";
            else
                sql = "select a.rowid ,a.zljdbh,b.sgdw,b.xmmc from uepp_zljdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh  where b.rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

    }
}
