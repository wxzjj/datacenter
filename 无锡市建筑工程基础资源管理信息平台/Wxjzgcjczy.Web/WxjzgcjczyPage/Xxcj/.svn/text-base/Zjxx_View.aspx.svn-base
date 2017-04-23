<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjxx_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Zjxx_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看质监信息</title>
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
<body style=" margin:0; padding:0;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td >
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            立项项目
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_LxPrjName" runat="server" ItemName="PrjName"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            项目编号
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_PrjNum" runat="server" ItemName="PrjNum" ItemIsRequired="true"
                                ItemNameCN="立项编号" ReadOnly="true"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            质量监督编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_aqjdbm" runat="server" ItemIsRequired="true" ItemName="zljdbm"
                                ItemNameCN="质量监督编码"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            报监工程名称
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_gcmc" runat="server" ItemName="gcmc" ItemIsRequired="true"
                                ItemNameCN="报监工程名称"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            施工招标编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_sgzbbm" runat="server" ItemName="sgzbbm"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                        </td>
                        <td width="35%" class="td-value">
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" class="td-text">
                            质量监督机构名称
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_zljdjgmc" runat="server" ItemIsRequired="true" ItemName="zljdjgmc"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            质量监督机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zjzbm" runat="server" ItemName="zjzbm" ItemIsRequired="true"
                                ItemNameCN="质量监督机构代码（社会信用代码）"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            工程造价（万元）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_gczj" runat="server" ItemName="gczj" ItemIsRequired="true"
                                ItemNameCN="工程造价"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            工程面积（平方米）
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_jzmj" runat="server" ItemName="jzmj"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            道路长度（米）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_dlcd" runat="server" ItemName="dlcd"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            建筑规模
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_jzgm" runat="server" ItemName="jzgm"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            结构类型
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBDropDownList ID="ddl_jglx" runat="server" ItemName="jglx"  Enabled="false">
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td width="15%" class="td-text">
                            层次
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="DBTextBox2" runat="server" ItemName="cc"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            报监日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_sbrq" runat="server" ItemType="Date" ItemName="sbrq" ItemIsRequired="true"
                                ItemNameCN="报监日期"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            形象进度
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_xxjd" runat="server" ItemName="xxjd"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            开工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_kgrq" runat="server" ItemType="Date" ItemName="kgrq" ItemIsRequired="true"
                                ItemNameCN="开工日期"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            计划竣工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jhjgrq" runat="server" ItemType="Date" ItemName="jhjgrq"
                                ItemIsRequired="true" ItemNameCN="计划竣工日期"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="db_bz" runat="server" ItemName="bz" ></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      
    </table>
     <div id="maingrid" style="background-color: White;">
                </div>

    <script type="text/javascript">
        var dia;
        var mana;
        var activeDialog;


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
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, isResize: false, timeParmName: 'a' };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function f_view(pkid) {
            OpenWindow('../Xxcj/Zjxx_Ryxx_View.aspx?pkid=' + pkid, "查看质监人员信息", 800, 400, false);
        
        }


        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [
     { display: '序号', name: 'xh', align: 'center', type: "text", width: "4%" },
   { display: '质量监督编码', name: 'zljdbm', align: 'center', type: "text", width: "14%" },
    { display: '单位类型', name: 'dwlx', align: 'center', type: "text", width: "9%" },
               { display: '单位名称', name: 'dwmc', align: 'left', type: "text", width: "19%" },
                { display: '组织机构代码(社会信用代码)', name: 'dwdm', align: 'center', type: "text", width: "9%" },
                 { display: '项目负责人', name: 'xmfzrxm', align: 'left', type: "text", width: "8%" },
                { display: '项目负责人身份证号', name: 'xmfzrdm', align: 'center', type: 'text', width: "13%" },
                { display: '项目技术负责人', name: 'jsfzr', align: 'center', type: 'text', width: "8%" },
                 { display: '质量员', name: 'zly', align: 'center', type: 'text', width: "8%" },
                  { display: '--', align: 'center', type: "text", width: "4%",
                      render: function(item) {
                          if (item.PKID != null && item.PKID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_view('" + item.PKID + "')\" >查看</a>";
                          }
                      }
                  }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=zjxx_ryxxList&zljdbm=<%=zljdbm %>',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'
            });

        });
       

       
        
    </script>

    </form>
</body>
</html>
