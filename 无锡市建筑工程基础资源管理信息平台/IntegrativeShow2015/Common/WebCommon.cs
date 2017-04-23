using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.SqlClient;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;


namespace IntegrativeShow2
{
    public class WebCommon
    {

        #region 配置项

        public static string ConnectionString_WJSJZX
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        /// <summary>
        /// 主题1
        /// </summary>
        public static string SystemTheme
        {
            get
            {
                return ConfigurationManager.AppSettings["IntegrativeShow2_Theme"];
            }
        }

        /// <summary>
        /// 得到登录页面URL
        /// </summary>
        /// <returns></returns>
        public static string GetLoginPageUrl()
        {
            return string.Format(ConfigurationManager.AppSettings["LoginPageUrl2"], ConfigurationManager.AppSettings["IntegrativeShow2_Index"]);
        }
        /// <summary>
        /// 得到重新登录页面URL
        /// </summary>
        /// <returns></returns>
        public static string GetReloginPageUrl()
        {
            return string.Format(ConfigurationManager.AppSettings["ReloginPageUrl2"], ConfigurationManager.AppSettings["IntegrativeShow2_Index"]);
        }

        /// <summary>
        /// 得到修改密码页面URL
        /// </summary>
        /// <returns></returns>
        public static string GetRePasswordPageUrl()
        {
            return ConfigurationManager.AppSettings["RePasswordPageUrl"];
        }

        /// <summary>
        /// 得到上传文件目录
        /// </summary>
        public static string GetSgtsjscFileDir()
        {
            return ConfigurationManager.AppSettings["SgtsjscFileDir"];
        }

        /// <summary>
        /// 得到年度考评的年份
        /// </summary>
        /// <returns></returns>
        public static string GetNdkpYear()
        {
            return ConfigurationSettings.AppSettings["NdkpYear"];
        }
        /// <summary>
        /// 预选承包商使用年度
        /// </summary>
        /// <returns></returns>
        public static string GetYxcbsSynd()
        {
            return ConfigurationSettings.AppSettings["Yxcbs_Synd"];
        }
        #endregion 配置项

        #region 数据库


        /// <summary>
        /// 获得 WJSJZX 数据库操作者
        /// </summary>
        /// <returns></returns>
        public static DBOperator GetDB_WJSJZX()
        {
            return DBOperatorFactory.GetDBOperator(ConnectionString_WJSJZX, DataBaseType.SQLSERVER2008.ToString());
        }
        /// <summary>
        /// 得到数据库服务器时间
        /// </summary>
        public static DateTime GetDBDateTime(DBOperator db)
        {
            return Convert.ToDateTime(db.ExeSqlForString("select getdate()", db.CreateSqlParameterCollection()));
        }
        #endregion 数据库

        #region 登录用户

        /// <summary>
        /// 匿名用户 - Guest
        /// </summary>
        public const string GuestloginID = "1";
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static string GetLoginUserId()
        {
            object sessionObject = System.Web.HttpContext.Current.Session["LOGINID"];
            if ((sessionObject == null) || (sessionObject.ToString().Trim() == string.Empty))
            {
                return GuestloginID;//匿名用户
            }
            return sessionObject.ToString();
        }
        #endregion 登录用户




