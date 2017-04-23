using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Bigdesk8.Data
{
    /// <summary>
    /// 数据处理功能函数
    /// </summary>
    public static class DataUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public const string PreErrorMessage = "未通过数据检查:";

        /// <summary>
        /// 必填项错误时的提示信息
        /// </summary>
        /// <param name="itemNameCN">必填项名称</param>
        /// <returns>提示信息</returns>
        public static string GetErrorMessage_DataRequired(string itemNameCN)
        {
            return string.Format(PreErrorMessage + "“{0}”不能为空！", itemNameCN);
        }

        /// <summary>
        /// 数据长度范围错误时的提示信息
        /// </summary>
        /// <param name="itemNameCN">必填项名称</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>提示信息</returns>
        public static string GetErrorMessage_DataLengthRange(string itemNameCN, DataType dataType, int minLength, int maxLength)
        {
            string ex = "";
            switch (dataType)
            {
                case DataType.String:
                    {
                        if (minLength > 0 && maxLength > 0)
                        {
                            ex = "长度范围：" + minLength + " ～ " + maxLength;
                        }
                        else
                        {
                            if (minLength > 0)
                            {
                                ex = "最小长度：" + minLength;
                            }
                            if (maxLength > 0)
                            {
                                ex = "最大长度：" + maxLength;
                            }
                        }
                    }
                    break;
            }

            return string.Format(PreErrorMessage + "“{0}”数据长度范围错误！{1}", itemNameCN, ex);
        }

        /// <summary>
        /// 数据类型错误时的提示信息
        /// </summary>
        /// <param name="itemNameCN">必填项名称</param>
        /// <param name="dataType">数据类型</param>
        /// <returns>提示信息</returns>
        public static string GetErrorMessage_DataType(string itemNameCN, DataType dataType)
        {
            string ex = "";
            switch (dataType)
            {
                case DataType.String:
                    ex = "";
                    break;
                case DataType.Boolean:
                    ex += "请输入 true，false，0，1 四个值中的任意一个。";
                    break;
                case DataType.Date:
                    ex += "请输入日期类型，如 2010-01-01，2010-07-15，2010-12-03。";
                    break;
                case DataType.Time:
                    ex += "请输入时间类型，如 09:00，10:30，13:00，14:45。";
                    break;
                case DataType.DateTime:
                    ex += "请输入日期时间类型，如 2010-01-01 09:00:00，2010-07-15 10:30:00，2010-12-03 13:00:00，2010-12-03 14:45:00。";
                    break;
                case DataType.Decimal:
                case DataType.Double:
                    ex += "请输入数字类型（可带小数），如 1000.1，2000.55，3000.999。";
                    break;
                case DataType.Int32:
                case DataType.Int64:
                    ex += "请输入整数类型，如 10，20，30。";
                    break;
            }

            return string.Format(PreErrorMessage + "“{0}”数据类型错误！{1}", itemNameCN, ex);
        }

        /// <summary>
        /// 数据格式错误时的提示信息
        /// </summary>
        /// <param name="itemNameCN">必填项名称</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="format">数据格式</param>
        /// <returns>提示信息</returns>
        public static string GetErrorMessage_DataFormat(string itemNameCN, DataType dataType, string format)
        {
            string ex = "";
            switch (dataType)
            {
                case DataType.String:
                case DataType.Boolean:
                    break;
                case DataType.Date:
                case DataType.Time:
                case DataType.DateTime:
                    ex += "参见 DateTime 格式字符串";
                    break;
                case DataType.Int32:
                case DataType.Int64:
                case DataType.Decimal:
                case DataType.Double:
                    ex += "参见数字格式字符串";
                    break;
            }

            return string.Format(PreErrorMessage + "“{0}”数据格式错误！{1}", itemNameCN, ex);
        }

        /// <summary>
        /// 数据取值范围错误时的提示信息
        /// </summary>
        /// <param name="itemNameCN">必填项名称</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="minData">最小值</param>
        /// <param name="maxData">最大值</param>
        /// <returns>提示信息</returns>
        public static string GetErrorMessage_DataRange(string itemNameCN, DataType dataType, object minData, object maxData)
        {
            string ex = "";
            switch (dataType)
            {
                case DataType.String:
                case DataType.Boolean:
                    break;
                case DataType.Date:
                case DataType.Time:
                case DataType.DateTime:
                case DataType.Int32:
                case DataType.Int64:
                case DataType.Decimal:
                case DataType.Double:
                    {
                        if (minData.IsEmpty() || maxData.IsEmpty())
                        {
                            if (maxData.IsEmpty())
                            {
                                ex = "最小值：" + minData.TrimString();
                            }
                            if (minData.IsEmpty())
                            {
                                ex = "最大值：" + maxData.TrimString();
                            }
                        }
                        else
                        {
                            ex = "取值范围：" + minData.TrimString() + " ～ " + maxData.TrimString();
                        }
                    }
                    break;
            }

            return string.Format(PreErrorMessage + "“{0}”数据取值范围错误！{1}", itemNameCN, ex);
        }

        /// <summary>
        /// 判断集合中数据项是否已经存在
        /// </summary>
        /// <param name="dataitem">集合</param>
        /// <param name="itemname">数据项名称</param>
        /// <returns></returns>
        public static bool ExistsDataItem(this List<IDataItem> dataitem, string itemname)
        {
            return dataitem.Exists(a => a.ItemName.ToLower() == itemname.ToLower());
        }

        /// <summary>
        /// 获得匹配的数据项，存在返回数据项，不存在时返回null
        /// </summary>
        /// <param name="dataitem">集合</param>
        /// <param name="itemname">数据项名称</param>
        /// <returns></returns>
        public static IDataItem GetDataItem(this List<IDataItem> dataitem, string itemname)
        {
            int index = dataitem.FindIndex(a => a.ItemName.ToLower() == itemname.ToLower());
            return index < 0 ? null : dataitem[index];
        }

        /// <summary>
        /// 搜索所有的名称(itemname)--区分大小写--符合正则表达式要求的dataitem
        /// </summary>
        /// <param name="dataItemList">集合</param>
        /// <param name="condition">搜索条件</param>
        /// <returns></returns>
        public static List<IDataItem> FindAllByItemName(this List<IDataItem> dataItemList, Regex condition)
        {
            return dataItemList.FindAll(a => condition.IsMatch(a.ItemName));
        }

        /// <summary>
        /// 根据数据类型进行相应的数据转换.如果转换失败，返回值为false;如果成功,true
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="datatype">数据类型</param>
        /// <returns>如果转换失败，返回值为false;如果成功,true</returns>
        public static bool CheckDataType(object data, DataType datatype)
        {
            string data2 = data.TrimString();
            if (data2 == string.Empty) return true;
            try
            {
                switch (datatype)
                {
                    case DataType.String:
                        return true;
                    case DataType.Boolean:
                        {
                            Boolean i = data2.ToBoolean();
                            return true;
                        }
                    case DataType.Int32:
                        {
                            Int32 i = data2.ToInt32();
                            return true;
                        }
                    case DataType.Int64:
                        {
                            Int64 i = data2.ToInt64();
                            return true;
                        }
                    case DataType.Double:
                        {
                            Double i = data2.ToDouble();
                            return true;
                        }
                    case DataType.Decimal:
                        {
                            Decimal i = data2.ToDecimal();
                            return true;
                        }
                    case DataType.Date:
                        {
                            return data2.IsDateTime();
                        }
                    case DataType.Time:
                        {
                            return data2.IsTime();
                        }
                    case DataType.DateTime:
                        {
                            return data2.IsDateTime();
                        }
                }
                return false;
            }
            catch { return false; }

        }

        /// <summary>
        /// 根据数据格式进行相应的格式化.如果格式化失败，返回值为string.Empty;如果成功,返回格式化的结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="datatype">数据类型</param>
        /// <param name="format">格式</param>
        /// <returns>如果格式化失败，返回值为string.Empty;如果成功,返回格式化的结果</returns>
        public static string FormatData(object data, DataType datatype, string format)
        {
            string data2 = data.TrimString();
            if (data2 == string.Empty) return string.Empty;

            try
            {
                if (format.IsEmpty())
                {
                    switch (datatype)
                    {
                        case DataType.Date:
                            return data2.ToDate2();
                        case DataType.Time:
                            return data2.ToTime2();
                        case DataType.DateTime:
                            return data2.ToDateTime2();
                    }
                    return data2;
                }
                string text = string.Empty;
                switch (datatype)
                {
                    case DataType.String:
                    case DataType.Boolean:
                        text = data2;
                        break;
                    case DataType.Int32:
                        {
                            text = data2.ToInt32().ToString(format);
                        }
                        break;
                    case DataType.Int64:
                        {
                            text = data2.ToInt64().ToString(format);
                        }
                        break;
                    case DataType.Double:
                        {
                            text = data2.ToDouble().ToString(format);
                        }
                        break;
                    case DataType.Decimal:
                        {
                            text = data2.ToDecimal().ToString(format);
                        }
                        break;
                    case DataType.Date:
                        {
                            text = data2.ToDate().ToString(format);
                        }
                        break;
                    case DataType.Time:
                        {
                            text = data2.ToTime().ToString(format);
                        }
                        break;
                    case DataType.DateTime:
                        {
                            text = data2.ToDateTime().ToString(format);
                        }
                        break;
                }

                return text;
            }
            catch { return ""; }

        }

        /// <summary>
        /// 检查数据取值范围
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="datatype">数据类型</param>
        /// <param name="minData">最小值</param>
        /// <param name="maxData">最大值</param>
        /// <returns>数据在取值范围内，返回true</returns>
        public static bool CheckDataRange(object data, DataType datatype, object minData, object maxData)
        {
            string data2 = data.TrimString();
            if (data2 == string.Empty) return true;
            bool minIsEmpty = minData.IsEmpty();
            bool maxIsEmpty = maxData.IsEmpty();
            if (minIsEmpty && maxIsEmpty) return true;

            switch (datatype)
            {
                case DataType.String:
                case DataType.Boolean:
                    return true;
                case DataType.Int32:
                    {
                        Int32 a = data2.ToInt32();
                        if (!minIsEmpty && a < minData.ToInt32()) return false;
                        if (!maxIsEmpty && a > maxData.ToInt32()) return false;
                    }
                    break;
                case DataType.Int64:
                    {
                        Int64 a = data2.ToInt64();
                        if (!minIsEmpty && a < minData.ToInt64()) return false;
                        if (!maxIsEmpty && a > maxData.ToInt64()) return false;
                    }
                    break;
                case DataType.Double:
                    {
                        Double a = data2.ToDouble();
                        if (!minIsEmpty && a < minData.ToDouble()) return false;
                        if (!maxIsEmpty && a > maxData.ToDouble()) return false;
                    }
                    break;
                case DataType.Decimal:
                    {
                        Decimal a = data2.ToDecimal();
                        if (!minIsEmpty && a < minData.ToDecimal()) return false;
                        if (!maxIsEmpty && a > maxData.ToDecimal()) return false;
                    }
                    break;
                case DataType.Date:
                    {
                        DateTime a = data2.ToDate();
                        if (!minIsEmpty && a < minData.ToDate()) return false;
                        if (!maxIsEmpty && a > maxData.ToDate()) return false;
                    }
                    break;
                case DataType.Time:
                    {
                        DateTime a = data2.ToTime();
                        if (!minIsEmpty && a < minData.ToTime()) return false;
                        if (!maxIsEmpty && a > maxData.ToTime()) return false;
                    }
                    break;
                case DataType.DateTime:
                    {
                        DateTime a = data2.ToDateTime();
                        if (!minIsEmpty && a < minData.ToDateTime()) return false;
                        if (!maxIsEmpty && a > maxData.ToDateTime()) return false;
                    }
                    break;
            }
            return true;
        }


        /// <summary>
        /// 将 DataRow 转换成 IDataItem
        /// </summary>
        /// <param name="datarow"></param>
        /// <returns></returns>
        public static List<IDataItem> ToDataItem(this DataRow datarow)
        {
            List<IDataItem> list = new List<IDataItem>();
            if (datarow == null) return list;
            foreach (DataColumn dc in datarow.Table.Columns)
            {
                IDataItem di = new DataItem();
                di.ItemName = dc.ColumnName;
                di.ItemData = datarow[dc.ColumnName].TrimString();
                list.Add(di);
            }
            return list;
        }

        /// <summary>
        /// 将 DataRow 转换成 DataItem 集合
        /// </summary>
        /// <param name="datarow"></param>
        /// <returns></returns>
        public static List<DataItem> DataRowToDataItem(this DataRow datarow)
        {
            List<DataItem> list = new List<DataItem>();
            if (datarow == null) return list;
            foreach (DataColumn dc in datarow.Table.Columns)
            {
                DataItem di = new DataItem();
                di.ItemName = dc.ColumnName;
                di.ItemData = datarow[dc.ColumnName].TrimString();
                list.Add(di);
            }
            return list;
        }

        /// <summary>
        /// 将 IDataItem 转换成 DataRow
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="datarow"></param>
        public static void ToDataRow(this List<IDataItem> dataitem, DataRow datarow)
        {
            if (dataitem == null || datarow == null) return;

            foreach (IDataItem di in dataitem)
            {
                if (!datarow.Table.Columns.Contains(di.ItemName)) continue;

                if (di.ItemData.IsEmpty())
                {
                    datarow[di.ItemName] = DBNull.Value;
                }
                else
                {
                    datarow[di.ItemName] = di.ItemData;
                }
            }
        }

        /// <summary>
        /// 将 DataItem 集合 转换成 DataRow
        /// </summary>
        /// <param name="listDataItem"></param>
        /// <param name="datarow"></param>
        public static void ToDataRow(this List<DataItem> listDataItem, DataRow datarow)
        {
            if (listDataItem == null || datarow == null) return;

            foreach (DataItem di in listDataItem)
            {
                if (!datarow.Table.Columns.Contains(di.ItemName)) continue;

                if (di.ItemData.IsEmpty())
                {
                    datarow[di.ItemName] = DBNull.Value;
                }
                else
                {
                    datarow[di.ItemName] = di.ItemData;
                }
            }
        }

        /// <summary>
        /// 将 IDataItem 转换成 DataRow
        /// </summary>
        /// <param name="dataitem"></param>
        /// <param name="datarow"></param>
        /// <param name="isEmptyItemToNullColumn">是否将ItemData=""转化为相应的Null字段</param>
        public static void ToDataRow(this List<IDataItem> dataitem, DataRow datarow, bool isEmptyItemToNullColumn)
        {
            if (dataitem == null || datarow == null) return;

            foreach (IDataItem di in dataitem)
            {
                if (!datarow.Table.Columns.Contains(di.ItemName)) continue;

                if (isEmptyItemToNullColumn && di.ItemData.IsEmpty())
                {
                    datarow[di.ItemName] = DBNull.Value;
                }
                else
                {
                    datarow[di.ItemName] = di.ItemData;
                }
            }
        }

        /// <summary>
        /// 将 Object 转换成 IDataItem，注意：只读写公共属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static List<IDataItem> ToDataItem<T>(this T obj)
        {
            List<IDataItem> list = new List<IDataItem>();
            Type sourceType = typeof(T);
            PropertyInfo[] pisSource = sourceType.GetProperties();
            foreach (PropertyInfo piSource in pisSource)
            {
                if (!piSource.CanRead) continue;

                DataItem di = new DataItem();
                di.ItemName = piSource.Name;
                di.ItemData = piSource.GetValue(obj, null).ToString2();
                list.Add(di);
            }
            return list;
        }

        /// <summary>
        /// 将 IDataItem 转换成 Object，注意：只读写公共属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="dataitem">数据集合</param>
        /// <param name="obj">对象</param>
        public static void ToObject<T>(this List<IDataItem> dataitem, T obj)
        {
            Type destinationType = typeof(T);
            PropertyInfo[] pisDestination = destinationType.GetProperties();

            IDataItem di;
            foreach (PropertyInfo piDest in pisDestination)
            {
                if (!piDest.CanWrite) continue;

                di = dataitem.GetDataItem(piDest.Name);
                if (di != null)
                {
                    piDest.SetValue(obj, ConvertObjectType(di.ItemData, piDest.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// 将 Object 转换成 DataRow，注意：只读写公共属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="datarow">数据行</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static void ObjectToDataRow<T>(T obj, DataRow datarow)
        {
            Type sourceType = typeof(T);
            PropertyInfo[] pisSource = sourceType.GetProperties();
            foreach (PropertyInfo piSource in pisSource)
            {
                if (!piSource.CanRead) continue;

                if (!datarow.Table.Columns.Contains(piSource.Name)) continue;

                datarow[piSource.Name] = piSource.GetValue(obj, null) == null ? DBNull.Value : piSource.GetValue(obj, null);
            }
        }

        /// <summary>
        /// 将 DataRow 转换成 Object，注意：只读写公共属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="datarow">数据行</param>
        /// <param name="obj">对象</param>
        public static void DataRowToObject<T>(DataRow datarow, T obj)
        {
            Type destinationType = typeof(T);
            PropertyInfo[] pisDestination = destinationType.GetProperties();

            foreach (PropertyInfo piDest in pisDestination)
            {
                if (!piDest.CanWrite) continue;

                if (!datarow.Table.Columns.Contains(piDest.Name)) continue;

                piDest.SetValue(obj, ConvertObjectType(datarow[piDest.Name], piDest.PropertyType), null);
            }
        }

        private static object ConvertObjectType(object obj, Type destType)
        {
            //if (!destType.IsValueType && destType != typeof(DateTime)) return obj;

            //string
            if (destType.Equals(typeof(string)))
            {
                if (obj.IsEmpty())
                    return "";
                else
                    return obj.ToString2();
            }

            if (destType.Equals(typeof(System.String)))
            {
                if (obj.IsEmpty())
                    return "";
                else
                    return obj.ToString2();
            }

            //int
            if (destType.Equals(typeof(int)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToInt32(obj);
            }

            //double
            if (destType.Equals(typeof(double)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToDouble(obj);
            }

            //decimal
            if (destType.Equals(typeof(decimal)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToDecimal(obj);
            }

            //bool
            if (destType.Equals(typeof(bool)))
            {
                if (obj.IsEmpty())
                    return false;
                else
                    return obj.ToBoolean();
            }

            //DateTime
            if (destType.Equals(typeof(DateTime)))
            {
                if (obj.IsEmpty())
                    return DateTime.MinValue;
                else
                    return obj.ToDateTime();
            }

            //char
            if (destType.Equals(typeof(char)))
            {
                if (obj.IsEmpty())
                    return '\0';
                else
                    return Convert.ToChar(obj);
            }

            //long
            if (destType.Equals(typeof(long)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToInt64(obj);
            }

            //float
            if (destType.Equals(typeof(float)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToSingle(obj);
            }

            //byte
            if (destType.Equals(typeof(byte)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToByte(obj);
            }

            //sbyte
            if (destType.Equals(typeof(sbyte)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return sbyte.Parse(obj.ToString());
            }

            //uint
            if (destType.Equals(typeof(uint)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToUInt32(obj);
            }

            //ulong
            if (destType.Equals(typeof(ulong)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToUInt64(obj);
            }

            //ushort
            if (destType.Equals(typeof(ushort)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToUInt16(obj);
            }

            //short
            if (destType.Equals(typeof(short)))
            {
                if (obj.IsEmpty())
                    return 0;
                else
                    return Convert.ToInt16(obj);
            }

            //....
            return obj;
        }

        /// <summary>
        /// 从 from 拷贝 到 to 中，规则：列名相同
        /// </summary>
        /// <param name="from">从 <see cref="DataRow"/></param>
        /// <param name="to">拷贝到 <see cref="DataRow"/></param>
        public static void ToDataRow(this DataRow from, DataRow to)
        {
            if (from == null || to == null) return;

            foreach (DataColumn dc in to.Table.Columns)
            {
                if (!from.Table.Columns.Contains(dc.ColumnName)) continue;

                to[dc] = from[dc.ColumnName];
            }
        }

        /// <summary>
        /// 将 IDataItem 转换成 查询条件 SQL + SqlParameterCollection
        /// </summary>
        /// <param name="dataitem">数据项集合</param>
        /// <param name="spc">返回 SQL 参数的集合</param>
        /// <param name="searchSQL">返回查询条件 SQL 语句</param>
        public static void GetSearchClause(this List<IDataItem> dataitem, SqlParameterCollection spc, ref string searchSQL)
        {
            GetSearchClause(dataitem, spc, ref searchSQL, true, true);
        }

        public static void GetSearchClause(this List<IDataItem> dataitem, DataBaseType dbType, SqlParameterCollection spc, ref string searchSQL)
        {
            GetSearchClause(dataitem, dbType, spc, ref searchSQL, true, true);
        }

        /// <summary>
        /// 将 IDataItem 转换成 查询条件 SQL + SqlParameterCollection
        /// </summary>
        /// <param name="dataitem">数据项集合</param>
        /// <param name="spc">返回 SQL 参数的集合</param>
        /// <param name="searchSQL">返回查询条件 SQL 语句</param>
        /// <param name="isAndRelation">dataitem与dataitem之间是否是And关系.true表示And关系,false表示Or关系.</param>
        /// <param name="isAndRelation2">dataitem集合与其它集合之间是否是And关系.true表示And关系,false表示Or关系.</param>
        /// <returns></returns>
        public static void GetSearchClause(this List<IDataItem> dataitem, SqlParameterCollection spc, ref string searchSQL, bool isAndRelation, bool isAndRelation2)
        {
            GetSearchClause(dataitem, DataBaseType.SQLSERVER2008, spc, ref searchSQL, isAndRelation, isAndRelation2);
        }

        public static void GetSearchClause(this List<IDataItem> dataitem, DataBaseType dbType, SqlParameterCollection spc, ref string searchSQL, bool isAndRelation, bool isAndRelation2)
        {
            if (dataitem == null) return;
            if (spc == null) throw new Exception("参数：spc 不能为 null！");

            string sql = "";
            foreach (IDataItem di in dataitem)
            {
                if (di.ItemName.IsEmpty() || di.ItemData.IsEmpty()) continue;

                /* 动态查询条件中的字段名往往也是前端输入的，特别是当查询条件拼凑成一个json串传到后台（例如：webservice）函数中的时候。
                 * 故需要对字段名的合法性进行基本的校验。2016-5-31 缪卫华
                 */ 
                if (di.ItemName.IndexOfAny(new char[]{' ',';'}) >= 0 ||
                    di.ItemName.IndexOf("char(",StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    throw new Exception("非法的字段名:" + di.ItemName);
                }

                string data = di.ItemData;
                if (di.ItemType == DataType.Date)
                {
                    switch (di.ItemRelation)
                    {
                        default:
                            data = data.ToDate2();
                            break;
                        case DataRelation.LessThan:
                        case DataRelation.LessThanOrEqual:
                            data = data.ToDate().ToString("yyyy-MM-dd 23:59:59");
                            break;
                    }
                }

                string parameterName;
                switch (dbType)
                {
                    case DataBaseType.ORACLE11G:
                        parameterName = ":p" + (spc.Count + 1);
                        break;
                    default:
                        parameterName = "@p" + (spc.Count + 1);
                        break;
                }
                string temp = "";
                switch (di.ItemRelation)
                {
                    case DataRelation.Equal:
                        {
                            temp = di.ItemName + " = " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.NotEqual:
                        {
                            temp = di.ItemName + " <> " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.GreaterThan:
                        {
                            temp = di.ItemName + " > " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.GreaterThanOrEqual:
                        {
                            temp = di.ItemName + " >= " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.LessThan:
                        {
                            temp = di.ItemName + " < " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.LessThanOrEqual:
                        {
                            temp = di.ItemName + " <= " + parameterName;
                            spc.Add(parameterName, data);
                        }
                        break;
                    case DataRelation.Like:
                        {
                            temp = di.ItemName + " like " + parameterName;
                            spc.Add(parameterName, "%" + data + "%");
                        }
                        break;
                    case DataRelation.LeftLike:
                        {
                            temp = di.ItemName + " like " + parameterName;
                            spc.Add(parameterName, "%" + data);
                        }
                        break;
                    case DataRelation.RightLike:
                        {
                            temp = di.ItemName + " like " + parameterName;
                            spc.Add(parameterName, data + "%");
                        }
                        break;
                }

                if (temp == "") continue;

                sql = (sql == "") ? temp : (isAndRelation ? sql + " and " + temp : sql + " or " + temp);
            }

            searchSQL += (sql == "") ? string.Empty : (isAndRelation2 ? " and (" + sql + ")" : " or (" + sql + ")");
        }

        /// <summary> 
        /// Converts a Generic List into a DataTable 
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param> 
        /// <returns></returns> 
        public static DataTable GetDataTable<T>(IList<T> list)
        {
            DataTable dt = new DataTable();
            // Get a list of all the properties on the object  
            Type typ = typeof(T);
            PropertyInfo[] pi = typ.GetProperties();
            // Loop through each property, and add it as a column to the datatable    
            foreach (PropertyInfo p in pi)
            {
                // The the type of the property       
                Type columnType = p.PropertyType;
                // We need to check whether the property is NULLABLE        
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"      
                    columnType = p.PropertyType.GetGenericArguments()[0];
                }
                // Add the column definition to the datatable.        
                dt.Columns.Add(new DataColumn(p.Name, columnType));
            }
            // For each object in the list, loop through and add the data to the datatable.   
            foreach (T obj in list)
            {
                object[] row = new object[pi.Length];
                int i = 0;
                foreach (PropertyInfo p in pi)
                {
                    row[i++] = p.GetValue(obj, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
