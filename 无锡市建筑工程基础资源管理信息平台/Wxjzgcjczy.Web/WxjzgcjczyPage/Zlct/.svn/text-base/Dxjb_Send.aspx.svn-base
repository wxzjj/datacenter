<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxjb_Send.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxjb_Send" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发送短信简报</title>
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

</head>
<body style="padding: 0px 5px 0px 5px;">
    <form id="form1" name="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="layout1">
        <div position="center" title="短信简报发送">
            <table border="0" width="100%">
                <tr>
                    <td colspan="4">
                        <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="margin: 2px;">
                            <tr style="height: 20px;">
                                <td width="100%" class="td-value" colspan="4">
                                
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                        <div style="margin-top:5px;">
                                            <input type="button" class="button button-s" style="width:80px;height:30px;" value="返回" onclick='javascript:window.location="DxjbSendRecord_List.aspx"' />
                                            <asp:Button CssClass="button button-s" Width="80px" Height="30px" ID="btn_send" Text="发 送" runat="server" OnClick="btn_send_Click" /></div>
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
                                    <span style="color: Red; font-weight: bold;">*</span>选择预制简报
                                </td>
                                <td width="85%" class="td-value" colspan="3">
                                    <Bigdesk8:DBDropDownList ID="ddl_yzjb" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_yzjb_SelectedIndexChanged">
                                    </Bigdesk8:DBDropDownList>
                                </td>
                            </tr>
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
                            <tr style="height: 140px;">
                                <td width="15%" class="td-text">
                                    通讯录收信人
                                </td>
                                <td width="85%" class="td-value" colspan="3" style="padding: 2px; vertical-align: top;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <Bigdesk8:DBMemo ID="db_fsdx" runat="server" Width="99%" Height="99%" ItemNameCN="定时发送对象"></Bigdesk8:DBMemo>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 100px;">
                                <td width="15%" class="td-text">
                                    新增收信手机号码<br />
                                    (多个用逗号隔开)
                                </td>
                                <td width="85%" class="td-value" colspan="3" style="padding: 2px;">
                                    <Bigdesk8:DBMemoBox ID="db_newSxr" runat="server" ItemName="SendPhone" Height="100px"
                                        Width="99%"></Bigdesk8:DBMemoBox>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none;">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:Button CssClass="button button-s" ID="btn_refresh" Text="刷新" runat="server"
                                        OnClick="btn_refresh_Click" />
                                    <Bigdesk8:DBTextBox ID="txtReceiverIds" runat="server"></Bigdesk8:DBTextBox>
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
        function f_save(dialog, manager) {
            dia = dialog;
            mana = manager;
            $.ligerDialog.waitting("正在保存中,请稍后...");

            if ($("[id$='txtReceiverIds']") == ",") {
                showWarn("请选择收信人！");
                return;
            }
            $("[id$='btn_save']").click();
        }

        //编辑控件
        //        var g_kindEditor;
        //        KindEditor.ready(function(K) {
        //            g_kindEditor = K.create('#txtInfoContent', {
        //                cssPath: '../../kindeditor-4.1.4/plugins/code/prettify.js',
        //                uploadJson: '',
        //                fileManagerJson: '',
        //                allowFileManager: true,
        //                height: '330px',
        //                afterCreate: function() {
        //                    var self = this;
        //                    K.ctrl(document, 13, function() {
        //                        self.sync();
        //                        // K('form[name=form1]')[0].submit();
        //                    });
        //                    K.ctrl(self.edit.doc, 13, function() {
        //                        self.sync();
        //                        //K('form[name=form1]')[0].submit();
        //                    });
        //                }
        //            });
        //            prettyPrint();
        //        });

        function SaveResult(mes) {
            $.ligerDialog.closeWaitting();
            var win = window || parent;
            win.LG.showSuccess(mes, function() {
                window.location = "DxjbSendRecord_List.aspx";
               
            });
        }

        function ShowWarnResult(mes) {
            $.ligerDialog.closeWaitting();
            var win = window || parent;
            win.LG.showWarn(mes, function() {
                window.location = "DxjbSendRecord_List.aspx";
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
                sReceiverIds = sReceiverIds.replace("," + code , "");
            }

            //给隐藏控件txtReceiverIds添加新code值
            sReceiverIds +=  ","+code ;
            $("[id$='txtReceiverIds']").val(sReceiverIds);
            $("[id$='btn_refresh']").click();
            
        }
        function fDeleteSjr(code, bCanDelete) {
            //如果bCanDelete为true，执行删除功能
            if (bCanDelete) {
                var sReceiverIds = $("[id$='txtReceiverIds']").val();
                $("[id$='txtReceiverIds']").val(sReceiverIds.replace("," + code , ""));
            }
            $("[id$='btn_refresh']").click();
        }
    </script>

    </form>
</body>
</html>
