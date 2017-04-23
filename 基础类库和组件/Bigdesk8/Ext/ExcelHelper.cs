using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace Bigdesk8.Ext
{
    /// <summary>
    /// 该类专门用于解析Excel文件，提供对Excel的读取功能
    /// </summary>
    public class ExcelHelper
    {
        public ExcelHelper(string fileName, int columnIndex, int columnCount,int rowIndex)
        {
            this.FileName = fileName;
            app = new ApplicationClass();
            app.Visible = false;
            this.DataColsIndex = columnIndex;
            this.DataCols = columnCount;
            this.DataRowsIndex = rowIndex;
        }
        /// <summary>
        /// 构造函数，实例化类时初始化对象的成员变量
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnIndex"></param>
        /// <param name="columnCount"></param>
        public ExcelHelper(string fileName, string sheetName, int columnIndex, int columnCount, int rowIndex)
            : this(fileName, columnIndex, columnCount, rowIndex)
        {
            this.SheetName = sheetName;
        }
        #region 字段
        /// <summary>
        /// 要读取的Excel文件名,完整的文件名称
        /// </summary>
        private String fileName = "";
        /// <summary>
        /// 要读取的文件的数据列数
        /// </summary>
        private int dataCols = 0;
        /// <summary>
        /// 开始读取的数据列的开始列的索引，从"1"开始
        /// </summary>
        private int dataColsIndex = 0;
        private int dataRowsIndex = 0;
    
        /// <summary>
        /// office对象句柄
        /// </summary>
        Microsoft.Office.Interop.Excel.ApplicationClass app = null;
        Workbook work = null;
        Sheets sheets = null;
        Worksheet dataSheet = null;

        private String sheetName = String.Empty;//待解析的表单页名称，若为空，则缺省只解析第一个表单
        #endregion

        #region 属性
        /// <summary>
        /// 要读的Excel文件名
        /// </summary>
        public String FileName
        {
            get { return fileName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("请给定要读取的文件！");
                }
                else
                    if (!File.Exists(value))
                    {
                        throw new Exception(String.Format("文件：\"{0}\"不存在！", value));
                    }
                    else
                        fileName = value;
            }
        }
        /// <summary>
        /// 要读取的表单的列数
        /// </summary>
        public int DataCols
        {
            get
            {
                return this.dataCols;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("指定的列数参数不正确！");
                }
                else
                    this.dataCols = value;
            }
        }
        /// <summary>
        /// 表单开始读的列的索引
        /// </summary>
        public int DataColsIndex
        {
            get { return dataColsIndex; }
            set 
            { 
                if(value<=0)
                {
                    throw new Exception("指定的列数参数不正确！");
                }
                else
                    this.dataColsIndex = value;
            }
        }
        /// <summary>
        /// 要读表单的行的起始索引
        /// </summary>
        public int DataRowsIndex
        {
            get { return dataRowsIndex; }
            set 
            { 
                 if(value<=0)
                 {
                    throw new Exception("指定的开始读取的行参数不正确！");
                 }
                 else
                    dataRowsIndex = value;

            }
        }
        /// <summary>
        /// 对于本Excel文档，待解析的sheet名称，若为空，则缺省的只解析第一个表单的内容
        /// </summary>
        public String SheetName
        {
            get
            {
                return this.sheetName;
            }
            set
            {
                this.sheetName = value;
            }
        }
        #endregion
        public String[] GetSheets()
        {
            work = app.Workbooks.Open(
                 fileName, Missing.Value, Missing.Value,
                 Missing.Value, Missing.Value, Missing.Value,
                 Missing.Value, Missing.Value, Missing.Value,
                 Missing.Value, Missing.Value, Missing.Value,
                 Missing.Value, Missing.Value, Missing.Value);
            String[] s = new String[work.Worksheets.Count];
            int i=0;
            foreach (Worksheet sheet in  work.Worksheets)
            {
                
                s[i++] = sheet.Name;
            }
            this.ReleaseRescorce();
            return s;
            
        }
        /// <summary>
        /// 将从指定Excel文件读取到的数据放到List数组中
        /// </summary>
        /// <returns></returns>
        public List<String[]> ParseExcel()
        {
          
            work = app.Workbooks.Open(
                fileName, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value);
            sheets = work.Worksheets;
            dataSheet = (Worksheet)this.GetWorksheet();
            List<string[]> sArr=new List<string[]>();
            try
            {
                int i = this.DataRowsIndex;
                string[] data = new string[this.DataCols];
                data = GetRowContent(i, this.DataColsIndex, this.DataCols);
                int flag = 1;       //用flag变量来标志当前所读的一行是否为空行，若是空行，则值为1；否则为0。
                foreach (string cellValue in data)
                {
                    if (!String.IsNullOrEmpty(cellValue))
                    {
                        flag = 0;
                        break;
                    }
                }
                while (flag == 0)//循环读取每一行数据，并将所读数据放入List<String[]>集合中，直到读取到空行结束读取操作
                {
                    sArr.Add(data);
                    i++;
                    data = GetRowContent(i, this.DataColsIndex, this.DataCols);
                    flag = 1;
                    foreach (string cellValue in data)
                    {
                        if (!String.IsNullOrEmpty(cellValue))
                        {
                            flag = 0;
                            break;
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                ReleaseRescorce();
            }
            return sArr;
        }
        /// <summary>
        /// 读取指定Excel表中指定行中指定开始列与列数的数据
        /// </summary>
        public String[] GetRowContent(int rowIndex,int columnIndex,int columnCount)
        {  
            String[] retContent = null;
            Range objRange = dataSheet.get_Range(((Char)('A' + columnIndex - 1)).ToString() + rowIndex.ToString(), ((Char)('A' + columnCount + columnIndex - 2)).ToString() + rowIndex.ToString());
            if(objRange != null)
            {
                System.Array values = (System.Array)objRange.Formula;
                retContent = new String[columnCount];
                 for (int index = 1; index <= columnCount;index ++ )
                 {
                     retContent[index - 1] = values.GetValue(1, index).ToString();
                 }
            } 
            return retContent;
        }
        /// <summary>
        /// 根据指定的表单名称，取得对应表单对象;
        /// 若未指定表单名称，则缺省只解析第一个表单;
        /// </summary>
        /// <returns></returns>
        private Worksheet GetWorksheet()
        {
            if (this.sheets != null)
            {
                if (String.IsNullOrEmpty(this.SheetName))
                {
                    return (Microsoft.Office.Interop.Excel.Worksheet)(sheets[1]);
                }
                else
                {
                    foreach (Microsoft.Office.Interop.Excel.Worksheet ws in sheets)
                    {
                        if (ws.Name.Trim().Equals(this.SheetName.Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            return ws;
                        }
                    }
                    throw new Exception("所给表单名称错误！");
                }
            }
            return null;
        }
        /// <summary>
        /// 释放COM组件所占用的资源
        /// </summary>
        public void ReleaseRescorce()
        {
            try
            {
                if (work != null || app != null)
                {
                    work.Close(null, null, null);
                    app.Workbooks.Close();
                    app.Quit();

                    if (dataSheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSheet);
                    if (work != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(work);                  
                    if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                    app = null;
                    work = null;
                    dataSheet = null;
                    GC.Collect();
                }
            }
            catch
            {
            }

        }
    }
}
