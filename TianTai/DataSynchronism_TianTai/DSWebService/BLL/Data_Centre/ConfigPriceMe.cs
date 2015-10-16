using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 自己公司价格加点配置
    /// </summary>
    public class ConfigPriceMe
    {
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        /// <summary>
        /// 价格名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 价格加点默认为一
        /// </summary>
        [MongoDB.Attributes.MongoDefault(1)]
        public float Price_Plus { get; set; }
        public ConfigPriceMe(int c) { }
        public ConfigPriceMe()
        {
            BLL.DbBase db = new DbBase();
            System.Data.DataTable dt = db.ExecuteDataSet("SELECT DISTINCT category FROM dbo.Price").Tables[0];
            var model = new ConfigPriceMe(0);
            foreach (System.Data.DataRow item in dt.Rows)
            {
                if (!Exist(item["category"].ToString()))
                {
                    model.id = MongoDB.Oid.NewOid().ToString();
                    model.name = item["category"].ToString();
                    model.Price_Plus = 1;
                    Insert(model);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        public void Insert(ConfigPriceMe model)
        {
            using (MDbBase db = new MDbBase())
            {
                db.GetCollection<ConfigPriceMe>().Insert(model, false);
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            using (MDbBase db = new MDbBase())
            {
                try
                {
                    return db.GetCollection<ConfigPriceMe>().Linq().Where(x => x.name == name).Count() > 0;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
        /// <summary>
        /// 取得所有加点数据
        /// </summary>
        /// <returns></returns>
        public List<ConfigPriceMe> GetList()
        {
            using (MDbBase db = new MDbBase())
            {

                return db.GetCollection<ConfigPriceMe>().FindAll().Documents.ToList();
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="name">价格名称</param>
        /// <param name="Price_Plus">加点</param>
        public void Update(ConfigPriceMe model)
        {
            using (MDbBase db = new MDbBase())
            {
                db.GetCollection<ConfigPriceMe>().Update(model, x => x.name == name);
            }
        }
        /// <summary>
        /// 取得加点设置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public decimal GetPrice_Plus(string name)
        {
            return decimal.Round((decimal)GetList().Where(x => x.name == name).Select(x => x.Price_Plus).First(), 2, MidpointRounding.AwayFromZero);
        }


        public void UpdateToSql(string category, decimal price_plus)
        {
            BLL.DbBase db = new DbBase();
            string SqlStr = string.Format("SELECT ID FROM Price WHERE category='{0}' AND Price_Plus=0", category);
            DataTable dt = db.ExecuteTable(SqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                //更新MongoDatabase
                var UpdateObj = GetList().Find(o => o.name == category);
                UpdateObj.Price_Plus = (float)price_plus;
                using (MDbBase mdb = new MDbBase())
                {
                    mdb.GetCollection<ConfigPriceMe>().Update(UpdateObj, o => o.id == UpdateObj.id);
                }
                //阻塞500毫秒（预防）
                Thread.Sleep(500);

                Price price = null;
                foreach (DataRow item in dt.Rows)
                {
                    string ID = item["ID"].ToString();
                    price = new Price();
                    //价格更新
                    price.Update(ID, category);
                }
            }
           

        }
    }
}