<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Lxxmdj_Menu.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Lxxmdj_Menu" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls"
    TagPrefix="Bigdesk8" %>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />

    <script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setHeight(height) {
         
            $("#iframe_item").height(height);
          
        }
    </script>
    
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="padding-left: 5px;" class="Menu_BK">
                <asp:Menu ID="myMenu" runat="server" Orientation="Horizontal" OnMenuItemClick="myMenu_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="项目信息" Value="0" Selected="true"></asp:MenuItem>
                        <asp:MenuItem Text="施工图审查" Value="6"></asp:MenuItem>
                        <asp:MenuItem Text="招投标信息" Value="1"></asp:MenuItem>
                        <asp:MenuItem Text="合同备案" Value="2"></asp:MenuItem>
                        <asp:MenuItem Text="安全报监" Value="3"></asp:MenuItem>
                        <asp:MenuItem Text="质量报监" Value="4"></asp:MenuItem>
                        <asp:MenuItem Text="施工许可" Value="5"></asp:MenuItem>
                        <asp:MenuItem Text="竣工备案" Value="7"></asp:MenuItem>
                    </Items>
                    <StaticMenuItemStyle CssClass="Menu_DefaultStyle_4" HorizontalPadding="0px" VerticalPadding="0px" />
                    <StaticSelectedStyle CssClass="Menu_SelectedStyle_4" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td class="menu_baseline">
            </td>
        </tr>
        <tr>
            <td id="td_item" valign="top" width="100%" height="100%">
                <iframe id="iframe_item" name="iframe_item" framespacing="0" src="<%=iframe_url %>"
                    frameborder="0" width="100%" scrolling="no" height="100%" onload="td_item.style.height=iframe_item.document.body.scrollHeight+10">
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>