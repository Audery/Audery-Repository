<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="article_list.aspx.cs" Inherits="_101shop.admin.v3.systeminfo.article_list" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="../../scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script src="../scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
         function ReSet() {
             location.href = "article_list.aspx";
         }
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
        new Ajax.Request('article_list.aspx', options);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">资讯管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    <asp:Literal ID="ltlLink" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="pagesarch">
文章标题：<asp:TextBox ID="w_l_Title" runat="server"></asp:TextBox> 
所属频道：<asp:DropDownList ID="w_d_Channel" runat="server"></asp:DropDownList>
文章属性：
<asp:DropDownList ID="w_s_Property" runat="server">
<asp:ListItem Value="" Text="请选择"></asp:ListItem>
<asp:ListItem Value="1" Text="推荐新闻"></asp:ListItem>
<asp:ListItem Value="3" Text="头条新闻"></asp:ListItem>
<asp:ListItem Value="5" Text="热门新闻"></asp:ListItem>
<asp:ListItem Value="7" Text="置顶新闻"></asp:ListItem>
<asp:ListItem Value="9" Text="精华新闻"></asp:ListItem>
</asp:DropDownList>
<asp:Button ID="butOk" runat="server" CssClass="inputbutton" onclick="btnOk_Click" Text="查  询" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
<input type="button" id="btnReSet" class="inputbutton" value="重  置" onmouseover="javascript:document.getElementById(this.id).className='inputbutton_a'" onmouseout="javascript:document.getElementById(this.id).className='inputbutton'" onclick="ReSet()" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="workspace" runat="server">
    <asp:Literal ID="ltlview" runat="server" EnableViewState="false"></asp:Literal>  
    <script type="text/javascript">
        $(document).ready(function () {
            $("a").each(function () {
                if ($(this).attr("href").indexOf("read") > 0) {
                    var href = $(this).attr("href");
                    var id = href.substring(href.indexOf("=") + 1, href.length);
                    $(this).attr("href", "http://<%=System.Configuration.ConfigurationManager.AppSettings["web"] %>/article/" + id + ".html");
                    $(this).attr("target", "_blank");
                }
            });
            $(".datatable tr").each(function () {
                $(this).find("td").eq(2).text(decodeURI($(this).find("td").eq(2).text()));

            });
        });
    </script>  
</asp:Content>
