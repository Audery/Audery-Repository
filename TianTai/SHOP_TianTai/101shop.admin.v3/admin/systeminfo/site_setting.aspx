<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="site_setting.aspx.cs" EnableViewState="false" Inherits="_101shop.admin.v3.systeminfo.site_setting" Title="" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script>
    function mouseover()
    {
       document.getElementById("<%=butSave.ClientID %>").className="inputbutton_a";
    }
    function mouseover_a()
    {
       document.getElementById("<%=butSave.ClientID %>").className="inputbutton";
    }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">网站参数设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">网站相关的参数配置 
    <asp:Button ID="butSave" runat="server" CssClass="inputbutton" onclick="btnSubmit_Click" Text="保存设置" />
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
    
    
    <table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
	    <tr>
		    <td class="form_table_input_info">网站名称：<asp:HiddenField ID="txtId" runat="server" />
            </td>
		    <td>
                <asp:TextBox ID="txtWebSiteName" CssClass="long_input"  runat="server"></asp:TextBox>
            </td>
		    <td>
		        <asp:Panel id="txtWebSiteNameTip" runat="server"></asp:Panel>
		    </td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">网站标题：</td>
		    <td>
                <asp:TextBox ID="txtWebSiteTitle"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtWebSiteTitleTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">联系电话：</td>
		    <td>
                <asp:TextBox ID="txtTel"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtTelTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">联系传真：</td>
		    <td>
                <asp:TextBox ID="txtFax"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtFaxTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">电子邮件：</td>
		    <td>
                <asp:TextBox ID="txtEmail"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtEmailTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">HTTP地址：</td>
		    <td>
                <asp:TextBox ID="txtWebSiteDomain"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtWebSiteDomainTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">安装路径：</td>
		    <td>
                <asp:TextBox ID="txtWebSitePath"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtWebSitePathTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">网站模板根目录：</td>
		    <td>
                <asp:TextBox ID="txtTemplatePath"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtTemplatePathTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">LOGO地址：</td>
		    <td>
                <asp:TextBox ID="txtLogoPath"  CssClass="long_input" runat="server"></asp:TextBox>
            </td>
		    <td><asp:Panel id="txtLogoPathTip" runat="server"></asp:Panel><a href=" http://www.sosoyy.com/" target="_blank"> Sosoyy.com网站banner在线生成</a>
        </td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">Banner地址：</td>
		    <td><asp:TextBox ID="txtBannerPath"  CssClass="long_input" runat="server"></asp:TextBox></td>
		    <td><asp:Panel id="txtBannerPathTip" runat="server"></asp:Panel><a href=" http://www.sosoyy.com/" target="_blank"> Sosoyy.com网站banner在线生成</a>
        </td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">统计代码：</td>
		    <td><asp:TextBox TextMode="MultiLine" ID="txtStatisticsCode"  CssClass="long_input" runat="server"></asp:TextBox></td>
		    <td><asp:Panel id="txtStatisticsCodeTip" runat="server"></asp:Panel>
        </td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">整站上传文件大小：</td>
		    <td><asp:TextBox ID="txtFileSize"  CssClass="long_input" runat="server" 
                    Width="46px"></asp:TextBox>&nbsp;KB</td>
		    <td><asp:Panel id="txtFileSizeTip" runat="server"></asp:Panel>
        </td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">网站关键词：</td>
		    <td><asp:TextBox ID="txtMeteKey"  CssClass="long_input" runat="server"></asp:TextBox></td>
		    <td><asp:Panel id="txtMeteKeyTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">网站描述：</td>
		    <td><asp:TextBox ID="txtMeteInfo" runat="server" TextMode="MultiLine"></asp:TextBox></td>
		    <td><asp:Panel id="txtMeteInfoTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">是否关闭网站：</td>
		    <td>
                <asp:CheckBox ID="ckbCloseWebSite" runat="server" Text="关闭网站" />
            </td>
		    <td><div id="ddlCloseWebSiteTip" class="msgNormal">是否关闭整个网站，关闭网站后，互联网用户将无法访问您的网站，只显示网站的关闭原因。</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">关闭网站描述：</td>
		    <td><asp:TextBox ID="txtCloseWebSiteInfo" runat="server" TextMode="MultiLine"></asp:TextBox></td>
		    <td><asp:Panel id="txtCloseWebSiteInfoTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">是否关闭论坛：</td>
		    <td dir="ltr" valign="middle">
                <asp:CheckBox ID="ckbCloseBBS" runat="server" Text="关闭论坛" />
            </td>
		    <td><div id="ddlCloseBBSTip" class="msgNormal">是否关闭论坛,关闭论坛将只能访问网站而不能访问论坛</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">关闭论坛描述：</td>
		    <td><asp:TextBox ID="txtCloseBBSInfo" runat="server" TextMode="MultiLine"></asp:TextBox></td>
		    <td><asp:Panel id="txtCloseBBSInfoTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">版权信息：</td>
		    <td><asp:TextBox ID="txtCopyRight" runat="server" TextMode="MultiLine"></asp:TextBox></td>
		    <td><asp:Panel id="txtCopyRightTip" runat="server"></asp:Panel></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">用户协议：</td>
		    <td colspan="2">
                <CE:Editor ID="txtUsersAgreement" runat="server" Height="410" CustomCulture="zh-CN"></CE:Editor>
            </td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">网站访问模式：</td>
		    <td>
                <asp:RadioButtonList ID="ddlPublicMethod" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="0">静态</asp:ListItem>
                    <asp:ListItem Value="1">动态</asp:ListItem>
                </asp:RadioButtonList>
            </td>
		    <td><div id="ddlPublicMethodTip" class="msgNormal">访问网站的模式：静态是生成静态文件，动态是不生成静态文件</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">伪静态扩展名：</td>
		    <td>
                <asp:DropDownList ID="ddlStaticPageFileType" runat="server">
                    <asp:ListItem>.htm</asp:ListItem>
                    <asp:ListItem Value=".html">.html</asp:ListItem>
                    <asp:ListItem Value=".shtm"></asp:ListItem>
                    <asp:ListItem>.shtml</asp:ListItem>
                    <asp:ListItem Value=".aspx">.aspx</asp:ListItem>
                </asp:DropDownList>
            </td>
		    <td><div id="ddlStaticPageFileTypeTip" class="msgNormal">采用伪静态访问模式后，访问文件的扩展名 </div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">伪静态规则编辑：</td>
		    <td><a href="pseudo_static_list.aspx">编辑伪静态规则</a></td>
		    <td><div id="div17" class="msgNormal">请编辑伪静态规则</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">是否开启店铺：</td>
		    <td>
                <asp:RadioButtonList ID="ddlCloseShop" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    
                </asp:RadioButtonList>
            </td>
		    <td><div id="ddlCloseShopTip" class="msgNormal">您的用户是否可以开启自己的店铺</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">是否开启分站：</td>
		    <td>
                <asp:RadioButtonList ID="ddlCloseStation" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    
                </asp:RadioButtonList>
            </td>
		    <td><div id="ddlCloseStationTip" class="msgNormal">是不是开启分站功能，比如地方站</div></td>
	    </tr>
	    <tr style="display:none;">
		    <td class="form_table_input_info">后台登陆方式：</td>
		    <td>
                <asp:RadioButtonList ID="ddlLoginMothod" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0" Selected="True">Session</asp:ListItem>
                    <asp:ListItem Value="1">Cookies</asp:ListItem>
                </asp:RadioButtonList>
            </td>
		    <td><div id="ddlLoginMothodTip" class="msgNormal">后台的登陆方式，是指将用户的信息存储在Session中还是Cookies中</div></td>
	    </tr>
	    
    </table>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="Button1" runat="server" CssClass="inputbutton" onclick="btnSubmit_Click" Text="保存设置" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
</asp:Content>
