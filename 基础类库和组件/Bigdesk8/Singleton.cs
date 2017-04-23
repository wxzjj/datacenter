using System;

namespace Bigdesk8
{
    /// <summary>
    /// Singleton 泛型类
    /// 
    /// Threadsafe Singleton best practice design pattern template
    /// 
    /// <![CDATA[
    /// Sample:
    /// 
    /// public class Demo
    /// {
    ///		public static Form1 instance1
    ///		{
    ///			get
    ///			{
    ///				return Singleton<Form1>.Instance;
    ///			}
    ///		}
    ///	}
    ///  
    /// ]]>
    /// </summary>
    /// <typeparam name="T">Any class that implements default constructor</typeparam>
    public sealed class Singleton<T> where T : new()
    {
        private Singleton() { }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested() { }

            internal static readonly T instance = new T();
        }
    }
}