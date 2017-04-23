using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL.Sqlserver;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8;
using Bigdesk8.Data;
namespace Wxjzgcjczy.BLL
{
    public class XmxxBLL
    {
        XmxxDAL DAL;
        public AppUser WorkUser { get; set; }
        public XmxxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
             DAL=new XmxxDAL();
             DAL.DB = new DatabaseOperator();
        }

        #region 读取单条记录
        public FunctionResult<DataTable> Read_aj_gcjbxx(string pkId)
        {
            FunctionResult<DataTable> result = new FunctionResult<DataTable>();
            try
            {
                result.Result = DAL.Read_aj_gcjbxx(pkId);
            }
            catch (Exception ex)
            {
                result.Message = ex;
                result.Status = FunctionResultStatus.Error;
            }

            return result;
        }

        public FunctionResult<DataTable> Read_zj_gcjbxx(string pkId)
        {
            FunctionResult<DataTable> result = new FunctionResult<DataTable>();
            try
            {
                result.Result = DAL.Read_zj_gcjbxx(pkId);
            }
            catch (Exception ex)
            {
                result.Message = ex;
                result.Status = FunctionResultStatus.Error;
            }

            return result;
        }

        public FunctionResult<DataTable> Read_TBProjectBuilderUserInfo(string pkId)
        {
            FunctionResult<DataTable> result = new FunctionResult<DataTable>();
            try
            {
                result.Result = DAL.Read_TBProjectBuilderUserInfo(pkId);
            }
            catch (Exception ex)
            {
                result.Message = ex;
                result.Status = FunctionResultStatus.Error;
            }

            return result;
        }

        public FunctionResult<DataTable> Read_TBProjectInfo(string PrjNum)
        {
            FunctionResult<DataTable> result = new FunctionResult<DataTable>();
            try
            {
                result.Result = DAL.Read_TBProjectInfo(PrjNum);
            }
            catch (Exception ex)
            {
                result.Message = ex;
                result.Status = FunctionResultStatus.Error;
            }

            return result;
        }

        /// <summary>
        /// 获取质量报监责任单位及人员
        /// </summary>
        /// <param name="pkId"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> Read_zj_gcjbxx_zrdw(string pkId)
        {
            FunctionResult<DataTable> result = new FunctionResult<DataTable>();
            try
            {
                result.Result = DAL.Read_zj_gcjbxx_zrdw(pkId);
            }
            catch (Exception ex)
            {
                result.Message = ex;
                result.Status = FunctionResultStatus.Error;
            }

            return result;
        }

        #endregion 

        #region 读取列表
        /// <summary>
        /// 所属地区
        /// </summary>
        /// <returns></returns>
        public DataTable GettbXzqdmDic()
        {
            DataTable dt = DAL.GettbXzqdmDic();
            return dt;

        }
        public DataTable GetQySsdq()
        {
            DataTable dt = DAL.GetQySsdq();
            return dt;

        }


        public DataTable GetRysd()
        {
            DataTable dt = DAL.GetRysd();
            return dt;

        }



        public DataTable GetXmxx(string ssdq,string xmdjrq,string xmmc)
        {
            DataTable dt = DAL.GetXmxx(ssdq,xmdjrq,xmmc);
            return dt;

        }
        public DataTable GetXmxx(string PKID)
        {
            DataTable dt = DAL.GetXmxx(PKID);
            return dt;

        }

