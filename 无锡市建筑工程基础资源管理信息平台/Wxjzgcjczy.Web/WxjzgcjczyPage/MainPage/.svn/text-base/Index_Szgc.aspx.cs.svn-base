using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class Index_Szgc : BasePage
    {
        protected string state, xmlx, ssdq, nodeID, treeID;
        protected void Page_Load(object sender, EventArgs e)
        {

            state = Request.QueryString["state"];
            xmlx = Request.QueryString["xmlx"];
            ssdq = Request.QueryString["ssdq"];
            if (!IsPostBack)
            {
                if (xmlx == "aqjd")
                {
                    nodeID = "01010000";
                    treeID = "left_icon_1_1";
                }
                else
                {
                    nodeID = "01060000";
                    treeID = "left_icon_1_6";
                }
            }
        }
    }
}
