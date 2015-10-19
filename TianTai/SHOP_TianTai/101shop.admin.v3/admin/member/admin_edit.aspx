<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true"
    CodeBehind="admin_edit.aspx.cs" Inherits="_101shop.admin.v3.member.admin_edit"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script type="text/javascript" src="../scripts/public.js"></script>
    <script type="text/javascript">
        function skip() {
            if (window.top == window.self) {
                location = "/admin/admin_index.aspx";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    编辑管理员
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="保  存" />
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="admin_list.aspx"
        Width="65px" Height="24px" Style="vertical-align: bottom;">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    管理员的信息管理&nbsp;
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
    <table id="formtbl" runat="server" class="form_table_input" border="0" width="100%"
        cellspacing="0" cellpadding="0">
        <tr>
            <td class="form_table_input_info">
                管理员帐号：<asp:HiddenField ID="txtAdminId" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtNameTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                管理员密码：
            </td>
            <td>
                <asp:TextBox ID="txtPasswordRe" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtPasswordReTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                密码确认：
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtPasswordTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr id="Tr0" runat="server">
            <td class="form_table_input_info">
                角色设置：
            </td>
            <td>
                <asp:RadioButtonList ID="ckbPower" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">普通管理员</asp:ListItem>
                    <asp:ListItem Value="0">超级管理员</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <div class="msgNormal">
                    超级管理员：拥有所有权限。某些权限（如管理员管理、网站信息配置、角色管理等管理权限）只有超级管理员才有。 普通管理员：需要详细指定每一项角色权限
                </div>
            </td>
        </tr>
        <tr id="Tr1" runat="server">
            <td class="form_table_input_info">
                管理员状态：
            </td>
            <td>
                <asp:CheckBox ID="ckbState" runat="server" Text="冻结该帐号" />
                <br />
                <asp:CheckBox ID="ckbAllowModifyPassword" runat="server" Text="允许该管理员修改自己的密码" />
            </td>
            <td>
                <div class="msgNormal">
                    管理员的其他一些信息</div>
            </td>
        </tr>
        <tr id="Tr2" runat="server">
            <td class="form_table_input_info">
                管理开始时间：
            </td>
            <td>
                <asp:TextBox ID="txtManageBeginTime" MaxLength="10" Width="70" CssClass="datepicker"
                    runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtManageBeginTimeTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr id="Tr3" runat="server">
            <td class="form_table_input_info">
                管理结束时间：
            </td>
            <td>
                <asp:TextBox ID="txtManageEndTime" MaxLength="10" Width="70" CssClass="datepicker"
                    runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtManageEndTimeTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="保  存" />
</asp:Content>
