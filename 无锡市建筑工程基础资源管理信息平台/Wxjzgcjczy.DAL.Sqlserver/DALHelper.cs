using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.Common;
using Bigdesk8.Data;

namespace Wxjzgcjczy.DAL.Sqlserver
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
                    spcname = @"@" + parm.Name;
                    p.Add(spcname, parm.Value);
                }
            }
        }
    }

}
