<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_list_Lock.aspx.cs" Inherits="_101shop.admin.v3.admin.product_manager.product_list_Lock" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../style/admin.css" type="text/css" />
    <link rel="stylesheet" href="../style/admin2.css" type="text/css" />
    <script src="../scripts/global.js" type="text/javascript"></script>
    <link href="../style/jquery.qtip.min.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.qtip.min.js" type="text/javascript"></script>
    <link href="../scripts/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../scripts/jquery-easyui-1.3.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../scripts/jquery-easyui-1.3.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/menu.css" rel="stylesheet"
        type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/linkbutton.css" rel="stylesheet"
        type="text/css" />
    <link href="../scripts/jquery-easyui-1.3.1/themes/default/menubutton.css" rel="stylesheet"
        type="text/css" />
    <link id="skin" rel="stylesheet" href="../scripts/jBox/Skins2/Green/jbox.css" />
    <script type="text/javascript" src="../scripts/jBox/jquery.jBox-2.3.min.js"></script>
    <script type="text/javascript" src="../scripts/jBox/i18n/jquery.jBox-zh-CN.js"></script>
    <script type="text/javascript" src="../scripts/productSelect.js"></script>
    <style type="text/css">
        a:link.Remark
        {
            color: #FF00BC;
        }
        a:visited.Remark
        {
            color: #FF00BC;
        }
        #ActList li
        {
            margin-left: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            商品名称：<asp:TextBox ID="TextBox6" runat="server" Width="100"></asp:TextBox>
            生产厂家：<asp:TextBox ID="TextBox1" runat="server" Width="100"></asp:TextBox>
            批准文号：<asp:TextBox ID="TextBox2" runat="server" Width="100"></asp:TextBox>
            下单时间：<asp:TextBox ID="TextBox4" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd',errDealMode:0 })"></asp:TextBox>
                到
            <asp:TextBox ID="TextBox5" runat="server" Width="100" onclick="WdatePicker({ skin: 'blueFresh', dateFmt: 'yyyy-MM-dd',errDealMode:0 })"></asp:TextBox>
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">3天前</asp:LinkButton>
            <cc1:ButtionManage ID="SearchManage" OnClick="Search_Click" Text="搜索" runat="server"
                onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            <input type="button" id="btnReSet" runat="server" class="inputbutton" onclick="location = 'product_list_check.aspx'"
                value="重置" onmouseover="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onmouseout="javascript:document.getElementById(this.id).className='inputbutton'" />
        </div>
    </div>
    <div class="page_main">
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:TemplateField HeaderText="商品名">
                    <ItemTemplate>
                        <a title="<%#Eval("Product_Id") %>" href="product_edit.aspx?pid=<%#Eval("Product_Id") %>">
                            <%#Eval("Product_Name")%></a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:BoundField DataField="DrugsBase_DrugName" HeaderText="通用名" />
                <asp:TemplateField HeaderText="规格">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Bind("DrugsBase_Specification") %>'></asp:Label>
                    </ItemTemplate>                 
                </asp:TemplateField>
                <asp:BoundField DataField="DrugsBase_Manufacturer" HeaderText="厂家" />                
                <asp:BoundField DataField="DrugsBase_ApprovalNumber" HeaderText="批文" />
                <asp:BoundField DataField="Price_01" HeaderText="批发价" DataFormatString="{0:f2}" />
                <asp:BoundField DataField="Price_02" HeaderText="OTC价" DataFormatString="{0:f2}" />
                <asp:BoundField DataField="stock" HeaderText="库存" />
                <asp:BoundField DataField="Stock2" HeaderText="锁库"/>
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="Stock_LockList.aspx?id=<%#Eval("Product_Id") %>">查看明细</a>
                    </ItemTemplate>                 
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
                float: right" ShowPageIndexBox="Never" OnPageChanged="AspNetPager1_PageChanged">
            </webdiyer:AspNetPager>
        </div>
    </div>
    </form>
    <%Response.Write("<script type=\"text/javascript\">var a=top.window.document.getElementById('mainFrame'),b=a.contentWindow;b.window.isAdd();/*isBrowse();isEdit();isAdd();isDelete();*/</script>"); %>
</body>
</html>