using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SOSOYY.Cached;

namespace SOSOshop.BLL
{
    public class SysParameter : Cache
    {

        #region 系统设置参数
        private string _websitename;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebSiteName
        {
            get { return _websitename; }
            set { _websitename = value; }
        }

        private string _websitetitle;
        /// <summary>
        /// 网站标题
        /// </summary>
        public string WebSiteTitle
        {
            get { return _websitetitle; }
            set { _websitetitle = value; }
        }

        private string _websitedescription;
        /// <summary>
        /// 网站描述
        /// </summary>
        public string WebSiteDescription
        {
            get { return _websitedescription; }
            set { _websitedescription = value; }
        }

        private string _websitekey;
        /// <summary>
        /// 网站关键字
        /// </summary>
        public string WebSiteKey
        {
            get { return _websitekey; }
            set { _websitekey = value; }
        }

        private string _websitecopyright;
        /// <summary>
        /// 版本信息
        /// </summary>
        public string WebSiteCopyright
        {
            get { return _websitecopyright; }
            set { _websitecopyright = value; }
        }

        private string _websitetemplatepath;
        /// <summary>
        /// 网站模板路径
        /// </summary>
        public string WebSiteTemplatePath
        {
            get { return _websitetemplatepath; }
            set { _websitetemplatepath = value; }
        }

        private string _websitelogo;
        /// <summary>
        /// 网站LOGO
        /// </summary>
        public string WebSiteLogo
        {
            get { return _websitelogo; }
            set { _websitelogo = value; }
        }

        private string _websitebanner;
        /// <summary>
        /// 网站Brannd
        /// </summary>
        public string WebSiteBrannd
        {
            get { return _websitebanner; }
            set { _websitebanner = value; }
        }

        private bool _isopensite;
        /// <summary>
        /// 是否开启网站
        /// </summary>
        public bool IsOpenSite
        {
            get { return _isopensite; }
            set { _isopensite = value; }
        }
        private string _colecsitedescription;
        /// <summary>
        /// 关闭网站描述
        /// </summary>
        public string ColecSiteDescription
        {
            get { return _colecsitedescription; }
            set { _colecsitedescription = value; }
        }
        private bool _isopenshops;
        /// <summary>
        /// 是否开启店铺
        /// </summary>
        public bool IsOpenShops
        {
            get { return _isopenshops; }
            set { _isopenshops = value; }
        }
        private string _closeStoreDescript;
        /// <summary>
        /// 关闭店铺描述
        /// </summary>
        public string CloseStoreDescription
        {
            get { return _closeStoreDescript; }
            set { _closeStoreDescript = value; }
        }
        private bool _isclosestation;
        /// <summary>
        /// 是否开启分店
        /// </summary>
        public bool IsCloseStation
        {
            get { return _isclosestation; }
            set { _isclosestation = value; }
        }
        private bool _isopenbbs;
        /// <summary>
        /// 是否开启论坛
        /// </summary>
        public bool IsOpenBBS
        {
            get { return _isopenbbs; }
            set { _isopenbbs = value; }
        }

