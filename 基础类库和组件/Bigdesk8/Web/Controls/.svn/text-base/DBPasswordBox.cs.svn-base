using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 密码文本框控件
    /// </summary>
    [ToolboxData("<{0}:DBPasswordBox runat=server></{0}:DBPasswordBox>")]
    public class DBPasswordBox : TextBox, IDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBPasswordBox()
        {
            base.TextMode = TextBoxMode.Password;
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

                return (bool)this.ViewState["DataItem_IsRequired"];
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
                    this._ErrorMessage = DataUtility.GetErrorMessage_DataLengthRange(nameCN, DataType.String, this.MinLength, this.MaxLength);
                    return this._ErrorMessage;
                }
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
                return DataType.String;
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
        DataRelation IDataItem.ItemRelation
        {
            get
            {
                return DataRelation.Equal;
            }
            set
            { }
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

        /// <summary>
        /// 获取文本框的单行模式
        /// </summary>
        [Browsable(false)]
        public new TextBoxMode TextMode
        {
            get
            {
                return TextBoxMode.Password;
            }
            set
            { }
        }

        #endregion 重写基类成员

        #region 重写受保护的方法

        /// <summary>
        /// 将需要呈现的 HTML 属性和样式添加到指定的 <see cref="HtmlTextWriter"/> 实例中。
        /// </summary>
        /// <param name="writer"><see cref="HtmlTextWriter"/>，表示要在客户端呈现 HTML 内容的输出流。</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!this._ErrorMessage.IsEmpty())
            {
                writer.AddAttribute("myerror", "true");
            }

            if (this.ItemIsRequired)
            {
                //writer.AddAttribute("class", "required");//这种写法输出的class会屏蔽掉控件中以CssClass引用的样式定义，也会屏蔽掉以Theme方式给出的样式定义，故换成下面的写法，缪卫华2012-11-23
                this.CssClass += " required";
            }

            writer.AddAttribute("value", this.Text);

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
                base.Text = text2;
                return true;
            }
            return false;
        }

        #endregion 重写受保护的方法
    }
}
