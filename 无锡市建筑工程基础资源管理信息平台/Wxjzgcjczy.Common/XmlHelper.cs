using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Wxjzgcjczy.Common
{
    /// <summary>
    /// XML解析帮助类
    /// </summary>
    public class XmlHelper
    {
        private Base64EncodeHelper base64EncodeHelper;
        public XmlHelper()
        {
            base64EncodeHelper = new Base64EncodeHelper();
        }
        public XmlHelper(Encoding encode)
        {
            base64EncodeHelper = new Base64EncodeHelper(encode);
        }

        /// <summary>
        /// 判断字段是否为空值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool DataFieldIsNullOrEmpty(object obj)
        {
            return obj == null || obj == DBNull.Value || string.IsNullOrEmpty(obj.ToString().Trim());
        }

        public string EncodeString(string str_source)
        {
            if(string.IsNullOrEmpty(str_source))
                return string.Empty;
            return base64EncodeHelper.Base64Encode(str_source);
        }





        /// <summary>
        /// 将传入的xml格式数据转换成DataTable
        /// </summary>
        /// <param name="xmlData">xml格式数据</param>
        /// <returns>DataTable数据集</returns>
        public DataTable ConvertXMLToDataTable(string xmlData, out string message)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            message = String.Empty;
            try
            {
                DataSet ds = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    message = "没有数据！";
                    return null;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (stream != null)
                    stream.Close();
            }
        }
        /// <summary>
        /// 将传入的经Base64编码的xml格式数据转换成DataTable并解码成明文
        /// </summary>
        /// <param name="xmlData">经Base64编码的xml格式数据</param>
        /// <returns>DataTable数据集</returns>
        public DataTable ConvertXMLToDataTableWithBase64Decoding(string xmlData, out string message)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            message = String.Empty;
            try
            {
                //if (!this.IsXml(xmlData))
                //{
                //    message = "不是标准的XML格式！";
                //    return  null;
                //}
                DataSet ds = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (!DataFieldIsNullOrEmpty(dt.Rows[i][j]))
                                dt.Rows[i][j] = base64EncodeHelper.Base64Decode(dt.Rows[i][j].ToString());
                        }

                    }
                    return dt;
                }
                else
                {
                    message = "没有数据！";
                    return null;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// 将DataTable数据集里的数据转换成XML格式返回
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="rootName">XML数据的根节点元素</param>
        /// <param name="rowElementName">根元素的子节点元素</param>
        /// <returns>XML格式字符串</returns>
        public string ConvertDataTableToXML(DataTable dt, string rootName, string rowElementName)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return String.Empty;
            }

            try
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("<{0}>", rootName);
                foreach (DataRow row in dt.Rows)
                {
                    str.AppendFormat("<{0}>", rowElementName);
                    foreach (DataColumn col in dt.Columns)
                    {
                        str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (row[col.ColumnName] == DBNull.Value) ? String.Empty : row[col.ColumnName].ToString(), col.ColumnName);
                    }
                    str.AppendFormat("</{0}>", rowElementName);
                }
                str.AppendFormat("</{0}>", rootName);
                return str.ToString();
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// 将DataTable数据集里的数据进行编码，并转换成XML格式返回
        /// </summary>
        /// <param name="dt">DataTable数据集</param>
        /// <param name="rootName">XML数据的根节点元素</param>
        /// <param name="rowElementName">根元素的子节点元素</param>
        /// <returns>XML格式字符串</returns>
        public string ConvertDataTableToXMLWithBase64Encoding(DataTable dt, string rootName, string rowElementName)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();

            try
            {
                str.AppendFormat("<{0}>", rootName);
                foreach (DataRow row in dt.Rows)
                {
                    str.AppendFormat("<{0}>", rowElementName);
                    foreach (DataColumn col in dt.Columns)
                    {
                        str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (row[col.ColumnName] == DBNull.Value) ? String.Empty : base64EncodeHelper.Base64Encode(row[col.ColumnName].ToString()), col.ColumnName);
                    }
                    str.AppendFormat("</{0}>", rowElementName);
                }
                str.AppendFormat("</{0}>", rootName);
                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将一个DataRow里的数据集转化成XML格式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string ConvertDataRowToXMLWithBase64Encoding(DataRow dr)
        {
            if (dr == null || dr.Table == null)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();
            try
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : base64EncodeHelper.Base64Encode(dr[col.ColumnName].ToString()), col.ColumnName);
                }

                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将一个DataRow里的数据集里，字段名在includeColumns中的字段转化成XML格式 , 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="includeColumns"></param>
        /// <returns></returns>
        public string ConvertDataRowToXMLWithBase64EncodingIncludeForAddPrj(DataRow dr, string[] includeColumns)
        {
            if (dr == null || dr.Table == null)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();
            str.Append("<data><result>");
            try
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    foreach (string includeCol in includeColumns)
                    {
                        if (string.Equals(includeCol, col.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            //暂时不需要base64编码，以后需要编码时再做更改
                            //str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : base64EncodeHelper.Base64Encode(dr[col.ColumnName].ToString()), col.ColumnName);
                            str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : dr[col.ColumnName].ToString(), col.ColumnName);
                        }
                    }

                }
                str.Append("</result></data>");
                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将一个DataRow里的数据集里，字段名在includeColumns中的字段转化成XML格式 , 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="includeColumns"></param>
        /// <returns></returns>
        public string ConvertDataRowToXMLWithBase64EncodingInclude(DataRow dr, string[] includeColumns)
        {
            if (dr == null || dr.Table == null)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();
            try
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    foreach (string includeCol in includeColumns)
                    {
                        if(string.Equals(includeCol , col.ColumnName , StringComparison.OrdinalIgnoreCase))
                        {
                            str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : base64EncodeHelper.Base64Encode(dr[col.ColumnName].ToString()), col.ColumnName);
                        }
                    }
                    
                }

                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 将一个DataRow里的数据集里，字段名不在excludeColumns中的所有字段转化成XML格式 , 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="includeColumns"></param>
        /// <returns></returns>
        public string ConvertDataRowToXMLWithBase64EncodingExclude(DataRow dr, string[] excludeColumns)
        {
            if (dr == null || dr.Table == null)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();
            try
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    foreach (string excludeCol in excludeColumns)
                    {
                        if (!string.Equals(excludeCol, col.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : base64EncodeHelper.Base64Encode(dr[col.ColumnName].ToString()), col.ColumnName);
                        }
                    }

                }

                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

        public string ConvertDataRowToXML(DataRow dr)
        {
            if (dr == null || dr.Table == null)
            {
                return String.Empty;
            }
            StringBuilder str = new StringBuilder();
            try
            {
                foreach (DataColumn col in dr.Table.Columns)
                {
                    str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty : dr[col.ColumnName].ToString(), col.ColumnName);
                }

                return str.ToString();

            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 反序列化XML为类实例  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xmlObj"></param>  
        /// <returns></returns>  
        public  T DeserializeXML<T>(string xmlObj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlObj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>  
        /// 序列化类实例为XML  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public  string SerializeXML<T>(T obj)
        {
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }


        public bool IsXml(string xmlData)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlData);//判断是否加载成功
                return true;
                //string path = @"D:\公司项目\无锡市建筑工程基础资源管理信息平台\Wxjzgcjczy.Web\xmlSchema\TBProjectInfo.xml";
                //string error = "";
                ////声明XmlSchema 
                //XmlSchemaSet schemas = new XmlSchemaSet();
                //schemas.Add("", XmlReader.Create(path));
                ////声明事件处理方法 
                //ValidationEventHandler eventHandler = new ValidationEventHandler(delegate(object sender, ValidationEventArgs e)
                //{
                //    switch (e.Severity)
                //    {
                //        case XmlSeverityType.Error:
                //            error += e.Message;
                //            break;
                //        case XmlSeverityType.Warning:
                //            break;
                //    }
                //});
                //xml.Schemas = schemas;
                ////验证xml 
                //xml.Validate(eventHandler);

                //if (string.IsNullOrEmpty(error))
                //    //xml.GetElementsByTagName();
                //    return true;//是xml文件，返回
                //else
                //    return false;
            }
            catch
            {
                return false;//不是xml文件，返回

            }
        }

    }
}
