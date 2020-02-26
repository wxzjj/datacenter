using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Web;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using Aspose.Cells;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage
{
    public partial class ToExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fileName = "ToExcel" + DateTime.Now.ToString("yyyyMMddhhMMss") + ".xls";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Z_ToExcel\\"+fileName;
            string start = startdate.Text;
            string end = enddate.Text;


            SystemBLL BLL = new SystemBLL();
            DataSet ds = BLL.GetDs(start,end);

            SaveExcel_Fb1(fileName, ds, this,filepath);
            filepath = "http://58.215.18.222:8889//Z_ToExcel//" + fileName;
            this.WindowLocation(filepath);
        }




        public static bool SaveExcel_Fb1(string fileName, DataSet oDS, System.Web.UI.Page PG, string path)
        {
            try
            {
                Aspose.Cells.License li = new Aspose.Cells.License();
                string _path = PG.Server.MapPath("Common/scripts/License.lic");
                li.SetLicense(_path);
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();

                Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
                style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                style2.Font.Name = "宋体";//文字字体 
                style2.Font.Size = 14;//文字大小 
                style2.Font.IsBold = true;//粗体 
                style2.IsTextWrapped = true;//单元格内容自动换行 
                style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                Aspose.Cells.Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
                style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                style3.Font.Name = "宋体";//文字字体 
                style3.Font.Size = 12;//文字大小        
                style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

 
                for (int k = 0; k < oDS.Tables.Count; k++)
                {

                    Aspose.Cells.Worksheet cellSheet = workbook.Worksheets.Add(oDS.Tables[k].TableName);

                    for (int i = 0; i < oDS.Tables[k].Columns.Count; i++)
                    {
                        cellSheet.Cells[0, i].PutValue(oDS.Tables[k].Columns[i].ColumnName);
                        cellSheet.Cells[0, i].SetStyle(style2);
                    }


                    if (oDS.Tables[k].Rows.Count > 0)
                    {
                        for (int i = 0; i < oDS.Tables[k].Rows.Count; i++)
                        {
                            for (int j = 0; j < oDS.Tables[k].Columns.Count; j++)
                            {
                                cellSheet.Cells[1 + i, j].PutValue(oDS.Tables[k].Rows[i][j].ToString());
                                cellSheet.Cells[1 + i, j].SetStyle(style3);
                            }
                        }
                    }

 
                }

                workbook.Save(path); 

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


    }
}
