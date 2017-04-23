using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Bigdesk2010.Data;
using System.Text;

namespace Bigdesk2010.Windows.Controls
{
    /// <summary>
    /// 多行文本框控件，通常数据长度大于300字
    /// </summary>
    public class DBMemoBox : TextBox, IDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBMemoBox()
        {
            base.Multiline = true;
        }

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
            string nameCN = this.ItemNameCN.IsEmpty() ? "控件ID：" + this.Name : this.ItemNameCN;
            string data = this.Text.TrimString();

            // 必填项检查
            if (this.ItemIsRequired && data.IsEmpty())
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataRequired(nameCN);
                return this._ErrorMessage;
            }

            if (data.IsEmpty()) return "";

            // 数据长度检查
            if (this.MinLength > 0 || this.MaxLength > 0)
            {
                int dl = Encoding.Default.GetByteCount(data);
                if (this.MinLength > dl || this.MaxLength < dl)
                {
                    this.Focus();
                    this._ErrorMessage = DataUtility.GetErrorMessage_DataLengthRange(nameCN, DataType.String, this.MinLength, this.MaxLength);
                    return this._ErrorMessage;
                }
            }


            //等等.....


            return string.Empty;
        }

        #endregion

        #region IDataItem 成员

        DataRelation IDataItem.ItemRelation
        {
            get
            {
                return DataRelation.Equal;
            }
            set
            { }
        }
        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.String;
            }
            set
            { }
        }
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
        int IDataItem.ItemLength
        {
            get
            {
                return this.MaxLength;
            }
            set
            { }
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

        #region 重写基类成员

        /// <summary>
        /// 获取或设置文本框中最少的字符数。如果为0，则不检查长度。默认值为 0。
        /// </summary>
        [
        Description("获取或设置文本框中最少的字符数。如果为0，则不检查长度。默认值为 0。"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(0)
        ]
        public int MinLength
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置文本框中最多允许的字符数。如果为0，则不检查长度。默认值为 0。
        /// </summary>
        [
        Description("获取或设置文本框中最多允许的字符数。如果为0，则不检查长度。默认值为 0。"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(0)
        ]
        public override int MaxLength
        {
            get
            {
                if (base.MaxLength < this.MinLength)
                    base.MaxLength = this.MinLength;
                return base.MaxLength;
            }
            set
            {
                base.MaxLength = value;
            }
        }

        #endregion 重写基类成员
    }
}
