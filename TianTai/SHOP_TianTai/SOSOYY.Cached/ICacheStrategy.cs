﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOYY.Cached
{
    /// <summary>
    /// 公共缓存策略接口
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        void AddObject(string objId, object o);
        /// <summary>
        /// 添加指定ID的对象(关联指定文件组)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="files"></param>
        void AddObjectWithFileChange(string objId, object o, string[] files);
        /// <summary>
        /// 添加指定ID的对象(关联指定键值组)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dependKey"></param>
        void AddObjectWithDepend(string objId, object o, string[] dependKey);
        /// <summary>
        /// 添加指定ID的对象(关联ICacheDependency)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dep"></param>
        void AddObjectWithDepend(string objId, object o, ICacheDependency dep);
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId"></param>
        void RemoveObject(string objId);
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">key1</param>
        /// <returns></returns>
        object RetrieveObject(string objId);
        /// <summary>
        /// 返回指定ID的cache     
        /// </summary>
        /// <param name="objId">DATA_key1，CTIME_key1,DEPEND_key1,DEPCTIME_key1</param>
        /// <returns></returns>
        object RetrieveCache(string objId);
        /// <summary>
        /// 到期时间,单位：分钟
        /// </summary>
        int TimeOut { set; get; }
    }
}
