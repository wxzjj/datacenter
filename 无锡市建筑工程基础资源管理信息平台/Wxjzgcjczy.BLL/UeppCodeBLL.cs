using System;
using System.Data;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using Wxjzgcjczy.Common;
using Wxjzgcjczy.DAL;
using Wxjzgcjczy.DAL.Sqlserver;

namespace Wxjzgcjczy.BLL
{
    public class UeppCodeBLL
    {
         #region BaseCode

        private UeppCodeDAL ueppCodeDAL;

        public DBOperator DB { get; set; }
        public UeppCodeBLL()
        {
            ueppCodeDAL = new UeppCodeDAL();

            ueppCodeDAL.DB = new DatabaseOperator();
        }

        #endregion

        /// <summary>
        /// 获取一组代码键值信息(原三层架构写法)
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="parentCodeType">父代码类型</param>
        /// <param name="parentCodeInfo">父代码的名称（注意：不是父代码值）</param>
        /// <returns></returns>
        public FunctionResult<System.Collections.Generic.Dictionary<string, string>> SelectKeyValue(CodeType codeType, CodeType parentCodeType, string parentCodeInfo)
        {
            return new FunctionResult<System.Collections.Generic.Dictionary<string, string>> { Result = ueppCodeDAL.SelectKeyValue(codeType, parentCodeType, parentCodeInfo) };
        }

        public FunctionResult<System.Collections.Generic.List<string>> SelectValueOnly(CodeType codeType, CodeType parentCodeType, string parentCodeInfo)
        {
            throw new NotImplementedException();
        }

        public FunctionResult<System.Collections.Generic.Dictionary<string, string>> SelectKeyValue(CodeType codeType)
        {
            return new FunctionResult<System.Collections.Generic.Dictionary<string, string>> { Result = ueppCodeDAL.SelectKeyValue(codeType, CodeType.空父代码类型, string.Empty) };
        }

        public FunctionResult<System.Collections.Generic.List<string>> SelectValueOnly(CodeType codeType)
        {
            throw new NotImplementedException();
        }

        public FunctionResult<DataTable> ReadKhfzr(string khfzrID)
        {
            return null;
        }

        public FunctionResult<DataTable> RetrieveKhfzr(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            allRecordCount = 0;
            return null;
        }
        /// <summary>
        /// 获取代码表的codeinfo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codetype"></param>
        /// <returns></returns>
        public string ReadCodeinfo(string code, string codetype)
        {
            return ueppCodeDAL.ReadCodeinfo(code, codetype);
        }

        /// <summary>
        /// 获取所有模块名称
        /// </summary>
        /// <returns></returns>
        public string ReadMkmc()
        {
            DataTable dt = ueppCodeDAL.ReadMkmc();
            string json = JSONHelper.DataTableToJson(dt);
            if (true)
            {
                string _str = "{ \"id\":\"-10000\",\"value\":\"-10000\",\"text\":\"--所有--\"},";
                json = _str + json;
            }
            json = @"[" + json + "]";
            return json;
        }
        /// <summary>
        /// 获取一组代码键值信息（最新写法）
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="parentCodeType">父代码类型</param>
        /// <param name="parentCodeInfo">父代码的名称（注意：不是父代码值）</param>
        /// <returns></returns>
        public string SelectKeyValue_New(CodeType codeType, CodeType parentCodeType, string parentCodeInfo, bool isAddSpaceItem)
        {
            DataTable dt = ueppCodeDAL.SelectKeyValue_New(codeType, parentCodeType, parentCodeInfo);
            string json = JSONHelper.DataTableToJson(dt);
            if (isAddSpaceItem)
            {
                string _str = "{ \"id\":\"-10000\",\"value\":\"-10000\",\"text\":\"--所有--\"},";
                json = _str + json;
            }
            json = @"[" + json + "]";
            return json;
        }
        /// <summary>
        /// 获取所有操作名称
        /// </summary>
        /// <returns></returns>
        public string ReadCzmc()
        {
            DataTable dt = ueppCodeDAL.ReadCzmc();
            string json = JSONHelper.DataTableToJson(dt);
            if (true)
            {
                string _str = "{ \"id\":\"-10000\",\"value\":\"-10000\",\"text\":\"--所有--\"},";
                json = _str + json;
            }
            json = @"[" + json + "]";
            return json;
        }
        /// <summary>
        /// 获取近五年的代码表
        /// </summary>
        /// <returns></returns>
        public string ReadJwn()
        {
            DataTable dt = ueppCodeDAL.ReadJwn();
            string json = JSONHelper.DataTableToJson(dt);
            json = @"[" + json + "]";
            return json;
        }
        /// <summary>
        /// 获取城市代码表
        /// </summary>
        /// <returns></returns>
        public DataTable ReadQyjsscs()
        {
            return ueppCodeDAL.ReadQyjsscs();
        }
        /// <summary>
        /// 通过城市code获取区县
        /// </summary>
        public DataTable ReadSsdqByParentCode(string parentcode)
        {
            return ueppCodeDAL.ReadSsdqByParentCode(parentcode);
        }
        /// <summary>
        /// 通过城市code获取区县2
        /// </summary>
        public DataTable ReadSsdqByParentCode2(string parentcode)
        {
            return ueppCodeDAL.ReadSsdqByParentCode2(parentcode);
        }
        /// <summary>
        /// 城市地区代码表
        /// </summary>
        /// <returns></returns>
        public string ReadCS()
        {
            DataTable dt = ueppCodeDAL.ReadQyjsscs2();
            string json = JSONHelper.DataTableToJson(dt);
            if (true)
            {
                string _str = "{ \"id\":\"-10000\",\"value\":\"-10000\",\"text\":\"--所有--\"},";
                json = _str + json;
            }
            json = @"[" + json + "]";
            return json;

        }

        public string ReadSsdq(string p)
        {
            throw new NotImplementedException();
        }
    }
}
