﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjjg_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Zjjg_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中介机构信息列表</title>
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
                <input type="text" class="field s-text" name="qymc" style="width: 200px" />
            </td>
                 <td width="15%" class="td-text">
                组织机构代码(社会信用代码)
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="zzjgdm" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td class="td-text">
                单位类型
            </td>
            <td class="td-value" colspan="3">
                <input name="aa" type="checkbox" value="4" />工程监理&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="7" />招标代理&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="8" />造价咨询&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="9" />工程检测&nbsp;&nbsp; 
                <input name="aa" type="checkbox" value="15" />施工图审查&nbsp;&nbsp; 
                <input name="aa" type="checkbox" value="16" />房地产估价&nbsp;&nbsp; 
                <input name="aa" type="checkbox" value="17" />物业服务
                  
                <span style="display: none;">
                    <input type="text" class="field" name="csywlxid" op="equal" /></span>
            </td>
        </tr>
        <tr>
          <td width="15%" class="td-text">
                属地
            </td>
            <td width="35%" class="td-value" id="sylxid">
                
                 <select class="field s-text" name="CountyID" op="equal" style="width: 250px;">
                   
                </select>
            </td>
             <td width="15%" class="td-text">
                
            </td>
            <td width="35%" class="td-value" >
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

        $.ajax({
            type: 'post', cache: false, dataType: 'text',
            url: '../Handler/Data.ashx?type=ssdq',
            async: false,
            success: function(result) {

                if (result) {
                    $("select[name='CountyID']").html(result);
                }
            },
            error: function() {

            }
        });
        
            $("#sylxid").ligerForm({
                toJSON: JSON2.stringify
            });


            manager = $("#maingrid").ligerGrid({
                columns: [
//               { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
               { display: '单位名称', name: 'qymc', align: 'left', type: "text", width: "20%",
                   render: function(item) {
                       if (item.qymc != null && item.qymc != "") {
                           return "<a target='_blank' href='QyxxToolBar.aspx?qyid=" + item.qyid + "&befrom=zjjg' style='color:#000066;text-decoration: none;' >" + item.qymc + "</a>";
                       }
                   }
               },
                 { display: '组织机构代码(社会信用代码)', name: 'zzjgdm', align: 'center', type: "text", width: "13%" },
                { display: '单位地址', name: 'xxdd', align: 'left', type: "text", width: "26%" },
                   { display: '营业执照注册号', name: 'yyzzzch', align: 'center', type: 'text', width: "13%" },
                { display: '企业联系电话', name: 'lxdh', align: 'center', type: 'text', width: "13%" },
//                { display: '联系人', name: 'lxr', align: 'center', type: 'text', width: "10%" },
//                { display: '联系电话', name: 'lxdh', align: 'center', type: 'text', width: "15%" },
             
                { display: '单位类型', name: 'csywlx', align: 'center', type: 'text', width: "7%" },
                { display: '属地', name: 'pcc', align: 'center', type: 'text', width: "6%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=Qyxx&qylx=zjjg',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
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

                $("input[name='csywlxid']").val(str);


            });


        });
    



      


    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
