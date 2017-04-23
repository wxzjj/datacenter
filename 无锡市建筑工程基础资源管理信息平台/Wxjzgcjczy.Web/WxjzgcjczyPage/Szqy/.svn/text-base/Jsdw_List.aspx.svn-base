<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jsdw_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Jsdw_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建设单位信息列表</title>
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
            <td width="15%" class="td-text">
                单位名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="jsdw" style="width: 200px" />
            </td>
           <td class="td-text" width="15%">
               组织机构代码<br />
               （社会信用代码）
            </td>
            <td class="td-value" width="35%">
             <input type="text" class="field s-text" name="zzjgdm" style="width: 200px" />
            </td>
        </tr>
         <tr>
            <td width="15%" class="td-text">
                信息来源
            </td>
            <td width="35%" class="td-value">
                <input name="bb" type="checkbox" value="江苏建设公共基础数据平台" />江苏建设公共基础数据平台 &nbsp;&nbsp; 
                 <input name="bb" type="checkbox" value="局一号通系统" />局一号通系统&nbsp;&nbsp; 
                <span style="display: none;">
                    <input id="tag1" type="text" class="field" name="tag" op="like" vt="string" >
                      <input id="tag2" type="text" class="field" name="tag" op="like" vt="string" ></span>
            </td>
             <td class="td-text" width="15%">
                单位类型
            </td>
            <td class="td-value" width="35%">
                <input name="aa" type="checkbox" value="1" />房地产开发企业
&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="3" />其它&nbsp;&nbsp; 
                <span style="display: none;">
                    <input type="text" class="field" name="dwflid" op="in" vt="int" /></span>
            </td>
        </tr>
        
        
        <tr>
            <td colspan="4" class="td-value">
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
               {display: '单位名称', align: 'left', type: "text", width: "27%",
               render: function(item) {
                   if (item.jsdw != null && item.jsdw != "") {
                       return "<a target='_blank' href='JsdwxxToolBar.aspx?jsdwid=" + item.jsdwid + "' style='color:#000066;text-decoration: none;' >" + item.jsdw + "</a>";
                   }
               }
           },
                { display: '组织机构代码(社会信用代码)', name: 'zzjgdm', align: 'center', type: 'text', width: "13%" },
                { display: '单位地址', name: 'dwdz', align: 'left', type: "text", width: "33%" },
                { display: '法定代表人', name: 'fddbr', align: 'center', type: 'text', width: "8%" },
                { display: '联系人', name: 'lxr', align: 'center', type: 'text', width: "8%" },
                //                { display: '联系电话', name: 'LXDH', align: 'center', type: 'text', width: "15%" },
                {display: '单位类型', name: 'dwfl', align: 'center', type: 'text', width: "9%" }
                //                { display: '属地', name: 'COUNTY', align: 'center', type: 'text', width: "10%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=Qyxx&qylx=jsdw',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                sortname: 'xgrqsj',
                sortorder: 'desc',
                height: getGridHeight(),
                headerRowHeight: 30
                
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

            $("input[name='bb']").click(function() {

                $("input[name='bb']:checkbox").each(function(i) {

                    if (i == 0) {
                        if ($(this).attr("checked") == true) {
                            $("input[id='tag1']").val($(this).val());
                        }
                        else {
                            $("input[id='tag1']").val("");
                        }
                    }
                    else {
                        if ($(this).attr("checked") == true) {
                            $("input[id='tag2']").val($(this).val());
                        }
                        else {
                            $("input[id='tag2']").val("");
                        }

                    }


                });
               

            });


        });  


    </script>

</body>
</html>
