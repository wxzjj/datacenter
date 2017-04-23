using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Bigdesk8;
using Bigdesk8.Business.FileManager;
using Bigdesk8.Business.LogManager;
using Bigdesk8.Data;
using Bigdesk8.Security;
using Bigdesk8.Web.Controls;

namespace SparkServiceDesk
{
    /// <summary>
    /// 本项目的目标是实现各类文件的集中上传，入库（如果业务需要），查看，下载.
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UploadFileService : IUploadFileService
    {
        private string TempPathForUploadedFiles
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

            string tempUpFilePath = TempPathForUploadedFiles;

            if (!Directory.Exists(tempUpFilePath)) Directory.CreateDirectory(tempUpFilePath);

            if (isFirstChunk)
            {
                // 第一个文件块
                ServiceLog.Log("大文件上传", "UploadFileService", fileName, "开始上传啦");
                tempFileName = GetNewFileName(tempUpFilePath, extension) + "_temp";
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
                result.FileID = DealUploadedFile(fileName, tempFilePath, customParams);

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

            string tempUpFilePath = TempPathForUploadedFiles;

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
        private string GetNewFileName(string path, string extension)
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

        private string DealUploadedFile(string shortFileName, string fullFileName, Dictionary<string, string> customParams)
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

            ConnectionStringSettings connstr = ConfigurationManager.ConnectionStrings["SparkServiceDesk_FileManagerConnectionString"];
            DBOperator db = DBOperatorFactory.GetDBOperator(connstr);

            IFileManager fileManager = FileManagerFactory.CreateFileManager("DEFAULT");
            fileManager.DB = db;

            //add by qianbin
            bool overWrite = false;
            if (customParams.ContainsKey("OverWrite"))
                overWrite = customParams["OverWrite"].ToBoolean(false);
            if (overWrite)
            {
                List<SimpleFileModel> list = fileManager.GetSimpleFileModel(systemID, moduleCode, categoryCode, masterID);
                foreach (SimpleFileModel innerSfm in list)
                {
                    fileManager.DeleteFile(innerSfm.FileID);
                }
            }
            //end add

            SimpleFileModel sfm = fileManager.AddFile(systemID, moduleCode, categoryCode, masterID, fileName, fileType, fileContent, FileState.Saved);
            return sfm.FileID.ToString();
        }

    }
}
