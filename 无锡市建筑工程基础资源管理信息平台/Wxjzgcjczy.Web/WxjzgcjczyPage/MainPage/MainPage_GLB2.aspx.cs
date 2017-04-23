using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class MainPage_GLB2 : BasePage
	{
        public ZlctBLL BLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);
            //SearchData_gzzs();
            if (!this.IsPostBack)
            {
                //SearchData_jytj();
                //SearchData_Gwtz();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            SearchData_gzzs();
        }

        public void SearchData_gzzs()
        {
            DataTable dt = BLL.RetrieveGzzs_NoRead().Result;
            this.DataList_gzzs.DataSource = dt;
            this.DataList_gzzs.DataBind();
        }

        public void SearchData_jytj()
        {
            DataTable dt = BLL.RetrieveJytj().Result;
            this.DataList_Jytj.DataSource = dt;
            this.DataList_Jytj.DataBind();
        }

        public void SearchData_Gwtz()
        {
            DataTable dtGwtz = BLL.ReadGwtz();
            // 公文通知
            this.DataList_Gwtz.DataSource = dtGwtz;
            this.DataList_Gwtz.DataBind();
        }
	}
}
