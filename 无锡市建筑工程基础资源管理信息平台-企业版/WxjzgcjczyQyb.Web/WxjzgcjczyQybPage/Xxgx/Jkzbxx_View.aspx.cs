using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WxjzgcjczyQyb.BLL;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx
{
    public partial class Jkzbxx_View : BasePage2
    {
        public string apiFlow;
        XxgxBLL xxgxBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            apiFlow = Request.QueryString["apiFlow"].ToString();
            xxgxBll = new XxgxBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData();
            }
        }

        public void SearchData()
        {
            DataTable dt = xxgxBll.GetApizbByApiFlow(apiFlow);
            this.SetControlValue(dt.Rows[0].ToDataItem());

            if (dt.Rows[0]["apiRunState"].ToString() == "1")
            {
                DBText12.Text = "正常";
            }
            else
            {
                DBText12.Text = "异常";
            }

            if (dt.Rows[0]["apiControl"].ToString() == "1")
            {
                DBText3.Text = "开启";
            }
            else
            {
                DBText3.Text = "关闭";
            }


            DBText1.Text = DBText1.Text.Replace("</br>", Environment.NewLine);

        }
    }
}
