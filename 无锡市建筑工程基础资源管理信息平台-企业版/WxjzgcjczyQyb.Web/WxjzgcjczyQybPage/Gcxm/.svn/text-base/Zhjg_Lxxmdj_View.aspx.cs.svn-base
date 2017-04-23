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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm
{
    public partial class Zhjg_Lxxmdj_View : BasePage
    {
        public string prjNum;
        GcxmBLL gcxm;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            prjNum = Request.QueryString["PrjNum"].ToString();
            gcxm = new GcxmBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData();
            }
        }

        public void SearchData()
        {
            DataTable dt = gcxm.GetXmxxByPrjNum(prjNum).Result;
            this.SetControlValue(dt.Rows[0].ToDataItem());

            if (!string.IsNullOrEmpty(dt.Rows[0]["jsdwID"].ToString2()))
            {
                this.hlk_Jsdw.NavigateUrl = "../Sczt/JsdwxxToolBar.aspx?jsdwid=" + dt.Rows[0]["jsdwID"].ToString2();
            }
            else
            {
                this.hlk_Jsdw.Enabled = false;
            }

            this.hlk_Jsdw.Text = dt.Rows[0]["BuildCorpName"].ToString2();
            DataTable dtdxxm = gcxm.GetDxXmxx(dt.Rows[0]["PKID"].ToString()).Result;
            this.Gdv_LxxmDxgcInfo.DataSource = dtdxxm;
            this.Gdv_LxxmDxgcInfo.DataBind();
        }
    }
}
