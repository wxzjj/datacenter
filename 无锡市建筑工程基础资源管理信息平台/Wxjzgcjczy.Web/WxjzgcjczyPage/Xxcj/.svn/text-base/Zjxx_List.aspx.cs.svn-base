using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
   
    public partial class Zjxx_List : BasePage
    {
        public int HasView = 0, HasCreateOrEdit = 0;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (ModuleOperate item in WorkUser.list)
            {
                if (item.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && item.operateCode.Equals(Zjxx_Operate.View.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    HasView = 1;
                }
                if (item.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && item.operateCode.Equals(Zjxx_Operate.CreateOrEdit.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    HasCreateOrEdit = 1;
                }
                
            }
        }


    }
}
