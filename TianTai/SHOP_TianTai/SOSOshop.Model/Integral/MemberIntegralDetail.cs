using System;
namespace SOSOshop.Model.Integral
{
    /// <summary>
    /// 会员积分明细表
    /// </summary>
    [Serializable]
    public partial class MemberIntegralDetail
    {
        public MemberIntegralDetail()
        { }
        #region Model
        private int? _uid;
        private int? _integral;
        private string _remarks;
        private string _action;
        private DateTime? _created;
        /// <summary>
        /// 会员ID
        /// </summary>
        public int? uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 此笔积分数量
        /// </summary>
        public int? integral
        {
            set { _integral = value; }
            get { return _integral; }
        }
        /// <summary>
        /// 此笔积分描述,比如哪种情况获得的此积分，或兑换了什么礼品
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 增加/兑换
        /// </summary>
        public string action
        {
            set { _action = value; }
            get { return _action; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? created
        {
            set { _created = value; }
            get { return _created; }
        }
        #endregion Model

    }
}

