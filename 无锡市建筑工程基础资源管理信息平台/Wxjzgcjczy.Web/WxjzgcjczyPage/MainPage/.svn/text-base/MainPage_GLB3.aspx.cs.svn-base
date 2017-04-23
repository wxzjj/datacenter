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
using System.Text;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class MainPage_GLB3 : BasePage
    {
        public ZlctBLL BLL;
        protected string zjajxm, sxsq1, xsq1, hsq1, bhq1, xq1, jy1, yx1, jgajxm, sxsq2, xsq2, hsq2, bhq2, xq2, jy2, yx2;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                SearchData_Gwtz();
                SearchData_gzzs();
                
            }
        }

        public void SearchData_gzzs()
        {
            //DataTable dt = BLL.RetrieveGzzs_NoRead().Result;

            //if (dt.Rows.Count > 0)
            //{
            //    StringBuilder str = new StringBuilder();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        str.AppendFormat("<li><font style=\"font-family: 宋体;\">[指示时间]{0}&nbsp;[指示主题]{1} &nbsp;[指示人]：{2} </font></li>", row["Zssj"], row["Gzzszt"],row["ZsrName"]);
            //    }
            //    Session["firstGzzs"] = str.ToString();

            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scrollDisplay(" + dt.Rows.Count + ");", true);

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

        public void SearchData_Gwtz()
        {
            //DataTable dtGwtz = BLL.ReadGwtz();
            //if (dtGwtz.Rows.Count > 0)
            //{
            //    StringBuilder str = new StringBuilder();
            //    foreach (DataRow row in dtGwtz.Rows)
            //    {

            //        str.AppendFormat("<li><a href=\"http://58.211.133.50:81/MajordomoMVC/OnlineOffice/LookGwtz?infoID={0}\" class=\"title\"  target=\"_blank\" title=\"{1}\">{2}</a></li>", row["infoID"], row["ContentID"], subStr(row["xxmc"].ToString(), 39));
            //    }
            //    Session["firstGwtz"] = str.ToString();

            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scrollDisplay(" + dtGwtz.Rows.Count + ");", true);
        }

       
    }
}
