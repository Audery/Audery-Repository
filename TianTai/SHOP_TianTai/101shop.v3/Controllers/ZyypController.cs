using System.Web.Mvc;
namespace _101shop.v3.Controllers
{
    public class ZyypController : Controller
    {

        // 中药饮片首页
        // GET: /zyyp/           
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分类导航
        /// </summary>
        /// <returns></returns>
        [OutputCache(CacheProfile = "public")]
        public ActionResult Navigate()
        {
            return View();
        }
    }    
}
