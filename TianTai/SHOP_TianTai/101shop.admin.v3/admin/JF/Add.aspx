<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="_101shop.admin.v3.admin.JF.Add" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link rel="stylesheet" href="/admin/style/admin1.css" type="text/css" />
    <link href="/admin/style/validator.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <script src="../scripts/validate.js" type="text/javascript"></script>
    <script src="/filehandle/LocationJson.ashx?f=jsonp" type="text/javascript"></script>
    <script src="/scripts/jquery/YLChinaArea/YlChinaArea.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
<%--    <link href="/admin/Goods/scripts/jquery/weebox/stylesheets/weebox.css" id="弹窗0" rel="stylesheet"
        type="text/css" />
    <script src="/admin/Goods/scripts/jquery/weebox/scripts/weebox.js" id="Script1" type="text/javascript"></script>--%>

    <style type="text/css">
        td
        {
            font-size: 12px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            padding: 1px 1px 4px 4px;
            height: 22px;
        }
        div
        {
            font-size: 12px;
        }
        .textOverFlow
        {
            display: block; /*内联对象需加*/
            width: 150px;
            word-break: keep-all; /* 不换行 */
            white-space: nowrap; /* 不换行 */
            overflow: hidden; /* 内容超出宽度时隐藏超出部分的内容 */
            text-overflow: ellipsis; /* 当对象内文本溢出时显示省略标记(...) ；需与overflow:hidden;一起使用。*/
        }
    </style>
</head>
<body onload="if(typeof(InitForm)=='function')InitForm();">
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            添加商品
            <asp:Button ID="Button1" runat="server" CssClass="inputbutton" Text="添 加" OnClick="Button1_Click"
                onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="Display.aspx"
                Width="53px" Height="20px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回</asp:HyperLink>
        </div>
    </div>
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        <asp:HiddenField ID="ReturnUrl" runat="server" />
    </asp:Panel>
    <div>
        <table class="datatable" cellspacing="0" rules="all" border="1" id="tablist" style="border-collapse: collapse;
            width: 100%">
            <tr>
                <td class="form_table_input_info">
                    商品名称：<asp:HiddenField ID="txtId" runat="server" />
                </td>
                <td>
                    
                   <cc1:TextBoxManage ID="txtName" runat="server" warning="必填" error="必填" tip="请填写商品名称"
                        validatetype="isnumber" Width="250" CanBeNull="必填" MaxLength="50"></cc1:TextBoxManage>
                  
                </td>
                <td>
                     <div class="tip" id="txtNameTip">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="form_table_input_info">
                    商品积分：<asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td>
                    
                   <cc1:TextBoxManage ID="jf" runat="server" warning="必填" error="必填" tip="请填写商品积分"
                        Width="250"   validatetype="isnull" CanBeNull="必填" MaxLength="50"></cc1:TextBoxManage>
                  
                </td>
                <td>
                     <div class="tip" id="jfTip">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="form_table_input_info">
                    商品属性：<asp:HiddenField ID="HiddenField2" runat="server" />
                </td>
                <td>
                   <cc1:TextBoxManage ID="pt" runat="server" warning="可为空" error="可为空" tip="请填写商品属性（豪礼或空）"
                        Width="250"  validatetype="isnull" CanBeNull="可为空" MaxLength="50"></cc1:TextBoxManage>
                </td>
                <td>
                     <div class="tip" id="ptTip">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="form_table_input_info">
                    商品图片：<asp:HiddenField ID="HiddenField3" runat="server" />
                </td>
                <td>
               
                    <asp:FileUpload ID="FileUpload1" runat="server" warning="必填" error="必填" tip="请填写商品积分"
                        Width="250"  CanBeNull="必填" MaxLength="50" />
                </td>
                <td>
                     <div class="tip" id="FileUpload1Tip">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="page_toolbar" id="Div1" style="margin-top: 5px;">
        <div class="page_info">
            <center>
                <asp:Button ID="Button2" runat="server" CssClass="inputbutton" Text="添 加" OnClick="Button1_Click"
                    />
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="inputbutton" NavigateUrl="Display.aspx"
                    Width="53px" Height="20px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                    onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回</asp:HyperLink>
            </center>
        </div>
    </div>
    </form>
</body>
</html>
