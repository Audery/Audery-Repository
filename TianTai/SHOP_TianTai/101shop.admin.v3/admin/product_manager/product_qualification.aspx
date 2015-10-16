<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_qualification.aspx.cs" Inherits="_101shop.admin.v3.admin.product_manager.product_qualification" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            商品名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            生产厂家：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            批准文号：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            是否上传：<asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem Value="0">全部</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
                <asp:ListItem Value="2">否</asp:ListItem>
            </asp:DropDownList>
            前台展示：<asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="0">全部</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
                <asp:ListItem Value="2">否</asp:ListItem>
            </asp:DropDownList>
            <cc1:ButtionManage ID="SearchManage" OnClick="Search_Click" Text="搜索" runat="server"
                onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            <input type="button" id="btnReSet" runat="server" class="inputbutton" onclick="location = 'product_report.aspx'"
                value="重置" onmouseover="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onmouseout="javascript:document.getElementById(this.id).className='inputbutton'" />
        </div>
    </div>
    <div class="page_main">
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:TemplateField HeaderText="商品编号">
                    <ItemTemplate>
                        <a href="/admin/product_manager/product_edit.aspx?pid=<%#Eval("product_Id") %>">
                            <%#Eval("product_Id")%></a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称">
                    <ItemTemplate>                        
                            <%#Eval("Product_Name")%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="规格">
                    <ItemTemplate>
                        <%#SOSOshop.BLL.Common.Public.GetSpecificationAndS(((System.Data.DataRowView)Container.DataItem).Row)%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批准文号">
                    <ItemTemplate>
                        <%#Eval("DrugsBase_ApprovalNumber")%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="生产厂家">
                    <ItemTemplate>
                        <%#Eval("DrugsBase_Manufacturer")%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="资质数量">
                    <ItemTemplate>
                        <%#SOSOshop.BLL.Report.Qualification.GetCount((int)Eval("product_Id"))%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="下载次数">
                    <ItemTemplate>
                        <%#SOSOshop.BLL.Report.Qualification.GetDowCount((int)Eval("product_Id"))%>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="product_qualificationAdd.aspx?id=<%#Eval("product_Id")%>&ProductName=<%#TextBox1.Text %>&FactoryName=<%#TextBox2.Text %>&CodeNum=<%#TextBox3.Text %>&IsUpload=<%#DropDownList2.SelectedIndex %>&IsForHead=<%#DropDownList1.SelectedIndex %>">资质管理</a>
                    </ItemTemplate>
                    <ItemStyle CssClass="unclick" />
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
