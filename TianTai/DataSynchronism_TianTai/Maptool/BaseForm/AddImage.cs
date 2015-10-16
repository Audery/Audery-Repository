using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Maptool.DrugsBaseService;

namespace Maptool.BaseForm
{
    public partial class AddImage : Form
    {
        private string _idenName;
        string sqlext = "";
        DrugsBaseSoapClient db = new DrugsBaseSoapClient();
        public DataRow imageDr { get; set; }
        public bool isSee { get; set; }
        List<string> images1 = new List<string>();
        List<string> images2 = new List<string>();
        List<string> images3 = new List<string>();
        public AddImage(string idenName)
        {
            InitializeComponent();
            this._idenName = idenName;
            dataGridViewX1.AutoGenerateColumns = false;
            BinType = 2;
            PageSize = 20;
            PageIndex = 1;
            order = false;
            orderField = "id";
            like = true;
            whereField = "DrugsBase_DrugName";
            whereString = null;
        }

        private void Bind()
        {
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();

            var li = bll.GetShopList(20, 1, false, "product_id", false, string.Empty, sqlext, 2, Login.authKey, this.iden);
            if (li != null && li.Tables[0].Rows.Count > 0)
            {
                var tempDT = li.Tables[0];
                dataGridViewX1.DataSource = tempDT;
                for (int i = 0; i < tempDT.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(tempDT.Rows[i]["image"].ToString()))
                    {
                        dataGridViewX1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
            }

        }

        public int BinType { get; set; }
        /// <summary>
        /// 合作企业编号
        /// </summary>
        public int iden { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool order { get; set; }
        public string orderField { get; set; }
        public bool like { get; set; }
        public string whereField { get; set; }
        public string whereString { get; set; }
        public int recordCount { get; set; }
        public int pageCount { get; set; }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            sqlext = "";
            if (textBoxX1.Text != "")
            {
                sqlext = " and DrugsBase_DrugName like('%" + textBoxX1.Text.Trim() + "%')";
            }
            if (textBoxX3.Text != "")
            {
                sqlext += " and DrugsBase_ApprovalNumber like('%" + textBoxX3.Text.Trim() + "%')";
            }
            if (textBoxX4.Text != "")
            {
                sqlext += " and DrugsBase_Manufacturer like('%" + textBoxX4.Text.Trim() + "%')";
            }
            if (textBoxX2.Text != "")
            {
                sqlext += " and DrugsBase_Specification like('%" + textBoxX2.Text.Trim() + "%')";
            }
            if (textBoxX5.Text != "")
            {
                sqlext += " and Product_ID ='" + textBoxX5.Text.Trim() + "'";
            }
            if (string.IsNullOrEmpty(sqlext))
            {
                MessageBox.Show("请输入查询条件");
            }
            else
            {
                Bind();
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns["yinyong"].Index)
            {
                int index = e.RowIndex;
                //string yinyong = getString("yinyong",index);
                string tym = getString("tym", index);
                string gg = getString("gg", index);
                string jx = getString("jx", index);
                string pzwh = getString("pzwh", index);
                //string dj = getString("dj", index);
                //string zf = getString("zf", index);
                //string px = getString("px", index);
                //string cd = getString("cd", index);
                string zhbid = getString("zhbid", index);
                string bzdw = getString("bzdw", index);
                string cj = getString("cj", index);
                string zhbsm = getString("zhbsm", index);

                this.Y1.Text = tym;
                this.Y2.Text = gg;
                this.Y3.Text = jx;
                this.Y4.Text = cj;
                this.Y5.Text = bzdw;
                this.Y6.Text = pzwh;
                this.Y7.Text = zhbid;
                this.labelX14.Text = zhbsm;
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
                this.listBox3.Items.Clear();
                images1.Clear();
                images2.Clear();
                images3.Clear();
            }
        }
        private string getString(string name, int index)
        {
            return dataGridViewX1.Rows[index].Cells[name].Value.ToString();
        }
        #region 选择图片事件
        private void buttonX1_Click(object sender, EventArgs e)
        {
            addUpload(listBox1);
        }


        private void buttonX2_Click(object sender, EventArgs e)
        {
            addUpload(listBox2);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            addUpload(listBox3);
        }
        #endregion 

        //定义一个文件上传函数
        private string upLoad(string fileName, string url)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var webclient = new WebClient();
            //webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] buffer = webclient.UploadFile(url, null, fileName);
            var msg = Encoding.UTF8.GetString(buffer);
            return msg.ToString();
        }

