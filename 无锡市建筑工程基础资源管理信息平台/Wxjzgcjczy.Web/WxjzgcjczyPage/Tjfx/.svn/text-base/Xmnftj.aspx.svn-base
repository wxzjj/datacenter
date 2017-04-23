<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xmnftj.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx.Xmnftj" %>

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
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-layout.css" rel="stylesheet"
        type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>

    <%--  <script type="text/javascript">
        $(function() {

            $.ajax({
                type: 'POST',
                url: '../Handler/Data.ashx?type=getTxlCombox',
                async: false,
                data: null,
                success: function(res) {
                    //                res = res.replace(/\"text\"/g, "text").replace(/\"id\"/g, "id");
                    var data = jQuery.parseJSON(res);
                    var data1 = [];
                    if (data != "") {

                        $.each(data, function(i, item) {
                            var t = { text: item.text, id: item.id };
                            data1.push(t);
                        });
                    }
                    $("#test1").ligerComboBox({ isShowCheckBox: true, isMultiSelect: true, width: 200,
                        data: data1, valueFieldID: 'test3'
                    });

                }
            });

        });
        function sendMsg() {
            if ($("#test3").val() == "") {
                alert("请选择发送短信的人！");
                return;
            }
            $("#txt_ry").val($("#test3").val());
            $.ligerDialog.waitting("短信发送中,请稍后...");
            $("#btn_send").click();
            
        }

        function showMessage(mes) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(mes);
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0" height="100%" cellspacing="5" cellpadding="0">
        <%--   <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    BackColor="#99CCFF" BorderColor="#6699FF" BorderStyle="Ridge" BorderWidth="1px"
                    AllowPaging="true" PageSize="10" CellSpacing="1">
                    <HeaderStyle BackColor="#FF8040" Font-Size="12pt" BorderWidth="2px" HorizontalAlign="Center"
                        Height="25pt"></HeaderStyle>
                    <Columns>
                        <asp:BoundField DataField="nd" HeaderText="年度">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Height="25pt"
                                Width="12%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="csdl" HeaderText="城市道路(万元)">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Height="25pt"
                                Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="csqh" HeaderText="城市桥涵(万元)">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Width="12%">
                            </ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="gggc" HeaderText="公共广场(万元)">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Width="12%">
                            </ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="qtgc" HeaderText="其它工程(万元)">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Width="12%">
                            </ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cou" HeaderText="合计(万元)">
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" ForeColor="White">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="10pt" Width="12%">
                            </ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BorderStyle="None" BorderWidth="3px" />
                </asp:GridView>
            </td>
        </tr>--%>
        <tr>
            <%-- <td width="40%" valign="middle" >
            <div style="text-align:center"><asp:Label ID="dt_title2" runat="server" Font-Bold="true"  Font="Trebuchet MS, 14.25pt, style=Bold" 
                            Text="短信简报" ForeColor="26, 59, 105">
                        </asp:Label></div>
             
                </br></br>
                <asp:Label ID="dt_title1" runat="server" Font-Size="10.25pt" Font-Bold="true" ForeColor="26, 59, 105"></asp:Label>
            </td>--%>
            <td width="100%" >
                <asp:Chart ID="Chart1" runat="server" Width="650px" Height="300px" BorderDashStyle="Solid"
                    Palette="BrightPastel" BackGradientStyle="TopBottom" BorderWidth="2" BackColor="WhiteSmoke"
                    BorderColor="26, 59, 105">
                    <Titles>
                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                            Text="" ForeColor="26, 59, 105">
                        </asp:Title>
                    </Titles>
                    <Legends>
                        <asp:Legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1"
                            Docking="Bottom" IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent"
                            Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center">
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
            </td>
        </tr>
        <%-- <tr>
            <td width="40%" valign="top">
                <input type="text" id="test1" />
                </br><input onclick="javascript:sendMsg();" type="button" value="发送" />
                </br></br></br>
            </td>
        </tr>
        <div style="display: none;">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
            <asp:Button CssClass="button button-s" ID="btn_send" Text="发送" runat="server" OnClick="btn_send_Click" />
             <Bigdesk8:DBTextBox ID="txt_ry" runat="server"></Bigdesk8:DBTextBox>
             </ContentTemplate>
                            </asp:UpdatePanel>
        </div>--%>
    </table>
    </form>
</body>
</html>
