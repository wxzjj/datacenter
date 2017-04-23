using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Bigdesk8.Data;

namespace SparkServiceDesk
{
    public class Ministrant
    {
        public static int PerPage = 15;
        public static int SystemID = 104;
        public static string ModuleCode = "PDA采集信息";
        public static string CategoryCode = "BLXW";

        #region 配置文件
  

        /// <summary>
        /// 连接TCSCIC60字符串
        /// </summary>
        /// <returns></returns>
        public static ConnectionStringSettings TCSCIC60BConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["JGTSCIC82ConnectionString"];
            }
        }
        /// <summary>
        /// 连接TCSCIC60
        /// </summary>
        /// <returns></returns>
        public static DBOperator TCSCIC60ForDBOperator()
        {
            return DBOperatorFactory.GetDBOperator(TCSCIC60BConnectionString);
        }

        /// <summary>
        /// 连接SCIC82字符串
        /// </summary>
        /// <returns></returns>
        public static ConnectionStringSettings SCIC82BConnectionString
        {
            get
            {
               // return ConfigurationManager.ConnectionStrings["PDASCIC82ConnectionString"];
                return ConfigurationManager.ConnectionStrings["PDASCIC82_TempConnectionString"];
                
            }
        }
        /// <summary>
        /// 连接SCIC60
        /// </summary>
        /// <returns></returns>
        public static DBOperator SCIC82ForDBOperator()
        {
            return DBOperatorFactory.GetDBOperator(SCIC82BConnectionString);
     
        }

        #endregion

        #region 公用函数

        /// <summary>
        /// 转换成SQL语句
        /// </summary>
        /// <param name="argA"></param>
        /// <param name="argB"></param>
        /// <param name="argC"></param>
        /// <returns></returns>
        public static string TransToSQL(string[] argA, string[] argB, string[] argC)
        {
            string strSql = string.Empty;
            for (int i = 0; i < argB.Length; i++)
            {
                if ((argB[i].Length > 0) && (argA[i].Length > 0))
                {
                    switch (argC[i].ToString().ToUpper())
                    {
                        case "TB":
                            strSql += " and " + argA[i] + " like '%" + argB[i] + "%'";
                            break;
                        case "CB":
                            strSql += " and " + argA[i] + " = '" + argB[i] + "'";
                            break;
                        case "DTP"://DateTimePicker
                            argB[i] = argB[i].ToString().Substring(0, argB[i].IndexOf(" "));
                            strSql += " and " + argA[i] + " = '" + argB[i] + "'";
                            break;
                        case "DTP_KS"://DateTimePicker开始
                            argB[i] = argB[i].ToString().Substring(0, argB[i].IndexOf(" "));
                            strSql += " and " + argA[i] + " >= '" + argB[i] + "'";
                            break;
                        case "DTP_JS"://DateTimePicker结束
                            argB[i] = argB[i].ToString().Substring(0, argB[i].IndexOf(" "));
                            strSql += " and " + argA[i] + " <= '" + argB[i] + "'";
                            break;

                        case "PROJECT":
                            if (argB[i].ToString() == "未办理")
                            {
                                strSql += " and " + argA[i] + " in(0,1,2,3,5) ";
                            }
                            else if (argB[i].ToString() == "已办理")
                            {
                                strSql += " and " + argA[i] + "=4";
                            }

                            break;
                    }
                }
            }

            return strSql;

        }


        public static string PaginationSQL(int PageNo, string oldSql, string Colm)
        {
            string Sql = string.Empty;

            int m = PerPage;
            int n = PageNo * PerPage;

            Sql = " select * from ("
                + " select top " + m.ToString() + " * from ("
                + " select top " + n.ToString() + " * from (" + oldSql + ")T order by " + Colm + " asc)TT"
                + " order by " + Colm + " desc)TTT order by " + Colm + " asc";

            return Sql;
        }


        //public static string GetNewXmid()
        //{
        //    string xmid = string.Empty;

        //    string tempid = "320585" + DateTime.Now.Year.ToString() + string.Format("{0:D2}", Convert.ToInt32(DateTime.Now.Month.ToString())) + string.Format("{0:D2}", Convert.ToInt32(DateTime.Now.Day.ToString())) + "PDA";
        //    xmid = Ministrant.SCIC82ForDBOperator().ExeSqlForString("select ISNULL(max(substring(xmbh,18,6)),0) as tempid from Uepp_PDAXmjbxx where substring(xmbh,1,14)='" + tempid + "'", null);

        //    xmid = string.Format("{0:D3}", (int.Parse(xmid) + 1));
        //    xmid = tempid + xmid;

        //    return xmid;
        //}


        #endregion
    }
}
