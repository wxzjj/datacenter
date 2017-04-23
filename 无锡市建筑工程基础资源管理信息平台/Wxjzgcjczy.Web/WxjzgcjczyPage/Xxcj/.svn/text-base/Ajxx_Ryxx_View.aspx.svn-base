<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajxx_Ryxx_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Ajxx_Ryxx_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增/编辑项目人员信息</title>
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

    <script src="../../MyDatePicker/WdatePicker.js" type="text/javascript"></script>
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
                        <td class="td-text" style="width: 15%;">
                            人员姓名
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_UserName" runat="server" ItemName="UserName" ItemIsRequired="true"
                                ItemNameCN="人员姓名" ></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            安全生产管理人员类型
                        </td>
                        <td width="35%" class="td-value">
                                <Bigdesk8:DBDropDownList ID="ddl_UserType" runat="server"   ItemName="UserType"  Enabled="false">
                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="1" Text="主要负责人"></asp:ListItem>
                                <asp:ListItem Value="2" Text="项目负责人"></asp:ListItem>
                                <asp:ListItem Value="3" Text="安全员"></asp:ListItem>
                                
                                </Bigdesk8:DBDropDownList>
                                
                        </td>
                    </tr>
                                       <tr>
                        <td class="td-text" style="width: 15%;">
                           所属单位名称
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_CorpName" runat="server" ItemName="CorpName" ItemIsRequired="true"
                                ItemNameCN="所属单位名称" ></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                           所属单位组织机构代码（社会信用代码）
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_CorpCode" runat="server" ItemName="CorpCode" ItemIsRequired="true"
                                ItemNameCN="所属单位组织机构代码（社会信用代码）"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span id="Span3" runat="server" style="color: Red; font-weight: bold;">*</span>安全监督编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_aqjdbm" runat="server" ItemIsRequired="true" ItemName="aqjdbm"
                                ItemNameCN="安全监督编码"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                           安全生产许可证编号
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_SafetyCerID" runat="server" ItemName="SafetyCerID" ItemIsRequired="true"
                                ItemNameCN="安全生产许可证编号"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            证件类型
                        </td>
                        <td class="td-value" style="width: 35%;">
                         <Bigdesk8:DBDropDownList ID="ddl_IDCardType" runat="server"   ItemName="IDCardTypeNum" Enabled="false"></Bigdesk8:DBDropDownList>
                        </td>
                        <td width="15%" class="td-text">
                             人员证件号码
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_IDCard" runat="server" ItemName="IDCard" ItemIsRequired="true" ItemNameCN="人员证件号码"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            人员电话
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_UserPhone" runat="server" ItemName="UserPhone"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                        安全生产考核合格证书编号
                        </td>
                        <td width="35%" class="td-value">
                         <Bigdesk8:DBText ID="db_CertID" runat="server" ItemName="CertID"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>

    <script type="text/javascript">
        var dia;
        var mana;
        var activeDialog;

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


        function OpenWindow(url, title, width, height) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, buttons: [

            { text: '关闭', onclick: function(item, dialog) {
                dialog.close();
            }
            }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function OnXmSelectHandler(pkid,prjNum,prjName) {

            $("[id$='db_LxPrjName']").val(prjName);
            $("[id$='db_PrjNum']").val(prjNum);

            if (activeDialog)
                activeDialog.close();
        }

        $(function() {
           

        });
        
    </script>

    </form>
</body>
</html>

