<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjxx_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Zjxx_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质监信息列表</title>
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
    <form id="formsearch" class="l-form" runat="server" style="padding: 0px 0px 0px 0px; margin: 1px;">
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                项目名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjNum" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                项目编号
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="PrjNum" style="width: 200px" />
            </td>
        </tr>
        <tr>
            <td width="15%" class="td-text">
                质量监督编码
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="zljdbm" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                报监工程名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="gcmc" style="width: 200px" />
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
        
        function f_btnClick(item) {
            switch (item.text) {
                case "新增质监信息":
                    f_add();
                    break;
            }
        }

       

        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
            { text: '保存', onclick: function(item, dialog) {
                dialog.frame.f_save(dialog, manager);
            }
            },
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

        function OpenWindowView(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: false,  isResize: false, timeParmName: 'a'
            };
            activeDialog = parent.parent.$.ligerDialog.open(dialogOptions);
        }
        

        function f_reload() {
            manager && manager.loadData(true);
        }

        function f_add() {
            OpenWindow('../Xxcj/Zjxx_Edit.aspx?operate=add', "新增质监信息", 1000, 550, true);
        }
        function f_edit(pkId) {
            OpenWindow('../Xxcj/Zjxx_Edit.aspx?operate=edit&pkid=' + pkId, "编辑质监信息", 1000, 500, true);
        }
        
        function f_editRyxx(zljdbm) {
            OpenWindowView('../Xxcj/Zjxx_Ryxx_List.aspx?zljdbm=' + zljdbm, "编辑项目人员信息", 1100, 600, true);
        }


        function f_view(pkId,zljdbm) {
            OpenWindowView('../Xxcj/Zjxx_View.aspx?pkid=' + pkId + '&zljdbm=' + zljdbm, "查看质监信息", 1100, 550, true);
        }


        
        
        function f_delete(pkid) {
            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除该质监信息吗?', function(confirm) {
                if (confirm) {
                    win.waitting("正在删除中...");
                    $.getJSON('Delete.ashx?operate=zjxx&pkid=' + pkid, { Rnd: Math.random() }, function(data) {
                        if (data.Type == "Success") {
                            win.closeWaitting();
                            win.success("删除成功!");
                            f_reload();
                        }
                        else if (data.Type == "Error") {
                            win.closeWaitting();
                            win.error("删除失败!<BR>" + data.Message);
                        }
                    });
                }
            });
        }
        

        $(function() {
        var hasCreateOrEdit = '<%=HasCreateOrEdit %>';

        if (parseInt(hasCreateOrEdit) > 0) {

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '项目编号', name: 'PrjNum', align: 'left', type: "text", width: "11%" },
               { display: '项目名称', align: 'left', type: "text", width: "19%",
                   render: function(item) {
                       if (item.LxPKID != null && item.LxPKID != "") {
                           return "<a  href='/IntegrativeShow2/SysFiles/PagesZHJG/Zhjg_Lxxmdj_View.aspx?LoginID=<%=this.WorkUser.UserID  %>&PKID=" + item.LxPKID + "'  style='color:#000066;text-decoration: none;' target='_blank' >" + item.PrjName + "</a>";
                       }
                   }
               },
                 { display: '质量监督编码', name: 'zljdbm', align: 'center', type: "text", width: "13%" },
                { display: '报监工程名称', name: 'gcmc', align: 'left', type: "text", width: "17%" },

                { display: '质量监督机构', name: 'zljdjgmc', align: 'left', type: 'text', width: "14%" },
                { display: '质量监督机构代码', name: 'zjzbm', align: 'center', type: 'text', width: "9%" },
                 { display: '工程造价(万元)', name: 'gczj', align: 'right', type: 'text', width: "7%" },
                 { display: '报监日期', name: 'sbrq', align: 'center', type: 'text', width: "7%" },
                 { display: '开工日期', name: 'kgrq', align: 'center', type: 'text', width: "7%" },
                   { display: '人员信息', align: 'center', type: "text", width: "5%",
                       render: function(item) {
                           if (item.PKID != null && item.PKID != "") {
                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_editRyxx('" + item.zljdbm + "')\" >编辑</a>";
                           }
                       }
                   },
                  { display: '--', align: 'center', type: "text", width: "4%",
                      render: function(item) {
                          if (item.PKID != null && item.PKID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_edit('" + item.PKID + "')\" >编辑</a>";
                          }
                      }
                  },

                   { display: '--', align: 'center', type: "text", width: "4%",
                       render: function(item) {
                           if (item.PKID != null && item.PKID != "") {
                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_delete('" + item.PKID + "')\" >删除</a>";
                           }
                       }
                   }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=zjxxList',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight(),
                toolbar: { items: [
                { text: '新增质监信息', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' }
                ]
                }
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);
            }
            else
            {
            
            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '项目编号', name: 'PrjNum', align: 'left', type: "text", width: "11%" },
               { display: '项目名称', align: 'left', type: "text", width: "19%",
                   render: function(item) {
                       if (item.LxPKID != null && item.LxPKID != "") {
                           return "<a  href='/IntegrativeShow2/SysFiles/PagesZHJG/Zhjg_Lxxmdj_View.aspx?LoginID=<%=this.WorkUser.UserID  %>&PKID=" + item.LxPKID + "'  style='color:#000066;text-decoration: none;' target='_blank' >" + item.PrjName + "</a>";
                       }
                   }
               },
                 { display: '质量监督编码', name: 'zljdbm', align: 'center', type: "text", width: "13%" },
                { display: '报监工程名称', name: 'gcmc', align: 'left', type: "text", width: "17%" },

                { display: '质量监督机构', name: 'zljdjgmc', align: 'left', type: 'text', width: "14%" },
                { display: '质量监督机构代码', name: 'zjzbm', align: 'center', type: 'text', width: "9%" },
                 { display: '工程造价(万元)', name: 'gczj', align: 'right', type: 'text', width: "7%" },
                 { display: '报监日期', name: 'sbrq', align: 'center', type: 'text', width: "7%" },
                 { display: '开工日期', name: 'kgrq', align: 'center', type: 'text', width: "7%" },
                 
//                   { display: '人员信息', align: 'center', type: "text", width: "5%",
//                       render: function(item) {
//                           if (item.PKID != null && item.PKID != "") {
//                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_viewRyxx('" + item.zljdbm + "')\" >查看</a>";
//                           }
//                       }
//                   },
                    { display: '--', align: 'center', type: "text", width: "5%",
                       render: function(item) {
                           if (item.PKID != null && item.PKID != "") {
                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_view('" + item.PKID + "','" + item.zljdbm + "')\" >查看</a>";
                           }
                       }
                   }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=zjxxList',
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
            }

        });

    </script>

   
</body>
</html>
