using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WxsjzxTimerService.Common
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
            if (string.IsNullOrEmpty(str_source))
                return string.Empty;
            return base64EncodeHelper.Base64Encode(str_source);
        }
        public string EncodeBytes(byte[] source)
        {
            return base64EncodeHelper.Base64Encode(source);
        }

        public string DecodeString(string str_source)
        {
            if (string.IsNullOrEmpty(str_source))
                return string.Empty;
            return base64EncodeHelper.Base64Decode(str_source);
        }

        public byte[] DecodeBytes(string str_source)
        {
            if (string.IsNullOrEmpty(str_source))
                return null;
            return base64EncodeHelper.Base64DecodeToBytes(str_source);
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
                    message = String.Empty;
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
        /// 将一个DataRow里的数据集转化成XML格式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
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
                    str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, (dr[col.ColumnName] == DBNull.Value) ? String.Empty :dr[col.ColumnName].ToString(), col.ColumnName);
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
        public T DeserializeXML<T>(string xmlObj)
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
        public string SerializeXML<T>(T obj)
        {
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }


    }
}
