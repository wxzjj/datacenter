using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.Common
{
    /// <summary>
    /// 将检索规则 翻译成 where sql 语句,并生成相应的参数列表
    /// 如果遇到{CurrentUserID}这种，翻译成对应的参数
    /// </summary>
    public class FilterTranslator
    {
        //几个前缀/后缀
        //protected char leftToken = '[';
        protected char leftToken = ' ';
        protected char paramPrefixToken = '@';
        // protected char paramPrefixToken = ':';  //oracle查询参数的前缀是“:”
        //protected char rightToken = ']';
        protected char rightToken = ' ';
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

        public void Remove(string name)
        {
            string sql = this.CommandText.Replace("(", "").Replace(")", "").ToLower();
            string sql1 = "(";
            string name1 = "";
            if (sql.Contains("and"))
            {
                foreach (string val in Regex.Split(sql, "and", RegexOptions.IgnoreCase))
                {
                    if (val.Contains(name.ToLower()))
                    {
                        name1 = val.Substring(val.LastIndexOf(paramPrefixToken) + 1).Trim();
                    }
                    else
                    {
                        sql1 += val + " and ";
                    }
                }
            }
            else
            {
                if (sql.Contains(name.ToLower()))
                {
                    name1 = sql.Substring(sql.LastIndexOf(paramPrefixToken) + 1).Trim();
                    sql1 = "( 1=1 and ";
                }
                else
                {
                    sql1 += sql + " and ";
                }

            }
            sql1 = sql1.Substring(0, sql1.LastIndexOf("and"));
            sql1 += ")";
            this.CommandText = sql1;
            foreach (FilterParam fp in this.Parms)
            {
                if (fp.Name == name1)
                {
                    this.Parms.Remove(fp);
                    break;
                }
            }
            if (this.Group != null)
            {
                foreach (FilterRule fr in this.Group.rules)
                {
                    if (fr.field.ToLower() == name.ToLower())
                    {
                        this.Group.rules.Remove(fr);
                        break;
                    }
                }
            }
        }

        public string GetValue(string name)
        {
            if (this.Group != null)
            {
                foreach (FilterRule fr in this.Group.rules)
                {
                    if (fr.field.ToLower() == name.ToLower())
                    {
                        return fr.value.ToString().Replace("%", "");
                    }
                }
            }
            return "";
        }

        public string TranslateGroup(FilterGroup group)
        {
            StringBuilder bulider = new StringBuilder();
            if (group == null) return " 1=1 ";
            var appended = false;
            bulider.Append(groupLeftToken);
            if (group.rules != null)
            {
                foreach (var rule in group.rules)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.op));
                    /* 去掉字符有空字符的情况，字符为空不查询，字符==“--所有--”时，说明为
                       下拉列表为选择的状态，此时亦不需要查询。
                     */

                    if (rule.value.ToString() != "--所有--" & rule.value.ToString().Replace(" ", "") != string.Empty)
                    {
                        bulider.Append(TranslateRule(rule));
                    }
                    else
                    {
                        bulider.Append(" 1= 1");
                    }
                    appended = true;
                }
            }
            if (group.groups != null)
            {
                foreach (var subgroup in group.groups)
                {
                    if (appended)
                        bulider.Append(GetOperatorQueryText(group.op));
                    bulider.Append(TranslateGroup(subgroup));
                    appended = true;
                }
            }
            bulider.Append(groupRightToken);
            if (appended == false) return " 1=1 ";

            if (bulider.ToString() == "()")
            {
                return " 1=1 ";
            }
            return bulider.ToString();
        }

        public string TranslateRule(FilterRule rule)
        {
            StringBuilder bulider = new StringBuilder();
            if (rule == null) return " 1=1 ";

            bulider.Append(leftToken + rule.field + rightToken);
            //操作符
            bulider.Append(GetOperatorQueryText(rule.op));

            var op = rule.op.ToLower();
            if (op == "like" || op == "endwith")
            {
                var value = rule.value.ToString();
                try
                {
                    object[] o1 = (object[])rule.value;
                    value = o1[0].ToString();
                }
                catch (Exception e)
                {
                }
                if (!value.StartsWith(this.likeToken.ToString()))
                {
                    rule.value = this.likeToken + value.Trim();
                }
            }
            if (op == "like" || op == "startwith")
            {
                var value = rule.value.ToString();
                try
                {
                    object[] o1 = (object[])rule.value;
                    value = o1[0].ToString();
                }
                catch (Exception e)
                {
                }
                if (!value.EndsWith(this.likeToken.ToString()))
                {
                    rule.value = value.Trim() + this.likeToken;
                }
            }
            if (op == "in")
            {
                if (rule.type == "string")
                {
                    bulider.Remove(0, bulider.Length);

                    bulider.Append(leftToken +"(");

                    var values = rule.value.ToString().Split(',');
                    var appended = false;
                    //bulider.Append("(");
                    foreach (var value in values)
                    {
                        if (appended)
                        {
                           // bulider.Append(",");
                            bulider.Append(" or " + rule.field + "=" + paramPrefixToken + CreateFilterParam(value, rule.type));
                        }
                        else
                        {
                            bulider.Append( rule.field +"="+ paramPrefixToken + CreateFilterParam(value, rule.type));

                            appended = true;
                        }
                    }
                    bulider.Append(")");
                }
                else
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
            }
            else
                if (op == "notin")
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
                    var value = rule.value.ToString();
                    try
                    {
                        object[] o1 = (object[])rule.value;
                        value = o1[0].ToString();
                    }
                    catch (Exception e)
                    {
                    }
                    bulider.Append(paramPrefixToken + CreateFilterParam(value, rule.type));
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
                // val = Convert.ToString(val).Replace(" ", "");//如果是字符型，需要去掉空格----2012-12-23，李海波
                val = Convert.ToString(val).Trim();//如果是字符型，需要去掉两端的空格----孙刚

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
