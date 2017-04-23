using System;
using System.ComponentModel;
using System.Web.UI;
using Bigdesk8.Data;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// HTML格式控件
    /// </summary>
    [ToolboxData("<{0}:DBHtml runat=\"server\"></{0}:DBHtml>")]
    public class DBHtml : TextControl, IShowDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBHtml()
            : base(HtmlTextWriterTag.Div)
        { }

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

        #endregion

        #region IDataItem 成员

        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.String;
            }
            set { }
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
        string IShowDataItem.ItemFormat
        {
            get
            {
                return "";
            }
            set { }
        }

        #endregion

        /// <summary>
        /// 获取或设置 <see cref="DBHtml"/> 控件的文本内容。返回控件的文本内容。默认值为 String.Empty。
        /// </summary>
        [
        PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty),
        Category("Appearance"),
        Bindable(true),
        DefaultValue(""),
        Description("Text")
        ]
        public override string Text
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
    }
}
