using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _101shop.v3.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using System.Data.Common;
using System.Data;

namespace _101shop.v3.Controllers
{
    public class JfController : Controller
    {
        //
        // GET: /Jf/

        public ActionResult Index()
        {
            SOSOshop.BLL.DbBase bll = new SOSOshop.BLL.DbBase();
            return View(bll.ExecuteTable("select * from MemberIntegralGift where State=1"));
        }

    }
}
