<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GcjdGantT.aspx.cs" Inherits="BuildingProject.Web.BuildingProjectPage.GantT.GcjdGantT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目进度横道图</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1">
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/prettify.css" />

    <script src="js/jquery-1.7.2.min.js"></script>

    <script src="js/jquery.fn.gantt.js" charset="gbk"></script>

    <script src="js/bootstrap-tooltip.js"></script>

    <script src="js/bootstrap-popover.js"></script>

    <script src="js/prettify.js"></script>

    <script src="js/data.js"></script>

    <script>

        $(function() {
            $(".gantt").gantt({
            source: datatest,
 //           source: 'List.ashx?fromwhere=gcjd&num=' + Math.random(),
                navigate: "scroll",
                scale: "weeks",
                maxScale: "months",
                minScale: "days",
                itemsPerPage: 14
            });
        });

    </script>

</head>
<body style=" margin:0; padding:0;">
    <form id="form1" runat="server">
    <div class="gantt" >
    </div>
    </form>
</body>
</html>
