function IsFilingSell(str)
{
    if(str==1)
    {
        document.getElementById("ctl00_workspace_trFilingTime").style.display="";
        document.getElementById("ctl00_workspace_trDownTime").style.display="";
        document.getElementById("ctl00_workspace_trFilingCount").style.display="";
    }
    else
    {
        document.getElementById("ctl00_workspace_trFilingTime").style.display="none";
        document.getElementById("ctl00_workspace_trDownTime").style.display="none";
        document.getElementById("ctl00_workspace_trFilingCount").style.display="none";
    }
}

function ReturnProductInfo(Return_Strs)
{
    if(!confirm("是否根据所选分类的默认类型重新设定商品类型？\n如果重设，可能丢失当前所输入的类型属性、关联品牌、参数表等类型相关数据。"))
    {
        return  false;
    }
    if(isArray(LastSelectObj))
    {
        //传递ID
        if (LastSelectObj[0]==null || typeof(LastSelectObj[0])=="undefined")
        {
            alert("选择失败，请重新选择。");
        }
        else
        {
            if(Return_Strs[0] == null || typeof(Return_Strs[0])=="undefined")
                Return_Strs[0] = '';
            LastSelectObj[0].value = Return_Strs[0];
            
            var param = "Option=ProductTypeId&ID=" +  Return_Strs[0];
            var options = 
            { method: 'post',parameters: param,onComplete:
                 function(transport)
                 {
                     var retv = transport.responseText;
                     if(retv!="")
                     {
                         document.getElementById("selProductType").value=retv;
                         ProductProperty(retv);
                         ProductParameter(retv);
                         ProductBrand(retv);
                     }
                 }
            }
            new Ajax.Request('product_info_edit.aspx', options);
        }	
        
        //传递值
        if (LastSelectObj[1]==null || typeof(LastSelectObj[1])=="undefined")
        {
            alert("选择失败，请重新选择。");
        }
        else
        {
            LastSelectObj[1].value = Return_Strs[1];
            if(Return_Strs[1] == null || typeof(Return_Strs[1])=="undefined")
            {
                Return_Strs[1] = '';
            }
        }	
        
    }
    else
    {
        if (LastSelectObj==null || typeof(LastSelectObj)=="undefined")
        {
            alert("选择失败，请重新选择。");
        }
        else
        {
            if(Return_Strs == null || typeof(Return_Strs)=="undefined")
                Return_Strs = '';
            LastSelectObj.value = Return_Strs;
        }	
    }
    document.getElementById("s_id").style.display="none";
}

function ProductProperty(id)
{
    var param = "Option=ProductPropertyId&ID=" + id;
    var options = 
    { method: 'post',parameters: param,onComplete:
         function(transport)
         {
             var retv = transport.responseText;
             if(retv!="")
             {
                 document.getElementById("ctl00_workspace_TabTitle4").style.display="";
                 document.getElementById("ctl00_workspace_tab4").innerHTML=retv;
             }
             else
             {
                 document.getElementById("ctl00_workspace_TabTitle4").style.display="none";
                 document.getElementById("ctl00_workspace_tab4").innerHTML=retv;
             }
         }
    }
    new Ajax.Request('product_info_edit.aspx', options);
}

function ProductParameter(id)
{
    var param = "Option=ProductParameterId&ID=" + id;
    var options = 
    { method: 'post',parameters: param,onComplete:
         function(transport)
         {
             var retv = transport.responseText;
             if(retv!="")
             {
                 document.getElementById("ctl00_workspace_TabTitle5").style.display="";
                 document.getElementById("ctl00_workspace_tab5").innerHTML=retv;
             }
             else
             {
                 document.getElementById("ctl00_workspace_TabTitle5").style.display="none";
                 document.getElementById("ctl00_workspace_tab5").innerHTML=retv;
             }
         }
    }
    new Ajax.Request('product_info_edit.aspx', options);
}

function ProductBrand(id)
{
    var param = "Option=ProductBrandId&ID=" + id;
    var options = 
    { method: 'post',parameters: param,onComplete:
         function(transport)
         {
             var retv = transport.responseText;
             if(retv!="")
             {
                 document.getElementById("divBrand").innerHTML=retv;
             }
         }
    }
    new Ajax.Request('product_info_edit.aspx', options);
}

function ProductTypeId(str)
{
    if(!confirm("切换类型将会有可能无法保存当前所输入的属性内容，确定要切换吗？"))
    {
        return  false;
    }
    ProductProperty(str);
    ProductParameter(str);
    ProductBrand(str);
}