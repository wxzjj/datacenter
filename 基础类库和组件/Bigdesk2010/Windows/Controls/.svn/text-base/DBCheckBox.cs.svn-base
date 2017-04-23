using System;
using System.ComponentModel;
using System.Windows.Forms;
using Bigdesk2010.Data;

namespace Bigdesk2010.Windows.Controls
{
    /// <summary>
    /// <see cref="DBCheckBox"/> 对 <see cref="CheckBox"/> 复选框控件进行功能扩展
    /// </summary>
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
            get;
            set;
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
            get;
            set;
        }

        #endregion 其它成员

    }
}