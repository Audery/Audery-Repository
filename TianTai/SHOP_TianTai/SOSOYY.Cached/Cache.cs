using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOSOYY.Cached;

namespace SOSOYY.Cached
{
    public class Cache
    {
        public int CacheTime { get; set; }
        public string DependKey { get; set; }

        public Cache()
        {
            CacheTime = 1000;
            DependKey = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 设置缓存1Hours
        /// </summary>
        public void SetCache1Hours(object output)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(DependKey, output, DateTime.Now.AddHours(1));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        public void SetCache(object output)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(DependKey, output, DateTime.Now.AddHours(CacheTime));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        public void SetCache(object output, DateTime endTime)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(DependKey, output, endTime);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        public void SetCache(string DependKey, object output, DateTime endTime)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Set(DependKey, output, endTime);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        public object GetCache()
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            if (!mc.KeyExists(DependKey)) return null;
            return mc.Get(DependKey);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        public object GetCache(string DependKey)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            if (!mc.KeyExists(DependKey)) return null;
            return mc.Get(DependKey);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        public void DeleteCache()
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Delete(DependKey);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        public void DeleteCache(string DependKey)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.Delete(DependKey);
        }

        protected string ObjId { get { return Guid.NewGuid().ToString("N"); } }

        public void FlushDependkey()
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.AddObject(ObjId, 1);
        }
        public void FlushDependkey(string ObjId)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.AddObject(ObjId, 1);
        }
        public void DeleteDepend()
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.RemoveObject(ObjId);
        }
        public void DeleteDepend(string ObjId)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.RemoveObject(ObjId);
        }
        public void SetDepend(object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = CacheTime;
            string[] temp = { DependKey };
            mc.AddObjectWithDepend(ObjId, output, temp);
        }
        public void SetDepend(string ObjId, object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = CacheTime;
            string[] temp = { DependKey };
            mc.AddObjectWithDepend(ObjId, output, temp);
        }
        public void SetDepend(object output, int timeout)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = timeout;
            mc.TimeOut = CacheTime;
            string[] temp = { DependKey };
            mc.AddObjectWithDepend(ObjId, output, temp);
        }
        public void SetDepend(string ObjId, object output, int timeout)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = timeout;
            mc.TimeOut = CacheTime;
            string[] temp = { DependKey };
            mc.AddObjectWithDepend(ObjId, output, temp);
        }
        public void SetObject(object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = CacheTime;
            mc.AddObject(ObjId, output);
        }
        public void SetObject(string ObjId, object output)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            mc.TimeOut = CacheTime;
            mc.AddObject(ObjId, output);
        }
        public object GetObject()
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            return mc.RetrieveObject(ObjId);
        }
        public object GetObject(string ObjId)
        {
            MemCachedStrategy mc = new MemCachedStrategy();
            return mc.RetrieveObject(ObjId);
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void ClearCache()
        {
            /*
            HttpRuntime.Close();
            IDictionaryEnumerator mycache = HttpContext.Current.Cache.GetEnumerator();
            while (mycache.MoveNext())
            {
                HttpContext.Current.Cache.Remove(mycache.Key.ToString());
            }*/
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.FlushAll();
        }
    }
}
