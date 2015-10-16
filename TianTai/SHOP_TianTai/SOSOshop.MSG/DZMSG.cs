using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SOSOshop.MSG
{
    /// <summary>
    /// 昆明鼎众短信接口
    /// </summary>
    public class DZMSG : IMSG
    {
        private string CorpID = string.Empty;
        private string Pwd = string.Empty;
        public bool Send(string mobile, string content)
        {
            bool Result = false;
            string Url = string.Format("http://sms80.kmdzgs.com/SDK/Send.aspx?CorpID={0}&Pwd={1}&Mobile={2}&Content={3}", CorpID, Pwd, mobile, content);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                Result = (wr != null && wr.ToString() == "0");

            }
            return Result;
        }

    }
}
