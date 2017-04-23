using System;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// 与分页相关的属性，方便执行分页操作。无法继承此类。
    /// </summary>
    [Serializable]
    public sealed class PagedData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="recordCount">所有记录的总数</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="currentPageIndex">当前页索引,从 0 开始</param>
        public PagedData(int recordCount, int pageSize, int currentPageIndex)
        {
            if (recordCount < 0) throw new Exception("参数 recordCount 不能小于 0。");
            if (pageSize < 1) throw new Exception("参数 pageSize 不能小于 1。");

            this.RecordCount = recordCount;
            this.PageSize = pageSize;
            this.CurrentPageIndex = currentPageIndex < 0 ? 0 : currentPageIndex;

            if (this.RecordCount == 0)
            {
                this.PageCount = 0;
                this.CurrentPageIndex = 0;
                this.IsFirstPage = true;
                this.IsLastPage = true;
                this.RecordCountInPage = 0;
                this.PreviousPageIndex = 0;
                this.NextPageIndex = 0;
                this.PageCountRemain = 0;
                this.RecordCountRemain = 0;
                this.FirstIndexInPage = 0;
                this.LastIndexInPage = 0;
            }
            else
            {
                this.CalculatePagedData();
            }
        }

        /// <summary>
        /// 计算分页数据
        /// </summary>
        private void CalculatePagedData()
        {
            this.PageCount = (this.RecordCount + this.PageSize - 1) / this.PageSize;
            if (this.CurrentPageIndex > this.PageCount - 1)
                this.CurrentPageIndex = this.PageCount - 1;
            this.IsFirstPage = this.CurrentPageIndex == 0;
            this.IsLastPage = this.CurrentPageIndex == this.PageCount - 1;
            this.RecordCountInPage = this.IsLastPage ? this.RecordCount - this.PageSize * this.CurrentPageIndex : this.PageSize;
            this.PreviousPageIndex = this.IsFirstPage ? 0 : this.CurrentPageIndex - 1;
            this.NextPageIndex = this.IsLastPage ? this.CurrentPageIndex : this.CurrentPageIndex + 1;
            this.PageCountRemain = this.PageCount - this.CurrentPageIndex - 1;
            this.FirstIndexInPage = this.PageSize * this.CurrentPageIndex;

            if (this.IsLastPage)
                this.LastIndexInPage = this.RecordCount - 1;
            else if (this.IsFirstPage)
                this.LastIndexInPage = this.PageSize - 1;
            else
                this.LastIndexInPage = this.PageSize * (this.CurrentPageIndex + 1) - 1;

            this.RecordCountRemain = this.RecordCount - this.LastIndexInPage - 1;
        }

        /// <summary>
        /// 获取当前页的索引，从 0 开始
        /// </summary>
        public int CurrentPageIndex { get; private set; }

        /// <summary>
        /// 获取要在每页上显示的记录数，从 1 开始
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 获取所有记录所需要的总页数，从 0 开始
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// 获取当前页显示的记录数，从 0 开始
        /// </summary>
        public int RecordCountInPage { get; private set; }

        /// <summary>
        /// 获取所有记录的总数，从 0 开始
        /// </summary>
        public int RecordCount { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示当前页是否是首页。如果当前页是首页，则为 true；否则为 false。
        /// </summary>
        public bool IsFirstPage { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示当前页是否是最后一页。如果当前页是最后一页，则为 true；否则为 false。
        /// </summary>
        public bool IsLastPage { get; private set; }

        /// <summary>
        /// 获取上一页索引，从 0 开始
        /// </summary>
        public int PreviousPageIndex { get; private set; }

        /// <summary>
        /// 获取下一页索引，从 0 开始
        /// </summary>
        public int NextPageIndex { get; private set; }

        /// <summary>
        /// 获取当前页之后未显示的页的总数，从 0 开始
        /// </summary>
        public int PageCountRemain { get; private set; }

        /// <summary>
        /// 获取在当前页之后还未显示的剩余记录数，从 0 开始
        /// </summary>
        public int RecordCountRemain { get; private set; }

        /// <summary>
        /// 获取页面中显示的首条记录的索引，从 0 开始
        /// </summary>
        public int FirstIndexInPage { get; private set; }

        /// <summary>
        /// 获取页面中显示的最后一条记录的索引，从 0 开始
        /// </summary>
        public int LastIndexInPage { get; private set; }
    }
}
