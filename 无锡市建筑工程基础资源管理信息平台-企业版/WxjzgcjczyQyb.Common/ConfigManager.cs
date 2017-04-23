using System;
using System.Configuration;
using System.Data;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;

namespace WxjzgcjczyQyb
{
    public static class ConfigManager
    {

        /// <summary>
        /// 功能树配置文件URL，必须以“/”开头。
        /// </summary>
        public static string GetTreeConfigMFileUrl()
        {
            return GetConfig("TreeConfigMFileUrl").ToString();
        }
        public static string GetTreeConfigEFileUrl()
        {
            return GetConfig("TreeConfigEFileUrl").ToString();
        }

        /// <summary>
        /// 获取根目录
        /// </summary>
        public static string GetRootUrl()
        {
            return GetConfig("RootUrl").ToString();
        }

        /// <summary>
        /// 获取当前系统的标识
        /// </summary>
        /// <returns></returns>
        public static string GetSystemCode()
        {
            return GetConfig("SystemCode").ToString();
        }
        /// <summary>
        /// 获取当前系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetSystemName()
        {
            return GetConfig("SystemName").ToString();
        }

        /// <summary>
        /// 是否启用调试功能，以输出详细的异常信息
        /// </summary>
        public static bool HaveDebugBeenTurnedOn()
        {
            return GetConfig("TurnOnDebug").ToBoolean(true);
        }

        /// <summary>
        /// 是否开启缓存功能
        /// </summary>
        public static bool HaveCacheBeenTurnedOn()
        {
            return GetConfig("TurnOnCache").ToBoolean(false);
        }

        /// <summary>
        /// 是否开启事务执行功能
        /// </summary>
        public static bool HaveTransactionBeenTurnedOn()
        {
            return GetConfig("TurnOnTransaction").ToBoolean(false);
        }

        /// <summary>
        /// 获取登录页面URL
        /// </summary>
        public static string GetLoginPageUrl()
        {
            return GetConfig("LoginPageUrl").ToString();
        }

        /// <summary>
        /// 获取重新登录页面URL
        /// </summary>
        public static string GetReloginPageUrl()
        {
            return GetConfig("ReloginPageUrl").ToString();
        }

        /// <summary>
        /// 获取修改密码页面URL
        /// </summary>
        public static string GetModifyPasswordPageUrl()
        {
            return GetConfig("ModifyPasswordPageUrl").ToString();
        }

        /// <summary>
        /// 数据库类型，如sqlserver2000，sqlserver2005。
        /// </summary>
        public static string GetDatabaseType()
        {
            return GetConfig("DatabaseType").ToString();
        }

        /// <summary>
        /// 数据库连接字符串 
        /// </summary>
        public static string GetConnectionString()
        {
            return GetConfig("ConnectionString").ToString();
        }

        /// <summary>
        /// 数据库连接字符串 
        /// </summary>
        public static string GetConnectionString_Webplat50()
        {
            return GetConfig("ConnectionString_webplat50").ToString();
        }

        /// <summary>
        /// 报表服务器URL
        /// </summary>
        public static string GetReportServerUrl()
        {
            return GetConfig("ReportServerUrl").ToString();
        }

        /// <summary>
        /// 报表服务根URL，必须以“/”开头和结尾
        /// </summary>
        public static string GetReportRootPath()
        {
            return GetConfig("ReportRootPath").ToString();
        }

        /// <summary>
        /// 是否开启大文件上传控件的调试功能
        /// </summary>
        public static bool DoesDebugBigFileUpload()
        {
            return GetConfig("DebugBigFileUpload").ToBoolean(false);
        }

        /// <summary>
        /// 获取使用的主题名称B
        /// </summary>
        public static string GetThemeInUsingB()
        {
            return GetConfig("ThemeInUsingB").ToString("WxjzgcjczyQyb_B_Theme");
        }
        /// <summary>
        /// 系统登录用户信息（一个结构体）会话键名，用此键名从 Session 中获取登录用户的用户信息结构体
        /// </summary>
        public static string GetSignInAppUserSessionName()
        {
            return GetConfig("SignInAppUserSessionName").ToString();
        }
        /// <summary>
        /// 上传文件数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetUpFileConnectionString()
        {
            return GetConfig("UpFileConnectionString").ToString();
        }

        /// <summary>
        /// 上传文件的最大块大小，默认值为500000=500KB
        /// </summary>
        public static long GetUpFileMaxChunkSize()
        {
            return GetConfig("UpFileMaxChunkSize").ToInt64(500000);
        }

        /// <summary>
        /// 上传文件的最大允许大小，默认值为1073741824=1G
        /// </summary>
        public static long GetUpFileMaxSize()
        {
            return GetConfig("UpFileMaxSize").ToInt64(1073741824);
        }

        /// <summary>
        /// 获取配置文件的物理路径
        /// </summary>
        /// 此处不把配置文件的路径定义为url，并用server.mappath(url)来获取物理路径的原因是：
        /// server.mappath是一个asp.net的功能，而我们期望Common项目可直接在c/s,b/s下使用。
        private static string GetConfigFilePath()
        {
            string relativePathOfConfigFile = ConfigurationManager.AppSettings["WxjzgcjczyQyb_RelativePathOfConfigFile"];

            string filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePathOfConfigFile.Replace("/", @"\"));

            if (!System.IO.File.Exists(filename))
            {
                throw new Exception("发生错误: 找不到配置文件" + filename);
            }

            return filename;
        }

        /// <summary>
        /// 缓存对象前缀 - 配置文件
        /// </summary>
        private const string CacheObjectPrefix_Config = "gczj___config___";

        /// <summary>
        /// 获取配置文件中配置项的值
        /// </summary>
        /// <param name="configName">配置项名称</param>
        /// <returns></returns>
        private static object GetConfig(string configName)
        {
            string objId = string.Format("{0}Config", CacheObjectPrefix_Config);
            DataTable dt = null;

            ICacheStrategy cache = new DefaultCacheStrategy();
            object obj = cache.RetrieveObject(objId);
            if (obj != null)
            {
                dt = obj as DataTable;
            }

            if (dt == null)
            {
                dt = LoadConfig();

                if (dt.Rows[0]["TurnOnCache"].ToBoolean(true))
                {
                    cache.AddObject(objId, dt);// 写入缓存
                }
            }

            return dt.Rows[0][configName];
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        public static DataTable LoadConfig()
        {
            DataTable tmp = new DataTable();

            string configFilePath = GetConfigFilePath();

            tmp.ReadXml(configFilePath);

            return tmp;
        }

    }

}
