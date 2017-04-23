<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Qyxx_View.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.Qyxx_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    基本信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
                    <!-- name="edit3"  要和下面的td中的一致-->
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="editbox" name="edit3">
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
                            组织机构代码（统一社会信用代码）
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
                            <Bigdesk8:DBText ID="DBText29" runat="server" ItemName="City">
                            </Bigdesk8:DBText>&nbsp;&nbsp;
                            <Bigdesk8:DBText ID="DBText69" runat="server" ItemName="County">
                            </Bigdesk8:DBText>
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
                        <%--  <td class="td-text">
                            企业市域类别
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="sylx">
                            </Bigdesk8:DBText>
                        </td>--%>
                        <td class="td-text">
                            企业经济性质
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="jjxz">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            营业执照注册号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="yyzzzch">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            注册资本(万元)
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="zczb">
                            </Bigdesk8:DBText>
                        </td>
                        <%--<tr>
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
                    </tr>--%>
                        <%--  <tr>
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
                    </tr>--%>
                        <%-- <tr>
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
                    </tr>--%>
                        <td class="td-text">
                            企业联系人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="lxr">
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
                        <%--  <td class="td-text">
                            企业联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="fddbrlxdh">
                            </Bigdesk8:DBText>
                        </td>--%>
                        <%--  <tr>
                       
                       <td class="td-text">
                            法人代表联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="fddbrlxdh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                        <%--    <td class="td-text">
                            企业负责人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="qyfzr">
                            </Bigdesk8:DBText>
                        </td>--%>
                        <td class="td-text">
                            技术负责人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="jsfzr">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--   <tr>--%>
                    <%-- <td class="td-text">
                            企业负责人联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="qyfzrlxdh">
                            </Bigdesk8:DBText>
                        </td>--%>
                    <%--    </tr>--%>
                    <%--   <tr>
                      
                     <td class="td-text">
                            技术负责人联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="jsfzrzc">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                    <%-- <tr>
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
                    </tr>--%>
                    <%-- <tr>--%>
                    <%--   <td class="td-text">
                            电子邮箱
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText26" runat="server" ItemName="email">
                            </Bigdesk8:DBText>
                        </td>--%>
                    <%--   <td class="td-text">
                            网址
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText27" runat="server" ItemName="webAddress">
                            </Bigdesk8:DBText>
                        </td>--%>
                    <%--  </tr>--%>
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
                            <Bigdesk8:DBText ID="DBText30" runat="server" ItemName="clrq" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--  <tr>
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
                    </tr>--%>
                    <%-- <tr>
                        <td class="td-text">
                            企业兼营范围
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBMemo ID="DBText24" runat="server" ItemName="jyfw">
                            </Bigdesk8:DBMemo>
                        </td>
                    </tr>--%>
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
                            <Bigdesk8:DBText ID="DBText32" runat="server" ItemName="xgrqsj" ItemType="Date">
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
    </form>
</body>
</html>
