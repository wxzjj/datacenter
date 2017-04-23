<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yhxx_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl.Yhxx_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户信息列表</title>
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
                用户名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="USERNAME" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                登录名
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="LOGINNAME" style="width: 200px" />
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

        function f_btnClick(item) {
            switch (item.text) {
                case "新增用户":
                    f_add();
                    break;
            }
        }



        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
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
            activeDialog = parent.parent.$.ligerDialog.open(dialogOptions);
        }

        function f_reload() {
            manager && manager.loadData(true);
        }

        function f_add() {
            OpenWindow('../Yhgl/Yhxx_Edit.aspx?operate=add', "新增用户", 700, 350, false);
        }

        function f_edit(yhid) {
            OpenWindow('../Yhgl/Yhxx_Edit.aspx?operate=edit&id=' + yhid, "修改用户", 700, 350, false);
        }

        function f_editUserRole(userID) {
            OpenWindow('../Yhgl/RoleRight_List.aspx?id=' + userID, "权限管理", 1000, 550, false);
        }
        function f_delete(yhId) {
            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除该用户信息吗?', function(confirm) {
                if (confirm) {
                    win.waitting("正在删除中...");
                    $.getJSON('Delete.ashx?operate=user&UserID=' + yhId, { Rnd: Math.random() }, function(data) {
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

            var hasAdd = '<%=HasAdd %>';
            var hasEdit = '<%=HasEdit %>';
            var hasDelete = '<%=HasDelete %>';

            var columns = [
                    { display: '用户名称', align: 'center', type: "text", width: "12%",
                        render: function(item) {
                            if (item.UserID != null && item.UserID != "") {
                                return "<a  href='javascript:void(-1)'  style='color:#000066;text-decoration: none;' >" + item.UserName + "</a>";
                            }
                        }
                    },
                     { display: '所属单位', name: 'OrgUnitName', align: 'left', type: "text", width: "28%" },
                    { display: '登录名', name: 'LoginName', align: 'left', type: "text", width: "12%" },

                    { display: '创建时间', name: 'UserRegTime', align: 'center', type: 'text', width: "13%" },
                    { display: '用户类型', name: 'UserType', align: 'center', type: 'text', width: "13%" }
            ];

            if (hasEdit == "1") {
                columns.push({ display: '权限管理', align: 'center', type: "text", width: "5%",
                    render: function(item) {
                        if (item.UserID != null && item.UserID != "") {
                            return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_editUserRole(" + item.UserID + ")' >编辑</a>";
                        }
                    }
                });

                columns.push({ display: '编辑', align: 'center', type: "text", width: "5%",
                    render: function(item) {
                        if (item.UserID != null && item.UserID != "") {
                            return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_edit(" + item.UserID + ")' >编辑</a>";
                        }
                    }
                });
            }

            if (hasDelete == "1") {
                columns.push({ display: '删除', align: 'center', type: "text", width: "5%",
                    render: function(item) {
                        if (item.UserID != null && item.UserID != "") {
                            return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick='f_delete(" + item.UserID + ")' >删除</a>";
                        }
                    }
                });
            }

            var toolbar = [];
            if (hasAdd == "1") {
                toolbar.push({ text: '新增用户', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' });
            }

            manager = $("#maingrid").ligerGrid({
                columns: columns,
                width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=yhxx',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight(),
                toolbar: { items: toolbar }

            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);



        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
