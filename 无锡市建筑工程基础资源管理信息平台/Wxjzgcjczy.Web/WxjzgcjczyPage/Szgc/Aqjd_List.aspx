<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aqjd_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Aqjd_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>安全监督信息列表</title>
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
                安监项目
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
                项目经理
            </td>
            <td class="td-value">
                <input type="text" class="field  s-text" name="xmjl" style="width: 200px" />
            </td>
            <td class="td-text">
                承包类型
            </td>
            <td class="td-value" id="Td1">
                <select class="field s-text" name="cblx" op="equal" style="width: 200px;">
                    <option value="">所有</option>
                    <option value="总承包">总承包</option>
                    <option value="其他">其他</option>
                </select>
            </td>
            <%--  <td class="td-text">
                安全报监时间
            </td>
            <td class="td-value">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 120px;">
                            <input type="text" class="field" id="bjsj1" name="aqjdslsj1" op="greaterorequal" />
                        </td>
                        <td align="center" style="width: 15px">
                            至
                        </td>
                        <td>
                            <input type="text" class="field" id="bjsj2" name="aqjdslsj2" op="lessorequal" />
                        </td>
                    </tr>
                </table>
            </td>--%>
            <td class="td-text">
                项目状态
            </td>
            <td class="td-value" id="Td2">
                <select id="aqjdflag" class="field s-text" name="aqjdflag" op="equal" style="width: 200px;">
                    <option value="">所有</option>
                    <option value="-1">新增待审</option>
                    <option value="0">在建</option>
                    <option value="1">竣工</option>
                    <option value="2">完工未办手续</option>
                    <option value="3">停工</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="td-text">
                所属区
            </td>
            <td class="td-value" colspan="3">
                <input id="sq" name="aa" type="checkbox" value="市区" />市辖区&nbsp;&nbsp;
                <input id="xs" name="aa" type="checkbox" value="锡山" />锡山区&nbsp;&nbsp;
                <input id="hs" name="aa" type="checkbox" value="惠山" />惠山区&nbsp;&nbsp;
                <input id="bh" name="aa" type="checkbox" value="滨湖" />滨湖区&nbsp;&nbsp;
                <input id="xq" name="aa" type="checkbox" value="新区" />新区&nbsp;&nbsp;
                <input id="jy" name="aa" type="checkbox" value="江阴" />江阴市&nbsp;&nbsp;
                <input id="yx" name="aa" type="checkbox" value="宜兴" />宜兴市&nbsp;&nbsp; <span style="display: none;">
                    <input type="text" class="field" name="ssdq" op="equal" /></span>
            </td>
            <td class="td-text">
                审核状态
            </td>
            <td class="td-value" id="Td3">
                <select id="status" class="field s-text" name="aqjd_status" op="equal" style="width: 200px;">
                    <option value="">所有</option>
                    <option value="-1">退回</option>
                    <option value="0">未审</option>
                    <option value="1">信用审核通过</option>
                    <option value="2">报监待审</option>
                    <option value="3">报监审核通过</option>
                </select>
            </td>
            <%--  <td class="td-text">
                承包类型
            </td>
            <td class="td-value" id="Td1">
                <select class="field s-text" name="cblx" op="equal" style="width: 200px;">
                    <option value="">所有</option>
                    <option value="总承包">总承包</option>
                    <option value="其他">其他</option>
                </select>
            </td>--%>
        </tr>
        <tr>
            <td class="td-value" colspan="6">
                <div id="btn_search" style="text-align: right">
                </div>
            </td>
        </tr>
        <%-- <tr>
            <td colspan="6" class="td-value">
              
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
        var state = '<%=state %>';
        var ssdq = '<%=ssdq %>';
        $(function() {
            //            $("#Td2").ligerForm({
            //                toJSON: JSON2.stringify
            //            });


            if (state != null && state != '') {
                var obj = document.getElementById("aqjdflag");
                switch (state) {
                    case "zj":
                        $("#aqjdflag").attr("value", "0");

                        break;
                    case "jg":
                        obj.options[1].select = true;
                        $("#aqjdflag").attr("value", "1");
                        break;

                }
            }

            if (ssdq != null && ssdq != '') {
                switch (ssdq) {
                    case "sxq":
                        $("input[id='sq']").attr("checked", "checked");
                        $("input[name='ssdq']").val("市区");
                        break;
                    case "xsq":
                        $("input[id='xs']").attr("checked", "checked");
                        $("input[name='ssdq']").val("锡山");
                        break;
                    case "hsq":
                        $("input[id='hs']").attr("checked", "checked");
                        $("input[name='ssdq']").val("惠山");
                        break;
                    case "bhq":
                        $("input[id='bh']").attr("checked", "checked");
                        $("input[name='ssdq']").val("滨湖");
                        break;
                    case "xq":
                        $("input[id='xq']").attr("checked", "checked");
                        $("input[name='ssdq']").val("新区");
                        break;
                    case "jy":
                        $("input[id='jy']").attr("checked", "checked");
                        $("input[name='ssdq']").val("江阴");
                        break;
                    case "yx":
                        $("input[id='yx']").attr("checked", "checked");
                        $("input[name='ssdq']").val("宜兴");
                        break;
                }
            }




            //            $("#bjsj1").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });
            //            $("#bjsj2").ligerDateEditor({ showTime: false, label: '', labelWidth: 100, labelAlign: 'left' });


            var prameters = "";
            var rule = LG.bulidFilterGroup("#formsearch");
            if (rule.rules.length) {
                prameters = JSON2.stringify(rule);
            }

            manager = $("#maingrid").ligerGrid({
                columns: [

                { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
               { display: '安全监督号', name: 'AQJDDABH', align: 'center', type: "text", width: "12%" },
               { display: '安监项目', name: 'XMMC', align: 'left', type: "text", width: "15%",
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
                { display: '审核状态', name: 'SFSH', align: 'center', type: 'text', width: "10%" },
                { display: '是否竣工', name: 'SFJG', align: 'center', type: 'text', width: "10%" },
                //                    render: function(item) {
                //                        if (item.XMMC != null && item.XMMC != "") {
                //                            if (item.AQJDFLAG != "0") {
                //                                return "是";
                //                            }
                //                            else
                //                                return "否";
                //                        }
                //                    }
                //                },
                {display: '所属区', name: 'SSDQ', align: 'center', type: 'text', width: "7%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=Xmxx&xmlx=aqjd',
                parms: { where: prameters },
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
            //LG.addSearchButtonsWithSearchForm("#formsearch", "#btn_search", manager,"",true);

            //$("#btn_search").click();

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
