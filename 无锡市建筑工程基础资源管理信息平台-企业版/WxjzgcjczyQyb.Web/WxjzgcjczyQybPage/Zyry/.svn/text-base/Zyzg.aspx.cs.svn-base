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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Zyry
{
    public partial class Zyzg : BasePage
    {
        public string ryid;
        public string rylx;
        ZcryBLL zcryBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            ryid = Request.QueryString["ryid"].ToString();
            rylx = Request.QueryString["rylx"].ToString();
            zcryBll = new ZcryBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData();
            }
        }

        public void SearchData()
        {
            DataTable dt = zcryBll.RetrieveZyryJbxxViewList(ryid,rylx).Result;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }
    }
}
