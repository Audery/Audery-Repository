<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="role_edit.aspx.cs" Inherits="_101shop.admin.v3.member.role_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script src="../scripts/public.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">管理员角色
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" />
    <asp:HyperLink ID="HyperLink1" runat="server"  CssClass="inputbutton" NavigateUrl="role_list.aspx" Width="65px" Height="24px" style="vertical-align:bottom;">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">添加或编辑角色&nbsp; 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
     <table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
		    <td class="form_table_input_info">角色名称：</td>
		    <td>
                <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
            </td>
		    <td>
                <asp:Panel ID="txtRoleNameTip" runat="server"></asp:Panel>
            </td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">描述：
            </td>
		    <td>
                <asp:TextBox ID="txtDescription"  TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
		    <td>
                <asp:Panel ID="txtDescriptionTip" runat="server">
                </asp:Panel>
            </td>
	    </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" />
</asp:Content>