<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="_101shop.admin.v3.admin.JF.Display" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script src="../scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/admin/scripts/ajax.js"></script>
    <script language="javascript" type="text/javascript">
        function Del(id) {
            if (confirm('确定要永久删除该信息吗?删除后将不能被恢复!')) {
                var param = "operate=del&id=" + id;
                var options =
            { method: 'POST', parameters: param, onComplete:
                 function (transport) {
                     alert("删除成功");
                     location.reload();
                 }
            }
            }
            else {
                return false;
            }
            new Ajax.Request('display.aspx', options);
        }
    </script>
    <style type="text/css">
        td
        {
            font-size: 12px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            padding: 1px 1px 4px 4px;
            height: 22px;
        }
        div
        {
            font-size: 12px;
        }
        .textOverFlow
        {
            display: block; /*内联对象需加*/
            width: 150px;
            word-break: keep-all; /* 不换行 */
            white-space: nowrap; /* 不换行 */
            overflow: hidden; /* 内容超出宽度时隐藏超出部分的内容 */
            text-overflow: ellipsis; /* 当对象内文本溢出时显示省略标记(...) ；需与overflow:hidden;一起使用。*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" action="/admin/JF/Display.aspx">
    <!--内容搜索-->
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            商品名称：
            <asp:TextBox ID="name" runat="server" Width="105px"></asp:TextBox>
            商品属性：
            <asp:TextBox ID="pt" runat="server" Width="104px"></asp:TextBox>
            商品积分：
            <asp:TextBox ID="jf" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" CssClass="inputbutton" Text="查 询" OnClick="Button1_Click1"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'" />
            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="inputbutton" NavigateUrl="Add.aspx"
                Width="53px" Height="20px" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">添加</asp:HyperLink><br />
        </div>
    </div>
    <!--内容列表-->
    <div>
        <table class="datatable" cellspacing="0" rules="all" border="1" id="tablist" style="border-collapse: collapse;
            width: 100%">
            <tbody>
                <tr>
                    <th scope="col" style="width: 120px; text-align: center;">
                        序号
                    </th>
                    <th scope="col" style="text-align: center;">
                        商品名称
                    </th>
                    <th scope="col" style="width: 150px; text-align: center;">
                        积分
                    </th>
                    <th scope="col" style="text-align: center;">
                        商品属性
                    </th>
                    <th scope="col" style="text-align: center;">
                        图片存储路径
                    </th>
                    <th scope="col" style="width: 60px; text-align: center;">
                        操作
                    </th>
                </tr>
            </tbody>
            <%if (testList != null && testList.Count > 0)
              {
            %>
            <%foreach (var item in testList)
              {
            %>
            <tr>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;" class="style1">
                    <%=item.id%>
                </td>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;">
                    <%=item.name%>
                </td>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;">
                    <%=item.jf%>
                </td>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;">
                    <%=item.pt%>
                </td>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;">
                    <%=item.img%>
                </td>
                <td style="text-align: center; border-color: #BAC9C6; border-style: solid; border-width: 1px;
                    border-bottom: none;">
                    <a href="Edit.aspx?id=<%=item.id %>">编辑</a> <a href='javascript:void(0)' onclick='Del(<%=item.id %>)'>
                        删除</a>
                </td>
            </tr>
            <%
              } 
            %>
            <%
              }
              else
              { 
            %>
            <tr>
                <td colspan="6" style="text-align: center; color: Red;">
                    <h3>暂无该商品</h3>
                </td>
            </tr>
            <%
              }
            %>
        </table>
        <div style="width:100%; margin-bottom:60px;">
        <div class="quotes" style="float:right;">
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
    </div>
   <hr style="color:#f5f5f5; border-bottom:2px;" />
    <div class="page_toolbar" style="text-align: center;">
        <span class="copyright">2015 蓉锦医药网 V1.0</span></div>
    </form>
    <script type="text/javascript">        document.write('<script type="text/javascript" src="' + location.href.substring(0, location.href.lastIndexOf("admin/") + 6) + 'scripts/RegisterToRemind.js"><' + '/script>')</script>
</body>
</html>
