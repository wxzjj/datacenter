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
    public partial class Zjxx_Edit : BasePage
    {
        public XmxxBLL BLL;
        public string Operate, PkId;

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new XmxxBLL(WorkUser);
            Operate = Request.QueryString["operate"];
            PkId = Request.QueryString["pkid"];
            if (!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(Zjxx_Operate.CreateOrEdit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }
                WebCommon.DropDownListDataBind(this.ddl_jglx, "PrjStructureType", true);
                if (Operate.Equals("edit", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.btnSelectLxxm.Visible = false;
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(Zjxx_Operate.CreateOrEdit.ToString(),StringComparison.CurrentCultureIgnoreCase)))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                return;
            }

            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + msg + "');", true);
                return;
            }

            List<IDataItem> list = this.GetControlValue();

            Decimal dec;
            if (!Decimal.TryParse(this.db_gczj.Text.Trim(), out dec))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_gczj.ItemNameCN + "格式不正确！');", true);
                return;
            }
            if (!string.IsNullOrEmpty(db_jzmj.Text) &&  !Decimal.TryParse(this.db_jzmj.Text.Trim(), out dec))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_jzmj.ItemNameCN + "格式不正确！');", true);
                return;
            }

            if (!string.IsNullOrEmpty(this.db_dlcd.Text) && !Decimal.TryParse(this.db_dlcd.Text.Trim(), out dec))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_dlcd.ItemNameCN + "格式不正确！');", true);
                return;
            }


            if ( !string.IsNullOrEmpty(this.db_zjzbm.Text))
            {
                Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");
                string zbdw_dwdm = this.db_zjzbm.Text;

                if (!reg_zzjgdm.IsMatch(zbdw_dwdm))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_zjzbm.ItemNameCN + "格式不正确！');", true);
                    return;
                }
            }


            if (Operate.Equals("add", StringComparison.CurrentCultureIgnoreCase))
            {
                FunctionResult<string> result = BLL.Save_zj_gcjbxx(list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result.Replace("'", "") + "');", true);
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

                FunctionResult<string> result = BLL.Update_zj_gcjbxx(this.PkId, list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result.Replace("'", "") + "');", true);
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
