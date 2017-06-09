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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx
{
    public partial class JbZxjk_Detail : BasePage2
    {
        public XxgxBLL xxgxBll;
        protected void Page_Load(object sender, EventArgs e)
        {

            xxgxBll = new XxgxBLL(this.WorkUser);

            string tableName = Request.QueryString["tableName"];
            string pkid = Request.QueryString["pkid"];
            string keyFields = Request.QueryString["keyFields"];

            DataTable dt = new DataTable();
            dt = xxgxBll.GetRecordItem(tableName, pkid);

            string msgTop = "";

            string msg = "";

            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    String columnName = dt.Columns[i].ColumnName;

                    if (!string.IsNullOrEmpty(keyFields) && string.Equals(columnName, keyFields, StringComparison.CurrentCultureIgnoreCase))
                    {
                        msgTop += "  ";
                        msgTop += columnName;
                        msgTop += " : ";
                        //msgTop += "<font color='blue'>";
                        msgTop += dr[i].ToString();
                        //msgTop += "</font>";
                        msgTop += "\r\n";
                    }

                    msg += "  ";
                    msg += columnName;
                    msg += " : ";
                    //msg += "<font color='blue'>";
                    msg += dr[i].ToString();
                    //msg += "</font>";
                    msg += "\r\n";
                }
            }

            this.TB_highlight.Text = msgTop;
            this.TB_detail.Text = msg;

        }
    }
}