using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL
{
    public class SzqyDAL
    {
        public DBOperator DB { get; set; }

        #region 获取表结构
        #endregion

        #region 读取


        public DataTable ReadQyxx(string rowid)
        {
            string sql = @"  select a.rowid,a.*,b.zsyxzrq  jgdmyxq,c.zsbh  zzzsbh,c.fzdw  zzzsfzdw,c.zsyxzrq  zzzsyxq ,c.bz  zzzsbz,
              d.zsbh  yyzzzch, d.zsyxzrq  yyzzzch,e.zsbh  aqscxkzbh,e.fzdw  aqscxkzfzdw ,e.fzrq  aqscxkzfzrq,
              e.zsyxzrq  aqscxkzyxq, f.zsbh  scbh, f.zsyxzrq  xyscyxq,f.fzdw  xyscfzdw, g.zc  frdbzc ,h.zc  qyjlzc,i.zc  jsfzrzc 
               from UEPP_Qyjbxx a left join UEPP_Qyzs b on a.qyid=b.qyid and b.DataState<>-1  and b.zslxID=1 
               left join UEPP_Qyzs c on a.qyid=c.qyid and c.DataState<>-1 and c.zslxID=10
               left join UEPP_Qyzs d on a.qyid=d.qyid and d.DataState<>-1 and d.zslxID=2
               left join UEPP_Qyzs e on a.qyid=e.qyid and e.DataState<>-1 and e.zslxID=11
               left join UEPP_Qyzs f on a.qyid=f.qyid and f.DataState<>-1 and f.zslxID=12
               left join Uepp_Ryjbxx g on a.fddbr_ryid=g.ryid and g.datastate<>-1 
               left join Uepp_Ryjbxx h on a.qyfzr_ryid=h.ryid and h.datastate<>-1 
               left join Uepp_Ryjbxx i on a.jsfzr_ryid=i.ryid and i.datastate<>-1 
              where a.rowid=:pRowID";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadJsdw(string rowID)
        {
            string sql = @"select * from uepp_jsdw where rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowID.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        #endregion

        #region 读取列表
        public DataTable RetrieveJsdwSsgc(string rowid)
        {

            string sql = @"select rownum, b.rowid row_id,b.zj,b.gm,b.jzmj,b.dd,b.xmmc,b.xmid,b.sgdwid,b.sgdw,b.xmjl,b.xmjlid,d.rowid qyrowid,e.rowid ryrowid 
from uepp_jsdw a inner join uepp_xmjbxx b on a.jsdwid=b.jsdwid   
 left join uepp_qyjbxx d on b.sgdwid=d.qyid left join uepp_ryjbxx e on b.xmjlid=e.ryid   
where a.rowid=:pRowID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");

        }

        public DataTable RetrieveZyry(string rowid, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            //            string sql = @" select rownum,a.qymc,b.*,c.xm,c.zjhm,d.ryzslx,d.zsbh,to_char(d.zsyxqrq,'yyyy-MM-dd') zsyxqrq,to_char(d.zsyxzrq,'yyyy-MM-dd') zsyxzrq 
            //from uepp_qyjbxx a inner join UEPP_QyRy b on a.qyid=b.qyid inner join uepp_ryjbxx c on b.ryid=c.ryid
            //left join uepp_ryzs d on b.ryid=d.ryid and b.ryzyzglxID=d.ryzyzglxID 
            //where a.rowid=:pRowID";
            string sql = @"select rownum,qymc,qyid,ryid,xm,zjhm,lxdh,row_id,ryzyzglx,ryzslx,zsbh,zsyxqrq,zsyxzrq,zsmx from(
select distinct a.qymc,b.qyid,b.ryid,c.xm,c.zjhm,nvl(nvl(c.lxdh,c.yddh),'0') lxdh,c.rowid row_id,d.ryzyzglx,d.ryzslx,d.zsbh,  
to_char(d.zsyxqrq,'yyyy-MM-dd') zsyxqrq,to_char(d.zsyxzrq,'yyyy-MM-dd') zsyxzrq,'查看明细' zsmx
from uepp_qyjbxx a inner join UEPP_QyRy b on a.qyid=b.qyid inner join uepp_ryjbxx c on b.ryid=c.ryid
inner join uepp_ryzs d on c.ryid=d.ryid where a.rowid=:pRowID
) ryxx where 1=1 and ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowID", rowid.Replace(" ", "+"));

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText.Trim();
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);

        }

        public DataTable RetrieveRyzs(string ryid)
        {
            string sql = "select rowid,ryzyzglx,ryzslx,zsbh,to_char(zsyxqrq,'yyyy-MM-dd') zsyxqrq,to_char(zsyxzrq,'yyyy-MM-dd') zsyxzrq,'查看明细' zsmx  from uepp_ryzs where ryid=:pRyID ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRyID", ryid);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveQyclgc(string rowid, string befrom, string dwlx)
        {
            string sql = "";
            if (dwlx == "sgdw")
            {
                sql = @"select rownum,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,jsdwrowid,ryrowid,qyrowid from (
select b.rowid row_id,b.xmmc,b.jsdw,b.sgdw,b.xmjl,b.cblx,b.ssdq,c.rowid jsdwrowid,d.rowid ryrowid,a.rowid qyrowid 
from uepp_qyjbxx a inner join uepp_xmjbxx b on a.qyid=b.sgdwid 
left join uepp_jsdw c on b.jsdwid=c.jsdwid left join uepp_ryjbxx d on b.xmjlid=d.ryid
where a.rowid=:pRowID ) where 1=1 and jsdw is not null ";
            }
//            else if (dwlx == "jldw")
//            {
//                sql = @"select rownum ,row_id,xmmc,xmjl,cblx,jsdw,ssdq,sgdw,jsdwrowid,ryrowid,qyrowid from (
//select c.rowid row_id,c.xmmc,c.xmjl,c.cblx,c.jsdw,c.ssdq,c.sgdw,d.rowid jsdwrowid,e.rowid ryrowid,f.rowid qyrowid
// from uepp_qyjbxx a inner join uepp_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxID=4
//inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh
//left join uepp_qyjbxx f on c.sgdwid=f.qyid 
//left join uepp_jsdw d on c.jsdwid=d.jsdwid 
//left join uepp_ryjbxx e on c.xmjlid=e.ryid 
//where a.rowid=:pRowID 
//) where 1=1";
//            }
            else
            {
                string csywlxid = "''";

                switch (befrom)
                {
                    case "sgdw":
                        csywlxid = "1,2,3";
                        break;
                    case "sjdw":
                        csywlxid = "5,6";
                        break;
                    case "jldw":
                        csywlxid = "4";
                        break;
                    case "zjjg":
                        csywlxid = "7,8,9";
                        break;
                    case "qtdw":
                        csywlxid = "''";
                        break;
                }
                sql = @"select rownum ,row_id,xmmc,jsdw,sgdw,xmjl,cblx,ssdq,qyrowid,jsdwrowid,ryrowid from (
select c.rowid row_id,c.xmmc,c.jsdw,c.sgdw,c.xmjl,c.cblx,c.ssdq,f.rowid qyrowid,d.rowid jsdwrowid,e.rowid ryrowid 
from uepp_qyjbxx a inner join UEPP_xmcjdwxx b on a.qyid=b.qyid and b.cjdwlxid in (" + csywlxid + ")";
                sql += @" inner join uepp_xmjbxx c on b.sgxmtybh=c.sgxmtybh left join uepp_qyjbxx f on c.sgdwid=f.qyid  
left join uepp_jsdw d on c.jsdwid=d.jsdwid left join uepp_ryjbxx e on c.xmjlid=e.ryid 
where a.rowid=:pRowID ) where 1=1  and jsdw is not null ";
            }
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //sp.Add(":pCsywlxID", csywlxid);
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");

        }

        public DataTable RetrieveQyzs(string rowid, string befrom)
        {
            string sql = "";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sql = @"select b.rowid,b.zslx,b.zsbh,b.fzdw,to_char(b.fzrq,'yyyy-MM-dd') fzrq,to_char(b.zsyxzrq,'yyyy-MM-dd') zsyxzrq
from uepp_qyjbxx a  inner join uepp_qyzs b on a.qyid=b.qyid 
where a.rowid=:pRowID";
            sp.Add(":pRowID", rowid.Replace(" ", "+"));

            return DB.ExeSqlForDataTable(sql, sp, "t");

        }


        public DataTable RetrieveQyxxList(string qylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            //string tagValue = "";
            if (qylx == "jsdw")
            {
                sql = "select rowid,jsdwid,jsdw,nvl(fddbr,'无') fddbr,dwflid,dwfl,nvl(dwdz,'无') dwdz,nvl(lxr,'无') lxr,lxdh,datastate,to_char(xgrqsj,'yyyy-MM-dd') xgrqsj,tag from uepp_jsdw where 1=1 and  ";

                //tagValue = ft.GetValue("tag");
                //ft.Remove("tag");
               
              
            }
            else
            {
                if (string.IsNullOrEmpty(orderby.Trim()))
                    orderby = " qyid  ";
                string csywlxID = "";
                switch (qylx)
                {
                    case "sgdw":
                        csywlxID = "1,2,3";
                        break;
                    case "sjdw":
                        csywlxID = "5,6";
                        break;
                    //case "jsdw":
                    //    csywlxID = "11,12";
                    //    break;
                    case "zjjg":
                        csywlxID = "4,7,8,9";
                        break;
                    case "qtdw":
                        csywlxID = "''";
                        break;
                }



//                sql = @"  select * from (
// select a.rowid row_id,a.qyid,a.qymc,a.xxdd,nvl(a.lxdh,'0') lxdh,nvl(a.lxr,'无') lxr,a.sylxid,a.sylx,nvl(a.county,'无') county,a.datastate,b.csywlxid,b.csywlx 
//from uepp_qyjbxx a inner join uepp_qycsyw b on a.qyid=b.qyid and b.csywlxid in(" + csywlxID + ")";

                sql = @"  select * from (
 select a.rowid row_id,a.qyid,a.qymc,a.xxdd,nvl(a.lxdh,'0') lxdh,nvl(a.lxr,'无') lxr,a.sylxid,a.sylx,nvl(a.county,'无') county,a.datastate,a.tag 
from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid in(" + csywlxID + ") ";

                string _csyelxid = ft.GetValue("csywlxid");
                if (!string.IsNullOrEmpty(_csyelxid))
                {
                    sql += " and csywlxid in (" + _csyelxid + ")  ";
                    ft.Remove("csywlxid");
                    //ft.Translate();
                }

                sql += "))  qyxx where 1=1  and ";   //and xxdd is not null

                string zhuxzz = ft.GetValue("zhuxzz");
                if (!string.IsNullOrEmpty(zhuxzz))
                {
                    sql += " qyid in (select qyid from uepp_qyzzmx where zzbz=:zzbz and zzlb=:zzlb)  and ";
                    sp.Add(":zzbz", "主项");
                    sp.Add(":zzlb", zhuxzz);
                    ft.Remove("zhuxzz");
                    //ft.Translate();
                }

                string zengxzz = ft.GetValue("zengxzz");
                if (!string.IsNullOrEmpty(zengxzz))
                {
                    sql += " qyid in (select qyid from uepp_qyzzmx where zzbz=:zzbz1 and zzlb=:zzlb1)  and ";
                    sp.Add(":zzbz1", "增项");
                    sp.Add(":zzlb1", zengxzz);
                    ft.Remove("zengxzz");
                    //ft.Translate();
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

            string sql = @" select rownum,qyid,zzbz,zzlb,zzdj,sortid from (
select qyid,zzbz,zzlb,zzdjid,zzdj,case when zzdj=:zzdj1 then 1 when zzdj=:zzdj2 then 2 when zzdj=:zzdj3 then 3 when zzdj=:zzdj4 then 4 else 5 end sortid
from UEPP_Qyzzmx where qyid=:pQyID order by  zzbz ,sortid  ) zz where 1=1";

            sp.Add(":pQyID", qyid);
            sp.Add(":zzdj1", "一级");
            sp.Add(":zzdj2", "二级");
            sp.Add(":zzdj3", "三级");
            sp.Add(":zzdj4", "四级");
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadZzmx(string rowid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select zslx,zsbh,zsyxqrq,zsyxzrq,fzdw,fzrq,bz,case when sfzzz=0 then '否' else '是' end sfzzz from uepp_qyzs where rowid=:pRowID";
            sp.Add(":pRowID", rowid.Replace(" ", "+"));
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 资质名称树结构
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string getZTreeOfZzmc()
        {

            string sql = @"select codetype,CODE,codeinfo from uepp_code where codetype=:codetype and CODE in ('10','11','12','20')  order by CODE  ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add(":codetype", "企业资质序列");
            DataTable dtZzxl = DB.ExeSqlForDataTable(sql, spc, "t");

            string sql2 = @"select codetype,code,codeinfo,parentcode from uepp_code where parentcodetype=:parentcodetype and codetype=:codetype and parentcode  in  ('10','11','12','20') order by parentcode, orderid";
            spc.Add(":parentcodetype", "企业资质序列");
            spc.Add(":codetype", "企业资质类别");
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
            string csywlx = "";
            string sql = "select distinct csywlx from uepp_qycsyw where qyid='" + qyID + "'";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");

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

        public string getCsywlxid(string rowid)
        {
            string sql = "select a.csywlxid from uepp_qycsyw a inner join uepp_qyjbxx b on a.qyid=b.qyid where b.rowid=:pRowid";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add(":pRowid", rowid.Replace(" ", "+"));
            return DB.ExeSqlForString(sql, sp);
        }
    }
}
