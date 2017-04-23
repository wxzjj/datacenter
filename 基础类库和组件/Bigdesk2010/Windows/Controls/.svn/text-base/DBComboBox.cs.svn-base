using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Bigdesk2010.Data;

namespace Bigdesk2010.Windows.Controls
{
    /// <summary>
    /// 只允许从中选择一项的下拉列表控件
    /// </summary>
    public class DBComboBox : ComboBox, IValueTextItem
    {
        #region ValueItem IDataItem 成员

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
            get;
            set;
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
            get;
            set;
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
            string nameCN = this.ItemNameCN.IsEmpty() ? "控件ID：" + this.Name : this.ItemNameCN;
            string data = base.SelectedValue.TrimString();

            // 必填项检查
            if (this.ItemIsRequired && data.IsEmpty())
            {
                this.Focus();
                this._ErrorMessage = DataUtility.GetErrorMessage_DataRequired(nameCN);
                return this._ErrorMessage;
            }

            if (data.IsEmpty()) return "";

            //等等.....


            return string.Empty;
        }

        /// <summary>
        /// Value 数据项名称
        /// </summary>
        [
        Description("Value 数据项名称"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string ItemName
        {
            get;
            set;
        }

        /// <summary>
        /// Value 数据类型
        /// </summary>
        [
        Description("Value 数据类型"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataType.String)
        ]
        public DataType ItemType
        {
            get;
            set;
        }

        /// <summary>
        /// Value 数据项与数据的关系
        /// </summary>
        [
        Description("数据项与数据的关系"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataRelation.Equal)
        ]
        public DataRelation ItemRelation
        {
            get;
            set;
        }

        private string ItemData
        {
            get
            {
                if (base.SelectedValue.IsEmpty())
                    return string.Empty;
                else
                    return base.SelectedValue.ToString();
            }
            set
            {
                string s = value.TrimString();
                if (string.IsNullOrEmpty(s)) return;

                base.SelectedValue = s;

                if (base.SelectedItem == null && this.IsAutoAddItem)
                {
                    base.Items.Add(new ListItem(s, s));
                    base.SelectedValue = s;
                }
            }
        }

        #endregion

        #region TextItem IDataItem 成员

        /// <summary>
        /// Text 数据项名称
        /// </summary>
        [
        Description("Text 数据项名称"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue("")
        ]
        public string TextItemName
        {
            get;
            set;
        }

        /// <summary>
        /// Text 数据类型
        /// </summary>
        [
        Description("Text 数据类型"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataType.String)
        ]
        public DataType TextItemType
        {
            get;
            set;
        }

        /// <summary>
        /// Text 数据项与数据的关系
        /// </summary>
        [
        Description("Text 数据项与数据的关系"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(DataRelation.Equal)
        ]
        public DataRelation TextItemRelation
        {
            get;
            set;
        }

        #endregion

        #region IValueTextItem 成员

        IDataItem IValueTextItem.ValueItem
        {
            get
            {
                //if (this.ItemName.IsEmpty())
                //    return null;

                DataItem di = new DataItem();
                di.ItemName = this.ItemName;
                di.ItemType = this.ItemType;
                di.ItemData = this.ItemData;
                di.ItemNameCN = this.ItemNameCN;
                di.ItemIsRequired = this.ItemIsRequired;
                di.ItemRelation = this.ItemRelation;

                di.ItemFormat = "";
                di.ItemLength = 0;
                di.ItemDefaultData = "";
                di.Checked += this.ItemCheck;
                di.ItemDataChanged += this.DataItem_ItemDataChanged;
                return di;
            }
            set
            {
                if (value == null)
                    this.ItemData = "";
                else
                    this.ItemData = value.ItemData;
            }
        }

        private void DataItem_ItemDataChanged(string newData)
        {
            this.ItemData = newData;
        }

        IDataItem IValueTextItem.TextItem
        {
            get
            {
                //if (this.TextItemName.IsEmpty())
                //    return null;

                DataItem di = new DataItem();
                di.ItemName = this.TextItemName;
                di.ItemType = this.TextItemType;
                di.ItemData = base.SelectedItem == null ? "" : base.SelectedText;
                di.ItemNameCN = this.ItemNameCN;
                //di.ItemIsRequired = this.ItemIsRequired;
                //di.ItemRelation = this.TextItemRelation;

                di.ItemFormat = "";
                di.ItemLength = 0;
                di.ItemDefaultData = "";
                //di.Checked += this.ItemCheck;
                //di.ItemDataChanged += this.DataItem_ItemDataChanged;
                return di;
            }
            set
            {
                //
            }
        }

        #endregion

        #region 重写属性

        /// <summary>
        /// WinForm中，如果ComboBox没用数据源绑定方式绑定数据，那么SelectedValue属性根本就无效的，
        /// 使用这个属性得到的值都是null，故进行了重写。
        /// </summary>
        [Category("重写属性")]
        [Description("因原始ComboBox的SelectedValue属性有些问题，故重写之")]
        [Browsable(true)]
        [Bindable(true)]
        [DefaultValue("")]
        public new object SelectedValue
        {
            get
            {
                if (this.DataSource != null)
                    return base.SelectedValue;
                else
                {
                    if (this.SelectedIndex > -1)
                    {
                        ListItem li = this.SelectedItem as ListItem;
                        if (li != null) return li.ListItemValue;
                    }
                    return base.SelectedValue;
                }
            }
            set
            {
                if (this.DataSource != null)
                    base.SelectedValue = value;
                else
                {
                    for (int i = 0; i < this.Items.Count; ++i)
                    {
                        ListItem item = this.Items[i] as ListItem;
                        if (item != null)
                        {
                            if (item.ListItemValue == value)
                            {
                                this.SelectedIndex = i;
                                return;
                            }
                        }
                        else
                        {
                            base.SelectedValue = value;
                            return;
                        }
                    }
                    this.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// WinForm中，如果ComboBox没用数据源绑定方式绑定数据，那么SelectedText属性根本就无效的，
        /// 使用这个属性得到的值都是null，故进行了重写。
        /// </summary>
        [Category("重写属性")]
        [Description("因原始ComboBox的SelectedText属性有些问题，故重写之")]
        [Browsable(true)]
        [Bindable(true)]
        [DefaultValue("")]
        public new string SelectedText
        {
            get
            {
                if (this.DataSource != null)
                    return base.SelectedText;
                else
                {
                    if (this.SelectedIndex > -1)
                    {
                        ListItem li = this.SelectedItem as ListItem;
                        if (li != null) return li.ListItemText;
                    }
                    return base.SelectedText;
                }
            }
            set
            {
                if (this.DataSource != null)
                    base.SelectedText = value;
                else
                {
                    for (int i = 0; i < this.Items.Count; ++i)
                    {
                        ListItem item = this.Items[i] as ListItem;
                        if (item != null)
                        {
                            if (item.ListItemText == value)
                            {
                                this.SelectedIndex = i;
                                return;
                            }
                        }
                        else
                        {
                            base.SelectedText = value;
                            return;
                        }
                    }
                    this.SelectedIndex = -1;
                }
            }
        }
        #endregion 重写属性

        #region 其它

        /// <summary>
        /// 当列表中可选项不存在时，是否自动添加
        /// </summary>
        [
        Description("当列表中可选项不存在时，是否自动添加"),
        Category("扩展属性"),
        Bindable(true),
        DefaultValue(true)
        ]
        public virtual bool IsAutoAddItem
        {
            get;
            set;
        }

        #endregion 其它
    }
}
