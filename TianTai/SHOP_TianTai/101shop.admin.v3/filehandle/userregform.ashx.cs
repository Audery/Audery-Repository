using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
namespace _101shop.admin.v3.filehandle
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class userregform : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            #region
            //是否允许注册新用户
            if (!sp.IsRegistered)
            {
                ChangeHope.WebPage.Script.AlertAndGoBack("温馨提示：目前不允许新用户注册！");
            }
            else
            {
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty)
                {
                    context.Response.Clear();
                    string types = context.Request.Form["Option"].Trim();
                    if (types == "NewRegisteredMembers" && context.Session["DoNotTipForNewRegisteredMembers"] == null)
                    {
                        if (context.Request.Form["DoNotTip"] != null && context.Request.Form["DoNotTip"] == "Yes")
                        {
                            context.Session["DoNotTipForNewRegisteredMembers"] = true;
                            context.Response.End();
                            return;
                        }
                        string json = "";//获取这两天刚注册的未导入CRM的会员
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        DataSet ds = db.ExecuteDataSet("SELECT a.UID,a.UserId,a.Email,b.TrueName,b.MobilePhone,a.RegisterIP FROM yxs_memberaccount AS a INNER JOIN yxs_memberinfo AS b ON a.UID=b.UID WHERE datediff(DAY,RegisterDate,GETDATE())<=2 AND Member_Class=1 AND State=0 AND CRMID=0");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = ds.Tables[0].Rows[i];
                                json += "{id:" + dr["UID"] + ",name:\"" + Convert.ToString(dr["UserId"]).Replace("\"", "") +
                                    "\",email:\"" + Convert.ToString(dr["Email"]).Replace("\"", "") +
                                    "\",truename:\"" + Convert.ToString(dr["TrueName"]).Replace("\"", "") +
                                    "\",mobilephone:\"" + Convert.ToString(dr["MobilePhone"]).Replace("\"", "") +
                                    "\",location:\"" + ChangeHope.WebPage.PageRequest.GetIPLocation(Convert.ToString(dr["RegisterIP"])).Replace("\"", "") + "\"},";
                            }
                            if (json != "") json = json.Substring(0, json.Length - 1);
                            json = "{result:[" + json + "]}";
                        }
                        context.Response.Write(json);
                        context.Response.End();
                        return;
                    }
                    else if (types == "NewRegisteredMembersDebug")
                    {
                        string json = "";
                        for (int i = 1; i < 10; i++)
                        {
                            json += "{id:" + i + ",name:\"会员名" + i + "\"},";
                        }
                        if (json != "") json = json.Substring(0, json.Length - 1);
                        json = "{result:[" + json + "]}";
                        context.Response.Write(json);
                        context.Response.End();
                        return;
                    }
                }
            }
            #endregion

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
