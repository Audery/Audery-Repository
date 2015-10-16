using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class ResourceController : Controller
    {
        [Authorize]
        public ActionResult Image(string id)
        {
            SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument> db = new SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument>();
            var file = db._gridFS.FindOne(id);
            if (file != null)
            {
                if (Library.Lang.DataValidator.IsDateTime(Request.Headers.Get("If-Modified-Since")))
                {
                    if (DateTime.Parse(Request.Headers.Get("If-Modified-Since")).ToString("yyyy-MM-dd mm:ss") == file.UploadDate.ToLocalTime().ToString("yyyy-MM-dd mm:ss"))
                    {
                        Response.Status = "304 Not Modified";
                        Response.StatusCode = 304;
                        return Content("");
                    }
                }
                byte[] bs = new byte[file.Length];
                using (var s = file.OpenRead())
                {
                    s.Read(bs, 0, bs.Length);
                    Response.Cache.SetLastModified(file.UploadDate);
                    SOSOshop.BLL.Report.DrugTestingReport bll = new SOSOshop.BLL.Report.DrugTestingReport();
                    bll.Inc(id);
                    return File(bs, "image/jpg");
                }
            }
            return Content("");
        }
        /// <summary>
        /// 打包下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult package()
        {
            string id = Request["file"];            
            using (var file = new SOSOshop.BLL.Report.Qualification().GetPackageFile(id, Public.GetUserId()))
            {
                return File(file.ToArray(), "application/x-zip-compressed", DateTime.Now.ToString("yyyy-MM-dd") + ".zip");
            }
        }

    }
}
