<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logintest.aspx.cs" Inherits="Bigdesk8.Business.Logintest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录-测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        用户名：<asp:TextBox ID="TextBox1" runat="server" Width="200px" Text="admin"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="登 录" OnClick="Button1_Click"></asp:Button><asp:Label
            ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
