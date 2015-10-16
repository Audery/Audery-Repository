using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace _101shop.admin.v3.admin.member
{
    public partial class Buyer_LoginTimes : SOSOshop.WEB.UI.ManageBasePage
    {
        /// <summary>
        /// 查询商城数据库
        /// </summary>
        SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
        string columnNames = "* ";
        string tableNames = @"(SELECT * FROM memberloginlog a LEFT JOIN
                            (
                              SELECT ProId,addDate,loginId,Product.Product_Name
                              FROM dbo.memberbrowserproductcontentlog
	                               INNER JOIN Product ON ProId=Product.Product_ID 
                            )tb ON a.id=tb.loginId) AS T";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                SOSOshop.Model.MemberAccount a = null;
                int uid = 0; int.TryParse(Request["UID"], out uid);

                if (uid > 0)
                {
                    SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
                    SOSOshop.Model.MemberInfo model = new SOSOshop.BLL.MemberInfo().GetModel(uid);
                    object obj = bll.ExecuteScalar("select top(1) Name from DrugsBase_Enterprise where ID=" + model.ParentId);

                    a = bll.GetModel(uid);
                    hfmobilePhone.Value = a.MobilePhone;
                    hftrueName.Value = model.TrueName;
                    hfdefaultIncName.Value = obj != null ? obj.ToString() : "未设置";
                    hfmember_Class.Value = "无"; if (model.Member_Class >= 0) hfmember_Class.Value = Enum.GetName(typeof(SOSOshop.Model.MemberKeyValue.Member_Class), model.Member_Class);

                    #region 实例化省市区联动

                    DataSet dsProvinces = bll.ExecuteDataSet("select isnull((select TOP(1) Name from Region where ID=" + model.Province + "),'') as a,isnull((select TOP(1) Name from Region where ID=" + model.City + "),'') as b,isnull((select TOP(1) Name from Region where ID=" + model.Borough + "),'') as c");

                    if (dsProvinces != null && dsProvinces.Tables.Count > 0 && dsProvinces.Tables[0].Rows.Count > 0)
                    {
                        hfConsigneeProvince.Value = dsProvinces.Tables[0].Rows[0][0].ToString();
                        hfConsigneeCity.Value = dsProvinces.Tables[0].Rows[0][1].ToString();
                        hfConsigneeBorough.Value = dsProvinces.Tables[0].Rows[0][2].ToString();
                    }

                    #endregion
                }

                if (a != null)
                {
                    this.hfUID.Value = uid.ToString();
                    //显示列表
                    Search_Click(null, null);
                }
                else
                {
                    Response.Write("<center><br><h3>未知买家！</h3>"); Response.End();
                }
            }
        }

        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            int recordCount, pageCount;
            string strWhereExt = "UID=" + this.hfUID.Value;
            string tableName = "(SELECT " + columnNames + " FROM " + tableNames + ") AS T";
            AspNetPager1.PageSize = 5;

            tablist.DataSource = bll.GetList(tableName, strWhereExt, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex,
                order, orderField, like, whereField, whereString, out recordCount, out pageCount);
            AspNetPager1.RecordCount = recordCount;

            tablist.DataBind();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            this.whereField = "";
            this.orderField = "id";
            this.order = true;
            this.like = false;
            this.whereString = "";

            StartLoad(1, whereString);
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }

        public override void SetModuleTag()
        {
            base.ModuleBrowse = "008009001";
            base.ModuleAdd = "008009001";
            base.ModuleDelete = "008009001";
            base.ModuleEdit = "008009001";
        }
    }
}