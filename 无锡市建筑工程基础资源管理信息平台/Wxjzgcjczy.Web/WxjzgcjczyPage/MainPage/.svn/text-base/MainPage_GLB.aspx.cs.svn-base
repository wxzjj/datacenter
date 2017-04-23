using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using System.Threading;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class MainPage_GLB : BasePage
    {
        public ZlctBLL BLL;
        protected string zjajxm, sxsq1, xsq1, hsq1, bhq1, xq1, jy1, yx1, jgajxm, sxsq2, xsq2, hsq2, bhq2, xq2, jy2, yx2;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);
            //SearchData_gzzs();
            if (!this.IsPostBack)
            {
      
                SearchData_jytj();
                SearchData_gzyj();
                //    SearchData_jytj();
                //    SearchData_Gwtz();
            }
        }

       

        /// <summary>
        /// 刷新
        /// </summary>
        void btnRefresh_Click(object sender, EventArgs e)
        {
            SearchData_gzzs();
        }

        protected void SearchData_jytj()
        {
            DataTable dt = BLL.GetJytj();
            DataList_Jytj.DataSource = dt;
            DataList_Jytj.DataBind();
          
        }

        private void SearchData_gzyj()
        {
            DataTable dt = BLL.RetrieveGzyj().Result;
            DataList_Gzyj.DataSource = dt;
            DataList_Gzyj.DataBind();
            //Thread.Sleep(1000);
        }

        public void SearchData_gzzs()
        {
            DataTable dt = BLL.RetrieveGzzs_NoRead().Result;
            this.DataList_gzzs.DataSource = dt;
            this.DataList_gzzs.DataBind();
        }
        

        public void SearchData_Gwtz()
        {
            DataTable dtGwtz = BLL.ReadGwtz();
            // 公文通知
            this.DataList_Gwtz.DataSource = dtGwtz;
            this.DataList_Gwtz.DataBind();
        }

        //protected void DataList_gzzs_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
        //}
    }
}
