<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Qyzsgq_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Gzyj.Qyzsgq_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业证书过期</title>
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

</head>
<body style="background-color: #EEEEEE;">
    <form id="formsearch" class="l-form" style="padding: 0px 0px 0px 0px; margin: 1px;">
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="11%" class="td-text">
                企业名称
            </td>
            <td width="22%" class="td-value">
                <input type="text" class="field s-text" name="qymc" style="width: 200px" />
            </td>
            <td class="td-text" width="11%">
                法定代表人
            </td>
            <td class="td-value" width="22%">
                <input type="text" class="field s-text" name="fddbr" style="width: 200px" />
            </td>
            <td class="td-text" width="11%">
                证书编号
            </td>
            <td class="td-value" width="22%">
                <input type="text" class="field s-text" name="zsbh" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td colspan="6" class="td-value">
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
        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [

//               { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
               { display: '单位名称', name: 'qymc', align: 'left', type: "text", width: "15%",
                   render: function(item) {
                   if (item.qymc != null && item.qymc != "") {
                       return "<a target='_blank' href='../Szqy/Qyxx_View.aspx?befrom=&qyid=" + item.qyID + "' style='color:#000066;text-decoration: none;' >" + item.qymc + "</a>";
                       }
                   }
               },
                { display: '单位地址', name: 'xxdd', align: 'left', type: "text", width: "15%" },
                { display: '法定代表人', name: 'fddbr', align: 'center', type: 'text', width: "10%" },
                { display: '联系人', name: 'lxr', align: 'center', type: 'text', width: "10%" },
                { display: '联系电话', name: 'lxdh', align: 'center', type: 'text', width: "10%" },
                { display: '证书类型', name: 'zslx', align: 'center', type: 'text', width: "10%" },
                { display: '证书编号', name: 'zsbh', align: 'center', type: 'text', width: "10%" },
                { display: '证书有效起日期', name: 'zsyxrq', align: 'center', type: 'text', width: "10%" },
                { display: '证书有效止日期', name: 'zsyxzrq', align: 'center', type: 'text', width: "10%" }
                //                { display: '属地', name: 'COUNTY', align: 'center', type: 'text', width: "10%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=qyzsgq',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight()
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);

            $("input[name='aa']").click(function() {

                var str = "";
                $("input[name='aa']:checkbox").each(function() {
                    if ($(this).attr("checked") == true) {

                        str += $(this).val() + ","
                    }
                })
                if (str != "")
                    str = str.substr(0, str.length - 1);

                $("input[name='dwflid']").val(str);

            });
        });  


    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
