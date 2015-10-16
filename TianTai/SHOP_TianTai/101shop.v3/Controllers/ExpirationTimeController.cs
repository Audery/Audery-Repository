using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _101shop.v3.Models;
using SOSOshop.BLL.Product;
using System.Data;

namespace _101shop.v3.Controllers
{
    public class ExpirationTimeController : Controller
    {
        //
        // GET: /ExpirationTime/

        public ActionResult Index()
        {

            List<ExpirationTimeModel> Model = new List<ExpirationTimeModel>();

            DataTable dt = ExpirationTime.CreateInstance().GetProduct_ExpirationTimeList();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    Model.Add(new ExpirationTimeModel
                    {
                        ID = Convert.ToInt32(item["ID"]),
                        Product_ID = Convert.ToInt32(item["Product_ID"]),
                        Erp_ID = item["Erp_ID"].ToString(),
                        Product_Name = Convert.ToString(item["Product_Name"] ?? string.Empty),
                        Image = Convert.ToString(item["Image"] ?? string.Empty),
                        ShowPrice = Convert.ToString(item["Price"]),
                        Goods_Unit = Convert.ToString(item["Goods_Unit"] ?? string.Empty),
                        ExpirationTime = item["ExpirationTime"] == null ? string.Empty : Convert.ToDateTime(item["ExpirationTime"]).ToString("yyyy-MM-dd")
                    });

                }

            }

            return View(Model);
        }

    }
}
