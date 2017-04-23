<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxyz_Edit.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxyz_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>预制短信简报</title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
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

    <script src="../../SparkClient/DateTime_ligerui.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateTime.js" type="text/javascript"></script>

    <style>
        .JbnrCss
        {
            padding: 3px;
            word-spacing: 5px;
        }
    </style>
</head>
<body style="padding: 0px 5px 0px 5px;">
    <form id="form1" name="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="layout1">
        <div position="center" title='<%=content %>'>
            <table border="0" width="100%">
                <tr>
                    <td colspan="4">
                        <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;">
                            <tr style="height: 20px;">
                                <td width="100%" class="td-value" colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                        <div style="margin-top:5px;">
                                            <input type="button" class="button button-s" style="width:80px;height:30px;" value="返回" onclick='javascript:f_back();' />
                                            <input id="Save" type="button" value="保存" onclick="onSave()" class="button button-s" style="width:80px;height:30px;" />
                                            <div style="display: none;">
                                                <asp:Button ID="btn_send" Text="保存" runat="server" OnClick="btn_send_Click" />
                                                <Bigdesk8:DBTextBox ID="txtReceiverIds" runat="server"></Bigdesk8:DBTextBox>
                                                <Bigdesk8:DBTextBox ID="lblPageIndex" runat="server"></Bigdesk8:DBTextBox>
                                            </div>
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
                        <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;" >
                            <tr style="height: 20px;">
                                <td width="15%" class="td-text">
                                    <span style="color: Red; font-weight: bold;">*</span>简报名称
                                </td>
                                <td width="35%" class="td-value" colspan="3">
                                    <Bigdesk8:DBTextBox ID="db_Jbmc" runat="server" ItemName="Jbmc" ItemIsRequired="true"
                                        Width="80%" ItemNameCN="简报名称"></Bigdesk8:DBTextBox>
                                </td>
                            </tr>
                            <tr style="height: 150px;">
                                <td width="15%" class="td-text">
                                    <span style="color: Red; font-weight: bold;">*</span>简报内容<br />
                                    (sql脚本要放在""####""内，如：##select * from table1;##)
                                </td>
                                <td width="35%" class="td-value" colspan="3" style="padding: 2px;">
                                    <Bigdesk8:DBMemoBox ID="db_Jbnr" runat="server" ItemName="Jbnr" Height="150px" ItemIsRequired="true"
                                        CssClass="JbnrCss" Width="99%" ItemNameCN="简报内容"></Bigdesk8:DBMemoBox>
                                </td>
                            </tr>
                            <tr style="height: 20px;">
                                <td width="15%" class="td-text">
                                    是否定时发送
                                </td>
                                <td width="35%" class="td-value">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <Bigdesk8:DBCheckBox ID="db_IsDsfs" runat="server" ItemName="IsDsfs" AutoPostBack="true"
                                                Text="是" OnCheckedChanged="db_IsDsfs_CheckedChanged"></Bigdesk8:DBCheckBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td width="15%" class="td-text">
                                    定时发送频率
                                </td>
                                <td width="35%" class="td-value">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButton GroupName="aa" ID="db_dsfs_1" runat="server" Text="每周发送一次" />
                                            <br />
                                            <asp:RadioButton ID="db_dsfs_2" runat="server" GroupName="aa" Text="每月发送一次" /><br />
                                            <asp:RadioButton ID="db_dsfs_3" runat="server" GroupName="aa" Text="每季度发送一次" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 80px;">
                                <td width="15%" class="td-text">
                                    通讯录收信人
                                </td>
                                <td width="35%" class="td-value" colspan="3" style="padding: 2px; vertical-align: top;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <Bigdesk8:DBMemo ID="db_fsdx" runat="server" Width="99%" ItemNameCN="已选通讯录收信人"></Bigdesk8:DBMemo>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 80px;">
                                <td width="15%" class="td-text">
                                    新增收信手机号码<br />
                                    (多个用逗号隔开)
                                </td>
                                <td width="85%" class="td-value" colspan="3" style="padding: 2px;">
                                    <Bigdesk8:DBMemoBox ID="db_newSxr" runat="server" ItemName="SendPhone" Height="80px"
                                        Width="99%"></Bigdesk8:DBMemoBox>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
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

        var dia;
        var mana;
        var btn;
        var win = parent.parent.parent;
        var activeDialog;

        var f_setFxr = function(id, name) {
            $("[id$='db_userId']").val(id);
            $("[id$='db_fxr']").val(name);
            activeDialog.hide();
        };

        function f_back() {
            window.location = "../Zlct/Dxyz_List.aspx?pageIndex=" + $("[id$='lblPageIndex']").val();
        }


        function f_send(dialog, manager) {
            dia = dialog;
            mana = manager;
            win.$.ligerDialog.waitting("正在保存中,请稍后...");

            if ($("[id$='txtReceiverIds']").val() == ",") {
                showWarn("请选择收信人！");
                return;
            }
            $("[id$='btn_send']").click();
        }
        function f_save(dialog, manager) {
            dia = dialog;
            mana = manager;
            win.$.ligerDialog.waitting("正在保存中,请稍后...");

            if ($("[id$='txtReceiverIds']") == ",") {
                showWarn("请选择收信人！");
                return;
            }
            $("[id$='btn_save']").click();
        }

        function onSave() {
            var operate = '<%=Operate %>';
            if (operate == "edit") {
                win.$.ligerDialog.confirm('确定要保存当前简报的修改吗？', function(b) {
                    if (b) {
                        win.$.ligerDialog.waitting("正在保存中,请稍后...");
                        $("[id$='btn_send']").click();
                    }
                });
            }
            else {
                win.$.ligerDialog.waitting("正在保存中,请稍后...");
                $("[id$='btn_send']").click();

            }
        }

        function SaveResult(mes) {

            win.$.ligerDialog.closeWaitting();
            // var win = window||parent;
            win.LG.showSuccess(mes, function() {
                f_back();
                //                dia.hide();
                //                mana.loadData(true);
            });
        }

        function f_selectFxr() {

            OpenWindow("SelectFxr_List.aspx", "选择发信人", 800, 400, false);
        }

        function showMessage(mes) {
            win.$.ligerDialog.closeWaitting();
            win.$.ligerDialog.alert(mes);
        }
        function showError(mes) {
            win.$.ligerDialog.closeWaitting();
            win.$.ligerDialog.error(mes);
        }
        function showWarn(mes) {
            win.$.ligerDialog.closeWaitting();
            win.$.ligerDialog.warn(mes);
        }
        function showSuccess(mes) {
            win.$.ligerDialog.closeWaitting();
            win.$.ligerDialog.success(mes);
        }
        $(function() {
            $("#layout1").ligerLayout({ rightWidth: 300, height: 600, space: 2 });
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



        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [

                 { text: '发送', onclick: function(item, dialog) {
                     dialog.frame.f_send(dialog, manager);
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
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }


    </script>

    </form>
</body>
</html>
