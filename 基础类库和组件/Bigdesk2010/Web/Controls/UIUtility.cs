using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Bigdesk2010.Data;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// UI 界面控件功能函数库
    /// </summary>
    public static class UIUtility
    {
        #region 界面数据检查

        #region 公共方法

        /// <summary>
        /// 界面数据的检查(包括数据的类型,数据的长度,必填项的检查,取值范围检查),如果检查成功返回String.Empty，检查不成功返回错误信息
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <returns>如果检查成功返回String.Empty，检查不成功返回错误信息</returns>
        public static string CheckControlValue(this Control control)
        {
            // 是否有控件
            if (control == null) return String.Empty;

            // 如果控件不可见,则不检查数据
            if (!control.Visible) return String.Empty;

            List<IDataItem> dis = GetDataItem(control);

            string result = String.Empty;
            foreach (IDataItem di in dis)
            {
                result = di.ItemCheck();
               
                // 如果检查有错误,则结束检查.
                if (!result.IsEmpty()) return result;
            }

            //下面开始递归运行
            foreach (Control tempControl in control.Controls)
            {
                result = tempControl.CheckControlValue();

                // 如果检查有错误,则结束检查.
                if (!result.IsEmpty()) return result;
            }

            return result;
        }

        #endregion 公共方法

        #endregion 界面数据检查

        #region 界面数据与 IDataItem 交互

        #region 公开方法

        /// <summary>
        /// 获得界面控件上的数据，并存放入 <see cref="IDataItem"/> 集合中
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        public static List<IDataItem> GetControlValue(this Control control)
        {
            List<IDataItem> list = new List<IDataItem>();

            control.GetControlValue(list, false, true);

            return list;
        }

        /// <summary>
        /// 获得界面控件上的数据，并存放入 <see cref="IDataItem"/> 集合中
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <param name="isRecursion">是否对子控件进行递归</param>
        public static List<IDataItem> GetControlValue(this Control control, bool isRecursion)
        {
            List<IDataItem> list = new List<IDataItem>();

            control.GetControlValue(list, false, isRecursion);

            return list;
        }

        /// <summary>
        /// 获得界面控件上的数据，并存放入 <see cref="IDataItem"/> 集合中
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <param name="dataitem"><see cref="IDataItem"/> 集合</param>
        /// <param name="overwrite">是否要覆盖现有数据</param>
        /// <param name="isRecursion">是否对子控件进行递归</param>
        public static void GetControlValue(this Control control, List<IDataItem> dataitem, bool overwrite, bool isRecursion)
        {
            if (control == null) return;

            List<IDataItem> dis = GetDataItem(control);

            foreach (IDataItem di in dis)
            {
                IDataItem di2 = dataitem.GetDataItem(di.ItemName);
                if (di2 == null || !overwrite)
                {
                    di2 = new DataItem();
                    di2.ItemName = di.ItemName;
                    dataitem.Add(di2);
                }

                di2.ItemType = di.ItemType;
                di2.ItemData = di.ItemData;
                di2.ItemRelation = di.ItemRelation;

                if (!di2.ItemData.IsEmpty())
                {
                    switch (di2.ItemType)
                    {
                        case DataType.Date:
                            di2.ItemData = di2.ItemData.ToDate2();
                            break;
                        case DataType.Time:
                            di2.ItemData = di2.ItemData.ToTime().ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case DataType.DateTime:
                            di2.ItemData = di2.ItemData.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                    }
                }
            }

            if (isRecursion)
            {
                //下面开始递归运行
                foreach (Control tempControl in control.Controls)
                {
                    tempControl.GetControlValue(dataitem, overwrite, isRecursion);
                }
            }
        }

        /// <summary>
        /// 将 IDataItem 中的数据显示在界面控件上
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <param name="dataitem">所有数据项</param>
        public static void SetControlValue(this Control control, List<IDataItem> dataitem)
        {
            if (dataitem == null || dataitem.Count <= 0) return;
            if (control == null) return;

            List<IShowDataItem> dis = GetShowDataItem(control);

            foreach (IShowDataItem di in dis)
            {
                IDataItem di2 = dataitem.GetDataItem(di.ItemName);
                if (di2 != null) di.ItemData = DataUtility.FormatData(di2.ItemData, di.ItemType, di.ItemFormat);
            }

            //下面开始递归运行
            foreach (Control tempControl in control.Controls)
            {
                tempControl.SetControlValue(dataitem);
            }
        }

        /// <summary>
        /// 将 IDataItem 中的默认数据显示在界面控件上
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <param name="overwrite">是否要覆盖现有数据</param>
        public static void SetControlDefaultValue(this Control control, bool overwrite)
        {
            if (control == null) return;

            List<IDataItem> dis = GetDataItem(control);

            foreach (IDataItem di in dis)
            {
                if (di.ItemData.IsEmpty() || overwrite)
                {
                    di.ItemData = DataUtility.FormatData(di.ItemDefaultData, di.ItemType, di.ItemFormat);
                }
            }

            //下面开始递归运行
            foreach (Control tempControl in control.Controls)
            {
                tempControl.SetControlDefaultValue(overwrite);
            }
        }

        private static List<IShowDataItem> GetShowDataItem(Control control)
        {
            List<IShowDataItem> dis = new List<IShowDataItem>();

            if (control is IShowDataItem)
            {
                dis.Add((IShowDataItem)control);
            }
            else if (control is IValueTextItem)
            {
                IValueTextItem vti = (IValueTextItem)control;
                if (vti.ValueItem != null) dis.Add(vti.ValueItem);
                if (vti.TextItem != null) dis.Add(vti.TextItem);
            }

            return dis;
        }

        /// <summary>
        /// 对下拉框控件，可以同时返回两个IDataItem，故本函数采用List<IDataItem>作为返回类型
        /// </summary>
        private static List<IDataItem> GetDataItem(Control control)
        {
            List<IDataItem> dis = new List<IDataItem>();

            if (control is IDataItem)
            {
                dis.Add((IDataItem)control);
            }
            else if (control is IValueTextItem)
            {
                IValueTextItem vti = (IValueTextItem)control;
                if (vti.ValueItem != null) dis.Add(vti.ValueItem);
                if (vti.TextItem != null) dis.Add(vti.TextItem);
            }

            return dis;
        }

        #endregion 公开方法

        #endregion 界面数据与 IDataItem 交互

        #region 数据绑定

        /// <summary>
        /// 将 IEnumerable 填充到 ListControl，以及从 <see cref="ListControl"/> 控件的子控件，例如：listControl,CheckBoxList,RadioButtonList...
        /// IEnumerable 中匿名类型的第一个属性表示文本,第二个属性表示代码. data 中的元素至少要有一个属性.
        /// 例子: var city1 = from a in db.d_city select new { a.NodeName, a.NodeID };
        /// 或者  var city2 = from a in db.d_city select new { a.NodeName };
        /// </summary>
        /// <param name="listControl">控件，从 <see cref="ListControl"/> 控件的子控件都可以绑定，例如：listControl,CheckBoxList,RadioButtonList...</param>
        /// <param name="data">数据</param>
        /// <param name="isAddSpaceItem">是否要加入一个空项</param>
        public static void ListControlDataBind(this ListControl listControl, IEnumerable data, bool isAddSpaceItem)
        {
            foreach (object obj in data)
            {
                Type type = obj.GetType();
                if (type.Namespace == "System")
                {
                    listControl.Items.Add(new ListItem(obj.ToString(), obj.ToString()));
                }
                else
                {
                    PropertyInfo[] pis = type.GetProperties();
                    switch (pis.Length)
                    {
                        case 1:
                            listControl.Items.Add(new ListItem(pis[0].GetValue(obj, null).ToString(), pis[0].GetValue(obj, null).ToString()));
                            break;
                        case 2:
                            listControl.Items.Add(new ListItem(pis[0].GetValue(obj, null).ToString(), pis[1].GetValue(obj, null).ToString()));
                            break;
                        default:
                            throw new Exception("ListControlDataBind 参数 data 不正确! data 中的元素至少要有一个属性,最多有两个属性.");
                    }
                }
            }
            if (isAddSpaceItem)
            {
                listControl.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// 将 DataTable 填充到 ListControl，以及从 <see cref="ListControl"/> 控件的子控件，例如：listControl,CheckBoxList,RadioButtonList...
        /// 如果 DataTable 有两列，则第一列表示文本，第二列表示值.data 中的 DataRow 至少要有一个字段.
        /// </summary>
        /// <param name="listControl">控件，从 <see cref="ListControl"/> 控件的子控件都可以绑定，例如：listControl,CheckBoxList,RadioButtonList...</param>
        /// <param name="data">数据</param>
        /// <param name="isAddSpaceItem">是否要加入一个空项</param>
        public static void ListControlDataBind(this ListControl listControl, DataTable data, bool isAddSpaceItem)
        {
            int columnCount = data.Columns.Count;
            foreach (DataRow dr in data.Rows)
            {
                switch (columnCount)
                {
                    case 1:
                        listControl.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                        break;
                    case 2:
                        listControl.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                        break;
                    default:
                        throw new Exception("ListControlDataBind 参数 data 不正确! data 中的 DataRow 至少要有一个字段.");
                }
            }
            if (isAddSpaceItem)
            {
                listControl.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// 将 IDictionary 填充到 ListControl，以及从 <see cref="ListControl"/> 控件的子控件，例如：listControl,CheckBoxList,RadioButtonList...
        /// IDictionary 的 Key 表示代码, value 表示文本
        /// 例子: Hashtable ht = new Hashtable(); ht[key]=value;
        /// </summary>
        /// <param name="listControl">控件，从 <see cref="ListControl"/> 控件的子控件都可以绑定，例如：listControl,CheckBoxList,RadioButtonList...</param>
        /// <param name="data">数据</param>
        /// <param name="isAddSpaceItem">是否要加入一个空项</param>
        public static void ListControlDataBind(this ListControl listControl, IDictionary data, bool isAddSpaceItem)
        {
            foreach (string key in data.Keys)
            {
                listControl.Items.Add(new ListItem(data[key].ToString(), key));
            }
            if (isAddSpaceItem)
            {
                listControl.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// 将 ListItem 填充到 ListControl，以及从 <see cref="ListControl"/> 控件的子控件，例如：listControl,CheckBoxList,RadioButtonList...
        /// </summary>
        /// <param name="listControl">控件，从 <see cref="ListControl"/> 控件的子控件都可以绑定，例如：listControl,CheckBoxList,RadioButtonList...</param>
        /// <param name="data">数据</param>
        /// <param name="isAddSpaceItem">是否要加入一个空项</param>
        public static void ListControlDataBind(this ListControl listControl, List<ListItem> data, bool isAddSpaceItem)
        {
            foreach (ListItem li in data.ToArray())
            {
                ListItem innerLi = new ListItem();
                innerLi.Value = li.Value;
                innerLi.Text = li.Text;
                listControl.Items.Add(innerLi);
            }

            if (isAddSpaceItem)
            {
                listControl.Items.Insert(0, new ListItem("", ""));
            }
        }

        #endregion 数据绑定

        /// <summary>
        /// 设置界面上控件的 Enabled=!isReadOnly 或者 ReadOnly=isReadOnly 或者 Disabled=isReadOnly
        /// </summary>
        /// <param name="control">控件或控件所在的父控件</param>
        /// <param name="isReadOnly">是否为只读</param>
        public static void SetControlReadOnly(this Control control, bool isReadOnly)
        {
            if (control == null) return;

            TextBox tb = control as TextBox;
            if (tb != null)
                tb.ReadOnly = isReadOnly;
            else
            {
                WebControl wc = control as WebControl;
                if (wc != null)
                    wc.Enabled = !isReadOnly;
                else
                {
                    HtmlControl hc = control as HtmlControl;
                    if (hc != null)
                        hc.Disabled = isReadOnly;
                }
            }

            //下面开始递归运行
            foreach (Control tempControl in control.Controls)
            {
                tempControl.SetControlReadOnly(isReadOnly);
            }
        }
    }
}
