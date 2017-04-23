using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace WxjzgcjczyQyb.DAL
{
    public class ZcryDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveZyryJbxx(string rylx, List<IDataItem> list, int pagesize, int page, string orderby, out int allRecordCount)
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

            string sql = @"   select  *,(case when SbToStState=1 then '上报失败' when SbToStState=0 then  '上报成功'  else '未上报' end) SbState from ( 
select   a.ryid,a.xm,a.zjlx, SUBSTRING(a.zjhm,1,10)+'******'+ SUBSTRING(a.zjhm,LEN(a.zjhm)-1,2)  as zjhm,a.AJ_EXISTINIDCARDS,a.AJ_IsRefuse,CountyID,
case when a.AJ_EXISTINIDCARDS='1' then '未实名认证'  when a.AJ_EXISTINIDCARDS='2' then '已实名认证' end sfsmrz,
c.qyid,c.qymc,a.zczh,ISNULL(a.zcjb,'无') zcjb,ISNULL(ISNULL(a.lxdh,a.yddh),'') lxdh,a.datastate,ISNULL(c.county,'无') county,c.provinceid,c.province,a.xgrqsj     
 ,ISNULL((select SbToStState from SaveToStLog2 where TableName='uepp_ryjbxx' and PKID=a.ryID ),-1) as SbToStState

 from uepp_ryjbxx a left join (select distinct ryid,qyid from uepp_qyry 
 where ryzyzglxid in ( " + ryzyzglxID + " ) ";

            IDataItem _ryzyzglxID = list.GetDataItem("ryzyzglxID");
            if (_ryzyzglxID != null)
            {
                if (_ryzyzglxID.ItemData.Length > 0)
                {
                    sql += " and ryzyzglxid in (" + _ryzyzglxID.ItemData + ")";
                    list.Remove(_ryzyzglxID);
                }
            }

            sql += " ) b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid where  1=1 ";

            IDataItem zsbh = list.GetDataItem("zsbh");
            if (zsbh != null)
            {
                if (zsbh.ItemData.Length > 0)
                {
                    sql += " and a.ryid in (select ryid from uepp_ryzs where zsbh like'%" + zsbh.ItemData + "%') ";
                    list.Remove(zsbh);
                }
            }


            sql += ") ryxx where 1=1   ";

            list.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
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

        public DataTable ReadRyxx(string ryID)
        {
            string sql = @"  select * from uepp_ryjbxx where ryID=@ryID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
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

            string sql = @" select distinct * from (
select   j.zyzgdj,a.Id,a.ryid,a.ryzyzglxID,a.ryzyzglx,b.ryzslxid,b.ryzslx,b.zsbh,b.zsyxqrq,b.zsyxzrq,e.qyid,e.qymc,'查看明细' ckmx ,f.tag  
from UEPP_Ryzyzg a inner join uepp_ryzs b on a.ryid=b.ryid and a.ryzyzglxID=b.ryzyzglxID  
inner join UEPP_Ryzymx j on a.ryID=j.ryID and a.ryzyzglxID=j.ryzyzglxID
 inner join uepp_ryjbxx d on a.ryid=d.ryid
 inner join (select  distinct qyid,ryid,ryzyzglxID,tag from UEPP_QyRy ) f  on d.ryID=f.ryID 
 left join uepp_qyjbxx e on f.qyid=e.qyid
where a.ryid=@ryID and a.ryzyzglxid in (" + ryzyzglxID + ")) ryzs  order by Id, ryzyzglxID   ";

            sp.Add("@ryID", ryid);
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


        }

    }
}
