using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChatRestful
{
    public partial class WebChatHandler : System.Web.UI.Page
    {
        private readonly string Token = "Audery";
        private static object _locker = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            //验证
            //string echoStr = base.Request.QueryString["echoStr"].ToString();
            //if (!string.IsNullOrWhiteSpace(echoStr))
            //{
            //    base.Response.Write(echoStr);
            //    base.Response.End();
            //}
           
            string Details = string.Empty;
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(base.Request.InputStream))
            {
                Details = streamReader.ReadToEnd();
            }
            this.Write(Details);
            
            base.Response.Write("<xml>\r\n<ToUserName><![CDATA[gh_a02e3ae93d00]]></ToUserName>\r\n<FromUserName><![CDATA[orzFvuKyTjHUzzD3f2V4PYuQlV6Q]]></FromUserName>\r\n<CreateTime>12345678</CreateTime>\r\n<MsgType><![CDATA[text]]></MsgType>\r\n<Content><![CDATA[我是回复消息]]></Content>\r\n</xml>");
            base.Response.End();
        }

        private void Valid()
        {
            string echoStr = base.Request.QueryString["echoStr"].ToString();
            if (this.CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    base.Response.Write(echoStr);
                    base.Response.End();
                }
            }
        }
        private bool CheckSignature()
        {
            string signature = base.Request.QueryString["signature"].ToString();
            string timestamp = base.Request.QueryString["timestamp"].ToString();
            string nonce = base.Request.QueryString["nonce"].ToString();
            string[] ArrTmp = new string[]
            {
                this.Token,
                timestamp,
                nonce
            };
            System.Array.Sort<string>(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            return tmpStr == signature;
        }

        public void Write(string message)
        {
            lock (_locker)
            {
                System.IO.FileStream fs = null;
                System.IO.StreamWriter sw = null;
                try
                {
                    string arg_42_0 = base.Server.MapPath("/");
                    System.DateTime now = DateTime.Now;
                    string fileName = arg_42_0 + now.ToString("yyyyMMdd") + ".log";
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    if (!fileInfo.Exists)
                    {
                        fileInfo.Create().Close();
                    }
                    else
                    {
                        if (fileInfo.Length > 2048000L)
                        {
                            fileInfo.Delete();
                        }
                    }
                    fs = fileInfo.OpenWrite();
                    sw = new System.IO.StreamWriter(fs);
                    sw.BaseStream.Seek(0L, System.IO.SeekOrigin.End);
                    sw.Write("Log Entry : ");
                    System.IO.TextWriter arg_EC_0 = sw;
                    string arg_EC_1 = "{0}";
                    now = DateTime.Now;
                    arg_EC_0.Write(arg_EC_1, now.ToString("yyyy年MM月dd日 HH:mm:ss"));
                    sw.Write(Environment.NewLine);
                    sw.Write(message);
                    sw.Write(Environment.NewLine);
                    sw.Write("------------------------------------");
                    sw.Write(Environment.NewLine);
                }
                catch
                {
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
        }
    }
}