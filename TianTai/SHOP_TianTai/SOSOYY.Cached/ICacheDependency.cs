using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOYY.Cached
{
    public interface ICacheDependency
    {
        string Dependkey { get; set; }
        EnumDependType DependType { get; set; }
    }
    /// <summary>
    /// 缓存依赖类型
    /// </summary>
    public enum EnumDependType
    {
        ///内存依赖
        CacheDepend = 0,
        ///文件依赖
        // FileDepend = 1,
    }
}
