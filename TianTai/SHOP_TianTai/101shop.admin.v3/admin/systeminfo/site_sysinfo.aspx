<%@ Page Language="C#" MasterPageFile="../admin_page.master" AutoEventWireup="true"
    CodeBehind="site_sysinfo.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.site_sysinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .stateSpan {
            float: left;
            padding: 0 5px;
            height: 17px;
            line-height: 17px;
            display: block;
        }

        .tagSpan ul {
            padding: 0;
            margin: 0 6px;
            height: 60px;
            line-height: 17px;
            display: block;
            overflow: auto;
            overflow-x: hidden;
        }

        .tagSpan li {
            cursor: default;
            list-style-image: none;
            list-style-position: inside;
            list-style-type: disc;
            marker-offset: auto;
        }

            .tagSpan li.level1 {
            }

            .tagSpan li.level2 {
                padding-left: 14px;
            }

            .tagSpan li.level3 {
                padding-left: 28px;
            }

            .tagSpan li.level4 {
                padding-left: 42px;
            }

        .tj {
            color: #666666;
        }

        .tj1, .tj3, .tj5 {
            color: #6699FF;
        }

        .tj2, .tj4 {
            color: #FF9966;
        }
    </style>
    <script type="text/javascript">        jQuery(function () { jQuery('.tagSpan li').each(function (i) { var o = jQuery(this); if (o.attr('website') == '2') o.hide(); o.html(o.html() + '(<font title="商品数目" size="2" color="Green">' + o.attr('sumproduct') + '</font>)'); }); });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    系统信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    系统的基本配置信息
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <table border="0" width="100%" cellpadding="0" cellspacing="0">
     <%--   <tr id="order1" runat="server">
            <td colspan="2" width="50%">
                <table class="datatable" cellspacing="0" cellpadding="0" width="100%" style="">
                    <caption>
                        系统通知您</caption>
                    <tr>
                        <td class="form_ctrl_row"></td>
                    </tr>                  
                </table>
            </td>
        </tr>--%>
        <tr id="order2" runat="server">
            <td valign="top" width="50%">
                <table class="datatable" cellspacing="0" cellpadding="0" width="100%" style="">
                    <caption>
                        监督提醒</caption>
                    <tr>
                        <td class="form_text_row">订单提醒：
                        </td>
                        <td class="form_ctrl_row">
                            <img src="../images/dot.gif" alt="" />
                            <a href="../order/order_list.aspx?OrderStatus=1">未审核订单：
                            <asp:Literal ID="ltlNotconfirmtheorder" runat="server"></asp:Literal></a>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table class="datatable" cellspacing="0" cellpadding="0" width="100%" style="">                   
                </table>
            </td>
        </tr>
    </table>
    <%if (adminInfo != null && adminInfo.AdminPowerType == "all")
      { %><script type="text/javascript">              jQuery(function () { jQuery('.debug').css('cursor', 'pointer').click(function (e) { window.location = '/debug/default.aspx?version=' + encodeURIComponent(jQuery('#ctl00_workspace_lbVesion').text()) }) })</script><%} %>
    <!--StatisticalTime(ms[Init,Load,Render])=[<%=StatisticalTime[0]%>,<%=StatisticalTime[1]%>,<%=StatisticalTime[2]%>]-->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".ordercancel").click(function () {
                $.post("site_sysinfo.aspx", "method=ordercancel", function (data) {
                    top.window.document.getElementById('mainFrame').contentWindow.document.getElementById('man_nav_3').click();
                    location = "/admin/order/customer_order_list.aspx?pstate=6";
                });
                return false;
            });
            $(".orderchange").click(function () {
                top.window.document.getElementById('mainFrame').contentWindow.document.getElementById('man_nav_3').click();
                $.post("site_sysinfo.aspx", "method=orderchange", function (data) {
                    location = "/admin/order/customer_order_list.aspx?pstate=7";
                });
                return false;
            });
            $(".SupplierChange").click(function () {
                $.post("site_sysinfo.aspx", "method=SupplierChange", function (data) {
                    location = "/admin/product/product_list.aspx?SupplierChange=1";
                });
                return false;
            });
        });
    </script>
</asp:Content>
