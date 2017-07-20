<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrameView.aspx.cs" Inherits="IntegrativeShow2.SysFiles.Pages.FrameView" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

<script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>
<title>无标题文档</title>
<style type="text/css">
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	overflow:hidden;
	table-layout:fixed;
}
</style>

</head>

<body>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr id="tr1" style=" height:600px;" >
   <td width="8" style=" background:url(../Images/FrameCenter/Center_01.jpg) repeat-y"  >　</td>
    <td align="center" valign="top" style="height:auto;">
	 <iframe name="I1" id="I1" src="<%=viewUrl %>" height="100%" width="100%" scrolling="auto" border="0" frameborder="0" onload="setHeight(I1.document.body.scrollHeight)">
	浏览器不支持嵌入式框架，或被配置为不显示嵌入式框架。</iframe>
	<script language="javascript" type="text/javascript">
	    function setHeight(height) {

	        var maxHeight = Math.max(height, window.document.body.offsetHeight);
	      
	      $("#T1").height(maxHeight);
	      $("#tr1").height(maxHeight);
          //修复在chrome/ff等浏览器中，td中的iframe显示高度太小的问题
	      $("#tr1").children("td").height(maxHeight);
	    }
	</script>
	</td>
	
   <td width="7" style="background:url(../Images/FrameCenter/Center_02.jpg) repeat-y">　</td>
  </tr>
</table>
</body>
</html>

