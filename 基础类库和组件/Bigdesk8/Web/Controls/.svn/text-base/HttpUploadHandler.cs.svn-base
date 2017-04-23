using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 文件上传服务抽象类 IHttpHandler
    /// </summary>
    public abstract class HttpUploadHandler : IHttpHandler, IUploadFileService
    {
        private const string ACTION_UploadFile = "UploadFile";
        private const string ACTION_CancelUpload = "CancelUpload";
        private const string ACTION_DeleteFile = "DeleteFile";
        private const string ACTION_GetFileViewUrl = "GetFileViewUrl";

        /// <summary>
        /// HTTP请求上下文
        /// </summary>
        public HttpContext HttpContext
        {
            get;
            private set;
        }

        private void ResponseWrite(string s)
        {
            HttpContext.Response.ClearContent();
            HttpContext.Response.ContentType = "text/plain";
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.Write(s);
        }

        #region IHttpHandler 成员

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                this.HttpContext = context;
                NameValueCollection query = HttpUtility.ParseQueryString(this.HttpContext.Request.Url.Query, Encoding.UTF8);
                string action = query["Action"];
                string fileID = query["FileID"];
                Dictionary<string, string> customParams = JsonConvert.DeserializeObject<Dictionary<string, string>>(query["CustomParams"]);

                switch (action)
                {
                    case ACTION_UploadFile:
                        {
                            string fileName = query["FileName"];
                            bool isFirstChunk = query["IsFirstChunk"].ToBoolean(false);
                            bool isLastChunk = query["IsLastChunk"].ToBoolean(false);

                            Stream inputStream = context.Request.InputStream;
                            if (inputStream.Length == 0)
                                throw new ArgumentException("没有上传文件。");

                            byte[] data = new byte[inputStream.Length];

                            inputStream.Seek(0L, SeekOrigin.Begin);
                            inputStream.Read(data, 0, data.Length);

                            UploadFileResult r = this.UploadFile(fileID, fileName, data, isFirstChunk, isLastChunk, customParams);

                            this.ResponseWrite(JsonConvert.SerializeObject(r));
                        }
                        break;
                    case ACTION_CancelUpload:
                        {
                            this.CancelUpload(fileID, customParams);
                            this.ResponseWrite("");
                        }
                        break;
                    case ACTION_DeleteFile:
                        {
                            this.DeleteFile(fileID, customParams);
                            this.ResponseWrite("");
                        }
                        break;
                    case ACTION_GetFileViewUrl:
                        {
                            string viewFileUrl = this.GetViewFileUrl(fileID, customParams);
                            this.ResponseWrite(viewFileUrl);
                        }
                        break;
                    default:
                        throw new ArgumentException("参数 Action 不存在！Action=" + action);
                }
            }
            catch (Exception ex)
            {
                this.ResponseWrite("ERROR;;;" + ex.Message);
            }
        }

        #endregion

        #region IUploadFileService 成员

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="data">数据</param>
        /// <param name="isFirstChunk">是否是第一个文件块</param>
        /// <param name="isLastChunk">是否是最后一个文件块</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>返回文件标识</returns>
        /// <returns>返回SHA1散列值</returns>
        public abstract UploadFileResult UploadFile(string fileID, string fileName, byte[] data, bool isFirstChunk, bool isLastChunk, Dictionary<string, string> customParams);

        /// <summary>
        /// 取消上传
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>取消是否成功</returns>
        public abstract void CancelUpload(string fileID, Dictionary<string, string> customParams);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>删除是否成功</returns>
        public abstract void DeleteFile(string fileID, Dictionary<string, string> customParams);

        /// <summary>
        /// 获得查看文件地址
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns></returns>
        public abstract string GetViewFileUrl(string fileID, Dictionary<string, string> customParams);

        #endregion
    }
}
