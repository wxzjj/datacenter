using System;
using System.Text;
using Microsoft.VisualBasic;

namespace Bigdesk2010
{
    /// <summary>
    /// 功能强大的字符串处理类
    /// </summary>
    public sealed class NukeString
    {
        #region 获取随机字符串

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">随机字符串长度</param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            // 为了避免视觉分不清，去掉了o-0，I-1，Z-2。
            string chars = "ABCDEFGHJKLMNPQRSTUVWXY3456789";

            Random r = new Random();

            string s = "";
            for (int i = 0; i < length; i++)
            {
                s += chars[r.Next(0, 30)];
            }
            return s;
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">随机字符串长度</param>
        /// <param name="isAllNumeric">当为false时，返回字符串全部为字母；当为true时，返回字符串全部为数字</param>
        /// <returns></returns>
        public static string GetRandomString(int length, bool isAllNumeric)
        {
            Random r = new Random();
            string s = "";

            if (isAllNumeric)
            {
                for (int i = 0; i < length; i++)
                {
                    s += r.Next(0, 10);
                }
            }
            else
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                for (int i = 0; i < length; i++)
                {
                    s += chars[r.Next(0, 26)];
                }
            }
            return s;
        }

        #endregion 获取随机字符串

        #region 汉字拼音简繁转换

        /// <summary>
        /// 拼音字符串数组
        /// </summary>
        private static string[] pystr = { 
            "a", "ai", "an", "ang", "ao", "ba", "bai", "ban", "bang", "bao", "bei", "ben", "beng", "bi", "bian", "biao", 
            "bie", "bin", "bing", "bo", "bu", "ca", "cai", "can", "cang", "cao", "ce", "ceng", "cha", "chai", "chan", "chang", 
            "chao", "che", "chen", "cheng", "chi", "chong", "chou", "chu", "chuai", "chuan", "chuang", "chui", "chun", "chuo", "ci", "cong", 
            "cou", "cu", "cuan", "cui", "cun", "cuo", "da", "dai", "dan", "dang", "dao", "de", "deng", "di", "dian", "diao", 
            "die", "ding", "diu", "dong", "dou", "du", "duan", "dui", "dun", "duo", "e", "en", "er", "fa", "fan", "fang", 
            "fei", "fen", "feng", "fo", "fou", "fu", "ga", "gai", "gan", "gang", "gao", "ge", "gei", "gen", "geng", "gong", 
            "gou", "gu", "gua", "guai", "guan", "guang", "gui", "gun", "guo", "ha", "hai", "han", "hang", "hao", "he", "hei", 
            "hen", "heng", "hong", "hou", "hu", "hua", "huai", "huan", "huang", "hui", "hun", "huo", "ji", "jia", "jian", "jiang", 
            "jiao", "jie", "jin", "jing", "jiong", "jiu", "ju", "juan", "jue", "jun", "ka", "kai", "kan", "kang", "kao", "ke", 
            "ken", "keng", "kong", "kou", "ku", "kua", "kuai", "kuan", "kuang", "kui", "kun", "kuo", "la", "lai", "lan", "lang", 
            "lao", "le", "lei", "leng", "li", "lia", "lian", "liang", "liao", "lie", "lin", "ling", "liu", "long", "lou", "lu", 
            "lv", "luan", "lue", "lun", "luo", "ma", "mai", "man", "mang", "mao", "me", "mei", "men", "meng", "mi", "mian", 
            "miao", "mie", "min", "ming", "miu", "mo", "mou", "mu", "na", "nai", "nan", "nang", "nao", "ne", "nei", "nen", 
            "neng", "ni", "nian", "niang", "niao", "nie", "nin", "ning", "niu", "nong", "nu", "nv", "nuan", "nue", "nuo", "o", 
            "ou", "pa", "pai", "pan", "pang", "pao", "pei", "pen", "peng", "pi", "pian", "piao", "pie", "pin", "ping", "po", 
            "pu", "qi", "qia", "qian", "qiang", "qiao", "qie", "qin", "qing", "qiong", "qiu", "qu", "quan", "que", "qun", "ran", 
            "rang", "rao", "re", "ren", "reng", "ri", "rong", "rou", "ru", "ruan", "rui", "run", "ruo", "sa", "sai", "san", 
            "sang", "sao", "se", "sen", "seng", "sha", "shai", "shan", "shang", "shao", "she", "shen", "sheng", "shi", "shou", "shu", 
            "shua", "shuai", "shuan", "shuang", "shui", "shun", "shuo", "si", "song", "sou", "su", "suan", "sui", "sun", "suo", "ta", 
            "tai", "tan", "tang", "tao", "te", "teng", "ti", "tian", "tiao", "tie", "ting", "tong", "tou", "tu", "tuan", "tui", 
            "tun", "tuo", "wa", "wai", "wan", "wang", "wei", "wen", "weng", "wo", "wu", "xi", "xia", "xian", "xiang", "xiao", 
            "xie", "xin", "xing", "xiong", "xiu", "xu", "xuan", "xue", "xun", "ya", "yan", "yang", "yao", "ye", "yi", "yin", 
            "ying", "yo", "yong", "you", "yu", "yuan", "yue", "yun", "za", "zai", "zan", "zang", "zao", "ze", "zei", "zen", 
            "zeng", "zha", "zhai", "zhan", "zhang", "zhao", "zhe", "zhen", "zheng", "zhi", "zhong", "zhou", "zhu", "zhua", "zhuai", "zhuan", 
            "zhuang", "zhui", "zhun", "zhuo", "zi", "zong", "zou", "zu", "zuan", "zui", "zun", "zuo"
         };

