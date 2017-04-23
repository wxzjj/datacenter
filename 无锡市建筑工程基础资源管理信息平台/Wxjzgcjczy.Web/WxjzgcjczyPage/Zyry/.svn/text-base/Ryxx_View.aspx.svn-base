<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ryxx_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry.Ryxx_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看人员基本信息</title>
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
            <td>
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td style="background-color: #333" colspan="2">
                            <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                                <%-- <tr>
                                    <td colspan="7" style="height: 25px;">
                                        <span style="font-size: 15px;"><b>人员基本信息</b></span>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="td-text" width="11%">
                                        姓名
                                    </td>
                                    <td class="td-value" width="17%">
                                        <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="xm">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text" width="11%">
                                        性别
                                    </td>
                                    <td class="td-value" width="17%">
                                        <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="xb">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text" width="11%">
                                        出生日期
                                    </td>
                                    <td class="td-value" width="17%">
                                        <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="csrq" ItemType="Date">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td rowspan="8" style="background-color: #FFF;">
                                        <div id="d1" runat="server">
                                            <iframe id="Iframe1" name="Iframe_Dmtxx1" frameborder="0" src="ImageShow.aspx?LoginID=<%=this.WorkUser.UserID %>&ryid=<%=ryID %>"
                                                scrolling="no" width="100%" height="100%"></iframe>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        身份证号
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="zjhm">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        民族
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="mz">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        学历
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText4" runat="server" ItemName="xl">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        住址
                                    </td>
                                    <td class="td-value" colspan="5">
                                        <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="ryzz">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        发证机关
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText8" runat="server" ItemName="fzjg">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        有效期限
                                    </td>
                                    <td class="td-value" colspan="3">
                                        <Bigdesk8:DBText ID="DBText9" runat="server" ItemName="yxqx">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        职称
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="zc">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        职称等级
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="zcjb">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        职务
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="zw">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        执业资格类型
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="ryzyzglx">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        执业资格等级
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="zyzgdj">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        证书类型
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="ryzslx">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        证书编号
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="zsbh">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        证书有效起日期
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText21" runat="server" ItemName="zsyxqrq">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        证书有效止日期
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="zsyxzrq">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        移动电话
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="yddh">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        联系电话
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="lxdh">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                    </td>
                                    <td class="td-value">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        简历
                                    </td>
                                    <td class="td-value" colspan="6">
                                        <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="gzjl">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        数据上报时间
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="xgrqsj" ItemType="Date">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        数据来源
                                    </td>
                                    <td class="td-value" colspan="4">
                                      
                                      <Bigdesk8:DBText ID="txtTag" runat="server" ItemName="tag">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--   <tr>
                        <td class="td-value" style="text-align: center">
                            <br />
                            <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                                height: 30px;" />
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">

        //        function openLdpsWindow() {
        //            var url = "../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add";
        //            var arguments = window;
        //            var features = "dialogWidth=950px;dialogHeight=530px;center=yes;status=yes";
        //            var argReturn = window.showModalDialog(url, arguments, features);
        //        }
        //        function OpenWin(url, title, width, height, isReload) {
        //            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
        //                { text: '发送', onclick: function(item, dialog) {
        //                    dialog.frame.f_send(dialog, null);
        //                }
        //                },
        //                { text: '关闭', onclick: function(item, dialog) {
        //                    dialog.close();
        //                }
        //                }
        //            ], isResize: true, timeParmName: 'a'
        //            };
        //            activeDialog = $.ligerDialog.open(dialogOptions);
        //        }
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
