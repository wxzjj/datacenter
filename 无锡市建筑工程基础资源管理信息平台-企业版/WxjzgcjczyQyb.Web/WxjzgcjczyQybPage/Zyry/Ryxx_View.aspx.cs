using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxjzgcjczyQyb.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Zyry
{
    public partial class Ryxx_View : BasePage
    {
        public ZcryBLL zcryBll;
        public string ryid;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            zcryBll = new ZcryBLL(this.WorkUser);
            ryid = Request.QueryString["ryid"].ToString();
            if (!IsPostBack)
            {
                SearData(0);

                if (!string.IsNullOrEmpty(txtSfzhm.Text.Trim()))
                {
                    txtSfzhm.Text = txtSfzhm.Text.Substring(0, 10) + "******" + txtSfzhm.Text.Substring(txtSfzhm.Text.Length - 2, 2);
                }
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            List<IDataItem> list= zcryBll.ReadRyxxView(ryid).Result;
            this.SetControlValue(list);
        }
    }
}