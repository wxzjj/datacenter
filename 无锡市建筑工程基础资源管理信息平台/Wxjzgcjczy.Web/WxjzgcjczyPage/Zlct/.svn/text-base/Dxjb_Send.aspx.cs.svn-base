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
   
    public partial class Dxjb_Send : BasePage
    {
        ZlctBLL BLL;
        protected string Src;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                Src = "Txl_Tree.aspx?type=dxjb&dxjbId=";
                List<IDataItem> list = new List<IDataItem>();
                DataTable dt = BLL.RetrieveDxjb_List(list).Result;
                if (dt.Rows.Count > 0)
                {
                    this.ddl_yzjb.Items.Add(new ListItem("--请选择短信简报模板--", ""));
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem li = new ListItem(item["Jbmc"].ToString2(), item["DxjbId"].ToString2());
                        this.ddl_yzjb.Items.Add(li);
                    }
                }
                else
                {
                    this.ddl_yzjb.Items.Add(new ListItem("--没有短信简报模板--", ""));
                }
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
            if (string.IsNullOrEmpty(this.txtReceiverIds.Text.Trim(','))&& string.IsNullOrEmpty(this.db_newSxr.Text.Trim().Trim(',')))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('请选择或输入收信人！');", true);
                return;
            }

            List<IDataItem> list = this.GetControlValue();
            string sxrIds = "";
            DataTable dt= BLL.RetrieveDxjb_AllSxr_List().Result;

            foreach(string s in this.txtReceiverIds.Text.Trim().Trim(',').Split(','))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    DataRow[] rows = dt.Select("SjmlID=" + s);
                    if (rows != null && rows.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(rows[0]["sjmlMobile"].ToString2().Trim()))
                            sxrIds += rows[0]["sjmlMobile"].ToString2().Trim() + ",";
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('\"" + rows[0]["SjmlName"] + "\"收信人号码为空，无法发送短信！');", true);
                            return;

                        }
                    }
                }
            }
            string sxr = this.db_fsdx.Text.Trim().Trim(',');
            string[] mobiles;
            if (!string.IsNullOrEmpty(this.db_newSxr.Text.Trim().Trim(new char[','])))
            {
                mobiles = this.db_newSxr.Text.Trim().Trim(',').Split(',');
               
                if (mobiles != null && mobiles.Length > 0)
                {
                     
                    foreach (var item in mobiles)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            sxrIds += item + ",";
                        }
                        if (string.IsNullOrEmpty(sxr))
                            sxr += "【" + item + "】";
                        else
                            sxr += ",【" + item + "】";
                    }
                }
            }
            if (string.IsNullOrEmpty(sxrIds.Trim(',')))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('收信人号码为空！');", true);
                return;
            }
            WebCommon.AddDataItem(list, "SxrNumbers", sxrIds.Trim(','));
            
            WebCommon.AddDataItem(list, "Jbmc", this.db_Jbmc.Text);
            WebCommon.AddDataItem(list, "Jbnr", this.db_Jbnr.Text);
            WebCommon.AddDataItem(list, "EveryWeekOne", "0");
            WebCommon.AddDataItem(list, "IsDsfs", "False");
            WebCommon.AddDataItem(list, "EveryWeekOne", "0");
            WebCommon.AddDataItem(list, "EveryMonthOne", "0");
            WebCommon.AddDataItem(list, "EveryQuarterOne", "0");
            WebCommon.AddDataItem(list, "Sxr", sxr);

            //WebCommon.AddDataItem(list, "UserID", this.WorkUser.UserID);
            //WebCommon.AddDataItem(list, "Fxr", this.WorkUser.UserName);
            //WebCommon.AddDataItem(list, "SendTime", DateTime.Now.ToString());
            string errorPhone="";
            FunctionResult<string> fr = BLL.SendDxjb(this.ddl_yzjb.SelectedValue, list, out errorPhone);
            if (fr.Status != FunctionResultStatus.Error)
            {
                if (string.IsNullOrEmpty(errorPhone))
                {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "", "SaveResult('" + fr.Result + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowWarnResult('" + fr.Result + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + fr.Result + "');", true);
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

        protected void ddl_yzjb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddl_yzjb.SelectedIndex == 0)
            {
                this.txtReceiverIds.Text = ",";
                this.db_Jbmc.Text = "";
                this.db_Jbnr.Text = "";
                this.db_fsdx.Text= "";
                this.db_newSxr.Text = "";
                
                Src = "Txl_Tree.aspx?dxjbId=";
                
                return;
            }
            Src = "Txl_Tree.aspx?type=dxjb&dxjbId=" + this.ddl_yzjb.SelectedValue;
            DataTable dt = BLL.ReadSzgkjc_Dxjb(this.ddl_yzjb.SelectedValue).Result;
            List<IDataItem> list = dt.Rows[0].ToDataItem();
            this.SetControlValue(list);

            string jbnr = list.GetDataItem("Jbnr").ItemData;
            string jbnrNew = string.Copy(jbnr);
            List<SqlPara> sqls = WebCommon.ConvertSql(jbnr);
            if (sqls.Count > 0)
            {
                foreach (SqlPara item in sqls)
                {
                    string str = BLL.ExecuteSql(item.executeSql);
                    item.newStr = str;
                    jbnrNew = jbnrNew.Replace(item.oldStr, item.newStr);
                }
            }
            this.db_Jbnr.Text = jbnrNew;
            dt = BLL.ReadSzgkjc_DxjbAndSjml(this.ddl_yzjb.SelectedValue).Result;
            if (dt.Rows.Count > 0)
            {
                string txt = "", txt2 = "";

                foreach (DataRow item in dt.Rows)
                {
                    txt +=  ","+item["SjmlID"] ;
                    txt2 += item["SjmlName"] + "【" + item["sjmlMobile"] + "】,";
                }
                this.txtReceiverIds.Text = txt;
                this.db_fsdx.Text = txt2.Trim(new char[] { ',' });
            }
        }
    }
}
