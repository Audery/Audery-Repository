using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SOSOshop.Model;

namespace _101shop.admin.v3.admin.product_manager
{
    /// <summary>
    /// product_update 的摘要说明
    /// </summary>
    public class product_update : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";            
            string bshelves = context.Request["bshelves"];
            string state = context.Request["state"];            
            string pid = context.Request["pid"];

            if (!string.IsNullOrEmpty(pid))
            {
                if (SOSOshop.BLL.PowerPass.isPass("001009004"))
                {
                    string s = string.Format("update product set Product_State='{1}' where [Product_ID]={2}", bshelves, state, pid);
                    try
                    {                    
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        int ret = db.ExecuteNonQuery(s);
                        if (ret > 0)
                        {
                            AdminInfo adminModel = (AdminInfo)SOSOshop.BLL.AdministrorManager.Get();                            
                            SOSOshop.BLL.Logs.Log.LogAdminAdd(string.Format("{2}了商品:[{0}][{1}]", pid, new SOSOshop.BLL.Db().ExecuteScalar("SELECT Product_Name FROM dbo.Product WHERE Product_ID=" + pid), bshelves == "1" ? "上架" : "下架"), adminModel.AdminId, adminModel.AdminName, 1);
                            context.Response.Write("{\"state\":" + ret + ",\"message\":\"更新成功！\"}");
                        }
                        else
                        {
                            context.Response.Write("{\"state\":" + ret + ",\"message\":\"更新失败！\"}");
                        }
                    }
                    catch(Exception x)
                    {
                        context.Response.Write("{\"state\":-2,\"message\":\"" + x.Message + "\"}");                        
                    }
                }
                else
                {
                    context.Response.Write("{\"state\":-1,\"message\":\"对不起，您没有编辑权限，请联系管理员！\"}");
                }
            }
            else
            {
                context.Response.Write("{\"state\":-1,\"message\":\"未知错误！\"}");
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