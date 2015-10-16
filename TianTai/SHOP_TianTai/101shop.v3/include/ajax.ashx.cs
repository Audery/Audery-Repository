using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SOSOshop.BLL;
using System.Text;
using System.Data.Common;
using System.Data;

namespace _101shop.v3.include
{
    /// <summary>
    /// ajax 的摘要说明
    /// </summary>
    public class ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            bool ok = false;
            bool is_ajax = false; bool.TryParse(context.Request["is_ajax"], out is_ajax);
            string act = context.Request["act"];
            int uid = _101shop.v3.Controllers.BaseController.GetUserId();
            int pid = 0; int.TryParse(context.Request["pid"], out pid);
            if (!string.IsNullOrEmpty(act))
            {
                if (is_ajax)
                {
                    switch (act)
                    {
                        case "AddFavorite"://需要登陆
                            if (uid > 0 && pid > 0)
                            {
                                SOSOshop.BLL.Memberfavorite bll = new SOSOshop.BLL.Memberfavorite();
                                ok = "0" != bll.AddMemberFavorite(uid, pid);
                                context.Response.Write(ok ? '1' : '0');
                            }
                            else
                            {
                                context.Response.Write('0');
                            }
                            break;
                    }
                    context.Response.End();
                }
                else
                {
                    switch (act)
                    {
                        case "updateEmail"://邮箱验证1
                            if (uid > 0)
                            {
                                int Step = 1;
                                DateTime h24 = DateTime.Now.AddHours(-24.0);
                                string source = "http://" + context.Request.Url.Host + "/include/ajax.ashx?act=" + act + "&v";
                                string where = "UID = " + uid + " AND CHARINDEX('" + context.Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                                int getpass_ticks = SysLog.SelectCount("FieldForValue = '1' AND FieldAfterValue = '0' AND " + where);
                                if (getpass_ticks > 0)
                                {
                                    Step = 2;
                                    context.Session["updateEmail"] = true;
                                    SysLog.Query("UPDATE yxs_SysLog SET FieldForValue = '1', FieldAfterValue = '1' WHERE " + where);
                                }
                                context.Response.Redirect("/MemberCenter/Email?Step=" + Step, true);
                            }
                            else
                            {
                                context.Response.Redirect("/Account/LogOn", true);
                            }
                            break;
                        case "updateEmailComplete"://邮箱验证2
                            if (uid > 0)
                            {
                                int Step = 2;
                                DateTime h24 = DateTime.Now.AddHours(-24.0);
                                string source = "http://" + context.Request.Url.Host + "/include/ajax.ashx?act=" + act + "&v";
                                string where = "UID = " + uid + " AND CHARINDEX('" + context.Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                                int getpass_ticks = SysLog.SelectCount("FieldForValue = '1' AND FieldAfterValue = '0' AND " + where);
                                if (getpass_ticks > 0)
                                {
                                    Step = 3;
                                    SysLog.Query("UPDATE yxs_SysLog SET FieldForValue = '1', FieldAfterValue = '1' WHERE " + where);
                                }
                                context.Response.Redirect("/MemberCenter/Email?Step=" + Step, true);
                            }
                            else
                            {
                                int.TryParse(context.Request["uid"], out uid);
                                DateTime h24 = DateTime.Now.AddHours(-24.0);
                                string source = "http://" + context.Request.Url.Host + "/include/ajax.ashx?act=" + act + "&v";
                                string where = "UID = " + uid + " AND CHARINDEX('" + context.Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                                int getpass_ticks = SysLog.SelectCount("FieldForValue = '1' AND FieldAfterValue = '0' AND " + where);
                                if (getpass_ticks > 0)
                                {
                                    SysLog.Query("UPDATE yxs_SysLog SET FieldForValue = '1', FieldAfterValue = '1' WHERE " + where);
                                    SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
                                    ok = 0 < bll.ExecuteNonQuery(string.Format("IF (NOT EXISTS(SELECT TOP(1) * FROM membercheck WHERE CheckType='E' AND UID={0})) INSERT INTO membercheck (UID, Checked, CheckType) VALUES ({0},1,'E') ELSE UPDATE membercheck SET Checked=1 WHERE CheckType='E' AND UID={0}", uid));
                                }
                                if (ok)
                                {
                                    context.Response.Redirect("/MemberCenter/EmailChecked?uid=" + uid, true);
                                }
                                else
                                {
                                    context.Response.Redirect("/", true);
                                }
                            }
                            break;
                        case "ClearCache"://清除所有缓存
                            if (context.Request["All"] != null)
                            {
                                new DbBase().ClearCache();
                            }
                            context.Response.Redirect("/", true);
                            break;
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}