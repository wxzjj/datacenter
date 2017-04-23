<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjxx_Ryxx_Edit.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Zjxx_Ryxx_Edit" %>
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                            <span style="color: Red; font-weight: bold;">*</span>序号
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_xh" runat="server" ItemIsRequired="true" ItemName="xh"
                                ItemNameCN="序号"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>上报地区
                        </td>
                        <td class="td-value" style="width: 35%;">
                         <Bigdesk8:DBDropDownList ID="ddl_sbdqbm" runat="server" ItemName="sbdqbm" ItemIsRequired="true"  ItemNameCN="上报地区">  
                         </Bigdesk8:DBDropDownList>
                         
                       
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>单位名称
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_dwmc" runat="server" ItemName="dwmc" ItemIsRequired="true"
                                ItemNameCN="所属单位名称"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>单位组织机构代码（社会信用代码）
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_dwdm" runat="server" ItemName="dwdm" ItemIsRequired="true"
                                ItemNameCN="单位组织机构代码（社会信用代码）"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                       <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>单位类型
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBDropDownList ID="ddl_dwlx" runat="server" ItemName="dwlx" ItemIsRequired="true"
                                ItemNameCN="单位类型">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="建设单位" Text="建设单位"></asp:ListItem>
                                <asp:ListItem Value="勘察单位" Text="勘察单位"></asp:ListItem>
                                <asp:ListItem Value="设计单位" Text="设计单位"></asp:ListItem>
                                <asp:ListItem Value="施工单位" Text="施工单位"></asp:ListItem>
                                <asp:ListItem Value="监理单位" Text="监理单位"></asp:ListItem>
                                <asp:ListItem Value="质量检测机构" Text="质量检测机构"></asp:ListItem>
                                <asp:ListItem Value="混凝土供应商" Text="混凝土供应商"></asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>项目负责人
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_xmfzrxm" runat="server" ItemName="xmfzrxm" ItemIsRequired="true"
                                ItemNameCN="项目负责人"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>项目负责人身份证号
                        </td>
                        <td class="td-value" style="width: 35%;">
                               <Bigdesk8:DBTextBox ID="db_xmfzrdm" runat="server" ItemName="xmfzrdm" ItemIsRequired="true"
                                ItemNameCN="项目负责人身份证号"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            项目负责人电话
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_xmfzr_lxdh" runat="server" ItemName="xmfzr_lxdh" ></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            项目技术负责人
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_UserPhone" runat="server" ItemName="jsfzr"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            项目技术负责人电话
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_jsfzr_lxdh" runat="server" ItemName="jsfzr_lxdh"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>  
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            质量员
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_zly" runat="server" ItemName="zly"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                           质量员电话
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_zly_lxdh" runat="server" ItemName="zly_lxdh"></Bigdesk8:DBTextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            取样员
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_qyy" runat="server" ItemName="qyy"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                           取样员电话
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_qyy_lxdh" runat="server" ItemName="qyy_lxdh"></Bigdesk8:DBTextBox>
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

        function OnXmSelectHandler(pkid, prjNum, prjName) {

            $("[id$='db_LxPrjName']").val(prjName);
            $("[id$='db_PrjNum']").val(prjNum);

            if (activeDialog)
                activeDialog.close();
        }

        $(function() {
            $("#btnSelectLxxm").click(function() {

                OpenWindow('LxxmSelect.aspx', "选择立项项目", 800, 400);
            });


            $("input[id$='bjrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });
            $("input[id$='db_gcgk_kgrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });
            $("input[id$='db_gcgk_jhjgrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });

        });
        
    </script>

    </form>
</body>
</html>
