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
    public partial class Aqjd_View : BasePage
    {
        private SzgcBLL BLL;
        protected string rowID, aqjdID, ryID;
        protected string befrom;
        protected string jsdwrowid, sgdwrowid, jldwrowid, ryrowid;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new SzgcBLL(WorkUser);
            aqjdID = Request.QueryString["aqjdid"];
            rowID = Request.QueryString["rowID"];
            befrom = Request.QueryString["befrom"];
            if (!IsPostBack)
            {
                List<IDataItem> list = BLL.ReadXmxx("aqjd", rowID, befrom).Result;
                List<IDataItem> list1 = BLL.ReadXmxx("gcxm", rowID, befrom).Result;
                if (list.Count <= 0)
                {
                    this.WindowAlert("没有获取到数据！");
                    return;
                }


                this.SetControlValue(list1);

                this.SetControlValue(list);

                if (list1.GetDataItem("jsdwrowid") != null)
                    jsdwrowid = list1.GetDataItem("jsdwrowid").ItemData;
                if (list1.GetDataItem("sgdwrowid") != null)
                    sgdwrowid = list1.GetDataItem("sgdwrowid").ItemData;
                if (list1.GetDataItem("jldwrowid") != null)
                    jldwrowid = list1.GetDataItem("jldwrowid").ItemData;
                if (list1.GetDataItem("ryrowid") != null)
                    ryrowid = list1.GetDataItem("ryrowid").ItemData;
                DBText9.Text = list1.GetDataItem("ssdq").ItemData;
                DBText14.Text = list1.GetDataItem("cblx").ItemData;
                DBLabel9.Text = list1.GetDataItem("lxwh").ItemData;
                if (list.Count > 0)
                {
                    if (tb_aqjd.Text == "0")
                        tb_aqjd.Text = "未竣工";
                    else
                        tb_aqjd.Text = "已竣工";
                }

                searchData_Aqbjxmzcy();
            }

        }


        private void searchData_Aqbjxmzcy()
        {
            FunctionResult<DataTable> fr = BLL.RetrieveAqbjxmzcy(rowID, "");
            if (!this.DealResult(fr)) return;
            pdg_Aqbjxmzcy.DataSource = fr.Result;
            pdg_Aqbjxmzcy.DataBind();
        }
    }
}
