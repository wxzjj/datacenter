using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy.Common;
using Bigdesk8.Data;
using Wxjzgcjczy.DAL.Sqlserver;

namespace Wxjzgcjczy.BLL
{
    public class SzqyBLL
    {
        public AppUser WorkUser { get; set; }
        private readonly SzqyDAL DAL = new SzqyDAL();
        public SzqyBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();
            //this.DAL.DB = DBOperatorFactory.GetDBOperator("User ID=SYSTEM;Password=oracle;Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.1.1.3)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = orcl)))", "ORACLE11G");

        }

        #region 获取表结构
        #endregion

        #region 新增


        #endregion

        #region 修改

        #endregion

        #region 删除

        #endregion

        #region 读取

        public FunctionResult<List<IDataItem>> ReadQyxx(string qyID)
        {
            DataTable dt = DAL.ReadQyxx(qyID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["sfsyq"].ToString2()))
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["sfsyq"].ToString()))
                    {
                        if (dt.Rows[0]["sfsyq"].ToString() == "0" || dt.Rows[0]["sfsyq"].ToString() == "否")
                            dt.Rows[0]["sfsyq"] = "非央企";
                        else
                            dt.Rows[0]["sfsyq"] = "央企";
                    }
                }
                list = dt.Rows[0].ToDataItem();
            }
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        public FunctionResult<List<IDataItem>> ReadJsdwxx(string rowID)
        {
            DataTable dt = DAL.ReadJsdw(rowID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
            {
                list = dt.Rows[0].ToDataItem();
            }
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        #endregion

        #region 读取列表


        public FunctionResult<DataTable> RetrieveQyxxList(string qylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;

            if (qylx == "jsdw")
            {
                //orderby = " rowid desc ";
                orderby = " xgrqsj desc ";
            }
            else
                orderby = "xgrqsj desc";
            DataTable dt = DAL.RetrieveQyxxList(qylx, ft, pagesize, page, orderby, out  allRecordCount);
            if (qylx != "jsdw")
            {
                if (!dt.Columns.Contains("csywlx"))
                    dt.Columns.Add("csywlx", typeof(string));
                if (dt.HasData())
                {
                    string qyid = "";
                    string csywlx = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        qyid = dr["qyid"].ToString();
                        csywlx = DAL.GetCsywlx(qyid);
                        dr["csywlx"] = csywlx;
                    }
                }
            }
            return new FunctionResult<DataTable>() { Result = dt };
        }
        public FunctionResult<DataTable> RetrieveJsdwSsgc(string jsdwID)
        {
            DataTable dt = DAL.RetrieveJsdwSsgc(jsdwID);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveZyry(string qyID, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = " ryid,zsbh ";

            DataTable dt = DAL.RetrieveZyry(qyID, ft, pagesize, page, orderby, out  allRecordCount);
            //dt.Columns.Add("ROWID", typeof(string));
            //dt.Columns.Add("RYZYZGLX", typeof(string));
            //dt.Columns.Add("RYZSLX", typeof(string));
            //dt.Columns.Add("ZSBH", typeof(string));
            //dt.Columns.Add("ZSYXQRQ", typeof(string));
            //dt.Columns.Add("ZSYXZRQ", typeof(string));
            //dt.Columns.Add("ZSMX", typeof(string));
            //if (dt.HasData())
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataTable dtZs = DAL.RetrieveRyzs(dr["ryid"].ToString());
            //        if (dtZs.HasData())
            //        {
            //            for (int i = 0; i < dtZs.Rows.Count; i++)
            //            {
            //                if (!string.IsNullOrEmpty(dtZs.Rows[i]["RYZSLX"].ToString()) && !dr["RYZSLX"].ToString().Contains(dtZs.Rows[i]["RYZSLX"].ToString()))
            //                {
            //                    dr["ROWID"] += dtZs.Rows[i]["ROWID"].ToString() + "<br />";
            //                    dr["RYZYZGLX"] += dtZs.Rows[i]["RYZYZGLX"].ToString() + "<br />";
            //                    dr["RYZSLX"] += dtZs.Rows[i]["RYZSLX"] + "<br />";
            //                    dr["ZSBH"] += dtZs.Rows[i]["ZSBH"].ToString() + "<br />";
            //                    dr["ZSYXQRQ"] += dtZs.Rows[i]["ZSYXQRQ"].ToString() + "<br />";
            //                    dr["ZSYXZRQ"] += dtZs.Rows[i]["ZSYXZRQ"].ToString() + "<br />";
            //                    dr["ZSMX"] += dtZs.Rows[i]["ZSMX"].ToString() + "<br />";
            //                }
            //            }
            //        }
            //    }
            //}

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveQyclgc(string qyID, string befrom, string dwlx)
        {
            DataTable dt = DAL.RetrieveQyclgc(qyID, befrom, dwlx);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveQyzs(string qyID, string befrom)
        {
            DataTable dt = DAL.RetrieveQyzs(qyID, befrom);

            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveQyxxViewList(string qyid)
        {
            DataTable dt = DAL.RetrieveQyxxViewList(qyid);


            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<List<IDataItem>> ReadZzmx(string ID)
        {
            DataTable dt = DAL.ReadZzmx(ID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }
        public FunctionResult<List<IDataItem>> ReadZsxx(string zsjlId)
        {
            DataTable dt = DAL.ReadZsxx(zsjlId);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result = list };
        }

        public string getZTreeOfZzmc()
        {
            return DAL.getZTreeOfZzmc();
        }



        #endregion

        public DataTable getCsywlxid(string qyID)
        {
            return DAL.getCsywlxid(qyID);
        }

        public string saveRegArea(string qyID, string city, string county)
        {
            string result = "OK";
            try
            {
                //BLLCommon.WriteLog("qyID: " + qyID + "city : " + city + ",county:" + county);
                DAL.UpdateRegArea(qyID, city, county);

            }
            catch (Exception ex)
            {
                result = ex.Message; 

            }
            return result;
        }

        #region 其它

        #endregion
    }
}
