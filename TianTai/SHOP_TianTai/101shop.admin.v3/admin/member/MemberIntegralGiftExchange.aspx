<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="MemberIntegralGiftExchange.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralGiftExchange" Title="" %>
<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <style type="text/css">
    table { width:98%; padding-left:10px; margin:10px 0px 20px 0px;} td { line-height:150%; padding:4px;}
    .State0 { color:#999999; }.State1 { color:#ff3333; }.State2 { color:#0066CC; }.State3 { color:#666666; }
    </style>
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

        function Edit(btn, id, State, act) {
            window.location = 'MemberIntegralGiftExchange_Comfirm.aspx?id=' + id + '&ReturnUrl=' + document.URL;
            return;
            if (State == '1') {
                window.location = 'MemberIntegralGiftExchange_Comfirm.aspx?id=' + id + '&ReturnUrl=' + document.URL;
            }
            else {
                if (window.confirm("确认" + btn.value + "？")) {
                    $.ajax({
                        type: 'POST',
                        url: 'MemberIntegralGiftExchange_Comfirm.aspx?ajax=1',
                        data: { id: id, act: act },
                        dataType: "json",
                        success: function (msg, textStatus) {
                            //alert(msg + textStatus);
                            switch (msg["state"]) {
                                case 1:
                                    alert(msg["message"]);
                                    window.location = 'MemberIntegralGiftExchange.aspx';
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
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">兑换礼品申请管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
<div id="SearchDiv">
客户姓名：<asp:TextBox ID="TextBox_truename" runat="server" Width="100"></asp:TextBox>
电话：<asp:TextBox ID="TextBox_phone" runat="server" Width="100"></asp:TextBox>
公司名称：<asp:TextBox ID="TextBox_CompanyName" runat="server" Width="100"></asp:TextBox>
礼品名称：<asp:TextBox ID="TextBox_GiftName" runat="server" Width="100"></asp:TextBox>
状态：<asp:DropDownList ID="DropDownList_State" runat="server">
        <asp:ListItem Value="">全部</asp:ListItem>
        <asp:ListItem Value="0">已取消</asp:ListItem>
        <asp:ListItem Value="1">待处理</asp:ListItem>
        <asp:ListItem Value="2">已处理</asp:ListItem>
        <asp:ListItem Value="3">已邮寄</asp:ListItem>
    </asp:DropDownList>
<input type="button" class="inputbutton" value="查询" onclick="Search('SearchDiv', 'truename,phone,CompanyName,GiftName,State', 'MemberIntegralGiftExchange.aspx')" style="width:65px;height:23px" />
        &nbsp;<font color="gray" style="font-size:12px;font-weight:normal;display:block;">
        (状态处理：&nbsp;待处理: 客户提交还未处理；&nbsp;已处理: 已经确认并检查礼品、邮寄地址；&nbsp;已邮寄: 已经邮寄出去)</font>
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
                    <b>公司名称</b>
                </td>
                <td>
                    <b>礼品名称</b>
                </td>
                <td align="center">
                    <b>兑换数量</b>
                </td>
                <td style="width:75px">
                    <b>状态</b>
                </td>
                <td>
                    <b>处理时间</b>
                </td>
                <td>
                    <b>处理人</b>
                </td>
                <td>
                    <b>备注</b>
                </td>
                <%if(isPass){ %>
                <td>
                    <b>操作</b>
                </td><%} %>
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
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("GiftName")%>
                        </td>
                        <td align="center">
                            <b><%#Convert.ToInt32(Eval("Gift_Number"))%></b>
                        </td>
                        <td>
                            <%#((int)Eval("State") == 0 ? "<span class='State0'>已取消</span>" : ((int)Eval("State") == 2 ? "<span class='State2' title='已经确认并检查礼品、邮寄地址'>已处理</span>" : ((int)Eval("State") == 3 ? "<span class='State3' title='已经邮寄出去'>已邮寄</span>" : "<span class='State1' title='客户提交还未处理'>待处理</span>")))%>
                        </td>
                        <td>
                            <%#Convert.ToDateTime(Eval("ontime")).ToString("yyyy-MM-dd HH:mm")%>
                        </td>
                        <td>
                            <%#Eval("EditerName")%>
                        </td>
                        <td>
                            <%#Eval("remark")%>
                        </td>
                        <td><%if(isPass){ %>
                            <input class="inputbutton" onclick="Edit(this,'<%#Eval("id")%>','<%#Eval("State")%>', '')" type="button" value="处理" style="border:0 none; color:Blue;" />
                        </td><%} %>
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