        /// <summary>
        /// 拼音值数组
        /// </summary>
        private static int[] pyvalue =  { 
            -20319, -20317, -20304, -20295, -20292, -20283, -20265, -20257, -20242, -20230, -20051, -20036, -20032, -20026, -20002, -19990, 
            -19986, -19982, -19976, -19805, -19784, -19775, -19774, -19763, -19756, -19751, -19746, -19741, -19739, -19728, -19725, -19715, 
            -19540, -19531, -19525, -19515, -19500, -19484, -19479, -19467, -19289, -19288, -19281, -19275, -19270, -19263, -19261, -19249, 
            -19243, -19242, -19238, -19235, -19227, -19224, -19218, -19212, -19038, -19023, -19018, -19006, -19003, -18996, -18977, -18961, 
            -18952, -18783, -18774, -18773, -18763, -18756, -18741, -18735, -18731, -18722, -18710, -18697, -18696, -18526, -18518, -18501, 
            -18490, -18478, -18463, -18448, -18447, -18446, -18239, -18237, -18231, -18220, -18211, -18201, -18184, -18183, -18181, -18012, 
            -17997, -17988, -17970, -17964, -17961, -17950, -17947, -17931, -17928, -17922, -17759, -17752, -17733, -17730, -17721, -17703, 
            -17701, -17697, -17692, -17683, -17676, -17496, -17487, -17482, -17468, -17454, -17433, -17427, -17417, -17202, -17185, -16983, 
            -16970, -16942, -16915, -16733, -16708, -16706, -16689, -16664, -16657, -16647, -16474, -16470, -16465, -16459, -16452, -16448, 
            -16433, -16429, -16427, -16423, -16419, -16412, -16407, -16403, -16401, -16393, -16220, -16216, -16212, -16205, -16202, -16187, 
            -16180, -16171, -16169, -16158, -16155, -15959, -15958, -15944, -15933, -15920, -15915, -15903, -15889, -15878, -15707, -15701, 
            -15681, -15667, -15661, -15659, -15652, -15640, -15631, -15625, -15454, -15448, -15436, -15435, -15419, -15416, -15408, -15394, 
            -15385, -15377, -15375, -15369, -15363, -15362, -15183, -15180, -15165, -15158, -15153, -15150, -15149, -15144, -15143, -15141, 
            -15140, -15139, -15128, -15121, -15119, -15117, -15110, -15109, -14941, -14937, -14933, -14930, -14929, -14928, -14926, -14922, 
            -14921, -14914, -14908, -14902, -14894, -14889, -14882, -14873, -14871, -14857, -14678, -14674, -14670, -14668, -14663, -14654, 
            -14645, -14630, -14594, -14429, -14407, -14399, -14384, -14379, -14368, -14355, -14353, -14345, -14170, -14159, -14151, -14149, 
            -14145, -14140, -14137, -14135, -14125, -14123, -14122, -14112, -14109, -14099, -14097, -14094, -14092, -14090, -14087, -14083, 
            -13917, -13914, -13910, -13907, -13906, -13905, -13896, -13894, -13878, -13870, -13859, -13847, -13831, -13658, -13611, -13601, 
            -13406, -13404, -13400, -13398, -13395, -13391, -13387, -13383, -13367, -13359, -13356, -13343, -13340, -13329, -13326, -13318, 
            -13147, -13138, -13120, -13107, -13096, -13095, -13091, -13076, -13068, -13063, -13060, -12888, -12875, -12871, -12860, -12858, 
            -12852, -12849, -12838, -12831, -12829, -12812, -12802, -12607, -12597, -12594, -12585, -12556, -12359, -12346, -12320, -12300, 
            -12120, -12099, -12089, -12074, -12067, -12058, -12039, -11867, -11861, -11847, -11831, -11798, -11781, -11604, -11589, -11536, 
            -11358, -11340, -11339, -11324, -11303, -11097, -11077, -11067, -11055, -11052, -11045, -11041, -11038, -11024, -11020, -11019, 
            -11018, -11014, -10838, -10832, -10815, -10800, -10790, -10780, -10764, -10587, -10544, -10533, -10519, -10331, -10329, -10328, 
            -10322, -10315, -10309, -10307, -10296, -10281, -10274, -10270, -10262, -10260, -10256, -10254
         };

