using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using Wxjzgcjczy.Common;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    public partial class Zjxx_View : BasePage
    {
        public string zljdbm;
        public XmxxBLL BLL;
        public string PkId;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new XmxxBLL(WorkUser);
            PkId = Request.QueryString["pkid"];
            zljdbm = Request.QueryString["zljdbm"];
            if (!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    zljdbm = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }
               
                   WebCommon.DropDownListDataBind(this.ddl_jglx, "PrjStructureType", true);

                    FunctionResult<DataTable> result = BLL.Read_zj_gcjbxx(PkId);

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
