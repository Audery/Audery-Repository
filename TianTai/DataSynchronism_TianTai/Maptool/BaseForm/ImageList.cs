using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maptool.DrugsBaseService;

namespace Maptool.BaseForm
{
    public partial class ImageList : DevComponents.DotNetBar.Office2007Form
    {
        private string sqlext;
        private int _iden;
        private string _idenName;
        public ImageList(int pHeight, int iden, string idenName)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this._iden = iden;
            this._idenName = idenName;
            PageSize = (pHeight - 210) / 21;
            PageIndex = 1;
            dataGridViewX1.AutoGenerateColumns = false;
            this.TopLevel = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            Bind();
        }

        /// <summary>
        /// 初始化分页状态
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        public void paginger()
        {
            labelX7.Text = string.Format("{0}/{1}", PageIndex, pageCount);
            if (PageIndex == 1)
            {
                buttonX5.Enabled = false;
            }
            else
            {
                buttonX5.Enabled = true;
            }
            if (PageIndex == pageCount)
            {
                buttonX4.Enabled = false;
            }
            else
            {
                buttonX4.Enabled = true;
            }
        }

        /// <summary>
        /// 上页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (PageIndex == 1) return;
            PageIndex--;
            Bind();
        }
        /// <summary>
        /// 下页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (PageIndex == pageCount) return;
            PageIndex++;
            Bind();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = int.Parse(textBoxX6.Text);
                Bind();

            }
            catch { }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool order { get; set; }
        public string orderField { get; set; }
        public bool like { get; set; }
        public string whereField { get; set; }
        public string whereString { get; set; }
        public int recordCount { get; set; }
        public int pageCount { get; set; }
        List<string> fileds = new List<string>();
        List<string> values = new List<string>();
        private void buttonX1_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            sqlext = "";
            fileds.Clear();
            values.Clear();
            if (textBoxX1.Text != "")
            {
                sqlext = " and DrugsBase_DrugName like('%" + textBoxX1.Text.Trim() + "%')";
                fileds.Add("DrugsBase_DrugName");
                values.Add(textBoxX1.Text.Trim());
            }
            if (textBoxX3.Text != "")
            {
                sqlext += " and DrugsBase_ApprovalNumber like('%" + textBoxX3.Text.Trim() + "%')";
                fileds.Add("DrugsBase_ApprovalNumber");
                values.Add(textBoxX3.Text.Trim());
            }
            if (textBoxX4.Text != "")
            {
                sqlext += " and DrugsBase_Manufacturer like('%" + textBoxX4.Text.Trim() + "%')";
                fileds.Add("DrugsBase_Manufacturer");
                values.Add(textBoxX4.Text.Trim());
            }
            if (textBoxX2.Text != "")
            {
                sqlext += " and DrugsBase_Specification like('%" + textBoxX2.Text.Trim() + "%')";
                fileds.Add("DrugsBase_Specification");
                values.Add(textBoxX2.Text.Trim());
            }
            if (this.comboBoxEx1.Text != "全部" && this.comboBoxEx1.Text != "")
            {
                fileds.Add("status");
                values.Add(comboBoxEx1.Text.Trim());
            }
            //if (string.IsNullOrEmpty(sqlext))
            //{
            //    MessageBox.Show("请输入查询条件");
            //}
            //else
            //{
                Bind();
            //}
        }
        DataSet ds;
        private void Bind()
        {            
            DrugsBaseSoapClient bll = new DrugsBaseSoapClient();
            
            int recordCount = 0;
            int pageCount = 0;
            ds = bll.GetDrugsImageApplyList(PageSize, PageIndex,_idenName, fileds.ToArray(),values.ToArray(), AddDrugBase.key);

            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns.Add("imagetype", typeof(string));
                foreach (DataRow r in dt.Rows)
                {
                    switch ((int)r["category"])
                    {
                        case 1:
                            r["imagetype"] = "包装盒图片";
                            break;
                        case 2:
                            r["imagetype"] = "中包装图片";
                            break;
                        case 3:
                            r["imagetype"] = "彩页图片";
                            break;
                    }
                }
                dataGridViewX1.DataSource = dt;
                recordCount = int.Parse(ds.Tables[1].Rows[0]["recordCount"].ToString());
                pageCount = int.Parse(ds.Tables[1].Rows[0]["pageCount"].ToString());
                this.recordCount = recordCount;
                this.pageCount = pageCount;
                paginger();
                labelX6.Text = string.Format("共{0}条记录", recordCount);
            }
            else
            {
                MessageBox.Show("您的登陆状态已经失效，请关闭后重新登陆!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            AddImage adb = new AddImage(_idenName);
            adb.iden = _iden;
            adb.ShowDialog();
        }

        private int CurrentRowIndex { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];
            if (!"已审核".Equals(row.Cells["Column13"].Value))
            {
                DataRow model = ds.Tables[0].Rows[CurrentRowIndex];
                AddImage ad = new AddImage(_idenName);
                ad.imageDr = model;
                ad.ShowDialog();
            }
        }

        /// <summary>
        /// 删除申请数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];
            if (!"已审核".Equals(row.Cells["Column13"].Value))
            {
                if (DialogResult.OK == MessageBox.Show("您确定要删除该条申请数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    DrugsBaseSoapClient db = new DrugsBaseSoapClient();
                    db.DeleteDrugsImageApplyId((int)ds.Tables[0].Rows[CurrentRowIndex]["Goods_ID"], AddDrugBase.key, (int)ds.Tables[0].Rows[CurrentRowIndex]["category"]);
                    Bind();
                }
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            CurrentRowIndex = e.RowIndex;
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];

            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            contextMenuStrip1.Items[0].Visible = true;
            contextMenuStrip1.Items[1].Visible = true;
            contextMenuStrip1.Items[2].Visible = true;
            contextMenuStrip1.Items[3].Visible = true;

            if ("未提交".Equals(row.Cells["Column13"].Value) || "未通过".Equals(row.Cells["Column13"].Value))
            {
                contextMenuStrip1.Items[0].Visible = false;
            }
            else
            {
                contextMenuStrip1.Items[1].Visible = false;
                contextMenuStrip1.Items[2].Visible = false;
                contextMenuStrip1.Items[3].Visible = false;
            }

        }
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           
            DrugsBaseSoapClient bll = new DrugsBaseSoapClient();
            DataRow dr = ds.Tables[0].Rows[CurrentRowIndex];           
            bll.SetDrugsImageApply((int)dr["Goods_ID"], (int)dr["category"], AddDrugBase.key);
            Bind();
        }

        /// <summary>
        /// 查看申请
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];

            DataRow model = ds.Tables[0].Rows[CurrentRowIndex];
            AddImage ad = new AddImage(_idenName);
            ad.imageDr = model;
            ad.isSee = true;
            ad.ShowDialog();

        }
    }
}
