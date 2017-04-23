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
    public partial class Zhjg_Zlbj_View : BasePage
    {
        public string PKID;
        public string Zljdbm;
        GcxmBLL gcxm;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            PKID = Request.QueryString["PKID"].ToString();
            Zljdbm = Request.QueryString["Zljdbm"].ToString();
            gcxm = new GcxmBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData();
            }
        }

        public void SearchData()
        {
            DataTable dt = gcxm.GetZlbjxxByPKID(PKID).Result;
            this.SetControlValue(dt.Rows[0].ToDataItem());

            DataTable dtdxxm = gcxm.GetZlbjxxByZljdbm(Zljdbm).Result;
            this.Gdv_ZlbjZrryInfo.DataSource = dtdxxm;
            this.Gdv_ZlbjZrryInfo.DataBind();
        }
    }
}