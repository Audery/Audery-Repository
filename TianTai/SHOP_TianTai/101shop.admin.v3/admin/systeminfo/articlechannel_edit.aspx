<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="articlechannel_edit.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.articlechannel_edit" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
     <link rel="stylesheet" href="../style/dtree.css" type="text/css" />
      <script type="text/javascript" src="../scripts/dtree.js"></script>
      <script type="text/javascript" src="../scripts/public.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">频道管理
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink1" runat="server"  CssClass="inputbutton" NavigateUrl="articlechannel_list.aspx" Width="65px" Height="24px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
&nbsp;
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="workspace" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td id="TabTitle0" class="titlemouseover">基本信息</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="9" style="border: 1px solid #bac9c6;">
            <!--基本信息-->
            <div>
               <div id="fileLists" class="dtree" style="display:none">
                    <asp:Literal ID="ltlFileList" runat="server"></asp:Literal>
               </div>
                 <table  id="tab0" class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
	                <tr>
		                <td class="form_table_input_info">所属栏目：
                        </td>
		                <td>	    
                           <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>               
                            <asp:HiddenField ID="txtParentId" runat="server" />
                            <asp:HiddenField ID="txtId" runat="server" />
                        </td>
		                <td>
                            <div class="msgNormal">该栏目的上级栏目</div>
                        </td>
	                </tr>
	                <tr>
		                <td class="form_table_input_info">栏目名称：
                        </td>
		                <td>
                            <asp:TextBox ID="txtName" CssClass="long_input" runat="server"></asp:TextBox>
                        </td>
		                <td>
                            <asp:Panel ID="txtNameTip" runat="server">
                            </asp:Panel>
                        </td>
	                </tr>
	                <tr style="display:none;">
		                <td class="form_table_input_info">栏目类别：
                        </td>
		                <td>
                            <asp:RadioButtonList ID="txtShop" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="system">系统</asp:ListItem>
                                <asp:ListItem Value="shop">网店</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
		                <td>
                            <div class="msgNormal">该栏目是系统栏目还是用户网店栏目</div>
                        </td>
	                </tr>
	                <tr style="display:none;">
                   <td class="form_table_input_info">栏目类型：</td>
                   <td>
                       <asp:RadioButtonList ID="ckbType" runat="server" RepeatDirection="Horizontal" 
                           RepeatLayout="Flow">
                           <asp:ListItem Value="1" Selected="True">系统栏目</asp:ListItem>
                           <asp:ListItem Value="2">系统单页</asp:ListItem>
                           <asp:ListItem Value="3">外部链接</asp:ListItem>
                       </asp:RadioButtonList>
                       <br />
                       <div id="div1" style="display:none">
                       <span id='classarea'>链接地址：</span>
                          <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><font color="red">*</font><br /><span id='classtips'>如 <font style="color:Blue">http://www.changehope.com</font> 等</span>
                       </div>
                   </td>
                   <td><div class="msgNormal">该频道是属于本系统内的系统栏目还是只是一个页面的文章，还是只是一个外部的链接链接到外部网站</div></td>
               </tr>
	                <tr style="display:none;">
                   <td class="form_table_input_info">栏目模板：</td>
                   <td>
                        <asp:TextBox ID="txtWebPagePath"  CssClass="long_input" runat="server" on></asp:TextBox>
                        
                   </td>
                   <td><div class="msgNormal">所选择的模板将作为该栏目前台显示的模板。</div></td>
               </tr>
	                <tr style="display:none;">
                   <td class="form_table_input_info">列表页模板：</td>
                   <td>
                       <asp:TextBox ID="txtListPageTemplate" CssClass="long_input" runat="server"></asp:TextBox>
                   </td>
                   <td><div class="msgNormal">所选择的模板将作为该栏目前台显示的模板。</div></td>
               </tr>
               
	                <tr style="display:none;">
                   <td class="form_table_input_info">内容页模板：</td>
                   <td>
                       <asp:TextBox ID="txtContentPageTemplate" CssClass="long_input" runat="server"></asp:TextBox>
                   </td>
                   <td><div class="msgNormal">所选择的模板将作为该栏目前台显示的模板。</div></td>
               </tr>
	                <tr style="display:none;">
                   <td class="form_table_input_info">打开方式：</td>
                   <td>
                       <asp:RadioButtonList ID="ckbTarget" runat="server" RepeatDirection="Horizontal" 
                           RepeatLayout="Flow">
                           <asp:ListItem Value="1">新页面</asp:ListItem>
                           <asp:ListItem Value="2" Selected="True">本页面</asp:ListItem>
                           <asp:ListItem Value="3">父页面</asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
                   <td><div class="msgNormal">打开方式是指在打开该栏目的时候是打开新页面还是在本页面里显示</div></td>
               </tr>
                </table>
     </div>
     
    <!--其它信息-->
    <div >
         <table id="tab1" class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
	                <tr style="display:none;">
                 <td class="form_table_input_info">项目名称：</td>
                 <td><asp:TextBox id="txtProjectName" runat="server" CssClass="long_input" ></asp:TextBox></td>
                 <td><asp:Panel id="txtProjectNameTip" runat="server"></asp:Panel></td>
            </tr>
	                <tr style="display:none;">
               <td class="form_table_input_info">项目单位：</td>
               <td><asp:TextBox id="txtProjectUtil" runat="server" CssClass="long_input" ></asp:TextBox></td>
               <td><asp:Panel id="txtProjectUtilTip" runat="server"></asp:Panel></td>
            </tr>
            <tr>
                <td class="form_table_input_info">栏目描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div class="msgNormal">对于该频道的描述</div>
                </td>
             </tr>
	                <tr style="display:none;">
               <td class="form_table_input_info">Mete关键字：</td>
               <td><asp:TextBox id="txtMeteKey" runat="server" CssClass="long_input" ></asp:TextBox></td>
               <td><asp:Panel id="txtMeteKeyTip" runat="server"></asp:Panel></td>
           </tr>
	                <tr style="display:none;">
               <td class="form_table_input_info">Mete描述：</td>
               <td><asp:TextBox id="txtMeteDescription" runat="server" CssClass="long_input" 
                       TextMode="MultiLine" ></asp:TextBox></td>
               <td><asp:Panel id="txtMeteDescriptionTip" runat="server"></asp:Panel></td>
           </tr>
	                <tr style="display:none;">
               <td class="form_table_input_info">权限：</td>
               <td>
                   <asp:CheckBoxList ID="ckbPower" runat="server" RepeatColumns="3" 
                       RepeatDirection="Horizontal" RepeatLayout="Flow">
                   </asp:CheckBoxList>
               </td>
               <td><div class="msgNormal">哪些用户可以浏览该栏目</div></td>
           </tr>
         </table>
    </div>
    </td>
    </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="保  存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink2" runat="server"  CssClass="inputbutton" NavigateUrl="articlechannel_list.aspx" Width="65px" Height="24px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回列表</asp:HyperLink>
</asp:Content>