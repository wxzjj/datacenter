using System.Collections.Generic;
using System.Data;
using Bigdesk2010.Data;

namespace Bigdesk2010.Business.UserRightManager
{
    /// <summary>
    /// 用户权限管理接口，主要对多个用户权限信息进行管理
    /// 
    /// 有三种返回数据的方法Read,Select,Search;
    /// Read：返回一个数据实体，采用关键编号作为条件，返回精确查询数据
    /// Select：返回一个数据集，采用关键编号作为条件，返回精确查询数据
    /// Search：返回一个数据集，采用数据信息作为条件，返回模糊查询数据
    /// </summary>
    public interface IUserRightManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 是否使用内部事务
        /// </summary>
        bool IsUseInnerTransaction { get; set; }

        #region 用户

        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="model">用户信息</param>
        UserModel CreateUser(UserModel model);

        /// <summary>
        /// 读取一个用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>返回用户信息</returns>
        UserModel ReadUser(int userID);

        /// <summary>
        /// 查询用户权限数据，只查询有权限的数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchUserRight(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="model">用户信息</param>
        void UpdateUser(UserModel model);

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        void DeleteUser(int userID);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        bool UserExists(int userID);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <returns></returns>
        bool UserExists(string loginName);

        /// <summary>
        /// 判断登录是否成功
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <param name="LoginPassword">登录密码</param>
        /// <returns>返回登录是否成功</returns>
        bool Login(string loginName, string LoginPassword);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <param name="newLoginPassword">新密码</param>
        /// <returns>返回修改密码是否成功</returns>
        void ChangePassword(string loginName, string newLoginPassword);

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <returns>返回新密码</returns>
        string PasswordRecovery(string loginName);

        /// <summary>
        /// 查询用户数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchUser(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        #endregion 用户

        #region 角色

        /// <summary>
        /// 增加一个角色
        /// </summary>
        /// <param name="model">角色信息</param>
        RoleModel CreateRole(RoleModel model);

        /// <summary>
        /// 读取一个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>返回角色信息</returns>
        RoleModel ReadRole(int roleID);

        /// <summary>
        /// 更新一个角色
        /// </summary>
        /// <param name="model">角色信息</param>
        void UpdateRole(RoleModel model);

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        void DeleteRole(int roleID);

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        bool RoleExists(int roleID);

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        bool RoleExists(string roleName);

        /// <summary>
        /// 查询角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchRole(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        #endregion 角色

        #region 用户角色关系

        /// <summary>
        /// 保存用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        void SaveUserRole(int userID, int roleID);

        /// <summary>
        /// 删除所有用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        void DeleteUserRole(int userID);

        /// <summary>
        /// 删除一个用户角色关系
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        void DeleteUserRole(int userID, int roleID);

        /// <summary>
        /// 判断用户角色关系是否存在
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="roleID">角色编号</param>
        bool UserRoleExists(int userID, int roleID);

        /// <summary>
        /// 查询用户角色数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchUserRole(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 查询用户角色数据, 将没有建立关系的也显示出来
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        DataTable SearchUserRole2(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        #endregion 用户角色关系

        #region 系统

        /// <summary>
        /// 读取一个系统
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回系统信息</returns>
        SystemModel ReadSystem(int systemID);

        /// <summary>
        /// 查询系统数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchSystem(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        #endregion 系统

        #region 模块

        /// <summary>
        /// 查询模块数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        DataTable SelectModule(int systemID);

        #endregion 模块

        #region 操作

        /// <summary>
        /// 查询操作数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        DataTable SelectOperate(int systemID);

        #endregion 操作

        #region 业务数据分类

        /// <summary>
        /// 查询业务数据分类数据
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        DataTable SelectDataType(int systemID);

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
        void SaveRoleRight(int roleID, int systemID, int moduleID, int operateID, int dataTypeID);

        /// <summary>
        /// 删除一个角色权限关系
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleID">模块编号</param>
        /// <param name="operateID">操作编号</param>
        /// <param name="dataTypeID">业务数据分类编号</param>
        void DeleteRoleRight(int roleID, int systemID, int moduleID, int operateID, int dataTypeID);

        /// <summary>
        /// 查询角色权限数据，不显示没有设置的权限
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchRoleRight(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 查询角色权限数据，将没有设置的权限写显示出来
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <param name="systemID">系统编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        DataTable SearchRoleRight(int roleID, int systemID, List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 查询角色权限数据，将没有设置的权限也显示出来
        /// </summary>
        DataTable SearchRoleRight2(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);

        #endregion 角色权限关系
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 所在单位
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; } //2011-12-12添加
    }

    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemModel
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemID { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 系统描述
        /// </summary>
        public string SystemDesc { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
