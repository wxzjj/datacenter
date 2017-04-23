<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xykp_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xytx.Xykp_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目位置信息列表</title>
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
                企业名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="qymc" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                企业组织机构代码（社会信用代码）
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="zzjgdm" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                资质类别
            </td>
            <td width="35%" class="td-value">
                <input name="bb" type="checkbox" value="总承包企业" />总承包企业&nbsp;
                <input name="bb" type="checkbox" value="专业承包企业" />专业承包企业&nbsp;
                <input name="bb" type="checkbox" value="一体化" />一体化&nbsp;
                <input name="bb" type="checkbox" value="劳务分包" />劳务分包 <span style="display: none;">
                    <input type="text" class="field" name="zzlb" op="equal" /></span>
            </td>
            <td width="15%" class="td-text">
                企业属地
            </td>
            <td width="35%" class="td-value">
                <input name="aa" type="checkbox" value="崇安区" />崇安区&nbsp;
                <input name="aa" type="checkbox" value="南长区" />南长区&nbsp;
                <input name="aa" type="checkbox" value="北塘区" />北塘区&nbsp;
                <input name="aa" type="checkbox" value="惠山区" />惠山区&nbsp;
                <input name="aa" type="checkbox" value="滨湖区" />滨湖区&nbsp;
                <input name="aa" type="checkbox" value="新区" />新区<br />
                <input name="aa" type="checkbox" value="宜兴市" />宜兴市&nbsp;
                <input name="aa" type="checkbox" value="江阴市" />江阴市&nbsp;
                <input name="aa" type="checkbox" value="省内外市" />省内外市&nbsp;
                <input name="aa" type="checkbox" value="省外企业" />省外企业 <span style="display: none;">
                    <input type="text" class="field" name="qysd" op="equal" />
                </span>
            </td>
        </tr>
        <tr>
            <td class="td-value" colspan="4" style="height: 35px;">
                <table width="100%">
                    <tr>
                        <td style="height: 100%;">
                            <a href="http://218.90.162.101:8088/NJJGCredit/loginSSO.aspx?username=admin&pwd=22222"
                                target="_blank" title="点击进入信用考评系统" style="color: Blue; font-size: 15px; margin-left: 10px;">
                                进入信用考评系统</a>
                        </td>
                        <td>
                            <div id="btn_search" style="text-align: right; float: left">
                            </div>
                        </td>
                    </tr>
                </table>
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

        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                //            { text: '保存', onclick: function(item, dialog) {
                //                dialog.frame.f_save(dialog, manager);
                //            }
                //            },
            {text: '关闭', onclick: function(item, dialog) {
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


        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '企业名称', align: 'left', type: "text", width: "30%",
                   render: function(item) {
                       if (item.qyID != null && item.qyID != "") {
                           return "<a target='_blank'  href='../Szqy/QyxxToolBar.aspx?qyid=" + item.qyID + "&befrom=sgdw' style='color:#000066;text-decoration: none;' >" + item.qymc + "</a>";
                       }
                       else {
                           return item.qymc;

                       }
                   }
               },
                 { display: '企业组织机构代码(社会信用代码)', name: 'zzjgdm', align: 'center', type: "text", width: "15%" },
                { display: '企业资质类型', name: 'zzlb', align: 'center', type: "text", width: "13%" },
                  { display: '企业所属地', name: 'qysd', align: 'center', type: "text", width: "7%" },
                  { display: '基本分', name: 'jbf', align: 'right', type: "text", width: "8%" },
                   { display: '综合大检查得分', name: 'zhdjcdf', align: 'right', type: "text", width: "8%" },
                   { display: '日常考核扣分', name: 'rckhkf', align: 'right', type: "text", width: "8%" },
                   { display: '信用分', name: 'xyf', align: 'right', type: "text", width: "9%" }


                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=qyxykp&qylx=<%=qylx %>',
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


            $("input[name='bb']").click(function() {

                var str = "";
                $("input[name='bb']:checkbox").each(function() {
                    if ($(this).attr("checked") == true) {

                        str += $(this).val() + ","
                    }
                })
                if (str != "")
                    str = str.substr(0, str.length - 1);

                $("input[name='zzlb']").val(str);


            });

            $("input[name='aa']").click(function() {

                var str = "";
                $("input[name='aa']:checkbox").each(function() {
                    if ($(this).attr("checked") == true) {

                        str += $(this).val() + ","
                    }
                })
                if (str != "")
                    str = str.substr(0, str.length - 1);

                $("input[name='qysd']").val(str);


            });


        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
