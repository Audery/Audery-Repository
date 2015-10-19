<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_top.aspx.cs" Inherits="_101shop.admin.v3.admin_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="style/mune.css" type="text/css" />
    <style type="text/css">
        .admin_top0
        {
            width: 100%;
            height: 50px;
            background: url(images/admin_top_bj.jpg) repeat-x;
        }
        .admin_top
        {
            width: 100%;
            height: 50px;
            background: url(images/admin_top_bj.jpg) repeat-x;
        }
        .admin_top_logo
        {
            width: 190px;
            padding: 3px;
            float: left;
        }
        .admin_top_menu
        {
            float: right;
            overflow: hidden;
        }
        .admin_top02
        {
            float: left;
            margin-top: 2px;
        }
        .admin_top02 ul
        {
            float: right;
            background: url(images/admin_top_char2.jpg) repeat-x;
        }
        .admin_top02 li
        {
            float: left;
            color: #000;
            height: 29px;
            background: url(images/admin_top_char2.jpg) repeat-x;
        }
        .admin_top02 li a
        {
            display: block;
            text-align: center;
            line-height: 20px;
            font-size: 14px;
            color: #000;
        }
        .admin_top02 li a:hover
        {
            display: block;
            text-align: center;
            line-height: 20px;
            font-size: 14px;
            color: red;
        }
    </style>
    <script type="text/javascript" src="/admin/scripts/jquery.js"></script>
    <script type="text/javascript">
        
    </script>
</head>
<body style="margin-right: 0;">
    <form runat="server">
    <div class="admin_top0">
        <div class="admin_top">
            <ul class="admin_top_logo">
                <img src="images/admin_top_logo.jpg" /></ul>
            <div style="float: left; margin-left: 15px; height: auto; max-width: 716px; display: none;
                overflow：hide" class="priceboard">
            </div>
            <%--<a href="member/BuyerLib.aspx?State=1" target="manFrame" runat="server" id="divUnReviewed" style="float: left; padding-left:820px; font-size:14px;">当前未审核用户数：<span style="color:#bd0000" id="notReviewedMemberCount" runat="server"></span></a>--%>
            <ul class="admin_top_menu">
                <ul class="admin_top02">
                    <li>
                        <img src="images/admin_top_char1.jpg" /></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">【清除缓存】</asp:LinkButton>
                    </li>
                </ul>
            </ul>
            <ul class="admin_top_menu">
                <li id="showCountMsg" style="color: Red; font-weight: bold; padding-right: 0px;">
                </li>
                <li id="showCountMemberMsg" style="color: Red; font-weight: bold; padding-right: 0px;">
                </li>
            </ul>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var notReviewedMemberCount = document.getElementById("notReviewedMemberCount");
        if (notReviewedMemberCount != undefined) {
            if (notReviewedMemberCount.innerHTML != 0) {
                var intervalID = setInterval(function () { setStyle(); }, 600);

                function setStyle() {
                    var divUnReviewed = document.getElementById("divUnReviewed");
                    if (divUnReviewed.style.fontWeight == "") {
                        divUnReviewed.style.fontWeight = "bold";
                    }
                    else {
                        divUnReviewed.style.fontWeight = "";
                    }
                }
            }
        }
    </script>
</body>
</html>
