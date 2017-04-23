using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxjzgcjczyQyb.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage
{
    public partial class Start : System.Web.UI.Page
    {
        public GcxmBLL gcxmBll;
        public ScztBLL scztBll;
        public ZcryBLL zcryBll;
        public XytxBLL xytxBll;
        private AppUser WorkUser = new AppUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            gcxmBll = new GcxmBLL(this.WorkUser);
            scztBll = new ScztBLL(this.WorkUser);
            zcryBll = new ZcryBLL(this.WorkUser);
            xytxBll = new XytxBLL(this.WorkUser);
            if (!IsPostBack)
            {
                string ssdq = "";
                string tblb = "";
                int allRecordCount;
                List<IDataItem> list = new List<IDataItem>();

                #region 工程项目
                /*项目信息*/
                gridView_xmxx.DataSource = gcxmBll.GetXmxx(list, ssdq, this.gridView_xmxx.PageSize, "BDate desc", 0, out allRecordCount).Result;
                gridView_xmxx.DataBind();

                /*施工图审查*/
                gridView_sgtsc.DataSource = gcxmBll.GetSgtsc(list, this.gridView_sgtsc.PageSize, "  CensorEDate desc ", 0, out allRecordCount).Result;
                gridView_sgtsc.DataBind();

                /*招标投标*/
                gridView_zbtb.DataSource = gcxmBll.GetZbtb(list, this.gridView_zbtb.PageSize, "  TenderResultDate desc ", 0, out allRecordCount).Result;
                gridView_zbtb.DataBind();

                /*合同备案*/
                gridView_htba.DataSource = gcxmBll.GetHtba(list, tblb, this.gridView_htba.PageSize, "  ContractDate desc ", 0, out allRecordCount).Result;
                gridView_htba.DataBind();

                /*安全监督*/
                gridView_aqjd.DataSource = gcxmBll.GetAqbjxx(list, this.gridView_aqjd.PageSize, " bjrq desc ", 0, out allRecordCount).Result;
                gridView_aqjd.DataBind();

                /*质量报监*/
                gridView_zljd.DataSource = gcxmBll.GetZlbjxx(list, this.gridView_zljd.PageSize, "sbrq desc", 0, out allRecordCount).Result;
                gridView_zljd.DataBind();

                /*施工许可*/
                gridView_sgxk.DataSource = gcxmBll.GetSgxk(list, this.gridView_sgxk.PageSize, " IssueCertDate desc", 0, out allRecordCount).Result;
                gridView_sgxk.DataBind();

                /*竣工备案*/
                gridView_jgba.DataSource = gcxmBll.GetJgbaxx(list, this.gridView_jgba.PageSize, " EDate desc", 0, out allRecordCount).Result;
                gridView_jgba.DataBind();

                #endregion

                #region 市场主体
                /*建设单位*/
                gridView_jsdw.DataSource = scztBll.RetrieveQyxxList("jsdw", list, this.gridView_jsdw.PageSize, 0, out allRecordCount).Result;
                gridView_jsdw.DataBind();

                /*勘察单位*/
                gridView_kcdw.DataSource = scztBll.RetrieveQyxxList("kcdw", list, this.gridView_kcdw.PageSize, 0, out allRecordCount).Result;
                gridView_kcdw.DataBind();

                /*设计单位*/
                gridView_sjdw.DataSource = scztBll.RetrieveQyxxList("sjdw", list, this.gridView_jsdw.PageSize, 0, out allRecordCount).Result;
                gridView_sjdw.DataBind();

                /*施工单位*/
                gridView_sgdw.DataSource = scztBll.RetrieveQyxxList("sgdw", list, this.gridView_jsdw.PageSize, 0, out allRecordCount).Result;
                gridView_sgdw.DataBind();

                /*中介机构*/
                gridView_zjjg.DataSource = scztBll.RetrieveQyxxList("zjjg", list, this.gridView_jsdw.PageSize, 0, out allRecordCount).Result;
                gridView_zjjg.DataBind();

                /*其他*/
                //gridView_qt.DataSource = scztBll.RetrieveQyxxList("qtdw", list, this.gridView_jsdw.PageSize, 0, out allRecordCount).Result;
                //gridView_qt.DataBind();

                #endregion

                #region 执业人员
                /*注册执业人员*/
                gridView_zczyry.DataSource = zcryBll.RetrieveZyryJbxx("zczyry", list, this.gridView_zczyry.PageSize, 0, "", out allRecordCount).Result;
                gridView_zczyry.DataBind();

                /*安全生产管理人员*/
                gridView_aqscglry.DataSource = zcryBll.RetrieveZyryJbxx("aqscglry", list, this.gridView_zczyry.PageSize, 0, "", out allRecordCount).Result;
                gridView_aqscglry.DataBind();

                /*企业技经人员*/
                gridView_qyjjry.DataSource = zcryBll.RetrieveZyryJbxx("qyjjry", list, this.gridView_zczyry.PageSize, 0, "", out allRecordCount).Result;
                gridView_qyjjry.DataBind();

                /*专业岗位管理人员*/
                gridView_zygwglry.DataSource = zcryBll.RetrieveZyryJbxx("zygwglry", list, this.gridView_zczyry.PageSize, 0, "", out allRecordCount).Result;
                gridView_zygwglry.DataBind();

                #endregion

                #region 信用体系
                /*信用公示*/
                gridView_xytx.DataSource = xytxBll.RetrieveQyxykp(list, this.gridView_xytx.PageSize, 0, "kpnd desc", out allRecordCount).Result;
                gridView_xytx.DataBind();

                #endregion
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TableCellCollection cells = e.Row.Cells;
                //foreach (TableCell cell in cells)
                //{
                //    if (!string.IsNullOrEmpty(cell.Text))
                //    {
                //        cell.Text = Server.HtmlDecode(cell.Text);
                //    }
                //}

                DBText lbryzyzglx = (DBText)e.Row.FindControl("lbryzyzglx");
                DBText lbzsbh = (DBText)e.Row.FindControl("lbzsbh");

                if (!string.IsNullOrEmpty(lbryzyzglx.Text))
                {
                    lbryzyzglx.Text = lbryzyzglx.Text.Replace("<br />", ",").Split(',')[0];
                }

                if (!string.IsNullOrEmpty(lbzsbh.Text))
                {
                    lbzsbh.Text = lbzsbh.Text.Replace("<br />", ",").Split(',')[0];
                }


            }
        }
    }
}
