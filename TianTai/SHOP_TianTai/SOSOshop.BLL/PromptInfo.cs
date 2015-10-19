using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SOSOshop.BLL
{
    public class PromptInfo
    {
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="powerStr"></param>
        public static void Popedom(string powerStr)
        {
            AdministrorManager.CheckAdmin();
            if (!PowerPass.isPass(powerStr))
            {
                ChangeHope.WebPage.Script.AlertAndGoBack("对不起，你没有权限浏览该页面!");
                HttpContext.Current.Response.End();
            }
        }
        public static void Popedom(string powerStr, string messge)
        {
            AdministrorManager.CheckAdmin();
            if (!PowerPass.isPass(powerStr))
            {
                ChangeHope.WebPage.Script.AlertAndGoBack(messge);
                HttpContext.Current.Response.End();
            }
        }

        public static string Message(string powerStr)
        {
            string messge = "";
            AdministrorManager.CheckAdmin();
            if (!PowerPass.isPass(powerStr))
            {
                messge = "ok";
            }
            return messge;
        }
    }
}
