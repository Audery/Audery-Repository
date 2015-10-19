using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace _101shop.admin.v3.filehandle
{
    /// <summary>
    /// pinyin 的摘要说明
    /// </summary>
    public class pinyin : IHttpHandler
    {
        /// <summary>
        /// 编码格式设置
        /// </summary>
        public static Encoding encoding = Encoding.UTF8;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = context.Request.Form["str"]; if (str == null && context.Request.QueryString["str"] != null) str = HttpUtility.ParseQueryString(context.Request.Url.Query, Encoding.Default)["str"];
            string head = context.Request.Form["ishead"]; if (head == null && context.Request.QueryString["ishead"] != null) head = context.Request.QueryString["ishead"];
            if (str != null)
            {
                bool ishead = (head != null && (head == "true" || head == "1"));
                context.Response.Write(Get(str, ishead, true));
            }
        }

        /// <summary>
        /// 拼音字母获取
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="ishead">是否只首字母获取</param>
        /// <param name="iscached">默认缓存字典[true]</param>
        /// <returns></returns>
        public static string Get(string str, bool ishead, bool iscached)
        {
            Dictionary<string, string> pinyins = new Dictionary<string, string>();
            if (HttpContext.Current.Application["pinyin_char"] != null) pinyins = (Dictionary<string, string>)HttpContext.Current.Application["pinyin_char"];
            //encoding = Encoding.ASCII;
            if (pinyins.Count == 0)
            {
                //string path = HttpContext.Current.Server.MapPath("data/pinyin.dat");
                string path = HttpContext.Current.Server.MapPath("~/App_Data/pinyin-utf8.dat");
                try
                {
                    using (StreamReader reader = new StreamReader(path, encoding))
                    {
                        string line = "";
                        string[] lines = reader.ReadToEnd().Split((char)10);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            line = lines[i].Trim();
                            pinyins[line.Substring(0, 1)] = line.Substring(2);
                        }
                        if (iscached) HttpContext.Current.Application["pinyin_char"] = pinyins;
                    }
                }
                catch { }
            }
            str = str.Trim();
            string restr = "";

            foreach (char ch in str)
            {
                int code = 0; foreach (byte b in encoding.GetBytes(new char[] { ch })) code += (int)b;
                if (code < 0x30)
                {
                    if (code == 0x25) restr += ch.ToString();
                }
                //数字
                else if (code < 0x3A)
                {
                    restr += ch.ToString();
                }
                else if (code < 0x41)
                {

                }
                //大写字母
                else if (code < 0x5B)
                {
                    restr += ch.ToString();
                }
                else if (code < 0x61)
                {

                }
                //小写字母
                else if (code < 0x7B)
                {
                    restr += ch.ToString();
                }
                else
                {
                    if (pinyins.Keys.Contains(ch.ToString()))
                    {
                        restr += ishead ? pinyins[ch.ToString()][0].ToString() : pinyins[ch.ToString()];
                    }
                    else
                    {
                        //restr += ch;
                    }
                }
            }
            if (!iscached)
            {
                HttpContext.Current.Application["pinyin_char"] = null;
            }
            return restr;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}