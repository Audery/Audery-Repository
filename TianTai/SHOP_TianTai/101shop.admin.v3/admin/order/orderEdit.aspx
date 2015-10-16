<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderEdit.aspx.cs" Inherits="_101shop.admin.v3.admin.order.orderEdit"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>系统后台管理页面 </title>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <script type="text/javascript" src="/admin/scripts/jquery.js"></script>
    <script type="text/javascript" src="/admin/scripts/ajax.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script src="../scripts/jquery-easyui-1.3.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../scripts/jquery-easyui-1.3.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/menu.css" rel="stylesheet"
        type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/linkbutton.css" rel="stylesheet"
        type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/menubutton.css" rel="stylesheet"
        type="text/css" />
    <link id="skin" rel="stylesheet" href="../scripts/jBox/Skins2/Green/jbox.css" />
    <script type="text/javascript" src="../scripts/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="../scripts/jBox/i18n/jquery.jBox-zh-CN.js"></script>
    <style type="text/css">
        .form_text_row
        {
            background-color: #F5F5F5;
            color: #555555;
            padding-left: 9px;
            text-align: right;
            width: 90px;
        }
        .datatable th
        {
            color: #000000;
            cursor: pointer;
            font-weight: bold;
            line-height: 150%;
            padding: 5px 4px 4px;
            text-align: left;
        }
        .order_print_no
        {
            color: Highlight;
            font-size: 15px;
            cursor: pointer;
            padding-left: 25px;
            background: url(../purchase/files/print.gif) no-repeat center left;
        }
    </style>
    <script type="text/javascript" src="../scripts/public.js"></script>
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <style type="text/css">
        #ActList li
        {
            margin-left: 5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".icon-help").attr("title", "选择调货周期！");
        });        
    </script>
