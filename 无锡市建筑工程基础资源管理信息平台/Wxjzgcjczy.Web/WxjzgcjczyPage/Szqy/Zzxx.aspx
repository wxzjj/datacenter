<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zzxx.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Zzxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看企业基本信息</title>
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

</head>
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <div style="padding: 2px 0px 2px 0px;">
        <div id="maingrid" style="background-color: White;">
        </div>
    </div>
       <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td colspan="6" class="td-value" style="text-align: center">

                <button type="button" id ="PullDataBtn" onclick='PullDataCorpCert()' style="width: 100px; height: 30px;" class="button button2 buttonnoicon">同步</button>
                <!--<input type="button" value="手动同步" onclick="openLdpsWindow()" class="button" style="width: 100px; height: 30px;" />-->
            </td>
        </tr>
    </table>
  <%--  <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td colspan="6" class="td-value" style="text-align: center">
                            <br />
                            <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                                height: 30px;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
--%>
    <script type="text/javascript">

        jQuery.fn.rowspan = function (colname, tableObj) {
            var colIdx;
            for (var i = 0, n = tableObj.columns.length; i < n; i++) {
                if (tableObj.columns[i]["columnname"] == colname) {
                    colIdx = i - 1 < 1 ? 0 : i - 1;
                    colIdx = colIdx + 1;
                    break;
                }
            }
            return this.each(function () {
                var that;
                $('tr', this).each(function (row) {
                    $('td:eq(' + colIdx + ')', this).filter(':visible').each(function (col) {
                        if (that != null && $(this).html() == $(that).html()) {
                            rowspan = $(that).attr("rowSpan");
                            if (rowspan == undefined) {
                                $(that).attr("rowSpan", 1);
                                rowspan = $(that).attr("rowSpan");
                            }
                            rowspan = Number(rowspan) + 1;
                            $(that).attr("rowSpan", rowspan);
                            $(this).hide();
                        } else {
                            that = this;
                        }
                    });
                });
            });
        }
        var manager;
        $(function () {
            
            manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '序号', name: 'rowno', align: 'center', type: "text", width: "5%" },
                { display: '资质类别', name: 'zzlb', align: 'center', type: "text", width: "15%" },
                { display: '资质证书号', name: 'zsbh', align: 'center', type: 'text', width: "20%" },
                { display: '资质名称', name: 'zzmc', align: 'center', type: 'text', width: "15%" },
                { display: '发证日期', name: 'fzrq', align: 'center', type: 'text', width: "15%" },
                { display: '证书有效期', name: 'yxq', align: 'center', type: 'text', width: "15%" },
                { display: '发证机关', name: 'fzdw', align: 'center', type: 'text', width: "15%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=QyxxView&qyid=<%=qyID %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto', //getGridHeight()
                onAfterShowData: function (s) {
                    setTimeout(function () {
                        $('#maingrid .l-grid-body-table tbody').rowspan('zzlb', manager);
                        $('#maingrid .l-grid-body-table tbody').rowspan('zsbh', manager);
                        $('#maingrid .l-grid-body-table tbody').rowspan('fzrq', manager);
                        $('#maingrid .l-grid-body-table tbody').rowspan('yxq', manager);
                        $('#maingrid .l-grid-body-table tbody').rowspan('fzdw', manager);
                    }, 0)
                }
            });
        })

        function PullDataCorpCert() {
            $.ajax({
                type: 'POST',
                url: '/WxjzgcjczyPage/Handler/Data.ashx?type=downloadCorpCert&qyid=<%=qyID %>',
                async: false,
                data: null,
                success: function (result) {
                    alert(result);
                    //$('#btn_search').click();
                    manager.loadData();
                }
            });
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
