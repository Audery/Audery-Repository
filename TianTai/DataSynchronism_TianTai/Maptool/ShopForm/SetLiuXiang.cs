using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maptool.ShopForm
{
    public partial class SetLiuXiang : Form
    {

        soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
        /// <summary>
        /// 记录控制流向的地区
        /// </summary>
        DataTable selectedRegion = new DataTable();
        /// <summary>
        /// 要设定销售流向的商品
        /// </summary>
        public string pid { get; set; }

        public SetLiuXiang()
        {
            InitializeComponent();
            AddColumns();
        }
        string[] addr_idList = new string[] { };

        private void AddColumns()
        {
            if (!selectedRegion.Columns.Contains("ID"))
            {
                selectedRegion.Columns.Add("ID", typeof(string));
                DataColumn[] cols = new DataColumn[] { selectedRegion.Columns["ID"] };
                selectedRegion.PrimaryKey = cols;
            }
            if (!selectedRegion.Columns.Contains("Name"))
            {
                selectedRegion.Columns.Add("Name", typeof(string));
            }
            if (!selectedRegion.Columns.Contains("EnglishName"))
            {
                selectedRegion.Columns.Add("EnglishName", typeof(string));
            }
            if (!selectedRegion.Columns.Contains("ParentId"))
            {
                selectedRegion.Columns.Add("ParentId", typeof(int));
            }
            if (!selectedRegion.Columns.Contains("ParentPath"))
            {
                selectedRegion.Columns.Add("ParentPath", typeof(string));
            }
            if (!selectedRegion.Columns.Contains("Depth"))
            {
                selectedRegion.Columns.Add("Depth", typeof(int));
            }
            if (!selectedRegion.Columns.Contains("OrderID"))
            {
                selectedRegion.Columns.Add("OrderID", typeof(int));
            }
            if (!selectedRegion.Columns.Contains("Child"))
            {
                selectedRegion.Columns.Add("Child", typeof(int));
            }
            if (!selectedRegion.Columns.Contains("IsUse"))
            {
                selectedRegion.Columns.Add("IsUse", typeof(int));
            }
            if (!selectedRegion.Columns.Contains("AddDate"))
            {
                selectedRegion.Columns.Add("AddDate", typeof(DateTime));
            }
        }


        private void SetLiuXiang_Load(object sender, EventArgs e)
        {
            //只支持区域流向

            this.Text = "设置区域流向控制";
            DataTable Region = bll.GetRegionAll();
            DataTable dt = bll.GetQuYuLiuXiang(pid, Login.authKey);


            string addr_id = dt.Rows.Count > 0 ? dt.Rows[0]["addr_id"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(addr_id))
            {
                addr_idList = addr_id.Split(',');
            }

            treeView1.CheckBoxes = true;
            foreach (DataRow item in Region.Select("ParentId=0"))
            {
                //绑定根节点
                TreeNode RootNode = new TreeNode();
                RootNode.Tag = item;
                RootNode.Text = item["Name"].ToString();

                if (addr_idList.Contains(item["ID"].ToString()))
                {
                    //此产品已经设置流向的区域

                    if (RootNode.Parent != null && RootNode.Parent.IsExpanded)
                    {
                        RootNode.Parent.Expand();
                    }
                    RootNode.ExpandAll();
                    RootNode.Checked = true;

                    //添加已选记录到selectedRegion
                    selectedRegion.Rows.Add(item.ItemArray);

                }

                treeView1.Nodes.Add(RootNode);

                BindChildAreas(RootNode, Region);
            }


        }

        /// <summary>
        /// 递归绑定子区域
        /// </summary>
        /// <param name="fNode"></param>
        /// <param name="dt"></param>

        private void BindChildAreas(TreeNode fNode, DataTable Region)
        {
            try
            {

                DataRow dr = (DataRow)fNode.Tag;
                //父节点数据关联的数据行           
                int ParentID = Convert.ToInt32(dr["ID"]);
                //用父节点ID查询子区域
                var DataRows = Region.Select(string.Format("ParentId={0}", ParentID));

                if (DataRows.Count() == 0)  //递归终止，区域不包含子区域时          
                {
                    return;
                }
                foreach (DataRow dRow in DataRows)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = dRow;
                    node.Text = dRow["Name"].ToString(); //添加子点              
                    fNode.Nodes.Add(node);                //递归   

                    if (addr_idList.Contains(dRow["ID"].ToString()))
                    {
                        //此产品已经设置流向的区域

                        node.Checked = true;

                        ExpandAllParentNote(node);

                        //添加已选记录到selectedRegion
                        selectedRegion.Rows.Add(dRow.ItemArray);

                    }
                    BindChildAreas(node, Region);
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        /// <summary>
        /// 展开父节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandAllParentNote(TreeNode node)
        {
            if (node.Parent != null && !node.Parent.IsExpanded)
            {
                node.Parent.Expand();
                ExpandAllParentNote(node.Parent);
            }
        }

        /// <summary>
        /// 当已选中或者取消选择节点上的复选框时候执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetLiuXiang_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                var TmpDR = e.Node.Tag as DataRow;
                if (e.Node.Checked)
                {

                    //将选择的结果更新到区域列表中
                    if (!selectedRegion.Rows.Contains(TmpDR["ID"]))
                    {
                        //如果不包含此节点的数据行，则增加                           
                        selectedRegion.Rows.Add(TmpDR.ItemArray);
                    }


                    //选中节点之后，将所有子节点选中
                    setChildNodeCheckedState(e.Node, true);

                }
                else
                {
                    if (selectedRegion.Rows.Contains(TmpDR["ID"]))
                    {
                        DataRow selectedRegionRow = selectedRegion.Rows.Find(TmpDR["ID"]);
                        //如果包含此节点的数据行，则删除
                        selectedRegion.Rows.Remove(selectedRegionRow);
                    }
                    //取消节点选中状态之后，取消所有父节点的选中状态
                    setChildNodeCheckedState(e.Node, false);
                    //如果节点存在父节点，取消父节点的选中状态
                    if (e.Node.Parent != null)
                    {
                        setParentNodeCheckedState(e.Node, false);
                    }
                }
            }
        }

        /// <summary>
        /// 设置父节点状态
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="state"></param>
        private void setParentNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNode parentNode = currNode.Parent;
            if (!state)
            {
                var TmpDR = parentNode.Tag as DataRow;
                //将选择的结果更新到区域列表中
                if (selectedRegion.Rows.Contains(TmpDR["ID"]))
                {
                    DataRow selectedRegionRow = selectedRegion.Rows.Find(TmpDR["ID"]);
                    //如果包含此节点的数据行，则删除
                    selectedRegion.Rows.Remove(selectedRegionRow);
                }
            }

            parentNode.Checked = state;
            if (currNode.Parent.Parent != null)
            {
                setParentNodeCheckedState(currNode.Parent, state);
            }
        }


        /// <summary>
        /// 设置所有子节点状态
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="state"></param>
        private void setChildNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
                foreach (TreeNode tn in nodes)
                {

                    tn.Checked = state;
                    var TmpDR = tn.Tag as DataRow;
                    //将选择的结果更新到区域列表中
                    if (state)
                    {

                        if (!selectedRegion.Rows.Contains(TmpDR["ID"]))
                        {
                            //如果不包含此节点的数据行，则增加                           
                            selectedRegion.Rows.Add(TmpDR.ItemArray);
                        }

                    }
                    else
                    {
                        if (selectedRegion.Rows.Contains(TmpDR["ID"]))
                        {
                            DataRow selectedRegionRow = selectedRegion.Rows.Find(TmpDR["ID"]);
                            //如果包含此节点的数据行，则删除
                            selectedRegion.Rows.Remove(selectedRegionRow);
                        }
                    }
                    setChildNodeCheckedState(tn, state);
                }
        }

        /// <summary>
        /// 查询treeNode节点下有多少节点被选中（递归实现，不受级数限制）
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        private int GetNodeChecked(TreeNode tv)
        {
            int x = 0;
            if (tv.Checked)
            {
                x++;
            }
            foreach (TreeNode item in tv.Nodes)
            {
                x += GetNodeChecked(item);

            }
            return x;

        }

        /// <summary>
        /// 查询TreeView下节点被checked的数目
        /// </summary>
        /// <param name="treev"></param>
        /// <returns></returns>
        private int GetTreeViewNodeChecked(TreeView treev)
        {
            int k = 0;
            foreach (TreeNode item in treev.Nodes)
            {
                k += GetNodeChecked(item);
            }
            return k;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int way = 0;
            if (radioButton2.Checked)
            {
                way = 1;
            }

            List<string> aid = new List<string>();


            foreach (DataRow dr in selectedRegion.Rows)
            {
                aid.Add(dr["ID"].ToString());
            }
            if (aid.Count > 0)
            {
                string Msg = bll.SetLiuXiang(pid, aid.ToArray(), 0, way, Login.authKey);
                MessageBox.Show(Msg);
            }
            else
            {
                bool Result = bll.SetCancelLiuXiang(pid, Login.authKey);

                MessageBox.Show(Result ? "取消成功" : "取消失败");
            }
        }

        //private void button6_Click(object sender, EventArgs e)
        //{

        //    if (!string.IsNullOrEmpty(pid))
        //    {
        //        if (bll.SetCancelLiuXiang(new string[] { pid }, 0, Login.authKey))
        //        {
        //            MessageBox.Show("取消成功！");
        //            this.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("取消失败！");
        //        }
        //    }
        //}

    }
}
