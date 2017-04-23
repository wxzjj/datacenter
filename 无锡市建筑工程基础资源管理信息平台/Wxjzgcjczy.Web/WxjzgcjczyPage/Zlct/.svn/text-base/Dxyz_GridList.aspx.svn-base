<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxyz_GridList.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxyz_GridList" %>
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>短信简报预制模板信息列表</title>
      <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../Common/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

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

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

</head>
<body style="background-color: rgb(238,238,238);">
    <form id="formsearch" class="l-form" style="padding: 0; margin:1px;">
       <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                简报名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="Jbmc" />
            </td>
            <td width="15%" class="td-text">
                发送类型
            </td>
            <td width="35%" class="td-value">
                <span id="rb">
                    <input type="radio" name="DataState" value="" style="width: 15px" checked="checked" />全部&nbsp;&nbsp;&nbsp;
                    <input type="radio" name="DataState" value="0" style="width: 15px" />手动发送&nbsp;&nbsp;&nbsp;
                    <input type="radio" name="DataState" value="1" style="width: 15px" />自动发送
                </span><span id="hidDataState" style="display: none;">
                    <input type="text" class="field" name="IsDsfs" value="" op="equal" style="width: 30px" /></span>
                
                
            </td>
        </tr>
        <tr>
            <td colspan="4" class="td-value">
                <div id="btn_search" style="text-align: right; width: 200px; float: right;">
                </div>
            </td>
        </tr>
    </table>
    </form>
    <div style="padding: 2px 1px 0px 1px;">
        <div id="maingrid">
        </div>
    </div>
    <form id="form1" runat="server" style="display: none">
    </form>

    <script type="text/javascript">
        var manager;

        function f_btnClick(item) {
            switch (item.text) {

                case "预制短信简报":
                    f_add();
                    break;
                case "批量删除所选":
                    deleteSelected();
                    break;
            }
        }
        function f_add() {
            //OpenWindow('../Zlct/Dxyz_Edit.aspx?operate=add', "预制短信简报", 1150, 600, false);
             window.location = '../Zlct/Dxyz_Edit.aspx?operate=add';
        }
        function f_edit(id) {
            //OpenWindow('../Zlct/Dxyz_Edit.aspx?operate=add', "预制短信简报", 1150, 600, false);
             window.location = '../Zlct/Dxyz_Edit.aspx?operate=edit&dxjbId='+id;
        }
    
        function f_view(id) {
           OpenWindowView('../Zlct/Dxyz_View.aspx?dxjbId=' + id, "查看短信简报信息", 1050, 550, false);

             //window.location = '../Zlct/Dxyz_View.aspx?dxjbId=' + id;
        }


        function f_delete(id) {

            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");
                     $.getJSON('../Handler/Delete.ashx?operate=deleteSzgkjc_Dxjb_Records&id=' + id, { Rnd: Math.random() }, function(data) {
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
            var id = "";
            var rows = manager.getSelectedRows();
            for (var i = 0; i < rows.length; i++) {
                if (i > 0) {
                    id += ",";
                }
                id += rows[i].DxjbId;
            }
            if (id.length == 0 || id == "undefined") {
                showError("请先选择要删除的预制的短信简报！");
                return;
            }

            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除所选吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");

                     $.ajax({
                         type: 'POST',
                         url: '../Handler/Delete.ashx?operate=deleteSzgkjc_Dxjb_YzJb&id=' + id + "&rnd=" + (new Date()).toString(),
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
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [

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


        $("input[name='Zssj']").ligerDateEditor(
         {
             format: "yyyy-MM-dd",
             width: 200,
             labelAlign: 'center',
             cancelable: false
         });


        function f_reload() {
            manager && manager.loadData(true);
        }
        var h;

        $(function() {
         h = (parent.window || window).document.documentElement.clientHeight - $("[class='l-form']").height() - 60;
        $("#rb").ligerForm({
            toJSON: JSON2.stringify
        });
        
            manager = $("#maingrid").ligerGrid({
            columns: [
                
                
                {display: '简报名称', name: 'Jbmc', align: 'center', type: "text", width: "20%" },
                { display: '收信人', name: 'Sxr', align: 'center', type: "text", width: "32%" },
                {display: '发送类型', align: 'center', type: "text", width: "15%",
                    render: function(item) {
                    if (item.DxjbId != null && item.DxjbId != "" && item.DxjbId != undefined) {
                            if (item.IsDsfs.toLocaleLowerCase() == "true") {
                                if (item.EveryWeekOne == "1")
                                    return "定时发送，每周发送一次";
                                else
                                    if (item.EveryMonthOne == "1")
                                    return "定时发送，每月发送一次";
                                else
                                    return "定时发送，每季度发送一次";
                            }
                            else {
                                return "手动发送";
                            }
                        }
                    }
                },
                 { display: '发信人', name: 'Fxr', align: 'center', type: "text", width: "15%" },
                { display: '--', align: 'center', type: "text", width: "9%",
                    render: function(item) {
                    if (item.DxjbId != null && item.DxjbId != "" && item.DxjbId != undefined)
                        return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_edit(" + item.DxjbId + ")'>编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;<a style='color: #000066; text-decoration: none;' onclick='javascript:f_view(" + item.DxjbId + ")'>查看</a>";
                    }
                },
                 { display: '--', align: 'center', type: "text", width: "7%",
                     render: function(item) {
                     if (item.DxjbId != null && item.DxjbId != "" && item.DxjbId != undefined)
                         return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_delete(" + item.DxjbId + ")'>删除</a>";
                     }
                 }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=YzDxjb_List',
                usePager: true,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: true,
                height: getTabGridHeight(),
                toolbar: { items: [
                    { text: '预制短信简报', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' },
                    { line: true },
                    { text: '批量删除所选', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/delete.gif' }
                    
                ]
                }
            });
            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);
            $("input[name='DataState']").click(function() {
                $("#hidDataState input:text").val($(this).val());
            });

        });
       
     

    </script>

</body>
</html>
