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
    public partial class Modify : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();
        private int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userID = this.Request.QueryString["UserID"].ToInt32();

            if (!this.IsPostBack)
            {
                UserModel model = userManager.ReadUser(this.userID);
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

            UserModel model = userManager.ReadUser(this.userID);
            list.ToObject<UserModel>(model);

            userManager.UpdateUser(model);

            this.Response.Redirect("UserList.aspx", true);
        }
    }
}