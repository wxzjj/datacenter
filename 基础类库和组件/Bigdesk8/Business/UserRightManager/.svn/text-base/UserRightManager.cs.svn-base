using System.Collections.Generic;
using System.Data;
using Bigdesk8.Data;
using System.Configuration;
using System.Web;
using System;

namespace Bigdesk8.Business.UserRightManager
{
    /// <summary>
    /// 用户权限管理，主要对多个用户权限信息进行管理
    /// </summary>
    public class UserRightManager : IUserRightManager
    {
        /// <summary>
        /// 匿名用户编号
        /// </summary>
        public const int GuestUserID = 1;
        /// <summary>
        /// 匿名用户名称
        /// </summary>
        public const string GuestLoginName = "GUEST";

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB
        {
            get;
            set;
        }

        /// <summary>
        /// 是否使用内部事务
        /// </summary>
        public bool IsUseInnerTransaction { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRightManager()
        {
            this.IsUseInnerTransaction = true;
        }

        #region 用户

        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="model">用户信息</param>
        public UserModel CreateUser(UserModel model)
        {
            if (this.IsUseInnerTransaction) DB.BeginTransaction();
            try
            {
                string sql = "select * from g_user where 1=2";
                DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                int maxID = DB.ExeSqlForObject("select max(userid) from g_user", null).ToInt32(0);
                if (maxID < 10001)
                    model.UserID = 10001;
                else
                    model.UserID = maxID.ToInt32() + 1;

                dr["userid"] = model.UserID;
                dr["UserName"] = model.UserName;
                dr["LoginName"] = model.LoginName;
                dr["OrgName"] = model.OrgName;
                dr["UserMobile"] = model.UserMobile;
                dr["LoginPassword"] = model.LoginPassword;
                dr["UserType"] = model.UserType; ; //2011-12-12添加

                DB.Update(sql, null, dt);

                //this.SaveUserRole(model.UserID, GuestUserID);

                if (this.IsUseInnerTransaction) DB.CommitTransaction();

                return model;
            }
            catch
            {
                if (this.IsUseInnerTransaction) DB.RollbackTransaction();

                throw;
            }
        }

        /// <summary>
        /// 读取一个用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>返回用户信息</returns>
        public UserModel ReadUser(int userID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);

            string sql = "select * from g_user where UserID=@UserID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count != 1) return null;

            UserModel model = new UserModel();
            model.UserID = dt.Rows[0]["UserID"].ToInt32();
            model.UserName = dt.Rows[0]["UserName"].ToString();
            model.LoginName = dt.Rows[0]["LoginName"].ToString();
            model.UserMobile = dt.Rows[0]["UserMobile"].ToString();
            model.OrgName = dt.Rows[0]["OrgName"].ToString();
            model.LoginPassword = dt.Rows[0]["LoginPassword"].ToString();

            return model;
        }

        /// <summary>
        /// 查询用户权限数据，只查询有权限的数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchUserRight(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = @"select c.RoleID,d.SystemID,e.SystemName ,a.UserID ,UserName, LoginName, RoleName, 
                                ModuleCode, ModuleName, OperateCode, OperateName, DataTypeCode, DataTypeName, 
                                d.ModuleID, d.OperateID, d.DataTypeID, f.Sort as ModuleSort,g.Sort as OperateSort,
                                h.Sort as DataTypeSort   
                            from g_user a 
                            inner join g_UserRole b on b.UserID =a.UserID
                            inner join g_RoleRight d on d.RoleID =b.RoleID 
                            inner join g_Role c on c.RoleID =d.RoleID
                            inner join g_System e on e.SystemID =d.SystemID 
                            inner join g_Module f on f.ModuleID =d.ModuleID and f.SystemID = d.SystemID
                            inner join g_Operate g on g.OperateID =d.OperateID and g.SystemID = d.SystemID
                            inner join g_DataType h on h.DataTypeID =d.DataTypeID and h.SystemID=d.SystemID  ";

            sql = "select * from ( " + sql + " ) as _t1 where 1=1 ";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "UserID,RoleID,SystemID,ModuleSort,OperateSort,DataTypeSort";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="model">用户信息</param>
        public void UpdateUser(UserModel model)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", model.UserID);

