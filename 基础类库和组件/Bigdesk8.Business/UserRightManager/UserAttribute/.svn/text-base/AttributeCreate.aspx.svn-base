<%@ Page Title="新增特性" Language="C#" MasterPageFile="../../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="AttributeCreate.aspx.cs" Inherits="Bigdesk8.Business.UserRightManager.UserAttribute.AttributeCreate" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Master_Content" runat="server">
    <table width="100%" border="0" cellspacing="5" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            您的位置：<span style="color: Blue">用户管理 - 新增特性</span>
                        </td>
                        <td align="right">
                            <a href="AttributeList.aspx" title="返回">
                                <img src="../../Styles/images/back.png" style="width: 24px; height: 24px" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="1" bgcolor="#999999">
            </td>
        </tr>
        <tr>
            <td height="5">
            </td>
        </tr>
        <tr>
            <td style="background: #666">
                <table width="100%" border="0" cellspacing="1" cellpadding="5">
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center; color: Red">
                            特性名称
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox17" runat="server" ItemName="AttributeName" ItemIsRequired="True"
                                ItemNameCN="特性名称"></Bigdesk8:DBTextBox>
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            特性描述
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" runat="server" ItemName="AttributeDesc" ItemNameCN="特性描述"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center; color: Red">
                            排序
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox18" runat="server" ItemName="Sort" ItemIsRequired="true"
                                ItemNameCN="排序" ItemType="Int32" MinData="1"></Bigdesk8:DBTextBox>
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 30%">
                        </td>
                        <td align="center" style="width: 40%">
                            <Bigdesk8:SubmitButton ID="btn_Save" runat="server" Text="保存" OnClick="btn_Save_Click" />
                        </td>
                        <td align="right" style="width: 30%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
