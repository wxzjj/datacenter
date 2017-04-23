using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Bigdesk2010.Data;

namespace Bigdesk2010.Business.FileManager
{
    /// <summary>
    /// 统一文件管理器
    /// 提供文件存储，浏览等接口
    /// 使用此接口的程序，不需要关注文件如何存储。存储只需要提供文件及其属性，浏览时只需要提供文件编号
    /// </summary>
    public sealed class FileManagerFactory
    {
        /// <summary>
        /// 创建文件管理器
        /// </summary>
        /// <param name="managerName">managerName</param>
        /// <returns>返回文件管理器</returns>
        public static IFileManager CreateFileManager(string managerName)
        {
            return new DBFileManager();
        }
    }

    /// <summary>
    /// 统一文件管理器
    /// </summary>
    internal class DBFileManager : IFileManager
    {
        public DBFileManager()
        {
            this.IsUseInnerTransaction = true;
        }

        public DBOperator DB { get; set; }

        public bool IsUseInnerTransaction { get; set; }

        public SimpleFileModel AddFile(int systemID, string moduleCode, string categoryCode, string masterID, string fileName, string fileType, byte[] fileContent, FileState fileState)
        {
            if (systemID <= 0) throw new ArgumentException("参数 systemID 不能小于零！");
            if (moduleCode.IsEmpty()) throw new ArgumentException("参数 moduleCode 不能为空！");
            if (categoryCode.IsEmpty()) throw new ArgumentException("参数 categoryCode 不能为空！");
            if (masterID.IsEmpty()) throw new ArgumentException("参数 masterID 不能为空！");
            if (fileName.IsEmpty()) throw new ArgumentException("参数 fileName 不能为空！");
            if (fileType.IsEmpty()) throw new ArgumentException("参数 fileType 不能为空！");
            if (fileContent == null || fileContent.Length <= 0) throw new ArgumentException("参数 fileContent 不能为空！");

            string sql = "select isnull(max(fileid),0) from g_businessfile";

            SimpleFileModel sfm = new SimpleFileModel();
            sfm.FileID = DB.ExeSqlForObject(sql, null).ToInt32(0) + 1;
            sfm.SystemID = systemID;
            sfm.ModuleCode = moduleCode;
            sfm.CategoryCode = categoryCode;
            sfm.MasterID = masterID;

            sfm.FileName = fileName;
            sfm.FileType = fileType;
            sfm.FileSize = fileContent.Length;
            sfm.FilePath = "";

            sfm.CreateDateTime = DateTime.Now;
            sfm.FileStatus = fileState;


            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@FileID", sfm.FileID);
            spc.Add("@SystemID", sfm.SystemID);
            spc.Add("@ModuleCode", sfm.ModuleCode);
            spc.Add("@CategoryCode", sfm.CategoryCode);
            spc.Add("@MasterID", sfm.MasterID);

            spc.Add("@FileName", sfm.FileName);
            spc.Add("@FileType", sfm.FileType);
            spc.Add("@FileSize", sfm.FileSize);
            spc.Add("@FilePath", sfm.FilePath);

            spc.Add("@FileContent", fileContent);

            spc.Add("@CreateDateTime", sfm.CreateDateTime);
            spc.Add("@Status", (int)fileState);

            if (this.IsUseInnerTransaction) DB.BeginTransaction();

            try
            {
                sql = @"insert into g_BusinessFile(FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FileContent,FilePath,CreateDateTime,Status)
values(@FileID,@SystemID,@ModuleCode,@CategoryCode,@MasterID,
@FileName,@FileType,@FileSize,@FileContent,@FilePath,@CreateDateTime,@Status)"; 

//                sql = @"insert into g_BusinessFile(SystemID,ModuleCode,CategoryCode,MasterID,
//FileName,FileType,FileSize,FileContent,FilePath,CreateDateTime,Status)
//values(@SystemID,@ModuleCode,@CategoryCode,@MasterID,
//@FileName,@FileType,@FileSize,@FileContent,@FilePath,@CreateDateTime,@Status)";
                DB.ExecuteNonQuerySql(sql, spc);

                if (this.IsUseInnerTransaction) DB.CommitTransaction();
            }
            catch
            {
                if (this.IsUseInnerTransaction) DB.RollbackTransaction();

                throw;
            }

            return sfm;
        }

        public SimpleFileModel GetSimpleFileModel(int fileID)
        {
            return GetSimpleFileModel(fileID, new FileState[] { FileState.Saved, FileState.NoSaved });
        }

        public SimpleFileModel GetSimpleFileModel(int fileID, FileState[] fileStates)
        {
            SimpleFileModel[] sfm = GetSimpleFileModel(new int[] { fileID }, fileStates);
            return sfm.Length > 0 ? sfm[0] : null;
        }

        public SimpleFileModel[] GetSimpleFileModel(int[] fileIDs)
        {
            return GetSimpleFileModel(fileIDs, new FileState[] { FileState.Saved, FileState.NoSaved });
        }

