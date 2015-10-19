<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="MemberIntegralGift.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralGift" Title="" %>
<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <style type="text/css">
    table { width:98%; padding-left:10px; margin:10px 0px 20px 0px;} td { line-height:150%; padding:4px;}
    .Member_Class { padding:4px;}
    </style>
    <script type="text/javascript">
        function Shangjia(btn, id) {
            if (window.confirm("确认要" + btn.value + "？")) {
                $.ajax({
                    type: 'POST',
                    url: 'MemberIntegralGift.aspx?ajax=1',
                    data: { id: id, Shangjia: 1 },
                    dataType: "json",
                    success: function (msg, textStatus) {
                        //alert(msg + textStatus);
                        switch (msg["state"]) {
                            case 1:
                                alert(msg["message"]);
                                window.location = 'MemberIntegralGift.aspx';
                                break;
                            default:
                                alert(msg["message"]);
                                break;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + " error");
                    }
                });
            }
        }
        function Del(btn, id) {
            if (window.confirm("确认要" + btn.value + "？")) {
                $.ajax({
                    type: 'POST',
                    url: 'MemberIntegralGift.aspx?ajax=1',
                    data: { id: id, Del: 1 },
                    dataType: "json",
                    success: function (msg, textStatus) {
                        //alert(msg + textStatus);
                        switch (msg["state"]) {
                            case 1:
                                alert(msg["message"]);
                                window.location = 'MemberIntegralGift.aspx';
                                break;
                            default:
                                alert(msg["message"]);
                                break;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + " error");
                    }
                });
            }
        }
        function Edit(btn, id) {
            window.location = 'MemberIntegralGift_Edit.aspx?id=' + id + '&ReturnUrl=' + document.URL;
        }
        function Add() {
            window.location = 'MemberIntegralGift_Add.aspx?ReturnUrl=MemberIntegralGift.aspx';
        }
    </script>
    <script type="text/javascript">
        function Search(id, elements, url) {
            var SearchDiv = jQuery('#' + id);
            var f = document.createElement('form'); f.setAttribute('action', url); f.setAttribute('method', 'get');
            var ids = elements.toString().split(',');
            for (i = 0; i < ids.length; i++) {
                var n = ids[i], l = jQuery('input[name*="' + n + '"],select[name*="' + n + '"]')[0];
                if (l.tagName.toLowerCase() == "input" && l.value.length > 0) {
                    p = document.createElement('input'); p.setAttribute('type', 'hidden');
                    p.setAttribute('name', n); p.setAttribute('value', l.value); f.appendChild(p);
                }
                if (l.tagName.toLowerCase() == "select" && l.selectedIndex > 0) {
                    p = document.createElement('input'); p.setAttribute('type', 'hidden');
                    p.setAttribute('name', n); p.setAttribute('value', l.options[l.selectedIndex].value); f.appendChild(p);
                }
            }
            document.body.appendChild(f); f.submit(); return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">积分礼品管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
<div id="SearchDiv">
礼品名称：<asp:TextBox ID="TextBox_name" runat="server" Width="100"></asp:TextBox>
状态：<asp:DropDownList ID="DropDownList_State" runat="server">
    <asp:ListItem Value="">全部</asp:ListItem>
    <asp:ListItem Value="0">已删除</asp:ListItem>
    <asp:ListItem Value="1">上架</asp:ListItem>
    <asp:ListItem Value="2">下架</asp:ListItem>
</asp:DropDownList>
<input type="button" class="inputbutton" value="查询" onclick="Search('SearchDiv', 'name,State', 'MemberIntegralGift.aspx')" style="width:65px;height:23px" />
<input type="button" class="inputbutton" value="增加礼品" onclick="Add()" style="width:65px;height:23px; color:Blue;" />
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
            <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <b>积分名称</b>
                </td>
                <td width="350">
                    <b>兑换说明</b>
                </td>
                <td align="center">
                    <b>兑换积分</b>
                </td>
                <td align="center">
                    <b>可兑换数量</b>
                </td>
                <td>
                    <b>可兑换客户类型</b>
                </td>
                <td>
                    <b>状态</b>
                </td>
                <td>
                    <b>操作</b>
                </td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("name")%>
                        </td>
                        <td>
                            <%#Eval("detail")%>
                        </td>
                        <td align="center">
                            <b><%#Convert.ToInt32(Eval("Integral"))%></b>
                        </td>
                        <td align="center">
                            <b><%#Convert.ToInt32(Eval("Number"))%></b>
                        </td>
                        <td>
                            <%#SOSOshop.BLL.Integral.MemberIntegralGift_Member_Class.Get((string)Eval("Member_Class"), "<span class=Member_Class>{0}</span>")%>
                        </td>
                        <td>
                            <%#((int)Eval("State") == 0 ? "已删除" : ((int)Eval("State") == 1 ? "上架" : "下架"))%>
                        </td>
                        <td>
                            <input class="inputbutton" onclick="Shangjia(this,'<%#Eval("id")%>')" type="button" value="<%#((int)Eval("State") == 1 ? "下架" : "上架")%>" 
                            style="<%#((int)Eval("State") == 0 ? "display:none;" : "")%>border:0 none; color:Blue;" />
                            <input class="inputbutton" onclick="Del(this,'<%#Eval("id")%>')" type="button" value="删除" 
                            style="<%#((int)Eval("State") == 0 ? "display:none;" : "")%>border:0 none; color:Blue;" />
                            <input class="inputbutton" onclick="Edit(this,'<%#Eval("id")%>')" type="button" value="修改" 
                            style="<%#((int)Eval("State") == 0 ? "display:none;" : "")%>border:0 none; color:Blue;" />
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
