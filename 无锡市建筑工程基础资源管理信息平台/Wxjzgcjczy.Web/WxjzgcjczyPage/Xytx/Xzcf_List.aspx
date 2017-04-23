<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xzcf_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xytx.Xzcf_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>行政处罚信息列表</title>
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
<body style="background-color: #EEEEEE;">
    <form id="formsearch" class="l-form" style="padding: 0px 0px 0px 0px; margin: 1px;">
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                案件编号
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="ajNo" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                项目编号
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="prjNum" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                违法违规项目名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="wfwgxm" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                违法违规单位/个人
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="wfwgdwry" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
               组织机构代码/个人身份证号
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="zzjgdmSfzh" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                违法行为
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="wfxw" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td class="td-value" colspan="4" >
                <div id="btn_search" style="text-align: right; float: left">
                </div>
            </td>
        </tr>
    </table>
    </form>
    <div style="padding: 2px 0px 0px 1px;">
        <div id="maingrid" style="background-color: White;">
        </div>
    </div>

    <script type="text/javascript">
        var manager;
        var activeDialog;

        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                //            { text: '保存', onclick: function(item, dialog) {
                //                dialog.frame.f_save(dialog, manager);
                //            }
                //            },
            {text: '关闭', onclick: function(item, dialog) {
                if (isReload) {
                    f_reload();
                }
                dialog.close();
            }
        }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.$.ligerDialog.open(dialogOptions);
        }

        function f_reload() {
            manager && manager.loadData(true);
        }


        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [
                  { display: '案件编号', align: 'center', type: "text", width: "13%",
                      render: function(item) {

                          return item.ajNo;
                      }
                  },
                   { display: '项目编号', align: 'center', type: "text", width: "11%",
                       render: function(item) {

                           return item.prjNum;
                       }
                   },
                  { display: '违法违规项目名称', name: 'wfwgxm', align: 'left', type: "text", width: "22%" },
                  { display: '违法违规单位/个人', name: 'wfwgdwry', align: 'center', type: "text", width: "15%" },
                  { display: '组织机构代码/个人身份证号', name: 'zzjgdmSfzh', align: 'center', type: "text", width: "9%" },
                  { display: '违法行为', name: 'wfxw', align: 'left', type: "text", width: "20%" },
                     { display: '立案时间', align: 'center', type: "text", width: "8%",
                         render: function(item) {

                             if (item.lasj != null && item.lasj != "" && item.lasj != undefined)
                                 return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.lasj));
                             else
                                 return "";
                         }
                     }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=xzcf',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                sortname: 'lasj',
                sortorder: 'desc',
                height: getGridHeight(),
                headerRowHeight: 43
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);


        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
