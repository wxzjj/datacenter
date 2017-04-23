<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_View.aspx.cs" Inherits="IntegrativeShow2.SysFiles.Pages.Index_View" %>

<html>
<head id="Head1" runat="server">
    <title>
        <%=titleName %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="stylesheet" type="text/css" />
<script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>
    
</head>


<body  style=" background-color:rgb(242,243,245)">
    <iframe src="FrameView.aspx?viewUrl=<%=viewUrl %>" frameborder="0" width="100%" height="100%" border="0" scrolling="no" name="mainFrame" id="mainFrame" onload="setHeight(mainFrame.document.body.scrollHeight)"></iframe>
    <script type="text/javascript" >
        function setHeight(height) {
            $("#mainFrame").height(height);


        }
       
    </script>
    
</body>
</html>
