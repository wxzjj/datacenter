using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace WxjzgcjczyHtbaBB
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public DBOperator DB
        {
            get { return Bigdesk8.Data.DBOperatorFactory.GetDBOperator(ConfigurationManager.AppSettings["ConnectionString"], ConfigurationManager.AppSettings["DatabaseType_Sqlserver"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string htqdRqStart = string.Empty;
            string htqdRqEnd = string.Empty;
            string ssdq = string.Empty;
            string htlb = string.Empty;
            try
            {
                htqdRqStart = Request.QueryString["htqdRqStart"].ToString();
                htqdRqEnd = Request.QueryString["htqdRqEnd"].ToString();
                ssdq = Request.QueryString["ssdq"].ToString();
                htlb = Request.QueryString["ContractTypeNum"].ToString();
            }
            catch
            {

            }
            if (!IsPostBack)
            {
                //Response.Write("<script type='text/javascript'>window.close();</script>");
                DataTable dt = RetrieveTjfx_Htba_Report(htqdRqStart, htqdRqEnd, ssdq, htlb);
                Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
                rds.Name = "DataSet1";
                rds.Value = dt;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);
                this.ReportViewer1.Visible = true;

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("./Report1.rdlc");
                this.ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string fileName = Guid.NewGuid().ToString();
                byte[] bytes = ReportViewer1.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out extension,
                        out streamids, out warnings);
                string fileSaveName, fileSaveUrl;
                fileSaveName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + extension;
                fileSaveUrl = "./AllFiles/" + fileSaveName;
                fileSaveUrl = Server.MapPath(fileSaveUrl);
                FileStream fs = new FileStream(fileSaveUrl, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
                Response.Write("<script type='text/javascript'> window.open('./AllFiles/" + fileSaveName + "') ;</script>");
                Response.Write("<script type='text/javascript'>window.close();</script>");
            }
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