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
    /// <summary>
    /// 商城数据库
    /// </summary>
    public class Db : DbBase
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public const string DbName = "SHOP_TianTai";
        public Db()
        {
            ChangeDB("ConnectionString");
            
        }        
    }
}
