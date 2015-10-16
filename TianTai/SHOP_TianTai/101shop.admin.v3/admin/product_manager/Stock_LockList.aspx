<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock_LockList.aspx.cs" Inherits="_101shop.admin.v3.admin.product_manager.Stock_LockList" %>

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
            $(".sl[oid]").each(function () {
                var o = $(this);
                $.ajax({
                    type: 'POST', url: "order_list.aspx?id=" + o.attr("oid") + "&jd=1",
                    success: function (c) { var h = $.trim(o.html()); if ('0' != c && '' != c) { o.html(h + " &nbsp;&nbsp;<span style='color:red'>[待建档<font color=blue>" + c + "</font>]</span>") } }
                });
            });
        });
    </script>
    <style type="text/css">
        a:link.Remark {
            color:#FF00BC;
        }
         a:visited.Remark {
            color:#FF00BC;
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
                            <a href="../order/orderEdit.aspx?id=<%#Eval("id") %>" class="<%#(string.IsNullOrEmpty((string)Eval("Remark"))?"":"Remark") %>">
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
                    <asp:TemplateField HeaderText="购买数量">
                        <ItemTemplate>
                            <a class="sl" href="orderEdit.aspx?id=<%#Eval("id") %>" oid="<%#Eval("orderId") %>">
                                <%#Eval("pcount")%></a>
                        </ItemTemplate>
                        <ItemStyle CssClass="unclick" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="买家姓名" />
                    <asp:BoundField DataField="parentCorpName" HeaderText="单位" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="金额" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="ShopDate" HeaderText="下单时间" DataFormatString="<span title='{0:yyyy-MM-dd HH:mm}'>{0:yyyy-MM-dd}<span>"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="BusinessCheckDate" HeaderText="确认供货时间" DataFormatString="<span title='{0:yyyy-MM-dd HH:mm}'>{0:yyyy-MM-dd}<span>"
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
                </Columns>
            </asp:GridView>
            <div class="quotes">
                <div style="float: right;">
                    共<span style="color: Red"><%=AspNetPager1.RecordCount%></span>条记录
                </div>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                    NextPageText="下一页" PageIndexBoxType="DropDownList" PrevPageText="上一页" SubmitButtonText="Go"
                    TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Style="text-align: right; float: right"
                    ShowPageIndexBox="Never" OnPageChanged="AspNetPager1_PageChanged">
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
