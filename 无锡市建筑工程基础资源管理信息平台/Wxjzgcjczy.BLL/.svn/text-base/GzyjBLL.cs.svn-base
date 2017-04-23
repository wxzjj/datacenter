using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Wxjzgcjczy.DAL.Sqlserver;
namespace Wxjzgcjczy.BLL
{
    public class GzyjBLL
    {

        public AppUser WorkUser { get; set; }
        private readonly GzyjDAL DAL = new GzyjDAL();

        public GzyjBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();

        }


        public FunctionResult<DataTable> Retrieve(string fromewhere, AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            //if (xmlx == "aqjd")
            //    orderby = " sortid,row_id desc ";
            //else
           
            DataTable dt = DAL.Retrieve(fromewhere, workUser, ft, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<List<IDataItem>> Read(string rowID)
        {
            DataTable dt = DAL.Read(rowID);
            List<IDataItem> list = new List<IDataItem>();
            if (dt.HasData())
                list = dt.Rows[0].ToDataItem();
            return new FunctionResult<List<IDataItem>>() { Result=list};
        }
    }
}
