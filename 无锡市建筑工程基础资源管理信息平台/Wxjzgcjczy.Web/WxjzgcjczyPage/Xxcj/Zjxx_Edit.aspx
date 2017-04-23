<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjxx_Edit.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Zjxx_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增/编辑质监信息</title>
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
                            <span id="lxxmmcgf" runat="server" style="color: Red; font-weight: bold;" >*</span>立项项目
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_LxPrjName" runat="server" ItemName="PrjName" ItemIsRequired="true"
                                ItemNameCN="立项项目" Width="220px" ReadOnly="true"></Bigdesk8:DBTextBox>
                            
                         <asp:Button ID="btnSelectLxxm" runat="server" Text="选择立项项目"  Width="95px"  OnClientClick="return selectLxxm();" />
                            
                        </td>
                        <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>项目编号
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_PrjNum" runat="server" ItemName="PrjNum" ItemIsRequired="true"
                                ItemNameCN="立项编号" ReadOnly="true"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span   style="color: Red; font-weight: bold;">*</span>质量监督编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_aqjdbm" runat="server" ItemIsRequired="true" ItemName="zljdbm"
                                ItemNameCN="质量监督编码"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                            <span style="color: Red; font-weight: bold;">*</span>报监工程名称
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_gcmc" runat="server" ItemName="gcmc" ItemIsRequired="true"
                                ItemNameCN="报监工程名称"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            施工招标编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_sgzbbm" runat="server" ItemName="sgzbbm"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                        </td>
                        <td width="35%" class="td-value">
                        </td>
                    </tr>
                    <tr>
                      <td width="15%" class="td-text">
                             <span style="color: Red; font-weight: bold;">*</span>质量监督机构名称
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_zljdjgmc" runat="server" ItemIsRequired="true" ItemName="zljdjgmc"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" style="width: 15%;">
                             <span style="color: Red; font-weight: bold;">*</span>质量监督机构代码<br />
                             （社会信用代码）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_zjzbm" runat="server" ItemName="zjzbm"  ItemIsRequired="true" ItemNameCN="质量监督机构代码（社会信用代码）"></Bigdesk8:DBTextBox>
                        </td>
                      
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>工程造价（万元）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_gczj" runat="server" ItemName="gczj" ItemIsRequired="true" ItemNameCN="工程造价"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                           工程面积（平方米）
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_jzmj" runat="server" ItemName="jzmj" ></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    
                   <tr>
                        <td class="td-text" style="width: 15%;">
                           道路长度（米）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_dlcd" runat="server" ItemName="dlcd" ></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                          建筑规模
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="db_jzgm" runat="server" ItemName="jzgm" ></Bigdesk8:DBTextBox>
                        </td>
                    </tr>  
                
                    <tr>
                        <td class="td-text" style="width: 15%;">
                             <span style="color: Red; font-weight: bold;">*</span>结构类型
                        </td>
                        <td class="td-value" style="width: 35%;">
                               <Bigdesk8:DBDropDownList ID="ddl_jglx" runat="server"   ItemName="jglx" ItemIsRequired="true" ItemNameCN="结构类型" ></Bigdesk8:DBDropDownList>
                        </td>
                        <td width="15%" class="td-text">
                            层次
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" runat="server" ItemName="cc"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>报监日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_sbrq" runat="server" ItemType="Date" ItemName="sbrq" ItemIsRequired="true" ItemNameCN="报监日期"></Bigdesk8:DBTextBox>
                        </td>
                        <td width="15%" class="td-text">
                         形象进度
                        </td>
                        <td width="35%" class="td-value">
                        
                            <Bigdesk8:DBTextBox ID="db_xxjd" runat="server"  ItemName="xxjd" ></Bigdesk8:DBTextBox>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>开工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_kgrq" runat="server" ItemType="Date" ItemName="kgrq" ItemIsRequired="true" ItemNameCN="开工日期"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            <span style="color: Red; font-weight: bold;">*</span>计划竣工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBTextBox ID="db_jhjgrq" runat="server" ItemType="Date" ItemName="jhjgrq" ItemIsRequired="true" ItemNameCN="计划竣工日期"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemoBox ID="db_bz" runat="server" ItemName="bz" Width="85%" Height="80px"></Bigdesk8:DBMemoBox>
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
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, isResize: false, timeParmName: 'a'};
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function OnXmSelectHandler(pkid,prjNum,prjName) {

            if (activeDialog)
                activeDialog.hide();
                
            $("[id$='db_LxPrjName']").val(prjName);
            $("[id$='db_PrjNum']").val(prjNum);
         
        }


        function selectLxxm() {

            OpenWindow('LxxmSelect.aspx', "选择立项项目", 800, 450);
            return false;
        }

        $(function() {



        $("input[id$='db_sbrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });
            $("input[id$='db_kgrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });
            $("input[id$='db_jhjgrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });

        });
        
    </script>

    </form>
</body>
</html>
