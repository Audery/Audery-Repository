<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="_101shop.admin.v3.admin.order.OrderList" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <link href="../style/jquery.qtip.min.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.qtip.min.js" type="text/javascript"></script>
    <link href="../scripts/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".cancel").click(function () {
                return confirm("您确定操作吗？");
            });
            $(".tip[oid]").each(function () {
                $(this).qtip({
                    content: {
                        text: "加载中...",
                        ajax: {
                            url: "order_list.aspx?id=" + $(this).attr("oid")
                        }
                    }
                });
            });
            //$(".sl[oid]").each(function () {
            //    var o = $(this);
            //    $.ajax({
            //        type: 'POST', url: "order_list.aspx?id=" + o.attr("oid") + "&jd=1",
            //        success: function (c) { var h = $.trim(o.html()); if ('0' != c && '' != c) { o.html(h + " &nbsp;&nbsp;<span style='color:red'>[待建档<font color=blue>" + c + "</font>]</span>") } }
            //    });
            //});
        });
    </script>
    <style type="text/css">
        a:link.Remark
        {
            color: #FF00BC;
        }
        a:visited.Remark
        {
            color: #FF00BC;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            订单编号：<asp:TextBox ID="TextBox6" runat="server" Width="100"></asp:TextBox>
            买家姓名：<asp:TextBox ID="TextBox1" runat="server" Width="100"></asp:TextBox>
            买家单位：<asp:TextBox ID="TextBox2" runat="server" Width="100"></asp:TextBox>
            买家类型：
            <asp:DropDownList ID="DropDownList1" runat="server">               
            </asp:DropDownList>
            付款方式：
            <asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem Value="0">全部</asp:ListItem>
                <asp:ListItem Value="1">货到付款</asp:ListItem>
                <asp:ListItem Value="2">款到发货</asp:ListItem>
                <asp:ListItem Value="3">帐期结算</asp:ListItem>
            </asp:DropDownList>
            订单状态
            <asp:DropDownList ID="DropDownListStatus" runat="server">
                <asp:ListItem Value="0">全部</asp:ListItem>
                <asp:ListItem Value="1">待审核</asp:ListItem>
                <asp:ListItem Value="2">已审核</asp:ListItem>
                <asp:ListItem Value="3">已支付</asp:ListItem>
                <asp:ListItem Value="4">已完成</asp:ListItem>
                <asp:ListItem Value="-1">已取消</asp:ListItem>
                <asp:ListItem Value="-2">已作废</asp:ListItem>
            </asp:DropDownList>
            <br />
            品种：<asp:TextBox ID="TextBox3" runat="server" Width="100"></asp:TextBox>
            生产厂家：<asp:TextBox ID="txtManufacturer" runat="server" Width="100"></asp:TextBox>
            批准文号：<asp:TextBox ID="txtApprovalNumber" runat="server" Width="100"></asp:TextBox>
            交易员：<asp:DropDownList ID="ddlEditer" runat="server" DataTextField="name" DataValueField="id"
                Width="75">             
            </asp:DropDownList>           
            <br />
            下单时间：<asp:TextBox ID="TextBox4" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd HH:mm',errDealMode:0 })"></asp:TextBox>
            到
            <asp:TextBox ID="TextBox5" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd HH:mm',errDealMode:0 })"></asp:TextBox>
            <span class="date" style="display: inline; padding-left: 5px;">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">今日</asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton3_Click">昨日</asp:LinkButton>
                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton3_Click">最近7天</asp:LinkButton>
                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton3_Click">最近30天</asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton3_Click">本月</asp:LinkButton>
                <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton3_Click">上月</asp:LinkButton>
                <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton3_Click">全部</asp:LinkButton>
            </span>
            <cc1:ButtionManage ID="SearchManage" OnClick="Search_Click" Text="搜索" runat="server"
                onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            <input type="button" id="btnReSet" runat="server" class="inputbutton" onclick="location = 'order_list.aspx'"
                value="重置" onmouseover="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onmouseout="javascript:document.getElementById(this.id).className='inputbutton'" />
        </div>
    </div>
    <div class="page_main">
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:TemplateField HeaderText="订单号">
                    <ItemTemplate>
                        <a href="orderEdit.aspx?id=<%#Eval("id") %>" class="<%#(string.IsNullOrEmpty((string)Eval("Remark"))?"":"Remark") %>">
                            <%#Eval("orderId")%></a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品">
                    <ItemTemplate>
                        <a class="tip" href="orderEdit.aspx?id=<%#Eval("id") %>" oid="<%#Eval("orderId") %>">
                            <%#Eval("ProName")%></a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="品种数量">
                    <ItemTemplate>
                        <a class="sl" href="orderEdit.aspx?id=<%#Eval("id") %>" oid="<%#Eval("orderId") %>">
                            <%#Eval("pcount")%></a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="买家姓名" />
                <asp:BoundField DataField="parentCorpName" HeaderText="单位" />
                <asp:BoundField DataField="TotalPrice" HeaderText="金额" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="ShopDate" HeaderText="下单时间" DataFormatString="<span title='{0:yyyy-MM-dd HH:mm}'>{0:yyyy-MM-dd HH:mm}<span>"
                    HtmlEncode="false" />                
              
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%#SOSOshop.Model.Order.Orders.GetOrderStatus((int)Eval("OrderStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交易员">
                    <ItemTemplate>
                        <%#Eval("adminname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="cancelOrder" CommandArgument='<%#Eval("orderid") %>'
                            CssClass="cancel">取消</asp:LinkButton>
                        &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" OnClick="ConfirmOrder"
                            CommandArgument='<%#Eval("orderid") %>' CssClass="cancel">确认支付</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="quotes">
            <div style="float: right;">
                共<span style="color: Red"><%=AspNetPager1.RecordCount%></span>条记录
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                NextPageText="下一页" PageIndexBoxType="DropDownList" PrevPageText="上一页" SubmitButtonText="Go"
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Style="text-align: right;
                float: right" ShowPageIndexBox="Auto" OnPageChanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
            <div style="float: right; color: #666666;">
                （品种数量:<span style="color: Red"><%=c_pcount%></span> 金额:<span style="color: Red"><%=c_TotalPrice.ToString("f2")%></span>）
            </div>
        </div>
    </div>
    </form>
    <%Response.Write("<script type=\"text/javascript\">var a=top.window.document.getElementById('mainFrame'),b=a.contentWindow;b.window.isAdd();/*isBrowse();isEdit();isAdd();isDelete();*/</script>"); %>
</body>
</html>
