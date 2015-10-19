using System;
namespace SOSOshop.Model.DrugsBase
{
    /// <summary>
    /// Goods:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Goods
    {
        public Goods()
        { }
        #region Model
        private int _goods_id;
        private int _drugsbase_id;
        private int _goods_converatio;
        private int _goods_converatio_unit_id;
        private string _goods_converatio_unit_name;
        /// <summary>
        /// 转换比编号
        /// </summary>
        public int Goods_ID
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 药品编号
        /// </summary>
        public int DrugsBase_ID
        {
            set { _drugsbase_id = value; }
            get { return _drugsbase_id; }
        }
        /// <summary>
        /// 转换比
        /// </summary>
        public int Goods_ConveRatio
        {
            set { _goods_converatio = value; }
            get { return _goods_converatio; }
        }
        /// <summary>
        /// 转换比单位
        /// </summary>
        public int Goods_ConveRatio_Unit_ID
        {
            set { _goods_converatio_unit_id = value; }
            get { return _goods_converatio_unit_id; }
        }
        /// <summary>
        /// 转换比说明
        /// </summary>
        public string Goods_ConveRatio_Unit_Name
        {
            set { _goods_converatio_unit_name = value; }
            get { return _goods_converatio_unit_name; }
        }
        #endregion Model

    }
}

