<%@ Page Language="C#" AutoEventWireup="true" Inherits="admin_systeminfo_log" CodeBehind="log.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="style/admin1.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_info">
            检索条件：<asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem Value="describe" Text="描述"></asp:ListItem>
                <asp:ListItem Value="source" Text="网址"></asp:ListItem>
                <asp:ListItem Value="ip" Text="ip"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp<asp:TextBox ID="TextBox1" Width="130" runat="server" CssClass="date_input"></asp:TextBox>
            日志类型:<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
            </asp:DropDownList>
            页大小
            <asp:DropDownList ID="pageSize" OnSelectedIndexChanged="PageSize_SelectedIndexChanged"
                AutoPostBack="true" runat="server">
            </asp:DropDownList>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="True" Text="精确" Selected="True"></asp:ListItem>
                <asp:ListItem Value="False" Text="模糊"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="Button1" CssClass="inputbutton" runat="server" Text="查询" OnClick="Button1_Click" />
            <asp:Button ID="Button2" CssClass="inputbutton" runat="server" Text="清除" OnClick="Button2_Click" />
            <a href="log.aspx">刷新</a>
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">重启服务</asp:LinkButton>
        </div>
    </div>
    <div class="page_main">
        <table width="100%" id="tablist" cellspacing="0" cellpadding="0" class="datatable"
            style="">
            <tr>
                <th width="30%">
                    描述
                </th>
                <th width="15%">
                    IP
                </th>
                <th width="25%">
                    网址
                </th>
                <th width="10%">
                    管理员
                </th>
                <th width="15%">
                    时间
                </th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="line-height: 23px">
                            <a class="viewMember" href="LogInfo.aspx?id=<%#Eval("id") %>" target="_blank">
                                <%#Eval("describe")%></a>
                        </td>
                        <td>
                            <%#Eval("ip")%>
                        </td>
                        <td>
                            <a href="<%#url(Eval("source"))%>" target="_blank">
                                <%#url(Eval("source"))%></a>
                        </td>
                        <td>
                            <%#Eval("username")%>
                        </td>
                        <td>
                            <%#Eval("created")%>
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
