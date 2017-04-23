using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class Index_Szqy : BasePage
    {
        protected string dwlx, nodeID, treeID;
        protected void Page_Load(object sender, EventArgs e)
        {
            dwlx = Request.QueryString["dwlx"];

            if (!IsPostBack)
            {
                switch (dwlx)
                {
                    default:
                    case "jsdw":
                        nodeID = "02010000";
                        treeID = "left_icon_2_1";
                        break;
                    case "kcsj":
                        nodeID = "02020000";
                        treeID = "left_icon_2_2";
                        break;
                    case "sgdw":
                        nodeID = "02030000";
                        treeID = "left_icon_2_3";
                        break;
                    case "zjjg":
                        nodeID = "02040000";
                        treeID = "left_icon_2_4";
                        break;
                    case "qt":
                        nodeID = "02050000";
                        treeID = "left_icon_2_5";
                        break;
                }
            }
        }
    }
}
