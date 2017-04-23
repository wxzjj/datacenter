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
    public partial class Cjgf :BasePage
    {
        private SzgcBLL BLL;
        private SzqyBLL bll;
        protected string rowID, jsdwrowid, jldwrowid, sgdwrowid, ryrowid;
        private string befrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new SzgcBLL(WorkUser);
            bll = new SzqyBLL(WorkUser);
            rowID = Request.QueryString["rowID"];
            befrom = Request.QueryString["befrom"];
            List<IDataItem> list = BLL.ReadXmxx("gcxm", rowID, befrom).Result;
            if (list.Count > 0)
            {
                jsdwrowid = list.GetDataItem("jsdwrowid").ItemData;
                jldwrowid = list.GetDataItem("jldwrowid").ItemData;
                sgdwrowid = list.GetDataItem("sgdwrowid").ItemData;
                ryrowid = list.GetDataItem("ryrowid").ItemData;
            }
            this.SetControlValue(list);

            //searchData();
            searchData_Aqbjxmzcy();
        }

        //private void searchData()
        //{
        //    FunctionResult<DataTable> fr = bll.RetrieveQyxxViewList(jsdwrowid);
        //    if (!this.DealResult(fr)) return;
        //    gridView.DataSource = fr.Result;
        //    gridView.DataBind();
        //}

        private void searchData_Aqbjxmzcy()
        {
            FunctionResult<DataTable> fr = BLL.RetrieveAqbjxmzcy(rowID, "");
            if (!this.DealResult(fr)) return;
            pdg_Aqbjxmzcy.DataSource = fr.Result;
            pdg_Aqbjxmzcy.DataBind();
        }

      
    }
}
