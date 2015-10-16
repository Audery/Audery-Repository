<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true"
    CodeBehind="MemberIntegralGiftExchange_Comfirm.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralGiftExchange_Comfirm"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <style type="text/css"> .msgNormal{ width:auto;} .form_table_input_info { width:180px;}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    确认待处理的兑换礼品申请
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="确  认" Visible="false" />
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="MemberIntegralGift.aspx"
        Width="65px" Height="24px" Style="vertical-align: bottom;">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        <asp:HiddenField ID="ReturnUrl" runat="server" /><asp:HiddenField ID="txtId" runat="server" />
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
    <td valign="top">
    <table class="form_table_input" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="form_table_input_info">
                礼品名称：<asp:HiddenField ID="txtGId" runat="server" />
            </td>
            <td>
                <asp:Label ID="txtName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                礼品说明：
            </td>
            <td>
                <asp:Label ID="txtDetail" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                兑换积分：
            </td>
            <td>
                <asp:Label ID="txtIntegral" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                可兑换数量：
            </td>
            <td>
                <asp:Label ID="txtNumber" runat="server" Font-Bold="true"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr id="Tr0" runat="server">
            <td class="form_table_input_info">
                可兑换客户类型：
            </td>
            <td>
                <asp:Label ID="txtMember_Class" runat="server"></asp:Label>
                <span style="display:none">
                <asp:CheckBoxList ID="ckbMember_Class" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">批发客户</asp:ListItem>
                    <asp:ListItem Value="1">OTC拆零客户</asp:ListItem>
                </asp:CheckBoxList>
                </span>
            </td>
            <td>
            </td>
        </tr>
    </table>    
    </td>
    </tr>
    <tr>
    <td valign="top">
    <table class="form_table_input" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="form_table_input_info">
                客户姓名：<asp:HiddenField ID="txtUID" runat="server" />
            </td>
            <td style="width:260px">
                <asp:Label ID="txtTruename" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                电话：
            </td>
            <td>
                <asp:Label ID="txtPhone" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                公司名称：
            </td>
            <td>
                <asp:Label ID="txtCompanyName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                兑换数量：
            </td>
            <td>
                <asp:Label ID="txtGift_Number" runat="server" Font-Bold="true"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                邮寄地址：
            </td>
            <td>
                <asp:Label ID="txtConsigneeAddress" runat="server"></asp:Label>                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                联系人：
            </td>
            <td>
                <asp:Label ID="txtConsigneeName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                联系电话：
            </td>
            <td>
                <asp:Label ID="txtConsigneePhone" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                处理状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Value="0" Text="取消"></asp:ListItem>
                    <asp:ListItem Value="1" Text="待处理"></asp:ListItem>
                    <asp:ListItem Value="2" Text="已处理"></asp:ListItem>
                    <asp:ListItem Value="3" Text="已邮寄"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <div class="msgNormal">请选择你要处理的状态</div>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </td>
    </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="确  认" />
    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="inputbutton" NavigateUrl="MemberIntegralGift.aspx"
        Width="65px" Height="24px" Style="vertical-align: bottom;">取  消</asp:HyperLink>
</asp:Content>
