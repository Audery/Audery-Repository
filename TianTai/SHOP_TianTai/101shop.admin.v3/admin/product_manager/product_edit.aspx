<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="product_edit.aspx.cs" Inherits="_101shop.admin.v3.admin.product_manager.product_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/style/validator.css" rel="stylesheet" type="text/css" />
    <link href="/admin/style/toolbar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            width: 479px;
            height: 30px;
            line-height: 30px;
        }
        .style4
        {
            width: 86px;
            font-weight: bold;
        }
        td
        {
            padding: 3px 3px 3px 3px;
            font-size: 12px;
            border-bottom-style: solid;
            border-bottom-color: #efefef;
            border-bottom-width: 1px;
        }
        #Text1
        {
            width: 352px;
        }
        #Text2
        {
            width: 352px;
        }
        #Text3
        {
            width: 352px;
        }
        #Text4
        {
            width: 352px;
        }
        .style5
        {
            width: 86px;
            font-weight: bold;
        }
        .style7
        {
            width: 85px;
            font-weight: bold;
        }
        .style8
        {
            width: 86px;
            font-weight: bold;
        }
        .style9
        {
            width: 98px;
        }
    </style>
    <script type="text/javascript" src="../scripts/productSelect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    <div style="height: 25px;">
        <div style="float: left; height: 25px;">
            商品信息
            <asp:Button ID="button2" runat="server" CssClass="inputbutton" OnClick="btnSave_Click"
                Text="保  存" /></div>
        <div style="float: left; height: 25px; width: 500px;">
            &nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="product_list.aspx"
                Width="65px" Height="24px" Style="vertical-align: bottom;">返 回</asp:HyperLink></div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:HiddenField ID="txtId" runat="server" />
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
    <table border="0" class="style1" cellpadding="2" cellspacing="0">
        <tr>
            <td class="style2" colspan="5">
                药品基础信息<hr />
            </td>
        </tr>
        <tr>
            <td class="style8" bgcolor="#E6E6E6">
                通用名：
            </td>
            <td class="style3">
                <%=product.DrugName%>&nbsp;
            </td>
            <td class="style4" bgcolor="#E6E6E6">
                商品名：
            </td>
            <td>
                <%=product.ProName%>&nbsp;
            </td>
            <td rowspan="4">
                <img src="<%=product.Image%>" width="168" onerror="this.src='/images/nopic1.jpg'">&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style8" bgcolor="#E6E6E6">
                规格：
            </td>
            <td class="style3">
                <%=product.Specification %>
                &nbsp;
            </td>
            <td class="style4" bgcolor="#E6E6E6">
                剂型：
            </td>
            <td>
                <%=product.Formulation %>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style8" bgcolor="#E6E6E6">
                生产厂家：
            </td>
            <td class="style3">
                <%=product.Manufacturer %>&nbsp;
            </td>
            <td class="style4" bgcolor="#E6E6E6">
                批准文号：
            </td>
            <td>
                <%=product.ApprovalNumber %>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="4">
                <b>转换比：</b><%=product.ConveRatio %>
                &nbsp;&nbsp;&nbsp; <b>件装量：</b><%=product.Goods_Pcs %>
                <%=product.Goods_Unit %>&nbsp;&nbsp;&nbsp; <b>中包装：</b><%=product.Goods_Pcs_Small %>
                <%=product.Goods_Unit %>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="4">
                商品信息<hr />
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                商品名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Width="438px" Style="-moz-user-select: none;"
                    onselectstart="javascript:return false;"></asp:TextBox>
                <span style="color: gray">(默认为药品基础信息的“通用名”)</span>
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                卖点：
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextBox2" runat="server" Width="439px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                广告语：
            </td>
            <td colspan="3">
                <asp:TextBox ID="TextBox3" runat="server" Width="438px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                是否控销
            </td>
            <td class="style9" colspan="3">
                <asp:DropDownList ID="DropDowndrug_sensitive1" runat="server">
                    <asp:ListItem Text="否" Value="False" />
                    <asp:ListItem Text="是" Value="True" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                销售方式：
            </td>
            <td colspan="4">
                <asp:DropDownList ID="DropDownSellType" runat="server">
                    <asp:ListItem Text="最小包装" Value="1" />
                    <asp:ListItem Text="中包装" Value="2" />
                    <asp:ListItem Text="整件" Value="3" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5" bgcolor="#E6E6E6">
                批发价：
            </td>
            <td class="style9">
                <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>
            </td>
            <td class="style7" bgcolor="#E6E6E6">
                零售价：
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button3" runat="server" CssClass="inputbutton" OnClick="btnSave_Click"
        Text="保  存" />
</asp:Content>
