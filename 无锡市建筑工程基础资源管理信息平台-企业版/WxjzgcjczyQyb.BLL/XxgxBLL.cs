using System;
using System.Collections.Generic;
using System.Text;
using WxjzgcjczyQyb.DAL;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;

namespace WxjzgcjczyQyb.BLL
{
    public class XxgxBLL
    {
        public XxgxDAL xxgxDal;
        public AppUser WorkUser;
        public XxgxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            xxgxDal = new XxgxDAL();
            xxgxDal.DB = new DatabaseOperator();
        }

        public DataTable RetrieveApiZb(List<IDataItem> list, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            return xxgxDal.RetrieveApiZb(list, pageSize, pageIndex, orderby, out  allRecordCount);
        }

        public void UpdateApiZbApiControl(string apiFlow, string apiControl)
        {
            xxgxDal.UpdateApiZbApiControl(apiFlow, apiControl);
        }

        public DataTable GetApizbByApiFlow(string apiFlow)
        {
            return xxgxDal.GetApizbByApiFlow(apiFlow);
        }


        public DataTable RetrieveApiCb(List<IDataItem> list, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            return xxgxDal.RetrieveApiCb(list, pageSize, pageIndex, orderby, out  allRecordCount);
        }

        public DataTable GetStLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string sfsccg)
        {
            return xxgxDal.GetStLog(condition, pageSize, pageIndex, out   allRecordCount, sfsccg, " UpdateDate desc");
        }

        public DataTable GetRecordItem(string tableName, string pkid)
        {
            return xxgxDal.GetRecordItem(tableName, pkid);
        }

        public DataTable GetSyzxspt(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string tableName)
        {
            return xxgxDal.GetSyzxspt(condition, pageSize, pageIndex, out   allRecordCount, " UpdateTime desc", tableName);
        }
    }
}
