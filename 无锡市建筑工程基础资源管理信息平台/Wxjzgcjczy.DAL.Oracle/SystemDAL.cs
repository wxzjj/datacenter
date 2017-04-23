using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace Wxjzgcjczy.DAL
{
    public class SystemDAL
    {
        public DBOperator DB { get; set; }

        /// <summary>
        /// 1.获取某个用户的信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":username", username);
            p.Add(":password", password);
            string sql = " select * from G_User where LoginName=:username and LoginPassword=:password";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 获取某个用户的信息
        /// </summary>
        /// <param name="userid">userid</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string userid)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":userid", userid);
            string sql = " select * from G_User where UserID=:userid";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        /// <summary>
        /// 2. 获取角色信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable GetRolesInfo(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":username", username);
            p.Add(":password", password);
            string sql = " select b.* from G_User  a "
                       + " inner join G_UserRole  b on a.UserID=b.UserID "
                       + " where a.LoginName=:username and a.LoginPassword=:password";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 更新用户登录时间
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="lastLoginTime">最后一次登录时间</param>
        public void UpdateLastLoginTime(string userID, string lastLoginTime)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":userid", userID);
            p.Add(":lastlogintime", lastLoginTime);
            string sql = "update g_user set LastLoginTime=to_date(:lastLoginTime,'YY/MM/DD HH24:MI:SS') where UserID=:userid";
            DB.ExecuteNonQuerySql(sql, p);
        }
     
        /// <summary>
        /// 判断是否存在该用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":username", username);
            p.Add(":password", password);

            string sql = "select * from G_User where LoginName=:username and LoginPassword=:password";
            return DB.ExeSqlForDataTable(sql, p, "t").Rows.Count > 0;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void ChangePwd(string userid, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":userid", userid);
            p.Add(":password", password);

            string sql = "update G_User set LoginPassword=:password where userid=:userid ";
            DB.ExecuteNonQuerySql(sql,p);
        }
        /// <summary>
        /// 获取企业用户信息
        /// </summary>
        /// <param name="userid"></param>
        public DataTable GetQyInfo(string userid)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":UserID", userid);

            string sql = "select * from UEPP_Qyjbxx where UserID=:UserID";
           return  DB.ExeSqlForDataTable(sql,p,"t");
        }
        /// <summary>
        /// 获取实施单位（建设单位）用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetJsdwInfo(string userid)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":UserID", userid);

            string sql = "select * from Szgkjc_Jsxm_Jsdw where UserID=:UserID";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 获取公文通知信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <returns></returns>
        public DataTable ReadGwtz(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":UserID", workUser.UserID);

            string sql = @"select c.*
                           from Gwtz_Info_Sjml  a
                           inner join Gwtz_Sjml  b on b.SjmlID = a.SjmlID 
                           inner join Gwtz_User_Relation  d on b.SjmlID=d.sjmlID
                           inner join Gwtz_Info  c on c.InfoId = a.InfoId 
                           where 1=1 and a.InnerMailReaded = 0 and c.IsDeleted<>1 and c.InfoType='公文通知' and d.UserID=:UserID";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        
        /// <summary>
        /// 创建一个新的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="loginName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int CreateUser(string userName, string loginName, string passWord)
        {
            string sql = "select * from g_user where 1=2";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["LoginName"] = loginName;
            dt.Rows[0]["LoginPassWord"] = passWord;
            dt.Rows[0]["UserName"] = userName;
            dt.Rows[0]["UserType"] = UserType.实施单位;
            dt.Rows[0]["UserState"] = "正常用户";
            int userID = DB.ExeSqlForObject("select max(userID) from g_user", null).ToInt32(0) + 1;
            dt.Rows[0]["UserID"] = userID;
            DB.Update(sql, null, dt);

            sql = "select * from g_UserRole where 1=2";
             dt = DB.ExeSqlForDataTable(sql, null, "t");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["RoleId"] = 2;
            dt.Rows[0]["UserID"] = userID;
            DB.Update(sql, null, dt);

            return userID;
        }
        /// <summary>
        /// 判断该用户名是否已存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool CheckLoginName(string loginName)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add(":LoginName", loginName);

            string sql = "select * from g_user where LoginName=:LoginName";
            return DB.ExeSqlForDataTable(sql, p, "t").Rows.Count>0?true:false;
        }
    }
}
