<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleRights_Edit.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl.RoleRights_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色权限设置</title>
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

    <style type="text/css">
        .title
        {
            font-size: 16px;
            font-weight: bold;
        }
        .moduleTitleCss
        {
            font-size: 15px;
            color: Green;
            font-weight: bold;
        }
        .operateItemCss
        {
            font-size: 14px;
            width: 70px;
        }
        .ulCss
        {
        	list-style-type:none;
        	float:left;
        }
        .ulCss li
        {
        	list-style-type:none;
        	float:left;
        }
        .cbBox
        {
        	height:30px; vertical-align:top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div style="margin: 0 5px; float: none;">
        <table id="table1" style="width: 100%;">
            <caption class="title">
                角色权限设置</caption>
        </table>

        <script type="text/javascript">

            var dia;
            var mana;
            var roleID = '<%=RoleID %>';
            function f_save(dialog, manager) {
                dia = dialog;
                mana = manager;
                $.ligerDialog.waitting("正在保存中,请稍后...");
                //$("#btn_save").click();
                //var aa = [{ moduleCode: '1', operateCode: '1' }, { moduleCode: '2', operateCode: '3'}];
                var str = "[";
                var arr = $("#table1").find("input[type='checkbox']:checked");


                for (var i = 0; i < arr.length; i++) {

                    if (i == 0) {
                        str += "{";
                    }
                    else {
                        str += ",{";
                    }

                    str += '"moduleCode":"' + $(arr[i]).attr("name") + '","operateCode":"' + $(arr[i]).attr("value") + '"';

                    str += "}";

                }
                str += "]";


                $.post("../Handler/Data.ashx?type=saveRoleRights", { RoleID: roleID, RoleRights: str }, function(result) {
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

            function f_SaveResult(mes) {
                $.ligerDialog.closeWaitting();
                window.LG.showSuccess(mes, function() {
                    dia.hide();
                    mana.loadData(true);
                });
            }

            $(function() {

                $.getJSON('../Handler/Data.ashx?type=roleRights&roleId=<%=RoleID %>', { Rnd: Math.random() }, function(data) {

                    if (data) {
                        var aa = data;
                        //$("#div1").html(data.length);
                        for (var i = 0; i < aa.length; i++) {

                            var trTitle = aa[i].moduleName;
                            var html = "";
                            if (aa[i].rights.length > 0) {
                                html += "<ul class='ulCss'>";
                            }

                            for (var j = 0; j < aa[i].rights.length; j++) {
                                html += "<li class='operateItemCss'>";
                                if (aa[i].rights[j].hasRights)
                                    html += '<input type="checkbox" name="' + aa[i].moduleCode + '" checked="checked" value="' + aa[i].rights[j].operateCode + '" />' + aa[i].rights[j].operateName;
                                else
                                    html += '<input type="checkbox" name="' + aa[i].moduleCode + '"  value="' + aa[i].rights[j].operateCode + '" />' + aa[i].rights[j].operateName; ;
                                html += "</li>";
                            }
                            if (aa[i].rights.length > 0) {
                                html += "</ul>";
                            }

                            $("#table1").append("<tr><td class='moduleTitleCss'>" + trTitle + "</td></tr><tr><td class='cbBox'>" + html + "</td></tr>");
                        }
                    }
                    else {

                    }

                });


                //var aa = [{ "moduleCode": "ajxx", "moduleName": "安监信息", "rights": [{ "operateCode": "View", "operateName": "查看", "hasRights": true }, { "operateCode": "Create", "operateName": "填报", "hasRights": true}] }, { "moduleCode": "zjxx", "moduleName": "质监信息", "rights": [{ "operateCode": "View", "operateName": "查看", "hasRights": true }, { "operateCode": "Create", "operateName": "填报", "hasRights": false}]}];




            });
        </script>

    </div>
    </form>
</body>
</html>
