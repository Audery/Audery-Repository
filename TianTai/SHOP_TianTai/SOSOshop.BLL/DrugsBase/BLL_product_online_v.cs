using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SOSOshop.Model.DrugsBase;

namespace SOSOshop.BLL.DrugsBase
{
    /// <summary>
    /// OnLineProducts视图的数据访问层
    /// </summary>
    public class BLL_product_online_v : DbBase
    {
        /// <summary>
        /// 药品
        /// </summary>
        public BLL_product_online_v() { }

        #region 销量最多或点击量最大
        /// <summary>
        /// 获取指定生产厂家的销量最多或点击量最大的药品
        /// </summary>
        /// <param name="drugsBase_Manufacturer">药厂名称</param>
        /// <param name="orderFieldName">排序字段</param>
        /// <returns></returns>
        public List<product_online_v_Model> GetHotAndClickDrugs(string drugsBase_Manufacturer, string orderFieldName)
        {
            try
            {
                List<product_online_v_Model> objs = new List<product_online_v_Model>();

                string sql = string.Format(@"SELECT TOP 6 a.Product_ID, 
                                                    a.Goods_ID, 
                                                    a.DrugsBase_ID, 
                                                    a.Goods_Package_ID, 
                                                    a.DrugsBase_DrugName, 
                                                    a.DrugsBase_Manufacturer, 
                                                    a.DrugsBase_Specification, 
                                                    a.DrugsBase_Formulation, 
                                                    a.Image,
                                                    a.created,
                                                    a.Product_SaleNum, 
                                                    a.Product_ClickNum,

                                                    a.Goods_ConveRatio_Unit_Name,
                                                    a.Goods_ConveRatio_Unit,
                                                    a.Goods_ConveRatio,
                                                    a.Goods_Unit

                                            FROM dbo.product_online_v a
                                            WHERE a.DrugsBase_Manufacturer = '{0}'
                                            ORDER BY {1} DESC ",
                                                    drugsBase_Manufacturer, orderFieldName);

                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        product_online_v_Model obj = new product_online_v_Model();
                        obj.Product_ID = (int)rowObj["Product_ID"];
                        obj.Goods_ID = (int)rowObj["Goods_ID"];
                        obj.DrugsBase_ID = (int)rowObj["DrugsBase_ID"];   //药品Id
                        obj.Goods_Package_ID = (int)rowObj["Goods_Package_ID"];

                        obj.DrugsBase_DrugName = rowObj["DrugsBase_DrugName"].ToString().Trim();  //药品名称
                        obj.DrugsBase_Manufacturer = drugsBase_Manufacturer; //生产企业名称

                        obj.DrugsBase_Specification = GetSpecificationAndS(rowObj); //规格
                        obj.DrugsBase_Formulation = rowObj["DrugsBase_Formulation"].ToString().Trim();

                        obj.Image = GetImagePath(rowObj["Image"].ToString());    //图片地址

                        obj.created = (DateTime)rowObj["created"]; //上架时间
                        obj.Product_SaleNum = (int)rowObj["Product_SaleNum"];  //销量

                        obj.Product_ClickNum = (int)rowObj["Product_ClickNum"];  //浏览量

                        objs.Add(obj);
                    }
                }

