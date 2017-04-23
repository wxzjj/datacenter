using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage
{
    public partial class Page_List : System.Web.UI.Page
    {
        public string menuno = string.Empty, itemno = string.Empty;
        public string contentId = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string iframeUrl = Request.RawUrl;
            string baseUrl = "";
            Dictionary<string, string> dic;
            iframeUrl = iframeUrl.Replace(";", "&");
            WebCommon.ParseUrl(iframeUrl, out baseUrl, out dic);

            menuno = dic["menuno"].ToString();  //一级菜单编号
            if (dic.ContainsKey("itemno"))
                itemno = dic["itemno"].ToString();        //左侧菜单编号
            if (dic.ContainsKey("contentId"))
                contentId = dic["contentId"].ToString();       //记录Id

        }
    }
}
