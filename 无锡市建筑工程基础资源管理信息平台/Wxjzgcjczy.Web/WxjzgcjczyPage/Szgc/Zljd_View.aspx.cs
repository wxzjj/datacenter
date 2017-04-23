using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Bigdesk8;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc
{
    public partial class Zljd_View : BasePage
    {
        private SzgcBLL BLL;
        protected string rowID, zljdID, jsdwrowid, sgdwrowid, jldwrowid, jcdwrowid, kcdwrowid, sjdwrowid, ryrowid, sgxmtybh;
        protected string befrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new SzgcBLL(WorkUser);
            zljdID = Request.QueryString["zljdid"];
            rowID = Request.QueryString["rowID"];
            befrom = Request.QueryString["befrom"];
            if (!IsPostBack)
            {
                List<IDataItem> list = BLL.ReadXmxx("zljd", rowID, befrom).Result;
                if (list.Count <= 0)
                {
                    this.WindowAlert("没有获取到数据！");
                    return;
                }
                this.SetControlValue(list);
                sgxmtybh = list.GetDataItem("sgxmtybh").ItemData;

                //jsdwrowid = list.GetDataItem("jsdwrowid").ItemData;
                //sgdwrowid = list.GetDataItem("sgdwrowid").ItemData;
                //jldwrowid = list.GetDataItem("jldwrowid").ItemData;
                //ryrowid = list.GetDataItem("ryrowid").ItemData;
                //jcdwrowid = list.GetDataItem("jcdwrowid").ItemData;
                //kcdwrowid = list.GetDataItem("kcdwrowid").ItemData;
                //sjdwrowid = list.GetDataItem("sjdwrowid").ItemData;
                searchData();
            }

        }

        private void searchData()
        {
            FunctionResult<DataTable> fr = BLL.RetrieveZljdDtgc(rowID, befrom);
            if (!this.DealResult(fr)) return;
            gridView.DataSource = fr.Result;
            gridView.DataBind();
        }
    }
}
