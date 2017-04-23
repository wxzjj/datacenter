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
using System.Drawing;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx
{
    public partial class Xmnftj : BasePage
    {
        private TjfxBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new TjfxBLL(this.WorkUser);
            searchData();
            if (!this.IsPostBack)
            {


            }
        }

        private void searchData()
        {
        //    DataTable dt = BLL.RetrieveTjfx_XmTj_Count("BDate").Result;
        //    //GridView1.DataSource = dt;
        //    //GridView1.DataBind();

        //    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

        //    Chart1.Titles[0].Text = "近五年项目统计（单位：个）";
        //    Chart1.Series["Default"].LegendText = "项目数量（个）";
        //    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        //    Chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Number;
        //    string title = "";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        title += dt.Rows[i]["nf"].ToString2() + "总数为:" + dt.Rows[i]["cou"].ToDouble() + "个,";
        //        Chart1.Series["Default"].Points.Add(new double[] { dt.Rows[i]["cou"].ToDouble() });
        //        Chart1.Series["Default"].Points[i].Label = (dt.Rows[i]["cou"].ToDouble() == 0) ? "" : String.Format("{0:F}", dt.Rows[i]["cou"].ToDouble());
        //        Chart1.Series["Default"].Points[i].AxisLabel = dt.Rows[i]["nf"].ToString2();
        //    }

        //    foreach (Series series in Chart1.Series)
        //    {
        //        series.ToolTip = "#AXISLABEL的总投资为#VAL万元";
        //    }
        //    double[] d_add = new double[dt.Rows.Count];
        //    d_add[0] = 0.0;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            if (j == i + 1)
        //            {
        //                d_add[i + 1] = (dt.Rows[i]["cou"].ToDouble() == 0 ? "0.0" : String.Format("{0:F}", (dt.Rows[j]["cou"].ToDouble() - dt.Rows[i]["cou"].ToDouble()) / dt.Rows[i]["cou"].ToDouble() * 100)).ToDouble();
        //            }
        //        }
        //    }
        //    MakeParetoChart(Chart1, "Default", "Pareto", d_add);

        //    Chart1.Series["Pareto"].ChartType = SeriesChartType.Line;
        //    Chart1.Series["Pareto"].LegendText = "增速(%)";
        //    Chart1.Series["Pareto"].IsValueShownAsLabel = true;
        //    Chart1.Series["Pareto"].MarkerColor = Color.Red;
        //    Chart1.Series["Pareto"].MarkerBorderColor = Color.MidnightBlue;
        //    Chart1.Series["Pareto"].MarkerStyle = MarkerStyle.Circle;
        //    Chart1.Series["Pareto"].MarkerSize = 8;
        //    Chart1.Series["Pareto"].LabelFormat = "0.#";  // format with one decimal and leading zero

        //    Chart1.Series["Pareto"].Color = Color.FromArgb(252, 180, 65);

            //dt_title1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + title.Remove(title.LastIndexOf(','));//注：增速=(2011年投资总额-2010年投资总额)/2010年投资总额
        }
        void MakeParetoChart(Chart chart, string srcSeriesName, string destSeriesName, double[] d_add)
        {

            string strChartArea = chart.Series[srcSeriesName].ChartArea;
            chart.Series[srcSeriesName].ChartType = SeriesChartType.Column;

            Series destSeries = new Series(destSeriesName);
            chart.Series.Add(destSeries);
            destSeries.ChartType = SeriesChartType.Line;
            destSeries.BorderWidth = 3;
            destSeries.ChartArea = chart.Series[srcSeriesName].ChartArea;
            destSeries.YAxisType = AxisType.Secondary;

            chart.ChartAreas[strChartArea].AxisY2.LabelStyle.Format = "0.0";
            chart.ChartAreas[strChartArea].AxisX.LabelStyle.IsEndLabelVisible = false;

            for (int i = 0; i < chart.Series[srcSeriesName].Points.Count; i++)
            {
                destSeries.Points.Add(d_add[i]);
            }

        }


    }
}
