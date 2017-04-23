using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy.DAL;
using Bigdesk8.Data;

namespace Wxjzgcjczy.BLL
{
    public class SzgcBLL
    {
        public AppUser WorkUser { get; set; }
        private readonly SzgcDAL DAL = new SzgcDAL();

        public SzgcBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();

        }

        public FunctionResult<DataTable> RetrieveLxbd(string rowid, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {


            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveLxbd(rowid, ft, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveSzgc(string xmlx, AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            //if (xmlx == "aqjd")
            //    orderby = " sortid,row_id desc ";
            //else
            //    orderby = "row_id desc";
            switch (xmlx)
            {
                case "lxxm":
                    orderby = "lxwh, lxrq desc ";
                    break;
                case "aqjd":
                    orderby = " aqjddabh ";
                    break;
                case "zljd":
                    orderby = " zljdslsj desc ";
                    break;
                case "sgxk":
                    orderby = " sgxkslsj desc ";
                    break;
                case "gcxm":
                    orderby = " operatedate desc ";
                    break;
                default :
                    orderby = "row_id desc";
                    break;

            }
            DataTable dt = DAL.RetrieveSzgc(xmlx, workUser, ft, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveGcxmAqjd(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveGcxmAqjd(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveGcxmHtba(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveGcxmHtba(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveGcxmZljd(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveGcxmZljd(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveGcxmSgxk(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveGcxmSgxk(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveGcxmJgys(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveGcxmJgys(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }
        public FunctionResult<DataTable> RetrieveDwgcList(string rowid, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = "row_id desc";
            DataTable dt = DAL.RetrieveDwgcList(rowid, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveAqbjxmzcy(string rowid, string befrom)
        {
            DataTable dt = DAL.RetrieveAqbjxmzcy(rowid, befrom);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveAqjdZljd(string rowid)
        {
            DataTable dt = DAL.RetrieveAqjdZljd(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveAqjdSgxk(string rowid)
        {
            DataTable dt = DAL.RetrieveAqjdSgxk(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveAqjdJgba(string rowid)
        {
            DataTable dt = DAL.RetrieveAqjdJgba(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveZljdDtgc(string rowid, string befrom)
        {
            DataTable dt = DAL.RetrieveZljdDtgc(rowid, befrom);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveZljdSgxk(string rowid)
        {
            DataTable dt = DAL.RetrieveZljdSgxk(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveZljdJgba(string rowid)
        {
            DataTable dt = DAL.RetrieveZljdJgba(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveSgxk(string rowid)
        {
            DataTable dt = DAL.RetrieveSgxk(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveSgxkJgba(string rowid)
        {
            DataTable dt = DAL.RetrieveSgxkJgba(rowid);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<List<IDataItem>> ReadXmxx(string rowID)
        {
            DataTable dt = DAL.ReadXmxx(rowID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };

        }

        public FunctionResult<List<IDataItem>> ReadHtba(string rowID)
        {
            DataTable dt = DAL.ReadHtba(rowID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };

        }
        public FunctionResult<List<IDataItem>> ReadDwgc(string rowID)
        {
            DataTable dt = DAL.ReadDwgc(rowID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };

        }

        public FunctionResult<List<IDataItem>> ReadXmxx(string xmlx, string rowID, string befrom)
        {
            DataTable dt = DAL.ReadXmxx(xmlx, rowID, befrom);
            dt.Columns.Add("zcjzszh", typeof(string));
            dt.Columns.Add("aqkhzsbh", typeof(string));
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
            {
                if (xmlx == "gcxm")
                {
                    string ryid = "";
                    if (dt.Columns.Contains("ryid"))
                        ryid = dt.Rows[0]["ryid"].ToString();
                    else
                        ryid = dt.Rows[0]["xmjlid"].ToString();
                    DataTable dtXmjl = ReadXmjl(ryid);
                    if (dtXmjl.HasData())
                    {
                        foreach (DataRow dr in dtXmjl.Rows)
                        {
                            if (dr["ryzslxid"].ToString() == "11")
                                dt.Rows[0]["zcjzszh"] = dr["zsbh"];
                            if (dr["ryzslxid"].ToString() == "42")
                                dt.Rows[0]["aqkhzsbh"] = dr["zsbh"];
                        }
                    }
                }
                list = dt.Rows[0].ToDataItem();


            }
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        public DataTable ReadXmjl(string ryid)
        {
            DataTable dt = DAL.ReadXmjl(ryid);

            return dt;
        }

        public DataTable GetZljdzs(string rowid, string befrom)
        {
            return DAL.GetZljdzs(rowid, befrom);
        }

        public FunctionResult<List<IDataItem>> ReadSgxkzs(string rowid, string befrom)
        {

            List<IDataItem> list = new List<IDataItem>();
            DataTable dt = DAL.ReadSgxkzs(rowid, befrom);
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();

            return new FunctionResult<List<IDataItem>>() { Result = list };
        }
    }
}
