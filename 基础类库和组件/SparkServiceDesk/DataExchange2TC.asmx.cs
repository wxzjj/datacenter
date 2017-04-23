using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Bigdesk8.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using Bigdesk8;

namespace SparkServiceDesk
{

    /****************************************************
     * 
     * Copyright(C)2015 南京群耀软件系统有限公司 版权所有
     * 
     * 描    述：1、常熟审图中心需读取立项信息 2、审图中心需将数据通过接口推送过来 3、常熟本地数据也需整理后推送过来
     *
     * 作    者：吴奎
     * 
     * 创建日期：2015/09/08
     * 
     * 修改历史：
     ***************************************************/
    public class DataExchange2TC : System.Web.Services.WebService
    {
        public DBOperator DB;

        //************************************     
        // 函数名称: DBServerOpen     
        // 函数说明：打开数据库连接
        // 作    者：吴奎    
        // 作成日期：2015/09/08
        // 返 回 值: void     
        //************************************
        private void DBServerOpen()
        {
            DB = new DatabaseOperator();
        }

        private class DatabaseOperator : SqlServerDbOperator
        {
            public DatabaseOperator()
                : base(GetConnectionString(), (DataBaseType)Enum.Parse(typeof(DataBaseType), GetDatabaseType().ToUpper()))
            {
            }
        }

        /// <summary >
        /// 数据库类型，如sqlserver2000，sqlserver2005。
        /// </summary>
        private static string GetDatabaseType()
        {
            return ConfigurationManager.ConnectionStrings["SCIC82ConnectionString"].ProviderName;
        }

        /// <summary>
        /// 数据库连接字符串，如data source=.;user id=sa;password=1;database=SCIC82。
        /// </summary>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SCIC82ConnectionString"].ConnectionString;
        }

