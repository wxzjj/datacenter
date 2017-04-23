<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToExcel.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.ToExcel" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../SparkClient/jquery-1.3.2.min.js" type="text/javascript"></script>

    <link href="../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {

            var input_controls = $(":text,:password,textarea,select,:file").filter(":visible");
            var enabled_input_controls = input_controls.filter(":enabled[readonly!='readonly'][readonly!=true]");
            enabled_input_controls.filter("[mytype='date']").datepicker({
                closeText: '关闭',
                prevText: '上月',
                nextText: '下月',
                currentText: '今天',
                monthNamesShort: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
                dateFormat: 'yy-mm-dd',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                yearSuffix: '',
                changeYear: true,
                changeMonth: true,
                showButtonPanel: true
            });
        })
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        日期
        <Bigdesk8:DBDatePicker ID="startdate" runat="server" Width="200px">
        </Bigdesk8:DBDatePicker>
        至
        <Bigdesk8:DBDatePicker ID="enddate" runat="server" Width="200px">
        </Bigdesk8:DBDatePicker>
        <br />
        <asp:Button ID="Button1" runat="server" Text="导出Excel" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
