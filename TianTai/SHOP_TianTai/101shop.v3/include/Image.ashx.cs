using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace _101shop.v3.include
{
    /// <summary>
    /// Image 的摘要说明
    /// </summary>
    public class Image : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            
            int UID = _101shop.v3.Controllers.BaseController.GetUserId();//账户ID
            if (UID > 0)
            {
                //调用ERP:wldwwdzl往来单位文档资料的图片
                string wldwwdid = context.Request.QueryString["wldwwdid"];
                if (!string.IsNullOrEmpty(wldwwdid))
                {
                    SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
                    object Data = bll.ExecuteScalar("SELECT wldwwd_image FROM wldwwdzl WHERE wldwwdid='" + wldwwdid.Replace("'", "") + "'");
                    if (Data != null)
                    {
                        byte[] imageData = (byte[])Data;
                        //写入图片信息到输出流中
                        context.Response.OutputStream.Write(imageData, 78, imageData.Length - 78);
                    }
                }
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