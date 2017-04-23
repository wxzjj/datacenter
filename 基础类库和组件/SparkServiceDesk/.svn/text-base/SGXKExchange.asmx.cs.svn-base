using System;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Web;
using System.Web.Services;
using Bigdesk8.Data;
using System.Data;

namespace SparkServiceDesk
{
    /// <summary>
    /// SGXKExchange 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SGXKExchange : System.Web.Services.WebService
    {

        public DBOperator ConnDBServer;

        private void DBServerOpen()
        {
            ConnDBServer = new DatabaseOperator();
        }

        private class DatabaseOperator : SqlServerDbOperator
        {
            public DatabaseOperator()
                : base(GetConnectionString(), (DataBaseType)Enum.Parse(typeof(DataBaseType), GetDatabaseType().ToUpper()))
            {
            }
        }

        /// <summary>
        /// 数据库类型，如sqlserver2000，sqlserver2005。
        /// </summary>
        private static string GetDatabaseType()
        {
            return ConfigurationManager.ConnectionStrings["SparkServiceDesk_SgxkConnectionString"].ProviderName;
        }

        /// <summary>
        /// 数据库连接字符串，如data source=.;user id=sa;password=1;database=SCIC82。
        /// </summary>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SparkServiceDesk_SgxkConnectionString"].ConnectionString;
        }

