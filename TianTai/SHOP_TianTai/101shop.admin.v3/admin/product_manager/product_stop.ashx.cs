using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace _101shop.admin.v3.admin.product_manager
{
    /// <summary>
    /// product_stop 的摘要说明
    /// </summary>
    public class product_stop : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["pid"];
            string stop = context.Request["stop"];
            string sql = null;
            
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(stop))
            {
                try
                {
                    //用户操作权限审核

                    if (SOSOshop.BLL.PowerPass.isPass("001009003"))
                    {
                        int sp = int.Parse(stop);
                        
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        if (sp == 1)
                        {
                            sql = "update product set Product_bShelves=1,Product_bStop=" + stop + " where Product_ID=" + id;//商品停用并下架
                        }
                        else
                        {
                            sql = "update product set Product_bStop=" + stop + " where Product_ID=" + id;//商品启用，不改变商品上下架状态
                        }
                        //context.Response.Write(sql);
                        int result = db.ExecuteNonQuery(sql);

                        //大表同步更新
                        //sql = "";



                        context.Response.Write("{\"state\":" + result + ",\"message\":\"已处理\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"state\":-1,\"message\":\"对不起！您没有操作权限。\"}");
                    }
                }
                catch (Exception x)
                {
                    context.Response.Write("{\"state\":-1,\"message\":\"" + x.Message + sql + "\"}");
                }

            }
            else
            {
                context.Response.Write("{\"state\":-1,\"message\":\"操作错误！\"}");
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