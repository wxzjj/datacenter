using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Text.RegularExpressions;

namespace Bigdesk2010
{
    /// <summary>
    /// 基本功能函数
    /// </summary>
    public static class Utilities
    {
        #region Base64、Utf-8、Unicode 相互转换

        /// <summary>
        /// 将 UTF-8 字符串 进行 Base64 编码
        /// </summary>
        /// <param name="str">UTF-8 字符串</param>
        /// <returns></returns>
        public static String ToBase64String(this Object str)
        {
            if (str.IsEmpty()) return String.Empty;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str.ToString()));
        }

        /// <summary>
        /// 将 UTF-8 字符串 进行 Base64 解码
        /// </summary>
        /// <param name="str">UTF-8 字符串</param>
        /// <returns></returns>
        public static String FromBase64String(this Object str)
        {
            if (str.IsEmpty()) return String.Empty;
            return Encoding.UTF8.GetString(Convert.FromBase64String(str.ToString()));
        }

        /// <summary>
        /// 将 UTF-8 字符串转换成十六进制数据编码，即转换成 Unicode 字符
        /// </summary>
        /// <param name="str">UTF-8 字符串</param>
        /// <returns></returns>
        public static String ToUnicodeString(this Object str)
        {
            if (str.IsEmpty()) return String.Empty;
            char[] chars = str.ToString().ToCharArray();

            StringBuilder builder = new StringBuilder(chars.Length);
            foreach (short shortx in chars)
            {
                builder.Append(shortx.ToString("X4"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 将十六进制字符串(即 Unicode 字符)转换成 UTF-8 字符串
        /// </summary>
        /// <param name="str">十六进制字符串(即 Unicode 字符)</param>
        /// <returns></returns>
        public static String FromUnicodeString(this Object str)
        {
            if (str.IsEmpty()) return String.Empty;

            String s = str.ToString();
            int len = s.Length / 4;
            StringBuilder builder = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                builder.Append((char)short.Parse(s.Substring(i * 4, 4), NumberStyles.HexNumber));
            }
            return builder.ToString();
        }

        #endregion Base64、Utf-8、Unicode 相互转换

        #region 基本数据类型转换

        /// <summary>
        /// 转换成 <see cref="String"/>，注意：null，DBNull，Trim()="" 将转换成 defaultData
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static String ToString(this Object str, string defaultData)
        {
            return (str == null || str == DBNull.Value || str.ToString().Trim() == String.Empty) ? defaultData : str.ToString();
        }

        /// <summary>
        /// 转换成 <see cref="String"/>，注意：null,DBNull 将转换成 String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ToString2(this Object str)
        {
            return str == null || str == DBNull.Value || str is DBNull ? String.Empty : str.ToString();
        }

        /// <summary>
        /// 去除字符串的前后字符，注意：null,DBNull 将转换成 String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String TrimString(this Object str)
        {
            return str == null || str == DBNull.Value ? String.Empty : str.ToString().Trim();
        }

        /// <summary>
        /// 获取字符串的长度（一个汉字长度为2）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int LengthGB(this Object str)
        {
            int num = 0;
            byte[] bytes = Encoding.ASCII.GetBytes(str.ToString2());
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num++;
                }
                num++;
            }
            return num;
        }

        /// <summary>
        /// 转换成 <see cref="Int32"/>，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this Object str)
        {
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 转换成 <see cref="Int32"/>，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Int32 ToInt32(this Object str, Int32 defaultData)
        {
            Int32 result;
            if (Int32.TryParse(str.ToString2(), out result)) return result;
            return defaultData;
        }

        /// <summary>
        /// 转换成 <see cref="Int64"/>，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this Object str)
        {
            return Convert.ToInt64(str);
        }

        /// <summary>
        /// 转换成 <see cref="Int64"/>，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Int64 ToInt64(this Object str, Int64 defaultData)
        {
            Int64 result;
            if (Int64.TryParse(str.ToString2(), out result)) return result;
            return defaultData;
        }

        /// <summary>
        /// 将字节转成字符串
        /// </summary>
        /// <param name="int64"></param>
        /// <returns></returns>
        public static String ToByteString(this Object int64)
        {
            double byteCount = int64.ToInt64(0);

            String size = "0 KB";
            if (byteCount >= 1073741824)
                size = String.Format("{0:##.#}", byteCount / 1073741824) + " GB";
            else if (byteCount >= 1048576)
                size = String.Format("{0:##.#}", byteCount / 1048576) + " MB";
            else if (byteCount >= 1024)
                size = String.Format("{0:##.#}", byteCount / 1024) + " KB";
            else if (byteCount > 0)
                size = "1 KB";

            return size;
        }

        /// <summary>
        /// 转换成 <see cref="Boolean"/>，true,false,1,0，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this Object str)
        {
            switch (str.TrimString().ToLower())
            {
                case "true":
                case "1":
                    return true;
                case "false":
                case "0":
                    return false;
                default:
                    throw new Exception("Boolean类型无法转换");
            }
        }

        /// <summary>
        /// 转换成 <see cref="Boolean"/>，true,false,1,0，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this Object str, Boolean defaultData)
        {
            switch (str.TrimString().ToLower())
            {
                case "true":
                case "1":
                    return true;
                case "false":
                case "0":
                    return false;
                default:
                    return defaultData;
            }
        }

        /// <summary>
        /// 转换成 <see cref="Double"/>，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Double ToDouble(this Object str)
        {
            return Convert.ToDouble(str);
        }

        /// <summary>
        /// 转换成 <see cref="Double"/>，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Double ToDouble(this Object str, Double defaultData)
        {
            Double result;
            if (Double.TryParse(str.ToString2(), out result)) return result;
            return defaultData;
        }

        /// <summary>
        /// 转换成 <see cref="Decimal"/>，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this Object str)
        {
            return Convert.ToDecimal(str);
        }

        /// <summary>
        /// 转换成 <see cref="Decimal"/>，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this Object str, Decimal defaultData)
        {
            Decimal result;
            if (Decimal.TryParse(str.ToString2(), out result)) return result;
            return defaultData;
        }

        /// <summary>
        /// 转换成 <see cref="Guid"/>，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this Object str)
        {
            return new Guid(str.ToString2());
        }

        /// <summary>
        /// 转换成 <see cref="Guid"/>，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static Guid ToGuid(this Object str, Guid defaultData)
        {
            try
            {
                return new Guid(str.ToString2());
            }
            catch
            {
                return defaultData;
            }
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，只包含日期部分，时间部分固定为00:00:00，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDate(this Object str)
        {
            if (IsDate(str))
                return Convert.ToDateTime(str).Date;
            else
                throw new Exception("日期转换失败！");
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，只包含日期部分，时间部分固定为00:00:00，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static DateTime ToDate(this Object str, DateTime defaultData)
        {
            if (IsDate(str))
                return Convert.ToDateTime(str).Date;
            else
                return defaultData.Date;
        }

        /// <summary>
        /// 将 DateTime 类型的字符串转换成 Date 类型的字符串，如：2010-01-01 10:10:00 转换成 2010-01-01
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ToDate2(this Object str)
        {
            if (IsDate(str))
                return Convert.ToDateTime(str).ToString("yyyy-MM-dd");
            else
                return "";
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，只包含时间部分，日期部分固定为2010-01-01，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToTime(this Object str)
        {
            if (IsDateTime(str) || IsTime(str))
            {
                DateTime dt = Convert.ToDateTime(str);
                return new DateTime(2010, 1, 1, dt.Hour, dt.Minute, dt.Second);
            }
            else
                throw new Exception("时间转换失败！");
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，只包含时间部分，日期部分固定为2010-01-01，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static DateTime ToTime(this Object str, DateTime defaultData)
        {
            if (IsDateTime(str) || IsTime(str))
            {
                DateTime dt = Convert.ToDateTime(str);
                return new DateTime(2010, 1, 1, dt.Hour, dt.Minute, dt.Second);
            }
            else
            {
                return new DateTime(2010, 1, 1, defaultData.Hour, defaultData.Minute, defaultData.Second);
            }
        }

        /// <summary>
        /// 将 DateTime 或 Time 类型转换成 Time 类型的字符串，如：2010-01-01 10:10:00 转换成 10:10:00
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ToTime2(this Object str)
        {
            return ToTime2(str, false);
        }

        /// <summary>
        /// 将 DateTime 或 Time 类型转换成 Time 类型的字符串，如：2010-01-01 10:10:00 转换成 10:10:00
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isShowSecond"></param>
        /// <returns></returns>
        public static String ToTime2(this Object str, Boolean isShowSecond)
        {
            String format = isShowSecond ? "HH:mm:ss" : "HH:mm";
            if (IsDateTime(str) || IsTime(str))
                return Convert.ToDateTime(str).ToString(format);
            else
                return "";
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，包含日期和时间两部分，转换失败将抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Object str)
        {
            if (IsDateTime(str) || IsTime(str))
                return Convert.ToDateTime(str);
            else
                throw new Exception("日期时间转换失败！");
        }

        /// <summary>
        /// 转换成 <see cref="DateTime"/>，包含日期和时间两部分，当前转换失败时返回指定的默认数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Object str, DateTime defaultData)
        {
            if (IsDateTime(str) || IsTime(str))
                return Convert.ToDateTime(str);
            else
                return defaultData;
        }

        /// <summary>
        /// 将 DateTime 类型的字符串转换成 DateTime 类型的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ToDateTime2(this Object str)
        {
            return ToDateTime2(str, false);
        }

        /// <summary>
        /// 将 DateTime 类型的字符串转换成 DateTime 类型的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isShowSecond"></param>
        /// <returns></returns>
        public static String ToDateTime2(this Object str, Boolean isShowSecond)
        {
            String format = isShowSecond ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd HH:mm";
            if (IsDateTime(str) || IsTime(str))
                return Convert.ToDateTime(str).ToString(format);
            else
                return "";
        }

        #endregion 基本数据类型转换

        #region 字符串分隔与数组

        /// <summary>
        /// 返回的字符串数组包含此字符串中的子字符串（由指定字符串分隔）。参数指定是否返回空数组元素。
        /// </summary>
        /// <param name="str">数据类型为字符串</param>
        /// <param name="separator">分隔此字符串中子字符串的字符串</param>
        /// <param name="options">指定 RemoveEmptyEntries 以省略返回的数组中的空数组元素，或指定 None 以包含返回的数组中的空数组元素。</param>
        /// <returns></returns>
        public static String[] SplitToString(this Object str, String separator, StringSplitOptions options)
        {
            String[] sep = separator.IsEmpty() ? null : new String[] { separator };
            return SplitToString(str, sep, options);
        }

        /// <summary>
        /// 返回的字符串数组包含此字符串中的子字符串（由指定字符串数组的元素分隔）。参数指定是否返回空数组元素。
        /// </summary>
        /// <param name="str">数据类型为字符串</param>
        /// <param name="separator">分隔此字符串中子字符串的字符串数组、不包含分隔符的空数组或 null</param>
        /// <param name="options">指定 RemoveEmptyEntries 以省略返回的数组中的空数组元素，或指定 None 以包含返回的数组中的空数组元素。</param>
        /// <returns></returns>
        public static String[] SplitToString(this Object str, String[] separator, StringSplitOptions options)
        {
            return str.ToString2().Split(separator, options);
        }

        /// <summary>
        /// 返回的整型数组包含此字符串中的子字符串（由指定字符串分隔）。参数指定是否返回空数组元素。
        /// </summary>
        /// <param name="str">数据类型为字符串</param>
        /// <param name="separator">分隔此字符串中子字符串的字符串</param>
        /// <param name="options">指定 RemoveEmptyEntries 以省略返回的数组中的空数组元素，或指定 None 以包含返回的数组中的空数组元素。</param>
        /// <returns></returns>
        public static Int32[] SplitToInt32(this Object str, String separator, StringSplitOptions options)
        {
            String[] sep = separator.IsEmpty() ? null : new String[] { separator };
            return SplitToInt32(str, sep, options);
        }

        /// <summary>
        /// 返回的整型数组包含此字符串中的子字符串（由指定字符串数组的元素分隔）。参数指定是否返回空数组元素。
        /// </summary>
        /// <param name="str">数据类型为字符串</param>
        /// <param name="separator">分隔此字符串中子字符串的字符串数组、不包含分隔符的空数组或 null</param>
        /// <param name="options">指定 RemoveEmptyEntries 以省略返回的数组中的空数组元素，或指定 None 以包含返回的数组中的空数组元素。</param>
        /// <returns></returns>
        public static Int32[] SplitToInt32(this Object str, String[] separator, StringSplitOptions options)
        {
            String[] ss = str.ToString2().Split(separator, options);
            int[] ii = new int[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                if (ss[i].IsEmpty())
                    ii[i] = 0;
                else
                    ii[i] = ss[i].ToInt32();
            }
            return ii;
        }

        /// <summary>
        /// 将数组转换成字符串
        /// </summary>
        /// <typeparam name="T">基本数据类型，如字符串，整型等</typeparam>
        /// <param name="array">数组</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static String ArrayToString<T>(this T[] array, String separator)
        {
            String s = String.Empty;
            foreach (T t in array)
            {
                if (s == String.Empty)
                    s = t.ToString2();
                else
                    s += separator + t;
            }
            return s;
        }

        #endregion 字符串分隔与数组

        #region 数据校验

        /// <summary>
        /// 判断字符串是否是空，（null，DBNull，Trim()=""，都被认为是空）
        /// </summary>
        /// <param name="str"></param>
        /// <returns>当为空时，返回true</returns>
        public static Boolean IsEmpty(this Object str)
        {
            return str == null || str == DBNull.Value || str.ToString().Trim() == String.Empty;
        }

        /// <summary>
        /// 判断是否为Int32类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsInt32(this Object str)
        {
            Int32 result;
            return Int32.TryParse(str.ToString2(), out result);
        }

        /// <summary>
        /// 判断是否为Int64类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsInt64(this Object str)
        {
            Int64 result;
            return Int64.TryParse(str.ToString2(), out result);
        }

        /// <summary>
        /// 判断是否为Double类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsDouble(this Object str)
        {
            Double result;
            return Double.TryParse(str.ToString2(), out result);
        }

        /// <summary>
        /// 判断是否为Decimal类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsDecimal(this Object str)
        {
            Decimal result;
            return Decimal.TryParse(str.ToString2(), out result);
        }

        /// <summary>
        /// 判断是否是日期时间格式，2010-01-01，2010-02-02 10:30:15
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功，返回true</returns>
        public static Boolean IsDateTime(this Object str)
        {
            if (str.IsEmpty()) return false;
            try
            {
                String datetime = str.TrimString();

                //由于系统函数，在没有日期的情况下，会自动填上当天的日期，这是允许的
                //所以，先判断是否有正确的日期，然后再整体判断
                String temp = String.Empty;
                int strIndex = datetime.IndexOf(":");
                if (strIndex > 0)
                {
                    strIndex = datetime.Substring(0, strIndex).LastIndexOf(" ");
                    if (strIndex <= 0) return false;

                    temp = datetime.Substring(0, strIndex);//截取日期
                    Convert.ToDateTime(temp);

                    temp = datetime.Substring(strIndex);//截取时间
                    Convert.ToDateTime("2010-01-01" + " " + temp);
                    return true;
                }

                //判断日期结束
                Convert.ToDateTime(datetime);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 判断是否是日期格式，2010-01-01
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功，返回true</returns>
        public static Boolean IsDate(this Object str)
        {
            if (str.IsEmpty()) return false;
            try
            {
                String datetime = str.TrimString();

                //由于系统函数，时间在没有日期的情况下，会自动填上当天的日期，这是允许的2010-01-01 09:00:00
                //所以，先判断是否有正确的日期
                String temp = datetime;
                int strIndex = datetime.LastIndexOf(" ");
                if (strIndex >= 0)
                    temp = datetime.Substring(0, strIndex);//截取日期

                //判断日期结束
                Convert.ToDateTime(temp);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 判断是否是时间格式, 10:30:15
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功，返回true</returns>
        public static Boolean IsTime(this Object str)
        {
            if (str.IsEmpty()) return false;
            try
            {
                Convert.ToDateTime("2010-01-01" + " " + str);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 判断两个指定的字符串是否相等
        /// </summary>
        /// <param name="strA">第一个字符串</param>
        /// <param name="strB">第二个字符串</param>
        /// <param name="ignoreCase">是否区分大小写。（true 表示不区分大小写。）</param>
        /// <returns></returns>
        public static Boolean Equals2(this Object strA, Object strB, Boolean ignoreCase)
        {
            return strA.ToString2().Equals(strB.ToString2(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// DataSet是否有数据
        /// </summary>
        public static Boolean HasData(this DataSet dataSet)
        {
            if (dataSet == null) return false;

            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.Rows.Count > 0) return true;
            }

            return false;
        }

        /// <summary>
        /// DataTable是否有数据
        /// </summary>
        public static Boolean HasData(this DataTable dataTable)
        {
            if (dataTable == null) return false;
            return dataTable.Rows.Count > 0;
        }

        /// <summary>
        /// DataRow是否有数据
        /// </summary>
        public static Boolean HasData(this DataRow dataRow)
        {
            if (dataRow == null) return false;
            return dataRow.ItemArray.Length > 0;
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsBase64String(this Object str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str.ToString2(), @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 判断是否是 Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsGUID(this Object str)
        {
            Regex regex = new Regex(@"^([a-fA-F0-9]{8})(-)([a-fA-F0-9]{4})\2([a-fA-F0-9]{4}\2([a-fA-F0-9]{4})\2([a-fA-F0-9]{12}))$");
            return regex.Match(str.ToString2()).Success;
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsNumber(this Object str)
        {
            return Regex.Match(str.ToString2(), @"^[-+]?\d*$", RegexOptions.IgnoreCase).Success;
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static Boolean IsSafeSqlString(this Object str)
        {
            return !Regex.IsMatch(str.ToString2(), @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static Boolean IsValidEmail(this Object strEmail)
        {
            return Regex.IsMatch(strEmail.ToString2(), @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static Boolean IsValidDoEmail(this Object strEmail)
        {
            return Regex.IsMatch(strEmail.ToString2(), @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证姓名
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static Boolean IsValidName(this Object strName)
        {
            return Regex.IsMatch(strName.ToString2(), @"^[\u4E00-\u9FFF]+$");
        }


        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="strCardNo"></param>
        /// <returns></returns>
        public static Boolean IsValidCardNo(this Object strCardNo)
        {
            return Regex.IsMatch(strCardNo.ToString2(), @"^((1[1-5])|(2[1-3])|(3[1-7])|(4[1-6])|(5[0-4])|(6[1-5])|71|(8[12])|91)\d{4}((19\d{2}(0[13-9]|1[012])(0[1-9]|[12]\d|30))|(19\d{2}(0[13578]|1[02])31)|(19\d{2}02(0[1-9]|1\d|2[0-8]))|(19([13579][26]|[2468][048]|0[48])0229))\d{3}(\d|X|x)?$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static Boolean IsURL(this Object strUrl)
        {
            return Regex.IsMatch(strUrl.ToString2(), @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

       
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static Boolean IsIP(this Object ip)
        {
            return Regex.IsMatch(ip.ToString2(), @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static Boolean IsIPSect(this Object ip)
        {
            return Regex.IsMatch(ip.ToString2(), @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        #endregion 数据校验

        #region 功能函数结果异常

        /// <summary>
        /// 获取返回结果异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public static FunctionResultException GetFunctionResultException(this Exception exception)
        {
            if (exception == null) return null;

            var a = exception as FunctionResultException;
            if (a != null) return a;

            if (exception.InnerException == null) return null;

            return GetFunctionResultException(exception.InnerException);
        }

        /// <summary>
        /// 获取返回结果异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public static FunctionResultException<T> GetFunctionResultException<T>(this Exception exception)
        {
            if (exception == null) return null;

            var a = exception as FunctionResultException<T>;
            if (a != null) return a;

            if (exception.InnerException == null) return null;

            return GetFunctionResultException<T>(exception.InnerException);
        }

        #endregion 功能函数结果异常

        #region 字符串替换

        /// <summary>
        /// 将一个字符串中的多个子字符串替换成新的多个子字符串
        /// </summary>
        /// <example>
        /// 示例：
        /// key     value
        /// ab      11
        /// cd      22
        /// ef      33
        /// 
        /// 原始字符串：aabbccddeeff
        /// 目标字符串：a11bc22de33f
        /// 
        /// </example>
        /// <param name="str">要修改的字符串</param>
        /// <param name="old_new_Value">新旧字符串，key表示要替换的字符串，value表示要替换 key 的所有匹配项的字符串</param>
        /// <param name="options">枚举值的按位“或”组合</param>
        /// <returns>已修改的字符串。</returns>
        public static string Replace(this Object str, Dictionary<string, string> old_new_Value, RegexOptions options)
        {
            string str2 = str.ToString2();
            if (str2.Length == 0 || old_new_Value == null || old_new_Value.Count == 0) return str2;

            lock (_Lock_Old_New_Value)
            {
                _Old_New_Value = new Dictionary<string, string>();
                string pattern2 = "";
                foreach (string key in old_new_Value.Keys)
                {
                    if (key.Length == 0) continue;

                    string k = Regex.Unescape(key);
                    _Old_New_Value.Add(k, old_new_Value[key]);
                    pattern2 += "|" + k;
                }
                if (pattern2.Length == 0)
                {
                    _Old_New_Value = null;
                    return str2;
                }
                pattern2 = pattern2.Remove(0, 1);

                MatchEvaluator evaluator = new MatchEvaluator(ReplaceMatchEvaluator);
                string result = Regex.Replace(str2, pattern2, evaluator, options);
                _Old_New_Value = null;
                return result;
            }
        }
        private static Dictionary<string, string> _Old_New_Value;
        private static readonly Object _Lock_Old_New_Value = new Object();
        private static string ReplaceMatchEvaluator(Match m)
        {
            if (_Old_New_Value == null || !_Old_New_Value.ContainsKey(m.Value)) return "";
            return _Old_New_Value[m.Value];
        }

        #endregion 字符串替换

        public static string FormatString(string str)
        {
            if (str.IndexOf("<") < 0)
            {
                //				str=str.Replace(" ",""); 
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace('\n'.ToString(), "<br>");
                str = str.Replace("<br>", "");
                str = str.Replace("1900-1-1 0:00:00".ToString(), "");
                str = str.Replace(".000000".ToString(), "");
                str = str.Replace(".00000".ToString(), "");
                str = str.Replace(".0000".ToString(), "");
                str = str.Replace(" 0:00:00".ToString(), "");
                str = str.Replace(":00:00".ToString(), ":00");
            }

            //判断是否为日期字符串,这个判断不严格，但可以满足大部分要求
            string pattern = @"\b(\d{4})-([1-9]{1}|[1][0-2]|[0][1-9])-([1-9]{1}|[1][0-9]|[2][0-9]|[3][0-1]|[0][1-9])(.|\b)";
            string tmp = Regex.Match(str, pattern).Value;
            if (!(tmp == null || tmp == ""))
                str = tmp;
            if (str.IndexOf("<") < 0)
            {
                str = str.Replace(" ", "");
            }
            return str;
        }
    }



    /// <summary>
    /// CheckBox 选中与不选中所代表的值
    /// </summary>
    public enum CheckBoxValue
    {
        /// <summary>
        /// 选中表示 1，不选中表示 0，默认情况
        /// </summary>
        XZ_1_BXZ_0,

        /// <summary>
        /// 选中表示 1，不选中表示 0 和 1 两种情况		
        /// </summary>
        XZ_1_BXZ_01,

        /// <summary>		
        /// 选中表示 0，不选中表示 0 和 1 两种情况
        /// </summary>
        XZ_0_BXZ_O1,

        /// <summary>
        ///  选中表示 0 ，不选中表示 1
        /// </summary>
        XZ_0_BXZ_1,

        /// <summary>
        /// 选中表示 0 和 1 两种情况,不选中表示 0
        /// </summary>
        XZ_01_BXZ_0,

        /// <summary>
        /// 选中表示 0 和 1 两种情况,不选中表示 1
        /// </summary>
        XZ_O1_BXZ_1
    }

    /// <summary>
    /// 字符大写小写
    /// </summary>
    public enum UpperLowerCharCase
    {
        /// <summary>
        /// 不设置选项
        /// </summary>
        None,

        /// <summary>
        /// 将字符转换成大写字符
        /// </summary>
        ToUpper,

        /// <summary>
        /// 将字符转换成小写字符
        /// </summary>
        ToLower,
    }

    /// <summary>
    /// 字符全角半角
    /// </summary>
    public enum FullHalfCharCase
    {
        /// <summary>
        /// 不设置选项
        /// </summary>
        None,

        /// <summary>
        /// 将字符转换成半角字符，包括数字、字母、小括号、大括号等
        /// </summary>
        ToHalf,

        /// <summary>
        /// 将字符转换成全角字符，包括数字、字母、小括号、大括号等
        /// </summary>
        ToFull,
    }
}
