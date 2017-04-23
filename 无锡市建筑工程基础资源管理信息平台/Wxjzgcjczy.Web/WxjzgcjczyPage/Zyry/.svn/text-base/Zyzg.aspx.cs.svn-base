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
    public partial class Zyzg : BasePage
    {
        private ZyryBLL BLL;
        protected string rowID, ryID, befrom,rylx;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZyryBLL(WorkUser);
            ryID = Request.QueryString["ryid"];
            //rowID = Request.QueryString["rowID"];
            befrom = Request.QueryString["befrom"];
            rylx=Request.QueryString["rylx"];
            if (!IsPostBack)
            {
                //List<IDataItem> list = BLL.ReadRyxx(ryID).Result;

                //if (string.IsNullOrEmpty(ryID) && list.Count > 0)
                //    ryID = list.GetDataItem("ryid").ItemData;

            }
        }
    }
}
