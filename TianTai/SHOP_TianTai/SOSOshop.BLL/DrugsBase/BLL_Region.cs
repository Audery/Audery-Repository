using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SOSOshop.Model.DrugsBase;

namespace SOSOshop.BLL.DrugsBase
{
    public class BLL_Region : DbBase
    {
        /// <summary>
        /// 地区
        /// </summary>
        public BLL_Region() { }

        

        /// <summary>
        /// 取得地区的基础数据(省名称和直辖市)
        /// </summary>
        /// <returns></returns>
        private DataTable GetRegionInitData()
        {
            string sql = string.Format(@"SELECT id, name, parentId, depth FROM Region WHERE depth<=2 ORDER BY id ASC");
            DataTable dt = base.ExecuteTableForCache(sql,DateTime.Now.AddDays(30));

            return dt;
        }


        /// <summary>
        /// 获取地区的基础数据
        /// </summary>
        /// <returns></returns>
        public List<Region_Model> GetRegionList()
        {
            try
            {
                List<Region_Model> objs = new List<Region_Model>();

                DataTable dt = GetRegionInitData();

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rowObj in dt.Rows)
                    {
                        Region_Model obj = new Region_Model();
                        obj.ID = (int)rowObj["ID"]; 
                        obj.Name = rowObj["Name"].ToString().Trim();
                        obj.ParentId = (int)rowObj["ParentId"];
                        obj.Depth = (int)rowObj["Depth"];

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