        //************************************     
        // 函数名称: ReadLxxm     
        // 函数说明：获取立项信息
        // 作    者：吴奎    
        // 作成日期：2015/09/08
        // 返 回 值: string     
        // 参    数: string username 
        // 参    数: string password     
        // 参    数: string beginDate     
        // 参    数: string endDate     
        //************************************
        [WebMethod]
        public string ReadLxxm(string username, string password, string beginDate, string endDate)
        {
            DBServerOpen();

            try
            {
                //用户名不正确
                if (username.Length != 6 || username.IndexOf("3205") < 0) return "用户名不正确或格式错误，格式参照如下，如常熟为320581，张家港320582，昆山320583，吴江320584，太仓320585";
                if (password.ToLower() != "3388930803df83cad63f04a38fc32f60") return "001：密码错误";
                DateTime dtbegintime, dtendtime;
                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;
                if (!string.IsNullOrEmpty(beginDate))
                {
                    if (!DateTime.TryParse(beginDate, out dtbegintime))
                    {
                        return "003：起时间格式错误！";
                    }
                    else
                    {
                        item = new DataItem();
                        item.ItemName = "shrqsj";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThan;
                        item.ItemType = DataType.String;
                        item.ItemData = dtbegintime.AddDays(-1).ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }
                if (!DateTime.TryParse(endDate, out dtendtime))
                {
                    return "003：止时间格式错误！";
                }
                else
                {
                    item = new DataItem();
                    item.ItemName = "shrqsj";
                    item.ItemData = dtendtime.AddDays(1).ToString("yyyy-MM-dd");
                    item.ItemType = DataType.String;
                    item.ItemRelation = Bigdesk8.Data.DataRelation.LessThan;
                    list.Add(item);
                }
                SqlParameterCollection sp = DB.CreateSqlParameterCollection();
                sp.Add("@city", username);
                string sql = @"select * from (select  a.*,b.shrqsj from TBProjectInfo a left join uepp_lxxm b on a.PrjInnerNum=b.lxxmid and b.datastate=99 ) as x where 1=1";
                list.GetSearchClause(sp, ref sql);

                DataSet ds = DB.ExeSqlForDataSet(sql, sp, "ds");
                if (ds.Tables[0].Rows.Count <= 0) return "未找到项目！";
                return ds.GetXml();
            }
            catch (Exception eee)
            {
                return "获取立项项目报错，请联系南京群耀软件系统有限公司！具体报错为：" + eee.Message;
            }
        }


        [WebMethod]
        //************************************     
        // 函数名称: SaveTBDataToSz     
        // 函数说明：将数据按照省12张表结构推送过来
        // 作    者：吴奎    
        // 作成日期：2015/09/08
        // 返 回 值: string     
        // 参    数: string tableName     
        // 参    数: string xmlData     
        // 参    数: string user     
        // 参    数: string password     
        //************************************
        public string SaveTBData(string tableName, string xmlData, string user, string password)
        {
            DBServerOpen();
            //用户名不正确
            if (user.Length != 6 || user.IndexOf("3205") < 0) return "用户名不正确或格式错误，格式参照如下，如常熟为320581，张家港320582，昆山320583，吴江320584，太仓320585";
            //密码错误
            if (!password.Equals("3388930803df83cad63f04a38fc32f60")) return "001，密码错误";
            //表名不正确
            if (!tableName.ToLower().Equals("tbprojectinfo") && !tableName.ToLower().Equals("xm_gcdjb_dtxm") && !tableName.ToLower().Equals("tbtenderinfo") &&
                !tableName.ToLower().Equals("tbcontractrecordmanage") && !tableName.ToLower().Equals("tbprojectcensorinfo") && !tableName.ToLower().Equals("tbprojectdesigneconuserinfo") &&
                !tableName.ToLower().Equals("tbbuilderlicencemanage") && !tableName.ToLower().Equals("tbprojectbuilderuserinfo") && !tableName.ToLower().Equals("tbprojectfinishmanage") &&
                !tableName.ToLower().Equals("aj_gcjbxx") && !tableName.ToLower().Equals("zj_gcjbxx") && !tableName.ToLower().Equals("zj_gcjbxx_zrdw"))
                return "002，数据表名不正确";
            StringReader sr = new StringReader(xmlData);
            TextReader tr = sr;
            DataSet dt = new DataSet();
            try
            {
                dt.ReadXml(tr);
            }
            catch
            {
                return "表：" + tableName + "，您提供的xml格式有问题，解析报错！";
            }

            if (dt.Tables.Count <= 0) return "表：" + tableName + "，此表没有数据！";
            if (dt.Tables[0].Rows.Count <= 0) return "表：" + tableName + "，此表没有数据！";

            string checkResult = "";

            foreach (DataRow dr in dt.Tables[0].Rows)
            {

                //执行增删改操作
                if (dr.Table.Columns.Contains("UpdateFlag"))
                {
                    //删除
                    if (dr["UpdateFlag"].ToString().ToUpper() == "D")
                    {
                        //删除操作，需要先校验本地库是否有此记录
                        checkResult = CheckSfHaveXm(dr, tableName, user);
                        if (!string.IsNullOrEmpty(checkResult))
                        {
                            //未找到项目
                            return checkResult;
                        }
                        else
                        {
                            //执行删除操作
                            checkResult = Delete(dr, tableName, user);
                            if (!string.IsNullOrEmpty(checkResult))
                            {
                                return checkResult;
                            }
                        }

                    }
                    //新增或修改
                    else if (dr["UpdateFlag"].ToString().ToUpper() == "U")
                    {
                        checkResult = Update(dr, tableName, user);
                        if (!string.IsNullOrEmpty(checkResult))
                        {
                            return checkResult;
                        }
                    }
                    else
                    {
                        return "表：" + tableName + "，UpdateFlag只能是D或U";
                    }
                }
                else
                {
                    //没有此列就,暂时zj_gcjbxx_zrdw表没有此字段
                    //目前zj_gcjbxx_zrdw 质量报监责任单位及人员此表没此字段
                    //暂时只新增或更新，不删除
                    checkResult = Update(dr, tableName, user);
                    if (!string.IsNullOrEmpty(checkResult))
                    {
                        return checkResult;
                    }
                }
            }

            if (checkResult != "")
            {
                return checkResult;
            }

            return "OK";

        }

        /// <summary>
        /// 新增和修改操作
        /// </summary>
        private string Update(DataRow dr, string tableName, string sbdqcode)
        {
            string nbsb = returnOnlySbCode(tableName);//内部识别
            string value = "";
            string result = "";

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@sbdqcode", sbdqcode);

            string sql = "";
            if (nbsb != "不存在唯一识别字段")
            {
                if (dr.Table.Columns.Contains(nbsb))
                {

                    value = dr[nbsb].ToString();
                }
                else
                {
                    return "表：" + tableName + ",未找到关键字段：" + nbsb;
                }

                sp.Add("@value", value);
                sql = "select * from " + tableName + " where " + nbsb + "=@value and sbdqbm=@sbdqcode and datastate<>-1";
            }
            else
            {
                return "接口代码检测问题，请联系南京群耀！";
            }

            DataTable dt = DB.ExeSqlForDataTable(sql, sp, "dt");
            if (dt.Rows.Count <= 0)
            {
                try
                {
                    //新增
                    string sqlSchema = "select * from " + tableName + " where 1=2";
                    DataTable dtSchema = DB.ExeSqlForDataTable(sqlSchema, null, "dt");
                    dtSchema.Rows.Add(dtSchema.NewRow());
                    //常熟太仓数据要生成部里的标准编码
                    string slbmzd = returnBlibiaozhun(tableName, sbdqcode);
                    if (slbmzd != "不存在唯一识别字段")
                    {
                        string slbmvalue = ReturnSlbmValue(tableName, dr);
                        if (slbmvalue != "没找到匹配的类型")
                        {
                            dr[slbmzd] = slbmvalue;
                        }
                    }

                    result = CheckSjWt(dtSchema, dr, tableName, "Add");
                    if (!string.IsNullOrEmpty(result)) return result;

                    dr.ToDataRow(dtSchema.Rows[0]);
                    dtSchema.Rows[0]["cjrqsj"] = DateTime.Now.ToString2();
                    dtSchema.Rows[0]["PKID"] = Guid.NewGuid().ToString2();
                    dtSchema.Rows[0]["sbdqbm"] = sbdqcode;

                    if (tableName.ToLower() == "tbprojectbuilderuserinfo")
                    {
                        //施工许可人员明细需要获取施工许可编号
                        string BuilderLicenceNum = ReturnSgxkbh(dr["InnerNum"].ToString2());
                        if (string.IsNullOrEmpty(BuilderLicenceNum)) return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",未在施工许可项目表中找到此项目";
                        dtSchema.Rows[0]["BuilderLicenceNum"] =BuilderLicenceNum;
                    }
                    else if (tableName.ToLower() == "tbprojectfinishmanage")
                    {
                        //竣工备案需找到此项目的施工许可编码
                         string BuilderLicenceNum =  ReturnSgxkbhByZJ(dr["PrjFinishInnerNum"].ToString2());
                         if (string.IsNullOrEmpty(BuilderLicenceNum)) return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",未在施工许可项目表中找到此项目";
                         dtSchema.Rows[0]["BuilderLicenceNum"] = BuilderLicenceNum;
                    }
                    else if (tableName.ToLower() == "zj_gcjbxx_zrdw")
                    { 
                        //质监顺序号要生产
                        int xh = ReturnZjxh(dr["zljdbm"].ToString2());
                        dtSchema.Rows[0]["xh"] = xh;
                    }

                    DB.Update(sqlSchema, null, dtSchema);
                }
                catch (Exception e)
                {
                    return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",新增此记录失败：具体原因为：" + e.Message;
                }
            }
            else
            {
                try
                {

                    result = CheckSjWt(dt, dr, tableName, "Update");
                    if (!string.IsNullOrEmpty(result)) return result;
                    //修改
                    //由于本地库是不保存部里编码的字段，所有需要去掉这些字段
                    string a = returnBlibiaozhun(tableName, sbdqcode);
                    if (a != "不存在唯一识别字段")
                    {
                        dr.Table.Columns.Remove(a);
                    }
                    if (tableName.ToLower() == "tbprojectfinishmanage" && dr.Table.Columns.Contains("BuilderLicenceNum"))
                    {
                        //竣工备案需清掉此字段
                        dr.Table.Columns.Remove("BuilderLicenceNum");
                    }
                    if (tableName.ToLower() == "zj_gcjbxx_zrdw" && dr.Table.Columns.Contains("xh"))
                    {
                        //竣工备案需清掉此字段
                        dr.Table.Columns.Remove("xh");
                    }

                    dr.ToDataRow(dt.Rows[0]);
                    dt.Rows[0]["xgrqsj"] = DateTime.Now.ToString2();
                    dt.Rows[0]["datastate"] = State.等待上传.ToInt32();
                    DB.Update(sql, sp, dt);
                }
                catch (Exception e)
                {
                    return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",更新此记录失败：具体原因为：" + e.Message;
                }
            }

            return "";
        }

        private int ReturnZjxh(string zljdbm)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@id",zljdbm);
            string sql = "select max(xh) from zj_gcjbxx_zrdw where zljdbm=@id and datastate<>-1";
            return DB.ExeSqlForObject(sql,sp).ToInt32(0)+1;
        }

