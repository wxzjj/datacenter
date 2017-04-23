using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk2010.Data;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// 单行文本框控件，通常数据长度在300字以下使用此控件，长度太长时，请使用DBMemoBox控件。
    /// </summary>
    /// <summary>
    /// </summary>
    [ToolboxData("<{0}:DBTextBox runat=server></{0}:DBTextBox>")]
    public class DBTextBox : TextBox, IDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBTextBox()
        {
            base.TextMode = TextBoxMode.SingleLine;
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
            get
            {
                if (this.ViewState["DataItem_Name"].IsEmpty()) return "";

                return this.ViewState["DataItem_Name"].TrimString();
            }
            set
            {
                this.ViewState["DataItem_Name"] = value;
            }
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
            get
            {
                if (this.ViewState["DataItem_NameCN"].IsEmpty()) return "";
                return this.ViewState["DataItem_NameCN"].TrimString();
            }
            set
            {
                this.ViewState["DataItem_NameCN"] = value;
            }
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
            get
            {
                if (this.ViewState["DataType_Type"].IsEmpty()) return DataType.String;

                return (DataType)this.ViewState["DataType_Type"];
            }
            set
            {
                this.ViewState["DataType_Type"] = value;
            }
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
            get
            {
                if (this.ViewState["DataItem_Format"].IsEmpty()) return "";

                return this.ViewState["DataItem_Format"].TrimString();
            }
            set
            {
                this.ViewState["DataItem_Format"] = value;
            }
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
            get
            {
                if (this.ViewState["DataItem_Relation"].IsEmpty()) return DataRelation.Like;

                return (DataRelation)this.ViewState["DataItem_Relation"];
            }
            set
            {
                this.ViewState["DataItem_Relation"] = value;
            }
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
            get
            {
                if (this.ViewState["DataItem_IsRequired"].IsEmpty()) return false;
                return this.ViewState["DataItem_IsRequired"].ToBoolean();
            }
            set
            {
                this.ViewState["DataItem_IsRequired"] = value;
            }
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
            get
            {
                if (this.ViewState["DataItem_DefaultData"].IsEmpty()) return "";
                return this.ViewState["DataItem_DefaultData"].TrimString();
            }
            set
            {
                this.ViewState["DataItem_DefaultData"] = value;
            }
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
            string nameCN = this.ItemNameCN.IsEmpty() ? "控件ID：" + this.ID : this.ItemNameCN;
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
            get
            {
                if (this.ViewState["MinLength"].IsEmpty()) return 0;

                int i = (int)this.ViewState["MinLength"];
                if (i < 0) return 0;

                return i;
            }
            set
            {
                this.ViewState["MinLength"] = value;
            }
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
            get
            {
                if (this.ViewState["_MinData"].IsEmpty()) return "";
                return this.ViewState["_MinData"].TrimString();
            }
            set
            {
                this.ViewState["_MinData"] = value;
            }
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
            get
            {
                if (this.ViewState["_MaxData"].IsEmpty()) return "";
                return this.ViewState["_MaxData"].TrimString();
            }
            set
            {
                this.ViewState["_MaxData"] = value;
            }
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
            get
            {
                if (this.ViewState["Validator"].IsEmpty()) return "";
                return this.ViewState["Validator"].TrimString();
            }
            set
            {
                this.ViewState["Validator"] = value;
            }
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
            get
            {
                if (this.ViewState["FullHalfCase"].IsEmpty()) return FullHalfCharCase.None;
                return (FullHalfCharCase)this.ViewState["FullHalfCase"];
            }
            set
            {
                this.ViewState["FullHalfCase"] = value;
            }
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
            get
            {
                if (this.ViewState["UpperLowerCase"].IsEmpty()) return UpperLowerCharCase.None;
                return (UpperLowerCharCase)this.ViewState["UpperLowerCase"];
            }
            set
            {
                this.ViewState["UpperLowerCase"] = value;
            }
        }

        #endregion 其它成员

        #region 重写基类成员

        /// <summary>
        /// 获取或设置文本框中最多允许的字符数。如果为0，则不检查长度。默认值为 0。
        /// </summary>
        [
        Description("获取或设置文本框中最多允许的字符数。如果为0，则不检查长度。默认值为 0。"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(0)
        ]
        public new int MaxLength
        {
            get
            {
                if (this.ViewState["MaxLength"].IsEmpty()) return 0;

                int i = (int)this.ViewState["MaxLength"];
                if (i < 0) return 0;

                return i;
            }
            set
            {
                this.ViewState["MaxLength"] = value;
            }
        }

        /// <summary>
        /// 获取文本框的单行模式
        /// </summary>
        [Browsable(false)]
        public new TextBoxMode TextMode
        {
            get
            {
                return TextBoxMode.SingleLine;
            }
            set
            { }
        }

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

        #region 重写受保护的方法

        /// <summary>
        /// 将需要呈现的 HTML 属性和样式添加到指定的 <see cref="HtmlTextWriter"/> 实例中。
        /// </summary>
        /// <param name="writer"><see cref="HtmlTextWriter"/>，表示要在客户端呈现 HTML 内容的输出流。</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // 服务器端校验
            writer.AddAttribute("mytype", this.ItemType.ToString().ToLower());
            if (!this._ErrorMessage.IsEmpty())
            {
                writer.AddAttribute("myerror", "true");
            }

            // 客户端校验必填项,数据类型
            if (this.ItemIsRequired)
            {
                string r = "";
                switch (this.ItemType)
                {
                    case DataType.Decimal:
                    case DataType.Double:
                        r = " number";
                        break;
                    case DataType.Int32:
                    case DataType.Int64:
                        r = " digits";
                        break;
                }
                switch (this.Validator)
                {
                    case "email":
                        r = " email";
                        break;
                    case "url":
                        r = " url";
                        break;
                }

                //writer.AddAttribute("class", "required" + r);//这种写法输出的class会屏蔽掉控件中以CssClass引用的样式定义，也会屏蔽掉以Theme方式给出的样式定义，故换成下面的写法，缪卫华2012-11-23
                this.CssClass += " required" + r;
            }
            else
            {
                switch (this.ItemType)
                {
                    case DataType.Decimal:
                    case DataType.Double:
                        //writer.AddAttribute("class", "number");//这种写法输出的class会屏蔽掉控件中以CssClass引用的样式定义，也会屏蔽掉以Theme方式给出的样式定义，故换成下面的写法，缪卫华2012-11-23
                        this.CssClass += " number";
                        break;
                    case DataType.Int32:
                    case DataType.Int64:
                        //writer.AddAttribute("class", "digits");
                        this.CssClass += " digits";
                        break;
                }
                switch (this.Validator)
                {
                    case "email":
                        //writer.AddAttribute("class", "email");
                        this.CssClass += " email";
                        break;
                    case "url":
                        //writer.AddAttribute("class", "url");
                        this.CssClass += " url";
                        break;
                }
            }

            // 客户端校验字符串长度
            if (this.ItemType == DataType.String)
            {
                if (this.MinLength > 0 && this.MaxLength > 0)
                {
                    writer.AddAttribute("rangelength", string.Format("[{0},{1}]", this.MinLength, this.MaxLength));
                }
                else if (this.MinLength > 0)
                {
                    writer.AddAttribute("minlength", this.MinLength.ToString());
                }
                //else if (this.MaxLength > 0)
                //{
                //    writer.AddAttribute("maxlength", this.MaxLength.ToString());
                //}
            }

            // 客户端校验数据取值范围
            switch (this.ItemType)
            {
                case DataType.Decimal:
                case DataType.Double:
                case DataType.Int32:
                case DataType.Int64:
                    {
                        /*
                         * jQuery和jquery.validate.min.js配合对range进行校验时有问题，
                         * 故放弃掉range校验，而对min,max分别进行校验。 缪卫华 2011-12-05
                        if (!this.MinData.IsEmpty() && !this.MaxData.IsEmpty())
                        {
                            writer.AddAttribute("range", string.Format("[{0},{1}]", this.MinData, this.MaxData));
                        }
                        else if (!this.MinData.IsEmpty())
                        {
                            writer.AddAttribute("min", this.MinData);
                        }
                        else if (!this.MaxData.IsEmpty())
                        {
                            writer.AddAttribute("max", this.MaxData);
                        }
                        */
                        if (!this.MinData.IsEmpty())
                        {
                            writer.AddAttribute("min", this.MinData);
                        }

                        if (!this.MaxData.IsEmpty())
                        {
                            writer.AddAttribute("max", this.MaxData);
                        }
                    }
                    break;
            }

            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// 处理 <see cref="DBTextBox"/> 控件的回发数据。
        /// 当 ReadOnly=True 时, 客户端JS对控件赋值, 服务器端可以接收到.
        /// </summary>
        /// <param name="postDataKey">已发送集合中引用要加载的内容的索引</param>
        /// <param name="postCollection">发送到服务器的集合</param>
        /// <returns>如果发送内容与上次的发送内容不同，则为 true；否则为 false。</returns>
        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            this.Page.ClientScript.ValidateEvent(postDataKey);
            string text1 = this.Text;
            string text2 = postCollection[postDataKey];
            if (!text1.Equals(text2, StringComparison.Ordinal))
            {
                this.Text = text2;
                return true;
            }
            return false;
        }

        #endregion 重写受保护的方法
    }
}
