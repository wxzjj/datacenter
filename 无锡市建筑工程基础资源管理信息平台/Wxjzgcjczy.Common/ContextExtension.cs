using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using Bigdesk8;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.Common
{
    public static class ContextExtension
    {
        public static FilterTranslator GetGridData(HttpContext context)
        {
            /*
             * where 为 json参数，格式如下： 
             * {
                  "roles":[
                     {"field":"ID","value":112,"op":"equal","type":"int"},
                      {"field":"Time","value":"2011-3-4","op":"greaterorequal","type":"date"}
                   ],
                  "op":"and","groups":null
             *  }
             *  FilterTranslator可以为以上格式的where表达式翻译为sql，并生成参数列表(FilterParam[])
             */
            var whereTranslator = new FilterTranslator();

            string where = context.Request.Params["where"];

            if (!where.IsEmpty())
            {
                //反序列化Filter Group JSON
                whereTranslator.Group = JSONHelper.FromJson<FilterGroup>(where);
            }
            whereTranslator.Translate();


            return whereTranslator;
        }

       
    }
}
