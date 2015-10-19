<%@ Page Language="C#" AutoEventWireup="true" Inherits="admin_systeminfo_log_price"
    CodeBehind="log_price.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_info">
            检索条件：<asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem Value="describe" Text="描述"></asp:ListItem>
                <asp:ListItem Value="username" Text="帐号"></asp:ListItem>
                <asp:ListItem Value="source" Text="网址"></asp:ListItem>
                <asp:ListItem Value="ip" Text="客户端IP"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp<asp:TextBox ID="TextBox1" Width="130" runat="server" CssClass="date_input"></asp:TextBox>
            页大小
            <asp:DropDownList ID="pageSize" OnSelectedIndexChanged="PageSize_SelectedIndexChanged"
                AutoPostBack="true" runat="server">
            </asp:DropDownList>
            <asp:Button ID="Button1" CssClass="inputbutton" runat="server" Text="查询" OnClick="Button1_Click" />
        </div>
    </div>
    <div class="page_main">
        <table width="100%" id="tablist" cellspacing="0" cellpadding="0" class="datatable"
            style="">
            <tr>
                <th>
                    描述
                </th>
                <th style="width: 150px">
                    时间
                </th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="line-height: 23px">
                            <%#Eval("describe")%>
                        </td>
                        <td>
                            <%#((DateTime)Eval("created")).ToLocalTime()%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="quotes">
            <div style="float: right;">
                共<span style="color: Red"><%=AspNetPager1.RecordCount%></span>条记录
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                NextPageText="下一页" PageIndexBoxType="DropDownList" PrevPageText="上一页" SubmitButtonText="Go"
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Style="text-align: right;
                float: right" ShowPageIndexBox="Never" OnPageChanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
        </div>
    </div>
    </form>
</body>
</html>
