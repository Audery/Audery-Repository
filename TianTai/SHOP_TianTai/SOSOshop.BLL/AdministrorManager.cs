using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOSOshop.Model;

namespace SOSOshop.BLL
{
    public class AdministrorManager
    {
        /// <summary>
        /// 按一天12个钟头来计算，并存储在Cookies中
        /// </summary>
        public static double CookiesTime = 60 * 12;

        public static bool CheckAdmin()
        {
            AdminInfo admin = (AdminInfo)Get();
            if (admin == null)
            {
                SysParameter sp = new SysParameter();
                ChangeHope.WebPage.Script.Alert("提示：您未登陆或登陆时间已过期，转向登录页面");
                ChangeHope.WebPage.Script.ParentPageRedirect(sp.DummyPaht + "admin/index.aspx");
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 获取存储在Session或Cookie中Admin的信息
        /// </summary>
        /// <returns></returns>
        public static AdminInfo Get()
        {
            return System.Web.HttpContext.Current.Session["Admin"] as AdminInfo;
        }

        public static bool GetJGManager()
        {
            AdminInfo adminInfo = Get();
            if (adminInfo != null)
            {
                return adminInfo.AdminName.EndsWith("jianguan") || adminInfo.AdminName.EndsWith("jg");
            }
            return false;
        }
        /// <summary>
        /// 清除存储在Session或Cookie中Admin的信息
        /// </summary>
        /// <returns></returns>
        public static void DelAdminInfo()
        {
            System.Web.HttpContext.Current.Session["Admin"] = null;
        }
        /// <summary>
        /// 把Admin信息存储在Session或Cookies中
        /// </summary>
        /// <param name="adminInfo"></param>
        public static void Set(Model.AdminInfo adminInfo)
        {
            System.Web.HttpContext.Current.Session["Admin"] = adminInfo;
        }
    }
}
