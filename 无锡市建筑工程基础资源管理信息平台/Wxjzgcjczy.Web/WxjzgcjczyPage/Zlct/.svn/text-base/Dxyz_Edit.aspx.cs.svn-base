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


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Dxyz_Edit : BasePage
    {
        ZlctBLL BLL;
        protected string Src;
        protected string DxjbId;
        protected string Operate;
        protected string PageIndex;
        protected string content = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Operate = Request.QueryString["operate"];
            DxjbId = Request.QueryString["dxjbId"];
            PageIndex = Request.QueryString["pageIndex"];
            Src = "Txl_Tree.aspx?type=dxjb&dxjbId=" + DxjbId;
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                this.lblPageIndex.Text = PageIndex;
                if (Operate == "edit")
                {

                    content = "短信简报修改";
                    DataTable dt = BLL.ReadSzgkjc_Dxjb(DxjbId).Result;
                    List<IDataItem> list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);

                    if (list.GetDataItem("EveryWeekOne").ItemData.ToInt32(0) > 0)
                    {
                        this.db_dsfs_1.Checked = true;
                    }
                    else
                        if (list.GetDataItem("EveryMonthOne").ItemData.ToInt32(0) > 0)
                        {
                            this.db_dsfs_2.Checked = true;
                        }
                        else
                            if (list.GetDataItem("EveryQuarterOne").ItemData.ToInt32(0) > 0)
                            {
                                this.db_dsfs_3.Checked = true;
                            }


                    dt = BLL.ReadSzgkjc_DxjbAndSjml(DxjbId).Result;
                    if (dt.Rows.Count > 0)
                    {
                        string txt = ",", txt2 = "";

                        foreach (DataRow item in dt.Rows)
                        {
                            txt += item["SjmlID"] + ",";
                            txt2 += item["SjmlName"] + "【" + item["sjmlMobile"] + "】,";
                        }
                        this.txtReceiverIds.Text = txt;
                        this.db_fsdx.Text = txt2.Trim(new char[] { ',' });
                    }
                    if (!this.db_IsDsfs.Checked)
                    {
                        this.db_dsfs_1.Enabled = false;
                        this.db_dsfs_2.Enabled = false;
                        this.db_dsfs_3.Enabled = false;
                    }
                    else
                    {
                        this.db_dsfs_1.Enabled = true;
                        this.db_dsfs_2.Enabled = true;
                        this.db_dsfs_3.Enabled = true;
                    }
                }
                else
                {
                    content = "预制短信简报";
                    this.txtReceiverIds.Text = ",";
                    if (db_IsDsfs.Checked)
                    {
                        this.db_dsfs_1.Enabled = true;
                        this.db_dsfs_2.Enabled = true;
                        this.db_dsfs_3.Enabled = true;
                    }
                    else
                    {
                        this.db_dsfs_1.Enabled = false;
                        this.db_dsfs_2.Enabled = false;
                        this.db_dsfs_3.Enabled = false;
                    }
                }
                //db_IsDsfs_CheckedChanged(sender,e);
            }
        }

       
        protected void btn_send_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + msg + "');", true);
                return;
            }
            if (this.db_IsDsfs.Checked
                && string.IsNullOrEmpty(this.txtReceiverIds.Text.Trim(new char[] { ',' })) && string.IsNullOrEmpty(this.db_newSxr.Text.Trim().Trim(',')))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('您设置为定时发送，请选择或输入收信人！');", true);
                return;
            }
            //if (this.db_IsDsfs.Checked&&string.IsNullOrEmpty(this.db_fxr.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('请选择发信人！');", true);
            //    return;
            //}

            List<SqlPara> sqls = WebCommon.ConvertSql(this.db_Jbnr.Text);
            if (sqls != null && sqls.Count > 0)
            {
                foreach (SqlPara item in sqls)
                {
                    try
                    {
                        BLL.ExecuteSql(item.executeSql);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn(\"保存失败，可执行sql脚本有错误！<br/>" + ex.Message.Trim('\'').Trim('"') + "\");", true);
                        return;
                    }
                }
            }

            List<IDataItem> list = this.GetControlValue();
            if (this.db_IsDsfs.Checked)
            {
                if (this.db_dsfs_1.Checked)
                {
                    WebCommon.AddDataItem(list, "EveryWeekOne", "1");
                    WebCommon.AddDataItem(list, "EveryMonthOne", "0");
                    WebCommon.AddDataItem(list, "EveryQuarterOne", "0");
                }
                else
                    if (this.db_dsfs_2.Checked)
                    {
                        WebCommon.AddDataItem(list, "EveryWeekOne", "0");
                        WebCommon.AddDataItem(list, "EveryMonthOne", "1");
                        WebCommon.AddDataItem(list, "EveryQuarterOne", "0");
                    }
                    else
                        if (this.db_dsfs_3.Checked)
                        {
                            WebCommon.AddDataItem(list, "EveryWeekOne", "0");
                            WebCommon.AddDataItem(list, "EveryMonthOne", "0");
                            WebCommon.AddDataItem(list, "EveryQuarterOne", "1");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('请选择一种自动发送类型！');", true);
                            return;
                        }
            }
            else
            {
                WebCommon.AddDataItem(list, "EveryWeekOne", "0");
                WebCommon.AddDataItem(list, "EveryMonthOne", "0");
                WebCommon.AddDataItem(list, "EveryQuarterOne", "0");
            }

            WebCommon.AddDataItem(list, "sxrIds", this.txtReceiverIds.Text.Trim(new char[] { ',' }));
            WebCommon.AddDataItem(list, "UserID", "");
            WebCommon.AddDataItem(list, "Fxr", "");

            if (Operate == OperateSt.add.ToString())
            {
                FunctionResult<string> fr = BLL.SaveDxjb(list);
                if (fr.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "SaveResult('" + fr.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + fr.Result + "');", true);
                    return;
                }
            }
            else
            {
                FunctionResult<string> fr = BLL.SaveDxjb(DxjbId, list);
                if (fr.Status != FunctionResultStatus.Error)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "SaveResult('" + fr.Result + "');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + fr.Result + "');", true);
                    return;
                }
            }

        }




        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            string txt = "";

            string sxrIds = this.txtReceiverIds.Text.Trim(new char[] { ',' });
            if (string.IsNullOrEmpty(sxrIds))
            {
                this.db_fsdx.Text = "";
                return;
            }

            DataTable dt = BLL.RetrieveSjml_List(sxrIds).Result;
            foreach (string item in sxrIds.Split(','))
            {
                DataRow[] rows = dt.Select("SjmlID=" + item);
                if (rows.Length > 0)
                {
                    txt += rows[0]["SjmlName"] + "【" + rows[0]["sjmlMobile"] + "】,";
                }
            }
            this.db_fsdx.Text = txt.Trim(new char[] { ',' });

        }

        protected void db_IsDsfs_CheckedChanged(object sender, EventArgs e)
        {
            if (db_IsDsfs.Checked)
            {
                this.db_dsfs_1.Enabled = true;
                this.db_dsfs_2.Enabled = true;
                this.db_dsfs_3.Enabled = true;
            }
            else
            {
                this.db_dsfs_1.Enabled = false;
                this.db_dsfs_2.Enabled = false;
                this.db_dsfs_3.Enabled = false;
            }
        }
    }
}
