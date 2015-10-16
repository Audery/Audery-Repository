using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;
using System.Data;
namespace SOSOshop.BLL.JTTX
{
    /// <summary>
    /// 价格体系
    /// </summary>
    [Serializable]
    public class Price
    {

        public SOSOshop.BLL.Db bll = new Db();
        Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
        public Price()
        {
            mc.PoolName = "Price_Cache";
        }
        public Dictionary<string, decimal> GetModel(int productId)
        {
            string key = "hl_p_" + productId;
            Dictionary<string, decimal> price = mc.Get(key) as Dictionary<string, decimal>;
            if (price == null)
            {
                price = new Dictionary<string, decimal>();
                bll.ChangeDataCenter();
                string sql = string.Format("SELECT a.category,a.Price_N FROM dbo.Price a INNER JOIN dbo.Product_DEF b ON a.ID=b.Product_id AND a.iden=b.iden WHERE b.id='{0}'", productId);
                var dt = bll.ExecuteTable(sql);
                foreach (DataRow item in dt.Rows)
                {
                    price.Add(item["category"].ToString(), (decimal)item["Price_N"]);
                }
                mc.Set("hl_p_" + productId, price, DateTime.Now.AddYears(1));
            }
            return price;
        }
    }
}
