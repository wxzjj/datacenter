using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;
using System.Collections;

namespace Bigdesk2010.Web
{
    /// <summary>
    /// 默认缓存管理类
    /// </summary>
    public class DefaultCacheStrategy : ICacheStrategy
    {
        private static volatile Cache webCache = HttpRuntime.Cache;

        /// <summary>
        /// 默认缓存存活期为3600秒(1小时)
        /// </summary>
        private int _timeOut = 3600;

        /// <summary>
        /// 设置到期相对时间[单位: 秒]，当为0时，表示无限期
        /// </summary>
        public virtual int TimeOut
        {
            get { return _timeOut >= 0 ? _timeOut : 3600; }
            set { _timeOut = value; }
        }

        /// <summary>
        /// 获取 Web 应用程序的缓存对象
        /// </summary>
        public static Cache GetWebCacheObj
        {
            get { return webCache; }
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        public virtual void AddObject(string objId, object o)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            if (TimeOut == 0)
            {
                webCache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(objId, o, null, DateTime.Now.AddSeconds(TimeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="files">监视的路径文件</param>
        public virtual void AddObjectWithFileChange(string objId, object o, string[] files)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            CacheDependency dep = new CacheDependency(files, DateTime.Now);

            webCache.Insert(objId, o, dep, DateTime.Now.AddSeconds(TimeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public virtual void AddObjectWithDepend(string objId, object o, string[] dependKey)
        {
            if (objId == null || objId.Length == 0 || o == null)
            {
                return;
            }

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);

            webCache.Insert(objId, o, dep, DateTime.Now.AddSeconds(TimeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 建立回调委托的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="reason"></param>
        public virtual void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
            //switch (reason)
            //{
            //    case CacheItemRemovedReason.DependencyChanged:
            //        break;
            //    case CacheItemRemovedReason.Expired:
            //        {
            //            break;
            //        }
            //    case CacheItemRemovedReason.Removed:
            //        {
            //            break;
            //        }
            //    case CacheItemRemovedReason.Underused:
            //        {
            //            break;
            //        }
            //    default: break;
            //}		
        }

        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        public virtual void RemoveObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return;
            }
            webCache.Remove(objId);
        }

        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <returns>对象</returns>
        public virtual object RetrieveObject(string objId)
        {
            if (objId == null || objId.Length == 0)
            {
                return null;
            }
            return webCache.Get(objId);
        }

        /// <summary>
        /// 获取ID和对象的数目
        /// </summary>
        public int Count
        {
            get { return webCache.Count; }
        }

        /// <summary>
        /// 移除所有的ID和对象
        /// </summary>
        public void Clear()
        {
            IDictionaryEnumerator de = webCache.GetEnumerator();
            while (de.MoveNext())
            {
                webCache.Remove(de.Key.ToString2());
            }

            //IList<string> objIds = GetObjIDs();
            //foreach (string objId in objIds)
            //{
            //    RemoveObject(objId);
            //}
        }

        /// <summary>
        /// 获取所有的ID
        /// </summary>
        /// <returns></returns>
        public IList<string> GetObjIDs()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator de = webCache.GetEnumerator();
            while (de.MoveNext())
            {
                keys.Add(de.Key.ToString2());
            }
            return keys;
        }

        /// <summary>
        /// 获取所有ID和对象
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            IDictionaryEnumerator de = webCache.GetEnumerator();
            while (de.MoveNext())
            {
                data.Add(de.Key.ToString2(), de.Value);
            }
            return data;
        }
    }
}
