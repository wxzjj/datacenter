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
    public class TjfxBLL
    {
        public AppUser WorkUser { get; set; }
        private readonly TjfxDAL DAL = new TjfxDAL();

        public TjfxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();

        }

        public FunctionResult<DataTable> RetrieveTjfx_Sgdwzz()
        {
            DataTable dt = DAL.RetrieveTjfx_Sgdwzz(this.WorkUser);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_SgdwzzTj()
        {
            DataTable dt = DAL.RetrieveTjfx_SgdwzzTj(this.WorkUser);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_ZyrylxTj()
        {
            DataTable dt = DAL.RetrieveTjfx_ZyrylxTj(this.WorkUser);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_XmTj_Count(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_XmTj_Count(xmfl, bdateStart, bdateEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_XmTj_Count_Jg(string xmfl, string edateStart, string edateEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_XmTj_Count_Jg(xmfl, edateStart, edateEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_XmTj_Ztz(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_XmTj_Ztz(xmfl, bdateStart, bdateEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveTjfx_XmTj_Zmj(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_XmTj_Zmj(xmfl, bdateStart, bdateEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_XmTj_Zmj_Jg(string xmfl, string edateStart, string edateEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_XmTj_Zmj_Jg(xmfl, edateStart, edateEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveTjfx_SgxkTj_Count(string fzrqStart, string fzrqEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_SgxkTj_Count(fzrqStart, fzrqEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_SgxkTj_Zmj(string fzrqStart, string fzrqEnd, string ssdq)
        {
            DataTable dt = DAL.RetrieveTjfx_SgxkTj_Zmj(fzrqStart, fzrqEnd, ssdq);
            return new FunctionResult<DataTable>() { Result = dt };
        }



        public FunctionResult<DataTable> RetrieveTjfx_HtBaTj_Count(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            DataTable dt = DAL.RetrieveTjfx_HtBaTj_Count(htqdRqStart, htqdRqEnd, ssdq, htlb);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveTjfx_HtBaTj_Htje(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            DataTable dt = DAL.RetrieveTjfx_HtBaTj_Htje(htqdRqStart, htqdRqEnd, ssdq, htlb);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> RetrieveTjfx_Htba_Report(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            DataTable dt = DAL.RetrieveTjfx_Htba_Report(htqdRqStart, htqdRqEnd, ssdq, htlb);
            return new FunctionResult<DataTable>() { Result = dt };
        }
    }
}
