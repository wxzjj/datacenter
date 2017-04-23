<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wbaj_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Gzyj.Wbaj_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>未安监项目</title>
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
                <input type="text" class="field  s-text" name="PrjName" style="width: 200px" />
            </td>
            <td width="11%" class="td-text">
                建设单位
            </td>
            <td width="22%" class="td-value">
                <input type="text" class="field  s-text" name="BuildCorpName" style="width: 200px" />
            </td>
            <td class="td-text">
                合同开工日期
            </td>
            <td class="td-value">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 120px;">
                            <input type="text" class="field" id="kgrq1" name="BDate" op="greaterorequal" />
                        </td>
                        <td align="center" style="width: 15px">
                            至
                        </td>
                        <td>
                            <input type="text" class="field" id="kgrq2" name="EDate" op="lessorequal" />
                        </td>
                    </tr>
                </table>
            </td>
            <%-- <td width="11%" class="td-text">
                  施工单位 
            </td>
            <td width="22%" class="td-value">
                 <input type="text" class="field  s-text" name="sgdw" style="width: 200px" /> 
            </td>--%>
        </tr>
        <tr>
            <%--<td class="td-text">
                项目经理
            </td>
            <td class="td-value">
                <input type="text" class="field s-text" name="xmjl" style="width: 200px" />
            </td>--%>
            <td class="td-text">
                所属区
            </td>
            <td class="td-value" colspan="5">
                <input name="aa" type="checkbox" value="市区" />市辖区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="锡山" />锡山区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="惠山" />惠山区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="滨湖" />滨湖区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="新区" />新区&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="江阴" />江阴市&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="宜兴" />宜兴市&nbsp;&nbsp; <span style="display: none;">
                    <input type="text" class="field" name="County" op="equal" /></span>
            </td>
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
            $("#kgrq1").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });
            $("#kgrq2").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });
            manager = $("#maingrid").ligerGrid({
                columns: [

                //               { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
                //               { display: '安全监督号', name: 'AQJDDABH', align: 'center', type: "text", width: "12%" },
                 {display: '项目编号', name: 'PrjNum', align: 'center', type: 'text', width: "25%" },
             { display: '项目名称', name: 'PrjName', align: 'left', type: "text", width: "25%",
                 render: function(item) {
                     if (item.PKID != null && item.PKID != "") {
                         return "<a target='_blank' href='/IntegrativeShow2/SysFiles/PagesZHJG/Zhjg_Lxxmdj_View.aspx?LoginID=<%=this.WorkUser.UserID %>&PKID=" + item.PKID + "' style='color:#000066;text-decoration: none;' >" + item.PrjName + "</a>";
                     }
                 }
             },
                { display: '建设单位', name: 'BuildCorpName', align: 'left', type: "text", width: "25%",
                    render: function(item) {
                        if (item.BuildCorpName != null && item.BuildCorpName != "") {
                            return "<a target='_blank' href='/WxjzgcjczyPage/Szqy/JsdwxxToolBar.aspx?jsdwid=" + item.BuildCorpCode + "' style='color:#000066;text-decoration: none;' >" + item.BuildCorpName + "</a>";
                        }
                    }
                },
                //                 { display: '施工单位', name: 'sgdw', align: 'left', type: "text", width: "20%",
                //                     render: function(item) {
                //                     if (item.sgdw != null && item.sgdw != "") {
                //                             //return "<a target='_blank' href='../Szqy/Qyxx_View.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.QYROWID + "' style='color:#000066;text-decoration: none;' >" + item.SGDW + "</a>";
                //                         return "<a  style='color:#000066;text-decoration: none;' >" + item.sgdw + "</a>";
                //                         
                //                         }
                //                     }
                //                 },
                //                  { display: '项目经理', name: 'xmjl', align: 'center', type: "text", width: "10%",
                //                      render: function(item) {
                //                      if (item.xmjl != null && item.xmjl != "") {
                //                             // return "<a target='_blank' href='../Zyry/Ryxx_View.aspx?LoginID=<%=this.WorkUser.UserID %>&rowid=" + item.RYROWID + "' style='color:#000066;text-decoration: none;' >" + item.XMJL + "</a>";
                //                          return "<a style='color:#000066;text-decoration: none;' >" + item.xmjl + "</a>";
                //                          
                //                          }
                //                      }
                //                  },
                {display: '开工日期', name: 'BDate', align: 'center', type: 'text', width: "10%" },
                { display: '所属区', name: 'County', align: 'center', type: 'text', width: "15%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=waqjd',
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

                $("input[name='County']").val(str);


            });


        });
     


    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
