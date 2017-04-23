<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jsdwssgc.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Jsdwssgc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建设单位所属工程</title>
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
    </script>

</head>
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <div style="padding: 2px 0px 2px 0px;">
        <div id="divSsgc" style="background-color: White; min-height:80px;  ">
        </div>
    </div>
   <%-- <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td class="td-value" style="text-align: center">
                <br />
                <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                    height: 30px;" />
            </td>
        </tr>
    </table>--%>

    <script type="text/javascript">
        var manager;
        $(function() {
            manager = $("#divSsgc").ligerGrid({
                columns: [
               { display: '项目编号', name: 'PrjNum', align: 'center', type: "text", width: "11%" },
               { display: '项目名称', align: 'left', type: "text", width: "23%",
                   render: function(item) {
                   if (item.PKID != null && item.PKID != "") {
                       return "<a target='_blank' href='/IntegrativeShow2/SysFiles/PagesZHJG/Zhjg_Lxxmdj_View.aspx?LoginID=<%=this.WorkUser.UserID %>&PKID=" + item.PKID + "' style='color:#000066;text-decoration: none;' >" + item.PrjName + "</a>";
                       }
                   }
               },
               { display: '项目分类', name: 'PrjType', align: 'left', type: "text", width: "9%" },   
               { display: '项目地点', name: 'xmdd', align: 'left', type: "text", width: "14%" },
               { display: '立项文号', name: 'PrjApprovalNum', align: 'left', type: "text", width: "14%" },
               { display: '总投资（万元）', name: 'AllInvest', align: 'right', type: "text", width: "8%" },
               { display: '总面积（平方米）', name: 'AllArea', align: 'right', type: "text", width: "9%" },
               { display: '建设单位', name: 'BuildCorpName', align: 'left', type: "text", width: "12%" }
               
//               { display: '施工单位',  align: 'left', type: "text", width: "22%",
//                   render: function(item) {
//                       if (item.sgdw != null && item.sgdw != "") {
//                           return "<a target='_blank' href='../Szqy/QyxxToolBar.aspx?qyid=" + item.qyid + "' style='color:#000066;text-decoration: none;' >" + item.sgdw + "</a>";
//                       }
//                   }
//               },
//                  { display: '项目经理', name: 'xmjl', align: 'center', type: "text", width: "21%",
//                      render: function(item) {
//                          if (item.xmjl != null && item.xmjl != "") {
//                              return "<a target='_blank' href='../Zyry/RyxxToolBar.aspx?ryid=" + item.ryid + "' style='color:#000066;text-decoration: none;' >" + item.xmjl + "</a>";
//                          }
//                      }
//                  }
                //                { display: '建筑面积', name: 'JZMJ', align: 'center', type: "text", width: "10%" },
                //                { display: '总造价', name: 'ZJ', align: 'center', type: 'text', width: "10%" },
                //                { display: '项目规模', name: 'GM', align: 'center', type: 'text', width: "10%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                  url: 'List.ashx?fromwhere=JsdwSsgc&jsdwid=<%=jsdwID %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                nowrap:true,
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'//getGridHeight()
            });
        });


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
