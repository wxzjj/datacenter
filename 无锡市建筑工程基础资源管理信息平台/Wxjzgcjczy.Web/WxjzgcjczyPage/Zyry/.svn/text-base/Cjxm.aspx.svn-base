<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cjxm.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry.Cjxm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参建项目</title>
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

    <script type="text/javascript">
       
    </script>

</head>
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <div style="padding: 2px 0px 2px 0px;">
        <div id="divSsgc" style="background-color: White;">
        </div>
    </div>

    <script type="text/javascript">
        var manager;
        $(function() {

            manager = $("#divSsgc").ligerGrid({
                columns: [
               { display: '合同备案名称', align: 'left', type: "text", width: "25%",
                   render: function(item) {
                       if (item.PKID != null && item.PKID != "") {
                           return "<a target='_blank' href='/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Htba_View.aspx$LoginID=<%=this.WorkUser.UserID  %>%PKID=" + item.PKID + "&titleName=" + encodeURI("合同备案-" + item.RecordName) + "' style='color:#000066;text-decoration: none;' >" + item.RecordName + "</a>";
                       }
                   }
               },
            { display: '建设单位', align: 'left', type: "text", width: "20%",
                render: function(item) {
                    if (item.jsdwID != null && item.jsdwID != undefined && item.jsdwID != "") {
                        return "<a target='_blank' href='../Szqy/JsdwxxToolBar.aspx?jsdwid=" + item.jsdwID + "' style='color:#000066;text-decoration: none;' >" + item.PropietorCorpName + "</a>";
                    }
                    else {
                        return item.PropietorCorpName;
                    }
                }
            },
             { display: '承包类型', name: 'ContractType', align: 'center', type: 'text', width: "10%" },
      { display: '合同金额(万元)', name: 'ContractMoney', align: 'right', type: "text", width: "13%" },
      { display: '合同签订日期', name: 'ContractDate', align: 'center', type: 'text', width: "10%",
          render: function(item) {

              return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.ContractDate));
          }
      },
      { display: '项目负责人', name: 'PrjHead', align: 'center', type: "text", width: "13%" }

                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
      url: 'List.ashx?fromwhere=Cjxm&ryid=<%=ryID %>&rylx=<%=rylx %>&befrom=<%=befrom %>',
                dataAction: 'server', //服务器排序
                usePager: false,       //服务器分页
                pageSize: 10,
                rownumbers: false,
                alternatingRow: true,
                checkbox: false,
                height: 'auto'//getGridHeight()
            });

            //            manager = $("#divSsgc").ligerGrid({
            //                columns: [
            //                //              { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "5%" },
            //               {display: '项目名称', name: 'xmmc', align: 'left', type: "text", width: "20%",
            //               render: function(item) {
            //                   if (item.gcmc != null && item.gcmc != "") {
            //                       return "<a target='_blank' href='/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Aqbj_View.aspx$PKID=" + item.PKID + "%titleName=安全报监-" + item.gcmc + "' style='color:#000066;text-decoration: none;' >" + item.gcmc + "</a>";
            //                   }
            //               }
            //           },
            //                { display: '总包单位', align: 'left', type: "text", width: "19%",
            //                    render: function(item) {
            //                    if (item.qyID != null &&item.qyID != undefined && item.qyID != "") {
            //                            return "<a target='_blank' href='../Szqy/QyxxToolBar.aspx?qyid=" + item.qyID + "' style='color:#000066;text-decoration: none;' >" + item.zbdw_dwmc + "</a>";
            //                        }
            //                        else {
            //                            return item.zbdw_dwmc;
            //                        }
            //                    }
            //                },
            //                 { display: '注册建造师', name: 'zbdw_zcjzs', align: 'center', type: 'text', width: "10%",
            //                     render: function(item) {
            //                         if (item.ryID != null && item.ryID != "") {
            //                             return "<a target='_blank' href='RyxxToolBar.aspx?ryid=" + item.ryID + "' style='color:#000066;text-decoration: none;' >" + item.zbdw_zcjzs + "</a>";
            //                         }
            //                         else {
            //                             return item.zbdw_zcjzs;
            //                         }
            //                     }
            //                 },
            //                 { display: '监理单位', name: 'jldw_dwmc', align: 'left', type: "text", width: "19%"
            //                     //                     render: function(item) {
            //                     //                     if (item.jldw_dwmc != null && item.jldw_dwmc != "") {
            //                     //                             return "<a target='_blank' href='../Szqy/QyxxToolBar.aspx?qyid=" + item.qyid + "' style='color:#000066;text-decoration: none;' >" + item.sgdw + "</a>";
            //                     //                         }
            //                     //                     }
            //                 },
            //                  { display: '项目总监', name: 'jldw_xmzj', align: 'center', type: "text", width: "8%"
            //                      //                    , render: function(item) {
            //                      //                      if (item.jldw_xmzj != null && item.jldw_xmzj != "") {
            //                      //                              return "<a target='_blank' href='../Zyry/RyxxToolBar.aspx?ryid=" + item.ryid + "' style='color:#000066;text-decoration: none;' >" + item.xmjl + "</a>";
            //                      //                          }
            //                      //                      }
            //                  },

            //                { display: '开工日期', align: 'center', type: 'text', width: "8%"
            //                        , render: function(item) {
            //                            if (item.gcgk_kgrq != null && item.gcgk_kgrq != undefined && item.gcgk_kgrq != "") {
            //                                return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.gcgk_kgrq));
            //                            }

            //                        }
            //                },
            //                { display: '计划竣工日期', align: 'center', type: 'text', width: "9%"
            //                        , render: function(item) {
            //                            if (item.gcgk_jhjgrq != null && item.gcgk_jhjgrq != undefined && item.gcgk_jhjgrq != "") {
            //                                return DateUtil.dateToStr('yyyy-MM-dd', DateUtil.strToDate(item.gcgk_jhjgrq));
            //                            }
            //                        }
            //                }

            //                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
            //                url: 'List.ashx?fromwhere=Cjxm&ryid=<%=ryID %>&rylx=<%=rylx %>&befrom=<%=befrom %>',
            //                dataAction: 'server', //服务器排序
            //                usePager: false,       //服务器分页
            //                pageSize: 10,
            //                rownumbers: false,
            //                alternatingRow: true,
            //                checkbox: false,
            //                height: 'auto'//getGridHeight()
            //            });
        });


        //        $("a[rel=example_group]").fancybox({
        //            'transitionIn': 'none',
        //            'transitionOut': 'none',
        //            'titlePosition': 'over',
        //            'titleFormat': function(title, currentArray, currentIndex, currentOpts) {
        //                return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
        //            }
        //        });
    </script>

    </form>
</body>
</html>
