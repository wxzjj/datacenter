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
using Bigdesk8.Business;

namespace Wxjzgcjczy.DAL.Sqlserver
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

        public DataTable RetrieveTjfx_XmTj_Count(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(xmfl))
            {
                condition = condition + " and PrjTypeNum='" + xmfl + "'";
            }
            if (!string.IsNullOrEmpty(bdateStart.ToString()))
            {
                startYear = bdateStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(bdateEnd.ToString()))
            {
                endYear = bdateEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }


            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf,count(0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-4 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,count(0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-3 and UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,count(0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-2 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,count(0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-1 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,count(0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())   and UpdateFlag='U'  " + condition + @"
                            ";
            }
            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }

            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }

        public DataTable RetrieveTjfx_XmTj_Count_Jg(string xmfl, string edateStart, string edateEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(xmfl))
            {
                condition = condition + " and PrjTypeNum='" + xmfl + "'";
            }
            if (!string.IsNullOrEmpty(edateStart.ToString()))
            {
                startYear = edateStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(edateEnd.ToString()))
            {
                endYear = edateEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";

            }
            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf,count(0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-4 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,count(0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-3 and UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,count(0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-2 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,count(0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-1 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,count(0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())   and UpdateFlag='U'  " + condition + @"
";
            }

            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBProjectInfo where Year(EDate)=" + startYear + " and UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBProjectInfo where Year(EDate)=" + startYear + " and UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }


            return DB.ExeSqlForDataTable(querySQL, null, "t");

        }

        public DataTable RetrieveTjfx_XmTj_Zmj(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(xmfl))
            {
                condition = condition + " and PrjTypeNum='" + xmfl + "'";
            }
            if (!string.IsNullOrEmpty(bdateStart.ToString()))
            {
                startYear = bdateStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(bdateEnd.ToString()))
            {
                endYear = bdateEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }


            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-4 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-3 and UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-2 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-1 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE()) and UpdateFlag='U'  " + condition + @"
";
            }

            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }


            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }

        public DataTable RetrieveTjfx_XmTj_Zmj_Jg(string xmfl, string edateStart, string edateEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(xmfl))
            {
                condition = condition + " and PrjTypeNum='" + xmfl + "'";
            }
            if (!string.IsNullOrEmpty(edateStart.ToString()))
            {
                startYear = edateStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(edateEnd.ToString()))
            {
                endYear = edateEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";

            }
            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-4 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-3 and UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-2 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE())-1 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf, isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=Year(GETDATE()) and UpdateFlag='U'  " + condition + @"
";
            }
            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=" + startYear + " and UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,isnull( sum(isnull(AllArea,0)),0) as cou from TBProjectInfo where Year(EDate)=" + startYear + " and UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }
            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }

        public DataTable RetrieveTjfx_XmTj_Ztz(string xmfl, string bdateStart, string bdateEnd, string ssdq)
        {

            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(xmfl))
            {
                condition = condition + " and PrjTypeNum='" + xmfl + "'";
            }
            if (!string.IsNullOrEmpty(bdateStart.ToString()))
            {
                startYear = bdateStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(bdateEnd.ToString()))
            {
                endYear = bdateEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }


            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-4 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-3 and UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-2 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,sum(isnull(AllInvest,0)) as cou from TBProjectInfo where Year(BDate)=Year(GETDATE())-1 and UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=Year(GETDATE()) and UpdateFlag='U'  " + condition + @"
";
            }
            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,sum(isnull(AllInvest,0))  as cou from TBProjectInfo where Year(BDate)=" + startYear + " and UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }

            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }


        public DataTable RetrieveTjfx_SgxkTj_Count(string fzrqStart, string fzrqEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;

            if (!string.IsNullOrEmpty(fzrqStart.ToString()))
            {
                startYear = fzrqStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(fzrqEnd.ToString()))
            {
                endYear = fzrqEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }


            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-4 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-3 and a.UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-2 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-1 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())   and a.UpdateFlag='U'  " + condition + @"
                            ";
            }
            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }

            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }

        public DataTable RetrieveTjfx_SgxkTj_Zmj(string fzrqStart, string fzrqEnd, string ssdq)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;

            if (!string.IsNullOrEmpty(fzrqStart.ToString()))
            {
                startYear = fzrqStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(fzrqEnd.ToString()))
            {
                endYear = fzrqEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }



            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf, isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-4 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,  isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-3 and a.UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,  isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-2 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,  isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE())-1 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,  isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=Year(GETDATE()) and a.UpdateFlag='U'  " + condition + @"
