using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOSOshop.BLL.JF;
using SOSOshop.Model.JF;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text;

namespace _101shop.admin.v3.admin.JF
{
    public partial class Add : SOSOshop.WEB.UI.ManageBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("006006002", "对不起，你没有添加商品的权限");
                //ChangeHope.WebPage.WebControl.Validate(this.txtName, "栏目名称，栏目下面可以设置子栏目,栏目名称设置为4~10个字符", "isnull_4_10", "必填", "该项为必填项");
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {

        }
        public string CreatePasswordHash(string pwd, int saltLenght)
        {
            string strSalt = CreateSalt(saltLenght);
            //把密码和Salt连起来
            string saltAndPwd = String.Concat(pwd, strSalt);
            //对密码进行哈希
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            //转为小写字符并截取前16个字符串
            hashenPwd = hashenPwd.ToLower().Substring(0, 16);
            //返回哈希后的值
            return hashenPwd;
        }
        public string CreateSalt(int saltLenght)
        {
            //生成一个加密的随机数
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);
            //返回一个Base64随机数的字符串
            return Convert.ToBase64String(buff);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)     //判断上传数据是否为空
            {
                //bool FileIsValid=false;
                String fileExtension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();//获取文件类型
                String[] restrictExtension = { ".gif", ".jpg", ".bmp", ".png" };
                int length = txtName.Text.Trim().Length;
                int length1 = jf.Text.Trim().Length;
                if (length != 0)
                {
                    if (length1 != 0)
                    {
                        if (restrictExtension.Contains(fileExtension) == true)
                        {
                            string filePath = "/JFimages/";
                            string fileName = filePath + CreatePasswordHash(FileUpload1.FileName, 4) + fileExtension;
                            string mappath = Server.MapPath(fileName);
                            FileUpload1.PostedFile.SaveAs(mappath);      //转换存储到服务器上的物理路径
                            SOSOshop.Model.JF.Jf_Model mdoel = new SOSOshop.Model.JF.Jf_Model();
                            mdoel.name = this.txtName.Text.Trim();
                            mdoel.jf = Convert.ToInt32(this.jf.Text.Trim());
                            mdoel.pt = this.pt.Text.Trim();
                            mdoel.img = fileName;
                            SOSOshop.BLL.JF.Jf bll = new SOSOshop.BLL.JF.Jf();
                            bll.Add(mdoel);

                            Response.Write("<script>alert('文件上传成功');window.location.href='Add.aspx';</script>");
                            //Response.Redirect("js_list.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('请选择正确的图片格式');window.location.href='Add.aspx';</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('请输入商品积分');window.location.href='Add.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('请输入商品名称');window.location.href='Add.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('请选择图片')</script>");
            }
        }


        public override void SetModuleTag()
        {
           
        }
    }
}
