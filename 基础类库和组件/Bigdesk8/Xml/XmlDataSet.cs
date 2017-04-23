using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Bigdesk8.Xml
{
    /// <summary>
    /// XML 与 DataSet 相互转换
    /// </summary>
    public class XmlDataSet
    {
        #region DataSet 转换成 XML

        /// <summary>
        /// 将 DataSet 转换成 XML 字符串。
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <param name="isShowXmlDeclaration">是否显示XML声明</param>
        /// <returns></returns>
        public static string ConvertToXml(DataSet dataSet, bool isShowNull, bool isShowXmlDeclaration)
        {
            XmlDocument doc = ConvertToXmlDocument(dataSet, isShowNull);
            return isShowXmlDeclaration ? doc.OuterXml : doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 将 DataSet 转换成 XML 字符串。
        /// </summary>
        /// <param name="encoding">文本编码</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <param name="isShowXmlDeclaration">是否显示XML声明</param>
        /// <returns></returns>
        public static string ConvertToXml(string encoding, DataSet dataSet, bool isShowNull, bool isShowXmlDeclaration)
        {
            XmlDocument doc = ConvertToXmlDocument("1.0", encoding, "yes", dataSet, isShowNull);
            return isShowXmlDeclaration ? doc.OuterXml : doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 将 DataTable 转换成 XML 字符串。
        /// </summary>
        /// <param name="dataTable">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <param name="isShowXmlDeclaration">是否显示XML声明</param>
        /// <returns></returns>
        public static string ConvertToXml(DataTable dataTable, bool isShowNull, bool isShowXmlDeclaration)
        {
            XmlDocument doc = ConvertToXmlDocument(dataTable, isShowNull);
            return isShowXmlDeclaration ? doc.OuterXml : doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 将 DataTable 转换成 XML 字符串。
        /// </summary>
        /// <param name="encoding">文本编码</param>
        /// <param name="dataTable">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <param name="isShowXmlDeclaration">是否显示XML声明</param>
        /// <returns></returns>
        public static string ConvertToXml(string encoding, DataTable dataTable, bool isShowNull, bool isShowXmlDeclaration)
        {
            XmlDocument doc = ConvertToXmlDocument("1.0", encoding, "yes", dataTable, isShowNull);
            return isShowXmlDeclaration ? doc.OuterXml : doc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 将 DataSet 转换成 XmlDocument
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <returns></returns>
        public static XmlDocument ConvertToXmlDocument(DataSet dataSet, bool isShowNull)
        {
            return ConvertToXmlDocument("1.0", "utf-8", "yes", dataSet, isShowNull);
        }

        /// <summary>
        /// 将 DataSet 转换成 XmlDocument
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="encoding">编码</param>
        /// <param name="standalone">是否独立文件,值为yes,no</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <returns></returns>
        public static XmlDocument ConvertToXmlDocument(string version, string encoding, string standalone, DataSet dataSet, bool isShowNull)
        {
            XmlDocument doc = new XmlDocument();

            //加入XML声明
            XmlDeclaration dec = doc.CreateXmlDeclaration(version, encoding, standalone);
            doc.AppendChild(dec);

            //加入一个根元素
            string dataSetName;
            if (string.IsNullOrEmpty(dataSet.DataSetName) || dataSet.DataSetName.Length <= 0)
                dataSetName = "NewDataSet";
            else
                dataSetName = dataSet.DataSetName;
            XmlElement root = doc.CreateElement(dataSetName);
            doc.AppendChild(root);

            //加入数据表
            ConvertToXmlNode(root, dataSet, isShowNull);

            return doc;
        }

        /// <summary>
        /// 将 DataTable 转换成 XmlDocument
        /// </summary>
        /// <param name="dataTable">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <returns></returns>
        public static XmlDocument ConvertToXmlDocument(DataTable dataTable, bool isShowNull)
        {
            return ConvertToXmlDocument("1.0", "utf-8", "yes", dataTable, isShowNull);
        }

        /// <summary>
        /// 将 DataTable 转换成 XmlDocument
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="encoding">编码</param>
        /// <param name="standalone">是否独立文件,值为yes,no</param>
        /// <param name="dataTable">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        /// <returns></returns>
        public static XmlDocument ConvertToXmlDocument(string version, string encoding, string standalone, DataTable dataTable, bool isShowNull)
        {
            XmlDocument doc = new XmlDocument();

            //加入XML声明
            XmlDeclaration dec = doc.CreateXmlDeclaration(version, encoding, standalone);
            doc.AppendChild(dec);

            //加入一个根元素
            string dataSetName;
            if (dataTable.DataSet == null || string.IsNullOrEmpty(dataTable.DataSet.DataSetName) || dataTable.DataSet.DataSetName.Length <= 0)
                dataSetName = "NewDataSet";
            else
                dataSetName = dataTable.DataSet.DataSetName;
            XmlElement root = doc.CreateElement(dataSetName);
            doc.AppendChild(root);

            //加入数据行
            ConvertToXmlNode(root, dataTable, isShowNull);

            return doc;
        }

        /// <summary>
        /// 将 DataSet 转换成 XmlNode
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        public static void ConvertToXmlNode(XmlNode parentXmlNode, DataSet dataSet, bool isShowNull)
        {
            foreach (DataTable dt in dataSet.Tables)
            {
                //加入数据行
                ConvertToXmlNode(parentXmlNode, dt, isShowNull);
            }
        }

        /// <summary>
        /// 将 DataTable 转换成 XmlNode
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="dataTable">数据集</param>
        /// <param name="isShowNull">当数据为null时，是否显示XML元素</param>
        public static void ConvertToXmlNode(XmlNode parentXmlNode, DataTable dataTable, bool isShowNull)
        {
            if (string.IsNullOrEmpty(dataTable.TableName) || dataTable.TableName.Length <= 0)
            {
                throw new Exception("DataTable.TableName 数据表名不能为空！");
            }
            string tableName = dataTable.TableName;
            foreach (DataRow dr in dataTable.Rows)
            {
                XmlElement dataRow = parentXmlNode.OwnerDocument.CreateElement(tableName);

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    if (dr[columnName] == DBNull.Value && !isShowNull) continue;

                    XmlElement dataField = parentXmlNode.OwnerDocument.CreateElement(columnName);
                    dataField.InnerText = FilterControlCharacter(dr[columnName].ToString());
                    dataRow.AppendChild(dataField);
                }

                parentXmlNode.AppendChild(dataRow);
            }
        }

        #endregion DataSet 转换成 XML

        #region XML 转换成 DataSet

        /// <summary>
        /// 将 XML 字符串转换成 DataSet
        /// </summary>
        /// <param name="xml">XML 字符串</param>
        /// <returns></returns>
        public static DataSet ConvertToDataSet(string xml)
        {
            DataSet ds = new DataSet();
            using (StringReader sr = new StringReader(xml))
            {
                ds.ReadXml(sr);
            }
            return ds;
        }

        /// <summary>
        /// 将 XmlDocument 转换成 DataSet
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <returns></returns>
        public static DataSet ConvertToDataSet(XmlDocument doc)
        {
            return ConvertToDataSet(doc.OuterXml);
        }

        /// <summary>
        /// 将 XML 字符串转换成 DataTable
        /// </summary>
        /// <param name="xml">XML 字符串</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(string xml)
        {
            DataTable dt = new DataTable();
            using (StringReader sr = new StringReader(xml))
            {
                dt.ReadXml(sr);
            }
            return dt;
        }

        /// <summary>
        /// 将 XmlDocument 转换成 DataTable
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(XmlDocument doc)
        {
            return ConvertToDataTable(doc.OuterXml);
        }

        #endregion XML 转换成 DataSet

        #region 私有方法

        /// <summary>
        /// 过滤控制字符
        /// XML标准规定的无效字节为：
        /// /*
        ///     0x00 - 0x08
        ///     0x0b - 0x0c
        ///     0x0e - 0x1f
        /// */
        /// </summary>
        /// <param name="xml">要过滤的内容</param>
        /// <returns>过滤后的内容</returns>
        private static string FilterControlCharacter(string xml)
        {
            return Regex.Replace(xml, "[\x00-\x08|\x0b-\x0c|\x0e-\x1f]", "");
        }

        #endregion 私有方法
    }
}
