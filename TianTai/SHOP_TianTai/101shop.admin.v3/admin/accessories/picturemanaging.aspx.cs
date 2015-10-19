using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace _101shop.admin.v3.accessories
{
    public partial class picturemanaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("012010000");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty)
                {
                    string type = ChangeHope.WebPage.PageRequest.GetFormString("Option");
                    switch (type)
                    {
                        case "delFile":
                            if (SOSOshop.BLL.PowerPass.isPass("012010003"))
                            {
                                string filepath = ChangeHope.WebPage.PageRequest.GetFormString("path");
                                delFile(filepath);
                            }
                            else
                            {
                                Response.Write("no");
                            }
                            break;
                        default:
                            break;
                    }
                    Response.End();
                }
                string name = "\\yxuploadfile";
                if (ChangeHope.WebPage.PageRequest.GetQueryString("name") != string.Empty)
                {
                    name = ChangeHope.WebPage.PageRequest.GetQueryString("name");
                }

                Get(name);
            }
        }

        #region 显示文件目录
        /// <summary>
        /// 显示文件目录
        /// </summary>
        public void Get(string name)
        {
            string str_lujing = "~" + name;
            if (name == "\\yxuploadfile")
            {
                this.hlback.Visible = false;
            }
            if (!Directory.Exists(Server.MapPath(str_lujing)))
            {
                Directory.CreateDirectory(Server.MapPath(str_lujing));
            }
            this.lbpath.Text = name;
            this.hfpath.Value = name;
            DirectoryInfo[] ChildDirectory;                         //子目录集
            FileInfo[] NewFileInfo;                                //当前所有文件
            DirectoryInfo FatherDirectory = new DirectoryInfo(Server.MapPath(str_lujing)); //当前目录
            ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集
            NewFileInfo = FatherDirectory.GetFiles();               //得到文件集，可以进行操作
            string str_html = "<table width=\"100%\" id=\"tablist\"  cellspacing=\"0\" cellpadding=\"0\" border=\"1\" bordercolorlight=\"#DDDDDD\""
            + "class=\"datatable\" bordercolordark=\"#FFFFFF\"><tr class='TrTitle' bgcolor='#78B9E6' style='color: White' height='25'><th width='30%'>文件名称</th><th width='10%'>类型</th><th width='20%'>大小</th>"
            + "<th width='20%'>最后修改时间</th><th width='20%'>操作</th></tr>";

            foreach (DirectoryInfo di in ChildDirectory)//显示文件夹
            {
                string strH = "<tr height='25'><td><img src='../images/img/imgfolder.gif' /><a href='picturemanaging.aspx?name=" + name + "\\" + di.Name + "'>" + di.Name
                    + "</a></td><td>文件夹</td><td  align='center'>...</td><td  align='center'>" + di.LastWriteTimeUtc + "</td><td align='center'><a href='picturemanaging.aspx?name=" + name + "\\" + di.Name + "'>查看详细</a></td></tr>";
                str_html += strH;
            }
            foreach (FileInfo fi in NewFileInfo)//显示文件
            {
                string extension = Path.GetExtension(fi.Name);
                string pic = "";
                if (CheckValidExt(extension))
                {
                    string ph = (name + "\\" + fi.Name);
                    System.Drawing.Image ig = System.Drawing.Image.FromFile(Server.MapPath("~\\" + ph));

                    if (ig.Width <= 100)
                    {
                        if (ig.Height <= 100)
                        {
                            pic = "<img alt=" + fi.Name + "  src='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' />";
                        }
                        else
                        {
                            pic = "<img alt=" + fi.Name + " height=100  src='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' />";

                        }
                    }
                    else
                    {
                        if (ig.Height <= 100)
                        {
                            pic = "<img alt=" + fi.Name + "  width=100 src='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' />";
                        }
                        else
                        {
                            pic = "<img alt=" + fi.Name + "  height=100 width=100  src='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' />";

                        }
                    }
                    string strh1 = "<tr><td><a href='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' target='_blank'>" + pic + "</a></td><td>" + fi.Extension + "文件</td><td>" + fi.Length + "字节</td><td>"
                    + fi.LastWriteTimeUtc + "</td><td><a href='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' target='_blank'>预览</a>&nbsp;<a href=\"javascript:DelFile('" + ph.Replace("\\", "\\\\") + "')\">删除</a></td></tr>";
                    str_html += strh1;
                }
                else
                {
                    string ph = fi.Name;
                    string strh1 = "<tr><td><a href='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' target='_blank'>" + pic + "</a></td><td>" + fi.Extension + "文件</td><td>" + fi.Length + "字节</td><td>"
                    + fi.LastWriteTimeUtc + "</td><td><a href='../../" + str_lujing.Replace("~\\", "") + "\\" + fi.Name + "' target='_blank'>预览</a>&nbsp;<a href=\"javascript:DelFile('" + ph.Replace("\\", "\\\\") + "')\">删除</a></td></tr>";
                    str_html += strh1;
                }
            }
            str_html += "</table>";
            this.Literal1.Text = str_html;
        }
        #endregion

        /// <summary>
        /// 检测扩展名的有效性
        /// </summary>
        /// <param name="sExt">文件名扩展名</param>
        /// <returns>如果扩展名有效,返回true,否则返回false.</returns>
        private bool CheckValidExt(string sExt)
        {
            string AllowExt = ".jpe|.jpeg|.jpg|.gif|.png|.tif|.tiff|.bmp";
            bool flag = false;
            string[] aExt = AllowExt.Split('|');
            foreach (string filetype in aExt)
            {
                if (filetype.ToLower() == sExt)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }



        #region 删除文件
        protected void delFile(string opath)
        {
            ChangeHope.Common.FileHelper fh = new ChangeHope.Common.FileHelper();
            if (fh.DeleteFile(Server.MapPath("~" + opath)))
            {
                Response.Write("ok");
            }
            else
            {
                Response.Write("删除失败！");
            }
        }
        #endregion




    }
}
