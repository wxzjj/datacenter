<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zljd_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Zljd_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看质量监督项目信息</title>
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
<body style="padding: 0px; margin: 0; margin-top: 2px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="padding: 0;">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td class="td-text" width="15%">
                            质量监督受理时间
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText40" runat="server" ItemName="zljdslsj" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            质量监督受理人
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText41" runat="server" ItemName="zljdslr">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            质量监督编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText42" runat="server" ItemName="zljdbh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            质量监督受理部门
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText43" runat="server" ItemName="zljdglbm">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            报监范围
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText39" runat="server" ItemName="bjfw">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            报监分部
                        </td>
                        <td class="td-value" style="height: 50px;">
                            <Bigdesk8:DBMemo ID="mb_bjfb" ItemName="bjfb" runat="server">
                            </Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            数据上报时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText44" runat="server" ItemName="xgrqsj" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            数据上报单位
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText45" runat="server" ItemName="xgr">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #FFF; padding-top: 2px; padding-bottom: 2px;" colspan="4">
                            <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                                PagerSettings-Position="Top" Width="100%" AllowPaging="false">
                                <Columns>
                                    <asp:BoundField DataField="rownum" HeaderText="序号">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="5%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mc" HeaderText="单体名称">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="12%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MJ" HeaderText="单体面积">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="11%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CS" HeaderText="单体层数">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="8%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ZDKD" HeaderText="单体最大跨度">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="8%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="YT_COM" HeaderText="单体用途">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="10%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JG_COM" HeaderText="结构">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="8%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JCLX_COM" HeaderText="基础类型">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="10%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DWGCZJ" HeaderText="单位工程造价">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="8%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JDZCH" HeaderText="监督注册号">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="10%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JGYSSJ" HeaderText="竣工验收时间" DataFormatString="{0:yyyy-MM-dd}">
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="10%" />
                                        <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    </asp:BoundField>
                                </Columns>
                            </Bigdesk8:PowerDataGrid>
                            <%-- <div id="maingrid" style="background-color: White; ">
                            </div>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td-value">
                            <a style="color: Blue" target="_blank" href='Zljdzs.aspx?rowid=<%=rowID %>&befrom=<%=befrom %>'>
                                查看质量监督证书</a>
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
                        window.open("../Szqy/Jsdw_View.aspx?rowid=" + id, "_blank");
                        break;
                    case "ry":
                        window.open("../Zyry/Ryxx_View.aspx?rowid=" + id, "_blank");
                        break;
                }

            }
        }
        $(function() {
            var manager;
            manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "5%" },
                { display: '单体名称', name: 'MC', align: 'center', type: "text", width: "12%" },
                { display: '单体面积(平方米)', name: 'DSMJ', align: 'center', type: 'text', width: "11%" },
                { display: '单体层数', name: 'DSCS', align: 'center', type: 'text', width: "8%" },
                { display: '单体最大跨度', name: 'ZDKD', align: 'center', type: 'text', width: "8%" },
                { display: '单体用途', name: 'YT_COM', align: 'center', type: 'text', width: "10%" },
                { display: '结构', name: 'JG_COM', align: 'center', type: 'text', width: "8%" },
                { display: '基础类型', name: 'JCLX_COM', align: 'center', type: 'text', width: "10%" },
                { display: '单位工程造价', name: 'DWGCZJ', align: 'center', type: 'text', width: "8%" },
                { display: '监督注册号', name: 'JDZCH', align: 'center', type: 'text', width: "10%" },
                { display: '竣工验收时间', name: 'JGYSSJ', align: 'center', type: 'text', width: "10%" }

                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=zljdDtgc&rowid=<%=rowID %>&befrom=<%=befrom %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'//getGridHeight()
            });
        })
        //        $(function() {
        //            var manager;
        //            manager = $("#DivSgxk").ligerGrid({
        //                columns: [
        //                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
        //                { display: '项目名称', name: 'XMMC', align: 'left', type: "text", width: "20%" },
        //                { display: '施工许可证编号', name: 'SGXKZBH', align: 'center', type: 'text', width: "15%" },
        //                { display: '施工许可受理时间', name: 'SGXKSLSJ', align: 'center', type: 'text', width: "15%",
        //                    render: function(item) {
        //                        if (item.SGXKSLSJ != "" && item.SGXKSLSJ != undefined) {
        //                            return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.SGXKSLSJ));
        //                        }
        //                    }
        //                },
        //                { display: '施工许可管理部门', name: 'SGXKGLBM', align: 'center', type: 'text', width: "15%" },
        //                { display: '施工许可受理人', name: 'SGXKSLR', align: 'center', type: 'text', width: "15%" },
        //                { display: '详细', name: '', align: 'center', type: 'text', width: "10%",
        //                    render: function(item) {
        //                        if (item.SGXKID != "" && item.SGXKID != undefined)
        //                            return "<a target='_blank' href='Sgxkzs.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.ROWID + "' style='color:#000066;text-decoration: none;' >" + "详细" + "</a>";
        //                    }
        //                }
        //                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
        //                url: 'List.ashx?fromwhere=zljdSgxk&rowid=<%=rowID %>',
        //                dataAction: 'server', //服务器排序
        //                usePager: false,       //服务器分页
        //                pageSize: 10,
        //                rownumbers: false,
        //                alternatingRow: true,
        //                checkbox: false,
        //                height: 'auto'//getGridHeight()
        //            });
        //        })
        //        $(function() {
        //            var manager;
        //            manager = $("#DivJgys").ligerGrid({
        //                columns: [
        //                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
        //                { display: '项目名称', name: 'XMMC', align: 'left', type: "text", width: "35%" },
        //                { display: '竣工备案管理部门', name: 'JGBAGLBM', align: 'center', type: 'text', width: "20%" },
        //                { display: '竣工备案受理时间', name: 'JGBASLSJ', align: 'center', type: 'text', width: "15%",
        //                    render: function(item) {
        //                        if (item.JGBASLSJ != "" && item.JGBASLSJ != undefined) {
        //                            return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.JGBASLSJ));
        //                        }
        //                    }
        //                },
        //                { display: '竣工备案受理人', name: 'JGBASLR', align: 'center', type: 'text', width: "20%" }
        //                { display: '详细', name: '', align: 'center', type: 'text', width: "15%",
        //                    render: function(item) {
        //                        if (item.ZLJDID != "" && item.ZLJDID != undefined)
        //                            return '详细';
        //                    }
        //                }
        //                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
        //                url: 'List.ashx?fromwhere=zljdJgba&rowid=<%=rowID %>',
        //                dataAction: 'server', //服务器排序
        //                usePager: false,       //服务器分页
        //                pageSize: 10,
        //                rownumbers: false,
        //                alternatingRow: true,
        //                checkbox: false,
        //                height: 'auto'//getGridHeight()
        //            });
        //        })
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
