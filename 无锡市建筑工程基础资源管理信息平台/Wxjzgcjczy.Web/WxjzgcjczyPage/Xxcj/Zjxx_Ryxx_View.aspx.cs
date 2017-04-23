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
using System.Data;
using System.Text.RegularExpressions;
using Wxjzgcjczy.BLL;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    public partial class Zjxx_Ryxx_View : BasePage
    {
 
        public XmxxBLL BLL;
        public string  PkId;
        protected void Page_Load(object seder, EventArgs e)
        {
            PkId = Request.QueryString["pkid"];
            BLL = new XmxxBLL(WorkUser);

            if (!this.IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.ddl_sbdqbm, "Xzqdm", true);
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }

                FunctionResult<DataTable> result = BLL.Read_zj_gcjbxx_zrdw(PkId);

                if (result.Status != FunctionResultStatus.Error)
                {
                    if (result.Result.Rows.Count > 0)
                    {
                        List<IDataItem> list = result.Result.Rows[0].ToDataItem();
                        this.SetControlValue(list);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Message.Message + "');", true);
                    return;
                }

            }
        }
    }
}
