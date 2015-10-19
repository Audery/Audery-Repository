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
    public partial class AddDrugBaseZyc : DevComponents.DotNetBar.Office2007Form
    {
        public const string key = "01CA23A6-8CF7-4A7D-91D5-54B2C083D9AF";
        private string _idenName;
       DrugsBaseSoapClient db = new DrugsBaseSoapClient();
        
        public bool isSee { get; set; }
        List<string> images = new List<string>();
        public DrugsBaseApplyZyc model { get; set; }
        public AddDrugBaseZyc(string idenName)
        {
            InitializeComponent();
            this._idenName = idenName;
            dataGridViewX1.AutoGenerateColumns = false;
            this.comboCcfs.DataSource = db.GetDrugsBaseEnum(DrugsBaseEnum.存储条件);
            this.comboBoxEx1.DataSource = db.GetDrugsBaseEnum(DrugsBaseEnum.中药类别);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

            string name = this.textBoxX2.Text;
            string gg = this.textBoxX3.Text;
            string dj = this.textBoxX4.Text;
            string zf = this.textBoxX5.Text;
            string cd = this.textBoxX1.Text;
            string sql = "";
            List<string> fileds = new List<string>();
            List<string> values = new List<string>();
            if (!string.IsNullOrEmpty(name))
            {
                sql = " And DrugsBase_DrugName='" + name + "'";
                fileds.Add("DrugsBase_DrugName");
                values.Add(name);
            }
            if (!string.IsNullOrEmpty(gg))
            {
                sql += " And DrugsBase_Specification='" + gg + "'";
                fileds.Add("DrugsBase_Specification");
                values.Add(gg);
            }
            if (!string.IsNullOrEmpty(dj))
            {
                sql += " And ProductionLevelName='" + dj + "'";
                fileds.Add("ProductionLevelName");
                values.Add(dj);
            }
            if (!string.IsNullOrEmpty(zf))
            {
                sql += " And ProductionMethodName='" + zf + "'";
                fileds.Add("ProductionMethodName");
                values.Add(zf);
            }

            if (!string.IsNullOrEmpty(cd))
            {
                sql += " And ProductionAddress='" + cd + "'";
                fileds.Add("ProductionAddress");
                values.Add(cd);
            }
            if (!string.IsNullOrEmpty(textBoxX6.Text))
            {
                fileds.Add("DrugsBase_Manufacturer");
                values.Add(textBoxX6.Text);
            }

            fileds.Add("DrugsBase_Formulation");
            values.Add("中药饮片");

            DataSet ds = db.GetDrugsbaseAllList(20, 1, fileds.ToArray(), values.ToArray(),false, key);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("yinyong", typeof(string));        
            foreach (DataRow dr in dt.Rows)
            {
                dr["yinyong"] = "引用";
            }
            dataGridViewX1.DataBindings.Clear();
            dataGridViewX1.DataSource = dt;

        }

       

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns["yinyong"].Index)
            {
                int index = e.RowIndex;
                //string yinyong = getString("yinyong",index);
                string tym = getString("tym", index);
                string spm = getString("spm", index);
                string gg = getString("gg", index);
                string dj = getString("dj", index);
                string zf = getString("zf", index);
                string px = getString("pianxing", index);              
                string cd = getString("cd", index);
                string cctj = getString("cctj", index);
                string bzdw = getString("bzdw", index);
                string cj = getString("cj", index);
                string zhbid = getString("zhbid", index);
                string zhb = getString("zhb", index);
                string zhbdw = getString("zhbdw", index);
                string zhbsm = getString("zhbsm", index);
                string drugid = getString("Column1", index);

                this.labDrugBaseID.Text = drugid;

                BagCapacity7.Text = getString("BagCapacity", index);
                this.txtTym.Text = tym;
                this.txtTym.Enabled = false;
                this.txtSpm.Text = spm;
                this.txtGg.Text = gg;
                this.txtCj.Text = cj;
                this.txtCj.Enabled = false;
                this.txtDj.Text = dj;
                this.txtBzdw.Text = bzdw;
                this.txtCd.Text = cd;
                this.txtPx.Text = px;
                this.txtZf.Text = zf;

                this.labZhbId.Text = zhbid;
                this.txtZhb.Text = zhb;
                this.txtZhbDw.Text = zhbdw;
                this.txtZhbsm.Text = zhbsm;
                //comboCcfs.Text = cctj;
            }
        }
        private string getString(string name,int index)
        {

            return dataGridViewX1.Rows[index].Cells[name].Value == null ? "" : dataGridViewX1.Rows[index].Cells[name].Value.ToString();
        }

        private void txtClear()
        {
            this.txtTym.Text = "";
            this.txtTym.Enabled = true;
            this.txtSpm.Text = "";
            this.txtGg.Text = "";
            this.txtCj.Text = "";
            this.txtDj.Text = "";
            this.txtBzdw.Text = "";
            this.txtCd.Text = "";
            this.txtPx.Text = "";
            this.txtZf.Text = "";
            this.txtJz.Text = "";
            this.txtZbz.Text = "";
           
            this.labDrugBaseID.Text = "";
            this.listBox1.Items.Clear();

            this.txtSpm.Enabled = true;
            this.txtGg.Enabled = true;
            this.txtCj.Enabled = true;
            this.txtDj.Enabled = true;
          
            this.txtBzdw.Enabled = true;
            this.txtCd.Enabled = true;
            this.txtPx.Enabled = true;
            this.txtZf.Enabled = true;
            this.txtJz.Enabled = true;
            this.txtZbz.Enabled = true;

            this.labZhbId.Text = "";
            this.txtZhb.Text = "";
            this.txtZhbDw.Text = "";
            this.txtZhbsm.Text = "";

            //this.comboCcfs.Enabled = true;
            images.Clear();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            txtClear();   
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DrugsBaseApplyZyc dba = new DrugsBaseApplyZyc();
            if (model != null)
            {
                dba = model;
            }
            int drugid = 0;
            int.TryParse(labDrugBaseID.Text, out drugid);
            dba.DrugsBase_ID = drugid;

            dba.DrugsBase_DrugName = this.txtTym.Text;
            dba.DrugsBase_ProName = this.txtSpm.Text;
            dba.DrugsBase_Formulation_1 = this.txtPx.Text;
            dba.DrugsBase_Manufacturer = this.txtCj.Text;
            dba.DrugsBase_Specification = this.txtGg.Text;

            dba.ProductionAddress = this.txtCd.Text;
            dba.ProductionLevel = this.txtDj.Text;
            dba.ProductionMethod = this.txtZf.Text;
            dba.ProductionClass = this.comboBoxEx1.Text;
            dba.ProductionBrand = this.txtSpm.Text;
            dba.DrugsBase_Formulation = "中药饮片";
            if (BagCapacity7.Text == "")
            {
                dba.BagCapacity = 1M;
            }
            else
            {
                dba.BagCapacity = decimal.Parse(BagCapacity7.Text);
            }
            
            //dba.cunchtj = this.comboCcfs.Text;
            //if (dba.cunchtj == "")
            //{
            //    MessageBox.Show("亲，请选择存储条件");
            //    return;
            //}
            dba.Goods_ConveRatio = 1;
            //dba.Goods_ConveRatio_Unit = this.txtPx.Text;            
            dba.Goods_Unit = this.txtBzdw.Text;
            dba.source=_idenName;
            dba.sourceName=Login.userName;
            int jz=0;
            if (!int.TryParse(this.txtJz.Text, out jz) && string.IsNullOrEmpty(this.txtJz.Text))
            {
                MessageBox.Show("亲，件装请填写数字");
                return;
            }
            dba.Goods_Pcs = jz;
            int zbz = 0;
            if (!int.TryParse(this.txtZbz.Text, out zbz) && string.IsNullOrEmpty(this.txtZbz.Text))
            {
                MessageBox.Show("亲，中包装请填写数字");
                return;
            }
            
            int zhbid = 0;
            int.TryParse(this.labZhbId.Text, out zhbid);
            dba.Goods_ID = zhbid;
            int zhb = 0;
            int.TryParse(this.txtZhb.Text, out zhb);            
            dba.Goods_ConveRatio = zhb;           
            dba.Goods_ConveRatio_Unit = this.txtZhbDw.Text;
            dba.Goods_ConveRatio_Unit_Name = this.txtZhbsm.Text;            
            dba.DrugsClass = "2";
            dba.Goods_Pcs_Small = zbz;
            dba.status = "未提交";
            dba.created = DateTime.Now;
            dba.updated = null;
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

            dba.ImageList = images.ToArray();
            if ( isn(dba.DrugsBase_DrugName) && isn(dba.DrugsBase_Manufacturer) && isn(dba.DrugsBase_Specification) && isn(dba.Goods_Unit))
            {
                if (model == null)
                {
                    db.AppendDrugsBaseApplyZyc(dba, key);
                    MessageBox.Show("保存成功");
                }
                else
                {
                    db.ModifyDrugsBaseApplyZyc(dba, key);
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

        private void AddDrugBaseZyc_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                this.txtTym.Text = model.DrugsBase_DrugName;
                this.txtSpm.Text = model.DrugsBase_ProName;
                this.txtGg.Text = model.DrugsBase_Specification;
                this.comboBoxEx1.Text = model.ProductionClass;
                this.txtCj.Text = model.DrugsBase_Manufacturer;
                this.txtDj.Text = model.ProductionLevel;
                this.txtZf.Text = model.ProductionMethod;
                this.labDrugBaseID.Text = model.DrugsBase_ID.ToString();
                //this.txtPzwh.Text = model.DrugsBase_ApprovalNumber;
                this.txtBzdw.Text = model.Goods_Unit;
                this.txtCd.Text = model.ProductionAddress;
                this.txtPx.Text = model.DrugsBase_Formulation_1;
                //this.txtXq.Text = model.YouXq.ToString();
                this.txtZhb.Text = model.Goods_ConveRatio.ToString();
                this.txtZhbDw.Text = model.Goods_ConveRatio_Unit.ToString();
                this.txtZhbsm.Text = model.Goods_ConveRatio_Unit_Name;
                this.labZhbId.Text = model.Goods_ID.ToString();
                this.txtZbz.Text = model.Goods_Pcs_Small.ToString();
                this.txtJz.Text = model.Goods_Pcs.ToString();
                this.comboCcfs.Text = model.cunchtj;
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
                    this.txtCj.Enabled = false;
                    this.buttonX4.Visible = false;
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
