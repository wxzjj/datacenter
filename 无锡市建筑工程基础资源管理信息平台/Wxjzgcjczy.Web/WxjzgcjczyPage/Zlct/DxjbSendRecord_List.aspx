<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DxjbSendRecord_List.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.DxjbSendRecord_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>已发送短信简报信息列表</title>
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
    <form id="formsearch" class="l-form" style="padding: 0 ; margin:1px;">
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                简报名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="Jbmc" />
            </td>
            <td width="15%" class="td-text">
                是否定时发送
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
                <%--<div style="width: 150px; float: left; vertical-align: middle; margin-top: 5px;">
                    <input type="button" class="button-s" value="返 回" onclick='javascript:window.location="Dxjb_Toolbar.aspx"' />
                </div>--%>
                <div id="btn_search" style="text-align: right; width: 200px; float: right;">
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
           
                case "发送短信简报":
                    f_add();
                    break;
                case "批量删除所选":
                    deleteSelected();
                    break;
            }
        }
        function f_add() {
            // OpenWindow('../Zlct/Dxjb_Send.aspx', "发送短信简报", 1100, 600, false);
            window.location = '../Zlct/Dxjb_Send.aspx';
        }
    
        function f_view(id) {
            OpenWindowView('../Zlct/Dxjb_Record_View.aspx?id=' + id, "短信简报发送记录查看", 950, 450, false);
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
                id += rows[i].Id;
            }
            if (id.length == 0 || id == "undefined") {
                showError("请先选择要删除的短信发送记录！");
                return;
            }

            var win = parent.parent.parent.$.ligerDialog;
            win.confirm('确定要删除所选吗？',
             function(b) {
                 if (b) {
                     $.ligerDialog.waitting("正在删除中...");

                     $.ajax({
                         type: 'POST',
                         url: '../Handler/Delete.ashx?operate=deleteSzgkjc_Dxjb_Records&id=' + id + "&rnd=" + (new Date()).toString(),
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
            activeDialog = parent.parent.parent.$.ligerDialog.open(dialogOptions);
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

        $(function() {
        var height = parent.window.document.documentElement.clientHeight - $("[class='l-form']").height() - 54;
        $("#rb").ligerForm({
            toJSON: JSON2.stringify
        });
            manager = $("#maingrid").ligerGrid({
                columns: [
                { display: '简报名称', name: 'Jbmc', align: 'center', type: "text", width: "20%" }
                ,
                //                { display: '简报内容', name: 'Jbnr', align: 'center', type: "text", width: "25%" },
                {display: '收信人', name: 'Sxr', align: 'center', type: "text", width: "32%" },
                { display: '简报发送时间', align: 'center', type: 'text', width: "13%",
                    render: function(item) {
                        if (item.Id != null && item.Id != "" && item.Id != undefined) {
                            return DateUtil.dateToStr('yyyy-MM-dd hh:mm', DateUtil.strToDate(item.SendTime));
                        }
                    }
                },
                { display: '发送类型', align: 'center', type: "text", width: "18%",
                    render: function(item) {
                        if (item.Id != null && item.Id != "" && item.Id != undefined) {
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
                { display: '--', align: 'center', type: "text", width: "7%",
                    render: function(item) {
                        if (item.Id != null && item.Id != "" && item.Id != undefined)
                            return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_view(" + item.Id + ")'>查看</a>";
                    }
                },
                 { display: '--', align: 'center', type: "text", width: "7%",
                     render: function(item) {
                         if (item.Id != null && item.Id != "" && item.Id != undefined)
                             return "<a style='color: #000066; text-decoration: none;' onclick='javascript:f_delete(" + item.Id + ")'>删除</a>";
                     }
                 }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=DxjbSendRecords_List',
                usePager: true,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: true,
                height: height,
                toolbar: { items: [
                    { text: '发送短信简报', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' },
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
//            $("[name='IsDsfs']").click(function() {
//           
//                if ($(this).val()==""||$(this).val() == "False") {
//                    $("input[name='IsDsfs']").val("True");
//                }
//                else {
//                    $("input[name='IsDsfs']").val("False");

//                }
//            });


        });
       
     

    </script>

</body>
</html>
