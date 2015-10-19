using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace SOSOshop.BLL.Category
{
    /// <summary>
    /// 全部分类(包括数量统计)
    /// </summary>
    [Serializable]
    public class Menu
    {

        #region Model
        /// <summary>
        /// 编号
        /// </summary>
        [BsonId]
        public string id { get; set; }
        /// <summary>
        /// 药理ID
        /// </summary>
        public int Pharm_ID { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public MenuEnum me { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 父级分类
        /// </summary>
        public string parentId { get; set; }

        #endregion

        #region method
        /// <summary>
        /// 添加一个分类
        /// </summary>
        public void Insert()
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            if (db._mongoCollection.AsQueryable().Where(x => x.me == me && x.Title == Title).Count() > 0)
            {
                db._mongoCollection.Update(Query.And(Query.EQ("Title", Title), Query.EQ("me", me)), Update.Set("Count", Count));
            }
            else
            {
                db.Insert(this);
            }

        }
        /// <summary>
        /// 添加一个分类[包括Pharm_ID]
        /// </summary>
        public void InsertOne()
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            if (db._mongoCollection.AsQueryable().Where(x => x.me == me && x.Title == Title && x.Pharm_ID == Pharm_ID).Count() > 0)
            {
                db._mongoCollection.Update(Query.And(Query.EQ("Title", Title), Query.EQ("me", me), Query.EQ("Pharm_ID", Pharm_ID)), Update.Set("Count", Count));
            }
            else
            {
                db.Insert(this);
            }

        }
        /// <summary>
        /// 取得分类
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public List<Menu> GetList(MenuEnum me)
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            string key = "BLL_Menu_GetList_" + me + parentId;
            List<Menu> obj = mc.Get(key) as List<Menu>;
            if (obj == null)
            {
                obj = db._mongoCollection.AsQueryable().Where(x => x.me == me).ToList();
                mc.Set(key, obj, DateTime.Now.AddHours(3));
            }
            return obj;
        }
        /// <summary>
        /// 取得中药饮片分类
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public List<Menu> GetList_Zyyp(string parentId)
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            string key = "BLL_Menu_GetList_Zyyp_" + parentId;
            List<Menu> obj = mc.Get(key) as List<Menu>;
            if (obj == null)
            {
                obj = db._mongoCollection.AsQueryable().Where(x => x.me == MenuEnum.中药饮片药理分类 && x.parentId == parentId).ToList();
                mc.Set(key, obj, DateTime.Now.AddHours(3));
            }
            return obj;
        }
        /// <summary>
        /// 将分类数据同步至b3的分类
        /// </summary>
        public void ToB3Menu()
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            var list = db._mongoCollection.FindAll();
            db.ChangeDB("MongoConnectionURL2", "SOSOshop_YSPT");
            db._mongoCollection.RemoveAll(SafeMode.True);
            db._mongoCollection.InsertBatch(list);
        }
        /// <summary>
        /// 取得分类
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public List<Menu> GetList(MenuEnum me, int top)
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            string key = "BLL_Menu_GetList_" + me + "_" + top;
            List<Menu> obj = mc.Get(key) as List<Menu>;
            if (obj == null)
            {
                obj = db._mongoCollection.AsQueryable().Where(x => x.me == me).OrderByDescending(x => x.Count).Take(top).ToList();
                mc.Set(key, obj, DateTime.Now.AddHours(3));
            }
            return obj;
        }
        /// <summary>
        /// 取得当前分类的总数量
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public int GetCount(MenuEnum me)
        {
            MongoHelper<Menu> db = new MongoHelper<Menu>();
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            string key = "BLL_Menu_GetCount_" + me;
            object obj = mc.Get(key);
            if (obj == null)
            {
                var li = db._mongoCollection.AsQueryable().Where(x => x.me == me).ToArray();
                obj = li.Sum(x => x.Count);
                mc.Set(key, obj, DateTime.Now.AddHours(3));
            }
            return (int)obj;

        }
        #endregion
        /// <summary>
        /// 初始化菜单分类
        /// </summary>
        public static void InitData()
        {
            SOSOshop.BLL.Db db = new Db();
            #region 进口药品
            string sql = @"SELECT TOP 100 *,(SELECT COUNT(0) FROM dbo.product_online_v WHERE DrugsBase_ID IN (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id=Tag_PharmAttribute.id))c FROM dbo.Tag_PharmAttribute WHERE tag_id=88  ORDER BY seq ";
            DataTable dt = db.ExecuteTable(sql);
            foreach (var item in dt.Select("ParentId=0"))
            {
                Menu bll = new Menu();
                bll.me = MenuEnum.进口药品;
                bll.parentId = "0";
                bll.Title = item["name"] as string;
                bll.Count = (int)item["c"];
                bll.Pharm_ID = (int)item["id"];
                bll.Insert();

                foreach (var item2 in dt.Select("ParentId=" + item["id"]))
                {
                    bll = new Menu();
                    bll.me = MenuEnum.进口药品;
                    bll.parentId = item["id"].ToString();
                    bll.Title = item2["name"] as string;
                    bll.Count = (int)item2["c"];
                    bll.Pharm_ID = (int)item2["id"];
                    bll.Insert();
                }
            }

            #endregion

            #region 中药饮片
            sql = @"SELECT *,
                    (
                    SELECT COUNT(0) FROM dbo.product_online_v WHERE DrugsBase_ID IN 
                    (
	                    SELECT DrugsBase_ID FROM dbo.DrugsBase_ZYC WHERE ProductionClassId=DrugsBase_ZYC_ProductionClass.ProductionClassId
                    ))c
                     FROM dbo.DrugsBase_ZYC_ProductionClass";
            foreach (DataRow item in db.ExecuteTable(sql).Rows)
            {
                Menu bll = new Menu();
                bll.me = MenuEnum.中药饮片;
                bll.parentId = "0";
                bll.Title = (string)item["ProductionClassName"];
                bll.Count = (int)item["c"];
                bll.Pharm_ID = (int)item["ProductionClassId"];
                bll.Insert();
            }
            #endregion

            #region 中药饮片药理分类
            sql = @"select y.Pharm_ID,y.Pharm_Parent_ID,y.Pharm_Name,
                    (SELECT COUNT(DISTINCT Product_ID) FROM product_online_v v LEFT JOIN DrugsBase_PharmMediNameLink a ON v.DrugsBase_ID=a.DrugsBase_ID 
                    LEFT JOIN DrugsBase_Pharm b ON a.Pharm_ID=b.Pharm_ID WHERE a.DrugsBase_ID is not null and b.Pharm_ID_Path LIKE y.Pharm_ID_Path+'%')c
                    FROM DrugsBase_Pharm y WHERE y.Pharm_ID_Path LIKE '\2973%'
                    ORDER BY y.Pharm_ID_Path";
            foreach (DataRow item in db.ExecuteTable(sql).Rows)
            {
                Menu bll = new Menu();
                bll.me = MenuEnum.中药饮片药理分类;
                bll.parentId = item["Pharm_Parent_ID"].ToString();
                bll.Title = (string)item["Pharm_Name"];
                bll.Count = (int)item["c"];
                bll.Pharm_ID = (int)item["Pharm_ID"];
                bll.InsertOne();
            }
            #endregion

            #region 医疗器械
            sql = @"SELECT * FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\2968%') AND Pharm_ID NOT IN (2969,2970,2971,2972,2968)";
            DataTable dtqx = db.ExecuteTable(sql);
            foreach (var item in dtqx.AsEnumerable().Where(x => x.Field<int>("Pharm_Level") == 2))
            {
                int count = (int)db.ExecuteScalar(string.Format(@"SELECT COUNT(*) FROM dbo.product_online_v WHERE DrugsBase_ID IN 
                                                                (
	                                                                SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN 
	                                                                (
		                                                                SELECT Pharm_id FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\{0}%')	
	                                                                )
                                                                )", item["Pharm_ID"]));
                Menu bll = new Menu();
                bll.me = MenuEnum.医疗器械;
                bll.parentId = "0";
                bll.Title = (string)item["Pharm_Name"];
                bll.Count = count;
                bll.Pharm_ID = (int)item["Pharm_ID"];
                bll.Insert();

                foreach (var item2 in dtqx.AsEnumerable().Where(x => x.Field<int>("Pharm_Level") == 3 && x.Field<string>("Pharm_ID_Path").Contains("\\" + item["Pharm_ID"])))
                {
                    count = (int)db.ExecuteScalar(string.Format(@"SELECT COUNT(*) FROM dbo.product_online_v WHERE DrugsBase_ID IN 
                                                                (
	                                                                SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN 
	                                                                (
		                                                                SELECT Pharm_id FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\{0}%')	
	                                                                )
                                                                )", item2["Pharm_ID"]));
                    bll = new Menu();
                    bll.me = MenuEnum.医疗器械;
                    bll.parentId = item["Pharm_ID"].ToString();
                    bll.Title = (string)item2["Pharm_Name"];
                    bll.Count = count;
                    bll.Pharm_ID = (int)item2["Pharm_ID"];
                    bll.Insert();
                }
            }

            #endregion

            #region 保健品
            sql = @"SELECT * FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\3051\%')";
            dtqx = db.ExecuteTable(sql);
            foreach (var item in dtqx.AsEnumerable().Where(x => x.Field<int>("Pharm_Level") == 2))
            {
                int count = (int)db.ExecuteScalar(string.Format(@"SELECT COUNT(*) FROM dbo.product_online_v WHERE DrugsBase_ID IN 
                                                                (
	                                                                SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN 
	                                                                (
		                                                                SELECT Pharm_id FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\{0}%')	
	                                                                )
                                                                )", item["Pharm_ID"]));
                Menu bll = new Menu();
                bll.me = MenuEnum.保健品;
                bll.parentId = "0";
                bll.Title = (string)item["Pharm_Name"];
                bll.Count = count;
                bll.Pharm_ID = (int)item["Pharm_ID"];
                bll.Insert();
            }

            #endregion

            #region 计生用品
            sql = @"SELECT * FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\3075\%')";
            dtqx = db.ExecuteTable(sql);
            foreach (var item in dtqx.AsEnumerable().Where(x => x.Field<int>("Pharm_Level") == 2))
            {
                int count = (int)db.ExecuteScalar(string.Format(@"SELECT COUNT(*) FROM dbo.product_online_v WHERE DrugsBase_ID IN 
                                                                (
	                                                                SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN 
	                                                                (
		                                                                SELECT Pharm_id FROM DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\{0}%')	
	                                                                )
                                                                )", item["Pharm_ID"]));
                Menu bll = new Menu();
                bll.me = MenuEnum.计生用品;
                bll.parentId = "0";
                bll.Title = (string)item["Pharm_Name"];
                bll.Count = count;
                bll.Pharm_ID = (int)item["Pharm_ID"];
                bll.Insert();
            }

            #endregion           
        }
    }
    public enum MenuEnum
    {
        进口药品 = 1, 中药饮片, 医疗器械, 保健品, 计生用品, 中药饮片药理分类
    }
}
