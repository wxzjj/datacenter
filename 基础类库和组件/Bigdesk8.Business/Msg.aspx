<%@ Page Title="信息提示" Language="C#" MasterPageFile="BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="Msg.aspx.cs" Inherits="Bigdesk8.Business.Msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Master_Content" runat="server">
    <table height="400" cellspacing="0" cellpadding="0" width="600" align="center" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="200">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <div align="center">
                                            <img height="128" src="Images/Help_Msg.jpg" width="128"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="1" cellpadding="10" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    您在访问本系统时遇到了以下的情况：
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p id="p_msg" runat="server" style="color: Red" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
