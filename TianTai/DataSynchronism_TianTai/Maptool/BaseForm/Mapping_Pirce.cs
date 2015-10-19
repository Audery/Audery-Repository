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
    public partial class Mapping_Pirce : DevComponents.DotNetBar.Office2007Form
    {
        public string sqlext = "";
        public Mapping_Pirce(int pHeight, int iden)
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
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //dataGridViewX1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridViewX1.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewX1_CellValueChanged);
            dataGridViewX1.CellContentClick += new DataGridViewCellEventHandler(dataGridViewX1_CellContentClick);
            //中药材显示的方式
            if (Program.iden_in_zyc.Contains(iden))
            {
                dataGridViewX1.Columns["Goods_Pcs"].HeaderText = "产地";
                dataGridViewX1.Columns["Goods_Pcs"].DataPropertyName = "ProductionAddress";
                dataGridViewX1.Columns["Goods_Pcs_Small"].HeaderText = "片型";
                dataGridViewX1.Columns["Goods_Pcs_Small"].DataPropertyName = "DrugsBase_Formulation1";
                dataGridViewX1.Columns.Insert(10, new DataGridViewTextBoxColumn() { HeaderText = "制法", DataPropertyName = "ProductionMethodName", });
            }

        }
        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            

        }
        private void dataGridViewX1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dataGridViewX1.Rows)
            {
                item.Cells["Stock"].ToolTipText = item.Cells["StockToolTipText"].Value.ToString();
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击了设置单元格
            if (e.ColumnIndex == dataGridViewX1.Columns["Column_Opearte"].Index)
            {
                int rowIndex = e.RowIndex;
                string ID = dataGridViewX1.Rows[rowIndex].Cells["ID"].Value.ToString();
                Mapping_PricePlus mp = new Mapping_PricePlus();
                mp.ERP_ID = ID;
                mp.Bind();
                mp.Show();
            }
        }
        public void Bind()
        {
            string msg = Main.Menu.First(x => x.id == iden).name.Replace("数据映射", "");
            switch (BinType)
            {
                case 0: groupBox1.Text = "价格管理(" + msg + ")"; break;
                default: groupBox1.Text = "价格管理(" + msg + ")"; break;
            }
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            var li = bll.GetMaptoolList(PageSize, PageIndex, order, orderField, like, whereField, sqlext, BinType, Login.authKey, this.iden);

            if (li != null)
            {
                //#region 货源2.0新增内容（销售单位，用于控制商城前台销售商品的可售单位（最小单位、中包装、件装））
                li.Tables[0].Columns.Add("st", typeof(string));
                //#endregion

                var dt = li.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //动态添加价格类型
                    string ID = dt.Rows[i]["ID"].ToString();
                    DataTable PriceTable = bll.GetPriceByID(ID);
                    if (PriceTable != null && PriceTable.Rows.Count > 0)
                    {

                        foreach (DataRow dr in PriceTable.Rows)
                        {
                            string tempcategory = dr["category"] == null ? string.Empty : dr["category"].ToString();
                            decimal tempprice = dr["price_n"] == null ? 0 : Convert.ToDecimal(dr["price_n"]);
                            //添加新列
                            if (!dt.Columns.Contains(tempcategory))
                            {
                                dt.Columns.Add(tempcategory, typeof(string));
                                if (!dataGridViewX1.Columns.Contains(tempcategory))
                                {
                                    dataGridViewX1.Columns.Insert(13, new DataGridViewTextBoxColumn()
                                    {
                                        HeaderText = tempcategory,
                                        DataPropertyName = tempcategory,
                                        Name = tempcategory
                                    });

                                }

                            }
                            dt.Rows[i][tempcategory] = tempprice;
                        }
                    }
                }
                dataGridViewX1.DataSource = dt;
                recordCount = int.Parse(li.Tables[1].Rows[0]["recordCount"].ToString());
                pageCount = int.Parse(li.Tables[1].Rows[0]["pageCount"].ToString());
                paginger();
                labelX1.Text = string.Format("共{0}条记录", recordCount);

            }
            else
            {
                MessageBox.Show("您的登陆状态已经失效，请关闭后重新登陆!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 初始化分页状态
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        public void paginger()
        {
            labelX4.Text = string.Format("{0}/{1}", PageIndex, pageCount);
            if (PageIndex == 1)
            {
                buttonX2.Enabled = false;
            }
            else
            {
                buttonX2.Enabled = true;
            }
            if (PageIndex == pageCount)
            {
                buttonX3.Enabled = false;
            }
            else
            {
                buttonX3.Enabled = true;
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
            if (PageIndex == 1) return;
            PageIndex--;
            Bind();
        }
        /// <summary>
        /// 下页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (PageIndex == pageCount) return;
            PageIndex++;
            Bind();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = int.Parse(textBoxX1.Text);
                Bind();

            }
            catch { }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            sqlext = "";
            if (textBoxX2.Text != "")
            {
                sqlext = " and DrugsBase_DrugName like('%" + textBoxX2.Text.Trim() + "%')";
            }
            if (textBoxX3.Text != "")
            {
                sqlext += " and DrugsBase_Manufacturer like('%" + textBoxX3.Text.Trim() + "%')";
            }
            if (textBoxX4.Text != "")
            {
                sqlext += " and DrugsBase_Specification like('%" + textBoxX4.Text.Trim() + "%')";
            }
            if (textBoxX5.Text != "")
            {
                sqlext += " and ID='" + textBoxX5.Text + "'";
            }
            if (comboBoxEx1.Text == "有")
            {
                sqlext += " and id in (SELECT id FROM dbo.View_Stock1 WHERE iden=" + iden + " AND Stock>0)";
            }
            else if (comboBoxEx1.Text == "无")
            {
                sqlext += " and id not in (SELECT id FROM dbo.View_Stock1 WHERE iden=" + iden + " AND Stock>0)";
            }
            if (!string.IsNullOrEmpty(textBoxX6.Text))
            {
                sqlext += " and DrugsBase_Formulation like('%" + textBoxX6.Text.Trim() + "%')";
            }

            if (!string.IsNullOrEmpty(textBoxX7.Text))
            {
                sqlext += " and Goods_Pcs_Small=" + textBoxX7.Text.Trim();
            }

            //Yj2015.0226 Add
            if (!string.IsNullOrEmpty(txtPiZhWh.Text.Trim()))
            {
                sqlext += " AND DrugsBase_ApprovalNumber like '%" + txtPiZhWh.Text.Trim() + "%'";
            }

            Bind();
        }
    }
}

