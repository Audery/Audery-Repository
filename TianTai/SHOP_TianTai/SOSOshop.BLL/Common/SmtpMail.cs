using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SOSOshop.BLL.Common
{
    /// <summary>
    /// 发送邮件类
    /// </summary>
    public class SmtpMail : ISmtpMail
    {
        private string _body;
        private string _from;
        private string _fromName;
        private bool _html = true;
        private string _mailDomain;
        private int _mailserverport;
        private string _password;
        private string _recipient;
        private string _recipientName;
        private string _subject;
        private string _username;

        public string ExceptionMessage = string.Empty;

        public bool AddRecipient(params string[] username)
        {
            this._recipient = username[0].Trim();
            return true;
        }

        private string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }

        public bool Send(string Subject, string Body)
        {
            if (string.IsNullOrEmpty(this.MailDomain))
            {
                SOSOshop.Model.MailSetting mailSetting = new SOSOshop.BLL.MailSetting().GetModel();
                if (mailSetting != null && mailSetting.SmtpServerPort > 0)
                {
                    this.MailDomain = mailSetting.SmtpServerIP;
                    this.MailDomainPort = (int)mailSetting.SmtpServerPort;
                    this.MailServerUserName = this.From = mailSetting.MailId;
                    this.MailServerPassWord = mailSetting.MailPassword;
                }
            }            
            this.Subject = Subject;
            this.Body = Body;
            MailMessage message = new MailMessage();
            Encoding displayNameEncoding = Encoding.GetEncoding("utf-8");
            message.From = new MailAddress(this.From, this.Subject, displayNameEncoding);
            message.To.Add(this._recipient);
            message.Subject = this.Subject;
            message.IsBodyHtml = this.Html;
            message.Body = this.Body;
            message.Priority = MailPriority.Normal;
            message.BodyEncoding = Encoding.GetEncoding("utf-8");
            SmtpClient client = new SmtpClient();
            client.Host = this.MailDomain;
            client.Port = this.MailDomainPort;
            client.Credentials = new NetworkCredential(this.MailServerUserName, this.MailServerPassWord);
            
            if (this.MailDomainPort != 0x19)
            {
                client.EnableSsl = true;
            }

            client.Send(message);
            return true;
        }

        public string Body
        {
            get
            {
                return this._body;
            }
            set
            {
                this._body = value;
            }
        }

        public string From
        {
            get
            {
                return this._from;
            }
            set
            {
                this._from = value;
            }
        }

        public string FromName
        {
            get
            {
                return this._fromName;
            }
            set
            {
                this._fromName = value;
            }
        }

        public bool Html
        {
            get
            {
                return this._html;
            }
            set
            {
                this._html = value;
            }
        }

        public string MailDomain
        {
            get
            {
                return this._mailDomain;
            }
            set
            {
                this._mailDomain = value;
            }
        }

        public int MailDomainPort
        {
            get
            {
                return this._mailserverport;
            }
            set
            {
                this._mailserverport = value;
            }
        }

        public string MailServerPassWord
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string MailServerUserName
        {
            get
            {
                return this._username;
            }
            set
            {
                if (value.Trim() != "")
                {
                    this._username = value.Trim();
                }
                else
                {
                    this._username = "";
                }
            }
        }

        public string RecipientName
        {
            get
            {
                return this._recipientName;
            }
            set
            {
                this._recipientName = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }
    }

    public interface ISmtpMail
    {
        // Methods
        bool AddRecipient(params string[] username);
        bool Send(string Subject, string Body);

        // Properties
        string Body { get; set; }
        string From { get; set; }
        string FromName { get; set; }
        bool Html { get; set; }
        string MailDomain { set; }
        int MailDomainPort { set; }
        string MailServerPassWord { set; }
        string MailServerUserName { set; }
        string RecipientName { get; set; }
        string Subject { get; set; }
    }

}
