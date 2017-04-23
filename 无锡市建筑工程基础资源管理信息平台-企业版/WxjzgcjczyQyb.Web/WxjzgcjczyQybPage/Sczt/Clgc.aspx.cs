using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WxjzgcjczyQyb.BLL;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt
{
    public partial class Clgc : BasePage
    {
        public string qyid;
        ScztBLL scztBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            qyid = Request.QueryString["qyid"].ToString();
            scztBll = new ScztBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData(0);
            }
        }

        public void SearchData(int pageIndex)
        {
            int allRecordCount;
            DataTable dt = scztBll.RetrieveQyclgc(qyid, this.gridView.PageSize, pageIndex, " ContractDate desc", out allRecordCount).Result;
            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        //分页
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SearchData(e.NewPageIndex);
        }

    }
}