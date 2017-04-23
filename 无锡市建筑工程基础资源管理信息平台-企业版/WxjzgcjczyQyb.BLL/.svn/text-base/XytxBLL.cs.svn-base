using System;
using System.Collections.Generic;
using System.Text;
using WxjzgcjczyQyb.DAL;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;

namespace WxjzgcjczyQyb.BLL
{
    public class XytxBLL
    {
        private XytxDAL xytxDal;
        private AppUser WorkUser;
        public XytxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            xytxDal = new XytxDAL();
            xytxDal.DB = new DatabaseOperator();
        }

        public FunctionResult<DataTable> RetrieveQyxykp(List<IDataItem> list,int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            DataTable dt = xytxDal.RetrieveQyxykp(list, pageSize, pageIndex, orderby, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }
    }
}
