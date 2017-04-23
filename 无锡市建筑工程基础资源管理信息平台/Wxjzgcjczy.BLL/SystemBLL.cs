/****************************************************
*
* Copyright(C)2013 南京群耀软件系统有限公司 版权所有
*
* 文 件 名：SystemBLL.cs
*
* 作 者：张鎏
*
* 创建日期：2013-11-16
*
* 说 明：用户登录及相关权限的处理
***************************************************/
using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using Wxjzgcjczy.Common;
using Wxjzgcjczy.DAL.Sqlserver;


namespace Wxjzgcjczy.BLL
{
    public class SystemBLL
    {
        #region BaseCode

        private SystemDAL systemDal;

        public DBOperator DB { get; set; }
        public SystemBLL()
        {
            systemDal = new SystemDAL();
            systemDal.DB = new DatabaseOperator();
        }

        #endregion

        /// <summary>
        /// 1. 用户登录判断
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密  码</param>
        /// <returns>true or false</returns>
        public bool UserLogin(string username, string password)
        {
            bool loginSuccess = systemDal.Login(username, password);

            return loginSuccess;
        }
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="datetime">最后一次登录时间</param>
        public void UpdateLoginTime(string userid, string datetime)
        {
            systemDal.UpdateLastLoginTime(userid, datetime);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="password">password</param>
        public void ChangePwd(string userid, string password)
        {
            systemDal.ChangePwd(userid, password);
        }

        /// <summary>
        /// 2. 初始化一个应用系统用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密  码</param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUser(string username, string password)
        {
            DataTable dtUser = systemDal.GetUserInfo(username, password);
            if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            return new FunctionResult<AppUser> { Result = GenerateUserInfo(dtUser) };
        }
        /// <summary>
        /// 3. 初始化一个应用系统用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密  码</param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUser(string userid)
        {
            DataTable dtUser = systemDal.GetUserInfo(userid);
            if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            return new FunctionResult<AppUser> { Result = GenerateUserInfo(dtUser) };
        }

        /// <summary>
        /// 根据登录名获取用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUserByLoginName(string loginName)
        {
            FunctionResult<AppUser> result = new FunctionResult<AppUser>();
            DataTable dtUser = systemDal.GetUserInfoByLoginName(loginName);
            if (dtUser.Rows.Count <= 0)
            {
                result.Status = FunctionResultStatus.Error;
                result.Message = new Exception("用户不存在！");
            }
            else
            {
                result.Result = GenerateUserInfo(dtUser);
                result.Status = FunctionResultStatus.None;
            }
            return result;
        }



        /// <summary>
        /// 3. 形成用户信息
        /// </summary>
        /// <param name="dtUser"></param>
        /// <returns></returns>
        private AppUser GenerateUserInfo(DataTable dtUser)
        {
            AppUser workUser = new AppUser();
            workUser.UserID = dtUser.Rows[0]["UserID"].ToString();
            workUser.LoginName = dtUser.Rows[0]["LoginName"].ToString();
            workUser.UserName = dtUser.Rows[0]["UserName"].ToString();
            workUser.LastLoginTime = dtUser.Rows[0]["LastLoginTime"].ToString();
            workUser.qyID = dtUser.Rows[0]["JGID"].ToString2();
            workUser.zzjgdm = dtUser.Rows[0]["ZZJGDM"].ToString2();

            string userType = dtUser.Rows[0]["UserType"].ToString2();
            if (!string.IsNullOrEmpty(userType))
                workUser.UserType = (UserType)Enum.Parse(typeof(UserType), userType);

            //权限相关
            YhglBLL yhglBLL = new YhglBLL(workUser);
            DataTable dt = yhglBLL.Get_UserRights_List().Result;
            List<ModuleOperate> list = new List<ModuleOperate>();
            ModuleOperate model;
            foreach (DataRow row in dt.Rows)
            {
                model = new ModuleOperate();
                model.moduleCode = row["ModuleCode"].ToString2();
                model.operateCode = row["OperateCode"].ToString2();
                list.Add(model);
            }
            workUser.list = list;

            //if (workUser.UserType.ToString() == UserType.监理单位.ToString()  || workUser.UserType.ToString() == UserType.代理机构.ToString() || workUser.UserType.ToString() == UserType.申报部门.ToString())
            //{
            //    DataTable dt = systemDal.GetQyInfo(workUser.UserID);
            //    if (dt.Rows.Count > 0)
            //    {
            //        workUser.qyID = dt.Rows[0]["qyID"].ToString();
            //        workUser.zzjgdm = dt.Rows[0]["zzjgdm"].ToString();
            //    }
            //}
            //else if (workUser.UserType.ToString() == UserType.实施单位.ToString())
            //{
            //    DataTable dt = systemDal.GetJsdwInfo(workUser.UserID);
            //    if (dt.Rows.Count > 0)
            //    {
            //        workUser.qyID = dt.Rows[0]["jsdwID"].ToString();
            //        workUser.zzjgdm = dt.Rows[0]["zzjgdm"].ToString();
            //    }
            //}
            //else
            //{
            //    workUser.qyID = string.Empty;
            //    workUser.zzjgdm = string.Empty;
            //}
            return workUser;
        }

        //public DataTable ReadGwtz(AppUser workUser)
        //{
        // return   systemDal.ReadGwtz(workUser);
        //}



        #region 导出Excel
        public DataSet GetDs(string start, string end)
        {
            return systemDal.GetDs(start, end);
        }

        public DataTable GetStLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string sfsccg)
        {
            return systemDal.GetStLog(condition, pageSize, pageIndex, out   allRecordCount, sfsccg, " UpdateDate desc");
        }

        #endregion
    }
}
