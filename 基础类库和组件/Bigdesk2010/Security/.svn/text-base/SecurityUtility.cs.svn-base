using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bigdesk2010.Security
{
    /// <summary>
    /// 数据安全功能函数
    /// </summary>
    public static class SecurityUtility
    {
        #region SHA1

        /// <summary>
        /// 计算SHA1哈希值
        /// </summary>
        /// <param name="stream">要计算其哈希代码的数据流</param>
        /// <returns>返回SHA1哈希值</returns>
        public static string SHA1(Stream stream)
        {
            stream.Position = 0;
            SHA1Managed sha1 = new SHA1Managed();
            return ConvertToString(sha1.ComputeHash(stream));
        }

        /// <summary>
        /// 计算SHA1哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <returns>返回SHA1哈希值</returns>
        public static string SHA1(byte[] buffer)
        {
            SHA1Managed sha1 = new SHA1Managed();
            return ConvertToString(sha1.ComputeHash(buffer));
        }

        /// <summary>
        /// 计算SHA1哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <returns>返回SHA1哈希值</returns>
        public static string SHA1(byte[] buffer, int offset, int count)
        {
            SHA1Managed sha1 = new SHA1Managed();
            return ConvertToString(sha1.ComputeHash(buffer, offset, count));
        }

        /// <summary>
        /// 计算SHA1哈希值
        /// </summary>
        /// <param name="str">要计算其哈希代码的字符串</param>
        /// <returns>返回SHA1哈希值</returns>
        public static string SHA1(string str)
        {
            return SHA1(Encoding.UTF8.GetBytes(str));
        }

        #endregion SHA1

        #region MD5

        /// <summary>
        /// 计算MD5哈希值
        /// </summary>
        /// <param name="stream">要计算其哈希代码的数据流</param>
        /// <returns>返回MD5哈希值</returns>
        public static string MD5(Stream stream)
        {
            stream.Position = 0;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return ConvertToString(md5.ComputeHash(stream));
        }

        /// <summary>
        /// 计算MD5哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <returns>返回MD5哈希值</returns>
        public static string MD5(byte[] buffer)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return ConvertToString(md5.ComputeHash(buffer));
        }

        /// <summary>
        /// 计算MD5哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <returns>返回MD5哈希值</returns>
        public static string MD5(byte[] buffer, int offset, int count)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return ConvertToString(md5.ComputeHash(buffer, offset, count));
        }

        /// <summary>
        /// 计算MD5哈希值
        /// </summary>
        /// <param name="str">要计算其哈希代码的字符串</param>
        /// <returns>返回MD5哈希值</returns>
        public static string MD5(string str)
        {
            return MD5(Encoding.UTF8.GetBytes(str));
        }

        #endregion MD5

        #region SHA256

        /// <summary>
        /// 计算SHA256哈希值
        /// </summary>
        /// <param name="stream">要计算其哈希代码的数据流</param>
        /// <returns>返回SHA256哈希值</returns>
        public static string SHA256(Stream stream)
        {
            stream.Position = 0;
            SHA256Managed sha256 = new SHA256Managed();
            return ConvertToString(sha256.ComputeHash(stream));
        }

        /// <summary>
        /// 计算SHA256哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <returns>返回SHA256哈希值</returns>
        public static string SHA256(byte[] buffer)
        {
            SHA256Managed sha256 = new SHA256Managed();
            return ConvertToString(sha256.ComputeHash(buffer));
        }

        /// <summary>
        /// 计算SHA256哈希值
        /// </summary>
        /// <param name="buffer">要计算其哈希代码的数组。</param>
        /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
        /// <param name="count">数组中用作数据的字节数。</param>
        /// <returns>返回SHA256哈希值</returns>
        public static string SHA256(byte[] buffer, int offset, int count)
        {
            SHA256Managed sha256 = new SHA256Managed();
            return ConvertToString(sha256.ComputeHash(buffer, offset, count));
        }

        /// <summary>
        /// 计算SHA256哈希值
        /// </summary>
        /// <param name="str">要计算其哈希代码的字符串</param>
        /// <returns>返回SHA256哈希值</returns>
        public static string SHA256(string str)
        {
            return SHA256(Encoding.UTF8.GetBytes(str));
        }

        #endregion SHA256

        private static string ConvertToString(byte[] buffer)
        {
            return BitConverter.ToString(buffer).Replace("-", "");
        }
    }
}
