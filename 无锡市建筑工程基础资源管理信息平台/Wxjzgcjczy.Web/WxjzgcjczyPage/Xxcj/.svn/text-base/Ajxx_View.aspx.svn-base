<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajxx_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj.Ajxx_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看安监信息</title>
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

    <script src="../../MyDatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body style=" margin:0; padding:0;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td >
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <td class="td-text" style="width: 15%;">
                           立项项目
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_LxPrjName" runat="server" ItemName="PrjName" ></Bigdesk8:DBText>
                            
                        </td>
                        <td width="15%" class="td-text">
                         项目编号
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_PrjNum" runat="server" ItemName="PrjNum" ></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                          安全监督编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_aqjdbm" runat="server"  ItemName="aqjdbm"
                                ></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                           报监工程名称
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_gcmc" runat="server" ItemName="gcmc" ></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            施工招标编码
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_sgzbbm" runat="server" ItemName="sgzbbm"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                          
                        </td>
                        <td width="35%" class="td-value">
                           
                        </td>
                    </tr>
                    <tr>
                      <td width="15%" class="td-text">
                          安全监督机构名称
                        </td>
                        <td width="35%" class="td-value">
                         <Bigdesk8:DBText ID="db_aqjdjgmc" runat="server" ItemName="aqjdjgmc"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            安全监督机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_sdCode" runat="server" ItemName="sdCode"></Bigdesk8:DBText>
                        </td>
                      
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            工程造价（万元）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_gcgk_yszj" runat="server" ItemName="gcgk_yszj"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            工程面积（平方米）
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_gcgk_jzmj" runat="server" ItemName="gcgk_jzmj" ></Bigdesk8:DBText>
                        </td>
                    </tr>
                
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            结构类型
                        </td>
                        <td class="td-value" style="width: 35%;">
                   
                               <Bigdesk8:DBDropDownList ID="ddl_gcgk_jglx" runat="server"   ItemName="gcgk_jglx" Enabled="false"></Bigdesk8:DBDropDownList>
                        </td>
                        <td width="15%" class="td-text">
                            层次
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="DBTextBox2" runat="server" ItemName="gcgk_cc"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            经度
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_gis_jd" runat="server" ItemName="gis_jd" ItemIsRequired="true" ItemNameCN="经度"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                            纬度
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="db_gis_wd" runat="server" ItemName="gis_wd" ItemIsRequired="true" ItemNameCN="纬度"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            报监日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_bjrq" runat="server" ItemType="Date" ItemName="bjrq" ItemIsRequired="true" ItemNameCN="报监日期"></Bigdesk8:DBText>
                        </td>
                        <td width="15%" class="td-text">
                        </td>
                        <td width="35%" class="td-value">
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            开工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_gcgk_kgrq" runat="server" ItemType="Date" ItemName="gcgk_kgrq" ItemIsRequired="true" ItemNameCN="开工日期"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            计划竣工日期
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_gcgk_jhjgrq" runat="server" ItemType="Date" ItemName="gcgk_jhjgrq" ItemIsRequired="true" ItemNameCN="计划竣工日期"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            总包单位名称
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_dwmc" runat="server" ItemName="zbdw_dwmc" ItemIsRequired="true" ItemNameCN="总包单位名称"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            总包单位组织机构代码（社会信用代码）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_dwdm" runat="server" ItemName="zbdw_dwdm" ItemIsRequired="true" ItemNameCN="总包单位组织机构代码（社会信用代码）"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                    <td class="td-text" style="width: 15%;">
                        总包单位安全生产许可证
                    </td>
                    <td class="td-value" style="width: 35%;">
                        <Bigdesk8:DBText ID="db_zbdw_aqxkzh" runat="server" ItemName="zbdw_aqxkzh" ItemIsRequired="true"  ItemNameCN="总包单位安全生产许可证"></Bigdesk8:DBText>
                    </td>
                    <td class="td-text" style="width: 15%;">
                        总包单位注册建造师
                    </td>
                    <td class="td-value" style="width: 35%;">
                        <Bigdesk8:DBText ID="db_zbdw_zcjzs" runat="server" ItemName="zbdw_zcjzs" ItemIsRequired="true" ItemNameCN="总包单位注册建造师"></Bigdesk8:DBText>
                    </td>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            总包单位注册建造师身份证号
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_zcjzsdm" runat="server" ItemName="zbdw_zcjzsdm" ItemIsRequired="true" ItemNameCN="总包单位注册建造师身份证号"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            总包单位注册建造师电话
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_zcjzs_lxdh" runat="server" ItemName="zbdw_zcjzs_lxdh"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                           总包单位专职安全员
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_aqy" runat="server" ItemName="zbdw_aqy" ItemIsRequired="true" ItemNameCN="总包单位专职安全员"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            总包单位专职安全员证号
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_zbdw_aqyzh" runat="server" ItemName="zbdw_aqyzh" ItemIsRequired="true" ItemNameCN="总包单位专职安全员证号" ></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            监理单位名称
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_dwmc" runat="server" ItemName="jldw_dwmc"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            监理单位组织机构代码（社会信用代码）
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_dwdm" runat="server" ItemName="jldw_dwdm"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            总监姓名
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_xmzj" runat="server" ItemName="jldw_xmzj"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            总监身份证号
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_zjdm" runat="server" ItemName="jldw_zjdm"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            总监电话
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_xmzj_lxdh" runat="server" ItemName="jldw_xmzj_lxdh"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            监理员集合
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="db_jldw_jlgcs" runat="server" ItemName="jldw_jlgcs"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="db_bz" runat="server" ItemName="bz" ></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
 <div id="maingrid" style="background-color: White;">
                </div>
    <script type="text/javascript">
        var dia;
        var mana;
        var activeDialog;


        function showMsg(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(msg);
        }
        function showWarn(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.warn(msg);
        }
        function showError(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.error(msg);
        }


        function OpenWindow(url, title, width, height) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, isResize: false, timeParmName: 'a'};
            activeDialog = $.ligerDialog.open(dialogOptions);
        }

        function f_view(pkid,aqjdbm) {

            OpenWindow('../Xxcj/Ajxx_Ryxx_View.aspx?aqjdbm=' + aqjdbm + "&pkid=" + pkid, "查看安监人员信息", 800, 400, false);
        }

        $(function() {

        manager = $("#maingrid").ligerGrid({
            columns: [

               { display: '单位名称', name: 'CorpName', align: 'center', type: "text", width: "15%" },
                { display: '组织机构代码(社会信用代码)', name: 'CorpCode', align: 'center', type: "text", width: "8%" },
                 { display: '姓名', name: 'UserName', align: 'left', type: "text", width: "7%" },
            //                { display: '证件类型', name: 'IDCardType', align: 'left', type: "text", width: "10%" },
                {display: '证件号码', name: 'IDCard', align: 'center', type: 'text', width: "13%" },
                { display: '电话', name: 'UserPhone', align: 'center', type: 'text', width: "13%" },
                 { display: '安全生产管理人员类型', name: 'UserType', align: 'center', type: 'text', width: "15%" },
                 { display: '安全生产许可证编号', name: 'SafetyCerID', align: 'center', type: 'text', width: "15%" },
                 { display: '安全生产考核合格证书编号', name: 'CertID', align: 'center', type: 'text', width: "15%" },
                  { display: '--', align: 'center', type: "text", width: "5%",
                      render: function(item) {
                          if (item.PKID != null && item.PKID != "") {
                              return "<a  href='javascript:void(-1)' style='color:#000066;text-decoration: none;' onclick=\"f_view('" + item.PKID + "','" + item.aqjdbm + "')\" >查看</a>";
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
            height: 'auto'
        });


        });
       
        
    </script>

    </form>
</body>
</html>
