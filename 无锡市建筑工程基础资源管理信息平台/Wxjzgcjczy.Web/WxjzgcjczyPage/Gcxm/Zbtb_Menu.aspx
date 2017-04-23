<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zbtb_Menu.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Gcxm.Zbtb_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls"
    TagPrefix="Bigdesk8" %>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/<%= this.Theme %>/Stylesheet1.css" rel="Stylesheet" type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="padding-left: 5px;" class="Menu_BK">
                <asp:Menu ID="myMenu" runat="server" Orientation="Horizontal" OnMenuItemClick="myMenu_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="招标信息" Value="0" Selected="true"></asp:MenuItem>
                        <asp:MenuItem Text="中标信息" Value="1"></asp:MenuItem>
                        <asp:MenuItem Text="合同备案" Value="2"></asp:MenuItem>
                        <asp:MenuItem Text="造价备案" Value="3"></asp:MenuItem>
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
            <td id="td_item" valign="top" width="100%" height="100%" style=" padding:0;">
                <iframe id="iframe_item" name="iframe_item" framespacing="0" src="<%=iframe_url %>"
                    frameborder="0" width="100%" scrolling="no" height="100%" onload="td_item.style.height=iframe_item.document.body.scrollHeight+10">
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>