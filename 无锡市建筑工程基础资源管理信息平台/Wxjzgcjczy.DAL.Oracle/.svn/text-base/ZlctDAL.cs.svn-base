using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8;


namespace Wxjzgcjczy.DAL
{
    public class ZlctDAL
    {
        public DBOperator DB { get; set; }

        #region 获取表结构
        /// <summary>
        /// 获取工作指示表结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaSzgkjc_Gzzs()
        {
            string sql = "select * from Szgkjc_Gzzs where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        /// <summary>
        /// 获取工作指示表结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaSzgkjc_Gzhf()
        {
            string sql = "select * from Szgkjc_Gzhf where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        /// <summary>
        /// 获取短信简报表结构
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaSzgkjc_Dxjb()
        {
            string sql = "select * from Szgkjc_Dxjb where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        /// <summary>
        /// 获取短信简报发送人员信息表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemaSzgkjc_Dxjb_Sjml()
        {
            string sql = "select * from Szgkjc_Dxjb_Sjml where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchemaSzgkjc_Dxjb_Records()
        {
            string sql = "select * from Szgkjc_Dxjb_Records where 1=2";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        #endregion

        #region 读取

        public int ReadSzgkjc_Gzzs_NewId()
        {
            string sql = "select max(GzzsId) from Szgkjc_Gzzs ";
            return DB.ExeSqlForString(sql, null).ToInt32(0) + 1;
        }

        public int ReadSzgkjc_Gzhf_NewId()
        {

            string sql = "select max(ZshfId) from Szgkjc_Gzhf ";
            return DB.ExeSqlForString(sql, null).ToInt32(0) + 1;
        }
        public int ReadSzgkjc_Dxjb_NewId()
        {

            string sql = "select max(DxjbId) from Szgkjc_Dxjb ";
            return DB.ExeSqlForString(sql, null).ToInt32(0) + 1;
        }
        public int ReadSzgkjc_Dxjb_Records_NewId()
        {
            string sql = "select max(Id) from Szgkjc_Dxjb_Records ";
            return DB.ExeSqlForString(sql, null).ToInt32(0) + 1;
        }
        public DataTable ReadSimlInfo(string userId)
        {
            string sql = @" select a.*,c.UserName from Gwtz_Sjml a
                             inner join dbo.Gwtz_User_Relation b on a.SjmlID=b.SjmlID
                             inner join g_user c on b.UserID=c.UserID
                             where b.UserID=@userId";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@userId", userId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadSzgkjc_Gzzs(string gzzsId)
        {
            string sql = "select * from Szgkjc_Gzzs where GzzsId=@GzzsId";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@GzzsId", gzzsId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable ReadSzgkjc_Gzhf(string zshfId)
        {
            string sql = "select * from Szgkjc_Gzhf where ZshfId=@ZshfId";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@ZshfId", zshfId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        /// <summary>
        /// 获取一条短信简报发送记录信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable ReadSzgkjc_Dxjb_Records(string Id)
        {
            string sql = "select * from Szgkjc_Dxjb_Records where Id in (" + @Id + ")";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        /// <summary>
        /// 获取收信人详细信息,输入参数是通讯录唯一编号，若有多个收信人，则将编号用逗号分隔
        /// </summary>
        /// <param name="sjmlID"></param>
        /// <returns></returns>
        public DataTable ReadZJG_Gwtz_Sjml(string sjmlIDs)
        {
            string sql = @"select a.*,b.UserID,b.SystemID,b.TableName from Gwtz_Sjml a 
                            inner join  dbo.Gwtz_User_Relation b on a.SjmlID=b.SjmlID 
                            where b.SjmlID in (" + sjmlIDs.Trim(new char[] { ',' }) + ")";

            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        /// <summary>
        /// 获取一个短信简报信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public DataTable ReadSzgkjc_Dxjb(string dxjbId)
        {
            string sql = "select * from Szgkjc_Dxjb where DxjbId=@DxjbId ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@DxjbId", dxjbId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 获取一个短信简报的发送对象信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public DataTable ReadSzgkjc_DxjbAndSjml(AppUser appWorker, string dxjbId)
        {
            string sql = @"select a.Dxjb_Sjml_Id,a.DxjbId,a.UserID,b.* from Szgkjc_Dxjb_Sjml a 
                            inner join Gwtz_Sjml b on a.SjmlID=b.SjmlID 
                            where a.DxjbId=@DxjbId";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@DxjbId", dxjbId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        /// <summary>
        /// 获取一个短信简报完整信息，包括收信人信息
        /// </summary>
        /// <param name="dxjbId"></param>
        /// <returns></returns>
        public DataSet ReadSzgkjc_Dxjb_All(string dxjbId)
        {
            string sql = @"select * from Szgkjc_Dxjb where DxjbId=@DxjbId;
                           select a.Dxjb_Sjml_Id,a.DxjbId,a.UserID,b.* from Szgkjc_Dxjb_Sjml a 
                           inner join Gwtz_Sjml b on a.SjmlID=b.SjmlID where a.DxjbId=@DxjbId ";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@DxjbId", dxjbId);
            return DB.ExeSqlForDataSetWithMultiDataTable(sql, sp);
        }

        #endregion

        #region 读取列表

        public DataTable RetrieveUser_List(AppUser workUser, List<IDataItem> condition, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "UserID desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select  * from g_user  ";
            condition.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }


        /// <summary>
        /// 通讯录树结构
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string ZTreeJsonOfTxl(GgfzInfoModel searchCondition)
        {
            string sql = @" select * from (
            select distinct a.groupID,a.groupName,c.sjmlID,c.sjmlName+' 【'+ nvl(c.sjmlMobile,'') +'】'  sjmlName,c.sjmlTel,c.SjmlEmail,d.UserID
                from Gwtz_Ggfz  a
                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                left join Gwtz_User_Relation d on c.SjmlID=d.SjmlID 
            ) t   where 1=1   ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql += " and SjmlName like '%" + searchCondition.SjmlName + "%' ";
                // spc.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }

            sql += " order by groupID ";
            DataTable dtSjr = this.DB.ExeSqlForDataTable(sql, spc, "t");

            string sql2 = @" select distinct a.groupID,a.groupName 
                                from Gwtz_Ggfz  a 
                                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                                where 1=1 ";

            SqlParameterCollection spc2 = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql2 += " and c.SjmlName like '%" + searchCondition.SjmlName + "%' ";
                //  spc2.Add("@SjmlName", "'%" + searchCondition.SjmlName + "%'");
            }
            DataTable dtSjz = this.DB.ExeSqlForDataTable(sql2, spc2, "t2");
            DataRow[] sjzRows = dtSjz.Select("1=1");

            string resultJosn = string.Empty;

            if (sjzRows.Length > 0)
            {
                resultJosn = @"[";
                for (int i = 0; i < dtSjz.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultJosn += @"{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false"",""isParent"":true";
                    }
                    else
                    {
                        resultJosn += @",{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false"",""isParent"":true";
                    }

                    if (dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).Length > 0)
                    {
                        DataTable tempdtSjr = dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).CopyToDataTable();
                        resultJosn += @",""children"":[";
                        for (int j = 0; j < tempdtSjr.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""isLeaf"":true }";
                            }
                            else
                            {
                                resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""isLeaf"":true }";
                            }
                        }
                        resultJosn += "]";
                    }
                    resultJosn += "}";
                }
                resultJosn += "]";
            }

            return resultJosn;
        }

        public DataTable ZComboxJsonOfTxl()
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select distinct c.sjmlName+' 【'+ isnull(c.sjmlMobile,'') +'】'  text,c.sjmlMobile  id
                from Gwtz_Ggfz  a
                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                left join dbo.Gwtz_User_Relation d on c.SjmlID=d.SjmlID  ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public string ZTreeJsonOfTxl(GgfzInfoModel searchCondition, string GzzsId)
        {
            if (string.IsNullOrEmpty(GzzsId))
                return "";
            string sql0 = "select * from Szgkjc_Gzhf where GzzsId=@GzzsId ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@GzzsId", GzzsId);
            DataTable dtGzhf = DB.ExeSqlForDataTable(sql0, spc, "t");

            string sql = @" select * from (
select distinct a.groupID,a.groupName,c.sjmlID,c.sjmlName+' 【'+ isnull(c.sjmlMobile,'') +'】'  sjmlName,c.sjmlTel,c.SjmlEmail
                            from Gwtz_Ggfz  a
                            inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                            inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                            where 1=1
) t 
where 1=1 ";
            spc = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql += " and SjmlName like @SjmlName";
                spc.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }

            sql += " order by groupID ";
            DataTable dtSjr = this.DB.ExeSqlForDataTable(sql, spc, "t");

            string sql2 = @" select distinct a.groupID,a.groupName 
                                from Gwtz_Ggfz  a 
                                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                                where 1=1 ";

            SqlParameterCollection spc2 = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql2 += " and c.SjmlName like @SjmlName";
                spc2.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }
            DataTable dtSjz = this.DB.ExeSqlForDataTable(sql2, spc2, "t2");
            DataRow[] sjzRows = dtSjz.Select("1=1");

