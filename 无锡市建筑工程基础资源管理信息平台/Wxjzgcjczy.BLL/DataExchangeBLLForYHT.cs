using Wxjzgcjczy.Common;
using System;
using System.Data;
using Wxjzgcjczy.BLL.model;
using Bigdesk8;
using Bigdesk8.Data;
using System.Configuration;

namespace Wxjzgcjczy.BLL
{

    /// <summary>
    /// 功能：一号通信息处理类
    /// 作者：huangzhengyu
    /// 时间：2018-09-18
    /// </summary>
    public class DataExchangeBLLForYHT
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYHT DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYHT();


        public DataExchangeBLLForYHT()
        {
           //this.DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperatorYHT(); 
            this.DAL.DB = DBOperatorFactory.GetDBOperator("data source=192.168.153.189;Initial Catalog=db_adminexam;user id=sa;password=wxjsj^201701;", "SQLSERVER2008");
        }


        public DataTable GetTBProjectInfo_PrjApprovalNum(string prjNum)
        {
            return this.DAL.GetTBProjectInfo_PrjApprovalNum(prjNum);
        }

    }

}