                return objs;
            }
            catch
            {
                return null;
            }
        }
        #endregion 

        #region 指定生产厂家的全部药品
        /// <summary>
        /// 获取指定生产厂家的全部药品
        /// </summary>
        /// <param name="drugsBase_Manufacturer">药厂名称</param>
        /// <returns></returns>
        public List<product_online_v_Model> GetDrugsList(string drugsBase_Manufacturer)
        {
            List<product_online_v_Model> drugsInfos = GetDrugsListInfo(drugsBase_Manufacturer); //指定药厂的全部药品
            List<PharmInfo> secondPharms = GetPharmInfo(2);         //二级分类的基础信息

            if (drugsInfos != null && drugsInfos.Count > 0 && secondPharms != null && secondPharms.Count > 0)
            {
                foreach (PharmInfo s in secondPharms)
                {
                    //查询药品的分类路径是否包含二级分类的分类路径
                    List<product_online_v_Model> tmpDrugs = drugsInfos.FindAll(d => d.Pharm_ID_Path.Contains(s.Pharm_ID_Path));
                    if (tmpDrugs != null && tmpDrugs.Count > 0)
                    {
                        foreach (product_online_v_Model d in tmpDrugs)
                        {
                            //将二级分类Id作为药品的Id，供分类导航使用
                            d.Pharm_ID = s.Pharm_ID;
                        }
                    }
                }
            }

            //中药饮片
            List<product_online_v_Model> ZYCInfos = GetZYCListInfo(drugsBase_Manufacturer);
            if (ZYCInfos != null && ZYCInfos.Count > 0)
            {
                drugsInfos.AddRange(ZYCInfos);
            }

            return drugsInfos;
        }

        /// <summary>
        /// 获取指定生产厂家的全部药品信息
        /// </summary>
        /// <param name="drugsBase_Manufacturer">药厂名称</param>
        /// <returns></returns>
        private List<product_online_v_Model> GetDrugsListInfo(string drugsBase_Manufacturer)
        {
            try
            {
                List<product_online_v_Model> objs = new List<product_online_v_Model>();

                string sql = string.Format(@"SELECT a.Product_ID, 
                                                    a.Goods_ID, 
                                                    a.DrugsBase_ID, 
                                                    a.Goods_Package_ID, 
                                                    a.DrugsBase_DrugName, 
                                                    a.DrugsBase_Manufacturer, 
                                                    a.DrugsBase_Specification, 
                                                    a.DrugsBase_Formulation, 
                                                    a.Image,
                                                    b.Pharm_ID,
	                                                c.Pharm_ID_Path AS Pharm_ID_Path,

                                                    a.Goods_ConveRatio_Unit_Name,
                                                    a.Goods_ConveRatio_Unit,
                                                    a.Goods_ConveRatio,
                                                    a.Goods_Unit

                                            FROM dbo.product_online_v a, dbo.DrugsBase_PharmMediNameLink b, dbo.DrugsBase_Pharm c 
                                            WHERE a.DrugsBase_Manufacturer = '{0}' 
                                            AND b.DrugsBase_ID = a.DrugsBase_ID 
                                            AND b.Pharm_ID = c.Pharm_ID",
                                                    drugsBase_Manufacturer);
                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));
                
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        product_online_v_Model obj = new product_online_v_Model();
                        obj.Product_ID = (int)rowObj["Product_ID"];
                        obj.Goods_ID = (int)rowObj["Goods_ID"];
                        obj.DrugsBase_ID = (int)rowObj["DrugsBase_ID"];   //药品Id
                        obj.Goods_Package_ID = (int)rowObj["Goods_Package_ID"]; 
                        
                        obj.DrugsBase_DrugName = rowObj["DrugsBase_DrugName"].ToString().Trim();  //药品名称
                        obj.DrugsBase_Manufacturer = drugsBase_Manufacturer; //生产企业名称

                        obj.DrugsBase_Specification = GetSpecificationAndS(rowObj); //规格
                        obj.DrugsBase_Formulation = rowObj["DrugsBase_Formulation"].ToString().Trim();

                        obj.Image = GetImagePath(rowObj["Image"].ToString());    //图片地址

                        obj.Pharm_ID = (int)rowObj["Pharm_ID"];  //分类Id
                        obj.Pharm_ID_Path = rowObj["Pharm_ID_Path"].ToString();  //分类路径
                        
                        objs.Add(obj);
                    }
                }

                return objs;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定生产厂家的中药饮片信息
        /// </summary>
        /// <param name="drugsBase_Manufacturer">药厂名称</param>
        /// <returns></returns>
        private List<product_online_v_Model> GetZYCListInfo(string drugsBase_Manufacturer)
        {
            try
            {
                List<product_online_v_Model> objs = new List<product_online_v_Model>();

                string sql = string.Format(@"SELECT a.Product_ID, 
                                                    a.Goods_ID, 
                                                    a.DrugsBase_ID, 
                                                    a.Goods_Package_ID, 
                                                    a.DrugsBase_DrugName, 
                                                    a.DrugsBase_Manufacturer, 
                                                    a.DrugsBase_Specification, 
                                                    a.DrugsBase_Formulation, 
                                                    a.Image,
                                                    b.ProductionClassId AS Pharm_Id,
                                                    c.ProductionClassName AS Pharm_Name,

                                                    a.Goods_ConveRatio_Unit_Name,
                                                    a.Goods_ConveRatio_Unit,
                                                    a.Goods_ConveRatio,
                                                    a.Goods_Unit

                                            FROM dbo.product_online_v a, dbo.DrugsBase_ZYC b, dbo.DrugsBase_ZYC_ProductionClass c 
                                            WHERE a.DrugsBase_Manufacturer = '{0}' 
                                            AND b.DrugsBase_ID = a.DrugsBase_ID 
                                            AND b.ProductionClassId = c.ProductionClassId",
                                                    drugsBase_Manufacturer);
                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        product_online_v_Model obj = new product_online_v_Model();
                        obj.Product_ID = (int)rowObj["Product_ID"];
                        obj.Goods_ID = (int)rowObj["Goods_ID"];
                        obj.DrugsBase_ID = (int)rowObj["DrugsBase_ID"];   //药品Id
                        obj.Goods_Package_ID = (int)rowObj["Goods_Package_ID"];

                        obj.DrugsBase_DrugName = rowObj["DrugsBase_DrugName"].ToString().Trim();  //药品名称
                        obj.DrugsBase_Manufacturer = drugsBase_Manufacturer; //生产企业名称

                        obj.DrugsBase_Specification = GetSpecificationAndS(rowObj); //规格
                        obj.DrugsBase_Formulation = rowObj["DrugsBase_Formulation"].ToString().Trim();

                        obj.Image = GetImagePath(rowObj["Image"].ToString());    //图片地址

                        obj.Pharm_ID = (int)rowObj["Pharm_ID"];  //分类Id
                        obj.Pharm_ID_Path = Enum_中药饮片.中药饮片.ToString();

                        objs.Add(obj);
                    }
                }

                return objs;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 指定厂家的二级药品分类信息
        /// <summary>
        /// 获取指定厂家的二级药品分类
        /// </summary>
        /// <param name="drugsBase_Manufacturer">厂家名称</param>
        /// <returns></returns>
        public List<DrugsPharm> GetDrugsPharm(string drugsBase_Manufacturer)
        {
            List<DrugsPharm> rtnObjs = new List<DrugsPharm>();
            
            List<DrugsPharm> drugsPharms = GetDrugsPharmInfo(drugsBase_Manufacturer); //指定药厂的药品分类
            List<PharmInfo> secondPharms = GetPharmInfo(2);  //二级分类的基础信息

            if (drugsPharms != null && drugsPharms.Count > 0 && secondPharms != null && secondPharms.Count > 0)
            {
                foreach (PharmInfo s in secondPharms)
                {
                    if (s.Pharm_ID_Path != ((int)Enum_中药饮片.中药饮片).ToString())
                    {
                        //查询药品的分类路径是否包含二级分类的分类路径
                        List<DrugsPharm> tmpDrugs = drugsPharms.FindAll(d => d.Pharm_ID_Path.Contains(s.Pharm_ID_Path));
                        if (tmpDrugs != null && tmpDrugs.Count > 0)
                        {
                            DrugsPharm tmpDrug = new DrugsPharm();
                            tmpDrug.DrugsBase_Manufacturer = drugsBase_Manufacturer;
                            tmpDrug.Pharm_ID = s.Pharm_ID;
                            tmpDrug.Pharm_Name = s.Pharm_Name;
                            tmpDrug.Pharm_ID_Path = s.Pharm_ID_Path;

                            //根级分类Id(二级分类的父Id即为该药品的根Id)
                            tmpDrug.Root_Pharm_ID = s.Pharm_Parent_ID;

                            //获取根级分类名称
                            string rootName = s.Pharm_Name_Path;
                            if (!string.IsNullOrEmpty(s.Pharm_Name_Path) && s.Pharm_Name_Path.Contains(@"\"))
                            {
                                int i = s.Pharm_Name_Path.IndexOf(@"\");
                                rootName = s.Pharm_Name_Path.Substring(0, i);
                            }
                            tmpDrug.Root_Pharm_Name = rootName;

                            int num = 0;
                            foreach (DrugsPharm d in tmpDrugs)
                            {
                                num += d.DrugNumber;
                            }
                            tmpDrug.DrugNumber = num;

                            rtnObjs.Add(tmpDrug);
                        }
                    }
                }
            }

            //指定药厂的中药饮片分类
            List<DrugsPharm> ZYCPharms = GetZYCPharmInfo(drugsBase_Manufacturer); 
            if (ZYCPharms != null && ZYCPharms.Count > 0)
            {
                rtnObjs.AddRange(ZYCPharms);
            }

            List<DrugsPharm> results = rtnObjs.OrderByDescending(d => d.DrugNumber).OrderBy(d => d.Root_Pharm_ID).ToList<DrugsPharm>();
            return results;
        }

        /// <summary>
        /// 获取指定厂家的药品分类信息
        /// </summary>
        /// <param name="drugsBase_Manufacturer">厂家名称</param>
        /// <returns></returns>
        private List<DrugsPharm> GetDrugsPharmInfo(string drugsBase_Manufacturer)
        {
            List<DrugsPharm> rtnObjs = null;

            try
            {
                string sql = string.Format(@"SELECT DISTINCT t1.*, t2.DrugNumber FROM

                                            (SELECT  
                                                a.DrugsBase_Manufacturer AS DrugsBase_Manufacturer,
                                                c.Pharm_ID_Path AS Pharm_ID_Path,
                                                c.Pharm_Name_Path AS Pharm_Name_Path,
                                                c.Pharm_ID AS Pharm_ID,  
                                                c.Pharm_Name AS Pharm_Name
                                            FROM 
                                                dbo.product_online_v a,  
                                                dbo.DrugsBase_PharmMediNameLink b , 
                                                dbo.DrugsBase_Pharm c 
                                            WHERE 
                                                a.DrugsBase_Manufacturer = '{0}' AND   
                                                b.DrugsBase_ID = a.DrugsBase_ID AND  
                                                b.Pharm_ID = c.Pharm_ID) AS t1,
        
                                            (SELECT  
                                                b.pharm_ID AS pharmID,
                                                count(b.Pharm_ID) AS DrugNumber
                                            FROM 
                                                dbo.product_online_v a,  
                                                dbo.DrugsBase_PharmMediNameLink b
                                            WHERE 
                                                a.DrugsBase_Manufacturer = '{0}' AND   
                                                b.DrugsBase_ID = a.DrugsBase_ID                                                    
                                            GROUP BY b.Pharm_ID) AS t2

                                            WHERE t1.pharm_ID=t2.pharmID
                                            ORDER BY DrugNumber DESC", drugsBase_Manufacturer); 

                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    rtnObjs = new List<DrugsPharm>();
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        DrugsPharm obj = new DrugsPharm();
                        obj.DrugsBase_Manufacturer = drugsBase_Manufacturer;
                        obj.Pharm_ID_Path = rowObj["Pharm_ID_Path"].ToString();
                        obj.Pharm_ID = (int)rowObj["Pharm_ID"];
                        obj.Pharm_Name = rowObj["Pharm_Name"].ToString();
                        obj.DrugNumber = (int)rowObj["DrugNumber"];
                        obj.Root_Pharm_Name = rowObj["Pharm_Name_Path"].ToString();

                        rtnObjs.Add(obj);
                    }
                }
            }
            catch
            {
            }

            return rtnObjs;
        }


        /// <summary>
        /// 获取指定厂家的中药饮片分类信息
        /// </summary>
        /// <param name="drugsBase_Manufacturer">厂家名称</param>
        /// <returns></returns>
        private List<DrugsPharm> GetZYCPharmInfo(string drugsBase_Manufacturer)
        {
            List<DrugsPharm> rtnObjs = null;

            try
            {
                string sql = string.Format(@"SELECT DISTINCT t1.*, t2.DrugNumber FROM
                                            (SELECT  
                                                a.DrugsBase_Manufacturer AS DrugsBase_Manufacturer,
                                                b.ProductionClassId AS Pharm_ID,
                                                c.ProductionClassName AS Pharm_Name
                                            FROM 
                                                dbo.product_online_v a,  
                                                dbo.DrugsBase_ZYC b ,
                                                dbo.DrugsBase_ZYC_ProductionClass c
                                            WHERE 
                                                a.DrugsBase_Manufacturer = '{0}' AND   
                                                b.DrugsBase_ID = a.DrugsBase_ID AND
                                                b.ProductionClassId = c.ProductionClassId) AS t1,

                                            (SELECT  
                                                b.ProductionClassId AS pharmID,
                                                count(b.ProductionClassId) AS DrugNumber
                                            FROM 
                                                dbo.product_online_v a,  
                                                dbo.DrugsBase_ZYC b
                                            WHERE 
                                                a.DrugsBase_Manufacturer  = '{0}' AND   
                                                b.DrugsBase_ID = a.DrugsBase_ID                                                    
                                            GROUP BY b.ProductionClassId) AS t2

                                            WHERE t1.pharm_ID=t2.pharmID
                                            ORDER BY DrugNumber DESC;", drugsBase_Manufacturer);

                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    rtnObjs = new List<DrugsPharm>();
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        DrugsPharm obj = new DrugsPharm();
                        obj.DrugsBase_Manufacturer = drugsBase_Manufacturer;
                        obj.Pharm_ID_Path = ((int)Enum_中药饮片.中药饮片).ToString();
                        obj.Pharm_ID = (int)rowObj["Pharm_ID"];
                        obj.Pharm_Name = rowObj["Pharm_Name"].ToString();
                        obj.DrugNumber = (int)rowObj["DrugNumber"];
                        obj.Root_Pharm_ID = (int)Enum_中药饮片.中药饮片;
                        obj.Root_Pharm_Name = Enum_中药饮片.中药饮片.ToString();

                        rtnObjs.Add(obj);
                    }
                }
            }
            catch
            {
            }

            return rtnObjs;
        }

        /// <summary>
        /// 获取二级分类信息
        /// </summary>
        /// <returns></returns>
        private List<PharmInfo> GetPharmInfo(int pharmLevel)
        {
            try
            {
                List<PharmInfo> objs = new List<PharmInfo>();

                string sql = string.Format(@"SELECT * FROM [dbo].[DrugsBase_Pharm] WHERE Pharm_Level={0} AND Pharm_Name_Path NOT LIKE '中药饮片%'", pharmLevel);
                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        PharmInfo obj = new PharmInfo();
                        obj.Pharm_ID = (int)rowObj["Pharm_ID"];
                        obj.Pharm_Name = rowObj["Pharm_Name"].ToString().Trim();
                        obj.Pharm_Parent_ID = (int)rowObj["Pharm_Parent_ID"];
                        obj.Pharm_Level = (int)rowObj["Pharm_Level"];
                        obj.Pharm_ID_Path = rowObj["Pharm_ID_Path"].ToString().Trim();
                        obj.Pharm_Name_Path = rowObj["Pharm_Name_Path"].ToString().Trim();  

                        objs.Add(obj);
                    }
                }

                //中药饮片 从另外的表取分类
                sql = "SELECT * FROM DrugsBase_ZYC_ProductionClass";
                dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        PharmInfo obj = new PharmInfo();
                        obj.Pharm_ID = (int)rowObj["ProductionClassId"];
                        obj.Pharm_Name = rowObj["ProductionClassName"].ToString().Trim();
                        obj.Pharm_Parent_ID = (int)Enum_中药饮片.中药饮片;
                        obj.Pharm_Level = 2;
                        obj.Pharm_ID_Path = rowObj["ProductionClassPath"].ToString().Trim();
                        obj.Pharm_Name_Path = Enum_中药饮片.中药饮片.ToString();

                        objs.Add(obj);
                    }
                }

                return objs;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static string GetSpecification(DataRow dr)
        {
            return SOSOshop.BLL.Order.OrderProduct.GetSpecification(dr).Trim('x');
        }
        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static string GetSpecificationAndS(DataRow dr)
        {
            return string.Format("{0}x{1}", dr["DrugsBase_Specification"], SOSOshop.BLL.Order.OrderProduct.GetSpecification(dr)).TrimStart('x');
        }
        /// <summary>
        /// 取得网站图片地址
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        private static string GetImagePath(object ImagePath)
        {
            if (Library.Lang.DataValidator.isNULL(ImagePath))
            {
                return System.Configuration.ConfigurationManager.AppSettings["101ShopUrl"] + "images/nopic1.jpg";
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings["imageUrl"] + ImagePath;
            }
        }

        public enum Enum_中药饮片
        {
            中药饮片 = 999999
        }
    }
}
