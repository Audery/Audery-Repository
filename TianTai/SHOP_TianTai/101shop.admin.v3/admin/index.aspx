<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="_101shop.admin.v3.index"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理登陆-<%=WebName %></title>
    <meta name ="keywords" content="XYShop,电子商务,B/C,电子商城,商城,网店,易想,ChangeHope" />
    <link rel="stylesheet" href="style/index.css" type="text/css" media="all"  />
    <script type="text/javascript" src="/admin/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script type="text/javascript">
    function CheckForm()
    {
        var n = $('input[id*="txtUserLoginName"]'), p = $('input[id*="txtUserLoginPwd"]'), c = $('input[id*="txtCheckCode"]');
        if(!IsNull(n,2,20))
        {
            alert("帐号不能为空"); n.focus();
            return false;
        }
        if(!CheckLength(p,5))
        {
            alert("密码至少为五位"); p.focus();
            return false;
        }
        if(!CheckEqualsLeng(c,5))
        {
            alert("验证码为5位"); c.focus();
            return false;
        }
    }
    function nkeyup(event) {
        var n = $('input[id*="txtUserLoginName"]'), p = $('input[id*="txtUserLoginPwd"]'), c = $('input[id*="txtCheckCode"]');
        var nv = n.val(), cp = CheckLength(p, 5);
        if (/*(nv == 'admin')*/IsNull(n, 2, 20) && cp) {
            //$('#imgCheckCode').css('display', ''); $('#spanCheckCode').css('display', 'none');
            //$('#imgCheckCode').css('display', 'none'); 
            //$('#spanCheckCode').css('display', 'inline');
        } else if (cp) {
        }
    }
    function nSms(event) {
        var o = $(this), v = o.html();
        if (v.indexOf("已经发送") != -1) return;
        if (v.indexOf("点击获取") != -1) {
            var n = $('input[id*="txtUserLoginName"]'), p = $('input[id*="txtUserLoginPwd"]'), c = $('input[id*="txtCheckCode"]');
            var nv = n.val(), pv = p.val(), cp = CheckLength(p, 5);
            if (!IsNull(n, 2, 20) || !CheckLength(p, 5)) return;
            var data = "Option=sendAdminLoginCheckCode&add=1&id=1&toUID=" + nv + "&toPWD=" + pv;
            jQuery.ajax({
                url: "index.aspx",
                type: "post",
                dataType: "text",
                data: data,
                async: false,
                success: function (res, status) {
                    if (res == "no") {
                        //$('#imgCheckCode').css('display', 'inline');
                        //$('#spanCheckCode').css('display', 'none');
                     return }
                    var ok = (res.indexOf("ok") != -1);
                    if (ok) {
                        res = res.split(",");
                        if (res.length > 1) res = res[1];
                        else res = "短信发送成功！";
                        alert(res);
                        o.html("已经发送60").attr("title", "正在计时...");
                        nSmsTimeout(o);
                    } else {
                        alert(res);
                    }
                }
            });
        }
    }
    function nSmsTimeout(o) {
        o = $(o), v = o.html();
        if (v.indexOf("已经发送") != -1) {
            setTimeout(function () { o = $(o), v = o.html(); var i = parseInt(v.replace("已经发送", "")); if (isNaN(i)) return o.html("点击获取").attr("title", ""); o.html("已经发送" + (i - 1 > 0 ? (i - 1).toString() : "")).attr("title", "正在计时..."); nSmsTimeout(o); }, 1000);
        }
    }
    function FormLoad() {
        var n = $('input[id*="txtUserLoginName"]'), p = $('input[id*="txtUserLoginPwd"]'), c = $('input[id*="txtCheckCode"]');
        p.val('');
        if (!IsNull(n, 2, 20)) {
            n.focus();
        } else {
            p.focus();
        }
        n.keyup(nkeyup);
        p.keyup(nkeyup);
        //$('#spanCheckCode').click(nSms);
    }
    </script>
</head>
<body onload="FormLoad()">
    <form id="form1" runat="server"  onsubmit="return CheckForm();">
    <table id="tbLogin" cellpadding="0" cellspacing="0" align="center">
	<tr>
		<td id="tdLeft" />
		<td id="tdMiddle">
			<table id="tbLoginForm"  cellpadding="0" cellspacing="0">
				<tr>
					<td id="tdTop" />
				</tr>
				<tr>
					<td id="tdMain">
					    <table cellspacing="0" cellpadding="0" id="tbLoginInfo">
						    <tr>
							    <td class="txtInfo">帐&nbsp;&nbsp;&nbsp;&nbsp;号：</td>
							    <td class="controlInfo"><asp:TextBox ID="txtUserLoginName" CssClass="userControl" runat="server"></asp:TextBox></td>
						    </tr>
						    <tr>
							    <td class="txtInfo">密&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
							    <td class="controlInfo"><asp:TextBox ID="txtUserLoginPwd" CssClass="userControl" runat="server" TextMode="Password"  EnableViewState="false"></asp:TextBox></td>
						    </tr>
						    <tr>
							    <td class="txtInfo">验证码：</td>
							    <td class="controlInfo">
							        <table cellspacing="0" cellpadding="0">
							            <tr>
							                <td><asp:TextBox ID="txtCheckCode" CssClass="checkCode" runat="server"></asp:TextBox></td>
							                <td>
                                            <img id="imgCheckCode" alt="点击刷新验证码" onclick="this.src='plugin/check_code.aspx?t='+Math.random();" src="plugin/check_code.aspx" class="pointer" />
                                            <span id="spanCheckCode" class="pointer" style="display:none;height:22px;vertical-align:top;font-size:16px;color:#55F;">点击获取</span></td>
							            </tr>
							        </table>
							    </td>
						    </tr>
						    <tr>
							    <td colspan="2">
							        <asp:ImageButton runat="server" ID="btnLogin" ImageUrl="images/admin_index_login.jpg" onclick="Login_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
							        <img alt="重置" src="images/admin_index_reset.jpg" onclick="reset();" class="pointer"/>
							    </td>
						    </tr>
					    </table>
					</td>
				</tr>
			</table>
		</td>
		<td id="tdRight" />
	</tr>
</table>
    </form>
</body>
</html>
<!--StatisticalTime(ms[Init,Load,Render])=[<%=StatisticalTime[0]%>,<%=StatisticalTime[1]%>,<%=StatisticalTime[2]%>]-->