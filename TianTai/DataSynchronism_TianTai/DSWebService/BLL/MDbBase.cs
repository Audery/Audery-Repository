using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;

namespace DSWebService.BLL
{
    /// <summary>
    /// 对Mongo和MongoDatabase的包装类
    /// </summary>
    public class MDbBase : IDisposable
    {
        private Mongo _mongo;
        private IMongoDatabase _db;

        /// <summary>
        /// 默认构造函数。
        /// 为了本程序方便使用，直接使用二个固定的参数。
        /// 采用MongoDb的默认连接字符串，连接MyNorthwind数据库。
        /// </summary>
        public MDbBase()
            : this(Config.Config.MongoConnectionString, Config.Config.MongoName)
        {
            
        }

        /// <summary>
        /// 构造函数。根据指定连接字符串和数据库名
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbName">数据库名，可为空，但必须在任何操作数据库之前要调用UseDb()方法</param>
        public MDbBase(string connectionString, string dbName)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            _mongo = new Mongo(connectionString);

            // 立即连接 MongoDB
            _mongo.Connect();

            if (string.IsNullOrEmpty(dbName) == false)
                _db = _mongo.GetDatabase(dbName);
        }

        /// <summary>
        /// 切换到指定的数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IMongoDatabase UseDb(string dbName)
        {
            if (string.IsNullOrEmpty(dbName))
                throw new ArgumentNullException("dbName");

            _db = _mongo.GetDatabase(dbName);
            return _db;
        }

        /// <summary>
        /// 获取当前连接的数据库
        /// </summary>
        public IMongoDatabase CurrentDb
        {
            get
            {
                if (_db == null)
                    throw new Exception("当前连接没有指定任何数据库。请在构造函数中指定数据库名或者调用UseDb()方法切换数据库。");

                return _db;
            }
        }

        /// <summary>
        /// 获取当前连接数据库的指定集合【依据类型】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return this.CurrentDb.GetCollection<T>(); 
        }

        /// <summary>
        /// 获取当前连接数据库的指定集合【根据指定名称】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">集合名称</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name) where T : class
        {
            return this.CurrentDb.GetCollection<T>(name);
        }

        public void Dispose()
        {
            if (_mongo != null)
            {
                _mongo.Dispose();
                _mongo = null;
            }
        }
    }
}
