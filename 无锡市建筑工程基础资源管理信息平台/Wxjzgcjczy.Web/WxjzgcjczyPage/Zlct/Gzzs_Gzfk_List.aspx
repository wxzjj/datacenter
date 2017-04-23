<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gzzs_Gzfk_List.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xzsp.Gzzs_Gzfk_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作指示反馈信息列表</title>
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
                <input type="text" class="field s-text" name="Gzzszt" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                指示人
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="ZsrName" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                指示时间
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field" name="Zssj" style="width: 80px" />
            </td>
            <td width="15%" class="td-text">
                回复时间
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field" name="Zshfsj" style="width: 80px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                指示回复状态
            </td>
            <td width="35%" class="td-value">
                <span id="rb">
                    <input type="radio" name="aaa" value="" style="width: 15px" checked="checked" />全部&nbsp;&nbsp;&nbsp;
                    <input type="radio" name="aaa" value="0" style="width: 15px" />未读&nbsp;&nbsp;&nbsp;
                    <input type="radio" name="aaa" value="10" style="width: 15px" />已读未回复&nbsp;&nbsp;&nbsp;
                    <input type="radio" name="aaa" value="20" style="width: 15px" />已回复 </span><span
                        id="hidDataState" style="display: none;">
                        <input type="text" class="field" name="DataState" value="" op="equal" style="width: 30px" /></span>
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
        function ZsHf(state, ZshfId) {
            switch (state) {
                case 1:
                    f_edit(ZshfId)
                    break;
                case 2:
                    f_view(ZshfId);
                    break;
            }
        }

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


        function f_edit(zshfId) {
            OpenWindow('../Zlct/Gzzs_Gzfk_Edit.aspx?zshfId=' + zshfId, "工作指示回复", 950, 450, true);
        }
        function f_view(zshfId) {
            OpenWindowView('../Zlct/Gzzs_Gzfk_View.aspx?zshfId=' + zshfId, "工作指示回复查看", 900, 450, true);
        }
        function f_delete(zlctId, zlctBt) {

            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");
                     $.getJSON('Delete.ashx?operate=zlct&zlctid=' + zlctId, { Rnd: Math.random() }, function(data) {
                         if (data.Type == "Success") {
                             $.ligerDialog.success("删除成功");
                         }
                         else if (data.Type == "Error") {
                             $.ligerDialog.error(data.Message);
                         }
                         $.ligerDialog.closeWaitting();
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
                zlctId += rows[i].ZlctId;
            }
            if (zlctId.length == 0 || zlctId == "undefined") {
                showError("请先选择要删除的政令信息！");
                return;
            }

            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除所选吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");
                     $.getJSON('Delete.ashx?operate=zlct&zlctid=' + zlctId, { Rnd: Math.random() }, function(data) {
                         if (data.Type == "Success") {
                             $.ligerDialog.success("删除成功");
                         }
                         else if (data.Type == "Error") {
                             $.ligerDialog.error(data.Message);
                         }
                         $.ligerDialog.closeWaitting();
                         f_reload();
                     });
                 }
             });
        }
        $("input[name='Zssj']").ligerDateEditor(
             {
                 format: "yyyy-MM-dd",
                 width: 200,
                 labelAlign: 'center',
                 cancelable: false
             });
        $("input[name='Zshfsj']").ligerDateEditor(
             {
                 format: "yyyy-MM-dd",
                 width: 200,
                 labelAlign: 'center',
                 cancelable: false
             });

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
                { text: '回复', onclick: function(item, dialog) {
                    dialog.frame.f_save(dialog, manager);
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
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


        function OpenWindowView(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [
                { text: '关闭', onclick: function(item, dialog) {
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

        var h;
        $(function() {

            h = parent.window.document.documentElement.clientHeight - $("[class='l-form']").height() -52;
            $("#rb").ligerForm({
                toJSON: JSON2.stringify
            });
            manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '回复状态', align: 'center', type: "text", width: "8%",
                    render: function(item) {
                        if (item.ZshfId != "" && item.ZshfId != undefined) {
                            if (item.DataState == "0") {
                                return "未读";
                            }
                            else if (item.DataState == "10") {
                                return "已读未回复";
                            }
                            else if (item.DataState == "20") {
                                return "已回复";
                            }
                        }
                    }
                },
                { display: '指示主题', name: 'Gzzszt', align: 'center', type: "text", width: "35%" },
                { display: '指示人', name: 'ZsrName', align: 'center', type: "text", width: "15%" },

                { display: '指示时间', name: 'Zssj', align: 'center', type: "text", width: "15%",
                    render: function(item) {
                        if (item.Zssj != "" && item.Zssj != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd hh:mm', DateUtil.strToDate(item.Zssj));
                        }
                    }
                },

                { display: '回复时间', name: 'Zshfsj', align: 'center', type: "text", width: "15%",
                    render: function(item) {
                        if (item.Zshfsj != "" && item.Zshfsj != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd hh:mm', DateUtil.strToDate(item.Zshfsj));
                        }
                    }
                },
                { display: '--', align: 'center', type: "text", width: "10%",
                    render: function(item) {
                        if (item.ZshfId != "" && item.ZshfId != undefined) {

                            if (item.DataState == "0") {
                                return "<a style='color: #000066; text-decoration: none;' onclick='javascript:ZsHf(1," + item.ZshfId + ")'>回复</a>"
                            }
                            else if (item.DataState == "10") {
                                return "<a style='color: #000066; text-decoration: none;' onclick='javascript:ZsHf(1," + item.ZshfId + ")'>回复</a>"
                            }
                            else if (item.DataState == "20") {
                                return "<a style='color: #000066; text-decoration: none;' onclick='javascript:ZsHf(2," + item.ZshfId + ")'>查看</a>"
                            }

                        }
                    }
                }

                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
                url: 'List.ashx?fromwhere=Zshf_all_List',
                usePager: true,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: h 

            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);
            $("input[name='aaa']").click(function() {
                $("#hidDataState input:text").val($(this).val());
            });

        });
       
     

    </script>

</body>
</html>
