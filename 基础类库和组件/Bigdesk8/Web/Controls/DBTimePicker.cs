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
    /// 时间控件
    /// </summary>
    [ToolboxData("<{0}:DBTimePicker runat=server></{0}:DBTimePicker>")]
    public class DBTimePicker : TextBox, IDataItem
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
                return this.MaxLength;
            }
            set { }
        }
        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.Time;
            }
            set { }
        }
        string IShowDataItem.ItemFormat
        {
            get
            {
                return this.IsShowSecond ? "HH:mm:ss" : "HH:mm";
            }
            set { }
        }

        #endregion

        #region 其它成员

        /// <summary>
        /// 是否显示秒，如10:30,10:30:30
        /// </summary>
        [
        Description("是否显示秒，如10:30,10:30:30"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(false)
        ]
        public bool IsShowSecond
        {
            get
            {
                if (this.ViewState["_IsShowSecond"].IsEmpty()) return false;
                return this.ViewState["_IsShowSecond"].ToBoolean();
            }
            set
            {
                this.ViewState["_IsShowSecond"] = value;
            }
        }

        /// <summary>
        /// 最小值
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
        /// 最大值
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
                return this.IsShowSecond ? 8 : 5;
            }
        }

        #endregion 重写基类成员

        #region 重写受保护的方法

        /// <summary>
        /// 在初始化之后，显示之前发生
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            base.MaxLength = this.MaxLength;
        }

        /// <summary>
        /// 将需要呈现的 HTML 属性和样式添加到指定的 <see cref="HtmlTextWriter"/> 实例中。
        /// </summary>
        /// <param name="writer"><see cref="HtmlTextWriter"/>，表示要在客户端呈现 HTML 内容的输出流。</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute("mytype", "time");
            if (this.IsShowSecond)
            {
                writer.AddAttribute("myshowsecond", "true");
            }
            if (!this._ErrorMessage.IsEmpty())
            {
                writer.AddAttribute("myerror", "true");
            }

            // 客户端校验必填项,数据类型
            if (this.ItemIsRequired)
            {
                //writer.AddAttribute("class", "required time");//这种写法输出的class会屏蔽掉控件中以CssClass引用的样式定义，也会屏蔽掉以Theme方式给出的样式定义，故换成下面的写法，缪卫华2012-11-23
                this.CssClass += " required time";
            }
            else
            {
                //writer.AddAttribute("class", "time");
                this.CssClass += " time";
            }

            // 客户端校验数据取值范围
            string min = DataUtility.FormatData(this.MinData, ((IDataItem)this).ItemType, ((IDataItem)this).ItemFormat);
            string max = DataUtility.FormatData(this.MaxData, ((IDataItem)this).ItemType, ((IDataItem)this).ItemFormat);
            if (!this.MinData.IsEmpty() && !this.MaxData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", min, max));
            }
            else if (!this.MinData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", min, this.IsShowSecond ? "23:59:59" : "23:59"));
            }
            else if (!this.MaxData.IsEmpty())
            {
                writer.AddAttribute("range", string.Format("['{0}','{1}']", this.IsShowSecond ? "00:00:00" : "00:00", max));
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

        #endregion 重写受保护的方法
    }
}
