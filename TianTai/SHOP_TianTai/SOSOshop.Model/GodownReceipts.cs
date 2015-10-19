using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 入库申请单据存放表
    /// </summary>
    [Serializable]
    public partial class GodownReceipts
    {
        public GodownReceipts()
        { }
        #region Model
        private int _id;
        private int? _classify;
        private string _numbers;
        private string _storehouse_name;
        private string _askadmin;
        private string _verifadmin;
        private bool _status;
        private DateTime _created;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 来源分类:1订单，2备货单，3退货单
        /// </summary>
        public int? classify
        {
            set { _classify = value; }
            get { return _classify; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string numbers
        {
            set { _numbers = value; }
            get { return _numbers; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StoreHouse_Name
        {
            set { _storehouse_name = value; }
            get { return _storehouse_name; }
        }
        /// <summary>
        /// 申请入库人(角色库管)
        /// </summary>
        public string askAdmin
        {
            set { _askadmin = value; }
            get { return _askadmin; }
        }
        /// <summary>
        /// 入库审核人(采购)
        /// </summary>
        public string verifAdmin
        {
            set { _verifadmin = value; }
            get { return _verifadmin; }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime created
        {
            set { _created = value; }
            get { return _created; }
        }
        #endregion Model

    }
}

