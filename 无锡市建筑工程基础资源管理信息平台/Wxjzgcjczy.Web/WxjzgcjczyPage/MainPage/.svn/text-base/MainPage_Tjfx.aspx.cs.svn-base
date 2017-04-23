using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class MainPage_Tjfx : BasePage
    {
        public ZlctBLL BLL;
        protected string zjajxm, sxsq1, xsq1, hsq1, bhq1, xq1, jy1, yx1, jgajxm, sxsq2, xsq2, hsq2, bhq2, xq2, jy2, yx2;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);

            //SearchData_gzzs();
            if (!this.IsPostBack)
            {


                SearchData_jytj();
            }
        }

        protected void SearchData_jytj()
        {
            DataTable dt = BLL.GetJytj();
            DataList_Jytj.DataSource = dt;
            DataList_Jytj.DataBind();

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
