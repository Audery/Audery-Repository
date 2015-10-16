<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/admin_page.master"
    CodeBehind="sms_list.aspx.cs" Inherits="_101shop.admin.v3.accessories.sms_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <link href="../../scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css"
        rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script src="../scripts/public.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Del(id) {
            var idStr;
            if (id < 0) {
                idStr = GetAllChecked();
                if (idStr == "") {
                    alert("您没有选择要删除的信息!");
                    return;
                }
            }
            else {
                idStr = id
            }
            if (confirm('确定要永久删除该信息吗?删除后将不能被恢复!')) {
                var url = "sms_list.aspx", param = "Option=del&id=" + idStr;
                jQuery.ajax({
                    type: "POST", url: url, async: true, data: param, dataType: "text",
                    success: function (ret) {
                        try {
                            if (ret == "ok") {
                                location.href = location.href; location.reload();
                            }
                            else if (ret == "no") {
                                alert("对不起，你没有删除权限！");
                            }
                            else if (ret != "") {
                                alert(ret);
                            }
                        } catch (e) { }
                    },
                    error: function (x, e) {
                        //alert("服务器连接失败！");
                    },
                    complete: function (x) {
                        //alert(x.responseText);
                    }
                });
            }
            else {
                return false;
            }
        }
        function sms_send(img0) {
            var b1 = (img0.src.indexOf('1.gif') > 0), b0 = (img0.src.indexOf('0.gif') > 0), loading = (img0.src.indexOf('loading.gif') > 0);
            if (b1 || b0) {
                if (confirm('确定要再次发送该信息吗?')) {
                    var els = jQuery(img0).parent().parent().find("td");
                    var ID = els.eq(0).find("*").val(), fromUID = els.eq(1).text(), toUID = els.eq(2).text(), Mobile = els.eq(3).text(), Msg = els.eq(4).find("*").val();
                    if (jQuery.trim(Msg) == "") {
                        alert("发送内容不能为空！"); return false;
                    }
                    var url = "../member/sms_send.aspx", param = "Option=send&id=" + ID + "&fromUID=" + fromUID + "&toUID=" + toUID + "&Mobile=" + Mobile + "&Msg=" + Msg;
                    if (b1) {
                        param += "&add=1";
                        img0.src = img0.src.replace('1.gif', 'loading.gif'); img0.style.width = img0.style.height = "22px";
                    } else if (b0) {
                        img0.src = img0.src.replace('0.gif', 'loading.gif'); img0.style.width = img0.style.height = "22px";
                    }
                    jQuery.ajax({
                        type: "POST", url: url, async: true, data: param, dataType: "text",
                        success: function (ret) {
                            try {
                                if (ret == "ok") {
                                    alert("发送成功了。");
                                    location.href = location.href; location.reload();
                                }
                                else if (ret == "no") {
                                    alert("对不起，你没有发送Sms消息的权限！");
                                }
                                else if (ret != "") {
                                    alert(ret);
                                }
                                if (b1) {
                                    img0.src = img0.src.replace('loading.gif', '1.gif'); 
                                } else if (b0) {
                                    img0.src = img0.src.replace('loading.gif', '0.gif'); 
                                }
                            } catch (e) { }
                        },
                        error: function (x, e) {
                            //alert("服务器连接失败！");
                        },
                        complete: function (x) {
                            //alert(x.responseText);
                        }
                    });
                }
            }
            else if (loading) {
                alert("正在发送..");
            }
            else {
                return false;
            }
        }
    </script>
    <script src="../scripts/images.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    Sms信息管理
    &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" CssClass="inputbutton" OnClientClick="location='../member/sms_send.aspx';return true;" style="color:Blue;"
                    Text="发送信息" UseSubmitBehavior="False" />
    &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CssClass="inputbutton" OnClientClick="location='../systeminfo/sms_setting.aspx';return true;" style="color:Blue;"
        Text="设置账户" UseSubmitBehavior="False" />
    &nbsp;&nbsp;<img src="../images/listen.gif" alt="> " />&nbsp;<label style="color:Green; font-size:13px;">支持重新发送(请点击状态栏图标)，发送内容可直接修改。</label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
    <%--<a href="javascript:void(0)" onclick="Del(-1)">批量删除</a>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagesarch" ID="ContentSearch" runat="server">
    <table border="0" cellspacing="1" cellpadding="1" width="100%">
        <tr>
            <td width="50">
                收件人：
            </td>
            <td width="70">
                <asp:TextBox ID="w_l_toUID" Width="70" runat="server"></asp:TextBox>
            </td>
            <td width="60">
                手机号码：
            </td>
            <td width="70">
                <asp:TextBox ID="w_l_Mobile" Width="70" runat="server"></asp:TextBox>
            </td>
            <td width="60">
                发送内容：
            </td>
            <td width="70">
                <asp:TextBox ID="w_l_Msg" Width="70" runat="server"></asp:TextBox>
            </td>
            <td width="260">
    &nbsp; 发送时间：从<asp:TextBox ID="fromOrderDate" MaxLength="10" Width="70" CssClass="datepicker" runat="server"></asp:TextBox>
    至<asp:TextBox ID="toOrderDate" MaxLength="10" Width="70" CssClass="datepicker" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="submitSearch" runat="server" CssClass="inputbutton" OnClick="btnSearch_Click"
                    Text="查  询" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                    onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:Literal ID="lblList" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="ContBottom" runat="server" ContentPlaceHolderID="pagebottom">
    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="inputbutton" NavigateUrl="#"
        onclick="Del(-1)" Width="65px" Height="23px"  >批量删除</asp:HyperLink>
</asp:Content>
