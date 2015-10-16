using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;
namespace SOSOshop.BLL.DrugsBase
{
    public class Tag_PharmAttribute : DbBase
    {
        /// <summary>
        /// 当前类的缓存依赖项
        /// </summary>
        public const string _Tag_PharmAttributeKey = "Tag_PharmAttribute";
        public Tag_PharmAttribute() { }
        /// <summary>
        /// 取得树状态标签分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string key = string.Format("Tag_PharmAttribute_GetList_{0}", 0);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                dt = base.ExecuteTable("SELECT id,name,ParentId,fullPath,ISNULL( b.Product_Count,0)count FROM dbo.[Tag_PharmAttribute] a LEFT JOIN Tag_PharmAttribute_Product_Count b ON a.id=b.Tag_PharmAttribute_id ORDER BY seq ASC,id asc");
                SetDepend(key, dt, _Tag_PharmAttributeKey);

            }
            return dt;
        }
        /// <summary>
        /// 取得树状态标签分类
        /// </summary>
        /// <param name="tag_id"></param>
        /// <returns></returns>
        public DataTable GetList(int tag_id)
        {
            string key = string.Format("Tag_PharmAttribute_GetList_{0}", tag_id);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                dt = base.ExecuteTable(string.Format("SELECT id,name,ParentId,fullPath,ISNULL( b.Product_Count,0)count FROM dbo.[Tag_PharmAttribute] a LEFT JOIN Tag_PharmAttribute_Product_Count b ON a.id=b.Tag_PharmAttribute_id WHERE tag_id={0} ORDER BY seq ASC,id asc", tag_id));
                SetDepend(key, dt, _Tag_PharmAttributeKey);

            }
            return dt;
        }
        /// <summary>
        /// 取得树状态标签分类
        /// </summary>
        /// <param name="tag_id"></param>
        /// <returns></returns>
        public DataTable GetList(int tag_id, string count)
        {
            string key = string.Format("Tag_PharmAttribute_GetList_{0}_count", tag_id);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                dt = base.ExecuteTable(string.Format("SELECT id,name,ParentId,{1} AS count FROM dbo.[Tag_PharmAttribute] AS tag WHERE tag_id={0} ORDER BY seq ASC,id asc", tag_id, count));
                SetDepend(key, dt, _Tag_PharmAttributeKey);

            }
            return dt;
        }

        /// <summary>
        /// 根据id取得树状态标签分类
        /// </summary>
        /// <param name="id">菜单的id</param>
        /// <param name="tag_id">标签id</param>
        /// <param name="isParent">是否是父菜单Id</param>
        /// <returns></returns>
        public DataTable GetListById(int id, int tag_id, bool isParent)
        {
            DataTable dt;
            string sql = string.Empty;

            if (isParent)
            {
                sql = string.Format("SELECT id,name,ParentId FROM dbo.[Tag_PharmAttribute] WHERE tag_id={0} and ParentId={1} ORDER BY seq ASC,id asc", tag_id, id);
            }
            else
            {
                sql = string.Format("SELECT id,name,ParentId FROM dbo.[Tag_PharmAttribute] WHERE tag_id={0} and ParentId IN(SELECT ParentId FROM dbo.[Tag_PharmAttribute] WHERE id={1}) ORDER BY seq ASC,id asc", tag_id, id);
            }

            dt = base.ExecuteTableForCache(sql);

            return dt;
        }

        /// <summary>
        /// 取得树状态标签分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetTagName(int id)
        {
            string sql = "SELECT name FROM dbo.[Tag_PharmAttribute] WHERE id=" + id;
            return ExecuteScalarForCache(sql) as string;
        }
        /// <summary>
        /// 取得商品数据
        /// </summary>
        /// <param name="tag_id"></param>
        /// <returns></returns>
        public int GetCount(int tag_id)
        {
            string sql = string.Format("SELECT COUNT(Product_ID) FROM dbo.product_online_v WHERE DrugsBase_ID IN ( (SELECT product_id FROM Tag_PharmProduct WHERE Tag_PharmAttribute_id in (select id from Tag_PharmAttribute where fullPath like '%/{0}/%')))", tag_id);
            return (int)ExecuteScalar(sql);
        }
        /// <summary>
        /// 取得otc经营品种总数
        /// </summary>
        /// <param name="type">1=化学药品 583=中成药</param>
        /// <returns></returns>
        public int GetTotalCount(int type)
        {
            //string sql = "SELECT COUNT(Product_ID) FROM dbo.product_online_v WHERE DrugsBase_ID IN ( (SELECT product_id FROM Tag_PharmProduct WHERE product_key='d' and Tag_PharmAttribute_id in (select id from Tag_PharmAttribute where tag_id=71))) and DrugsBase_ID in (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (SELECT Pharm_ID FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\" + type + "\\%')))";
            // string sql = "SELECT COUNT(Product_ID) FROM dbo.product_online_v WHERE is_cl='是' and DrugsBase_ID in (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (SELECT Pharm_ID FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\" + type + "\\%')))";
            string sql = "SELECT COUNT(Product_ID) FROM dbo.product_online_v WHERE DrugsBase_ID in (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (SELECT Pharm_ID FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\" + type + "\\%')))";
            return (int)base.ExecuteScalarForCache(sql);
        }
        /// <summary>
        /// 取得基药树状分类的一级分类
        /// </summary>
        /// <returns></returns>
        public string GetJyJsonlevel1()
        {
            Hashtable ht = new Hashtable();
            ht.Add(533, "化学药品及生物制品");
            ht.Add(534, "中成药");
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(ht);
        }
        /// <summary>
        /// 取得基药树状分类的二级分类
        /// </summary>
        /// <returns></returns>
        public string GetJyJsonlevel2()
        {
            string json = Get("BLL_GetJyJson") as string;
            if (json == null)
            {

                Hashtable ht = new Hashtable();
                var temp = base.ExecuteTableForCache("SELECT id,name,ParentId,b.Product_Count FROM dbo.Tag_PharmAttribute a INNER JOIN Tag_PharmAttribute_Product_Count b ON a.id=b.Tag_PharmAttribute_id WHERE tag_id=69 and id in (SELECT Tag_PharmAttribute_id FROM dbo.Tag_PharmAttribute_Product_Count WHERE Product_Count>0) order by seq desc");
                Hashtable ht1 = new Hashtable();
                foreach (var item in new int[] { 533, 534 })
                {
                    foreach (DataRow item1 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item select a))
                    {
                        ht1.Add(item1["id"], item1["name"]);
                    }
                    ht.Add(item, ht1);
                }
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ht);
                Set("BLL_GetJyJson", json);
            }
            return json;
        }
        /// <summary>
        /// 取得基药树状分类的三级分类
        /// </summary>
        /// <returns></returns>
        public string GetJyJsonlevel3()
        {
            string json = Get("BLL_GetJyJson3") as string;
            if (json == null)
            {
                Hashtable ht = new Hashtable();
                Hashtable ht1 = new Hashtable();
                var temp = base.ExecuteTableForCache("SELECT id,name,ParentId,b.Product_Count FROM dbo.Tag_PharmAttribute a INNER JOIN Tag_PharmAttribute_Product_Count b ON a.id=b.Tag_PharmAttribute_id WHERE tag_id=69 and id in (SELECT Tag_PharmAttribute_id FROM dbo.Tag_PharmAttribute_Product_Count WHERE Product_Count>0) order by seq desc");
                foreach (var item in new int[] { 533, 534 })
                {
                    foreach (var item1 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item select a.Field<int>("id")))
                    {
                        foreach (DataRow item12 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item1 select a))
                        {
                            ht1.Add(item12["id"], item12["name"]);
                        };
                        ht.Add(item1, ht1);
                    }

                }
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ht);
                Set("BLL_GetJyJson3", json);
            }
            return json;
        }
        /// <summary>
        /// 取得基药树状分类的四级分类
        /// </summary>
        /// <returns></returns>
        public string GetJyJsonlevel4()
        {
            string json = Get("BLL_GetJyJson4") as string;
            if (json == null)
            {
                Hashtable ht = new Hashtable();
                Hashtable ht1 = new Hashtable();
                var temp = base.ExecuteTableForCache("SELECT id,name,ParentId,b.Product_Count FROM dbo.Tag_PharmAttribute a INNER JOIN Tag_PharmAttribute_Product_Count b ON a.id=b.Tag_PharmAttribute_id WHERE tag_id=69 and id in (SELECT Tag_PharmAttribute_id FROM dbo.Tag_PharmAttribute_Product_Count WHERE Product_Count>0) order by seq desc");
                foreach (var item in new int[] { 533, 534 })
                {
                    foreach (var item1 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item select a.Field<int>("id")))
                    {
                        foreach (var item2 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item1 select a.Field<int>("id")))
                        {
                            foreach (DataRow item13 in (from a in temp.AsEnumerable() where a.Field<int>("ParentId") == item2 select a))
                            {
                                ht1.Add(item13["id"], item13["name"]);
                            };
                            ht.Add(item2, ht1);
                        }
                    }

                }
                json = Newtonsoft.Json.JsonConvert.SerializeObject(ht);
                Set("BLL_GetJyJson4", json);
            }
            return json;
        }
        /// <summary>
        /// 取得搜索列表选择了otc的关键字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetKeyWord(int id)
        {
            string sql = "SELECT fullPath FROM dbo.Tag_PharmAttribute WHERE id=" + id;
            string fullPath = ExecuteScalarForCache(sql) as string;
            if (fullPath == null) return "";
            sql = string.Format("SELECT name FROM dbo.Tag_PharmAttribute WHERE id IN ({0})", fullPath.Trim('/').Replace('/', ','));
            return string.Join(">", ExecuteTableForCache(sql).AsEnumerable().Select(x => x.Field<string>("name")));
        }
        /// <summary>
        /// 取得前台在经营的基药分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetJYList()
        {
            string key = "BLL_DrugsBase_Tag_PharmAttribute_GetJYList";
            DataTable dt = Get(key) as DataTable;
            if (dt == null)
            {
                dt = ExecuteTable("SELECT id,name,ParentId,0 Product_Count FROM dbo.Tag_PharmAttribute WHERE tag_id=69 order by seq desc");
                DataTable dt2 = ExecuteTable("SELECT (SELECT TOP 1 fullPath FROM dbo.Tag_PharmAttribute WHERE id=(SELECT TOP 1 Tag_PharmAttribute_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM  dbo.Tag_PharmAttribute WHERE tag_id=69) AND dbo.Tag_PharmProduct.product_id=t.DrugsBase_ID AND dbo.Tag_PharmProduct.product_key='d')) fullPath from product_online_v T  WHERE 1=1  and DrugsBase_ID in (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM Tag_PharmAttribute WHERE tag_id=69)) and (goods_id in (select goods_id from [exchange].[dbo].[LinkRegionBidPricing]) or goods_id in (select goods_id from [exchange].[dbo].[LinkRegionLimitPricing])) and price_01>0");
                foreach (DataRow item in dt.Rows)
                {
                    item["Product_Count"] = dt2.AsEnumerable().Where(x => x.Field<string>("fullPath").Contains("/" + item["id"] + "/")).Count();
                }
                dt = dt.AsEnumerable().Where(x => x.Field<int>("Product_Count") > 0).CopyToDataTable();
                Set(key, dt, DateTime.Now.AddHours(3));
            }
            return dt;
        }
        /// <summary>
        /// 取得前台在经营的基药分类
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public DataTable GetJYList(string ids)
        {
            string key = "BLL_DrugsBase_Tag_PharmAttribute_GetJYList_" + ids;
            DataTable dt = Get(key) as DataTable;
            if (dt == null)
            {
                dt = ExecuteTable("SELECT id,name,ParentId,0 Product_Count FROM dbo.Tag_PharmAttribute WHERE tag_id=69  AND id IN (" + ids + ") order by seq desc");
                DataTable dt2 = ExecuteTable("SELECT (SELECT TOP 1 fullPath FROM dbo.Tag_PharmAttribute WHERE id=(SELECT TOP 1 Tag_PharmAttribute_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM  dbo.Tag_PharmAttribute WHERE tag_id=69) AND dbo.Tag_PharmProduct.product_id=t.DrugsBase_ID AND dbo.Tag_PharmProduct.product_key='d')) fullPath from product_online_v T  WHERE 1=1  and DrugsBase_ID in (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM Tag_PharmAttribute WHERE tag_id=69)) and (goods_id in (select goods_id from [exchange].[dbo].[LinkRegionBidPricing]) or goods_id in (select goods_id from [exchange].[dbo].[LinkRegionLimitPricing])) and price_01>0");
                foreach (DataRow item in dt.Rows)
                {
                    item["Product_Count"] = dt2.AsEnumerable().Where(x => x.Field<string>("fullPath").Contains("/" + item["id"] + "/")).Count();
                }
                dt = dt.AsEnumerable().Where(x => x.Field<int>("Product_Count") > 0).CopyToDataTable();
                Set(key, dt, DateTime.Now.AddHours(3));
            }
            return dt;
        }
    }
}