        private int imageSize;
        /// <summary>
        /// 上传图片大小
        /// </summary>
        public int ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; }
        }

        private bool _isrewrite;
        /// <summary>
        /// 是否伪静态
        /// </summary>
        public bool ISRewrite
        {
            get { return _isrewrite; }
            set { _isrewrite = value; }
        }

        private string _staticpagefiletype;
        /// <summary>
        /// 伪静态后缀名
        /// </summary>
        public string StaticPageFileType
        {
            get { return _staticpagefiletype; }
            set { _staticpagefiletype = value; }
        }
        private string _sitehttp;
        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteHttp
        {
            get { return _sitehttp; }
            set { _sitehttp = value; }
        }
        private string _dummypath;
        /// <summary>
        /// 安装虚拟目录
        /// </summary>
        public string DummyPaht
        {
            get { return _dummypath; }
            set { _dummypath = value; }
        }

        private string _sitetel;
        /// <summary>
        /// 网站电话
        /// </summary>
        public string SiteTel
        {
            get { return _sitetel; }
            set { _sitetel = value; }
        }

        private string _sitefax;
        /// <summary>
        /// 网站传真
        /// </summary>
        public string SiteFax
        {
            get { return _sitefax; }
            set { _sitefax = value; }
        }

        private string _siteemail;
        /// <summary>
        /// 网站email
        /// </summary>
        public string SiteEmail
        {
            get { return _siteemail; }
            set { _siteemail = value; }
        }

        private string _copyright;
        /// <summary>
        /// 网站版权信息
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            set { _copyright = value; }
        }

        private string _versions = "";
        /// <summary>
        /// 网站版本
        /// </summary>
        public string Versions
        {
            get { return _versions; }
            set { _versions = value; }
        }
        private string _statisticalCode;

        public string StatisticalCode
        {
            get { return _statisticalCode; }
            set { _statisticalCode = value; }
        }

        private bool _issession;
        /// <summary>
        /// 是否使用Session存储值
        /// </summary>
        public bool IsSession
        {
            get { return _issession; }
            set { _issession = value; }
        }
        #endregion

        #region 用户参数设置
        private bool _isregistered;
        /// <summary>
        /// 是否启用用户注册
        /// </summary>
        public bool IsRegistered
        {
            get { return _isregistered; }
            set { _isregistered = value; }
        }

        private bool _sameemailregister;
        /// <summary>
        /// 同一Email多次注册
        /// </summary>
        public bool SameEmailRegister
        {
            set { _sameemailregister = value; }
            get { return _sameemailregister; }
        }

        private bool _adminvalidate;
        /// <summary>
        /// 是否需要管理员认证
        /// </summary>
        public bool AdminValidate
        {
            set { _adminvalidate = value; }
            get { return _adminvalidate; }
        }

        private bool _emailvalidate;
        /// <summary>
        /// 是否需要邮件验证
        /// </summary>
        public bool EmailValidate
        {
            set { _emailvalidate = value; }
            get { return _emailvalidate; }
        }

        private string _emailvalidatecontent;
        /// <summary>
        /// 验证邮件的内容
        /// </summary>
        public string EmailValidateContent
        {
            set { _emailvalidatecontent = value; }
            get { return _emailvalidatecontent; }
        }

        private bool _handselcoupons;
        /// <summary>
        /// 注册后是否赠送点券
        /// </summary>
        public bool HandselCoupons
        {
            set { _handselcoupons = value; }
            get { return _handselcoupons; }
        }

        private int _handselcouponsnumber;
        /// <summary>
        /// 赠送点卷数量
        /// </summary>
        public int HandselCouponsNumber
        {
            set { _handselcouponsnumber = value; }
            get { return _handselcouponsnumber; }
        }

        private DateTime _handselcouponsbegintime;
        /// <summary>
        /// 点卷赠送起始时间
        /// </summary>
        public DateTime HandselCouponsBeginTime
        {
            set { _handselcouponsbegintime = value; }
            get { return _handselcouponsbegintime; }
        }

        private DateTime _handselcouponsendtime;
        /// <summary>
        /// 点卷赠送结束时间
        /// </summary>
        public DateTime HandselCouponsEndTime
        {
            set { _handselcouponsendtime = value; }
            get { return _handselcouponsendtime; }
        }

        private string _handselpoint;
        /// <summary>
        /// 新会员赠送积分
        /// </summary>
        public string HandselPoint
        {
            set { _handselpoint = value; }
            get { return _handselpoint; }
        }

        private string _forbiduserid;
        /// <summary>
        /// 禁止使用的姓名
        /// </summary>
        public string ForbidUserId
        {
            set { _forbiduserid = value; }
            get { return _forbiduserid; }
        }

        private string _userdefaultgroup;
        /// <summary>
        /// 用户注册默认会员组
        /// </summary>
        public string UserDefaultGroup
        {
            set { _userdefaultgroup = value; }
            get { return _userdefaultgroup; }
        }

        private decimal _loginpoint;
        /// <summary>
        /// 登陆赠送的积分
        /// </summary>
        public decimal LoginPoint
        {
            set { _loginpoint = value; }
            get { return _loginpoint; }
        }
        private bool _isloginvalidate;
        /// <summary>
        /// 是否起用登陆验证
        /// </summary>
        public bool Isloginvalidate
        {
            get { return _isloginvalidate; }
            set { _isloginvalidate = value; }
        }
        private bool _ismultilogin;
        /// <summary>
        /// 是否起用多人使用同一帐号
        /// </summary>
        public bool Ismultilogin
        {
            get { return _ismultilogin; }
            set { _ismultilogin = value; }
        }

        #endregion

        #region 缩略图参数
        private bool _isthumbnails;
        /// <summary>
        /// 是否生成缩略图
        /// </summary>
        public bool IsThumbnaile
        {
            get { return _isthumbnails; }
            set { _isthumbnails = value; }
        }

        private int _thumbnailswidth;
        /// <summary>
        /// 缩略图宽
        /// </summary>
        public int ThumbnailsWidth
        {
            get { return _thumbnailswidth; }
            set { _thumbnailswidth = value; }
        }

        private int _thumbnailsheight;
        /// <summary>
        /// 缩略图高
        /// </summary>
        public int ThumbnailsHeight
        {
            get { return _thumbnailsheight; }
            set { _thumbnailsheight = value; }
        }

        private int _imagesthumbnailswidth;
        /// <summary>
        /// 大图缩略宽
        /// </summary>
        public int ImagesThumbnailsWidth
        {
            get { return _imagesthumbnailswidth; }
            set { _imagesthumbnailswidth = value; }
        }

        private int _imagesthumbnailsheight;
        /// <summary>
        /// 大图缩略高
        /// </summary>
        public int ImagesThumbnailsHeight
        {
            get { return _imagesthumbnailsheight; }
            set { _imagesthumbnailsheight = value; }
        }

        private bool _iswatermark;
        /// <summary>
        /// 是否给图片添加水印
        /// </summary>
        public bool IsWatermark
        {
            get { return _iswatermark; }
            set { _iswatermark = value; }
        }

        private string _textorimageswatermark;
        /// <summary>
        /// 文字水印与图片水印
        /// </summary>
        public string TextOrImagesWatermark
        {
            get { return _textorimageswatermark; }
            set { _textorimageswatermark = value; }
        }

        private int _imagewatermarktransparent;
        /// <summary>
        /// 图片水印透明度
        /// </summary>
        public int ImageWatermarkTransparent
        {
            get { return _imagewatermarktransparent; }
            set { _imagewatermarktransparent = value; }
        }

        private string _watermarkimage;
        /// <summary>
        /// 水印图片
        /// </summary>
        public string WatermarkImage
        {
            get { return _watermarkimage; }
            set { _watermarkimage = value; }
        }

        private int _textwatermarktransparent;
        /// <summary>
        /// 文字水印透明度
        /// </summary>
        public int TextWatermarkTransparent
        {
            get { return _textwatermarktransparent; }
            set { _textwatermarktransparent = value; }
        }

        private string _watermarktext;
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WatermarkText
        {
            get { return _watermarktext; }
            set { _watermarktext = value; }
        }

        private string _watermarkposition;
        /// <summary>
        /// 水印位置
        /// </summary>
        public string WatermarkPosition
        {
            get { return _watermarkposition; }
            set { _watermarkposition = value; }
        }
        #endregion

        #region 商品参数
        private bool _allowgroupbuydeposit;
        /// <summary>
        /// 团购时是否缴纳保证金
        /// </summary>
        public bool AllowGroupBuyDeposit
        {
            get { return _allowgroupbuydeposit; }
            set { _allowgroupbuydeposit = value; }
        }

        private bool _allowauctiondeposit;
        /// <summary>
        /// 拍卖时是否缴纳保证金
        /// </summary>
        public bool AllowAuctionDeposit
        {
            get { return _allowauctiondeposit; }
            set { _allowauctiondeposit = value; }
        }

        private bool _ispaymentweboperation;
        /// <summary>
        /// 是否支付给网站运营方
        /// </summary>
        public bool IsPayMentWebOperation
        {
            get { return _ispaymentweboperation; }
            set { _ispaymentweboperation = value; }
        }

        private string _ordersText = "";

        public string OrdersText
        {
            get { return _ordersText; }
            set { _ordersText = value; }
        }

        #endregion

        public SysParameter()
        {
            string cc_WebSetting = "WebSetting";
            string cc_CustomerSetting = "CustomerSetting";
            string cc_ThumbnailsSetting = "ThumbnailsSetting";
            string cc_ShopSetting = "ShopSetting";

            #region 系统参数设置
            object objWebSetting = GetObject(cc_WebSetting);
            Model.WebSetting wsmodel = null;
            if (objWebSetting != null)
            {
                wsmodel = (Model.WebSetting)objWebSetting;
            }
            else
            {
                BLL.WebSetting wsbll = new BLL.WebSetting();
                wsmodel = wsbll.GetModel();
                SetObject(cc_WebSetting, wsmodel);
            }
            if (wsmodel != null)
            {
                imageSize = wsmodel.Filesize;
                _isopensite = wsmodel.CloseWebSite == 1 ? true : false;
                _isopenshops = wsmodel.CloseShop == 1 ? true : false;
                _isopenbbs = wsmodel.CloseBBS == 1 ? true : false;
                _sitehttp = wsmodel.WebSiteDomain;
                _dummypath = wsmodel.WebSitePath;
                _websitename = wsmodel.WebSiteName;
                _websitetitle = wsmodel.WebSiteTitle;
                _websitedescription = wsmodel.MeteInfo;
                _websitekey = wsmodel.MeteKey;
                _websitetemplatepath = wsmodel.TmplatePath;
                _websitelogo = wsmodel.LogoPath;
                _websitebanner = wsmodel.BannerPath;
                _isrewrite = wsmodel.PublicMethod == 0 ? true : false;
                _staticpagefiletype = wsmodel.StaticPageFileType;
                _isclosestation = wsmodel.CloseStation == 1 ? true : false;
                _sitetel = wsmodel.Tel;
                _sitefax = wsmodel.Fax;
                _siteemail = wsmodel.Email;
                _copyright = wsmodel.CopyRight;
                _issession = wsmodel.LoginMothod == 0 ? true : false;
                _colecsitedescription = wsmodel.CloseWebSiteInfo;
                _statisticalCode = wsmodel.Statisticscode;
            }
            #endregion

            #region 用户参数设置
            object objCustomerSetting = GetObject(cc_CustomerSetting);
            Model.CustomerSetting csmodel = null;
            if (objCustomerSetting != null)
            {
                csmodel = (Model.CustomerSetting)objCustomerSetting;
            }
            else
            {
                BLL.CustomerSetting csbll = new BLL.CustomerSetting();
                csmodel = csbll.GetModel();
                SetObject(cc_CustomerSetting, csmodel);
            }
            if (csmodel != null)
            {
                _isregistered = csmodel.AllowRegister.Trim() == "1" ? true : false;
                _sameemailregister = csmodel.SameEmailRegister == 1 ? true : false;
                _adminvalidate = csmodel.AdminValidate == 1 ? true : false;
                _emailvalidate = csmodel.EmailValidate == 1 ? true : false;
                _emailvalidatecontent = csmodel.EmailValidateContent.Trim();
                _handselcoupons = csmodel.HandselCoupons == 1 ? true : false;
                _handselcouponsnumber = Convert.ToInt32(csmodel.HandselCouponsNumber);
                _handselcouponsbegintime = Convert.ToDateTime(csmodel.HandselCouponsBeginTime);
                _handselcouponsendtime = Convert.ToDateTime(csmodel.HandselCouponsEndTime);
                _handselpoint = csmodel.HandselPoint;
                _forbiduserid = csmodel.ForbidUserId;
                _userdefaultgroup = csmodel.UserDefaultGroup;
                _loginpoint = Convert.ToDecimal(csmodel.LoginPoint);
                _isloginvalidate = csmodel.LoginIsNeedCheckCode == 1 ? true : false;
                _ismultilogin = csmodel.AllowOtherLogin == 1 ? true : false;
            }
            #endregion

            #region 缩略图参数设置
            object objThumbnailsSetting = GetObject(cc_ThumbnailsSetting);
            Model.ThumbnailsSetting tsmodel = null;
            if (objThumbnailsSetting != null)
            {
                tsmodel = (Model.ThumbnailsSetting)objThumbnailsSetting;
            }
            else
            {
                BLL.ThumbnailsSetting tsbll = new BLL.ThumbnailsSetting();
                tsmodel = tsbll.GetModel();
                SetObject(cc_ThumbnailsSetting, tsmodel);
            }

            if (tsmodel != null)
            {
                _thumbnailsheight = Convert.ToInt32(tsmodel.ThumbnailsHeight);
                _thumbnailswidth = Convert.ToInt32(tsmodel.ThumbnailsWidth);
                _imagesthumbnailsheight = Convert.ToInt32(tsmodel.ImageHeight);
                _imagesthumbnailswidth = Convert.ToInt32(tsmodel.ImageWidth);
                _imagewatermarktransparent = Convert.ToInt32(tsmodel.ImgTransparent);
                _textwatermarktransparent = Convert.ToInt32(tsmodel.CharTransparent);
                _watermarktext = tsmodel.Characters;
                _watermarkimage = tsmodel.WatermarkPicturePath;
                _watermarkposition = tsmodel.WatermarkPosition;
                _textorimageswatermark = tsmodel.Type;
            }
            #endregion

            #region 商铺参数设置
            object objShopSetting = GetObject(cc_ShopSetting);
            Model.ShopSetting ssmodel = null;
            if (objShopSetting != null)
            {
                ssmodel = (Model.ShopSetting)objShopSetting;
            }
            else
            {
                BLL.ShopSetting ssbll = new BLL.ShopSetting();
                ssmodel = ssbll.GetModel();
                SetObject(cc_ShopSetting, ssmodel);
            }
            if (ssmodel != null)
            {
                _isthumbnails = ssmodel.Thumbnails == 1 ? true : false;
                _iswatermark = ssmodel.WaterMark == 1 ? true : false;
                _allowgroupbuydeposit = ssmodel.AllowGroupbuyDeposit == 1 ? true : false;
                _allowauctiondeposit = ssmodel.AllowAuctionDeposit == 1 ? true : false;
                _ispaymentweboperation = ssmodel.OrdersReceive == 1 ? true : false;
                _ordersText = ssmodel.OrdersText;
            }
            #endregion
        }

    }
}
