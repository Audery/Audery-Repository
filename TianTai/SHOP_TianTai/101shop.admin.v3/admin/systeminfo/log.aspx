<%@ Page Language="C#" AutoEventWireup="true" Inherits="admin_systeminfo_log" CodeBehind="log.aspx.cs" %>

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
            日志来源：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="LogAdmin">后台日志</asp:ListItem>
                <%--<asp:ListItem Value="LogDrugsbase">基础数据</asp:ListItem>--%>
                <asp:ListItem Value="LogShop">商城日志</asp:ListItem>
                <asp:ListItem Value="LogService">服务日志</asp:ListItem>
            </asp:DropDownList>
            检索条件：<asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem Value="describe" Text="描述"></asp:ListItem>
                <asp:ListItem Value="username" Text="帐号"></asp:ListItem>
                <asp:ListItem Value="source" Text="网址"></asp:ListItem>
                <asp:ListItem Value="ip" Text="客户端IP"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp<asp:TextBox ID="TextBox1" Width="130" runat="server" CssClass="date_input"></asp:TextBox>
            日志类型:<asp:DropDownList ID="DropDownList4" runat="server">
                <asp:ListItem Value="-1">全部</asp:ListItem>
                <asp:ListItem Value="0">系统日志</asp:ListItem>
                <asp:ListItem Value="1">操作日志</asp:ListItem>
                <asp:ListItem Value="2">系统异常</asp:ListItem>
                <asp:ListItem Value="3">404Code</asp:ListItem>
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
        </div>
    </div>
    <div class="page_main">
        <table width="100%" id="tablist" cellspacing="0" cellpadding="0" class="datatable"
            style="">
            <tr>
                <th>
                    描述
                </th>
                <th>
                    客户端IP
                </th>
                <th>
                    来源
                </th>
                <th>
                    帐号
                </th>
                <th>
                    日志类型
                </th>
                <th style="width: 150px">
                    时间
                </th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="line-height: 23px">
                            <a class="viewMember" href="LogInfo.aspx?type=<%#DropDownList1.SelectedValue %>&id=<%#Eval("id") %>"
                                target="_blank">
                                <%#Server.HtmlEncode(Eval("describe").ToString())%></a>
                        </td>
                        <td>
                            <%#Eval("ip")%>
                        </td>
                        <td>
                            <a >
                                <%#Server.HtmlEncode(Eval("source").ToString())%></a>
                        </td>
                        <td>
                            <%#Eval("username")%>
                        </td>
                        <td>
                            <%#SOSOshop.Model.Logs.Log.GetType((int)Eval("type")) %>
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
