<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="sms_setting.aspx.cs" EnableViewState="false" Inherits="_101shop.admin.v3.systeminfo.sms_setting" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">设置Sms账户及相关信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">设置Sms账户及相关信息的参数
    <asp:Button ID="butSave" runat="server" CssClass="inputbutton" onclick="btnSubmit_Click" Text="保存设置" />
</asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
    
    
    <table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="3">
	    <tr>
		    <td class="form_table_input_info" width="198">企业用户登录名称：
            </td>
		    <td>
                <input type="text" name="Name" class="long_input" value='<%=Application["SmsConfig_Name"] %>' />
            </td>
		    <td>
		        <span id="txtNameTip"></span>
		    </td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">企业用户登录密码：</td>
		    <td>
                <input type="text" name="Pwd" class="long_input" value='<%=Application["SmsConfig_Pwd"] %>' />
            </td>
		    <td><span id="txtPwdTip"></span></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">网址接口：</td>
		    <td>
                <input type="text" name="DstUrl" class="long_input" value='<%=Application["SmsConfig_DstUrl"] %>' />
            </td>
		    <td><span id="txtDstUrlTip"></span></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">返回Sms发送短信结果匹配：</td>
		    <td>
                <input type="text" name="ReturnRegexPattern" class="long_input" value='<%=Application["SmsConfig_ReturnRegexPattern"] %>' />
            </td>
		    <td><span id="txtReturnRegexPatternTip"></span></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">返回Sms查询余额结果匹配：</td>
		    <td>
                <input type="text" name="GetFeeRegexPattern" class="long_input" value='<%=Application["SmsConfig_GetFeeRegexPattern"] %>' />
            </td>
		    <td><span id="txtGetFeeRegexPatternTip"></span></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info">字符编码：</td>
		    <td>
                <input type="text" name="Encoding" class="long_input" value='<%=Application["SmsConfig_Encoding"] %>' />
            </td>
		    <td><span id="txtEncodingTip">提示：一般填写gb2312</span></td>
	    </tr>
	    <tr>
		    <td class="form_table_input_info"><img src="../images/listen.gif" alt="> " />&nbsp;手机短信取回密码：</td>
		    <td>
                <input type="text" name="getpass_tosms" class="long_input" value='<%=Application["SmsConfig_getpass_tosms"] %>' />
            </td>
		    <td><span id="txtgetpass_tosmsTip">提示：如果不开启请填写“0”</span></td>
	    </tr>
	    
    </table>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="Button1" runat="server" CssClass="inputbutton" onclick="btnSubmit_Click" Text="保存设置" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
</asp:Content>
