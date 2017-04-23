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
                    sql = @"select rownum,row_id,qymc,xxdd,lxr,lxdh,fddbr,zsyxqrq,zsyxzrq,zslx,zsbh  from ( 
select distinct a.rowid row_id ,a.qymc,a.xxdd,a.lxr,a.lxdh,a.fddbr,b.zslx,b.zsbh,to_char(b.zsyxqrq,'yyyy-mm-dd') zsyxqrq,
to_char(b.zsyxzrq,'yyyy-mm-dd') zsyxzrq from uepp_qyjbxx a inner join uepp_qyzs b on a.qyid=b.qyid 
) qy where 1=1 and (zsyxzrq between to_char(sysdate,'yyyy-mm-dd') and to_char((select add_months(sysdate,3) from dual),'yyyy-mm-dd' ) 
or zsyxzrq<=to_char(sysdate,'yyyy-mm-dd')) and ";
                    break;

                case "ryzsgq"://三个月内即将过期和已过期的人员证书
                    sql = @"select rownum,row_id,xm,zjhm,zcjb,ryzyzglx,lxdh,ryzslx,zsbh,zsyxqrq,zsyxzrq from (
select distinct a.rowid row_id, a.xm,a.zjhm,a.zcjb,b.ryzyzglx,a.lxdh,b.ryzslx,b.zsbh,to_char(b.zsyxqrq,'yyyy-mm-dd') zsyxqrq,
to_char(b.zsyxzrq,'yyyy-mm-dd') zsyxzrq from uepp_ryjbxx a inner join uepp_ryzs b on a.ryid=b.ryid )  
ry where 1=1 and    (zsyxzrq between to_char(sysdate,'yyyy-mm-dd') and to_char((select add_months(sysdate,3) from dual),'yyyy-mm-dd' ) 
or zsyxzrq<=to_char(sysdate,'yyyy-mm-dd')) and ";

                    break;
                case "zjgyxm"://造价过亿项目
                    sql = @"select rownum,row_id,xmmc,jsdw,sgdw,xmjl,zj,ssdqid,ssdq,jsdwrowid,qyrowid,ryrowid from (
select distinct a.rowid row_id,a.xmmc,a.jsdw,a.sgdw,a.xmjl,a.zj,a.ssdqid,a.ssdq,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
from uepp_xmjbxx a inner join uepp_jsdw b on a.jsdwid=b.jsdwid left join uepp_qyjbxx c on a.sgdwid=c.qyid left join uepp_ryjbxx d on a.xmjlid=d.ryid 
where a.zj>10000) xm where 1=1 and ";

                    break;
                case "wbsgxkz"://未办施工许可证项目
                    sql = @"select rownum,row_id,xmmc,ssdq,jsdw,sgdw,xmjl,sgxmtybh,kgrq,jsdwrowid,qyrowid,ryrowid from (
select distinct a.rowid row_id,a.xmmc,a.ssdq,a.jsdw,a.sgdw,a.xmjl, a.sgxmtybh,to_char(b.kgrq,'yyyy-mm-dd') kgrq,e.rowid jsdwrowid,f.rowid qyrowid,g.rowid ryrowid from uepp_xmjbxx a 
inner join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh and to_char(b.kgrq,'yyyy-mm-dd') <=to_char(sysdate,'yyyy-mm-dd')
left join uepp_jsdw e on a.jsdwid=e.jsdwid left join uepp_qyjbxx f on a.sgdwid=f.qyid left join uepp_ryjbxx g on a.xmjlid=g.ryid 
where a.sgxmtybh not in (select distinct d.sgxmtybh from uepp_sgxkxx c inner join UEPP_SgxkAndBdRelation d on c.sgxkid=d.sgxkid )
) xm where 1=1 and ";

                    string kgrq1 = ft.GetValue("kgrq1");
                    string kgrq2 = ft.GetValue("kgrq2");
                    if (!string.IsNullOrEmpty(kgrq1))
                    {
                        sql += " to_char(kgrq,'yyyy-MM-dd')  >= '" + kgrq1 + "' and ";
                        ft.Remove("kgrq1");
                        //ft.Translate();
                    }
                    if (!string.IsNullOrEmpty(kgrq2))
                    {
                        sql += " to_char(kgrq,'yyyy-MM-dd')  <= '" + kgrq2 + "' and ";
                        ft.Remove("kgrq2");
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
                            sql += " ssdq like :pp1 or ssdq like :pp2";
                            sp.Add(":pp1", "%市区%");
                            sp.Add(":pp2", "%市辖区%");
                        }
                        else
                        {
                            sql += "  ssdq like :pp3";
                            sp.Add(":pp3", "%" + strSsdq[i] + "%");
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
