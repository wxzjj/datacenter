using System;
using System.Collections.Generic;
using Bigdesk8.Data;

namespace Bigdesk8.Business.UserRightManager
{
    /// <summary>
    /// 用户权限信息接口，主要针对一个用户权限信息
    /// </summary>
    public interface IUserRightInfo
    {
        //IUserRightInfo(int userID);
        //IUserRightInfo(string loginName);

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        #region 属性

        /// <summary>
        /// 用户是否存在
        /// </summary>
        [Obsolete("请改用属性CountOfUserMatched")]
        bool Exists { get; }

        /// <summary>
        /// 用户是否是匿名用户
        /// </summary>
        bool IsGuest { get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        int UserID { get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 登录名称
        /// </summary>
        string LoginName { get; }

        /// <summary>
        /// 科室  张鎏添加
        /// </summary>
        string OrgUnitName { get; }

        BasicUserType UserType { get; }

        /// <summary>
        /// 符合构造函数条件的用户的个数
        /// </summary>
        /// 该死的数据库中可能存在重复的用户记录
        int CountOfUserMatched { get; }

        #endregion

        #region 方法


        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        bool HasRole(int roleID);

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        bool HasRole(string roleName);


        /// <summary>
        /// 判断用户是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns></returns>
        bool HasRight(int systemID);

        /// <summary>
        /// 判断用户对某个模块是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <returns></returns>
        bool HasRight(int systemID, string moduleCode);

        /// <summary>
        /// 判断用户对某个模块的某个操作是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns></returns>
        bool HasRight(int systemID, string moduleCode, string operateCode);

        /// <summary>
        /// 判断用户对某个模块的某个操作的某个业务数据分类是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <param name="dataTypeCode">业务数据分类代码</param>
        /// <returns></returns>
        bool HasRight(int systemID, string moduleCode, string operateCode, string dataTypeCode);

        /// <summary>
        /// 判断用户对某个操作是否具有权限
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns></returns>
        bool HasRight2(int systemID, string operateCode);



        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        Dictionary<string, string> SelectDataType(int systemID, string moduleCode);

        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        Dictionary<string, string> SelectDataType(int systemID, string moduleCode, string operateCode);

        /// <summary>
        /// 获得用户具有操作权限的业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=DataTypeCode,value=DataTypeName</returns>
        Dictionary<string, string> SelectDataType2(int systemID, string operateCode);


        /// <summary>
        /// 获得用户的模块
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回值key=ModuleCode,value=ModuleName</returns>
        Dictionary<string, string> SelectModule(int systemID);

        /// <summary>
        /// 获得用户具有操作权限的模块
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=ModuleCode,value=ModuleName</returns>
        Dictionary<string, string> SelectModule(int systemID, string operateCode);


        /// <summary>
        /// 获得用户的模块业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <returns>返回值key=ModuleCode;DataTypeCode,value=ModuleName;DataTypeName</returns>
        Dictionary<string, string> SelectModuleDataType(int systemID);

        /// <summary>
        /// 获得用户的模块业务数据分类
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="operateCode">操作代码</param>
        /// <returns>返回值key=ModuleCode;DataTypeCode,value=ModuleName;DataTypeName</returns>
        Dictionary<string, string> SelectModuleDataType(int systemID, string operateCode);

        #endregion 方法
    }
}