using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace _101shop.admin.v3.filehandle
{
    /// <summary>
    /// productSelect 的摘要说明
    /// </summary>
    public class productSelect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder s = new StringBuilder();
            string types = ChangeHope.WebPage.PageRequest.GetFormString("Option");
            int id = ChangeHope.WebPage.PageRequest.GetFormInt("id");
            if (types != string.Empty && id > 0)
            {
                //获取销售方式
                int sellType = id;
                if (types == "GetSellTypeName")
                {
                    s.Append(_101shop.Common.SellType.GetType(sellType));
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
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