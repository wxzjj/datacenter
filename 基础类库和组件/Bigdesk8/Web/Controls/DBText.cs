using System;
using System.Web.UI;
using System.Web;
using System.ComponentModel;
using Bigdesk8.Data;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 普通文本格式，换行符不做特殊处理
    /// </summary>
    [ToolboxData("<{0}:DBText runat=\"server\"></{0}:DBText>")]
    public class DBText : TextControl, IShowDataItem
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
                if (this.ViewState["DataItem_Type"].IsEmpty()) return DataType.String;

                return (DataType)this.ViewState["DataItem_Type"];
            }
            set
            {
                this.ViewState["DataItem_Type"] = value;
            }
        }

        /// <summary>
        /// 显示格式，注意：与数据类型有关系。格式字符串，如yyyy-MM-dd HH:mm:ss,标准数字格式字符串,日期和时间格式字符串
        /// </summary>
        [
        Description("显示格式，注意：与数据类型有关系。格式字符串，如yyyy-MM-dd HH:mm:ss,标准数字格式字符串,日期和时间格式字符串"),
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

            //s = s.Replace(Environment.NewLine, "<br/>");
            //s = s.Replace(Convert.ToChar(13).ToString(), "");   //回车   
            //s = s.Replace(Convert.ToChar(10).ToString(), "<br/>");//换行 
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
            string html = Convert2Html(DataUtility.FormatData(this.Text, this.ItemType, this.ItemFormat));
            writer.Write(html);
        }

        #endregion 重写基类成员
    }
}
