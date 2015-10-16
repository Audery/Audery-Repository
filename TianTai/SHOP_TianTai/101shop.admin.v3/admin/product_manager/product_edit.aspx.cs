using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SOSOshop.Model;
using System.Data.Common;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class product_edit : System.Web.UI.Page
    {
        public Productinfo product;
        public ShopInfo shop;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SOSOshop.BLL.PowerPass.isPass("001009007")) SOSOshop.BLL.PromptInfo.Popedom("001009001");
            if (!IsPostBack)
            {
                ViewState["returnUrl"] = Request.UrlReferrer;
                HyperLink1.NavigateUrl = Request.UrlReferrer + "";

                bool edit = SOSOshop.BLL.PowerPass.isPass("001009004");
                this.TextBox2.Enabled = edit;
                this.TextBox3.Enabled = edit;
                this.button2.Enabled = edit;
                this.button3.Enabled = edit;
            }

            string sql = string.Format("select *,(select top 1 image from Goods_Image where Goods_ID=product.Goods_ID) as images from product where product_id={0}", Request["pid"]);
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            //Response.Write(sql);
            //Response.End();
            product = new Productinfo(db.ExecuteTable(sql).Rows[0]);            
            //商品信息

            string sqlshop = string.Format("select *," +
                "(select b.is_cl from product_online_v_1 as b where b.Product_ID= {0}) as is_cl," +//可拆零
                "(select b.Price_01 from product_online_v_1 as b where b.Product_ID= {0}) as price_01," +
                "(select b.Price_02 from product_online_v_1 as b where b.Product_ID= {0}) as price_02," +
                "isnull((select b.minsell from product_online_v_1 as b where b.Product_ID= {0}),0) as minsell,Product_State," +
                "case when(select [Stock] from [Product_Stock] as d where d.Product_ID={0}) is null then 0 else 1 end as stock " +
                "from product where product_id={0}", Request["pid"]);
            var dt = db.ExecuteTable(sqlshop);
            shop = new ShopInfo(dt.Rows[0]);
            if (!IsPostBack)
            {
                this.txtId.Value = shop.ID.ToString();
                TextBox1.Text = shop.ProductName;
                TextBox2.Text = shop.SellingPoint;
                TextBox3.Text = shop.Advertisement;
                DropDowndrug_sensitive1.SelectedValue = dt.Rows[0]["drug_sensitive"].ToString();
                Label1.Text = string.Format("{0:f2}", shop.price_01);
                Label2.Text = string.Format("{0:f2}", shop.price_02);
                this.DropDownSellType.SelectedValue = shop.sellType.ToString();
            }
        }

        #region request
        string request(string name)
        {
            string value = string.Empty;
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                string k = Request.QueryString.Keys[i];
                if (k.Contains(name))
                {
                    value = Request.QueryString[k];
                    break;
                }
            }
            for (int i = 0; i < Request.Form.Count; i++)
            {
                string k = Request.Form.Keys[i];
                if (k.Contains(name))
                {
                    value = Request.Form[k];
                    break;
                }
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Product_ID = 0; int.TryParse(request("txtId"), out Product_ID);
            string Product_Name = request("TextBox1");
            string SellingPoint = request("TextBox2");
            string Advertisement = request("TextBox3");

            string sql = "update product set Product_Name=@Product_Name, Product_SellingPoint=@Product_SellingPoint, Product_Advertisement=@Product_Advertisement,drug_sensitive=@drug_sensitive,SellType=@SellType where Product_ID=@Product_ID";
            try
            {
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                DbCommand dbCommand = db._db.GetSqlStringCommand(sql);
                db._db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, Product_ID);
                db._db.AddInParameter(dbCommand, "Product_Name", DbType.AnsiString, Product_Name);
                db._db.AddInParameter(dbCommand, "Product_SellingPoint", DbType.AnsiString, SellingPoint);
                db._db.AddInParameter(dbCommand, "Product_Advertisement", DbType.AnsiString, Advertisement);
                db._db.AddInParameter(dbCommand, "drug_sensitive", DbType.Boolean, bool.Parse(DropDowndrug_sensitive1.SelectedValue));
                db._db.AddInParameter(dbCommand, "SellType", DbType.Int32, int.Parse(DropDownSellType.SelectedValue));
                int ret = db._db.ExecuteNonQuery(dbCommand);
                if (ret > 0)
                {
                    AdminInfo adminModel = (AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd(string.Format("编辑商品:[{0}][{1}]", Product_ID, TextBox1.Text), adminModel.AdminId, adminModel.AdminName, 1);

                    this.ltlMsg.Text = "保存成功·<script>if(confirm('编辑成功！继续编辑请点击确定。')){location.href='product_edit.aspx?pid=" + Product_ID + "';}else{location.href='" + ViewState["returnUrl"] + "';}</script>";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionOk";
                }
                else
                {
                    this.ltlMsg.Text = "保存失败";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                }
            }
            catch (Exception ex)
            {
                this.ltlMsg.Text = "保存失败" + "\r\n" + ex.ToString();
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
            }
        }

    }

    /// <summary>
    /// 药品信息
    /// </summary>
    public class Productinfo
    {
        public Productinfo(DataRow dr)
        {
            DrugName = dr["DrugsBase_DrugName"].ToString();
            ProName = dr["DrugsBase_ProName"].ToString();
            Specification = dr["DrugsBase_Specification"].ToString();
            Formulation = dr["DrugsBase_Formulation"].ToString();
            Manufacturer = dr["DrugsBase_Manufacturer"].ToString();
            if (string.IsNullOrEmpty(dr["images"].ToString()))
            {
                Image = System.Configuration.ConfigurationManager.AppSettings["imageUrl"] + "images/nopic1.jpg";
            }
            else
            {
                Image = SOSOshop.BLL.Common.Public.RawImage(dr["images"].ToString());
            }
            if (dr["DrugsBase_ApprovalNumber"] != null)
            {
                ApprovalNumber = dr["DrugsBase_ApprovalNumber"].ToString();
            }
            else if (dr["Registration_No"] != null)
            {
                ApprovalNumber = dr["Registration_No"].ToString();
            }
            ConveRatio = dr["Goods_ConveRatio"].ToString();
            Goods_Pcs = int.Parse(dr["Goods_Pcs"].ToString());
            Goods_Pcs_Small = int.Parse(dr["Goods_Pcs_Small"].ToString());
            Goods_Unit = dr["Goods_Unit"].ToString();

        }



        /// <summary>
        /// 通用名
        /// </summary>
        public string DrugName { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string Formulation { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 包装盒
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 批准文号
        /// </summary>
        public string ApprovalNumber { get; set; }

        /// <summary>
        /// 转换比
        /// </summary>
        public string ConveRatio { get; set; }

        /// <summary>
        /// 件装量
        /// </summary>
        public int Goods_Pcs { get; set; }
        /// <summary>
        /// 中包装
        /// </summary>
        public int Goods_Pcs_Small { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string Goods_Unit { get; set; }

    }

    /// <summary>
    /// 商品信息
    /// </summary>
    public class ShopInfo
    {
        public ShopInfo(DataRow dr)
        {
            ID = dr["Product_ID"].ToString();
            ProductName = dr["Product_Name"].ToString();
            SellingPoint = dr["Product_SellingPoint"].ToString();
            Advertisement = dr["Product_Advertisement"].ToString();
            isShelves = (int)dr["Product_bShelves"];
            isStock = (int)dr["stock"];
            is_cl = Convert.ToString(dr["is_cl"]).Trim() == "是";
            decimal price1 = 0;
            decimal.TryParse(dr["Price_01"].ToString(), out price1);
            decimal price2 = 0;
            decimal.TryParse(dr["Price_02"].ToString(), out price2);
            price_01 = price1;
            price_02 = price2;
            MinSell = (int)dr["minsell"];
            ProductState = dr["Product_State"].ToString();
            sellType = (int)dr["sellType"];
        }

        public string ID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 卖点
        /// </summary>
        public string SellingPoint { get; set; }

        /// <summary>
        /// 广告语
        /// </summary>
        public string Advertisement { get; set; }

        /// <summary>
        /// 购买须知
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否停用
        /// </summary>
        public int isStop { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public int isShelves { get; set; }

        /// <summary>
        /// 是否缺货
        /// </summary>
        public int isStock { get; set; }

        /// <summary>
        /// 是否可拆零
        /// </summary>
        public bool is_cl { get; set; }
        /// <summary>
        /// 101批发价
        /// </summary>
        public decimal price_01 { get; set; }
        /// <summary>
        /// 101拆零价
        /// </summary>
        public decimal price_02 { get; set; }

        /// <summary>
        /// 最小购买数量
        /// </summary>
        public int MinSell { get; set; }

        /// <summary>
        /// 产品状态串
        /// </summary>
        public string ProductState { get; set; }

        /// <summary>
        /// 销售方式 1 不限制，2中包装，3整件
        /// </summary>
        public int sellType { get; set; }
    }
}