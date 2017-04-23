<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LxxmSelect.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.LxxmSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>安监信息列表</title>
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
                项目名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjName" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                项目编码
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjNum" style="width: 200px" />
            </td>
        </tr>  
        <tr>
            <td width="15%" class="td-text">
                建设单位名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="BuildCorpName" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                单位组织机构代码（社会信用代码）
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="BuildCorpCode" style="width: 200px" />
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
  
        function f_select(pkid,PrjNum,PrjName) {

            parent.OnXmSelectHandler(pkid, PrjNum, PrjName);

        }

        $(function() {

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '项目编号', name: 'PrjNum', align: 'center', type: "text", width: "16%" },
               { display: '项目名称', align: 'left', type: "text", width: "26%",
                   render: function(item) {
                       if (item.PKID != null && item.PKID != "") {
                           return item.PrjName;
                       }
                       else
                           return "";
                   }
               },
                { display: '建设单位名称', name: 'BuildCorpName', align: 'left', type: "text", width: "18%" },
                { display: '单位组织机构代码(社会信用代码)', name: 'BuildCorpCode', align: 'center', type: "text", width: "15%" },

                  { display: '项目分类', name: 'PrjType', align: 'center', type: "text", width: "15%" },

                  { display: '选择', align: 'center', type: "text", width: "7%",
                      render: function(item) {
                      if (item.PKID != null && item.PKID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_select('" + item.PKID+"','"+item.PrjNum+"','" +item.PrjName+ "')\" >选择</a>";
                          }
                      }
                  }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=lxxmList',
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

        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>
