using System;
using System.Configuration;
using System.Data;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy
{
    public static class ConfigManager
    {
        /// <summary>
        /// 功能树配置文件URL（绝对虚拟路径），必须以“/”开头。
        /// </summary>
        public static string GetTreeConfigFileUrl()
        {
            return GetConfig("TreeConfigFileUrl").ToString();
        }

        /// <summary>
        /// 以“/”开头和结尾的SparkClient目录的绝对虚拟路径。
        /// </summary>
        public static string GetSparkClientPath()
        {
            return GetConfig("SparkClientPath").ToString();
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
        /// 系统登录用户信息（一个结构体）会话键名，用此键名从 Session 中获取登录用户的用户信息结构体
        /// </summary>
        public static string GetSignInAppUserSessionName()
        {
            return GetConfig("SignInAppUserSessionName").ToString();
        }

        /// <summary>
        /// 获取登录页面URL
        /// </summary>
        public static string GetLoginPageUrl()
        {
            return GetConfig("LoginPageUrl").ToString();
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

        public static string GetDatabaseType_Sqlserver()
        {
            return GetConfig("DatabaseType_Sqlserver").ToString();
        }

        /// <summary>
        /// 数据库连接字符串，如data source=.;user id=sa;password=1;database=SCIC82。
        /// </summary>
        public static string GetConnectionString()
        { 
            DecryptAndEncryptionHelper helper = new DecryptAndEncryptionHelper(ConfigInformation.Key, ConfigInformation.Vector);
            string encrytStr = GetConfig("ConnectionString").ToString();
            return helper.Decrypto(encrytStr);
        }
        /// <summary>
        /// 数据库连接字符串，如data source=.;user id=sa;password=1;database=SCIC82。
        /// </summary>
        public static string GetConnectionString_Sqlserver()
        {
            DecryptAndEncryptionHelper helper = new DecryptAndEncryptionHelper(ConfigInformation.Key, ConfigInformation.Vector);
            string encrytStr = GetConfig("ConnectionString_Sqlserver").ToString();
            return helper.Decrypto(encrytStr);
        }

        public static string GetConnectionString_YHTSqlserver()
        {
            return GetConfig("ConnectionString_YHTSqlserver").ToString();
        }

       
        public static string GetCqfxConnectionString()
        {
            return GetConfig("CqfxConnectionString").ToString();
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
        /// 上传文件的最大块大小，默认值为524288=512KB
        /// </summary>
        public static long GetUpFileMaxChunkSize()
        {
            return GetConfig("UpFileMaxChunkSize").ToInt64(5242880);
        }

        /// <summary>
        /// 上传文件的最大允许大小，默认值为1073741824=1G
        /// </summary>
        public static long GetUpFileMaxSize()
        {
            return GetConfig("UpFileMaxSize").ToInt64(1073741824);
        }

        /// <summary>
        /// 上传文件的链接，默认值为http://localhost:4242/Majordomo/UploadHandler/FileUploader.aspx
        /// </summary>
        public static string GetFileUploaderUrl()
        {
            return GetConfig("FileUploaderUrl").ToString("http://localhost:4242/Majordomo/UploadHandler/FileUploader.aspx");
        }

        /// <summary>
        /// 查看上传文件的链接
        /// </summary>
        public static string GetFileViewerUrl()
        {
            return GetConfig("FileViewerUrl").ToString("http://localhost:4242/Majordomo/UploadHandler/FileListView.aspx");
        }

        /// <summary>
        /// 获取使用LigerUI插件的主题名称
        /// </summary>
        public static string GetThemeInUsing()
        {
            return GetConfig("ThemeInUsing").ToString();
        }
        /// <summary>
        /// 不用LigerUI插件所用的主题名称
        /// </summary>
        /// <returns></returns>
        public static string GetTheme2InUsing()
        {
            return GetConfig("Theme2InUsing").ToString();
        }
        /// <summary>
        /// 调用服务的用户名
        /// </summary>
        /// <returns></returns>
        public static string GetServiceUserName()
        {
            return GetConfig("ServiceUserName").ToString();
        }
        /// <summary>
        /// 调用服务的密码
        /// </summary>
        /// <returns></returns>
        public static string GetServicePassword()
        {
            return GetConfig("ServicePassword").ToString();
        }
        /// <summary>
        /// 获取用户登录次数超限锁定时间(分钟)
        /// </summary>
        /// <returns></returns>
        public static int GetLoginErrorWait()
        {
            return GetConfig("LoginErrorWait").ToString().ToInt32(15);
        }
        /// <summary>
        /// 获取允许用户登录出错次数
        /// </summary>
        /// <returns></returns>
        public static int GetAllowLoginCount()
        {
            return GetConfig("AllowLoginCount").ToString().ToInt32(5);
        }
        /// <summary>
        /// 获取验证码的Session名称
        /// </summary>
        /// <returns></returns>
        public static string GetVerificationCode_SessionName()
        {
            return GetConfig("VerificationCode_SessionName").ToString();
        }

        /// <summary>
        /// 获取在局OA系统注册时分配的系统验证账户Name
        /// </summary>
        /// <returns></returns>
        public static string GetValidateName()
        {
            return GetConfig("ValidateName").ToString();
        }
        /// <summary>
        /// 获取在局OA系统注册时分配的系统验证码Pass
        /// </summary>
        /// <returns></returns>
        public static string GetValidatePass()
        {
            return GetConfig("ValidatePass").ToString();
        }


        /// <summary>
        /// 获取配置文件的物理路径
        /// </summary>
        private static string GetConfigFilePath()
        {
           string relativePathOfConfigFile = ConfigurationManager.AppSettings["GreenConProject_RelativePathOfConfigFile"];
            // string relativePathOfConfigFile = ConfigurationManager.AppSettings["WJSTyxmdjSys_RelativePathOfConfigFile"];
            string filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePathOfConfigFile.Replace("/", @"\"));

            //if (!System.IO.File.Exists(filename))
            //{
            //    throw new Exception("发生错误: 找不到配置文件" + filename);
            //}

            return filename;
        }

        /// <summary>
        /// 缓存对象前缀
        /// </summary>
        private const string CacheObjectPrefix_Config = "Wxjzgcjczy";

        /// <summary>
        /// 获取配置文件中配置项的值
        /// </summary>
        /// <param name="configName">配置项名称</param>
        /// <returns></returns>
        private static object GetConfig(string configName)
        {
            string objId = string.Format("{0}.Config", CacheObjectPrefix_Config);
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
