<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gzzs_Gzzs_List.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xzsp.Gzzs_Gzzs_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作指示信息列表</title>
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

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>
</head>
<body style="background-color: rgb(238,238,238);">
    <form id="formsearch" class="l-form" style="padding: 0; margin:1px;">
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                指示主题
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="Gzzszt" />
            </td>
            <td width="15%" class="td-text">
              指示人
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="ZsrName" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                指示时间
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field" name="Zssj" />
            </td>
            <td width="15%" class="td-text">
                
            </td>
            <td width="35%" class="td-value">
              
            </td>
        </tr>
        <tr>
            <td colspan="4" class="td-value">
                <div id="btn_search" style="text-align: right">
                </div>
            </td>
        </tr>
    </table>
    </form>
    <div style="padding: 2px 0px 0px 1px;">
        <div id="maingrid">
        </div>
    </div>
    <form id="form1" runat="server" style="display: none">
    </form>

    <script type="text/javascript">
        var manager;

        function f_btnClick(item) {
            switch (item.text) {
                case "新增工作批示":
                    f_add();
                    break;
                case "批量删除所选":
                    deleteSelected();
                    break;
            }
        }

        function f_add() {
            OpenWindow('../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add', "新增工作指示", 1150, 550, true);
        }

        function f_edit(gzzsId) {
            OpenWindow('../Zlct/Gzzs_Gzzs_Edit.aspx?operate=edit&gzzsId=' + gzzsId, "修改工作指示", 1150, 550, true);
        }
        function f_view(gzzsId) {
            OpenWindowView('../Zlct/Gzzs_Gzzs_View.aspx?gzzsId=' + gzzsId, "查看工作指示", 1150, 550, false);
        }


        function f_delete(zlctId) {

            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");
                     $.getJSON('../Handler/Delete.ashx?operate=deleteGzzs&gzzsId=' + zlctId, { Rnd: Math.random() }, function(data) {
                         $.ligerDialog.closeWaitting();

                         if (data.Type == "Success") {
                             showMessage(data.Message);
                         }
                         else {
                             showError(data.Message);
                         }

                         f_reload();
                     });
                 }
             });
        }

        function deleteSelected() {
            var zlctId = "";
            var rows = manager.getSelectedRows();
            for (var i = 0; i < rows.length; i++) {
                if (i > 0) {
                    zlctId += ",";
                }
                zlctId += rows[i].GzzsId;
            }
            if (zlctId.length == 0 || zlctId == "undefined") {
                showError("请先选择要删除的工作指示信息！");
                return;
            }

            var win = parent.parent.parent.$.ligerDialog;
            win.confirm('确定要删除所选吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");

                     $.ajax({
                         type: 'POST',
                         url: '../Handler/Delete.ashx?operate=deleteGzzs&gzzsId=' + zlctId + "&rnd=" + (new Date()).toString(),
                         async: true,
                         cache: false,
                         data: null,
                         success: function(data) {
                             $.ligerDialog.closeWaitting();
                             if (data != "" && data != undefined) {
                                 var json = eval('(' + data + ')');
                                 if (json.Type == "Success") {
                                     showMessage(json.Message);
                                 }
                                 else {
                                     showError(json.Message);
                                 }
                             }
                             else {
                                 showError("出现错误！");
                             }
                             f_reload();
                         },
                         error: function(err) {

                         }
                     });
                 }
             });
        }


        function showWaitting() {
            parent.parent.$.ligerDialog.waitting("正在保存中,请稍后...");
        }

        function closeWaitting() {
            parent.parent.$.ligerDialog.closeWaitting();

        }
        function showMessage(mes) {
            parent.parent.$.ligerDialog.alert(mes);
        }
        function showError(mes) {
            parent.parent.$.ligerDialog.error(mes);
        }
        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '发送', onclick: function(item, dialog) {
                    dialog.frame.f_send(dialog, manager);
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.parent.$.ligerDialog.open(dialogOptions);
        }

        $("input[name='Zssj']").ligerDateEditor(
         {
             format: "yyyy-MM-dd",
             width: 200,
             labelAlign: 'center',
             cancelable: false
         });

        function OpenWindowView(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [
            { text: '关闭', onclick: function(item, dialog) {
                        if (isReload) {
                                f_reload();
                            }
                            dialog.close();
                        }
                    }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.parent.$.ligerDialog.open(dialogOptions);
        }

        function f_reload() {
            manager && manager.loadData(true);
        }
        var h;
        $(function() {

            h = (parent.window||window).document.documentElement.clientHeight - $("[class='l-form']").height()-52;

            manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '指示主题', name: 'Gzzszt', align: 'center', type: "text", width: "25%" }
                ,
                { display: '指示人', name: 'ZsrName', align: 'center', type: "text", width: "15%" },

                { display: '指定回复人', align: 'center', type: 'text', width: "28%",
                    render: function(item) {
                        if (item.GzzsId != null && item.GzzsId != "" && item.GzzsId != undefined) {
                            return "<div id='" + item.GzzsId + "'>" + item.zdhfr + "<div>";
                        }
                    }
                },
                { display: '指示时间', name: 'Zssj', align: 'center', type: "text", width: "15%",
                    render: function(item) {
                        if (item.Zssj != null && item.Zssj != "" && item.Zssj != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd hh:mm', DateUtil.strToDate(item.Zssj));
                        }
                    }
                },
                { display: '--', align: 'center', type: "text", width: "7%",
                    render: function(item) {
                        if (item.GzzsId != null && item.GzzsId != "" && item.GzzsId != undefined)
                            if (item.DataState == "0")
                            return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_edit(" + item.GzzsId + ")'>修改</a>";
                        else
                            if (item.DataState == "10")
                            return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_view(" + item.GzzsId + ")'>查看</a>";
                    }
                },
                 { display: '--', align: 'center', type: "text", width: "7%",
                     render: function(item) {
                         if (item.GzzsId != null && item.GzzsId != "" && item.GzzsId != undefined)
                             return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_delete(" + item.GzzsId + ")'>删除</a>";
                     }
                 }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=Gzzs_List',
                usePager: true,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: true,
                height: h,
                toolbar: { items: [
                { text: '新增工作批示', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' },
                { line: true },
                { text: '批量删除所选', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/delete.gif' }
                ]
                }
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);


        });
       
     

    </script>

</body>
</html>
