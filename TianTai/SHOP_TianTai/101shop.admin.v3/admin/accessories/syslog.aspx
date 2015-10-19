<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/admin/admin_page.master"  CodeBehind="syslog.aspx.cs" Inherits="_101shop.admin.v3.accessories.syslog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script src="../scripts/public.js" type="text/javascript"></script>
    <script src="../scripts/images.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function Del(id)
    { 
       var idStr;
       if(id<0)
       {
            idStr = GetAllChecked();
            if(idStr == "")
            {
                alert("您没有选择要删除的信息!");
                return;
            }
       }
       else
       {
           idStr=id
       }
       if(confirm('确定要永久删除该信息吗?删除后将不能被恢复!'))
       {
            var param = "Option=del&id=" + idStr;
            var options =
            { method: 'post', parameters: param, onComplete:
                 function (transport) {
                     var retv = transport.responseText;
                     if (retv == "ok") {
                         location.href = location.href; location.reload();
                     }
                     else if (retv == "no") {
                         alert("对不起，你没有删除权限！");
                     }
                 }
            }
        }
        else
        {
            return false;
        }
        new Ajax.Request('syslog.aspx', options);
    }
    </script>
    <script src="../scripts/images.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">系统操作日志
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
<%--<a href="javascript:void(0)" onclick="Del(-1)">批量删除</a>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagesarch" ID="ContentSearch" runat="server">
    <table border="0" cellspacing="1" cellpadding="1" width="100%">
    <tr>
        <td width="42">关键词:</td>
        <td width="70">
            <asp:TextBox ID="q" runat="server" Width="80"></asp:TextBox>
        </td>
        <%--<td width="55">系统模块:</td>
        <td width="80">
            <asp:TextBox ID="w_l_SubModule" runat="server" Width="80"></asp:TextBox>
        </td>
        <td width="55">事件描述:</td>
        <td width="80">
            <asp:TextBox ID="w_l_Keyword" runat="server" Width="70"></asp:TextBox>
        </td>--%>
        <td width="42" align="right">时间:</td>
        <td width="80">
            <asp:TextBox ID="t" MaxLength="10" Width="70" CssClass="datepicker" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="submitSearch" runat="server" CssClass="inputbutton" onclick="btnSearch_Click" Text="查  询" />
        </td>
    </tr>
</table> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<asp:Literal ID="lblList" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="ContBottom" runat="server" ContentPlaceHolderID="pagebottom">
<asp:HyperLink ID="HyperLink2" runat="server"  CssClass="inputbutton" NavigateUrl="#"  onclick="Del(-1)" Width="65px" Height="23px">批量删除</asp:HyperLink>
</asp:Content>
