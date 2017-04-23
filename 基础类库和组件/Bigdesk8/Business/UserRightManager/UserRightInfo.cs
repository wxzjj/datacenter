using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Bigdesk8.Data;

namespace Bigdesk8.Business.UserRightManager
{
    /// <summary>
    /// 用户权限信息，主要针对一个用户权限信息
    /// </summary>
    public class UserRightInfo : IUserRightInfo
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbOperator">数据库访问类</param>
        /// <param name="userID">用户编号</param>
        /// 
        public UserRightInfo(DBOperator dbOperator, int userID)
        {
            DB = dbOperator;

            string sql = "select * from g_user where UserID=@UserID";

            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@UserID", userID);
            DataTable dt = DB.ExeSqlForDataTable(sql, p, "table");

            this.Init(dt);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbOperator">数据库访问类</param>
        /// <param name="loginName">登录名称</param>
        public UserRightInfo(DBOperator dbOperator, string loginName)
        {
            DB = dbOperator;

            string sql = "select * from g_user where LoginName=@LoginName";

            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@LoginName", loginName);
            DataTable dt = DB.ExeSqlForDataTable(sql, p, "table");

            this.Init(dt);
        }

        public UserRightInfo(DBOperator dbOperator, string loginName, string passWord)
        {
            DB = dbOperator;

            string sql = "select * from g_user where LoginName=@LoginName and LoginPassword=@LoginPassword";

            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@LoginName", loginName);
            p.Add("@LoginPassword", passWord);
            DataTable dt = DB.ExeSqlForDataTable(sql, p, "table");

            this.Init(dt);
        }
        private void Init(DataTable dt)
        {
            this.CountOfUserMatched = dt.Rows.Count;

            if (dt.Rows.Count != 1)
            {
                this.Exists = false;
                return;
            }

            this.UserID = dt.Rows[0]["UserID"].ToInt32();
            this.UserName = dt.Rows[0]["UserName"].ToString();
            this.LoginName = dt.Rows[0]["LoginName"].ToString();
            this.OrgUnitName = dt.Rows[0]["OrgUnitName"].ToString();
            this.UserType = (BasicUserType)Enum.Parse(typeof(BasicUserType), dt.Rows[0]["UserType"].ToString());

            this.Exists = true;
            this.IsGuest = this.UserID == UserRightManager.GuestUserID;
        }

        #endregion 构造函数

        #region 属性

        /// <summary>
        /// 用户是否存在
        /// </summary>
        public bool Exists { get; private set; }

        /// <summary>
        /// 用户是否是匿名用户
        /// </summary>
        public bool IsGuest { get; private set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; private set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; private set; }

        /// <summary>
        /// 科室   张鎏添加
        /// </summary>
        public string OrgUnitName { get; private set; }

        public BasicUserType UserType { get; private set; }

        /// <summary>
        /// 符合构造函数条件的用户的个数
        /// </summary>
        /// 该死的数据库中可能存在重复的用户记录
        public int CountOfUserMatched { get; private set; }

        #endregion

        #region 方法

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public bool HasRole(int roleID)
        {
            string sql = "select count(1) from g_UserRole where UserID=@UserID and RoleID=@RoleID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@RoleID", roleID);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool HasRole(string roleName)
        {
            string sql = "select count(1) from g_v_UserRole where UserID=@UserID and RoleName=@RoleName";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@RoleID", roleName);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断用户是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        public bool HasRight(int systemID)
        {
            string sql = "select count(1) from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 获得用户对某个模块是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <returns></returns>
        public bool HasRight(int systemID, string moduleCode)
        {
            string sql = "select count(1) from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and ModuleCode=@ModuleCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;

        }

        /// <summary>
        /// 判断用户对某个模块的某个操作是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns></returns>
        public bool HasRight(int systemID, string moduleCode, string operateCode)
        {
            string sql = "select count(1) from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and ModuleCode=@ModuleCode and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@OperateCode", operateCode);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断用户对某个模块的某个操作的某个业务数据分类是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <param name="dataTypeCode">业务数据分类代码</param>
        /// <returns></returns>
        public bool HasRight(int systemID, string moduleCode, string operateCode, string dataTypeCode)
        {
            string sql = "select count(1) from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and ModuleCode=@ModuleCode and OperateCode=@OperateCode and DataTypeCode=@DataTypeCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@OperateCode", operateCode);
            spc.Add("@DataTypeCode", dataTypeCode);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断用户对某个操作是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns></returns>
        public bool HasRight2(int systemID, string operateCode)
        {
            string sql = "select count(1) from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@OperateCode", operateCode);

            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        public Dictionary<string, string> SelectDataType(int systemID, string moduleCode)
        {
            string sql = "select distinct DataTypeCode,DataTypeName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and ModuleCode=@ModuleCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["DataTypeCode"].ToString(), dr["DataTypeName"].ToString());
            }
            return dic;
        }

        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        public Dictionary<string, string> SelectDataType(int systemID, string moduleCode, string operateCode)
        {
            string sql = "select distinct DataTypeCode,DataTypeName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and ModuleCode=@ModuleCode and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@OperateCode", operateCode);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["DataTypeCode"].ToString(), dr["DataTypeName"].ToString());
            }
            return dic;
        }

        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        public Dictionary<string, string> SelectDataType2(int systemID, string operateCode)
        {
            string sql = "select distinct DataTypeCode,DataTypeName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@OperateCode", operateCode);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["DataTypeCode"].ToString(), dr["DataTypeName"].ToString());
            }
            return dic;
        }


        /// <summary>
        /// 获得用户的模块
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回值key=ModuleCode,value=ModuleName</returns>
        public Dictionary<string, string> SelectModule(int systemID)
        {
            string sql = "select distinct ModuleCode,ModuleName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["ModuleCode"].ToString(), dr["ModuleName"].ToString());
            }
            return dic;
        }

        /// <summary>
        /// 获得用户具有操作权限的模块
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=ModuleCode,value=ModuleName</returns>
        public Dictionary<string, string> SelectModule(int systemID, string operateCode)
        {
            string sql = "select distinct ModuleCode,ModuleName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@OperateCode", operateCode);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["ModuleCode"].ToString(), dr["ModuleName"].ToString());
            }
            return dic;
        }


        /// <summary>
        /// 获得用户的模块业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回值key=ModuleCode;DataTypeCode,value=ModuleName;DataTypeName</returns>
        public Dictionary<string, string> SelectModuleDataType(int systemID)
        {
            string sql = "select distinct ModuleCode,ModuleName,DataTypeCode,DataTypeName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["ModuleCode"].ToString() + ";" + dr["DataTypeCode"].ToString(), dr["ModuleName"].ToString() + ";" + dr["DataTypeName"].ToString());
            }
            return dic;
        }

        /// <summary>
        /// 获得用户的模块业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=ModuleCode;DataTypeCode,value=ModuleName;DataTypeName</returns>
        public Dictionary<string, string> SelectModuleDataType(int systemID, string operateCode)
        {
            string sql = "select distinct ModuleCode,ModuleName,DataTypeCode,DataTypeName from g_v_UserRoleRight where UserID=@UserID and SystemID=@SystemID and OperateCode=@OperateCode";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", this.UserID);
            spc.Add("@SystemID", systemID);
            spc.Add("@OperateCode", operateCode);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["ModuleCode"].ToString() + ";" + dr["DataTypeCode"].ToString(), dr["ModuleName"].ToString() + ";" + dr["DataTypeName"].ToString());
            }
            return dic;
        }

        #endregion 方法

    }
}