            string resultJosn = string.Empty;

            if (sjzRows.Length > 0)
            {
                resultJosn = @"[";
                for (int i = 0; i < dtSjz.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultJosn += @"{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false"",""isParent"":true";
                    }
                    else
                    {
                        resultJosn += @",{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false"",""isParent"":true";
                    }

                    if (dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).Length > 0)
                    {
                        DataTable tempdtSjr = dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).CopyToDataTable();
                        resultJosn += @",""children"":[";
                        for (int j = 0; j < tempdtSjr.Rows.Count; j++)
                        {
                            DataRow[] rows = dtGzhf.Select("ZshfId=" + tempdtSjr.Rows[j]["sjmlID"]);
                            if (rows.Length > 0)
                            {
                                if (j == 0)
                                {
                                    resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""checked"":true,""isLeaf"":true }";

                                }
                                else
                                {
                                    resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""checked"":true,""isLeaf"":true }";
                                }
                            }
                            else
                            {
                                if (j == 0)
                                {
                                    resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""isLeaf"":true }";

                                }
                                else
                                {
                                    resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""" ,""isLeaf"":true}";
                                }
                            }
                        }
                        resultJosn += "]";
                    }
                    resultJosn += "}";
                }
                resultJosn += "]";
            }

            return resultJosn;
        }

        /// <summary>
        /// 短信简报预制通讯录
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string ZTreeJsonOfDxjb(GgfzInfoModel searchCondition)
        {
            string sql = @" select * from (
select distinct a.groupID,a.groupName,c.sjmlID,c.sjmlName+' 【'+ isnull(c.sjmlMobile,'') +'】'  sjmlName,c.sjmlTel,c.SjmlEmail
                            from Gwtz_Ggfz  a
                            inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                            inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                            where 1=1
) t 
where 1=1 ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql += " and SjmlName like '%" + searchCondition.SjmlName + "%'";
                // spc.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }

            sql += " order by groupID ";
            DataTable dtSjr = this.DB.ExeSqlForDataTable(sql, spc, "t");

            string sql2 = @" select distinct a.groupID,a.groupName 
                                from Gwtz_Ggfz  a 
                                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                                where 1=1 ";

            SqlParameterCollection spc2 = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql2 += " and c.SjmlName like '%" + searchCondition.SjmlName + "%'";
                //spc2.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }
            DataTable dtSjz = this.DB.ExeSqlForDataTable(sql2, spc2, "t2");
            DataRow[] sjzRows = dtSjz.Select("1=1");

            string resultJosn = string.Empty;

            if (sjzRows.Length > 0)
            {
                resultJosn = @"[";
                for (int i = 0; i < dtSjz.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultJosn += @"{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false""";
                    }
                    else
                    {
                        resultJosn += @",{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false""";
                    }