";
            }

            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf, isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf, isnull( sum(isnull(Area,0)),0) as cou from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(IssueCertDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }


            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }



        public DataTable RetrieveTjfx_HtBaTj_Count(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;

            if (!string.IsNullOrEmpty(htqdRqStart.ToString()))
            {
                startYear = htqdRqStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(htqdRqEnd.ToString()))
            {
                endYear = htqdRqEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }

            if (!string.IsNullOrEmpty(htlb))
            {
                condition = condition + " AND ContractTypeNum in (" + htlb + ")";
            }
 

            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf,count(0) as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE())-4 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,count(0) as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum where Year(ContractDate)=Year(GETDATE())-3 and a.UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,count(0) as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum where Year(ContractDate)=Year(GETDATE())-2 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,count(0) as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum where Year(ContractDate)=Year(GETDATE())-1 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,count(0) as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum where Year(ContractDate)=Year(GETDATE())   and a.UpdateFlag='U'  " + condition + @"
                            ";
            }
            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf,count(0) as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }

            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }

        public DataTable RetrieveTjfx_HtBaTj_Htje(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            int startYear = 0;
            int endYear = 0;
            int seJg = 0;
            string querySQL = string.Empty;
            string condition = string.Empty;

            if (!string.IsNullOrEmpty(htqdRqStart.ToString()))
            {
                startYear = htqdRqStart.ToInt32();
            }
            if (!string.IsNullOrEmpty(htqdRqEnd.ToString()))
            {
                endYear = htqdRqEnd.ToInt32();
            }

            if (!string.IsNullOrEmpty(ssdq))
            {
                condition = condition + " and CountyNum in (" + ssdq + ")";
            }

            if (!string.IsNullOrEmpty(htlb))
            {
                condition = condition + " AND ContractTypeNum in (" + htlb + ")";
            }


            if (startYear == 0 && endYear == 0)
            {
                querySQL = @"
                                select Year(GETDATE())-4 as nf, isnull( sum(isnull(ContractMoney,0)),0) as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE())-4 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-3 as nf,  isnull( sum(isnull(ContractMoney,0)),0)  as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE())-3 and a.UpdateFlag='U' " + condition + @"
                                union all 
                                select Year(GETDATE())-2 as nf,  isnull( sum(isnull(ContractMoney,0)),0)  as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE())-2 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE())-1 as nf,  isnull( sum(isnull(ContractMoney,0)),0)  as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE())-1 and a.UpdateFlag='U'  " + condition + @"
                                union all 
                                select Year(GETDATE()) as nf,  isnull( sum(isnull(ContractMoney,0)),0)  as cou from  TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=Year(GETDATE()) and a.UpdateFlag='U'  " + condition + @"
