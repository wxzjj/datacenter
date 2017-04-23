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
using Bigdesk8;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc
{
    public partial class Zljdzs : BasePage
    {
         private SzgcBLL BLL;
        private string rowID;
        protected string zljdbh, xmmc, sgdw,befrom;
        protected void Page_Load(object sender, EventArgs e)
        {

            BLL = new SzgcBLL(WorkUser);
            rowID = Request.QueryString["rowid"];
            befrom=Request.QueryString["befrom"];
            DataTable dt = BLL.GetZljdzs(rowID, befrom);
            if (dt.HasData())
            {
                zljdbh=dt.Rows[0]["zljdbh"].ToString();
                sgdw = dt.Rows[0]["sgdw"].ToString();
                xmmc = dt.Rows[0]["xmmc"].ToString();
            }
        }
    }
}
