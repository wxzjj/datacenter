using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.Common;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy.DAL.Sqlserver;
using Bigdesk8.Data;
using System.Transactions;
namespace Wxjzgcjczy.BLL
{
    public class ZlctBLL
    {
        public AppUser WorkUser { get; set; }
        private readonly ZlctDAL DAL = new ZlctDAL();
        public ZlctBLL(AppUser workUser)
        {
            this.WorkUser = workUser;
            this.DAL.DB = new DatabaseOperator();
        }

        #region 获取表结构
        #endregion

        #region 新增
        /// <summary>
        /// 发送工作指令信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SendGzzs(List<IDataItem> list)
        {
            try
            {

                string content = "";
                IDataItem item = list.GetDataItem("Sxtxx");
                string Sxtxx = item.ItemData.Trim(new char[] { ',' });
                list.Remove(item);
                if (Sxtxx == "")
                    return new FunctionResult<string>() { Result = "发送工作指示添加失败！", Status = FunctionResultStatus.Error };

                item = list.GetDataItem("GzzsNr");
                string gzzsNr = item.ItemData;

                item = list.GetDataItem("Gzzszt");
                string gzzszt = item.ItemData;

                content = "指示主题：" + gzzszt + "\n" + "指示内容：" + gzzsNr + "\n" + "指示人：" + WorkUser.UserName;

                item = list.GetDataItem("IsDxfs");
                string isFsdx = item.ItemData;

                string[] sxr = Sxtxx.Split(',');
                DataTable dtSxr = DAL.ReadZJG_Gwtz_Sjml(Sxtxx);

                DataTable dtUser = DAL.ReadSimlInfo(WorkUser.UserID);
                if (dtUser.Rows.Count == 0)
                    return new FunctionResult<string>() { Result = "发送工作指示添加失败！<br/>该用户没有与通讯录里的联系人信息相关联", Status = FunctionResultStatus.Error };
                DataTable dtGzzs = DAL.GetSchemaSzgkjc_Gzzs();
                DataRow row = dtGzzs.NewRow();
                list.ToDataRow(row);
                dtGzzs.Rows.Add(row);
                dtGzzs.Rows[0]["UserId"] = this.WorkUser.UserID;
                dtGzzs.Rows[0]["ZsrId"] = dtUser.Rows[0]["SjmlID"];
                dtGzzs.Rows[0]["ZsrName"] = dtUser.Rows[0]["SjmlName"];
                dtGzzs.Rows[0]["Zssj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dtGzzs.Rows[0]["DataState"] = 10;

                using (TransactionScope scope = BLLCommon.GetTransactionScope())
                {
                    dtGzzs.Rows[0]["GzzsId"] = DAL.ReadSzgkjc_Gzzs_NewId();
                    if (DAL.SubmitSzgkjc_Gzzs(dtGzzs))
                    {
                        DataTable dtZshf = DAL.GetSchemaSzgkjc_Gzhf();
                        int i = 0;
                        List<string> sxrNumbers = new List<string>();

                        foreach (string s in sxr)
                        {
                            if (s != "")
                            {
                                DataRow[] rows = dtSxr.Select("SjmlID=" + s);
                                if (rows.Length > 0)
                                {
                                    row = dtZshf.NewRow();
                                    list.ToDataRow(row);
                                    dtZshf.Rows.Add(row);
                                    row["GzzsId"] = dtGzzs.Rows[0]["GzzsId"];
                                    row["ZshfrId"] = s.Trim();
                                    row["ZshfrName"] = rows[0]["SjmlName"];
                                    row["Tele"] = rows[0]["SjmlTel"];
                                    row["Phone"] = rows[0]["sjmlMobile"];
                                    row["Email"] = rows[0]["SjmlEmail"];
                                    row["UserId"] = rows[0]["UserID"];
                                    row["ZshfId"] = DAL.ReadSzgkjc_Gzhf_NewId() + i++;

                                    if (!string.IsNullOrEmpty(rows[0]["sjmlMobile"].ToString2().Trim()))
                                    {
                                        sxrNumbers.Add(rows[0]["sjmlMobile"].ToString2().Trim());
                                    }

                                }
                                else
                                {
                                    return new FunctionResult<string>() { Result = "工作指示发送失败！<br/>收信人信息与系统用户信息关联有误", Status = FunctionResultStatus.Error };
                                }
                            }
                        }
                        if (dtZshf.Rows.Count > 0)
                        {
                            if (DAL.SubmitSzgkjc_Gzhf(dtZshf))
                            {
                                if (sxrNumbers.Count > 0 && isFsdx.ToLower() == "true")
                                {
                                    //发送短信息
                                    string errorPhone = "";
                                    FunctionResult<String> fr = SendMSG(sxrNumbers, content, out errorPhone);
                                    if (fr.Status != FunctionResultStatus.Error)
                                    {
                                        scope.Complete();
                                    }
                                    return fr;
                                }
                                else
                                {
                                    scope.Complete();
                                    return new FunctionResult<string>() { Result = "工作指示发送成功！" };
                                }
                            }
                            else
                            {
                                return new FunctionResult<string>() { Result = "工作指示发送失败！", Status = FunctionResultStatus.Error };
                            }
                        }
                        else
                        {
                            return new FunctionResult<string>() { Result = "工作指示发送失败！", Status = FunctionResultStatus.Error };
                        }
                    }
                    else
                    {
                        return new FunctionResult<string>() { Result = "工作指示发送失败！", Status = FunctionResultStatus.Error };
                    }

                }
            }
            catch (Exception ex)
            {
                return new FunctionResult<string>() { Result = "工作指示发送失败！<br/>" + ex.Message, Status = FunctionResultStatus.Error };
            }
        }

        public FunctionResult<String> SendMSG(List<string> numbers, string content, out string errorPhone)
        {
            errorPhone = "";
            FunctionResult<string> fr = null;
            int num = 0;
            int flag = 1;
            if (numbers.Count == 0)
            {
                fr = new FunctionResult<string>() { Result = "发送失败！<br/>没有收信人号码", Status = FunctionResultStatus.Error };
            }

            //发送短信息
            Wxjzgcjczy.BLL.WebReference.WebServiceSendMSG service = new Wxjzgcjczy.BLL.WebReference.WebServiceSendMSG();
            foreach (string phoneNumber in numbers)
            {
                flag = service.SendMessage("0512C00136628", "1234qwer", "202.102.41.101", "9005", phoneNumber, content);
                if (flag == 1)
                {
                    flag = service.SendMessage("0512C00136628", "1234qwer", "202.102.41.101", "9005", phoneNumber, content);
                    if (flag == 1)
                    {
                        flag = service.SendMessage("0512C00136628", "1234qwer", "202.102.41.101", "9005", phoneNumber, content);
                        if (flag == 0)
                        {
                            num++;
                        }
                        else
                        {
                            errorPhone += phoneNumber + ",";
                        }
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num++;
                }
            }
            if (num > 0)
            {
                if (num < numbers.Count)
                    fr = new FunctionResult<string>() { Result = "部分发送成功！<br/>未发送成功的号码：【" + errorPhone.Trim(',') + "】" };
                else
                    fr = new FunctionResult<string>() { Result = "发送成功！" };
            }
            else
            {
                fr = new FunctionResult<string>() { Result = "发送失败！", Status = FunctionResultStatus.Error };
            }
            return fr;
        }

        /// <summary>
        /// 保存工作指令信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SaveGzzs(List<IDataItem> list)
        {
            IDataItem item = list.GetDataItem("Sxtxx");
            string Sxtxx = item.ItemData.Trim(new char[] { ',' });
            list.Remove(item);
            if (Sxtxx == "")
                return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };

            string[] sxr = Sxtxx.Split(',');
            DataTable dtGzzs = DAL.GetSchemaSzgkjc_Gzzs();
            DataRow row = dtGzzs.NewRow();
            list.ToDataRow(row);
            dtGzzs.Rows.Add(row);
            dtGzzs.Rows[0]["DataState"] = 0;

            DataTable dtSxr = DAL.ReadZJG_Gwtz_Sjml(Sxtxx);

            using (TransactionScope scope = BLLCommon.GetTransactionScope())
            {
                dtGzzs.Rows[0]["GzzsId"] = DAL.ReadSzgkjc_Gzzs_NewId();

                if (DAL.SubmitSzgkjc_Gzzs(dtGzzs))
                {
                    DataTable dtZshf = DAL.GetSchemaSzgkjc_Gzhf();
                    int i = 0;
                    foreach (string s in sxr)
                    {
                        if (s != "")
                        {
                            DataRow[] rows = dtSxr.Select("SjmlID=" + s);
                            if (rows.Length > 0)
                            {
                                row = dtZshf.NewRow();
                                list.ToDataRow(row);
                                dtZshf.Rows.Add(row);
                                row["GzzsId"] = dtGzzs.Rows[0]["GzzsId"];
                                row["ZshfrId"] = s.Trim();
                                row["ZshfrName"] = rows[0]["SjmlName"];
                                row["Tele"] = rows[0]["SjmlTel"];
                                row["Phone"] = rows[0]["sjmlMobile"];
                                row["Email"] = rows[0]["SjmlEmail"];
                                row["ZshfId"] = DAL.ReadSzgkjc_Gzhf_NewId() + i++;
                            }
                            else
                            {
                                return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                            }
                        }
                    }
                    if (dtZshf.Rows.Count > 0)
                    {
                        if (DAL.SubmitSzgkjc_Gzhf(dtZshf))
                        {
                            scope.Complete();
                            return new FunctionResult<string>() { Result = "工作指示添加成功！" };
                        }
                        else
                        {
                            return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                        }
                    }
                    else
                    {
                        return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                    }
                }
                else
                {
                    return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                }

            }
        }
        /// <summary>
        /// 保存已存在的工作指令信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SaveGzzs(string gzzsId, List<IDataItem> list)
        {
            IDataItem item = list.GetDataItem("Sxtxx");
            string Sxtxx = item.ItemData.Trim(new char[] { ',' });
            list.Remove(item);
            if (Sxtxx == "")
                return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };

            string[] sxr = Sxtxx.Split(',');
            DataTable dtGzzs = DAL.ReadSzgkjc_Gzzs(gzzsId);
            DataRow row = dtGzzs.Rows[0];
            list.ToDataRow(row);
            dtGzzs.Rows[0]["DataState"] = 0;
            dtGzzs.Rows[0]["GzzsId"] = DAL.ReadSzgkjc_Gzzs_NewId();

            DataTable dtSxr = DAL.ReadZJG_Gwtz_Sjml(Sxtxx);

            if (!DAL.DeleteOnlyGzzshfById(gzzsId))
            {
                return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
            }
            using (TransactionScope scope = BLLCommon.GetTransactionScope())
            {
                if (DAL.SubmitSzgkjc_Gzzs(dtGzzs))
                {
                    DataTable dtZshf = DAL.GetSchemaSzgkjc_Gzhf();
                    int i = 0;
                    foreach (string s in sxr)
                    {
                        if (s != "")
                        {
                            DataRow[] rows = dtSxr.Select("SjmlID=" + s);
                            if (rows.Length > 0)
                            {
                                row = dtZshf.NewRow();
                                list.ToDataRow(row);
                                dtZshf.Rows.Add(row);
                                row["GzzsId"] = dtGzzs.Rows[0]["GzzsId"];
                                row["ZshfrId"] = s.Trim();
                                row["ZshfrName"] = rows[0]["SjmlName"];
                                row["Tele"] = rows[0]["SjmlTel"];
                                row["Phone"] = rows[0]["sjmlMobile"];
                                row["Email"] = rows[0]["SjmlEmail"];
                                row["ZshfId"] = DAL.ReadSzgkjc_Gzhf_NewId() + i++;
                            }
                            else
                            {
                                return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                            }
                        }
                    }
                    if (dtZshf.Rows.Count > 0)
                    {
                        if (DAL.SubmitSzgkjc_Gzhf(dtZshf))
                        {
                            scope.Complete();
                            return new FunctionResult<string>() { Result = "工作指示添加成功！" };
                        }
                        else
                        {
                            return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                        }
                    }
                    else
                    {
                        return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                    }
                }
                else
                {
                    return new FunctionResult<string>() { Result = "工作指示添加失败！", Status = FunctionResultStatus.Error };
                }

            }
        }

