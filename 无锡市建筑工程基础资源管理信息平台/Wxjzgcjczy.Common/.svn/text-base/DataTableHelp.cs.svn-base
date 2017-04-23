using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Wxjzgcjczy.Common
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
            //int flag = 0;
            for (int i = 0; i < dr_to.Table.Columns.Count; i++)
            {
                //flag = 0;
                for (int j = 0; j < dr_source.Table.Columns.Count; j++)
                {
                    if (dr_to.Table.Columns[i].ColumnName.ToUpper().Equals(dr_source.Table.Columns[j].ColumnName.ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                    {

                        //flag = 1;
                        if (DataFieldIsNullOrEmpty(dr_source[j]) && (
                                dr_to.Table.Columns[i].DataType == typeof(Decimal) || dr_to.Table.Columns[i].DataType == typeof(Double)
                               || dr_to.Table.Columns[i].DataType == typeof(SByte) || dr_to.Table.Columns[i].DataType == typeof(Byte)
                               || dr_to.Table.Columns[i].DataType == typeof(Int16) || dr_to.Table.Columns[i].DataType == typeof(UInt16)
                               || dr_to.Table.Columns[i].DataType == typeof(Int32) || dr_to.Table.Columns[i].DataType == typeof(UInt32)
                               || dr_to.Table.Columns[i].DataType == typeof(Int64) || dr_to.Table.Columns[i].DataType == typeof(UInt64)
                               || dr_to.Table.Columns[i].DataType == typeof(DateTime) || dr_to.Table.Columns[i].DataType == typeof(Boolean)
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
                //if (flag == 0)
                //{
                //    dr_to[i] = DBNull.Value;
                //}
            }
        }
        public static bool DataFieldIsNullOrEmpty(object obj)
        {
            return obj == null || obj == DBNull.Value || string.IsNullOrEmpty(obj.ToString());
        }
        public static void DataRow2DataRow(DataRow dr_source, DataRow dr_to, List<string> notCopyFields)
        {
            for (int i = 0; i < dr_to.Table.Columns.Count; i++)
            {
                if (notCopyFields != null && notCopyFields.Count > 0 && notCopyFields.Exists(p => p.ToUpper().Equals(dr_to.Table.Columns[i].ColumnName.ToUpper(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }
              
                for (int j = 0; j < dr_source.Table.Columns.Count; j++)
                {
                    if (dr_to.Table.Columns[i].ColumnName.ToUpper().Equals(dr_source.Table.Columns[j].ColumnName.ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (DataFieldIsNullOrEmpty(dr_source[j]) && (
                                dr_to.Table.Columns[i].DataType == typeof(Decimal) || dr_to.Table.Columns[i].DataType == typeof(Double)
                               || dr_to.Table.Columns[i].DataType == typeof(SByte) || dr_to.Table.Columns[i].DataType == typeof(Byte)
                               || dr_to.Table.Columns[i].DataType == typeof(Int16) || dr_to.Table.Columns[i].DataType == typeof(UInt16)
                               || dr_to.Table.Columns[i].DataType == typeof(Int32) || dr_to.Table.Columns[i].DataType == typeof(UInt32)
                               || dr_to.Table.Columns[i].DataType == typeof(Int64) || dr_to.Table.Columns[i].DataType == typeof(UInt64)
                               || dr_to.Table.Columns[i].DataType == typeof(DateTime) || dr_to.Table.Columns[i].DataType == typeof(Boolean)
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
