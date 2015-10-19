using System;

namespace SOSOshop.Model
{
    public class MailReceiver
    {
        public MailReceiver()
        {
        }
        #region Model
        private int id;
        private int? receiverid;
        private string receiver;
        private DateTime receivetime;
        private int? stat;
        private int? isread;
        private string title;
        private string body;
        private string sender;
        /// <summary>
        /// 主键自增
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 收件人uid
        /// </summary>
        public int? ReceiverId
        {
            set { receiverid = value; }
            get { return receiverid; }
        }
        /// <summary>
        /// 接收者账号
        /// </summary>
        public string Receiver
        {
            set { receiver = value; }
            get { return receiver; }
        }
        /// <summary>
        /// 收件时间
        /// </summary>
        public DateTime ReceiveTime
        {
            set { receivetime = value; }
            get { return receivetime; }
        }
        /// <summary>
        /// 状态 0收件箱 1废件箱 
        /// </summary>
        public int? Stat
        {
            set { stat = value; }
            get { return stat; }
        }
        /// <summary>
        /// 是否已读 0未 1已
        /// </summary>
        public int? IsRead
        {
            set { isread = value; }
            get { return isread; }
        }

        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            set { sender = value; }
            get { return sender; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { title = value; }
            get { return title; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            set { body = value; }
            get { return body; }
        }
        #endregion Model
    }
}
