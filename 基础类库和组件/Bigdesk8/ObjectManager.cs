using System;
using System.Configuration;
using Bigdesk8.Web;

namespace Bigdesk8
{
    /// <summary>
    /// 对象管理器
    /// </summary>
    public static class ObjectManager
    {
        #region 创建对象

        /// <summary>
        /// 创建 <see cref="T"> 对象
        /// </summary>
        /// <typeparam name="T">object</typeparam>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="paramObjects"></param>
        /// <returns></returns>
        public static T CreateObject<T>(string assemblyName, string className, params object[] paramObjects) where T : class
        {
            T obj = null;
            Type type = Type.GetType(string.Format("{0}, {1}", className, assemblyName), false, true);
            if (type == null)
                throw new Exception(string.Format("找不到类型 {0}。", className));

            obj = (T)Activator.CreateInstance(type, paramObjects);// 创建对象

            return obj;

        }

        #endregion 创建对象
    }
}
