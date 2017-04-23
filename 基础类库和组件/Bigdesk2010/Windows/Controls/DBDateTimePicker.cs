using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Bigdesk2010.Data;
using System.Text;

namespace Bigdesk2010.Windows.Controls
{
    /// <summary>
    /// 日期控件
    /// </summary>
    public class DBDateTimePicker : DateTimePicker, IDataItem
    {
        #region IDataItem 成员

        /// <summary>
        /// 数据项名称
        /// </summary>
        [
        Description("数据项名称"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string ItemName
        {
            get;
            set;
        }

        /// <summary>
        /// 中文名称
        /// </summary>
        [
        Description("数据项中文名称"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string ItemNameCN
        {
            get;
            set;
        }

        /// <summary>
        /// 数据项与数据的关系
        /// </summary>
        [
        Description("数据项与数据的关系"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataRelation.Equal)
        ]
        public DataRelation ItemRelation
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为必填项
        /// </summary>
        [
        Description("是否为必填项"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(false)
        ]
        public bool ItemIsRequired
        {
            get;
            set;
        }

        /// <summary>
        /// 默认数据，默认值为string.Empty
        /// </summary>
        [
        Description("默认数据"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string ItemDefaultData
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息，如果不为String.Empty,则有错误
        /// </summary>
        private string _ErrorMessage;

        /// <summary>
        /// 检查类型是否匹配，长度是否超长，必填项是否已填，取值范围是否正确等等.....
        /// </summary>
        /// <returns>检查成功返回true,不成功返回false</returns>
        public string ItemCheck()
        {
            string nameCN = this.ItemNameCN.IsEmpty() ? "控件名称：" + this.Name : this.ItemNameCN;
            string data = this.Text.TrimString();

            // 必填项检查
            if (this.ItemIsRequired && data.IsEmpty())
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataRequired(nameCN);
                return this._ErrorMessage;
            }

            if (data.IsEmpty()) return "";

            // 数据类型检查
            if (!DataUtility.CheckDataType(data, ((IDataItem)this).ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN, ((IDataItem)this).ItemType);
                return this._ErrorMessage;
            }

            return string.Empty;
        }

        #endregion

        #region IDataItem 成员

        string IShowDataItem.ItemData
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.DateTime;
            }
            set { }
        }

        int IDataItem.ItemLength
        {
            get
            {
                return 0;
            }
            set { }
        }

        string IShowDataItem.ItemFormat
        {
            get
            {
                return "";
            }
            set
            { }
        }
        #endregion
    }
}

