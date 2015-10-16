using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 资质数据库
    /// </summary>
    [Serializable]
    public partial class Qualifications
    {
        public Qualifications()
        { }
        #region Model
        private int _id;
        private int _drugsbase_enterprise_id;
        private int _name;
        private string _attachment;
        private string _classify = "0";
        private int _expirydate;
        private DateTime _alertdate;
        private bool _istransition;
        private bool _isyearcheck;
        private string _remark;
        private DateTime _ceated;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业库关联ID
        /// </summary>
        public int DrugsBase_Enterprise_ID
        {
            set { _drugsbase_enterprise_id = value; }
            get { return _drugsbase_enterprise_id; }
        }
        /// <summary>
        /// 资质名称
        /// </summary>
        public int Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 附件路径(如证书图片)
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 建档资质
        /// </summary>
        public string Classify
        {
            set { _classify = value; }
            get { return _classify; }
        }
        /// <summary>
        /// 有效期，以年为单位
        /// </summary>
        public int ExpiryDate
        {
            set { _expirydate = value; }
            get { return _expirydate; }
        }
        /// <summary>
        /// 提醒日期
        /// </summary>
        public DateTime AlertDate
        {
            set { _alertdate = value; }
            get { return _alertdate; }
        }
        /// <summary>
        /// 是否有过度期
        /// </summary>
        public bool IsTransition
        {
            set { _istransition = value; }
            get { return _istransition; }
        }
        /// <summary>
        /// 是否年检
        /// </summary>
        public bool IsYearCheck
        {
            set { _isyearcheck = value; }
            get { return _isyearcheck; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Ceated
        {
            set { _ceated = value; }
            get { return _ceated; }
        }
        #endregion Model

    }
}

