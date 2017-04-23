<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XmLocation_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt.XmLocation_List" %>

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
               项目编码
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjNum" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                项目名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjName" style="width: 200px" />
            </td>
        </tr>
           <tr>
            <td width="15%" class="td-text">
                组织机构代码<br />（社会信用代码）
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="BuildCorpCode" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                建设单位名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="BuildCorpName" style="width: 200px" />
            </td>
        </tr>
           <tr>
            <td width="15%" class="td-text">
                标注状态
            </td>
            <td width="35%" class="td-value">
                 <select class="field s-text" name="IsSgbz" op="equal" style="width: 200px;">
                    <option value="">请选择</option>
                    <option value="-1">未设置经纬度</option>
                    <option value="0">未标注</option>
                    <option value="1">已标注</option>
                </select>
            </td>
            <td width="15%" class="td-text">
                
            </td>
            <td width="35%" class="td-value">
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
        var activeDialog;

        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
//            { text: '保存', onclick: function(item, dialog) {
//                dialog.frame.f_save(dialog, manager);
//            }
//            },
            { text: '关闭', onclick: function(item, dialog) {
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

        function f_tdtbz(pkid) {
            OpenWindow('../Tdt/LocationBz.aspx?PKID=' + pkid, "地图标注", 1000, 530, true);
        }
        
        $(function() {

        manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '项目名称',  align: 'left', type: "text", width: "30%",
                   render: function(item) {
                   if (item.PKID != null && item.PKID != "") {
                       return "<a  href='/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID=<%=this.WorkUser.UserID  %>%PKID="+item.PKID+"%PrjNum="+item.PrjNum+"&titleName="+item.PrjName+"' style='color:#000066;text-decoration: none;' >" + item.PrjName + "</a>";
                       }
                   }
               },
                 { display: '项目编码', name: 'PrjNum', align: 'left', type: "text", width: "11%" },
                { display: '建设单位', name: 'BuildCorpName', align: 'left', type: "text", width: "18%" },
                 { display: '组织机构代码(社会信用代码)', name: 'BuildCorpCode', align: 'center', type: "text", width: "13%" },
                  { display: '经度', name: 'jd', align: 'right', type: "text", width: "7%" },
                  { display: '纬度', name: 'wd', align: 'right', type: "text", width: "6%" },
                   { display: '标注状态', name: 'isSgbzState', align: 'center', type: "text", width: "8%" },

                  { display: '标注', align: 'center', type: "text", width: "5%",
                      render: function(item) {
                      if (item.PKID != null && item.PKID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_tdtbz('" + item.PKID + "')\" >标注</a>";
                          }
                      }
                  }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=xmLocation',
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

