using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SOSOshop.Model;

namespace _101shop.admin.v3.admin.product_manager
{
    /// <summary>
    /// product_shelves 的摘要说明
    /// </summary>
    public class product_shelves : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["pid"];
            string shelves = context.Request["shelves"];
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(shelves))
            {
                try
                {
                    //用户操作权限审核                    
                    if (SOSOshop.BLL.PowerPass.isPass("001009005"))
                    {
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();


                        string sql = string.Format("UPDATE product SET beactive='{0}' ,Product_bShelves='{1}' WHERE Product_ID={2}", shelves.Equals("1") ? "是" : "否", shelves, id);
                        //context.Response.Write(sql);
                        int result = db.ExecuteNonQuery(sql);
                        AdminInfo adminModel = (AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                        SOSOshop.BLL.Logs.Log.LogAdminAdd(string.Format("{2}了商品:[{0}][{1}]", id, new SOSOshop.BLL.Db().ExecuteScalar("SELECT Product_Name FROM dbo.Product WHERE Product_ID=" + id), shelves == "1" ? "上架" : "下架"), adminModel.AdminId, adminModel.AdminName, 1);
                        //大表同步更新
                        sql = "";

                        context.Response.Write("{\"state\":" + result + ",\"message\":\"已处理\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"state\":-1,\"message\":\"对不起！您没有操作权限。\"}");
                    }
                }
                catch (Exception x)
                {
                    context.Response.Write("{\"state\":-1,\"message\":\"" + x.Message + "\"}");
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