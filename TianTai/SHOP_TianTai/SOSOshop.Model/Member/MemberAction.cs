using System;
namespace SOSOshop.Model.Member
{
    /// <summary>
    /// 会员浏览行为分析
    /// </summary>
    [Serializable]
    public partial class MemberAction
    {
        public MemberAction()
        { }
        #region Model
        private int _id;
        private int _uid;
        private string _controller;
        private string _action;
        private string _query;
        private string _httpmethod;
        private string _sessionid;
        private string _url;
        private string _actuation;
        private string _actuationvalue;
        private DateTime? _created;
        private int? _sleeptime;
        private string _os;
        private string _webbrowser;
        private string _distinguishability;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 页面控制器
        /// </summary>
        public string controller
        {
            set { _controller = value; }
            get { return _controller; }
        }
        /// <summary>
        /// 控制器所调用的方法
        /// </summary>
        public string action
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// URL后面所根的查询参数
        /// </summary>
        public string Query
        {
            set { _query = value; }
            get { return _query; }
        }
        /// <summary>
        /// 页面请求的方式
        /// </summary>
        public string HttpMethod
        {
            set { _httpmethod = value; }
            get { return _httpmethod; }
        }
        /// <summary>
        /// 当次登陆标识
        /// </summary>
        public string sessionid
        {
            set { _sessionid = value; }
            get { return _sessionid; }
        }
        /// <summary>
        /// 当前面完整路径
        /// </summary>
        public string url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 用户行为 浏览商品，浏览页面，搜索商品，下单
        /// </summary>
        public string actuation
        {
            set { _actuation = value; }
            get { return _actuation; }
        }
        /// <summary>
        /// 用户行为对应的值
        /// </summary>
        public string ActuationValue
        {
            set { _actuationvalue = value; }
            get { return _actuationvalue; }
        }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime? created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 在当前页停留的时间(以秒为单位)
        /// </summary>
        public int? SleepTime
        {
            set { _sleeptime = value; }
            get { return _sleeptime; }
        }
        /// <summary>
        /// 用户的操作系统
        /// </summary>
        public string OS
        {
            set { _os = value; }
            get { return _os; }
        }
        /// <summary>
        /// 用户的浏览器
        /// </summary>
        public string WebBrowser
        {
            set { _webbrowser = value; }
            get { return _webbrowser; }
        }
        /// <summary>
        /// 用户系统的分辨率
        /// </summary>
        public string distinguishability
        {
            set { _distinguishability = value; }
            get { return _distinguishability; }
        }
        #endregion Model

    }
}

