function onName()
	{
	    if($("regusername").value=="")
		{
		   
		   $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的用户名！";
		   $("spname").style.color="#000000";
		   return false;
		}
		else if($("regusername").value.length<4||$("regusername").value.length>20)
		{
		     $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您的用户名长度只能在4-20位字符之间!";
			 $("spname").style.color="#000000";
		   return false;
		}
		else
		{
		  var param = "Option=Main&UserName="+$("regusername").value;
         var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
	      var retv=transport.responseText;
	      if(retv!="")
	      {
	         $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;"+retv+"";
	      }
	      else
	      {
	         $("spname").innerHTML="<img src=\'/images/check_right.gif\'/>";
	      }
	      $("spname").style.color="#000000";
		}     
	    }
	   new  Ajax.Request('/filehandle/userregform.ashx',options);
		}
		
	}
	function onuserpwd()
	{
	    if($("reguserpwd").value=="")
		{
		   $("sppwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请设置您密码！";
		   $("sppwd").style.color="#000000";
		   return false;
		}
		else if($("reguserpwd").value.length<4||$("reguserpwd").value.length>16)
		{
		     $("sppwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您设置密码长度只能在4-16位字符之间!";
			 $("sppwd").style.color="#000000";
		   return false;
		}
		else
		{
		  $("sppwd").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRepwd()
	{
	   if($("regrepwd").value!=$("reguserpwd").value)
		{
		   $("sprepwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;两次输入的密码不一致！";
		   $("sprepwd").style.color="#000000";
		   return false;
		}
		else
		{
		  $("sprepwd").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	}
	
	function oninsolution()
	{
	    if($("insolution").value=="")
		{
		   $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入答案！";
		   $("spinsolution").style.color="#000000";
		   return false;
		}
		else if($("insolution").value.length<6||$("insolution").value.length>30)
		{
		     $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您答案长度只能在6-30位字符之间!";
			 $("spinsolution").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spinsolution").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function oninsolution()
	{
	    if($("question").value=="")
		{
		   $("spquestion").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必选项，请选择密码保护问题！";
		   $("spquestion").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spquestion").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	    if($("insolution").value=="")
		{
		   $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入答案！";
		   $("spinsolution").style.color="#000000";
		   return false;
		}
		else if($("insolution").value.length<6||$("insolution").value.length>30)
		{
		     $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您答案长度只能在6-30位字符之间!";
			 $("spinsolution").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spinsolution").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onEmail()
	{
	   var reg =/\w+@\w+\.\w+/;
	   if($("Email").value=="")
		{
		   $("spEmail").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入E_mali！";
		   $("spEmail").style.color="#000000";
		   return false;
		}
		else if(!reg.test($("Email").value))
		{
		     $("spEmail").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您输入Email格式不正确!";
			 $("spEmail").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spEmail").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegName()
	{
	   if($("RegName").value=="")
		{
		   $("spRegName").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的真实姓名！";
		   $("spRegName").style.color="#000000";
		   return false;
		}
		else if($("RegName").value.length<2||$("RegName").value.length>8)
		{
		     $("spRegName").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您真实姓名长度只能在2-8位字符之间!";
			 $("spRegName").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegName").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegEmailTM()
	{
	   if($("RegEmailTM").value=="")
		{
		   $("spRegEmailTM").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的电子邮箱TM号！";
		   $("spRegEmailTM").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegEmailTM").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegTel()
	{
	   if($("RegTel").value=="")
		{
		   $("spRegTel").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的固定电话！";
		   $("spRegTel").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegTel").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegCompanyAddress()
	{
	   if($("RegCompanyAddress").value=="")
		{
		   $("spRegCompanyAddress").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的固定电话！";
		   $("spRegCompanyAddress").style.color="#000000";
		   return false;
		}
		else if($("RegCompanyAddress").value.length<4||$("RegCompanyAddress").value.length>50)
		{
		     $("spRegCompanyAddress").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您公司地址长度只能在4-50位字符之间!";
			 $("spRegCompanyAddress").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegCompanyAddress").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegMSN()
	{
	   if($("RegMSN").value=="")
		{
		   $("spRegMSN").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的MSN！";
		   $("spRegMSN").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegMSN").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegStreet()
	{
	   if($("RegStreet").value=="")
		{
		   $("spRegStreet").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入街道地址！";
		   $("spRegStreet").style.color="#000000";
		   return false;
		}
		else if($("RegStreet").value.length<4||$("RegStreet").value.length>50)
		{
		     $("spRegStreet").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;街道地址长度只能在4-50位字符之间!";
			 $("spRegStreet").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegStreet").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegFax()
	{
	   if($("RegFax").value=="")
		{
		   $("spRegFax").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的传真！";
		   $("spRegFax").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegFax").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegPhone()
	{
	   if($("RegPhone").value=="")
		{
		   $("spRegPhone").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的手机！";
		   $("spRegPhone").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegPhone").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegCode()
	{
	   if($("RegCode").value=="")
		{
		   $("spRegCode").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的邮编！";
		   $("spRegCode").style.color="#000000";
		   return false;
		}
		else if($("RegCode").value.length!=6)
		{
		     $("spRegCode").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;邮编格式不正确!";
			 $("spRegCode").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegCode").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegCompanyName()
	{
	   if($("RegCompanyName").value=="")
		{
		   $("spRegCompanyName").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的邮编！";
		   $("spRegCompanyName").style.color="#000000";
		   return false;
		}
		else if($("RegCompanyName").value.length<4||$("RegCompanyName").value.length>50)
		{
		     $("spRegCompanyName").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;公司名称长度只能在4-50位字符之间!";
			 $("spRegCompanyName").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegCompanyName").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegPost()
	{
	   if($("RegPost").value=="")
		{
		   $("spRegPost").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的邮编！";
		   $("spRegPost").style.color="#000000";
		   return false;
		}
		else if($("RegPost").value.length<4||$("RegPost").value.length>16)
		{
		     $("spRegPost").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您的职位长度只能在4-16位字符之间!";
			 $("spRegPost").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegPost").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	function onRegverify()
	{
	   if($("Regverify").value=="")
		{
		   $("spRegverify").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入验证码！";
		   $("spRegverify").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spRegverify").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	}
	
	function RegionVerify()
	{
	    if($("regusername").value=="")
		{
		   $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入您的用户名！";
		   $("spname").style.color="#000000";
		   return false;
		}
		else if($("regusername").value.length<4||$("regusername").value.length>20)
		{
		     $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您的用户名长度只能在4-20位字符之间!";
			 $("spname").style.color="#000000";
		   return false;
		}
		else
		{
		var param = "Option=Main&UserName="+$("regusername").value;
         var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
	      var retv=transport.responseText;
	      if(retv!="")
	      {
	         $("spname").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;"+retv+"";
	      }
	      else
	      {
	         $("spname").innerHTML="<img src=\'/images/check_right.gif\'/>";
	      }
	      $("spname").style.color="#000000";
	      $("spname").style.color="#000000";
		}     
	    }
	         new  Ajax.Request('/filehandle/userregform.ashx',options);
		}
		if($("spname").innerText!="")
		{
		   return false;
		}
		else
		{
		   $("spname").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	    if($("reguserpwd").value=="")
		{
		   $("sppwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请设置您密码！";
		   $("sppwd").style.color="#000000";
		   return false;
		}
		else if($("reguserpwd").value.length<4||$("reguserpwd").value.length>16)
		{
		     $("sppwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您设置密码长度只能在4-16位字符之间!";
			 $("sppwd").style.color="#000000";
		   return false;
		}
		else
		{
		  $("sppwd").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	   if($("regrepwd").value!=$("reguserpwd").value)
		{
		   $("sprepwd").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;两次输入的密码不一致！";
		   $("sprepwd").style.color="#000000";
		   return false;
		}
		else
		{
		  $("sprepwd").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	    if($("insolution").value=="")
		{
		   $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入答案！";
		   $("spinsolution").style.color="#000000";
		   return false;
		}
		else if($("insolution").value.length<6||$("insolution").value.length>30)
		{
		     $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您答案长度只能在6-30位字符之间!";
			 $("spinsolution").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spinsolution").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	    if($("question").value=="")
		{
		   $("spquestion").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必选项，请选择密码保护问题！";
		   $("spquestion").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spquestion").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
	    if($("insolution").value=="")
		{
		   $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入答案！";
		   $("spinsolution").style.color="#000000";
		   return false;
		}
		else if($("insolution").value.length<6||$("insolution").value.length>30)
		{
		     $("spinsolution").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您答案长度只能在6-30位字符之间!";
			 $("spinsolution").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spinsolution").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		
	   var reg =/\w+@\w+\.\w+/;
	   if($("Email").value=="")
		{
		   $("spEmail").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;此项为必填项，请输入E_mali！";
		   $("spEmail").style.color="#000000";
		   return false;
		}
		else if(!reg.test($("Email").value))
		{
		     $("spEmail").innerHTML="<img src=\'/images/check_error.gif\'/>&nbsp;您输入Email格式不正确!";
			 $("spEmail").style.color="#000000";
		   return false;
		}
		else
		{
		  $("spEmail").innerHTML="<img src=\'/images/check_right.gif\'/>";
		}
		if($("cbagreement").checked==false)
		{
		  if(confirm("是否同意商城服务协议?"))
		  {
		      return false;
		  }
		  else
		  {
		     window.history.back();
		  }
		}
		
	}
	
	