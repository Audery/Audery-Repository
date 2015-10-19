<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buyer_LoginTimes.aspx.cs" Inherits="_101shop.admin.v3.admin.member.Buyer_LoginTimes" %>

<%@ Register Assembly="Library.UI" Namespace="Library.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <script type="text/javascript" src="/admin/scripts/jquery.js"></script>
    <style type="text/css">
    DIV.quotes { float: none; height: 28px; padding-right: 20px; padding-top: 5px;}
    .datatable th, .datatable td { cursor:auto;}
    .unclick a, .unclick button { margin:0 2px; line-height:150%;}
    .datatable tr td { line-height: 19px;}
    .datatable th { border:1px solid #BAD9C6;}
    .list { display:block; max-height:100px; overflow:auto;}
    .list ul, .list li { list-style:none; margin:0; padding:0;}
    .list li a, .list li span { text-decoration:none; height:19px; line-height:19px; display:block; float:left; overflow:hidden; }
    .list li a { width:130px; }
    .loginintime2 { display:none;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var productName = $("#tablist a");
            
            var parent = productName.parent();
            for (var i = 0; i < productName.length; i++) {
                if (productName[i].innerHTML == "") {
                    parent[i].innerHTML = "无";
                }
            }
        });
        function SendAjax(data, success_function) { var aurl = 'Buyer_LoginTimes.aspx?is_ajax=1'; jQuery.ajax({ url: aurl, type: "post", dataType: "text", data: data, async: false, success: success_function }); }
        function GetDateFormat(d) { return(d.toString().split(' ')[1]); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hftrueName" runat="server" />
    <asp:HiddenField ID="hfmobilePhone" runat="server" />
    <asp:HiddenField ID="hfdefaultIncName" runat="server" />
    <asp:HiddenField ID="hfConsigneeCity" runat="server" />
    <asp:HiddenField ID="hfConsigneeBorough" runat="server" />
    <asp:HiddenField ID="hfConsigneeProvince" runat="server" />
    <asp:HiddenField ID="hfmember_Class" runat="server" />
    <asp:HiddenField ID="hfUID" runat="server" />
    <div>
        <asp:GridView ID="tablist" CssClass="datatable" runat="server" AutoGenerateColumns="False"
            Style="width: 100%">
            <Columns>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate><%=hftrueName.Value%></ItemTemplate>
                    <ItemStyle Width="50" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机号">
                    <ItemTemplate><%=hfmobilePhone.Value%></ItemTemplate>
                    <ItemStyle Width="77" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位(默认)">
                    <ItemTemplate><%=hfdefaultIncName.Value%></ItemTemplate>
                    <ItemStyle Width="145" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地区">
                    <ItemTemplate>
                    <span class="Province"><%=hfConsigneeProvince.Value%></span>
                    <span class="City"><%=hfConsigneeCity.Value%></span>
                    <span class="County"><%=hfConsigneeBorough.Value%></span>
                    </ItemTemplate>
                    <ItemStyle Width="78" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="买家类别">
                    <ItemTemplate><%=hfmember_Class.Value%></ItemTemplate>
                    <ItemStyle Width="52" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="登陆日期">
                    <ItemTemplate>
                        <span class="loginintime"><%#Eval("loginintime")%></span>
                    </ItemTemplate>
                    <ItemStyle Width="114" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IP地址">
                    <ItemTemplate><%#Eval("loginip")%></ItemTemplate>
                    <ItemStyle Width="72" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="地区">
                    <ItemTemplate><%#Eval("loginregion")%></ItemTemplate>
                    <ItemStyle Width="52" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="浏览商品">
                    <ItemTemplate>
                        <span><a href="http://www.101yao.com/<%#Eval("ProId")%>.html" target="_blank"><%#Eval("Product_Name")%></a></span>
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
</body>
</html>
