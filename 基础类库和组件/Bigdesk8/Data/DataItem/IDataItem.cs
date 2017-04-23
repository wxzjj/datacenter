using System;

namespace Bigdesk8.Data
{
    /// <summary>
    /// 单一显示数据项接口，界面显示控件实现此接口，方便数据与界面挂接
    /// </summary>
    public interface IShowDataItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        string ItemName { get; set; }
       
        /// <summary>
        /// 类型
        /// </summary>
        DataType ItemType { get; set; }

        /// <summary>
        /// 数据，注意：DBNull，null自动转换成string.Empty
        /// </summary>
        string ItemData { get; set; }

        /// <summary>
        /// 显示格式，注意：与数据类型有关系。格式字符串，如yyyy-MM-dd HH:mm:ss,日期和时间格式字符串,标准数字格式字符串
        /// </summary>
        string ItemFormat { get; set; }
    }

    /// <summary>
    /// 单一数据项接口，界面控件实现此接口，方便数据与界面挂接
    /// </summary>
    public interface IDataItem : IShowDataItem
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        string ItemNameCN { get; set; }

        /// <summary>
        /// 最大长度。默认值为 0。
        /// </summary>
        int ItemLength { get; set; }

        /// <summary>
        /// 是否为必填项。默认值为 false。
        /// </summary>
        bool ItemIsRequired { get; set; }

        /// <summary>
        /// 与数据的关系
        /// </summary>
        DataRelation ItemRelation { get; set; }


        /// <summary>
        /// 默认数据，默认值为string.Empty
        /// </summary>
        string ItemDefaultData { get; set; }

        /// <summary>
        /// 检查类型是否匹配，长度是否超长，必填项是否已填，取值范围是否正确等等.....
        /// </summary>
        /// <returns>如果检查成功返回String.Empty，检查不成功返回错误信息</returns>
        string ItemCheck();
    }

    /// <summary>
    /// Value、Text 数据项接口，界面控件实现此接口，方便数据与界面挂接
    /// </summary>
    public interface IValueTextItem
    {
        /// <summary>
        /// Value 数据项
        /// </summary>
        IDataItem ValueItem { get; set; }

        /// <summary>
        /// Text 数据项
        /// </summary>
        IDataItem TextItem { get; set; }
    }
    
    /// <summary>
    /// 单一数据项
    /// </summary>
    [Serializable]
    public class DataItem : IDataItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string ItemNameCN { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DataType ItemType { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        private string _ItemData;
        /// <summary>
        /// 数据
        /// </summary>
        public string ItemData
        {
            get
            {
                return this._ItemData.TrimString();
              
            }
            set
            {
                this._ItemData = value;

                this.OnItemDataChanged(value);
            }
        }

        /// <summary>
        /// 最大长度。默认值为 0。
        /// </summary>
        public int ItemLength { get; set; }

        /// <summary>
        /// 是否为必填项。默认值为 false。
        /// </summary>
        public bool ItemIsRequired { get; set; }

        /// <summary>
        /// 与数据的关系
        /// </summary>
        public DataRelation ItemRelation { get; set; }

        /// <summary>
        /// 默认数据，默认值为string.Empty
        /// </summary>
        public string ItemDefaultData { get; set; }

        /// <summary>
        /// 显示格式
        /// </summary>
        public string ItemFormat { get; set; }

        /// <summary>
        /// 检查类型是否匹配，长度是否超长，必填项是否已填，取值范围是否正确等等.....
        /// </summary>
        /// <returns>检查成功返回true,不成功返回false</returns>
        public string ItemCheck()
        {
            return this.OnChecked();
        }

        /// <summary>
        /// 检查完成后事件
        /// </summary>
        public event Func<string> Checked;

        /// <summary>
        /// 检查完成后执行
        /// </summary>
        /// <returns></returns>
        protected virtual string OnChecked()
        {
            if (Checked != null)
            {
                return Checked();
            }
            return "";
        }

        /// <summary>
        /// 数据改变后事件
        /// </summary>
        public event Action<string> ItemDataChanged;

        /// <summary>
        /// 数据改变后执行
        /// </summary>
        /// <param name="newData">新数据</param>
        protected virtual void OnItemDataChanged(string newData)
        {
            if (ItemDataChanged != null)
            {
                ItemDataChanged(newData);
            }
        }
    }
}
