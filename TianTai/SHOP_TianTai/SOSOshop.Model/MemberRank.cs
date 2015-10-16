using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类MemberRank 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MemberRank
    {
        public MemberRank()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _logopic;
        private decimal? _discount;
        private decimal? _minscore;
        private decimal? _maxscore;
        private int? _priority;
        private int? _isspecial;
        private int? _isshowprice;
        private int? _article;
        private int? _product;
        private int? _articleauditing;
        private int? _productauditing;
        private decimal? _maxmoney;
        private decimal? _upgrademoney;
        private int? _isupgrade;

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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LogoPic
        {
            set { _logopic = value; }
            get { return _logopic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MinScore
        {
            set { _minscore = value; }
            get { return _minscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaxScore
        {
            set { _maxscore = value; }
            get { return _maxscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsSpecial
        {
            set { _isspecial = value; }
            get { return _isspecial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsShowPrice
        {
            set { _isshowprice = value; }
            get { return _isshowprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Article
        {
            set { _article = value; }
            get { return _article; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Product
        {
            set { _product = value; }
            get { return _product; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ArticleAuditing
        {
            set { _articleauditing = value; }
            get { return _articleauditing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ProductAuditing
        {
            set { _productauditing = value; }
            get { return _productauditing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaxMoney
        {
            set { _maxmoney = value; }
            get { return _maxmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? UpgradeMoney
        {
            set { _upgrademoney = value; }
            get { return _upgrademoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsUpgrade
        {
            set { _isupgrade = value; }
            get { return _isupgrade; }
        }
        #endregion Model

    }
}
