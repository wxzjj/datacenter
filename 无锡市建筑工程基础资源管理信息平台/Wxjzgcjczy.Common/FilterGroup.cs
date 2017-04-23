using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wxjzgcjczy.Common
{
    /// <summary>
    /// 对应前台 ligerFilter 的检索规则数据
    /// </summary>
    public class FilterGroup
    {
        public IList<FilterRule> rules { get; set; }
        public string op { get; set; }
        public IList<FilterGroup> groups { get; set; }
    }
}