        public DataTable GetXmxx(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if(string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby=" CreateDate desc ";
            DataTable dt = DAL.GetXmxx(WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }

        /// <summary>
        /// 获取安监信息数据列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveAjxxList(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " CreateDate desc ";

            DataTable dt = DAL.RetrieveAjxxList( WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }
        /// <summary>
        /// 获取安监项目的人员信息列表
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveAjxx_RyxxList(string aqjdbm, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " UserName asc ";

            DataTable dt = DAL.RetrieveAjxx_RyxxList(aqjdbm, WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zljdbm"></param>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveZjxx_RyxxList(string zljdbm, FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " xh asc ";

            DataTable dt = DAL.RetrieveZjxx_RyxxList(zljdbm, WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }
        /// <summary>
        /// 获取质监信息数据列表
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveZjxxList(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " CreateDate desc ";

            DataTable dt = DAL.RetrieveZjxxList(WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }

        public DataTable RetrieveLxxmList(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            if (string.IsNullOrEmpty(orderby.ToString2().Trim()))
                orderby = " CreateDate desc ";

            DataTable dt = DAL.RetrieveLxxmList(WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            return dt;

        }


        #endregion 

        #region 保存
        public FunctionResult<string> Save_aj_gcjbxx(List<IDataItem> conditions) 
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.GetSchema_aj_gcjbxx();
                dt.Rows.Add(dt.NewRow());
                conditions.ToDataRow(dt.Rows[0]);
                dt.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dt.Rows[0]["UpdateFlag"] = "U";
                dt.Rows[0]["sbdqbm"] = "320200";
                dt.Rows[0]["PKID"] = Guid.NewGuid();
                dt.Rows[0]["JGID"] = WorkUser.qyID;

                if (DAL.Submit_aj_gcjbxx(dt))
                {
                    result.Result="信息保存成功！";
                    result.Status = FunctionResultStatus.None;
                }
                else
                {
                     result.Result="信息保存失败！";
                     result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        public FunctionResult<string> Save_zj_gcjbxx(List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.GetSchema_zj_gcjbxx();
                dt.Rows.Add(dt.NewRow());
                conditions.ToDataRow(dt.Rows[0]);
                dt.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dt.Rows[0]["UpdateFlag"] = "U";
                dt.Rows[0]["sbdqbm"] = "320200";
                dt.Rows[0]["PKID"] = Guid.NewGuid();
                dt.Rows[0]["JGID"] = WorkUser.qyID;

                if (DAL.Submit_zj_gcjbxx(dt))
                {
                    result.Result = "信息保存成功！";
                    result.Status = FunctionResultStatus.None;
                }
                else
                {
                    result.Result = "信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }


        public FunctionResult<string> Save_TBProjectBuilderUserInfo(string aqjdbm,List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt_zjxx = DAL.Read_zj_gcjbxxByZljdbm(aqjdbm);
                if (dt_zjxx.Rows.Count == 0)
                {
                    result.Result = "安全报监信息不存在！";
                    result.Status = FunctionResultStatus.Error;
                    return result;
                }

                DataTable dt = DAL.GetSchema_zj_gcjbxx();
                dt.Rows.Add(dt.NewRow());
                conditions.ToDataRow(dt.Rows[0]);

                dt.Rows[0]["PKID"] = Guid.NewGuid();
                dt.Rows[0]["aqjdbm"] = aqjdbm;
                dt.Rows[0]["PrjNum"] = dt_zjxx.Rows[0]["PrjNum"];
                dt.Rows[0]["UpdateFlag"] = "U";
                dt.Rows[0]["sbdqbm"] = "320200";

                if (DAL.Submit_TBProjectBuilderUserInfo(dt))
                {
                    result.Result = "信息保存成功！";
                    result.Status = FunctionResultStatus.None;
                }
                else
                {
                    result.Result = "信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }


        public FunctionResult<string> Save_zj_gcjbxx_zrdw(string zljdbm, List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt_ajxx = DAL.Read_zj_gcjbxxByZljdbm(zljdbm);
                if (dt_ajxx.Rows.Count == 0)
                {
                    result.Result = "质量报监信息不存在！";
                    result.Status = FunctionResultStatus.Error;
                    return result;
                }

                DataTable dt = DAL.GetSchema_zj_gcjbxx_zrdw();
                dt.Rows.Add(dt.NewRow());
                conditions.ToDataRow(dt.Rows[0]);

                dt.Rows[0]["PKID"] = Guid.NewGuid();
                dt.Rows[0]["zljdbm"] = zljdbm;

                if (DAL.Submit_zj_gcjbxx_zrdw(dt))
                {
                    result.Result = "信息保存成功！";
                    result.Status = FunctionResultStatus.None;
                }
                else
                {
                    result.Result = "信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }



        public bool SaveXmxx(DataTable dt)
        {
            return DAL.SubmitXmxx(dt);
        }


        #endregion  

        #region  更新

        public FunctionResult<string> Update_aj_gcjbxx(string pkId, List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_aj_gcjbxxWithRights(pkId,WorkUser);
                if (dt.Rows.Count > 0)
                {
                    conditions.ToDataRow(dt.Rows[0]);
                    dt.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dt.Rows[0]["sbdqbm"] = "320200";
                    if (DAL.Submit_aj_gcjbxx(dt))
                    {
                        result.Result = "信息保存成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息保存失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        public FunctionResult<string> Update_zj_gcjbxx(string pkId, List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_zj_gcjbxxWithRights(pkId,WorkUser);
                if (dt.Rows.Count > 0)
                {
                    conditions.ToDataRow(dt.Rows[0]);
                    dt.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dt.Rows[0]["sbdqbm"] = "320200";
                    if (DAL.Submit_zj_gcjbxx(dt))
                    {
                        result.Result = "信息保存成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息保存失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        public FunctionResult<string> Update_TBProjectBuilderUserInfo(string pkId, List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_TBProjectBuilderUserInfo(pkId);
                if (dt.Rows.Count > 0)
                {
                    conditions.ToDataRow(dt.Rows[0]);
                    dt.Rows[0]["sbdqbm"] = "320200";
                    if (DAL.Submit_TBProjectBuilderUserInfo(dt))
                    {
                        result.Result = "信息保存成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息保存失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }


        public FunctionResult<string> Update_zj_gcjbxx_zrdw(string pkId, List<IDataItem> conditions)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_zj_gcjbxx_zrdw(pkId);
                if (dt.Rows.Count > 0)
                {
                    conditions.ToDataRow(dt.Rows[0]);
                    if (DAL.Submit_zj_gcjbxx_zrdw(dt))
                    {
                        result.Result = "信息保存成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息保存失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息保存失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        #endregion 


        #region 删除
        public FunctionResult<string> Delete_aj_gcjbxx(string pkId)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_aj_gcjbxx(pkId);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["UpdateFlag"] = "D";
                    if (DAL.Submit_aj_gcjbxx(dt))
                    {
                        result.Result = "信息删除成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息删除失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息删除失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        public FunctionResult<string> Delete_zj_gcjbxx(string pkId)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_zj_gcjbxx(pkId);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["UpdateFlag"] = "D";
                    if (DAL.Submit_zj_gcjbxx(dt))
                    {
                        result.Result = "信息删除成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息删除失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息删除失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }

        public FunctionResult<string> Delete_TBProjectBuilderUserInfo(string pkId)
        {
            FunctionResult<string> result = new FunctionResult<string>();
            try
            {
                DataTable dt = DAL.Read_TBProjectBuilderUserInfo(pkId);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["UpdateFlag"] = "D";
                    if (DAL.Submit_aj_gcjbxx(dt))
                    {
                        result.Result = "信息删除成功！";
                        result.Status = FunctionResultStatus.None;
                    }
                    else
                    {
                        result.Result = "信息删除失败！";
                        result.Status = FunctionResultStatus.Error;
                    }
                }
                else
                {
                    result.Result = "不存在该记录，信息删除失败！";
                    result.Status = FunctionResultStatus.Error;
                }
            }
            catch (Exception ex)
            {
                result.Result = ex.Message;
                result.Status = FunctionResultStatus.Error;
            }
            return result;
        }


        #endregion 

    }
}
