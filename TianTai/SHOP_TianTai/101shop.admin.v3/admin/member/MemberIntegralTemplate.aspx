<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true"
    CodeBehind="MemberIntegralTemplate.aspx.cs" Inherits="_101shop.admin.v3.member.MemberIntegralTemplate"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript" src="../scripts/listtable.js"></script>
    <style type="text/css">
        table
        {
            margin: 10px 20px 20px 30px;
        }
        td
        {
            line-height: 150%;
            padding: 4px;
        }
    </style>
    <script type="text/javascript">
        function Edit(id, integral, type) {
            var integral1 = window.prompt('请输入数值', type === 1 ? parseInt(integral) : integral);
            if ((integral1 == false && integral1 != "0") || integral1 == integral || isNaN(parseFloat(integral1))) {
                return false;
            }
            if (parseFloat(integral1) < 0) {
                return alert('请输入数值，并且要大于等于零');
            }
            $.ajax({
                type: 'POST',
                url: 'MemberIntegralTemplate.aspx?ajax=1',
                data: { id: id, integral: integral1 },
                dataType: "json",
                success: function (msg, textStatus) {
                    //alert(msg + textStatus);
                    switch (msg["state"]) {
                        case 1:
                            alert(msg["message"]);
                            window.location = 'MemberIntegralTemplate.aspx';
                            break;
                        default:
                            alert(msg["message"]);
                            break;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown + " error");
                }
            });
        }
        //修改倍数据
        function editday(id, m) {
            var v = prompt("填写积分倍数", $.trim($(".multiple" + id).html()));
            if (isInt(v)) {
                $.post("MemberIntegralTemplate.aspx", "id=" + id + "&multiple=" + v, function () {
                    $(".multiple" + id).html(v);
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    积分规则管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <table class="form_table_input" border="0" width="600" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <b>积分名称</b>
            </td>
            <td>
                <b>频率</b>
            </td>
            <td>
                <b>奖励积分</b>
            </td>
            <td>
                <b>&nbsp;&nbsp;&nbsp;&nbsp;操作</b>
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#getname((string)Eval("name"))%>
                    </td>
                    <td>
                        <%#SOSOshop.BLL.Integral.MemberIntegralTemplateDetail.MemberIntegralTemplateEnumFrequency((SOSOshop.BLL.Integral.MemberIntegralTemplateEnum)Enum.Parse(typeof(SOSOshop.BLL.Integral.MemberIntegralTemplateEnum), Convert.ToString(Eval("id"))))%>
                    </td>
                    <td>
                        <%#SOSOshop.BLL.Integral.MemberIntegralTemplateDetail.MemberIntegralTemplateEnumIntegral((SOSOshop.BLL.Integral.MemberIntegralTemplateEnum)Enum.Parse(typeof(SOSOshop.BLL.Integral.MemberIntegralTemplateEnum), Convert.ToString(Eval("id"))), (decimal)Eval("Integral"))%>
                    </td>
                    <td>
                        <input onclick="Edit('<%#Eval("id")%>','<%#Convert.ToString(Eval("Integral")).TrimEnd('0').TrimEnd('.')%>',<%#Eval("type")%>)"
                            type="button" value="修改积分" style="border: 0 none; color: Blue;" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
            终端客户会员日设置(成交订单积分翻倍)
        </div>
    </div>
    <table class="form_table_input" border="0" width="600" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <b>星期</b>
            </td>
            <td>
                <b>倍数</b>
            </td>
            <td>
                <b>频率</b>
            </td>
            <td>
                <b>&nbsp;&nbsp;&nbsp;&nbsp;操作</b>
            </td>
        </tr>
        <asp:Repeater ID="Repeater2" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        星期<%#((string)Eval("id") == "0" ? "日" : Eval("id"))%>
                    </td>
                    <td class="multiple<%#Eval("id") %>">
                        <%#Eval("multiple")%>
                    </td>
                    <td>
                        无限制
                    </td>
                    <td>
                        <a onclick="editday(<%#Eval("id") %>,<%#Eval("multiple") %>)" style="cursor: pointer">
                            修改积分</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
