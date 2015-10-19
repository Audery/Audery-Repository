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
    /// <summary>
    /// 货源管理2.0增加功能：数据标准化（主要解决中包装数据含义不清的问题）
    /// </summary>
    public partial class Mapping_Mid : DevComponents.DotNetBar.Office2007Form
    {
        public string sqlext = "";
        public Mapping_Mid(int pHeight, int iden)
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
            order = true;
            orderField = "created";
            like = true;
            whereField = "DrugsBase_DrugName";
            whereString = null;
            dataGridViewX1.CellContentClick += new DataGridViewCellEventHandler(dataGridViewX1_CellContentClick);
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewX1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridViewX1.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewX1_CellValueChanged);

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
            if (e.ColumnIndex == dataGridViewX1.Columns["Sum"].Index)
            {
                setData(e.RowIndex, 1);
            }
            if (e.ColumnIndex == dataGridViewX1.Columns["StockTypes"].Index)
            {
                setData(e.RowIndex, 2);
            }
            if (e.ColumnIndex == dataGridViewX1.Columns["PriceTypes"].Index)
            {
                setData(e.RowIndex, 3);
            }

        }

        private void setData(int row, int col)
        {
            if (!string.IsNullOrEmpty(dataGridViewX1.Rows[row].Cells["sum"].Value.ToString()))
            {
                int s = 1;
                int.TryParse(dataGridViewX1.Rows[row].Cells["sum"].Value.ToString(), out s);
                if (s == 0)
                {
                    MessageBox.Show("数量不能是零！");
                    return;
                }

            }
            Maptool.soso.Link_Mid m = new soso.Link_Mid();
            m.id = (int)dataGridViewX1.Rows[row].Cells["linkid"].Value;
            m.iden = Convert.ToInt32(iden_lab.Text);
            m.Sum = string.IsNullOrEmpty(dataGridViewX1.Rows[row].Cells["sum"].Value.ToString()) ? 1 : (int)dataGridViewX1.Rows[row].Cells["sum"].Value;
            m.StockType = GetUnitNo(dataGridViewX1.Rows[row].Cells["stocktypes"].Value.ToString());
            m.PriceType = GetUnitNo(dataGridViewX1.Rows[row].Cells["pricetypes"].Value.ToString());
            m.Updated = DateTime.Now;
            if (m.StockType != 1 && string.IsNullOrEmpty(dataGridViewX1.Rows[row].Cells["sum"].Value.ToString()))
            {
                MessageBox.Show("数量不能空！");
            }
            else
            {
                if (m.Sum == 0)
                {
                    m.Sum = 1;
                }
                soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                bll.SetData(m, col, Login.authKey);
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
                //if (dataGridViewX1.Rows[e.RowIndex].Cells[1].Value.ToString() == "未映射")
                //{
                //    MappingSet ms = new MappingSet();
                //    ms.knid = dataGridViewX1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                //    ms.DrugsBase_DrugName = dataGridViewX1.Rows[e.RowIndex].Cells["DrugsBase_DrugName"].Value.ToString().Trim();
                //    ms.Goods_ConveRatio = dataGridViewX1.Rows[e.RowIndex].Cells["Goods_ConveRatio"].Value.ToString();
                //    ms.price = decimal.Parse(dataGridViewX1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                //    ms.iden = this.iden;
                //    ms.from1 = this;
                //    ms.ShowDialog();
                //}
                //else
                //{
                //    MappingEdit ms = new MappingEdit();
                //    ms.knid = dataGridViewX1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                //    ms.DrugsBase_DrugName = dataGridViewX1.Rows[e.RowIndex].Cells["DrugsBase_DrugName"].Value.ToString();
                //    ms.Goods_ConveRatio = dataGridViewX1.Rows[e.RowIndex].Cells["Goods_ConveRatio"].Value.ToString();
                //    ms.price = decimal.Parse(dataGridViewX1.Rows[e.RowIndex].Cells["price"].Value.ToString());
                //    ms.iden = this.iden;
                //    ms.from1 = this;
                //    ms.ShowDialog();
                //    // MappingShow ms = new MappingShow(dataGridViewX1.Rows[e.RowIndex].Cells["ID"].Value.ToString(), iden);
                //    // ms.ShowDialog();
                //    //MessageBox.Show("此产品已经映射不能重复映射!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }
        public void Bind()
        {
            iden_lab.Text = iden.ToString();
            string msg = Main.Menu.First(x => x.id == iden).name.Replace("数据映射", "");
            switch (BinType)
            {
                case 0: groupBox1.Text = "全部数据(" + msg + ")"; break;
                case 1: groupBox1.Text = "数据标准化(" + msg + ")"; break;
            }
            //if (BinType != 1)
            //{
            dataGridViewX1.Columns["Column2"].Visible = false;
            dataGridViewX1.Columns["Column1"].Visible = false;
            dataGridViewX1.Columns["linkid"].Visible = false;
            //}
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            var li = bll.GetMaptoolDataList(PageSize, PageIndex, order, orderField, like, whereField, sqlext, BinType, Login.authKey, this.iden);

            if (li != null)
            {
                var dt = li.Tables[0];
                dt.Columns.Add("StockTypes", typeof(string));
                dt.Columns.Add("PriceTypes", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["StockType"] != System.DBNull.Value)
                    {
                        switch (Convert.ToInt32(dt.Rows[i]["StockType"]))
                        {
                            case 1:
                                dt.Rows[i]["StockTypes"] = GetUnit(1);
                                break;
                            case 2:
                                dt.Rows[i]["StockTypes"] = GetUnit(2);
                                break;
                            case 3:
                                dt.Rows[i]["StockTypes"] = GetUnit(3);
                                break;
                        }
                    }
                    if (dt.Rows[i]["PriceType"] != System.DBNull.Value)
                    {
                        switch (Convert.ToInt32(dt.Rows[i]["PriceType"]))
                        {
                            case 1:
                                dt.Rows[i]["PriceTypes"] = GetUnit(1);
                                break;
                            case 2:
                                dt.Rows[i]["PriceTypes"] = GetUnit(2);
                                break;
                            case 3:
                                dt.Rows[i]["PriceTypes"] = GetUnit(3);
                                break;
                        }
                    }

                    #region 动态添加价格类型
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
                                    dataGridViewX1.Columns.Insert(12, new DataGridViewTextBoxColumn()
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
                    #endregion
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

        public static string GetUnit(int unit)
        {
            switch (unit)
            {
                case 1:
                    return "最小单位";
                case 2:
                    return "中包装";
                case 3:
                    return "件装";
            }
            return "最小单位";
        }

        public static int GetUnitNo(string unit)
        {
            switch (unit)
            {
                case "最小单位":
                    return 1;
                case "中包装":
                    return 2;
                case "件装":
                    return 3;
            }
            return 1;
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
        /// 绑定的数据类型 0 全部 1 数据标准化（中包装数大于零）
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

            if (comboBoxEx2.Text != "全部")
            {
                switch (comboBoxEx2.Text)
                {
                    case "未处理":
                        {
                            sqlext += " and id not in (SELECT id FROM dbo.tags WHERE tab='" + comboBoxEx2.Text + "' AND iden=" + iden + ")";
                            break;
                        }
                    default:
                        {
                            sqlext += " and id in (SELECT id FROM dbo.tags WHERE tab='" + comboBoxEx2.Text + "' AND iden=" + iden + ")";
                            break;
                        }
                }
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

