using System;
namespace DSWebService.Model.Data_Centre
{
    /// <summary>
    /// Link_Mid:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Link_Mid
    {
        public Link_Mid()
        { }
        #region Model
        private int _id;
        private int _iden;
        private int _sum;
        private int _stocktype;
        private int _pricetype;        
        private DateTime _created;
        private DateTime _updated;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 10000-天奇,10001-蓉锦
        /// </summary>
        public int iden
        {
            set { _iden = value; }
            get { return _iden; }
        }

        /// <summary>
        /// 中包装或件装数量
        /// </summary>
        public int Sum
        {
            set { _sum = value; }
            get { return _sum; }
        }
        /// <summary>
        /// 库存类型
        /// </summary>
        public int StockType
        {
            set { _stocktype = value; }
            get { return _stocktype; }
        }
        /// <summary>
        /// 价格类型
        /// </summary>
        public int PriceType
        {
            set { _pricetype = value; }
            get { return _pricetype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated
        {
            set { _updated = value; }
            get { return _updated; }
        }
       
        #endregion Model

    }
}

