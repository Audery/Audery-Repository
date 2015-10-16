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
    public partial class Mapping_PricePlus : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 客户产品ID
        /// </summary>
        public string ERP_ID { get; set; }

        public Mapping_PricePlus()
        {
            InitializeComponent();
            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.RowHeadersVisible = false;
            dataGridViewX1.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewX1_CellValueChanged);
        }

        public void Bind()
        {
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            var li = bll.GetPriceByID(ERP_ID);
            if (li != null && li.Rows.Count > 0)
            {
                li.Columns.Add("Price_Sale", typeof(string));
                decimal Price = 0;
                decimal Price_Plus = 0;
                foreach (DataRow item in li.Rows)
                {
                    Price = item["Price"] != null ? Convert.ToDecimal(item["Price"]) : 0;
                    Price_Plus = item["Price_Plus"] != null ? Convert.ToDecimal(item["Price_Plus"]) : 0;
                    item["Price_Sale"] = Math.Round(Price * Price_Plus, 2);
                    item["Price_Plus"] = Math.Round(Price_Plus, 2);
                }
                dataGridViewX1.DataSource = li;
            }


        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //修改的是加点
            if (e.ColumnIndex == dataGridViewX1.Columns["Price_Plus"].Index)
            {
                soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                string ID = dataGridViewX1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string CateGory = dataGridViewX1.Rows[e.RowIndex].Cells["CateGory"].Value.ToString();
                Decimal Price_Plus = dataGridViewX1.Rows[e.RowIndex].Cells["Price_Plus"].Value == null ? 0 : Convert.ToDecimal(dataGridViewX1.Rows[e.RowIndex].Cells["Price_Plus"].Value);
                bll.SetPricePlus(ID, Price_Plus, CateGory);
            }
            //修改的是售价
            if (e.ColumnIndex == dataGridViewX1.Columns["Price_Sale"].Index)
            {
                soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                string ID = dataGridViewX1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string CateGory = dataGridViewX1.Rows[e.RowIndex].Cells["CateGory"].Value.ToString();
                Decimal Price = dataGridViewX1.Rows[e.RowIndex].Cells["Price"].Value == null ? 0 : Convert.ToDecimal(dataGridViewX1.Rows[e.RowIndex].Cells["Price"].Value);
                Decimal Price_Sale = dataGridViewX1.Rows[e.RowIndex].Cells["Price_Sale"].Value == null ? 0 : Convert.ToDecimal(dataGridViewX1.Rows[e.RowIndex].Cells["Price_Sale"].Value);
                decimal Price_Plus = Price_Sale / Price;
                bll.SetPricePlus(ID, Price_Plus, CateGory);
            }
            //重新绑定数据
            Bind();
        }
    }
}