        /// <summary>
        /// 获取汉字的拼音
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string GB2Spell(string str)
        {
            byte[] bytes = new byte[2];
            string str2 = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            char[] chArray = str.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                bytes = Encoding.Default.GetBytes(chArray[i].ToString());
                num2 = bytes[0];
                num3 = bytes[1];
                num = ((num2 * 0x100) + num3) - 0x10000;
                if ((num > 0) && (num < 160))
                {
                    str2 = str2 + chArray[i];
                }
                else
                {
                    for (int j = pyvalue.Length - 1; j >= 0; j--)
                    {
                        if (pyvalue[j] < num)
                        {
                            str2 = str2 + pystr[j];
                            break;
                        }
                    }
                }
            }
            return str2;
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSimplifiedChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTraditionalChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 转换为全角字符
        /// </summary>
        public static string ToSBCCase(string str)
        {
            return Strings.StrConv(str, VbStrConv.Wide, 0); // 半角转全角
        }

        /// <summary>
        /// 转换为半角字符
        /// </summary>
        public static string ToDBCCase(string str)
        {
            return Strings.StrConv(str, VbStrConv.Narrow, 0); // 全角转半角
        }

        /// <summary>
        /// 将字符串中每个单词的首字母转换为大写
        /// </summary>
        public static string ToProperCase(string str)
        {
            return Strings.StrConv(str, VbStrConv.ProperCase, 0); // 首字母大写
        }

        #endregion 汉字拼音简繁转换

        #region 数字大小写转换

        /// <summary>
        /// 将字符串中的数字转换成中文，如1转换成一，5转换成五。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToChineseUpper(string str)
        {
            string s = "";
            foreach (char c in str)
            {
                s += ToChineseUpper(c);
            }
            return s;
        }

        /// <summary>
        /// 将字符串中的数字转换成汉字，如1转换成壹，5转换成伍。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToChineseWord(string str)
        {
            string s = "";
            foreach (char c in str)
            {
                s += ToChineseWord(c);
            }
            return s;
        }

