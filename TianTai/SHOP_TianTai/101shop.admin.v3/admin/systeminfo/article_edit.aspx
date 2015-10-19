<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="article_edit.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.article_edit" Title="无标题页" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%--<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript" src="../../scripts/validate.js"></script>
    <script type="text/javascript" src="../scripts/public.js"></script>
     <script src="/ckeditor/ckeditor.js" type="text/javascript"></script>  
<!--注意 适配器js必须放在ckeditor.js后面，否则将会出错，因为适配器需要引用ckeditor.js里面定义的类-->  
<script src="/ckeditor/adapters/jquery.js" type="text/javascript"></script>  
<script src="/ckfinder/ckfinder.js" type="text/javascript"></script>  
    <script type="text/javascript">
    function UpLoadImages(ImagesSoure)
    {
        if(ImagesSoure==1)
        {
            document.getElementById("ctl00_workspace_UpLoadImages").style.display="";
            document.getElementById("ctl00_workspace_ImagesAddress").style.display="none";
        }
        else if(ImagesSoure==2)
        {
            document.getElementById("ctl00_workspace_ImagesAddress").style.display="";
            document.getElementById("ctl00_workspace_UpLoadImages").style.display="none";
        }
        else
        {
            document.getElementById("ctl00_workspace_UpLoadImages").style.display="none";
            document.getElementById("ctl00_workspace_ImagesAddress").style.display="none";
        }
    }
    </script>
    <script type="text/javascript">
        $(function () {
            //利用jQuery加载  
            $('#content').ckeditor();
            //绑定CKFinder到当前编辑器中，第二个参数指定CKFinder的路径  
            CKFinder.setupCKEditor(null, '/ckfinder/');
        });  
