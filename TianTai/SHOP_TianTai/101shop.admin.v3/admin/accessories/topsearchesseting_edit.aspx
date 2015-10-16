<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="topsearchesseting_edit.aspx.cs" Inherits="_101shop.admin.v3.accessories.topsearchesseting_edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script src="../scripts/public.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">热门搜索设置
<asp:Button ID="Button2" runat="server" CssClass="inputbutton" onclick="lbutSave_Click" Text="保存" />
    <asp:HyperLink ID="HyperLink1" NavigateUrl="topsearchesseting_list.aspx" runat="server"  CssClass="inputbutton" Width="65px" Height="24px" style="vertical-align:bottom;">返 回</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
     <table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
		    <td class="form_table_input_info">关键词：</td>
		    <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
		    <td>
                <asp:Panel ID="txtNameTip" runat="server"></asp:Panel>
            </td>
	    </tr>
	     <tr>
		    <td class="form_table_input_info">链接地址：</td>
		    <td>
                <asp:TextBox ID="txtLinkAd" runat="server" Width="250"></asp:TextBox>
            </td>
		    <td>
                <asp:Panel ID="txtLinkAdTip" runat="server"></asp:Panel>
            </td>
	    </tr>
        <tr>
		    <td class="form_table_input_info">排列顺序：
            </td>
		    <td>
                <asp:TextBox ID="txtSort" CssClass="short_input" runat="server"></asp:TextBox>
            </td>
		    <td>
                <asp:Panel ID="txtSortTip" runat="server">
                </asp:Panel>
            </td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">是否显示：
            </td>
		    <td>
                <asp:RadioButtonList ID="rdolstIsShow" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="是" />
                    <asp:ListItem Value="0" Text="否" Selected="True" />
                </asp:RadioButtonList>
            </td>
		    <td>
                <asp:Panel ID="rdolstIsShowTip" runat="server">
                </asp:Panel>
            </td>
	    </tr>
    </table>
</asp:Content>

<asp:Content ID="Contbottom" runat="server" ContentPlaceHolderID="pagebottom">
  <asp:Button ID="Button1" runat="server" CssClass="inputbutton" onclick="lbutSave_Click" Text="保存" />
    <asp:HyperLink ID="HyperLink2" NavigateUrl="topsearchesseting_list.aspx" runat="server"  CssClass="inputbutton" Width="65px" Height="24px" style="vertical-align:bottom;">返 回</asp:HyperLink>
</asp:Content>
