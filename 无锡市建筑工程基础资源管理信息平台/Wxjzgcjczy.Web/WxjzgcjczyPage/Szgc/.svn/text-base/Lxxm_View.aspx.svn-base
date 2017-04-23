<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lxxm_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Lxxm_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看立项项目信息</title>
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
                    <%-- <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">项目概况</span>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">基本信息 </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            立项编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBLabel1" runat="server" ItemName="lxxmtybh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            省治工办统一编号
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
                            所属地区
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="ssdq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            建筑面积
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText9" runat="server" ItemName="jzmj">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            投资总额（万元）
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="zj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td colspan="4" style="background-color: #fff;">
                            <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">--%>
                    <tr>
                        <td class="td-text">
                            投资类型
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBLabel9" runat="server" ItemName="tzlx">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            建设单位
                        </td>
                        <td class="td-value">
                           <a style="color: Blue; text-decoration: none; cursor: hand;" onclick="CheckID('<%=jsdwrowid %>','jsdw')"
                                target="_blank">
                                <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="jsdw">
                                </Bigdesk8:DBText></a>
                         <%--   <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="jsdw">
                            </Bigdesk8:DBText>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">立项信息 </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            立项文件名称
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="lxwjmc">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            立项部门
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="lxbm">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            立项文件类型
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="lxwjlx">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            立项文号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="lxwh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            立项日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="lxrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            备注
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBMemo ID="DBMemo6" runat="server" ItemName="lxbz" Height="50px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">国有土地使用信息 </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            土地使用证号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText24" runat="server" ItemName="tdsyzh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            土地使用者
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText25" runat="server" ItemName="tdsyz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            用地坐落
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText8" runat="server" ItemName="ydzl">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            使用权面积
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="syqmj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            土地性质
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="tdxz">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            土地等级
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="tddj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            使用权类型
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="syqlx">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            使用权终止日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="syqzzrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证机关
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="ydxkfzjg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            发证日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText20" runat="server" ItemName="ydxkfzrq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBMemo1" runat="server" ItemName="tdsyzbz" Height="50px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">规划许可信息 </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            建设项目选址意见书号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="xzyjsh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            拟选位置
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText26" runat="server" ItemName="nxwz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            拟用地面积
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText27" runat="server" ItemName="nydmj">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            建设依据
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText28" runat="server" ItemName="jsyj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证机关
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText29" runat="server" ItemName="xzyjsfzjg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            发证日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText30" runat="server" ItemName="xzyjsfzrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBMemo2" runat="server" ItemName="xzyjsbz" Height="50px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            建设用地规划许可证号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText33" runat="server" ItemName="ydghxkzh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            用地位置
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText34" runat="server" ItemName="ydwz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            用地面积
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText21" runat="server" ItemName="ydmj">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            用地性质
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="ydxz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证机关
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText31" runat="server" ItemName="ydghxkzfzjg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            发证日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText32" runat="server" ItemName="ydghxkzfzrq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBMemo3" runat="server" ItemName="ydghxkzbz" Height="50px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            建设工程规划许可证号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText35" runat="server" ItemName="ghxkzh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            建设位置
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText36" runat="server" ItemName="jswz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            建设规模
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBMemo8" runat="server" ItemName="jsgm" Height="50px"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证机关
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText39" runat="server" ItemName="ghxkfzjg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            发证日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText40" runat="server" ItemName="ghxkfzrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <tr>
                            <td class="td-text">
                                备注
                            </td>
                            <td class="td-value" colspan="3">
                                <Bigdesk8:DBMemo ID="DBMemo4" runat="server" ItemName="ghxkbz" Height="50px"></Bigdesk8:DBMemo>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="td-value">
                                <span style="color: blue; font-size: 14px;">消防设计审核意见书信息 </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text">
                                消防设计审核意见书编号
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText41" runat="server" ItemName="xfsjshyjsbh">
                                </Bigdesk8:DBText>
                            </td>
                            <td class="td-text">
                                审核机构
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText42" runat="server" ItemName="xfsjshyjsshjg">
                                </Bigdesk8:DBText>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text">
                                审核日期
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText43" runat="server" ItemName="xfsjshyjsshrq" ItemType="Date">
                                </Bigdesk8:DBText>
                            </td>
                            <td class="td-text">
                                备注
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBMemo ID="DBMemo5" runat="server" ItemName="xfsjbz" Height="50px"></Bigdesk8:DBMemo>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="td-value">
                                <span style="color: blue; font-size: 14px;">环境影响审批意见书 </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text">
                                环境影响审批意见书编号
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="hjyxspyjsbh">
                                </Bigdesk8:DBText>
                            </td>
                            <td class="td-text">
                                审核机构
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText38" runat="server" ItemName="hjyxspyjsshjg">
                                </Bigdesk8:DBText>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text">
                                审核日期
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBText ID="DBText44" runat="server" ItemName="hjyxspyjsshrq" ItemType="Date">
                                </Bigdesk8:DBText>
                            </td>
                            <td class="td-text">
                                备注
                            </td>
                            <td class="td-value">
                                <Bigdesk8:DBMemo ID="DBMemo7" runat="server" ItemName="hjyxbz" Height="50px"></Bigdesk8:DBMemo>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="td-value" style="text-align: center">
                                <br />
                                <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                                    height: 30px;" />
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function CheckID(id, lx) {
            if (id == "" || id == null) {
                alert("您所访问的信息不存在！");
            }
            else {
                switch (lx) {
                    case "qy":
                        window.open("../Szqy/Qyxx_View.aspx?rowid=" + id, "_blank");
                        break;
                    case "jsdw":
                        window.open("../Szqy/JsdwxxToolBar.aspx?rowid=" + id, "_blank");
                        break;
                    case "ry":
                        window.open("../Zyry/Ryxx_View.aspx?rowid=" + id, "_blank");
                        break;
                }

            }
        }

        function openLdpsWindow() {
            var url = "../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add";
            var arguments = window;
            var features = "dialogWidth=950px;dialogHeight=530px;center=yes;status=yes";
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
