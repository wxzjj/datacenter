<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Qyxx_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Qyxx_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看企业基本信息</title>
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
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                    <tr>
                        <%-- <td class="td-value" style="text-align: center" width="10%" rowspan="6">
                            基<br />
                            本<br />
                            情<br />
                            况
                        </td>--%>
                        <td class="td-text" width="15%">
                            企业名称
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBLabel1" runat="server" ItemName="QYMC">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            组织机构代码（社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="ZZJGDM">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            机构代码有效期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="jgdmyxq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            注册地区
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText9" runat="server" ItemName="Province">
                            </Bigdesk8:DBText>&nbsp;&nbsp;
                            <Bigdesk8:DBTextBox ID="DBTextCity" ItemName="City" runat="server" style="width: 70px"></Bigdesk8:DBTextBox>
                            &nbsp;&nbsp;
                            <Bigdesk8:DBTextBox ID="DBTextCounty" ItemName="County" runat="server" style="width: 70px"></Bigdesk8:DBTextBox>
                           &nbsp;&nbsp;
                           <% if ("wangyj" == this.WorkUser.LoginName.ToString() || "wangxp" == this.WorkUser.LoginName.ToString()) { %> 
                               <asp:Button ID="saveButton" runat="server" Text="保存"  OnClick="saveBtnClick" />
                            <% } else { %>  <% } %>
                        </td>
                        <td class="td-text">
                            企业注册地点
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="zcdd">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业市域类别
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="sylx">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            企业经济性质
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="jjxz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            营业执照注册号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="yyzzzch">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            注册资本(万元)
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="zczb">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            开户银行
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="khyh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            银行账号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="yhzh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            是否为央企
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText21" runat="server" ItemName="sfsyq">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            工程技术人员总数
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBLabel9" runat="server" ItemName="gcjsry_zs">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            工程技术人员中<br />
                            高级职称人数
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="gcjsry_gjzcrs">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            工程技术人员中<br />
                            中级职称人数
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText20" runat="server" ItemName="gcjsry_zjzcrs">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业联系人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="lxr">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            企业联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="fddbrlxdh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            法人代表
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="fddbr">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            法人代表联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="fddbrlxdh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业负责人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="qyfzr">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            企业负责人联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="qyfzrlxdh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            技术负责人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="jsfzr">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            技术负责人联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="jsfzrzc">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            邮政编码
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText8" runat="server" ItemName="yzbm">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            传真
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText25" runat="server" ItemName="cz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            电子邮箱
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText26" runat="server" ItemName="email">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            网址
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText27" runat="server" ItemName="webAddress">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            详细地点
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText279" runat="server" ItemName="xxdd">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            成立日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText30" runat="server" ItemName="clrq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业简介
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBText28" runat="server" ItemName="qyjj">
                            </Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业主营范围
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBText4" runat="server" ItemName="zyfw">
                            </Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业兼营范围
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBText24" runat="server" ItemName="jyfw">
                            </Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            数据来源
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText31" runat="server" ItemName="tag">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            更新时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText32" runat="server" ItemName="xgrqsj" ItemType="DateTime">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td class="td-value" style="text-align: center;">
                            资<br />
                            质<br />
                            证<br />
                            书<br />
                            情<br />
                            况
                        </td>
                        <td style="background-color: #333" colspan="4">
                            <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
                                <tr>
                                    <td class="td-text">
                                        证书编号
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="zzzsbh">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        发证机关
                                    </td>
                                    <td class="td-value" colspan="3">
                                        <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="zzzsfzdw">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text" width="11%">
                                        法定代表人
                                    </td>
                                    <td class="td-value" width="22%">
                                        <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="fddbr">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text" width="11%">
                                        职称
                                    </td>
                                    <td class="td-value" width="22%">
                                        <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="fddbrzc">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text" width="11%">
                                        联系电话
                                    </td>
                                    <td class="td-value" width="22%">
                                        <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="fddbrlxdh">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        企业负责人
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="qyfzr">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        职称
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="qyfzrzc">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        联系电话
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="qyfzrlxdh">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        技术负责人
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="jsfzr">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        职称
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="jsfzrzc">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        联系电话
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="jsfzrlxdh">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-text">
                                        资质有效期
                                    </td>
                                    <td class="td-value">
                                        <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="zzzsyxq" ItemType="Date">
                                        </Bigdesk8:DBText>
                                    </td>
                                    <td class="td-text">
                                        备注
                                    </td>
                                    <td class="td-value" colspan="3">
                                        <Bigdesk8:DBText ID="DBText20" runat="server" ItemName="zzzsbz">
                                        </Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-value" colspan="6" style="padding-top: 2px; padding-bottom: 2px;">
                                        <div id="maingrid" style="background-color: White;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-value" style="text-align: center" rowspan="4">
                            营<br />
                            业<br />
                            执<br />
                            照
                        </td>
                        <td class="td-text">
                            注册号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText21" runat="server" ItemName="yyzzzch">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            注册资本(万元)
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="zczb">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            公司类型
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="sfsyq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            成立日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText24" runat="server" ItemName="clrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            年检年度
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText25" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            营业限期
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText26" runat="server" ItemName="yyzzyxq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-value" style="text-align: center" rowspan="4">
                            安<br />
                            全<br />
                            生<br />
                            产<br />
                            许<br />
                            可<br />
                            证
                        </td>
                        <td class="td-text">
                            编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText27" runat="server" ItemName="aqscxkzbh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            主要负责人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText28" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            许可范围
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText29" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证机关
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText32" runat="server" ItemName="aqscxkzfzdw">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            发证时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText30" runat="server" ItemName="aqscxkzfzrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            有效期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText31" runat="server" ItemName="aqscxkzyxq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-value" style="text-align: center" rowspan="5">
                            信<br />
                            用<br />
                            手<br />
                            册
                        </td>
                        <td class="td-text">
                            手册编号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText33" runat="server" ItemName="scbh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            信用评分
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText34" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            有效期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText35" runat="server" ItemName="xyscyxq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            发证单位
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText39" runat="server" ItemName="xyscfzdw">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            详细地点
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText36" runat="server" ItemName="xxdd">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            企业地址
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText40" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            企业简介
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText37" runat="server" ItemName="qyjj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            当前状态
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText38" runat="server" ItemName="">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">

        //        $(function() {
        //            var manager;
        //            manager = $("#maingrid").ligerGrid({
        //                columns: [
        //                { display: '序号', name: 'ROWNUM', align: 'center', type: "text", width: "10%" },
        //                { display: '主项/增项', name: 'ZZBZ', align: 'center', type: "text", width: "30%" },
        //                { display: '资质名称', name: 'ZZLB', align: 'center', type: 'text', width: "30%" },
        //                { display: '资质等级', name: 'ZZDJ', align: 'center', type: 'text', width: "30%" }
        //                ], width: '99.8%', pageSizeOptions: [5, 10, 15, 20], isScroll: true,
        //                url: 'List.ashx?fromwhere=QyxxView&qyid=<%=qyID %>',
        //                dataAction: 'server', //服务器排序
        //                usePager: false,       //服务器分页
        //                pageSize: 10,
        //                rownumbers: false,
        //                alternatingRow: true,
        //                checkbox: false,
        //                height: 'auto'//getGridHeight()
        //            });
        //        })
        function openLdpsWindow() {
            var url = "../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add";
            var arguments = window;
            var features = "dialogWidth=950px;dialogHeight=530px;center=yes;status=yes";
            var argReturn = window.showModalDialog(url, arguments, features);
        }
        function OpenWin(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '发送', onclick: function(item, dialog) {
                    dialog.frame.f_send(dialog, null);
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }
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
