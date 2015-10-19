<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="article_read.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.article_read" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../../scripts/validate.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    <asp:Literal ID="ltlLink" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="workspace" runat="server">
    <div class="textcontent">
        <asp:Label ID="lblMark" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="ltlContent" runat="server" Text="Label"></asp:Label>
    </div>
</asp:Content>
