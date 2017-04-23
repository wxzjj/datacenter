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
using Bigdesk8.Security;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm
{
    public partial class Zhjg_Htba_View : BasePage
    {
        protected string PKID;
        GcxmBLL gcxm;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            //实例化
            PKID = Request.QueryString["PKID"];
            gcxm = new GcxmBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData();
            }
        }


        public void SearchData()
        {
            DataTable dt = gcxm.GetHtbaByPKID(PKID).Result;
            this.SetControlValue(dt.Rows[0].ToDataItem());
            this.hlk_PropietorCorpName.Text = dt.Rows[0]["PropietorCorpName"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["jsdwID"].ToString()))
            {
                this.hlk_PropietorCorpName.Enabled = false;
            }
            else
            {
                this.hlk_PropietorCorpName.NavigateUrl = string.Format("../Sczt/JsdwxxToolBar.aspx?jsdwid={0}", dt.Rows[0]["jsdwID"]);
            }

            this.hlk_ContractorCorpName.Text = dt.Rows[0]["ContractorCorpName"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["qyID"].ToString()))
            {
                this.hlk_ContractorCorpName.Enabled = false;
            }
            else
            {
                this.hlk_ContractorCorpName.NavigateUrl = string.Format("../Sczt/QyxxToolBar.aspx?qyid={0}", dt.Rows[0]["qyID"]);
            }

            this.hlk_PrjHead.Text = dt.Rows[0]["PrjHead"].ToString();
            if (dt.Rows[0]["ContractTypeNum"].ToString().IndexOf("301,302,304") >= 0)
            {
                this.hlk_PrjHead.NavigateUrl = string.Format("../Zyry/RyxxToolBar.aspx?ryid={0}&rylx=zczyry", dt.Rows[0]["ryID"]);
            }

            if (dt.Rows[0]["tag"].ToString() == "省一体化平台")
            {
                this.hl_htbaxxView.Enabled = true;
                string htbabm = dt.Rows[0]["RecordInnerNum"].ToString();
                string key = SecurityUtility.MD5(dt.Rows[0]["RecordInnerNum"].ToString() + "htba" + DateTime.Now.ToString("yyyyMMdd"));
                this.hl_htbaxxView.NavigateUrl = string.Format("http://58.213.147.230:8089/Jsjzyxyglpt/faces/sghtba.jsp?htbabm={0}&key={1}", htbabm, key);
            }
            else
            {
                this.hl_htbaxxView.Enabled = false;
            }

        }
      
    }
}
