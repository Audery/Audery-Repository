using SOSOshop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace _101shop.admin.v3.admin.cuxiao
{
    /// <summary>
    /// SetCuxiao 的摘要说明
    /// </summary>
    public class SetCuxiao : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string id = context.Request["pid"];
            string cuprice = context.Request["cuprice"];
            string discount = context.Request["discount"];
            string btime = context.Request["btime"];
            string etime = context.Request["etime"];
            string maxsell = context.Request["maxsell"];
            string minsell = context.Request["minsell"];
            string otcminsell = context.Request["otcminsell"];
            string canel = context.Request["canel"];
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    //用户操作权限审核                    
                    if (SOSOshop.BLL.PowerPass.isPass("001030000"))
                    {
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        //设置促销
                        string sql = string.Format("update product set cuprice={0},discount={1},maxsell={2},begindate='{3}',enddate='{4}',otcminsell={6},minsell={7} where Product_ID={5}", cuprice, discount, maxsell, btime, etime, id, otcminsell, minsell);
                        //取消促销设置
                        if (!string.IsNullOrEmpty(canel))
                        {
                            sql = string.Format("update product set cuprice=0,discount=0,maxsell=0,minsell=0,otcminsell=0,begindate=null,enddate=null where Product_ID={0}", id);
                        }   
                        int result = db.ExecuteNonQuery(sql);
                        AdminInfo adminModel = (AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                        SOSOshop.BLL.Logs.Log.LogAdminAdd(string.Format("设置了促销商品:[{0}][{1}]", id, new SOSOshop.BLL.Db().ExecuteScalar("SELECT Product_Name FROM dbo.Product WHERE Product_ID=" + id), string.IsNullOrEmpty(canel) ? "促销" : "取消促销"), adminModel.AdminId, adminModel.AdminName, 1);
                        //大表同步更新
                       // sql = "";

                        context.Response.Write("{\"state\":" + result + ",\"message\":\"设置完成\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"state\":-1,\"message\":\"对不起！您没有操作权限。\"}");
                    }
                }
                catch (Exception x)
                {
                    context.Response.Write("{\"state\":-1,\"message\":\"" + x.Message + "\"}");
                }

            }
            else
            {
                context.Response.Write("{\"state\":-1,\"message\":\"操作错误！\"}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}