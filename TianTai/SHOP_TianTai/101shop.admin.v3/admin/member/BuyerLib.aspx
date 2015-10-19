<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true"
    CodeBehind="BuyerLib.aspx.cs" Inherits="_101shop.admin.v3.member.BuyerLib" Title="" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/admin1.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script src="../../filehandle/LocationJson.ashx?f=jsonp" type="text/javascript"></script>
    <script src="../../scripts/jquery/YLChinaArea/YlChinaArea.js" type="text/javascript"></script>
    <script type="text/javascript">        jQuery(document).ready(function () { jQuery("#ChinaArea").jChinaArea({ aspnet: true, s1: "<%=province %>", s2: "<%=city %>", s3: "" }) });</script>
    <link href="/scripts/jquery/weebox/stylesheets/weebox.css" id="弹窗css" rel="stylesheet"
        type="text/css" />
    <script src="/scripts/jquery/weebox/scripts/weebox.js" id="弹窗js" type="text/javascript"></script>
    <style type="text/css">
        .page_info
        {
            line-height: 24px;
        }
        .datatable th, .datatable td
        {
            cursor: auto;
        }
        .unclick a, .unclick button
        {
            margin: 0 2px;
            line-height: 150%;
        }
        .datatable tr td
        {
            line-height: 19px;
        }
        DIV.quotes
        {
            float: none;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () { jQuery("table.datatable tr").each(function (i) { jQuery("td span", this).each(function () { var s = jQuery(this); if (s.html() == "") s.html("&nbsp;&nbsp;").css("background-color", "#EEE"); }); }); });
        //获取上级单位
        function GetParentIncName(idStrs) {
            var s = idStrs.split('-'), uid = s[0], idStr = s[1], idStrs = s[2];
            document.write("<span id=\"" + uid + "\" style=\"text-align:center;\"><font color=Gray>未设置</font></span>");
            if (idStr != "") {
                SendAjax("Option=GetParentIncName&id=" + idStr,
                function (res, status) {
                    document.getElementById(uid).innerHTML = "<font color=Black>" + res + "</font>";
                }
                );
            }
        }
        //删除
        function DelAll(id, a) {
            var idStr;
            if (id < 0) {
                idStr = GetAllChecked();
                if (idStr == "") {
                    alert("您没有选择要批量删除的信息!");
                    return;
                }
            }
            else {
                idStr = id;
            }
            if (confirm('确定要永久删除该信息吗?删除后将不能被恢复!')) {
                SendAjax("Option=del&id=" + idStr + "&noBecause=",
                function (res, status) {
                    if (res == "ok") {
                        alert("处理成功！");
                        jQuery(a).parent().parent().remove();
                    } else if (res == "no") {
                        alert("无权限操作！");
                    } else if (res == "noBecauseOrders") {
                        alert('此用户下过订单，不能删除该信息!');
                    } else {
                        alert("处理失败！");
                    }
                }
                );
            }
        }
        //审核
        function State(id, a) {
            var idStr;
            if (jQuery(a).text() == '已审核') return false;
            if (id < 0) {
                idStr = GetAllChecked();
                if (idStr == "") {
                    alert("您没有选择要批量审核的信息!");
                    return;
                }
            }
            else {
                idStr = id;
            }
            if (confirm('确定要审核该信息吗?')) {
                SendAjax("Option=State&id=" + idStr,
                function (res, status) {
                    if (res == "ok") {
                        alert("处理成功！");
                        jQuery(a).html('<font color=green>已审核</font>');
                    } else if (res == "no") {
                        alert("无权限操作！");
                    } else {
                        alert("处理失败！");
                    }
                }
                );
            }
        }

        //查看登陆
        function SeeLoginTimes(id, a) {
            if (jQuery(a).text() == '0') return false;
            if (id <= 0) {
                alert("您没有选择要查看的买家!");
            } else {
                window.__f = jQuery.weeboxs.open('<iframe src="Buyer_LoginTimes.aspx?UID=' + id + '" width="900" height="360" frameborder="no" scrolling="no" noresize="noresize" />', {
                    title: '<span style="padding-left:16px;"><s></s><font color="Green">买家的登陆</font></span>', showCancel: false, okBtnName: '', width: 920, height: 365,
                    onopen: function (f) { f.db.hide(); }, onok: function (f) { }
                });
            }
        }
        //查看权限
        function SeePermissions(id, a) {
            if (id <= 0) {
                alert("您没有选择要查看的买家!");
            } else {
                window.__f = jQuery.weeboxs.open('<iframe src="Buyer_Permissions.aspx?UID=' + id + '" width="500" height="340" frameborder="no" scrolling="no" noresize="noresize" />', {
                    title: '<span style="padding-left:16px;"><s></s><font color="Green">买家的权限</font></span>', showCancel: false, okBtnName: '', width: 520, height: 350,
                    onopen: function (f) { f.db.hide(); }, onok: function (f) { }
                });
            }
        }

        //赠送积分
        function PresentIntegral(id, TrueName, a) {
            if (id <= 0) {
                alert("您没有选择要赠送积分的买家!");
            } else {
                SendAjax("Option=getIntegral&id=" + id,
                function (res, status) {
                    window.__f = jQuery.weeboxs.open('<table cellpadding="0" cellspacing="10" style="text-align: left; border: 0 none;" width="240" height="45"><tr><td align="right">请填写赠送的积分数：</td><td><input type="text" value="0" style="width:50px;padding:3px;" /></td></tr></table>', {
                        title: '<span style="padding-left:16px;"><s></s><font color="Green">赠送积分（当前积分为：' + res + '）</font></span>', showCancel: false, okBtnName: '确定', width: 300, height: 50,
                    onopen: function (f) { }, onok: function (f) {
                        var input = jQuery('input', f.dc), integral = parseInt(input.val());
                        if (isNaN(integral)) {
                            alert("请填写整数!");
                            input[0].focus(); input[0].select();
                        } else {
                            SendAjax("Option=editIntegral&id=" + id + "&TrueName=" + TrueName + "&integral=" + integral,
                            function (res, status) {
                                if (res == "ok") {
                                    alert("处理成功！");
                                } else if (res == "no") {
                                    alert("无权限操作！");
                                } else {
                                    alert("处理失败！");
                                }
                                if (__f) __f.close(); __f = null;
                            });
                        }
                    }
                });
                });
            }
        }

        function ReSet() {
            location.href = "BuyerLib.aspx";
        }
        //时间比较(与当前时间相比)
        function comptimeToNow(beginTime, days) { var s = beginTime.split(' '), d = (s[0].indexOf('/') > 0 ? s[0].split('/') : s[0].split('-')), t = s[1].split(':'); var dt = new Date(), now = new Date(); if (d.length == 3) { var y = parseInt(d[0]), m = parseInt(d[1]) - 1, d = parseInt(d[2]); dt.setYear(y); dt.setMonth(m < 0 ? 0 : m); dt.setDate(d); } if (t.length == 3) { var h = parseInt(t[0]), m = parseInt(t[1]), s = parseInt(t[2]); dt.setHours(h); dt.setMinutes(m); dt.setSeconds(s); } var a = dt - now; if (a < 0) { return -1; } else if (a > 0) { if (days) { var d = dt.getDate() - days; dt.setDate(d); a = dt - now; if (a > 0) { return 1; } else { return -2; } } return 1; } else if (a == 0) { return 0; } else { return 'exception'; } }
        //注册过期时间比较
        function GetPeriodOfValidity(time) {
            if (time != "" && time.indexOf("1900") !== 0) {
                if (time.split(' ').length < 2) time += " 11:59:59";
                var b = comptimeToNow(time, 7);
                if (b === 1) {
                } else {
                    if (b === -2) document.write("<br><span style=\"color:blue;\">注册快要过期</span>");
                    else document.write("<br><span style=\"color:red;\">注册已经过期</span>");
                }
            }
        }
        //证书有效过期时间比较
        function GetIncPeriodOfValidity(times) {
            times = times.split("—");
            var z = "营业执照,生产许可证,GMP证,经营许可证,GSP证,组织机构代码证,医疗机构许可证".split(",");
            var k = 0, h = 0;
            for (var i = 0; i < times.length; i++) {
                var time = times[i], b;
                if (time == "永久" || time.indexOf("1900") == 0) {
                    h = h + 1; continue;
                }
                if (time != "") {
                    if (time.split(' ').length < 2) time += " 11:59:59";
                    b = comptimeToNow(time, 7);
                    if (b === 1) {
                        h = h + 1;
                    } else {
                        if (b === -2) document.write("<span style=\"color:blue;display:block;\">" + z[i] + "快要过期</span>");
                        else document.write("<span style=\"color:red;display:block;\">" + z[i] + "已经过期</span>");
                    }
                } else {
                    k = k + 1;
                }
            }
            if (k == times.length) {
                //document.write("<span style=\"color:red;display:block;\">证书未上传</span>");
            } else if (h == 4) {
                //document.write("<span style=\"color:green;display:block;\">证书有效</span>");
            }
        }

        function SendAjax(data, success_function) { var aurl = 'BuyerLib.aspx?is_ajax=1'; jQuery.ajax({ url: aurl, type: "post", dataType: "text", data: data, async: false, success: success_function }); }
        function GetDateFormat(d) { document.write(d.toString().split(' ')[0]); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    买家库信息
    <%if (SOSOshop.BLL.PowerPass.isPass("008009002"))
      { %>
    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="inputbutton" NavigateUrl="Buyer_edit.aspx?type=lib"
        Width="65px" Height="23px">添加买家</asp:HyperLink><%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    买家姓名：<asp:TextBox ID="w_d_TrueName" runat="server" Width="90" CssClass="autocomplete"
        autosubmit="submitSearchAgent" autocomplete="off" spellcheck="false" x-webkit-grammar="builtin:search"
        lang="zh_cn" x-webkit-speech speech></asp:TextBox>
    &nbsp;手机号：<asp:TextBox ID="w_l_MobilePhone" runat="server" Width="90" CssClass="autocomplete"
        autosubmit="submitSearchAgent" autocomplete="off" spellcheck="false" x-webkit-grammar="builtin:search"
        lang="zh_cn" x-webkit-speech speech></asp:TextBox>
    单位：<asp:TextBox ID="txtParents" runat="server" Width="90" CssClass="autocomplete"
        autosubmit="submitSearchAgent" autocomplete="off" spellcheck="false" x-webkit-grammar="builtin:search"
        lang="zh_cn" x-webkit-speech speech></asp:TextBox>
 <%--   &nbsp;会员类别：<asp:DropDownList ID="w_d_UserType" runat="server">
        <asp:ListItem Value="">不限制</asp:ListItem>
        <asp:ListItem Value="0">普通会员</asp:ListItem>
        <asp:ListItem Value="1">企业会员</asp:ListItem>
    </asp:DropDownList>--%>
    &nbsp;买家类别：<asp:DropDownList ID="w_d_Member_Class" runat="server">
    </asp:DropDownList>
    <br />
    所在地区<span id="ChinaArea" class="ChinaArea" style="margin-left: 5px;"> 省：<select id="province"
        name="province" style="width: 65px;"></select>
        市：<select id="city" name="city" style="width: 75px;"></select>
        县：<select id="county" name="county" style="width: 100px;"></select>
        <input type="hidden" id="ddlProvince" name="ddlProvince" />
        <input type="hidden" id="ddlCity" name="ddlCity" />
        <input type="hidden" id="ddlBorough" name="ddlBorough" />
    </span>
    &nbsp;状态：<asp:DropDownList ID="w_d_State" runat="server">
        <asp:ListItem Value="">不限制</asp:ListItem>
        <asp:ListItem Value="1">未审核</asp:ListItem>
        <asp:ListItem Value="0">已审核</asp:ListItem>
        <asp:ListItem Value="2">冻结</asp:ListItem>
    </asp:DropDownList>
    &nbsp;来源：
    <asp:DropDownList ID="w_d_Member_Type" runat="server">
        <asp:ListItem Value="">所有</asp:ListItem>
        <asp:ListItem Value="0">网上注册</asp:ListItem>
        <asp:ListItem Value="1">电话注册</asp:ListItem>
    </asp:DropDownList>
    &nbsp;是否成交：
       <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="">所有</asp:ListItem>
        <asp:ListItem Value="0">有成交</asp:ListItem>
        <asp:ListItem Value="1">无成交</asp:ListItem>
    </asp:DropDownList>
    <%-- 外销人员：--%><asp:DropDownList ID="ddlOSP" Visible="false" runat="server" DataTextField="ospname" DataValueField="ospid"
            Width="75">
        </asp:DropDownList>
    <br />
        客服：<asp:DropDownList ID="ddlEditer" runat="server" DataTextField="name" DataValueField="id"
            Width="75">
        </asp:DropDownList>
    GSP审核：<asp:DropDownList ID="w_d_Gsp" runat="server">
        <asp:ListItem Value="">不限制</asp:ListItem>
        <asp:ListItem Value="1">已审核</asp:ListItem>
        <asp:ListItem Value="0">未审核</asp:ListItem>
    </asp:DropDownList>
    &nbsp;未下过订单的，时间为：<asp:DropDownList ID="w_d_hasOrder" runat="server" ToolTip="注册时间">
        <asp:ListItem Value="">不限制</asp:ListItem>
        <asp:ListItem Value="30">1个月以上</asp:ListItem>
        <asp:ListItem Value="90">3个月以上</asp:ListItem>
        <asp:ListItem Value="180">6个月以上</asp:ListItem>
    </asp:DropDownList>
    &nbsp;上级单位：<asp:DropDownList ID="w_d_hasParent" runat="server">
        <asp:ListItem Value="">不限制</asp:ListItem>
        <asp:ListItem Value="1">有</asp:ListItem>
        <asp:ListItem Value="0">无</asp:ListItem>
    </asp:DropDownList>
    &nbsp;<br />
    注册时间：
    <asp:TextBox ID="fromDate" MaxLength="10" Width="70" CssClass="datepicker" runat="server"></asp:TextBox>
    &nbsp;至
    <asp:TextBox ID="toDate" MaxLength="10" Width="70" CssClass="datepicker" runat="server"></asp:TextBox>
    <asp:Button ID="submitSearch" runat="server" CssClass="inputbutton" OnClick="Search_Click"
        Text="查  询" />
    <input type="button" id="btnReSet" runat="server" class="inputbutton" onclick="ReSet()"
        value="重  置" />
    <font color="gray">（搜索包括：联系人、手机号等）</font>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
        Style="width: 100%" AllowSorting="True">
        <Columns>
            <asp:TemplateField Visible="false">
                <HeaderTemplate>
                    <input type="checkbox" id="chkAll" title="全选" /></HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk" CssClass="chk" ToolTip='<%#Eval("UID")%>' runat="server" Enabled="false" />
                </ItemTemplate>
                <ItemStyle CssClass="unclick" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="姓名">
                <ItemTemplate>
                    <%#Eval("TrueName")%></ItemTemplate>
                <ItemStyle Width="50" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="手机号">
                <ItemTemplate>
                    <%#Eval("MobilePhone")%></ItemTemplate>
                <ItemStyle Width="77" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单位(默认)">
                <ItemTemplate>
                    <script type="text/javascript">
                        GetParentIncName('<%#Eval("UID")%>-<%#Eval("ParentId")%>-<%#Eval("Parents")%>');</script>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地区">
                <ItemTemplate>
                    <span class="Province">
                        <%#Eval("Province")%></span> <span class="City">
                            <%#Eval("City")%></span> <span class="County">
                                <%#Eval("Borough")%></span>
                </ItemTemplate>
                <ItemStyle Width="88" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="注册日期">
                <ItemTemplate>
                    <%#Convert.ToDateTime(Eval("RegisterDate")).ToString("yyyy-MM-dd")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="买家状态">
                <ItemTemplate>
                    <span id='s<%#Eval("UID")%>'>
                        <%#Convert.ToInt32(Eval("State")) == 0 ? "<font color=Green>已审核</font>" : Convert.ToInt32(Eval("State")) == 2 ? "<font color=Red>冻结</font>" : "<font color=Red>未审核</font>"%>
                    </span>
                </ItemTemplate>
                <ItemStyle Width="53" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="买家类别">
                <ItemTemplate>
                    <span id='mc<%#Eval("UID")%>' title='<%#Convert.ToInt32(Eval("Member_Class")) == 0 ? "批发客户" : (Convert.ToInt32(Eval("Member_Class")) == -1 ? "" : "OTC客户")%>'>                        
                        <%#Eval("CompanyClass")%>
                    </span>
                </ItemTemplate>
                <ItemStyle Width="60" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GSP状态">
                <ItemTemplate>
                    <span id='gsp<%#Eval("UID")%>' title='自动从ERP同步GSP状态到商城'>
                        <%#GetGSP(Convert.ToInt32(Eval("UID"))) == 1 ? "<font color=Green>已通过</font>" : "<font color=Red>待审核</font>"%>
                    </span>
                </ItemTemplate>
                <ItemStyle Width="50" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最近成交">
                <ItemTemplate>
                    <span id='order<%#Eval("UID")%>'>
                        <%#GetOrderDate(Convert.ToInt32(Eval("UID")))%>
                    </span>
                </ItemTemplate>
                <ItemStyle Width="50" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="登录" SortExpression="LoginCount">
                <ItemTemplate>
                    <a onclick='SeeLoginTimes(<%#Eval("UID")%>,this);return false;' href="#" title="查看详情"
                        style="height: 22px; display: block; width: 30px;">
                        <%#Eval("LoginCount")%></a>
                </ItemTemplate>
                <ItemStyle Width="30" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客服">
                <ItemTemplate>
                    <%--    <span id='ut<%#Eval("UID")%>'>
                        <%#Convert.ToInt32(Eval("UserType")) == 0 ? "<font color=Green>普通会员</font>" : "<font color=Red>企业会员</font>"%>
                        </span>--%>
                    <%#Eval("adminname") %>
                </ItemTemplate>
                <ItemStyle Width="53" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户来源">
                <ItemTemplate>
                    <span id='mt<%#Eval("UID")%>'>
                        <%#Convert.ToInt32(Eval("Member_Type")) == 0 ? "<font color=Green>网上注册</font>" : "<font color=Red>电话注册</font>"%>
                    </span>
                </ItemTemplate>
                <ItemStyle Width="53" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="权限">
                <ItemTemplate>
                    <a onclick='SeePermissions(<%#Eval("UID")%>,this);return false;' href="#">查看</a>
                </ItemTemplate>
                <ItemStyle Width="30" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%--                        <%if(isCheckUp){ %><a class="del" onclick='State(<%#Eval("UID")%>,this);return false;' href="#" title="已冻结的会员不能登陆">
                            <%#Convert.ToInt32(Eval("State")) != 0 ? (Convert.ToInt32(Eval("State")) != 2 ? "审核" : "") : ""%></a><%} %>
                    --%>
                    <%if (IsEdit)
                      { %><a class="edit" href='Buyer_edit.aspx?uid=<%#Eval("UID")%>&Code=<%#Eval("Code")%>'>编辑</a><%} %><%if (IsDelete)
                      { %><a class="del" onclick='DelAll(<%#Eval("UID")%>,this);return false;' href="#" style='<%#(Convert.ToInt32(Eval("State")) == 1 || (Convert.ToInt32(Eval("ParentId")) == 0 && Convert.ToInt32(Eval("Member_Class")) == -1))?"":"display:none"%>'>删除</a><%} %><%if (IsPresentIntegral)
                      { %><a class="edit" style="display:block" onclick='PresentIntegral(<%#Eval("UID")%>,"<%#Eval("TrueName")%>",this);return false;' href="#">赠送积分</a><%} %>
                    
                </ItemTemplate>
                <ItemStyle Width="80" CssClass="unclick" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="quotes">
        <div style="float: right;">
            共<span style="color: Red"><%=AspNetPager1.RecordCount%></span>条记录
        </div>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
            NextPageText="下一页" PageIndexBoxType="DropDownList" PrevPageText="上一页" SubmitButtonText="Go"
            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Style="text-align: right;
            float: right" ShowPageIndexBox="Auto" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="pagebottom" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            //省
            jQuery("span.Province").each(function (i) {
                try {
                    var val = jQuery.trim(jQuery(this).html());
                    if (val == 'N/A' || val == '0' || val.replace('&nbsp;', '') == '') jQuery(this).html('');
                    else {
                        var province = new Location().findProvince(val);
                        if (province) jQuery(this).html(province);
                    }
                } catch (e) { }
            });
            //市
            jQuery("span.City").each(function (i) {
                try {
                    var val = jQuery.trim(jQuery(this).html());
                    if (val == 'N/A' || val == '0' || val.replace('&nbsp;', '') == '') jQuery(this).html('');
                    else {
                        var city = new Location().findCity(val);
                        if (city) jQuery(this).html(city);
                    }
                } catch (e) { }
            });
            //区
            jQuery("span.County").each(function (i) {
                try {
                    var val = jQuery.trim(jQuery(this).html());
                    if (val == 'N/A' || val == '0' || val.replace('&nbsp;', '') == '') jQuery(this).html('');
                    else {
                        var county = new Location().findCounty(val);
                        if (county) jQuery(this).html(county);
                    }
                } catch (e) { }
            });

            //这里不允许删除OTC客户
            jQuery("#tablist tr").each(function (i) {
                if (i > 0) {
                    var td = jQuery(this), del = td.find(".del"), Member_Class = td.find(".Member_Class"), htm = Member_Class.html();
                    if (htm.indexOf("OTC") != -1 && del.length > 0) {
                        del.parent().append(jQuery('<font title="请在OTC商城进行此操作" color=gray>删除</font>'));
                        del.remove();
                        td.find('input[type="checkbox"]').attr("disabled", "disabled");
                    }
                }
            });
            //操作
            jQuery("#tablist a").each(function (i) { if (jQuery.trim(jQuery(this).text()) == '') jQuery(this).remove(); });
        });
    </script>
    <!--StatisticalTime(ms[Init,Load,Render])=[<%=StatisticalTime[0]%>,<%=StatisticalTime[1]%>,<%=StatisticalTime[2]%>]-->
</asp:Content>
