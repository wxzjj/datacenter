using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class Index_Zyry : System.Web.UI.Page
    {
        protected string zyry, rylx, nodeID, treeID;
        protected void Page_Load(object sender, EventArgs e)
        {
            zyry=Request.QueryString["zyry"];
            rylx=Request.QueryString["rylx"];
            
            switch (zyry)
            {
                default:
                case "zczyry":
                    nodeID = "03010000";
                    treeID = "left_icon_3_1";
                    break;
                case "aqscglry":
                    nodeID = "03020000";
                    treeID = "left_icon_3_2";
                    break;
                case "qyjjry":
                    nodeID = "03030000";
                    treeID = "left_icon_3_3";
                    break;
                case "zygwglry":
                    nodeID = "03040000";
                    treeID = "left_icon_3_4";
                    break;
            }
        }
    }
}
