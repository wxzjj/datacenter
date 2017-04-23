using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Bigdesk8;
using Bigdesk8.Business.FileManager;
using Bigdesk8.Data;
using Bigdesk8.Security;
using Bigdesk8.Web.Controls;

namespace SparkServiceDesk.Xmwj
{
    /// <summary>
    /// 项目文件上传（起始于苏州的一个烂需求）
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UploadFileService : IUploadFileService
    {
        private string TempPathForUploadedFiles
        {
            get { return ConfigurationManager.AppSettings["SparkServiceDesk_XmwjPath"]; }
        }
        private string TempPathForUploadedFilesLxxm
        {
            get { return ConfigurationManager.AppSettings["SparkServiceDesk_LxXmwjPath"]; }
        }

        #region IUploadFileService 成员

        [OperationContract]
        public UploadFileResult UploadFile(string fileID, string fileName, byte[] data, bool isFirstChunk, bool isLastChunk, Dictionary<string, string> customParams)
        {
            UploadFileResult result = new UploadFileResult();
            string extension = Path.GetExtension(fileName);
            string tempFileName = "";
            string tempFilePath = "";
            string tempUpFilePath = "";
            string categoryCode = customParams["CategoryCode"];
            if (categoryCode == "10000020" || categoryCode == "10000030" || categoryCode == "10000040")
                tempUpFilePath = TempPathForUploadedFilesLxxm;
            else
                tempUpFilePath = TempPathForUploadedFiles;

            if (!Directory.Exists(tempUpFilePath)) Directory.CreateDirectory(tempUpFilePath);

            if (isFirstChunk)
            {
                // 第一个文件块
                tempFileName = GetNewXmwjFileName(tempUpFilePath, extension);
                tempFilePath = Path.Combine(tempUpFilePath, tempFileName);
            }
            else
            {
                // 非第一个文件块
                tempFileName = fileID;
                tempFilePath = Path.Combine(tempUpFilePath, tempFileName);
            }

            // 临时文件名作为文件唯一标识
            result.FileID = tempFileName;
            //result.FileID = "tempfile";

            // 生成 SHA1 散列值
            result.SHA1 = SecurityUtility.SHA1(data);

            // 写入数据
            using (FileStream fs = File.Open(tempFilePath, FileMode.Append))
            {
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
            }

            if (isLastChunk)
            {
                // 保存上传文件
                DealUploadedXmwj(fileName, tempFilePath, customParams);
                result.FileID = fileID;

                // 删除临时文件
                //if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
            }

            return result;
        }

        [OperationContract]
        public void CancelUpload(string fileID, Dictionary<string, string> customParams)
        {
            if (fileID.IsEmpty())
                throw new Exception("参数fileID不能为空！");

            string tempUpFilePath = "";
            string categoryCode = customParams["CategoryCode"];
            if (categoryCode == "10000020" || categoryCode == "10000030" || categoryCode == "10000040")
                tempUpFilePath = TempPathForUploadedFilesLxxm;
            else
                tempUpFilePath = TempPathForUploadedFiles;
            string tempFilePath = Path.Combine(tempUpFilePath, fileID);

            if (File.Exists(tempFilePath))
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch { }
            }
        }

        [OperationContract]
        public void DeleteFile(string fileID, Dictionary<string, string> customParams)
        {
            //
        }

        [OperationContract]
        public string GetViewFileUrl(string fileID, Dictionary<string, string> customParams)
        {
            return "../Majordomo/UploadHandler/FileViewer.aspx?FileID=" + fileID;
        }

        #endregion

        /// <summary>
        /// 生成一个新的文件名
        /// </summary>
        /// <param name="path">文件所在目录</param>
        /// <param name="extension">文件扩展名</param>
        /// <returns></returns>
        private string GetNewXmwjFileName(string path, string extension)
        {
            string fileName = "";
            Random r = new Random();
            do
            {
                int ri = r.Next(100, int.MaxValue);

                fileName = string.Format("{0}_{1}{2}", DateTime.Now.ToString("yyyy-MM-dd"), Guid.NewGuid().ToString(), extension);
            }
            while (File.Exists(Path.Combine(path, fileName)));

            return fileName;
        }

