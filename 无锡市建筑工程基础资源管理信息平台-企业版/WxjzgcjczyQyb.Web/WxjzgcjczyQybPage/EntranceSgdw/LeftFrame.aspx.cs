using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using System.Data;
using WxjzgcjczyQyb;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Entrance
{
    /// <summary>
    /// 左侧框架页：管理版
    /// </summary>
    public partial class LeftFrame : BasePage2
    {
        public string parentNodeID = string.Empty;
        public string baseimgurl = string.Empty;
        public string theme = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            parentNodeID = Request.QueryString["parentNodeID"];
            this.baseimgurl = @"../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/";

            if (!IsPostBack)
            {
                this.Page.RegisterStartupScript(parentNodeID, "<script>initmenu(" + parentNodeID + ")</script>");
            }

        }
 
    }
}