        /// <summary>
        /// 数字金额转换成大写汉字金额
        /// </summary>
        /// <param name="m">数字金额</param>
        /// <returns></returns>
        public static string MoneyToRMB(double m)
        {
            string dX = m.ToString();
            int index = dX.IndexOf(".", 0);
            if(index == -1)
            {
                return RY(dX);
            }
            string str3 = "";
            string str2 = RY(dX.Substring(0, index));
            string str5 = dX.Substring(index, dX.Length - index).Substring(1);
            if ((str5.Length == 1) && (str5[0] != '0'))
            {
                str3 = ToChineseWord(str5[0]) + "角零分";
            }
            if (str5.Length == 2)
            {
                if ((str5[1] != '0') && (str5[0] != '0'))
                {
                    str3 = ToChineseWord(str5[0]) + "角" + ToChineseWord(str5[1]) + "分";
                }
                else if (str5[0] == '0')
                {
                    str3 = ToChineseWord(str5[1]) + "分";
                }
            }
            return (str2 + str3);
        }
        /// <summary>
        /// 将一个数字转换成中文，如1转换成一，5转换成五。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string ToChineseUpper(char num)
        {
            switch (num)
            {
                case '0': return "○";
                case '1': return "一";
                case '2': return "二";
                case '3': return "三";
                case '4': return "四";
                case '5': return "五";
                case '6': return "六";
                case '7': return "七";
                case '8': return "八";
                case '9': return "九";
                default: return "";
            }
        }
        /// <summary>
        /// 将一个数字转换成汉字，如1转换成壹，5转换成伍。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string ToChineseWord(char num)
        {
            switch (num)
            {
                case '0': return "零";
                case '1': return "壹";
                case '2': return "贰";
                case '3': return "叁";
                case '4': return "肆";
                case '5': return "伍";
                case '6': return "陆";
                case '7': return "柒";
                case '8': return "捌";
                case '9': return "玖";
                default: return "";
            }
        }
        /// <summary>
        /// 整数转换成大写汉字金额
        /// </summary>
        /// <param name="DX"></param>
        /// <returns></returns>
        private static string RY(string DX)
        {
            string str = "";
            int length = DX.Length;
            if (DX != "0")
            {
                int num2 = 0;
                for (int i = 0; i < DX.Length; i++)
                {
                    int num4 = DX.Length - i;
                    if (DX[i] != '0')
                        str = str + ToChineseWord(DX[i]);
                    if ((((num2 == 0) && (DX[i] == '0')) && ((num4 != 5) && (num4 != 1))) && (i < (DX.Length - 1)))
                    {
                        bool flag = true;
                        //for (int j = i + 1; j < DX.Length; j++)
                        //{
                        //    if (DX[j] == '0')
                        //    {
                        //        flag = false;
                        //    }
                        //}
                        //if (flag)
                        //{
                        //    str = str + "零";
                        //    num2 = 1;
                        //}

                        for (int j = i + 1; j < DX.Length; j++)
                        {
                            if (DX[j] == '0')
                            {
                                flag = false;
                            }
                            else 
                            {
                                if (flag)
                                {
                                    str = str + "零";
                                    //num2 = 1;
                                    break;
                                }
                            }

                        }
                        
                    }
                    if ((num4 == 8) && (DX[i] != '0'))
                    {
                        str = str + "仟";
                    }
                    if ((num4 == 7) && (DX[i] != '0'))
                    {
                        str = str + "佰";
                    }
                    if ((num4 == 6) && (DX[i] != '0'))
                    {
                        str = str + "拾";
                    }
                    if (num4 == 5)
                    {
                        str = str + "万";
                    }
                    if ((num4 == 4) && (DX[i] != '0'))
                    {
                        str = str + "仟";
                    }
                    if ((num4 == 3) && (DX[i] != '0'))
                    {
                        str = str + "佰";
                    }
                    if ((num4 == 2) && (DX[i] != '0'))
                    {
                        str = str + "拾";
                    }
                    if (num4 == 1)
                    {
                        str = str + "元";
                    }
                }
            }
            return str;
        }

        #endregion 数字大小写转换

        #region 子字符串

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {
                //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns></returns>
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = str.LengthGB();// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos) + p_TailString;
            }
            else
                result = str;

            return result;
        }

        #endregion 子字符串

    }
}
