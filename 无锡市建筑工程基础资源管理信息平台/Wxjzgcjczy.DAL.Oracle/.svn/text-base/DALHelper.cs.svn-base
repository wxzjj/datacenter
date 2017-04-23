using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Wxjzgcjczy.Common;

namespace Wxjzgcjczy.DAL
{
    public class DALHelper
    {
        public static void GetSearchClause(ref SqlParameterCollection p, FilterTranslator ft)
        {
            string where = ft.CommandText;
            FilterParam[] parms = ft.Parms.ToArray();
            string spcname = string.Empty;

            if (parms != null)
            {
                foreach (var parm in parms)
                {
                    //spcname = @"@" + parm.Name;
                    spcname = @":" + parm.Name;//oracle查询的参数前缀是“:”
                    p.Add(spcname, parm.Value);
                }
            }
        }
    }
}
