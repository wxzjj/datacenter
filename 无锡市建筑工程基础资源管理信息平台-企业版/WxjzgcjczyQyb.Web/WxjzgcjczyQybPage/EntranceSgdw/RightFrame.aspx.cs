using System;
using System.Collections.Generic;
using Bigdesk8;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Entrance
{
    public partial class RightFrame : BasePage2
    {
        public string iframeUrl = string.Empty , baseUrl;
        Dictionary<string,string> dic;
        public string slide = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WorkUser.UserID.ToString()!=string.Empty)
            {
                slide = Session["systemslide"].ToString2();
                if (slide == string.Empty)
                {
                    slide = "show";
                    Session["systemslide"] = "hide";
                }
                iframeUrl = Request.QueryString["url"].ToString();
                iframeUrl = iframeUrl.Replace(";", "&");

                WebCommon.ParseUrl(iframeUrl, out baseUrl, out dic);
                this.lb_nav.Text = dic["MenuText"].ToString();
            }
        }
    }
}