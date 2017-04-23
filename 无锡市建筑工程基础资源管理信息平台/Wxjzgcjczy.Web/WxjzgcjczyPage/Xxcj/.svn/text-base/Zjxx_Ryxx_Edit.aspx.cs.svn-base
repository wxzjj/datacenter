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
    public partial class Zjxx_Ryxx_Edit : BasePage
    {
        public string zljdbm;
        public XmxxBLL BLL;
        public string Operate, PkId;
        protected void Page_Load(object seder, EventArgs e)
        {
            zljdbm=Request.QueryString["zljdbm"];
            Operate = Request.QueryString["operate"];
            PkId = Request.QueryString["pkid"];
            BLL = new XmxxBLL(WorkUser);

            if (!this.IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.ddl_sbdqbm, "Xzqdm", true);
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(Zjxx_Operate.CreateOrEdit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }

                if (Operate.Equals("edit", StringComparison.CurrentCultureIgnoreCase))
                {
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + msg + "');", true);
                return;
            }
            Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");
            string corpCode = this.db_dwdm.Text;

            if (!reg_zzjgdm.IsMatch(corpCode))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_dwdm.ItemNameCN + "格式不正确！');", true);
                return;
            }
            Regex reg_TWSfzh = new Regex(@"^[A-z]\d{17}$");

            if (!Utilities.IsValidCardNo(this.db_xmfzrdm.Text) && !reg_TWSfzh.IsMatch(this.db_xmfzrdm.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_xmfzrdm.ItemNameCN + "格式不正确！');", true);
                return;

            }

            List<IDataItem> list = this.GetControlValue();

            if (Operate.Equals("add", StringComparison.CurrentCultureIgnoreCase))
            {
                FunctionResult<string> result = BLL.Save_zj_gcjbxx_zrdw(zljdbm, list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Result.Replace("'", "") + "');", true);
                    return;
                }
            }
            else
            {

                FunctionResult<string> result = BLL.Update_zj_gcjbxx_zrdw(this.PkId, list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Result.Replace("'", "") + "');", true);
                    return;
                }
            }
        }
    }
}
