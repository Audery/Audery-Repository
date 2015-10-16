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
    public partial class MappingSet : DevComponents.DotNetBar.Office2007Form
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool order { get; set; }
        public string orderField { get; set; }
        public bool like { get; set; }
        public string whereField { get; set; }
        public string whereString { get; set; }
        public int recordCount { get; set; }
        public int pageCount { get; set; }
        public string Erp_ID { get; set; }
        public string sql = "";
        /// <summary>
        /// 合作企业编号
        /// </summary>
        public int iden { get; set; }
        /// <summary>
        /// 底价
        /// </summary>
        public decimal price { get; set; }
        public Mapping from1;
        public MappingSet()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            dataGridViewX1.CellContentClick += new DataGridViewCellEventHandler(dataGridViewX1_CellContentClick);
            //dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewX1.DataError += new DataGridViewDataErrorEventHandler(dataGridViewX1_DataError);

            //Yj2015.0226 Add 双击通用名显示图片
            dataGridViewX1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewX1_CellDoubleClick);
        }
        public void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("输入格式不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
        }

        //Yj2015.0226 Add 双击通用名显示图片 ---
        void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns["Column1"].Index)
            {
                int goodsPackageId = 0;
                int goodsId = 0;
                if (int.TryParse(dataGridViewX1.Rows[e.RowIndex].Cells["Product_ID"].Value.ToString(), out goodsPackageId)
                   && int.TryParse(dataGridViewX1.Rows[e.RowIndex].Cells["Goods_Id"].Value.ToString(), out goodsId))
                {
                    soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                    string imgUrl = bll.GetImageUrl(goodsId, goodsPackageId);

                    string frmCaption = dataGridViewX1.Rows[e.RowIndex].Cells["Column1"].Value.ToString()
                                        + "—"
                                        + dataGridViewX1.Rows[e.RowIndex].Cells["Column2"].Value.ToString()
                                        + "—"
                                        + dataGridViewX1.Rows[e.RowIndex].Cells["Column4"].Value.ToString();

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

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns[0].Index)
            {
                if (DialogResult.OK == MessageBox.Show("您是否确认映射？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    
                  
                    soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                    
                    Maptool.soso.Link model = new soso.Link();
                    model.created = DateTime.Now;
                    model.updated = model.created;
                    model.id = int.Parse(dataGridViewX1.Rows[e.RowIndex].Cells["Product_ID"].Value.ToString());
                    model.iden = this.iden;                   
                    model.t_id = this.Erp_ID;
                    if (bll.AddLink(model, Login.authKey))
                    {
                        MessageBox.Show("操作成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        from1.Bind();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("您可能已经映射过此供应商的数据!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        ///// <summary>
        ///// 批文
        ///// </summary>
        //public string DrugsBase_ApprovalNumber { get; set; }
        /// <summary>
        /// 通用名
        /// </summary>
        public string DrugsBase_DrugName { get; set; }
        public string Goods_ConveRatio { get; set; }
        public new void ShowDialog()
        {
            PageSize = 20;
            PageIndex = 1;
            textBoxX3.Text = DrugsBase_DrugName;
            orderField = "Product_ID";
            this.Text = string.Format("{0}-转换比{1}－数据映射", DrugsBase_DrugName, Goods_ConveRatio);
            sql = null;
            buttonX1_Click(null, null);
            base.ShowDialog();
        }
        public void Bind()
        {

            dataGridViewX1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "产地", DataPropertyName = "ProductionAddress", Name = "ProductionAddress" });
            dataGridViewX1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "片型", Name = "DrugsBase_Formulation2", });
            dataGridViewX1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "制法", Name = "ProductionMethodName", });
            dataGridViewX1.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "等级", Name = "ProductionLevelName", });


            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            
            var li = bll.GetListAll101(PageSize, PageIndex, order, orderField, like, whereField, whereString, sql, Login.authKey);
            if (li != null)
            {
                dataGridViewX1.Rows.Clear();
                for (int i = 0; i < li.Tables[0].Rows.Count; i++)
                {
                    dataGridViewX1.Rows.Add();

                    dataGridViewX1.Rows[i].Cells["ProductionAddress"].Value = li.Tables[0].Rows[i]["ProductionAddress"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["DrugsBase_Formulation2"].Value = li.Tables[0].Rows[i]["DrugsBase_Formulation2"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["ProductionMethodName"].Value = li.Tables[0].Rows[i]["ProductionMethodName"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["ProductionLevelName"].Value = li.Tables[0].Rows[i]["ProductionLevelName"].ToString().Trim();

                    dataGridViewX1.Rows[i].Cells["Column9"].Value = li.Tables[0].Rows[i]["Product_ID"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Column1"].Value = li.Tables[0].Rows[i]["DrugsBase_DrugName"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Column1"].ToolTipText = "双击查看图片";
                    dataGridViewX1.Rows[i].Cells["Column2"].Value = li.Tables[0].Rows[i]["DrugsBase_Specification"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Column3"].Value = li.Tables[0].Rows[i]["DrugsBase_Formulation"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells[6].Value = li.Tables[0].Rows[i]["DrugsBase_Manufacturer"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells[7].Value = li.Tables[0].Rows[i]["DrugsBase_ApprovalNumber"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells[8].Value = li.Tables[0].Rows[i]["Registration_No"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Goods_Unit"].Value = li.Tables[0].Rows[i]["Goods_Unit"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Goods_Pcs"].Value = li.Tables[0].Rows[i]["Goods_Pcs"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Goods_Id"].Value = li.Tables[0].Rows[i]["Goods_Id"].ToString().Trim();

                    dataGridViewX1.Rows[i].Cells["Goods_Pcs_Small"].Value = li.Tables[0].Rows[i]["Goods_Pcs_Small"].ToString().Trim();
                    var dr = li.Tables[0].Rows[i];
                    dataGridViewX1.Rows[i].Cells["Column6"].Value = string.Format("{0}{1}/{2}", dr["Goods_ConveRatio"], dr["Goods_ConveRatio_Unit"], dr["Goods_Unit"]);
                    dataGridViewX1.Rows[i].Cells["Product_ID"].Value = li.Tables[0].Rows[i]["Product_ID"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Column8"].ToolTipText = li.Tables[0].Rows[i]["Product_ID"].ToString().Trim();
                    dataGridViewX1.Rows[i].Cells["Column7"].Value = dr["Goods_ConveRatio"].ToString();
                    
                  
                }
                recordCount = int.Parse(li.Tables[1].Rows[0]["recordCount"].ToString());
                pageCount = int.Parse(li.Tables[1].Rows[0]["pageCount"].ToString());
                paginger();

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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (PageIndex == 1) return;
            PageIndex--;
            Bind();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (PageIndex == pageCount) return;
            PageIndex++;
            Bind();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            sql = "";
            if (!string.IsNullOrEmpty(textBoxX3.Text))
            {
                sql += " and DrugsBase_DrugName like('%" + textBoxX3.Text + "%')";
            }
            if (!string.IsNullOrEmpty(textBoxX2.Text))
            {
                sql += " and DrugsBase_Specification like('%" + textBoxX2.Text + "%')";
            }
            if (!string.IsNullOrEmpty(textBoxX4.Text))
            {
                sql += " and DrugsBase_Manufacturer like('%" + textBoxX4.Text + "%')";
            }
            if (!string.IsNullOrEmpty(textBoxX1.Text))
            {
                sql += " and DrugsBase_ApprovalNumber like('%" + textBoxX1.Text + "%')";
            }
            Bind();
        }
    }
}
