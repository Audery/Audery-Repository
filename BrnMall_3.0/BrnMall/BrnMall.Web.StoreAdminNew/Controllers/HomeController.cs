using System.Web.Mvc;
namespace BrnMall.Web.StoreAdminNew.Controllers
{
    public partial class HomeController :Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
    }
}