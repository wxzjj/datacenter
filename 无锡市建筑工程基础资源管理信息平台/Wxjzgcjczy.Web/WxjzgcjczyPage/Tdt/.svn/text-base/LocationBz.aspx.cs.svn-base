using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;




namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt
{
    public partial class LocationBz : BasePage
    {
      
        XmxxBLL BLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            string  pkid=Request.QueryString["PKID"];
            BLL=new XmxxBLL(WorkUser);
            if (!this.IsPostBack)
            {
                this.db_PKID.Text = pkid;
                DataTable dt = BLL.GetXmxx(pkid);
                if (dt.Rows.Count > 0)
                {
                    List<IDataItem> list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);

                }

            }

        }
    }
}
