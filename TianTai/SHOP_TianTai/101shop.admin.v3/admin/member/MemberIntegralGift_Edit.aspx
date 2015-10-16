<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true"
    CodeBehind="MemberIntegralGift_Edit.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralGift_Edit"
    Title="" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    编辑积分礼品
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="保  存" />
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="MemberIntegralGift.aspx"
        Width="65px" Height="24px" Style="vertical-align: bottom;">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    积分礼品管理&nbsp;
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        <asp:HiddenField ID="ReturnUrl" runat="server" />
    </asp:Panel>
    <table id="formtbl" runat="server" class="form_table_input" border="0" width="100%"
        cellspacing="0" cellpadding="0">
        <tr>
            <td class="form_table_input_info">
                礼品名称：<asp:HiddenField ID="txtId" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" tip="请填写礼品名称" validatetype="isnull" warning="必填" error="该项为必填项"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtNameTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                兑换说明：
            </td>
            <td>
                <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" tip="请填写兑换说明" validatetype="isnull" warning="必填" error="该项为必填项" Width="420px" Height="180px"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtDetailTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                兑换积分：
            </td>
            <td>
                <asp:TextBox ID="txtIntegral" runat="server" tip="请填写兑换积分" validatetype="isnumber" warning="必填" error="该项为必填项"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtIntegralTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="form_table_input_info">
                可兑换数量：
            </td>
            <td>
                <asp:TextBox ID="txtNumber" runat="server" tip="请填写可兑换数量" validatetype="isnumber" warning="必填" error="该项为必填项"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="txtNumberTip" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr id="Tr0" runat="server">
            <td class="form_table_input_info">
                可兑换客户类型：
            </td>
            <td>
                <asp:CheckBoxList ID="ckbMember_Class" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0">批发/连锁</asp:ListItem>
                    <asp:ListItem Value="1">终端</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td>
                <div class="msgNormal">
                    请至少选择一个
                </div>
            </td>
        </tr>
        <tr id="Tr1" runat="server">
            <td class="form_table_input_info">
                礼品图片：
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <CE:Uploader ID="Uploader1" runat="server" UploadingMsg="正在上载..." InsertText="选择文件"
                ValidateOption-MaxSizeKB="300" ValidateOption-AllowedFileExtensions="jpg" CancelText="取消">
            </CE:Uploader>
            </td>
            <td>
                <div class="msgNormal">
                    请上传礼品图片 大小 220px*220px
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" OnClick="LinkButton1_Click"
        Text="保  存" />
</asp:Content>