        /// <summary>
        /// 回复工作指令信息
        /// </summary>
        /// <param name="zlhfId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> HfZlhfxx(string zlhfId, List<IDataItem> list)
        {
            DataTable dt = DAL.ReadSzgkjc_Gzhf(zlhfId);
            list.ToDataRow(dt.Rows[0]);
            //dt.Rows[0]["ZshrName"] = WorkUser.UserName;
            dt.Rows[0]["Zshfsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt.Rows[0]["DataState"] = 20;
            if (DAL.SubmitSzgkjc_Gzhf(dt))
            {
                return new FunctionResult<string>() { Message = new Exception("工作指令回复成功！") };
            }
            else
            {
                return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("工作指令回复失败！") };
            }

        }
        /// <summary>
        /// 将指令回复信息更新到已读状态
        /// </summary>
        /// <param name="zlhfId"></param>
        /// <returns></returns>
        public FunctionResult<string> UpdateZlhfToReadedState(string zlhfId)
        {
            DataTable dt = DAL.ReadSzgkjc_Gzhf(zlhfId);
            dt.Rows[0]["DataState"] = 10;
            if (DAL.SubmitSzgkjc_Gzhf(dt))
            {
                return new FunctionResult<string>() { Message = new Exception("工作指令回复状态更新成功！") };
            }
            else
            {
                return new FunctionResult<string>() { Status = FunctionResultStatus.Error, Message = new Exception("工作指令回复状态更新失败！") };
            }

        }

