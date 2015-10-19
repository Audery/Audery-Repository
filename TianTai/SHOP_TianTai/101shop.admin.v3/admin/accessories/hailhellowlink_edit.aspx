<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/admin/admin_page.master" CodeBehind="hailhellowlink_edit.aspx.cs" Inherits="_101shop.admin.v3.accessories.hailhellowlink_edit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script src="../scripts/public.js" type="text/javascript"></script>
    <script src="../scripts/images.js" type="text/javascript"></script>
        <script type="text/javascript">
        function LogoType(obj)
        {
        var logoimagetype = obj.value;
        
            if(logoimagetype=='1')
            {
                trlogoAdd.style.display="";
                trlogoImage.style.display="none"; 
                
            }
            else
            {
               trlogoImage.style.display=""; 
               trlogoAdd.style.display="none";
               document.getElementByID("txtSiteLogo").Text="";
            }
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">友情链接管理
<asp:Button ID="Button2" runat="server" CssClass="inputbutton" onclick="lbtnSave_Click" Text="保存" />
    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/admin/accessories/hailhellowlink_list.aspx" runat="server"  CssClass="inputbutton" Width="65px" Height="24px" style="vertical-align:bottom;">返回列表</asp:HyperLink>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">

<asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
    <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
</asp:Panel>

<table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td class="form_table_input_info">网站名称：</td>
        <td>
            <asp:TextBox ID="txtSiteName" runat="server" MaxLength="50" CssClass="long_input"></asp:TextBox>
        </td>
        <td>
            <asp:Panel ID="txtSiteNameTip" runat="server"></asp:Panel>
        </td>
    </tr>
    
    <tr>
        <td class="form_table_input_info">网站所在区域：</td>
        <td>
            <asp:TextBox ID="txtArea" runat="server" ReadOnly="true" MaxLength="30"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hfAread" />
        </td>
        <td>
            <div class="msgNormal">请选择网站所在的区域</div>      
        </td>
    </tr>
     
    <tr>
       <td class="form_table_input_info">网站地址：</td>
       <td>
           <asp:TextBox ID="txtSiteUrl" runat="server" MaxLength="50">http://</asp:TextBox>
       </td>
       <td>
           <asp:Panel ID="txtSiteUrlTip" runat="server"></asp:Panel>
       </td>
    </tr>
    
    
    <tr>
        <td class="form_table_input_info">上传类型：</td>
        <td>
          
            <input onclick="LogoType(this)" type=radio  name="radioLogoType" value="1" checked/>地址
             <input onclick="LogoType(this)" type=radio  name="radioLogoType" value="2" />图片
        </td>
        <td>
           <div class="msgNormal">请选择上传Logo的方式</div>      
       </td>
    </tr>
    
    
    <tr id="trlogoAdd">
        <td class="form_table_input_info">网站Logo地址：</td>
        <td>
            <asp:TextBox ID="txtSiteLogo" runat="server" MaxLength="50"></asp:TextBox>
        </td>
        <td>
           <div class="msgNormal">请填入链接网站的Logo地址</div> 
       </td>
    </tr>
    <!--上传图片--->
    <tr id="trlogoImage" style="display:none;">
	                        <td class="form_table_input_info">网站Logo图片：</td>
	                        <td>
	                            <asp:FileUpload ID="fuSmallImages" runat="server" />
                            </td>
	                        <td><div class="msgNormal">上传100px*100px大小的图,前台显示为最佳效果</div></td>
                        </tr>
                        
                        
    
    <tr>
        <td class="form_table_input_info">优先级：</td>
        <td>
            <asp:TextBox ID="txtSiteLevel" runat="server" MaxLength="5"></asp:TextBox>
        </td>
        <td>
           <asp:Panel ID="txtSiteLevelTip" runat="server"></asp:Panel>
       </td>
    </tr>
    
    <tr>
       <td class="form_table_input_info">网站简介：</td>
       <td>
          <asp:TextBox ID="txtSiteContent" runat="server" MaxLength="500"  TextMode="MultiLine"></asp:TextBox>
       </td>
       <td>
           <div class="msgNormal">请输入对该网站的简短介绍</div>      
       </td>
    </tr>
    
    
    <tr>
        <td class="form_table_input_info">链接类型：</td>
        <td>
            <asp:RadioButtonList ID="rablistLinkType" RepeatLayout="Flow" RepeatColumns="2" 
                runat="server">
               <asp:ListItem Value="1" Text="文字" Selected="True"></asp:ListItem>
               <asp:ListItem Value="2" Text="图片"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td>
           <div class="msgNormal">请选择该链接的类型</div>      
       </td>
    </tr>
    
    
    <tr>
        <td class="form_table_input_info">状态：</td>
        <td>
            <asp:RadioButtonList ID="rablistSiteState" RepeatLayout="Flow" RepeatColumns="2"  runat="server">
               <asp:ListItem Value="0" Text="待审核" Selected="True"></asp:ListItem>
               <asp:ListItem Value="1" Text="终审通过"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td>
           <div class="msgNormal">请选择该链接的审核状态</div>      
       </td>
    </tr>
    
    <tr>
        <td class="form_table_input_info">点击数：</td>
        <td>
            <asp:TextBox ID="txtSiteClickCount" runat="server" Text="0" Width="60px" MaxLength="5"></asp:TextBox>
        </td>
        <td>
           <asp:Panel ID="txtSiteClickCountTip" runat="server"></asp:Panel>
       </td>
    </tr>
</table>   
</asp:Content>
<asp:Content ID="ContBottom" runat="server" ContentPlaceHolderID="pagebottom">
<asp:Button ID="Button1" runat="server" CssClass="inputbutton" onclick="lbtnSave_Click" Text="保存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/admin/accessories/hailhellowlink_list.aspx" runat="server"  CssClass="inputbutton" Width="65px" Height="23px">返回列表</asp:HyperLink>
</asp:Content>