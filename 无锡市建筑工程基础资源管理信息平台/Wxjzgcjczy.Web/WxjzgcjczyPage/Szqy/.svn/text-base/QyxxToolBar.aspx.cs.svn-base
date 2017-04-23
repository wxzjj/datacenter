using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy
{
    public partial class QyxxToolBar : BasePage
    {
        protected string qyID, befrom, dwlx;
        private SzqyBLL BllSzqy;
        protected void Page_Load(object sender, EventArgs e)
        {
            BllSzqy = new SzqyBLL(this.WorkUser);
            qyID = Request.QueryString["qyid"];
            befrom = Request.QueryString["befrom"];
            dwlx = Request.QueryString["dwlx"];
            if (string.IsNullOrEmpty(dwlx))
            {
                DataTable dt = BllSzqy.getCsywlxid(qyID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string csywlxid = dt.Rows[i][0].ToString();
                    if (csywlxid == "1" || csywlxid == "2" || csywlxid == "3")
                    {
                        dwlx = "sgdw";
                        break;
                    }
                    else if (csywlxid == "5" || csywlxid == "6")
                    {
                        dwlx = "kcsjdw";
                        break;
                    }
                    else if (csywlxid == "4" || csywlxid == "7" || csywlxid == "8" || csywlxid == "9")
                    {
                        dwlx = "zjjg";
                        break;
                    }
                }
                if (string.IsNullOrEmpty(dwlx))
                    dwlx = "qt";
            }


        }
    }
}