";
            }

            else
            {
                if (startYear > 0)
                {
                    if (endYear == 0)
                    {
                        endYear = DateTime.Now.Year;
                    }
                }
                else
                {
                    if (endYear > 0)
                    {
                        startYear = 2010;
                    }
                }

                seJg = endYear - startYear + 1;
                for (int i = 0; i < seJg; i++)
                {
                    if (startYear != endYear)
                    {
                        querySQL = querySQL + " select " + startYear + " as nf, isnull( sum(isnull(ContractMoney,0)),0)  as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "  union all ";
                    }
                    else
                    {
                        querySQL = querySQL + " select " + startYear + " as nf, isnull( sum(isnull(ContractMoney,0)),0)  as cou from TBContractRecordManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum  where Year(ContractDate)=" + startYear + " and a.UpdateFlag='U'  " + condition + "   ";
                    }

                    startYear = startYear + 1;
                }
            }


            return DB.ExeSqlForDataTable(querySQL, null, "t");
        }






        public DataTable RetrieveTjfx_Htba_Report(string htqdRqStart, string htqdRqEnd, string ssdq, string htlb)
        {
            string sqlSearch = string.Empty;
            //合同签订日期开始
            if (!string.IsNullOrEmpty(htqdRqStart))
            {
                sqlSearch += " AND ContractDate >= '" + htqdRqStart + "'";
            }
            //合同签订日期结束
            if (!string.IsNullOrEmpty(htqdRqEnd))
            {
                sqlSearch += " AND ContractDate <= '" + htqdRqEnd + "'";
            }

            //项目属地
            if (!string.IsNullOrEmpty(ssdq))
            {
                sqlSearch += " AND CountyNum in (" + ssdq + ")";
            }

            //合同类别 
            if (!string.IsNullOrEmpty(htlb))
            {
                sqlSearch += " AND ContractTypeNum in (" + htlb + ")";
            }

            string sql = @"select   
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='01' then 1 else 0 end ) as SnqyJzgcXmCount,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='100'   then 1 else 0 end ) as SnqyKcXmCount,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='02' then 1 else 0 end ) as SnqySzXmCount,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then 1 else 0 end ) as SnqyHjXmCount,

	  sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='01' then isnull(ContractMoney,0) else 0 end ) as SnqyJzgcHtje,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='100'   then isnull(ContractMoney,0) else 0 end ) as SnqyKcHtje,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='02' then isnull(ContractMoney,0) else 0 end ) as SnqySzHtje,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(ContractMoney,0) else 0 end ) as SnqyHjHtje,

	  sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='01' then isnull(AllInvest,0) else 0 end ) as SnqyJzgcZtz,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='100'   then isnull(AllInvest,0) else 0 end ) as SnqyKcZtz,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ContractTypeNum='200' and PrjTypeNum='02' then isnull(AllInvest,0) else 0 end ) as SnqySzZtz,
      sum( case when (d.Province='江苏省' or d.Province is null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(AllInvest,0) else 0 end ) as SnqyHjZtz,

	  sum( case when d.Province='江苏省' and ContractTypeNum='200' and PrjTypeNum='01' then isnull(AllArea,0) else 0 end ) as SnqyJzgcZmj,
      sum( case when d.Province='江苏省' and ContractTypeNum='100'   then isnull(AllArea,0) else 0 end ) as SnqyKcZmj,
      sum( case when d.Province='江苏省' and ContractTypeNum='200' and PrjTypeNum='02' then isnull(AllArea,0) else 0 end ) as SnqySzZmj,
      sum( case when d.Province='江苏省' and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(AllArea,0) else 0 end ) as SnqyHjZmj,

	  sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='01' then 1 else 0 end ) as SwqyJzgcXmCount,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='100'   then 1 else 0 end ) as SwqyKcXmCount,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='02' then 1 else 0 end ) as SwqySzXmCount,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then 1 else 0 end ) as SwqyHjXmCount,

	  sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='01' then isnull(ContractMoney,0) else 0 end ) as SwqyJzgcHtje,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='100'   then isnull(ContractMoney,0) else 0 end ) as SwqyKcHtje,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='02' then isnull(ContractMoney,0) else 0 end ) as SwqySzHtje,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(ContractMoney,0) else 0 end ) as SwqyHjHtje,

	  sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='01' then isnull(AllInvest,0) else 0 end ) as SwqyJzgcZtz,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='100'   then isnull(AllInvest,0) else 0 end ) as SwqyKcZtz,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ContractTypeNum='200' and PrjTypeNum='02' then isnull(AllInvest,0) else 0 end ) as SwqySzZtz,
      sum( case when (d.Province!='江苏省' and d.Province is not null)  and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(AllInvest,0) else 0 end ) as SwqyHjZtz,

	  sum( case when (d.Province!='江苏省' and d.Province is not null) and ContractTypeNum='200' and PrjTypeNum='01' then isnull(AllArea,0) else 0 end ) as SwqyJzgcZmj,
      sum( case when (d.Province!='江苏省' and d.Province is not null) and ContractTypeNum='100'   then isnull(AllArea,0) else 0 end ) as SwqyKcZmj,
      sum( case when (d.Province!='江苏省' and d.Province is not null) and ContractTypeNum='200' and PrjTypeNum='02' then isnull(AllArea,0) else 0 end ) as SwqySzZmj,
      sum( case when (d.Province!='江苏省' and d.Province is not null) and ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' )then isnull(AllArea,0) else 0 end ) as SwqyHjZmj,
                
	  sum( case when ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' ) then 1 else 0 end ) as HjXmCount,
      sum( case when ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' ) then isnull(ContractMoney,0) else 0 end ) as HjHtje,
      sum( case when ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' ) then isnull(AllInvest,0) else 0 end ) as HjZtz,
      sum( case when ((ContractTypeNum='200' and (PrjTypeNum='02' or PrjTypeNum='01') ) or  ContractTypeNum='100' ) then isnull(AllArea,0) else 0 end ) as HjZmj

				    FROM TBContractRecordManage a 
                    left join TBProjectInfo b on a.PrjNum=b.PrjNum 
					left join UEPP_Qyjbxx d  on a.ContractorCorpCode=d.zzjgdm 
					where a.UpdateFlag='U'  ";
            sql = sql + sqlSearch;
            return DB.ExeSqlForDataTable(sql, null, "HtBa");
        }
    }
}
