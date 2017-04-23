<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleRight_List.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl.RoleRight_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户角色信息信息列表</title>
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
                角色名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="RoleName" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
            </td>
            <td width="35%" class="td-value">
            </td>
        </tr>
        <tr>
            <td class="td-value" colspan="6">
                <div id="btn_search" style="text-align: right">
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


        var dia;
        var mana;
        function f_save(dialog, manager) {
            dia = dialog;
            mana = manager;
            $.ligerDialog.waitting("正在保存中,请稍后...");
            //$("#btn_save").click();
            var arr = $("#maingrid").find(".l-grid-body-table").find("input[type='checkbox']:checked");
          
            var str = "";
            for (var i = 0; i < arr.length; i++) {
                if (i == 0)
                    str += $(arr[i]).attr('name');
                else
                    str += "," + $(arr[i]).attr('name');

            }

            var userId='<%=UserID %>';
            $.post("../Handler/Data.ashx?type=saveRoles", { UserID: userId, RoleIDs: str }, function(result) {
                if (result) {

                    if (result.isSuccess) {

                        f_SaveResult(result.Msg);
                    }
                    else {

                        showError(result.Msg);
                    }
                }
                else {
                    showError("获取服务器数据错误！");
                }

            }, 'json');


        }

        function f_btnClick(item) {
            switch (item.text) {
                case "新增角色":
                    f_add();
                    break;
            }
        }



        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, buttons: [
            { text: '保存', onclick: function(item, dialog) {
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
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function f_reload() {
            manager && manager.loadData(true);
        }

        function f_add() {
            OpenWindow('../Yhgl/Role_Edit.aspx?operate=add', "新增角色", 700, 350, false);
        }

        function f_edit(roleId) {
            OpenWindow('../Yhgl/Role_Edit.aspx?operate=edit&id=' + roleId, "编辑角色", 700, 350, false);
        }
        function f_editRoleRights(roleId) {
            OpenWindow('../Yhgl/RoleRights_Edit.aspx?roleId=' + roleId, "角色权限设置", 700, 350, false);
        }


        function f_SaveResult(mes) {
            $.ligerDialog.closeWaitting();
            window.LG.showSuccess(mes, function() {
                dia.hide();
                //mana.loadData(true);
            });
        }

        function showMsg(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(msg);
        }
        function showWarn(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.warn(msg);
        }
        function showError(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.error(msg);
        }
        
        function f_delete(roleId) {
            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除该角色信息吗?', function(confirm) {
                if (confirm) {
                    win.waitting("正在删除中...");
                    $.getJSON('Delete.ashx?operate=role&roleID=' + roleId, { Rnd: Math.random() }, function(data) {
                        if (data.Type == "Success") {
                            win.closeWaitting();
                            win.success("删除成功!");
                            f_reload();
                        }
                        else if (data.Type == "Error") {
                            win.closeWaitting();
                            win.error("删除失败!<BR>" + data.Message);
                        }
                    });
                }
            });
        }


        $(function() {

        var userID = '<%=UserID %>';

            manager = $("#maingrid").ligerGrid({
                columns: [
                 { display: '选择', align: 'center', type: "text", width: "5%",
                     render: function(item) {
                         if (item.RoleID != null && item.RoleID != "") {
                             if (item.HasRole > 0)
                                 return ' <input type="checkbox" name="' + item.RoleID + '" checked="checked"  />';
                             else
                                 return ' <input type="checkbox" name="' + item.RoleID + '"   />';
                         }
                     }
                 },

               { display: '角色名称', align: 'center', type: "text", width: "12%",
                   render: function(item) {
                       if (item.RoleID != null && item.RoleID != "") {
                           return "<a  href='javascript:void(-1)'  style='color:#000066;text-decoration: none;' >" + item.RoleName + "</a>";
                       }
                   }
               },
                 { display: '角色描述', name: 'RoleDesc', align: 'left', type: "text", width: "28%" },
                { display: '权限设置', align: 'center', type: "text", width: "8%",
                    render: function(item) {
                        if (item.RoleID != null && item.RoleID != "") {
                            return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_editRoleRights(" + item.RoleID + ")' >设置</a>";
                        }
                    }
                },
                  { display: '编辑', align: 'center', type: "text", width: "5%",
                      render: function(item) {
                          if (item.RoleID != null && item.RoleID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_edit(" + item.RoleID + ")' >编辑</a>";
                          }
                      }
                  },
                   { display: '删除', align: 'center', type: "text", width: "5%",
                       render: function(item) {
                           if (item.RoleID != null && item.RoleID != "") {
                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_delete(" + item.RoleID + ")' >删除</a>";
                           }
                       }
                   }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                   url: 'List.ashx?fromwhere=roleRight_List&userID=' + userID,
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight(),
                toolbar: { items: [
                { text: '新增角色', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' }
                ]
                }
                //                , onAfterShowData: function(currentData) {
                //                    //alert( JSON2.stringify( currentData));
                //                    for (var i = 0; i < currentData.Rows.length; i++) {
                //                        if (currentData.Rows[i].HasRight > 0) {

                //                            $("#maingrid").ligerGrid("select", i);
                //                        }
                //                    }

                //                }
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);

        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
