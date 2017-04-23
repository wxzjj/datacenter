<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dxjb_Record_View.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Dxjb_Record_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/css/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../SparkClient/DateTime.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" width="100%">
        <tr>
            <td style="width: 100%;">
                <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="padding: 2px;">
                    <tr>
                        <td width="15%" class="td-text">
                            简报名称
                        </td>
                        <td width="85%" class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="db_Jbmc" runat="server" ItemName="Jbmc"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" class="td-text">
                            简报内容
                        </td>
                        <td width="85%" class="td-value" colspan="3" style="padding: 2px; vertical-align: text-top;">
                            <Bigdesk8:DBMemo ID="db_Jbnr" runat="server" ItemName="Jbnr"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" class="td-text">
                            发送类型
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBMemo ID="db_fslx" runat="server"></Bigdesk8:DBMemo>
                        </td>
                        <td width="15%" class="td-text">
                            发送时间
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBText ID="DBMemo1" runat="server" ItemName="SendTime" ItemType="DateTime"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" class="td-text">
                            收信人
                        </td>
                        <td width="85%" class="td-value" colspan="3" style="padding: 2px; vertical-align: top;">
                            <Bigdesk8:DBMemo ID="db_fsdx" runat="server" ItemName="Sxr"></Bigdesk8:DBMemo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
