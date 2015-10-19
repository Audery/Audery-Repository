using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _101shop.admin.v3.systeminfo
{
    public partial class site_sysinfo : System.Web.UI.Page
    {
        /// <summary>
        /// 公用系统参数
        /// </summary>
        public SOSOshop.Model.AdminInfo adminInfo = null;

        #region 统计时间(毫秒)
        public DateTime _StatisticalTime = DateTime.Now;//开始时间
        public int[] StatisticalTime = new int[3];//初始化,加载数据,呈现页面
        protected override void OnPreLoad(EventArgs e)
        {
            StatisticalTime[0] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds);
            base.OnPreLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            StatisticalTime[1] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[0];
            base.OnPreRender(e);
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            StatisticalTime[2] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[1];
            base.OnPreRenderComplete(e);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            adminInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
            if (!IsPostBack)
            {
                DateTime timeBegin = DateTime.Now;
                //获取订单统计信息
                GetOrderInfo();
                //获取商品统计信息
                GetGoodsInfo();
                //获取药品统计信息
                GetDrugsInfo();
                DateTime timeEnd = DateTime.Now;
                if (SOSOshop.BLL.AdministrorManager.GetJGManager()/*监管*/)
                {
                   // this.order1.Visible = false;
                    this.order2.Visible = false;
                }
            }
        }

        private string fontKey(int key)
        {
            if (key < 100)
            {
                return "<font color=Red size=2>" + key + "</font>";
            }
            else if (key < 500)
            {
                return "<font color=Fuchsia size=2>" + key + "</font>";
            }
            else if (key < 1000)
            {
                return "<font color=Green size=2>" + key + "</font>";
            }
            else
            {
                return "<font color=Purple size=2>" + key + "</font>";
            }
        }
        /// <summary>
        /// 未审核订单等订单统计信息
        /// </summary>
        private void GetOrderInfo()
        {
            ltlNotconfirmtheorder.Text = string.Format("<font color='Red' size='4' style=\"width:70px\">{0}</font>",
             (new SOSOshop.BLL.Db().ExecuteScalar("select count(1) from orders where (OrderStatus=3 or OrderStatus=1)") ?? 0));
        }
        /// <summary>
        /// 商品标签 ltlProductTags
        /// </summary>
        private void GetGoodsInfo()
        {
           
        }
        /// <summary>
        /// 基础产品 lbl_Goods_Count
        /// </summary>
        private void GetDrugsInfo()
        {
            //统计基础数据信息（包括包装盒、彩页等）
            //SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();            
            //object obj1 = db.ExecuteScalar("SELECT COUNT(*) FROM Goods_Image");
            //object obj2 = db.ExecuteScalar("SELECT COUNT(*) FROM (SELECT DISTINCT Goods_ID,Goods_Package_ID FROM dbo.Goods_Image)a ");
            //object obj22 = db.ExecuteScalar("SELECT COUNT(*) FROM Goods_Image WHERE datediff(month,UpdateDate,getdate())=0");
            //object obj23 = db.ExecuteScalar("SELECT COUNT(*) FROM Goods_Image WHERE datediff(month,UpdateDate,getdate())=1");
            //object obj24 = db.ExecuteScalar(" SELECT COUNT(*) FROM  (SELECT DISTINCT Goods_ID FROM dbo.Goods_Image WHERE UpdateDate>'"+DateTime.Now.ToString("yyyy-MM-dd")+"')a");
            //this.lbl_Goods_Count.Text += "<span class=tj>包装盒<b class=tj2>" + obj2 + "</b>/<b class=tj3>" + obj1
            //    + "</b>个 [本月新增<b class=tj2>" + obj22 + "</b>个,上月新增<b class=tj2>" + obj23 + "</b>个,] [今日新增加<b class=tj2>" + obj24 + "</b>个]</span>";
            //object obj3 = db.ExecuteScalar("SELECT COUNT(*) FROM Goods_Picture");
            //object obj4 = db.ExecuteScalar("SELECT COUNT(*) FROM (SELECT DISTINCT Goods_ID,Goods_Package_ID FROM Goods_Picture)a");
            //object obj44 = db.ExecuteScalar("SELECT COUNT(*) FROM (SELECT DISTINCT Goods_ID,Goods_Package_ID FROM Goods_Picture WHERE datediff(month,UpdateDate,getdate())=0)a");
            //object obj45 = db.ExecuteScalar("SELECT COUNT(*) FROM (SELECT DISTINCT Goods_ID,Goods_Package_ID FROM Goods_Picture WHERE datediff(month,UpdateDate,getdate())=1)a");
            //object obj46 = db.ExecuteScalar("SELECT COUNT(*) FROM dbo.Product WHERE datepart(day,getdate())=datepart(day,created) and datepart(month,getdate())=datepart(month,created) and datepart(year,getdate())=datepart(year,created)");
            //object obj47 = db.ExecuteScalar("SELECT COUNT(*) FROM dbo.Product WHERE datepart(month,getdate())=datepart(month,created) and datepart(year,getdate())=datepart(year,created)");
            //this.lbl_Goods_Count.Text += "<span class=tj>彩页数<b class=tj4>" + obj4 + "</b>/<b class=tj3>" + obj3
            //    + "</b>个 [本月新增<b class=tj4>" + obj44 + "</b>个,上月新增<b class=tj4>" + obj45 + "</b>个] </span>";
            //object obj5 = new SOSOshop.BLL.Db().ExecuteScalar("select count(1) from Product");
            //this.lbl_Goods_Count.Text += "<span class=tj>商品<b class=tj5>" + obj5 + "</b>个</span>";
            //this.lbl_Goods_Count.Text += "<span class=tj>今日新增<b class=tj5>" + obj46 + "</b>个,</span>";
            //this.lbl_Goods_Count.Text += "<span class=tj>本月新增<b class=tj5>" + obj47 + "</b>个</span>";
        }
        /// <summary>
        /// 取得挂网价等数据抓取及映射
        /// </summary>
        /// <returns></returns>
        public string GetCrawl()
        {
            SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();
            object 基 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.Crawl_Data_Goods_RegionBidPricing");
            object 基映 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.LinkRegionBidPricing");

            object 调 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.Crawl_Data_Goods_RegionLimitPricing");
            object 调映 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.LinkRegionLimitPricing");

            object 挂 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.Crawl_Data_Goods_RegionPricing");
            object 挂映 = db.ExecuteScalar("SELECT COUNT(*) FROM exchange.dbo.LinkRegionPricing");

            string str = string.Format("基(<span style='color:red'>{0}</span>/<span style='color:#6699FF'>{1}</span>)  调(<span style='color:red'>{2}</span>/<span style='color:#6699FF'>{3}</span>)  挂(<span style='color:red'>{4}</span>/<span style='color:#6699FF'>{5}</span>)", 基, 基映, 调, 调映, 挂, 挂映);

            return str;
        }

    }
}
