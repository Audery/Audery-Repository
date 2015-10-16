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
    public partial class NoDrugBase : DevComponents.DotNetBar.Office2007Form
    {
        private int _iden;
        private string _idenName;
        public NoDrugBase(int pHeight, int iden, string idenName)
        {

            InitializeComponent();
            this.ShowInTaskbar = false;
            this._iden = iden;
            this._idenName = idenName;
            PageSize = (pHeight - 210) / 21;
            PageIndex = 1;
            dataGridViewX1.AutoGenerateColumns = false;
            //dataGridViewX1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewX1_DataBindingComplete);
            this.TopLevel = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            AddNoDrug adb = new AddNoDrug(_idenName);
            adb.ShowDialog();
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

        List<string> fields = new List<string>();
        List<string> values = new List<string>();
        DrugsBaseApply[] li;

        private void Bind()
        {
            DrugsBaseSoapClient bll = new DrugsBaseSoapClient();
            int recordCount = 0;
            int pageCount = 0;

            li = bll.GetDrugsBaseApplyList(out recordCount, out pageCount, PageSize, PageIndex, _idenName, fields.ToArray(), values.ToArray(), AddDrugBase.key);
            if (li != null)
            {
                foreach (DrugsBaseApply dr in li)
                {
                    dr.created = dr.created.ToLocalTime();
                    dr.updated = dr.updated == null ? null : (DateTime?)dr.updated.Value.ToLocalTime();
                }
                dataGridViewX1.DataSource = li;
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
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool order { get; set; }
        public string orderField { get; set; }
        public bool like { get; set; }
        public string whereField { get; set; }
        public string whereString { get; set; }
        public int recordCount { get; set; }
        public int pageCount { get; set; }
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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            fields.Clear();
            values.Clear();
            if (!string.IsNullOrEmpty(textBoxX2.Text))
            {
                fields.Add("DrugsBase_DrugName");
                values.Add(textBoxX2.Text);
            }
            if (!string.IsNullOrEmpty(textBoxX3.Text))
            {
                fields.Add("DrugsBase_Specification");
                values.Add(textBoxX3.Text);
            }
            if (!string.IsNullOrEmpty(textBoxX4.Text))
            {
                fields.Add("DrugsBase_Formulation");
                values.Add(textBoxX4.Text);
            }
            if (!string.IsNullOrEmpty(textBoxX5.Text))
            {
                fields.Add("DrugsBase_ApprovalNumber");
                values.Add(textBoxX5.Text);
            }
            if (!string.IsNullOrEmpty(textBoxX1.Text))
            {
                fields.Add("DrugsBase_Manufacturer");
                values.Add(textBoxX1.Text);
            }
            if (comboBoxEx1.Text != "全部")
            {
                switch (comboBoxEx1.Text)
                {
                    case "未提交":
                        fields.Add("status");
                        values.Add("未提交");
                        break;
                    case "未审核":
                        fields.Add("status");
                        values.Add("未审核");
                        break;
                    case "已审核":
                        fields.Add("status");
                        values.Add("已审核");
                        break;
                    case "未通过":
                        fields.Add("status");
                        values.Add("未通过");
                        break;
                }
            }
            fields.Add("DrugsClass");
            values.Add("3");
            Bind();
        }

        private int CurrentRowIndex { get; set; }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];
            if (!"已审核".Equals(row.Cells["Column13"].Value))
            {
                DrugsBaseApply model = li[CurrentRowIndex];
                AddDrugBase ad = new AddDrugBase(_idenName);
                ad.model = model;
                ad.ShowDialog();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];
            if (!"已审核".Equals(row.Cells["Column13"].Value))
            {
                if (DialogResult.OK == MessageBox.Show("您确定要删除该条申请数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    DrugsBaseSoapClient db = new DrugsBaseSoapClient();
                    db.DeleteDrugsBaseApply(row.Cells["id"].Value.ToString(), AddDrugBase.key);
                    Bind();
                }
            }
        }

        private void dataGridViewX1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            CurrentRowIndex = e.RowIndex;
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DrugsBaseApply model = li[CurrentRowIndex];
            DrugsBaseSoapClient bll = new DrugsBaseSoapClient();
            model.status = "未审核";
            model.updated = DateTime.Now;
            bll.ModifyDrugsBaseApply(model, AddDrugBase.key);
            Bind();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewX1.Rows[CurrentRowIndex];
            DrugsBaseApply model = li[CurrentRowIndex];
            AddDrugBase ad = new AddDrugBase(_idenName);
            ad.model = model;
            ad.isSee = true;
            ad.ShowDialog();
        }
    }
}
