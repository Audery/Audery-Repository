using System;
using System.Collections.Generic;
using System.Text;
using Library.Lang;
namespace SOSOshop.WEB.UI
{
    public class WebHint
    {

        /// <summary>
        /// 页面出错提示
        /// </summary>
        /// <param name="ErrMsg">提示的消息</param>
        /// <param name="Url">转向的网址,没有可以为 ""</param>
        /// <param name="returnUrl">是否转向</param>
        static public void ShowError(string ErrMsg, string Url, bool returnUrl)
        {
            PageRender(ErrMsg, Url, false, returnUrl);
        }
        static public void ShowError(string ErrMsg)
        {
            ShowError(ErrMsg, "", false);
        }
        static public void ShowError()
        {
            ShowError("操作失败!", "", false);
        }

        static public void ShowRight(string RightMsg, string[] parm)
        {
            PageRender(RightMsg, "", true, false, parm);
        }
        /// <summary>
        /// 页面操作成功提示信息
        /// </summary>
        /// <param name="RightMsg">成功的消息</param>
        /// <param name="Url">成功后转向的地址</param>
        /// <param name="returnUrl">是否转向</param>
        public static void ShowRight(string RightMsg, string Url, bool returnUrl)
        {
            PageRender(RightMsg, Url, true, returnUrl);
        }
        public static void ShowRight()
        {
            ShowRight("操作成功!", "", false);
        }
        static internal void PageRender(string Msg, string Url, bool Succeed, bool returnUrl)
        {
            PageRender(Msg, Url, Succeed, returnUrl, null);
        }

        static internal void PageRender(string Msg, string Url, bool Succeed, bool returnUrl, string[] parm)
        {
            string cssDir = ServerInfo.GetRootURI() + "/sysImages/";
            string STitle = "操作结果!";
            string ReUrlStr = "";
            string _tmp = "<img src=\"" + cssDir + "folder/success.gif\" border=\"0\">";
            string SCaption = "恭喜！操作成功";
            if (!Succeed)
            {
                STitle = "操作失败信息";
                _tmp = "<img src=\"" + cssDir + "folder/error.gif\" border=\"0\">";
                SCaption = "<font color=\"red\">抱歉！操作失败</font>";
            }
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r<head>\r");
            System.Web.HttpContext.Current.Response.Write("<title>" + STitle + "</title>\r");
            System.Web.HttpContext.Current.Response.Write("<link href=\"/sysImages/default/css/css.css\" rel=\"stylesheet\" type=\"text/css\" />\r");
            string js = "<script type=\"text/javascript\">var i = 3;function returnPage(url) {setInterval(function () {if (i == 0) location = url;document.getElementById(\"veer\").innerHTML=(i + \"秒后自动转向...\");i -= 1;}, 1000);}</script>";
            System.Web.HttpContext.Current.Response.Write(js);
            System.Web.HttpContext.Current.Response.Write("\r</head>\r");
            if (returnUrl)
            {
                if (Url != string.Empty && Url != null)
                {
                    System.Web.HttpContext.Current.Response.Write("<body onload=\"returnPage('" + Url + "');\" style=\"margin-top:50px;\">\r");
                    ReUrlStr = "<li><span style=\"color:blue\" id='veer' class='veer'>4秒后自动转向...</span></li>";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("<body style=\"margin-top:50px;\">\r");
            }
            System.Web.HttpContext.Current.Response.Write("    <table style=\"width:65%;height:180px;\"  border=\"0\" align=\"center\" cellspacing=\"1\" cellpadding=\"5\" class=\"table\">\r   <tr class=\"TR_BG\"><td class=\"sysmain_navi\" style=\"height:38px;\" colspan=\"2\">" + SCaption + "</td>\r");
            System.Web.HttpContext.Current.Response.Write("</tr><tr class=\"TR_BG_list\"><td class=\"list_link\" align=\"center\" style=\"40%\">" + _tmp + "<br /><br /></td><td class=\"list_link\"><font color=red>操作描述：</font>\r");
            System.Web.HttpContext.Current.Response.Write("    <ul>\r");
            if (parm == null)
            {
                System.Web.HttpContext.Current.Response.Write("        <li><span style=\"word-wrap:bread-word;word-break:break-all;font-size:11.5px;\">" + Msg + "</span></li>\r         <li><a href='javascript:history.back();'><font color=\"red\">返回上一级</font></a>&nbsp;&nbsp;&nbsp;&nbsp;" + UserUrl(Url) + "</li>" + ReUrlStr + "\r");
            }
            else
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (string item in parm)
                {
                    sb.Append(item);
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                System.Web.HttpContext.Current.Response.Write("<li><span style=\"word-wrap:bread-word;word-break:break-all;font-size:11.5px;\">" + Msg + "</span></li>\r<li>" + sb.ToString() + "</li>" + ReUrlStr + "\r");
            }
            //System.Web.HttpContext.Current.Response.Write("     <li style=\"line-height:20px;\">Copyrigh V" + "1.0.1" + "</li>\r");
            System.Web.HttpContext.Current.Response.Write("     </ul></td></tr>\r    </table>\r");
            //System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.parent.window.scroll(0, 0);</script>");
            System.Web.HttpContext.Current.Response.Write(js);
            System.Web.HttpContext.Current.Response.Write("</body>\r</html>\r");
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="StrUrl"></param>
        /// <returns></returns>
        static private string UserUrl(string StrUrl)
        {
            if (StrUrl.Trim() != string.Empty && StrUrl.Trim().Length > 5)
            {
                StrUrl = "<a href=\"" + StrUrl + "\"><font color=\"red\">返回管理</font></a>";
            }
            return StrUrl;
        }
    }

}
