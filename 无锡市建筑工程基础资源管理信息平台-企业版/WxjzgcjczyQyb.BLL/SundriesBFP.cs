using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Business.UserRightManager;
using Bigdesk8.Business.FileManager;
using Bigdesk8.Data;
using WxjzgcjczyQyb.DAL;
using System.Transactions;



namespace WxjzgcjczyQyb.BLL
{
    /// <summary>
    /// 杂物(sundries)功能包 的业务逻辑层
    /// </summary>
    public class SundriesBFP
    {
        private HospiceDAL hospiceDal;

        public SundriesBFP()
        {
            hospiceDal = new HospiceDAL();
            hospiceDal.DB = new DatabaseOperator();
        }

        #region CodeType
        public FunctionResult<DataTable> GetCode_CodeInfo(CodeType codeType, bool isKeyValue)
        {
            return new FunctionResult<DataTable> { Result = hospiceDal.GetCode_CodeInfo(codeType, isKeyValue) };
        }

        //直接用SQL绑定LIST
        public FunctionResult<DataTable> GetDTBySql(string SQl)
        {
            return new FunctionResult<DataTable> { Result = hospiceDal.GetDTBySql(SQl) };
        }
        #endregion

        /// <summary>
        /// 读代码表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="codetype">代码类型</param>
        /// <returns></returns>
        public DataRow ReadCode(string code, string codetype)
        {
            DataTable dt_code = hospiceDal.ReadCode(code, codetype);
            return dt_code.Rows.Count == 0 ? null : dt_code.Rows[0];
        }

        public FunctionResult<DataTable> GetCode(string parentCodeType, string parentCode, string codeType)
        {
            DataTable dt_code = hospiceDal.GetCode(parentCodeType, parentCode, codeType);
            return new FunctionResult<DataTable>() { Result = dt_code };
        }

        public FunctionResult<DataSet> GetUserRealName(string LoginID)
        {
            return new FunctionResult<DataSet> { Result = hospiceDal.GetUserRealName(LoginID) };
        }

        //参数类型是Image对象，返回Byte[]类型:
        public byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        {
            //将Image转换成流数据，并保存为byte[]
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }


        //通过userid查询userinfo表
        public FunctionResult<DataRow> ReadUser(string userID)
        {
            DataTable dt = hospiceDal.ReadUser(Convert.ToInt32(userID));
            return new FunctionResult<DataRow>() { Result = dt.Rows.Count > 0 ? dt.Rows[0] : null };
        }


        #region 初始化用户信息

        /// <summary>
        /// 2. 初始化一个应用系统用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密  码</param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUser(string username, string password)
        {
            DataTable dtUser = hospiceDal.ReadUser(username, password);
            if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            else
            {
                if (dtUser.Rows[0]["UserType"].ToString2() != "管理用户")
                {
                    return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户类型不符合登录条件（只有管理用户可以登录）！") };                       
                }
            }

            return new FunctionResult<AppUser> { Result = GenerateUserInfo(dtUser) };
        }
        /// <summary>
        /// 3. 初始化一个应用系统用户
        /// </summary>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUser(int loginid)
        {
            DataTable dtUser = hospiceDal.ReadUser(loginid);
            if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            return new FunctionResult<AppUser> { Result = GenerateUserInfo(dtUser) };
        }
        /// <summary>
        /// 3. 初始化一个应用系统用户
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUser(string loginname)
        {
            DataTable dtUser = hospiceDal.ReadUser(loginname);
            if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            return new FunctionResult<AppUser> { Result = GenerateUserInfo(dtUser) };
        }

        /// <summary>
        /// 初始化一个应用系统用户
        /// </summary>
        /// <param name="zzjgdm">组织机构代码</param>
        /// <returns></returns>
        public FunctionResult<AppUser> InitAppUserByzzjgdm(string zzjgdm)
        {
            DataTable dtUser = hospiceDal.ReadUserByzzjgdm(zzjgdm);
            int LoginID;
            if (dtUser.Rows.Count > 1)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户冲突！") };
            }
            else if (dtUser.Rows.Count <= 0)
            {
                return new FunctionResult<AppUser>() { Status = FunctionResultStatus.Error, Message = new Exception("用户不存在！") };
            }
            else
            {
                LoginID = Convert.ToInt32(dtUser.Rows[0]["UserID"]);
                return this.InitAppUser(LoginID);
            }

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
            workUser.UserType = (UserType)Enum.Parse(typeof(UserType), dtUser.Rows[0]["UserType"].ToString2());
            return workUser;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 用户所具有的权限(针对 模块)
        /// </summary>
        public bool GetUserRight(string moduleCode, string LoginID)
        {
            return hospiceDal.GetUserRight(moduleCode, LoginID);
        }
        /// <summary>
        /// 获得用户是否有新增的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserAddRight(string moduleCode, string LoginID)
        {
            return hospiceDal.GetUserAddRight(moduleCode, LoginID);
        }

        /// <summary>
        /// 获得用户是否有编辑的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserEditRight(string moduleCode, string LoginID)
        {
            return hospiceDal.GetUserEditRight(moduleCode, LoginID);
        }

        /// <summary>
        /// 获得用户是否有删除的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserDelRight(string moduleCode, string LoginID)
        {
            return hospiceDal.GetUserDelRight(moduleCode, LoginID);
        }

        /// <summary>
        /// 获得用户是否有 审核/调动/发放/统计 权限的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserVerifyRight(string moduleCode, string LoginID)
        {
            return hospiceDal.GetUserVerifyRight(moduleCode, LoginID);
        }

        /// <summary>
        /// 得到数据库服务器时间
        /// </summary>
        public DateTime GetServerDateTime()
        {
            return hospiceDal.GetServerDateTime();
        }

        #endregion


    }
}