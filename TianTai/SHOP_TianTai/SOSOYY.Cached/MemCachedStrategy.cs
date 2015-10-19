using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOYY.Cached
{
    /// <summary>
    /// MemCache缓存策略类 
    /// </summary>
    public class MemCachedStrategy : ICacheStrategy
    {
        Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
        /// <summary>
        /// 到期时间(以分钟为单位)
        /// </summary>
        public int TimeOut { set; get; }
        #region  操作指定key的Cache
        private void AddCache(string objId, object o)
        {
            RemoveCache(objId);
            if (TimeOut > 0)
            {
                mc.Set(objId, o, System.DateTime.Now.AddMinutes(TimeOut));                
            }
            else
            {
                mc.Set(objId, o, DateTime.Now.AddHours(24));                
            }
            
        }

        private void RemoveCache(string objId)
        {
            if (mc.KeyExists(objId))
                mc.Delete(objId);
        }

        private bool KeyExists(string objId)
        {
            return mc.KeyExists(objId);
        }

        public object RetrieveCache(string objId)
        {
            return mc.Get(objId);
        }

        #endregion

        /// <summary>
        ///  添加指定ID的cache 没有依赖项
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        public void AddObject(string objId, object o)
        {
            string data_key = CacheKeys.DATA + objId;
            string ctime_key = CacheKeys.CTIME + objId;
            string ctime_value = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //DATA
            AddCache(data_key, o);
            //CTIME
            AddCache(ctime_key, ctime_value);            
        }
        /// <summary>
        /// 添加指定ID的cache 有依赖项 
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dependkey">依赖项key，目前只支持设置一个依赖key</param>
        public void AddObjectWithDepend(string objId, object o, string dependkey)
        {
            string depend_key = CacheKeys.DEPEND + objId;
            string depend_value = dependkey;
            string depctime_key = CacheKeys.DEPCTIME + objId;
            if (!mc.KeyExists(CacheKeys.DATA + dependkey))
            {
                AddObject(dependkey, DateTime.Now.Ticks.ToString());
            }
            object depctime_value = RetrieveCache(CacheKeys.CTIME + dependkey);
            AddObject(objId, o);
            //Depend key
            AddCache(depend_key, depend_value);
            //DEPTIME
            AddCache(depctime_key, depctime_value);

        }
        /// <summary>
        /// 添加指定ID的cache 有依赖项
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dep">ICacheDependency</param>
        public void AddObjectWithDepend(string objId, object o, ICacheDependency dep)
        {
            throw new Exception("未能实现此方法！");
        }
        public void AddObjectWithDepend(string objId, object o, string[] dependkey)
        {
            throw new Exception("未能实现此方法！");
        }
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        public void RemoveObject(string objId)
        {
            string data_key = CacheKeys.DATA + objId;
            string ctime_key = CacheKeys.CTIME + objId;
            string depend_key = CacheKeys.DEPEND + objId;
            string depctime_key = CacheKeys.DEPCTIME + objId;

            RemoveCache(data_key);
            RemoveCache(ctime_key);
            RemoveCache(depend_key);
            RemoveCache(depctime_key);
        }
        /// <summary>
        /// 返回指定ID的对象，并根据缓存依赖做失效操作 
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public object RetrieveObject(string objId)
        {
            string data_key = CacheKeys.DATA + objId;
            string ctime_key = CacheKeys.CTIME + objId;
            string depend_key = CacheKeys.DEPEND + objId;
            string depctime_key = CacheKeys.DEPCTIME + objId;

            object obj = null;
            //判断objId是否依赖于其他key
            if (!KeyExists(depend_key) && !KeyExists(depctime_key))
            {
                obj = RetrieveCache(data_key);
            }
            else
            {
                object depkey = RetrieveCache(depend_key);//depend key 
                string oldtime = RetrieveCache(depctime_key) as string;
                string newtime = RetrieveCache(CacheKeys.CTIME + depkey.ToString()) as string;
                //判断依赖项的key是否过期
                if (oldtime == newtime)
                {
                    if (oldtime!= null && newtime != null)
                    {
                        obj = RetrieveCache(data_key);
                    }
                }
                else
                {
                    RemoveObject(objId);
                }
            }
            return obj;

        }

        public void AddObjectWithFileChange(string objId, object o, string[] files)
        {

        }

    }
}
