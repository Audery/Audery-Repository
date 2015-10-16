using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisingManagement.BLL
{
    class Log
    {
        public static void AddLog(string msg)
        {
            try
            {
                //System.IO.File.WriteAllText(BLL.Core.RootPath + "Log.log", msg);
                //soso.updater.updater bll = new soso.updater.updater();
                //bll.AddLog(msg, 0);
            }
            catch (Exception e)
            {// System.IO.File.AppendAllText(BLL.Core.RootPath + "error.txt", e.ToString()); 
            }
        }
        /// <summary>
        /// 日志类型0错误1消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type">0错误，1消息,2同步,500服务器端错误,404更新服务消息</param>
        public static void AddLog(string msg, int type)
        {
            try
            {
                //System.IO.File.WriteAllText(BLL.Core.RootPath + "Log.log", msg);
               // soso.updater.updater bll = new soso.updater.updater();
                //bll.AddLog(msg, type);
            }
            catch (Exception e)
            {// System.IO.File.AppendAllText(BLL.Core.RootPath + "error.txt", e.ToString()); 
            }
        }
    }
}
