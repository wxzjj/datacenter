<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxyz_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxyz_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <%--  <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../Common/css/Style.css" rel="stylesheet" type="text/css" />--%>
    <%--    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />--%>

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

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <style>
        .linkButtonCss
        {
            color: #000066;
            text-decoration: none;
        }
    </style>
</head>
<body style="background-color: rgb(238,238,238);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div style="width: 100%;">
            <asp:LinkButton ID="lnkAddDxjb" runat="server" Text="预制短信简报" OnClick="lnkAddDxjb_Click" />
            <%--   <asp:LinkButton ID="lnkDxjbSendRecord" runat="server" Text="短信简报发送记录" OnClick="lnkDxjbSendRecord_Click" />--%>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-top: 2px;">
                    第<asp:Label ID="lblCurrentPage" runat="server" ForeColor="Red"></asp:Label>页 &nbsp;共<asp:Label
                        ID="lblPageCount" runat="server" ForeColor="Red"></asp:Label>页 &nbsp;每页显示<asp:Label
                            ID="lblCountPerPage" runat="server" ForeColor="Red"></asp:Label>
                    条&nbsp; 共<asp:Label ID="lblAllCount" runat="server" ForeColor="Red"></asp:Label>条
                    &nbsp;&nbsp; &nbsp;
                    <asp:LinkButton ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />
                    <asp:LinkButton ID="btnUp" runat="server" Text="上一页" OnClick="btnUp_Click" />
                    <asp:LinkButton ID="btnNext" runat="server" Text="下一页" OnClick="btnNext_Click" />
                    <asp:LinkButton ID="btnLast" runat="server" Text="末页" OnClick="btnLast_Click" />
                    转到第<asp:DropDownList ID="ddlTurnToPage" runat="server" Width="50px" OnSelectedIndexChanged="ddlTurnToPage_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    页
                </div>
                <div style="display: none;">
                    <Bigdesk8:SubmitButton ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click" />
                    <Bigdesk8:DBTextBox ID="lblPageIndex" runat="server"></Bigdesk8:DBTextBox>
                </div>
                <asp:DataList ID="dlDxjbxx" runat="server" RepeatColumns="4" OnItemDataBound="dlDxjbxx_ItemDataBound">
                    <ItemStyle Width="290px" />
                    <ItemTemplate>
                        <div style="width: 290px; height: 200px; border: solid 1px rgb(0,100,165); margin-top: 3px;">
                            <table width="100%" style="margin: 5px;">
                                <tr>
                                    <td>
                                        <img width="80px" height="80px"  src="../Common/images/jianbao.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       简报名称： <a href='##' onclick='<%#string.Format("javascript:f_view({0});",Eval("DxjbId")) %>' class="linkButtonCss"> <%#Eval("Jbmc")%></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>发送类型：<%# (Eval("IsDsfs").ToString().ToLower() == "true") ? "自动发送，"+(Int32.Parse(Eval("EveryWeekOne").ToString()) == 1 ? "每周发送一次" : (Int32.Parse(Eval("EveryMonthOne").ToString()) == 1 ? "每月发送一次" : "每季度发送一次")) : "手动发送"%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href='##' onclick='<%#string.Format("javascript:f_edit({0});",Eval("DxjbId")) %>'
                                            class="linkButtonCss">编辑</a>
                                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%#Eval("IsDsfs")%>' />
                                        <asp:HiddenField ID="hiddenDxjbId" runat="server" Value='<%#Eval("DxjbId")%>' />
                                        <asp:LinkButton ID="send" runat="server" PostBackUrl='Dxjb_Send.aspx?dxjbId=' Text="发送"
                                            CssClass="linkButtonCss" Visible="false"></asp:LinkButton>
                                       <a href='##' class="linkButtonCss" onclick='javascript:f_delete(<%#Eval("DxjbId") %>)'>
                                                删除</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script>

        function f_edit(dxjbId) {
            window.location = "Dxyz_Edit.aspx?operate=edit&dxjbId=" + dxjbId + "&pageIndex=" + $("[id$='lblPageIndex']").val();
        }
        function f_view(dxjbId) {
            window.location = "Dxyz_View.aspx?dxjbId=" + dxjbId + "&pageIndex=" + $("[id$='lblPageIndex']").val();
        }
        function f_delete(dxjbId) {
            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");
                     $.getJSON('../Handler/Delete.ashx?operate=deleteSzgkjc_Dxjb_YzJb&id=' + dxjbId, { Rnd: Math.random() }, function(data) {
                         $.ligerDialog.closeWaitting();

                         if (data.Type == "Success") {
                             showMessage(data.Message);
                             $("[id$='btnRefresh']").click();
                         }
                         else {
                             showError(data.Message);
                         }
                     });
                 }
             });
        }
        function showWaitting() {
            parent.parent.$.ligerDialog.waitting("正在保存中,请稍后...");
        }

        function closeWaitting() {
            parent.parent.$.ligerDialog.closeWaitting();

        }
        function showMessage(mes) {
            parent.parent.$.ligerDialog.alert(mes);
        }
        function showError(mes) {
            parent.parent.$.ligerDialog.error(mes);
        }
    </script>

    </form>
</body>
</html>