</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">资讯管理
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink3" runat="server"  CssClass="inputbutton" NavigateUrl="article_list.aspx" Width="65px" Height="24px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagesarch" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="workspace" runat="server">
<asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
<table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
   <tr>
       <td class="form_table_input_info">所属频道：</td>
       <td><asp:DropDownList ID="ddlChannel" runat="server"></asp:DropDownList>
           <asp:HiddenField ID="txtId" runat="server" />
       </td>
       <td><div class="msgNormal">该文章所属频道</div></td>
   </tr>
   <tr style="display:none">
       <td class="form_table_input_info">地区：</td>
       <td><asp:TextBox id="txtArea" runat="server" CssClass="long_input" ></asp:TextBox>
       <asp:HiddenField ID="txtAreaValue" runat="server" />
       </td>
       <td><asp:Panel id="txtAreaTip" runat="server"></asp:Panel></td>
   </tr>
   <tr>
       <td class="form_table_input_info">标题：</td>
       <td><asp:TextBox id="txtTitle" runat="server" CssClass="long_input" ></asp:TextBox></td>
       <td><asp:Panel id="txtTitleTip" runat="server"></asp:Panel></td>
   </tr>
   <tr>
       <td class="form_table_input_info">副标题：</td>
       <td><asp:TextBox id="txtSubTitle" runat="server" CssClass="long_input" ></asp:TextBox></td>
       <td><asp:Panel id="txtSubTitleTip" runat="server"></asp:Panel></td>
   </tr>
   <tr>
       <td class="form_table_input_info">关键字：</td>
       <td><asp:TextBox id="txtKeyWord" runat="server" CssClass="long_input" ></asp:TextBox></td>
       <td><asp:Panel id="txtKeyWordTip" runat="server"></asp:Panel></td>
   </tr>

   <tr>
       <td class="form_table_input_info">来源：</td>
       <td><asp:TextBox id="txtCopyFrom" runat="server" CssClass="long_input" ></asp:TextBox></td>
       <td><asp:Panel id="txtCopyFromTip" runat="server"></asp:Panel></td>
   </tr>
   <tr>
       <td class="form_table_input_info">转向链接：</td>
       <td><asp:TextBox id="txtLinkUrl" runat="server" Width="140px" CssClass="long_input" ></asp:TextBox>
       <asp:CheckBox ID="cbIsLinkUrl" runat="server" /><span style="color:Red;">使用转向链接</span></td>
       <td><asp:Panel id="txtLinkUrlTip" runat="server"></asp:Panel></td>
   </tr>
   <tr>
       <td class="form_table_input_info">作者：</td>
       <td><asp:TextBox id="txtAuthor" runat="server" CssClass="long_input" ></asp:TextBox></td>
       <td><asp:Panel id="txtAuthorTip" runat="server"></asp:Panel></td>
   </tr>
   
   <tr>
       <td class="form_table_input_info">简介：</td>
       <td><asp:TextBox id="txtIntroduction" runat="server" CssClass="long_input" 
               TextMode="MultiLine" ></asp:TextBox></td>
       <td><asp:Panel id="txtIntroductionTip" runat="server"></asp:Panel></td>
   </tr>
  <tr>
      <td class="form_table_input_info">文章属性：</td>
      <td>
          <asp:CheckBoxList ID="cblArticlePropery" runat="server" RepeatDirection="Horizontal">
              <asp:ListItem Value="1" Text="推荐"></asp:ListItem>
              <asp:ListItem Value="2" Text="头条"></asp:ListItem>
              <asp:ListItem Value="3" Text="热门"></asp:ListItem>
              <asp:ListItem Value="5" Text="置顶"></asp:ListItem>
              <asp:ListItem Value="6" Text="精华"></asp:ListItem>
              <asp:ListItem Value="4" Text="滚动"></asp:ListItem>
              
          </asp:CheckBoxList>
      </td>
      <td></td>
  </tr>
  <tr>
      <td class="form_table_input_info">图片来源:</td>
      <td>
          <asp:RadioButtonList ID="rblImagesSoure" runat="server" RepeatDirection="Horizontal">
               <asp:ListItem Value="0" Selected="True" onclick="UpLoadImages('0')">没有图片</asp:ListItem>
               <asp:ListItem Value="1" onclick="UpLoadImages('1')">本地上传</asp:ListItem>
               <asp:ListItem Value="2" onclick="UpLoadImages('2')">外部来源</asp:ListItem>
          </asp:RadioButtonList>
      </td>
      <td></td>
  </tr>
  <tr id="UpLoadImages" style="display:none" runat="server">
      <td class="form_table_input_info">上传图片：</td>
      <td>
          <asp:FileUpload ID="FileUpload1" runat="server" />
      </td>
      <td></td>
  </tr>
  <tr id="ImagesAddress" style="display:none" runat="server">
      <td class="form_table_input_info">图片地址：</td>
      <td>
          <asp:TextBox ID="txtImagesSoure" runat="server"></asp:TextBox>
      </td>
      <td></td>
  </tr>
   <tr>
       <td class="form_table_input_info">其他属性：</td>
       <td>
            <asp:CheckBox ID="chkIsPageType" Text="是否分页" runat="server" Checked="False" />
            <asp:CheckBox ID="chkState" Text="审核通过" runat="server" Checked="False" />
       </td>
       <td><div class="msgNormal">该资讯的其他信息</div></td>
   </tr>
   <tr>
       <td class="form_table_input_info">内容：</td>
       <td colspan="2">
            <%--<CE:Editor ID="txtContent" runat="server" Height="410" CustomCulture="zh-CN"></CE:Editor>--%>
            <div>
		<CKEditor:CKEditorControl ID="txtContent" runat="server" Height="200" BasePath="~/ckeditor">
		&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. You are using &lt;a href="http://ckeditor.com/"&gt;CKEditor&lt;/a&gt;.&lt;/p&gt;
		</CKEditor:CKEditorControl>
	</div>
       </td>
   </tr>
   
</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink1" runat="server"  CssClass="inputbutton" NavigateUrl="article_list.aspx" Width="65px" Height="24px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回列表</asp:HyperLink>
</asp:Content>