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
using Wxjzgcjczy.BLL;
using System.Data;
using System.Text.RegularExpressions;
using Wxjzgcjczy.Common;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    public partial class Ajxx_View : BasePage
    {
        public XmxxBLL BLL;
        public string PkId,aqjdbm;

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new XmxxBLL(WorkUser);
            PkId = Request.QueryString["pkid"];
            aqjdbm=Request.QueryString["aqjdbm"];
            if (!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    aqjdbm = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }

                WebCommon.DropDownListDataBind(this.ddl_gcgk_jglx, "PrjStructureType", true);

                FunctionResult<DataTable> result = BLL.Read_aj_gcjbxx(PkId);

                if (result.Status != FunctionResultStatus.Error)
                {
                    if (result.Result.Rows.Count > 0)
                    {
                        List<IDataItem> list = result.Result.Rows[0].ToDataItem();
                        this.SetControlValue(list);

                        result = BLL.Read_TBProjectInfo(list.GetDataItem("PrjNum").ItemData);

                        if (result.Status != FunctionResultStatus.Error)
                        {
                            if (result.Result.Rows.Count > 0)
                            {
                                list = result.Result.Rows[0].ToDataItem();
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
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Message.Message + "');", true);
                    return;
                }
            }
        }




    }
}
