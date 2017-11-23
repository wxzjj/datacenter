using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace IntegrativeShow2.Common
{
    public static class Public
    {
        public static string logPath = System.Windows.Forms.Application.StartupPath + "\\无锡数据中心定时服务Log\\";
        
        public static void WriteLog(string msg)
        {
            string isWriteLog = ConfigurationManager.AppSettings["IsWriteLog"];
            if (!String.IsNullOrEmpty(isWriteLog) && (isWriteLog.Equals("1") || isWriteLog.Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string fileFullName = logPath+"Log_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                try
                {
                    if (!File.Exists(fileFullName))
                    {
                        File.WriteAllText(fileFullName, msg + "\r\n", Encoding.UTF8);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(fileFullName))
                        {
                            sw.WriteLine(msg);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        public static void WriteLog(string fileName,string msg)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                WriteLog(msg);
                return ;
            }
            string isWriteLog = ConfigurationManager.AppSettings["IsWriteLog"];

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            string fileFullName = logPath + fileName + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

            if (!String.IsNullOrEmpty(isWriteLog) && (isWriteLog.Equals("1") || isWriteLog.Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    if (!File.Exists(fileFullName))
                    {
                        File.WriteAllText(fileFullName, msg + "\r\n", Encoding.UTF8);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(fileFullName))
                        {
                            sw.WriteLine(msg);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        public static string ShxydmToZzjgdm(string shxydm)
        {
            if (string.IsNullOrEmpty(shxydm))
                return String.Empty;

            //if (shxydm.Length == 9)
            //    return shxydm.Substring(0, 8) + "-" + shxydm.Substring(8,1);

            if (shxydm.Length != 18)
                return shxydm;
            return shxydm.Substring(8, 8) + "-" + shxydm.Substring(16, 1);
              
        }
        /// <summary>
        /// 把九位组织机构代码转化为标准的十位组织机构代码
        /// </summary>
        /// <param name="zzjgdm"></param>
        /// <returns></returns>
        public static string ZzjgdmToStandard(string zzjgdm)
        {
            if (string.IsNullOrEmpty(zzjgdm))
                return String.Empty;

            if (zzjgdm.Length == 9)
            {
                return zzjgdm.Substring(0, 8) +"-" + zzjgdm.Substring(8, 1);
            }
            else
            {
                return zzjgdm;
            }
        }

    }
}
