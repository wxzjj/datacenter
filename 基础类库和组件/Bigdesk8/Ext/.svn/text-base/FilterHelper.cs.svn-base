using Bigdesk8.Data;

namespace Bigdesk8.Ext
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonSearchString"></param>
        /// <param name="sql"></param>
        /// <param name="spc"></param>
        /// <param name="isAndRelation">生成的子句和主sql之间是否用and连接，true: and, false: or</param>
        public static void GetSearchClause(string jsonSearchString, ref string sql, ref SqlParameterCollection spc, bool isAndRelation)
        {
            /*
             * where 为 json参数，格式如下： 
             * {
                  "rules":[
                     {"field":"ID","value":112,"op":"equal"},
                     {"field":"Time","value":"2011-3-4","op":"greaterorequal"}
                   ],
                  "op":"and",   //此处的and/or表示的查询规则各项之间的相互关系
                  "groups":null
             *  }
             *  FilterTranslator可以为以上格式的where表达式翻译为sql，并生成参数列表(FilterParam[])
             */
            var whereTranslator = new FilterTranslator();
            if (!jsonSearchString.IsEmpty())
                whereTranslator.Group = Newtonsoft.Json.JsonConvert.DeserializeObject<FilterGroup>(jsonSearchString);
            whereTranslator.Translate();

            if (!string.IsNullOrEmpty(whereTranslator.CommandText))
            {
                if (isAndRelation)
                    sql += " and " + whereTranslator.CommandText;
                else
                    sql += " or " + whereTranslator.CommandText;
            }

            foreach (var parm in whereTranslator.Parms)
            {
                string spcname = @"@" + parm.Name;
                spc.Add(spcname, parm.Value);
            }
        }
    }
}
