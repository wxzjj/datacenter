using System;
using System.IO;
using System.Collections.Generic;
using Bigdesk8.Data;

namespace Bigdesk8.Business.FileManager
{
    /// <summary>
    /// 文件管理接口
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 是否使用内部事务
        /// </summary>
        bool IsUseInnerTransaction { get; set; }

        /// <summary>
        /// 添加文件，返回 FileModel
        /// </summary>
        /// <param name="systemID">系统编号，此参数不能为空</param>
        /// <param name="moduleCode">模块代码，此参数不能为空</param>
        /// <param name="categoryCode">文件种类代码，此参数不能为空</param>
        /// <param name="masterID">业务关键字字串，此参数不能为空</param>
        /// <param name="fileName">文件名称，不包含目录路径，包含扩展名，此参数不能为空</param>
        /// <param name="fileType">文件扩展名，如.doc，此参数不能为空</param>
        /// <param name="fileContent">文件内容，此参数不能为空</param>
        /// <param name="fileState">文件状态，此参数不能为空</param>
        /// <returns>返回 FileModel</returns>
        SimpleFileModel AddFile(int systemID, string moduleCode, string categoryCode, string masterID, string fileName, string fileType, byte[] fileContent, FileState fileState);

        /// <summary>
        /// 获得指定的文件
        /// </summary>
        /// <param name="fileID">文件唯一编号</param>
        /// <returns>返回指定文件</returns>
        SimpleFileModel GetSimpleFileModel(int fileID);

        /// <summary>
        /// 获得指定的文件
        /// </summary>
        /// <param name="fileID">文件唯一编号</param>
        /// <param name="fileStates">文件状态</param>
        /// <returns>返回指定文件</returns>
        SimpleFileModel GetSimpleFileModel(int fileID, FileState[] fileStates);

        /// <summary>
        /// 获得指定的多个文件
        /// </summary>
        /// <param name="fileIDs">文件唯一编号</param>
        /// <returns>返回多个文件</returns>
        SimpleFileModel[] GetSimpleFileModel(int[] fileIDs);

        /// <summary>
        /// 获得指定的多个文件
        /// </summary>
        /// <param name="fileIDs">文件唯一编号</param>
        /// <param name="fileStates">文件状态</param>
        /// <returns>返回多个文件</returns>
        SimpleFileModel[] GetSimpleFileModel(int[] fileIDs, FileState[] fileStates);

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="fileID">文件编号</param>
        /// <returns></returns>
        byte[] GetFileContent(int fileID);

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="fileID">文件唯一编号</param>
        void DeleteFile(int fileID);

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="fileIDs">文件唯一编号</param>
        void DeleteFile(int[] fileIDs);

        /// <summary>
        /// 保存指定的文件
        /// </summary>
        /// <param name="fileID">文件唯一编号</param>
        void SaveFile(int fileID);

        /// <summary>
        /// 保存指定的文件
        /// </summary>
        /// <param name="fileIDs">文件唯一编号</param>
        void SaveFile(int[] fileIDs);

        /// <summary>
        /// 清理垃圾文件，未与业务关联的文件并且上传时间超过 n 天
        /// </summary>
        /// <param name="day">超过多少天</param>
        void CleanupFile(int day);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="categoryCode">种类代码</param>
        /// <param name="masterID">业务标识</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="masterID">业务标识</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string masterID, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="categoryCode">种类代码</param>
        /// <param name="masterID">业务标识</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="categoryCode">种类代码</param>
        /// <param name="masterID">业务标识</param>
        /// <param name="fileStates">文件状态</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, FileState[] fileStates);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="masterID">业务标识</param>
        /// <param name="fileStates">文件状态</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string masterID, FileState[] fileStates, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="systemID">系统编号</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="categoryCode">种类代码</param>
        /// <param name="masterID">业务标识</param>
        /// <param name="fileStates">文件状态</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(int systemID, string moduleCode, string categoryCode, string masterID, FileState[] fileStates, int pageSize, int pageIndex, out int allRecordCount);

        /// <summary>
        /// 获得文件列表
        /// </summary>
        /// <param name="condition">搜索条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<SimpleFileModel> GetSimpleFileModel(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);
    }

    /// <summary>
    /// 简单的文件模型，不包含文件内容等
    /// </summary>
    public class SimpleFileModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SimpleFileModel() { }

        /// <summary>
        /// 文件的唯一编号
        /// </summary>
        public int FileID { get; set; }

        /// <summary>
        /// 文件所在的系统编号
        /// </summary>
        public int SystemID { get; set; }

        /// <summary>
        /// 文件所在的模块代码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 文件的种类代码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 业务关键字字串
        /// </summary>
        public string MasterID { get; set; }

        /// <summary>
        /// 文件的原始名称(包含扩展名)，不包含目录路径
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件的后缀名称
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件的大小（以字节为单位）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 相对于上传文件统一管理根的完整相对路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件的创建日期时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 文件状态
        /// </summary>
        public FileState FileStatus { get; set; }
    }

    /// <summary>
    /// 文件状态
    /// </summary>
    public enum FileState
    {
        /// <summary>
        /// 已删除的
        /// </summary>
        Deleted = -1,

        /// <summary>
        /// 未保存的
        /// </summary>
        NoSaved = 0,

        /// <summary>
        /// 已保存的
        /// </summary>
        Saved = 1,
    }
}
