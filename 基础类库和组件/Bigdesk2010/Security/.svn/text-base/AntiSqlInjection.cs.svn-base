using System;
using System.Collections.Generic;
using System.Text;

namespace Bigdesk2010.Security
{
    /// <summary>
    /// AntiSqlInjection 的摘要说明。
    /// </summary>
    public class AntiSqlInjection
    {
        /// <summary>
        /// 过滤危险 SQL
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static string GetSafeSql(string sqlString)
        {
            if (sqlString == null) return sqlString;
            string localString = sqlString.Trim().ToLower();

            //if (localString.LastIndexOf("delete") > 0)
            //{
            //    // ??? 如果中间有 delete 等关键字,并非是最后一个???
            //    if (localString.LastIndexOf("delete") - (localString.LastIndexOf("_delete")) != 1)
            //        throw new Exception("Delete_ isnot the first word!");
            //}


            if (localString.IndexOf("delete ", 1) > 0)  
                throw new Exception("Delete_ isnot the first word!");

            if (localString.IndexOf("update ", 1) > 0)
                throw new Exception("Update_ isnot the first word!");

            if (localString.IndexOf("insert ", 1) > 0)
                throw new Exception("Insert_ isnot the first word!");

            if (localString.IndexOf("execute ", 1) > 0)
                throw new Exception("execute_ isnot the first word!");

            if (localString.IndexOf("exec ", 1) > 0)
                throw new Exception("exec_ isnot the first word!");

            if (localString.IndexOf("create ", 1) > 0)
                throw new Exception("Create_ isnot the first word!");

            if (localString.IndexOf("drop ", 1) > 0)
                throw new Exception("Drop_ isnot the first word!");

            if ((localString.IndexOf(" dt_", 1) >= 0)
                || (localString.IndexOf(" ms_", 1) >= 0)
                || (localString.IndexOf(" sp_", 1) >= 0)
                || (localString.IndexOf(" xp_", 1) >= 0))
                throw new Exception("Systemp procedure isnot permitted!");

            return sqlString;
        }

        /// <summary>
        /// 为防止sql注入，对用户的输入进行安全检查。
        /// 1. 如果试图sql注入，则输入的字符串中间必然含有空格
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string GetSafeInput(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return inputString;
            string localString = inputString.Trim().ToLower();

            if (localString.IndexOf(" ") > 0)
                throw new Exception("用户输入“" + inputString + "”中含有非法的空格！");

            return inputString;
        }

        /// <summary>
        /// 将sql语句in子句中以逗号分隔的字符串，转换成sql参数的形式
        /// </summary>
        /// <param name="inClause">sql语句in子句中以逗号分隔的字符串</param>
        /// <param name="preOfParam">参数名称的前缀，需要加“@”或者“:”符号</param>
        /// <param name="sp">传入的SqlParameterCollection对象</param>
        /// <returns>形成的sql参数的字符串</returns>
        public static string ParameterizeInClause(string inClause, string preOfParam, ref Bigdesk2010.Data.SqlParameterCollection sp)
        {
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(inClause))
                return sql.ToString();
            string[] paraArr = inClause.Trim(',').Split(',');
            string paraName = String.Empty;
            for (int i = 0; i < paraArr.Length; i++)
            {
                paraName = string.Format("{0}{1}", preOfParam, i);
                sp.Add(paraName, paraArr[i]);
                if (i == 0)
                {
                    sql.Append(paraName);
                }
                else
                {
                    sql.AppendFormat(",{0}", paraName);
                }

            }
            return sql.ToString();

        }
    }
}
