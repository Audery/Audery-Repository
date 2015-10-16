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
using System.Web;
using Maptool.DrugsBaseService;

namespace Maptool.BaseForm
{
    public partial class AddNoDrug : DevComponents.DotNetBar.Office2007Form
    {
        public const string key = "01CA23A6-8CF7-4A7D-91D5-54B2C083D9AF";
        private string _idenName;
        DrugsBaseSoapClient db = new DrugsBaseSoapClient();
        public bool isSee { get; set; }
        List<string> images = new List<string>();
        public DrugsBaseApply model { get; set; }
        public AddNoDrug(string idenName)
        {
            InitializeComponent();
            this._idenName = idenName;
            dataGridViewX1.AutoGenerateColumns = false;
            this.comboCcfs.DataSource = db.GetDrugsBaseEnum(DrugsBaseEnum.存储条件);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string name = this.textBoxX2.Text;
            string gg = this.textBoxX3.Text;
            string jx = this.textBoxX4.Text;
            string pzwh = this.textBoxX5.Text;
            string sql = "";
            List<string> fileds = new List<string>();
            List<string> values = new List<string>();
            if (!string.IsNullOrEmpty(name))
            {
                sql = " And DrugsBase_DrugName like '%" + name + "%'";
                fileds.Add("DrugsBase_DrugName");
                values.Add(name);
            }
            if (!string.IsNullOrEmpty(gg))
            {
                sql += " And DrugsBase_Specification like'%" + gg + "%'";
                fileds.Add("DrugsBase_Specification");
                values.Add(gg);
            }
            if (!string.IsNullOrEmpty(jx))
            {
                sql += " And DrugsBase_Formulation like '%" + jx + "%'";
                fileds.Add("DrugsBase_Formulation");
                values.Add(jx);
            }
            if (!string.IsNullOrEmpty(pzwh))
            {
                sql += " And DrugsBase_ApprovalNumber like '%" + pzwh + "%'";
                fileds.Add("DrugsBase_ApprovalNumber");
                values.Add(pzwh);
            }
            if (!string.IsNullOrEmpty(textBoxX1.Text))
            {
                fileds.Add("DrugsBase_Manufacturer");
                values.Add(textBoxX1.Text);
            }
           
            DataSet ds = db.GetDrugsbaseAllList(20, 1, fileds.ToArray(),values.ToArray(),false, key);
            dataGridViewX1.DataBindings.Clear();
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("yinyong", typeof(string));
            dt.Columns.Add("zhbyy", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["yinyong"] = "药品引用";
                dr["zhbyy"] = "转换比引用";
            }
            dataGridViewX1.DataSource = ds.Tables[0];
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewX1.Columns["yinyong"].Index)
            {
                txtClear();
                int index = e.RowIndex;
                //string yinyong = getString("yinyong", index);
                string tym = getString("tym", index);
                string spm = getString("spm", index);
                string gg = getString("gg", index);
                string jx = getString("jx", index);
                string cj = getString("cj", index);
                string pzwh = getString("pzwh", index);
                //string xq = getString("xq", index);
                //string cctj = getString("cctj", index);
                string bzdw = getString("bzdw", index);
                string zhbid = getString("zhbid", index);
                string zhb = getString("zhb", index);
                string zhbdw = getString("zhbdw", index);
                string drugbaseid = getString("Column1", index);
                labDrugBaseID.Text = drugbaseid;
                //string zhbsm = getString("zhbsm", index);
                this.txtTym.Text = tym;
                this.txtTym.Enabled = false;
                this.txtSpm.Text = spm;
                this.txtSpm.Enabled = false;

                this.txtGg.Text = gg;
                this.txtGg.Enabled = false;

                this.txtJx.Text = jx;
                this.txtJx.Enabled = false;

                this.txtCj.Text = cj;
                this.txtCj.Enabled = false;

                this.txtPzwh.Text = pzwh;
                this.txtPzwh.Enabled = false;

                //this.txtBzdw.Text = bzdw;
                //this.txtBzdw.Enabled = false;

                //this.txtXq.Text = xq;
                //this.txtXq.Enabled = false;

                //this.comboCcfs.Text = cctj;
                //this.comboCcfs.Enabled = false;

                //this.txtZhbsm.Text = zhbsm;
                //this.txtZhbsm.Enabled = false;

            }
            else if (e.ColumnIndex == dataGridViewX1.Columns["yyzhb"].Index)
            {
                txtClear();
                int index = e.RowIndex;
                //string yinyong = getString("yinyong", index);
                string tym = getString("tym", index);
                string spm = getString("spm", index);
                string gg = getString("gg", index);
                string jx = getString("jx", index);
                string cj = getString("cj", index);
                string pzwh = getString("pzwh", index);
                //string xq = getString("xq", index);
                //string cctj = getString("cctj", index);
                string bzdw = getString("bzdw", index);
                string zhbid = getString("zhbid", index);
                string zhb = getString("zhb", index);
                string zhbdw = getString("zhbdw", index);
                string zhbsm = getString("zhbsm", index);
                string drugbaseid = getString("Column1", index);
                labDrugBaseID.Text = drugbaseid;
                labGoodsId.Text = zhbid;

                this.txtTym.Text = tym;
                this.txtTym.Enabled = false;
                this.txtSpm.Text = spm;
                this.txtSpm.Enabled = false;

                this.txtGg.Text = gg;
                this.txtGg.Enabled = false;

                this.txtJx.Text = jx;
                this.txtJx.Enabled = false;

                this.txtCj.Text = cj;
                this.txtCj.Enabled = false;

                this.txtPzwh.Text = pzwh;
                this.txtPzwh.Enabled = false;

                this.txtBzdw.Text = bzdw;
                this.txtBzdw.Enabled = false;

                //this.txtXq.Text = xq;
                //this.txtXq.Enabled = false;

                //this.comboCcfs.Text = cctj;
                //this.comboCcfs.Enabled = false;

                this.txtZhb.Text = zhb;
                this.txtZhb.Enabled = false;

                this.txtZhbDw.Text = zhbdw;
                this.txtZhbDw.Enabled = false;

                this.txtZhbsm.Text = zhbsm;
                this.txtZhbsm.Enabled = false;
            }
        }
        private string getString(string name, int index)
        {

            return dataGridViewX1.Rows[index].Cells[name].Value == null ? "" : dataGridViewX1.Rows[index].Cells[name].Value.ToString();
        }

