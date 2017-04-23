using System;
using System.Collections.Generic;
using Bigdesk2010.Data;

namespace Bigdesk2010.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// 将 System.Web.Mvc.FormCollection 集合 转换成 DataRow
        /// </summary>
        /// <param name="formCollection"></param>
        /// <param name="datarow"></param>
        public static void FormCollectionToDataRow(System.Web.Mvc.FormCollection formCollection, System.Data.DataRow datarow)
        {
            if (formCollection == null || datarow == null) return;

            foreach (string keyName in formCollection.AllKeys)
            {
                if (!datarow.Table.Columns.Contains(keyName)) continue;

                if (formCollection[keyName].IsEmpty())
                {
                    datarow[keyName] = DBNull.Value;
                }
                else
                {
                    /*
                     * 因为当用Html.CheckBoxFor在界面上呈现一个checkbox时，会自动生成一个同名的hidden控件，
                     * 这样，对选中的checkbox，form里传过来的值就是“true,false”；故对这样的情况，要特别如下处理一下
                     */
                    if (formCollection[keyName].Equals("true,false"))
                        datarow[keyName] = true;
                    else
                        datarow[keyName] = formCollection[keyName];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<IDataItem> GetSearchConditionList(System.Web.Mvc.FormCollection formSearch, Dictionary<string, DataItem> dicSearchCondition)
        {
            if (formSearch == null || dicSearchCondition == null) return null;

            List<IDataItem> listSearchCondition = new List<IDataItem>();
            foreach (string keyName in dicSearchCondition.Keys)
            {
                if (!formSearch[keyName].IsEmpty())
                {
                    /*
                     * 因为当用Html.CheckBoxFor在界面上呈现一个checkbox时，会自动生成一个同名的hidden控件，
                     * 这样，对选中的checkbox，form里传过来的值就是“true,false”；故对这样的情况，要特别如下处理一下
                     */
                    if (formSearch[keyName].Equals("true,false"))
                    {
                        dicSearchCondition[keyName].ItemData = true.ToString();
                    }
                    else
                    {
                        dicSearchCondition[keyName].ItemData = formSearch[keyName];
                    }
                    listSearchCondition.Add(dicSearchCondition[keyName]);
                }
            }
            return listSearchCondition;
        }
    }
}