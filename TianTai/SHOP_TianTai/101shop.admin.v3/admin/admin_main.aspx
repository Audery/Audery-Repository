<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_main.aspx.cs" Inherits="_101shop.admin.v3.admin_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="style/mune.css" type="text/css" />
    <title>管理导航区域</title>
    <style type="text/css">
.mdear .bakhis {
    float: right;
    height: 30px;
    padding: 0 0 0 2px;
    position: relative;
    width: 105px;
    z-index: 10;
    top: -4px;
    right: 1px;
}
.mdear .bakhis .his_one {
    background: url("images/history.gif") no-repeat scroll 0 0 transparent;
    float: right;
    width: 71px;
}
.mdear .bakhis .his_con {
    background: url("images/blank.gif") no-repeat scroll 0 0 transparent;
    display: block;
    height: 30px;
    overflow: hidden;
    text-indent: -9999em;
    width: 71px;
}
.mdear .bakhis .show_bakhis {
    background: url("images/history.gif") no-repeat scroll -71px 0 transparent;
    float: right;
    height: 30px;
    overflow: hidden;
    text-indent: -9999px;
    width: 30px;
}
.mdear .bakhis .show_bak {
    background: url("images/bg_pos.gif") repeat-y scroll 28px 0 #F0F0F0;
    border: 1px solid #979797;
    padding: 6px 0;
    position: absolute;
    right: 0;
    top: 34px;
    width: 188px;
    z-index: 10000;
}
.mdear .bakhis .show_bak a {
    color: #333333;
    display: inline-block;
    font-size: 12px;
    height: 18px;
    line-height: 18px;
    margin: 2px 0;
    overflow: hidden;
    padding: 2px 2px 2px 34px;
    width: 153px;
}
.mdear .bakhis .show_bak a.cur {
    background: url("images/his_tobg.gif") no-repeat scroll 2px -28px transparent;
    font-weight: bold;
}
.mdear .bakhis .show_bak a:hover {
    background: url("images/his_tobg.gif") no-repeat scroll 2px 0 transparent;
}
.mdear .bakhis .show_bak a.cur:hover {
    background-position: 2px -28px;
}
.mdear .bakhis .cout {
    background-position: 0 -30px;
}
.mdear .bakhis .hover_show {
    background-position: -71px -30px;
}
.mdear .disabled .his_one {
    background-position: 0 -90px;
}
.mdear .disabled .show_bakhis {
    background-position: -71px -90px;
}
.mdear {
}
    </style>
</head>
<script type="text/javascript" src="scripts/title.js"></script>
<body>
    <div id="nav">
        <ul>
<%if (BigMenu.Contains("快捷导航") || AdminPowerType == "all") { %><li id="man_nav_1" onclick="list_sub_nav(id,'快捷导航')" class="bg_image_onclick">快捷导航</li><%} %>
<%if (BigMenu.Contains("用户管理") || AdminPowerType == "all") { %><li id="man_nav_4" onclick="list_sub_nav(id,'用户管理')" class="bg_image">用户管理</li><%} %>
<%if (BigMenu.Contains("订单管理") || AdminPowerType == "all") { %><li id="man_nav_3" onclick="list_sub_nav(id,'订单管理')" class="bg_image">订单管理</li><%} %>
<%if (BigMenu.Contains("商品管理") || AdminPowerType == "all") { %><li id="man_nav_5" onclick="list_sub_nav(id,'商品管理')" class="bg_image">商品管理</li><%} %>
<%if (BigMenu.Contains("资讯频道") || AdminPowerType == "all") { %><li id="man_nav_6" onclick="list_sub_nav(id,'资讯频道')" class="bg_image">资讯频道</li><%} %>
<%if (BigMenu.Contains("系统设置") || AdminPowerType == "all") { %><li id="man_nav_7" onclick="list_sub_nav(id,'系统设置')" class="bg_image">系统设置</li><%} %>
<%if (BigMenu.Contains("综合管理") || AdminPowerType == "all") { %><li id="man_nav_9" onclick="list_sub_nav(id,'综合管理')" class="bg_image">综合管理</li><%} %>
<%if (BigMenu.Contains("广告管理") || AdminPowerType == "all") { %><li id="man_nav_10" onclick="list_sub_nav(id,'广告管理')" class="bg_image">广告管理</li><%} %>
        </ul>
    </div>
    <div id="sub_info" class="mdear">
        <span style="float: left;">&nbsp;&nbsp;<img src="images/listen.gif" alt="> " />&nbsp;<span id="show_text"></span></span>
        <div id="back_history" class="bakhis disabled">
			<a id="show_bakhis" class="show_bakhis" href="javascript:void(0)">查看</a>
			<div class="his_one">
				<a id="his_con" class="his_con" href="javascript:void(0)" title=""></a>
			</div>
			<div style="display:none" id="li_list" class="show_bak"></div>
		</div>
    </div>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" 
        ShowCheckBoxes="All" BorderStyle="None" 
        ExpandDepth="0" ImageSet="Arrows" ondatabound="TreeView1_DataBound" Width="100%" >
        <DataBindings>
            <asp:TreeNodeBinding TextField="Text" ValueField="Value" />
        </DataBindings>
    </asp:TreeView>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Admin/xml/power_list.xml" XPath="//item"></asp:XmlDataSource>
<script type="text/javascript">
    var BackHistoryIni = ''; if (window.top != window.self && window.top.frames.length && window.top.frames['manFrame']) BackHistoryIni = window.top.frames['manFrame'].window.location.href;
    if (BackHistoryIni == '' || BackHistoryIni.indexOf('/admin/') == -1) BackHistoryIni = 'systeminfo/site_sysinfo.aspx';
    function BackHistory() {
        if (window.top != window.self && window.top.frames.length && window.top.frames['manFrame']) {
            var back_history = document.getElementById('back_history'), his_con = document.getElementById('his_con'), mywindow = window.top.frames['manFrame'].window;
            if (mywindow.history.length) {
                if (mywindow.location.href.indexOf('/admin/') != -1 && mywindow.location.href.indexOf(BackHistoryIni) == -1) {
                    back_history.className = back_history.className.replace(' disabled', '');
                } else {
                    if (back_history.className.indexOf('disabled') == -1) back_history.className += ' disabled';
                }
                //his_con
                if (his_con.onclick == undefined || his_con.onclick == null) {
                    his_con.onclick = function (e) {
                        if (mywindow.location.href.indexOf('/admin/') != -1 && mywindow.location.href.indexOf(BackHistoryIni) == -1) {
                            back_history.className = back_history.className.replace(' disabled', '');
                            mywindow.history.go(-1);
                        } else {
                            if (back_history.className.indexOf('disabled') == -1) back_history.className += ' disabled';
                        }
                    };
                    his_con.onmouseover = function (e) {
                        if (mywindow.location.href.indexOf('/admin/') != -1 && mywindow.location.href.indexOf(BackHistoryIni) == -1) {
                            if (his_con.parentNode.className.indexOf('cout') == -1) his_con.parentNode.className += ' cout';
                        }
                    };
                    his_con.onmouseout = function (e) {
                        his_con.parentNode.className = his_con.parentNode.className.replace(' cout', '');
                    };
                }
                //show_bakhis
            } else {
                if (back_history.className.indexOf('disabled') == -1) back_history.className += ' disabled';
            }
        } else {
            window.top = 'index.aspx';
        }
    }
    setInterval(BackHistory, 200);
</script>
</body>
</html>