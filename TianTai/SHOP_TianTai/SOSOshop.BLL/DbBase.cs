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
using SOSOYY.Cached;
using System.Collections;
using System.Reflection;

namespace SOSOshop.BLL
{

    public class DbBase
    {

        public int MemcachedCacheTime { get; set; }
        public Database _db;

        public DbBase()
        {
            MemcachedCacheTime = 30;
            _db = DatabaseFactory.CreateDatabase();
            DbHelperSQL.connectionString = _db.ConnectionString;
            Library.Data.DbHelper.connString = _db.ConnectionString;
        }
        public DbBase(string name)
        {
            MemcachedCacheTime = 30;
            _db = DatabaseFactory.CreateDatabase(name);
            DbHelperSQL.connectionString = _db.ConnectionString;
            Library.Data.DbHelper.connString = _db.ConnectionString;
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
        public DataTable ExecuteTable(string sql, int time)
        {
            var dbcomm = db.GetSqlStringCommand(sql);
            dbcomm.CommandTimeout = time;
            return db.ExecuteDataSet(dbcomm).Tables[0];
        }
        public DataTable ExecuteTable(string sql, params object[] obj)
        {
            return db.ExecuteDataSet(CommandType.Text, string.Format(sql, obj)).Tables[0];
        }
        /// <summary>
        /// 从缓存里面取出DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteTableForCache(string sql)
        {
            string key = Library.Lang.Input.MD5(sql);
            DataTable dt = Get(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                Set(key, dt, DateTime.Now.AddDays(1));
            }
            return dt;
        }
        /// <summary>
        /// 从缓存里面取出DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteTableForCache(string sql, DateTime expireTime, string key = "")
        {
            key = key == "" ? Library.Lang.Input.MD5(sql) : key;
            DataTable dt = Get(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                Set(key, dt, expireTime);
            }
            return dt;
        }
        public DataTable ExecuteTableForDependCache(string sql, string dependKey)
        {
            string key = Library.Lang.Input.MD5(sql);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                SetDepend(key, dt, dependKey);
            }
            return dt;
        }
        /// <summary>
        /// 从缓存里面取出DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteTableForCache(string sql, string key)
        {
            DataTable dt = Get(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                Set(key, dt);
            }
            return dt;
        }

