using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8.Data;
using Wxjzgcjczy.DAL.Sqlserver;

namespace Wxjzgcjczy.BLL
{
    public class YhglBLL
    {
        public AppUser WorkUser { get; set; }
        private readonly YhglDAL DAL = new YhglDAL();

        public YhglBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();

        }

        public FunctionResult<DataTable> ReadUser(string userID)
        {
            DataTable dt = DAL.ReadUser(userID);
            return new FunctionResult<DataTable>() { Result = dt };

        }
        public FunctionResult<DataTable> ReadRole(string roleID)
        {
            DataTable dt = DAL.ReadRole(roleID);
            return new FunctionResult<DataTable>() { Result = dt };

        }

        public FunctionResult<DataTable> Retrieve_g_user(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            if (string.IsNullOrEmpty(orderby.Trim()))
                orderby = " USERREGTIME desc ";
            DataTable dt = DAL.Retrieve_g_user(WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> Retrieve_RoleRight_List(string userID,FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            if (string.IsNullOrEmpty(orderby.Trim()))
                orderby = " Sort desc ";
            DataTable dt = DAL.Retrieve_RoleRight_List(userID,WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> Get_RoleModules_List(string roleID)
        {
            DataTable dt = DAL.Get_RoleModules_List(WorkUser, roleID);
            return new FunctionResult<DataTable>() { Result = dt };
        }
        public FunctionResult<DataTable> Get_UserRights_List()
        {
            DataTable dt = DAL.Get_UserRights_List(WorkUser);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public FunctionResult<DataTable> Get_ModuleOperators_List(string roleID, string moduleCode)
        {
            DataTable dt = DAL.Get_ModuleOperators_List(WorkUser, roleID, moduleCode);
            return new FunctionResult<DataTable>() { Result = dt };
        }


        public bool Add_User(List<IDataItem> list)
        {
            DataTable dt = DAL.GetSchema_User();
            dt.Rows.Add(dt.NewRow());
            list.ToDataRow(dt.Rows[0]);
            dt.Rows[0]["UserID"] = DAL.GetNewID_User();

            dt.Rows[0]["UserType"] = "管理用户";
            dt.Rows[0]["UserState"] = "正常用户";
            dt.Rows[0]["USERREGTIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return DAL.SubmitUser(dt);
        }

        public bool Add_Role(List<IDataItem> list)
        {
            DataTable dt = DAL.GetSchema_Role();
            dt.Rows.Add(dt.NewRow());
            list.ToDataRow(dt.Rows[0]);
            dt.Rows[0]["RoleID"] = DAL.GetNewID_Role();
            dt.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt.Rows[0]["UpdateDate"] = dt.Rows[0]["CreateDate"];
            dt.Rows[0]["Sort"] = dt.Rows[0]["RoleID"];
            return DAL.SubmitRole(dt);
        }

        public bool Update_UserRoles(string userId,string roleIds)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }


            bool dt_HasChanged = false;

            DataTable dt = DAL.Get_UserRoles_List(userId);

            if (string.IsNullOrEmpty(roleIds))
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row.Delete();
                    }
                   return DAL.SubmitUserRole(dt);
                }
                else
                {
                    return true;
                }
            }

            List<string> roleIDs = roleIds.Split(',').ToList<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (!roleIDs.Exists(p => p == row["RoleID"].ToString2()))
                {
                    row.Delete();
                    dt_HasChanged = true;
                }
            }

            foreach (string item in roleIDs)
            {
                int flag = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if (item == row["RoleID"].ToString2())
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["UserID"] = userId;
                    row["RoleID"] = item;
                    dt_HasChanged = true;
                }
            }
            if (dt_HasChanged)
                return DAL.SubmitUserRole(dt);
            else
                return true;
        }


        public bool Update_RoleRights(string roleId, List<ModuleOperate> list)
        {
            bool dt_HasChanged = false;

            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }

            DataTable dt = DAL.Get_RoleRights_List(WorkUser, roleId);
            foreach (DataRow row in dt.Rows)
            {
                if (!list.Exists(p => p.moduleCode.Equals(row["ModuleCode"].ToString2(), StringComparison.CurrentCultureIgnoreCase) && p.operateCode.Equals(row["OperateCode"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    row.Delete();
                    dt_HasChanged = true;
                }
            }

            foreach (ModuleOperate item in list)
            {
                int flag = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if (item.moduleCode.Equals(row["ModuleCode"].ToString2(), StringComparison.CurrentCultureIgnoreCase)
                        && item.operateCode.Equals(row["OperateCode"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag = 1;
                        break;
                    }

                }
                if (flag == 0)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["SystemID"] = 1;
                    row["RoleID"] = roleId;
                    row["ModuleCode"] = item.moduleCode;
                    row["OperateCode"] = item.operateCode;
                    dt_HasChanged = true;
                }
            }
            if (dt_HasChanged)
                return DAL.SubmitRoleRight(dt);
            else
                return true;
        }

        public bool Edit_User(string userID, List<IDataItem> list)
        {
            DataTable dt = DAL.ReadUser(userID);
            if (dt.Rows.Count > 0)
            {
                list.ToDataRow(dt.Rows[0]);
                return DAL.SubmitUser(dt);
            }
            else
            {
                return false;
            }
        }
        public bool Edit_Role(string roleID, List<IDataItem> list)
        {
            DataTable dt = DAL.ReadRole(roleID);
            if (dt.Rows.Count > 0)
            {
                list.ToDataRow(dt.Rows[0]);
                dt.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return DAL.SubmitRole(dt);
            }
            else
            {
                return false;
            }
        }


        public FunctionResult<string> DeleteUser(string UserID)
        {
            DataTable dt = this.ReadUser(UserID).Result;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["UserState"] = "删除用户";
                if (DAL.SubmitUser(dt))
                    return new FunctionResult<string>() { Status = FunctionResultStatus.None };
                else
                    return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("用户删除失败！") };
            }
            else
            {
                return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("不存在此用户！") };
            }
        }
        public FunctionResult<string> DeleteRole(string roleID)
        {
            DataTable dt = this.ReadRole(roleID).Result;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0].Delete();

                if (DAL.SubmitRole(dt))
                    return new FunctionResult<string>() { Status = FunctionResultStatus.None };
                else
                    return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("角色删除失败！") };
            }
            else
            {
                return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("不存在此角色！") };
            }
        }
    }
}
