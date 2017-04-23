<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="BuildingProject.Web.BuildingProjectPage.GantT.Test" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jQuery.Gantt</title>
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
            $(".gantt1").gantt({
                //            source: datatest,
            source: 'List.ashx?fromwhere=test2&num=' + Math.random(),
                navigate: "scroll",
                scale: "weeks",
                maxScale: "months",
                minScale: "days",
                itemsPerPage: 10
            });
        });

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="gantt1" >
    </div>
    </form>
</body>
</html>
