using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8;
using Bigdesk8.Web;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Dxyz_List : BasePage
    {
        protected ZlctBLL BLL;
        protected int pageSize = 8;
        protected string PageIndex;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                PageIndex = Request.QueryString["pageIndex"];
                int pageIndex=0;
                if(!string.IsNullOrEmpty(PageIndex)&&int.TryParse(PageIndex,out pageIndex))
                    SearchData(pageIndex);
                else
                    SearchData(0);
            }
        }
        public void NoPageHandler()
        {
            this.ddlTurnToPage.Items.Clear();
            this.lblCurrentPage.Text = "0";
            this.lblPageCount.Text = "0";
            this.lblCountPerPage.Text = pageSize.ToString();
            this.lblAllCount.Text = "0";

            this.btnUp.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;

        }
        public void OnePageHandler()
        {
            this.ddlTurnToPage.Items.Clear();
            this.lblCurrentPage.Text = "1";
            this.lblPageCount.Text = "1";
            this.lblCountPerPage.Text = pageSize.ToString();
            this.lblAllCount.Text = ViewState["allCount"].ToString();

            this.btnUp.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;

            this.ddlTurnToPage.Items.Add(new ListItem("1", "1"));
            this.ddlTurnToPage.SelectedIndex = 0;
        }
        public void OtherPageHandler(int pageIndex, int pageCount)
        {
            this.ddlTurnToPage.Items.Clear();
            this.lblCurrentPage.Text = (pageIndex + 1).ToString();
            this.lblPageCount.Text = pageCount.ToString();
            this.lblCountPerPage.Text = pageSize.ToString();
            this.lblAllCount.Text = ViewState["allCount"].ToString();

            this.btnUp.Enabled = true;
            this.btnFirst.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;

            int i = 1;

            while (i <= pageCount)
            {
                this.ddlTurnToPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i++;
            }

            this.ddlTurnToPage.SelectedIndex = pageIndex;
        }
        public void FirstPageHandler(int pageCount)
        {
            this.ddlTurnToPage.Items.Clear();
            this.lblCurrentPage.Text = "1";
            this.lblPageCount.Text = pageCount.ToString();
            this.lblCountPerPage.Text = pageSize.ToString();
            this.lblAllCount.Text = ViewState["allCount"].ToString();

            int i = 1;
            while (i <= pageCount)
            {
                this.ddlTurnToPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i++;
            }
            this.ddlTurnToPage.SelectedIndex = 0;

            this.btnUp.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;

        }
        public void LastPageHandler(int pageCount)
        {
            this.ddlTurnToPage.Items.Clear();
            this.lblCurrentPage.Text = pageCount.ToString();
            this.lblPageCount.Text = pageCount.ToString();
            this.lblCountPerPage.Text = pageSize.ToString();
            this.lblAllCount.Text = ViewState["allCount"].ToString();

            int i = 1;
            while (i <= pageCount)
            {
                this.ddlTurnToPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i++;
            }
            this.ddlTurnToPage.SelectedIndex = pageCount - 1;
            this.btnUp.Enabled = true;
            this.btnFirst.Enabled = true;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;

        }

        public void SearchData(int pageIndex)
        {
            List<IDataItem> list = new List<IDataItem>();
            int allRecordCount = 0;
            DataTable dt = BLL.RetrieveDxjb_List(list, pageSize, pageIndex, out allRecordCount).Result;
            this.dlDxjbxx.DataSource = dt;
            this.dlDxjbxx.DataBind();
            int pageCount = 0;
            if (allRecordCount % pageSize == 0)
            {
                pageCount = allRecordCount / pageSize;
            }
            else
            {
                pageCount = allRecordCount / pageSize + 1;
            }
          
            ViewState["pageCount"] = pageCount;
            ViewState["allCount"] = allRecordCount;

            if (pageCount == 0)
            {
                ViewState["pageIndex"] = 0;
                this.lblPageIndex.Text = "0";
                NoPageHandler();
            }
            else
                if (pageIndex == pageCount - 1 && pageIndex == 0)
                {
                    ViewState["pageIndex"] = 0;
                    this.lblPageIndex.Text = "0";
                    OnePageHandler();
                }
                else
                    if (pageIndex <= 0)
                    {
                        ViewState["pageIndex"] = 0;
                        this.lblPageIndex.Text = "0";
                        FirstPageHandler(pageCount);
                    }
                    else
                        if (pageIndex >= pageCount - 1)
                        {
                            ViewState["pageIndex"] = pageCount - 1;
                            this.lblPageIndex.Text = (pageCount - 1).ToString();
                            LastPageHandler(pageCount);
                        }
                        else
                        {
                            ViewState["pageIndex"] = pageIndex;
                            this.lblPageIndex.Text = pageIndex.ToString();
                            OtherPageHandler(pageIndex, pageCount);
                        }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            SearchData(Int32.Parse(ViewState["pageIndex"].ToString()) + 1);
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            SearchData(0);
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            SearchData(Int32.Parse(ViewState["pageIndex"].ToString()) - 1);
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            SearchData(Int32.Parse(ViewState["pageCount"].ToString()) - 1);

        }

        protected void ddlTurnToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTurnToPage.Items.Count > 0)
            {
                SearchData(this.ddlTurnToPage.SelectedIndex);
            }
        }

        protected void dlDxjbxx_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item)
            {
                HiddenField h = e.Item.FindControl("hiddenId") as HiddenField;
                HiddenField hiddenDxjbId = e.Item.FindControl("hiddenDxjbId") as HiddenField;
                LinkButton b = e.Item.FindControl("send") as LinkButton;
                b.PostBackUrl += hiddenDxjbId.Value;
                if (h.Value.ToLower() == "true")
                {
                    b.Enabled = false;
                }
                else
                {
                    b.Enabled = true;
                }
                
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField h = e.Item.FindControl("hiddenId") as HiddenField;
                HiddenField hiddenDxjbId = e.Item.FindControl("hiddenDxjbId") as HiddenField;
                LinkButton b = e.Item.FindControl("send") as LinkButton;
                b.PostBackUrl += hiddenDxjbId.Value;
                if (h.Value.ToLower() == "true")
                {
                    b.Enabled = false;
                }
                else
                {
                    b.Enabled = true;
                }
            }
        }

        protected void lnkAddDxjb_Click(object sender, EventArgs e)
        {
            //int pageIndex = 0;
            //if(!string.IsNullOrEmpty(this.lblPageIndex.Text)&&int.TryParse(this.lblPageIndex.Text,out pageIndex))
            //    this.WindowLocation("Dxyz_Edit.aspx?operate=add&pageIndex="+pageIndex);
            //else
                this.WindowLocation("Dxyz_Edit.aspx?operate=add");

        }
        protected void lnkDxjbSendRecord_Click(object sender, EventArgs e)
        {
            this.WindowLocation("DxjbSendRecord_List.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            SearchData(Int32.Parse(ViewState["pageIndex"].ToString()));
        }

    }
}
