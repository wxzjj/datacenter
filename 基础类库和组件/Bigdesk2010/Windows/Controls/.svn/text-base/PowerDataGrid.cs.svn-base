using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Bigdesk2010.Windows.Controls
{
    public delegate void PageChangedEventHandler(object sender, GridViewPageEventArgs args);

    /// <summary>
    /// 
    /// </summary>
    /// 参考自http://blog.appdoc.cn/2011/03/10/splitpager-control-used-in-datagridview-of-winform/
    public partial class PowerDataGrid : DataGridView
    {
        public event PageChangedEventHandler PageChanged;
        public PowerDataGrid()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 当前所在的页面
        /// </summary>
        private int pageIndex = 1;

        public int PageIndex
        {
            get { return pageIndex; }
            //set
            //{
            //    pageIndex = value;
            //    this.lblPageIndex.Text = string.Format("第{0}页", pageIndex);
            //}
        }
        /// <summary>
        /// 每页面显示记录的个数
        /// </summary>
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                if (0 == value)
                    pageSize = 1;
            }
        }
        /// <summary>
        /// 页面的总数
        /// </summary>
        private int pageCount;

        public int PageCount
        {
            get { return pageCount; }
        }
        /// <summary>
        /// 数据记录的总数
        /// </summary>
        private int recordCount;

        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }
        private void OnPageChanged(GridViewPageEventArgs args)
        {
            if (PageChanged != null)
                PageChanged(this, args);
        }
        //public void Initialize(int recordCount)
        //{
        //    this.recordCount = recordCount;
        //    this.pageCount = (this.recordCount % this.pageSize == 0) ? (this.recordCount / this.pageSize) : (this.recordCount / this.pageSize + 1);
        //    this.lblPageIndex.Text = string.Format("第{0}页", this.pageIndex);
        //    this.lblPageCount.Text = string.Format("共{0}页", this.pageCount);
        //}
        private void RefreshPage(int newPageIndex)
        {
            OnPageChanged(new GridViewPageEventArgs(newPageIndex));
            this.pageCount = (this.recordCount % this.pageSize == 0) ? (this.recordCount / this.pageSize) : (this.recordCount / this.pageSize + 1);
            this.pageIndex = newPageIndex;
            this.lblPageIndex.Text = string.Format("第{0}页", this.pageIndex);
            this.lblPageCount.Text = string.Format("共{0}页", this.pageCount);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.pageIndex > 1)
                RefreshPage(this.pageIndex - 1);
            else
                RefreshPage(1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.pageIndex < this.pageCount)
                RefreshPage(this.pageIndex + 1);
            else
                RefreshPage(this.pageCount);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int gotoIndex;
            if (!int.TryParse(this.txtGoIndex.Text, out gotoIndex))
                gotoIndex = 1;
            if (gotoIndex < 1)
                gotoIndex = 1;
            if (gotoIndex > this.pageCount)
                gotoIndex = this.pageCount;
            RefreshPage(gotoIndex);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            RefreshPage(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            RefreshPage(this.pageCount);
        }
    }
    public class GridViewPageEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPageIndex">新页面的index</param>
        public GridViewPageEventArgs(int newPageIndex)
        {
            this.NewPageIndex = newPageIndex;
        }

        public int NewPageIndex
        {
            get;
            set;
        }
    }
}
