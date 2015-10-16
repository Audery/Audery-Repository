
<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="picturemanaging.aspx.cs" Inherits="_101shop.admin.v3.accessories.picturemanaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script src="../scripts/public.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    //删除文件
    function DelFile(FilePath)
    { 
       if(confirm('确定要永久删除该文件吗?删除后将不能被恢复!'))
       {
            var param = "Option=delFile&path=" + FilePath;
            var options = 
            { method: 'post',parameters: param,onComplete:
                 function(transport)
                 {
                     var retv = transport.responseText;
                     if(retv=="ok")
                     {
                         window.location.reload();
                     }
                     else if(retv=="no")
                     {
                       alert("对不起，你没有删除权限");
                     }
                     else
                     {
                         alert(retv);
                     }
                 }
            }
        }
        
        new Ajax.Request('picturemanaging.aspx', options);
    }
    

    //获取路径
    function repath()
    {
       var path=document.getElementById("<%=hfpath.ClientID %>").value;
       return path;
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    图片管理 <asp:HyperLink ID="HyperLink2" runat="server"  CssClass="inputbutton" NavigateUrl="javascript:show('ImportFile',document.getElementById('adress'),'选择导入图片地址',500,66);" Width="65px" Height="24px" style="vertical-align:bottom;">导入图片</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
<span id="adress"></span>
<asp:HyperLink ID="hlback" runat="server"  onclick="javascript:window.history.back();">返回上级目录</asp:HyperLink> 当前目录：<asp:Label ID="lbpath" runat="server" Text="Label"></asp:Label><asp:HiddenField
    ID="hfpath" runat="server" />
</asp:Content>
<asp:Content ID="ContentSearch" ContentPlaceHolderID="pagesarch" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
