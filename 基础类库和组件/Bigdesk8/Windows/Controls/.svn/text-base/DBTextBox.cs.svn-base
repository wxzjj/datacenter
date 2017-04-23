using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Bigdesk8.Data;

namespace Bigdesk8.Windows.Controls
{
    /// <summary>
    /// 单行文本框控件，通常数据长度在300字以下使用此控件，长度太长时，请使用DBMemoBox控件。
    /// </summary>
    public class DBTextBox : TextBox, IDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBTextBox()
        {
            base.Multiline = false;
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
        /// 数据类型
        /// </summary>
        [
        Description("数据类型"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataType.String)
        ]
        public DataType ItemType
        {
            get;
            set;
        }

        /// <summary>
        /// 显示格式，注意：与数据类型有关系。格式字符串，如yyyy-MM-dd HH:mm:ss,日期和时间格式字符串,标准数字格式字符串
        /// </summary>
        [
        Description("显示格式，注意：与数据类型有关系。格式字符串，如yyyy-MM-dd HH:mm:ss,日期和时间格式字符串,标准数字格式字符串"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string ItemFormat
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
        DefaultValue(DataRelation.Like)
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
                    this._ErrorMessage = DataUtility.GetErrorMessage_DataLengthRange(nameCN, this.ItemType, this.MinLength, this.MaxLength);
                    return this._ErrorMessage;
                }
            }

            // 数据类型检查
            if (!DataUtility.CheckDataType(data, this.ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN, this.ItemType);
                return this._ErrorMessage;
            }

            // 数据格式检查
            data = DataUtility.FormatData(data, this.ItemType, this.ItemFormat);
            if (data.IsEmpty())
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataFormat(nameCN, this.ItemType, this.ItemFormat);
                return this._ErrorMessage;
            }

            this.Text = data;


            // 最小值数据类型检查
            if (!DataUtility.CheckDataType(this.MinData, this.ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN + "(最小值)", this.ItemType);
                return this._ErrorMessage;
            }
            // 最大值数据类型检查
            if (!DataUtility.CheckDataType(this.MaxData, this.ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN + "(最大值)", this.ItemType);
                return this._ErrorMessage;
            }
            // 数据取值范围检查
            if (!DataUtility.CheckDataRange(data, this.ItemType, this.MinData, this.MaxData))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataRange(nameCN, this.ItemType, this.MinData, this.MaxData);
                return this._ErrorMessage;
            }


            //等等.....


            return string.Empty;
        }

        #endregion

        #region IDataItem 成员

        string IShowDataItem.ItemData
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
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

        #endregion

        #region 其它成员

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
        /// 最小值，注意：DBNull，string.Empty自动转换成null
        /// </summary>
        [
        Description("数据最小值"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string MinData
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值，注意：DBNull，string.Empty自动转换成null
        /// </summary>
        [
        Description("数据最大值"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string MaxData
        {
            get;
            set;
        }

        /// <summary>
        /// 校验器名称，支持“电子邮箱”、“网址”
        /// </summary>
        [
        Description("校验器名称，支持“电子邮箱”、“网址”"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string Validator
        {
            get;
            set;
        }

        /// <summary>
        /// 指定将 Text 文本内容转换成全角或半角字符
        /// </summary>
        [
        Description("指定将 Text 文本内容转换成全角或半角字符"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(FullHalfCharCase.None)
        ]
        public FullHalfCharCase FullHalfCase
        {
            get;
            set;
        }

        /// <summary>
        /// 指定将 Text 文本内容转换成大写或小写字符
        /// </summary>
        [
        Description("指定将 Text 文本内容转换成大写或小写字符"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(UpperLowerCharCase.None)
        ]
        public UpperLowerCharCase UpperLowerCase
        {
            get;
            set;
        }

        #endregion 其它成员

        #region 重写基类成员

        /// <summary>
        /// 获取或设置控件的文本内容
        /// </summary>
        [Category("扩展属性")]
        [Bindable(true)]
        [Description("获取或设置控件的文本内容")]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                switch (this.FullHalfCase)
                {
                    case FullHalfCharCase.ToFull:
                        base.Text = NukeString.ToSBCCase(base.Text);
                        break;
                    case FullHalfCharCase.ToHalf:
                        base.Text = NukeString.ToDBCCase(base.Text);
                        break;
                }

                switch (this.UpperLowerCase)
                {
                    case UpperLowerCharCase.ToUpper:
                        base.Text = base.Text.ToUpper();
                        break;
                    case UpperLowerCharCase.ToLower:
                        base.Text = base.Text.ToLower();
                        break;
                }
            }
        }

        #endregion 重写基类成员
    }
}
