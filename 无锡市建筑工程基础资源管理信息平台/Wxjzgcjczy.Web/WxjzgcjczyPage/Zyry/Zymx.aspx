﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zymx.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry.Zymx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>执业明细</title>
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
    <div style="padding: 2px 0px 2px 0px;">
        <div id="maingrid" style="background-color: White;">
        </div>
    </div>
  <%--  <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td class="td-value" style="text-align: center">
                <br />
                <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                    height: 30px;" />
            </td>
        </tr>
    </table>--%>

    <script type="text/javascript">

        $(function() {
            var manager;
            manager = $("#maingrid").ligerGrid({
                columns: [
//                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
                //                { display: '人员执业资格类型', name: 'RYZYZGLX', align: 'center', type: "text", width: "20%" },
                //                { display: '人员证书类型', name: 'RYZSLX', align: 'center', type: "text", width: "20%" },
                {display: '主项/增项', name: 'zzbz', align: 'center', type: 'text', width: "20%" },
                { display: '执业资格类别', name: 'ryzyzglx', align: 'center', type: 'text', width: "15%" },
                { display: '执业资格等级', name: 'zyzgdj', align: 'center', type: 'text', width: "15%" },
                  { display: '工作范围', name: 'gzfw', align: 'center', type: 'text', width: "40%" }

                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=RyxxZymx&ryid=<%=ryID %>&ryzyzglxid=<%=ryzyzglxid %>&ryzslxid=<%=ryzslxid %>',
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
