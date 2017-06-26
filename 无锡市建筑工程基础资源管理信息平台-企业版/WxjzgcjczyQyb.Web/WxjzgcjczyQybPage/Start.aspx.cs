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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage
{
    public partial class Start : System.Web.UI.Page
    {
        
        private AppUser WorkUser = new AppUser();
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TableCellCollection cells = e.Row.Cells;
                //foreach (TableCell cell in cells)
                //{
                //    if (!string.IsNullOrEmpty(cell.Text))
                //    {
                //        cell.Text = Server.HtmlDecode(cell.Text);
                //    }
                //}

                DBText lbryzyzglx = (DBText)e.Row.FindControl("lbryzyzglx");
                DBText lbzsbh = (DBText)e.Row.FindControl("lbzsbh");

                if (!string.IsNullOrEmpty(lbryzyzglx.Text))
                {
                    lbryzyzglx.Text = lbryzyzglx.Text.Replace("<br />", ",").Split(',')[0];
                }

                if (!string.IsNullOrEmpty(lbzsbh.Text))
                {
                    lbzsbh.Text = lbzsbh.Text.Replace("<br />", ",").Split(',')[0];
                }


            }
        }
    }
}
