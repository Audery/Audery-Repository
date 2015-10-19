var CurrentTR = null;
var ConTxt = '';
function ChangeDbTable(obj,flag)
{
    var val = obj.options[obj.selectedIndex].value;
    
    var tbd = document.getElementById('TBD'+ flag);
    var tbl = document.getElementById('TBL'+ flag);
    var len = tbl.rows.length;
    for(var i=1;i<len;i++)
    {
        tbl.deleteRow(1);
    }
    var oth = 'SelPrin';
    if(flag == 0)
    {
        oth = 'SelSub';
        document.getElementById('SelJoinPrin').options.length = 1;
    }
    else
    {
        document.getElementById('SelJoinSub').options.length = 1;
    }
    
    if(val == '0')
        return;
    
    var objoth = document.getElementById(oth);
    if(objoth.options[objoth.selectedIndex].value == val)
    {
        alert('主从表不能选择同一个数据表!');
        obj.selectedIndex = 0;
        return;
    }
    var tr = document.createElement('tr');
    tbd.appendChild(tr);
    var td = document.createElement('td');
    td.align = 'center';
    td.innerHTML = '正在读取数据,请稍候...';
    tr.appendChild(td);
    var options={
        method:'post',
        parameters:'Option=GetFields&TableName='+ val,
        onComplete:
            function(transport)
	        {
	            var retv =transport.responseText;
	            OnGetFields(retv,flag);
	        }
	    }
    new  Ajax.Request('freedomlabel_edit.aspx',options);
}
function OnGetFields(retv,flag)
{
    var tbd = document.getElementById('TBD'+ flag);
    var tbl = document.getElementById('TBL'+ flag);
    var objjn;
    if(flag == 0)
        objjn = document.getElementById('SelJoinPrin');
    else
        objjn = document.getElementById('SelJoinSub');
    if(tbl.rows.length > 1)
        tbl.deleteRow(1);
    var ret = retv.split(';');
    for(var i = 0;i < ret.length; i++)
    {
        var fld = ret[i].split(',');
        var tr = document.createElement('tr');
        tr.style.cursor = 'hand';
        tr.onclick = function(){RowClick(this);};
        tr.setAttribute('height','22');
        tbd.appendChild(tr);
        var td1 = document.createElement('td');
        td1.innerHTML = fld[0];
        tr.appendChild(td1);
        var td2 = document.createElement('td');
        td2.innerHTML = fld[1];
        tr.appendChild(td2);
        var td3 = document.createElement('td');
        td3.align = 'center';
        tr.appendChild(td3);
        var td4 = document.createElement('td');
        td4.align = 'center';
        tr.appendChild(td4);
        var td5 = document.createElement('td');
        td5.align = 'center';
        tr.appendChild(td5);
        var op = document.createElement('option');
        op.value = fld[0];
        op.innerHTML = fld[0];
        objjn.appendChild(op);
    }
}
function TxtFocus(obj)
{
    ConTxt = obj.value;
}
function TxtBlur(obj)
{
    if(obj.value != ConTxt)
    {
        ChangeSql(obj);
    }
}
function RowClick(obj)
{
    if(obj != CurrentTR)
    {
        if(CurrentTR != null)
            HidOld(CurrentTR);
        //第三列
        if(obj.childNodes[2].firstChild != null && obj.childNodes[2].firstChild.type == 'checkbox')
            return;
        obj.className = 'TR_BG';
        var td3 = obj.childNodes[2];
        var dis = td3.innerHTML;
        td3.innerHTML = '';
        var ele_check_3 = document.createElement('input');
        ele_check_3.setAttribute('type','checkbox');
        ele_check_3.onclick = function(){ChangeSql(this);};
        td3.appendChild(ele_check_3);
        if(dis == '√')
        {
            ele_check_3.checked = true;
        }
        //第四列
        var td4 = obj.childNodes[3];
        var con = td4.innerHTML;
        td4.innerHTML = '';
        var con1 = '',con2 = '';
        if(con != '')
        {
            var pos = con.indexOf(' ');
            con1 = con.substr(0,pos);
            con2 = con.substr(pos+1);
        }
        var ele_sel_4 = document.createElement('select');
        ele_sel_4.onclick = function(){ChangeSql(this);};
        td4.appendChild(ele_sel_4);
        for(var i=0;i<9;i++)
        {
            var opt = document.createElement('option');
            switch(i)
            {
                case 0:
                    opt.value = '';
                    opt.innerHTML = '';
                    break;
                case 1:
                    opt.value = '<>';
                    opt.innerHTML = '&lt;&gt;';
                    break;
                case 2:
                    opt.value = '<=';
                    opt.innerHTML = '&lt;=';
                    break;
                case 3:
                    opt.value = '>=';
                    opt.innerHTML = '&gt;=';
                    break;
                case 4:
                    opt.value = '<';
                    opt.innerHTML = '&lt;';
                    break;
                case 5:
                    opt.value = '>';
                    opt.innerHTML = '&gt;';
                    break;
                case 6:
                    opt.value = '=';
                    opt.innerHTML = '=';
                    break;
                case 7:
                    opt.value = 'in';
                    opt.innerHTML = 'in';
                    break;
                case 8:
                    opt.value = 'like';
                    opt.innerHTML = 'like';
                    break;
            }
            if(con1 == opt.innerHTML)
                opt.setAttribute('selected','selected');
            ele_sel_4.appendChild(opt);
        }
        var ele_txt_4 = document.createElement('input');
        //onblur onfocus
        ele_txt_4.setAttribute('type','text');
        ele_txt_4.setAttribute('size','5');
        ele_txt_4.setAttribute('value',con2);
        ele_txt_4.onfocus = function(){TxtFocus(this);};
        ele_txt_4.onblur = function(){TxtBlur(this);};
        td4.appendChild(ele_txt_4);
        //第五列
        var td5 = obj.childNodes[4];
        var ord = td5.innerHTML;
        td5.innerHTML = '';
        var ele_sel_5 = document.createElement('select');
        ele_sel_5.onclick = function(){ChangeSql(this);};
        td5.appendChild(ele_sel_5);
        for(var i=0;i<3;i++)
        {
            var opt = document.createElement('option');
            switch(i)
            {
                case 0:
                    opt.value = '';
                    opt.innerHTML = '';
                    break;
                case 1:
                    opt.value = 'ASC';
                    opt.innerHTML = '升序';
                    break;
                case 2:
                    opt.value = 'DESC';
                    opt.innerHTML = '降序';
                    break;
            }
            if(ord == opt.innerHTML)
                opt.setAttribute('selected','selected');
            ele_sel_5.appendChild(opt);
        }
        CurrentTR = obj;
    }
}
function HidOld(obj)
{
    obj.className = null;
    //第三列
    var td3 = obj.childNodes[2];
    if(td3.firstChild != null && td3.firstChild.type == 'checkbox')
    {
        var ch = td3.firstChild;
        var str1 = '';
        if(ch.checked)
            str1 = '√';
        td3.removeChild(td3.firstChild);
        td3.innerHTML = str1;
    }
    //第四列
    var td4 = obj.childNodes[3];
    if(td4.firstChild != null && td4.firstChild.type == 'select-one' && td4.lastChild != null && td4.lastChild.type == 'text')
    {
        var selc = td4.firstChild;
        var txt = td4.lastChild;
        var s = selc.options[selc.selectedIndex].value;
        var t = txt.value;
        td4.removeChild(td4.firstChild);
        td4.removeChild(td4.lastChild);
        if(s != '' && t != '')
        {
            td4.innerHTML = s +' '+ t;
        }
    }
    //第五列
    var td5 = obj.childNodes[4];
    if(td5.firstChild != null && td5.firstChild.type == 'select-one')
    {
        var sel = td5.firstChild;
        var ord = sel.options[sel.selectedIndex].innerHTML;
        td5.removeChild(sel);
        td5.innerHTML = ord;
    }
}
function ChangeSql(obj)
{
    var otb0 = document.getElementById('SelPrin');
    var otb1 = document.getElementById('SelSub');
    var tb0 = otb0.options[otb0.selectedIndex].value;
    var tb1 = otb1.options[otb1.selectedIndex].value;
    var ojn0 = document.getElementById('SelJoinPrin');
    var ojn1 = document.getElementById('SelJoinSub');
    var jn0 = ojn0.options[ojn0.selectedIndex].value;
    var jn1 = ojn1.options[ojn1.selectedIndex].value;
 //   var num = document.getElementById('TxtNum').value;
    var flag = false;
    if(tb0 != '0' && tb1 != '0' && jn0 != '0' && jn1 != '0')
        flag = true;
    var sql1 = null;
    var sql2 = null;
    if(tb0 != '0')
    {
        var tbl0 = document.getElementById("TBL0");
        sql1 = GenerateSql(tbl0,tb0,flag);
    }
    if(tb1 != '0')
    {
        var tbl1 = document.getElementById("TBL1");
        sql2 = GenerateSql(tbl1,tb1,flag);
    }
    var sql = '';
    var sqlwh = '';
    var sqlrd = '';
    if(flag)
    {
        var s = '';
        if(sql1[0] != '')
        {
            s = sql1[0];        
        }
        if(sql2[0] != '')
        {
            if(s != '')
                s += ',';
            s += sql2[0];
            
        }
        if(s != '')
        {
            sql = 'select '
//            if(num != '')
//                sql += 'top '+ num +' ';
            sql +=  s +' from '+ tb0 +','+ tb1 +' where '+ tb0 +'.'+ jn0 +'='+ tb1 +'.'+ jn1;
            if(sql1[1] != '')
                sql += ' and '+ sql1[1];
            if(sql2[1] != '')
                sql += ' and '+ sql2[1];
            if(sql1[2] != '')
            {
                sql += ' order by '+ sql1[2];
            }
            if( sql2[2] != '')
            {
                if(sql1[2] != '')
                    sql += ','+ sql2[2];
                else
                    sql += ' order by '+ sql2[2];
            }
        }
    }
    else
    {
        var asq = null;
        var dtb;
        if(sql1 != null)
        {
            asq = sql1;
            dtb = tb0;
        }
        else if(sql2 != null)
        {
            asq = sql1;
            dtb = tb0;
        }
        if(asq != null)
        {
            if(asq[0] != '')
            {
                sql = 'select '
//                if(num != '')
//                    sql += 'top '+ num +' ';
                sql += asq[0] +' from '+ dtb;
                if(asq[1] != '')
                    sql += ' where '+ asq[1];
                if(asq[2] != '')
                    sql += ' order by '+ asq[2];
            }
        }
    }
    
    document.getElementById("TxtSql").value = sql;
    
}
function GenerateSql(tbl,tb,flag)
{
    var sqls = '';
    var sqlw = '';
    var sqld = '';
    var n = tbl.rows.length;
    for(var i=1;i<n;i++)
    {
        var tr = tbl.rows[i];
        if(tr == null)
            continue;
        var fld = tr.cells[0].innerHTML;
        
        var obj1 = tr.cells[2].firstChild;
        var bdis = false;
        if(obj1 != null && obj1.type == 'checkbox')
        {
            bdis = obj1.checked;
        }
        else
        {
            if(tr.cells[2].innerHTML == '√')
                bdis = true;
        }
        
        var obj2 = tr.cells[3].firstChild;
        var obj3 = tr.cells[3].lastChild;
        var con1 = '';
        var con2 = '';
        if(obj2 != null && obj3 != null && obj2.type == 'select-one' && obj3.type == 'text')
        {
            con1 = obj2.options[obj2.selectedIndex].value;
            con2 = obj3.value;
        }
        else
        {
            var con = tr.cells[3].innerHTML;
            if(con != '')
            {
                var pos = con.indexOf(' ');
                con1 = con.substr(0,pos);
                con2 = con.substr(pos+1);
            }
        }
        
        var obj4 = tr.cells[4].firstChild;
        var ordr = '';
        if(obj4 != null && obj4.type == 'select-one')
        {
            ordr = obj4.options[obj4.selectedIndex].value;
        }
        else
        {
            var ord = tr.cells[4].innerHTML;
            if(ord == '升序')
                ordr = 'ASC';
            else if(ord == '降序')
                ordr = 'DESC';
        }
        
        if(flag)
            fld = tb +'.'+ fld;
        //选中某字段
        if(bdis)
        {
            if(sqls != '')
                sqls += ',';
            sqls += fld;
        }
        if(con1 != '' && con2 != '')
        {
            if(sqlw != '')
                sqlw += ' and ';
            con1 = con1.replace('&lt;','<');
            con1 = con1.replace('&gt;','>');
            sqlw += fld +' '+ con1 +' '+ con2;
        }
        if(ordr != '')
        {
            if(sqld != '')
                sqld += ',';
            sqld += fld +' '+ ordr;
        }
    }
    var ret = new Array(3);
    ret[0] = sqls;
    ret[1] = sqlw;
    ret[2] = sqld;
    return ret;
    
}
