using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bigdesk8.Data;
using Bigdesk8;
using System.Configuration;
using System.Data;


namespace IntegrativeShow2.Common
{
    public class SystemBLL
    {
        #region BaseCode
        public SystemDAL systemDal;
        public SystemBLL()
        {
            systemDal = new SystemDAL();
            systemDal.DB = WebCommon.GetDB_WJSJZX();
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
            string userType = dtUser.Rows[0]["UserType"].ToString2();
            if (!string.IsNullOrEmpty(userType))
                workUser.UserType = (UserType)Enum.Parse(typeof(UserType), userType);

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

        public DataTable ReadGwtz(AppUser workUser)
        {
            return systemDal.ReadGwtz(workUser);
        }
    }
}
