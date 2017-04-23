<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gcxm_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Gcxm_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看标段项目信息</title>
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
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="padding: 0; margin: 0;">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td class="td-text" width="15%">
                            标段编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBLabel1" runat="server" ItemName="sgxmtybh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            中标公示日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText4" runat="server" ItemName="zbgsrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            项目名称
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="xmmc">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            项目地址
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="dd">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            所属地区
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText9" runat="server" ItemName="ssdq">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            所属地区ID
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="ssdqid">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td colspan="4" style="background-color: #fff;">
                            <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">--%>
                    <tr>
                        <td class="td-text">
                            立项文号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBLabel9" runat="server" ItemName="lxwh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            立项统一编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="LxxmTybh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            项目统一编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="sgxmtybh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            建筑面积(平方米)
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="jzmj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            结构
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="jg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            规模
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="gm">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            项目类别
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="xmlb">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            承包类型
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="cblx">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--   <tr>
                        <td class="td-text">
                            规划许可证
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText81" runat="server" ItemName="Ghxkz">无附件
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4" class="td-value" style="text-align: center">
                            <br />
                            <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                                height: 30px;" />&nbsp;&nbsp;&nbsp;<input type="button" value="信用考评" class="button"
                                    style="width: 100px; height: 30px;" onclick="GotoUrl()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        //        function CheckID(id, lx) {
        //            if (id == "" || id == null) {
        //                alert("您所访问的信息不存在！");
        //            }
        //            else {
        //                switch (lx) {
        //                    case "qy":
        //                        window.open("../Szqy/Qyxx_View.aspx?rowid=" + id, "_blank");
        //                        break;
        //                    case "jsdw":
        //                        window.open("../Szqy/Jsdw_View.aspx?rowid=" + id, "_blank");
        //                        break;
        //                    case "ry":
        //                        window.open("../Zyry/Ryxx_View.aspx?rowid=" + id, "_blank");
        //                        break;
        //                }

        //            }
        //        }

        function openLdpsWindow() {
            var url = "../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add";
            var arguments = window;
            var features = "dialogWidth=950px;dialogHeight=530px;center=yes;status=yes";
            var argReturn = window.showModalDialog(url, arguments, features);
        }

        function GotoUrl() {
            var url = "http://218.90.162.101:8088/EpointFrameZS_WXSZJS/WuxiCenter/Login.aspx?LoginGuid=e344a77c-a55a-4713-8266-3ffb97a2fc91";
            var arguments = window;
            var features = "dialogWidth=1200px;dialogHeight=800px;center=yes;status=yes";
            var argReturn = window.showModalDialog(url, arguments, features);
        }



        function OpenWin(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '发送', onclick: function(item, dialog) {
                    dialog.frame.f_send(dialog, null);
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
        //        $("a[rel=example_group]").fancybox({
        //            'transitionIn': 'none',
        //            'transitionOut': 'none',
        //            'titlePosition': 'over',
        //            'titleFormat': function(title, currentArray, currentIndex, currentOpts) {
        //                return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
        //            }
        //        });
    </script>

    </form>
</body>
</html>
