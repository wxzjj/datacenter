using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.BLL
{
    public static class Validator
    {

        #region  18位统一社会信用代码或者组织机构验证
        /// <summary>
        /// 验证输入字符串为 统一社会信用代码或者组织机构
        /// </summary>
        /// <param name="code">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsUnifiedSocialCreditCodeOrOrgCode(string code)
        {
            return IsUnifiedSocialCreditCode(code) || IsOrganizationCode(code);
        }
        #endregion


        #region  18位统一社会信用代码验证
        /// <summary>
        /// 验证输入字符串为 统一社会信用代码
        /// </summary>
        /// <param name="code">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsUnifiedSocialCreditCode(string code)
        {
            return Regex.IsMatch(code, "^[0-9A-HJ-NPQRTUWXY]{2}\\d{6}[0-9A-HJ-NPQRTUWXY]{10}$");
        }
        #endregion

        #region  组织机构代码验证:XXXXXXXX-X
        /// <summary>
        /// 验证输入字符串为 统一社会信用代码
        /// </summary>
        /// <param name="code">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsOrganizationCode(string code)
        {
            return Regex.IsMatch(code, "^[a-zA-Z0-9]{8}-[a-zA-Z0-9]");
        }
        #endregion

        #region  项目编号验证:16位纯数字编码
        /// <summary>
        /// 验证输入字符串为16位纯数字编码的项目编号
        /// </summary>
        /// <param name="projectNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsProjectNum(string projectNum)
        {
            return Regex.IsMatch(projectNum, "^[\\d]{16}");
        }
        #endregion
    }
}
