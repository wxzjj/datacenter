<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zyzg.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry.Zyzg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>执业资格</title>
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
 <%--   <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
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
//                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "6%" },
                {display: '人员执业资格类型', name: 'ryzyzglx', align: 'center', type: "text", width: "12%" },
                { display: '执业资格等级', name: 'zyzgdj', align: 'center', type: "text", width: "8%" },
                { display: '人员证书类型', name: 'ryzslx', align: 'center', type: "text", width: "13%" },
                { display: '证书编号', name: 'zsbh', align: 'center', type: 'text', width: "15%" },
                { display: '证书有效期始',  align: 'center', type: 'text', width: "8%",
                    render: function(item) {
                    if (item.zsyxqrq != "" && item.zsyxqrq != undefined) {
                        return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.zsyxqrq));
                        }
                    }
                },
                { display: '证书有效期止', align: 'center', type: 'text', width: "8%",
                    render: function(item) {
                    if (item.zsyxzrq != "" && item.zsyxzrq != undefined) {
                        return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.zsyxzrq));
                        }
                    }
                },
                //                { display: '执业证书聘用单位', name: 'QYMC', align: 'center', type: 'text', width: "13%" }
               {display: '执业证书聘用单位', align: 'left', type: "text", width: "20%",
               render: function(item) {
                   if (item.qymc != null && item.qymc != "") {
                       return "<a target='_blank' href='../Szqy/Qyxx_View.aspx?qyID=" + item.qyid + "' style='color:#000066;text-decoration: none;' >" + item.qymc + "</a>";
                   }
               }
           },
                { display: '数据来源', name: 'tag', align: 'left', type: 'text', width: "14%" }
//              ,  { display: '查看明细',  align: 'center', type: "text", width: "10%",
//                    render: function(item) {
//                        if (item.qyid != null && item.qyid != "") {
//                            return "<a target='_blank' href='../Zyry/Zymx.aspx?ryID=" + item.ryid + "&ryzyzglxid=" + item.ryzyzglxID + "&ryzslxid=" + item.ryzslxid + "' style='color:#000066;text-decoration: none;' >" + item.ckmx + "</a>";
//                        }
//                    }
//                }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=RyxxView&ryid=<%=ryID %>&rylx=<%=rylx %>',
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