        /// <summary>
        /// 保存短信简报信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SaveDxjb(List<IDataItem> list)
        {
            IDataItem item = list.GetDataItem("sxrIds");
            string sxrIds = item.ItemData.Trim(new char[] { ',' });
            list.Remove(item);

            item = list.GetDataItem("SendPhone");
            string sendPhone = item.ItemData;

            if (sxrIds == "" && string.IsNullOrEmpty(sendPhone.Trim().Trim(',')))
                return new FunctionResult<string>() { Result = "短信简报信息添加失败！<br/>没有收信人信息", Status = FunctionResultStatus.Error };

            DataTable dtDxjb = DAL.GetSchemaSzgkjc_Dxjb();
            DataRow row = dtDxjb.NewRow();
            list.ToDataRow(row);
            dtDxjb.Rows.Add(row);
            dtDxjb.Rows[0]["DataState"] = 0;
            dtDxjb.Rows[0]["DxjbId"] = DAL.ReadSzgkjc_Dxjb_NewId();

            using (TransactionScope scope = BLLCommon.GetTransactionScope())
            {
                if (DAL.SubmitSzgkjc_Dxjb(dtDxjb))
                {
                    if (!string.IsNullOrEmpty(sxrIds))
                    {
                        string[] sxr = sxrIds.Split(',');
                        DataTable dtSxr = DAL.ReadZJG_Gwtz_Sjml(sxrIds);
                        DataTable dtSzgkjc_Dxjb_Sjml = DAL.GetSchemaSzgkjc_Dxjb_Sjml();
                        foreach (string s in sxr)
                        {
                            if (s != "")
                            {
                                DataRow[] rows = dtSxr.Select("SjmlID=" + s);
                                if (rows.Length > 0)
                                {
                                    row = dtSzgkjc_Dxjb_Sjml.NewRow();
                                    list.ToDataRow(row);
                                    dtSzgkjc_Dxjb_Sjml.Rows.Add(row);
                                    row["DxjbId"] = dtDxjb.Rows[0]["DxjbId"];
                                    row["SjmlID"] = s.Trim();
                                    row["UserID"] = rows[0]["UserID"];
                                    row["Dxjb_Sjml_Id"] = 0;
                                }
                                else
                                {
                                    return new FunctionResult<string>() { Result = "短信简报信息添加失败！", Status = FunctionResultStatus.Error };
                                }
                            }
                        }
                        if (dtSzgkjc_Dxjb_Sjml.Rows.Count > 0)
                        {
                            if (DAL.SubmitSzgkjc_Dxjb_Sjml(dtSzgkjc_Dxjb_Sjml))
                            {
                                scope.Complete();
                                return new FunctionResult<string>() { Result = "短信简报信息添加成功！" };
                            }
                            else
                            {
                                return new FunctionResult<string>() { Result = "短信简报信息添加失败！", Status = FunctionResultStatus.Error };
                            }
                        }
                        else
                        {
                            return new FunctionResult<string>() { Result = "短信简报信息添加失败！", Status = FunctionResultStatus.Error };
                        }
                    }
                    else
                    {
                        scope.Complete();
                        return new FunctionResult<string>() { Result = "短信简报信息添加成功！" };
                    }
                }
                else
                {
                    return new FunctionResult<string>() { Result = "短信简报信息添加失败！", Status = FunctionResultStatus.Error };
                }

            }
        }

