using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    public partial class Yhxx_List : BasePage
    {
        public int HasAdd=0,HasEdit=0,HasDelete=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
           && p.operateCode.Equals(Yhgl_Operate.Add.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                HasAdd = 1;
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
          && p.operateCode.Equals(Yhgl_Operate.Edit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                HasEdit = 1;
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
        && p.operateCode.Equals(Yhgl_Operate.Delete.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                HasDelete = 1;
            }
        }
    }
}
