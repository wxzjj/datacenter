using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL.Sqlserver;
using Bigdesk8;
using System.Data;
using Wxjzgcjczy.Common;


namespace Wxjzgcjczy.BLL
{
    public class XytxBLL
    {
        XytxDAL DAL;
        public AppUser WorkUser { get; set; }
        public XytxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            DAL = new XytxDAL();
            DAL.DB = new DatabaseOperator();
        }


        #region  数据列表
        public DataTable RetrieveQyxykp(string qylx, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " zzjgdm desc ";
            DataTable dt = DAL.RetrieveQyxykp(qylx, WorkUser, ft, pagesize, page, orderby, out allRecordCount);
         
            return dt;

        }
        /// <summary>
        /// 行政处罚列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveXzcf(string type, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            if (string.IsNullOrEmpty(orderby.Trim()))
                orderby = " lasj desc ";

            DataTable dt = DAL.RetrieveXzcf(type, WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;
        }
        #endregion 
    }
}
