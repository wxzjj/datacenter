using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy.Common;
using Wxjzgcjczy.DAL.Sqlserver;
using Bigdesk8.Data;

namespace Wxjzgcjczy.BLL
{
    public class ZyryBLL
    {
        public AppUser WorkUser { get; set; }
        public readonly ZyryDAL DAL = new ZyryDAL();

        public DataTable Exec(string sql,SqlParameterCollection sp)
        {
            return DAL.DB.ExeSqlForDataTable(sql,sp,"dt");

        }

        public SqlParameterCollection CreatePara()
        {
            return DAL.DB.CreateSqlParameterCollection();
        }

        public ZyryBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator(18000);
            //this.DAL.DB = DBOperatorFactory.GetDBOperator("User ID=SYSTEM;Password=oracle;Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.1.1.3)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = orcl)))", "ORACLE11G");

        }

        public FunctionResult<DataTable> RetrieveZyryJbxx(string rylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = " xgrqsj desc ";
            DataTable dt = DAL.RetrieveZyryJbxx(rylx, ft, pagesize, page, orderby, out allRecordCount);
            dt.Columns.Add("ryzyzglx", typeof(string));
            dt.Columns.Add("zsbh", typeof(string));
            //dt.Columns.Add("ZYZGDJ", typeof(string));

            if (dt.HasData())
            {
                string ryIds = "(";
                foreach (DataRow dr in dt.Rows)
                {
                    ryIds = ryIds + "'" + dr["ryid"].ToString() + "'" + ",";
                }
                ryIds = ryIds.Remove(ryIds.Length - 1);
                ryIds = ryIds + ")";

                DataTable dtRyzs = DAL.GetRyzyzgByRyids(ryIds, rylx);

                string ryid = "";

                foreach (DataRow dr in dt.Rows)
                {
                    ryid = dr["ryid"].ToString();
                    DataRow[] drArray = dtRyzs.Select("ryid = '" + ryid + "'");

                    if (drArray.Length > 0)
                    {
                        for (int i = 0; i < drArray.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(drArray[i]["ryzyzglx"].ToString()) && !dr["ryzyzglx"].ToString().Contains(drArray[i]["ryzyzglx"].ToString()))
                            {
                                dr["ryzyzglx"] += drArray[i]["ryzyzglx"] + "<br />";

                                if (!string.IsNullOrEmpty(drArray[i]["zsbh"].ToString2()))
                                    dr["zsbh"] += drArray[i]["zsbh"] + "<br />";

                                //if (!dr["zyzgdj"].ToString().Contains(dtRyzs.Rows[i]["zyzgdj"].ToString()))
                                //    dr["zyzgdj"] += dtRyzs.Rows[i]["zyzgdj"] + "<br />";
                            }
                        }
                    }
                }
            }

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveCjxm(string ryID)
        {
            DataTable dt = DAL.RetrieveCjxm(ryID);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveZyryJbxxViewList(string ryid, string rylx)
        {
            DataTable dt = DAL.RetrieveZyryJbxxViewList(ryid, rylx);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveRyzymx(string ryid, string ryzyzglxid, string ryzslxid, FilterTranslator ft)
        {
            DataTable dt = DAL.RetrieveRyzymx(ryid, ryzyzglxid, ryzslxid, ft);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveRyzsmx(string rowid)
        {
            DataTable dt = DAL.RetrieveRyzsmx(rowid);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<List<IDataItem>> ReadRyxx(string ryID)
        {
            DataTable dt = DAL.ReadRyxx(ryID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
            {
                dt.Columns.Add("yxqx", typeof(string));
                dt.Rows[0]["yxqx"] = dt.Rows[0]["sfzyxqs"].ToString().ToDate2() + " - " + dt.Rows[0]["sfzyxqz"].ToString().ToDate2();
                list = dt.Rows[0].ToDataItem();
            }
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        public FunctionResult<List<IDataItem>> ReadRyxxView(string ryID)
        {
            DataTable dt = DAL.ReadRyxxView(ryID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
            {
                dt.Columns.Add("yxqx", typeof(string));
                dt.Rows[0]["yxqx"] = dt.Rows[0]["sfzyxqs"].ToString().ToDate2() + " - " + dt.Rows[0]["sfzyxqz"].ToString().ToDate2();
                list = dt.Rows[0].ToDataItem();
            }
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }
        public DataTable Read(string rowID)
        {
            DataTable dt = DAL.ReadRyxx(rowID);
            if (dt.HasData())
                return dt;
            return null;
        }

        public FunctionResult<List<IDataItem>> ReadRyzs(string rowid)
        {
            DataTable dt = DAL.ReadRyzs(rowid);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData()) 
            list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        public string GetRyzglxid(string rowid)
        {
            return DAL.GetRyzglxid(rowid);
        }


        /// <summary>
        /// 获取注册建造师信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Qyry_zcjzs(string ryID)
        {
            string sql = @"select  a.*,c.qyID as zydwID,c.qymc as zydw ,c.zzjgdm,d.zsyxqrq,d.zsyxzrq,d.zsbh,d.fzdw,d.fzrq,b.ryzyzglxid
from uepp_ryjbxx a 
inner join (select distinct ryid,qyid,ryzyzglxid from uepp_qyry  where ryzyzglxid in (1,2) ) b on a.ryid=b.ryid 
inner join uepp_qyjbxx c on b.qyid=c.qyid
inner join uepp_Ryzs d on a.ryID=d.ryID
where  a.ryid=@ryID ";
            SqlParameterCollection sp = DAL.DB.CreateSqlParameterCollection();
            sp.Add("@ryID", ryID);
            return DAL.DB.ExeSqlForDataTable(sql, sp, "dt");

        }

    }
}
