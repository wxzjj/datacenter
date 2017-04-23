using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using System.Text;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 日期控件
    /// </summary>
    [ToolboxData("<{0}:DBDateTimePicker runat=server></{0}:DBDateTimePicker>")]
    public class DBDateTimePicker : TextBox, IDataItem
    {
        string dateFmt;
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBDateTimePicker()
        {
            base.MaxLength = this.MaxLength;
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
            get
            {
                if (this.ViewState["DataItem_Relation"].IsEmpty()) return DataRelation.Equal;

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
            if (dateFmt == "yyyy-MM-dd HH"){
                if (data.Length == 13)
                {
                    data = data + ":00:00";
                }
            }
            if(dateFmt == "yyyy-MM-dd HH:mm")
            {
                if (data.Length == 16)
                {
                    data = data + ":00";
                }
            }
          
            // 数据类型检查
            if (!DataUtility.CheckDataType(data, ((IDataItem)this).ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN, ((IDataItem)this).ItemType);
                return this._ErrorMessage;
            }

            // 数据格式检查
            data = DataUtility.FormatData(data, ((IDataItem)this).ItemType, ((IDataItem)this).ItemFormat);
            if (data.IsEmpty())
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataFormat(nameCN, ((IDataItem)this).ItemType, ((IDataItem)this).ItemFormat);
                return this._ErrorMessage;
            }

            this.Text = data;

            // 最小值数据类型检查
            if (!DataUtility.CheckDataType(this.MinData, ((IDataItem)this).ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN + "(最小值)", ((IDataItem)this).ItemType);
                return this._ErrorMessage;
            }
            // 最大值数据类型检查
            if (!DataUtility.CheckDataType(this.MaxData, ((IDataItem)this).ItemType))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataType(nameCN + "(最大值)", ((IDataItem)this).ItemType);
                return this._ErrorMessage;
            }
            // 数据取值范围检查
            if (!DataUtility.CheckDataRange(data, ((IDataItem)this).ItemType, this.MinData, this.MaxData))
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataRange(nameCN, ((IDataItem)this).ItemType, this.MinData, this.MaxData);
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
                base.MaxLength = 20;
                return base.MaxLength;
            }
            set { }
        }
        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.DateTime;
            }
            set { }
        }
        string IShowDataItem.ItemFormat
        {
            get
            {
                return "yyyy-MM-dd HH:mm:ss";
            }
            set { }
        }

        #endregion

        #region 其它成员

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

        #endregion 其它成员

        #region 重写基类成员

        /// <summary>
        /// 获取或设置文本框中最多允许的字符数
        /// </summary>
        [Browsable(false)]
        public new int MaxLength
        {
            get
            {
                return 20;
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
            //writer.AddAttribute("onClick", "");
            if (!this._ErrorMessage.IsEmpty())
            {
                writer.AddAttribute("myerror", "true");
            }

            // 客户端校验必填项,数据类型
            if (this.ItemIsRequired)
            {
                //writer.AddAttribute("class", "required datetimeISO");//这种写法输出的class会屏蔽掉控件中以CssClass引用的样式定义，也会屏蔽掉以Theme方式给出的样式定义，故换成下面的写法，缪卫华2012-11-23
                this.CssClass += " required datetimeISO";
            }
            else
            {
                //writer.AddAttribute("class", "datetimeISO");
                this.CssClass += " datetimeISO";
            }

            // 客户端校验数据取值范围
            if (!this.MinData.IsEmpty() && !this.MaxData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", this.MinData, this.MaxData));
            }
            else if (!this.MinData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", this.MinData, "9999-12-31"));
            }
            else if (!this.MaxData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", "1000-01-01", this.MaxData));
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
                base.Text = text2;
                return true;
            }
            return false;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            dateFmt= base.Attributes["dateFmt"].ToString2();
            //base.Attributes.Add("onclick","setDayHM(this);");
           base.Attributes.Add("onclick", "WdatePicker({skin:'whyGreen',dateFmt:'" + dateFmt + "'})");
           base.Style["height"] = "20px";
           base.Style["Font-Size"] = "14px";
            base.OnLoad(e);
        }
        #endregion 重写受保护的方法
    }
}

