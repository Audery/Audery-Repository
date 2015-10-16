using System;
using System.Linq;
using System.Collections.Generic;
namespace SOSOshop.Model.Logs
{

    [Serializable]
    public class Log
    {

        public Log()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        /// <summary>
        /// 操作IP地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 日志类型0系统日志,1 用户操作日志,2 异常日志,3 HttpCode404日志
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 日志描述
        /// </summary>
        public string describe { get; set; }
        /// <summary>
        /// 操作人员编号
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 操作人员帐号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 错误详细
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime created { get; set; }
        #endregion Model
        /// <summary>
        /// 取得日志类型日志类型0系统日志,1 用户操作日志,2 异常日志,3 HttpCode404日志
        /// </summary>
        /// <param name="type"></param>
        public static string GetType(int type)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(0, "系统日志");
            di.Add(1, "操作日志");
            di.Add(2, "异常日志");
            di.Add(3, "404日志");
            return di[type];
        }
    }
}