        /// <summary>
        /// 保存短信简报信息
        /// </summary>
        /// <param name="dxjbId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SaveDxjb(string dxjbId, List<IDataItem> list)
        {
            IDataItem item = list.GetDataItem("sxrIds");
            string sxrIds = item.ItemData.Trim(new char[] { ',' });
            list.Remove(item);

            item = list.GetDataItem("SendPhone");
            string sendPhone = item.ItemData;

            if (sxrIds == "" && string.IsNullOrEmpty(sendPhone.Trim().Trim(',')))
            {
                return new FunctionResult<string>() { Result = "短信简报信息添加失败！<br/>没有收信人信息", Status = FunctionResultStatus.Error };


            }
            string[] sxr = sxrIds.Split(',');

            DataTable dtDxjb = DAL.ReadSzgkjc_Dxjb(dxjbId);
            if (dtDxjb.Rows.Count == 0)
            {
                return new FunctionResult<string>() { Result = "短信简报信息已被删除，更新失败！", Status = FunctionResultStatus.Error };
            }
            DataRow row = dtDxjb.Rows[0];
            list.ToDataRow(row);

            using (TransactionScope scope = BLLCommon.GetTransactionScope())
            {
                try
                {
                    if (DAL.SubmitSzgkjc_Dxjb(dtDxjb))
                    {
                        DAL.DeleteZJG_Dxjb_Sjml(dxjbId);
                        if (string.IsNullOrEmpty(sxrIds))
                        {
                            scope.Complete();
                            return new FunctionResult<string>() { Result = "短信简报信息添加成功！<br/>但没有添加收信人信息" };
                        }
                        //&& string.IsNullOrEmpty(sendPhone.Trim().Trim(','))

                        else
                        {
                            DataTable dtSxr = DAL.ReadZJG_Gwtz_Sjml(sxrIds);
                            DataTable dtSzgkjc_Dxjb_Sjml = DAL.GetSchemaSzgkjc_Dxjb_Sjml();
                            foreach (string s in sxr)
                            {
                                if (s != "")
                                {
                                    DataRow[] rows = dtSxr.Select("SjmlID=" + s);
                                    if (rows.Length > 0)
                                    {
                                        row = dtSzgkjc_Dxjb_Sjml.NewRow();
                                        list.ToDataRow(row);
                                        dtSzgkjc_Dxjb_Sjml.Rows.Add(row);
                                        row["DxjbId"] = dtDxjb.Rows[0]["DxjbId"];
                                        row["SjmlID"] = s.Trim();
                                        row["UserID"] = rows[0]["UserID"];
                                        row["Dxjb_Sjml_Id"] = 0;
                                    }
                                    else
                                    {
                                        return new FunctionResult<string>() { Result = "短信简报信息修改失败！", Status = FunctionResultStatus.Error };
                                    }
                                }
                            }
                            if (dtSzgkjc_Dxjb_Sjml.Rows.Count > 0)
                            {
                                if (DAL.SubmitSzgkjc_Dxjb_Sjml(dtSzgkjc_Dxjb_Sjml))
                                {
                                    scope.Complete();
                                    return new FunctionResult<string>() { Result = "短信简报信息修改成功！" };
                                }
                                else
                                {
                                    return new FunctionResult<string>() { Result = "短信简报信息修改失败！", Status = FunctionResultStatus.Error };
                                }
                            }
                            else
                            {
                                return new FunctionResult<string>() { Result = "短信简报信息修改失败！", Status = FunctionResultStatus.Error };
                            }
                        }
                    }
                    else
                    {
                        return new FunctionResult<string>() { Result = "短信简报信息修改失败！", Status = FunctionResultStatus.Error };
                    }
                }
                catch (Exception ex)
                {
                    return new FunctionResult<string>() { Result = "短信简报信息修改失败！<br/>" + ex.Message, Status = FunctionResultStatus.Error };
                }

            }
        }

