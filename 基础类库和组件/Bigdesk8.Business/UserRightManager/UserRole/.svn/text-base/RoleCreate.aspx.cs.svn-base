using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.UserRightManager.UserRole
{
    public partial class RoleCreate : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 设置默认值
                this.SetControlDefaultValue(false);
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string result = this.CheckControlValue();
            if (!result.IsEmpty())
            {
                this.WindowAlert(result);
                return;
            }

            List<IDataItem> list = this.GetControlValue();

            RoleModel model = new RoleModel();
            list.ToObject<RoleModel>(model);

            if (userManager.RoleExists(model.RoleName))
            {
                this.WindowAlert("角色名称已经存在了！");
                return;
            }

            model = userManager.CreateRole(model);

            this.Response.Redirect("RoleList.aspx", true);
        }
    }
}