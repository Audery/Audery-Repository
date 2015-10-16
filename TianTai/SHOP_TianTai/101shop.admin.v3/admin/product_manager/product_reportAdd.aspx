<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_reportAdd.aspx.cs"
    Inherits="_101shop.admin.v3.admin.product_manager.product_reportAdd" %>

<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html>
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
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
            商品名称：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp; 批号：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            药检：<asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
            <CE:Uploader ID="Uploader1" runat="server" UploadingMsg="正在上载..." InsertText="选择"
                ValidateOption-MaxSizeKB="1024" ValidateOption-AllowedFileExtensions="jpg" CancelText="取消">
            </CE:Uploader>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <cc1:ButtionManage ID="SearchManage" Text="保存" runat="server" OnClick="SearchManage_Click" />
        </div>
        <div class="page_sarch">
            <hr style="border: 1px dashed #DDDDDD; margin-bottom: 8px;" />
        </div>
    </div>
    <div class="page_main">
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:BoundField HeaderText="批号" DataField="pihao" />
                <asp:BoundField HeaderText="来源" DataField="iden" />
                <asp:BoundField HeaderText="下载次数" DataField="dowCount" />
                <asp:BoundField HeaderText="日期" DataField="created" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        药检报告</HeaderTemplate>
                    <ItemTemplate>
                        <%#GetFile(Eval("file")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        操作</HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("id")%>' runat="server"
                            CssClass="del" OnClick="Delte_Click">删除</asp:LinkButton>
                        ｜<asp:LinkButton ID="Button1" CommandArgument='<%#Eval("id")%>' OnClick="Edit_Click" runat="server" Text="编辑" />
                    </ItemTemplate>
                    <ItemStyle Width="110" CssClass="unclick" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