        #region 填充 DropDownList
        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void DropDownListDataBind(DBDropDownList dropdonlist, DataTable dt, bool addSpaceItem)
        {

            //UIvsData.DropDownListDataBind(dropdonlist, dt, addSpaceItem);\
            UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
        }
        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void DropDownListDataBind(DBDropDownList dropdonlist, string sql, bool addSpaceItem)
        {
            if (sql != string.Empty)
            {
                DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
                UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
            }

        }

        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void DropDownListDataBind(DBDropDownList dropdonlist, bool addSpaceItem)
        {
            string sql = string.Empty;
            switch (dropdonlist.ToolTip)
            {
                /*2015-3-31 李贯涛 综合监管招标方式*/
                case "zbfs"://招标方式
                    sql = "select CodeInfo,Code from tbTenderTypeDic ";
                    break;

                case "PrjType":
                    sql = "select CodeInfo,Code from tbPrjTypeDic ";
                    break;
                case "Lxjb":
                    sql = "select CodeInfo,Code from tbLxjbDic ";
                    break;
                case "PrjProperty":
                    sql = "select CodeInfo,Code from tbPrjPropertyDic ";
                    break;
                case "PrjStructureType":
                    sql = "select CodeInfo,Code from tbPrjStructureTypeDic ";
                    break;
                case "TenderClass":
                    sql = "select CodeInfo,Code from tbTenderClassDic ";
                    break;
                case "TenderType":
                    sql = "select CodeInfo,Code from tbTenderTypeDic ";
                    break;
                case "ContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic order by OrderID ";
                    break;
                case "WorkDuty":
                    sql = "select CodeInfo,Code from tbWorkDutyDic ";
                    break;
                case "IDCardType":
                    sql = "select CodeInfo,Code from tbIDCardTypeDic ";
                    break;
                case "SpecialtyType":
                    sql = "select CodeInfo,Code from tbSpecialtyTypeDic ";
                    break;
                case "Xzqdm":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;
                case "ApprovalLevel":
                    sql = "select CodeInfo,Code from tbLxjbDic  ";
                    break;
                case "xmsd":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;


            }
            if (sql != string.Empty)
            {
                DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
                UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
            }
        }

        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void CheckBoxListDataBind(CheckBoxList checkBoxList)
        {
            string sql = string.Empty;
            switch (checkBoxList.ClientID)
            {
                case "cbl_ssdq":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;
                case "cbl_Htlb":
                    sql = "select CodeInfo,Code from tbContractTypeDic where OrderID >=0 order by OrderID  ";
                    break;
            }


            if (sql != string.Empty)
            {
                DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
                foreach (DataRow row in dt.Rows)
                {
                    checkBoxList.Items.Add(new ListItem(row[0].ToString(), row[1].ToString()));
                }
            }
        }




        #endregion 填充 DropDownList

        #region    业务功能函数

        #endregion 业务功能函数

        #region 执行自动提醒配置项

        //public static void 执行自动提醒配置项()
        //{
        //    DBOperator db = WebCommon.GetDB_WJSJZX();
        //    SqlParameterCollection p = db.CreateSqlParameterCollection();

        //    string sql = "select * from d_Aqjd_txpzx where zt=0 and (sj is null or datediff(day,getdate(),sj)<>0)";
        //    DataTable dt = db.ExeSqlForDataTable(sql, null, "t");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        string tempsql = FilterSql(dr["nr"].ToString());
        //        db.ExecuteNonQuerySql(tempsql, null);


        //        p.Clear();
        //        p.Add("@id", dr["id"].ToString());
        //        db.ExecuteNonQuerySql("update d_Aqjd_txpzx set sj=getdate() where id=@id", p);
        //    }
        //}

        ///// <summary>
        ///// 过滤SQL语句
        ///// </summary>
        ///// <param name="sql">SQL语句</param>
        ///// <returns></returns>
        //private static string FilterSql(string sql)
        //{
        //    if (sql == null || sql == string.Empty) return sql;

        //    // 删除特殊字符--如回车,换行,TAB等
        //    StringBuilder stringBuilder = new StringBuilder(1024);
        //    for (int i = 0; i < sql.Length; i++)
        //    {
        //        int unicode = sql[i];
        //        if (unicode >= 16)
        //        {
        //            stringBuilder.Append(sql[i].ToString());
        //        }
        //        else
        //        {
        //            stringBuilder.Append(" ");
        //        }
        //    }

        //    // 去掉SQL语句中的GO
        //    string pattern = " go ";
        //    string replacement = " ";//" ; ";
        //    RegexOptions options = RegexOptions.IgnoreCase;
        //    return Regex.Replace(stringBuilder.ToString(), pattern, replacement, options);
        //}

        #endregion 执行自动提醒配置项

        #region 处理身份证

