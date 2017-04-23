<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkArea.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceGlyh.WorkArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../Common/jquery-easyui-1.3.3/jquery-1.8.0.min.js" type="text/javascript"></script>

    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/workarea.css" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        a:hover
        {
            color: red;
        }
    </style>
</head>
<body style="padding: 20px; overflow-x: hidden;">
    <div class="wnavbar">
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/logperson.png" /></div>
        <div class="wnavbar-inner">
            <b><span id="Span1" style="color: #46a3ff;">当前登录人：<asp:Label ID="label1" ForeColor="Red"
                runat="server" /></span><span>，</span>欢迎登录使用《无锡市住房和城乡建设局公共信息服务平台》！ </b>
        </div>
    </div>
    <br />
    <div class="wnavline">
    </div>
    <br />
    <div class="wnavbar">
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/bestseller.png" /></div>
        <div class="wnavbar-inner">
            <b>办事动态</b>
        </div>
    </div>
    <div class="wcontent">
        <table style="border-width: 0px; width: 100%;" cellpadding="2px" cellspacing="0px">
            <tr>
                <td style="width: 50%">
                    <table style="border: 1px solid #e0e0e0; width: 100%; min-height: 90px;" cellpadding="2px"
                        cellspacing="0px">
                        <tr>
                            <td style="width: 26px; border-right: 1px solid #e0e0e0; background-color: #ecf5ff;
                                text-align: center; color: #005757;">
                            </td>
                            <td style="vertical-align: top;">
                                <%--  <div style="cursor:pointer;position:relative;width:105px; height:45px; background-color:#00baae; left:10px; top:10px; float:left;margin-bottom:10px;margin-right:10px;">
                                    <div style=" position:absolute; top:10px; left:8px; float:left;"><img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" /></div>
                                    <div style=" position:absolute; top:5px; left:35px; float:left;">
                                        <a href="../Module_Lypj/Lypjxxsb_List.aspx" id='dsb' style="text-decoration:none" ><span style="font-size:14px; color:White;">待申报</span></a>
                                        <span style="font-size:14px; color:White;"><asp:Label runat="server" ID="lblDsb"></asp:Label></span>
                                      </div>
                                </div>
                                <div style="cursor:pointer;position:relative;width:105px; height:45px; background-color:#ff7777; left:10px; top:10px; float:left;margin-bottom:10px;margin-right:10px;">
                                    <div style=" position:absolute; top:10px; left:8px; float:left;"><img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" /></div>
                                    <div style=" position:absolute; top:5px; left:35px; float:left;">
                                        <a href="../Module_Lypj/Lypjxxsb_List.aspx" id='A1' style="text-decoration:none" ><span style="font-size:14px; color:White;">审核中</span></a>
                                        <span style="font-size:14px; color:White;"><asp:Label runat="server" ID="LblShz"></asp:Label></span>
                                      </div>
                                </div>
                                <div style="cursor:pointer;position:relative;width:105px; height:45px; background-color:#ffa032; left:10px; top:10px; float:left;margin-bottom:10px;margin-right:10px;">
                                    <div style=" position:absolute; top:10px; left:8px; float:left;"><img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" /></div>
                                    <div style=" position:absolute; top:5px; left:35px; float:left;">
                                        <a href="../Module_Lypj/Lypjxxsb_List.aspx" id='A2' style="text-decoration:none"><span style="font-size:14px; color:White;">通过</span></a>
                                        <span style="font-size:14px; color:White;"><asp:Label runat="server" ID="Lblshtg"></asp:Label></span>
                                      </div>
                                </div>
                                <div style="cursor:pointer;position:relative;width:105px; height:45px; background-color:#3aba7d; left:10px; float:left; top:10px; margin-bottom:10px;margin-right:10px;">
                                    <div style=" position:absolute; top:10px; left:8px; float:left;"><img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" /></div>
                                    <div style=" position:absolute; top:5px; left:35px; float:left;">
                                        <a href="../Module_Lypj/Lypjxxsb_List.aspx" id='A3' style="text-decoration:none"><span style="font-size:14px; color:White;">不通过</span></a>
                                        <span style="font-size:14px; color:White;"><asp:Label runat="server" ID="lblshb"></asp:Label></span>
                                      </div>
                                </div>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px;">
                    &nbsp;
                </td>
                <td style="width: 50%">
                    <table style="border: 1px solid #e0e0e0; width: 100%; min-height: 90px;" cellpadding="2px"
                        cellspacing="0px">
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="wnavbar">
        <div class="wnavbar-l">
        </div>
        <div class="wnavbar-r">
        </div>
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/comment2.png" /></div>
        <div class="wnavbar-inner">
            <b>系统操作指南</b>
        </div>
    </div>
    <br />
    <div class="wlinks">
       <%-- <div class="wlink" url="../Public/FlowChart_View.aspx?chartnum=1">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/a.png" /><a
                href="javascript:void(0)" style="color: #333333;">履约评价申报流程</a>
        </div>
        <div class="wlink">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" /><a
                href="javascript:void(0)" style="color: #333333;">预选名录申请说明</a>
        </div>
        <div class="wlink">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/c.png" /><a
                href="javascript:void(0)" style="color: #333333;">业务办理流程</a>
        </div>
        <div class="wlink">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/d.png" /><a
                href="javascript:void(0)" style="color: #333333;">配套文件查询</a>
        </div>
        <div class="wlink" style="display: none">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/book.png" /><a href="javascript:void(0)"
                style="color: #333333;">操作说明下载</a>
        </div>--%>
    </div>
    <div class="wl-clear">
    </div>
    <br />
    <br />
    <div class="wnavline">
    </div>
    <div class="wwithicon">
        <div class="icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/alert.png" align="absmiddle" /></div>
        <span style="padding-left: 33px;">业务咨询电话：<span style="color: Red;">0512-65109046</span>
    </div>

    <%--<script type="text/javascript">
        $("div.wlink").live("mouseover", function() {
            $(this).addClass("wlinkover");

        }).live("mouseout", function() {
            $(this).removeClass("wlinkover");


        }).live('click', function(e) {
            var url = $(this).attr("url");
            window.open(url, "_blank");
        });
     
    </script>--%>

</body>
</html>
