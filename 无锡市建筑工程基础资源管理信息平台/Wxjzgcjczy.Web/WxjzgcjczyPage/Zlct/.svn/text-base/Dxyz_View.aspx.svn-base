<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxyz_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxyz_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>短信简报查看</title>
      <%--<link href="../Common/css/base.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Common/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../SparkClient/DateTime.js" type="text/javascript"></script>

</head>
<body style="padding: 0px 5px 0px 5px;">
    <form id="form1" name="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" width="100%">
        <tr>
            <td colspan="4" style=" display:block;">
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;">
                    <tr style="height: 20px;">
                        <td width="100%" class="td-value" colspan="4">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <input type="button" class="btn_long" value="返回" onclick='javascript:f_back();' />
                                     <div style=" display:none;">
                                      <Bigdesk8:DBTextBox ID="lblPageIndex" runat="server" ></Bigdesk8:DBTextBox>
                                     </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;">
                    <tr style="height: 20px;">
                        <td width="15%" class="td-text">
                            简报名称
                        </td>
                        <td width="85%" class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="db_Jbmc" runat="server" ItemName="Jbmc"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr style="height: 140px;">
                        <td width="15%" class="td-text">
                            简报内容
                        </td>
                        <td width="85%" class="td-value" colspan="3" style="padding: 2px; vertical-align: text-top;">
                            <Bigdesk8:DBMemo ID="db_Jbnr" runat="server" ItemName="Jbnr" Height="140px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr >
                        <td width="15%" class="td-text">
                           发送类型
                        </td>
                        <td width="35%" class="td-value" >
                           <Bigdesk8:DBText ID="db_fslx" runat="server" ></Bigdesk8:DBText>
                        </td>  
                        <td width="15%" class="td-text">
                            定时发送周期
                        </td>
                        <td width="35%" class="td-value" >
                               <Bigdesk8:DBMemo ID="db_fszq" runat="server"  ></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    
                    
                    <tr style="height: 140px;">
                        <td width="15%" class="td-text">
                            通讯录收信人
                        </td>
                        <td width="85%" class="td-value" colspan="3" style="padding: 2px; vertical-align: top;">
                         <Bigdesk8:DBMemo ID="db_fsdx" runat="server" Width="99%" Height="99%" ></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr style="height: 100px;">
                        <td width="15%" class="td-text">
                            新增收信手机号码
                        </td>
                        <td width="85%" class="td-value" colspan="3" style="padding: 2px;">
                            <Bigdesk8:DBMemo ID="db_newSxr" runat="server" ItemName="SendPhone"  ></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
      
        function showMessage(mes) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(mes);
        }
        function showError(mes) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.error(mes);
        }
        function showWarn(mes) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.warn(mes);
        }
        function showSuccess(mes) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.success(mes);
        }
        function f_back() {

            window.location = "Dxyz_List.aspx?pageIndex=" + $("[id$='lblPageIndex']").val();
        }
    </script>

    </form>
</body>
</html>
