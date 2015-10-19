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
    public partial class Config : DevComponents.DotNetBar.Office2007Form
    {
        string _iden;
        public Config(string iden)
        {
            _iden = iden;
            InitializeComponent();
            this.ShowInTaskbar = false;
            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.RowHeadersVisible = false;

            dataGridViewX1.DataError += new DataGridViewDataErrorEventHandler(dataGridViewX1_DataError);
            var model = new soso.syntoolSoapClient().GetAllConfigPriceMe(Login.authKey);
            if (model != null)
            {
                dataGridViewX1.DataSource = model;
            }

        }

        void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var NewData = dataGridViewX1.DataSource as Maptool.soso.ConfigPriceMe[];
            if (NewData != null)
            {
                //需要更新的价格类型
                DataTable dt = new DataTable();
                dt.Columns.Add("Price_Plus", typeof(decimal));
                dt.Columns.Add("CateGory", typeof(string));
                dt.TableName = "Table_PricePlus";
                foreach (var item in NewData)
                {
                    string Name = item.name;
                    float Price_Plus = item.Price_Plus;
                    if (!string.IsNullOrEmpty(Name) && Price_Plus > 0)
                    {
                        var dr = dt.NewRow();
                        dr["Price_Plus"] = Price_Plus;
                        dr["CateGory"] = Name;
                        dt.Rows.Add(dr);
                    }
                }
                soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                bool Result = bll.SetConfigPriceMe(Login.authKey, dt);
                if (Result)
                {
                    MessageBox.Show("保存成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
                this.Close();
            }
            
        }

    }
}
