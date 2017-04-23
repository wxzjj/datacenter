<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectFxr_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.SelectFxr_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>实施单位管理</title>
    <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../../SparkClient/DateTime_ligerui.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />

    <script src="../../SparkClient/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/ligerui.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

</head>
<body style="padding: 5px; height: 100%">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td>
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td width="15%" class="td-text">
                            用户类型
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_jsdw" runat="server" ItemName="UserType"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            用户名
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_zzjghm" runat="server" ItemName="UserName"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value" style="text-align: right">
                            <Bigdesk8:SubmitButton CssClass="button button-s" ID="btnSearch" runat="server" Text="搜索"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="td-powergridview">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div>
                            <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                                AllowPaging="True" PageSize="10" OnPageIndexChanging="gridView_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="UserType" HeaderText="用户类型" ItemStyle-Width="25%">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="18%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="用户名" ItemStyle-Width="25%">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="10%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                        <HeaderTemplate>
                                            选择
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a  onclick='<%#string.Format("javascript:f_selectFxr(\"{0}\",\"{1}\");",Eval("UserID"),Eval("UserName")) %>'
                                                href="##">选择</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </Bigdesk8:PowerDataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button CssClass="button button-s" ID="btnRefresh" Text="刷新" runat="server" OnClick="btnRefresh_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">

        var activeDialog = null;


        function f_add() {
            OpenWindow('SsdwxxEdit.aspx?operate=add', "新增实施单位", 550, 300, true);
        }
        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, buttons: [
            { text: '保存', onclick: function(item, dialog) {
                dialog.frame.f_save(dialog, $("[id$='btnRefresh']"));
            }
            },
            { text: '关闭', onclick: function(item, dialog) {
                dialog.close();
            }
            }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }
        function OpenWindowView(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
            { text: '关闭', onclick: function(item, dialog) {
                if (isReload) {
                    f_reload();
                }
                dialog.close();
            }
            }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }
        function showSuccess(mes) {
            $.ligerDialog.success(msg);
        }
        function showError(msg) {
            $.ligerDialog.error(msg);
        }
        function showInfo(msg) {
            $.ligerDialog.alert(msg);
        }

        function f_selectFxr(id, name) {
            parent.f_setFxr(id, name);
        }
     
    </script>

    </form>
</body>
</html>
