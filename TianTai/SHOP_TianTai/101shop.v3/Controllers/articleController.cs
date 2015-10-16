using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using System.Data;

namespace _101shop.v3.Controllers
{
    public class articleController : Controller
    {
        //
        // GET: /article/
        [OutputCache(CacheProfile = "public")]
        public ActionResult Index(int id)
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            ViewBag._101资讯 = bll.ExecuteTableForCache("SELECT TOP 10 id,Title FROM dbo.yxs_article WHERE Channel LIKE('100%') and id<>" + id + " ORDER BY id DESC");
            string sql = "SELECT Title,SubTitle,KeyWord,Content,Introduction,CopyFrom,updateTime FROM dbo.yxs_article WHERE id=" + id;
            return View(bll.ExecuteTableForCache(sql).Rows);
        }
        /// <summary>
        /// 资讯列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult list(string id = "100")
        {
            //记录总数
            int recordCount = 0;

            //页总数
            int pageCount = 0;

            //页大小
            int pageSize = 12;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            ViewBag.id = id;
            var bll = new SOSOshop.BLL.Db();
            DataTable key = bll.ExecuteTableForCache("SELECT Name,Description,MeteKey,MeteDescription FROM dbo.yxs_articlechannel where id=" + Library.Lang.Input.Filter(id));
            if (key.Rows.Count > 0)
            {
                ViewBag.Name = key.Rows[0]["Name"];                
                ViewBag.MeteKey = key.Rows[0]["MeteKey"];
                ViewBag.MeteDescription = key.Rows[0]["MeteDescription"];
            }
            string where = string.Format(" AND Channel LIKE('{0}%')", Library.Lang.Input.Filter(id));
            DataTable dt = bll.GetListByPage("yxs_article", "id,Title,CreateTime", pageSize, pageIndex, " id desc ", where, out recordCount, out pageCount);
            //定义页面
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);

            return View(pl);
        }

    }
}