            string sql = "select * from g_user where UserID=@UserID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count != 1) return;

            DataRow dr = dt.Rows[0];

            dr["UserName"] = model.UserName;
            dr["LoginName"] = model.LoginName;
            dr["LoginPassword"] = model.LoginPassword;
            dr["UserMobile"] = model.UserMobile;
            dr["OrgName"] = model.OrgName;

            DB.Update(sql, spc, dt);
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        public void DeleteUser(int userID)
        {
            if (this.IsUseInnerTransaction) DB.BeginTransaction();
            try
            {
                // 删除角色
                DeleteUserRole(userID);

                // 删除用户
                SqlParameterCollection spc = DB.CreateSqlParameterCollection();
                spc.Add("@UserID", userID);

                string sql = "delete from g_user where UserID=@UserID";
                DB.ExecuteNonQuerySql(sql, spc);

                if (this.IsUseInnerTransaction) DB.CommitTransaction();
            }
            catch
            {
                if (this.IsUseInnerTransaction) DB.RollbackTransaction();

                throw;
            }
        }

        /// <summary>
        /// 判断用户名称是否存在
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        public bool UserExists(int userID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);

            string sql = "select count(1) from g_user where UserID=@UserID";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断用户名称是否存在
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <returns></returns>
        public bool UserExists(string loginName)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@LoginName", loginName);

            string sql = "select count(1) from g_user where LoginName=@LoginName";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断登录是否成功
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <param name="LoginPassword">登录密码</param>
        /// <returns>返回登录是否成功</returns>
        public bool Login(string loginName, string LoginPassword)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@LoginName", loginName);
            spc.Add("@LoginPassword", LoginPassword);

            string sql = "select count(1) from g_user where LoginName=@LoginName and LoginPassword=@LoginPassword";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <param name="newLoginPassword">新密码</param>
        public void ChangePassword(string loginName, string newLoginPassword)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@LoginName", loginName);
            spc.Add("@LoginPassword", newLoginPassword);