        private void txtClear()
        {
            this.txtTym.Text = "";
            this.txtSpm.Text = "";
            this.txtGg.Text = "";
            this.txtJx.Text = "";
            this.txtCj.Text = "";
            this.txtPzwh.Text = "";
            this.txtBzdw.Text = "";
            this.txtZhb.Text = "";
            this.txtZhbDw.Text = "";
            this.txtZbz.Text = "";
            this.txtJz.Text = "";
            //this.txtXq.Text = "";
            //this.comboCcfs.Text = "";
            this.listBox1.Items.Clear();
            this.txtZhbsm.Text = "";
            this.labGoodsId.Text = "";
            this.labDrugBaseID.Text = "";

            this.txtTym.Enabled = true;
            this.txtSpm.Enabled = true;
            this.txtGg.Enabled = true;
            this.txtJx.Enabled = true;
            this.txtCj.Enabled = true;
            this.txtPzwh.Enabled = true;
            this.txtBzdw.Enabled = true;
            this.txtZhb.Enabled = true;
            this.txtZhbDw.Enabled = true;
            this.txtZbz.Enabled = true;
            this.txtJz.Enabled = true;
            //this.txtXq.Enabled = true;
            //this.comboCcfs.Enabled = true;
            this.txtZhbsm.Enabled = true;
            images.Clear();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            txtClear();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DrugsBaseApply dba = new DrugsBaseApply();
            if (model != null)
            {
                dba = model;
            }
            int gid = 0;
            int.TryParse(labGoodsId.Text, out gid);
            dba.Goods_ID = gid;
            int did = 0;
            int.TryParse(labDrugBaseID.Text, out did);
            dba.DrugsBase_ID = did;
            dba.DrugsBase_DrugName = this.txtTym.Text;
            dba.DrugsBase_ProName = this.txtSpm.Text;
            dba.DrugsBase_Formulation = this.txtJx.Text;
            dba.DrugsBase_Manufacturer = this.txtCj.Text;
            dba.DrugsBase_Specification = this.txtGg.Text;
            dba.DrugsBase_ApprovalNumber = this.txtPzwh.Text;
            //dba.cunchtj = this.comboCcfs.Text;
            //if (dba.cunchtj == "")
            //{
            //    MessageBox.Show("亲，请选择存储条件");
            //    return;
            //}
            //int xq = 0;
            //if (!int.TryParse(this.txtXq.Text, out xq))
            //{
            //    MessageBox.Show("亲，有效期请填写月数字");
            //    return;
            //}
            //else if(xq==0)
            //{
            //    MessageBox.Show("亲，有效期请填写具体有限月数");
            //    return;
            //}
            //dba.YouXq = xq;
            int zhb = 0;
            if (!int.TryParse(this.txtZhb.Text, out zhb))
            {
                MessageBox.Show("亲，转换比请填写数字");
                return;
            }
            dba.Goods_ConveRatio = zhb;
            dba.Goods_ConveRatio_Unit = this.txtZhbDw.Text;
            dba.Goods_Unit = this.txtBzdw.Text;
            dba.Goods_ConveRatio_Unit_Name = this.txtZhbsm.Text;
            dba.DrugsClass = "3";
            dba.source = _idenName;
            dba.sourceName = Login.userName;
            int jz = 0;
            if (!int.TryParse(this.txtJz.Text, out jz))
            {
                MessageBox.Show("亲，件装请填写数字");
                return;
            }
            dba.Goods_Pcs = jz;
            int zbz = 0;
            if (!int.TryParse(this.txtZbz.Text, out zbz))
            {
                MessageBox.Show("亲，中包装请填写数字");
                return;
            }
            dba.Goods_Pcs_Small = zbz;
            //List<string> images=new List<string>();
            //for(int i=0;i<listBox1.Items.Count;i++)
            //{
            //    images.Add(listBox1.Items[i].ToString());
            //}
            if (images.Count == 0)
            {
                MessageBox.Show("亲，请您上传该产品的图片后再来提交！");
                return;
            }
            dba.status = "未提交";
            dba.ImageList = images.ToArray();
            dba.created = DateTime.Now;
            dba.updated = null;
            if ( isn(dba.DrugsBase_DrugName) && isn(dba.DrugsBase_Manufacturer))
            {
                if (model == null)
                {
                    db.AppendDrugsBaseApply(dba, key);
                    MessageBox.Show("保存成功");
                }
                else
                {
                    db.ModifyDrugsBaseApply(dba, key);
                    MessageBox.Show("保存成功");
                    this.Close();
                }

                txtClear();
            }
            else
            {
                MessageBox.Show("亲，请检查数据是否完整！");
            }

        }