        /// <summary>
        /// 从缓存里面取出DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSetForCache(string sql, string key)
        {
            DataSet dt = Get(key) as DataSet;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql);
                Set(key, dt);
            }
            return dt;
        }
        /// <summary>
        /// 从缓存里面取出DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSetForCache(string sql, string key, DateTime expireTime)
        {
            DataSet dt = Get(key) as DataSet;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql);
                Set(key, dt, expireTime);
            }
            return dt;
        }
        /// </summary>
        /// 从缓存里面取出DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSetForCache(string sql, DateTime expireTime, string key = "")
        {
            key = key == "" ? Library.Lang.Input.MD5(sql) : key;
            DataSet dt = Get(key) as DataSet;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, sql);
                Set(key, dt, expireTime);
            }
            return dt;
        }
        /// <summary>
        /// 从缓存里面取出object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public object ExecuteScalarForCache(string sql, DateTime expireTime)
        {
            string key = Library.Lang.Input.MD5(sql);
            object obj = Get(key);
            if (obj == null)
            {
                obj = db.ExecuteScalar(CommandType.Text, sql);
                Set(key, obj, expireTime);
            }
            return obj;
        }
        /// <summary>
        /// 从缓存里面取出object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="key"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public object ExecuteScalarForCache(string sql)
        {
            string key = Library.Lang.Input.MD5(sql);
            object obj = Get(key);
            if (obj == null)
            {
                obj = db.ExecuteScalar(CommandType.Text, sql);
                Set(key, obj);
            }
            return obj;
        }

        public DataSet ExecuteDataSet(string sql)
        {
            return db.ExecuteDataSet(CommandType.Text, sql);
        }
        public IDataReader ExecuteReader(string sql)
        {
            return db.ExecuteReader(CommandType.Text, sql);
        }
        public int ExecuteNonQuery(string sql)
        {
            return db.ExecuteNonQuery(CommandType.Text, sql);
        }
        public int ExecuteNonQuery(string sql, int second)
        {
            var command = db.GetSqlStringCommand(sql);
            command.CommandTimeout = second;
            return db.ExecuteNonQuery(command);
        }
        public object ExecuteScalar(string sql)
        {
            return db.ExecuteScalar(CommandType.Text, sql);
        }
        public object ExecuteScalar(string sql, params object[] obj)
        {
            return db.ExecuteScalar(CommandType.Text, string.Format(sql, obj));
        }
        public void ChangeDB(string dbkey)
        {
            this._db = DatabaseFactory.CreateDatabase(dbkey);
            DbHelperSQL.connectionString = _db.ConnectionString;
        }
        public void ChangeShop()
        {
            this._db = DatabaseFactory.CreateDatabase("ConnectionString");
            DbHelperSQL.connectionString = _db.ConnectionString;
        }
        public void ChangeDataCenter()
        {
            this._db = DatabaseFactory.CreateDatabase("ConnectionStringDataCentre");
            DbHelperSQL.connectionString = _db.ConnectionString;
        }

        /// <summary>
        /// 取得字段中最大的ID
        /// </summary>
        /// <param name="FieldName">字段名称</param>
        /// <param name="TableName">表名称</param>
        /// <returns></returns>
        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = ExecuteScalar(strsql);
            if (!Library.Lang.DataValidator.isNumber(obj))
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        #region 分页操作
        public DataTable GetList(string tableName, string filter, int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount)
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
            DataSet ds = db.ExecuteDataSet(dbCommand);
            DataTable dt = ds.Tables.Count > 0 ? ds.Tables[0] : null;
            recordCount = 0; int.TryParse(Convert.ToString(db.GetParameterValue(dbCommand, "recct")), out recordCount);
            pageCount = recordCount / PageSize;
            if ((recordCount % PageSize) != 0)
            {
                pageCount++;
            }
            return dt;
        }
        public DataTable GetListByPage(string tableName, string field, int PageSize, int PageIndex, string orderby, string strWhere, out int recordCount, out int pageCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.AppendFormat(")AS Row, {0}  from {1} T ", field, tableName);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" WHERE 1=1 " + strWhere);
            }
            strSql.Append(" ) TT");
            recordCount = GetRecordCount(tableName, strWhere);
            pageCount = recordCount / PageSize;
            if (recordCount % PageSize != 0) pageCount++;
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", (PageSize * (PageIndex - 1)) + 1, PageSize * PageIndex);
            return ExecuteTable(strSql.ToString());
        }
        public DataTable GetListByPageForCache(string tableName, string field, int PageSize, int PageIndex, string orderby, string strWhere, out int recordCount, out int pageCount)
        {
            string key = Library.Lang.Input.MD5(string.Format("{0}_{1}_{2}_{3}_{4}_{5}", tableName, field, PageSize, PageIndex, orderby, strWhere), true);
            ArrayList ar = Get(key) as ArrayList;
            if (ar == null)
            {
                DataTable dt;
                dt = GetListByPage(tableName, field, PageSize, PageIndex, orderby, strWhere, out recordCount, out pageCount);
                ar = new ArrayList();
                ar.Add(dt);
                ar.Add(recordCount);
                ar.Add(pageCount);
                Set(key, ar);
            }
            else
            {
                recordCount = (int)ar[1];
                pageCount = (int)ar[2];
            }
            return (DataTable)ar[0];
        }
        public int GetRecordCount(string tableName, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) FROM  " + tableName + " T ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
        /// <summary>
        /// BulkToDB,批量插入数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        public void BulkToDB(DataTable dt, string tableName, string[] ColumnMapping)
        {
            SqlConnection sqlConn = new SqlConnection(db.ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = dt.Rows.Count;
            if (ColumnMapping != null)
            {
                foreach (string dc in ColumnMapping)
                {
                    bulkCopy.ColumnMappings.Add(dc, dc);
                }
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
            try
            {
                Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
                if (!mc.KeyExists(key)) return null;
                return mc.Get(key);
            }
            catch
            {
                return null;
            }
        }

        protected void Delete(string key)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Delete(key);
        }

        public string dependkey { get; set; }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        protected void FlushDependkey()
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.AddObject(dependkey, 1);
        }
        protected void DeleteDepend(string key)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.RemoveObject(key);
        }
        protected void SetDepend(string key, object output, string dependKey)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = MemcachedCacheTime;
            mc.AddObjectWithDepend(key, output, dependKey);
        }
        protected void SetDepend(string key, object output, string dependKey, int timeout)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = timeout;
            mc.AddObjectWithDepend(key, output, dependKey);
        }
        protected void SetDepend(string key, object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = MemcachedCacheTime;
            mc.AddObject(key, output);
        }
        protected object GetDepend(string key)
        {
            try
            {
                MemCachedStrategy mc = new MemCachedStrategy();
                return mc.RetrieveObject(key);
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region 附加方法
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void ClearCache()
        {
            HttpRuntime.Close();
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.FlushAll();
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

    public class EntityHandler<EntityObject>
              where EntityObject : new()
    {
        /// <summary>  
        /// 填充对象列表  
        /// </summary>  
        public static List<EntityObject> ReceiveEntity(DataTable dt)
        {
            List<EntityObject> entityList = new List<EntityObject>();
            foreach (DataRow dr in dt.Rows)
            {
                EntityObject baseEntity = new EntityObject();
                foreach (PropertyInfo propertyInfo in typeof(EntityObject).GetProperties())
                {
                    baseEntity.GetType().GetProperty(propertyInfo.Name).SetValue(baseEntity, dr[propertyInfo.Name], null);
                }
                entityList.Add(baseEntity);
            }
            return entityList;
        }
    }

}