            string sql = "update g_user set LoginPassword=@LoginPassword where LoginName=@LoginName";
            DB.ExecuteNonQuerySql(sql, spc);
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <returns>返回新密码</returns>
        public string PasswordRecovery(string loginName)
        {
            Random r = new Random();
            string newLoginPassword = r.Next(100000, 999999).ToString();
            ChangePassword(loginName, newLoginPassword);
            return newLoginPassword;
        }

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchUser(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select *  from g_user where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "UserID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion 用户

        #region 角色

        /// <summary>
        /// 增加一个角色
        /// </summary>
        /// <param name="model">角色信息</param>
        public RoleModel CreateRole(RoleModel model)
        {
            string sql = "select * from g_Role where 1=2";
            DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");
            DataRow dr = dt.NewRow();

            int maxID = DB.ExeSqlForObject("select max(RoleID) from g_Role", null).ToInt32(0);
            if (maxID < 10001)
                model.RoleID = 10001;
            else
                model.RoleID = maxID.ToInt32() + 1;

            dr["RoleID"] = model.RoleID;
            dr["RoleName"] = model.RoleName;
            dr["RoleDesc"] = model.RoleDesc;
            dr["Sort"] = model.Sort;

            dt.Rows.Add(dr);

            DB.Update(sql, null, dt);

            return model;
        }

        /// <summary>
        /// 读取一个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>返回角色信息</returns>
        public RoleModel ReadRole(int roleID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleID", roleID);

            string sql = "select * from g_Role where RoleID=@RoleID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count != 1) return null;

            RoleModel model = new RoleModel();
            model.RoleID = dt.Rows[0]["RoleID"].ToInt32();
            model.RoleName = dt.Rows[0]["RoleName"].ToString();
            model.RoleDesc = dt.Rows[0]["RoleDesc"].ToString();
            model.Sort = dt.Rows[0]["Sort"].ToInt32();

            return model;
        }

        /// <summary>
        /// 更新一个角色
        /// </summary>
        /// <param name="model">角色信息</param>
        public void UpdateRole(RoleModel model)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleID", model.RoleID);

            string sql = "select * from g_Role where RoleID=@RoleID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count != 1) return;

            DataRow dr = dt.Rows[0];

            dr["RoleName"] = model.RoleName;
            dr["RoleDesc"] = model.RoleDesc;
            dr["Sort"] = model.Sort;

            DB.Update(sql, spc, dt);
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        public void DeleteRole(int roleID)
        {
            if (this.IsUseInnerTransaction) DB.BeginTransaction();
            try
            {
                SqlParameterCollection spc = DB.CreateSqlParameterCollection();
                spc.Add("@RoleID", roleID);

                // 删除用户角色关系
                string sql = "delete from g_UserRole where RoleID=@RoleID";
                DB.ExecuteNonQuerySql(sql, spc);

                // 删除角色
                sql = "delete from g_Role where RoleID=@RoleID";
                DB.ExecuteNonQuerySql(sql, spc);

                if (this.IsUseInnerTransaction) DB.CommitTransaction();
            }
            catch
            {
                if (this.IsUseInnerTransaction) DB.RollbackTransaction();

                throw;
            }
        }

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public bool RoleExists(int roleID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleID", roleID);

            string sql = "select count(1) from g_Role where RoleID=@RoleID";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool RoleExists(string roleName)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleName", roleName);

            string sql = "select count(1) from g_Role where RoleName=@RoleName";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 查询角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchRole(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select distinct a.* from g_Role a left join g_RoleRight b on a.RoleID =b.RoleID "
            + " left join g_System c on b.SystemID =c.SystemID "
            + " left join g_Module f on b.ModuleID =f.ModuleID "
            + " left join g_Operate g on b.OperateID =g.OperateID "
            + " left join g_DataType h on b.DataTypeID =h.DataTypeID   where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "Sort,RoleID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion 角色

        #region 用户角色关系

        /// <summary>
        /// 保存用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        public void SaveUserRole(int userID, int roleID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);
            spc.Add("@RoleID", roleID);

            string sql = "select * from g_UserRole where UserID=@UserID and RoleID=@RoleID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[0]["UserID"] = userID;
                dt.Rows[0]["RoleID"] = roleID;
                DB.Update(sql, spc, dt);
            }
        }

        /// <summary>
        /// 删除用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        public void DeleteUserRole(int userID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);

            string sql = "delete from g_UserRole where UserID=@UserID";
            DB.ExecuteNonQuerySql(sql, spc);
        }

        /// <summary>
        /// 删除一个用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        public void DeleteUserRole(int userID, int roleID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);
            spc.Add("@RoleID", roleID);

            string sql = "delete from g_UserRole where UserID=@UserID and RoleID=@RoleID";
            DB.ExecuteNonQuerySql(sql, spc);
        }

        /// <summary>
        /// 判断用户角色关系是否存在
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        public bool UserRoleExists(int userID, int roleID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", userID);
            spc.Add("@RoleID", roleID);

            string sql = "select count(1) from g_UserRole where UserID=@UserID and RoleID=@RoleID";
            return DB.ExeSqlForObject(sql, spc).ToInt32() > 0;
        }

