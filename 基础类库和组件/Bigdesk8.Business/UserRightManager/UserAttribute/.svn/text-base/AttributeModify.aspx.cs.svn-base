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
    public partial class AttributeModify : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();
        private int attributeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.attributeID = this.Request.QueryString["AttributeID"].ToInt32();

            if (!this.IsPostBack)
            {
                AttributeModel model = userManager.ReadAttribute(this.attributeID);
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

            AttributeModel model = userManager.ReadAttribute(this.attributeID);
            list.ToObject<AttributeModel>(model);

            userManager.UpdateAttribute(model);

            this.Response.Redirect("AttributeList.aspx", true);
        }
    }
}