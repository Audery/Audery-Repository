<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="articlechannel_list.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.articlechannel_list" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="../../scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Del(id)
    { 
       if(confirm('确定要永久删除该信息吗?删除后将不能被恢复!'))
       {
            var param = "Option=del&id=" + id;
            var options = 
            { method: 'post',parameters: param,onComplete:
                 function(transport)
                 {
                     var retv = transport.responseText;
                     if(retv=="ok")
                     {
                         window.location.href=window.location.href;
                     }
                     else if(retv=="no")
                     {
                      alert("对不起，你没有删除权限！");
                     }
                     else if(retv=="haschild")
                     {
                        alert("该频道下还有子频道，请先删除子频道");
                     }
                     else if(retv=="hasarticel")
                     {  
                        alert("该频道下还有文章，请先删除文章");
                     }
                 }
            }
        }
        else
        {
            return false;
        }
        new Ajax.Request('articlechannel_list.aspx', options);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">频道管理
    <asp:HyperLink ID="lnkAddChannel" runat="server"  CssClass="inputbutton" Width="65px" Height="24px" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">添加频道</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server"> 
    <asp:Literal ID="link" runat="server"></asp:Literal> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="workspace" runat="server">
    <asp:Label ID="lblView" runat="server" Text="Label"></asp:Label>
</asp:Content>
