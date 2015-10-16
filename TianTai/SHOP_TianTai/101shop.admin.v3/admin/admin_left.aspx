<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_left.aspx.cs" Inherits="_101shop.admin.v3.admin_left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link rel="stylesheet" href="style/mune.css" type="text/css" />
<title>左侧导航栏</title>
<script type="text/javascript"><%=script %></script>
<script type="text/javascript" src="scripts/mune.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div id="left_content">
     <div id="user_info">
        欢迎您，
        <strong><asp:Literal ID="ltlAdminUserName" runat="server"></asp:Literal></strong>
        <br />[<a href="member/admin_edit.aspx?adminid=<%=AdminId %>&edit=pwd" target="manFrame">修改密码</a>]&nbsp;&nbsp;[<a href="admin_logout.aspx" target="_parent">退出</a>]</div>
	 <div id="main_nav">
	     <div id="left_main_nav"></div>
		 <div id="right_main_nav"></div>
	 </div>
    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" 
        ShowCheckBoxes="All" BorderStyle="None" 
        ExpandDepth="0" ImageSet="Arrows" ondatabound="TreeView1_DataBound" Width="100%" >
        <DataBindings>
            <asp:TreeNodeBinding TextField="Text" ValueField="Value" />
        </DataBindings>
    </asp:TreeView>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Admin/xml/power_list.xml" XPath="//item"></asp:XmlDataSource>
</div>
</form>
</body>
</html>