using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.MSG
{
    /// <summary>
    /// 短信接口
    /// </summary>
    public interface IMSG
    {
        bool Send(string mobile, string content);
    }
}
