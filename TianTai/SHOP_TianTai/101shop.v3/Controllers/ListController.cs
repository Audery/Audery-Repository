using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using SOSOshop.BLL;
using _101shop.v3.Models;
using Webdiyer.WebControls.Mvc;
using System.Data;
using System.Configuration;
using SOSOshop.BLL.Common;
namespace _101shop.v3.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        [OutputCache(CacheProfile = "public")]
        public ActionResult Index(string filter, int pageIndex)
        {

            #region 控制搜索引擎访问频率(目前设置的十秒钟内最多可以访问10次)
            string SearchEngines = Public.IsSearchEnginesGet();
            if (SearchEngines != null)
            {
                SOSOshop.BLL.SearchEngines blls = new SearchEngines();
                if (!blls.isPower(SearchEngines))
                {
                    Response.StatusCode = 503;
                    return Content("");
                }
                blls.created = DateTime.Now;
                blls.Engines = SearchEngines;
                blls.ip = Request.UserHostAddress;
                blls.insert();
            }
            #endregion

            ViewBag.Title = ConfigurationManager.AppSettings["List_Title"];
            ViewBag.description = ConfigurationManager.AppSettings["List_Description"];
            ViewBag.keywords = ConfigurationManager.AppSettings["List_Key"];



            ViewBag.ActuationValue = "查看所有商品";
            SOSOshop.BLL.Db db = new Db();
            int showpic = 0;//用户对列表状态无选择则为0，大图为1，列表为2.
            string isshow = Request["show"];
            isshow = Library.Lang.Input.Filter(isshow);
            if (!string.IsNullOrEmpty(isshow))
            {
                if (int.Parse(isshow) == 1)
                {
                    showpic = 1;
                }
                else
                {
                    showpic = 2;
                }
            }
            //设置是否是大图列表
            ViewBag.Show = showpic;

            //设置排序
            string sort = Request["sort"];
            sort = Library.Lang.Input.Filter(sort);
            ViewBag.Sort = 0;
            string order = " order by Product_ID desc";
            if (!string.IsNullOrEmpty(sort))
            {
                switch (int.Parse(sort))
                {
                    case 1:
                        order = " order by Product_ClickNum desc";
                        ViewBag.Sort = 1;
                        break;
                    case 2:
                        ViewBag.Sort = 2;
                        order = " order by Price_01 asc";
                        #region 买家类型判断
                        int UID = BaseController.GetUserId();
                        if (UID > 0)
                        {
                            int Member_Class = 0;//批发
                            object objMC = db.ExecuteScalarForCache("SELECT Member_Class FROM memberinfo WHERE UID=" + UID);
                            if (objMC != null) Member_Class = int.Parse(objMC.ToString());
                            if (Member_Class == 1) order = " order by Price_02 desc";//OTC
                        }
                        #endregion
                        break;
                    case 3:
                        ViewBag.Sort = 3;
                        order = " order by Product_SaleNum desc";
                        break;
                    case 4:
                        ViewBag.Sort = 4;
                        order = " order by price_03 asc";
                        break;
                }
            }

            //定义分页信息
            PageInfo pg = new PageInfo();
            pg.pageSize = 24;
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            //设置分类固定长度,增加新的分类后要将长度调整为对应的长度
            string SearchUrl = HomeController.SearchUrl(0, 0);
            int[] keyIds = new int[filter.Split('-').Length];
            if (keyIds.Length < 10)
            {
                return Redirect(HomeController.SearchUrl(0, 0));
            }

            int[] akeyIds = (from a in filter.Split('-') select int.Parse(a)).ToArray();
            for (int x = 0; x < akeyIds.Length && akeyIds.Length <= keyIds.Length; x++)
            {
                keyIds[x] = akeyIds[x];
            }

            // url_filter = string.Join("-", keyIds);//filter;

            List<string> keywords = new List<string>();//选择的条件
            List<int> keyposit = new List<int>();//选择条件的位置
            #region 拼接sql条件
            StringBuilder filterExpression = new StringBuilder(177);
            filterExpression.Append(" 1=1");
            int index = 0;
            string type = null;

            //药理药效
            if (keyIds[index] != 0)
            {

                filterExpression.Append(" and DrugsBase_ID in (select DrugsBase_ID from [DrugsBase_PharmMediNameLink] where [Pharm_ID] in (select [Pharm_ID] from [DrugsBase_Pharm] where [Pharm_ID_Path] like '%\\" + keyIds[index].ToString() + "%'))");
                if (keyIds[index] == 1)
                {
                    keywords.Add("西药");
                }
                else if (keyIds[index] == 583)
                {
                    keywords.Add("中成药");
                }
                else
                {
                    if (SearchModel.GetList(SearchModelEnum.药理二级).Where(x => x.id == keyIds[index]).Count() > 0)
                    {
                        keywords.Add(SearchModel.GetList(SearchModelEnum.药理二级).Where(x => x.id == keyIds[index]).First().name);
                    }
                }
                keyposit.Add(0);
                type = "药理药效";
                //分类热销品种
                SelectPharmById(keyIds[index]);
            }

            //适应症
            index = 1;
            if (keyIds[index] != 0)
            {
                //filterExpression.AppendFormat(" and DrugsBase_id in(SELECT product_id FROM Tag_PharmProduct WHERE product_key='d' and Tag_PharmAttribute_id={0})", keyIds[index]);
                string sql_syz = string.Format(" and DrugsBase_id in(SELECT product_id FROM Tag_PharmProduct WHERE Tag_PharmAttribute_id in (select id from Tag_PharmAttribute where fullPath like '%/{0}/%'))", keyIds[index]);
                filterExpression.Append(sql_syz);
                var blltag = new SOSOshop.BLL.DrugsBase.Tag_PharmAttribute();
                string syz = blltag.GetTagName(keyIds[index]);
                keywords.Add(blltag.GetKeyWord(keyIds[index]));
                keyposit.Add(1);
                type = "适应症";
                //列出适应症的二级分类
                string syzsql = string.Format("select id,name from Tag_PharmAttribute where fullPath like '%/'+convert(varchar,(select case when ParentId=0 then id else ParentId end  from Tag_PharmAttribute where id={0}))+'/%' order by ParentId", keyIds[index]);
                ViewBag.SyzList = db.ExecuteTableForCache(syzsql);
                ViewBag.SelectMenuId = keyIds[index];
                //分类热销品种
                SelectSYZById(keyIds[index], syz, sql_syz);
            }
            //厂家数量
            index = 2;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.厂家数量);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "厂家数量";
                }

            }
            //价格区间
            index = 3;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.价格区间);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "价格区间";
                }
            }
            //剂型
            index = 4;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.剂型二级);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.AppendFormat(model.condition == null ? " and DrugsBase_DrugName like('%{0}%')" : model.condition, model.name);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "剂型";
                }
            }
            //热门标签
            index = 5;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.热门标签);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "热门标签";
                }
            }
            //品牌厂家
            index = 6;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.品牌厂家);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "品牌厂家";
                }
            }


            //中药饮片
            index = 8;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.中药饮片);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "中药饮片";
                }

            }
            //进口药品
            index = 9;
            if (keyIds[index] != 0)
            {
                var list = SearchModel.GetList(SearchModelEnum.进口药品);
                if (list.Exists(x => x.id == keyIds[index]))
                {
                    var model = list.Where(x => x.id == keyIds[index]).First();
                    filterExpression.Append(model.condition);
                    keywords.Add(model.name);
                    keyposit.Add(index);
                    type = "进口药品";
                }

            }
            #endregion

            //新品上架的品种必须有库存
            if ("1".Equals(Request.QueryString["new"]))
            {
                filterExpression.Append(" and Product_ID in (SELECT a.Product_ID FROM dbo.Product  a LEFT OUTER JOIN dbo.Stock_Lock b ON a.Product_ID = b.Product_ID WHERE a.Stock-ISNULL(b.Stock,0)>0)");
            }
            //折扣商品列表
            if ("2".Equals(Request.QueryString["new"]))
            {
                filterExpression.Append(" and Product_ID in (SELECT a.Product_ID FROM dbo.Product a where Discount>0 AND Discount<>1 AND GETDATE() BETWEEN BeginDate AND EndDate )");
            }
            //促销商品列表
            if ("3".Equals(Request.QueryString["new"]))
            {
                filterExpression.Append(" and Product_ID in (SELECT a.Product_ID FROM dbo.Product a where (CuPrice>0 or (discount>0 and discount<>1)) AND GETDATE() BETWEEN BeginDate AND EndDate )");
            }

            //产品搜索
            string search = Request["q"];
            if (search != null)
            {
                search = search.Trim().Trim('+');
            }
            search = Library.Lang.Input.Filter(search);
            if (!string.IsNullOrEmpty(search))
            {
                filterExpression.Append(string.Format(" and ([Product_Name] like '%{0}%' or DrugsBase_SimpeCode like '%{0}%'  or [DrugsBase_DrugName] like '%{0}%' or [DrugsBase_ProName] like '%{0}%' or [DrugsBase_Manufacturer] like '%{0}%'  or [DrugsBase_ApprovalNumber] like '%{0}%' )", search));
                type = "搜索";
                ViewBag.ActuationValue = string.Format("搜索：" + search);
                ViewBag.Search = search;
                ViewBag.Title = search + ConfigurationManager.AppSettings["List_Search_Title"];
                ViewBag.description = search + ConfigurationManager.AppSettings["List_Search_Description"];
                ViewBag.keywords = search + ConfigurationManager.AppSettings["List_Search_Key"];

            }

            string ypmc = Request["ypmc"];
            if (ypmc != null)
            {
                ypmc = ypmc.Trim().Trim('+');
            }
            ypmc = Library.Lang.Input.Filter(ypmc);
            if (!string.IsNullOrEmpty(ypmc))
            {
                filterExpression.Append(string.Format(" and ([Product_Name] like '%{0}%' or DrugsBase_SimpeCode like '%{0}%'  or [DrugsBase_DrugName] like '%{0}%' or [DrugsBase_ProName] like '%{0}%' or [DrugsBase_Manufacturer] like '%{0}%'  or [DrugsBase_ApprovalNumber] like '%{0}%' )", ypmc));
                type = "搜索";
                ViewBag.Search = search + " " + ypmc;
            }

            string sccj = Request["sccj"];
            if (sccj != null)
            {
                sccj = sccj.Trim().Trim('+');
            }
            sccj = Library.Lang.Input.Filter(sccj);
            if (!string.IsNullOrEmpty(sccj))
            {
                filterExpression.Append(string.Format(" and ([DrugsBase_Manufacturer] like '%{0}%')", sccj));
                type = "搜索";
                ViewBag.Search = search + " " + ypmc + " " + sccj;
            }

            //参数限定
            string stop = "[Product_bShelves] =1"; //and [DrugsBase_bStop]=0 and [Product_bStop]=0";
            filterExpression.Append(" and " + stop);

            //显示的字段列表
            string fields = SOSOshop.BLL.Product.Product._PriceTableColumns + "[Product_ID],[Product_Name],[Product_ClickNum],[Product_SaleNum] ,[Product_State] ,"
                + "DrugsBase_ProName,DrugsBase_Manufacturer,DrugsBase_Specification,"
                + "Goods_ID,Goods_Pcs,Goods_Pcs_Small,Goods_ConveRatio,Goods_Unit,Image,drug_sensitive,"
                + "Goods_ConveRatio_Unit,Goods_ConveRatio_Unit_Name,is_cl,"
                + "stock-ISNULL((select stock from stock_lock where [Product_ID]=pt.[Product_ID]),0) stock_lock,"
                //袋装量
                + @"ISNULL(( SELECT ISNULL(BagCapacity, 1.00) AS BagCapacity
                 FROM   dbo.DrugsBase_ZYC
                 WHERE  DrugsBase_ID = pt.DrugsBase_ID
               ), 1) AS BagCapacity";
            string stock = "case when Stock<=0 or (sellType=2 and Stock<Goods_Pcs_Small) or (sellType=3 and Stock<Goods_Pcs) then '可预订' else '现货' end as stock1";
            fields = string.Format("{0},{1} ", fields, stock);

            //药品列表
            string sql = "SELECT TOP " + pg.pageSize + " " + fields + " FROM product_online_v pt WHERE ([Product_ID] NOT IN(SELECT TOP (" + pg.pageSize + " * " + (pageIndex - 1) + ") [Product_ID] FROM product_online_v where " + filterExpression + " " + order + ")) and " + filterExpression + " " + order;
            //Response.Write(sql);
            //Response.End();
            ViewBag.List = db.ExecuteTableForCache(string.Format(sql, filter), DateTime.Now.AddHours(1)).GetPriceTable();
            ViewBag.url = filter;
            //列表总数
            string countsql = "select count(Product_ID) as pagecount from product_online_v where " + filterExpression;
            //计算总数
            DataTable page = db.ExecuteTableForCache(countsql, DateTime.Now.AddHours(1));
            double cs = ((int)page.Rows[0]["pagecount"]) / pg.pageSize;
            //页总数
            try
            {
                //Response.Write(((int)page.Rows[0]["pagecount"]) / pg.pageSize +" "+ Math.Ceiling((double)(((int)page.Rows[0][0]) / pg.pageSize)) + " " + cs + " " + page.Rows[0]["pagecount"] + " " + pg.pageSize);
                //Response.End();
                ViewBag.PageCount = int.Parse(Math.Ceiling(cs).ToString());
            }
            catch
            {
                ViewBag.PageCount = 1;
            }
            //当前页
            ViewBag.CurrentPage = pageIndex;
            //记录总数
            ViewBag.RecordSize = (int)page.Rows[0]["pagecount"];

            //文件url
            ViewBag.PageURL = filter;
            ViewBag.Selected = keywords;
            ViewBag.Posit = keyposit;
            ViewBag.Type = type;
            PagedList<DataRow> pl = null;
            if (ViewBag.List != null)
            {
                ViewBag.List = ViewBag.List;
                pl = new PagedList<DataRow>(ViewBag.List.Select(), pageIndex, pg.pageSize, (int)page.Rows[0]["pagecount"]);
            }
            //SEO 复合条件选择
            if (keywords.Count > 0 && string.IsNullOrEmpty(search))
            {
                ViewBag.ActuationValue = string.Format("筛选条件：{0}", string.Join("-", keywords));
                ViewBag.Title = string.Join("-", keywords) + ConfigurationManager.AppSettings["List_Search2_Title"];
                string[] descriptions = ConfigurationManager.AppSettings["List_Search2_Description"].Split('|');
                ViewBag.description = string.Format("{0}为您找到 " + string.Join(",", keywords) + " 的药品信息" + page.Rows[0]["pagecount"] + "条；更多网上药品批发,药品采购，药品价格信息登录{1}，随时查询，方便快捷，采购就这么简单！", descriptions[0], descriptions[0]);
                ViewBag.keywords = string.Join(",", keywords) + ConfigurationManager.AppSettings["List_Search2_Key"];
            }

            MemberInfo member = new MemberInfo();
            int uid = BaseController.GetUserId();
            int member_Class = -1;//客户未登录
            if (uid > 0)
            {
                member_Class = member.GetModel(uid).Member_Class;//用户类型 0 批发客户，1 OTC批零客户
            }
            ViewBag.Memberclass = member_Class;
            ViewBag.UID = uid;

            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            //最近浏览过的商品
            if (uid != 0)
            {
                ViewBag.History_Of_ProductList = bll.Get_History_Of_ProductList(10, uid).GetPriceTable();
            }
            //热销榜

            string sqlhot = string.Empty;

            if (member_Class == 1)
            {
                sqlhot = "SELECT TOP 9  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Name,Product_Advertisement,Image," +
                             "Product_SaleNum as SaleNum,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,Goods_Unit Goods_Unit1, DrugsBase_ID " +
                             "FROM product_online_v p " +
                             "ORDER BY SaleNum DESC";
            }
            else
            {
                sqlhot = "SELECT TOP 9  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image, " +
                             "( " +
                                 "SELECT SUM(Product_SaleNum/Goods_pcs) " +
                                 "FROM product_online_v " +
                                 "WHERE DrugsBase_ID=p.DrugsBase_ID " +
                             ")SaleNum,'件' Goods_Unit1,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum " +
                             "FROM product_online_v p " +
                             "where (Price_01  is not null and Price_01!=0.000000) " +
                             "AND p.Goods_Pcs != 1 " +
                             "ORDER BY SaleNum DESC";
            }
            try
            {
                ViewBag.Hot = db.ExecuteTableForCache(sqlhot).GetPriceTable();
            }
            catch (Exception)
            {

                ViewBag.Hot = new DataTable();
            }

            //热门推荐
            string sqltj = "select top 2 " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,product_name,DrugsBase_Specification as gg,DrugsBase_Manufacturer,Product_SaleNum/Goods_Pcs as jian,Image,[Goods_ConveRatio] ,[Goods_ConveRatio_Unit] ,[Goods_ConveRatio_Unit_Name],[Goods_Pcs],[Goods_Unit],drug_sensitive,minsell,maxsell from product_online_v where product_id=24 or product_id=37 order by Product_SaleNum desc";
            ViewBag.HotTj = db.ExecuteTableForCache(sqltj).GetPriceTable();
            ViewBag.ActuationValue += ":pageIndex-" + pageIndex;
            return View(pl);
        }

        private void SelectPharmById(int pid)
        {
            SOSOshop.BLL.DrugsBase.DrugsBase_Pharm bllp = new SOSOshop.BLL.DrugsBase.DrugsBase_Pharm();
            DataTable all = bllp.GetList();
            DataView dv = new DataView(all);
            dv.RowFilter = "Pharm_ID=" + pid;
            if (dv.Count > 0)
            {
                DataView dv2 = new DataView(all);
                dv2.RowFilter = "Pharm_Parent_ID=" + pid;
                ViewBag.DrugsBase_Pharm_Of_Product = dv2.ToTable();
                ViewBag.ThisClass = dv.ToTable().Rows[0]["Pharm_Name"].ToString();//分类
                ViewBag.ThisClassID = pid;//分类ID
                //分类热销品种列表
                SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
                DataTable ThisClass_ProductList = bll.Get_ThisClasses_ProductList(4, pid).GetPriceTable();
                ViewBag.ThisClass_ProductList = ThisClass_ProductList;
                ViewBag.ThisClass_ProductList_Count = ThisClass_ProductList == null ? 0 : ThisClass_ProductList.Rows.Count;
            }
        }
        private void SelectSYZById(int pid, string syz, string sql_syz)
        {
            if (pid > 0)
            {
                ViewBag.ThisClass_SYZ = syz;//分类
                ViewBag.ThisClassID_SYZ = pid;//分类ID
                //分类热销品种列表
                SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
                DataTable ThisClass_ProductList = bll.GetPageListByWhere(4, sql_syz).GetPriceTable();
                ViewBag.ThisClass_ProductList_SYZ = ThisClass_ProductList;
                ViewBag.ThisClass_ProductList_Count_SYZ = ThisClass_ProductList == null ? 0 : ThisClass_ProductList.Rows.Count;
            }
        }
        /// <summary>
        /// 关注
        /// </summary>
        [HttpPost]
        public void Shore()
        {
            Response.Clear();
            if (User.Identity.IsAuthenticated)
            {
                string pid = Request["pid"];
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Write("{\"error\":\"请选择要收藏的药品！\"}");
                }
                else
                {
                    Response.Write("{\"error\":\"收藏失败！\"}");
                }
            }
            else
            {
                Response.Write("{\"error\":\"登录后才能收藏！\"}");
            }
            Response.End();
        }

        /// <summary>
        /// 标识
        /// </summary>
        /// <param name="tb">库状态串</param>
        /// <returns></returns>
        public static SOSOshop.Model.ProductState tab(string tb)
        {
            return SOSOshop.BLL.ProductState.GetModel(tb);
        }

        /// <summary>
        /// 搜索URL拼接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static string SearchUrl(int id, int loc, string filter)
        {
            int[] urls = (from a in filter.Split('-') select int.Parse(a)).ToArray();
            urls[loc] = id;
            return string.Format("/products/{0}-1.html", string.Join("-", urls));
        }

        /// <summary>
        /// 去掉选择中的标签内容
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static string DeleteUrl(int loc, string filter)
        {
            int[] urls = (from a in filter.Split('-') select int.Parse(a)).ToArray();
            urls[loc] = 0;
            return string.Format("/products/{0}-1.html", string.Join("-", urls));
        }

        /// <summary>
        /// 去掉搜索的条件
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public static string DeleteSearch(HttpRequestBase rp)
        {
            string sr = "";
            int i = 0;
            foreach (string c in rp.QueryString.AllKeys)
            {
                if (c != "q")
                {
                    sr += "&" + c + "=" + rp.QueryString[c];
                    i++;
                }
            }
            string ret = "";
            switch (i)
            {
                case 0:
                    ret = string.Format("{0}", rp.Path);
                    break;
                case 1:
                    ret = string.Format("{0}?x{1}", rp.Path, sr);
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 标签选中样式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static string Selected(int id, int pos, string filter)
        {
            int[] urls = (from a in filter.Split('-') select int.Parse(a)).ToArray();
            if (urls[pos] == id)
            {
                return "class=s-box";
            }
            return "";
        }

        /// <summary>
        /// 标签内容不限
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static string Selected(int pos, string filter)
        {
            int[] urls = (from a in filter.Split('-') select int.Parse(a)).ToArray();
            if (urls[pos] == 0)
            {
                return "class=s-box";
            }
            return "";
        }

        public static string FilterComper(string x)
        {
            string s = x;
            //s = s.Replace("有限责任公司", "").Replace("股份有限公司", "").Replace("股份有限责任公司", "").Replace("有限公司", "");
            return s;
        }
    }
}
