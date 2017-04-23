using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using WxjzgcjczyQyb.BLL;
using Bigdesk8.Business;
using System.Data;
using System.IO;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceJsdw
{
    public partial class WorkArea : BasePage2
    {
        private SundriesBFP sundriesBFP; 
        protected void Page_Load(object sender, EventArgs e)
        {
            sundriesBFP = new SundriesBFP();

            if (!IsPostBack)
            {
                label1.Text = WorkUser.UserName.ToString();
               
            }

        }
    }
}