</head>
<body>
    <form id="aspnetForm" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
            订单处理
            <img src="../images/back.gif" alt="" />&nbsp;<a id="ctl00_pagetitle_HyperLink1" href="order_list.aspx">返回订单列表</a>
        </div>
        <%--<div class="page_info">
            <div style="min-height: 15px; height: auto; width: 100%;">
                <ul id="ActList" style="list-style: none; min-height: 15px; height: auto; width: 100%;">
                    <li style="height: 20px; float: left;">
                        <img src="../images/direct_blue.gif">
                        <asp:LinkButton ID="LinkButton1" runat="server">确认分单</asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>--%>
        <div class="page_sarch">
        </div>
    </div>
    <div class="page_main">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0,7)">
                    订单信息
                </td>
                <%--<td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1,7)">
                    <span id="ctl00_workspace_Label1">付款信息 <font color="red">1</font> </span>
                </td>--%>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="9" style="border: 1px solid #bac9c6;">
                    <!--订单信息-->
                    <div id="tab0" class="tabs" style="display: block;">
                        <table width="100%" cellspacing="0" cellpadding="0" class="datatable" style="">
                            <tr>
                                <td class="form_text_row" width="100">
                                    订单编号：
                                </td>
                                <td class="form_ctrl_row" width="130">
                                    <span id="ctl00_workspace_lbOrderNo" style="color: highlight;">
                                        <%=model.OrderId %></span>
                                </td>
                                <td class="form_text_row" width="100">
                                    订单状态：
                                </td>
                                <td class="form_ctrl_row" width="180">
                                    <%=SOSOshop.Model.Order.Orders.GetOrderStatus(model.OrderStatus)%>
                                </td>
                                <td class="form_text_row" width="100">
                                    付款情况：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbPayment" style="color: highlight;">
                                        <%=model.PaymentType == 1 ? "在线支付：" : "银行转帐"%>
                                        <%if (model.PaymentType == 1)
                                          { 
                                        %>
                                        <%=model.PaymentStatus == 0 ? "未付款" : "已付款"%>
                                        <%if (model.PaymentStatus == 1)
                                          { %>
                                        <span style="display: block; color: #777;">
                                            <%=new SOSOshop.BLL.Db().ExecuteScalarForCache("SELECT TOP 1 PayBankName+''+TransOrderId FROM OrderTransOnline WHERE CHARINDEX(OrderId,'"+model.OrderId+"')=1 AND TransStatus='00' ORDER BY ID DESC")%></span>
                                        <%} %>
                                        <%
                                          } %>
                                    </span>
                                </td>
                                <td class="form_text_row" width="100">
                                    物流状态：
                                </td>
                                <td class="form_ctrl_row" width="130">
                                    <span id="ctl00_workspace_lbLogisticsStatus" style="color: highlight;">
                                        <%=SOSOshop.Model.Order.Orders.GetOgisticsStatus(model.OgisticsStatus) %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    客户名称：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbUserName">
                                        <%=model.UserName %></span> <span id="ctl00_workspace_lbTrust" style="color: Red;">
                                    </span>
                                </td>
                                <td class="form_text_row">
                                    客户单位：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbIncName">
                                        <%=model.parentCorpName %></span>
                                </td>
                                <td class="form_text_row">
                                    &nbsp; 开票单位：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbBillingCorp">
                                        <%=model.BillingCorpName%></span>
                                </td>
                                <td class="form_text_row">
                                    下单时间：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbOrderDateTime">
                                        <%=model.ShopDate %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    付款方式：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbPaymentMode" style="color: highlight;">
                                        <%=SOSOshop.Model.Order.Orders.GetPayment(model.Payment) %></span>
                                    <%--&nbsp;&nbsp;已付金额：<span id="ctl00_workspace_lbPaid" style="color: Red;">0.00</span>--%>
                                </td>
                                <td class="form_text_row">
                                    送货方式：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbDeliveryMode" style="color: highlight;">
                                        <%=SOSOshop.Model.Order.Orders.GetCarriage(model.Carriage) %></span>
                                </td>
                                <td class="form_text_row">
                                    确认供货时间：
                                </td>
                                <td class="form_ctrl_row">
                                    <%=model.BusinessCheckDate %>
                                </td>
                                <td class="form_text_row">
                                    确认支付时间：
                                </td>
                                <td class="form_ctrl_row">
                                    <%=model.FinancialCheckDate %>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellspacing="0" cellpadding="0" class="datatable" style="">
                            <tr>
                                <td class="form_text_row">
                                    收货人姓名：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeName">
                                        <%=model.ConsigneeRealName %></span>
                                </td>
                                <td class="form_text_row">
                                    收货地址：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeAddress">
                                        <%=model.ConsigneeProvince %>
                                        <%=model.ConsigneeCity %>
                                        <%=model.ConsigneeBorough %>
                                        <%=model.ConsigneeAddress %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    收货人手机：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeModile">
                                        <%=model.ConsigneePhone %></span>
                                </td>
                                <td class="form_text_row">
                                    下单IP：
                                </td>
                                <td class="form_ctrl_row">
                                    <%=model.ConsigneeConstructionSigns %>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    收货人电话：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeTel">
                                        <%=model.ConsigneeTel %></span>
                                </td>
                                <td class="form_text_row">
                                    邮政编码：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeZip">
                                        <%=model.ConsigneeZip %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    收货人邮箱：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneeEmail">
                                        <%=model.ConsigneeEmail %></span>
                                </td>
                                <td class="form_text_row">
                                    最佳送货时间：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbConsigneTime">
                                        <%=model.ConsignesTime %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    <img src="../images/notice.gif" alt="" title="订单金额=商品总价+运费+支付手续费" style="vertical-align: bottom;" />订单金额：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbOrderTotalPrice" style="color: Red; padding-top: 7px;
                                        display: block;">                                        
                                        <%=string.Format("{0:C}",model.TotalPrice) %>
                                    </span>
                                </td>
                                <td class="form_text_row">
                                    <img src="../images/notice.gif" alt="" title="保证金额=商品列表中的保证金之和" style="vertical-align: bottom;" />保证金：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbMarginPrice" style="color: Red; padding-top: 7px; display: block;">
                                        0.00</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    手续费：
                                </td>
                                <td class="form_ctrl_row">
                                </td>
                                <td class="form_text_row">
                                    <img src="../images/notice.gif" alt="" title="不含税金额=订单金额/1.17" style="vertical-align: bottom;" />是否带发票：
                                </td>
                                <td class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbBillingInvoice" style="color: Red; padding-top: 7px;
                                        display: block;">
                                        <%=model.Invoice==1?"是":"否" %></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    <img src="../images/notice.gif" alt="" style="vertical-align: bottom;" />运费：
                                </td>
                                <td colspan="3" class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbCarriage" style="color: Red;">
                                        <%=string.Format("{0:C}", model.TradeFees)%></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    其他费用：
                                </td>
                                <td colspan="3" class="form_ctrl_row">
                                    <span id="ctl00_workspace_lbOtherFees" style="color: Red;"><span style="">
                                        <%=string.Format("{0:C}", model.OtherFees)%></span></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_text_row">
                                    备注：
                                </td>
                                <td colspan="3" class="form_ctrl_row">
                                    <span style="background: #ACC0DB; min-width: 200px;">
                                        <%=model.Remark %></span>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="form_text_row">
                                    交易服务费：
                                </td>
                                <td class="form_ctrl_row" colspan="3">
                                    <span id="ctl00_workspace_lblservicePay">7.50</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    (备注：服务费按商品总金额5‰收取，不足5元按5元计算。)
                                </td>
                            </tr>
                        </table>
                        <table width="100%" id="tablist" cellspacing="0" cellpadding="0" class="datatable"
                            style="">
                            <tr>
                                <th width="">
                                    商品名称
                                </th>
                                <th width="">
                                    商品信息
                                </th>
                                <th width="">
                                    单价
                                </th>
                                <th width="">
                                    数量
                                </th>
                                <th width="">
                                    小计(单位:元)
                                </th>
                                <th>
                                    商品状态
                                </th>
                                <th width="" style="display: none">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href='<%#"http://"+System.Configuration.ConfigurationManager.AppSettings["web"]+"/"+Eval("ProId") %>.html'
                                                target='_blank' title="<%#Eval("ProName") %>">
                                                <%#Eval("ProName")%></a>
                                        </td>
                                        <td>
                                            规格：<%#Eval("DrugsBase_Specification")%>x<%#Eval("Specification")%>&nbsp;&nbsp;&nbsp;&nbsp;厂家：<%#Eval("DrugsBase_Manufacturer")%>
                                        </td>
                                        <td>
                                            <span style="color: red">
                                                <%#Eval("ProPrice","{0:C}") %></span>
                                        </td>
                                        <td>
                                            <font color="Blue">
                                                <%#Eval("ProNum")%></font>
                                        </td>
                                        <td>
                                            <font color="Red">
                                                <%#string.Format("{0:C}", (decimal)Eval("ProPrice") * (int)Eval("ProNum"))%></font>
                                        </td>
                                        <td>
                                            <%#SOSOshop.Model.Order.OrderProduct.GetStauts((int)Eval("Status"))%><%#(Eval("describe") != DBNull.Value ? "(" + Eval("describe") + ")" : "")%>
                                        </td>
                                        <td style="display: none">
                                            <a href="#" style="display: <%#(((int)Eval("Status")==1&&!(bool)Eval("issplit")&&SOSOshop.BLL.PowerPass.isPass("005004004")&&GetPaymentAndPaymentStatus())?"":"none")%>"
                                                class="easyui-menubutton" data-options="menu:'.mm<%#Eval("id") %>',iconCls:'icon-edit'">
                                                确认供货</a>
                                            <div class="mm<%#Eval("id") %>" style="width: 100px;">
                                                <div>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" Font-Underline="False"
                                                        CommandArgument='<%#(Eval("id")+":2") %>' OnClick="affirmOrder" OnClientClick="return confirm('您确定要设置确认供货?')">确认供货</asp:LinkButton></div>
                                                <div class="menu-sep">
                                                </div>
                                                <div data-options="iconCls:'icon-help'">
                                                    <span>确认预购</span>
                                                    <div style="width: 150px;">
                                                        <div>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" Font-Underline="False"
                                                                CommandArgument='<%#(Eval("id")+":3") %>' OnClick="affirmOrder" Style="padding: 0 90px 0 0;"
                                                                OnClientClick="return confirm('您确定要设置确认缺货，并将预计到货周期设置为3天?')">3天</asp:LinkButton></div>
                                                        <div>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" Font-Underline="False"
                                                                CommandArgument='<%#(Eval("id")+":3") %>' OnClick="affirmOrder" Style="padding: 0 90px 0 0;"
                                                                OnClientClick="return confirm('您确定要设置确认缺货，并将预计到货周期设置为7天?')">7天</asp:LinkButton></div>
                                                        <div>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Black" Font-Underline="False"
                                                                CommandArgument='<%#(Eval("id")+":3") %>' OnClick="affirmOrder" Style="padding: 0 90px 0 0;"
                                                                OnClientClick="return confirm('您确定要设置确认缺货，并将预计到货周期设置为15天?')">15天</asp:LinkButton></div>
                                                        <div>
                                                            <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Black" Font-Underline="False"
                                                                CommandArgument='<%#(Eval("id")+":3") %>' OnClick="affirmOrder" Style="padding: 0 70px 0 0;"
                                                                OnClientClick="return confirm('您确定要设置确认缺货，并将预计到货周期设置为1个月?')">1个月</asp:LinkButton></div>
                                                    </div>
                                                </div>
                                                <div class="menu-sep">
                                                </div>
                                                <div data-options="iconCls:'icon-no'">
                                                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Black" Font-Underline="False"
                                                        CommandArgument='<%#(Eval("id")+":5") %>' OnClick="affirmOrder" Style="padding: 0 38px 0 0;"
                                                        OnClientClick="return confirm('您确定要设置无货?')">无货</asp:LinkButton></div>
                                                <div data-options="iconCls:'icon-cancel'">
                                                    <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Black" Font-Underline="False"
                                                        CommandArgument='<%#(Eval("id")+":6") %>' OnClick="affirmOrder" Style="padding: 0 38px 0 0;"
                                                        OnClientClick="return confirm('您确定要设置取消?')">取消</asp:LinkButton></div>
                                            </div>
                                            <div style='<%#GetOperation((int)Eval("Status"))%>'>
                                                <asp:LinkButton ID="LinkButton8" OnClientClick="return confirm('您确定要完成?')" OnClick="SetOperation"
                                                    CommandArgument='<%#(Eval("id")+":8") %>' runat="server">完成</asp:LinkButton>|
                                                <asp:LinkButton ID="LinkButton9" OnClientClick="return confirm('您确定要取消?')" OnClick="SetOperation"
                                                    CommandArgument='<%#(Eval("id")+":6") %>' runat="server">取消</asp:LinkButton>
                                            </div>
                                            <div style='display: none'>
                                                <asp:LinkButton ID="LinkButton10" OnClientClick="return confirm('您确定要删除?')" OnClick="affirmOrder"
                                                    CommandArgument='<%#(Eval("id")+":0") %>' runat="server">删除</asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="5">
                                    <strong>商品总金额：<font color="Red"><%=string.Format("{0:C}",bllp.GetTotalPrices(model.OrderId))%></font></strong>
                                </td>
                            </tr>
                        </table>
                    </div>
                   
                </td>
            </tr>
        </table>
    </div>
    <div class="page_bottom">
    </div>
    <div class="page_toolbar" style="text-align: center;">
    </div>
    </form>
</body>
</html>
