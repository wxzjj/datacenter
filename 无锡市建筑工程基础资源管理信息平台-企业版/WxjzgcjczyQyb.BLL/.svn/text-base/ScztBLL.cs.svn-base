using System;
using System.Collections.Generic;
using System.Text;
using WxjzgcjczyQyb.DAL;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;
namespace WxjzgcjczyQyb.BLL
{
    public class ScztBLL
    {
        private ScztDAL scztDal;
        private AppUser WorkUser;
        public ScztBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            scztDal = new ScztDAL();
            scztDal.DB = new DatabaseOperator();
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="qylx"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveQyxxList(string qylx, List<IDataItem> list, int pagesize, int page, out int allRecordCount)
        {
            string orderby = string.Empty;
            allRecordCount = 0;

            if (qylx == "jsdw")
            {
                orderby = " xgrqsj desc ";
            }
            else
                orderby = "xgrqsj desc";

            DataTable dt = scztDal.RetrieveQyxxList(qylx, list, pagesize, page, orderby, out  allRecordCount);
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
                        csywlx = scztDal.GetCsywlx(qyid);
                        dr["csywlx"] = csywlx;
                    }
                }
            }
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> ReadJsdwxx(string rowID)
        {
            DataTable dt = scztDal.ReadJsdw(rowID);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveJsdwSsgc(string jsdwID)
        {
            DataTable dt = scztDal.RetrieveJsdwSsgc(jsdwID);

            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<List<IDataItem>> ReadQyxx(string qyID)
        {
            DataTable dt = scztDal.ReadQyxx(qyID);
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

        public FunctionResult<DataTable> RetrieveQyxxViewList(string qyid)
        {
            DataTable dt = scztDal.RetrieveQyxxViewList(qyid);


            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveQyzs(string qyID)
        {
            DataTable dt = scztDal.RetrieveQyzs(qyID);

            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveZyry(string qyID, List<IDataItem> list, int pagesize, int page, string orderby, out int allRecordCount)
        {
            orderby = " zsjlId ";

            DataTable dt = scztDal.RetrieveZyry(qyID, list, pagesize, page, orderby, out  allRecordCount);


            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveQyclgc(string qyID, int pagesize, int page, string orderby, out int allRecordCount)
        {
            DataTable dt = scztDal.RetrieveQyclgc(qyID, pagesize, page, orderby, out   allRecordCount);

            return new FunctionResult<DataTable>() { Result = dt };
        }


    }
}
