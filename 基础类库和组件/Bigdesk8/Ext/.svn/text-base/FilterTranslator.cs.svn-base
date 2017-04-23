using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bigdesk8.Ext
{
    /// <summary>
    /// 将检索规则 翻译成 where sql 语句,并生成相应的参数列表
    /// 如果遇到{CurrentUserID}这种，翻译成对应的参数
    /// </summary>
    /// 原始作者：ligerUI的作者
    public class FilterTranslator
    {
        //几个前缀/后缀
        protected char leftToken = '[';
        protected char paramPrefixToken = '@';
        protected char rightToken = ']';
        protected char groupLeftToken = '(';
        protected char groupRightToken = ')';
        protected char likeToken = '%';
        /// <summary>
        /// 参数计数器
        /// </summary>
        private int paramCounter = 0;

        //几个主要的属性
        public FilterGroup Group { get; set; }
        public string CommandText { get; private set; }
        public IList<FilterParam> Parms { get; private set; }

        public FilterTranslator()
            : this(null)
        {
        }

        public FilterTranslator(FilterGroup group)
        {
            this.Group = group;
            this.Parms = new List<FilterParam>();
        }
        public void Translate()
        {
            this.CommandText = TranslateGroup(this.Group);
        }

        public string TranslateGroup(FilterGroup group)
        {
            StringBuilder searchClauseBulider = new StringBuilder();    //查询子句构造器
            if (group == null) return string.Empty;
            if (group.rules != null)
            {
                foreach (var rule in group.rules)
                {
                    /* 去掉字符有空字符的情况，字符为空不查询，字符==“--所有--”时，说明为
                       下拉列表为选择的状态，此时亦不需要查询。
                     */
                    if (rule.value.ToString() != "--所有--" & rule.value.ToString().Replace(" ", "") != string.Empty)
                    {
                        if (searchClauseBulider.Length == 0)    //如果查询子句构造器中尚没有内容
                        {
                            searchClauseBulider.Append(groupLeftToken);
                            searchClauseBulider.Append(TranslateRule(rule));
                        }
                        else
                        {
                            searchClauseBulider.Append(GetOperatorQueryText(group.op));
                            searchClauseBulider.Append(TranslateRule(rule));
                        }
                    }
                }
            }

            if (group.groups != null)
            {
                foreach (var subgroup in group.groups)
                {
                    string subClause = TranslateGroup(subgroup);

                    if (!string.IsNullOrEmpty(subClause))
                    {
                        if (searchClauseBulider.Length == 0)    //如果查询子句构造器中尚没有内容
                        {
                            searchClauseBulider.Append(groupLeftToken);
                            searchClauseBulider.Append(subClause);
                        }
                        else
                        {
                            searchClauseBulider.Append(GetOperatorQueryText(group.op));
                            searchClauseBulider.Append(subClause);
                        }
                    }
                }
            }
            
            if (searchClauseBulider.Length > 0) 
                searchClauseBulider.Append(groupRightToken);

            return searchClauseBulider.ToString();
        }

        public string TranslateRule(FilterRule rule)
        {
            StringBuilder bulider = new StringBuilder();
            if (rule == null) return string.Empty;

            if (rule.field.Contains("[") || rule.field.Contains("]"))
                throw new Exception("非法的字段名:" + rule.field);

            bulider.Append(leftToken + rule.field + rightToken); //将字段名的前后用[ ]括起来，以将前台传入的字段名处理为一个整体，防止前台传入恶意字符串作为字段名进行sql注入攻击
            //操作符
            bulider.Append(GetOperatorQueryText(rule.op));

            var op = rule.op.ToLower();
            if (op == "like" || op == "endwith")
            {
                var value = rule.value.ToString();
                if (!value.StartsWith(this.likeToken.ToString()))
                {
                    rule.value = this.likeToken + value;
                }
            }
            if (op == "like" || op == "startwith")
            {
                var value = rule.value.ToString();
                if (!value.EndsWith(this.likeToken.ToString()))
                {
                    rule.value = value + this.likeToken;
                }
            }
            if (op == "in" || op == "notin")
            {
                var values = rule.value.ToString().Split(',');
                var appended = false;
                bulider.Append("(");
                foreach (var value in values)
                {
                    if (appended) bulider.Append(",");

                    bulider.Append(paramPrefixToken + CreateFilterParam(value, rule.type));

                    appended = true;
                }
                bulider.Append(")");
            }
            //is null 和 is not null 不需要值
            else if (op != "isnull" && op != "isnotnull")
            {
                bulider.Append(paramPrefixToken + CreateFilterParam(rule.value, rule.type));
            }
            return bulider.ToString();
        
        }

        private string CreateFilterParam(object value, string type)
        {
            string paramName = "p" + ++paramCounter;
            object val = value;
            if (type == "int" || type == "digits")
            {
                val = Convert.ToInt32(val);
            }
            else if (type == "float" || type == "number")
            {
                val = Convert.ToDecimal(val);
            }
            else if (type == "string")
            {
                val = Convert.ToString(val).Replace(" ","");//如果是字符型，需要去掉空格----2012-12-23，李海波
            }

            FilterParam param = new FilterParam(paramName, val);
            this.Parms.Add(param);
            
            return paramName;
        }

        public override string ToString()
        {
            StringBuilder bulider = new StringBuilder();
            bulider.Append("CommandText:");
            bulider.Append(this.CommandText);
            bulider.AppendLine();
            bulider.AppendLine("Parms:");
            foreach (var parm in this.Parms)
            {
                bulider.AppendLine(string.Format("{0}:{1}", parm.Name, parm.Value));
            }
            return bulider.ToString();
        }

        #region 公共工具方法
        /// <summary>
        /// 获取操作符的SQL Text
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns> 
        public static string GetOperatorQueryText(string op)
        {
            switch (op.ToLower())
            {
                case "add":
                    return " + ";
                case "bitwiseand":
                    return " & ";
                case "bitwisenot":
                    return " ~ ";
                case "bitwiseor":
                    return " | ";
                case "bitwisexor":
                    return " ^ ";
                case "divide":
                    return " / ";
                case "equal":
                    return " = ";
                case "greater":
                    return " > ";
                case "greaterorequal":
                    return " >= ";
                case "isnull":
                    return " is null ";
                case "isnotnull":
                    return " is not null ";
                case "less":
                    return " < ";
                case "lessorequal":
                    return " <= ";
                case "like":
                    return " like ";
                case "startwith":
                    return " like ";
                case "endwith":
                    return " like ";
                case "modulo":
                    return " % ";
                case "multiply":
                    return " * ";
                case "notequal":
                    return " <> ";
                case "subtract":
                    return " - ";
                case "and":
                    return " and ";
                case "or":
                    return " or ";
                case "in":
                    return " in ";
                case "notin":
                    return " not in ";
                default:
                    return " = ";
            }
        }
        #endregion

    }
}