        /// <summary>
        /// 将15位身份证升级到18位
        /// </summary>
        /// <param name="IDCard15">15位身份证号</param>
        /// <returns>18位身份证号</returns>
        public static string IDCard15To18(string IDCard15)
        {
            string result = string.Empty;
            string pattern = @"\d{15}";
            string IDCard18 = string.Empty;
            string IDCard17 = string.Empty;
            int length = IDCard15.Length;
            if (length != 15)
            {
                result = "身份证号位数不正确，当前为" + length.ToString() + "位，请采用15位。";
                return string.Empty;
            }

            if (Regex.Match(IDCard15, pattern).Value == "") // 验证为15位数字
            {
                result = "身份证号格式有误，应该为15位数字";
                return string.Empty;
            }

            // 如果身份证后三位顺序码是996 997 998 999，这些是为百岁以上老人的特殊编码
            string lastIDCard3 = IDCard15.Substring(12, 3);
            if (lastIDCard3 == "996" || lastIDCard3 == "997" || lastIDCard3 == "998" || lastIDCard3 == "999")
            {
                IDCard17 = IDCard15.Substring(0, 6) + "18" + IDCard15.Substring(6, 9);
            }
            else
            {
                IDCard17 = IDCard15.Substring(0, 6) + "19" + IDCard15.Substring(6, 9);
            }

            // 计算最后位一位
            string VerifyNumber = GetIDCard18LastNo(IDCard17);
            if (VerifyNumber != string.Empty)
            {
                IDCard18 = IDCard17 + VerifyNumber;
            }
            else
            {
                result = "身份证号格式有误，没能计算出最后一位";
            }
            return IDCard18;
        }
        /// <summary>
        /// 由身份证号前17位计算第18位
        /// </summary>
        /// <param name="IDCard17">身份证号前17位</param>
        /// <returns>身份证号第18位</returns>
        private static string GetIDCard18LastNo(string IDCard17)
        {
            string result = string.Empty;
            try
            {
                if (IDCard17.Length != 17)
                {
                    return string.Empty;
                }
                // 加权因子  
                int[] W = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

                // 校验码对应值  
                string LastCode = "10X98765432";

                int checkSum = 0;
                for (int i = 0; i < IDCard17.Length; i++)
                {
                    checkSum += Convert.ToInt32(IDCard17.Substring(i, 1)) * W[i];
                }
                int j = checkSum % 11;
                result = LastCode.Substring(j, 1);
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 18位身份证校验码有效性检查
        /// </summary>
        /// <param name="IDCard18">18位身份证号</param>
        /// <returns>验证通过[return string.Empty]，验证不通过[return 具体原因]</returns>
        public static string CheckIDCard18(string IDCard18)
        {
            string result = string.Empty;
            int length = IDCard18.Length;
            string pattern = @"\d{17}[0-9Xx]";
            string patternDate = @"(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})(((0[13578]|1[02])(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)(0[1-9]|[12][0-9]|30))|(02(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))0229)";

            if (length != 18)   // 验证长度为18位
            {
                result = "身份证号位数不正确，当前为" + length.ToString() + "位，请采用18位。";
                return result;
            }

            if (Regex.Match(IDCard18, pattern).Value == "") // 验证前17位为数字，最后一位为数字或X
            {
                result = "身份证号格式有误，正确的格式应该是：前17位为数字，最后一位为数字或X";
                return result;
            }

            string IDCardDate = IDCard18.Substring(6, 8);
            if (Regex.Match(IDCardDate, patternDate).Value == "")  // 验证出生日期的有效性
            {
                result = "身份证号格式有误，当前7至14位出生日期为" + IDCardDate + "，这是一个无效的日期";
                return result;
            }
            string IDCard17 = IDCard18.Substring(0, 17);
            string lastNo = GetIDCard18LastNo(IDCard17);
            if (lastNo != IDCard18.Substring(17, 1).ToUpper())
            {
                result = "身份证号格式有误，最后一位校验码应该为" + lastNo + "。";
                return result;
            }
            return result;
        }
        /// <summary>
        /// 身份证前17位有效性检查
        /// </summary>
        /// <param name="IDCard18">18位身份证号</param>
        /// <returns>验证通过[return string.Empty]，验证不通过[return 具体原因]</returns>
        public static string CheckIDCard17(string IDCard18)
        {
            string result = string.Empty;
            int length = IDCard18.Length;
            string pattern = @"\d{17}[0-9Xx]";
            string patternDate = @"(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})(((0[13578]|1[02])(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)(0[1-9]|[12][0-9]|30))|(02(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))0229)";

            if (length != 18)   // 验证长度为18位
            {
                result = "身份证号位数不正确，当前为" + length.ToString() + "位，请采用18位。";
                return result;
            }

            if (Regex.Match(IDCard18, pattern).Value == "") // 验证前17位为数字，最后一位为数字或X
            {
                result = "身份证号格式有误，正确的格式应该是：前17位为数字，最后一位为数字或X";
                return result;
            }

            string IDCardDate = IDCard18.Substring(6, 8);
            if (Regex.Match(IDCardDate, patternDate).Value == "")  // 验证出生日期的有效性
            {
                result = "身份证号格式有误，当前7至14位出生日期为" + IDCardDate + "，这是一个无效的日期";
                return result;
            }
            //string IDCard17 = IDCard18.Substring(0, 17);
            //string lastNo = GetIDCard18LastNo(IDCard17);
            //if (lastNo != IDCard18.Substring(17, 1).ToUpper())
            //{
            //    result = "身份证号格式有误，最后一位校验码应该为" + lastNo + "。";
            //    return result;
            //}
            return result;
        }
        #endregion

        #region 字符串控制函数

        /// <summary>
        /// 如果长度大于totalLen，则截取len长度，其余.添充
        /// </summary>
        /// <param name="s">要截取的字符串</param>
        /// <param name="len">截取的长度</param>
        /// <param name="totalLen">添充.后的总长度</param>
        /// <returns></returns>
        public static string SubTitle(string s, int len)
        {
            string result = ""; //最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(s);  //单字节字符长度
            if (byteLen <= len) return s;
            int charLen = s.Length; //把字符平等对待时的字符串长度
            int byteCount = 0;  //记录读取进度{中文按两单位计算}
            int pos = 0;    //记录截取位置{中文按两单位计算}
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (byteCount >= len)   //到达指定长度时，记录指针位置并停止
                    {
                        pos = i;
                        break;
                    }
                    if (Convert.ToInt32(s.ToCharArray()[i]) > 255)  //遇中文字符计数加2
                        byteCount += 2;
                    else         //按英文字符计算加1
                        byteCount += 1;

                }
                if (pos == 0)
                {
                    pos = len / 2;
                }
                result = s.Substring(0, pos) + "...";
            }
            else
                result = s;

            return result;
        }
        /// <summary>
        /// 格式化日期时间
        /// </summary>
        public static string FormatDateTime(object obj, string format)
        {
            try
            {
                return Convert.ToDateTime(obj).ToString(format);
            }
            catch
            { return ""; }
        }
        /// <summary>
        /// 格式化“url”
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FormatUrl(string url)
        {
            url = url.Replace("%", "&");
            url = url.Replace("$", "?");
            return url;
        }
        #endregion 字符串控制函数

