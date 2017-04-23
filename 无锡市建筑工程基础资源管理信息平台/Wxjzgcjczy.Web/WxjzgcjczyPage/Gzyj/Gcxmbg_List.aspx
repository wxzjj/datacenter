<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gcxmbg_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Gzyj.Gcxmbg_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程变更项目列表</title>
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
            <td width="11%" class="td-text">
                项目名称
            </td>
            <td width="22%" class="td-value">
                <input type="text" class="field  s-text" name="xmmc" style="width: 200px" />
            </td>
            <td width="11%" class="td-text">
                建设单位
            </td>
            <td width="22%" class="td-value">
                <input type="text" class="field  s-text" name="jsdw" style="width: 200px" />
            </td>
            <td width="11%" class="td-text">
                施工单位
            </td>
            <td width="22%" class="td-value">
                <input type="text" class="field  s-text" name="sgdw" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td class="td-text">
                项目变更名称
            </td>
            <td class="td-value">
                <input type="text" class="field  s-text" name="xmbgmc" style="width: 200px" />
            </td>
            <td class="td-text">
                项目变更部位
            </td>
            <td class="td-value">
                <input type="text" class="field  s-text" name="xmbgbw" style="width: 200px" />
            </td>
            <td class="td-text">
                项目变更内容
            </td>
            <td class="td-value">
                <input type="text" class="field  s-text" name="xmbgnr" style="width: 200px" />
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
        $(function() {


            $("#Td2").ligerForm({
                toJSON: JSON2.stringify
            });

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
                //               { display: '安全监督号', name: 'AQJDDABH', align: 'center', type: "text", width: "12%" },
               {display: '项目名称', name: 'XMMC', align: 'left', type: "text", width: "20%",
               render: function(item) {
                   if (item.ROW_ID != null && item.ROW_ID != "") {
                       return "<a target='_blank' href='Gcxmbg_View.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.ROW_ID + "' style='color:#000066;text-decoration: none;' >" + item.XMMC + "</a>";
                   }
               }
           },
                { display: '建设单位', name: 'JSDW', align: 'left', type: "text", width: "20%",
                    render: function(item) {
                    if (item.JSDW != null && item.JSDW != "") {
                            return "<a target='_blank' href='../Szqy/Jsdw_View.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.JSDWROWID + "' style='color:#000066;text-decoration: none;' >" + item.JSDW + "</a>";
                        }
                    }
                },
                 { display: '施工单位', name: 'SGDW', align: 'left', type: "text", width: "20%",
                     render: function(item) {
                     if (item.SGDW != null && item.SGDW != "") {
                             return "<a target='_blank' href='../Szqy/Qyxx_View.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.QYROWID + "' style='color:#000066;text-decoration: none;' >" + item.SGDW + "</a>";
                         }
                     }
                 },
                { display: '项目变更名称', name: 'XMBGMC', align: 'center', type: "text", width: "15%" },
                { display: '项目变更部位', name: 'XMBGBW', align: 'center', type: "text", width: "10%" },
                { display: '变更估算金额(万元)', name: 'BGGSJE', align: 'center', type: 'text', width: "10%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=gcbgxm',
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

                $("input[name='ssdq']").val(str);


            });


        });
     


    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
