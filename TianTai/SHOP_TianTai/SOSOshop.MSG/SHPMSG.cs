using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace SOSOshop.MSG
{
    /// <summary>
    /// “短信王”发送短信接口
    /// </summary>
    public class SHPMSG : IMSG
    {
        #region 属性{用户名与密码等,用于官方接口验证的}
        private string _name = " "/*"sosoyy"*/;
        private string _pwd = " "/*"201314"*/;
        private string _dst = "";
        private string _msg = "";
        private string _time = "";
        private string _sender = "";
        private string _txt = "";
        private Encoding _encoding = System.Text.Encoding.GetEncoding("gb2312");
        private string _dsturl = "http://www.china-sms.com/send/gsend.asp?name={0}&pwd={1}&dst={2}&sender={3}&time={4}&txt={5}&msg={6}";
        private string _return = "";
        private string _returnregexpattern = @"num=(?<num>\d+)&success=(?<success>[\d\,]*)&faile=(?<faile>[\d\,]*)&err=(?<err>[\s\S]*)&errid=(?<errid>\d+)";
        private string _getfeeregexpattern = @"id=(?<id>\d+)&err=(?<err>[\s\S]*)&errid=(?<errid>\d+)&short=(?<short>[\s\S]*)";
        /// <summary>
        /// 企业用户登录名称,用于官方接口验证的
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 企业用户登录密码,用于官方接口验证的
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 群发目标手机号[手机号之间用英文逗号分割,最后一个手机号后不加逗号, 必填, 小灵通号码发送请和手机号码分离单独作为一组发送。请少于100个号码。]
        /// </summary>
        public string Dst
        {
            set { _dst = value; }
            get { return _dst; }
        }
        /// <summary>
        /// 发送短信内容[为不超过60个汉字、字符、数字的字符串，小灵通号码不超过40个字。超过的字符自动截掉。如果是超长短信，不能超过240个字符。]
        /// </summary>
        public string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 定时时间[格式: YYYYMMDDHHMM；12位时间表示，不符合规则的将立即进行发送。]
        /// </summary>
        public string Time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 长号码[您的特服代码为0888008，想让此条短信的发送者为088800800，则sender=00即可；可填,值为空则默认为您的特服代码。注意：并不是所有的通道均支持。 纯数字的字符串，长度<9,完整的特服号码长度<17，在商讯·中国分配的特服代码后附加的数字.需要特别申请。]
        /// </summary>
        public string Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// 短信类型[txt=ccdx表示启用超长短信功能。账号需要开通此功能，且通道和手机支持才能使用。]
        /// </summary>
        public string Txt
        {
            set { _txt = value; }
            get { return _txt; }
        }
        /// <summary>
        /// 字符编码
        /// </summary>
        public Encoding Encoding
        {
            set { Encoding _encoding = value; }
            get { return _encoding; }
        }
        /// <summary>
        /// 网址接口
        /// </summary>
        public string DstUrl
        {
            set { _dsturl = value; }
            get { return _dsturl; }
        }
        /// <summary>
        /// 返回结果[成功的返回：num=1&success=13008124279&faile=&err=发送成功！&errid=0]
        /// </summary>
        public string Return
        {
            set { _return = value; }
            get { return _return; }
        }
        /// <summary>
        /// 返回Sms发送短信结果匹配
        /// </summary>
        public string ReturnRegexPattern
        {
            set { _returnregexpattern = value; }
            get { return _returnregexpattern; }
        }
        /// <summary>
        /// 返回Sms查询余额结果匹配
        /// </summary>
        public string GetFeeRegexPattern
        {
            set { _getfeeregexpattern = value; }
            get { return _getfeeregexpattern; }
        }
        #endregion


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Send(string mobile, string content)
        {
            this.Dst = mobile;
            this.Msg = content;
            return _Send(_DstUrl(), "", "");
        }
        public bool Send(string mobile, string content, string from, string to)
        {
            this.Dst = mobile;
            this.Msg = content;
            if (string.IsNullOrEmpty(from))
            {
                from = "?";
            }
            if (string.IsNullOrEmpty(to))
            {
                to = "?";
            }
            return _Send(_DstUrl(), from, to);
        }

        /// <summary>
        /// 获得企业用户余额
        /// </summary>
        /// <returns></returns>
        public int GetFee()
        {
            try
            {
                string DstUrl = string.Format("http://www.china-sms.com/send/getfee.asp?name={0}&pwd={1}", this.Name, this.Pwd);
                System.Net.HttpWebResponse rs = (System.Net.HttpWebResponse)System.Net.HttpWebRequest.Create(DstUrl).GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(rs.GetResponseStream(), this.Encoding);
                this.Return = sr.ReadToEnd();
                Regex rx = new Regex(this.GetFeeRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                Match match = rx.Match(this.Return);
                var retobj = new { id = 0, err = "企业用户验证失败", errid = 6010, company = "" };
                if (match.Success && match.Groups.Count == 5)
                {
                    retobj = new
                    {
                        id = int.Parse(match.Groups[1].Value),
                        err = match.Groups[2].Value,
                        errid = int.Parse(match.Groups[3].Value),
                        company = match.Groups[4].Value
                    };
                }
                return retobj.id;
            }
            catch { return 0; }
        }

        private string _DstUrl()
        {
            return string.Format(this.DstUrl, this.Name, this.Pwd, this.Dst, this.Sender, this.Time, this.Txt, System.Web.HttpUtility.UrlEncode(this.Msg, this.Encoding));
        }
        private bool _Send(string DstUrl, string from, string to)
        {
            try
            {
                System.Net.HttpWebResponse rs = (System.Net.HttpWebResponse)System.Net.HttpWebRequest.Create(DstUrl).GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(rs.GetResponseStream(), this.Encoding);
                this.Return = sr.ReadToEnd();
                Regex rx = new Regex(this.ReturnRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                Match match = rx.Match(this.Return);
                var retobj = new { num = 0, success = "", faile = "", err = "", errid = 1 };
                if (match.Success && match.Groups.Count == 6)
                {
                    retobj = new
                    {
                        num = int.Parse(match.Groups[1].Value),
                        success = match.Groups[2].Value,
                        faile = match.Groups[3].Value,
                        err = match.Groups[4].Value,
                        errid = int.Parse(match.Groups[5].Value)
                    };
                }
                if (retobj.errid == 0 && retobj.num > 0)
                {
                    //发送成功
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