        public SimpleFileModel[] GetSimpleFileModel(int[] fileIDs, FileState[] fileStates)
        {
            if (fileIDs == null || fileIDs.Length <= 0) return new SimpleFileModel[0];

            string sql = "";
            if (fileStates.Length > 0)
            {
                for (int i = 0; i < fileStates.Length; i++)
                {
                    if (i == 0)
                        sql += (int)fileStates[i];
                    else
                        sql += "," + (int)fileStates[i];
                }

                sql = string.Format(" and Status in ({0})", sql);
            }
            sql += string.Format(" and FileID in ({0})", fileIDs.ArrayToString(","));

            sql = string.Format(@"select FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FilePath,CreateDateTime,Status
from g_BusinessFile where 1=1{0}", sql);

            DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");
            return ToSimpleFileModel(dt).ToArray();
        }

        public byte[] GetFileContent(int fileID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@FileID", fileID);

            string sql = "select FileContent from g_BusinessFile where FileID=@FileID";
            return (byte[])DB.ExeSqlForObject(sql, spc);
        }

        public void DeleteFile(int fileID)
        {
            DeleteFile(new int[] { fileID });
        }

        public void DeleteFile(int[] fileIDs)
        {
            string sql = string.Format("update g_BusinessFile set Status={0} where FileID in ({1})",
                (int)FileState.Deleted,
                fileIDs.ArrayToString(","));
            DB.ExecuteNonQuerySql(sql, null);
        }

        public void SaveFile(int fileID)
        {
            SaveFile(new int[] { fileID });
        }

        public void SaveFile(int[] fileIDs)
        {
            string sql = string.Format("update g_BusinessFile set Status={0} where FileID in ({1})",
                (int)FileState.Saved,
                fileIDs.ArrayToString(","));
            DB.ExecuteNonQuerySql(sql, null);
        }

        public void CleanupFile(int day)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@day", day);

            string sql = string.Format("delete from g_BusinessFile where Status in ({0},{1}) and datediff(day,CreateDateTime,getdate())>@day",
                (int)FileState.Deleted,
                (int)FileState.NoSaved);
            DB.ExecuteNonQuerySql(sql, spc);
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID)
        {
            return GetSimpleFileModel(systemID, moduleCode, categoryCode, masterID, new FileState[] { FileState.Saved, FileState.NoSaved });
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string masterID, int pageSize, int pageIndex, out int allRecordCount)
        {
            return GetSimpleFileModel(systemID, moduleCode, masterID, new FileState[] { FileState.Saved, FileState.NoSaved }, pageSize, pageIndex, out allRecordCount);
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, int pageSize, int pageIndex, out int allRecordCount)
        {
            return GetSimpleFileModel(systemID, moduleCode, categoryCode, masterID, new FileState[] { FileState.Saved, FileState.NoSaved }, pageSize, pageIndex, out allRecordCount);
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, FileState[] fileStates)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CategoryCode", categoryCode);
            spc.Add("@MasterID", masterID);

            string sql = "";
            if (fileStates.Length > 0)
            {
                for (int i = 0; i < fileStates.Length; i++)
                {
                    if (i == 0)
                        sql += (int)fileStates[i];
                    else
                        sql += "," + (int)fileStates[i];
                }

                sql = string.Format(" and Status in ({0})", sql);
            }

            sql = @"select FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FilePath,CreateDateTime,Status
from g_BusinessFile
where SystemID=@SystemID and ModuleCode=@ModuleCode and CategoryCode=@CategoryCode and MasterID=@MasterID"
                + sql;
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            return ToSimpleFileModel(dt);
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string masterID, FileState[] fileStates, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@MasterID", masterID);

            string sql = "";
            if (fileStates.Length > 0)
            {
                for (int i = 0; i < fileStates.Length; i++)
                {
                    if (i == 0)
                        sql += (int)fileStates[i];
                    else
                        sql += "," + (int)fileStates[i];
                }

                sql = string.Format(" and Status in ({0})", sql);
            }

            string orderby = "FileID desc";
            sql = @"select FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FilePath,CreateDateTime,Status
from g_BusinessFile
where SystemID=@SystemID and ModuleCode=@ModuleCode and MasterID=@MasterID"
                + sql;
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
            return ToSimpleFileModel(dt);
        }

        public List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, FileState[] fileStates, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CategoryCode", categoryCode);
            spc.Add("@MasterID", masterID);

            string sql = "";
            if (fileStates.Length > 0)
            {
                for (int i = 0; i < fileStates.Length; i++)
                {
                    if (i == 0)
                        sql += (int)fileStates[i];
                    else
                        sql += "," + (int)fileStates[i];
                }

                sql = string.Format(" and Status in ({0})", sql);
            }

            string orderby = "FileID desc";
            sql = @"select FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FilePath,CreateDateTime,Status
from g_BusinessFile
where SystemID=@SystemID and ModuleCode=@ModuleCode and CategoryCode=@CategoryCode and MasterID=@MasterID"
                + sql;
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
            return ToSimpleFileModel(dt);
        }

        public List<SimpleFileModel> GetSimpleFileModel(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = @"select FileID,SystemID,ModuleCode,CategoryCode,MasterID,
FileName,FileType,FileSize,FilePath,CreateDateTime,Status
from g_BusinessFile where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "FileID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);
            return ToSimpleFileModel(dt);
        }

        private List<SimpleFileModel> ToSimpleFileModel(DataTable datatable)
        {
            List<SimpleFileModel> files = new List<SimpleFileModel>();
            foreach (DataRow dr in datatable.Rows)
            {
                SimpleFileModel sfm = new SimpleFileModel();

                sfm.FileID = dr["FileID"].ToInt32();

                sfm.SystemID = dr["SystemID"].ToInt32();
                sfm.ModuleCode = dr["ModuleCode"].ToString();
                sfm.CategoryCode = dr["CategoryCode"].ToString();
                sfm.MasterID = dr["MasterID"].ToString();

                sfm.FileName = dr["FileName"].ToString();
                sfm.FileType = dr["FileType"].ToString();
                sfm.FileSize = dr["FileSize"].ToInt64();

                sfm.FilePath = dr["FilePath"].ToString();

                sfm.CreateDateTime = dr["CreateDateTime"].ToDateTime();
                sfm.FileStatus = (FileState)dr["Status"].ToInt32();

                files.Add(sfm);
            }
            return files;
        }
    }
}
