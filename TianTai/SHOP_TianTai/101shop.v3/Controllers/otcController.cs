using System;
using System.Web.Mvc;
namespace _101shop.v3.Controllers
{
    public class otcController : Controller
    {
        //
        // GET: /otc/
        /// <summary>
        /// otc主页
        /// </summary>
        /// <returns></returns>        
        public ActionResult Index()
        {
            DateTime d = DateTime.Parse("2014-04-10");
            while (d < DateTime.Now)
            {
                d = d.AddDays(4);
            }
            ViewBag.lxfEndtime = d.ToString("yyyy-MM-dd");
            //OTC分类
            ViewBag.PharmAttribute = new SOSOshop.BLL.DrugsBase.Tag_PharmAttribute().GetList(71);
            //用户UID
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.促销公告 = new SOSOshop.BLL.DbBase().ExecuteTableForCache("SELECT TOP 3 id,Title,LinkUrl FROM dbo.yxs_article  WHERE Channel LIKE('101102%') AND State=1 ORDER BY IsTop DESC,id DESC", DateTime.Now.AddHours(1));
            return View();
        }


    }
}
