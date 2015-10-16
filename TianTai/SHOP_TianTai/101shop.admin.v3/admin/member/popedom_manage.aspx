<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" AutoEventWireup="true" CodeBehind="popedom_manage.aspx.cs" Inherits="_101shop.admin.v3.member.popedom_manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <script type="text/javascript">
function client_OnTreeNodeChecked()
{
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    var checkedState;
    if (obj.tagName == "INPUT" && obj.type == "checkbox")
    {
        var treeNode = obj;
        checkedState = treeNode.checked;
        do
        {
            obj = obj.parentElement;
        }
        while (obj.tagName != "TABLE")
        var parentTreeLevel = obj.rows[0].cells.length;
        var parentTreeNode = obj.rows[0].cells[0];
        var tables = obj.parentElement.getElementsByTagName("TABLE");
        var numTables = tables.length
        if (numTables >= 1)
        {
            for (i=0; i < numTables; i++)
            {
                if (tables[i] == obj)
                {
                    treeNodeFound = true;
                    i++;
                    if (i == numTables)
                    {
                        return;
                    }
                }
                if (treeNodeFound == true)
                {
                    var childTreeLevel = tables[i].rows[0].cells.length;
                    if (childTreeLevel > parentTreeLevel)
                    {
                        var cell = tables[i].rows[0].cells[childTreeLevel - 1];
                        var inputs = cell.getElementsByTagName("INPUT");
                        inputs[0].checked = checkedState;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
//选择或者取消祖先结点的选择
function client_OnTreeNodeChecked1()
{
    var obj = window.event.srcElement;
    var treeNodeFound = false;
    var checkedState;
    if (obj.tagName == "INPUT" && obj.type == "checkbox")
    {
        var treeNode = obj;
        checkedState = treeNode.checked;
        do
        {
            obj = obj.parentElement;
        }
        while (obj.tagName != "TABLE")
        var objT=obj;
        do
        {
            obj = obj.parentElement;
        }
        while (obj.tagName != "DIV")
        checkParNodes(obj,objT,checkedState);
    }
}
    function checkParNodes(obj,objT,checkedState)
	{
	    if(obj==null||obj==undefined)
	    {  
	        return;
	    }
	    var unChecked=true;
	    var treeNodeFound = false;
        if(checkedState==false)
        {
            var baseTreeLevel=objT.rows[0].cells.length;
            var tables = objT.parentElement.getElementsByTagName("TABLE");
            var numTables = tables.length;
            if (numTables >= 1)
            {
                for (i=0; i < numTables; i++)
                {
                        var childTreeLevel = tables[i].rows[0].cells.length;
                        if(baseTreeLevel!=childTreeLevel)
                        {
                            continue;
                        }
                        var cell = tables[i].rows[0].cells[childTreeLevel - 1];
                        var inputs = cell.getElementsByTagName("INPUT");
                        if(inputs[0].checked)
                        {
                            unChecked=false;
                            break;    
                        }
                        if (i == numTables)
                        {
                            break;
                        }
                }
            }
        }
        if(unChecked==true){
		var id=obj.id.replace("Nodes","");
		var pObj=document.getElementById(id+"CheckBox");
		if(pObj==null)
		{
		    return;
		}
		pObj.checked=checkedState;
       
	        if (pObj.tagName == "INPUT" && pObj.type == "checkbox")
            {
                 do
                {
                    pObj =pObj.parentElement;
                }
                while (pObj.tagName != "TABLE")
                var pObjT=pObj;
                do
                {
                    pObj = pObj.parentElement;
                }
                while (pObj.tagName != "DIV")
                checkParNodes(pObj,pObjT,checkedState);
            }
		    
	    }
	    }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">角色管理
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" onclick="Button1_Click" Text="保  存" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'" onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'"/>
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="role_list.aspx"
                Width="65px" Height="24px" style="vertical-align:bottom;" onMouseOver="javascript:document.getElementById(this.id).className='inputbutton_a'"
                onMouseOut="javascript:document.getElementById(this.id).className='inputbutton'">返回列表</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageinfo" runat="server">权限设置，角色名称：<asp:Label ID="RoleName" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
<table  class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
   <tr>
       <td colspan="2" style=" font-weight:bold;" height="22px">
           <asp:Label ID="showMSG" runat="server" Text="选择详细权限树"></asp:Label>
       </td>
   </tr>
   <tr>
       <td>
            <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" 
                ShowCheckBoxes="All" 
                onclick="client_OnTreeNodeChecked();client_OnTreeNodeChecked1();" BorderStyle="None" 
                ExpandDepth="0" ImageSet="Arrows" ondatabound="TreeView1_DataBound" Width="100%" >
                <DataBindings>
                    <asp:TreeNodeBinding TextField="Text" ValueField="Value" />
                </DataBindings>
            </asp:TreeView>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Admin/xml/power_list.xml" XPath="//item"></asp:XmlDataSource>
        </td>
        <td>
        </td>
    </tr>
    <tr style="display:none;">
        <td> 
            <span style=" font-weight:bold;" height="22px"><asp:Label ID="Label1" runat="server" Text="特殊权限选择"></asp:Label></span>
            <div id="especial">
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" 
                AutoPostBack="True" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <asp:ListItem Value="look">查看</asp:ListItem>
                <asp:ListItem Value="add">添加</asp:ListItem>
                <asp:ListItem Value="update">修改</asp:ListItem>
                <asp:ListItem Value="del">删除</asp:ListItem>
            </asp:CheckBoxList>
            </div>
        </td>
    </tr>
</table>
</asp:Content>

