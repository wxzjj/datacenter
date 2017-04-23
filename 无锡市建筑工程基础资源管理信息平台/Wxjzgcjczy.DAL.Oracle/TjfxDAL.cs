using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Bigdesk8;

namespace Wxjzgcjczy.DAL
{
    public class TjfxDAL
    {
        public DBOperator DB { get; set; }

        public DataTable RetrieveTjfx_Sgdwzz(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string querySQL = @"select '一级企业'as lx,count(Qyssdq) as cou,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='一级企业' and Qyssdq='本市企业') as bsqy,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='一级企业' and Qyssdq='本省外市') as bsws,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='一级企业' and Qyssdq='外省企业') as wsqy
 from Szgkjc_Jctz_Htjg where Qyzzdj='一级企业'
 
 union all 
 
 select '二级企业'as lx,count(Qyssdq) as cou,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='二级企业' and Qyssdq='本市企业') as bsqy,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='二级企业' and Qyssdq='本省外市') as bsws,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='二级企业' and Qyssdq='外省企业') as wsqy
 from Szgkjc_Jctz_Htjg where Qyzzdj='二级企业'
 
  union all 
 
 select '三级企业' as lx,count(Qyssdq) as cou,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='三级企业' and Qyssdq='本市企业') as bsqy,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='三级企业' and Qyssdq='本省外市') as bsws,
(select count(*) from  Szgkjc_Jctz_Htjg where Qyzzdj='三级企业' and Qyssdq='外省企业') as wsqy
 from Szgkjc_Jctz_Htjg where Qyzzdj='三级企业'
 ";

            return DB.ExeSqlForDataTable(querySQL, p, "t");
        }

        public DataTable RetrieveTjfx_SgdwzzTj(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string querySQL = @"select 'sgyth' as lx,count(0) as cou from uepp_qyjbxx a inner join UEPP_Qycsyw b on a.qyid=b.qyid where b.csywlxid=2
                 union all 
                 select 'jzsg' as lx,count(0) as cou from uepp_qyjbxx a inner join UEPP_Qycsyw b on a.qyid=b.qyid where b.csywlxid=1
                 union all 
                 select 'szqy' as lx,count(0) as cou from uepp_qyjbxx a inner join UEPP_Qycsyw b on a.qyid=b.qyid where b.csywlxid=''
                 union all
                 select 'yllh' as lx,count(0) as cou from uepp_qyjbxx a inner join UEPP_Qycsyw b on a.qyid=b.qyid where b.csywlxid=3
                 ";

            return DB.ExeSqlForDataTable(querySQL, p, "t");
        }

        public DataTable RetrieveTjfx_ZyrylxTj(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string querySQL = @"select 'jzs' as lx,count(0) as cou from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid where b.ryzyzglxid=1 
                 union all 
                select 'jls' as lx,count(0) as cou from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid where b.ryzyzglxid=21 
                 union all 
                select 'zjs' as lx,count(0) as cou from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid where b.ryzyzglxid=41 
                 union all
                select 'jzhus' as lx,count(0) as cou from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid where b.ryzyzglxid=51 
                union all
                select 'jgs' as lx,count(0) as cou from uepp_ryjbxx a inner join UEPP_Ryzyzg b on a.ryid=b.ryid where b.ryzyzglxid=61 
                 ";

            return DB.ExeSqlForDataTable(querySQL, p, "t");
        }

        public DataTable RetrieveTjfx_XmTj(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            string querySQL = @"select '2010' as nf,count(0) as cou from uepp_xmjbxx a inner join uepp_aqjdxx b
on a.sgxmtybh=b.sgxmtybh where to_char(b.kgrq,'yyyy')='2010'
 union all 
select '2011' as nf,count(0) as cou from uepp_xmjbxx a inner join uepp_aqjdxx b
on a.sgxmtybh=b.sgxmtybh where to_char(b.kgrq,'yyyy')='2011'
  union all 
select '2012' as nf,count(0) as cou from uepp_xmjbxx a inner join uepp_aqjdxx b
on a.sgxmtybh=b.sgxmtybh where to_char(b.kgrq,'yyyy')='2012' 
   union all 
select '2013' as nf,count(0) as cou from uepp_xmjbxx a inner join uepp_aqjdxx b
on a.sgxmtybh=b.sgxmtybh where to_char(b.kgrq,'yyyy')='2013' 
 union all 
 select '2014' as nf,count(0) as cou from uepp_xmjbxx a inner join uepp_aqjdxx b
on a.sgxmtybh=b.sgxmtybh where to_char(b.kgrq,'yyyy')='2014'";

            return DB.ExeSqlForDataTable(querySQL, p, "t");
        }

    }
}
