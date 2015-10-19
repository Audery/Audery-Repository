<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/admin_page.master" CodeBehind="hailhellowlink_list.aspx.cs" Inherits="_101shop.admin.v3.accessories.hailhellowlink_list" %>

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
                     if(retv=="ok")
                     {
                         window.location.href=window.location.href;
                     }
                     else if(retv=="no")
                     {
                      alert("对不起，你没有删除权限！");
                     }
                 }
            }
        }
        else
        {
            return false;
        }
        new Ajax.Request('hailhellowlink_list.aspx', options);
    }
    
    function SetLeavel(id)
    {
        var leavelid=$("leavel"+id).value;
        var num="^[0-9]*[1-9][0-9]*$";
        if(leavelid.search(num)!=0)
        {
          return false;
        }
        var param = "Option=leavel&leavelID="+leavelid+"&id=" + id;
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
                  alert("对不起，你没有编辑权限！");
                 }
             }
        }
        new Ajax.Request('hailhellowlink_list.aspx', options);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
友情链接管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="ContentSearch" ContentPlaceHolderID="pagesarch" runat="server">
会员名称：  
    <asp:TextBox ID="w_l_sitename"  runat="server"></asp:TextBox>
    <asp:Button ID="butSearch" runat="server" CssClass="inputbutton" onclick="lbtnSearch_Click" Text="查  询" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<asp:Literal ID="lblList" runat="server"></asp:Literal>
</asp:Content>
