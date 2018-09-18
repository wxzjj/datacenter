using Wxjzgcjczy.Common;
using System;
using System.Data;
using Wxjzgcjczy.BLL.model;
using Bigdesk8;
using Bigdesk8.Data;

namespace Wxjzgcjczy.BLL
{

    /// <summary>
    /// 功能：合同备案处理类
    /// 作者：huangzhengyu
    /// 时间：2018-09-18
    /// </summary>
    public class HtbaBLL
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.HtbaDAL DAL = new Wxjzgcjczy.DAL.Sqlserver.HtbaDAL();
 

        public HtbaBLL()
        {
           this.DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        /// <summary>
        /// 更新合同备案-工程类型
        /// </summary>
        /// <param name="recordNum"></param>
        /// <param name="prjType"></param>
        public void saveHtbaPrjType(string recordNum, string prjType)
        {
            DAL.UpdateHtbaPrjType(recordNum, prjType);
        }


    }

}
