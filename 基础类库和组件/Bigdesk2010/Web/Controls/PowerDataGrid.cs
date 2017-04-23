using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// <see cref="PowerDataGrid"/> 对 <see cref="GridView"/> 进行功能扩展，
    /// 必须设置 RecordCount（记录总数），DataSource（当前页数据）。
    /// </summary>
    [ToolboxData("<{0}:PowerDataGrid runat=server></{0}:PowerDataGrid>")]
    public class PowerDataGrid : GridView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PowerDataGrid()
        {
            this.IsOnePageShowPager = true;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        [Browsable(false)]
        public virtual int RecordCount
        {
            get
            {
                if (this.ViewState["_RecordCount"].IsEmpty()) return 0;

                return this.ViewState["_RecordCount"].ToInt32();
            }
            set
            {
                this.ViewState["_RecordCount"] = value;
            }
        }

        /// <summary>
        /// 获取或设置当前显示页的索引。从 0 开始
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        public new virtual int PageIndex
        {
            get
            {
                if (this.ViewState["_PageIndex"].IsEmpty()) return 0;

                int i = this.ViewState["_PageIndex"].ToInt32();
                if (i > this.PageCount - 1)
                    return this.PageCount - 1;
                else if (i < 0)
                    return 0;
                else
                    return i;
            }
            set
            {
                base.PageIndex = 0;
                this.ViewState["_PageIndex"] = value;
            }
        }

        /// <summary>
        /// 页数
        /// </summary>
        [Browsable(false)]
        public override int PageCount
        {
            get
            {
                return (this.RecordCount + this.PageSize - 1) / this.PageSize;
            }
        }

        /// <summary>
        /// 重写 <see cref="GridView"/>.OnRowCreated 事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                base.OnRowCreated(e);
                return;
            }

            PowerDataPager pager = new PowerDataPager();
            pager.PageIndexChanging += new GridViewPageEventHandler(pager_PageIndexChanging);
            pager.PageIndexChanged += new EventHandler(pager_PageIndexChanged);
            pager.RecordCount = this.RecordCount;
            pager.PageSize = this.PageSize;
            pager.PageIndex = this.PageIndex;

            /*
             * 即使在没有显式定义GridView的PagerStyle, PagerSettings，asp.net会自动给以缺省，
             * 故这里不需判断GridView的PagerStyle, PagerSettings是否为空
             */ 
            pager.PagerStyle = this.PagerStyle;

            pager.PagerSettings.FirstPageImageUrl = this.PagerSettings.FirstPageImageUrl;
            pager.PagerSettings.FirstPageText = this.PagerSettings.FirstPageText;
            pager.PagerSettings.LastPageImageUrl = this.PagerSettings.LastPageImageUrl;
            pager.PagerSettings.LastPageText = this.PagerSettings.LastPageText;
            pager.PagerSettings.NextPageImageUrl = this.PagerSettings.NextPageImageUrl;
            pager.PagerSettings.NextPageText = this.PagerSettings.NextPageText;
            pager.PagerSettings.PreviousPageImageUrl = this.PagerSettings.PreviousPageImageUrl;
            pager.PagerSettings.PreviousPageText = this.PagerSettings.PreviousPageText;

            e.Row.Controls.Clear();
            TableCell tc = new TableCell();
            tc.ColumnSpan = this.Columns.Count;
            tc.Controls.Add(pager);
            e.Row.Controls.Add(tc);

            base.OnRowCreated(e);
        }

        void pager_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.OnPageIndexChanging(e);
        }

        void pager_PageIndexChanged(object sender, EventArgs e)
        {
            this.OnPageIndexChanged(e);
        }

        /// <summary>
        /// 当只有一页数据时，是否显示Pager信息，默认值为true
        /// </summary>
        [
        Category("分页"),
        Description("当只有一页数据时，是否显示Pager信息，默认值为true"),
        DefaultValue(true),
        ]
        public bool IsOnePageShowPager
        {
            get { return Convert.ToBoolean(this.ViewState["IsOnePageShowPager"]); }
            set { this.ViewState["IsOnePageShowPager"] = value; }
        }

        /// <summary>
        /// 重写数据绑定
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);

            if (this.IsOnePageShowPager)
            {
                if (this.TopPagerRow != null)
                {
                    this.TopPagerRow.Visible = true;
                }
                if (this.BottomPagerRow != null)
                {
                    this.BottomPagerRow.Visible = true;
                }
            }
        }

        /// <summary>
        /// 根据Column定义中的HeaderText属性查找索引最小的相符Column
        /// </summary>
        public DataControlField FindDataControlField(string headerText)
        {
            foreach (DataControlField dcf in this.Columns)
            {
                if (dcf.HeaderText.Equals(headerText))
                    return dcf;
            }
            return null;
        }
    }
}
