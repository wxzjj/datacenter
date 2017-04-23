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
    public partial class Jsdw_View : BasePage
    {
        private SzqyBLL BLL;
        protected string rowID, jsdwID;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new SzqyBLL(WorkUser);
           // rowID = Request.QueryString["rowid"];
            jsdwID = Request.QueryString["jsdwid"];
            if (!IsPostBack)
            {
                List<IDataItem> list = BLL.ReadJsdwxx(jsdwID).Result;
                //if (string.IsNullOrEmpty(jsdwID) && list.Count > 0)
                //    jsdwID = list.GetDataItem("jsdwid").ItemData;
                if (list.Count <= 0)
                {
                    this.WindowAlert("没有获取到数据！");
                    return;
                }
                this.SetControlValue(list);

            }
        }


    }
}
