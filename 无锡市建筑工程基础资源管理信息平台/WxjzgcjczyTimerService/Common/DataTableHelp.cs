using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WxsjzxTimerService.Common
{
    public class DataTableHelp
    {
        private DataTable dt;
        public DataTableHelp()
        {
            dt = new DataTable();
        }
        public static void AddColumn(DataTable dt, string[] columnNames)
        {
            foreach (string item in columnNames)
            {
                dt.Columns.Add(new DataColumn(item));
            }
        }
        public static void AddRow(DataTable dt, string[] rowData)
        {
            DataRow row = dt.NewRow();
            int i = 0;
            foreach (string item in rowData)
            {
                row[i++] = item;
            }
            dt.Rows.Add(row);
        }

        public DataTable AddColumn(string[] columnNames)
        {
            dt.Columns.Clear();
            foreach (string item in columnNames)
            {
                dt.Columns.Add(new DataColumn(item));
            }
            return dt;
        }
        public DataTable AddRow(string[] rowData)
        {
            DataRow row = dt.NewRow();
            int i = 0;
            foreach (string item in rowData)
            {
                if (i < dt.Columns.Count)
                    row[i++] = item;
            }
            dt.Rows.Add(row);
            return dt;
        }

        public static void DataRow2DataRow(DataRow dr_source, DataRow dr_to)
        {
            int flag = 0;
            for (int i = 0; i < dr_to.Table.Columns.Count; i++)
            {
                flag = 0;
                for (int j = 0; j < dr_source.Table.Columns.Count; j++)
                {
                    if (dr_to.Table.Columns[i].ColumnName.ToUpper().Equals(dr_source.Table.Columns[j].ColumnName.ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag = 1;
                        if (DataFieldIsNullOrEmpty(dr_source[j]) && (
                               dr_to.Table.Columns[i].DataType == typeof(double) || dr_to.Table.Columns[i].DataType == typeof(Double)
                               || dr_to.Table.Columns[i].DataType == typeof(SByte) || dr_to.Table.Columns[i].DataType == typeof(Byte)
                               || dr_to.Table.Columns[i].DataType == typeof(Int16) || dr_to.Table.Columns[i].DataType == typeof(UInt16)
                               || dr_to.Table.Columns[i].DataType == typeof(Int32) || dr_to.Table.Columns[i].DataType == typeof(UInt32)
                               || dr_to.Table.Columns[i].DataType == typeof(Int64) || dr_to.Table.Columns[i].DataType == typeof(UInt64)
                               || dr_to.Table.Columns[i].DataType == typeof(DateTime) || dr_to.Table.Columns[i].DataType == typeof(Boolean)
                               || dr_to.Table.Columns[i].DataType == typeof(Decimal) || dr_to.Table.Columns[i].DataType == typeof(decimal)
                               || dr_to.Table.Columns[i].DataType == typeof(Single) || dr_to.Table.Columns[i].DataType == typeof(float)

                               )
                           )
                        {
                            dr_to[i] = DBNull.Value;
                            break;
                        }
                        else
                        {
                            dr_to[i] = dr_source[j];
                            break;

                        }



                    }
                }
                if (flag == 0)
                {
                    dr_to[i] = DBNull.Value;
                }
            }
        }

        private static bool DataFieldIsNullOrEmpty(object obj)
        {
            return obj == null || obj == DBNull.Value || string.IsNullOrEmpty(obj.ToString());
        }

        /// <summary>
        /// 检查数据行里字段是否存在和集合里面的值相等
        /// </summary>
        /// <param name="invalidateValues">检查字段可能的值</param>
        /// <param name="fields">待检查的字段</param>
        /// <param name="row">数据行</param>
        /// <param name="msg">输出信息，包含了不合法的字段信息</param>
        /// <returns></returns>
        public static bool DataFieldIsNullOrEmpty(List<string> invalidateValues, string[] fields, DataRow row, out string msg)
        {

            msg = String.Empty;
            bool hasEmptyField = false;
            if (invalidateValues == null || invalidateValues.Count == 0 ||
                row == null || fields == null || fields.Length == 0) return false;
            foreach (string fieldName in fields)
            {
                if (invalidateValues.Exists(p => (row[fieldName] == DBNull.Value || p.Equals(row[fieldName].ToString().Trim()))))
                {
                    hasEmptyField = true;
                    msg += fieldName + "、";
                }
            }
            if (!string.IsNullOrEmpty(msg))
                msg = msg.TrimEnd('、');

            return hasEmptyField;
        }

        public static void DataRow2DataRow(DataRow dr_source, DataRow dr_to, List<string> notCopyFields)
        {
            for (int i = 0; i < dr_to.Table.Columns.Count; i++)
            {
                if (notCopyFields != null && notCopyFields.Count > 0 && notCopyFields.Exists(p => p.ToUpper().Equals(dr_to.Table.Columns[i].ColumnName.ToUpper())))
                {
                    continue;
                }
     
                for (int j = 0; j < dr_source.Table.Columns.Count; j++)
                {
                    if (dr_to.Table.Columns[i].ColumnName.ToUpper().Equals(dr_source.Table.Columns[j].ColumnName.ToUpper()))
                    {
                        if (DataFieldIsNullOrEmpty(dr_source[j]) && (
                                dr_to.Table.Columns[i].DataType == typeof(Double) || dr_to.Table.Columns[i].DataType == typeof(double)
                               || dr_to.Table.Columns[i].DataType == typeof(SByte) || dr_to.Table.Columns[i].DataType == typeof(Byte)
                               || dr_to.Table.Columns[i].DataType == typeof(Int16) || dr_to.Table.Columns[i].DataType == typeof(UInt16)
                               || dr_to.Table.Columns[i].DataType == typeof(Int32) || dr_to.Table.Columns[i].DataType == typeof(UInt32)
                               || dr_to.Table.Columns[i].DataType == typeof(Int64) || dr_to.Table.Columns[i].DataType == typeof(UInt64)
                               || dr_to.Table.Columns[i].DataType == typeof(DateTime) || dr_to.Table.Columns[i].DataType == typeof(Boolean)
                               || dr_to.Table.Columns[i].DataType == typeof(Decimal) || dr_to.Table.Columns[i].DataType == typeof(decimal)
                               || dr_to.Table.Columns[i].DataType == typeof(Single) || dr_to.Table.Columns[i].DataType == typeof(float)
                               )
                           )
                        {
                            dr_to[i] = DBNull.Value;
                            break;
                        }
                        else
                        {
                            dr_to[i] = dr_source[j];
                            break;

                        }
                    }
                }
            }
        }
    }
}
