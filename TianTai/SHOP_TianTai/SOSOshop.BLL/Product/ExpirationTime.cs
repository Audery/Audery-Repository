using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOSOshop.BLL.Product
{
    public class ExpirationTime : Db
    {
        #region 单例
        private volatile static ExpirationTime _instance = null;
        private static readonly object lockHelper = new object();
        private ExpirationTime() { }
        public static ExpirationTime CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ExpirationTime();
                }
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// 获取近效期产品数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetProduct_ExpirationTimeList()
        {
            string sql = @"SELECT  p1.id ,
        p.product_id ,
        p1.Extended1 ,
        p1.Stock ,
        p1.price ,
        p1.ExpirationTime ,
        p1.erp_id ,
        p1.Goods_Unit ,
        p.Product_Name ,
        p.Image
FROM    product_expirationTime p1
        JOIN dbo.product_online_v p ON p.spid = p1.Erp_ID";

            DataTable dt = base.ExecuteTable(sql);

            //给Product_ID赋值
            foreach (DataRow item in dt.Rows)
            {
                int Product_ID = Convert.ToInt32(item["Product_ID"]);

                sql = string.Format("UPDATE Product_ExpirationTime SET Product_ID={0}",Product_ID);
                base.ExecuteNonQuery(sql);
            }
            return dt;
        }

    }


}
