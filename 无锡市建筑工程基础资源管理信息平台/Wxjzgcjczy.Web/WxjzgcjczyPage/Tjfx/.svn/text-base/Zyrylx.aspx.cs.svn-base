using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using System.Web.UI.DataVisualization.Charting;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx
{
    public partial class Zyrylx : BasePage
    {
        private TjfxBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new TjfxBLL(this.WorkUser);

            if (!this.IsPostBack)
            {
                searchData();
                Chart1.Series["Series1"].Points[0].CustomProperties += "Exploded=true";
            }
        }

        private void searchData()
        {
            //DataTable dt = BLL.RetrieveTjfx_Sgdwzz().Result;
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            DataTable modelTj = BLL.RetrieveTjfx_ZyrylxTj().Result;

            int t1 = 0;
            int t2 = 0;
            int t3 = 0;
            int t4 = 0;
            int t5 = 0;
            foreach (DataRow dr in modelTj.Rows)
            {
                if (dr["lx"].ToString2() == "jzs")
                {
                    t1 = dr["cou"].ToInt32();
                }
                if (dr["lx"].ToString2() == "jls")
                {
                    t2 = dr["cou"].ToInt32();
                }
                if (dr["lx"].ToString2() == "zjs")
                {
                    t3 = dr["cou"].ToInt32();
                }
                if (dr["lx"].ToString2() == "jzhus")
                {
                    t4 = dr["cou"].ToInt32();
                }
                if (dr["lx"].ToString2() == "jgs")
                {
                    t5 = dr["cou"].ToInt32();
                }

            }
            Chart1.Series["Series1"].Points.Add(new double[] { t1 });
            Chart1.Series["Series1"].Points.Add(new double[] { t2 });
            Chart1.Series["Series1"].Points.Add(new double[] { t3 });
            Chart1.Series["Series1"].Points.Add(new double[] { t4 });
            Chart1.Series["Series1"].Points.Add(new double[] { t5 });
            Chart1.Series["Series1"].Points[0].LegendText = "建造师有" + t1 + "人";
            Chart1.Series["Series1"].Points[1].LegendText = "监理师" + t2 + "人";
            Chart1.Series["Series1"].Points[2].LegendText = "造价师" + t3 + "人";
            Chart1.Series["Series1"].Points[3].LegendText = "建筑师" + t4 + "人";
            Chart1.Series["Series1"].Points[4].LegendText = "结构师" + t5 + "人";

            Chart1.Series["Series1"].Points[0].Label = t1 == 0 ? "" : (t1 + "人\n(" + Chart1.Series["Series1"].Points[0].Label + ")");
            Chart1.Series["Series1"].Points[1].Label = t2 == 0 ? "" : (t2 + "人\n(" + Chart1.Series["Series1"].Points[1].Label + ")");
            Chart1.Series["Series1"].Points[2].Label = t3 == 0 ? "" : (t3 + "人\n(" + Chart1.Series["Series1"].Points[2].Label + ")");
            Chart1.Series["Series1"].Points[3].Label = t4 == 0 ? "" : (t4 + "人\n(" + Chart1.Series["Series1"].Points[3].Label + ")");
            Chart1.Series["Series1"].Points[4].Label = t5 == 0 ? "" : (t5 + "人\n(" + Chart1.Series["Series1"].Points[4].Label + ")");
            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = true;
            //dt_title1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;设计施工一体化企业有" + t1 + "家，chr(10)+chr(13)建筑施工企业有" + t2 + "家，chr(10)+chr(13)市政企业有" + t3 + "家，chr(10)+chr(13)园林绿化企业有" + t4 + "家";

        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            searchData();
            int pointIndex = int.Parse(e.PostBackValue);
            Series series = Chart1.Series["Series1"];

            if (pointIndex >= 0 && pointIndex < series.Points.Count)
            {
                series.Points[pointIndex].CustomProperties += "Exploded=true";
            }
        }
    }
}
