<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zljd_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Zljd_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质量监督信息列表</title>
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
                质监项目
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field  s-text" name="xmmc" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                建设单位
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field  s-text" name="jsdw" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                施工单位
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field  s-text" name="sgdw" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                项目经理
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field  s-text" name="xmjl" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                质量报监时间
            </td>
            <td width="35%" class="td-value">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 120px;">
                            <input type="text" class="field" id="bjsj1" name="zljdslsj1" op="greaterorequal" />
                        </td>
                        <td align="center" style="width: 15px">
                            至
                        </td>
                        <td>
                            <input type="text" class="field" id="bjsj2" name="zljdslsj2" op="lessorequal" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15%" class="td-text">
                是否竣工
            </td>
            <td width="35%" class="td-value" id="Td2">
                <select class="field s-text" name="isjg" op="equal" style="width: 200px;">
                    <option value="">所有</option>
                    <option value="否">在建</option>
                    <option value="是">竣工</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="td-text">
                所属区
            </td>
            <td class="td-value" colspan="2">
                <input name="aa" type="checkbox" value="市区" />市辖区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="锡山" />锡山区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="惠山" />惠山区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="滨湖" />滨湖区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="新区" />新区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="江阴" />江阴市&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="宜兴" />宜兴市&nbsp;&nbsp; <span style="display: none;">
                    <input type="text" class="field" name="ssdq" op="equal" /></span>
            </td>
            <td class="td-value">
                <div id="btn_search" style="text-align: right">
                </div>
            </td>
        </tr>
        <%--  <tr>
            <td colspan="6" class="td-value">
                <div id="btn_search" style="text-align: right">
                </div>
            </td>
        </tr>--%>
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

            $("#bjsj1").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });
            $("#bjsj2").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
               { display: '质监监督号', name: 'ZLJDBH', align: 'center', type: "text", width: "12%" },
               { display: '质监项目', name: 'XMMC', align: 'left', type: "text", width: "15%",
                   render: function(item) {
                       if (item.XMMC != null && item.XMMC != "") {
                           return "<a target='_blank' href='SggcToolBar.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.ROW_ID + "' style='color:#000066;text-decoration: none;' >" + item.XMMC + "</a>";
                       }
                   }
               },
                { display: '建设单位', name: 'JSDW', align: 'left', type: "text", width: "15%",
                    render: function(item) {
                        if (item.JSDW != null && item.JSDW != "") {
                            return "<a target='_blank' href='../Szqy/JsdwxxToolBar.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.JSDWROWID + "' style='color:#000066;text-decoration: none;' >" + item.JSDW + "</a>";
                        }
                    }
                },
                 { display: '施工单位', name: 'SGDW', align: 'left', type: "text", width: "15%",
                     render: function(item) {
                         if (item.SGDW != null && item.SGDW != "") {
                             return "<a target='_blank' href='../Szqy/QyxxToolBar.aspx?LoginID=<%=this.WorkUser.UserID %>&dwlx=sgdw&rowid=" + item.QYROWID + "' style='color:#000066;text-decoration: none;' >" + item.SGDW + "</a>";
                         }
                     }
                 },
                { display: '项目经理', name: 'XMJL', align: 'center', type: "text", width: "10%",
                    render: function(item) {
                        if (item.XMJL != null && item.XMJL != "") {
                            return "<a target='_blank' href='../Zyry/RyxxToolBar.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.RYROWID + "' style='color:#000066;text-decoration: none;' >" + item.XMJL + "</a>";
                        }
                    }
                },
                { display: '质量报监时间', name: 'ZLJDSLSJ', align: 'center', type: 'text', width: "10%" },
                { display: '是否竣工', name: 'ISJG', align: 'center', type: 'text', width: "8%" },
                { display: '所属区', name: 'SSDQ', align: 'center', type: 'text', width: "10%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=Xmxx&xmlx=zljd',
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
