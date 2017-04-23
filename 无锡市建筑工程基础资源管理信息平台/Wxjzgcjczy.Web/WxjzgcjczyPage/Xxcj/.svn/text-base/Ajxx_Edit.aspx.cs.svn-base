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
    public partial class Ajxx_Edit : BasePage
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
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(Ajxx_Operate.CreateOrEdit.ToString())))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }

                WebCommon.DropDownListDataBind(this.ddl_gcgk_jglx, "PrjStructureType", true);
                if (Operate.Equals("edit", StringComparison.CurrentCultureIgnoreCase))
                {
                    this.btnSelectLxxm.Visible = false;
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase) 
                && p.operateCode.Equals(Ajxx_Operate.CreateOrEdit.ToString(), StringComparison.CurrentCultureIgnoreCase)))
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
            if (!Decimal.TryParse(this.db_gcgk_yszj.Text.Trim(), out dec))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_gcgk_yszj.ItemNameCN + "格式不正确！');", true);
                return;
            }
            if (!Decimal.TryParse(this.db_gcgk_jzmj.Text.Trim(), out dec))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_gcgk_jzmj.ItemNameCN + "格式不正确！');", true);
                return;
            }

            //if (!Decimal.TryParse(this.db_gis_jd.Text.Trim(), out dec))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_gis_jd.ItemNameCN + "格式不正确！');", true);
            //    return;
            //}
            //if (!Decimal.TryParse(this.db_gis_wd.Text.Trim(), out dec))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_gis_wd.ItemNameCN + "格式不正确！');", true);
            //    return;
            //}


            Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");
            string zbdw_dwdm = this.db_zbdw_dwdm.Text;

            if (!reg_zzjgdm.IsMatch(zbdw_dwdm))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_zbdw_dwdm.ItemNameCN + "格式不正确！');", true);
                return;
            }
            string zbdw_zcjzsdm = this.db_zbdw_zcjzsdm.Text;

            Regex reg_TWSfzh = new Regex(@"[A-z]\d{17}");
            if (!Utilities.IsValidCardNo(zbdw_zcjzsdm) && !reg_TWSfzh.IsMatch(zbdw_zcjzsdm))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "showWarn('" + this.db_zbdw_zcjzsdm.ItemNameCN + "格式不正确！');", true);
                return;

            }


            if (Operate.Equals("add", StringComparison.CurrentCultureIgnoreCase))
            {
                FunctionResult<string> result = BLL.Save_aj_gcjbxx(list);
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

                FunctionResult<string> result = BLL.Update_aj_gcjbxx(this.PkId, list);
                if (result.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScript", "f_SaveResult('" + result.Result.Replace("'", "") + "');", true);
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
