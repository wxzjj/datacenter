using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Bigdesk8.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

namespace SparkServiceDesk
{
    /// <summary>
    /// CreditService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CreditService : System.Web.Services.WebService
    {
        /// <summary>
        /// 为防止sql注入，缪卫华添加于2015-9-16
        /// </summary>
        /// <param name="searchClause"></param>
        /// <returns></returns>
        private string GetSafeSearchClause(string searchClause)
        {
            if (string.IsNullOrEmpty(searchClause)) return searchClause;
            string localString = searchClause.Trim().ToLower();

            if (localString.IndexOf("delete ", 0) > 0)
                throw new Exception("Delete isnot alllowed!");

            if (localString.IndexOf("update ", 0) > 0)
                throw new Exception("Update isnot alllowed!");

            if (localString.IndexOf("insert ", 0) > 0)
                throw new Exception("Insert isnot alllowed!");

            if (localString.IndexOf("execute ", 0) > 0)
                throw new Exception("execute isnot alllowed!");

            if (localString.IndexOf("exec ", 0) > 0)
                throw new Exception("exec isnot alllowed!");

            if (localString.IndexOf("create ", 0) > 0)
                throw new Exception("Create isnot alllowed!");

            if (localString.IndexOf("drop ", 0) > 0)
                throw new Exception("Drop isnot !");

            if ((localString.IndexOf(" dt_", 0) >= 0)
                || (localString.IndexOf(" ms_", 0) >= 0)
                || (localString.IndexOf(" sp_", 0) >= 0)
                || (localString.IndexOf(" xp_", 0) >= 0))
                throw new Exception("Systemp procedure isnot permitted!");

            return searchClause;
        }

        [WebMethod]
        public string VlidateUser(string loginName, string passWord)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = " select a.LoginID,b.UserGroup,a.UserRealName,b.GroupCode from TcWebPlat50..UserInfo as a inner join Credit_LoginList as b on a.LoginID=b.LoginID where a.loginName=@LoginName and a.Password=@Password";
            spc.Add("LoginName", loginName);
            spc.Add("Password", passWord);

            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string UserRealName = ds.Tables[0].Rows[0]["UserRealName"].ToString();
                string userGroup = ds.Tables[0].Rows[0]["UserGroup"].ToString();

                string LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                string GroupCode = ds.Tables[0].Rows[0]["GroupCode"].ToString();

                return UserRealName + "," + userGroup + "," + LoginID + "," + GroupCode;
                //return "ok";
            }
            else
            {
                return "error";
            }
        }




        /// <summary>
        /// 分页获取项目表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_xmInfoBySearch(int pageNo, string strSearch)
        {
            //string sql = "select active_id as actid,Project_id as xmid ,itm_name as xmmc  from bid_zbgs  where 1=1  ";
            string sql = "select xmid as actid, xmid , xmmc  from UEPP_xmjbxx  where 1=1  ";

            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "xmid");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "xmid");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "xmid");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得项目记录的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_xmInfoBySearch(string strSearch)
        {
            // string sql = "select active_id as actid,Project_id as xmid ,itm_name as xmmc  from bid_zbgs  where 1=1  ";
            string sql = "select xmid as actid, xmid , xmmc  from UEPP_xmjbxx  where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }



        /// <summary>
        /// 分页获取任务表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_rwInfoBySearch(int pageNo, string strSearch)
        {
            string sql = @"select a.rwid,a.rwmc,a.xmid as active_id,a.xmid, b.xmmc  from credit_rwfpb as a  
                           left join UEPP_xmjbxx as b on a.xmid=b.xmid  where 1=1 ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "rwid");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "rwid");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "rwid");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得任务记录的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_rwInfoBySearch(string strSearch)
        {
            string sql = @"select a.rwid,a.rwmc,a.xmid as active_id,a.xmid, b.xmmc  from credit_rwfpb as a 
                           left join UEPP_xmjbxx as b on a.xmid=b.xmid  where 1=1 ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }



        /// <summary>
        /// 分页获取企业记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_qyInfoBySearch(int pageNo, string strSearch)
        {
            string sql = "select qyid,qymc   from Credit_view_enterprise   where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "qyid");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "qyid");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "qyid");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得企业的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_qyInfoBySearch(string strSearch)
        {
            string sql = "select  qyid,qymc   from Credit_view_enterprise    where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }


        /// <summary>
        /// 分页获取人员记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_ryInfoBySearch(int pageNo, string strSearch)
        {
            string sql = "select ryid,ryxm,ryzylxid,ryzylx   from Credit_view_person   where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "ryid");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "ryid");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "ryid");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得人员的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_ryInfoBySearch(string strSearch)
        {
            string sql = "select ryid,ryxm   from Credit_view_person    where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }




        /// <summary>
        /// 分页获取企业不良记录代码表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_qyblxwInfoBySearch(int pageNo, string strSearch)
        {
            string sql = "select xwdm,xwxz,kfbz,blxw from credit_d_qyblxwbz  where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "xwdm");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "xwdm");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "xwdm");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得企业不良行为代码表记录的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_qyblxwInfoBySearch(string strSearch)
        {
            string sql = "select xwdm,xwxz,kfbz,blxw from credit_d_qyblxwbz  where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }




        /// <summary>
        /// 分页获取不良记录表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_blxwInfoBySearch(int pageNo, string strSearch)
        {
            string sql = @"select a.credit_id,a.qymc ,a.qyid,b.xwdm,a.qyywlx from  Credit_BlxwAndQyRelation as a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id 
                            where 1=1 ";

            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "credit_id");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "credit_id");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "credit_id");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得不良行为记录的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_blxwInfoBySearch(string strSearch)
        {
            string sql = @"select a.credit_id,a.qymc ,a.qyid,b.xwdm from  Credit_BlxwAndQyRelation as a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id 
                            where 1=1 ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }




        /// <summary>
        /// 分页获取人员不良记录表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_ryblxwListBySearch(int pageNo, string strSearch)
        {
            //此处ryxm 转变为qymc 是为了在android客户端解析的时候和企业不良信息公用一套解析方法

            string sql = @"select a.credit_id,a.ryxm as qymc ,a.ryid as qyid,b.xwdm,a.ryzylx,a.ryxm from  Credit_BlxwAndRyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id
                            where 1=1  ";

            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "credit_id");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "credit_id");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "credit_id");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得人员不良行为记录的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_ryblxwListBySearch(string strSearch)
        {
            string sql = @"select a.credit_id,a.ryxm as qymc ,a.ryid as qyid,b.xwdm from  Credit_BlxwAndRyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id
                            where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }




        /// <summary>
        /// 分页获取人员不良记录代码表记录
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_ryblxwInfoBySearch(int pageNo, string strSearch)
        {
            string sql = "select xwdm,xwxz,kfbz,blxw from credit_d_ryblxwbz  where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "xwdm");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "xwdm");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "xwdm");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }


        /// <summary>
        /// 通过sql语句获得人员不良信息代码表的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_ryblxwInfoBySearch(string strSearch)
        {
            string sql = "select xwdm,xwxz,kfbz,blxw from credit_d_ryblxwbz  where 1=1  ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }



        /// <summary>
        /// 分页获取人员不良记录表记录(包括企业和人员)
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public string Get_allblxwListBySearch(int pageNo, string strSearch)
        {

            string sql = @" select * from ( select a.credit_id,a.qymc ,a.qyid,b.xwdm,b.xmid from  Credit_BlxwAndQyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id
                            union  
                            select a.credit_id,a.ryxm as qymc ,a.ryid as qyid,b.xwdm,b.xmid  from  Credit_BlxwAndRyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id  ) as t where 1=1 ";

            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "   " + strSearch;
            }

            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, null, "TempTable").Tables[0];
            if (dt.Rows.Count > 15)
            {
                if (pageNo * 15 <= dt.Rows.Count)
                {
                    sql = PaginationSQL(pageNo, 15, sql, "credit_id");
                }
                else
                {
                    int leftNum = dt.Rows.Count - (pageNo - 1) * 15;
                    sql = PaginationSQL(pageNo, leftNum, sql, "credit_id");
                }
            }
            else
            {
                sql = PaginationSQL(pageNo, dt.Rows.Count, sql, "credit_id");
            }

            dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 通过sql语句获得不良行为记录(包括企业和人员)的数目
        /// </summary>
        /// <param name="strSearch"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetCount_allblxwListBySearch(string strSearch)
        {
            string sql = @" select * from ( select a.credit_id,a.qymc ,a.qyid,b.xwdm,b.xmid from  Credit_BlxwAndQyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id
                            union  
                            select a.credit_id,a.ryxm as qymc ,a.ryid as qyid,b.xwdm,b.xmid  from  Credit_BlxwAndRyRelation as  a 
                            left join Credit_blxwjlb as b on a.credit_id=b.credit_id  ) as t where 1=1 ";
            if (strSearch != null && strSearch.Trim() != "")
            {
                strSearch = GetSafeSearchClause(strSearch);
                sql = sql + "  " + strSearch;
            }
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return dt.Rows.Count;
        }


        /// <summary>
        /// 企业从事业务类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetQycsywlx(string blank)
        {
            string sql = "select code as id,codeinfo as value from UEPP_Code   where CodeType='企业从事业务类型' ";
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }


        /// <summary>
        /// 人员类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetRylx(string blank)
        {
            string sql = "select distinct bzlxid as id ,bzlx as value from Qyywlx_ChangeCode ";
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }


        /// <summary>
        /// 人员执业资格类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetRyzyzglx(string blank)
        {
            string sql = "select code as id,codeinfo as value from UEPP_Code   where CodeType='人员执业资格类型' ";
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }

        /// <summary>
        /// 生成新的检查记录id
        /// </summary>
        /// <returns></returns>
        private string GetNewCreaditId()
        {
            string newcreditid = "";
            string sql = " select ISNULL(max(credit_id),0) as tempid from Credit_blxwjlb  ";
            try
            {
                DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
                if (dt.Rows.Count > 0)
                {
                    newcreditid = dt.Rows[0]["tempid"].ToString();
                    newcreditid = (int.Parse(newcreditid) + 1).ToString();
                    return newcreditid;
                }
            }
            catch (Exception)
            {
                return "";
            }
            return newcreditid;
        }


        /// <summary>
        /// 获得最大检查记录id
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetMaxCreaditId()
        {
            string maxcreditid = "";
            string sql = " select ISNULL(max(credit_id),0) as tempid from Credit_blxwjlb  ";
            try
            {
                DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
                if (dt.Rows.Count > 0)
                {
                    maxcreditid = dt.Rows[0]["tempid"].ToString();

                    return maxcreditid;
                }
            }
            catch (Exception)
            {
                return "";
            }
            return maxcreditid;
        }
        //添加信息到企业不良信息记录表

        [WebMethod]
        public string InsertBlxwjl(string xmid, string xmmc, string xwdm, string sjkf, string blxwss,
            string cjrloginid, string cjrusername, string cjsj, string cjbmcode, string cjbm,
            string qyid, string qymc, string qyywlxid, string qyywlx)
        {
            string credit_id = GetNewCreaditId();
            Bigdesk8.Data.SqlParameterCollection p = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            p.Add("@credit_id", credit_id);
            p.Add("@xmid", xmid);
            p.Add("@xmmc", xmmc);
            p.Add("@xwdm", xwdm);
            p.Add("@sjkf", sjkf);
            p.Add("@blxwss", blxwss);
            p.Add("@cjrloginid", cjrloginid);
            p.Add("@cjrusername", cjrusername);
            p.Add("@cjsj", cjsj);
            p.Add("@cjbmcode", cjbmcode);
            p.Add("@cjbm", cjbm);
            string sql = "insert into Credit_blxwjlb(credit_id,xmid,xmmc,xwdm,sjkf,blxwss,txr_userid,txr_username,cjrqsj,checkdeptcode,checkdeptname) "
             + "values (@credit_id, @xmid, @xmmc ,@xwdm,@sjkf ,@blxwss ,@cjrloginid ,@cjrusername ,@cjsj ,@cjbmcode ,@cjbm )";

            p.Add("@qyid", qyid);
            p.Add("@qymc", qymc);
            p.Add("@qyywlxid", qyywlxid);
            p.Add("@qyywlx", qyywlx);
            string sql2 = "insert into Credit_BlxwAndQyRelation(credit_id,qyid,qymc,qyywlxid,qyywlx)"

                 + "values (@credit_id ,@qyid ,@qymc ,@qyywlxid ,@qyywlx )";
            try
            {
                int count1 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql, p);
                int count2 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql2, p);
                //return "ok";
                return credit_id;
            }
            catch (Exception)
            {
                return "error";
            }
        }


        //修改企业不良信息记录表

        [WebMethod]
        public string UpdatetBlxwjl(string creditid, string xmid, string xmmc, string xwdm, string sjkf, string blxwss,
            string cjrloginid, string cjrusername, string cjsj, string cjbmcode, string cjbm,
            string qyid, string qymc, string qyywlxid, string qyywlx)
        {
            Bigdesk8.Data.SqlParameterCollection p = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            p.Add("@creditid", creditid);
            p.Add("@xmid", xmid);
            p.Add("@xmmc", xmmc);
            p.Add("@xwdm", xwdm);
            p.Add("@sjkf", sjkf);
            p.Add("@blxwss", blxwss);
            p.Add("@cjrloginid", cjrloginid);
            p.Add("@cjrusername", cjrusername);
            p.Add("@cjsj", cjsj);
            p.Add("@cjbmcode", cjbmcode);
            p.Add("@cjbm", cjbm);
            string sql = "update Credit_blxwjlb set xmid=@xmid,xmmc=@xmmc, xwdm=@xwdm ,sjkf=@sjkf ,blxwss=@blxwss ,txr_userid=@cjrloginid,txr_username=@cjrusername,cjrqsj=@cjsj,checkdeptcode=@cjbmcode,checkdeptname=@cjbm where credit_id=@creditid";

            p.Add("@qyid", qyid);
            p.Add("@qymc", qymc);
            p.Add("@qyywlxid", qyywlxid);
            p.Add("@qyywlx", qyywlx);
            string sql2 = "update Credit_BlxwAndQyRelation set qyid=@qyid ,qymc=@qymc,qyywlxid=@qyywlxid,qyywlx=@qyywlx where credit_id=@creditid";

            try
            {
                int count1 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql, p);
                int count2 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql2, p);
                return "ok";

            }
            catch (Exception)
            {
                return "error";
            }
        }



        //添加信息到人员不良信息记录表

        [WebMethod]
        public string InsertRyBlxwjl(string xmid, string xmmc, string xwdm, string sjkf, string blxwss,
            string cjrloginid, string cjrusername, string cjsj, string cjbmcode, string cjbm,
            string ryid, string ryxm, string ryzylxid, string ryzylx)
        {
            string credit_id = GetNewCreaditId();
            Bigdesk8.Data.SqlParameterCollection p = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            p.Add("@credit_id", credit_id);
            p.Add("@xmid", xmid);
            p.Add("@xmmc", xmmc);
            p.Add("@xwdm", xwdm);
            p.Add("@sjkf", sjkf);
            p.Add("@blxwss", blxwss);
            p.Add("@cjrloginid", cjrloginid);
            p.Add("@cjrusername", cjrusername);
            p.Add("@cjsj", cjsj);
            p.Add("@cjbmcode", cjbmcode);
            p.Add("@cjbm", cjbm);
            string sql = @"insert into 
                            Credit_blxwjlb(credit_id,xmid,xmmc,xwdm,sjkf,blxwss,txr_userid,txr_username,cjrqsj,checkdeptcode,checkdeptname) 
                            values (@credit_id, @xmid, @xmmc, @xwdm, @sjkf, @blxwss, @cjrloginid, @cjrusername, @cjsj, @cjbmcode, @cjbm )";

            p.Add("@ryid", ryid);
            p.Add("@ryxm", ryxm);
            p.Add("@ryzylxid", ryzylxid);
            p.Add("@ryzylx", ryzylx);
            string sql2 = @"insert into Credit_BlxwAndRyRelation(credit_id,ryid,ryxm,ryzylxid,ryzylx)
                            values (@credit_id, @ryid, @ryxm, @ryzylxid, @ryzylx)";
            try
            {
                int count1 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql, p);
                int count2 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql2, p);
                //return "ok";
                return credit_id;
            }
            catch (Exception)
            {
                return "error";
            }
        }



        //修改人员不良信息记录表

        [WebMethod]
        public string UpdatetRyBlxwjl(string creditid, string xmid, string xmmc, string xwdm, string sjkf, string blxwss,
            string cjrloginid, string cjrusername, string cjsj, string cjbmcode, string cjbm,
            string ryid, string ryxm, string ryzylxid, string ryzylx)
        {
            Bigdesk8.Data.SqlParameterCollection p = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            p.Add("@creditid", creditid);
            p.Add("@xmid", xmid);
            p.Add("@xmmc", xmmc);
            p.Add("@xwdm", xwdm);
            p.Add("@sjkf", sjkf);
            p.Add("@blxwss", blxwss);
            p.Add("@cjrloginid", cjrloginid);
            p.Add("@cjrusername", cjrusername);
            p.Add("@cjsj", cjsj);
            p.Add("@cjbmcode", cjbmcode);
            p.Add("@cjbm", cjbm);
            string sql = "update Credit_blxwjlb set xmid=@xmid,xmmc=@xmmc, xwdm=@xwdm ,sjkf=@sjkf ,blxwss=@blxwss ,txr_userid=@cjrloginid,txr_username=@cjrusername,cjrqsj=@cjsj,checkdeptcode=@cjbmcode,checkdeptname=@cjbm where credit_id=@creditid";

            p.Add("@ryid", ryid);
            p.Add("@ryxm", ryxm);
            p.Add("@ryzylxid", ryzylxid);
            p.Add("@ryzylx", ryzylx);
            string sql2 = "update Credit_BlxwAndRyRelation set ryid=@ryid, ryxm=@ryxm, ryzylxid=@ryzylxid, ryzylx=@ryzylx where credit_id=@creditid";

            try
            {
                int count1 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql, p);
                int count2 = Ministrant.TCSCIC60ForDBOperator().ExecuteNonQuerySql(sql2, p);
                return "ok";

            }
            catch (Exception)
            {
                return "error";
            }
        }



        //查询不良信息记录表

        [WebMethod]
        public string SelectBlxwjlByCredit_ID(string credit_id)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = @"SELECT a.qyywlxid,a.qyywlx,a.qymc, xmmc,b.xwdm,c.xwxz,sjkf,blxwss,checkdeptname,cjrqsj,txr_username 
                    FROM  Credit_BlxwAndQyRelation as a
                    left join Credit_blxwjlb as b  on a.credit_id=b.credit_id
                    inner join credit_d_qyblxwbz as c on b.xwdm=c.xwdm 
                    where a.credit_id=@credit_id";
            spc.Add("credit_id", credit_id);

            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string qyywlxid = ds.Tables[0].Rows[0]["qyywlxid"].ToString();
                string qyywlx = ds.Tables[0].Rows[0]["qyywlx"].ToString();
                string qymc = ds.Tables[0].Rows[0]["qymc"].ToString();

                string xmmc = ds.Tables[0].Rows[0]["xmmc"].ToString();
                string xwdm = ds.Tables[0].Rows[0]["xwdm"].ToString();
                string xwxz = ds.Tables[0].Rows[0]["xwxz"].ToString();

                string sjkf = ds.Tables[0].Rows[0]["sjkf"].ToString();
                string blxwss = ds.Tables[0].Rows[0]["blxwss"].ToString();

                string checkdeptname = ds.Tables[0].Rows[0]["checkdeptname"].ToString();
                string cjrqsj = string.Empty;
                if (ds.Tables[0].Rows[0]["cjrqsj"].ToString() != null && !ds.Tables[0].Rows[0]["cjrqsj"].ToString().Trim().Equals(""))
                {
                    cjrqsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["cjrqsj"].ToString()).ToShortDateString();
                }

                string txr_username = ds.Tables[0].Rows[0]["txr_username"].ToString();


                return qyywlxid + "," + qyywlx + "," + qymc + "," + xmmc + "," + xwdm + "," + xwxz + "," + sjkf + "," + blxwss + "," + checkdeptname + "," + cjrqsj + "," + txr_username;

            }
            else
            {
                return "error";
            }


        }



        //查询人员不良信息详细表

        [WebMethod]
        public string SelectRyBlxwjlByCredit_ID(string credit_id)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = @"SELECT a.ryzylxid,a.ryzylx,a.ryxm, xmmc,b.xwdm,c.xwxz,sjkf,blxwss,checkdeptname,cjrqsj,txr_username 
                    FROM  Credit_BlxwAndRyRelation as a
                    left join Credit_blxwjlb as b  on a.credit_id=b.credit_id
                    inner join credit_d_ryblxwbz as c on b.xwdm=c.xwdm 
                    where a.credit_id=@credit_id";
            spc.Add("credit_id", credit_id);

            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string ryzylxid = ds.Tables[0].Rows[0]["ryzylxid"].ToString();
                string ryzylx = ds.Tables[0].Rows[0]["ryzylx"].ToString();
                string ryxm = ds.Tables[0].Rows[0]["ryxm"].ToString();

                string xmmc = ds.Tables[0].Rows[0]["xmmc"].ToString();
                string xwdm = ds.Tables[0].Rows[0]["xwdm"].ToString();
                string xwxz = ds.Tables[0].Rows[0]["xwxz"].ToString();

                string sjkf = ds.Tables[0].Rows[0]["sjkf"].ToString();
                string blxwss = ds.Tables[0].Rows[0]["blxwss"].ToString();

                string checkdeptname = ds.Tables[0].Rows[0]["checkdeptname"].ToString();
                string cjrqsj = string.Empty;
                if (ds.Tables[0].Rows[0]["cjrqsj"].ToString() != null && !ds.Tables[0].Rows[0]["cjrqsj"].ToString().Trim().Equals(""))
                {
                    cjrqsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["cjrqsj"].ToString()).ToShortDateString();
                }

                string txr_username = ds.Tables[0].Rows[0]["txr_username"].ToString();


                return ryzylxid + "," + ryzylx + "," + ryxm + "," + xmmc + "," + xwdm + "," + xwxz + "," + sjkf + "," + blxwss + "," + checkdeptname + "," + cjrqsj + "," + txr_username;

            }
            else
            {
                return "error";
            }


        }



        //查询项目信息记录表

        [WebMethod]
        public string SelectXminfoByActive_ID(string active_id)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = @"SELECT  itm_name,itm_where, itm_ssdq,const_area,zb_price,itm_conten,itm_zbfs,begin_date,end_date FROM bid_zbgs
                    where Active_ID=@Active_ID ";

            spc.Add("Active_ID", active_id);

            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string itm_name = ds.Tables[0].Rows[0]["itm_name"].ToString();
                string itm_where = ds.Tables[0].Rows[0]["itm_where"].ToString();

                string itm_ssdq = ds.Tables[0].Rows[0]["itm_ssdq"].ToString();
                string const_area = ds.Tables[0].Rows[0]["const_area"].ToString();

                string zb_price = ds.Tables[0].Rows[0]["zb_price"].ToString();
                string itm_conten = ds.Tables[0].Rows[0]["itm_conten"].ToString();

                string itm_zbfs = ds.Tables[0].Rows[0]["itm_zbfs"].ToString();

                string begin_date = string.Empty;
                if (ds.Tables[0].Rows[0]["begin_date"].ToString() != null && !ds.Tables[0].Rows[0]["begin_date"].ToString().Trim().Equals(""))
                {
                    begin_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["begin_date"].ToString()).ToShortDateString();
                }

                string end_date = string.Empty;
                if (ds.Tables[0].Rows[0]["end_date"].ToString() != null && !ds.Tables[0].Rows[0]["end_date"].ToString().Trim().Equals(""))
                {
                    end_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["end_date"].ToString()).ToShortDateString();
                }

                return itm_name + "," + itm_where + "," + itm_ssdq + "," + const_area + "," + zb_price + "," + itm_conten + "," + itm_zbfs + "," + begin_date + "," + end_date;

            }
            else
            {
                return "error";
            }


        }

        /// <summary>
        /// 根据xmid查询某条项目的信息
        /// </summary>
        /// <param name="xmid"></param>
        /// <returns></returns>
        [WebMethod]
        public string SelectXminfoByXmid(string xmid)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = "SELECT *  FROM UEPP_xmjbxx where xmid=@xmid ";

            spc.Add("xmid", xmid);

            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string xmmc = ds.Tables[0].Rows[0]["xmmc"].ToString();              //项目名称
                string dd = ds.Tables[0].Rows[0]["dd"].ToString();                  //工程地点
                string ssdq = ds.Tables[0].Rows[0]["ssdq"].ToString();              //工程属地
                string jzmj = ds.Tables[0].Rows[0]["jzmj"].ToString();              //建筑面积
                string gm = ds.Tables[0].Rows[0]["gm"].ToString();                  //工程规模
                string zj = ds.Tables[0].Rows[0]["zj"].ToString();                  //投资总额
                string xmlb = ds.Tables[0].Rows[0]["xmlb"].ToString();              //工程性质
                string zyfl = ds.Tables[0].Rows[0]["zyfl"].ToString();              //工程专业类型
                string tzlx = ds.Tables[0].Rows[0]["tzlx"].ToString();              //工程投资类型
                string sylb = ds.Tables[0].Rows[0]["sylb"].ToString();              //工程使用类型

                string jsdw = ds.Tables[0].Rows[0]["jsdw"].ToString();              //建设单位
                string jsdwdz = ds.Tables[0].Rows[0]["jsdwdz"].ToString();          //地址
                string jsdwdh = ds.Tables[0].Rows[0]["jsdwdh"].ToString();          //电话
                string jsdwfzr = ds.Tables[0].Rows[0]["jsdwfzr"].ToString();        //负责人
                string jsdwfrzdh = ds.Tables[0].Rows[0]["jsdwfrzdh"].ToString();    //负责人电话

                string sgdw = ds.Tables[0].Rows[0]["sgdw"].ToString();              //施工单位
                string sgdwdz = ds.Tables[0].Rows[0]["sgdwdz"].ToString();          //地址
                string sgdwzzdj = ds.Tables[0].Rows[0]["sgdwzzdj"].ToString();      //资格等级
                string sgdwzzzsh = ds.Tables[0].Rows[0]["sgdwzzzsh"].ToString();    //资质证书号
                string xmjl = ds.Tables[0].Rows[0]["xmjl"].ToString();              //项目经理
                string xmjlzzdj = ds.Tables[0].Rows[0]["xmjlzzdj"].ToString();      //项目经理资质等级
                string xmjlzzzsh = ds.Tables[0].Rows[0]["xmjlzzzsh"].ToString();    //项目经理资质证书号
                string xmjlaqhgzh = ds.Tables[0].Rows[0]["xmjlaqhgzh"].ToString();  //B类安全合格证号
                string xmjldh = ds.Tables[0].Rows[0]["xmjldh"].ToString();          //项目经理联系电话

                string cblx = ds.Tables[0].Rows[0]["cblx"].ToString();              //承包类型

                //以下这些字段在本地块无数据结构，在服务上有，故备两套以保障本地和服务器都正确
                //string sghtbh = ds.Tables[0].Rows[0]["sghtbh"].ToString();          //施工合同编号
                //string sghtbaglbm = ds.Tables[0].Rows[0]["sghtbaglbm"].ToString();  //施工合同备案管理部门
                //string sghtbaslr = ds.Tables[0].Rows[0]["sghtbaslr"].ToString();    //施工合同备案受理人

                //string sghtbaslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["sghtbaslsj"].ToString() != null && !ds.Tables[0].Rows[0]["sghtbaslsj"].ToString().Trim().Equals(""))
                //{
                //    sghtbaslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["sghtbaslsj"].ToString()).ToShortDateString();//施工合同备案受理时间
                //}

                //string jlhtbh = ds.Tables[0].Rows[0]["jlhtbh"].ToString();      	  //监理合同编号
                //string jlhtbaglbm = ds.Tables[0].Rows[0]["jlhtbaglbm"].ToString();  //监理合同备案管理部门
                //string jlhtbaslr = ds.Tables[0].Rows[0]["jlhtbaslr"].ToString();    //监理合同备案受理人

                //string jlhtbaslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["jlhtbaslsj"].ToString() != null && !ds.Tables[0].Rows[0]["jlhtbaslsj"].ToString().Trim().Equals(""))
                //{
                //    jlhtbaslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["jlhtbaslsj"].ToString()).ToShortDateString();//监理合同备案受理时间
                //}

                //string aqjddabh = ds.Tables[0].Rows[0]["aqjddabh"].ToString();      //安全监督档案编号
                //string aqjdglbm = ds.Tables[0].Rows[0]["aqjdglbm"].ToString();      //安全监督管理部门
                //string aqjdslr = ds.Tables[0].Rows[0]["aqjdslr"].ToString();    	  //安全监督受理人

                //string aqjdslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["aqjdslsj"].ToString() != null && !ds.Tables[0].Rows[0]["aqjdslsj"].ToString().Trim().Equals(""))
                //{
                //    aqjdslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["aqjdslsj"].ToString()).ToShortDateString();//安全监督受理时间
                //}

                //string aqjdjar = ds.Tables[0].Rows[0]["aqjdjar"].ToString();        //安全监督结案处理人

                //string aqjdjasj = string.Empty;
                //if (ds.Tables[0].Rows[0]["aqjdjasj"].ToString() != null && !ds.Tables[0].Rows[0]["aqjdjasj"].ToString().Trim().Equals(""))
                //{
                //    aqjdjasj = Convert.ToDateTime(ds.Tables[0].Rows[0]["aqjdjasj"].ToString()).ToShortDateString();//安全监督结案时间
                //}

                //string zljddabh = ds.Tables[0].Rows[0]["zljddabh"].ToString();        //质量监督档案编号
                //string zljdglbm = ds.Tables[0].Rows[0]["zljdglbm"].ToString();    	//质量监督管理部门
                //string zljdslr = ds.Tables[0].Rows[0]["zljdslr"].ToString();      	//质量监督受理人

                //string zljdslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["zljdslsj"].ToString() != null && !ds.Tables[0].Rows[0]["zljdslsj"].ToString().Trim().Equals(""))
                //{
                //    zljdslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["zljdslsj"].ToString()).ToShortDateString();//质量监督受理时间
                //}

                //string zljdjar = ds.Tables[0].Rows[0]["zljdjar"].ToString();     	    //质量监督结案处理人

                //string zljdjasj = string.Empty;
                //if (ds.Tables[0].Rows[0]["zljdjasj"].ToString() != null && !ds.Tables[0].Rows[0]["zljdjasj"].ToString().Trim().Equals(""))
                //{
                //    zljdjasj = Convert.ToDateTime(ds.Tables[0].Rows[0]["zljdjasj"].ToString()).ToShortDateString();//质量监督结案时间
                //}


                //string sgxkzbh = ds.Tables[0].Rows[0]["sgxkzbh"].ToString();          //施工许可证编号
                //string sgxkglbm = ds.Tables[0].Rows[0]["sgxkglbm"].ToString();     	//施工许可管理部门
                //string sgxkslr = ds.Tables[0].Rows[0]["sgxkslr"].ToString();          //施工许可受理人

                //string sgxkslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["sgxkslsj"].ToString() != null && !ds.Tables[0].Rows[0]["sgxkslsj"].ToString().Trim().Equals(""))
                //{
                //    sgxkslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["sgxkslsj"].ToString()).ToShortDateString();//施工许可受理时间
                //}

                //string jgbaglbm = ds.Tables[0].Rows[0]["jgbaglbm"].ToString();        //竣工备案管理部门
                //string jgbaslr = ds.Tables[0].Rows[0]["jgbaslr"].ToString();      	//竣工备案受理人

                //string jgbaslsj = string.Empty;
                //if (ds.Tables[0].Rows[0]["jgbaslsj"].ToString() != null && !ds.Tables[0].Rows[0]["jgbaslsj"].ToString().Trim().Equals(""))
                //{
                //    jgbaslsj = Convert.ToDateTime(ds.Tables[0].Rows[0]["jgbaslsj"].ToString()).ToShortDateString();//竣工备案受理时间
                //}




                //---------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------

                string sghtbh = "";       //施工合同编号
                string sghtbaglbm = "";   //施工合同备案管理部门
                string sghtbaslr = "";    //施工合同备案受理人
                string sghtbaslsj = "";   //施工合同备案受理时间

                string jlhtbh = "";       //监理合同编号
                string jlhtbaglbm = "";   //监理合同备案管理部门
                string jlhtbaslr = "";    //监理合同备案受理人
                string jlhtbaslsj = "";   //监理合同备案受理时间

                string aqjddabh = "";       //安全监督档案编号
                string aqjdglbm = "";     	//安全监督管理部门
                string aqjdslr = "";    	//安全监督受理人
                string aqjdslsj = "";     	//安全监督受理时间
                string aqjdjar = "";      	//安全监督结案处理人
                string aqjdjasj = "";       //安全监督结案时间


                string zljddabh = "";       //质量监督档案编号
                string zljdglbm = "";    	//质量监督管理部门
                string zljdslr = "";      	//质量监督受理人
                string zljdslsj = "";   	//质量监督受理时间
                string zljdjar = "";     	//质量监督结案处理人
                string zljdjasj = "";     	//质量监督结案时间

                string sgxkzbh = "";        //施工许可证编号
                string sgxkglbm = "";     	//施工许可管理部门
                string sgxkslr = "";        //施工许可受理人
                string sgxkslsj = "";       //施工许可受理时间

                string jgbaglbm = "";       //竣工备案管理部门
                string jgbaslr = "";      	//竣工备案受理人
                string jgbaslsj = "";     	//竣工备案受理时间

                return xmmc + "," + dd + "," + ssdq + "," + jzmj + "," + gm + "," + zj + "," + xmlb + "," + zyfl + "," + tzlx + "," + sylb
                 + "," + jsdw + "," + jsdwdz + "," + jsdwdh + "," + jsdwfzr + "," + jsdwfrzdh
                 + "," + sgdw + "," + sgdwdz + "," + sgdwzzdj + "," + sgdwzzzsh
                 + "," + xmjl + "," + xmjlzzdj + "," + xmjlzzzsh + "," + xmjlaqhgzh + "," + xmjldh
                 + "," + cblx
                 + "," + sghtbh + "," + sghtbaglbm + "," + sghtbaslr + "," + sghtbaslsj
                 + "," + jlhtbh + "," + jlhtbaglbm + "," + jlhtbaslr + "," + jlhtbaslsj
                 + "," + aqjddabh + "," + aqjdglbm + "," + aqjdslr + "," + aqjdslsj + "," + aqjdjar + "," + aqjdjasj
                 + "," + zljddabh + "," + zljdglbm + "," + zljdslr + "," + zljdslsj + "," + zljdjar + "," + zljdjasj
                 + "," + sgxkzbh + "," + sgxkglbm + "," + sgxkslr + "," + sgxkslsj
                 + "," + jgbaglbm + "," + jgbaslr + "," + jgbaslsj;
            }
            else
            {
                return "error";
            }
        }



        /// <summary>
        /// 查询施工单位名称
        /// </summary>
        /// <param name="xmid"></param>
        /// <returns></returns>

        [WebMethod]
        public string SelectSgdwinXminfoByXmid(string xmid)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = "SELECT sgdwid,sgdw FROM UEPP_xmjbxx where xmid=@xmid ";
            spc.Add("xmid", xmid);
            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string sgdwid = ds.Tables[0].Rows[0]["sgdwid"].ToString();
                string sgdw = ds.Tables[0].Rows[0]["sgdw"].ToString();
                return sgdwid + "," + sgdw;
            }
            else
            {
                return "error";
            }
        }

        /// <summary>
        ///  查询某项目的项目经理
        /// </summary>
        /// <param name="xmid"></param>
        /// <returns></returns>
        [WebMethod]
        public string SelectXmjlinXminfoByXmid(string xmid)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = "SELECT xmjlid,xmjl FROM UEPP_xmjbxx where xmid=@xmid ";
            spc.Add("xmid", xmid);
            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string xmjlid = ds.Tables[0].Rows[0]["xmjlid"].ToString();
                string xmjl = ds.Tables[0].Rows[0]["xmjl"].ToString();
                return xmjlid + "," + xmjl;
            }
            else
            {
                return "error";
            }
        }




        /// <summary>
        /// 查询参建单位信息
        /// </summary>
        /// <param name="xmid"></param>
        /// <returns></returns>

        [WebMethod]
        public string SelectXmcjdwByXmid(string xmid, string cjdwlxID)
        {
            string sql = string.Empty;
            Bigdesk8.Data.SqlParameterCollection spc = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            sql = "SELECT * FROM UEPP_xmcjdwxx where xmid=@xmid and cjdwlxid=@cjdwlxID ";
            spc.Add("xmid", xmid);
            spc.Add("cjdwlxID", cjdwlxID);
            DataSet ds = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataSet(sql, spc, "TempTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string qyid = ds.Tables[0].Rows[0]["qyid"].ToString();               /*企业id*/
                string qymc = ds.Tables[0].Rows[0]["qymc"].ToString();               /*企业名称*/

                string qydz = ds.Tables[0].Rows[0]["qydz"].ToString();	             /*单位地址*/
                string qyzzdjID = ds.Tables[0].Rows[0]["qyzzdjID"].ToString();	     /*单位资质等级ID，见代码表*/
                string qyzzdj = ds.Tables[0].Rows[0]["qyzzdj"].ToString();	         /*单位资质等级，见代码表*/
                string qyzzzsh = ds.Tables[0].Rows[0]["qyzzzsh"].ToString();	     /*单位资质证书号*/
                string qyfddbr = ds.Tables[0].Rows[0]["qyfddbr"].ToString();	     /*单位法定代表人*/
                string qyfddbrlxdh = ds.Tables[0].Rows[0]["qyfddbrlxdh"].ToString(); /*单位法定代表人联系电话*/

                string fzrID = ds.Tables[0].Rows[0]["fzrID"].ToString();	         /*负责人编号，统一人员标识*/
                string fzrxm = ds.Tables[0].Rows[0]["fzrxm"].ToString();	         /*负责人姓名*/
                string fzrzjlxID = ds.Tables[0].Rows[0]["fzrzjlxID"].ToString();	 /*负责人证件类型ID，见代码表*/
                string fzrzjlx = ds.Tables[0].Rows[0]["fzrzjlx"].ToString();	     /*负责人证件类型，见代码表*/
                string fzrzjhm = ds.Tables[0].Rows[0]["fzrzjhm"].ToString();	     /*负责人证件号码*/
                string fzrdh = ds.Tables[0].Rows[0]["fzrdh"].ToString();	         /*负责人电话*/

                return qyid + "," + qymc + "," + qydz + "," + qyzzdjID + "," + qyzzdj + "," + qyzzzsh + "," + qyfddbr + "," + qyfddbrlxdh + "," + fzrID + "," + fzrxm + "," + fzrzjlxID + "," + fzrzjlx + "," + fzrzjhm + "," + fzrdh;

            }
            else
            {
                return "error";
            }
        }




        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <param name="PageNo"></param>
        /// <param name="leftNum"></param>
        /// <param name="oldSql"></param>
        /// <param name="Colm"></param>
        /// <returns></returns>
        private string PaginationSQL(int PageNo, int leftNum, string oldSql, string Colm)
        {
            string Sql = string.Empty;
            int n = PageNo * 15;
            Sql = " select * from ("
            + " select top " + leftNum.ToString() + " * from ("
            + " select top " + n.ToString() + " * from (" + oldSql + ")T order by " + Colm + " asc) TT"
            + " order by " + Colm + " desc) TTT order by " + Colm + " asc";

            return Sql;
        }



        /// <summary>
        /// 将dt转换成xml字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ConvertDtToXml(DataTable dt)
        {
            if (dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }




        [WebMethod]
        public string InsertIntoG_BusinessFile(string creditID, string filename, string base64String)
        {
            byte[] buffer = Convert.FromBase64String(base64String);

            string sql = string.Empty;
            string FileSize = buffer.Length + "";
            string CreateDateTime = DateTime.Now.ToShortDateString();

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JGTSCIC82ConnectionString"].ToString());
                conn.Open();

                sql = @"insert into g_BusinessFile(SystemID,ModuleCode,CategoryCode,MasterID,FileContent,FileName,FileType,FileSize,CreateDateTime,Status) 
                values (154,'PDAXYCJ','BLXW',@creditID , @FileContent ,@filename,'jpg', @FileSize , @CreateDateTime ,0)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@creditID", SqlDbType.VarChar).Value = creditID;
                cmd.Parameters.Add("@FileContent", SqlDbType.Image).Value = buffer;
                cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename;
                cmd.Parameters.Add("@FileSize", SqlDbType.VarChar).Value = FileSize;
                cmd.Parameters.Add("@CreateDateTime", SqlDbType.VarChar).Value = CreateDateTime;
                cmd.ExecuteNonQuery();

                conn.Close();

                return "ok";
            }
            catch (Exception)
            {
                return "error";
            }


        }

        [WebMethod]
        public string GetPicFromG_BusinessFileByID(string FileID)
        {
            byte[] imagebytes = null;

            string sql = string.Empty;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JGTSCIC82ConnectionString"].ToString());
            conn.Open();


            sql = "SELECT FileContent FROM g_businessFile where FileID='" + Bigdesk8.Security.AntiSqlInjection.GetSafeInput(FileID) + "'";


            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    imagebytes = (byte[])reader.GetValue(0);

                }
                reader.Close();
                conn.Close();
                //string filecontent = Convert.ToBase64String(new byte[] { ds.Tables[0].Rows[0]["FileContent"].ToString() });
                return Convert.ToBase64String(imagebytes);
            }
            catch (Exception)
            {
                //reader.Close();
                conn.Close();
                return "error";
            }



        }




        [WebMethod]
        public string GetAllPicNameByCreditID(String creditid)
        {
            ////声明一个名称为dt的datatable
            //DataTable dt = new DataTable("dt");

            ////声明String类型的列picname
            //DataColumn dc = new DataColumn("value", typeof(String));
            ////为Datatable增加一列
            //dt.Columns.Add(dc);

            ////dc.AutoIncrement = true;

            ////String path = @"d:\太仓市工程建设网\AndroidPic\" + xmid ;
            //String path = @"C:\androidtest";

            //DirectoryInfo folder = new DirectoryInfo(path);

            //foreach (FileInfo file in folder.GetFiles("*.jpg"))
            //{
            //    //为Datatable增加一行
            //    DataRow dr = dt.NewRow();
            //    //dr["picname"] = file.Name;
            //    dr["value"] = file.Name;
            //    dt.Rows.Add(dr);
            //}

            string sql = "select FileID as id,filename as value  from g_businessFile where MasterId='" + creditid + "'";
            
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, null, "dt");
            return ConvertDtToXml(dt);
        }


        [WebMethod]
        public int GetAllPicNameCountByCreditID(String creditID)
        {
            //String path = @"C:\androidtest";
            //DirectoryInfo folder = new DirectoryInfo(path);
            //FileInfo[] files = folder.GetFiles("*.jpg");
            //return files.Length;


            string sql = "select fileid  from g_businessFile  where MasterID=@creditID";
            Bigdesk8.Data.SqlParameterCollection p = Ministrant.TCSCIC60ForDBOperator().CreateSqlParameterCollection();
            p.Add("@creditID", creditID);
            DataTable dt = Ministrant.TCSCIC60ForDBOperator().ExeSqlForDataTable(sql, p, "dt");
            return dt.Rows.Count;

        }


    }
}
