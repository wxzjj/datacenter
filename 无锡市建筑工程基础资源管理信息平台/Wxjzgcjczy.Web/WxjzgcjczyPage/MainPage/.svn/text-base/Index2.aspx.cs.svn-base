using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class Index2 : BasePage
    {
        public int HasXxcj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WorkUser.list.Exists(p => (p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase) || p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase))))
            {
                HasXxcj = 1;
            }
            if (!this.IsPostBack)
            {
                this.lblLoginTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblUserName.Text = this.WorkUser.UserName;

            }
        }
    }
}
