<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="topsearchesseting_list.aspx.cs" Inherits="_101shop.admin.v3.accessories.topsearchesseting_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
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
                     if(retv=="no")
                     {
                      alert("对不起，你没有权限删除！");
                     }
                     else
                     {
                     window.location.reload();
                     }
                     
                 }
            }
        }
        else
        {
            return false;
        }
        new Ajax.Request('topsearchesseting_list.aspx', options);
    }
    
    function Sort(id)
    {
        var sort=$("sort"+id).value;
        var num="^[0-9]*[1-9][0-9]*$";
        if(sort.search(num)!=0)
        {
          return false;
        }
        var param = "Option=sort&sort="+sort+"&id=" + id;
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
                   alert("对不起，你没有编辑权限");
                 }
             }
        }
        new Ajax.Request('topsearchesseting_list.aspx', options);
    }
    </script>
    <script src="../scripts/images.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">热门搜索设置
<asp:HyperLink ID="HyperLink1"  NavigateUrl="~/admin/accessories/topsearchesseting_edit.aspx" runat="server"  CssClass="inputbutton" Width="65px" Height="23px">添加热门</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<div id="Layer1" style="display:none;position:absolute;z-index:1;"></div>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
