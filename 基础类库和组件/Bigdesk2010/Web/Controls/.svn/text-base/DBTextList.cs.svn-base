using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Bigdesk2010.Data;

namespace Bigdesk2010.Web.Controls
{

    /// <summary>
    /// 普通文本格式列表，换行符不做特殊处理
    /// </summary>
    [ToolboxData("<{0}:DBTextList runat=\"server\"></{0}:DBTextList>")]
    public class DBTextList : ListControl, IRepeatInfoUser, INamingContainer, IShowDataItem
    {
        /// <summary>
        /// 选中项的标识
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        [Description("选中项的标识")]
        public string CheckedText
        {
            get
            {
                return this.ViewState["_CheckedText"].ToString2();
            }
            set
            {
                this.ViewState["_CheckedText"] = value;
            }
        }

        /// <summary>
        /// 未选中项的标识
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        [Description("未选中项的标识")]
        public string UnCheckedText
        {
            get
            {
                return this.ViewState["_UnCheckedText"].ToString2();
            }
            set
            {
                this.ViewState["_UnCheckedText"] = value;
            }
        }

        /// <summary>
        /// 是否显示未选中项，默认值true，显示
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("未选中项的标识")]
        public bool IsShowUnchecked
        {
            get
            {
                object obj2 = this.ViewState["_IsShowUnchecked"];
                if (obj2 != null)
                    return obj2.ToBoolean();
                return true;
            }
            set
            {
                this.ViewState["_IsShowUnchecked"] = value;
            }
        }

        /// <summary>
        /// 内容分隔符
        /// </summary>
        [
        Description("内容分隔符"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string SplitString
        {
            get
            {
                return this.ViewState["_SplitString"].TrimString();
            }
            set
            {
                this.ViewState["_SplitString"] = value;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示当用户更改列表中的选定内容时是否自动产生向服务器的回发。默认值为 false。
        /// </summary>
        [Browsable(false)]
        public override bool AutoPostBack
        {
            get
            {
                return base.AutoPostBack;
            }
            set { }
        }

        /// <summary>
        /// 获取一个值，该值指示在单击从 System.Web.UI.WebControls.ListControl 类派生的控件时是否执行验证。
        /// </summary>
        [Browsable(false)]
        public override bool CausesValidation
        {
            get
            {
                return base.CausesValidation;
            }
            set { }
        }

        /// <summary>
        /// 将控件呈现给指定的 HTML 编写器。
        /// </summary>
        /// <param name="writer">接收控件内容的 <see cref="HtmlTextWriter"/> 对象。</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.IsShowUnchecked) DeleteUnChecked();

            if (this.Items.Count <= 0) return;

            RepeatInfo info = new RepeatInfo();
            info.RepeatColumns = this.RepeatColumns;
            info.RepeatDirection = this.RepeatDirection;
            if (!base.DesignMode && !this.Context.Request.Browser.Tables)
            {
                info.RepeatLayout = RepeatLayout.Flow;
            }
            else
            {
                info.RepeatLayout = this.RepeatLayout;
            }
            Style controlStyle = base.ControlStyleCreated ? base.ControlStyle : null;
            info.RenderRepeater(writer, this, controlStyle, this);
        }

        /// <summary>
        /// 删除未选中项
        /// </summary>
        private void DeleteUnChecked()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Selected) continue;

                this.Items.RemoveAt(i);
                DeleteUnChecked();
                return;
            }
        }

        /// <summary>
        /// 用指定的信息呈现列表中的项
        /// </summary>
        /// <param name="itemType"><see cref="ListItemType"/> 枚举值之一</param>
        /// <param name="repeatIndex">指定列表控件中项的位置的序号索引。</param>
        /// <param name="repeatInfo">表示用于呈现列表中的项的信息。</param>
        /// <param name="writer">表示要在客户端呈现 HTML 内容的输出流。</param>
        protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            ListItem item = this.Items[repeatIndex];

            Literal checktext = new Literal();
            checktext.Text = item.Selected ? this.CheckedText : this.UnCheckedText;

            HtmlGenericControl controlToRepeat = new HtmlGenericControl("span");
            controlToRepeat.Attributes.Clear();
            if (item.Attributes.Count > 0)
            {
                foreach (string str in item.Attributes.Keys)
                {
                    controlToRepeat.Attributes[str] = item.Attributes[str];
                }
            }
            controlToRepeat.ID = repeatIndex.ToString(NumberFormatInfo.InvariantInfo);
            controlToRepeat.InnerText = item.Text;

            if (repeatIndex != 0)
            {
                Literal splittext = new Literal();
                splittext.Text = this.SplitString;
                splittext.RenderControl(writer);
            }

