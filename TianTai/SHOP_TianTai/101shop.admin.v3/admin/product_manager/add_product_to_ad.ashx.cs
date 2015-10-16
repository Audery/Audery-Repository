using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _101shop.admin.v3.admin.product_manager
{
    /// <summary>
    /// AddProductToAD 的摘要说明
    /// </summary>
    public class add_product_to_ad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["pid"];
            string code = context.Request["code"];
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(code))
            {
                try
                {
                    SOSOshop.BLL.Advertising ad = new SOSOshop.BLL.Advertising();
                    SOSOshop.BLL.Advertising ads = ad.GetModelByCode(code);
                    List<int> ids = new List<int>();

                    if (ads != null)
                    {
                        
                        if (ads.ProductID != null)
                        {
                            ids = ads.ProductID;
                            if (ids.IndexOf(Convert.ToInt32(id)) == -1)
                            {
                                ids.Add(Convert.ToInt32(id));
                                ads.ProductID = ids;
                            }
                        }
                        else
                        {
                            ids.Add(Convert.ToInt32(id));
                            ads.ProductID = ids;
                        }
                        ad.Update(ads);
                    }
                    else
                    {
                        ad.Code = code;
                        ids.Add(Convert.ToInt32(id));
                        ad.ProductID = ids;
                        ad.Update(ad);
                    }
                    context.Response.Write("{\"state\":0,\"message\":\"添加成功！\"}");
                   
                }
                catch (Exception x)
                {
                    context.Response.Write("{\"state\":0,\"message\":\"添加失败Error！\"}");
                    
                }
            }
            else
            {
                context.Response.Write("{\"state\":0,\"message\":\"添加失败！\"}");                
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