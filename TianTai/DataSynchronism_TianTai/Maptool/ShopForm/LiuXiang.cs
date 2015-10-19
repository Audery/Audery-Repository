using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maptool.ShopForm;

namespace Maptool.BaseForm
{
    public partial class LiuXiang : DevComponents.DotNetBar.Office2007Form
    {
        public string sqlext = "";
        
        /// <summary>
        /// 是否是比价打开的界面
        /// </summary>
        public bool Pirce_PK = false;
        public LiuXiang(int pHeight)
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
            orderField = "Product_ID";
            like = true;
            whereField = "DrugsBase_DrugName";
            whereString = null;
            dataGridViewX1.CellContentClick += new DataGridViewCellEventHandler(dataGridViewX1_CellContentClick);
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                      
        }
        private void dataGridViewX1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dataGridViewX1.Rows)
            {
                item.Cells["Column1"].ToolTipText = string.Format("商品ID：{0}", item.Cells["Product_ID"].Value.ToString());
            }
        }
        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击了流向设置单元格
            if (e.ColumnIndex == dataGridViewX1.Columns["Column1"].Index)
            {
                int rowIndex = e.RowIndex;
                string Product_ID = dataGridViewX1.Rows[rowIndex].Cells["Product_ID"].Value.ToString();
                SetLiuXiang ms2 = new SetLiuXiang();
                ms2.pid = Product_ID;
                ms2.ShowDialog();

            }
        }
        public void Bind()
        {
            string msg = "商城数据";
            switch (BinType)
            {
                case 0: groupBox1.Text = "全部数据(" + msg + ")"; break;
                case 1: groupBox1.Text = "未映射数据(" + msg + ")"; break;
                case 2: groupBox1.Text = "已映射数据(" + msg + ")"; break;
            }
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();

            var li = bll.GetShopList(PageSize, PageIndex, order, orderField, like, whereField, sqlext, BinType, Login.authKey, iden);

            if (BinType == 1)
            {
                dataGridViewX1.Columns["sup"].Visible = false;
            }
            else
            {
                dataGridViewX1.Columns["sup"].Visible = true;
            }
            if (li != null)
            {
                //取商品流向设置
                DataTable lx = bll.GetLiuXiangList(Login.authKey);
                var dt = li.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //动态添加价格类型
                    string ID = dt.Rows[i]["spid"].ToString();
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

        private void search()
        {
            sqlext = "";
            if (textBoxX2.Text != "")
            {
                sqlext = " and DrugsBase_DrugName like('%" + textBoxX2.Text.Trim().Replace("'", "") + "%')";
            }
            if (textBoxX3.Text != "")
            {
                sqlext += " and DrugsBase_Manufacturer like('%" + textBoxX3.Text.Trim().Replace("'", "") + "%')";
            }
            if (textBoxX4.Text != "")
            {
                sqlext += " and DrugsBase_Specification like('%" + textBoxX4.Text.Trim().Replace("'", "") + "%')";
            }
            if (comboBoxEx1.Text == "多个")
            {
                sqlext += " and Product_ID in (SELECT id FROM Data.Data_Centre.dbo.Link GROUP BY id HAVING COUNT(id)>1)";
            }
            if (comboBoxEx1.Text == "单个")
            {
                sqlext += " and Product_ID in (SELECT id FROM Data.Data_Centre.dbo.Link GROUP BY id HAVING COUNT(id)=1)";
            }

            if (comboBoxEx2.Text == "有")
            {
                sqlext += " and Product_ID in (SELECT Product_ID FROM dbo.product_online_v_1 WHERE Stock>0)";
            }
            else if (comboBoxEx2.Text == "无")
            {
                sqlext += " and Product_ID not in (SELECT Product_ID FROM dbo.product_online_v_1 WHERE Stock>0)";
            }


            if (comboBoxEx6.Text == "是")
            {
                sqlext += " and isLiuXiang=1";
            }
            else if (comboBoxEx6.Text == "否")
            {
                sqlext += " and isLiuXiang=0";
            }
                       
            Bind();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            search();
        }




        private void textBoxX2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void textBoxX3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void textBoxX4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void comboBoxEx1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void comboBoxEx2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void comboBoxEx3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }

        private void comboBoxEx6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                search();
            }
        }
    }

}

