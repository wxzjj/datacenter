using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.UserRightManager.UserInfo
{
    public partial class Create : SystemBasePage
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

            UserModel model = new UserModel();
            list.ToObject<UserModel>(model);

            if (userManager.UserExists(model.LoginName))
            {
                this.WindowAlert("登录名称已经存在了！");
                return;
            }

            model = userManager.CreateUser(model);

            this.Response.Redirect("UserList.aspx", true);
        }
    }
}