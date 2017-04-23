using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wxjzgcjczy.Common
{
    public class FilterRule
    {
        public FilterRule()
        {
        }
        public FilterRule(string field, object value)
            : this(field, value, "equal")
        {
        }

        public FilterRule(string field, object value, string op)
        {
            this.field = field;
            this.value = value;
            this.op = op;
        }
        public FilterRule(string field, object value, string op, string type)
        {
            this.field = field;
            this.value = value;
            this.op = op;
            this.type = type;
        }


        public string field { get; set; }
        public object value { get; set; }
        public string op { get; set; }
        public string type { get; set; }
    }
}