        #region 系统常用配置

        /// <summary>
        /// 执业执业资格类别
        /// </summary>
        public static class EnumRylb
        {
            public const string 施工类 = "SGL";
            public const string 监理类 = "JLL";
        }

        /// <summary>
        /// 施工企业 相关图片目录 路径
        /// 如果施工企业图片 位置发生变化 只要修改此处即可
        /// </summary>
        public static string GetRyxx_SglImgUrl()
        {
            //2013-04-29 统一库中人员照片与旧信用手册系统共享
            //return "/SgqyGl/Pages/FileUpload/";
            return "/AllFiles/SgqyUpFiles/oldFiles/";
        }
        /// <summary>
        /// 预选承包商附件
        /// </summary>
        /// <returns></returns>
        public static string GetYxcbs_FjUrl()
        {
            return "http://218.4.45.169";
        }
        /// <summary>
        /// 预选承包商业绩管理附件
        /// </summary>
        /// <returns></returns>
        public static string GetYxcbsYj_FjUrl()
        {
            return "http://218.4.45.169";
        }
        #endregion


        public static void GenerateSearchClauseAndSPC(Control cl, ref string strSqlCondition, Bigdesk8.Data.SqlParameterCollection spc)
        {
            List<IDataItem> list = cl.GetControlValue();
            list.GetSearchClause(spc, ref strSqlCondition);

        }
    }
}
