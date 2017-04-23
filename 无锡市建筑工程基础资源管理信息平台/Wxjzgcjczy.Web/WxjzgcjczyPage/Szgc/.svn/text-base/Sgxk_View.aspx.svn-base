<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sgxk_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Sgxk_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看施工许可项目信息</title>
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
    <form id="form1" runat="server" style="text-align: center;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <%-- <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">施工许可信息</span>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-text" width="15%">
                            施工合同开工日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText45" runat="server" ItemName="sghtkgrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            施工合同竣工日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText46" runat="server" ItemName="sghtjgrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            施工许可受理时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText40" runat="server" ItemName="sgxkslsj" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            施工许可受理人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText41" runat="server" ItemName="sgxkslr">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            施工许可编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText42" runat="server" ItemName="sgxkzbh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            施工许可受理部门
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText43" runat="server" ItemName="sgxkglbm">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">施工许可</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #FFF; height: 25px; padding-top: 2px; padding-bottom: 2px;"
                            colspan="4">
                            <div id="DivSgxk" style="background-color: White;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value">
                            <span style="color: blue; font-size: 14px;">竣工验收</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #FFF; height: 25px; padding-top: 2px; padding-bottom: 2px;"
                            colspan="4">
                            <div id="DivJgys" style="background-color: White;">
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4" class="td-value">
                            <a style="color: Blue" target="_blank" href='Sgxkzs.aspx?rowid=<%=rowID %>&befrom=<%=befrom %>'>
                                查看施工许可证书</a>
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


        $(function() {
            var manager;
            manager = $("#DivSgxk").ligerGrid({
                columns: [
                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
                { display: '项目名称', name: 'XMMC', align: 'center', type: "text", width: "20%" },
                { display: '施工许可证编号', name: 'SGXKZBH', align: 'center', type: 'text', width: "15%" },
                { display: '施工许可受理时间', name: 'SGXKSLSJ', align: 'center', type: 'text', width: "15%",
                    render: function(item) {
                        if (item.SGXKSLSJ != "" && item.SGXKSLSJ != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.SGXKSLSJ));
                        }
                    }
                },
                { display: '施工许可管理部门', name: 'SGXKGLBM', align: 'center', type: 'text', width: "15%" },
                { display: '施工许可受理人', name: 'SGXKSLR', align: 'center', type: 'text', width: "15%" },
                { display: '详细', name: '', align: 'center', type: 'text', width: "10%",
                    render: function(item) {
                        if (item.SGXKID != "" && item.SGXKID != undefined)
                            return '详细';
                    }
                }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=sgxk&rowid=<%=rowID %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'//getGridHeight()
            });
        })
        $(function() {
            var manager;
            manager = $("#DivJgys").ligerGrid({
                columns: [
                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
                { display: '项目名称', name: 'XMMC', align: 'center', type: "text", width: "20%" },
                { display: '竣工备案管理部门', name: 'JGBAGLBM', align: 'center', type: 'text', width: "20%" },
                { display: '竣工备案受理时间', name: 'JGBASLSJ', align: 'center', type: 'text', width: "15%",
                    render: function(item) {
                        if (item.JGBASLSJ != "" && item.JGBASLSJ != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.JGBASLSJ));
                        }
                    }
                },
                { display: '竣工备案受理人', name: 'JGBASLR', align: 'center', type: 'text', width: "20%" },
                { display: '详细', name: '', align: 'center', type: 'text', width: "15%",
                    render: function(item) {
                        if (item.SGXKID != "" && item.SGXKID != undefined)
                            return '详细';
                    }
                }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=sgxkJgba&rowid=<%=rowID %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'//getGridHeight()
            });
        })
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
