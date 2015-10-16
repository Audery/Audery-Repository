using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL
{
    public class PowerPass
    {
        public PowerEnum PowerList(List<SOSOshop.Model.Roles_Permissions> rpList)
        {
            PowerEnum pp = new PowerEnum();
            int depth = 0;
            PowerNode pn2 = null, pn3 = null;
            PowerNode pn = null;
            bool bl2 = true, bl3 = true;//true表示有子项
            foreach (SOSOshop.Model.Roles_Permissions rpModel in rpList)
            {
                depth = GetDepth(rpModel.OperateCode);
                pn = new PowerNode(rpModel.OperateCode.ToString(), rpModel.OperateCode.ToString(), depth);
                switch (depth)
                {
                    case 1:
                        if (pn2 != null)
                        {
                            pn.ParNode = pn2;
                            pn2.ChildNodes.Add(pn);//此对象不必附加子对象,指定当前的pn2为其父对象,并添加进去
                        }
                        else
                        {
                            bl2 = true;
                            if (pn3 != null)
                            {
                                pn.ParNode = pn3;//直接把该对象附加给pn3,因为此时pn2为空
                                pn3.ChildNodes.Add(pn);
                            }
                        }
                        break;
                    case 2:
                        if (bl2 && pn3 != null)
                        {
                            pn.ParNode = pn3;
                            pn2 = pn;//存pn2中,子对象没有附加
                            bl2 = false;
                        }
                        else//出现新的pn2(pn3的子对象)对象.pn3还存在,但是bl2为false,因为它已附加了子对象,不管是否附加完整
                        {
                            if (pn2 != null)
                            {
                                pn3.ChildNodes.Add(pn2);//old把已经存在的pn2附加到pn3中
                            }
                            pn.ParNode = pn3;//设置新的pn2对象
                            pn2 = pn;//new
                        }
                        break;
                    case 3:
                        if (bl3)
                        {
                            pn.ParNode = null;
                            pn3 = pn;//存于pn3实例中,子对象没有附加
                            bl3 = false;
                        }
                        else//已经为pn3附加子对象,或者为其附加了孙对象
                        {
                            if (pn2 != null)//存在没有附加了pn2对象
                            {
                                pn3.ChildNodes.Add(pn2);
                                pn2 = null;
                                bl2 = true;
                            }
                            pp.PNodes.Add(pn3);//把pn3附加到节点树中

                            pn.ParNode = null;
                            pn3 = pn;
                        }
                        break;
                }
                pn = null;
            }
            if (pn3 != null && pn2 != null)
            {
                pn3.ChildNodes.Add(pn2);
                pn2 = null;
                bl2 = true;
            }
            if (pn2 != null)
            {
                pp.PNodes.Add(pn2);
                pn2 = null;
                bl2 = false;
            }
            if (pn2 == null && pn3 != null)
            {
                pp.PNodes.Add(pn3);
            }
            return pp;
        }
        private int GetDepth(int value)
        {
            int depth = 0;
            string v = value.ToString();
            int l = v.Length;
            if (l < 9)
            {
                for (int i = 0; i < (9 - l); i++)
                {
                    v = "0" + v;
                }
            }
            string[] arr = new string[3];
            arr[0] = v.Substring(0, 3);
            arr[1] = v.Substring(3, 3);
            arr[2] = v.Substring(6, 3);
            if (arr[2] == "000" && arr[1] == "000")
            {
                depth = 3;
            }
            else if (arr[2] == "000" && arr[1] != "000")
            {
                depth = 2;
            }
            else if (arr[2] != "000")
            {
                depth = 1;
            }
            return depth;
        }
        private static int GetDepth(string v)
        {
            int depth = 0;
            if (v.Length != 9)
            {
                return depth;
            }
            string[] arr = new string[3];
            arr[0] = v.Substring(0, 3);
            arr[1] = v.Substring(3, 3);
            arr[2] = v.Substring(6, 3);
            if (arr[2] == "000" && arr[1] == "000")
            {
                depth = 3;
            }
            else if (arr[2] == "000" && arr[1] != "000")
            {
                depth = 2;
            }
            else if (arr[2] != "000")
            {
                depth = 1;
            }
            return depth;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="powerStr">权限编号</param>
        /// <param name="pType">类型：add, update, look, del, other </param>
        /// <returns></returns>
        public static bool isPass(string powerStr)
        {
            int depth = 0;
            depth = GetDepth(powerStr);
            SOSOshop.Model.AdminInfo aInfo = AdministrorManager.Get();
            if (aInfo != null)
            {
                if (aInfo.AdminPowerType == "all")
                {
                    return true;
                }
                if (aInfo.AdminRole != null)
                {
                    string RoleStr = aInfo.AdminRole;
                    if (RoleStr != string.Empty)
                    {
                        Roles_Permissions rbll = new Roles_Permissions();
                        List<SOSOshop.Model.Roles_Permissions> list = rbll.GetListByColumn(" id in ('" + RoleStr.Replace(",", "','") + "') and operatecode=" + powerStr + "");
                        if (list != null && list.Count > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 只有超级管理员才有的权限
        /// </summary>
        /// <returns></returns>
        public static bool isPass()
        {
            bool bl = false;
            SOSOshop.Model.AdminInfo aInfo = AdministrorManager.Get();
            if (aInfo != null && !string.IsNullOrEmpty(aInfo.AdminPowerType))
            {
                string[] powerStrArr = aInfo.AdminPowerType.Split(',');
                string powerTypeStr = string.Empty;
                for (int i = 0; i < powerStrArr.Length; i++)
                {
                    if (powerStrArr[i] == "AllPower")
                    {
                        bl = true;
                        break;
                    }
                }
            }
            return bl;
        }
        private static bool isNumber(string str)
        {
            if (string.IsNullOrEmpty(str)) { return false; }
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]+$");
            return r.IsMatch(str);
        }
    }
    public class PowerNode
    {
        public PowerNode(string nodeValue, string nodeText, int nodeDepth)
        {
            this._NodeValue = nodeValue;
            this._NodeText = nodeText;
            this._NodeDepth = nodeDepth;
        }
        private Nodes _ChildNodes;

        public Nodes ChildNodes
        {
            get
            {
                if (_ChildNodes == null)
                {
                    _ChildNodes = new Nodes();
                }
                return _ChildNodes;
            }
        }
        private PowerNode _ParNode;

        public PowerNode ParNode
        {
            get { return _ParNode; }
            set { _ParNode = value; }
        }
        private int _NodeDepth;

        public int NodeDepth
        {
            get { return _NodeDepth; }
        }

        private string _NodeValue;

        public string NodeValue
        {
            get { return _NodeValue; }
        }
        private string _NodeText;

        public string NodeText
        {
            get { return _NodeText; }
        }
    }
    public class PowerEnum
    {
        //查看,增加,修改,删除,增加和删除,增加和修改,修改和删除,所有,其它
        public enum PowerType { add, update, look, del, deladd, delupdate, updateadd, all, other }
        public PowerEnum()
        {

        }
        private Nodes _PNodes;

        public Nodes PNodes
        {
            get
            {
                if (_PNodes == null)
                {
                    _PNodes = new Nodes();
                }
                return _PNodes;
            }
            set { _PNodes = value; }
        }
        public PowerNode FindNode(PowerNode pNode)
        {
            return _PNodes.FindNode(this.PNodes, pNode, new PowerPath(pNode));
        }
        ~PowerEnum()
        {
            _PNodes = null;
        }
    }
    public class Nodes
    {
        private List<PowerNode> _PowerList;
        public int Count
        {
            get { return _PowerList.Count; }
        }
        public Nodes()
        {
            _PowerList = new List<PowerNode>();
        }
        public Nodes(int maxCount)
        {
            _PowerList = new List<PowerNode>(maxCount);
        }
        public void Add(PowerNode pNode)
        {
            _PowerList.Add(pNode);
        }
        public void AddAd(int index, PowerNode pNode)
        {
            if (index >= 0 && index < this.Count)
            {
                _PowerList.Insert(index, pNode);
            }
        }
        public void Remove(PowerNode pNode)
        {
            _PowerList.Remove(pNode);
        }
        public void RemoveAd(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                _PowerList.RemoveAt(index);
            }
        }
        public void Clear()
        {
            _PowerList.Clear();
        }
        public PowerNode FindNode(Nodes nodes, PowerNode pNode, PowerPath pPath)
        {
            PowerNode reNode = null;
            foreach (PowerNode node in nodes._PowerList)
            {
                if (!string.IsNullOrEmpty(pPath.PathNode3) && node.NodeDepth == 3)
                {
                    if (node.NodeValue != pPath.PathNode3)
                    {
                        continue;
                    }
                }
                else if (!string.IsNullOrEmpty(pPath.PathNode2) && node.NodeDepth == 2)
                {
                    if (node.NodeValue != pPath.PathNode2)
                    {
                        continue;
                    }
                }
                if (node.NodeDepth == pNode.NodeDepth)
                {
                    if (node.NodeText == pNode.NodeText && node.NodeValue == pNode.NodeValue)
                    {
                        reNode = node;
                        break;
                    }
                }
                else if (node.ChildNodes.Count > 0)
                {
                    reNode = FindNode(node.ChildNodes, pNode, pPath);
                    if (reNode != null)
                        break;
                }
            }
            return reNode;
        }
        public PowerNode this[int index]
        {
            get
            {
                PowerNode[] pnArr = _PowerList.ToArray();
                if (index > 0 && index < this.Count)
                {
                    return pnArr[index];
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class PowerPath
    {
        private string _PathNode3;

        public string PathNode3
        {
            get { return _PathNode3; }
            set { _PathNode3 = _pNode.NodeValue.Substring(0, _pNode.NodeValue.Length - 6) + "000000"; }
        }
        private string _PathNode2;

        public string PathNode2
        {
            get { return _PathNode2; }
            set { _PathNode2 = _pNode.NodeValue.Substring(0, _pNode.NodeValue.Length - 3) + "000"; }
        }
        private string _PathNode1;

        public string PathNode1
        {
            get { return _PathNode1; }
            set { _PathNode1 = _pNode.NodeValue; }
        }
        private PowerNode _pNode;
        public PowerPath(PowerNode pNode)
        {
            _pNode = pNode;
        }
    }
}
