<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gzzs_Gzzs_Edit.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Gzzs_Gzzs_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增工作指示信息</title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-layout.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../Common/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

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

    <script src="../../SparkClient/DateTime.js" type="text/javascript"></script>

    <%--    <link rel="stylesheet" href="../../kindeditor-4.1.4/themes/default/default.css" />
    <link rel="stylesheet" href="../../kindeditor-4.1.4/plugins/code/prettify.css" />

    <script type="text/javascript" charset="utf-8" src="../../kindeditor-4.1.4/kindeditor.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../kindeditor-4.1.4/lang/zh_CN.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../kindeditor-4.1.4/plugins/code/prettify.js"></script>--%>
    <style type="text/css">
        </style>
</head>
<body style="padding: 0px 5px 0px 5px;">
    <form id="form1" name="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="layout1">
        <div position="center" title="工作指示发送">
            <table border="0" width="100%">
                <tr>
                    <td style="width: 100%;">
                        <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;">
                            <tr style="height: 20px;">
                                <td width="15%" class="td-text">
                                    <span style="color: Red; font-weight: bold;">*</span>指示主题
                                </td>
                                <td width="35%" class="td-value" colspan="3">
                                    <Bigdesk8:DBTextBox ID="db_Gzzszt" runat="server" ItemName="Gzzszt" ItemIsRequired="true"
                                        Width="80%" ItemNameCN="指示主题"></Bigdesk8:DBTextBox>
                                </td>
                            </tr>
                            <tr style="height: 140px;">
                                <td width="15%" class="td-text">
                                    指示内容
                                </td>
                                <td width="35%" class="td-value" colspan="3" style="padding: 2px;">
                                    <Bigdesk8:DBMemoBox ID="db_GzzsNr" runat="server" ItemName="GzzsNr" Height="140px"
                                        ItemIsRequired="true" Width="99%" ItemNameCN="指示内容"></Bigdesk8:DBMemoBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" class="td-text">
                                    发送短信
                                </td>
                                <td width="35%" class="td-value" colspan="3" style="padding: 2px;">
                                    <Bigdesk8:DBCheckBox ID="db_cbFsdx" runat="server" ItemName="IsDxfs" Text="是" />
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" class="td-text">
                                    发送对象
                                </td>
                                <td width="35%" class="td-value" colspan="3" style="padding: 2px;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <Bigdesk8:DBMemo ID="db_fsdx" runat="server" Width="99%" ItemNameCN="发送对象"></Bigdesk8:DBMemo>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button CssClass="button button-s" ID="btn_send" Text="发送" runat="server" OnClick="btn_send_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div style="display: none;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <Bigdesk8:DBTextBox ID="txtReceiverIds" runat="server"></Bigdesk8:DBTextBox>
                                    <asp:Button CssClass="button button-s" ID="btn_refresh" Text="刷新" runat="server"
                                        OnClick="btn_refresh_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div position="right" id="rightmenu" title="收信人员列表">
            <iframe frameborder="0" id="Iframe1" name="IframeTreeTxl" src='<%=Src %>' width="100%"
                height="600px" bordercolor="white" scrolling="no" style="border: none;"></iframe>
        </div>
    </div>

    <script type="text/javascript">

        //编辑控件
        //  var g_kindEditor;

        var dia;
        var btn;

        function f_send(dialog, manager) {
            dia = dialog;
            mana = manager;
            $.ligerDialog.waitting("正在保存中,请稍后...");

            if ($("[id$='txtReceiverIds']").val() == ",") {
                showWarn("请选择收信人！");
                return;
            }

            $("[id$='btn_send']").click();
        }


        function SaveResult(mes) {
            $.ligerDialog.closeWaitting();
            var win = window || parent;
            win.LG.showSuccess(mes, function() {
                dia.hide();
                mana.loadData(true);
            });

        }
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
        $(function() {
            $("#layout1").ligerLayout({ rightWidth: 300, height: '100%', space: 2 });
            /* 初始化为一个“ , ”，
            后面再加的时候用“ += code + "," ”这样的方式，
            最后字符串会形成“ ,2,12,16, ”这样的形式，
            即可以保证每个数两边都有“ , ” ，
            这样在判断是否有字符“ 2 ”存在的时候，
            用“ ,2, ”字符形式进行判断，
            不会与“ 12...21...25...32...42 ”等带2的字符,
            因为相像而导致判断错误。 */
            //$("[id$='txtReceiverIds']").val(',');
        });

        function fSelectedSjr(code, name) {
            var sReceiverIds = $("[id$='txtReceiverIds']").val();

            if (sReceiverIds.indexOf("," + code + ",") != -1) {
                //如果联系人已在其他组中选中，则先进行删除
                sReceiverIds = sReceiverIds.replace("," + code + ",", ",");
            }

            //给隐藏控件txtReceiverIds添加新code值
            sReceiverIds += code + ",";
            $("[id$='txtReceiverIds']").val(sReceiverIds);

            $("[id$='btn_refresh']").click();
        }
        function fDeleteSjr(code, bCanDelete) {
            //如果bCanDelete为true，执行删除功能
            if (bCanDelete) {
                var sReceiverIds = $("[id$='txtReceiverIds']").val();
                $("[id$='txtReceiverIds']").val(sReceiverIds.replace("," + code + ",", ","));
            }
            $("[id$='btn_refresh']").click();
        }
    </script>

    </form>
</body>
</html>
