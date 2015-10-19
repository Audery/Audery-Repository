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
    public partial class frmCheckData : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 绑定的数据类型：0-全部，1-数据标准化（中包装数大于零）
        /// </summary>
        private int BinType = 0;

        /// <summary>
        /// 合作企业编号
        /// </summary>
        private int iden = -1;

        /// <summary>
        /// 数据检查类别：1-销售单位，2-批准文号
        /// </summary>
        private CheckDataType FormCheckType { get; set; }

        private int PageSize = 0;
        private int PageIndex = 1;
        private bool order = false;
        private string orderField = "Erp_Id";
        private bool like = true;
        private string whereField = "DrugsBase_DrugName";
        //private string whereString = "";
        private int recordCount = 0;
        private int pageCount = 0;

        private string sqlext = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmCheckData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pHeight">窗体高度</param>
        /// <param name="iden">合作企业编号</param>
        /// <param name="binType">绑定的数据类型：0-全部，1-数据标准化（中包装数大于零）</param>
        /// <param name="checkType">数据检查类别：1-销售单位，2-批准文号</param>
        public frmCheckData(int pHeight, int iden, int binType, CheckDataType checkType)
        {
            InitializeComponent();

            InitFormInfo(pHeight, iden, binType, checkType);
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="pHeight">窗体高度</param>
        /// <param name="iden">合作企业编号</param>
        /// <param name="binType">绑定的数据类型：0-全部，1-数据标准化（中包装数大于零）</param>
        /// <param name="checkType">数据检查类别：1-销售单位，2-批准文号</param>
        private void InitFormInfo(int pHeight, int iden, int binType, CheckDataType checkType)
        {
            this.ShowInTaskbar = false;
            this.TopLevel = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.dataGridViewX1.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewX1_CellValueChanged);
            PageSize = (pHeight - 210) / 21;

            this.iden = iden;
            this.BinType = binType;
            this.FormCheckType = checkType;

            string msg = Main.Menu.First(x => x.id == iden).name.Replace("数据映射", "");
            grpInfo.Text = "数据检查—" + checkType.ToString() + "(" + msg + ")";

            if (checkType == CheckDataType.批准文号)
            {
                dataGridViewX1.Columns["StockNumber"].Visible = false;
                dataGridViewX1.Columns["StockTypes"].Visible = false;
                dataGridViewX1.Columns["PriceTypes"].Visible = false;

                grpCheckApprovalNumber.Visible = true;
                grpCheckGoodsUnit.Visible = false;

                InitComboBox(cmbApprovalNumber);
            }
            else if (checkType == CheckDataType.销售单位)
            {
                dataGridViewX1.Columns["StockNumber"].Visible = true;
                dataGridViewX1.Columns["StockTypes"].Visible = true;
                dataGridViewX1.Columns["PriceTypes"].Visible = true;

                grpCheckApprovalNumber.Visible = false;
                grpCheckGoodsUnit.Visible = true;

                InitComboBox(cmbGoodsUnit);
            }

        }

        void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.Columns[e.ColumnIndex].Name.ToLower() == "remark")
            {
                var TempObj = dataGridViewX1.Rows[e.RowIndex].Cells["remark"].Value;
                string Remark = TempObj == null ? string.Empty : TempObj.ToString();
                string ERP_ID = dataGridViewX1.Rows[e.RowIndex].Cells["ERP_Id"].Value.ToString();
                soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
                bll.SetTag_2(ERP_ID, iden, Remark, Login.authKey);
            }
        }

        /// <summary>
        /// 初始化检查条件下拉框
        /// </summary>
        /// <param name="cmbObj"></param>
        private void InitComboBox(ComboBox cmbObj)
        {
            try
            {
                string[] strs = Enum.GetNames(typeof(CheckCondition));

                int len = strs.Count<string>();
                if (len > 0)
                {
                    cmbObj.Items.Clear();
                    for (int i = 0; i < len; i++)
                    {
                        cmbObj.Items.Add(strs[i]);
                    }

                    cmbObj.Text = CheckCondition.忽略.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("初始化下拉框出错!" + e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Bind()
        {
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            var li = bll.GetCheckDataInfo(this.iden, PageSize, PageIndex, order, orderField, like, whereField, sqlext, Login.authKey);

            if (li != null)
            {
                li.Tables[0].Columns.Remove("row");
                dataGridViewX1.DataSource = li.Tables[0];
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            sqlext = "";

            //ERP数据
            if (txtErpId.Text.Trim() != "")
            {
                sqlext = " and Erp_Id like('%" + txtErpId.Text.Trim() + "%')";
            }
            if (txtErpDrugName.Text.Trim() != "")
            {
                sqlext = " and Erp_DrugName like('%" + txtErpDrugName.Text.Trim() + "%')";
            }
            if (txtErpManufacturer.Text.Trim() != "")
            {
                sqlext += " and Erp_Manufacturer like('%" + txtErpManufacturer.Text.Trim() + "%')";
            }
            if (txtErpApprovalNumber.Text.Trim() != "")
            {
                sqlext += " and Erp_ApprovalNumber like('%" + txtErpApprovalNumber.Text.Trim() + "%')";
            }

            //基础数据平台
            if (txtGoodsPackageId.Text.Trim() != "")
            {
                sqlext = " and DrugsBase_GoodsPackageId like('%" + txtGoodsPackageId.Text.Trim() + "%')";
            }
            if (txtDrugsBaseName.Text.Trim() != "")
            {
                sqlext = " and DrugsBase_DrugName like('%" + txtDrugsBaseName.Text.Trim() + "%')";
            }
            if (txtDrugsBaseManufacturer.Text.Trim() != "")
            {
                sqlext += " and DrugsBase_Manufacturer like('%" + txtDrugsBaseManufacturer.Text.Trim() + "%')";
            }
            if (txtDrugsBaseApprovalNumber.Text.Trim() != "")
            {
                sqlext += " and DrugsBase_ApprovalNumber like('%" + txtDrugsBaseApprovalNumber.Text.Trim() + "%')";
            }

            if (this.FormCheckType == CheckDataType.批准文号)
            {
                if (cmbApprovalNumber.Text == CheckCondition.忽略.ToString())
                {
                    ;
                }
                else if (cmbApprovalNumber.Text == CheckCondition.不一致.ToString())
                {
                    sqlext += " AND Erp_ApprovalNumber!=DrugsBase_ApprovalNumber ";
                }
                else if (cmbApprovalNumber.Text == CheckCondition.一致.ToString())
                {
                    sqlext += " AND Erp_ApprovalNumber=DrugsBase_ApprovalNumber ";
                }
            }
            else if (this.FormCheckType == CheckDataType.销售单位)
            {
                if (cmbGoodsUnit.Text == CheckCondition.忽略.ToString())
                {
                    ;
                }
                else if (cmbGoodsUnit.Text == CheckCondition.不一致.ToString())
                {
                    sqlext += " AND Erp_GoodsUnit!=DrugsBase_GoodsUnit ";
                }
                else if (cmbGoodsUnit.Text == CheckCondition.一致.ToString())
                {
                    sqlext += " AND Erp_GoodsUnit=DrugsBase_GoodsUnit ";
                }
            }

            Bind();
        }

        /// <summary>
        /// 初始化分页状态
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        private void paginger()
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

        /// <summary>
        /// GO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = int.Parse(textBoxX1.Text);
                Bind();

            }
            catch { }
        }
    }

    /// <summary>
    /// 数据检查类别
    /// </summary>
    public enum CheckDataType
    {
        销售单位 = 1,
        批准文号 = 2
    }

    /// <summary>
    /// 数据检查条件
    /// </summary>
    public enum CheckCondition
    {
        忽略 = 1,
        不一致 = 2,
        一致 = 3
    }
}