        //************************************     
        // 函数名称: ReturnSgxkbhByZJ     
        // 函数说明：质监也要施工许可编号
        // 作    者：吴奎    
        // 作成日期：2015/04/22     
        // 返 回 值: string     
        // 参    数: string p     
        //************************************
        private string ReturnSgxkbhByZJ(string JgbaBusinessID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@id",JgbaBusinessID);
            string sql = @"select d.BuilderLicenceNum from ZLJD_Jgba a inner join ZLJD_Tyxm_Zjxm b on a.xmid=b.zjxmid and b.relState<>-1
                         inner join sole_sgxk c on b.tyxmid=c.tyxmid and c.sjzt<>-1
                        inner join TBBuilderLicenceManage d on c.businessid=d.BuilderLicenceInnerNum and d.datastate<>-1
                        where a.businessid=@id";
            return DB.ExeSqlForString(sql, sp);
        }

        //************************************     
        // 函数名称: ReturnSgxkbh     
        // 函数说明：返回施工许可表的部里编码
        // 作    者：吴奎    
        // 作成日期：2015/04/20     
        // 返 回 值: string     
        // 参    数: string businessid     
        //************************************
        private string ReturnSgxkbh(string businessid)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@id", businessid);
            string sql = "select BuilderLicenceNum	 from TBBuilderLicenceManage where BuilderLicenceInnerNum=@id and datastate<>-1";
            return DB.ExeSqlForString(sql, sp);
        }

        //************************************     
        // 函数名称: ReturnSlbmValue     
        // 函数说明：通过表名和类型获取最新编码
        // 作    者：吴奎    
        // 作成日期：2015/04/20     
        // 返 回 值: string     
        // 参    数: string tablename     
        // 参    数: DataRow dr     
        //************************************
        private string ReturnSlbmValue(string tablename, DataRow dr)
        {
            string modulecode = "";
            switch (tablename.ToLower())
            {
                case "xm_gcdjb_dtxm"://单项工程基本信息
                    modulecode = "";
                    break;
                case "tbtenderinfo"://招投标信息
                    {
                        if (dr["TenderClassNum"].ToString2() == "003")
                        {
                            modulecode = "BD";//施工
                        }
                        else if (dr["TenderClassNum"].ToString2() == "004")
                        {
                            modulecode = "BE";//监理
                        }
                        else
                        {
                            return "没找到匹配的类型";
                        }
                    }
                    break;
                case "tbbuilderlicencemanage"://施工许可信息
                    modulecode = "SX";
                    break;
                case "tbprojectfinishmanage"://竣工验收备案信息
                    modulecode = "JX";
                    break;
                case "tbcontractrecordmanage"://合同备案信息
                    {
                        if (dr["ContractTypeNum"].ToString2() == "301")
                        {
                            modulecode = "HZ";//施工总包
                        }
                        else if (dr["ContractTypeNum"].ToString2() == "302")
                        {
                            modulecode = "HF";//施工分包
                        }
                        else if (dr["ContractTypeNum"].ToString2() == "303")
                        {
                            modulecode = "HL";//施工劳务
                        }
                        else if (dr["ContractTypeNum"].ToString2() == "400")
                        {
                            modulecode = "HE";//监理
                        }
                        else
                        {
                            return "没找到匹配的类型";
                        }
                    }
                    break;
                default:
                    return "没找到匹配的类型";
            }

            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@lxbh", dr["PrjNum"].ToString2());
            sp.Add("@hj", modulecode);
            string sql = "select dbo.Function_GET_STBM(@lxbh,@hj)";
            return DB.ExeSqlForString(sql, sp);

        }

        /// <summary>
        /// 数据校验
        /// </summary>
        private string CheckSjWt(DataTable dtData, DataRow drFrom, string tablename, string type)
        {
            string checkResult = "";
            foreach (DataColumn dcData in dtData.Columns)
            {
                if (dcData.ColumnName.ToLower() == "pkid" || dcData.ColumnName.ToLower() == "sbdqbm"
                    || dcData.ColumnName.ToLower() == "createdate" || dcData.ColumnName.ToLower() == "UpdateFlag") continue;
                foreach (DataColumn dcfrom in drFrom.Table.Columns)
                {
                    if (dcfrom.ColumnName.ToLower() == "pkid" || dcfrom.ColumnName.ToLower() == "sbdqbm"
                     || dcfrom.ColumnName.ToLower() == "createdate" || dcfrom.ColumnName.ToLower() == "UpdateFlag") continue;

                    if (dcData.ColumnName.ToLower() == dcfrom.ColumnName.ToLower())
                    {
                        if (type == "Add")
                        {
                            //非空
                            if (CheckIsAllowNotNull_Add(tablename.ToLower(), dcfrom.ColumnName.ToLower()) && string.IsNullOrEmpty(drFrom[dcfrom].ToString2()))
                            {
                                if (checkResult.IndexOf("表：" + tablename + "中字段" + dcData.ColumnName + "不能为空") < 0)
                                {
                                    checkResult = checkResult + "表：" + tablename + "中字段" + dcData.ColumnName + "不能为空！ ";
                                }
                            }
                            else
                            {
                                //其他校验
                            }
                        }
                        else
                        {
                            //编辑
                            //非空
                            if (CheckIsAllowNotNull_Update(tablename.ToLower(), dcfrom.ColumnName.ToLower()) && string.IsNullOrEmpty(drFrom[dcfrom].ToString2()))
                            {
                                if (checkResult.IndexOf("表：" + tablename + "中字段" + dcData.ColumnName + "不能为空") < 0)
                                {
                                    checkResult = checkResult + "表：" + tablename + "中字段" + dcData.ColumnName + "不能为空！ ";
                                }
                            }
                            else
                            {
                                //其他校验
                            }
                        }
                    }
                }
            }
            return checkResult.TrimEnd('|');
        }

        /// <summary>
        /// 校验一个表某字段是否是必填的，更新，更新对于部里的编码不用管
        /// </summary>
        private bool CheckIsAllowNotNull_Update(string tablename, string columnName)
        {
            string[] trueString = new string[100];

            //项目基本信息
            string[] tbprojectinfo = { "PrjNum", "PrjInnerNum", "PrjName", "PrjTypeNum", "BuildCorpName", "BuildCorpCode", "ProvinceNum", "CityNum", "CountyNum", "PrjPropertyNum", "PrjFunctionNum" };
            //单项工程基本信息
            string[] xm_gcdjb_dtxm = { "PrjNum", "fxnbbm", "xmmc" };
            //招投标信息
            string[] tbtenderinfo = { "TenderName", "TenderInnerNum", "PrjNum", "TenderClassNum", "TenderTypeNum", "TenderResultDate", "TenderMoney", "Area", "TenderCorpName", "TenderCorpCode", "shypbf" };
            //合同备案信息
            string[] TBContractRecordManage = { "RecordName", "RecordInnerNum", "PrjNum", "ContractNum", "ContractTypeNum", "ContractMoney", "ContractDate", "PropietorCorpName", "PropietorCorpCode", "ContractorCorpName", "ContractorCorpCode", "PrjHead" };
            //施工图审查信息
            string[] TBProjectCensorInfo = { "CensorNum", "CensorInnerNum", "PrjNum", "CensorCorpName", "CensorCorpCode", "CensorEDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode" };
            //勘察设计审图人员明细表
            string[] TBProjectDesignEconUserInfo = { "CensorNum", "PrjNum", "CorpName", "CorpCode", "UserName", "IDCardTypeNum", "IDCard", "SpecialtyTypNum", "PrjDuty" };
            //施工许可信息
            string[] TBBuilderLicenceManage = { "BuilderLicenceName", "BuilderLicenceInnerNum", "PrjNum", "Area", "IssueCertDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode", 
           "ConsCorpName", "ConsCorpCode", "SuperCorpName", "SuperCorpCode", "ConstructorName", "CIDCardTypeNum", "ConstructorIDCard", "SupervisionName", "SIDCardTypeNum", "SupervisionIDCard" };
            //施工安全从业人员明细表
            string[] TBProjectBuilderUserInfo = { "PrjNum", "CorpName", "UserName", "IDCardTypeNum", "IDCard", "CertID", "UserType" };
            //竣工验收备案信息
            string[] TBProjectFinishManage = { "PrjNum", "PrjFinishName",  "PrjFinishInnerNum", "FactCost", "FactArea", "BDate", "EDate" };
            //安全报监信息
            string[] aj_gcjbxx = { "PrjNum", "aqjdbm", "gcmc", "Aqjdjgmc", "gcgk_jzmj", "bjrq", "gcgk_kgrq", "gcgk_jhjgrq", "zbdw_dwdm", "zbdw_dwmc", "zbdw_aqxkzh", "zbdw_zcjzs", "zbdw_zcjzsdm", "zbdw_zcjzs", "zbdw_zcjzsdm", "zbdw_aqy", "zbdw_aqyzh" };
            //质量报监信息
            string[] zj_gcjbxx = { "PrjNum", "gcmc", "zljdbm", "sbrq", "kgrq", "jhjgrq" };
            //质量报监责任单位及人员
            string[] zj_gcjbxx_zrdw = { "zljdbm", "dwlx", "Xh", "Dwmc", "dwdm", "xmfzrxm", "xmfzrdm" };

            switch (tablename)
            {
                case "tbprojectinfo":
                    trueString = tbprojectinfo;
                    break;
                case "xm_gcdjb_dtxm":
                    trueString = xm_gcdjb_dtxm;
                    break;
                case "tbtenderinfo":
                    trueString = tbtenderinfo;
                    break;
                case "tbcontractrecordmanage":
                    trueString = TBContractRecordManage;
                    break;
                case "tbprojectcensorinfo":
                    trueString = TBProjectCensorInfo;
                    break;
                case "tbprojectdesigneconuserinfo":
                    trueString = TBProjectDesignEconUserInfo;
                    break;
                case "tbbuilderlicencemanage":
                    trueString = TBBuilderLicenceManage;
                    break;
                case "tbprojectbuilderuserinfo":
                    trueString = TBProjectBuilderUserInfo;
                    break;
                case "tbprojectfinishmanage":
                    trueString = TBProjectFinishManage;
                    break;
                case "aj_gcjbxx":
                    trueString = aj_gcjbxx;
                    break;
                case "zj_gcjbxx":
                    trueString = zj_gcjbxx;
                    break;
                case "zj_gcjbxx_zrdw":
                    trueString = zj_gcjbxx_zrdw;
                    break;
                default:
                    break;

            }

            foreach (string s in trueString)
            {
                if (s.ToLower() == columnName)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 校验一个表某字段是否是必填的，新增
        /// </summary>
        private bool CheckIsAllowNotNull_Add(string tablename, string columnName)
        {
            string[] trueString = new string[100];

            //项目基本信息
            string[] tbprojectinfo = { "PrjNum", "PrjInnerNum", "PrjName", "PrjTypeNum", "BuildCorpName", "BuildCorpCode", "ProvinceNum", "CityNum", "CountyNum", "PrjPropertyNum", "PrjFunctionNum" };
            //单项工程基本信息
            string[] xm_gcdjb_dtxm = { "PrjNum", "fxnbbm", "xmmc", "fxbm" };
            //招投标信息
            string[] tbtenderinfo = { "TenderName", "TenderInnerNum", "TenderNum", "PrjNum", "TenderClassNum", "TenderTypeNum", "TenderResultDate", "TenderMoney", "Area", "TenderCorpName", "TenderCorpCode", "shypbf" };
            //合同备案信息
            string[] TBContractRecordManage = { "RecordName", "RecordInnerNum", "RecordNum", "PrjNum", "ContractNum", "ContractTypeNum", "ContractMoney", "ContractDate", "PropietorCorpName", "PropietorCorpCode", "ContractorCorpName", "ContractorCorpCode", "PrjHead" };
            //施工图审查信息
            string[] TBProjectCensorInfo = { "CensorNum", "CensorInnerNum", "PrjNum", "CensorCorpName", "CensorCorpCode", "CensorEDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode" };
            //勘察设计审图人员明细表
            string[] TBProjectDesignEconUserInfo = { "CensorNum", "PrjNum", "CorpName", "CorpCode", "UserName", "IDCardTypeNum", "IDCard", "SpecialtyTypNum", "PrjDuty" };
            //施工许可信息
            string[] TBBuilderLicenceManage = { "BuilderLicenceName", "BuilderLicenceInnerNum","PrjNum", "Area", "IssueCertDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode", 
           "ConsCorpName", "ConsCorpCode", "SuperCorpName", "SuperCorpCode", "ConstructorName", "CIDCardTypeNum", "ConstructorIDCard", "SupervisionName", "SIDCardTypeNum", "SupervisionIDCard" };
            //施工安全从业人员明细表
            string[] TBProjectBuilderUserInfo = { "PrjNum", "CorpName", "UserName", "IDCardTypeNum", "IDCard", "CertID", "UserType" };
            //竣工验收备案信息
            string[] TBProjectFinishManage = { "PrjNum", "PrjFinishName", "PrjFinishNum", "PrjFinishInnerNum", "FactCost", "FactArea", "BDate", "EDate" };
            //安全报监信息
            string[] aj_gcjbxx = { "PrjNum", "aqjdbm", "gcmc", "Aqjdjgmc", "gcgk_jzmj", "bjrq", "gcgk_kgrq", "gcgk_jhjgrq", "zbdw_dwdm", "zbdw_dwmc", "zbdw_aqxkzh", "zbdw_zcjzs", "zbdw_zcjzsdm", "zbdw_zcjzs", "zbdw_zcjzsdm", "zbdw_aqy", "zbdw_aqyzh" };
            //质量报监信息
            string[] zj_gcjbxx = { "PrjNum", "gcmc", "zljdbm", "sbrq", "kgrq", "jhjgrq" };
            //质量报监责任单位及人员
            string[] zj_gcjbxx_zrdw = { "zljdbm", "dwlx", "Dwmc", "dwdm", "xmfzrxm", "xmfzrdm" };

            switch (tablename)
            {
                case "tbprojectinfo":
                    trueString = tbprojectinfo;
                    break;
                case "xm_gcdjb_dtxm":
                    trueString = xm_gcdjb_dtxm;
                    break;
                case "tbtenderinfo":
                    trueString = tbtenderinfo;
                    break;
                case "tbcontractrecordmanage":
                    trueString = TBContractRecordManage;
                    break;
                case "tbprojectcensorinfo":
                    trueString = TBProjectCensorInfo;
                    break;
                case "tbprojectdesigneconuserinfo":
                    trueString = TBProjectDesignEconUserInfo;
                    break;
                case "tbbuilderlicencemanage":
                    trueString = TBBuilderLicenceManage;
                    break;
                case "tbprojectbuilderuserinfo":
                    trueString = TBProjectBuilderUserInfo;
                    break;
                case "tbprojectfinishmanage":
                    trueString = TBProjectFinishManage;
                    break;
                case "aj_gcjbxx":
                    trueString = aj_gcjbxx;
                    break;
                case "zj_gcjbxx":
                    trueString = zj_gcjbxx;
                    break;
                case "zj_gcjbxx_zrdw":
                    trueString = zj_gcjbxx_zrdw;
                    break;
                default:
                    break;

            }

            foreach (string s in trueString)
            {
                if (s.ToLower() == columnName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        private string Delete(DataRow dr, string tableName, string sbdqcode)
        {
            string nbsb = returnOnlySbCode(tableName);//内部识别
            string value = "";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "";
            if (nbsb != "不存在唯一识别字段")
            {
                if (dr.Table.Columns.Contains(nbsb))
                {
                    value = dr[nbsb].ToString();
                }
                else
                {
                    return "表：" + tableName + ",未找到关键字段：" + nbsb;
                }
                sp.Add("@value", value);
                sp.Add("@sbdqcode", sbdqcode);

                sql = "update " + tableName + " set datastate=-1,UpdateFlag='D',xgrqsj=getdate() where " + nbsb + "=@value and sbdqbm=@sbdqcode and datastate<>-1";

            }

            else
            {
                return "接口代码检测问题，请联系南京群耀！";
            }

            try
            {

                DB.ExecuteNonQuerySql(sql, sp);
            }
            catch (Exception eDelete)
            {
                return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",删除此记录失败：具体原因为：" + eDelete.Message;

            }
            return "";
        }

        /// <summary>   
        /// 如果删除表 需要先在库中查看此记录是否存在，不存在需要提示
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string CheckSfHaveXm(DataRow dr, string tableName, string sbdqcode)
        {

            string nbsb = returnOnlySbCode(tableName);//内部识别
            string value = "";

            if (nbsb != "不存在唯一识别字段")
            {
                if (dr.Table.Columns.Contains(nbsb))
                {
                    value = dr[nbsb].ToString();
                }
                else
                {
                    return "表：" + tableName + ",为找到关键字段：" + nbsb;
                }
                SqlParameterCollection sp = DB.CreateSqlParameterCollection();
                sp.Add("@value", value);
                sp.Add("@sbdqcode", sbdqcode);

                string sql = "select * from " + tableName + " where " + nbsb + "=@value and sbdqbm=@sbdqcode and datastate<>-1";
                try
                {
                    if (DB.ExeSqlForObject(sql, sp).ToInt32() <= 0)
                    {
                        return "005，表：" + tableName + "，唯一识别字段" + nbsb + "，值为" + value + "，未能在库中找到此记录，不能执行删除操作！";
                    }
                }
                catch (Exception e)
                {
                    return "表：" + tableName + ",关键字段为" + nbsb + ",值为：" + value + ",删除此记录失败：具体原因为：" + e.Message;
                }
            }
            return "";

        }


        /// <summary>
        /// 返回某个表唯一能识别的关键字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string returnOnlySbCode(string tableName)
        {
            string nbsb = "";
            switch (tableName.ToLower())
            {
                case "tbprojectinfo"://项目基本信息
                    nbsb = "PrjInnerNum";
                    break;
                case "xm_gcdjb_dtxm"://单项工程基本信息
                    nbsb = "fxnbbm";
                    break;
                case "tbtenderinfo"://招投标信息
                    nbsb = "TenderInnerNum";
                    break;
                case "tbprojectcensorinfo"://施工图审查信息
                    nbsb = "CensorInnerNum";
                    break;
                case "tbbuilderlicencemanage"://施工许可信息
                    nbsb = "BuilderLicenceInnerNum";
                    break;
                case "tbprojectfinishmanage"://竣工验收备案信息
                    nbsb = "PrjFinishInnerNum";
                    break;
                case "aj_gcjbxx"://安监
                    nbsb = "aqjdbm";
                    break;
                case "zj_gcjbxx"://质监
                    nbsb = "zljdbm";
                    break;
                case "zj_gcjbxx_zrdw"://质量报监责任单位及人员
                    nbsb = "InnerNum";
                    break;
                case "tbprojectbuilderuserinfo"://施工许可人员
                    nbsb = "InnerNum";
                    break;
                case "tbprojectdesigneconuserinfo"://勘察设计审图人员明细表
                    nbsb = "InnerNum";
                    break;
                case "tbcontractrecordmanage"://合同备案信息
                    nbsb = "RecordInnerNum";
                    break;
                default:
                    return "不存在唯一识别字段";
            }
            return nbsb;
        }

        /// <summary>
        /// 返回某个表中部里的编码字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string returnBlibiaozhun(string tableName, string sbdqcode)
        {
            string nbsb = "";
            if (sbdqcode == "320581")//常熟
            {
                switch (tableName.ToLower())
                {
                    case "xm_gcdjb_dtxm"://单项工程基本信息
                        nbsb = "fxbm";
                        break;
                    case "tbtenderinfo"://招投标信息
                        nbsb = "TenderNum";
                        break;
                    case "tbbuilderlicencemanage"://施工许可信息
                        nbsb = "BuilderLicenceNum";
                        break;
                    case "tbprojectfinishmanage"://竣工验收备案信息
                        nbsb = "PrjFinishNum";
                        break;
                    case "tbcontractrecordmanage"://合同备案信息
                        nbsb = "RecordNum";
                        break;
                    case "tbprojectbuilderuserinfo"://施工安全从业人员明细表
                        nbsb = "BuilderLicenceNum";
                        break;
                    case "aj_gcjbxx"://安监
                    case "zj_gcjbxx"://质监
                    case "zj_gcjbxx_zrdw"://质量报监责任单位及人员
                    case "TBProjectDesignEconUserInfo"://勘察设计审图人员明细表
                    case "tbprojectinfo"://项目基本信息
                    case "tbprojectcensorinfo"://施工图审查信息
                        nbsb = "不存在唯一识别字段";
                        break;
                    default:
                        return "不存在唯一识别字段";
                }
            }
            else if (sbdqcode == "320585")//太仓
            {
                switch (tableName.ToLower())
                {
                    case "xm_gcdjb_dtxm"://单项工程基本信息
                        nbsb = "fxbm";
                        break;
                    case "tbtenderinfo"://招投标信息
                        nbsb = "TenderNum";
                        break;
                    case "tbcontractrecordmanage"://合同备案信息
                        nbsb = "RecordNum";
                        break;
                    case "aj_gcjbxx"://安监
                    case "zj_gcjbxx"://质监
                    case "zj_gcjbxx_zrdw"://质量报监责任单位及人员
                    case "tbprojectbuilderuserinfo"://施工安全从业人员明细表
                    case "TBProjectDesignEconUserInfo"://勘察设计审图人员明细表
                    case "tbprojectcensorinfo"://施工图审查信息
                    case "tbbuilderlicencemanage"://施工许可信息
                    case "tbprojectfinishmanage"://竣工验收备案信息
                    case "tbprojectinfo"://项目基本信息
                        nbsb = "不存在唯一识别字段";
                        break;
                    default:
                        return "不存在唯一识别字段";
                }

            }
            else
            {
                //其他地区不是我单位的
                return "不存在唯一识别字段";
            }

            return nbsb;
        }



    }

    public enum State
    {
        等待上传 = 0,
        成功上传 = 999,
        上传失败 = -999

    }
}
