<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JbZxjk_Detail.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.JbZxjk_Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1
        {
            height: 228px;
            width: 754px;
        }
        #TA_Detail
        {
            height: 130px;
            width: 751px;
        }
    </style>
</head>
<body>
    <BR>
    <form id="form1" runat="server">
    <div>
    
        <strong>记录信息</strong></div>
    <div>
    
        主要信息</div>
    <p>
        <asp:TextBox ID="TB_highlight" runat="server" Height="56px" ReadOnly="True" 
            style="margin-left: 5px; margin-top: 5px" TextMode="MultiLine" 
            Width="753px"></asp:TextBox>
    </p>
    <div>
    
        详细信息</div>
    <p>
        <asp:TextBox ID="TB_detail" runat="server" Height="241px" ReadOnly="True" 
            style="margin-left: 5px; margin-top: 5px" TextMode="MultiLine" Width="753px"></asp:TextBox>
    </p>
    </form>
</body>
</html>
