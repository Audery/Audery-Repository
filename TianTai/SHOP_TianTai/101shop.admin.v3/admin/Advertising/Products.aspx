<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/admin_page.master" CodeBehind="Products.aspx.cs" Inherits="_101shop.admin.v3.admin.Advertising.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />

    <style type="text/css">
        #ul {
            position: absolute;
            /*left:100px;
  top:150px;*/
            margin: 0px;
            padding: 0px;
            list-style-type: none;
        }

        .it {
            margin: 0px;
            width: 80px;
            height: 25px;
            color: white;
            text-align: center;
            line-height: 25px;
            cursor: pointer;
            background: black;
            border: 1px solid white;
        }

        #ul li {
            height: 25px;
            color: blue;
            text-align: center;
            line-height: 25px;
            cursor: pointer;          
            z-index: 1000;
        }

        .highLight {
            width: 80px;
            background: #808080;
            border: 1px solid blue;
            height: 50px;
            color: black;
        }
        .code {
            display:none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    商品广告位管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">
</asp:Content>
<asp:Content ID="ContentSearch" ContentPlaceHolderID="pagesarch" runat="server">
    广告位：<asp:DropDownList ID="dropdown" runat="server"></asp:DropDownList> 栏目名称：  
    <asp:TextBox ID="w_l_sitename" runat="server"></asp:TextBox>
    <asp:Button ID="butSearch" runat="server" CssClass="inputbutton" OnClick="Create_Click" Text="创  建" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
   
    <ul id='ul'>     
        <li> <asp:TextBox ID="Code" runat="server" CssClass="code"></asp:TextBox>
             <asp:TextBox ID="PorductId" runat="server" CssClass="code"></asp:TextBox>
            <asp:Button ID="Del" runat="server"  OnClick="lbtnSearch_Click" Text="清  除" />     
        </li>       
    </ul>
    <table width="100%" id="tablist" cellspacing="0" cellpadding="0" class="datatable" style="">
        <tr>
            <th style="line-height: 23px">位置编号</th>
            <th>栏目名称</th>
            <th>商品内容</th>
        </tr>
        <% 
            SOSOshop.BLL.Advertising ad = new SOSOshop.BLL.Advertising();
            StringBuilder str = new StringBuilder();
            foreach (SOSOshop.BLL.Advertising a in ad.GetList())
            {
                str.AppendFormat(@"<tr><td style='line-height: 23px'>{0}</td><td>{1}</td><td>{2}</td></tr>", a.Code, a.Title, a.ProductID == null ? "" : GetProductName(a.ProductID, a.Code));
            }
            Response.Write(str);
        %>
    </table>
    <script language="javascript" type="text/javascript">
        var pid = 0;
        $('#ul').hide(); $('#ul').hover(  //鼠标滑过下拉列表自身也要显示，防止无法点击下拉列表
  function () {
      $('#ul').show();
  },
  function () {
      $('#ul').hide();
  }
  );
        function del() {            
        }
        function shows(v, id, code) {
            $("#workspace_Code").val(code);
            $("#workspace_PorductId").val(id);
            pid = id;
            $($("#ul")).insertAfter(v);
            $('#ul').css({ left: $(v).offset().left + 'px' });
            $('#ul').show();

        }
        function hides(v) {
            $('#ul').hide();
        }

    </script>
</asp:Content>
