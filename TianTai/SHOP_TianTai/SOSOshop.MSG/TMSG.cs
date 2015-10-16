using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace SOSOshop.MSG
{
    /// <summary>
    /// 天下畅通短信接口
    /// </summary>
    public class TMSG : IMSG
    {
        private string userid = string.Empty;
        private string account = string.Empty;
        private string password = string.Empty;

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Send(string mobile, string content)
        {
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                string param = "action=send&userid=" + userid + "&account=" + HttpUtility.UrlEncode(account, myEncoding) + "&password=" + HttpUtility.UrlEncode(password, myEncoding) + "&mobile=" + mobile + "&content=" + HttpUtility.UrlEncode(content, myEncoding);
                byte[] postBytes = Encoding.ASCII.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://xtx.telhk.cn:8080/sms.aspx");
                //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://114.113.227.101:8888/sms.aspx");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                req.ContentLength = postBytes.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }

                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);                    
                    System.IO.StreamReader xmlStreamReader = sr;
                    xmlDoc.Load(xmlStreamReader);
                }
                if (xmlDoc == null)
                {
                    return false;
                }
                else
                {
                    String message = xmlDoc.GetElementsByTagName("message").Item(0).InnerText.ToString();
                    if (message == "ok")
                    {
                        Console.WriteLine(message);
                        return true;
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine(message);
                            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\MsgError.txt", message + "[" + content + "][" + mobile + "]" + "\r\n");
                        }
                        catch { }
                        return false;
                    }
                }
            }
            catch (System.Net.WebException WebExcp)
            {
                return false;
            }
        }
    }
}
