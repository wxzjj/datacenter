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
    public partial class Ajxx_Ryxx_Edit : BasePage
    {
        public XmxxBLL BLL;
        public string Operate, PkId, aqjdbm;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new XmxxBLL(WorkUser);
            Operate = Request.QueryString["operate"];
            PkId = Request.QueryString["pkid"];
            aqjdbm = Request.QueryString["aqjdbm"];

            if (!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(Ajxx_Operate.CreateOrEdit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }
                WebCommon.DropDownListDataBind(this.ddl_IDCardType, "IDCardType", false);
                if (Operate.Equals("edit", StringComparison.CurrentCultureIgnoreCase))
                {
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + msg + "');", true);
                return;
            }
            Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");
            string corpCode = this.db_CorpCode.Text;

            if (!reg_zzjgdm.IsMatch(corpCode))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_CorpCode.ItemNameCN + "格式不正确！');", true);
                return;
            }
            Regex reg_TWSfzh = new Regex(@"^[A-z]\d{17}$");

            if (!Utilities.IsValidCardNo(this.db_IDCard.Text) && !reg_TWSfzh.IsMatch(this.db_IDCard.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_IDCard.ItemNameCN + "格式不正确！');", true);
                return;

            }

           

            List<IDataItem> list = this.GetControlValue();

            if (Operate.Equals("add", StringComparison.CurrentCultureIgnoreCase))
            {
                FunctionResult<string> result = BLL.Save_TBProjectBuilderUserInfo(aqjdbm,list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Result.Replace("'","") + "');", true);
                    return;
                }
            }
            else
            {

                FunctionResult<string> result = BLL.Update_TBProjectBuilderUserInfo(this.PkId, list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showError('" + result.Result.Replace("'","") + "');", true);
                    return;
                }
            }
        }
    }
}
