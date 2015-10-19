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
    public partial class ConfigList : DevComponents.DotNetBar.Office2007Form
    {
        public string sqlext = "";
        public ConfigList(int pHeight)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewX1_DataBindingComplete);
            this.TopLevel = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            PageSize = (pHeight - 210) / 21;
            PageIndex = 1;
            order = false;
            orderField = "id";
            like = true;
            whereField = "DrugsBase_DrugName";
            whereString = null;
            //dataGridViewX1.CellContentClick += new DataGridViewCellEventHandler(dataGridViewX1_CellContentClick);
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewX1.DataError += new DataGridViewDataErrorEventHandler(dataGridViewX1_DataError);
            dataGridViewX1.UserDeletingRow += new DataGridViewRowCancelEventHandler(dataGridViewX1_UserDeletingRow);
        }
        public void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("输入格式不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
        }
        public void dataGridViewX1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("确认是否删除此条数据!", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        private void dataGridViewX1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dataGridViewX1.Rows)
            {
                item.Cells["Column1"].ToolTipText = item.Cells["ID"].Value.ToString();
                item.Cells["Stock"].ToolTipText = item.Cells["StockToolTipText"].Value.ToString();
            }
        }
        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns["Stock"].Index)
            {
                Clipboard.SetText(dataGridViewX1.Rows[e.RowIndex].Cells["Stock"].ToolTipText.ToString());
                return;
            }
            if (e.ColumnIndex == dataGridViewX1.Columns[0].Index)
            {

            }
        }
        public void Bind()
        {

            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            var li = bll.GetConfigList();

            if (li != null)
            {
                int i = 0;
                foreach (var item in li)
                {
                    dataGridViewX1.Rows.Add();
                    dataGridViewX1.Rows[i].Cells["id"].Value = item.id;
                    dataGridViewX1.Rows[i].Cells["id"].ValueType = typeof(int);

                    dataGridViewX1.Rows[i].Cells["incName"].Value = item.incName;

                    dataGridViewX1.Rows[i].Cells["discountRate"].Value = Math.Round(item.discountRate, 2);
                    dataGridViewX1.Rows[i].Cells["discountRate"].ValueType = typeof(decimal);

                    dataGridViewX1.Rows[i].Cells["cgy"].Value = item.cgy;
                    i++;
                }
                recordCount = li.Count();
                pageCount = 1;

            }
            else
            {
                MessageBox.Show("您的登陆状态已经失效，请关闭后重新登陆!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 绑定的数据类型 0 全部 1 未映射 2 已经映射
        /// </summary>
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
        /// <summary>
        /// 上页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Bind();
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            List<soso.Config> li = new List<soso.Config>();
            foreach (DataGridViewRow item in dataGridViewX1.Rows)
            {
                if (item.Cells["id"].Value == null)
                {
                    break;
                }
                soso.Config model = new soso.Config();
                model.id = item.Cells["id"].Value.ToString();
                model.cgy = item.Cells["cgy"].Value.ToString();
                model.incName = item.Cells["incName"].Value.ToString();
                model.discountRate = float.Parse(item.Cells["discountRate"].Value.ToString());
                li.Add(model);
            }
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();

            bll.SetConfigList(li.ToArray(), Login.authKey);
            MessageBox.Show("操作成功!", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

