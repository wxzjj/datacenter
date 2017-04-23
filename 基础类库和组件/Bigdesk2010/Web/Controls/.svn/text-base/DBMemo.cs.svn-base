using System;
using System.Web.UI;
using System.Web;
using Bigdesk2010.Data;
using System.ComponentModel;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// 普通文本格式，将换行符替换成HTML标签<br/>
    /// </summary>
    [ToolboxData("<{0}:DBMemo runat=\"server\"></{0}:DBMemo>")]
    public class DBMemo : TextControl, IShowDataItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        //public DBMemo()
        //    : base(HtmlTextWriterTag.P)
        //{ }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// 原先生成到客户端的html控件P的显示效果不易调控，故改成了html控件Label。2012-11-12 缪卫华
        public DBMemo()
            : base(HtmlTextWriterTag.Label)
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

        #region 重写基类成员

        private string Convert2Html(string text)
        {
            string s = text;

            if (base.DesignMode)
            {
                s = s.Replace('&'.ToString(), "&amp;");
                s = s.Replace('"'.ToString(), "&quot;");
                s = s.Replace('<'.ToString(), "&lt;");
                s = s.Replace('>'.ToString(), "&gt;");
            }
            else
            {
                s = HttpContext.Current.Server.HtmlEncode(s);
            }

            s = s.Replace(Environment.NewLine, "<br/>");
            s = s.Replace(Convert.ToChar(13).ToString(), "");   //回车   
            s = s.Replace(Convert.ToChar(10).ToString(), "<br/>");//换行 
            s = s.Replace(Convert.ToChar(32).ToString(), "&nbsp;"); // 空格
            s = s.Replace(Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;");//Tab  

            return s;
        }

        /// <summary>
        /// 将 <see cref="DBMemo"/> 的内容呈现到指定的编写器中。
        /// </summary>
        /// <param name="writer">向客户端呈现 HTML 内容的输出流。</param> 
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string html = Convert2Html(this.Text);
            writer.Write(html);
        }

        #endregion 重写基类成员
    }
}
