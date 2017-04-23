using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace WxjzgcjczyQyb.DAL
{
    public class ScztDAL
    {
        public DBOperator DB { get; set; }


        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="qylx"></param>
        /// <param name="list"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveQyxxList(string qylx, List<IDataItem> list, int pagesize, int page, string orderby, out int allRecordCount)
        {

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            if (qylx == "jsdw")
            {
                sql = @" select * from (
 select jsdwid,zzjgdm,jsdw,ISNULL(fddbr,'无') fddbr,dwflid,dwfl,ISNULL(dwdz,'无') dwdz,ISNULL(lxr,'无') lxr,lxdh,a.datastate,CONVERT(varchar(10),a.xgrqsj,120) xgrqsj,a.tag 
 from uepp_jsdw a
 ) as aaa  where 1=1   ";
            }
            else
            {
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
                    case "zjjg":
                        csywlxID = "7,4,8,9,15,16,17";
                        break;
                    case "qtdw":
                        csywlxID = "''";
                        break;
                }

                sql = @"  select *,(case when SbToStState=0 then '已上报' when SbToStState=-1 then '未上报' when SbToStState=2 then '未更新'  else '上报出错' end) as SbState from (
 select a.qyid,a.zzjgdm,a.yyzzzch,a.qymc,a.zcdd,a.xxdd,a.lxdh lxdh,a.lxr lxr,a.sylxid,a.sylx,a.county ,a.datastate,a.tag,a.CountyID,CONVERT(varchar(10),xgrqsj,120) xgrqsj 
,ISNULL((select  SbToStState from SaveToStLog2 where TableName='uepp_qyjbxx' and PKID=a.qyid ),-1) as SbToStState
,(select  SbToStMsg from SaveToStLog2 where TableName='uepp_qyjbxx' and PKID=a.qyid ) as SbToStMsg
from uepp_qyjbxx a  where a.qyid in (select qyid from uepp_qycsyw where csywlxid in(" + csywlxID + ") ";

                IDataItem _csywlxid = list.GetDataItem("csywlxid");

                if (_csywlxid != null)
                {
                    if (_csywlxid.ItemData.Trim().Length > 0)
                    {
                        sql += " and csywlxid in (" + _csywlxid.ItemData + ")  ";
                        list.Remove(_csywlxid);
                    }
                }

                sql += "))  qyxx where 1=1    ";   //and xxdd is not null

                IDataItem zhuxzz = list.GetDataItem("zhuxzz");
                if (zhuxzz != null)
                {
                    if (zhuxzz.ItemData.Trim().Length > 0)
                    {
                        sql += " and qyid in (select qyid from uepp_qyzzmx where zzbz=@zzbz and zzlb=@zzlb and DataState<>-1 )  and ";
                        sp.Add("@zzbz", "主项");
                        sp.Add("@zzlb", zhuxzz.ItemData);
                        list.Remove(zhuxzz);
                    }
                }

                IDataItem zengxzz = list.GetDataItem("zengxzz");
                if (zengxzz != null)
                {
                    if (zengxzz.ItemData.Trim().Length > 0)
                    {
                        sql += " and qyid in (select qyid from uepp_qyzzmx where zzbz=@zzbz1 and zzlb=@zzlb1 and DataState<>-1)  and ";
                        sp.Add("@zzbz1", "增项");
                        sp.Add("@zzlb1", zengxzz.ItemData);
                        list.Remove(zengxzz);
                    }
                }
            }
            list.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);
        }



        public string GetCsywlx(string qyID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            string csywlx = "";
            string sql = "select distinct csywlx from uepp_qycsyw where qyid=@qyID ";
            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    csywlx += dt.Rows[i]["csywlx"].ToString() + " <br/> ";
                }
            }

            return csywlx;
        }


        public DataTable ReadJsdw(string jsdwID)
        {
            string sql = @"select * from uepp_jsdw where jsdwID=@jsdwID";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveJsdwSsgc(string jsdwID)
        {
            string sql = @"select *,ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=ProvinceNum),'')
+ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=CityNum),'')
+ISNULL((select CodeInfo from UEPP_Code where CodeType='城市地区' and Code=CountyNum),'') as xmdd
,(select CodeInfo from tbPrjTypeDic where Code=PrjTypeNum) as PrjType from TBProjectInfo where UpdateFlag='U' and  BuildCorpCode=(select zzjgdm from UEPP_Jsdw where jsdwID=@jsdwID) ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@jsdwID", jsdwID);
            return DB.ExeSqlForDataTable(sql, sp, "t");

        }


        public DataTable ReadQyxx(string qyID)
        {
            string sql = @" select a.*,b.zsyxzrq  jgdmyxq,c.zsbh  zzzsbh,c.fzdw  zzzsfzdw,c.zsyxzrq  zzzsyxq ,c.bz  zzzsbz,
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
              where a.qyID=@qyID";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable RetrieveQyxxViewList(string qyid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            string sql = @" select distinct qyid,zzbz,zzlb,zzdj,sortid,tag,xgrqsj from (
select qyid,zzbz,zzlb,zzdjid,zzdj,tag,convert(varchar(19),xgrqsj,120) xgrqsj,(case when zzdj=@zzdj1 then 1 when zzdj=@zzdj5 then 1 when zzdj=@zzdj2 then 2 when zzdj=@zzdj6 then 2 when zzdj=@zzdj3 then 3 when zzdj=@zzdj7 then 3 when zzdj=@zzdj4 then 4 when zzdj=@zzdj8 then 4 else 5 end) sortid
from UEPP_Qyzzmx where qyid=@pQyID and DataState<>-1) zz where 1=1 order by  zzbz desc ,sortid  ";

            sp.Add("@pQyID", qyid);
            sp.Add("@zzdj1", "一级");
            sp.Add("@zzdj2", "二级");
            sp.Add("@zzdj3", "三级");
            sp.Add("@zzdj4", "四级");
            sp.Add("@zzdj5", "壹级");
            sp.Add("@zzdj6", "贰级");
            sp.Add("@zzdj7", "叁级");
            sp.Add("@zzdj8", "肆级");
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        public DataTable RetrieveQyzs(string qyID)
        {
            string sql = "";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sql = @"select distinct zslx,zsbh,fzdw,Convert(varchar(10),fzrq,120) fzrq,Convert(varchar(10),zsyxzrq,120) zsyxzrq,tag
from uepp_qyzs where qyID=@qyID and DataState<>-1 
and zsjlId in (select MAX(zsjlId) from uepp_qyzs
where qyID=@qyID and DataState<>-1
group by zsbh)
";
            sp.Add("@qyID", qyID);

            return DB.ExeSqlForDataTable(sql, sp, "t");

        }

        public DataTable RetrieveZyry(string qyID, List<IDataItem> list, int pagesize, int page, string orderby, out int allRecordCount)
        {
            string sql = @"select qymc,qyid,ryid,xm,zjhm,lxdh,ryzyzglxid,ryzyzglx,zsjlId,ryzslxid,ryzslx,zsbh,zsyxqrq,zsyxzrq,zsmx from(
select distinct a.qymc,b.qyid,b.ryid,c.xm,c.zjhm,ISNULL(c.lxdh,c.yddh) lxdh,b.ryzyzglxid,b.ryzyzglx,d.zsjlId,d.ryzslxid,d.ryzslx,d.zsbh,  
CONVERT(varchar(10),d.zsyxqrq,120) zsyxqrq,CONVERT(varchar(10),d.zsyxzrq,120) zsyxzrq,'查看明细' zsmx
from uepp_qyjbxx a 
inner join UEPP_QyRy b on a.qyid=b.qyid and b.DataState<>-1
inner join uepp_ryjbxx c on b.ryid=c.ryid and c.DataState<>-1
left join uepp_ryzs d on c.ryid=d.ryid and d.DataState<>-1 and b.ryzyzglxID=d.ryzyzglxID  where a.qyID=@qyID 
) ryxx where 1=1 ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);

            list.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);

        }

        /// <summary>
        ///勘察设计企业承揽工程
        /// </summary>
        /// <param name="qyID"></param>
        /// <param name="befrom"></param>
        /// <param name="dwlx"></param>
        /// <returns></returns>
        public DataTable RetrieveQyclgc(string qyID, int pagesize, int page, string orderby, out int allRecordCount)
        {
            string sql = "";
            sql = @"select a.*,(select CodeInfo from  tbContractTypeDic where Code=ContractTypeNum) as ContractType,b.PKID as LxPKID,b.PrjName as LxPrjName,c.qyID,ISNULL(d.jsdwID,'') as jsdwID from TBContractRecordManage a
inner  join TBProjectInfo b on a.PrjNum=b.PrjNum
inner join UEPP_Qyjbxx c on c.zzjgdm=a.ContractorCorpCode 
left join UEPP_Jsdw d on d.zzjgdm=a.PropietorCorpCode 
 where a.UpdateFlag='U' and b.UpdateFlag='U' and c.qyID=@qyID ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@qyID", qyID);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pagesize, page, out allRecordCount);

        }



    }
}
