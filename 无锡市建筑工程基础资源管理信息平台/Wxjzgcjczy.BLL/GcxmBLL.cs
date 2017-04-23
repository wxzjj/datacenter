using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL.Sqlserver;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8;

namespace Wxjzgcjczy.BLL
{
    public class GcxmBLL
    {
         public AppUser WorkUser { get; set; }
         public readonly GcxmDAL DAL = new GcxmDAL();

        public GcxmBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();

        }

        public DataTable GetTable(string sql,SqlParameterCollection sp,string tablaName)
        {
            return this.DAL.DB.ExeSqlForDataTable(sql, sp, tablaName);
        }


        public FunctionResult<DataTable> RetrieveKcsjht( List<IDataItem> conditions, int pagesize, int page,  out int allRecordCount)
        {
            DataTable dt = DAL.RetrieveKcsjht( conditions, pagesize, page, " ContractDate desc ", out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveSgjlht(List<IDataItem> conditions, int pagesize, int page, out int allRecordCount)
        {
            DataTable dt = DAL.RetrieveSgjlht(conditions, pagesize, page, " ContractDate desc ", out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

    }
}