        private string DealUploadedXmwj(string shortFileName, string fullFileName, Dictionary<string, string> customParams)
        {
            int systemID = customParams["SystemID"].ToInt32();
            string moduleCode = customParams["ModuleCode"];
            string categoryCode = customParams["CategoryCode"];
            string masterID = customParams["MasterID"];

            string fileName = shortFileName;
            string fileType = Path.GetExtension(shortFileName);

            //byte[] fileContent = null;
            //using (FileStream fs = File.Open(fullFileName, FileMode.Open))
            //{
            //    using (BinaryReader reader = new BinaryReader(fs))
            //    {
            //        fs.Seek(0L, SeekOrigin.Begin);
            //        fileContent = reader.ReadBytes((int)fs.Length);
            //    }
            //}

            ConnectionStringSettings connstr = ConfigurationManager.ConnectionStrings["SparkServiceDesk_XmwjDBConnectionString"];
            DBOperator db = DBOperatorFactory.GetDBOperator(connstr);

            //如果是立项项目
            if (categoryCode == "10000020" || categoryCode == "10000030" || categoryCode == "10000040")
            {
                string sql = string.Empty;
                SqlParameterCollection spc = db.CreateSqlParameterCollection();
                spc.Add("@masterID", masterID);
                sql = @"select a.Active_ID from bid_lxdj as a
                        inner join bid_bagl as b on a.Project_ID=b.Project_ID 
                        inner join tyxmk as c on b.Active_ID=c.zbbaID 
                        where c.xmid=@masterID ";
                string xmID = db.ExeSqlForString(sql, spc);
                string sql1 = "select count(1) from bid_filesCatalog where xmid=@xmid and fjcode=@categoryCode and status=0";
                spc.Add("@xmid", xmID);
                spc.Add("@categoryCode", categoryCode);
                int CountByXmID = db.ExeSqlForObject(sql1, spc).ToInt32();
                fullFileName = fullFileName.Substring(2);


                spc.Add("@shortFileName", shortFileName);
                spc.Add("@fullFileName", fullFileName);
                if (CountByXmID == 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    spc.Add("@guid", guid);
                    spc.Add("@createTime", DateTime.Now.ToDate2());
                    sql = @"insert into bid_filesCatalog (xmid,xmtype,fjguid,fjcode,createTime,status) values (@xmID,'立项项目',@guid, @categoryCode, @createTime, 0)";
                    db.ExecuteNonQuerySql(sql, spc);

                    string sql2 = "insert into public_fjk (fjGuid,fjname,fjurl,fjl,fjbz) values (@guid, @shortFileName, @fullFileName,'','施工许可系统')";
                    return db.ExecuteNonQuerySql(sql2, spc).ToString2();
                }
                else
                {
                    string sqlForGuid = "select distinct fjguid from bid_filesCatalog where xmid=@xmID and @categoryCode and status=0";
                    string guid = db.ExeSqlForString(sqlForGuid, spc).ToString2();
                    spc.Add("@guid", guid);
                    string sql2 = "insert into public_fjk (fjGuid,fjname,fjurl,fjl,fjbz) values (@guid, @shortFileName, @fullFileName,'','施工许可系统')";
                    return db.ExecuteNonQuerySql(sql2, null).ToString2();
                }
            }
            //是标段项目
            else
            {
                string sql = string.Empty;
                SqlParameterCollection spc = db.CreateSqlParameterCollection();
                spc.Add("@masterID", masterID);
                sql = @"select (a.Project_ID+'_'+a.bdxh) as bdguid  from Bid_ItemAndBdRelation as a
                    inner join Bid_bagl as b on a.Active_ID=b.Active_ID and a.ModuleCode='ZBBA'
                    inner join tyxmk as c on b.Active_ID=c.ZbbaID 
                    where c.xmid=@masterID";
                string xmID = db.ExeSqlForString(sql, spc);
                string sql1 = "select count(1) from bid_filesCatalog where xmid=@xmid and fjcode=@categoryCode and status=0";
                spc.Add("@xmid", xmID);
                spc.Add("@categoryCode", categoryCode);
                int CountByXmID = db.ExeSqlForObject(sql1, spc).ToInt32();
                fullFileName = fullFileName.Substring(2);

                spc.Add("@shortFileName", shortFileName);
                spc.Add("@fullFileName", fullFileName);
                if (CountByXmID == 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    spc.Add("@guid", guid);
                    spc.Add("@createTime", DateTime.Now.ToDate2());
                    sql = @"insert into bid_filesCatalog (xmid,xmtype,fjguid,fjcode,createTime,status) values (@xmID,'标段项目',@guid,@categoryCode, @createTime,0)";
                    db.ExecuteNonQuerySql(sql, spc);

                    string sql2 = "insert into public_fjk (fjGuid,fjname,fjurl,fjl,fjbz) values (@guid, @shortFileName, @fullFileName,'','施工许可系统')";
                    return db.ExecuteNonQuerySql(sql2, spc).ToString2();
                }
                else
                {
                    string sqlForGuid = "select distinct fjguid from bid_filesCatalog where xmid=@xmID and fjcode=@categoryCode and status=0";
                    string guid = db.ExeSqlForString(sqlForGuid, null).ToString2();
                    spc.Add("@guid", guid);
                    string sql2 = "insert into public_fjk (fjGuid,fjname,fjurl,fjl,fjbz) values (@guid, @shortFileName,@fullFileName,'','施工许可系统')";
                    return db.ExecuteNonQuerySql(sql2, spc).ToString2();
                }
            }
        }
    }
}