        private bool isn(string n)
        {
            return !string.IsNullOrEmpty(n);
        }

        private void buttonX5_Click(object sender, EventArgs e)
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
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    string newFile = upLoad(fileDialog.FileName, Properties.Settings.Default["upload"].ToString());
                    if (newFile.IndexOf("上传文件") == -1)
                    {
                        images.Add(newFile);
                        this.listBox1.Items.Add(fileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show(newFile);
                    }
                }
            }
        }
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

        private void AddDrugBase_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                this.txtTym.Text = model.DrugsBase_DrugName;
                this.txtSpm.Text = model.DrugsBase_ProName;
                this.txtGg.Text = model.DrugsBase_Specification;
                this.txtJx.Text = model.DrugsBase_Formulation;
                this.txtCj.Text = model.DrugsBase_Manufacturer;
                this.txtPzwh.Text = model.DrugsBase_ApprovalNumber;
                this.txtBzdw.Text = model.Goods_Unit;
                //this.txtXq.Text = model.YouXq.ToString();
                this.txtZhb.Text = model.Goods_ConveRatio.ToString();
                this.txtZhbDw.Text = model.Goods_ConveRatio_Unit.ToString();
                this.txtZbz.Text = model.Goods_Pcs_Small.ToString();
                this.txtJz.Text = model.Goods_Pcs.ToString();
                this.txtZhbsm.Text = model.Goods_ConveRatio_Unit_Name.ToString();
                this.labDrugBaseID.Text = model.DrugsBase_ID.ToString();
                this.labGoodsId.Text = model.Goods_ID.ToString();
                //this.comboCcfs.Text = model.cunchtj;
                foreach (string s in model.ImageList)
                {
                    this.listBox1.Items.Add(s);
                }
                images = model.ImageList.ToList();
                this.buttonX1.Text = "修改";
                this.groupBox1.Visible = false;
                if (isSee)
                {
                    this.buttonX1.Visible = false;
                    this.buttonX5.Visible = false;
                }
                if (model.DrugsBase_ID > 0)
                {
                    this.txtTym.Enabled = false;
                    this.txtSpm.Enabled = false;
                    this.txtGg.Enabled = false;
                    this.txtJx.Enabled = false;
                    this.txtCj.Enabled = false;
                    this.txtPzwh.Enabled = false;
                    
                    //this.txtJz.Enabled = false;
                    //this.txtXq.Enabled = false;
                    //this.comboCcfs.Enabled = false;                   
                    this.buttonX4.Enabled = false;
                }
                if (model.Goods_ID > 0)
                {
                    this.txtBzdw.Enabled = false;
                    this.txtZhb.Enabled = false;
                    this.txtZhbDw.Enabled = false;
                    this.txtZhbsm.Enabled = false;
                }

            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show("DEL");
                if (listBox1.Items.Count > 0 && listBox1.SelectedIndex > -1)
                {
                    images.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string file = listBox1.Items[index].ToString();
                if (file.IndexOf('\\') == -1)
                {
                    var webclient = new WebClient();
                    byte[] buffer = webclient.DownloadData(Properties.Settings.Default["upload"].ToString() + "?filename=" + file);
                    var msg = Encoding.UTF8.GetString(buffer);
                    file = msg.ToString();
                }
                //MessageBox.Show(msg.ToString());
                frmShowGoodsImage frmTmp = new frmShowGoodsImage(file);
                frmTmp.Show(this);
            }

        }
    }
}
