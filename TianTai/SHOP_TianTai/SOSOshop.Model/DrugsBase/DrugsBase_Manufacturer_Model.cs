using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model.DrugsBase
{
    /// <summary>
    /// DrugsBase_Manufacture表的数据模型
    /// </summary>
    public class DrugsBase_Manufacturer_Model
    {
        private int _DrugsBase_Manufacturer_ID;
        /// <summary>
        /// 生产企业ID
        /// </summary>
        public int DrugsBase_Manufacturer_ID
        {
            get { return _DrugsBase_Manufacturer_ID; }
            set { _DrugsBase_Manufacturer_ID = value; }
        }

        private string _DrugsBase_Manufacturer;
        /// <summary>
        /// 生产企业名称
        /// </summary>
        public string DrugsBase_Manufacturer1
        {
            get { return _DrugsBase_Manufacturer; }
            set { _DrugsBase_Manufacturer = value; }
        }
        /// <summary>
        /// 企业简称（只读）
        /// </summary>
        public string ManufacturerName_
        {
            get
            {
                string name = "";
                if (!string.IsNullOrEmpty(DrugsBase_Manufacturer1))
                {
                    name = DrugsBase_Manufacturer1.Replace("有限责任公司", "").Replace("股份有限公司", "").Replace("股份有限责任公司", "").Replace("有限公司", "");
                }

                return name;
            }
        }
        /// <summary>
        /// 企业名称+药品数量（只读）
        /// </summary>
        public string Manufacturer_
        {
            get
            {
                if (this.DrugNumber <= 0)
                {
                    return ManufacturerName_;
                }
                else
                {
                    return ManufacturerName_ + "(" + this.DrugNumber.ToString() + ")";
                }
            }
        }

        private string _PYJM;
        /// <summary>
        /// 企业名称全拼
        /// </summary>
        public string PYJM
        {
            get { return _PYJM; }
            set { _PYJM = value; }
        }
        /// <summary>
        /// 企业名称的第一个字母大写（只读）
        /// </summary>
        public string PYJM_FirstChar_
        {
            get
            {
                if (!string.IsNullOrEmpty(this.PYJM.Trim()))
                {
                    return this.PYJM.Substring(0, 1).ToUpper();
                }
                else
                {
                    return "";
                }
            }
        }

        private int _Province;
        /// <summary>
        /// 省份
        /// </summary>
        public int Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        private int _City;
        /// <summary>
        /// 城市
        /// </summary>
        public int City
        {
            get { return _City; }
            set { _City = value; }
        }
        
        private int _DrugNumber;
        /// <summary>
        /// 药品数量
        /// </summary>
        public int DrugNumber
        {
            get { return _DrugNumber; }
            set { _DrugNumber = value; }
        }
    }

    /// <summary>
    /// 地区
    /// </summary>
    public class Region_Model
    {
        public Region_Model()
        {
        }

        public Region_Model(int id, string name, int parentId, int depth)
        {
            this._ID = id;
            this._Name = name;
            this._ParentId = parentId;
            this._Depth = depth;
        }

        private int _ID;
        /// <summary>
        /// 地区编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _ParentId;
        /// <summary>
        /// 所属上级地区编号
        /// </summary>
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        private int _Depth;
        /// <summary>
        /// 地区级数
        /// </summary>
        public int Depth
        {
            get { return _Depth; }
            set { _Depth = value; }
        }

        private int _ManufactNum;
        /// <summary>
        /// 药厂数量
        /// </summary>
        public int ManufactNum
        {
            get { return _ManufactNum; }
            set { _ManufactNum = value; }
        }

        /// <summary>
        /// 地区名称+药厂数量（只读）
        /// </summary>
        public string Name_
        { 
            get
            {
                if (_ManufactNum <= 0)
                {
                    return _Name;
                }
                else
                {
                    return _Name + "(" + _ManufactNum.ToString() + ")";
                }
            }
        }
    }
}
