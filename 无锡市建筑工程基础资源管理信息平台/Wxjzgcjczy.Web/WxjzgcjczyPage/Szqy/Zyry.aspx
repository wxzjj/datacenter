<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyry.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Zyry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>执业人员信息</title>
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
    <form id="formsearch" class="l-form" style="padding: 0px; margin: 1px;">
    <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td class="td-text" width="11%">
                人员姓名
            </td>
            <td class="td-value" width="22">
                <input type="text" class="field s-text" name="xm" style="width: 200px" />
            </td>
            <td class="td-text" width="11%">
                身份证号
            </td>
            <td class="td-value" width="22%">
                <input type="text" class="field s-text" name="zjhm" style="width: 200px" />
            </td>
            <td class="td-text" width="11%">
                证书编号
            </td>
            <td class="td-value" width="22%">
                <input type="text" class="field s-text" name="zsbh" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td colspan="6" class="td-value">
                <div id="btn_search" style="text-align: right">
                </div>
            </td>
        </tr>
    </table>
    <div style="padding: 2px 0px 2px 0px;">
        <div id="maingrid" style="background-color: White;">
        </div>
    </div>
   <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td colspan="6" class="td-value" style="text-align: center">

                <button type="button" id ="PullDataBtn" onclick='PullDataCorpRegStaff()' style="width: 100px; height: 30px;" class="button button2 buttonnoicon">同步</button>
                <!--<input type="button" value="手动同步" onclick="openLdpsWindow()" class="button" style="width: 100px; height: 30px;" />-->
            </td>
        </tr>
    </table>

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
        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [
                {display: '序号', name: 'rowno', align: 'center', type: "text", width: "5%" },
                { display: '姓名', name: 'xm', align: 'center', type: "text", width: "8%" ,
                    render: function(item) {
                    if (item.xm != null && item.xm != "") {
                        return "<a target='_blank' href='../Zyry/RyxxToolBar.aspx?ryid=" + item.ryid + "' style='color:#000066;text-decoration: none;' >" + item.xm + "</a>";
                        }
                    }
                },
                { display: '身份证号', name: 'zjhm', align: 'center', type: 'text', width: "15%" },
                //{ display: '联系电话', name: 'lxdh', align: 'center', type: 'text', width: "12%" },
                { display: '执业资格类型', name: 'ryzyzglx', align: 'center', type: 'text', width: "12%" },
                { display: '人员证书类型', name: 'ryzslx', align: 'center', type: 'text', width: "15%" },
                { display: '证书编号', name: 'zsbh', align: 'center', type: 'text', width: "16%" },
                { display: '证书有效止日期', name: 'zsyxzrq', align: 'center', type: 'text', width: "10%" }
//                 { display: '查看明细', name: 'zsmx', align: 'center', type: "text", width: "8%",
//                     render: function(item) {
//                         if (item.zsmx != null && item.zsmx != "") {
//                             return "<a target='_blank' href='../Zyry/Zymx.aspx?ryid=" + item.ryid + "&ryzyzglxid=" + item.ryzyzglxid + "&ryzslxid=" + item.ryzslxid + "' style='color:#000066;text-decoration: none;' > " + item.zsmx + " </a>";
//                         }
//                     }
//                 }

                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=Zyry&qyid=<%=qyID %>',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight(),
                onAfterShowData: function (s) {
                    setTimeout(function () {
                        $('#maingrid .l-grid-body-table tbody').rowspan('xm', manager);
                        $('#maingrid .l-grid-body-table tbody').rowspan('zjhm', manager);
                    }, 0)
                }
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);
        })

        function PullDataCorpRegStaff() {
            $.ajax({
                type: 'POST',
                url: '/WxjzgcjczyPage/Handler/Data.ashx?type=downloadCorpRegStaff&qyid=<%=qyID %>',
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
