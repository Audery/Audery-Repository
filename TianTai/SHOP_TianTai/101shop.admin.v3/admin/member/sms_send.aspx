<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/admin/admin_page.master" CodeBehind="sms_send.aspx.cs" Inherits="_101shop.admin.v3.member.sms_send" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/admin.css" type="text/css" /> 
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
   <script type="text/javascript" src="/scripts/validate.js"></script>
    <script src="../scripts/public.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">Sms信息发送
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    <asp:LinkButton ID="lbSave" runat="server" onclick="lbSave_Click">发送</asp:LinkButton>
&nbsp;
    <asp:HyperLink ID="returnLink" runat="server" NavigateUrl="BuyerLib.aspx">返回买家列表</asp:HyperLink>
&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../accessories/sms_list.aspx">返回信息列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
       
<table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
       
   <tr>
	   <td class="form_table_input_info" style="height:30px">指定会员组：</td>
	   <td>
           <asp:CheckBoxList ID="cbxlMemberRank" RepeatColumns="3" runat="server">
           <asp:ListItem Value="" Text=""></asp:ListItem>
           <asp:ListItem Value="" Text=""></asp:ListItem>
           <asp:ListItem Value="" Text=""></asp:ListItem>
           <asp:ListItem Value="" Text=""></asp:ListItem>
           </asp:CheckBoxList>
       </td>
       <td>
            <div class="msgNormal">选择后将会给所有您选择的会员组发送该邮件</div>  
       </td>
   </tr>
   
   <tr>
	   <td class="form_table_input_info">指定用户手机：</td>
	   <td>
	       <label>
           <asp:TextBox ID="txtMobile" CssClass="long_input" runat="server"></asp:TextBox></label>
       </td>
       <td>
           <div class="msgNormal">输入时请注意手机号码的格式 多个请用,号分隔</div> 
       </td>
   </tr>
   
   <tr>
	   <td class="form_table_input_info" valign="top">Sms短信内容：</td>
	   <td>
           <asp:TextBox ID="txtMsg" CssClass="long_input" runat="server" Width="300" MaxLength="60" TextMode="MultiLine"></asp:TextBox> 
       </td>
       <td>
           <asp:Panel ID="txtMsgTip" runat="server"></asp:Panel>
       </td>
   </tr>
   
</table>
</asp:Content>
