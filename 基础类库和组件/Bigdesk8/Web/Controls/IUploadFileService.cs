using System;
using System.Collections.Generic;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 上传文件服务
    /// </summary>
    public interface IUploadFileService
    {
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
        UploadFileResult UploadFile(string fileID, string fileName, byte[] data, bool isFirstChunk, bool isLastChunk, Dictionary<string, string> customParams);

        /// <summary>
        /// 取消上传
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>取消是否成功</returns>
        void CancelUpload(string fileID, Dictionary<string, string> customParams);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>删除是否成功</returns>
        void DeleteFile(string fileID, Dictionary<string, string> customParams);

        /// <summary>
        /// 获得查看文件地址
        /// </summary>
        /// <param name="fileID">文件标识</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns></returns>
        string GetViewFileUrl(string fileID, Dictionary<string, string> customParams);
    }

    /// <summary>
    /// 上传文件时的返回结果
    /// </summary>
    public class UploadFileResult
    {
        /// <summary>
        /// 文件标识
        /// </summary>
        public string FileID { get; set; }

        /// <summary>
        /// SHA1散列值
        /// </summary>
        public string SHA1 { get; set; }

        /// <summary>
        /// 自定义参数
        /// </summary>
        public Dictionary<string, string> CustomParams { get; set; }
    }
}
