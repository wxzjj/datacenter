<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yhxx_Edit.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl.Yhxx_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单位工程信息</title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/ligerui.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/json2.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/ligerui.expand.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td style="padding: 3px;">
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        
                    <tr>
                        <td class="td-text" style=" width:15%;">
                            <span id="lxxmmcgf" runat="server" style="color: Red; font-weight: bold;">*</span>用户姓名
                        </td>
                        <td class="td-value" style=" width:35%;">
                            <Bigdesk8:DBTextBox ID="db_lxxmmc" runat="server" ItemName="USERNAME"  ItemIsRequired="true"
                                ItemNameCN="用户姓名"></Bigdesk8:DBTextBox>
                          
                        </td>
                          <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>所属单位
                        </td>
                        <td width="35%" class="td-value" >
                            <Bigdesk8:DBTextBox ID="db_xmdz" runat="server" ItemName="ORGUNITNAME" ItemIsRequired="true"
                                ItemNameCN="所属单位"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                     <td  class="td-text" style=" width:15%;">
                            <span id="Span1" runat="server" style="color: Red; font-weight: bold;">*</span>登录名
                        </td>
                        <td class="td-value" style=" width:35%;">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" runat="server"  ItemIsRequired="true" ItemName="LOGINNAME"
                                ItemNameCN="登录名"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>登录密码
                        </td>
                        <td width="35%" class="td-value" >
                            <Bigdesk8:DBTextBox ID="db_LOGINPASSWORD" runat="server" ItemName="LOGINPASSWORD" ItemIsRequired="true"
                                ItemNameCN="登录密码" Text="123456"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="display: none;">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div>
                            <Bigdesk8:SubmitButton ID="btn_save" runat="server" OnClick="btn_save_Click" Text="保存" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <script type="text/javascript">
        var dia;
        var mana;
        function f_save(dialog, manager) {
            dia = dialog;
            mana = manager;
            $.ligerDialog.waitting("正在保存中,请稍后...");
            $("#btn_save").click();
        }

        function f_SaveResult(mes) {
            $.ligerDialog.closeWaitting();
            window.LG.showSuccess(mes, function() {
                dia.hide();
                mana.loadData(true);
            });
        }

        function showMsg(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(msg);
        }
        function showWarn(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.warn(msg);
        }
        function showError(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.error(msg);
        }
    </script>
    </form>
</body>
</html>