                    if (dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).Length > 0)
                    {
                        DataTable tempdtSjr = dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).CopyToDataTable();
                        resultJosn += @",""children"":[";
                        for (int j = 0; j < tempdtSjr.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""" }";
                            }
                            else
                            {
                                resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""" }";
                            }
                        }
                        resultJosn += "]";
                    }
                    resultJosn += "}";
                }
                resultJosn += "]";
            }

            return resultJosn;
        }


        public string ZTreeJsonOfDxjb(GgfzInfoModel searchCondition, string dxjbId)
        {
            string sql0 = "select * from Szgkjc_Dxjb_Sjml where DxjbId=@DxjbId ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@DxjbId", dxjbId);
            DataTable dtGzhf = DB.ExeSqlForDataTable(sql0, spc, "t");

            string sql = @" select * from (
select distinct a.groupID,a.groupName,c.sjmlID,c.sjmlName+' 【'+ isnull(c.sjmlMobile,'') +'】'  sjmlName,c.sjmlTel,c.SjmlEmail
                            from Gwtz_Ggfz  a
                            inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                            inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                            where 1=1
) t 
where 1=1 ";
            spc = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql += " and SjmlName like @SjmlName";
                spc.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }

            sql += " order by groupID ";
            DataTable dtSjr = this.DB.ExeSqlForDataTable(sql, spc, "t");

            string sql2 = @" select distinct a.groupID,a.groupName 
                                from Gwtz_Ggfz  a 
                                inner join Gwtz_Ggfz_Sjml  b on b.groupID = a.groupID
                                inner join Gwtz_Sjml  c on c.sjmlID = b.sjmlID
                                where 1=1 ";

            SqlParameterCollection spc2 = this.DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(searchCondition.SjmlName))
            {
                sql2 += " and c.SjmlName like @SjmlName";
                spc2.Add("@SjmlName", '%' + searchCondition.SjmlName + '%');
            }
            DataTable dtSjz = this.DB.ExeSqlForDataTable(sql2, spc2, "t2");
            DataRow[] sjzRows = dtSjz.Select("1=1");

            string resultJosn = string.Empty;

            if (sjzRows.Length > 0)
            {
                resultJosn = @"[";
                for (int i = 0; i < dtSjz.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultJosn += @"{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false""";
                    }
                    else
                    {
                        resultJosn += @",{""id"":" + dtSjz.Rows[i]["groupID"] + @",""name"":""" + dtSjz.Rows[i]["groupName"] + @""",""open"":""false""";
                    }

                    if (dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).Length > 0)
                    {
                        DataTable tempdtSjr = dtSjr.Select("groupID=" + dtSjz.Rows[i]["groupID"]).CopyToDataTable();
                        resultJosn += @",""children"":[";
                        for (int j = 0; j < tempdtSjr.Rows.Count; j++)
                        {
                            DataRow[] rows = dtGzhf.Select("SjmlID=" + tempdtSjr.Rows[j]["sjmlID"]);
                            if (rows.Length > 0)
                            {
                                if (j == 0)
                                {
                                    resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""checked"":true }";

                                }
                                else
                                {
                                    resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""",""checked"":true }";
                                }
                            }
                            else
                            {
                                if (j == 0)
                                {
                                    resultJosn += @"{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""" }";

                                }
                                else
                                {
                                    resultJosn += @",{""id"":" + tempdtSjr.Rows[j]["sjmlID"] + @",""name"":""" + tempdtSjr.Rows[j]["sjmlName"] + @""" }";
                                }
                            }
                        }
                        resultJosn += "]";
                    }
                    resultJosn += "}";
                }
                resultJosn += "]";
            }

            return resultJosn;
        }

        /// <summary>
        /// 获取工作指示信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveGzzs_List(AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            string Zssj = ft.GetValue("Zssj");
            ft.Remove("Zssj");
            ft.Translate();
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "GzzsId desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            //string sql = "select * from Szgkjc_Gzzs a where UserId=@UserId and ";
            string sql = @"select *,stuff((select ','+ZshfrName+'【'+(case when Phone is null then '' else phone end)+'】' from Szgkjc_Gzhf a inner join dbo.Gwtz_Sjml b on a.ZshfrId=b.SjmlID 
                    where a.GzzsId=c.GzzsId for xml path('')),1,1,'')  zdhfr
                            from Szgkjc_Gzzs  c  where UserId=@UserId and ";
            if (!string.IsNullOrEmpty(Zssj))
            {
                sql += " CONVERT(VARCHAR(50),Zssj,112)=@Zssj and";
                sp.Add("@Zssj", DateTime.Parse(Zssj).ToString("yyyyMMdd"));
            }
            sp.Add("@UserId", workUser.UserID);
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }
        /// <summary>
        /// 指示回复列表
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveZshf_all_List(AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            string DataState = ft.GetValue("DataState");
            string Zssj = ft.GetValue("Zssj");
            string Zshfsj = ft.GetValue("Zshfsj");
            ft.Remove("DataState");
            ft.Remove("Zssj");
            ft.Remove("Zshfsj");
            ft.Translate();

            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "ZshfId desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from (select a.*,b.Gzzszt,b.GzzsNr,b.ZsrName,b.ZsrPhone,b.ZsrEmail,b.Zssj from Szgkjc_Gzhf a 
                            inner join Szgkjc_Gzzs b on a.GzzsId=b.GzzsId where a.UserId=@UserId)  aa where 1=1 and ";
            sp.Add("@UserId", workUser.UserID);
            if (!string.IsNullOrEmpty(DataState))
            {
                sql += " DataState=@DataState and";
                sp.Add("@DataState", DataState);
            }
            if (!string.IsNullOrEmpty(Zssj))
            {
                sql += " CONVERT(VARCHAR(50),Zssj,112)=@Zssj and";
                sp.Add("@Zssj", DateTime.Parse(Zssj).ToString("yyyyMMdd"));
            }
            if (!string.IsNullOrEmpty(Zshfsj))
            {
                sql += " CONVERT(VARCHAR(50),Zshfsj,112)=@Zshfsj and";
                sp.Add("@Zshfsj", DateTime.Parse(Zshfsj).ToString("yyyyMMdd"));
            }

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }
        /// <summary>
        /// 获取某一工作指示的回复信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public DataTable RetrieveZshfListByGzzsId(AppUser workUser, string gzzsId)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select a.*,b.* from Szgkjc_Gzhf a inner join dbo.Gwtz_Sjml b on a.ZshfrId=b.SjmlID where GzzsId=@GzzsId ";
            sp.Add("@GzzsId", gzzsId);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        public DataTable RetrieveZshfListByGzzsId(AppUser workUser, List<IDataItem> condition, string gzzsId, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "ZshfId desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select a.*,b.* from Szgkjc_Gzhf a inner join dbo.Gwtz_Sjml b on a.ZshfrId=b.SjmlID where GzzsId=@GzzsId ";
            sp.Add("@GzzsId", gzzsId);
            condition.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }

        /// <summary>
        /// 获取短信简报信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveDxjb_List(AppUser workUser, List<IDataItem> conditions, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "DxjbId desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from  Szgkjc_Dxjb  where  DataState<>-1 ";
            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }

        public DataTable RetrieveDxjb_List(AppUser workUser, List<IDataItem> conditions)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from  Szgkjc_Dxjb  where DataState<>-1   ";
            conditions.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }


        public DataTable RetrieveDxjb_Sxr_List(AppUser workUser, string dxjbId)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select a.*,b.SjmlName,b.SjmlTel from Szgkjc_Dxjb_Sjml a 
                            inner join Gwtz_Sjml b on a.SjmlID=b.SjmlID where a.DxjbId in (" + dxjbId + ") ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable RetrieveDxjb_AllSxr_List()
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from  Gwtz_Sjml  ";
            return DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        /// <summary>
        /// 根据通讯录id获取通讯录详细信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="sjmlIds"></param>
        /// <param name="conditions"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveSjml_List(AppUser workUser, string sjmlIds)
        {
            string sql = @" select * from Gwtz_Sjml a where a.SjmlID in (" + sjmlIds + ") ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 获取短信简报发送记录信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="sjmlIds"></param>
        /// <returns></returns>
        public DataTable RetrieveDxjbSendRecords_List(AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "Id desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from Szgkjc_Dxjb_Records a where a.DataState<>-1 and UserId=@UserId and  ";
            sp.Add("UserId", workUser.UserID);
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }
        /// <summary>
        /// 预制短信简报列表
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveYzDxjb_List(AppUser workUser, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.Trim())) { orderby = "DxjbId desc"; };
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @" select * from Szgkjc_Dxjb a where a.DataState<>-1 and  ";
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "dt", orderby, pagesize, page, out allRecordCount);
        }
        /// <summary>
        /// 获取短信发送记录的收信人信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <param name="sjmlIds"></param>
        /// <returns></returns>
        public DataTable RetrieveDxjbSendSxr_List(AppUser workUser, string sjmlIds)
        {
            string sql = @" select * from Gwtz_Sjml a where a.SjmlID in (" + sjmlIds + ") ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable RetrieveJytj(AppUser workUser)
        {
            string sql = @"select '市政企业 '+(select convert(varchar,count(*)) from Szgkjc_Qyjbxx)+' 家，其中本地企业 '+
(select convert(varchar,count(*)) from Szgkjc_Qyjbxx where City='张家港')+' 家，外地企业 '+
(select convert(varchar,count(*)) from Szgkjc_Qyjbxx where City<>'张家港' or City is null )+' 家。'  tjxx
union all 
select '市政工程 '+(select convert(varchar,count(*)) from Szgkjc_Lxxmxx  )+' 个，总投资 '+
(select convert(varchar,sum(Xmzj)) from Szgkjc_Lxxmxx  )+' 万元。'  tjxx

union all 
select '桥梁 '+(select convert(varchar,count(*)) from Szgkjc_Ql_Qljbxx  )+' 座，总面积 '+
(select convert(varchar,sum(Qmmj)) from Szgkjc_Ql_Qljbxx )+' 平方米，其中沥青面积 '+
(select convert(varchar,sum(Lqmj)) from Szgkjc_Ql_Qljbxx  )+' 平方米，砼路面面积 '+
(select convert(varchar,sum(Tlmmj)) from Szgkjc_Ql_Qljbxx  )+' 平方米。'   tjxx

union all 

select '道路 '+(select convert(varchar,count(*)) from Szgkjc_Szss_Dlxx where  Dllx='道路')+' 条，总长度 '+
(select convert(varchar,sum(DlLong)) from Szgkjc_Szss_Dlxx where  Dllx='道路')+' 米，道路总面积 '+
(select convert(varchar,sum(Dlkcdmj)+sum(Dlmcdmj)) from Szgkjc_Szss_Dlxx where  Dllx='道路')+' 平方米，其中快车道面积'+
(select convert(varchar,sum(Dlkcdmj)) from Szgkjc_Szss_Dlxx where  Dllx='道路')+' 平方米，慢车道面积 '+
(select convert(varchar,sum(Dlmcdmj)) from Szgkjc_Szss_Dlxx where  Dllx='道路')+' 平方米。'  tjxx

union all 
select '里弄 '+(select convert(varchar,count(*)) from Szgkjc_Szss_Dlxx where  Dllx='里弄')+' 条，总长度 '+
(select convert(varchar,sum(DlLong)) from Szgkjc_Szss_Dlxx where  Dllx='里弄')+' 米，道路总面积 '+
(select convert(varchar,sum(Dlzmj)) from Szgkjc_Szss_Dlxx where  Dllx='里弄')+' 平方米，其中车行道面积'+
(select convert(varchar,sum(Dlcxdmj)) from Szgkjc_Szss_Dlxx where  Dllx='里弄')+' 平方米，道口面积 '+
(select convert(varchar,sum(Dkmj)) from Szgkjc_Szss_Dlxx where  Dllx='里弄')+' 平方米。'  tjxx
union all 
select '行政审批事项'+(select convert(varchar,count(*)) from Szgkjc_XzspXmxx  )+' 项，其中道口开设 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='道口开设'  )+' 项，已完成 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='道口开设' and DataState=400 )+' 项；道路占用'+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='道路占用'  )+' 项，已完成'+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='道路占用' and DataState=200  )+' 项；临时挖掘 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='临时挖掘'  )+' 项，已完成 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='临时挖掘' and DataState=200  )+' 项；雨污接入'+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='雨污接入'  )+' 项，已完成 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='雨污接入' and DataState=400 )+' 项；排水许可证 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='排水许可证'  )+' 项，已完成 '+
(select convert(varchar,count(*)) from Szgkjc_XzspXmxx where Xmlx='排水许可证' and DataState=400 )+' 项。'
 ";

            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取公文通知信息
        /// </summary>
        /// <param name="workUser"></param>
        /// <returns></returns>
        public DataTable ReadGwtz(AppUser workUser)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@UserID", workUser.UserID);

            string sql = @"select top 4 c.*,(case when  LEN(infotitle)>13 then SUBSTRING(infotitle,0,13)+'......' else infotitle end)  xxmc
                           from Gwtz_Info_Sjml  a
                           inner join Gwtz_Sjml  b on b.SjmlID = a.SjmlID 
                           inner join Gwtz_User_Relation  d on b.SjmlID=d.sjmlID
                           inner join Gwtz_Info  c on c.InfoId = a.InfoId 
                           where 1=1  and c.IsDeleted<>1 and c.InfoType='公文通知' and d.UserID=@UserID order by c.SendDate desc";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        public DataTable RetrieveGzzs_NoRead(AppUser workUser)
        {
            string sql = @" select b.SjmlName,a.*,c.Gzzszt,ZsrName,substring(convert(varchar(50),Zssj,120),1,16)  Zssj from Szgkjc_Gzhf a inner join dbo.Gwtz_Sjml b on a.ZshfrId=b.SjmlID
inner join dbo.Szgkjc_Gzzs c on a.GzzsId=c.GzzsId where a.UserID=@userId and  a.DataState=0 order by Zssj desc ";

            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add("@userId", workUser.UserID);
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }

        public DataTable RetrieveGzyj()
        {
            string sql = @" select * from (
select row_id,qymc mc,:lx1 lx,'../Szqy/QyxxToolBar.aspx?rowid=' url   from ( 
select distinct a.rowid row_id ,a.qymc,a.xxdd,a.lxr,a.lxdh,a.fddbr,b.zslx,b.zsbh,to_char(b.zsyxqrq,'yyyy-mm-dd') zsyxqrq,
to_char(b.zsyxzrq,'yyyy-mm-dd') zsyxzrq from uepp_qyjbxx a inner join uepp_qyzs b on a.qyid=b.qyid 
) qy where 1=1 and (zsyxzrq between to_char(sysdate,'yyyy-mm-dd') and to_char((select add_months(sysdate,3) from dual),'yyyy-mm-dd' ) 
 or zsyxzrq<=to_char(sysdate,'yyyy-mm-dd'))  and rownum<=5 order  by row_id desc )
 union all 
 select * from (
 select row_id,xm mc,:lx2 lx,'../Zyry/RyxxToolBar.aspx?rowid=' url from (
select  a.rowid row_id, a.xm,a.zjhm,a.zcjb,b.ryzyzglx,a.lxdh,b.ryzslx,b.zsbh,to_char(b.zsyxqrq,'yyyy-mm-dd') zsyxqrq,
to_char(b.zsyxzrq,'yyyy-mm-dd') zsyxzrq from uepp_ryjbxx a inner join uepp_ryzs b on a.ryid=b.ryid
) ry where 1=1 and  (zsyxzrq between to_char(sysdate,'yyyy-mm-dd') and to_char((select add_months(sysdate,3) from dual),'yyyy-mm-dd' ) 
or zsyxzrq<=to_char(sysdate,'yyyy-mm-dd')) and rownum<=5 ) ";
            //造价过亿的项目中zj的字段含有多类非数字字符，不好转换，故先去掉
            //union all
            //select * from (
            //select row_id,xmmc mc,:lx3 lx from (
            //select distinct a.rowid row_id,a.xmmc,a.jsdw,a.sgdw,a.xmjl,a.zj,a.ssdqid,a.ssdq,b.rowid jsdwrowid,c.rowid qyrowid,d.rowid ryrowid
            //from uepp_xmjbxx a inner join uepp_jsdw b on a.jsdwid=b.jsdwid left join uepp_qyjbxx c on a.sgdwid=c.qyid left join uepp_ryjbxx d on a.xmjlid=d.ryid
            //where a.zj>10000) xm where 1=1 and rownum<=5 order  by row_id desc)
            sql += @"
union all
select * from (
select row_id,xmmc mc,:lx4 lx,'../Szgc/SggcToolBar.aspx?rowid=' url from (
select distinct a.rowid row_id,a.xmmc,a.ssdq,a.jsdw,a.sgdw,a.xmjl, a.sgxmtybh,to_char(b.kgrq,'yyyy-mm-dd') kgrq,e.rowid jsdwrowid,f.rowid qyrowid,g.rowid ryrowid from uepp_xmjbxx a 
inner join uepp_aqjdxx b on a.sgxmtybh=b.sgxmtybh and to_char(b.kgrq,'yyyy-mm-dd') <=to_char(sysdate,'yyyy-mm-dd')
left join uepp_jsdw e on a.jsdwid=e.jsdwid left join uepp_qyjbxx f on a.sgdwid=f.qyid left join uepp_ryjbxx g on a.xmjlid=g.ryid
where a.sgxmtybh not in (select distinct d.sgxmtybh from uepp_sgxkxx c inner join UEPP_SgxkAndBdRelation d on c.sgxkid=d.sgxkid )
) xm where 1=1 and rownum<=5 order  by row_id desc)
union all
select * from (
select  row_id,xmmc,:lx5 lx,'../Szgc/SggcToolBar.aspx?rowid=' url from (
select distinct b.rowid row_id,a.xmmc,a.jsdw,a.sgdw,b.xmbgmc,b.xmbgbw,b.bggsje,c.rowid jsdwrowid,
d.rowid qyrowid from uepp_xmjbxx a inner join  UEPP_xmBgjl b  on a.sgxmtybh =b.sgxmtybh 
left join uepp_jsdw c on a.jsdwid=c.jsdwid left join uepp_qyjbxx d on a.sgdwid=d.qyid
) xm where 1=1  and rownum<=5 order  by row_id desc) ";

            SqlParameterCollection sp = this.DB.CreateSqlParameterCollection();
            sp.Add(":lx1", "企业证书过期");
            sp.Add(":lx2", "人员证书过期");
            //sp.Add(":lx3", "项目造价过亿");
            sp.Add(":lx4", "未办施工许可证");
            sp.Add(":lx5", "工程变更项目");
            return this.DB.ExeSqlForDataTable(sql, sp, "dt");
        }
        #endregion

        #region 更新数据
        public bool SubmitSzgkjc_Gzhf(DataTable dt)
        {

            string sql = "select * from Szgkjc_Gzhf where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SubmitSzgkjc_Gzzs(DataTable dt)
        {

            string sql = "select * from Szgkjc_Gzzs where 1=2";
            return DB.Update(sql, null, dt);
        }

        public bool SubmitSzgkjc_Dxjb(DataTable dt)
        {

            string sql = "select * from Szgkjc_Dxjb where 1=2";
            return DB.Update(sql, null, dt);
        }
        public bool SubmitSzgkjc_Dxjb_Sjml(DataTable dt)
        {

            string sql = "select * from Szgkjc_Dxjb_Sjml where 1=2";
            return DB.Update(sql, null, dt);
        }
        /// <summary>
        /// 更新短信简报记录信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SubmitSzgkjc_Dxjb_Records(DataTable dt)
        {

            string sql = "select * from Szgkjc_Dxjb_Records where 1=2";
            return DB.Update(sql, null, dt);
        }
        #endregion

        #region 删除
        public bool DeleteGzzshfById(string gzzsId)
        {
            string sql = "delete from  dbo.Szgkjc_Gzzs where GzzsId in (" + gzzsId + "); delete from  dbo.Szgkjc_Gzhf where GzzsId in (" + gzzsId + ") ";
            return DB.ExecuteNonQuerySql2(sql, null) > 0;
        }

        public bool DeleteDxjbById(string dxjbId)
        {
            string sql = "delete from Szgkjc_Dxjb where DxjbId in (" + dxjbId + "); delete from  Szgkjc_Dxjb_Sjml where DxjbId in (" + dxjbId + ") ";
            return DB.ExecuteNonQuerySql2(sql, null) > 0;
        }
        /// <summary>
        /// 删除指定的工作指示编号的回复记录
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public bool DeleteOnlyGzzshfById(string gzzsId)
        {
            string sql = " delete from  dbo.Szgkjc_Gzhf where GzzsId in (" + gzzsId + ") ";
            return DB.ExecuteNonQuerySql2(sql, null) > 0;
        }
        /// <summary>
        /// 删除短信简报的联系人信息
        /// </summary>
        /// <param name="dxjbId"></param>
        /// <returns></returns>
        public bool DeleteZJG_Dxjb_Sjml(string dxjbId)
        {
            string sql = " delete from  Szgkjc_Dxjb_Sjml where DxjbId in (" + dxjbId + ") ";
            return DB.ExecuteNonQuerySql2(sql, null) > 0;
        }


        #endregion



        public string ExecuteSql(string sqlStr)
        {
            return DB.ExeSqlForString(sqlStr, null);
        }


        #region 首页简要统计

        public DataTable GetTjb()
        {
            string sql = "select * from uepp_tjb ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }

        public string GetXmtj(int aqjdflag, string ssdq)
        {
            string sql = "";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            switch (aqjdflag)
            {
                case 1://竣工项目
                    if (!string.IsNullOrEmpty(ssdq))
                    {
                        sql = "select count(0) from uepp_aqjdxx a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.aqjdflag=1 and a.jsdw is not null and ";// (aqjdflag=1 or aqjdflag is null ) and ";
                        if (ssdq == "市区")
                        {
                            sql += " (b.ssdq like :pp1 or b.ssdq like :pp2)";
                            sp.Add(":pp1", "%市区%");
                            sp.Add(":pp2", "%市辖区%");
                        }
                        else
                        {
                            sql += "  b.ssdq like :pp3";
                            sp.Add(":pp3", "%" + ssdq + "%");
                        }
                    }
                    else
                        sql = " select count(0)  from uepp_aqjdxx  a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.aqjdflag=1 and a.jsdw is not null  ";//or aqjdflag is null";
                    break;
                case 0://在建项目
                    if (!string.IsNullOrEmpty(ssdq))
                    {
                        sql = "select count(0) from uepp_aqjdxx  a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh  where a.aqjdflag=0  and a.jsdw is not null  and ";
                        if (ssdq == "市区")
                        {
                            sql += " (b.ssdq like :pp1 or b.ssdq like :pp2)";
                            sp.Add(":pp1", "%市区%");
                            sp.Add(":pp2", "%市辖区%");
                        }
                        else
                        {
                            sql += "  b.ssdq like :pp3";
                            sp.Add(":pp3", "%" + ssdq + "%");
                        }
                    }
                    else
                        sql = " select count(0)  from uepp_aqjdxx  a inner join uepp_xmjbxx b on a.sgxmtybh=b.sgxmtybh where a.aqjdflag=0  and a.jsdw is not null ";
                    break;
            }
            return DB.ExeSqlForString(sql, sp);
        }

        public string GetQytj(string qylx)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            if (qylx == "jsdw")
                sql = "select count(0) from uepp_jsdw ";
            else
            {
                string csywlxID = "";
                switch (qylx)
                {
                    case "sgdw":
                        csywlxID = "1,2,3";
                        break;
                    case "kcsjdw":
                        csywlxID = "5,6";
                        break;
                    case "zjjg":
                        csywlxID = "4,7,8,9";
                        break;
                    case "qt":
                        csywlxID = "''";
                        break;
                }
                sql = "select count(0) from uepp_qyjbxx a inner join uepp_qycsyw b on a.qyid=b.qyid where b.csywlxid in (" + csywlxID + ")";
            }

            return DB.ExeSqlForString(sql, null);
        }


        public string GetRytj(int pRyzyzglxID)
        {
            //string sql = @"select count(0) from uepp_ryjbxx a inner join  uepp_ryzyzg b on a.ryid=b.ryid inner join uepp_qyry c on a.ryid=c.ryid inner join uepp_qyjbxx d on c.qyid=d.qyid 
//where b.ryzyzglxid=" + pRyzyzglxID;
            string sql = @"select count(0) from (select distinct a.rowid row_id, a.ryid,a.xm,a.zjlx,a.zjhm,c.qyid,c.qymc,a.zczh,a.sylx,a.zcjb,nvl(a.lxdh,a.yddh) lxdh,a.datastate,c.county,c.provinceid,c.province,c.rowid qyrowid     
 from uepp_ryjbxx a inner join uepp_qyry b on a.ryid=b.ryid inner join uepp_qyjbxx c on b.qyid=c.qyid  where a.ryid in (select ryid from UEPP_Ryzyzg where ryzyzglxid in (" + pRyzyzglxID + ")))";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            return DB.ExeSqlForString(sql, null);
        }
        #endregion
    }
}
