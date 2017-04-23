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

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy
{
    public partial class Zzxx : BasePage
    {
        private SzqyBLL BLL;
        protected string qyID, rowID;
        private string befrom;
        protected void Page_Load(object sender, EventArgs e)
        {
           // BLL = new SzqyBLL(WorkUser);
            qyID = Request.QueryString["qyid"];
            //rowID = Request.QueryString["rowID"];
            befrom = Request.QueryString["befrom"];
            if (!IsPostBack)
            {
                //List<IDataItem> list = BLL.ReadQyxx(qyID).Result;
                
                //this.SetControlValue(list);
            }
        }
    }
}
