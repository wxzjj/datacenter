using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace Wxjzgcjczy.BLL
{
    public class FjxxBLL
    {
        private readonly FjxxDAL DAL = new FjxxDAL();
        public AppUser WorkUser { get; set; }
        public FjxxBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            DAL.DB = new DatabaseOperator();
        }

        #region 读取


        #endregion

        #region 读取列表
        public FunctionResult<DataTable> RetrieveFjxx(string lxxmid, string bdxmid, string ywlxId, string ywjd, string ywlx, string wjmc, int pageSize, int pageIndex, out int allRecordCount)
        {
            DataTable dt = DAL.RetrieveFjxx(this.WorkUser, lxxmid, bdxmid, ywlxId, ywjd, ywlx, wjmc, pageSize, pageIndex, " RegId asc ", out allRecordCount);
            return new FunctionResult<DataTable>() { Result = dt };
        }

        public FunctionResult<DataTable> RetrieveFjxx_Xmqqsx2(string lxxmid, string ywjd)
        {
            //创建一个table结构
            DataTable model = new DataTable();
            model.Columns.AddRange(new DataColumn[] { new DataColumn("isParent"), new DataColumn("parentId"),new DataColumn("id"), new DataColumn("GroupName"), new DataColumn("ywlx"),  
                new DataColumn("wjbm"), new DataColumn("wjlx"), new DataColumn("tjr"), new DataColumn("tjrq"),new DataColumn("wjId"),new DataColumn("Wjxxdz") });

            //获取业务类型
            DataTable modelYwlx = DAL.RetrieveYwlx(ywjd);
            foreach (DataRow dr in modelYwlx.Rows)
            {
                DataRow row;
                //获取附件名称
                DataTable modelFjmc = DAL.RetrieveFjmc(dr["Ywlx"].ToString2(), ywjd);
                foreach (DataRow drFjmc in modelFjmc.Rows)
                {
                    DataTable modelBdxmxx = DAL.RetrieveBdxmxx(lxxmid);
                    if (modelBdxmxx.Rows.Count > 0)
                    {
                        foreach (DataRow drBdxmxx in modelBdxmxx.Rows)
                        {
                            DataTable modelBdxmFj = DAL.RetrieveBdxmFjBywjbh(drBdxmxx["BdxmId"].ToString2(), drFjmc["Wjbh"].ToString2());
                            if (modelBdxmFj.Rows.Count > 1)
                            {
                                row = model.NewRow();
                                row["isParent"] = true;
                                row["parentId"] = "";
                                row["id"] = drFjmc["Wjbh"].ToString2() + "三";
                                row["GroupName"] = dr["Ywlx"].ToString2();
                                row["ywlx"] = drBdxmxx["xmmc"].ToString2();
                                row["wjbm"] = "";
                                row["wjlx"] = "";
                                row["tjr"] = "";
                                row["tjrq"] = "";
                                row["wjId"] = "";
                                row["Wjxxdz"] = "";
                                model.Rows.Add(row);
                                foreach (DataRow drfj in modelBdxmFj.Rows)
                                {
                                    row = model.NewRow();
                                    row["isParent"] = false;
                                    row["parentId"] = drFjmc["Wjbh"].ToString2() + "三";
                                    row["id"] = "";
                                    row["GroupName"] = dr["Ywlx"].ToString2();
                                    row["ywlx"] = drFjmc["Wjmc"].ToString2();
                                    row["wjbm"] = drfj["Wjbh"].ToString2();
                                    row["wjlx"] = drfj["Wjlx"].ToString2();
                                    row["tjr"] = drfj["Wjscr"].ToString2();
                                    row["tjrq"] = drfj["EditDate"].ToString2();
                                    row["wjId"] = drfj["FjId"].ToString2();
                                    row["Wjxxdz"] = drfj["Wjxxdz"].ToString2();
                                    model.Rows.Add(row);
                                }
                            }
                            else
                            {
                                if (modelBdxmFj.Rows.Count == 1)
                                {
                                    row = model.NewRow();
                                    row["isParent"] = true;
                                    row["parentId"] = "";
                                    row["id"] = "";
                                    row["GroupName"] = dr["Ywlx"].ToString2();
                                    row["ywlx"] = drFjmc["Wjmc"].ToString2() + "(" + drBdxmxx["xmmc"].ToString2() + ")";
                                    row["wjbm"] = modelBdxmFj.Rows[0]["Wjbh"].ToString2();
                                    row["wjlx"] = modelBdxmFj.Rows[0]["Wjlx"].ToString2();
                                    row["tjr"] = modelBdxmFj.Rows[0]["Wjscr"].ToString2();
                                    row["tjrq"] = modelBdxmFj.Rows[0]["EditDate"].ToString2();
                                    row["wjId"] = modelBdxmFj.Rows[0]["FjId"].ToString2();
                                    row["Wjxxdz"] = modelBdxmFj.Rows[0]["Wjxxdz"].ToString2();
                                    model.Rows.Add(row);
                                }
                                else
                                {
                                    row = model.NewRow();
                                    row["isParent"] = true;
                                    row["parentId"] = "";
                                    row["id"] = "";
                                    row["GroupName"] = dr["Ywlx"].ToString2();
                                    row["ywlx"] = drFjmc["Wjmc"].ToString2();
                                    row["wjbm"] = drFjmc["Wjbh"].ToString2();
                                    row["wjlx"] = "";
                                    row["tjr"] = "";
                                    row["tjrq"] = "";
                                    row["wjId"] = "";
                                    row["Wjxxdz"] = "";
                                    model.Rows.Add(row);
                                }
                            }
                        }
                    }
                    else
                    {
                        row = model.NewRow();
                        row["isParent"] = true;
                        row["parentId"] = "";
                        row["id"] = "";
                        row["GroupName"] = dr["Ywlx"].ToString2();
                        row["ywlx"] = drFjmc["Wjmc"].ToString2();
                        row["wjbm"] = drFjmc["Wjbh"].ToString2();
                        row["wjlx"] = "";
                        row["tjr"] = "";
                        row["tjrq"] = "";
                        row["wjId"] = "";
                        row["Wjxxdz"] = "";
                        model.Rows.Add(row);
                    }

                }
            }
            return new FunctionResult<DataTable>() { Result = model };
        }


        public FunctionResult<DataTable> RetrieveFjxx_Xmqqsx3(string lxxmid, string ywjd)
        {
            //创建一个table结构
            DataTable model = new DataTable();
            model.Columns.AddRange(new DataColumn[] { new DataColumn("isParent"), new DataColumn("parentId"),new DataColumn("id"), new DataColumn("GroupName"), new DataColumn("ywlx"),  
                new DataColumn("wjbm"), new DataColumn("wjlx"), new DataColumn("tjr"), new DataColumn("tjrq"),new DataColumn("wjId"),new DataColumn("Wjxxdz") });

            //获取业务类型
            DataTable modelYwlx = DAL.RetrieveYwlx(ywjd);
            foreach (DataRow dr in modelYwlx.Rows)
            {
                DataRow row;
                //获取附件名称
                DataTable modelFjmc = DAL.RetrieveFjmc(dr["Ywlx"].ToString2(), ywjd);
                foreach (DataRow drFjmc in modelFjmc.Rows)
                {
                    //获取立项附件信息
                    DataTable modelFj = DAL.RetrieveFj(lxxmid, drFjmc["Wjbh"].ToString2());
                    if (modelFj.Rows.Count > 1)
                    {
                        row = model.NewRow();
                        row["isParent"] = true;
                        row["parentId"] = "";
                        row["id"] = drFjmc["Wjbh"].ToString2() + "二";
                        row["GroupName"] = dr["Ywlx"].ToString2();
                        row["ywlx"] = drFjmc["Wjmc"].ToString2();
                        row["wjbm"] = "";
                        row["wjlx"] = "";
                        row["tjr"] = "";
                        row["tjrq"] = "";
                        row["wjId"] = "";
                        row["Wjxxdz"] = "";
                        model.Rows.Add(row);
                        foreach (DataRow drfj in modelFj.Rows)
                        {
                            row = model.NewRow();
                            row["isParent"] = false;
                            row["parentId"] = drFjmc["Wjbh"].ToString2() + "二";
                            row["id"] = "";
                            row["GroupName"] = dr["Ywlx"].ToString2();
                            row["ywlx"] = "";
                            row["wjbm"] = drfj["Wjbh"].ToString2();
                            row["wjlx"] = drfj["Wjlx"].ToString2();
                            row["tjr"] = drfj["Wjscr"].ToString2();
                            row["tjrq"] = drfj["EditDate"].ToString2();
                            row["wjId"] = drfj["FjId"].ToString2();
                            row["Wjxxdz"] = drfj["Wjxxdz"].ToString2();
                            model.Rows.Add(row);
                        }
                    }
                    else
                    {
                        if (modelFj.Rows.Count == 1)
                        {
                            row = model.NewRow();
                            row["isParent"] = true;
                            row["parentId"] = "";
                            row["id"] = "";
                            row["GroupName"] = dr["Ywlx"].ToString2();
                            row["ywlx"] = drFjmc["Wjmc"].ToString2();
                            row["wjbm"] = modelFj.Rows[0]["Wjbh"].ToString2();
                            row["wjlx"] = modelFj.Rows[0]["Wjlx"].ToString2();
                            row["tjr"] = modelFj.Rows[0]["Wjscr"].ToString2();
                            row["tjrq"] = modelFj.Rows[0]["EditDate"].ToString2();
                            row["wjId"] = modelFj.Rows[0]["FjId"].ToString2();
                            row["Wjxxdz"] = modelFj.Rows[0]["Wjxxdz"].ToString2();
                            model.Rows.Add(row);
                        }
                        else
                        {

                            row = model.NewRow();
                            row["isParent"] = true;
                            row["parentId"] = "";
                            row["id"] = "";
                            row["GroupName"] = dr["Ywlx"].ToString2();
                            row["ywlx"] = drFjmc["Wjmc"].ToString2();
                            row["wjbm"] = drFjmc["Wjbh"].ToString2();
                            row["wjlx"] = "";
                            row["tjr"] = "";
                            row["tjrq"] = "";
                            row["wjId"] = "";
                            row["Wjxxdz"] = "";
                            model.Rows.Add(row);
                        }
                    }
                }
            }
            return new FunctionResult<DataTable>() { Result = model };
        }


        #endregion
    }
}
