using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver.GridFS;

namespace SOSOshop.BLL
{
    public class MongoHelper<T> where T : class
    {
        public MongoCollection<T> _mongoCollection;
        private MongoServer _mongoServer;
        private MongoDatabase _mongoDb;
        public MongoGridFS _gridFS;
        private string _collectionName;
        public MongoHelper(string collectionName)
        {
            _mongoServer = MongoServer.Create(ConfigurationManager.AppSettings["MongoConnectionURL"]);
            _mongoDb = _mongoServer.GetDatabase(ConfigurationManager.AppSettings["MongoName"]);
            _mongoCollection = _mongoDb.GetCollection<T>(collectionName);
            _collectionName = collectionName;
            _gridFS = _mongoDb.GridFS;

        }
        public MongoHelper(string MongoUrl,string MongoName, string collectionName)
        {
            _mongoServer = MongoServer.Create(MongoUrl);
            _mongoDb = _mongoServer.GetDatabase(MongoName);
            _mongoCollection = _mongoDb.GetCollection<T>(collectionName);
            _collectionName = collectionName;
            _gridFS = _mongoDb.GridFS;

        }
        public void ChangeDB(string dbname)
        {
            _mongoDb = _mongoServer.GetDatabase(dbname);
            _mongoCollection = _mongoDb.GetCollection<T>(_collectionName);            
            _gridFS = _mongoDb.GridFS;
        }

        public void ChangeDB(string MongoConnectionURL, string dbname)
        {
            _mongoServer = MongoServer.Create(ConfigurationManager.AppSettings[MongoConnectionURL]);
            _mongoDb = _mongoServer.GetDatabase(dbname);
            _mongoCollection = _mongoDb.GetCollection<T>(_collectionName);
            _gridFS = _mongoDb.GridFS;
        }
        ~MongoHelper()
        {
            // _mongoServer.Disconnect();
        }
        public MongoHelper()
        {
            _mongoServer = MongoServer.Create(ConfigurationManager.AppSettings["MongoConnectionURL"]);
            _mongoDb = _mongoServer.GetDatabase(ConfigurationManager.AppSettings["MongoName"]);
            _collectionName = typeof(T).Name;
            _mongoCollection = _mongoDb.GetCollection<T>(_collectionName);
            _gridFS = _mongoDb.GridFS;

        }
        public int Execute(Action<MongoCollection<T>> action)
        {
            int statusCode = 0;
            try
            {
                //_mongoServer.Reconnect();
                action(_mongoCollection);
                statusCode = 1;
            }
            finally
            {
                //  _mongoServer.Disconnect();
            }
            return statusCode;
        }

        public List<T> GetListNoPaging(BsonDocument fileds, BsonDocument greps, BsonDocument sorts)
        {
            List<T> dataList = new List<T>();
            Execute(delegate(MongoCollection<T> mongoCollection)
            {
                MongoCursor<T> dataCursor = mongoCollection.Find(new QueryDocument(greps))
                    .SetFields(new FieldsDocument(fileds))
                    .SetSortOrder(new SortByDocument(sorts));
                dataList.AddRange(dataCursor.ToList());
            });
            return dataList;
        }
        public List<T> GetListPaging(BsonDocument fileds, BsonDocument greps, BsonDocument sorts, int limit, int skip, out long pageCount, out long rowCount)
        {
            long _rowcount = 0;
            long _pagecount = 0;
            List<T> dataList = new List<T>();
            Execute(delegate(MongoCollection<T> mongoCollection)
            {

                _rowcount = mongoCollection.Find(new QueryDocument(greps)).Count();
                MongoCursor<T> dataCursor = mongoCollection.Find(new QueryDocument(greps))
                    .SetFields(new FieldsDocument(fileds))
                    .SetSortOrder(new SortByDocument(sorts))
                    .SetLimit(limit).SetSkip(skip);
                _pagecount = _rowcount % limit == 0 ? _rowcount / limit : _rowcount / limit + 1;
                dataList.AddRange(dataCursor.ToList());
            });
            rowCount = _rowcount;
            pageCount = _pagecount;
            return dataList;
        }

        public int Insert(T instance)
        {
            int resultCode = 0;
            string _id_instance = GetObjectId(instance);
            Execute(delegate(MongoCollection<T> mongoCollection)
            {
                Func<T, string> _getId = new Func<T, string>(GetObjectId);
                QueryDocument query = new QueryDocument(new BsonElement("_id", BsonValue.Create(_getId(instance))));
                //o => _getId(o) == _id_instance
                T temp = mongoCollection.FindOne(query);
                if (temp == null)
                {
                    mongoCollection.Insert(instance);
                    resultCode = 1;
                }
            });
            return resultCode;
        }

        public T One(BsonDocument fileds, BsonDocument greps)
        {
            T resultInstance = null;
            Execute(delegate(MongoCollection<T> mongoCollection)
            {
                resultInstance = mongoCollection.Find(new QueryDocument(greps))
                      .SetFields(new FieldsDocument(fileds)).FirstOrDefault();
            });
            return resultInstance;
        }

        public int Update(T instance)
        {
            int resultCode = 0;
            Execute(delegate(MongoCollection<T> mongoCollection)
            {
                Func<T, string> _getId = new Func<T, string>(GetObjectId);
                QueryDocument query = new QueryDocument(new BsonElement("_id", BsonValue.Create(_getId(instance))));
                SafeModeResult result = mongoCollection.Update(query,
                    new UpdateDocument(BsonExtensionMethods.ToBsonDocument<T>(instance)));
                resultCode = 1;
            });
            return resultCode;
        }

        private string GetObjectId(T obj)
        {
            Type t = typeof(T);
            PropertyInfo propertyInfo = t.GetProperty("id");
            if (propertyInfo.GetValue(obj, null) == null)
            {
                propertyInfo.SetValue(obj, MongoDB.Bson.BsonObjectId.GenerateNewId().ToString(), null);
            }
            return propertyInfo.GetValue(obj, null).ToString();
        }
    }
}
