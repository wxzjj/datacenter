using System;
using System.Collections.Generic;
using System.Text;
using WxjzgcjczyQyb.DAL;
using Bigdesk8;
using System.Data;
using Bigdesk8.Data;

namespace WxjzgcjczyQyb.BLL
{
    public class GcxmBLL
    {
        private GcxmDAL gcxmDal;
        private AppUser WorkUser;
        public GcxmBLL(AppUser workUser)
        {
            WorkUser = workUser;
            gcxmDal = new GcxmDAL();
            gcxmDal.DB = new DatabaseOperator();
        }

        #region 项目信息
        /// <summary>
        /// 项目信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="zzjgdm"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetXmxx(List<IDataItem> condition, string ssdq, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetXmxx(condition, ssdq, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        public FunctionResult<DataTable> GetXmxxByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetXmxxByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }


          /// <summary>
        /// 根据项目编号获取项目信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetXmxxByPrjNum(string PrjNum)
        {
            DataTable dt = gcxmDal.GetXmxxByPrjNum(PrjNum);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 项目信息-单项项目信息GridViewSQL处理方法
        /// </summary>
        /// <param name="PKID"></param>
        public FunctionResult<DataTable> GetDxXmxx(string PKID)
        {
            DataTable dt = gcxmDal.GetDxXmxx(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 施工图审查
        /// <summary>
        /// 取出所有施工图审查
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetSgtsc(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetSgtsc(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }


        /// <summary>
        /// 根据主键获取施工图审查
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetSgtscByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetSgtscByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 施工图审查人员信息
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="strParas"></param>
        public FunctionResult<DataTable> GetSgtscRyByCensorNum(string CensorNum)
        {
            DataTable dt = gcxmDal.GetSgtscRyByCensorNum(CensorNum);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 招标投标
        /// <summary>
        /// 取出所有招标投标项目
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetZbtb(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetZbtb(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取招标投标项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetZbtbByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetZbtbByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 合同备案
        /// <summary>
        /// 取出所有合同备案项目
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetHtba(List<IDataItem> condition, string htlb, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetHtba(condition, htlb, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取合同备案项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetHtbaByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetHtbaByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 安全报监
        /// <summary>
        /// 取出所有安全报监项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetAqbjxx(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetAqbjxx(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取安全报监项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetAqbjxxByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetAqbjxxByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// <summary>
        /// 安全报监人员信息
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="strParas"></param>
        public FunctionResult<DataTable> GetAqbjxxByAqjdbm(string aqjdbm)
        {
            DataTable dt = gcxmDal.GetAqbjxxByAqjdbm(aqjdbm);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 质量报监
        /// <summary>
        /// 取出所有质量报监项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetZlbjxx(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetZlbjxx(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取质量报监项目
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="cl"></param>
        public FunctionResult<DataTable> GetZlbjxxByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetZlbjxxByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// <summary>
        /// 质量报监责任单位及人员
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="strParas"></param>
        public FunctionResult<DataTable> GetZlbjxxByZljdbm(string zljdbm)
        {
            DataTable dt = gcxmDal.GetZlbjxxByZljdbm(zljdbm);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 施工许可证
        /// <summary>
        /// 所有施工许可证
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="zzjgdm"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetSgxk(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetSgxk(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取施工许可证
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetSgxkByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetSgxkByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }
        #endregion

        #region 竣工备案
        /// <summary>
        /// 取出所有竣工备案项目
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetJgbaxx(List<IDataItem> condition, int pageSize, string orderby, int pageIndex, out int allRecordCount)
        {
            DataTable dt = gcxmDal.GetJgbaxx(condition, pageSize, orderby, pageIndex, out allRecordCount);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 根据主键获取项目信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> GetJgbaxxByPKID(string PKID)
        {
            DataTable dt = gcxmDal.GetJgbaxxByPKID(PKID);
            return new FunctionResult<DataTable> { Result = dt };
        }

        #endregion
    }
}
