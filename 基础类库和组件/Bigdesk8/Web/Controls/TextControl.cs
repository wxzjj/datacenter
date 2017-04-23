using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 普通文本格式控件
    /// </summary>
    [
    ToolboxData("<{0}:TextControl runat=\"server\"></{0}:TextControl>"),
    ControlBuilder(typeof(LabelControlBuilder)),
    ControlValueProperty("Text"),
    DefaultProperty("Text"),
    ParseChildren(false),
    Designer("System.Web.UI.Design.WebControls.LabelDesigner")
    ]
    public class TextControl : WebControl, ITextControl
    {
        /// <summary>
        /// 初始化表示 Span HTML 标记的 <see cref="TextControl"/> 类的新实例。
        /// </summary>
        public TextControl()
            : base()
        { }

        /// <summary>
        /// 初始化表示 Span HTML 标记的 <see cref="TextControl"/> 类的新实例。
        /// </summary>
        /// <param name="tag"><see cref="HtmlTextWriterTag"/> 值之一。</param>
        public TextControl(HtmlTextWriterTag tag)
            : base(tag)
        { }

        /// <summary>
        /// 初始化表示 Span HTML 标记的 <see cref="TextControl"/> 类的新实例。
        /// </summary>
        /// <param name="tag">HTML 标记</param>
        public TextControl(string tag)
            : base(tag)
        { }

        /// <summary>
        /// 通知控件某元素已经过分析，并将该元素添加到 <see cref="TextControl"/> 控件。
        /// </summary>
        /// <param name="obj">表示已分析元素的对象。</param>
        protected override void AddParsedSubObject(object obj)
        {
            if (this.HasControls())
            {
                base.AddParsedSubObject(obj);
            }
            else if (obj is LiteralControl)
            {
                this.Text = ((LiteralControl)obj).Text;
            }
            else
            {
                string text = this.Text;
                if (text.Length != 0)
                {
                    this.Text = string.Empty;
                    base.AddParsedSubObject(new LiteralControl(text));
                }
                base.AddParsedSubObject(obj);
            }
        }

        /// <summary>
        /// 加载以前保存的控件的状态。
        /// </summary>
        /// <param name="savedState">包含控件的保存的视图状态值的对象。</param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                base.LoadViewState(savedState);
                string str = (string)this.ViewState["Text"];
                if (str != null)
                {
                    this.Text = str;
                }
            }
        }

        /// <summary>
        /// 将 <see cref="TextControl"/> 的内容呈现到指定的编写器中。
        /// </summary>
        /// <param name="writer">向客户端呈现 HTML 内容的输出流。</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(this.Text);

            base.RenderContents(writer);
        }

        /// <summary>
        /// 获取或设置 <see cref="TextControl"/> 控件的文本内容。返回控件的文本内容。默认值为 String.Empty。
        /// </summary>
        [
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        Category("Appearance"),
        Bindable(true),
        DefaultValue(""),
        Description("Text")
        ]
        public virtual string Text
        {
            get
            {
                object obj2 = this.ViewState["Text"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                if (this.HasControls())
                {
                    this.Controls.Clear();
                }
                this.ViewState["Text"] = value;
            }
        }
    }
}
