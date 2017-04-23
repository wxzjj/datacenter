<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zzxx.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.Zzxx" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    资质信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
                    <!-- name="edit3"  要和下面的td中的一致-->
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="maingrid">
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="false">
                    <Columns>
                        <asp:BoundField HeaderText="主项/增项" DataField="zzbz">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质名称" DataField="zzlb">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质等级" DataField="zzdj">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="数据来源" DataField="tag">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="更新时间" DataField="xgrqsj" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
