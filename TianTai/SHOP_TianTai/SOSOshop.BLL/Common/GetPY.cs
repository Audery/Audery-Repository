using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL.Common
{
    /// <summary>
    /// 获取汉字拼音的简写
    /// </summary>
    public class GetPY
    {
        public static string Get(string hanzichuan)
        {
            DbBase db = new DbBase();
            db.ChangeShop();
            return (string)db.ExecuteScalar(string.Format("select dbo.fun_getPY('{0}')", hanzichuan));                
        }
    }
}
