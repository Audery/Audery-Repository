using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

public class SignHelper
{
    //生成待签名的字符串
    public static string getSignatureContent(Dictionary<string, string> dic)
    {
        StringBuilder content = new StringBuilder();
        List<string> keys = new List<string>();
        foreach (string temp in dic.Keys)
        {
            keys.Add(temp);
        }
        keys.Sort();
        for (int i = 0; i < keys.Count; i++)
        {
            string key = keys[i];
            string value = dic[key];
            //判断value是否为空
            if (value != null && value.Length > 0)
            {
                content.Append((i == 0 ? "" : "&") + key + "=" + value);
            }
           
        }
        Console.WriteLine("待签名内容：" + content.ToString());
        return content.ToString();
    }
    //生成32位md5摘要
    public static string MD5Encrypt(string str)
    {
        byte[] result = Encoding.UTF8.GetBytes(str);     
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(result);
        string Text = BitConverter.ToString(output).Replace("-", "").ToLower(); 
        return Text;
    }
    //签名
    public static string sign(Dictionary<string, string> param, string key)
    {
        string content = getSignatureContent(param) + key;
        return MD5Encrypt(content);
    }
    //验签  
    public static Boolean checkSign(Dictionary<string, string> param, string key, string sign) { 
        if( null != param && key.Length >0 && key != null && sign.Length >0 && sign != null){
            string signed = SignHelper.sign(param, key);
            Console.WriteLine("验签 signed ：" + signed);
            Console.WriteLine("验签 sign ：" + sign);
            if (signed.Equals(sign))
                return true;
        }
        return false;
    }

    //生成url值
    public static string dicToUrl(Dictionary<string, string> dic)
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, string> pair in dic)
        {

            sb.Append(pair.Key);
            sb.Append("=");

            Encoding utf8 = Encoding.UTF8;
            string encode = HttpUtility.UrlEncode(pair.Value, utf8);
            sb.Append(encode);
            sb.Append("&");

        }

        string urlTemp = sb.ToString();
        return urlTemp;
    }

    //将url值写入dic
    public static Dictionary<string, string> urlToDic( string returnUrl) { 
        Dictionary<string, string> dic = new Dictionary<string,string>();
        int splitIndex = returnUrl.IndexOf("?");
        if(splitIndex == -1){
    		Console.WriteLine("returnUrl格式不正确:" + returnUrl);
    		return null;
    	}
        returnUrl = returnUrl.Substring(splitIndex + 1);
        string[] tokens = returnUrl.Split('&');
        foreach (string token in tokens){
            String key = token.Substring(0, token.IndexOf("="));
			String value = token.Substring(token.IndexOf("=") + 1);
            if(key == "md5_sign"){	//签名字段无需做url_decode
				dic.Add(key, value);
			}else{	//其他字段需做url_decode
                value = HttpUtility.UrlDecode(value, Encoding.UTF8);
				dic.Add(key, value);
			    Console.WriteLine(key + "   " + value);
			}
        }
        return dic;
    } 
}