        /// <summary>
        /// 查询用户角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchUserRole(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_v_UserRole where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "UserID,RoleSort,RoleID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 查询用户角色数据, 将没有建立关系的也显示出来
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchUserRole2(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            /*
            string sql = @"select distinct (case when isnull(c.RoleID,-1)=-1 then cast(0 as bit) else cast(1 as bit) end) as HaveRelation,
                                  a.UserID, a.LoginName, a.UserName, b.RoleID, b.RoleName 
                            from g_user a
                            left join g_Role b on b.RoleID > 0
                            left join g_UserRole c on c.UserID =a.UserID and c.RoleID = b.RoleID ";

            if (!string.IsNullOrEmpty(condition.GetDataItem("SystemID").ItemData))  //此时，仅查询有权限的角色
            {
                sql += " inner join g_RoleRight d on d.RoleID = b.RoleID ";
            }

            sql += " where 1=1 ";
            */
            //上面这种采用distinct的写法，会碰到严重的效率问题，故换成下面这种写法。

            string sql = @"select (case when isnull(c.RoleID,-1)=-1 then cast(0 as bit) else cast(1 as bit) end) as HaveRelation,
                                  a.UserID, a.LoginName, a.UserName, b.RoleID, b.RoleName 
                            from g_user a
                            inner join g_Role b on 1=1
                            left join g_UserRole c on c.UserID =a.UserID and c.RoleID = b.RoleID ";

            string roleIdInSql = string.Empty;
            if (!string.IsNullOrEmpty(condition.GetDataItem("SystemID").ItemData))  //此时，仅查询有权限的角色
            {
                roleIdInSql = " select roleid from g_RoleRight d where d.SystemId = " + condition.GetDataItem("SystemID").ItemData;
                condition.Remove(condition.GetDataItem("SystemID"));
                if (!string.IsNullOrEmpty(condition.GetDataItem("ModuleID").ItemData))
                {
                    roleIdInSql += " and d.ModuleID = " + condition.GetDataItem("ModuleID").ItemData;
                    condition.Remove(condition.GetDataItem("ModuleID"));
                }
                if (!string.IsNullOrEmpty(condition.GetDataItem("OperateID").ItemData))
                {
                    roleIdInSql += " and d.OperateID = " + condition.GetDataItem("OperateID").ItemData;
                    condition.Remove(condition.GetDataItem("OperateID"));
                }
                if (!string.IsNullOrEmpty(condition.GetDataItem("DataTypeID").ItemData))
                {
                    roleIdInSql += " and d.DataTypeID = " + condition.GetDataItem("DataTypeID").ItemData;
                    condition.Remove(condition.GetDataItem("DataTypeID"));
                }
            }

            if (string.IsNullOrEmpty(roleIdInSql))
                sql += " where 1=1 ";
            else
                sql += " where b.roleid in ( " + roleIdInSql + ") ";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "UserID, RoleID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion 用户角色关系

        #region 系统

        /// <summary>
        /// 读取一个系统
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回系统信息</returns>
        public SystemModel ReadSystem(int systemID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);

            string sql = "select * from g_System where SystemID=@SystemID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count != 1) return null;

            SystemModel model = new SystemModel();
            model.SystemID = dt.Rows[0]["SystemID"].ToInt32();
            model.SystemName = dt.Rows[0]["SystemName"].ToString();
            model.SystemDesc = dt.Rows[0]["SystemDesc"].ToString();
            model.Sort = dt.Rows[0]["Sort"].ToInt32();

            return model;
        }

        /// <summary>
        /// 查询系统数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchSystem(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_System where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "Sort";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion 系统

        #region 模块

        /// <summary>
        /// 查询模块数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        public DataTable SelectModule(int systemID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);

            string sql = "select * from g_Module where SystemID=@SystemID order by Sort asc";
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        #endregion 模块

        #region 操作

        /// <summary>
        /// 查询操作数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        public DataTable SelectOperate(int systemID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);

            string sql = "select * from g_Operate where SystemID=@SystemID order by Sort asc";
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        #endregion 操作

        #region 业务数据分类

        /// <summary>
        /// 查询业务数据分类数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        public DataTable SelectDataType(int systemID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);

            string sql = "select * from g_DataType where SystemID=@SystemID order by Sort asc";
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        #endregion 业务数据分类


        #region 角色权限关系

        /// <summary>
        /// 保存角色权限关系
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleID">模块编号</param>
        /// <param name="operateID">操作编号</param>
        /// <param name="dataTypeID">业务数据分类编号</param>
        public void SaveRoleRight(int roleID, int systemID, int moduleID, int operateID, int dataTypeID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleID", roleID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleID", moduleID);
            spc.Add("@OperateID", operateID);
            spc.Add("@DataTypeID", dataTypeID);

