using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Reporting.WebForms;
using Bigdesk2010.Data;

namespace Bigdesk2010.MsReporting
{
    /// <summary>
    /// 微软报表web运行模式下的功能实现助手
    /// </summary>
    public class WebAssistant
    {
        // ReportParameter 中的关键字：{{{;}}}、{{{=}}}、{{{,}}}。
        // {{{;}}}：将参数与参数区分开。
        // {{{=}}}：将参数名称与参数值分开。
        // {{{,}}}：将参数值是多值区分开。
        // 
        // 完整的 ReportParameter 字符串表示格式：
        // ParameterName1{{{=}}}ParameterValue1{{{;}}}ParameterName2{{{=}}}ParameterValue21{{{,}}}ParameterValue22{{{,}}}ParameterValue23

        private const string ParametersSplitString = "{{{;}}}";
        private const string ParameterNameValuesSplitString = "{{{=}}}";
        private const string ParameterValuesSplitString = "{{{,}}}";

        /// <summary>
        /// 将 参数名称和参数值 转换成 经过编码的、规范化的报表参数字符串, 以用于页面间的报表参数传递
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        public static string ToNormalizedString(string parameterName, string parameterValue)
        {
            return ReportParametersToNormalizedString(new ReportParameter[] { new ReportParameter(parameterName, parameterValue) });
        }

        /// <summary>
        /// 将 两对参数名称和参数值 转换成 经过编码的、规范化的报表参数字符串, 以用于页面间的报表参数传递
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        public static string ToNormalizedString(string parameterName1, string parameterValue1, string parameterName2, string parameterValue2)
        {
            return ReportParametersToNormalizedString(new ReportParameter[] { new ReportParameter(parameterName1, parameterValue1), new ReportParameter(parameterName2, parameterValue2) });
        }

        /// <summary>
        /// 将 IDataItem 集合转换成 ReportParameter 集合
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<ReportParameter> GetParameters(List<IDataItem> condition)
        {
            List<ReportParameter> reportParameters = new List<ReportParameter>();
            if (condition == null) return reportParameters;

            foreach (IDataItem di in condition)
            {
                if (di.ItemName.IsEmpty()) continue;

                string name = di.ItemName.TrimString();
                string data = di.ItemData.TrimString();

                switch (di.ItemRelation)
                {
                    case DataRelation.GreaterThan:
                    case DataRelation.GreaterThanOrEqual:
                        name += "___begin";
                        break;
                    case DataRelation.LessThan:
                    case DataRelation.LessThanOrEqual:
                        name += "___end";
                        break;
                }

                if (data != "" && di.ItemType == DataType.Date)
                {
                    switch (di.ItemRelation)
                    {
                        default:
                            data = data.ToDate2();
                            break;
                        case DataRelation.LessThan:
                        case DataRelation.LessThanOrEqual:
                            data = data.ToDate().ToString("yyyy-MM-dd 23:59:59");
                            break;
                    }
                }

                reportParameters.Add(new ReportParameter(name, data));
            }
            return reportParameters;
        }

        /// <summary>
        /// 将 ReportParameter 集合转换成 经过编码的、规范化的字符串
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        public static string ReportParametersToNormalizedString(IEnumerable<ReportParameter> reportParameters)
        {
            string parameters = "";
            foreach (ReportParameter p in reportParameters)
            {
                string name = p.Name.ToBase64String();
                string values = "";
                foreach (string v in p.Values)
                {
                    string s = v.ToBase64String();
                    if (values == "")
                        values = s;
                    else
                        values += string.Format("{0}{1}", ParameterValuesSplitString, s);
                }

                if (parameters == "")
                    parameters += string.Format("{0}{1}{2}", name, ParameterNameValuesSplitString, values);
                else
                    parameters += string.Format("{0}{1}{2}{3}", ParametersSplitString, name, ParameterNameValuesSplitString, values);
            }
            return parameters.ToBase64String();
        }

        /// <summary>
        /// 将 经过编码的、规范化的字符串 转换成 ReportParameter 集合
        /// </summary>
        /// <param name="normalizedString">符合预定格式的字符串</param>
        /// <returns></returns>
        public static List<ReportParameter> GetReportParameters(string normalizedString)
        {
            string parameters = normalizedString.FromBase64String();
            string[] parameterArray = parameters.SplitToString(ParametersSplitString, StringSplitOptions.None);

            List<ReportParameter> rps = new List<ReportParameter>();

            foreach (string p in parameterArray)
            {
                string[] nameValues = p.SplitToString(ParameterNameValuesSplitString, StringSplitOptions.None);
                if (nameValues.Length != 2) continue;

                string name = nameValues[0].FromBase64String();
                string[] values = nameValues[1].SplitToString(ParameterValuesSplitString, StringSplitOptions.None);
                if (values == null) continue;

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].FromBase64String();
                }

                rps.Add(new ReportParameter(name.ToLower(), values));
            }

            return rps;
        }
    }
}
