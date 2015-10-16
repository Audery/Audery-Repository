using System.Web.Mvc;
using SOSOshop.BLL;
using SOSOshop.BLL.Common;
namespace _101shop.v3.Controllers
{
    public class PromotionsController : Controller
    {
        //
        // GET: /Promotions/

        /// <summary>
        /// 劲爆促销
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            //一排
            ViewBag.中暑 = bll.GetOtcPageList("804,811,807,530").GetPriceTable();//.GetOTCPriceTable();
            if (ViewBag.中暑 != null)
            {
                ViewBag.HeatstrokeCount = GetCountOfProductByClass("1066");
            }
            //二排
            ViewBag.暑湿感冒 = bll.GetOtcPageList("317,802,839,586").GetPriceTable();
            if (ViewBag.暑湿感冒 != null)
            {
                ViewBag.Heatstroke_CodeCount = GetCountOfProductByClass("1065");
            }
            //三排
            ViewBag.胃肠感冒 = bll.GetOtcPageList("481,452,631,350").GetPriceTable();
            if (ViewBag.胃肠感冒 != null)
            {
                ViewBag.Stomach_FluCount = GetCountOfProductByClass("15");
            }
            //四排
            ViewBag.风热感冒 = bll.GetOtcPageList("806,828,696,789").GetPriceTable();
            if (ViewBag.风热感冒 != null)
            {
                ViewBag.Windheat_Code = GetCountOfProductByClass("13");
            }
            //用户UID
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;

            return View();
        }
       
        /// <summary>
        /// 一元促销
        /// </summary>
        /// <returns></returns>
        public ActionResult C158AABC_005E()
        {
            
            return View();
        }
        
        /// <summary>
        /// 根据种类取得该类药品的个数
        /// </summary>
        public static int GetCountOfProductByClass(string classId)
        {
            string sql = string.Format(@"SELECT count(*)
                                        FROM dbo.product_online_v
                                        WHERE DrugsBase_id in(
	                                        SELECT product_id 
	                                        FROM Tag_PharmProduct 
	                                        WHERE product_key='d' 
		                                          and Tag_PharmAttribute_id in (
			                                          select id 
			                                          from Tag_PharmAttribute 
			                                          where fullPath like '%/{0}/%'))", classId);

            Db db = new Db();

            object obj = db.ExecuteScalarForCache(sql);

            int count = 0;
            int.TryParse(obj.ToString(), out count);

            return count;
        }

    }
}
