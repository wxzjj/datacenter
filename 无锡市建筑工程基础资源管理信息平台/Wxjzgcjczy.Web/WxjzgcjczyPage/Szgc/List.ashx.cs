using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.Common;
using System.Data;
using Wxjzgcjczy.BLL;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class List : IHttpHandler, IRequiresSessionState
    {

        private SzgcBLL BLL;
        private SzqyBLL bll;
        private string json;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int allRecordCount = 0;
            string sortname = context.Request.Params["sortname"];
            string sortorder = context.Request.Params["sortorder"];
            int page = Convert.ToInt32(context.Request.Params["page"]) - 1; // 系统的索引从0开始，所以此处需要减1
            int pagesize = Convert.ToInt32(context.Request.Params["pagesize"]);
            string fromWhere = context.Request.QueryString["fromwhere"];
            string xmlx = context.Request.QueryString["xmlx"];
            string rowid = context.Request.QueryString["rowid"];
            string zljdid = context.Request.QueryString["zljdid"];
            string aqjdid = context.Request.QueryString["aqjdid"];
            string sgxkid = context.Request.QueryString["sgxkid"];
            string sgxmtybh = context.Request.QueryString["sgxmtybh"];
            string qyId = context.Request.QueryString["qyid"];
            string befrom = context.Request.QueryString["befrom"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            AppUser workUser = (AppUser)sessionAppUser;
            BLL = new SzgcBLL(workUser);
            bll = new SzqyBLL(workUser);

            //排序
            string orderby = @" " + sortname + " " + sortorder + " ";
            //通过检索翻译 生成查询条件
            FilterTranslator ft = ContextExtension.GetGridData(context);
            //分页
            DataTable dt = new DataTable();
            switch (fromWhere)
            {
                case "Lxbd":
                    dt = BLL.RetrieveLxbd(rowid, ft, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "Xmxx":
                    dt = BLL.RetrieveSzgc(xmlx, workUser, ft, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "GcxmHtba":
                    dt = BLL.RetrieveGcxmHtba(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "GcxmAqjd":
                    dt = BLL.RetrieveGcxmAqjd(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "GcxmZljd":
                    dt = BLL.RetrieveGcxmZljd(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "GcxmSgxk":
                    dt = BLL.RetrieveGcxmSgxk(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "GcxmJgys":
                    dt = BLL.RetrieveGcxmJgys(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "DwgcList":
                    dt = BLL.RetrieveDwgcList(rowid, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "Aqbjxmzcy":
                    dt = BLL.RetrieveAqbjxmzcy(rowid, befrom).Result;
                    break;
                case "AqjdZljd":
                    dt = BLL.RetrieveAqjdZljd(rowid).Result;
                    break;
                case "AqjdSgxk":
                    dt = BLL.RetrieveAqjdSgxk(rowid).Result;
                    break;
                case "AqjdJgba":
                    dt = BLL.RetrieveAqjdJgba(rowid).Result;
                    break;
                case "zljdDtgc":
                    dt = BLL.RetrieveZljdDtgc(rowid, befrom).Result;
                    break;
                case "zljdSgxk":
                    dt = BLL.RetrieveZljdSgxk(rowid).Result;
                    break;
                case "zljdJgba":
                    dt = BLL.RetrieveZljdJgba(rowid).Result;
                    break;
                case "sgxk":
                    dt = BLL.RetrieveSgxk(rowid).Result;
                    break;
                case "sgxkJgba":
                    dt = BLL.RetrieveSgxkJgba(rowid).Result;
                    break;
                case "QyxxView":
                    dt = bll.RetrieveQyxxViewList(qyId).Result;
                    break;
            }
            ft.Parms.Clear();

            string result = JSONHelper.DataTableToJson(dt);
            //result = Regex.Replace(result, @"[/n/r]", ""); //去掉字符串里所有换行符
            //result = result.TrimEnd((char[])"\n\r".ToCharArray());  //去掉换行符
            json = @"{""Rows"":[" + result + @"],""Total"":""" + allRecordCount + @"""}";
            context.Response.Write(json);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

