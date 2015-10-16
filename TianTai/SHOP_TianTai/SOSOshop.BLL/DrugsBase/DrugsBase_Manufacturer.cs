using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SOSOshop.Model.DrugsBase;
namespace SOSOshop.BLL.DrugsBase
{
    /// <summary>
    /// 厂家
    /// </summary>
    public class DrugsBase_Manufacturer : DbBase
    {
        /// <summary>
        /// 当前类的缓存依赖项
        /// </summary>
        public const string dependkey = "DrugsBase_Manufacturer";
        /// <summary>
        /// 厂家
        /// </summary>
        public DrugsBase_Manufacturer() { }
        /// <summary>
        /// 取得厂家列表[缓存]
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string key = string.Format(dependkey + "_{0}", 0);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                string sql = "SELECT * FROM DrugsBase_Manufacturer";
                dt = base.ExecuteTable(sql);
                SetDepend(key, dt, dependkey);

            }

            return dt;
        }

        /// <summary>
        /// 取得药品的厂家
        /// </summary>
        /// <param name="DrugsBase_ID">药品ID</param>
        /// <returns></returns>
        public DataTable GetListByDrugsBase_ID(int DrugsBase_ID)
        {
            string sql = string.Format("SELECT * FROM DrugsBase_Manufacturer " +
                "WHERE DrugsBase_Manufacturer_ID IN (SELECT DISTINCT DrugsBase_Manufacturer_ID FROM DrugsBase " +
                "WHERE DrugsBase_DrugName_ID=(SELECT DrugsBase_DrugName_ID FROM DrugsBase WHERE DrugsBase_ID={0}))", DrugsBase_ID);
            DataSet ds = ExecuteDataSet(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 取得药品的其它生产厂家
        /// </summary>
        /// <param name="topCount">查询条数</param>
        /// <param name="DrugsBase_ID">药品ID</param>
        /// <param name="manufacturer">生产厂家名称</param>
        /// <returns></returns>
        public DataTable GetOhterManufacturerList(int topCount, string product_name, string manufacturer)
        {
            string sql = string.Format("select distinct top {0} DrugsBase_Manufacturer " +
                        "from product_online_v " +  
                        "where Product_Name = '{1}' " +
                        "and DrugsBase_Manufacturer<>'{2}'", topCount, product_name, manufacturer);
            DataSet ds = base.ExecuteDataSetForCache(sql,DateTime.Now.AddDays(3));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }



        /// <summary>
        /// 获取药厂的基础数据
        /// </summary>
        /// <returns></returns>
        public List<DrugsBase_Manufacturer_Model> GetManufactInitData()
        {
            try
            {
                List<DrugsBase_Manufacturer_Model> objs = new List<DrugsBase_Manufacturer_Model>();
                string sql = @"SELECT t1.*, t2.DrugNumber AS DrugNumber
                            FROM
                                (SELECT DrugsBase_Manufacturer_ID, DrugsBase_Manufacturer 
                                FROM DrugsBase_Manufacturer 
                                WHERE DrugsBase_Manufacturer IN (SELECT DrugsBase_Manufacturer FROM product_online_v)) AS t1,

                                (SELECT  
                                    a.DrugsBase_Manufacturer AS DrugsBase_Manufacturer,
                                    count(a.DrugsBase_Manufacturer) AS DrugNumber
                                FROM 
                                    dbo.product_online_v a
                                GROUP BY a.DrugsBase_Manufacturer) AS t2

                            WHERE t1.DrugsBase_Manufacturer=t2.DrugsBase_Manufacturer
                            ORDER BY t2.DrugNumber DESC";

                DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddDays(5));

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        DrugsBase_Manufacturer_Model obj = new DrugsBase_Manufacturer_Model();
                        int id;
                        if (int.TryParse(rowObj["DrugsBase_Manufacturer_ID"].ToString(), out id))
                        {
                            obj.DrugsBase_Manufacturer_ID = id;
                        }

                        obj.DrugsBase_Manufacturer1 = rowObj["DrugsBase_Manufacturer"].ToString().Trim();
                        obj.DrugNumber = (int)rowObj["DrugNumber"];

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

    }
}
