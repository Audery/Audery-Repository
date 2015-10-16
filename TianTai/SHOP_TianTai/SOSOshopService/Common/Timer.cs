using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace SOSOshopService.Common
{
    public class Timer
    {

        /// <summary>
        /// 第天晚上重新计算一下销售数据,
        /// </summary>
        public static void InitProduct_SaleNum()
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();

        }
        /// <summary>
        /// 重新计算一下锁库数量
        /// </summary>
        public static void InitStock_Lock()
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            bll.ExecuteNonQuery("UPDATE dbo.Stock_Lock SET Stock=dbo.fn_select_lockStock(Product_ID) WHERE Stock>0 AND Stock<>dbo.fn_select_lockStock(Product_ID)");
        }
        
        /// <summary>
        /// 重新计算是否有在出售的otc品种
        /// </summary>
        public static void InitProduct_otc()
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            DataSet ds1 = bll.ExecuteDataSet("select id from Tag_PharmAttribute AS tag where tag_id=71 order by id");
            SOSOshop.BLL.DrugsBase.Tag_PharmAttribute ta = new SOSOshop.BLL.DrugsBase.Tag_PharmAttribute();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                int tagid = (int)ds1.Tables[0].Rows[i][0];
                int count = ta.GetCount(tagid);
                string sql = string.Format(@"IF EXISTS(SELECT * FROM Tag_PharmAttribute_Product_Count WHERE Tag_PharmAttribute_id={1})
UPDATE Tag_PharmAttribute_Product_Count SET Product_Count={0} WHERE Tag_PharmAttribute_id={1}
ELSE
INSERT Tag_PharmAttribute_Product_Count (Tag_PharmAttribute_id,Product_Count) VALUES({1},{0})", count, tagid);
                bll.ExecuteNonQuery(sql);
            }

        }
       
       
    }
}