            string sql = "select * from g_RoleRight where RoleID=@RoleID and SystemID=@SystemID and ModuleID=@ModuleID and OperateID=@OperateID and DataTypeID=@DataTypeID";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[0]["RoleID"] = roleID;
                dt.Rows[0]["SystemID"] = systemID;
                dt.Rows[0]["ModuleID"] = moduleID;
                dt.Rows[0]["OperateID"] = operateID;
                dt.Rows[0]["DataTypeID"] = dataTypeID;
                DB.Update(sql, spc, dt);
            }
        }

        /// <summary>
        /// 删除一个角色权限关系
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleID">模块编号</param>
        /// <param name="operateID">操作编号</param>
        /// <param name="dataTypeID">业务数据分类编号</param>
        public void DeleteRoleRight(int roleID, int systemID, int moduleID, int operateID, int dataTypeID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@RoleID", roleID);
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleID", moduleID);
            spc.Add("@OperateID", operateID);
            spc.Add("@DataTypeID", dataTypeID);

            string sql = "delete from g_RoleRight where RoleID=@RoleID and SystemID=@SystemID and ModuleID=@ModuleID and OperateID=@OperateID and DataTypeID=@DataTypeID";
            DB.ExecuteNonQuerySql(sql, spc);
        }

        /// <summary>
        /// 查询角色权限数据，不显示没有设置的权限
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchRoleRight(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_v_RoleRight where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "RoleSort,RoleID,SystemSort,SystemID,ModuleSort,ModuleID,OperateSort,OperateID,DataTypeSort,DataTypeID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 查询角色权限数据，将没有设置的权限也显示出来
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="systemID">系统编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        public DataTable SearchRoleRight(int roleID, int systemID, List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("RoleID", roleID);
            spc.Add("SystemID", systemID);

            string sql = @"select (case when isnull(f.ModuleID,-1)=-1 then cast(0 as bit) else cast(1 as bit) end) as Selected,
                                a.ModuleID,a.ModuleCode,a.ModuleName,a.Sort as ModuleSort,
                                b.OperateID,b.OperateCode,b.OperateName,b.Sort as OperateSort,
                                c.DataTypeID,c.DataTypeCode,c.DataTypeName,c.Sort as DataTypeSort,
                                d.SystemID,e.RoleID
                            from g_Module as a
                            inner join g_Operate as b on b.SystemID=@SystemID
                            inner join g_DataType as c on c.SystemID=@SystemID
                            inner join g_System as d on d.SystemID=@SystemID 
                            inner join g_Role as e on e.RoleID=@RoleID
                            left join g_RoleRight as f on f.ModuleID=a.ModuleID 
                            and f.OperateID=b.OperateID and f.DataTypeID=c.DataTypeID and f.SystemID=d.SystemID and f.RoleID=e.RoleID";

            sql = "select * from ( " + sql + " ) as _t1 where 1=1 ";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "ModuleSort,ModuleID,OperateSort,OperateID,DataTypeSort,DataTypeID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 查询角色权限数据，将没有设置的权限也显示出来
        /// </summary>
        public DataTable SearchRoleRight2(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = @"select (case when isnull(f.ModuleID,-1)=-1 then cast(0 as bit) else cast(1 as bit) end) as HaveRight,
                                a.SystemID,a.SystemName,
                                b.ModuleID,b.ModuleCode,b.ModuleName,b.Sort as ModuleSort,
                                c.OperateID,c.OperateCode,c.OperateName,c.Sort as OperateSort,
                                d.DataTypeID,d.DataTypeCode,d.DataTypeName,d.Sort as DataTypeSort,
                                e.RoleID,e.RoleName
                            from g_System as a
                            inner join g_Module as b on b.SystemID=a.SystemID
                            inner join g_Operate as c on c.SystemID=a.SystemID
                            inner join g_DataType as d on d.SystemID=a.SystemID 
                            inner join g_Role as e on e.RoleID > 0
                            left join g_RoleRight as f on f.SystemID=a.SystemID and f.ModuleID=b.ModuleID 
                            and f.OperateID=c.OperateID and f.DataTypeID=d.DataTypeID and f.RoleID=e.RoleID";

            sql = "select * from ( " + sql + " ) as _t1 where 1=1 ";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "RoleID,SystemID,ModuleSort,ModuleID,OperateSort,OperateID,DataTypeSort,DataTypeID";
            return DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        #endregion 角色权限关系
    }
}