        /// <summary>
        /// 发送短信简报
        /// </summary>
        /// <param name="dxjbId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<string> SendDxjb(string dxjbId, List<IDataItem> list, out string errorPhone)
        {
            errorPhone = "";
            FunctionResult<string> fr;
            IDataItem sxrNumbersItem = list.GetDataItem("SxrNumbers");
            list.Remove(sxrNumbersItem);
            string sxrNumbers = sxrNumbersItem.ItemData.Trim(',');

            if (string.IsNullOrEmpty(sxrNumbers))
            {
                return new FunctionResult<string>() { Result = "发送失败！<br/>没有收信号码", Status = FunctionResultStatus.Error };
            }
            IDataItem item = list.GetDataItem("Jbnr");
            string jbnr = item.ItemData;

            DataTable dtDxjbSendRecord = DAL.GetSchemaSzgkjc_Dxjb_Records();
            DataRow row = dtDxjbSendRecord.NewRow();

            list.ToDataRow(row);
            dtDxjbSendRecord.Rows.Add(row);
            dtDxjbSendRecord.Rows[0]["UserId"] = this.WorkUser.UserID;
            dtDxjbSendRecord.Rows[0]["Fxr"] = this.WorkUser.UserName;
            dtDxjbSendRecord.Rows[0]["SendTime"] = DateTime.Now.ToString();
            dtDxjbSendRecord.Rows[0]["DataState"] = 0;
            dtDxjbSendRecord.Rows[0]["Id"] = DAL.ReadSzgkjc_Dxjb_Records_NewId();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (DAL.SubmitSzgkjc_Dxjb_Records(dtDxjbSendRecord))
                    {
                        fr = SendMSG(sxrNumbers.Split(',').ToList<string>(), jbnr, out errorPhone);
                        if (fr.Status != FunctionResultStatus.Error)
                        {
                            if (!string.IsNullOrEmpty(errorPhone))
                            {
                                dtDxjbSendRecord = DAL.ReadSzgkjc_Dxjb_Records(dtDxjbSendRecord.Rows[0]["Id"].ToString());
                                dtDxjbSendRecord.Rows[0]["ErrorNumbers"] = errorPhone;
                                DAL.SubmitSzgkjc_Dxjb_Records(dtDxjbSendRecord);
                            }
                            scope.Complete();
                        }
                        else
                        {
                            fr = new FunctionResult<string>() { Result = "发送失败！", Status = FunctionResultStatus.Error };
                        }
                        return fr;
                    }
                    else
                    {
                        fr = new FunctionResult<string>() { Result = "发送失败！", Status = FunctionResultStatus.Error };
                    }
                }
            }
            catch (Exception ex)
            {
                fr = new FunctionResult<string>() { Result = "发送失败！", Status = FunctionResultStatus.Error };
            }
            return fr;
        }
        #endregion

        #region 修改

        #endregion

        #region 删除
        public FunctionResult DeleteGzzshfById(string gzzsId)
        {
            try
            {
                if (DAL.DeleteGzzshfById(gzzsId))
                {
                    return new FunctionResult() { Message = new Exception("删除成功！") };
                }
                else
                {
                    return new FunctionResult() { Status = FunctionResultStatus.Error, Message = new Exception("删除失败！") };
                }

            }
            catch (Exception e)
            {
                return new FunctionResult() { Status = FunctionResultStatus.Error, Message = e };
            }
        }

        public FunctionResult DeleteSzgkjc_Dxjb_Records(string Id)
        {
            DataTable dt = DAL.ReadSzgkjc_Dxjb_Records(Id);
            dt.Rows[0]["DataState"] = -1;
            if (DAL.SubmitSzgkjc_Dxjb_Records(dt))
            {
                return new FunctionResult() { Message = new Exception("删除成功！") };
            }
            else
            {
                return new FunctionResult() { Message = new Exception("删除失败！"), Status = FunctionResultStatus.Error };
            }
        }
        public FunctionResult DeleteSzgkjc_Dxjb_YzJb(string Id)
        {
            DataTable dt = DAL.ReadSzgkjc_Dxjb(Id);
            dt.Rows[0]["DataState"] = -1;
            if (DAL.SubmitSzgkjc_Dxjb(dt))
            {
                return new FunctionResult() { Message = new Exception("删除成功！") };
            }
            else
            {
                return new FunctionResult() { Message = new Exception("删除失败！"), Status = FunctionResultStatus.Error };
            }
        }

        #endregion

        #region 读取

        public FunctionResult<DataTable> GetCurrentSimlInfo()
        {
            DataTable dt = DAL.ReadSimlInfo(WorkUser.UserID);
            return new FunctionResult<DataTable> { Result = dt };
        }

        /// <summary>
        /// 工作指示信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> ReadSzgkjc_Gzzs(string gzzsId)
        {
            return new FunctionResult<DataTable> { Result = DAL.ReadSzgkjc_Gzzs(gzzsId) };
        }
        /// <summary>
        /// 工作指示回复
        /// </summary>
        /// <param name="zshfId"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> ReadSzgkjc_Gzhf(string zshfId)
        {
            return new FunctionResult<DataTable> { Result = DAL.ReadSzgkjc_Gzhf(zshfId) };
        }
        /// <summary>
        /// 获取短信简报信息
        /// </summary>
        /// <param name="dxjbId"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> ReadSzgkjc_Dxjb(string dxjbId)
        {
            return new FunctionResult<DataTable> { Result = DAL.ReadSzgkjc_Dxjb(dxjbId) };
        }
        public FunctionResult<DataTable> ReadSzgkjc_DxjbAndSjml(string dxjbId)
        {
            return new FunctionResult<DataTable>() { Result = DAL.ReadSzgkjc_DxjbAndSjml(this.WorkUser, dxjbId) };
        }

        public FunctionResult<DataTable> ReadSzgkjc_Dxjb_Records(string id)
        {
            return new FunctionResult<DataTable>() { Result = DAL.ReadSzgkjc_Dxjb_Records(id) };
        }
        #endregion

        #region 读取列表

        public FunctionResult<DataTable> RetrieveUser_List(List<IDataItem> list, int pagesize, int page, out int allRecordCount)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveUser_List(this.WorkUser, list, pagesize, page, "", out allRecordCount) };
        }
        /// <summary>
        /// 通讯录树结构
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string ZTreeJsonOfTxl(GgfzInfoModel searchCondition)
        {
            return DAL.ZTreeJsonOfTxl(searchCondition);
        }

        /// <summary>
        /// 通讯录
        /// </summary>
        /// <returns></returns>
        public DataTable ZComboxJsonOfTxl()
        {
            return DAL.ZComboxJsonOfTxl();
        }
        /// <summary>
        /// 通讯录树结构
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public string ZTreeJsonOfTxl(GgfzInfoModel searchCondition, string gzzsId)
        {
            return DAL.ZTreeJsonOfTxl(searchCondition, gzzsId);
        }
        public string ZTreeJsonOfDxjb(GgfzInfoModel searchCondition)
        {
            return DAL.ZTreeJsonOfDxjb(searchCondition);
        }

        public string ZTreeJsonOfDxjb(GgfzInfoModel searchCondition, string dxjbId)
        {
            return DAL.ZTreeJsonOfDxjb(searchCondition, dxjbId);
        }
        public FunctionResult<DataTable> RetrieveGzzs_List(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {

            DataTable dt = DAL.RetrieveGzzs_List(this.WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            //if (dt.Rows.Count > 0)
            //{
            //    dt.Columns.Add(new DataColumn("zdhfr"));
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        string str = "";
            //        DataTable dts = DAL.RetrieveZshfListByGzzsId(WorkUser, item["GzzsId"].ToString());
            //        foreach (DataRow row in dts.Rows)
            //        {
            //            str += row["ZshfrName"] + "【" + row["Phone"] + "】,";
            //        }
            //        item["zdhfr"] = str.TrimEnd(new char[] { ',' });
            //    }
            //}
            return new FunctionResult<DataTable>()
            {
                Result = dt
            };
        }

        public FunctionResult<DataTable> RetrieveZshf_all_List(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveZshf_all_List(this.WorkUser, ft, pagesize, page, orderby, out allRecordCount) };
        }
        /// <summary>
        /// 获取某一工作指示的回复信息
        /// </summary>
        /// <param name="gzzsId"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveZshfListByGzzsId(string gzzsId)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveZshfListByGzzsId(this.WorkUser, gzzsId) };
        }
        public FunctionResult<DataTable> RetrieveZshfListByGzzsId(List<IDataItem> condition, string gzzsId, int pagesize, int page, out int allRecordCount)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveZshfListByGzzsId(this.WorkUser, condition, gzzsId, pagesize, page, "", out allRecordCount) };
        }
        /// <summary>
        /// 短信简报信息
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjb_List(List<IDataItem> list, int pagesize, int page, out int allRecordCount)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_List(this.WorkUser, list, pagesize, page, "", out allRecordCount) };
        }
        /// <summary>
        /// 所有的短信模板信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjb_List(List<IDataItem> list)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_List(this.WorkUser, list) };
        }
        /// <summary>
        /// 定时发送的短信模板信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjb_Dsfs_List(List<IDataItem> list)
        {
            IDataItem item = new DataItem();
            item.ItemName = "IsDsfs";
            item.ItemData = "1";
            list.Add(item);
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_List(this.WorkUser, list) };
        }
        /// <summary>
        /// 手动发送的短信简报
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjb_Sdfs_List(List<IDataItem> list)
        {
            IDataItem item = new DataItem();
            item.ItemName = "IsDsfs";
            item.ItemData = "0";
            list.Add(item);
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_List(this.WorkUser, list) };
        }

        /// <summary>
        /// 获取短信简报所有收信人信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjb_Sxr_List(string dxjbId)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_Sxr_List(this.WorkUser, dxjbId) };
        }
        public FunctionResult<DataTable> RetrieveDxjb_AllSxr_List()
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveDxjb_AllSxr_List() };
        }



        /// <summary>
        /// 根据通讯录id获取通讯录详细信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sjmlIds"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveSjml_List(string sjmlIds)
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveSjml_List(this.WorkUser, sjmlIds) };
        }
        /// <summary>
        /// 获取短信简报发送记录信息
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveDxjbSendRecords_List(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            DataTable dt = DAL.RetrieveDxjbSendRecords_List(this.WorkUser, ft, pagesize, page, orderby, out allRecordCount);

            return new FunctionResult<DataTable>() { Result = dt };
        }
        /// <summary>
        /// 预制短信简报列表
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveYzDxjb_List(FilterTranslator ft, int pagesize, int page, string orderby, out int allRecordCount)
        {
            DataTable dt = DAL.RetrieveYzDxjb_List(this.WorkUser, ft, pagesize, page, orderby, out allRecordCount);
            dt.Columns.Add(new DataColumn("Sxr"));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string str = "";
                    DataTable dts = DAL.RetrieveDxjb_Sxr_List(WorkUser, item["DxjbId"].ToString());
                    foreach (DataRow row in dts.Rows)
                    {
                        str += row["SjmlName"] + "【" + row["SjmlTel"] + "】,";
                    }
                    item["Sxr"] = str.TrimEnd(new char[] { ',' });
                }
            }

            return new FunctionResult<DataTable>() { Result = dt };
        }
        /// <summary>
        /// 获取未读工作指示
        /// </summary>
        /// <param name="workUser"></param>
        /// <returns></returns>
        public FunctionResult<DataTable> RetrieveGzzs_NoRead()
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveGzzs_NoRead(this.WorkUser) };
        }

        public FunctionResult<DataTable> RetrieveJytj()
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveJytj(this.WorkUser) };
        }

        public DataTable ReadGwtz()
        {
            return DAL.ReadGwtz(this.WorkUser);
        }


        public FunctionResult<DataTable> RetrieveGzyj()
        {
            return new FunctionResult<DataTable>() { Result = DAL.RetrieveGzyj() };
        }

        #endregion


        #region
        public string ExecuteSql(string sqlStr)
        {
            return DAL.ExecuteSql(sqlStr);
        }
        #endregion


        #region 首页简要统计
        public DataTable GetJytj()
        {
            DataTable dt = DAL.GetTjb();
            //DataTable dt = new DataTable();
            ////工程项目
            //dt.Columns.Add("zjajxm", typeof(string));
            //dt.Columns.Add("sxsq1", typeof(string));
            //dt.Columns.Add("xsq1", typeof(string));
            //dt.Columns.Add("hsq1", typeof(string));
            //dt.Columns.Add("bhq1", typeof(string));
            //dt.Columns.Add("xq1", typeof(string));
            //dt.Columns.Add("jy1", typeof(string));
            //dt.Columns.Add("yx1", typeof(string));
            //dt.Columns.Add("jgajxm", typeof(string));
            //dt.Columns.Add("sxsq2", typeof(string));
            //dt.Columns.Add("xsq2", typeof(string));
            //dt.Columns.Add("hsq2", typeof(string));
            //dt.Columns.Add("bhq2", typeof(string));
            //dt.Columns.Add("xq2", typeof(string));
            //dt.Columns.Add("jy2", typeof(string));
            //dt.Columns.Add("yx2", typeof(string));

            ////市场主体
            //dt.Columns.Add("jsdwNo", typeof(string));
            //dt.Columns.Add("kcsjdwNo", typeof(string));
            //dt.Columns.Add("sgdwNo", typeof(string));
            //dt.Columns.Add("zjjgNo", typeof(string));
            //dt.Columns.Add("qtNo", typeof(string));

            ////执业人员
            //dt.Columns.Add("jzsNo", typeof(string));
            //dt.Columns.Add("jlsNo", typeof(string));
            //dt.Columns.Add("zjsNo", typeof(string));
            //dt.Columns.Add("jzhusNo", typeof(string));
            //dt.Columns.Add("jgsNo", typeof(string));
            //dt.Columns.Add("alNo", typeof(string));
            //dt.Columns.Add("blNo", typeof(string));
            //dt.Columns.Add("clNo", typeof(string));
            //dt.Columns.Add("jsryNo", typeof(string));
            //dt.Columns.Add("jjryNo", typeof(string));
            //DataRow dr = dt.NewRow();

            //dr["zjajxm"] = DAL.GetXmtj(0, "");
            //dr["sxsq1"] = DAL.GetXmtj(0, "市区");
            //dr["xsq1"] = DAL.GetXmtj(0, "锡山");
            //dr["hsq1"] = DAL.GetXmtj(0, "惠山");
            //dr["bhq1"] = DAL.GetXmtj(0, "滨湖");
            //dr["xq1"] = DAL.GetXmtj(0, "新区");
            //dr["jy1"] = DAL.GetXmtj(0, "江阴");
            //dr["yx1"] = DAL.GetXmtj(0, "宜兴");
            //dr["jgajxm"] = DAL.GetXmtj(1, "");
            //dr["sxsq2"] = DAL.GetXmtj(1, "市区");
            //dr["xsq2"] = DAL.GetXmtj(1, "锡山");
            //dr["hsq2"] = DAL.GetXmtj(1, "惠山");
            //dr["bhq2"] = DAL.GetXmtj(1, "滨湖");
            //dr["xq2"] = DAL.GetXmtj(1, "新区");
            //dr["jy2"] = DAL.GetXmtj(1, "江阴");
            //dr["yx2"] = DAL.GetXmtj(1, "宜兴");

            //dr["jsdwNo"] = DAL.GetQytj("jsdw");
            //dr["kcsjdwNo"] = DAL.GetQytj("kcsjdw");
            //dr["sgdwNo"] = DAL.GetQytj("sgdw");
            //dr["zjjgNo"] = DAL.GetQytj("zjjg");
            //dr["qtNo"] = DAL.GetQytj("qt");

            //dr["jzsNo"] = DAL.GetRytj(1);
            //dr["jlsNo"] = DAL.GetRytj(21);
            //dr["zjsNo"] = DAL.GetRytj(41);
            //dr["jzhusNo"] = DAL.GetRytj(51);
            //dr["jgsNo"] = DAL.GetRytj(61);
            //dr["alNo"] = DAL.GetRytj(4);
            //dr["blNo"] = DAL.GetRytj(5);
            //dr["clNo"] = DAL.GetRytj(6);
            //dr["jsryNo"] = DAL.GetRytj(71);
            //dr["jjryNo"] = DAL.GetRytj(72);

            //dt.Rows.Add(dr);
            return dt;
        }

        public DataTable GetXmtj(int aqjdflag)
        {
            DataTable dt = new DataTable();
            //工程项目
            dt.Columns.Add("gcxm", typeof(string));
            dt.Columns.Add("sxsq", typeof(string));
            dt.Columns.Add("xsq", typeof(string));
            dt.Columns.Add("hsq", typeof(string));
            dt.Columns.Add("bhq", typeof(string));
            dt.Columns.Add("xq", typeof(string));
            dt.Columns.Add("jy", typeof(string));
            dt.Columns.Add("yx", typeof(string));

            DataRow dr = dt.NewRow();
            dr["gcxm"] = DAL.GetXmtj(aqjdflag, "");
            dr["sxsq"] = DAL.GetXmtj(aqjdflag, "市区");
            dr["xsq"] = DAL.GetXmtj(aqjdflag, "锡山");
            dr["hsq"] = DAL.GetXmtj(aqjdflag, "惠山");
            dr["bhq"] = DAL.GetXmtj(aqjdflag, "滨湖");
            dr["xq"] = DAL.GetXmtj(aqjdflag, "新区");
            dr["jy"] = DAL.GetXmtj(aqjdflag, "江阴");
            dr["yx"] = DAL.GetXmtj(aqjdflag, "宜兴");

            dt.Rows.Add(dr);
            return dt;


        }

        public DataTable GetSczt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("jsdwNo", typeof(string));
            dt.Columns.Add("kcsjdwNo", typeof(string));
            dt.Columns.Add("sgdwNo", typeof(string));
            dt.Columns.Add("zjjgNo", typeof(string));
            dt.Columns.Add("qtNo", typeof(string));
            DataRow dr = dt.NewRow();
            dr["jsdwNo"] = DAL.GetQytj("jsdw");
            dr["kcsjdwNo"] = DAL.GetQytj("kcsjdw");
            dr["sgdwNo"] = DAL.GetQytj("sgdw");
            dr["zjjgNo"] = DAL.GetQytj("zjjg");
            dr["qtNo"] = DAL.GetQytj("qt");
            dt.Rows.Add(dr);
            return dt;
        }

        public DataTable GetZyry()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("jzsNo", typeof(string));
            dt.Columns.Add("jlsNo", typeof(string));
            dt.Columns.Add("zjsNo", typeof(string));
            dt.Columns.Add("jzhusNo", typeof(string));
            dt.Columns.Add("jgsNo", typeof(string));
            dt.Columns.Add("alNo", typeof(string));
            dt.Columns.Add("blNo", typeof(string));
            dt.Columns.Add("clNo", typeof(string));
            dt.Columns.Add("jsryNo", typeof(string));
            dt.Columns.Add("jjryNo", typeof(string));
            DataRow dr = dt.NewRow();
            dr["jzsNo"] = DAL.GetRytj(1);
            dr["jlsNo"] = DAL.GetRytj(21);
            dr["zjsNo"] = DAL.GetRytj(41);
            dr["jzhusNo"] = DAL.GetRytj(51);
            dr["jgsNo"] = DAL.GetRytj(61);
            dr["alNo"] = DAL.GetRytj(4);
            dr["blNo"] = DAL.GetRytj(5);
            dr["clNo"] = DAL.GetRytj(6);
            dr["jsryNo"] = DAL.GetRytj(71);
            dr["jjryNo"] = DAL.GetRytj(72);
            dt.Rows.Add(dr);
            return dt;
        }

        #endregion

    }
}
