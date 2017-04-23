<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Lxxmdj_Dxgc_View.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Lxxmdj_Dxgc_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls"
    TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 单项工程基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_gcjbxx.gif" height="25px" alt="" />
            </td>
        </tr>        
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            单项编码
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="fxbm" ItemName="fxbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            单项名称
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="xmmc" ItemName="xmmc" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            单项项目分类
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gclb" ItemName="gclb" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            单项投资（万元）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gczj" ItemName="gczj" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            单项建筑面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jzmj" ItemName="jzmj" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建设规模
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jsgm" ItemName="jsgm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            结构类型
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jglx" ItemName="jglx" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            工程用途
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jsyt" ItemName="jsyt runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            地上层数
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="dscs" ItemName="dscs" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            地下层数
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="dxcs" ItemName="dxcs" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="td_text" width="15%">
                            高度(米)
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gd" ItemName="gd" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            跨度(米)
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="kd" ItemName="kd" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            计划开工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jhkgrq" ItemName="jhkgrq" FieldType="Date"  runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            计划竣工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jhjgrq" ItemName="jhjgrq" FieldType="Date"  runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>


