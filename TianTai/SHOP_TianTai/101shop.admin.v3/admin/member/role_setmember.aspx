<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="role_setmember.aspx.cs" Inherits="_101shop.admin.v3.member.role_setmember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">成员设置
    <asp:HyperLink ID="HyperLink2" runat="server"  CssClass="inputbutton" NavigateUrl="role_list.aspx" Width="65px" Height="24px" style="vertical-align:bottom;">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">角色成员设置
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td height="22px" align="right" class="form_table_input_info">角色名：</td>
        <td align="left">
            <asp:Label ID="lbRoleName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  height="22px" class="form_table_input_info" align="right">
        角色描述：
        </td>
        <td align="left">
            <asp:Label ID="lbRoleDescription" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td height="22px" class="leftColumn" align="right">&nbsp;
        </td>
        <td align="left">
        <table width="200" border="0">
    <tr>
        <td rowspan="5" style="border-top:0 none;">
        被选项<br />
            <asp:ListBox ID="lbOption" runat="server" Height="259px" Width="237px">
            </asp:ListBox>
        </td>
        <td style="border-top:0 none;">
            &nbsp;</td>
        <td rowspan="5" style="border-top:0 none;">
         选中项<br />
            <asp:ListBox ID="lbOption2" runat="server" Height="259px" Width="233px">
            </asp:ListBox>
        </td>
    </tr>
    <tr>
        <td style="border-top:0 none;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="border-top:0 none;">
            <asp:Button ID="butAddRoleMember" runat="server" Text="添加&gt;&gt;" OnClick="butAddRoleMember_Click"  />
        </td>
    </tr>
    <tr>
        <td style="border-top:0 none;">
       
            <asp:Button ID="butRemoerMember" runat="server" Text="&lt;&lt;移除" OnClick="butRemoerMember_Click"  />
        </td>
    </tr>
    <tr>
    <td style="border-top:0 none;">&nbsp;</td>
    </tr>
        <td colspan="3"><span style="color:Red;">注：只需将被选项添加到选中项即可</span></td>
    </table>
    </td>

    </tr>
    <tr>
        <td class="leftColumn">&nbsp;
        </td>
        <td>
        </td>

    </tr>
</table>
</asp:Content>