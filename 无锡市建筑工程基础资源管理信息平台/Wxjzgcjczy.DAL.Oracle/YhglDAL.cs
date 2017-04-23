using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8;

namespace Wxjzgcjczy.DAL
{
    public class YhglDAL
    {
        public DBOperator DB { get; set; }
        #region 表结构
        public DataTable GetSchema_User()
        {
            string sql = "select * from g_user where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        #endregion

        public Int64 GetNewID_User()
        {
            string sql = "select max(UserID) from g_user ";
            return (DB.ExeSqlForString(sql, null).ToInt64(0)+1);
        }

        #region 列表
        public DataTable Retrieve_g_user(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from g_user where USERSTATE=:UserState and  ";
           
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            sp.Add(":UserState", "正常用户");
            return DB.ExeSqlForDataTable(sql, sp, "t",orderby,pageSize,pageIndex,out allRecordCount);
        }
        #endregion

        #region 单条记录
        public DataTable ReadUser(string userID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from g_user where UserID=:UserID ";
            sp.Add(":UserID", userID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        #endregion

        #region 更新记录

        public bool SubmitUser(DataTable dt)
        {
            string sql = "select * from g_user where 1=2 ";
            return DB.Update(sql,null,dt);
        }
        #endregion

       




    }
}
