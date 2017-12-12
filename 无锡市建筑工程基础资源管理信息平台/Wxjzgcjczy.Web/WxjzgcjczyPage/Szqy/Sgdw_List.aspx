<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sgdw_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Sgdw_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工单位信息列表</title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

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

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <%--   <script src="../../LigerUI/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>--%>
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
                资质名称(主项)
            </td>
            <td class="td-value">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="200px">
                            <input id="iptZhux" type="text" class="field s-text" name="zhuxzz" style="width: 200px"
                                onclick="SetnPos('iptZhux')" readonly="readonly" op="equal" />
                        </td>
                        <td align="left" valign="middle">
                            <div onmouseover="this.style.cursor='hand'" onclick="clearVal('iptZhux')" style="width: 21px">
                                <img src="../../SparkClient/images/close.jpg" style="vertical-align: middle" width="20px"
                                    height="20px" /></div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td-text">
                资质名称(增项)
            </td>
            <td class="td-value">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="200px">
                            <input id="iptZengx" type="text" class="field s-text" name="zengxzz" style="width: 200px"
                                readonly="readonly" onclick="SetnPos('iptZengx')" op="equal" />
                        </td>
                        <td align="left" valign="middle">
                            <div onmouseover="this.style.cursor='hand'" onclick="clearVal('iptZengx')" style="width: 21px">
                                <img src="../../SparkClient/images/close.jpg" style="vertical-align: middle" width="20px"
                                    height="20px" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="td-text">
                单位类型
            </td>
            <td class="td-value" colspan="3" >
                <input name="aa" type="checkbox" value="1" />建筑施工&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="2" />设计施工一体化&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="13" />房屋拆迁&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="3" />园林绿化&nbsp;&nbsp;
                <input name="aa" type="checkbox" value="14" />安全生产许可证
                
                 <span style="display: none;">
                    <input type="text" class="field" name="csywlxid" op="equal" /></span>
            </td>
            
        </tr>
        <tr>
            <td class="td-text">
           
            属地
            </td>
              <td class="td-value" id="Td3" >
          <%--    <select class="field s-text" name="SbToStState" op="equal" style="width: 200px;">
                    <option value="">请选择</option>
                    <option value="-1">未上报</option>
                    <option value="0">已上报</option>
                    <option value="1">上报出错</option>
                </select>--%>
                 <select class="field s-text" name="CountyID"  op="equal"  style="width: 250px;">
                </select>
            </td>
             <td width="15%" class="td-text">
                
            </td>
            <td width="35%" class="td-value" id="sylxid">
              <%--  <select class="field s-text" name="sylxid" op="equal" style="width: 200px;">
                    <option value="">请选择</option>
                    <option value="1">本地企业</option>
                    <option value="2">外地企业</option>
                </select>--%>
                <%--<div style=" display:none;">
                      <input  type="text" class="field s-text" name="county" op="equal" />
                </div>--%>
                
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
    <div id="divZzmc" style="position: absolute; display: none; background-color: White;">
        <div width="95%" style="background-color: #f0f6e4">
            <table style="background-color: White;" width="200px">
                <tr>
                    <td style="vertical-align: middle; font-size: 12px;" width="90%">
                        &nbsp; --请选择--
                    </td>
                    <td align="right" width="10%">
                        <div onclick="closeDIV('divZzmc')" onmouseover="this.style.cursor='hand'">
                            <img src="../../SparkClient/images/close.jpg" width="15px" height="15px" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <iframe frameborder="0" id="Iframe1" name="IframeTreeZzmc" src="Zzmc_Tree.aspx" width="200px"
            height="350px" scrolling="no" style="border: none;"></iframe>
    </div>
    <div style="display: none">
        <input id="ipt" type="text" class="field" name="ipt" />
    </div>

    <script type="text/javascript">
        var manager;
        $(function() {



            $.ajax({
                type: 'post', cache: false, dataType: 'text',
                url: '../Handler/Data.ashx?type=QySsdq',
                async: false,
                success: function(result) {

                    if (result) {
                        $("select[name='CountyID']").html(result);
                    }
                },
                error: function() {

                }
            });

            $("#Td3").ligerForm({
                toJSON: JSON2.stringify
            });

            $("[name='county']").ligerComboBox({
                isMultiSelect: false
            });


            manager = $("#maingrid").ligerGrid({
                columns: [

                //                { display: '序号', name: 'ROWNUMBER___', align: 'center', type: "text", width: "5%" },
                {display: '单位名称', name: 'qymc', align: 'left', type: "text", width: "25%",
                render: function(item) {
                    if (item.qymc != null && item.qymc != "") {
                        return "<a target='_blank' href='QyxxToolBar.aspx?qyid=" + item.qyid + "&befrom=sgdw' style='color:#000066;text-decoration: none;' >" + item.qymc + "</a>";
                    }
                }
            },
                { display: '组织机构代码(社会信用代码)', name: 'zzjgdm', align: 'center', type: "text", width: "13%" },
                { display: '单位地址', name: 'zcdd', align: 'left', type: "text", width: "37%" },
                { display: '联系电话', name: 'lxdh', align: 'center', type: 'text', width: "10%" },
            //                { display: '联系人', name: 'lxr', al   ign: 'center', type: 'text', width: "10%" },
            //                { display: '联系电话', name: 'lxdh', align: 'center', type: 'text', width: "10%" },
                {display: '单位类型', name: 'csywlx', align: 'center', type: 'text', width: "7%" },
                { display: '属地', name: 'pcc', align: 'center', type: 'text', width: "6%" }
//                { display: '上报状态', name: 'SbState', align: 'center', type: 'text', width: "6%" }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
            url: 'List.ashx?fromwhere=Qyxx&qylx=sgdw',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 15,
            rownumbers: false,
            alternatingRow: true,
            checkbox: false,
            height: getGridHeight(),
            headerRowHeight :30
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


        function SetnPos(obj) {
            window.frames["Iframe1"].LoadSjmlZTree();
            $("[id$='ipt']").val(obj);
            var pos = getAbsolutePosition(document.getElementById(obj));
            var div = document.getElementById("divZzmc");
            div.style.display = "block";
            div.style.left = pos.x + "px";
            div.style.top =  pos.y + "px";


        }

        function getAbsolutePosition(obj) {
            position = new Object();
            position.x = 0;
            position.y = 0;
            var tempobj = obj;
            while (tempobj != null && tempobj != document.body) {
                // if (window.navigator.userAgent.indexOf("MSIE") != -1) {
                if (!!window.ActiveXObject || "ActiveXObject" in window) {   //2015-3-10
                    position.x += tempobj.offsetLeft;
                    position.y += tempobj.offsetTop +12;
                }
               // else if (window.navigator.userAgent.indexOf("Firefox") != -1) {
                else if (navigator.userAgent.indexOf("Firefox") != -1) {    //2015-3-10
                    position.x += tempobj.offsetLeft;
                    position.y += tempobj.offsetTop;
                }
                tempobj = tempobj.offsetParent
            }
            return position;
        }

        function closeDIV(obj) {
            (document.getElementById(obj)).style.display = "none";

        }

        function fSelectedZzmc(code, name) {
            var vName = $("[id$='ipt']").val();
            $("[id$='" + vName + "']").val(name);
        }

        function clearVal(obj) {
            $("[id$='" + obj + "']").val('');
        }
    </script>

</body>
</html>
