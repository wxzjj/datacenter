using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.UserRightManager.UserAttribute
{
    public partial class AttributeCreate : SystemBasePage
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

            AttributeModel model = new AttributeModel();
            list.ToObject<AttributeModel>(model);

            if (userManager.AttributeExists(model.AttributeName))
            {
                this.WindowAlert("特性名称已经存在了！");
                return;
            }

            model = userManager.CreateAttribute(model);

            this.Response.Redirect("AttributeList.aspx", true);
        }
    }
}