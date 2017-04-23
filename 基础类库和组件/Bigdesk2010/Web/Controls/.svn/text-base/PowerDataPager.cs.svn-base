using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using Bigdesk2010.Data;
using System.ComponentModel;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Security.Permissions;
using System.Drawing;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// <see cref="PowerDataPager"/> 数据分页控件
    /// </summary>
    [ToolboxData("<{0}:PowerDataPager runat=server></{0}:PowerDataPager>")]
    public class PowerDataPager : WebControl, INamingContainer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PowerDataPager()
            : base(HtmlTextWriterTag.Div)
        {
            this.PagerSettings = new PagerSettings();
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        [Browsable(false)]
        public PagedData PagedData
        {
            get
            {
                if (this.ViewState["_PagedData"] == null)
                    return new PagedData(0, 10, 0);
                return (PagedData)this.ViewState["_PagedData"];
            }
            private set
            {
                this.ViewState["_PagedData"] = value;
            }
        }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        [Description("每页显示记录数")]
        [Browsable(true)]
        [DefaultValue(10)]
        public int PageSize
        {
            get
            {
                return this.PagedData.PageSize;
            }
            set
            {
                if (this.PageSize != value)
                {
                    this.PagedData = new PagedData(this.RecordCount, value, this.PageIndex);
                }
            }
        }
        /// <summary>
        /// 记录总数
        /// </summary>
        [Description("记录总数")]
        [Browsable(true)]
        [DefaultValue(0)]
        public int RecordCount
        {
            get
            {
                return this.PagedData.RecordCount;
            }
            set
            {
                if (this.RecordCount != value)
                {
                    this.PagedData = new PagedData(value, this.PageSize, this.PageIndex);
                }
            }
        }
        /// <summary>
        /// 当前页索引
        /// </summary>
        [Description("当前页索引")]
        [Browsable(true)]
        [DefaultValue(0)]
        public int PageIndex
        {
            get
            {
                return this.PagedData.CurrentPageIndex;
            }
            set
            {
                if (this.PageIndex != value)
                {
                    this.PagedData = new PagedData(this.RecordCount, this.PageSize, value);
                }
            }
        }

        /// <summary>
        /// 页导航属性
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty),
        Description("页导航属性"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        NotifyParentProperty(true)]
        public PagerSettings PagerSettings { get; private set; }

        /// <summary>
        /// 获取或设置页导航行的外观。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("页导航属性")]
        [Description("获取或设置页导航行的外观")]
        public TableItemStyle PagerStyle { get; set; }

        private static readonly object EventPageIndexChanged = new object();
        /// <summary>
        /// 在单击某一页导航按钮时，但在控件处理分页操作之后发生。
        /// </summary>
        [Category("Action"), Description("在单击某一页导航按钮时，但在控件处理分页操作之后发生。")]
        public event EventHandler PageIndexChanged
        {
            add
            {
                base.Events.AddHandler(EventPageIndexChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageIndexChanged, value);
            }
        }

        private static readonly object EventPageIndexChanging = new object();
        /// <summary>
        /// 在单击某一页导航按钮时，但在控件处理分页操作之前发生。
        /// </summary>
        [Category("Action"), Description("在单击某一页导航按钮时，但在控件处理分页操作之前发生。")]
        public event GridViewPageEventHandler PageIndexChanging
        {
            add
            {
                base.Events.AddHandler(EventPageIndexChanging, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageIndexChanging, value);
            }
        }

        /// <summary>
        /// 由 ASP.NET 页面框架调用，以通知使用基于合成的实现的服务器控件创建它们包含的任何子控件，以便为回发或呈现做准备。
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Control control = CreateFirstLastNextPrevPager();
            this.Controls.Add(control);
        }

        /// <summary>
        /// 在加载 Control 对象之后、呈现之前发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.Controls.Clear();
            Control control = CreateFirstLastNextPrevPager();
            this.Controls.Add(control);
        }

        /// <summary>
        /// 引发 <see cref="PageIndexChanged"/> 事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected virtual void OnPageIndexChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventPageIndexChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 引发 <see cref="PageIndexChanging"/> 事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected virtual void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            GridViewPageEventHandler handler = (GridViewPageEventHandler)base.Events[EventPageIndexChanging];
            if (handler != null)
            {
                handler(this, e);
            }
            else if (!e.Cancel)
            {
                throw new Exception(string.Format("控件 {0} 未设置 {1} 事件", this.ID, "PageIndexChanging"));
            }
        }


        private Control CreateFirstLastNextPrevPager()
        {
            const string PageString = "第&nbsp;<font color = '#ff0000'>{0}</font>&nbsp;页，共&nbsp;<font color = '#0000ff'>{1}</font>&nbsp;页，共&nbsp;<font color = '#0000ff'>{2}</font>&nbsp;条";

            Control firstControl = CreatePageButton(this.PagerSettings.FirstPageImageUrl, this.PagerSettings.FirstPageText, this.PagerSettings.FirstPageToolTipText, "$_First", !this.PagedData.IsFirstPage);
            Control prevControl = CreatePageButton(this.PagerSettings.PreviousPageImageUrl, this.PagerSettings.PreviousPageText, this.PagerSettings.PreviousPageToolTipText, "$_Prev", !this.PagedData.IsFirstPage);
            Control nextControl = CreatePageButton(this.PagerSettings.NextPageImageUrl, this.PagerSettings.NextPageText, this.PagerSettings.NextPageToolTipText, "$_Next", !this.PagedData.IsLastPage);
            Control lastControl = CreatePageButton(this.PagerSettings.LastPageImageUrl, this.PagerSettings.LastPageText, this.PagerSettings.LastPageToolTipText, "$_Last", !this.PagedData.IsLastPage);
            Control pageIndexControl = CreatePageDropDownList(this.PagedData.PageCount, this.PagedData.CurrentPageIndex);

            Table table = new Table();
            table.Width = new Unit("100%");
            table.CellSpacing = 0;
            table.BorderWidth = 0;
            table.Height = this.PagerStyle.Height;
            table.BackColor = this.PagerStyle.BackColor;
            table.ForeColor = this.PagerStyle.ForeColor;

            TableRow row = new TableRow();
            TableCell row1cell1 = new TableCell();
            TableCell row1cell2 = new TableCell();

            switch (this.PagerStyle.HorizontalAlign)
            {
                case HorizontalAlign.Left:
                case HorizontalAlign.Right:
                case HorizontalAlign.Center:
                    row1cell1.HorizontalAlign = this.PagerStyle.HorizontalAlign;
                    row1cell1.BorderWidth = 0;
                    row1cell1.BackColor = this.PagerStyle.BackColor;
                    row1cell1.ForeColor = this.PagerStyle.ForeColor;

                    row1cell1.Controls.Add(new LiteralControl(string.Format(PageString, this.PagedData.CurrentPageIndex + 1, this.PagedData.PageCount, this.PagedData.RecordCount)));

                    row1cell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell1.Controls.Add(firstControl);
                    row1cell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell1.Controls.Add(prevControl);
                    row1cell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell1.Controls.Add(nextControl);
                    row1cell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell1.Controls.Add(lastControl);
                    row1cell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell1.Controls.Add(new LiteralControl("转到："));
                    row1cell1.Controls.Add(pageIndexControl);
                    row1cell1.Controls.Add(new LiteralControl("页"));

                    row.Cells.Add(row1cell1);

                    table.Rows.Add(row);
                    break;
                default:
                    row1cell1.HorizontalAlign = HorizontalAlign.Left;
                    row1cell1.BorderWidth = 0;
                    row1cell1.BackColor = this.PagerStyle.BackColor;
                    row1cell1.ForeColor = this.PagerStyle.ForeColor;
                    row1cell1.Controls.Add(new LiteralControl(string.Format(PageString, this.PagedData.CurrentPageIndex + 1, this.PagedData.PageCount, this.PagedData.RecordCount)));
                    row.Cells.Add(row1cell1);

                    row1cell2.HorizontalAlign = HorizontalAlign.Right;
                    row1cell2.BorderWidth = 0;
                    row1cell2.BackColor = this.PagerStyle.BackColor;
                    row1cell2.ForeColor = this.PagerStyle.ForeColor;
                    row1cell2.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell2.Controls.Add(firstControl);
                    row1cell2.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell2.Controls.Add(prevControl);
                    row1cell2.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell2.Controls.Add(nextControl);
                    row1cell2.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell2.Controls.Add(lastControl);
                    row1cell2.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    row1cell2.Controls.Add(new LiteralControl("转到："));
                    row1cell2.Controls.Add(pageIndexControl);
                    row1cell2.Controls.Add(new LiteralControl("页"));
                    row.Cells.Add(row1cell2);

                    table.Rows.Add(row);
                    break;
            }
            return table;
        }

        private Control CreatePageButton(string imageUrl, string text, string toolTipText, string commandArgument, bool isEnabled)
        {
            text = HttpUtility.HtmlDecode(text);
            toolTipText = HttpUtility.HtmlDecode(toolTipText);

            if (!isEnabled)
            {
                HtmlGenericControl c = new HtmlGenericControl("span");
                c.InnerText = text;
                c.Style[HtmlTextWriterStyle.Color] = "silver";
                c.Attributes["title"] = toolTipText;
                c.Disabled = true;
                return c;
            }

            IButtonControl control;
            if (imageUrl.Length > 0)
            {
                ImageButton c = new ImageButton();
                c.ImageUrl = imageUrl;
                c.AlternateText = toolTipText;
                c.ToolTip = toolTipText;
                c.BorderWidth = 0;
                control = c;
            }
            else
            {
                LinkButton c = new LinkButton();
                c.Text = text;
                c.ToolTip = toolTipText;
                c.Font.Underline = false;
                control = c;
            }

            control.CommandName = "$_Page";
            control.CommandArgument = commandArgument;
            control.Command += this.PageButton_Command;
            return (Control)control;
        }
        private Control CreatePageDropDownList(int pageCount, int currentPageIndex)
        {
            DropDownList c = new DropDownList();
            c.AutoPostBack = true;
            c.SelectedIndexChanged += new EventHandler(selectPageIndexDropDownList_SelectedIndexChanged);
            c.Width = new Unit(7 * pageCount.ToString().Length + 30);

            for (int i = 1; i <= pageCount; i++)
            {
                c.Items.Add(new ListItem(i.ToString()));
            }
            c.SelectedIndex = currentPageIndex;
            return c;
        }
        private void selectPageIndexDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageButton_Command(sender, new CommandEventArgs("$_Page", "$_Select"));
        }
        private void PageButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName != "$_Page") return;

            string commandArgument = (string)e.CommandArgument;
            switch (commandArgument.ToLower())
            {
                case "$_first":
                    this.PageIndex = 0;
                    break;
                case "$_prev":
                    this.PageIndex = this.PagedData.PreviousPageIndex;
                    break;
                case "$_next":
                    this.PageIndex = this.PagedData.NextPageIndex;
                    break;
                case "$_last":
                    this.PageIndex = this.PagedData.PageCount - 1;
                    break;
                case "$_select":
                    this.PageIndex = (sender as DropDownList).SelectedIndex;
                    break;
            }

            GridViewPageEventArgs pagee = new GridViewPageEventArgs(this.PagedData.CurrentPageIndex);
            this.OnPageIndexChanging(pagee);
            if (!pagee.Cancel)
            {
                this.OnPageIndexChanged(EventArgs.Empty);
            }
        }
    }
}
