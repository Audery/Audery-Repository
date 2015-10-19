using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类WebSetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class WebSetting
    {
        public WebSetting()
        { }
        #region Model
        private int _id;
        private string _websitetitle;
        private string _tel;
        private string _fax;
        private string _email;
        private string _websitepath;
        private string _logopath;
        private string _statisticscode; //统计代码
        private int _filesize; //整站上传_附件大小
        private string _bannerpath;
        private string _copyright;
        private string _metekey;
        private string _meteinfo;
        private int _publicmethod;
        private int _closewebsite;
        private string _closewebsiteinfo;
        private int _closebbs;
        private string _closebbsinfo;
        private string _websitename;
        private int _closeshop;
        private int _closestation;
        private string _websitedomain;
        private string _usersagreement;
        private int _loginmothod;
        private string _staticpagefiletype;
        private string _tmplatepath;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebSiteTitle
        {
            set { _websitetitle = value; }
            get { return _websitetitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebSitePath
        {
            set { _websitepath = value; }
            get { return _websitepath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogoPath
        {
            set { _logopath = value; }
            get { return _logopath; }
        }
        /// <summary>
        /// 统计代码
        /// </summary>
        public string Statisticscode
        {
            get { return _statisticscode; }
            set { _statisticscode = value; }
        }
        /// <summary>
        /// 整站上传大小
        /// </summary>
        public int Filesize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BannerPath
        {
            set { _bannerpath = value; }
            get { return _bannerpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CopyRight
        {
            set { _copyright = value; }
            get { return _copyright; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MeteKey
        {
            set { _metekey = value; }
            get { return _metekey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MeteInfo
        {
            set { _meteinfo = value; }
            get { return _meteinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PublicMethod
        {
            set { _publicmethod = value; }
            get { return _publicmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CloseWebSite
        {
            set { _closewebsite = value; }
            get { return _closewebsite; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CloseWebSiteInfo
        {
            set { _closewebsiteinfo = value; }
            get { return _closewebsiteinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CloseBBS
        {
            set { _closebbs = value; }
            get { return _closebbs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CloseBBSInfo
        {
            set { _closebbsinfo = value; }
            get { return _closebbsinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebSiteName
        {
            set { _websitename = value; }
            get { return _websitename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CloseShop
        {
            set { _closeshop = value; }
            get { return _closeshop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CloseStation
        {
            set { _closestation = value; }
            get { return _closestation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebSiteDomain
        {
            set { _websitedomain = value; }
            get { return _websitedomain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsersAgreement
        {
            set { _usersagreement = value; }
            get { return _usersagreement; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LoginMothod
        {
            set { _loginmothod = value; }
            get { return _loginmothod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StaticPageFileType
        {
            set { _staticpagefiletype = value; }
            get { return _staticpagefiletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TmplatePath
        {
            set { _tmplatepath = value; }
            get { return _tmplatepath; }
        }
        #endregion Model

    }
}
