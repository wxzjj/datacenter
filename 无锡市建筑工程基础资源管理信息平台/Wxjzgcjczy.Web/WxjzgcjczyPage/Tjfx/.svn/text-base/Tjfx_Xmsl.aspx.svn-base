<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tjfx_Xmsl.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx.Tjfx_Xmsl" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

    <script src="../../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <link href="../../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/Calendar.js" type="text/javascript"></script>

    <%--    <script src="../../SparkClient/control.js" type="text/javascript"></script>--%>
    <%--    <script type="text/javascript">
        $(document).ready(function() {
        debugger
            alert(screen.width);
            if (screen.width < 1400) {
                $("#Chart1").css("Width", "1080px");
                $("#Chart1").css("Height", "330px");
            }
            else {
                $("#Chart1").css("Width", "1155px");
                $("#Chart1").css("Height", "450px");

            }
        });
     
    </script>--%>
</head>
<body style="background-color: #EEEEEE;">
    <form runat="server" id="formsearch" class="l-form" style="padding: 0px 0px 0px 0px;
    margin: 1px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td width="15%" class="td-text">
                项目分类
            </td>
            <td width="35%" class="td-value">
                <Bigdesk8:DBDropDownList ID="ddl_Xmfl" runat="server" ItemRelation="Equal" ToolTip="PrjType"
                    ItemName="PrjTypeNum">
                </Bigdesk8:DBDropDownList>
            </td>
            <td class="td-text">
                开工年度
            </td>
            <td class="td-value">
                <Bigdesk8:DBDropDownList ID="BdateStart" runat="server" Width="100px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>2010</asp:ListItem>
                    <asp:ListItem>2011</asp:ListItem>
                    <asp:ListItem>2012</asp:ListItem>
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                </Bigdesk8:DBDropDownList>
                至
                <Bigdesk8:DBDropDownList ID="BdateEnd" runat="server" Width="100px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>2010</asp:ListItem>
                    <asp:ListItem>2011</asp:ListItem>
                    <asp:ListItem>2012</asp:ListItem>
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                </Bigdesk8:DBDropDownList>
            </td>
        </tr>
        <tr>
            <td class="td-text">
                项目属地
            </td>
            <td class="td-value" colspan="3">
                <asp:CheckBoxList ID="cbl_ssdq" runat="server" RepeatColumns="11">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td style="background-color: White" colspan="4">
                <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 5%; text-align: right">
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 5%; text-align: right">
                        </td>
                        <td style="width: 35%">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="width: 6%; text-align: right; padding-right: 5px;">
                            <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="../Common/Images/Search_Button3.png"
                                OnClick="ImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="padding: 2px 0px 0px 1px; text-align:center; background-color:White;">
        <asp:Chart ID="Chart1" runat="server" BorderDashStyle="Solid" Palette="BrightPastel" Height="330px"
            Width="980px" BackGradientStyle="TopBottom" BorderWidth="2" BackColor="WhiteSmoke"
            BorderColor="26, 59, 105">
            <Titles>
                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                    Text="" ForeColor="26, 59, 105">
                </asp:Title>
            </Titles>
            <Legends>
                <asp:Legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1"
                    Alignment="Far" Docking="Top" IsDockedInsideChartArea="False" Name="Default"
                    BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="Emboss"></BorderSkin>
            <Series>
                <asp:Series Name="Default" BorderColor="180, 26, 59, 105">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White"
                    BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                    <AxisY2 IsLabelAutoFit="false" Interval="25">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                    </AxisY2>
                    <Area3DStyle Rotation="10" LightStyle="Realistic" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False" />
                    <AxisY LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
