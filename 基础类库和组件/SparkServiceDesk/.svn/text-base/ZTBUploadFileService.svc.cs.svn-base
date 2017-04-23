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

namespace SparkServiceDesk.ZTB
{
    /// <summary>
    /// 本项目的目标是实现各类文件的集中上传，入库（如果业务需要），查看，下载.
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UploadFileService : IUploadFileService
    {
        private string ZTBTempPathForUploadedFiles
        {
            get { return ConfigurationManager.AppSettings["SparkServiceDesk_TempPathForUploadedFiles"]; }
        }

        #region IUploadFileService 成员

        [OperationContract]
        public UploadFileResult UploadFile(string fileID, string fileName, byte[] data, bool isFirstChunk, bool isLastChunk, Dictionary<string, string> customParams)
        {
            UploadFileResult result = new UploadFileResult();
            string extension = Path.GetExtension(fileName);
            string tempFileName = "";
            string tempFilePath = "";

            string tempUpFilePath = ZTBTempPathForUploadedFiles;

            if (!Directory.Exists(tempUpFilePath)) Directory.CreateDirectory(tempUpFilePath);

            if (isFirstChunk)
            {
                // 第一个文件块
                tempFileName = GetNewZTBFileName(tempUpFilePath, extension) + "_temp";
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
                result.FileID = DealUploadedZTBFile(fileName, tempFilePath, customParams);

                // 删除临时文件
                if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
            }

            return result;
        }

        [OperationContract]
        public void CancelUpload(string fileID, Dictionary<string, string> customParams)
        {
            if (fileID.IsEmpty())
                throw new Exception("参数fileID不能为空！");

            string tempUpFilePath = ZTBTempPathForUploadedFiles;

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
        private string GetNewZTBFileName(string path, string extension)
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

        private string DealUploadedZTBFile(string shortFileName, string fullFileName, Dictionary<string, string> customParams)
        {
            int systemID = customParams["SystemID"].ToInt32();
            string moduleCode = customParams["ModuleCode"];
            string categoryCode = customParams["CategoryCode"];
            string masterID = customParams["MasterID"];

            string fileName = shortFileName;
            string fileType = Path.GetExtension(shortFileName);

            byte[] fileContent = null;
            using (FileStream fs = File.Open(fullFileName, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    fs.Seek(0L, SeekOrigin.Begin);
                    fileContent = reader.ReadBytes((int)fs.Length);
                }
            }

            ConnectionStringSettings connstr = ConfigurationManager.ConnectionStrings["SparkServiceDesk_ZTBFileManagerConnectionString"];
            DBOperator db = DBOperatorFactory.GetDBOperator(connstr);
            

            //IFileManager fileManager = FileManagerFactory.CreateFileManager("DEFAULT");
            //fileManager.DB = db;
            //SimpleFileModel sfm = fileManager.AddFile(systemID, moduleCode, categoryCode, masterID, fileName, fileType, fileContent, FileState.Saved);
            int sccs = 0;
            string sql = "";
            SqlParameterCollection spc = db.CreateSqlParameterCollection();
            spc.Add("@fileContent", fileContent);
            spc.Add("@masterID", masterID);
            spc.Add("@fileName", fileName);
            spc.Add("@scsj", DateTime.Now.ToString());
            if (categoryCode == "招标文件")
            {
                //如果已上传，Update
                string guid = Guid.NewGuid().ToString();
                string querrysql = " select COUNT(*) from bid_Zbwj where sccs=0 and xmbdID=@masterID";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update Bid_Zbwj set fjName =@fileName, scsj=@scsj ,fjl=@fileContent where sccs=0 and xmbdID=@masterID";
                }
                else
                {
                    spc.Add("@guid", guid);
                    spc.Add("@sccs", sccs);
                    sql = "insert into Bid_Zbwj(fjGuid, xmbdID, sccs, fjName, scsj, fjl) values(@guid, @masterID, @sccs, @fileName ,@scsj,@fileContent)";
                }

            }
            else if (categoryCode == "资格审查文件")
            {
                //如果已上传，Update
                string guid = Guid.NewGuid().ToString();
                string querrysql = " select COUNT(*) from bid_Zswj where sccs=0 and xmbdID=@masterID";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update Bid_Zswj set fjName =@fileName, scsj=@scsj, fjl=@fileContent where sccs=0 and xmbdID=@masterID";
                }
                else
                {
                    spc.Add("@guid", guid);
                    spc.Add("@sccs", sccs);
                    sql = "insert into Bid_Zswj(fjGuid, xmbdID, sccs, fjName, scsj, fjl) values(@guid, @masterID, @sccs, @fileName, @scsj, @fileContent)";
                }

            }
            else if (categoryCode == "预审答疑文件")
            {
                //如果已上传，Update
                string guid = Guid.NewGuid().ToString();
                string dycs = customParams["Dycs"].ToString();
                spc.Add("@dycs", dycs);
                string querrysql = " select COUNT(*) from bid_Zswj where sccs<>0 and xmbdID=@masterID and sccs=@dycs";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update Bid_Zswj set fjName =@fileName, scsj=@scsj, fjl=@fileContent where xmbdID=@masterID and sccs=@dycs ";
                }
                else
                {
                    spc.Add("@guid", guid);
                    sql = "insert into Bid_Zswj(fjGuid, xmbdID, sccs, fjName, scsj, fjl) values(@guid, @masterID, @dycs, @fileName, @scsj, @fileContent)";
                }

            }
            else if (categoryCode == "招标附属文件")
            {
                string dycs = customParams["Dycs"].ToString();
                string guid = Guid.NewGuid().ToString();
                spc.Add("@dycs", dycs);
                string querrysql = " select COUNT(*) from bid_Zbwj where xmbdID=@masterID and sccs=@dycs ";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update Bid_Zbwj set fjName_fs = @fileName , scsj_fs=@scsj, fjl_fs=@fileContent where  xmbdID=@masterID and sccs=@dycs";
                }
                else
                {
                    spc.Add("@guid", guid);
                    sql = "insert into Bid_Zbwj(fjGuid, xmbdID, sccs,fjName_fs, scsj_fs, fjl_fs) values(@guid, @masterID, @dycs, @fileName, @scsj, @fileContent)";
                }
            }
            else if (categoryCode == "答疑文件")
            {
                string dycs = customParams["Dycs"].ToString();
                string guid = Guid.NewGuid().ToString();
                spc.Add("@dycs", dycs);
                string querrysql = " select COUNT(*) from bid_Zbwj where sccs<>0 and xmbdID=@masterID and sccs=@dycs";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update Bid_Zbwj set fjName =@fileName, scsj=@scsj, fjl=@fileContent where  xmbdID=@masterID and sccs=@dycs";
                }
                else
                {
                    spc.Add("@guid", guid);
                    sql = "insert into Bid_Zbwj(fjGuid, xmbdID, sccs, fjName, scsj, fjl) values(@guid, @masterID, @dycs, @fileName, @scsj, @fileContent)";
                }
            }
            else if (categoryCode == "标底计价文件")
            {
                //如果已上传，Update
                string guid = Guid.NewGuid().ToString();
                string querrysql = " select COUNT(*) from bid_bdjjwj where  xmbdID=@masterID";
                sccs = db.ExeSqlForObject(querrysql, spc).ToInt32();
                if (sccs > 0)
                {
                    sql = " update bid_bdjjwj set fjName =@fileName, scsj=@scsj ,fjl=@fileContent where  xmbdID=@masterID";
                }
                else
                {
                    spc.Add("@guid", guid);
                    sql = "insert into bid_bdjjwj(fjGuid, xmbdID,  fjName, scsj, fjl) values(@guid, @masterID, @fileName, @scsj, @fileContent)";
                }
            }


            db.ExecuteNonQuerySql(sql, spc);

            sql = "  select max(ID) from bid_Zbwj ";

            return db.ExeSqlForObject(sql, null).ToString();

        }
    }
}
