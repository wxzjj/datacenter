<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajxx_Ryxx_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Ajxx_Ryxx_List" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目人员信息列表</title>
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
                所属单位名称
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="CorpName" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                 所属单位组织机构代码（社会信用代码）
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="CorpCode" style="width: 200px" />
            </td>
        </tr>
         <tr>
            <td width="15%" class="td-text">
                姓名
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="UserName" style="width: 200px" />
            </td>
            <td width="15%" class="td-text">
                人员证件号码
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="IDCard" style="width: 200px" />
            </td>
        </tr>
        
         <tr>
            <td width="15%" class="td-text">
                安全生产管理人员类型
            </td>
            <td width="35%" class="td-value">
              <%--  <input type="text" class="field s-text" name="UserName" style="width: 200px" />--%>
                <select  name="UserType"  class="field s-text">
                    <option value=""></option>
                    <option value="1">主要负责人</option>
                    <option value="2">项目负责人</option>
                    <option value="3">安全员</option>
                    
                </select>
                
            </td>
            <td width="15%" class="td-text">
                安全生产许可证编号
            </td>
            <td width="35%" class="td-value">
                <input type="text" class="field s-text" name="SafetyCerID" style="width: 200px" />
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
                case "新增人员信息":
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
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function f_reload() {
            manager && manager.loadData(true);
        }

        function f_add() {
          
            OpenWindow('../Xxcj/Ajxx_Ryxx_Edit.aspx?operate=add&aqjdbm=<%=aqjdbm %>', "新增人员信息", 940, 350, true);
        }
        function f_edit(pkId) {
            OpenWindow('../Xxcj/Ajxx_Ryxx_Edit.aspx?operate=edit&pkid=' + pkId, "编辑人员信息", 900, 350, true);
        }
        
        function f_delete(pkid) {
            var win = parent.parent.$.ligerDialog;
            win.confirm('确定要删除该人员信息吗?', function(confirm) {
                if (confirm) {
                    win.waitting("正在删除中...");
                    $.getJSON('Delete.ashx?operate=ajxx_ryxx&pkid=' + pkid, { Rnd: Math.random() }, function(data) {
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

            manager = $("#maingrid").ligerGrid({
                columns: [

               { display: '单位名称', name: 'CorpName', align: 'center', type: "text", width: "15%" },
                { display: '组织机构代码(社会信用代码)', name: 'CorpCode', align: 'center', type: "text", width: "8%" },
                 { display: '姓名', name: 'UserName', align: 'left', type: "text", width: "7%" },
//                { display: '证件类型', name: 'IDCardType', align: 'left', type: "text", width: "10%" },
                { display: '证件号码', name: 'IDCard', align: 'center', type: 'text', width: "13%" },
                { display: '电话', name: 'UserPhone', align: 'center', type: 'text', width: "13%" },
                 { display: '安全生产管理人员类型', name: 'UserType', align: 'center', type: 'text', width: "15%" },
                 { display: '安全生产许可证编号', name: 'SafetyCerID', align: 'center', type: 'text', width: "15%" },
                 { display: '安全生产考核合格证书编号', name: 'CertID', align: 'center', type: 'text', width: "15%" },
                  { display: '编辑', align: 'center', type: "text", width: "5%",
                      render: function(item) {
                      if (item.PKID != null && item.PKID != "") {
                          return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_edit('" + item.PKID + "')\" >编辑</a>";
                          }
                      }
                  },
                  
                   { display: '删除', align: 'center', type: "text", width: "5%",
                       render: function(item) {
                           if (item.PKID != null && item.PKID != "") {
                               return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_delete('" + item.PKID + "')\" >删除</a>";
                           }
                       }
                   }
                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: getGridIsScroll(),
                url: 'List.ashx?fromwhere=ajx_ryxxList&aqjdbm=<%=aqjdbm %>',
                dataAction: 'server', //服务器排序
                usePager: true,       //服务器分页
                pageSize: 15,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: getGridHeight(),
                toolbar: { items: [
                { text: '新增人员信息', click: f_btnClick, img: '../../LigerUI/ligerUI/skins/icons/add.gif' }
                ]
                }
            });

            //增加搜索按钮,并创建事件
            LG.appendSearchButtons1("#formsearch", "#btn_search", manager);

        });

    </script>

    <form id="form1" runat="server" style="display: none">
    </form>
</body>
</html>

