using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOYY.Cached
{
    public class MemCacheDependency : ICacheDependency
    {
        public string Dependkey { get; set; }
        public EnumDependType DependType { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dependkey">缓存依赖项key</param>
        public MemCacheDependency(string dependkey)
        {
            Dependkey = dependkey;
            DependType = EnumDependType.CacheDepend;
        }
    }
}
