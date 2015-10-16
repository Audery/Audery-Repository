<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAction.aspx.cs" Inherits="_101shop.admin.v3.admin._member.MemberAction" %>

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
    <link href="../scripts/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ReSet() {
            location.href = "MemberAction.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            检索条件：<asp:DropDownList ID="whereFieldDr" runat="server">
                <asp:ListItem Value="Name" Text="买家姓名"></asp:ListItem>
                <asp:ListItem Value="MobilePhone" Text="手机号码"></asp:ListItem>
                <asp:ListItem Value="sessionid" Text="登陆批次"></asp:ListItem>
            </asp:DropDownList>&nbsp;<asp:TextBox ID="whereStringTe" Width="100" runat="server" CssClass="date_input"></asp:TextBox>&nbsp;
            <%if(SOSOshop.BLL.PowerPass.isPass("008010001")){ %>交易员：<asp:DropDownList ID="ddlEditer" runat="server" DataTextField="name" DataValueField="id" Width="75"></asp:DropDownList>
            <%} %>&nbsp;
            浏览页面：<asp:TextBox ID="TextBox1" Width="100" runat="server" CssClass="date_input"></asp:TextBox>
            执行操作：<asp:TextBox ID="TextBox2" Width="100" runat="server" CssClass="date_input"></asp:TextBox>
            &nbsp;&nbsp; 起始时间：
            <asp:TextBox ID="TextBox4" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd',errDealMode:0 })"></asp:TextBox>
            到
            <asp:TextBox ID="TextBox5" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd',errDealMode:0 })"></asp:TextBox>
            &nbsp;&nbsp;页大小
            <asp:DropDownList ID="pageSize" OnSelectedIndexChanged="PageSize_SelectedIndexChanged"
                AutoPostBack="true" runat="server">
            </asp:DropDownList>
            <cc1:ButtionManage ID="SearchManage" OnClick="Search_Click" Text="搜索" runat="server"
                onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
        </div>
    </div>
    <div class="page_main">
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:BoundField DataField="TrueName" HeaderText="会员名" />
                <asp:BoundField DataField="MobilePhone" HeaderText="手机" />
                <asp:BoundField DataField="actuation" HeaderText="浏览页面" />
                <asp:BoundField DataField="actuationvalue" HeaderText="执行操作" />
                <asp:TemplateField HeaderText="停留时间">
                    <ItemTemplate>
                        <%#GetShowTime(Eval("SleepTime")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="created" HeaderText="浏览时间" />
                <asp:BoundField DataField="OS" HeaderText="操作系统" />
                <asp:TemplateField HeaderText="浏览器">
                    <ItemTemplate>
                        <a title="<%#Eval("sessionid") %>">
                            <%#Eval("WebBrowser")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="方法">
                    <ItemTemplate>
                        <a href="<%#Eval("url") %>" target="_blank">
                            <%#Eval("HttpMethod") %></a>
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
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Style="text-align: right;
                float: right" ShowPageIndexBox="Never" OnPageChanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
        </div>
    </div>
    </form>
    <%Response.Write("<script type=\"text/javascript\">var a=top.window.document.getElementById('mainFrame'),b=a.contentWindow;b.window.isAdd();/*isBrowse();isEdit();isAdd();isDelete();*/</script>"); %>
</body>
</html>
