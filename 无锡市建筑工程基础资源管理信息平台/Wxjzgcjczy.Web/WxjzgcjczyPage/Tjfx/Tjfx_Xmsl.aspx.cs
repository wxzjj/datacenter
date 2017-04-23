using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using Bigdesk8.Web.Controls;
using Bigdesk8.Web;
using Bigdesk8.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx
{
    public partial class Tjfx_Xmsl : BasePage
    {
        private TjfxBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new TjfxBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.ddl_Xmfl, true);
                WebCommon.CheckBoxListDataBind(this.cbl_ssdq);
                searchData();
            }
        }

        private void searchData()
        {
            string ssdq = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Value + ",";
            }
            if (!string.IsNullOrEmpty(ssdq))
            {
                ssdq = ssdq.Substring(0, ssdq.Length - 1);
            }

            if (!string.IsNullOrEmpty(BdateEnd.SelectedValue) && !string.IsNullOrEmpty(BdateStart.SelectedValue))
            {
                if (BdateEnd.SelectedValue.ToInt32() <= BdateStart.SelectedValue.ToInt32())
                {
                    this.WindowAlert("提示：开工年度范围(结束年度必须大于开始年度)!");
                    return;
                }
            }

            DataTable dt = BLL.RetrieveTjfx_XmTj_Count(ddl_Xmfl.SelectedValue, BdateStart.SelectedValue, BdateEnd.SelectedValue, ssdq).Result;

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            Chart1.Titles[0].Text = "年度项目开工数量统计（单位：个）";
            Chart1.Series["Default"].LegendText = "项目开工数量（个）";
            Chart1.Series["Default"]["PointWidth"] = "0.3";
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Number;
            string title = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                title += dt.Rows[i]["nf"].ToString2() + "总数为:" + dt.Rows[i]["cou"].ToDouble() + "个,";
                Chart1.Series["Default"].Points.Add(new double[] { dt.Rows[i]["cou"].ToDouble() });
                Chart1.Series["Default"].Points[i].Label = (dt.Rows[i]["cou"].ToDouble() == 0) ? "" : String.Format("{0:F}", dt.Rows[i]["cou"].ToDouble());
                Chart1.Series["Default"].Points[i].AxisLabel = dt.Rows[i]["nf"].ToString2();
             
            }
            Chart1.Series["Default"].Color = Color.Blue;
            Chart1.Series["Default"].ToolTip = "#AXISLABEL的项目开工数量为#VAL个";

            double[] d_add = new double[dt.Rows.Count];
            d_add[0] = 0.0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (j == i + 1)
                    {
                        d_add[i + 1] = (dt.Rows[i]["cou"].ToDouble() == 0 ? "0.0" : String.Format("{0:F}", (dt.Rows[j]["cou"].ToDouble() - dt.Rows[i]["cou"].ToDouble()) / dt.Rows[i]["cou"].ToDouble() * 100)).ToDouble();
                    }
                }
            }
            MakeParetoChart(Chart1, "Default", "Pareto", d_add, "项目开工数量增幅(%)", Color.Orange);

            Chart1.ChartAreas["ChartArea1"].AxisY2.Interval = 100;
            Chart1.ChartAreas["ChartArea1"].AxisY2.MajorGrid.Enabled = false;
        }
        void MakeParetoChart(Chart chart, string srcSeriesName, string destSeriesName, double[] d_add, string legendText, Color cl)
        {
            string strChartArea = chart.Series[srcSeriesName].ChartArea;

            Series destSeries = new Series(destSeriesName);
            chart.Series.Add(destSeries);
            destSeries.ChartType = SeriesChartType.Line;
            destSeries.BorderWidth = 3;
            destSeries.ChartArea = strChartArea;
            destSeries.YAxisType = AxisType.Secondary;
            destSeries.LegendText = legendText;
            destSeries.IsValueShownAsLabel = true;
            destSeries.MarkerColor = Color.Red;
            destSeries.MarkerStyle = MarkerStyle.Circle;
            destSeries.MarkerSize = 8;
            destSeries.Color = cl;


            chart.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "0";
            chart.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

            for (int i = 0; i < chart.Series[srcSeriesName].Points.Count; i++)
            {
                destSeries.Points.Add(d_add[i]);
                destSeries.Points[i].AxisLabel = (2012 + i).ToString();
            }

        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            searchData();
        }


    }
}
