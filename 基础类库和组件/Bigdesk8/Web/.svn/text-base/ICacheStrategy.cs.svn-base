using System;
using System.Collections.Generic;

namespace Bigdesk8.Web
{
    /// <summary>
    /// 公共缓存策略接口
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        void AddObject(string objId, object o);

        /// <summary>
        /// 添加指定ID的对象(关联指定文件组)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="files"></param>
        void AddObjectWithFileChange(string objId, object o, string[] files);

        /// <summary>
        /// 添加指定ID的对象(关联指定键值组)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dependKey"></param>
        void AddObjectWithDepend(string objId, object o, string[] dependKey);

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        void RemoveObject(string objId);

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        object RetrieveObject(string objId);

        /// <summary>
        /// 到期时间,单位：秒，当为0时，表示无限期
        /// </summary>
        int TimeOut { set; get; }

        /// <summary>
        /// 获取ID和对象的数目
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 移除所有的ID和对象
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取所有的ID
        /// </summary>
        /// <returns></returns>
        IList<string> GetObjIDs();

        /// <summary>
        /// 获取所有ID和对象
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object> GetData();
    }
}
