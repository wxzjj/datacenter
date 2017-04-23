using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL.Sqlserver;
using System.Data;

namespace Wxjzgcjczy.BLL
{
    public class XxgxBLL
    {
        public AppUser WorkUser { get; set; }
        public XxgxDAL DAL;
        public XxgxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            DAL = new XxgxDAL();
             DAL.DB = new DatabaseOperator();
        }

        #region  获取列表
        /// <summary>
        /// 获取各服务运行情况信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetXxgx_Csjk()
        {
            //DataTable dt_csjl=new DataTable();
            //dt_csjl.Columns.Add("name");
            //dt_csjl.Columns.Add("name");
            //dt_csjl.Columns.Add("value");
            DataTable dt = DAL.GetXxgx_Csjk();
            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRow row = dt_csjl.NewRow();
            //    row[0] = item["DataFlow"];
            //    if (Int32.Parse(item["IsOk"].ToString()) > 0)
            //    {
            //        row[1] = "1";
            //    }
            //    else
            //    {
            //        row[1] = "0";
            //    }
            //    dt_csjl.Rows.Add(row);
            //}

            return dt;

        }
        #endregion

        #region  

        #endregion
    }
}
