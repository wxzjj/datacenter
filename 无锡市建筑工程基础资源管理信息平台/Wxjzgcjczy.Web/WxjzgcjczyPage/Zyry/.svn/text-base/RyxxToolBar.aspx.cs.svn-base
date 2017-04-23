using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry
{
    public partial class RyxxToolBar : BasePage
    {
        private ZyryBLL BllZyry;
        protected string ryID, befrom;
        protected string rylx;
        protected void Page_Load(object sender, EventArgs e)
        {
            BllZyry = new ZyryBLL(this.WorkUser);
            ryID = Request.QueryString["ryid"];
            befrom = Request.QueryString["befrom"];
            rylx = Request.QueryString["rylx"];

            if (string.IsNullOrEmpty(rylx))
            {
                string ryzyzglxid = BllZyry.GetRyzglxid(ryID);
                if (ryzyzglxid == "1" || ryzyzglxid == "2" || ryzyzglxid == "21" || ryzyzglxid == "41" || ryzyzglxid == "51" || ryzyzglxid == "61")
                    rylx = "zczyry";
                else if (ryzyzglxid == "4" || ryzyzglxid == "5" || ryzyzglxid == "6")
                    rylx = "aqscglry";
                else if (ryzyzglxid == "20" )
                    rylx = "qyjjry";
                else if (ryzyzglxid == "7" || ryzyzglxid == "8" || ryzyzglxid == "9" || ryzyzglxid == "11" || ryzyzglxid == "12" || ryzyzglxid == "14" || ryzyzglxid == "15" || ryzyzglxid == "16" || ryzyzglxid == "17" || ryzyzglxid == "18" || ryzyzglxid == "22" || ryzyzglxid == "42")
                    rylx = "zygwglry";
                else rylx = "";
            }
        }
    }
}
