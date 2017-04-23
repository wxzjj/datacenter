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

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc
{
    public partial class Sgxk_View : BasePage
    {
        private SzgcBLL BLL;
        protected string sgxkID, rowID;
        protected string befrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new SzgcBLL(WorkUser);
            rowID = Request.QueryString["rowID"];
            sgxkID = Request.QueryString["sgxkid"];
            befrom = Request.QueryString["befrom"];
            List<IDataItem> list = BLL.ReadXmxx("sgxk", rowID, befrom).Result;
            if (list.Count <= 0)
            {
                this.WindowAlert("没有获取到数据！");
                return;
            }
            this.SetControlValue(list);
        }
    }
}
