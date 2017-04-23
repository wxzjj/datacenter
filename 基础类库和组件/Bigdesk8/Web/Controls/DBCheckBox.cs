using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// <see cref="DBCheckBox"/> 对 <see cref="CheckBox"/> 复选框控件进行功能扩展
    /// </summary>
    [ToolboxData("<{0}:DBCheckBox runat=server></{0}:DBCheckBox>")]
    public class DBCheckBox : CheckBox, IDataItem
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

        #endregion

        #region IDataItem 成员

        string IDataItem.ItemNameCN
        {
            get
            {
                return "";
            }
            set { }
        }
        DataType IShowDataItem.ItemType
        {
            get
            {
                return DataType.Boolean;
            }
            set { }
        }
        int IDataItem.ItemLength
        {
            get
            {
                return 0;
            }
            set { }
        }
        DataRelation IDataItem.ItemRelation
        {
            get
            {
                return DataRelation.Equal;
            }
            set { }
        }
        bool IDataItem.ItemIsRequired
        {
            get
            {
                return false;
            }
            set { }
        }
        string IShowDataItem.ItemData
        {
            get
            {
                switch (this.QueryValue)
                {
                    default:
                    case CheckBoxValue.XZ_1_BXZ_0://选中表示 1，不选中表示 0，默认情况
                        return base.Checked.ToString();
                    case CheckBoxValue.XZ_01_BXZ_0:// 选中表示 0 和 1 两种情况,不选中表示 0
                        {
                            if (!base.Checked)
                                return false.ToString();
                        }
                        break;
                    case CheckBoxValue.XZ_0_BXZ_1:
                        {
                            return (!base.Checked).ToString();
                        }
                    case CheckBoxValue.XZ_0_BXZ_O1:
                        {
                            if (base.Checked)
                                return false.ToString();
                        }
                        break;
                    case CheckBoxValue.XZ_1_BXZ_01:
                        {
                            if (base.Checked)
                                return true.ToString();
                        }
                        break;
                    case CheckBoxValue.XZ_O1_BXZ_1:
                        {
                            if (!base.Checked)
                                return true.ToString();
                        }
                        break;
                }
                return "";
            }
            set
            {
                base.Checked = value.ToBoolean(false);
            }
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
        string IDataItem.ItemDefaultData
        {
            get
            {
                return "";
            }
            set { }
        }
        string IDataItem.ItemCheck()
        {
            return "";
        }

        #endregion

        #region 其它成员

        /// <summary>
        ///  CheckBox 在查询时,选中与不选中所代表的值
        /// </summary>
        [
        Description("在查询时,选中与不选中所代表的值"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(CheckBoxValue.XZ_1_BXZ_0)
        ]
        public CheckBoxValue QueryValue
        {
            get
            {
                if (this.ViewState["_QueryValue"] == null)
                    return CheckBoxValue.XZ_1_BXZ_0;

                return (CheckBoxValue)this.ViewState["_QueryValue"];
            }
            set
            {
                this.ViewState["_QueryValue"] = value;
            }
        }

        #endregion 其它成员

        #region 重写受保护的方法

        ///// <summary>
        ///// 将需要呈现的 HTML 属性和样式添加到指定的 <see cref="HtmlTextWriter"/> 实例中。
        ///// </summary>
        ///// <param name="writer"><see cref="HtmlTextWriter"/>，表示要在客户端呈现 HTML 内容的输出流。</param>
        //protected override void AddAttributesToRender(HtmlTextWriter writer)
        //{
        //    base.AddAttributesToRender(writer);
        //}

        #endregion 重写受保护的方法

    }
}