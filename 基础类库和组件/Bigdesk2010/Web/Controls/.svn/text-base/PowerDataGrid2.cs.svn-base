using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// 根据数据集判断记录总数，设置的 DataSource 值必须是所有查询的数据（包括当前页数据和非当前页数据）
    /// </summary>
    [ToolboxData("<{0}:PowerDataGrid2 runat=server></{0}:PowerDataGrid2>")]
    public class PowerDataGrid2 : PowerDataGrid
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        [Browsable(false)]
        public override int RecordCount
        {
            get
            {
                return base.RecordCount;
            }
            set
            {
                throw new Exception("不能指定记录总数，此属性由 DataSource 自动获得");
            }
        }

        /// <summary>
        /// 获取或设置当前显示页的索引。从 0 开始
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        public override int PageIndex
        {
            get
            {
                return ((GridView)this).PageIndex;
            }
            set
            {
                ((GridView)this).PageIndex = value;
            }
        }

        /// <summary>
        /// 重写 <see cref="GridView"/>.OnDataBinding 事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataBinding(EventArgs e)
        {
            IListSource listSource = this.DataSource as IListSource;
            if (listSource != null)
            {
                base.RecordCount = listSource.GetList().Count;
            }
            else
            {
                ICollection collection = this.DataSource as ICollection;
                if (collection != null)
                {
                    base.RecordCount = collection.Count;
                }
            }

            base.OnDataBinding(e);
        }
    }
}
