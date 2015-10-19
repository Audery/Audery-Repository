using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 
namespace Maptool.BaseForm
{
    public partial class frmShowGoodsImage : Form
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; }

        public frmShowGoodsImage()
        {
            InitializeComponent();
        }

        public frmShowGoodsImage(string imageUrl)
        {
            InitializeComponent();
            this.ImageUrl = imageUrl;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowGoodsImage_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                picGoodsImage.ImageLocation = ImageUrl;
            }
        }
    }
}
