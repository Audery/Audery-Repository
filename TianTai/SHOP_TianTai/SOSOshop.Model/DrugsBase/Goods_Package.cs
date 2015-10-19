using System;
namespace SOSOshop.Model.DrugsBase
{
    /// <summary>
    /// Goods_Package:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Goods_Package
    {
        public Goods_Package()
        { }
        #region Model
        private int _goods_package_id;
        private int _drugsbase_id = 0;
        private int _goods_id = 0;
        private int _goods_unit_id;
        private string _goods_package_material = "";
        private string _goods_package_material_name = "";
        private int _goods_pcs;
        private int _goods_pcs_small;
        /// <summary>
        /// 包装编号
        /// </summary>
        public int Goods_Package_ID
        {
            set { _goods_package_id = value; }
            get { return _goods_package_id; }
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
        /// 转换比编号
        /// </summary>
        public int Goods_ID
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 包装单位
        /// </summary>
        public int Goods_Unit_ID
        {
            set { _goods_unit_id = value; }
            get { return _goods_unit_id; }
        }
        /// <summary>
        /// 包装材料
        /// </summary>
        public string Goods_Package_Material
        {
            set { _goods_package_material = value; }
            get { return _goods_package_material; }
        }
        /// <summary>
        /// 包装材料说明
        /// </summary>
        public string Goods_Package_Material_Name
        {
            set { _goods_package_material_name = value; }
            get { return _goods_package_material_name; }
        }
        /// <summary>
        /// 件装量
        /// </summary>
        public int Goods_Pcs
        {
            set { _goods_pcs = value; }
            get { return _goods_pcs; }
        }
        /// <summary>
        /// 中包装
        /// </summary>
        public int Goods_Pcs_Small
        {
            set { _goods_pcs_small = value; }
            get { return _goods_pcs_small; }
        }
        #endregion Model

    }
}

