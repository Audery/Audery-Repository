<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="MemberIntegralRank.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralRank" Title="" %>
<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <style type="text/css">
    table { width:98%; padding-left:10px; margin:10px 0px 20px 0px;} td { line-height:150%; padding:4px;}
    .Member_Class { padding:4px;}
    </style>
    <script type="text/javascript">
        function Search(id, elements, url) {
            var SearchDiv = jQuery('#' + id);
            var f = document.createElement('form'); f.setAttribute('action', url); f.setAttribute('method', 'get');
            var ids = elements.toString().split(',');
            for (i = 0; i < ids.length; i++) {
                var n = ids[i], ls = jQuery('input[name*="' + n + '"],select[name*="' + n + '"]');
                if (ls.length) {
                    var l = ls[0];
                    if (l.tagName.toLowerCase() == "input") {
                        p = document.createElement('input'); p.setAttribute('type', 'hidden');
                        p.setAttribute('name', n);
                        if (l.getAttribute("type") == "checkbox" || l.getAttribute("type") == "radio") {
                            p.setAttribute('value', l.checked ? "1" : "0");
                        } else {
                            p.setAttribute('value', l.value);
                        }
                        f.appendChild(p);
                    }
                    if (l.tagName.toLowerCase() == "select" && l.selectedIndex > 0) {
                        p = document.createElement('input'); p.setAttribute('type', 'hidden');
                        p.setAttribute('name', n); p.setAttribute('value', l.options[l.selectedIndex].value); f.appendChild(p);
                    }
                }
            }
            document.body.appendChild(f); f.submit(); return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">积分排行查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
<div id="SearchDiv">
客户姓名：<asp:TextBox ID="TextBox_truename" runat="server" Width="100"></asp:TextBox>
电话：<asp:TextBox ID="TextBox_phone" runat="server" Width="100"></asp:TextBox>
类型：<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
公司名称：<asp:TextBox ID="TextBox_CompanyName" runat="server" Width="100"></asp:TextBox>
<%if(seeAll){ %>交易员：<asp:DropDownList ID="ddlEditer" runat="server" DataTextField="name" DataValueField="id" Width="75"></asp:DropDownList><%} %>
时间：<asp:TextBox ID="fromDate" runat="server" CssClass="datepicker" Width="70"></asp:TextBox>至<asp:TextBox ID="toDate" runat="server" CssClass="datepicker" Width="70"></asp:TextBox>
&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" Text="查看自己客户" />
<input type="button" class="inputbutton" value="查询" onclick="Search('SearchDiv', 'truename,phone,DropDownList1,CompanyName,fromDate,toDate,CheckBox1,Editer', 'MemberIntegralRank.aspx')" style="width:65px;height:23px" />
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
            <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <b>客户姓名</b>
                </td>
                <td>
                    <b>电话</b>
                </td>
                <td>
                    <b>类型</b>
                </td>
                <td>
                    <b>公司名称</b>
                </td>
                <td>
                    <b>积分</b>
                </td>
                <td>
                    <b>可用积分</b>
                </td>
                <td>
                    <b>总积分</b>
                </td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("truename")%>
                        </td>
                        <td>
                            <%#Eval("phone")%>
                        </td>
                        <td>
                            <%#Eval("CompanyClass")%>
                        </td>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <a href="MemberIntegralDetail.aspx?uid=<%#Eval("uid")%>&fromDate=<%=Request["fromDate"]%>&toDate=<%=Request["toDate"]%>&editer=<%=Request["editer"]%>"><%#Eval("integral")%></a>
                        </td>
                        <td>
                            <%#Eval("realityIntegral")%>
                        </td>
                        <td>
                            <%#Eval("totalIntegral")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="quotes" style="float: right;">
            <div>
                <asp:Literal ID="pages" runat="server"></asp:Literal>
            </div>
        </div>
</asp:Content>
