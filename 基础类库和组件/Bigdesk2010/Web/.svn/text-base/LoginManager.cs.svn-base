using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;

using Bigdesk2010.Business;
using Bigdesk2010.Data;

namespace Bigdesk2010.Web
{
    /// <summary>
    /// 为web应用程序管理登录信息
    /// </summary>
    public static class LoginManager
    {
        private const string KeyNameOfLoginID = "LoginID";

        //虽然LoginID,UserID的内容可能完全一样，但我们分开定义，以使得登录LoginID、和ScicLoginInfo的使用不要纠缠在一起
        private const string KeyNameOfUserID = "UserID";
        private const string KeyNameOfLoginName = "LoginName";
        private const string KeyNameOfUserName = "UserName";
        private const string KeyNameOfOrgID = "OrgID";
        private const string KeyNameOfUserType = "UserType";
        private const string KeyNameOfLoginType = "LoginType";
        private const string KeyNameOfSelectableLoginTypes = "SelectableLoginTypes";
        //张鎏  添加
        private const string KeyNameOfOrgUnitName = "OrgUnitName";

        private const string CookieNameOfScicLoginInfo = "ScicLoginCookie";
        private const string SessionNameOfScicLoginInfo = "ScicLoginSession";

        private const string CookieNameOfBasicLoginInfo = "BasicLoginCookie";
        private const string SessionNameOfBasicLoginInfo = "BasicLoginSession";

