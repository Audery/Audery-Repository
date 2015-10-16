using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 价格维护类
    /// </summary>
    public class Price : DbBase
    {
        Memcached.ClientLibrary.MemcachedClient mc;
        public Price()
        {
            mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
        }
        /// <summary>
        /// 重新重新价格至价格缓存
        /// </summary>
        /// <param name="iden"></param>
        /// <param name="Product_ID"></param>
        public void InitPriceCache(string id)
        {
            Dictionary<string, decimal> price = new Dictionary<string, decimal>();
            foreach (var item in new ConfigPriceMe().GetList())
            {
                price.Add(item.name, 0M);
            }
            int spid = 0;
            string sql = string.Format("SELECT b.id,a.category,a.Price_N FROM dbo.Price a INNER JOIN Product_DEF b ON a.ID=b.Product_id AND a.iden=b.iden WHERE a.id='{0}'", id);
            foreach (DataRow item in ExecuteTable(sql).Rows)
            {
                spid = (int)item["id"];
                price[(string)item["category"]] = (decimal)item["Price_N"];
            }
            mc.Set("hl_p_" + spid, price, DateTime.Now.AddYears(1));
        }

        /// <summary>
        /// 价格计算
        /// </summary>
        public void InitPrice()
        {
            long start = Environment.TickCount;
            string sql = "SELECT * FROM Price_TEMP EXCEPT SELECT id,iden,category,price FROM dbo.Price";
            DataTable dt = ExecuteTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                string id = (string)item["id"];
                string category = (string)item["category"];
                if (!Exist(id, category))
                {
                    Insert(id, category);
                }
                else
                {
                    Update(id, category);                    
                }
            }
            sql = "SELECT id,iden,category,price FROM dbo.Price WHERE Price_N<>0 EXCEPT SELECT * FROM Price_TEMP";
            dt = ExecuteTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                string id = (string)item["id"];
                string category = (string)item["category"];
                Disable(id, category);
                InitPriceCache(id);
            }
            Product_Centre.AddLog("执行了商品价格处理,共执行" + (Environment.TickCount - start) + "毫秒!", 1);
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Exist(string id, string category)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.Price WHERE ID='{0}' AND category='{1}'", id, category);
            return (int)ExecuteScalar(sql) > 0;
        }
        /// <summary>
        /// 添加价格
        /// </summary>
        public void Insert(string id, string category)
        {
            string sql = string.Format("INSERT INTO Price (id,iden,category,price,price_n) SELECT id,iden,category,price,price FROM Price_TEMP WHERE ID='{0}' AND category='{1}'", id, category);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        public void Update(string id, string category)
        {
            string sql = string.Format("UPDATE Price SET price=b.price FROM Price a INNER JOIN Price_TEMP b ON a.ID=b.id AND a.category=b.category WHERE b.ID='{0}' AND b.category='{1}'", id, category);
            ExecuteNonQuery(sql);
            //价格含义
            object o = ExecuteScalar(string.Format("SELECT TOP 1 sum FROM dbo.Link_Mid WHERE id=(SELECT id FROM dbo.Link WHERE t_id='{0}') AND PriceType<>1", id));
            int sum = 0;
            if (!Library.Lang.DataValidator.isNULL(o))
            {
                sum = (int)o;
            }
            //加点计算
            decimal d = (decimal)ExecuteScalar(string.Format("SELECT Price_Plus FROM dbo.Price WHERE ID='{0}' AND category='{1}'", id, category));
            if (d > 0)
            {
                if (sum != 0)
                {
                    ExecuteNonQuery(string.Format("UPDATE Price SET Price_N=Price/{2}*Price_Plus  WHERE ID='{0}' AND category='{1}'", id, category, sum));
                }
                else
                {
                    ExecuteNonQuery(string.Format("UPDATE Price SET Price_N=Price*Price_Plus  WHERE ID='{0}' AND category='{1}'", id, category));
                }

            }
            else
            {
                decimal Price_Plus = new ConfigPriceMe().GetPrice_Plus(category);
                if (sum != 0)
                {
                    ExecuteNonQuery(string.Format("UPDATE Price SET Price_N=Price/{2}*{3}  WHERE ID='{0}' AND category='{1}'", id, category, sum, Price_Plus));
                }
                else
                {
                    ExecuteNonQuery(string.Format("UPDATE Price SET Price_N=Price*{2}  WHERE ID='{0}' AND category='{1}'", id, category, Price_Plus));
                }
            }
            InitPriceCache(id);
        }

        /// <summary>
        /// 更新此品种的所有价格
        /// </summary>
        /// <param name="id"></param>
        public void Updates(string id)
        {
            foreach (DataRow item in ExecuteTable("SELECT id,category FROM dbo.Price where ID='" + Library.Lang.Input.Filter(id) + "'").Rows)
            {
                Update((string)item["id"], (string)item["category"]);
            }
        }

        /// <summary>
        /// 禁用价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        public void Disable(string id, string category)
        {
            string sql = string.Format(" UPDATE Price SET price=0,Price_N=0 WHERE ID='{0}' AND category='{1}'", id, category);
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 每天晚上重新计算一次所有价格(防止价格有异常)
        /// </summary>
        public static void InitAllPrice()
        {
            Price bll = new Price();
            foreach (DataRow item in bll.ExecuteTable("SELECT t_id FROM dbo.Link").Rows)
            {
                bll.Updates(item["t_id"].ToString());
            }
        }
    }
}