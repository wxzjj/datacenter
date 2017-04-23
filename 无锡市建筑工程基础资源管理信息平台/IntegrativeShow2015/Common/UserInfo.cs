using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Bigdesk8.Data;

namespace IntegrativeShow2
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        private UserInfo() { }

        public UserInfo(string loginID)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();

            string sql = "select * from UserInfo where loginID=@loginID";
            p.Add("@loginID", loginID);
            DataTable dt = db.ExeSqlForDataTable(sql, p, "table");
            if (dt.Rows.Count == 1)
            {
                this.currUserInfo = dt.Rows[0];
            }
        }

        public UserInfo(string loginName, string password)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();

            string sql = "select * from UserInfo where loginName=@loginName and password=@password ";
            p.Add("@loginName", loginName);
            p.Add("@password", password);
            DataTable dt = db.ExeSqlForDataTable(sql, p, "table");
            if (dt.Rows.Count == 1)
            {
                this.currUserInfo = dt.Rows[0];
            }
        }

        #region 私有成员

        private DataRow currUserInfo = null;

        #endregion 私有成员

        #region 公共属性

        /// <summary>
        /// 登录用户 ID
        /// </summary>
        public string LoginID
        {
            get
            {
                if (this.currUserInfo == null) return WebCommon.GuestloginID;
                return this.currUserInfo["LoginID"].ToString();
            }
        }

        /// <summary>
        /// 登录用户名称
        /// </summary>
        public string LoginName
        {
            get
            {
                if (this.currUserInfo == null) return "";
                return this.currUserInfo["LoginName"].ToString();
            }
        }

        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string UserRealName
        {
            get
            {
                if (this.currUserInfo == null) return "";
                return this.currUserInfo["UserRealName"].ToString();
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType
        {
            get
            {
                if (this.currUserInfo == null) return "";
                return this.currUserInfo["UserType"].ToString();
            }
        }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return this.LoginID != WebCommon.GuestloginID;
            }
        }
       
        #endregion 公共属性

        #region 私有方法

        /// <summary>
        /// 是否需要判断权限
        /// </summary>
        public bool IsUserRight(string module)
        {
            bool result = true;
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            string sql = " select top 1 isRight from d_modules"
                + " where isRight=0 and moduleNo=@module ";
            if (db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0)
            {
                result = false;
            }
            return result;
        }
        #endregion 私有方法

        #region 公共方法

        /// <summary>
        /// 获得管理用户是否有权限
        /// </summary>
        public bool GetUserRight(string module)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            if (IsUserRight(module) == false)
            {
                return true;
            }
            else
            {
                string sql = " select top 1 loginID from V_Scgl_UserRoleRights"
                    + " where (Right_View=1 or Right_Add=1 or Right_Edit=1 or Right_Delete=1) and loginID=@loginID and moduleNo=@module ";
                return db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0;
            }
        }

        /// <summary>
        /// 管理用户是否具有查看权限
        /// </summary>
        public bool GetUserRight_View(string module)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            if (IsUserRight(module) == false)
            {
                return true;
            }
            else
            {
                string sql = "select top 1 loginID from V_Scgl_UserRoleRights"
                    + " where loginID=@loginID and (Right_View=1) and moduleNo=@module";
                return db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0;
            }
        }

        /// <summary>
        /// 管理用户是否具有添加权限
        /// </summary>
        public bool GetUserRight_Add(string module)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            if (IsUserRight(module) == false)
            {
                return true;
            }
            else
            {
                string sql = "select top 1 loginID from V_Scgl_UserRoleRights"
                    + " where loginID=@loginID and (Right_Add=1) and moduleNo=@module";
                return db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0;
            }
        }

        /// <summary>
        /// 管理用户是否具有编辑权限
        /// </summary>
        public bool GetUserRight_Edit(string module)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            if (IsUserRight(module) == false)
            {
                return true;
            }
            else
            {
                string sql = "select top 1 loginID from V_Scgl_UserRoleRights"
                    + " where loginID=@loginID and (Right_Edit=1) and moduleNo=@module";
                return db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0;
            }
        }

        /// <summary>
        /// 管理用户是否具有删除权限
        /// </summary>
        public bool GetUserRight_Delete(string module)
        {
            DBOperator db = WebCommon.GetDB_WJSJZX();
            SqlParameterCollection p = db.CreateSqlParameterCollection();
            p.Add("@loginID", this.LoginID);
            p.Add("@module", module);

            if (IsUserRight(module) == false)
            {
                return true;
            }
            else
            {
                string sql = "select top 1 loginID from V_Scgl_UserRoleRights"
                    + " where loginID=@loginID and (Right_Delete=1) and moduleNo=@module";
                return db.ExeSqlForDataTable(sql, p, "table").Rows.Count > 0;
            }
        }

        #endregion 公共方法
    }

    //public class UserType
    //{
    //    public const string 管理用户 = "管理用户";
    //}
}
