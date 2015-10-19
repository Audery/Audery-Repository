<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buyer_Permissions.aspx.cs"
    Inherits="_101shop.admin.v3.admin.member.Buyer_Permissions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <style type="text/css">
        th h2
        {
            font-size: 14px;
            font-weight: normal;
            line-height: 18px;
            margin: 0;
        }
        th h2 b
        {
            text-indent: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 4px">
        <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
            <thead>
                <tr>
                    <th colspan="2">
                        <h2>
                            <asp:Literal ID="ltlMemberinfo" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfUID" runat="server" />
                        </h2>
                    </th>
                </tr>
            </thead>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle">
                    批发价格查看：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsLookPrice_01" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsLookPrice_01_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle">
                    批发商品查看：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsLookProduct_01" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsLookProduct_01_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle">
                    OTC价格查看：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsLookPrice_02" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsLookPrice_02_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle">
                    OTC商品查看：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsLookProduct_02" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsLookProduct_02_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle">
                    交易权限：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsTrade" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsTrade_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="230" class="form_table_input_info" valign="middle">
                    实时库存查看：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsLookStock" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsLookStock_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td width="150" class="form_table_input_info" valign="middle">
                    定期结算：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsPeriodicalSettle" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsPeriodicalSettle_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td width="150" class="form_table_input_info" valign="middle">
                    货到付款：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsCOD" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsCOD_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td width="150" class="form_table_input_info" valign="middle">
                    款到发货：
                </td>
                <td>
                    <asp:RadioButtonList ID="cb_IsMoneyAndShipping" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsMoneyAndShipping_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none">
                <td width="150" class="form_table_input_info" valign="middle" title="是否有货先发的权限(默认批发才有，OTC都没得)">
                    有货先发：
                </td>
                <td title="是否有货先发的权限(默认批发才有，OTC都没得)">
                    <asp:RadioButtonList ID="cb_IsPriorDistribution" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsPriorDistribution_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td width="150" class="form_table_input_info" valign="middle" title="是否有送货上门的权限">
                    送货上门：
                </td>
                <td title="是否有送货上门的权限">
                    <asp:RadioButtonList ID="cb_IsShippingFor48h" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsShippingFor48h_SelectedIndexChanged">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td width="150" class="form_table_input_info" valign="middle" title="是否拥有快捷开通交易的权限">
                    <font color="red">快捷开通交易权限：</font>
                </td>
                <td title="是否拥有快捷开通交易的权限">
                    <asp:RadioButtonList ID="cb_IsSpecialTrade" runat="server" Enabled="false" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="cb_IsSpecialTrade_SelectedIndexChanged"
                        Style="float: left">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="float: left; margin-top: 7px; color: Red">
                        (7天后自动失效)</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
