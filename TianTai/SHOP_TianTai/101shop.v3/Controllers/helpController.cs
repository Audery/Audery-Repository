using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _101shop.v3.Controllers
{
    public class helpController : Controller
    {
        //
        // GET: /help/
        [OutputCache(CacheProfile = "public")]
        public ActionResult Index(int id)
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            ViewBag._101资讯 = bll.ExecuteTableForCache("SELECT TOP 10 Id,Title,Channel FROM dbo.yxs_article WHERE State=1 AND Channel LIKE('102%')");
            string sql = "SELECT Title,SubTitle,KeyWord,Content,Introduction, (SELECT TOP (1) Name FROM yxs_articlechannel WHERE Id = a.Channel) AS Channel FROM dbo.yxs_article a WHERE State=1 AND Id=" + id;
            return View(bll.ExecuteTableForCache(sql).Rows);
        }

    }
}