        private void addUpload(ListBox listbox)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(fileDialog.FileName).ToLower();
                string[] str = new string[] { ".jpeg", ".jpg" };
                if (!str.Contains(extension))
                {
                    MessageBox.Show("仅能上传jpeg，jpg格式的图片！");
                }
                else
                {
                    listbox.Items.Add(fileDialog.FileName);
                }
            }
        }


        private void Y7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Y7.Text))
            {
                this.buttonX1.Enabled = true;
                this.buttonX2.Enabled = true;
                this.buttonX3.Enabled = true;
            }
            else
            {
                this.buttonX1.Enabled = false;
                this.buttonX2.Enabled = false;
                this.buttonX3.Enabled = false;
            }
        }
        /// <summary>
        /// 保存图片数据
        /// </summary>
        /// <param name="image"></param>
        /// <param name="category"></param>
        private void saveData(string image, int category)
        {
            DrugsImageApply dia = new DrugsImageApply();
            dia.Goods_ID = int.Parse(Y7.Text);
            dia.source = _idenName;
            dia.sourceName = Login.userName;
            dia.Image = image;
            dia.category = category;
            dia.status = "未提交";
            dia.created = DateTime.Now;
            dia.updated = DateTime.Now;
            new DrugsBaseSoapClient().AddDrugsImageApply(dia, AddDrugBase.key);
        }

        /// <summary>
        /// 初始化加载图片(主要是列表页面过来的查看，修改)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImage_Load(object sender, EventArgs e)
        {
            if (imageDr != null)
            {
                this.Y1.Text = imageDr["DrugsBase_DrugName"].ToString();
                this.Y2.Text = imageDr["DrugsBase_Specification"].ToString();
                this.Y3.Text = imageDr["DrugsBase_Formulation"].ToString();
                this.Y4.Text = imageDr["DrugsBase_Manufacturer"].ToString();
                this.Y5.Text = imageDr["Goods_Unit"].ToString();
                this.Y6.Text = imageDr["DrugsBase_ApprovalNumber"].ToString();
                this.Y7.Text = imageDr["Goods_ID"].ToString();
                groupBox4.Visible = false;
                string gid = imageDr["Goods_ID"].ToString();
                setImage(listBox1, 1, gid);
                setImage(listBox2, 2, gid);
                setImage(listBox3, 3, gid);
                this.buttonX1.Enabled = false;
                this.buttonX2.Enabled = false;
                this.buttonX3.Enabled = false;
                this.buttonX5.Enabled = false;
                this.buttonX6.Enabled = false;
                this.buttonX7.Enabled = false;
                switch ((int)imageDr["category"])
                {
                    case 1:
                        this.buttonX1.Enabled = true;
                        this.buttonX5.Enabled = true;
                        break;
                    case 2:
                        this.buttonX2.Enabled = true;
                        this.buttonX6.Enabled = true;
                        break;
                    case 3:
                        this.buttonX3.Enabled = true;
                        this.buttonX7.Enabled = true;
                        break;
                }
                if (isSee)
                {
                    this.buttonX1.Enabled = false;
                    this.buttonX2.Enabled = false;
                    this.buttonX3.Enabled = false;
                    this.buttonX5.Enabled = false;
                    this.buttonX6.Enabled = false;
                    this.buttonX7.Enabled = false;
                }
            }
        }

        private void setImage(ListBox listbox, int type, string gid)
        {
            var li = db.GetListArray(string.Format(" and Goods_ID={0}", gid), type, _idenName, AddDrugBase.key);
            foreach (DrugsImageApply im in li)
            {               
                listbox.Items.Add(im.Image);
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show("DEL");
                if (listBox1.Items.Count > 0)
                {
                    if (listBox1.SelectedItem.ToString().IndexOf('\\') == -1)
                    {
                        images1.Add(listBox1.SelectedItem.ToString());
                    }
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            }
        }

        private void showPicture(string file)
        {
            if (file.IndexOf('\\') == -1)
            {
                var webclient = new WebClient();
                byte[] buffer = webclient.DownloadData(Properties.Settings.Default["upload"].ToString() + "?filename=" + file);
                var msg = Encoding.UTF8.GetString(buffer);
                file = msg.ToString();
            }
            frmShowGoodsImage frmTmp = new frmShowGoodsImage(file);
            frmTmp.Show(this);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string file = listBox1.Items[index].ToString();
                showPicture(file);
            }
        }

        private void listBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show("DEL");
                if (listBox2.Items.Count > 0)
                {
                    if (listBox2.SelectedItem.ToString().IndexOf('\\') == -1)
                    {
                        images2.Add(listBox2.SelectedItem.ToString());
                    }
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string file = listBox2.Items[index].ToString();
                showPicture(file);
            }
        }

        private void listBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show("DEL");
                if (listBox3.Items.Count > 0)
                {
                    if (listBox3.SelectedItem.ToString().IndexOf('\\') == -1)
                    {
                        images3.Add(listBox3.SelectedItem.ToString());
                    }
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                }
            }
        }

        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox3.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string file = listBox3.Items[index].ToString();
                showPicture(file);
            }
        }

        #region 上传图片
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count==0)
            {
                MessageBox.Show("请先选择图片！");
                return;
            }

            //提交要上传的图片
            foreach (string filename in listBox1.Items)
            {
                if (filename.IndexOf('\\') > -1)
                {
                    uploadSave(filename, 1);
                }
            }
            //删除已经上传的图片
            if (images1.Count > 0)
            {
                db.DeleteDrugsImageApply(images1.ToArray(), AddDrugBase.key);
            }

            string gid = Y7.Text;
            listBox1.Items.Clear();
            setImage(listBox1, 1, gid);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("请先选择图片！");
                return;
            }
            //提交要上传的图片
            foreach (string filename in listBox2.Items)
            {
                if (filename.IndexOf('\\') > -1)
                {
                    uploadSave(filename, 2);
                }
            }
            //删除已经上传的图片
            if (images2.Count > 0)
            {
                db.DeleteDrugsImageApply(images2.ToArray(), AddDrugBase.key);
            }

            string gid = Y7.Text;
            listBox2.Items.Clear();
            setImage(listBox2, 2, gid);
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (listBox3.Items.Count == 0)
            {
                MessageBox.Show("请先选择图片！");
                return;
            }
            //提交要上传的图片
            foreach (string filename in listBox3.Items)
            {
                if (filename.IndexOf('\\') > -1)
                {
                    uploadSave(filename, 3);
                }
            }
            //删除已经上传的图片
            if (images3.Count > 0)
            {
                db.DeleteDrugsImageApply(images3.ToArray(), AddDrugBase.key);
            }

            string gid = Y7.Text;
            listBox3.Items.Clear();
            setImage(listBox3, 3, gid);
        }

        /// <summary>
        /// 保存图片到文件服务器
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="category"></param>
        private void uploadSave(string filename, int category)
        {
            FileInfo fileInfo = new FileInfo(filename);
            string newFile = upLoad(filename, Properties.Settings.Default["upload"].ToString());
            //listbox.Items.Add(newFile);
            if (newFile.IndexOf("上传文件") == -1)
            {
                saveData(newFile, category);
            }
            else
            {
                MessageBox.Show(newFile);
            }

        }



        #endregion

        private void dataGridViewX1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns["tym"].Index)
            {
                int goodsPackageId = 0;
                int goodsId = 0;
                if (int.TryParse(dataGridViewX1.Rows[e.RowIndex].Cells["Column1"].Value.ToString(), out goodsPackageId)
                   && int.TryParse(dataGridViewX1.Rows[e.RowIndex].Cells["zhbid"].Value.ToString(), out goodsId))
                {
                    soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                    string imgUrl = bll.GetImageUrl(goodsId, goodsPackageId);

                    string frmCaption = dataGridViewX1.Rows[e.RowIndex].Cells["tym"].Value.ToString()
                                        + "—"
                                        + dataGridViewX1.Rows[e.RowIndex].Cells["gg"].Value.ToString()
                                        + "—"
                                        + dataGridViewX1.Rows[e.RowIndex].Cells["cj"].Value.ToString();

                    frmShowGoodsImage frmTmp = new frmShowGoodsImage(imgUrl);
                    frmTmp.Text = frmCaption;
                    frmTmp.Show(this);
                }
                else
                {
                    MessageBox.Show("无有效的物品编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
