using System;
using System.Collections.Generic;
using System.Text;
using WxjzgcjczyQyb.DAL;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;

namespace WxjzgcjczyQyb.BLL
{
    public class ZcryBLL
    {
        ZcryDAL zcryDal;
        AppUser WorkUser;
        public ZcryBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            zcryDal = new ZcryDAL();
            zcryDal.DB = new DatabaseOperator();
        }

        public FunctionResult<DataTable> RetrieveZyryJbxx(string rylx, List<IDataItem> list, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = " xgrqsj desc ";
            DataTable dt = zcryDal.RetrieveZyryJbxx(rylx, list, pagesize, page, orderby, out allRecordCount);
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

                DataTable dtRyzs = zcryDal.GetRyzyzgByRyids(ryIds, rylx);

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


        public FunctionResult<List<IDataItem>> ReadRyxxView(string ryID)
        {
            DataTable dt = zcryDal.ReadRyxxView(ryID);
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
            DataTable dt = zcryDal.ReadRyxx(rowID);
            if (dt.HasData())
                return dt;
            return null;
        }

        public FunctionResult<DataTable> RetrieveZyryJbxxViewList(string ryid, string rylx)
        {
            DataTable dt = zcryDal.RetrieveZyryJbxxViewList(ryid, rylx);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveCjxm(string ryID)
        {
            DataTable dt = zcryDal.RetrieveCjxm(ryID);

            return new FunctionResult<DataTable>() { Result = dt };
        }
    }
}
