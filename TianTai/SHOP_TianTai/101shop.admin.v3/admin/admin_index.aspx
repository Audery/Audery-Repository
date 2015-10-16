<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_index.aspx.cs" Inherits="_101shop.admin.v3.admin_index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台管理-<%=WebName %></title>
    <meta name ="keywords" content="<%=WebName %>,电子商务,B/C,电子商城,商城,网店" />
</head>
<frameset rows="50,*,5" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="admin_top.aspx" frameborder="no" scrolling="no" noresize="noresize" name="topFrame" id="topFrame" />
  <frameset cols="199,7,*" frameborder="no" border="0" framespacing="0" name="myFrame" id="myFrame">
    <frame src="admin_left.aspx" frameborder="no" scrolling="no" noresize="noresize" name="leftFrame" id="leftFrame" />
	<frame src="bar.html" frameborder="no" scrolling="no" noresize="noresize" name="midFrame" id="midFrame" />
    <frameset rows="65,*" cols="*" frameborder="no" scrolling="auto" border="0" framespacing="0">
         <frame src="admin_main.aspx" frameborder="no" scrolling="no"  noresize="noresize" name="mainFrame" id="mainFrame" />
         <frame src="systeminfo/site_sysinfo.aspx" scrolling="auto" frameborder="0" name="manFrame" id="manFrame" />
     </frameset>
  </frameset>
</frameset>
</html>