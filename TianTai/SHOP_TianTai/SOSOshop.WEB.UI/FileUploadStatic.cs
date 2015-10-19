using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
namespace SOSOshop.WEB.UI
{
    public static class FileUploadStatic
    {
        /// <summary>
        /// 判断文件后缀名是否合法
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="Ext">允许上传文件后缀名以,号分隔要加.</param>
        /// <returns></returns>
        public static bool IsExtension(this FileUpload fu, string Ext)
        {
            if (fu.HasFile)
            {
                string temp = System.IO.Path.GetExtension(fu.FileName);
                foreach (var item in Ext.Split(','))
                {
                    if (item == temp)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="ext">允许上传文件后缀名以,号分隔要加.</param>
        /// <param name="path">要上传的路径（相对路径）</param>
        public static string Uploading(this FileUpload fu, string path)
        {
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
            }
            string temp = System.IO.Path.GetExtension(fu.FileName);
            string fileName =MongoDB.Bson.BsonObjectId.GenerateNewId().ToString() + temp;
            string filePath = path + "/" + fileName;
            fu.SaveAs(System.Web.HttpContext.Current.Server.MapPath(filePath));
            return filePath;
        }
    }
}
