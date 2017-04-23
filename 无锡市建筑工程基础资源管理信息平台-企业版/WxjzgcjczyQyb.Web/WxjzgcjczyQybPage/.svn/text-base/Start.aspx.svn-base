<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Start" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>无锡市住房和城乡建设局公共信息服务平台</title>
    <meta name="keywords" content="abc" />
    <meta name="description" content="abc" />
    <link href="Common/css/Startcss_1.css" rel="stylesheet" type="text/css" />
    <link href="Common/css/Startcss_2.css" rel="stylesheet" type="text/css" />

    <script src="Common/scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <!--[if IE]>
  <script src="js/html5.js"></script>
<![endif]-->
</head>
<body class="body_bg">
    <form id="form1" runat="server">
    <p>
        <!-- Page_Top:begin -->
        <iframe width="100%" height="190px" frameborder="0" scrolling="no" src="Page_top.aspx"></iframe>
        <!-- Page_Top:end -->
    </p>
    <div class="w1002 clearfix">
        <div class="content">
            <div class="box">
                <div class="zhaobiao_left">
                    <a class="one" href="http://js.wuxi.gov.cn/" target="_blank">住建局网站</a> <a class="two"
                        href="http://218.90.162.110:8889/WxjzgcjczyPage/Login.htm" target="_blank">一体化平台</a>
                    <a class="three" href="Login.aspx" target="_blank">后台管理</a>
                </div>
                <div class="zhaobiao_right">
                    <h2 style="width: 810px">
                        <a class="current">项目信息</a><a>施工图审查</a><a>招标投标</a><a>合同备案</a><a>安全监督</a><a>质量报监</a><a>施工许可</a><a>竣工备案</a></h2>
                    <ul class="mt" style="width: 810px">
                        <!--项目信息-->
                        <Bigdesk8:PowerDataGrid ID="gridView_xmxx" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="开工日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[开工日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("BDate") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=01"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--施工图审查-->
                        <Bigdesk8:PowerDataGrid ID="gridView_sgtsc" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审查完成日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[审查完成日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("CensorEDate") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=02"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--招标投标-->
                        <Bigdesk8:PowerDataGrid ID="gridView_zbtb" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="中标日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[中标日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("TenderResultDate") %>' ID="lb_kgrq"
                                            ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=03"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--合同备案-->
                        <Bigdesk8:PowerDataGrid ID="gridView_htba" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合同签订日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[合同签订日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("ContractDate") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=04"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--安全监督-->
                        <Bigdesk8:PowerDataGrid ID="gridView_aqjd" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("xmbm") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("xmbm")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="报监日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[报监日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("bjrq") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=05"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--质量报监-->
                        <Bigdesk8:PowerDataGrid ID="gridView_zljd" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="报监日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[报监日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("sbrq") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=06"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--施工许可-->
                        <Bigdesk8:PowerDataGrid ID="gridView_sgxk" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发证日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[发证日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("IssueCertDate") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=07"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                    <ul class="mt hide" style="width: 810px">
                        <!--竣工备案-->
                        <Bigdesk8:PowerDataGrid ID="gridView_jgba" runat="server" AutoGenerateColumns="False"
                            CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" PageSize="6">
                            <Columns>
                                <asp:TemplateField HeaderText="项目编号">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[项目编号]" ID="lbl_xmbh" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%# Eval("PrjNum") %>' ID="lb_xmbh" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Gcxm/Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                            Target="_blank" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                    <ItemStyle CssClass="pdg-itemstyle-left" Width="50%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实际竣工日期">
                                    <ItemTemplate>
                                        <Bigdesk8:DBText runat="server" Text="[实际竣工日期]" ID="lblkgrq" ForeColor="Gray"></Bigdesk8:DBText>
                                        <Bigdesk8:DBText runat="server" Text='<%#Eval("EDate") %>' ID="lb_kgrq" ForeColor="Black"></Bigdesk8:DBText>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="pdg-headerstyle-center" />
                                    <ItemStyle CssClass="pdg-itemstyle-center" Width="25%" />
                                </asp:TemplateField>
                            </Columns>
                        </Bigdesk8:PowerDataGrid>
                        <li style="width: 780px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=01&itemno=08"
                            target="_blank">
                            <img src="Common/images/more.png" align="absmiddle" /></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="box friendlink">
            <h2>
                查询快捷链接</h2>
            <a href="Page_List.aspx?menuno=01" target="_top">
                <img src="Common/images/gcxm.jpg" width="200" height="80"></a> <a href="Page_List.aspx?menuno=02">
                    <img src="Common/images/qyxx.jpg" width="200" height="80"></a> <a href="Page_List.aspx?menuno=03"
                        target="_top">
                        <img src="Common/images/ryxx.jpg" width="200" height="80"></a> <a href="Page_List.aspx?menuno=04"
                            target="_top">
                            <img src="Common/images/xyxx.jpg" width="200" height="80"></a>
        </div>
        <div class="box">
            <div class="zhaobiao_right percent">
                <h2>
                    <a class="current">市场主体</a></h2>
            </div>
            <div class="sczt_right">
                <h2>
                    <a class="current">建设单位</a><a>勘察单位</a><a>设计单位</a><a>施工单位</a><a>中介机构</a></h2>
                <ul class="mt">
                    <!--建设单位-->
                    <Bigdesk8:PowerDataGrid ID="gridView_jsdw" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("jsdw") %>' NavigateUrl='<%#string.Format("Sczt/JsdwxxToolBar.aspx?jsdwid={0}",Eval("jsdwid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("dwdz") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="联系人">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[联系人]" ID="lbl_lxr" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("lxr") %>' ID="lblxr" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=01"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--勘察单位-->
                    <Bigdesk8:PowerDataGrid ID="gridView_kcdw" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("xxdd") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=02"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--设计单位-->
                    <Bigdesk8:PowerDataGrid ID="gridView_sjdw" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("xxdd") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=03"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--施工单位-->
                    <Bigdesk8:PowerDataGrid ID="gridView_sgdw" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zcdd") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=04"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--中介机构-->
                    <Bigdesk8:PowerDataGrid ID="gridView_zjjg" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("xxdd") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=05"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <%--    <ul class="mt hide">
                    <!--其他-->
                    <Bigdesk8:PowerDataGrid ID="gridView_qt" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位地址">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[单位地址]" ID="lbl_dwdz" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("xxdd") %>' ID="lbdwdz" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=02&itemno=06"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>--%>
            </div>
        </div>
        <div class="box">
            <div class="zhaobiao_right percent">
                <h2>
                    <a class="current">执业人员</a></h2>
            </div>
            <div class="zyry_right">
                <h2>
                    <a class="current">注册执业人员</a><a>安全生产管理人员</a><a>企业技经人员</a><a>专业岗位管理人员</a></h2>
                <ul class="mt">
                    <!--注册执业人员-->
                    <Bigdesk8:PowerDataGrid ID="gridView_zczyry" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false" OnRowDataBound="gridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="执业资格类型">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[执业资格类型]" ID="lbl_ryzyzglx" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("ryzyzglx") %>' ID="lbryzyzglx" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewxm" runat="server" Text='<%#Eval("xm") %>' NavigateUrl='<%#string.Format("Zyry/RyxxToolBar.aspx?ryid={0}&rylx=zczyry",Eval("ryid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="证书编号">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[证书编号]" ID="lbl_zsbh" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zsbh") %>' ID="lbzsbh" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewqymc" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%" />
                            </asp:TemplateField>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=03&itemno=01"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--安全生产管理人员-->
                    <Bigdesk8:PowerDataGrid ID="gridView_aqscglry" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false" OnRowDataBound="gridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="执业资格类型">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[执业资格类型]" ID="lbl_ryzyzglx" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("ryzyzglx") %>' ID="lbryzyzglx" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewxm" runat="server" Text='<%#Eval("xm") %>' NavigateUrl='<%#string.Format("Zyry/RyxxToolBar.aspx?ryid={0}&rylx=aqscglry",Eval("ryid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="证书编号">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[证书编号]" ID="lbl_zsbh" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zsbh") %>' ID="lbzsbh" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewqymc" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%" />
                            </asp:TemplateField>
                            <%--  <asp:BoundField HeaderText="是否安监实名认证" DataField="sfsmrz">
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:BoundField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=03&itemno=02"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--企业技经人员-->
                    <Bigdesk8:PowerDataGrid ID="gridView_qyjjry" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false" OnRowDataBound="gridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="执业资格类型">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[执业资格类型]" ID="lbl_ryzyzglx" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("ryzyzglx") %>' ID="lbryzyzglx" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewxm" runat="server" Text='<%#Eval("xm") %>' NavigateUrl='<%#string.Format("Zyry/RyxxToolBar.aspx?ryid={0}&rylx=qyjjry",Eval("ryid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="证书编号">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[证书编号]" ID="lbl_zsbh" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zsbh") %>' ID="lbzsbh" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="所属企业">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewqymc" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%" />
                            </asp:TemplateField>
                            <%--  <asp:BoundField HeaderText="是否安监实名认证" DataField="sfsmrz">
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:BoundField>--%>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=03&itemno=03"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
                <ul class="mt hide">
                    <!--专业岗位管理人员-->
                    <Bigdesk8:PowerDataGrid ID="gridView_zygwglry" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="798px" AllowPaging="false" ShowHeader="false" OnRowDataBound="gridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="执业资格类型">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[执业资格类型]" ID="lbl_ryzyzglx" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("ryzyzglx") %>' ID="lbryzyzglx" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewxm" runat="server" Text='<%#Eval("xm") %>' NavigateUrl='<%#string.Format("Zyry/RyxxToolBar.aspx?ryid={0}&rylx=zygwglry",Eval("ryid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="证书编号">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[证书编号]" ID="lbl_zsbh" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zsbh") %>' ID="lbzsbh" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_Viewqymc" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_county" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("county") %>' ID="lbcounty" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="2%" />
                            </asp:TemplateField>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=03&itemno=04"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
            </div>
        </div>
        <div class="box">
            <div class="zhaobiao_right percent">
                <h2>
                    <a class="current">信用公示</a></h2>
            </div>
            <div class="xygs_right">
                <h2>
                    <a class="current">施工企业</a></h2>
                <ul class="mt">
                    <Bigdesk8:PowerDataGrid ID="gridView_xytx" runat="server" AutoGenerateColumns="False"
                        CellPadding="1" Width="1000px" AllowPaging="false" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="企业组织机构代码">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[企业组织机构代码]" ID="lbl_zzjgdm" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzjgdm") %>' ID="lbzzjgdm" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="企业名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyID")) %>'
                                        Target="_blank" />
                                </ItemTemplate>
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="企业资质类型">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[资质类型]" ID="lbl_zzlb" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("zzlb") %>' ID="lbzzlb" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="考评年度">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[考评年度]" ID="lbl_kpnd" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("kpnd") %>' ID="lbkpnd" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="属地">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[属地]" ID="lbl_qysd" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("qysd") %>' ID="lbqysd" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="基本分" DataField="jbf">
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="综合大检查得分" DataField="zhdjcdf">
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="日常考核扣分" DataField="rckhkf">
                                <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="信用分">
                                <ItemTemplate>
                                    <Bigdesk8:DBText runat="server" Text="[信用分]" ID="lbl_xyf" ForeColor="Gray"></Bigdesk8:DBText>
                                    <Bigdesk8:DBText runat="server" Text='<%# Eval("xyf") %>' ID="lbxyf" ForeColor="Black"></Bigdesk8:DBText>
                                </ItemTemplate>
                                <HeaderStyle CssClass="pdg-headerstyle-center" />
                                <ItemStyle CssClass="pdg-itemstyle-left" Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                    </Bigdesk8:PowerDataGrid>
                    <li style="width: 970px; text-align: right; margin-top: 2px;"><a href="Page_List.aspx?menuno=04&itemno=01"
                        target="_blank">
                        <img src="Common/images/more.png" align="absmiddle" /></a></li>
                </ul>
            </div>
        </div>
    </div>
    </form>
    <p>
        <iframe width="100%" frameborder="0" height="96" scrolling="no" src="Page_End.aspx">
        </iframe>
    </p>

    <script type="text/javascript">
        $(document).ready(function() {
            $('.news a').click(function() {
                $(this).addClass("current").siblings().removeClass("current");
                $(".news ul").eq($(".news a").index(this)).show().siblings('ul').hide();
            });

            $('.zhaobiao_right a').click(function() {
                $(this).addClass("current").siblings().removeClass("current");
                $(".zhaobiao_right ul").eq($(".zhaobiao_right a").index(this)).show().siblings('ul').hide();
            });

            $('.sczt_right a').click(function() {
                $(this).addClass("current").siblings().removeClass("current");
                $(".sczt_right ul").eq($(".sczt_right a").index(this)).show().siblings('ul').hide();
            });

            $('.zyry_right a').click(function() {
                $(this).addClass("current").siblings().removeClass("current");
                $(".zyry_right ul").eq($(".zyry_right a").index(this)).show().siblings('ul').hide();
            });

        });
    </script>

</body>
</html>
