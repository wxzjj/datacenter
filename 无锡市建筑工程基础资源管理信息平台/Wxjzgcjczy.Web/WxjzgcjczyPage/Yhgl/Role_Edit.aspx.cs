using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    public partial class Role_Edit : BasePage
    {
        public string RoleId;
        public string Operate;
        public YhglBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoleId=Request.QueryString["id"];
            Operate=Request.QueryString["operate"];
            BLL = new YhglBLL(WorkUser);
            if(!this.IsPostBack)
            {
                if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
            && (p.operateCode.Equals(Yhgl_Operate.Add.ToString(), StringComparison.CurrentCultureIgnoreCase) || p.operateCode.Equals(Yhgl_Operate.Edit.ToString(), StringComparison.CurrentCultureIgnoreCase))))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('您没有权限进行此操作！');", true);
                    return;
                }
                if (Operate.Equals("edit"))
                {
                    DataTable dt = BLL.ReadRole(this.RoleId).Result;
                    if (dt.Rows.Count > 0)
                    {
                       this.SetControlValue( dt.Rows[0].ToDataItem());
                    }
                }

            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
              && (p.operateCode.Equals(Yhgl_Operate.Add.ToString(), StringComparison.CurrentCultureIgnoreCase) || p.operateCode.Equals(Yhgl_Operate.Edit.ToString(), StringComparison.CurrentCultureIgnoreCase))))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('您没有权限进行此操作！');", true);
                return;
            }
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + msg + "');", true);
                return;
            }

            List<IDataItem> list = this.GetControlValue();
            if (Operate.Equals("edit"))
            {
                if (BLL.Edit_Role(RoleId, list))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "f_SaveResult('角色信息修改成功！');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('角色信息修改成功！');", true);
                    return;
                }
            }
            else
            {
                if (BLL.Add_Role(list))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "f_SaveResult('角色信息创建成功！');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('角色信息创建成功！');", true);
                    return;
                }
            }
        }
    }
}
