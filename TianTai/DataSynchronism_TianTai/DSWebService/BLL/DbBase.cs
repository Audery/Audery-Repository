using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Library.Data;
using System.Data.Common;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DSWebService.Config;
using SOSOYY.Cached;
using System.Collections;
using System.Reflection;

namespace DSWebService.BLL
{
    [Serializable]
    public class DbBase
    {
        [MongoDB.Attributes.MongoIgnore]
        public int MemcachedCacheTime { get; set; }
        public Database _db;
        public DbBase()
        {
            MemcachedCacheTime = 30;
            _db = DatabaseFactory.CreateDatabase();
            DbHelperSQL.connectionString = _db.ConnectionString;
        }

        /// <summary>
        /// 切换到商城数据库
        /// </summary>
        public void ChangeDBShop()
        {
            _db = DatabaseFactory.CreateDatabase("ConnectionString");
            DbHelperSQL.connectionString = db.ConnectionString;
        }
        /// <summary>
        /// 切换到数据交换中心数据库
        /// </summary>
        public void ChangeDBData_Centre()
        {
            _db = DatabaseFactory.CreateDatabase("ConnectionStringDataCentre");
            DbHelperSQL.connectionString = db.ConnectionString;
        }

        protected Database db
        {
            get
            {
                return _db;
            }
        }
        public DataTable ExecuteTable(string sql)
        {
            return db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
        }
        public DataTable ExecuteTable(string sql, int timeout)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            dbCommand.CommandTimeout = timeout;
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        public DataSet ExecuteDataSet(string sql)
        {
            return db.ExecuteDataSet(CommandType.Text, sql);
        }
        public DataSet ExecuteDataSet(string sql, int timeout)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            dbCommand.CommandTimeout = timeout;
            return db.ExecuteDataSet(dbCommand);
        }
        public IDataReader ExecuteReader(string sql)
        {
            return db.ExecuteReader(CommandType.Text, sql);
        }
        public int ExecuteNonQuery(string sql)
        {
            return db.ExecuteNonQuery(CommandType.Text, sql);
        }
        public int ExecuteNonQuery(string sql, int timeout)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            dbCommand.CommandTimeout = timeout;
            return db.ExecuteNonQuery(dbCommand);
        }
        public object ExecuteScalar(string sql)
        {
            return db.ExecuteScalar(CommandType.Text, sql);
        }
        public DbBase ChangeDB(string dbkey)
        {
            this._db = DatabaseFactory.CreateDatabase(dbkey);
            return this;
        }

        /// <summary>
        /// BulkToDB,批量插入数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        public void BulkToDB(DataTable dt, string tableName)
        {
            SqlConnection sqlConn = new SqlConnection(db.ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = dt.Rows.Count;
            foreach (DataColumn dc in dt.Columns)
            {
                bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
            }
            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(tableName + ex.ToString());
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }
        #region 分页操作
        protected DataTable GetList(string tableName, string filter, int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount)
        {
            orderField = Library.Lang.Input.Filter(orderField);
            whereField = Library.Lang.Input.Filter(whereField);
            whereString = Library.Lang.Input.Filter(whereString);
            string TableList = "*";
            string orderfileds = orderField;
            if (order)
            {
                orderfileds += " desc";
            }
            else
            {
                orderfileds += " asc";
            }

            if (whereString != null)
            {
                if (like)
                {
                    filter += string.Format(" and {0} like('%{1}%')", whereField, whereString);
                }
                else
                {
                    filter += string.Format(" and {0}='{1}'", whereField, whereString);
                }
            }
            DbCommand dbCommand = db.GetStoredProcCommand("UtilPAGE");
            db.AddInParameter(dbCommand, "datasrc", DbType.String, tableName);
            db.AddInParameter(dbCommand, "orderBy", DbType.String, orderfileds);
            db.AddInParameter(dbCommand, "fieldlist", DbType.String, TableList);
            db.AddInParameter(dbCommand, "filter", DbType.String, filter);
            db.AddInParameter(dbCommand, "pageNum", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "pageSize", DbType.Int32, PageSize);
            db.AddOutParameter(dbCommand, "recct", DbType.Int32, 4);
            DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
            recordCount = (int)db.GetParameterValue(dbCommand, "recct");
            if (0 == recordCount)
            {
                pageCount = 1;  //Yj2015.0303 Add
            }
            else
            {
                pageCount = recordCount / PageSize;
                if ((recordCount % PageSize) != 0)
                {
                    pageCount++;
                }
            }
            return dt;
        }
        #endregion
        /// 字符串安全检查
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        protected string Filtrate(string sqlString)
        {
            if (Library.Lang.Input.SqlFilter(sqlString))
            {
                throw new Exception("SQL参数中含有危险字符串!");
            }
            return sqlString;
        }

        #region 缓存操作

        /// <summary>
        /// 设置缓存
        /// </summary>
        protected void Set(string key, object output)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(key, output, DateTime.Now.AddHours(1));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        protected void Set(string key, object output, DateTime endTime)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(key, output, endTime);

        }
        protected object Get(string key)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            if (!mc.KeyExists(key)) return null;
            return mc.Get(key);

        }

        protected void Delete(string key)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Delete(key);
        }
        [MongoDB.Attributes.MongoIgnore]
        public string dependkey { get; set; }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        public void FlushDependkey()
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.AddObject(dependkey, 1);
        }
        public void DeleteDepend(string key)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.RemoveObject(key);
        }
        public void SetDepend(string key, object output, string dependKey)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = MemcachedCacheTime;
            string[] temp = { dependKey };
            mc.AddObjectWithDepend(key, output, temp);
        }
        public void SetDepend(string key, object output, string dependKey, int timeout)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = timeout;
            mc.TimeOut = MemcachedCacheTime;
            string[] temp = { dependKey };
            mc.AddObjectWithDepend(key, output, temp);
        }
        public void SetDepend(string key, object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = MemcachedCacheTime;
            mc.AddObject(key, output);
        }
        public object GetDepend(string key)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            return mc.RetrieveObject(key);
        }
        #endregion

        #region 附加方法
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void ClearCache()
        {
            HttpRuntime.Close();
            /*
            IDictionaryEnumerator mycache = HttpContext.Current.Cache.GetEnumerator();
            while (mycache.MoveNext())
            {
                HttpContext.Current.Cache.Remove(mycache.Key.ToString());
            }*/
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.FlushAll();
        }
        #endregion
        #region 扩展方法
        /// <summary>
        /// 将DataTable转成List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> AsObjectList<T>(DataTable dt) where T : class, new()
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] piList = type.GetProperties();
            T item;

            List<string> listHear = new List<string>();
            foreach (DataColumn dtCl in dt.Columns)
            {
                listHear.Add(dtCl.ColumnName);
            }

            foreach (DataRow dr in dt.Rows)
            {
                item = new T();
                foreach (PropertyInfo pi in piList)
                {
                    // 数据表中没有对应的列则直接返回
                    if (!listHear.Contains(pi.Name))
                    {
                        continue;
                    }

                    if (dr[pi.Name] == DBNull.Value)
                    {
                        if (pi.PropertyType == typeof(string))
                        {
                            pi.SetValue(item, "", null);
                        }
                    }
                    else
                    {
                        string fullName = pi.PropertyType.FullName;
                        if (fullName.IndexOf("Nullable") > -1)
                        {
                            if (fullName.IndexOf("Int32") > -1)
                            {
                                string a = dr[pi.Name].ToString();
                                int? temp = Convert.ToInt32(dr[pi.Name].ToString());
                                pi.SetValue(item, temp, null);
                            }
                            else if (fullName.IndexOf("Double") > -1)
                            {
                                double? temp = Convert.ToDouble(dr[pi.Name].ToString());
                                pi.SetValue(item, temp, null);
                            }
                            else if (fullName.IndexOf("DateTime") > -1)
                            {
                                DateTime? temp = Convert.ToDateTime(dr[pi.Name].ToString());
                                pi.SetValue(item, temp, null);
                            }
                            else if (fullName.IndexOf("Decimal") > -1)
                            {
                                decimal? temp = Convert.ToDecimal(dr[pi.Name].ToString());
                                pi.SetValue(item, temp, null);
                            }
                            else if (fullName.IndexOf("Boolean") > -1)
                            {
                                bool? temp = Convert.ToBoolean(dr[pi.Name].ToString());
                                pi.SetValue(item, temp, null);
                            }
                        }
                        else
                        {
                            pi.SetValue(item, dr[pi.Name], null);

                        }

                    }

                }
                list.Add(item);
            }
            return list;
        }
        #endregion
    }

}
