using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using System.Data;
using System.Text.RegularExpressions;
using Wxjzgcjczy.Common;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    public partial class Ajxx_Ryxx_View : BasePage
    {
        public XmxxBLL BLL;
        public string PkId;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new XmxxBLL(WorkUser);
            PkId = Request.QueryString["pkid"];

            if (!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }
                WebCommon.DropDownListDataBind(this.ddl_IDCardType, "IDCardType", false);

                FunctionResult<DataTable> result = BLL.Read_TBProjectBuilderUserInfo(PkId);

                if (result.Status != FunctionResultStatus.Error)
                {
                    if (result.Result.Rows.Count > 0)
                    {
                        List<IDataItem> list = result.Result.Rows[0].ToDataItem();
                        this.SetControlValue(list);
                        if (result.Result.Rows.Count > 0)
                        {
                            list = result.Result.Rows[0].ToDataItem();
                            this.SetControlValue(list);
                        }
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
