using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _101shop.v3.Controllers
{
    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 搜索引擎抓取的sitemap
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 60 * 24 * 7)]
        public ActionResult Sitemap()
        {
            Response.AppendHeader("Content-Type", "text/xml");
            return View();
        }

    }
}