        /// <summary>
        /// 初始化一个SCIC平台用户
        /// </summary>
        public static ResultOfInitScicLogin InitScicLogin(DBOperator dbOperator, int userID)
        {
            ResultOfInitScicLogin myResult = new ResultOfInitScicLogin();
            Bigdesk2010.Business.UserRightManager.IUserRightInfo ur = new Bigdesk2010.Business.UserRightManager.UserRightInfo(dbOperator, userID);
            if (ur.CountOfUserMatched < 1)
            {
                myResult.ResultCode = ResultCodeOfInitScicLogin.用户不存在;
                myResult.Message = "用户不存在！";
                return myResult;
            }
            else if (ur.CountOfUserMatched > 1)
            {
                myResult.ResultCode = ResultCodeOfInitScicLogin.用户重复;
                myResult.Message = "用户重复，请与系统管理员联系！";
                return myResult;
            }
            myResult.ResultCode = ResultCodeOfInitScicLogin.唯一用户;

            ScicLoginInfo workUser = new ScicLoginInfo();
            workUser.UserID = ur.UserID;
            workUser.LoginName = ur.LoginName;
            workUser.UserName = ur.UserName;
            workUser.UserType = ur.UserType;
            workUser.LoginType = ScicLoginType.未定义;
            workUser.OrgID = string.Empty;
            workUser.OrgUnitName = ur.OrgUnitName;

            List<ScicLoginType> selectableLoginTypes = new List<ScicLoginType>();
            string sql = string.Empty;
            SqlParameterCollection spc = dbOperator.CreateSqlParameterCollection();
            switch (workUser.UserType)
            {
                case BasicUserType.建设单位:
                    sql = "select jsdwid from uepp_jsdw where userid = @UserID";
                    spc.Add("@UserID", workUser.UserID);
                    DataTable dtJsdw = dbOperator.ExeSqlForDataTable(sql, spc, "uepp_jsdw");
                    if (dtJsdw.Rows.Count == 1)
                    {
                        workUser.OrgID = dtJsdw.Rows[0]["jsdwid"].ToString();
                        selectableLoginTypes.Add(ScicLoginType.建设单位用户);
                    }
                    else if (dtJsdw.Rows.Count > 1)
                    {
                        myResult.ResultCode = ResultCodeOfInitScicLogin.基础数据异常;
                        myResult.Message = "建设单位表中用户编号(" + ur.UserID.ToString() + ")重复";
                        return myResult;
                    }
                    else
                    {
                        myResult.ResultCode = ResultCodeOfInitScicLogin.基础数据异常;
                        myResult.Message = "建设单位表中用户编号(" + ur.UserID.ToString() + ")不存在";
                        return myResult;
                    }
                    break;
                case BasicUserType.管理用户:
                    selectableLoginTypes.Add(ScicLoginType.管理用户);
                    break;
                case BasicUserType.经办人:
                    selectableLoginTypes.Add(ScicLoginType.经办人);
                    break;
                case BasicUserType.企业用户:
                    sql = @"select b.* 
                            from uepp_qyjbxx a, uepp_qycsyw b
                            where a.userid = @UserID
                            and   a.qyid = b.qyid";
                    spc.Add("@UserID", workUser.UserID);
                    DataTable dtQyywlx = dbOperator.ExeSqlForDataTable(sql, spc, "uepp_qycsyw");
                    if (dtQyywlx.Rows.Count > 0)
                    {
                        workUser.OrgID = dtQyywlx.Rows[0]["qyid"].ToString();
                        foreach (DataRow dr in dtQyywlx.Rows)
                        {
                            QyYwlxCode localYwlx = (QyYwlxCode)Enum.Parse(typeof(QyYwlxCode), dr["csywlxid"].ToString());
                            switch (localYwlx)
                            {
                                case QyYwlxCode.建筑施工:
                                case QyYwlxCode.设计施工一体化:
                                case QyYwlxCode.园林绿化:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.施工企业用户)) selectableLoginTypes.Add(ScicLoginType.施工企业用户);
                                    break;
                                case QyYwlxCode.工程监理:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.监理企业用户)) selectableLoginTypes.Add(ScicLoginType.监理企业用户);
                                    break;
                                case QyYwlxCode.招标代理:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.代理机构用户)) selectableLoginTypes.Add(ScicLoginType.代理机构用户);
                                    break;
                                case QyYwlxCode.工程勘察:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.勘察企业用户)) selectableLoginTypes.Add(ScicLoginType.勘察企业用户);
                                    break;
                                case QyYwlxCode.工程设计:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.设计企业用户)) selectableLoginTypes.Add(ScicLoginType.设计企业用户);
                                    break;
                                case QyYwlxCode.造价咨询:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.造价咨询用户)) selectableLoginTypes.Add(ScicLoginType.造价咨询用户);
                                    break;
                                case QyYwlxCode.工程检测:
                                    if (!selectableLoginTypes.Contains(ScicLoginType.检测机构用户)) selectableLoginTypes.Add(ScicLoginType.检测机构用户);
                                    break;
                            }
                        }
                    }
                    break;
            }

            workUser.SelectableLoginTypes = selectableLoginTypes;
            if (selectableLoginTypes.Count == 1)
                workUser.LoginType = selectableLoginTypes[0];

            myResult.WorkUser = workUser;
            return myResult;
        }

        /// <summary>
        /// 初始化一个应用系统用户
        /// </summary>
        public static ResultOfInitBasicLogin InitBasicLogin(DBOperator dbOperator, int userID)
        {
            ResultOfInitBasicLogin myResult = new ResultOfInitBasicLogin();
            Bigdesk2010.Business.UserRightManager.IUserRightInfo ur = new Bigdesk2010.Business.UserRightManager.UserRightInfo(dbOperator, userID);
            if (ur.CountOfUserMatched < 1)
            {
                myResult.ResultCode = ResultCodeOfInitBasicLogin.用户不存在;
                myResult.Message = "用户不存在！";
                return myResult;
            }
            else if (ur.CountOfUserMatched > 1)
            {
                myResult.ResultCode = ResultCodeOfInitBasicLogin.用户重复;
                myResult.Message = "用户重复，请与系统管理员联系！";
                return myResult;
            }
            myResult.ResultCode = ResultCodeOfInitBasicLogin.唯一用户;

            BasicLoginInfo workUser = new BasicLoginInfo();
            workUser.UserID = ur.UserID;
            workUser.LoginName = ur.LoginName;
            workUser.UserName = ur.UserName;
            workUser.UserType = ur.UserType;
            workUser.OrgUnitName = ur.OrgUnitName;

            myResult.WorkUser = workUser;
            return myResult;
        }

        public static void SetLoginID(string loginID)
        {
            if (GetAuthenticationMode() == "Forms")
            {
                /* 
                 * 下面的方法，涉及到对用户认证信息的加密，而在一个网站应用程序环境下（如.net 3.5)的加密信息，
                 * 到了另一个应用程序环境下（如.net 4.0)不一定能正确解密。从而无法共享用户信息。
                 * 已知这种情况出现
                 * 一，windows server 2008上。这是由于安全漏洞引起的，及时更新安全补丁后问题得以解决。
                 */
                FormsAuthentication.SetAuthCookie(loginID, true);

                /*
                 * 为保证用户登录的loginID在网站的各应用程序中能够顺利共享，我们干脆将loginID在cookie中直接存一份。
                 */
                HttpCookie cookie = new HttpCookie(KeyNameOfLoginID, loginID);
                HttpContext.Current.Response.AppendCookie(cookie);

                //为了能将CSSgxcAqjd(常熟安监)等系统融合进来，达成单点登录的效果，故有下面这句代码。
                //随着CSSgxcAqjd(常熟安监)等系统的自然消亡，应适时将这句代码删除！！！
                HttpContext.Current.Session["LOGINID"] = loginID;
            }
            else
            {
                HttpContext.Current.Session[KeyNameOfLoginID] = loginID;
            }
        }

        public static string GetLoginID()
        {
            string loginID = string.Empty;

            if (!HttpContext.Current.Session[KeyNameOfLoginID].IsEmpty()) //如果用户已经以session方式登录
            {
                loginID = HttpContext.Current.Session[KeyNameOfLoginID].ToString();
            }
            else if (HttpContext.Current.User.Identity.AuthenticationType == "Forms" && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                loginID = HttpContext.Current.User.Identity.Name;
            }

            //最后再尝试从cookie中取一下用户登录ID
            if (string.IsNullOrEmpty(loginID) && HttpContext.Current.Request.Cookies[KeyNameOfLoginID] != null)
            {
                loginID = HttpContext.Current.Request.Cookies[KeyNameOfLoginID].Value;
            }
            return loginID;
        }

        public static void SignOut()
        {
            HttpContext.Current.Session[KeyNameOfLoginID] = null;
            HttpCookie cookie = new HttpCookie(KeyNameOfLoginID, string.Empty);
            HttpContext.Current.Response.AppendCookie(cookie);
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SetScicLoginInfo(ScicLoginInfo loginInfo)
        {
            if (GetAuthenticationMode() == "Forms")
            {
                HttpCookie scicLoginCookie = new HttpCookie(CookieNameOfScicLoginInfo);
                scicLoginCookie.Expires = DateTime.Now.AddDays(1);  //设置Cookie超时时间
                scicLoginCookie[KeyNameOfUserID] = loginInfo.UserID.ToString();
                scicLoginCookie[KeyNameOfLoginName] = loginInfo.LoginName;
                scicLoginCookie[KeyNameOfUserName] = HttpUtility.UrlEncode(loginInfo.UserName);//因为cookie里中文字符在浏览器和web server间直接传递时，会变成乱码，故需要Encode一下
                scicLoginCookie[KeyNameOfOrgID] = loginInfo.OrgID;
                scicLoginCookie[KeyNameOfUserType] = HttpUtility.UrlEncode(loginInfo.UserType.ToString());
                scicLoginCookie[KeyNameOfLoginType] = HttpUtility.UrlEncode(loginInfo.LoginType.ToString());
                string strSelectableLogintTypes = loginInfo.SelectableLoginTypes[0].ToString();
                for (int i = 1; i < loginInfo.SelectableLoginTypes.Count; i++)
                    strSelectableLogintTypes += "," + loginInfo.SelectableLoginTypes[i].ToString();
                scicLoginCookie[KeyNameOfSelectableLoginTypes] = HttpUtility.UrlEncode(strSelectableLogintTypes);

                //张鎏  添加
                scicLoginCookie[KeyNameOfOrgUnitName] = HttpUtility.UrlEncode(loginInfo.OrgUnitName);
                HttpContext.Current.Response.AppendCookie(scicLoginCookie);
            }
            else
            {
                HttpContext.Current.Session[SessionNameOfScicLoginInfo] = loginInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearScicLoginInfo()
        {
            if (GetAuthenticationMode() == "Forms")
            {
                HttpCookie scicLoginCookie = new HttpCookie(CookieNameOfScicLoginInfo);
                scicLoginCookie.Expires = DateTime.Now.AddDays(-1);  //设置Cookie超时时间为-1，以使cookie失效，从而达到清空cookie的效果

                HttpContext.Current.Response.AppendCookie(scicLoginCookie);
            }
            else
            {
                HttpContext.Current.Session[SessionNameOfScicLoginInfo] = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SetBasicLoginInfo(BasicLoginInfo loginInfo)
        {
            if (GetAuthenticationMode() == "Forms")
            {
                HttpCookie basicLoginCookie = new HttpCookie(CookieNameOfBasicLoginInfo);
                basicLoginCookie.Expires = DateTime.Now.AddDays(1);  //设置Cookie超时时间
                basicLoginCookie[KeyNameOfUserID] = loginInfo.UserID.ToString();
                basicLoginCookie[KeyNameOfLoginName] = HttpUtility.UrlEncode(loginInfo.LoginName);
                basicLoginCookie[KeyNameOfUserName] = HttpUtility.UrlEncode(loginInfo.UserName);//因为cookie里中文字符在浏览器和web server间直接传递时，会变成乱码，故需要Encode一下
                basicLoginCookie[KeyNameOfUserType] = HttpUtility.UrlEncode(loginInfo.UserType.ToString());
                basicLoginCookie[KeyNameOfOrgUnitName] = HttpUtility.UrlEncode(loginInfo.OrgUnitName);
                HttpContext.Current.Response.AppendCookie(basicLoginCookie);
            }
            else
            {
                HttpContext.Current.Session[SessionNameOfBasicLoginInfo] = loginInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearBasicLoginInfo()
        {
            if (GetAuthenticationMode() == "Forms")
            {
                HttpCookie basicLoginCookie = new HttpCookie(CookieNameOfBasicLoginInfo);
                basicLoginCookie.Expires = DateTime.Now.AddDays(-1);  //设置Cookie超时时间为-1，以使cookie失效，从而达到清空cookie的效果

                HttpContext.Current.Response.AppendCookie(basicLoginCookie);
            }
            else
            {
                HttpContext.Current.Session[SessionNameOfBasicLoginInfo] = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static ScicLoginInfo GetScicLoginInfo()
        {
            if (HttpContext.Current.User.Identity.AuthenticationType == "Forms" && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //因为cookie是全网站公用的，故首先看cookie
                ScicLoginInfo loginInfo = new ScicLoginInfo();
                HttpCookie scicLoginCookie = HttpContext.Current.Request.Cookies[CookieNameOfScicLoginInfo];
                if (!scicLoginCookie.IsEmpty())
                {
                    loginInfo.UserID = scicLoginCookie[KeyNameOfUserID].ToInt32();
                    loginInfo.LoginName = HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfLoginName]);
                    loginInfo.UserName = HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfUserName]);
                    loginInfo.OrgID = scicLoginCookie[KeyNameOfOrgID];
                    //张鎏  添加
                    loginInfo.OrgUnitName = HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfOrgUnitName]);
                    loginInfo.UserType = (BasicUserType)Enum.Parse(typeof(BasicUserType), HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfUserType]));
                    loginInfo.LoginType = (ScicLoginType)Enum.Parse(typeof(ScicLoginType), HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfLoginType]));
                    string[] arraySelectableLoginTypes = HttpUtility.UrlDecode(scicLoginCookie[KeyNameOfSelectableLoginTypes]).Split(new char[] { ',' });
                    loginInfo.SelectableLoginTypes = new List<ScicLoginType>();
                    foreach (string strLoginType in arraySelectableLoginTypes)
                        loginInfo.SelectableLoginTypes.Add((ScicLoginType)Enum.Parse(typeof(ScicLoginType), strLoginType));

                    return loginInfo;
                }
            }
            else if (!HttpContext.Current.Session[SessionNameOfScicLoginInfo].IsEmpty()) //如果没有用cookie,则从session中查找
            {
                return (ScicLoginInfo)HttpContext.Current.Session[SessionNameOfScicLoginInfo];
            }

            return null;    //"未取到登录信息
        }

        /// <summary>
        /// 
        /// </summary>
        public static BasicLoginInfo GetBasicLoginInfo()
        {
            if (HttpContext.Current.User.Identity.AuthenticationType == "Forms" && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //因为cookie是全网站公用的，故首先看cookie
                BasicLoginInfo loginInfo = new BasicLoginInfo();
                HttpCookie basicLoginCookie = HttpContext.Current.Request.Cookies[CookieNameOfBasicLoginInfo];
                if (!basicLoginCookie.IsEmpty())
                {
                    loginInfo.UserID = basicLoginCookie[KeyNameOfUserID].ToInt32();
                    loginInfo.LoginName = basicLoginCookie[KeyNameOfLoginName];
                    loginInfo.UserName = HttpUtility.UrlDecode(basicLoginCookie[KeyNameOfUserName]);
                    loginInfo.OrgUnitName = HttpUtility.UrlDecode(basicLoginCookie[KeyNameOfOrgUnitName]);
                    loginInfo.UserType = (BasicUserType)Enum.Parse(typeof(BasicUserType), HttpUtility.UrlDecode(basicLoginCookie[KeyNameOfUserType]));
                    return loginInfo;
                }
            }
            else if (!HttpContext.Current.Session[SessionNameOfScicLoginInfo].IsEmpty()) //如果没有用cookie,则从session中查找
            {
                return (BasicLoginInfo)HttpContext.Current.Session[SessionNameOfBasicLoginInfo];
            }

            return null;    //"未取到登录信息
        }

        private static string GetAuthenticationMode()
        {
            XmlDocument doc = new XmlDocument();

            if (File.Exists(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config")))
            {
                //首先试着读取应用程序根目录下的web.config中的authentication配置
                doc.Load(System.IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                XmlNode node = doc.SelectSingleNode("configuration/system.web/authentication/@mode");
                if (node != null) return node.Value;
            }
            else if (File.Exists(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "../web.config")))
            {
                //若应用程序以虚拟目录方式部署，则其根目录的父目录应该就是网站根目录。接着尝试网站根目录下的web.config中的authentication配置
                doc.Load(System.IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "../web.config"));
                XmlNode node = doc.SelectSingleNode("configuration/system.web/authentication/@mode");
                if (node != null) return node.Value;
            }
            return string.Empty;
        }
    }
}