        /// <summary>
        /// 提交项目信息
        /// </summary>             
        /// <param name="argZzjgdm">组织机构代码即用户名</param>
        /// <param name="argPassWD">密码</param>
        /// <param name="argXmlProjInfo">argXmlProjInfo字符串类型，为XML的表示</param>  
        /// <returns>字符串类型，为XML的表示</returns>
        [WebMethod]
        public string ApplyProjInfo(string argLoginName, string argPassWD, string argXmlProjInfo)
        {
            DBServerOpen();
            string sqlTemp = "select '' AS status, '' AS reason  from webplat50..UserInfo where 1 = 2";
            DataSet dsTurn = ConnDBServer.ExeSqlForDataSet(sqlTemp, null, "ReturnInfo");
            DataRow ReturnDR = dsTurn.Tables["ReturnInfo"].NewRow();
            ReturnDR["status"] = string.Empty;
            ReturnDR["reason"] = string.Empty;
            dsTurn.Tables["ReturnInfo"].Rows.Add(ReturnDR);

            //用户合法性校验
            //sqlTemp = "select LoginID from webplat50..UserInfo where LoginName = '" + argLoginName + "' and Password = '" + argPassWD + "'";
            //if (ConnDBServer.ExeSqlForString(sqlTemp, null).Length == 0)
            //{
            //    //返回DataSet构造
            //    dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
            //    dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "用户名或密码错误！数据上传失败！";
            //    return dsTurn.GetXml();
            //}

            DataSet dsSource = new DataSet();
            try
            {
                dsSource.ReadXml(new System.IO.StringReader(argXmlProjInfo));
            }
            catch (Exception ee)
            {
                dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                return dsTurn.GetXml();
            }

            try
            {
                if (dsSource.Tables.Count > 0)
                {
                    //保存数据

                    ConnDBServer.BeginTransaction();
                    #region 项目信息
                    //处理项目信息
                    foreach (DataRow dr in dsSource.Tables["项目信息"].Rows)
                    {
                        string Project_GUID = dr["Project_GUID"].ToString();
                        string ProjectMc = dr["ProjectMc"].ToString();
                        try
                        {
                            string sql = "select * from SGXK_XK_ProjectInfo where Project_GUID='" + Project_GUID + "'";
                            if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                            {
                                ConnDBServer.RollbackTransaction();
                                dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "项目的编号(" + Project_GUID + ")已经存在！";
                                return dsTurn.GetXml();
                            }
                            //添加项目

                            string sql3 = "select * from SGXK_XK_ProjectInfo where 1=2";
                            DataTable dt = ConnDBServer.ExeSqlForDataTable(sql3, null, "t3");
                            dt.Rows.Add(dt.NewRow());
                            /*2015-5-14 李贯涛新增 新版数据对接新增字段：项目备案编号*/
                            if (string.IsNullOrEmpty(dr["XiangMuNum"].ToString()))
                            {
                                dr["XiangMuNum"] = null;
                            }
                            if (string.IsNullOrEmpty(dr["JHKaiGRiQ"].ToString()))
                            {
                                dr["JHKaiGRiQ"] = "1900-1-1";
                            }
                            if (string.IsNullOrEmpty(dr["JHJunGRiQ"].ToString()))
                            {
                                dr["JHJunGRiQ"] = "1900-1-1";
                            }
                            if (string.IsNullOrEmpty(dr["JingDuGis"].ToString()))
                            {
                                dr["JingDuGis"] = null;
                            }

                            if (string.IsNullOrEmpty(dr["ProjectHeZRiQ"].ToString()))
                            {
                                dr["ProjectHeZRiQ"] = null;
                            }

                            if (string.IsNullOrEmpty(dr["WeiDuGis"].ToString()))
                            {
                                dr["WeiDuGis"] = null;
                            }
                            if (string.IsNullOrEmpty(dr["AddDate"].ToString()))
                            {
                                dr["AddDate"] = DateTime.Now;
                            }

                            dr.ToDataRow(dt.Rows[0]);
                            ConnDBServer.Update(sql3, null, dt);


                        }
                        catch (Exception ee)
                        {
                            ConnDBServer.RollbackTransaction();
                            dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                            dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                            return dsTurn.GetXml();
                        }

                    }
                    #endregion

                    #region 流程信息
                    if (dsSource.Tables["流程信息"] != null)
                    {
                        foreach (DataRow dr in dsSource.Tables["流程信息"].Rows)
                        {
                            string GC_GUID = dr["GC_GUID"].ToString();
                            if (string.IsNullOrEmpty(GC_GUID))
                            {
                                continue;
                            }
                            try
                            {
                                string sql = "select * from SGXK_xmfjb where GC_GUID='" + GC_GUID + "'";
                                if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                                {
                                    ConnDBServer.RollbackTransaction();
                                    dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                    dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "GC_Info的编号(" + GC_GUID + ")已经存在！";
                                    return dsTurn.GetXml();
                                }

                                //添加项目附加表
                                string sql2 = "select * from SGXK_xmfjb where 1=2 ";
                                DataTable dt = ConnDBServer.ExeSqlForDataTable(sql2, null, "t3");
                                dt.Rows.Add(dt.NewRow());
                                if (string.IsNullOrEmpty(dr["FbfaDate"].ToString()))
                                {
                                    dr["FbfaDate"] = "1900-1-1";
                                }

                                if (string.IsNullOrEmpty(dr["ZbggDate"].ToString()))
                                {
                                    dr["ZbggDate"] = "1900-1-1";
                                }

                                if (string.IsNullOrEmpty(dr["ZbgsDate"].ToString()))
                                {
                                    dr["ZbgsDate"] = "1900-1-1";
                                }
                                if (string.IsNullOrEmpty(dr["ZbbaDate"].ToString()))
                                {
                                    dr["ZbbaDate"] = "1900-1-1";
                                }
                                if (string.IsNullOrEmpty(dr["HtbaDate"].ToString()))
                                {
                                    dr["HtbaDate"] = "1900-1-1";
                                }
                                if (string.IsNullOrEmpty(dr["ZjbaDate"].ToString()))
                                {
                                    dr["ZjbaDate"] = "1900-1-1";
                                }


                                dr.ToDataRow(dt.Rows[0]);
                                ConnDBServer.Update(sql2, null, dt);
                            }

                            catch (Exception ee)
                            {
                                ConnDBServer.RollbackTransaction();
                                dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                                return dsTurn.GetXml();
                            }
                        }
                    }
                    #endregion

                    #region 工程信息
                    //处理工程信息
                    //GC_Info
                    if (dsSource.Tables["GC_Info"] != null)
                    {
                        foreach (DataRow dr in dsSource.Tables["GC_Info"].Rows)
                        {
                            string GC_GUID = dr["GC_GUID"].ToString();
                            string GongCName = dr["GongCName"].ToString();
                            try
                            {
                                string sql = "select * from SGXK_GC_Info where GC_GUID='" + GC_GUID + "'";
                                if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                                {
                                    ConnDBServer.RollbackTransaction();
                                    dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                    dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "GC_Info的编号(" + GC_GUID + ")已经存在！";
                                    return dsTurn.GetXml();
                                }


                                string sql3 = "select * from SGXK_GC_Info where 1=2";
                                DataTable dt = ConnDBServer.ExeSqlForDataTable(sql3, null, "t3");
                                dt.Rows.Add(dt.NewRow());
                                if (string.IsNullOrEmpty(dr["JHKaiGRiQ"].ToString()))
                                {
                                    dr["JHKaiGRiQ"] = "1900-1-1";
                                }
                                if (string.IsNullOrEmpty(dr["JHJunGRiQ"].ToString()))
                                {
                                    dr["JHJunGRiQ"] = "1900-1-1";
                                }

                                if (string.IsNullOrEmpty(dr["GongCGuiMo"].ToString()))
                                {
                                    dr["GongCGuiMo"] = "无";
                                }

                                if (string.IsNullOrEmpty(dr["LZTime"].ToString()))
                                {
                                    dr["LZTime"] = null;
                                }
                                if (string.IsNullOrEmpty(dr["HeiTongZaoJ"].ToString()))
                                {
                                    dr["HeiTongZaoJ"] = null;
                                }
                                if (string.IsNullOrEmpty(dr["JianZMianj"].ToString()))
                                {
                                    dr["JianZMianj"] = null;
                                }

                                dr.ToDataRow(dt.Rows[0]);

                                ConnDBServer.Update(sql3, null, dt);
                            }
                            catch (Exception ee)
                            {
                                ConnDBServer.RollbackTransaction();
                                dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                                return dsTurn.GetXml();
                            }
                        }
                    }
                    #endregion

                    #region DataList
                    //处理datalist
                    if (dsSource.Tables["DataList"] != null)
                    {
                        foreach (DataRow dr in dsSource.Tables["DataList"].Rows)
                        {
                            //参见单位
                            if (argXmlProjInfo.Contains("Dept_GUID"))
                            {
                                string Dept_GUID = dr["Dept_GUID"].ToString();
                                if (!string.IsNullOrEmpty(Dept_GUID))
                                {
                                    try
                                    {
                                        string sql = "select * from SGXK_GC_CJDept where  Dept_GUID='" + Dept_GUID + "'";
                                        if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                                        {
                                            ConnDBServer.RollbackTransaction();
                                            dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                            dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "SGXK_GC_CJDept的编号(" + Dept_GUID + ")已经存在！";
                                            return dsTurn.GetXml();
                                        }
                                        //新增

                                        string sql2 = "select * from SGXK_GC_CJDept where 1=2";
                                        DataTable dt = ConnDBServer.ExeSqlForDataTable(sql2, null, "t");
                                        dt.Rows.Add(dt.NewRow());

                                        if (string.IsNullOrEmpty(dr["CJDeptType"].ToString()))
                                        {
                                            dr["CJDeptType"] = '无';
                                        }

                                        if (string.IsNullOrEmpty(dr["HeTongBeiAnNum"].ToString()))
                                        {
                                            dr["HeTongBeiAnNum"] = '无';
                                        }

                                        if (string.IsNullOrEmpty(dr["ZiZhiXuKeJG"].ToString()))
                                        {
                                            dr["ZiZhiXuKeJG"] = DBNull.Value;
                                        }

                                        if (string.IsNullOrEmpty(dr["IdentifyType"].ToString()))
                                        {
                                            dr["IdentifyType"] = '无';
                                        }

                                        if (string.IsNullOrEmpty(dr["IdentifyNum"].ToString()))
                                        {
                                            dr["IdentifyNum"] = '无';
                                        }

                                        if (string.IsNullOrEmpty(dr["ZiGeXuKeJG"].ToString()))
                                        {
                                            dr["ZiGeXuKeJG"] = '无';
                                        }

                                        dr.ToDataRow(dt.Rows[0]);
                                        ConnDBServer.Update(sql2, null, dt);
                                    }
                                    catch (Exception ee)
                                    {
                                        ConnDBServer.RollbackTransaction();
                                        dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                        dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                                        return dsTurn.GetXml();
                                    }
                                }

                            }

                            ////单体工程
                            //if (argXmlProjInfo.Contains("C_GUID"))
                            //{
                            //    string C_GUID = dr["C_GUID"].ToString();
                            //    if (!string.IsNullOrEmpty(C_GUID))
                            //    {
                            //        try
                            //        {
                            //            string sql = "select * from SGXK_GC_Content where C_GUID='" + C_GUID + "'";
                            //            if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                            //            {
                            //                ConnDBServer.RollbackTransaction();
                            //                dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                            //                dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "SGXK_GC_Content的编号(" + C_GUID + ")已经存在！";
                            //                return dsTurn.GetXml();
                            //            }
                            //            //新增
                            //            string sql2 = "select * from SGXK_GC_Content where 1=2";
                            //            DataTable dt = ConnDBServer.ExeSqlForDataTable(sql2, null, "t");
                            //            dt.Rows.Add(dt.NewRow());
                            //            if (string.IsNullOrEmpty(dr["DiShangCengShu"].ToString()))
                            //            {
                            //                dr["DiShangCengShu"] = DBNull.Value;
                            //            }

                            //            if (string.IsNullOrEmpty(dr["DiXiaCengShu"].ToString()))
                            //            {
                            //                dr["DiXiaCengShu"] = DBNull.Value;
                            //            }

                            //            if (string.IsNullOrEmpty(dr["DiXiaArea"].ToString()))
                            //            {
                            //                dr["DiXiaArea"] = DBNull.Value;
                            //            }

                            //            if (string.IsNullOrEmpty(dr["DiShangChangDu"].ToString()))
                            //            {
                            //                dr["DiShangChangDu"] = DBNull.Value;
                            //            }

                            //            if (string.IsNullOrEmpty(dr["DiXiaChangDu"].ToString()))
                            //            {
                            //                dr["DiXiaChangDu"] = DBNull.Value;
                            //            }

                            //            dr.ToDataRow(dt.Rows[0]);

                            //            ConnDBServer.Update(sql2, null, dt);
                            //        }
                            //        catch (Exception ee)
                            //        {
                            //            ConnDBServer.RollbackTransaction();
                            //            dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                            //            dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                            //            return dsTurn.GetXml();
                            //        }
                            //    }
                            //}


                            //证明材料表
                            if (argXmlProjInfo.Contains("WJ_Guid"))
                            {
                                string AttGuid = dr["WJ_Guid"].ToString();
                                if (!string.IsNullOrEmpty(AttGuid))
                                {
                                    try
                                    {
                                        string sql = "select * from SGXK_WJ_Info where WJ_Guid='" + AttGuid + "'";
                                        if (ConnDBServer.ExeSqlForDataTable(sql, null, "t").Rows.Count > 0)
                                        {
                                            ConnDBServer.RollbackTransaction();
                                            dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                            dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "SGXK_WJ_Info的编号(" + AttGuid + ")已经存在！";
                                            return dsTurn.GetXml();
                                        }
                                        //新增
                                        string sql2 = "select * from SGXK_WJ_Info where 1=2";
                                        DataTable dt = ConnDBServer.ExeSqlForDataTable(sql2, null, "t");
                                        dt.Rows.Add(dt.NewRow());
                                        if (string.IsNullOrEmpty(dr["PiZhunRq"].ToString()))
                                        {
                                            dr["PiZhunRq"] = null;
                                        }

                                        if (string.IsNullOrEmpty(dr["PiZhunDX"].ToString()))
                                        {
                                            dr["PiZhunDX"] = DBNull.Value;
                                        }

                                        if (string.IsNullOrEmpty(dr["LimitType"].ToString()))
                                        {
                                            dr["LimitType"] = '无';
                                        }

                                        if (string.IsNullOrEmpty(dr["YouXiaoDate"].ToString()))
                                        {
                                            dr["YouXiaoDate"] = "1900-1-1";
                                        }

                                        dr.ToDataRow(dt.Rows[0]);

                                        ConnDBServer.Update(sql2, null, dt);
                                    }
                                    catch (Exception ee)
                                    {
                                        ConnDBServer.RollbackTransaction();
                                        dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "false";
                                        dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = ee.ToString();
                                        return dsTurn.GetXml();
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    ConnDBServer.CommitTransaction();


                    //返回DataSet构造
                    dsTurn.Tables["ReturnInfo"].Rows[0]["status"] = "true";
                    dsTurn.Tables["ReturnInfo"].Rows[0]["reason"] = "数据上传成功！";


                }
                return dsTurn.GetXml();
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }

    }
}

