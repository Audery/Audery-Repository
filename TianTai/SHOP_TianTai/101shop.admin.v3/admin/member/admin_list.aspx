<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="admin_list.aspx.cs" Inherits="_101shop.admin.v3.member.admin_list" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript">        var aurl = "admin_edit.aspx?is_ajax=1";</script>
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <script type="text/javascript">
        function GetStat(stat)
        {
            if(stat=="1")
            {
                document.write("是");
            }else
            {
                document.write("否");
            }
        }
        function OtherAction(Str) {
            var idStr = Str.split("—");
            var adminid = idStr[0], OfficePhone = idStr[1], HomePhone = idStr[2], MobilePhone = idStr[3], LoginAuthenticationOfficePhone = idStr[4], QQ = idStr[5];
            //document.write('<label style="display:block;clear:both;" title="用于管理人员登陆验证的电话"><font color="#669">登陆验证电话：</font><span onmouseover="listTable.over(this)" onmouseout="listTable.out(this)" onclick="listTable.edit(this, \'edit:yxs_administrators:LoginAuthenticationOfficePhone:adminid\', \'' + adminid + '\')">' + LoginAuthenticationOfficePhone + '</span></label>');
            document.write('<label style="display:block;clear:both;" title="管理人员的办公电话(用于本人的公司业务的联系方式,或者作为被管理人员登陆验证的电话)"><font color="#669">公司办公电话：</font><span onmouseover="listTable.over(this)" onmouseout="listTable.out(this)" onclick="listTable.edit(this, \'edit:yxs_administrators:OfficePhone:adminid\', \'' + adminid + '\')">' + OfficePhone + '</span></label>');
            document.write('<label style="display:block;clear:both;" title="管理人员的QQ(用于本人的公司业务的联系方式)"><font color="#669">联系QQ：</font><span onmouseover="listTable.over(this)" onmouseout="listTable.out(this)" onclick="listTable.edit(this, \'edit:yxs_administrators:QQ:adminid\', \'' + adminid + '\')">' + QQ + '</span></label>');
        }
        jQuery(document).ready(function () {
            jQuery(".datatable span").each(function (i) {
                var val = jQuery.trim(jQuery(this).html());
                if (val == '' || val == 'N/A' || val == '&nbsp;&nbsp;') {
                    this.style.backgroundColor = "#EEE";
                }
                if (val == '') jQuery(this).html('&nbsp;&nbsp;');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">管理员管理
    <asp:HyperLink ID="HyperLink2" runat="server"  CssClass="inputbutton" NavigateUrl="admin_edit.aspx" Width="65px" Height="23px">添加管理员</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">管理员信息管理,其中ADMIN帐号已被系统保护，无法删除。

帐号名称：<asp:TextBox ID="w_l_Name" runat="server"></asp:TextBox>
    <asp:Button ID="button1" runat="server" CssClass="inputbutton" onclick="LinkButton1_Click" Text="查  询" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Literal ID="ltlView" runat="server"></asp:Literal>
</asp:Content>
