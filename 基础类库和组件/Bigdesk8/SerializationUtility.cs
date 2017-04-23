using System;
using System.IO;
using System.Xml.Serialization;

namespace Bigdesk8
{
    /// <summary>
    /// 序列化和反序列化处理类
    /// </summary>
    public static class SerializationUtility
    {
        /// <summary>
        /// 序列化 - 将对象序列化成文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save<T>(T obj, string filename) where T : class
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(fs, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化 - 将文件序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static T Load<T>(string filename) where T : class
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 序列化 - 将对象序列化成XML字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>XML字符串</returns>
        public static string Serialize<T>(T obj) where T : class
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(ms, obj);

                    ms.Seek(0, SeekOrigin.Begin);
                    using (StreamReader sr = new StreamReader(ms))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化 - 将XML字符串序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objXML">对象XML字符串</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string objXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(objXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
