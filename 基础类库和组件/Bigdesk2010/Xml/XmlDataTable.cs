using System;
using System.Data;
using System.Xml;

namespace Bigdesk2010.Xml
{
    /// <summary>
    /// XML 和 DataTable 互相转换
    /// </summary>
    public class XmlDataTable
    {
        #region XML 和 DataTable 互相转换

        /// <summary>
        /// <see cref="DataTable"/> 转换为 XML 字符串，XML 字符串格式：
        /// <![CDATA[
        /// <xmlroot>
        ///     <datarow>
        ///         <列名1>列值1</列名1>
        ///         <列名2>列值2</列名2>
        ///     </datarow>
        ///     <datarow>
        ///         <列名1>列值3</列名1>
        ///         <列名2>列值4</列名2>
        ///     </datarow>
        /// </xmlroot>
        /// ]]>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertToXml(DataTable dt)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<xmlroot></xmlroot>");
            foreach (DataRow dr in dt.Rows)
            {
                AppendChildXmlElement(doc, dr);
            }
            return doc.InnerXml;
        }

        /// <summary>
        /// <see cref="DataRow"/> 转换为 XML 字符串，XML 字符串格式：
        /// <![CDATA[
        /// <xmlroot>
        ///     <datarow>
        ///         <列名1>列值1</列名1>
        ///         <列名2>列值2</列名2>
        ///     </datarow>
        /// </xmlroot>
        /// ]]>
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string ConvertToXml(DataRow dr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<xmlroot></xmlroot>");

            AppendChildXmlElement(doc, dr);

            return doc.InnerXml;
        }

        private static void AppendChildXmlElement(XmlDocument doc, DataRow dr)
        {
            XmlElement datarow = doc.CreateElement("datarow");
            doc.DocumentElement.AppendChild(datarow);

            foreach (DataColumn dc in dr.Table.Columns)
            {
                XmlElement datacolumn = doc.CreateElement(dc.ColumnName);
                datacolumn.InnerText = dr[dc].ToString();
                datarow.AppendChild(datacolumn);
            }
        }

        /// <summary>
        /// XML 字符串转换为 <see cref="DataTable"/>，XML 字符串格式：
        /// <![CDATA[
        /// <xmlroot>
        ///     <datarow>
        ///         <列名1>列值1</列名1>
        ///         <列名2>列值2</列名2>
        ///     </datarow>
        ///     <datarow>
        ///         <列名1>列值3</列名1>
        ///         <列名2>列值4</列名2>
        ///     </datarow>
        /// </xmlroot>
        /// ]]>
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(string xml)
        {
            DataTable dt = new DataTable("t");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList xnl = doc.SelectNodes(@"xmlroot/datarow");
            if (xnl.Count <= 0) return dt;

            foreach (XmlNode xn in xnl[0].ChildNodes)
            {
                dt.Columns.Add(xn.Name);
            }

            foreach (XmlNode dr in xnl)
            {
                DataRow datarow = dt.NewRow();
                dt.Rows.Add(datarow);

                foreach (XmlNode dc in dr.ChildNodes)
                {
                    datarow[dc.Name] = dc.InnerText;
                }
            }

            return dt;
        }

        #endregion XML 和 DataTable 互相转换
    }
}
