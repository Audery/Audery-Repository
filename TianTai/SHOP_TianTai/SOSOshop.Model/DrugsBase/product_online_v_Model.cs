using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model.DrugsBase
{
    /// <summary>
    /// 在线商品数据模型
    /// </summary>
    public class product_online_v_Model
    {
        public product_online_v_Model() { }

        public int Product_ID { get; set; }

        public int Goods_ID { get; set; }

        /// <summary>
        /// 药品Id
        /// </summary>
        public int DrugsBase_ID { get; set; }

        public int Goods_Package_ID { get; set; }

        public string Product_Name { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int Product_SaleNum { get; set; }

        /// <summary>
        /// 药厂Id
        /// </summary>
        public int DrugsBase_Manufacturer_Id { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string DrugsBase_Manufacturer { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugsBase_DrugName { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string DrugsBase_Formulation { get; set; }

        public int Goods_ConveRatio { get; set; }

        public string Goods_ConveRatio_Unit_Name { get; set; }

        public string Goods_ConveRatio_Unit { get; set; }

        public string DrugsBase_ProName { get; set; }

        public string Original { get; set; }

        public string Image { get; set; }

        public string Goods_Unit { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string DrugsBase_Specification { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Product_ClickNum { get; set; }

        public string Product_State { get; set; }

        public DateTime created { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int Pharm_ID { get; set; }

        /// <summary>
        /// 分类路径
        /// </summary>
        public string Pharm_ID_Path { get; set; }

        //private int _SecondPharmId;
        ///// <summary>
        ///// 二级分类Id
        ///// </summary>
        //public int SecondPharmId
        //{
        //    get { return _SecondPharmId; }
        //    set { _SecondPharmId = value; }
        //}

        /// <summary>
        /// 二级分类Id
        /// </summary>
        public int SecondPharmId
        {
            get
            {
                int secondId = 0;

                try
                {
                    if (!string.IsNullOrEmpty(this.Pharm_ID_Path) && this.Pharm_ID_Path.Length > 2)
                    {
                        string secondPharmId = "";
                        if (this.Pharm_ID_Path.Contains(@"\"))
                        {
                            int firstChar = this.Pharm_ID_Path.IndexOf(@"\",2);
                            int secondChar = this.Pharm_ID_Path.IndexOf(@"\", firstChar + 1);

                            if (secondChar != -1)
                            {
                                secondPharmId = this.Pharm_ID_Path.Substring(firstChar + 1, secondChar - firstChar - 1);
                            }
                            else
                            {
                                secondPharmId = this.Pharm_ID_Path.Substring(firstChar + 1);
                            }
                        }
                        int.TryParse(secondPharmId, out secondId);
                    }
                }
                catch
                { }

                return secondId;
            }
        }


        public string ShowPrice { get; set; }

        /// <summary>
        /// 药品价格
        /// </summary>
        public string Price { get; set; }
    }

    /// <summary>
    /// 药品分类
    /// </summary>
    public class DrugsPharm
    {
        public DrugsPharm() { }

        /// <summary>
        /// 药厂Id
        /// </summary>
        public int DrugsBase_Manufacturer_Id { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string DrugsBase_Manufacturer { get; set; }

        /// <summary>
        /// 药品Id
        /// </summary>
        public int DrugsBase_ID { get; set; }

        /// <summary>
        /// 分类路径
        /// </summary>
        public string Pharm_ID_Path { get; set; }

        /// <summary>
        /// 父级分类ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 根分类Id
        /// </summary>
        public int Root_Pharm_ID { get; set; }

        /// <summary>
        /// 根分类名称
        /// </summary>
        public string Root_Pharm_Name { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int Pharm_ID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Pharm_Name { get; set; }

        /// <summary>
        /// 分类下的药品个数
        /// </summary>
        public int DrugNumber { get; set; }

        /// <summary>
        /// 分类名称+分类下的药品总数
        /// </summary>
        public string PharmName_
        {
            get
            {
                return this.Pharm_Name + "(" + this.DrugNumber.ToString() + ")";
            }
        }

        /// <summary>
        /// 二级分类Id
        /// </summary>
        public int SecondPharmId
        {
            get
            {
                int secondId = 0;

                try
                {
                    if (!string.IsNullOrEmpty(this.Pharm_ID_Path) && this.Pharm_ID_Path.Length > 2)
                    {
                        string secondPharmId = "";
                        if (this.Pharm_ID_Path.Contains(@"\"))
                        {
                            int firstChar = this.Pharm_ID_Path.IndexOf(@"\",2);
                            int secondChar = this.Pharm_ID_Path.IndexOf(@"\", firstChar + 1);

                            if (secondChar != -1)
                            {
                                secondPharmId = this.Pharm_ID_Path.Substring(firstChar + 1, secondChar - firstChar - 1);
                            }
                            else
                            {
                                secondPharmId = this.Pharm_ID_Path.Substring(firstChar + 1);
                            }
                        }
                        int.TryParse(secondPharmId, out secondId);
                    }
                }
                catch
                { }

                return secondId;
            }
        }
    }

    /// <summary>
    /// 根级分类（药厂的分页导航页面使用）
    /// </summary>
    public class RootPharm
    {
        public RootPharm() { }

        public RootPharm(int rootId, string rootName)
        {
            this.Root_Pharm_ID = rootId;
            this.Root_Pharm_Name = rootName;
        }

        /// <summary>
        /// 根分类Id
        /// </summary>
        public int Root_Pharm_ID { get; set; }
        /// <summary>
        /// 根分类名称
        /// </summary>
        public string Root_Pharm_Name { get; set; }
    }

    /// <summary>
    /// 二级分类
    /// </summary>
    public class PharmInfo
    {

        /// <summary>
        /// 分类Id
        /// </summary>
        public int Pharm_ID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Pharm_Name { get; set; }

        /// <summary>
        /// 分类级数
        /// </summary>
        public int Pharm_Level { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public int Pharm_Parent_ID { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Pharm_ID_Path { get; set; }

        /// <summary>
        /// 路径名称
        /// </summary>
        public string Pharm_Name_Path { get; set; }
    }
}