            switch (this.TextAlign)
            {
                default:
                    {
                        checktext.RenderControl(writer);
                        controlToRepeat.RenderControl(writer);
                    }
                    break;
                case TextAlign.Left:
                    {
                        controlToRepeat.RenderControl(writer);
                        checktext.RenderControl(writer);
                    }
                    break;
            }
        }

        /// <summary>
        /// 获取或设置单元格内容和单元格边框之间的空间量。
        /// </summary>
        [DefaultValue(-1), Category("Layout"), Description("单元格内容与单元格边框之间的距离（以像素为单位）。默认值为 -1，表示未设置此属性。")]
        public virtual int CellPadding
        {
            get
            {
                if (!base.ControlStyleCreated)
                {
                    return -1;
                }
                return ((TableStyle)base.ControlStyle).CellPadding;
            }
            set
            {
                ((TableStyle)base.ControlStyle).CellPadding = value;
            }
        }

        /// <summary>
        /// 获取或设置表单元格之间的距离。
        /// </summary>
        [DefaultValue(-1), Category("Layout"), Description("表单元格之间的距离（以像素为单位）。默认值为 -1，表示未设置此属性。")]
        public virtual int CellSpacing
        {
            get
            {
                if (!base.ControlStyleCreated)
                {
                    return -1;
                }
                return ((TableStyle)base.ControlStyle).CellSpacing;
            }
            set
            {
                ((TableStyle)base.ControlStyle).CellSpacing = value;
            }
        }

        /// <summary>
        /// 获取或设置要显示的列数。
        /// </summary>
        [Category("Layout"), DefaultValue(0), Description("要显示的列数。默认值为 0，表示未设置该属性。")]
        public virtual int RepeatColumns
        {
            get
            {
                object obj2 = this.ViewState["RepeatColumns"];
                return obj2.IsEmpty() ? 0 : obj2.ToInt32();
            }
            set
            {
                this.ViewState["RepeatColumns"] = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示控件是垂直显示还是水平显示。默认值为 Vertical
        /// </summary>
        [Description("获取或设置一个值，该值指示控件是垂直显示还是水平显示。默认值为 Vertical"), DefaultValue(1), Category("Layout")]
        public virtual RepeatDirection RepeatDirection
        {
            get
            {
                object obj2 = this.ViewState["RepeatDirection"];
                if (obj2 != null)
                {
                    return (RepeatDirection)obj2;
                }
                return RepeatDirection.Vertical;
            }
            set
            {
                this.ViewState["RepeatDirection"] = value;
            }
        }

        /// <summary>
        /// 获取或设置复选框的布局。默认值为 Table。
        /// </summary>
        [Description("获取或设置复选框的布局。默认值为 Table。"), Category("Layout"), DefaultValue(0)]
        public virtual RepeatLayout RepeatLayout
        {
            get
            {
                object obj2 = this.ViewState["RepeatLayout"];
                if (obj2 != null)
                {
                    return (RepeatLayout)obj2;
                }
                return RepeatLayout.Table;
            }
            set
            {
                this.ViewState["RepeatLayout"] = value;
            }
        }

        /// <summary>
        /// 获取或设置组内复选框的文本对齐方式。默认值为 Right。
        /// </summary>
        [DefaultValue(2), Description("获取或设置组内复选框的文本对齐方式。默认值为 Right。"), Category("Appearance")]
        public virtual TextAlign TextAlign
        {
            get
            {
                object obj2 = this.ViewState["TextAlign"];
                if (obj2 != null)
                {
                    return (TextAlign)obj2;
                }
                return TextAlign.Right;
            }
            set
            {
                this.ViewState["TextAlign"] = value;
            }
        }

        /// <summary>
        /// 创建内部用来实现所有与样式有关的属性的样式对象。
        /// </summary>
        /// <returns></returns>
        protected override Style CreateControlStyle()
        {
            return new TableStyle(this.ViewState);
        }

        /// <summary>
        /// 检索列表控件中指定索引位置的指定项类型的样式。
        /// </summary>
        /// <param name="itemType"><see cref="ListItemType"/> 枚举值之一</param>
        /// <param name="repeatIndex">指定列表控件中项的位置的序号索引。</param>
        /// <returns></returns>
        protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
        {
            return null;
        }

        /// <summary>
        /// 获取列表项数
        /// </summary>
        protected virtual int RepeatedItemCount
        {
            get
            {
                if (this.Items == null)
                {
                    return 0;
                }
                return this.Items.Count;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示是否包脚注部分
        /// </summary>
        protected virtual bool HasFooter
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示是否包含标题节。
        /// </summary>
        protected virtual bool HasHeader
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示是否包含列表项之间的分隔符。
        /// </summary>
        protected virtual bool HasSeparators
        {
            get
            {
                return false;
            }
        }

        Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
        {
            return this.GetItemStyle(itemType, repeatIndex);
        }

        void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
        }

        bool IRepeatInfoUser.HasFooter
        {
            get
            {
                return this.HasFooter;
            }
        }

        bool IRepeatInfoUser.HasHeader
        {
            get
            {
                return this.HasHeader;
            }
        }

        bool IRepeatInfoUser.HasSeparators
        {
            get
            {
                return this.HasSeparators;
            }
        }

        int IRepeatInfoUser.RepeatedItemCount
        {
            get
            {
                return this.RepeatedItemCount;
            }
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

        #endregion

        #region IShowDataItem 成员

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
                return base.SelectedValue;
            }
            set
            {
                string s = value.TrimString();
                ListItem li = base.Items.FindByValue(s);
                if (li != null)
                {
                    li.Selected = true;
                }
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
    }
}
