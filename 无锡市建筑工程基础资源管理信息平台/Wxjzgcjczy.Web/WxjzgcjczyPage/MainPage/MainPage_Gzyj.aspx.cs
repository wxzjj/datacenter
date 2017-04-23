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
    public partial class MainPage_Gzyj : BasePage
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

              
                SearchData_gzyj();
            }
        }


        /// <summary>
        /// 刷新
        /// </summary>
        void btnRefresh_Click(object sender, EventArgs e)
        {
            // SearchData_gzzs();
        }

     
        private void SearchData_gzyj()
        {
            DataTable dt = BLL.RetrieveGzyj().Result;
            DataList_Gzyj.DataSource = dt;
            DataList_Gzyj.DataBind();
            //Thread.Sleep(1000);
        }

    
        public static string subStr(string str, int leng)
        {
            if (str.Length <= leng)
            {
                return str;
            }
            string sNewStr = str.Substring(0, leng);
            sNewStr = sNewStr + "...";
            return sNewStr;
        }

        
     
    }
}
