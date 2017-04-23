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
    public partial class RoleModify : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();
        private int roleID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.roleID = this.Request.QueryString["RoleID"].ToInt32();

            if (!this.IsPostBack)
            {
                RoleModel model = userManager.ReadRole(this.roleID);
                List<IDataItem> list = model.ToDataItem();
                this.SetControlValue(list);
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

            RoleModel model = userManager.ReadRole(this.roleID);
            list.ToObject<RoleModel>(model);

            userManager.UpdateRole(model);

            this.Response.Redirect("RoleList.aspx", true);
        }
    }
}