using System;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// 数据项与数据关系
    /// </summary>
    public enum DataRelation
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 字符串匹配
        /// </summary>
        Like,

        /// <summary>
        /// 左边字符串匹配
        /// </summary>
        LeftLike,

        /// <summary>
        /// 右边字符串匹配
        /// </summary>
        RightLike
    }
}
