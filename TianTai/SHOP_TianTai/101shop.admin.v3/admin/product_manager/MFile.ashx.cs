using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _101shop.admin.v3.admin.product_manager
{
    /// <summary>
    /// MFile 的摘要说明
    /// </summary>
    public class MFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["file"].EndsWith("rar") || context.Request.QueryString["file"].EndsWith("zip"))
            {
                context.Response.ContentType = "application/x-rar-compressed";
                context.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", context.Request.QueryString["file"]));
            }
            else
            {
                context.Response.ContentType = "image/jpg";
            }
            SOSOshop.BLL.Report.DrugTestingReport bll = new SOSOshop.BLL.Report.DrugTestingReport();
            using (var fileStream = bll.GetFile(context.Request.QueryString["file"]))
            {
                long fileSize = fileStream.Length;
                byte[] fileBuffer = new byte[fileSize];
                fileStream.Read(fileBuffer, 0, (int)fileSize);
                context.Response.BinaryWrite(fileBuffer);
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