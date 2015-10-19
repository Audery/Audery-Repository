using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace _101shop.admin.v3.accessories
{
    public partial class sms_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("012005001");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormString("id") != string.Empty)
                {
                    string types = Request.Form["Option"].Trim();
                    string StrID = ChangeHope.WebPage.PageRequest.GetFormString("id");
                    if (types == "del")
                    {
                        if (SOSOshop.BLL.PowerPass.isPass("012005003"))
                        {
                            Del(StrID);
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    Response.End();
                    return;
                }
                this.lblList.Text = GetList();
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        protected string GetList()
        {
            ChangeHope.WebPage.Table table = new ChangeHope.WebPage.Table();
            YXShop.BLL.Accessories.Sms bll = new YXShop.BLL.Accessories.Sms();
            string sqlWhere = "1=1";
            #region 时间
            DateTime now = DateTime.Now; DateTime from = now; DateTime to = now;
            try { if (fromOrderDate.Text != null && fromOrderDate.Text.Trim() != "") from = DateTime.Parse(fromOrderDate.Text.Trim()); }
            catch { }
            try { if (toOrderDate.Text != null && toOrderDate.Text.Trim() != "") to = DateTime.Parse(toOrderDate.Text.Trim()); }
            catch { }
            if (from != now && to != now)
            {
                sqlWhere += string.Format(" and OperateTime >= '{0}' and OperateTime <= '{1}' ", from.ToString("yyyy/MM/dd"), to.ToString("yyyy/MM/dd"));
            }
            else if (from != now)
            {
                sqlWhere += string.Format(" and OperateTime >= '{0}' ", from.ToString("yyyy/MM/dd"));
            }
            else if (to != now)
            {
                sqlWhere += string.Format(" and OperateTime <= '{0}' ", to.ToString("yyyy/MM/dd"));
            }
            #endregion

            ChangeHope.DataBase.DataByPage dataPage = bll.GetList(sqlWhere);
            //第一步先添加表头
            table.AddHeadCol("5", "<input type=\"checkbox\" id=\"chkAll\" onclick=\"CheckAll(this.form)\" alt=\"全选/取消\" title=\"全选/取消\" />");
            table.AddHeadCol("80", "发件人");
            table.AddHeadCol("80", "收件人");
            table.AddHeadCol("80", "发送手机");
            table.AddHeadCol("", "发送内容");
            table.AddHeadCol("120", "发送时间");
            table.AddHeadCol("30", "状态");
            table.AddRow();
            //添加表的内容
            if (dataPage.DataReader != null)
            {
                int curpage = ChangeHope.WebPage.PageRequest.GetInt("pageindex");
                if (curpage < 0)
                {
                    curpage = 1;
                }
                int count = 0; string state = "";
                while (dataPage.DataReader.Read())
                {
                    count++;
                    string No = (15 * (curpage - 1) + count).ToString();

                    table.AddCol("<input ID=\"cBox\" type=\"checkbox\" name=\"chk\" value=\"" + dataPage.DataReader["id"].ToString() + "\" />");

                    table.AddCol(dataPage.DataReader["fromUID"].ToString());
                    table.AddCol(dataPage.DataReader["toUID"].ToString());
                    table.AddCol(dataPage.DataReader["Mobile"].ToString());
                    table.AddCol("<textarea rows=\"5\" cols=\"8\" style=\"border: 0pt none; width: 100%; height: 41px;\">" + dataPage.DataReader["Msg"].ToString() + "</textarea>");
                    table.AddCol(dataPage.DataReader["OperateTime"].ToString());
                    state = dataPage.DataReader["State"].ToString();
                    if (state == "1")
                    {
                        table.AddCol(string.Format("<img src='../images/{0}.gif' style='cursor:pointer;' title='{1}'  />", state, "成功"));
                    }
                    else
                    {
                        table.AddCol(string.Format("<img src='../images/{0}.gif' style='cursor:pointer;' title='{1}'  />", state, "失败"));
                    }
                    table.AddRow();
                }
            }
            string view = table.GetTable() + dataPage.PageToolBar;
            dataPage.Dispose();
            dataPage = null;
            return view;
        }

        private void Del(string id)
        {
            YXShop.BLL.Accessories.Sms bll = new YXShop.BLL.Accessories.Sms();
            bll.Delete(id);
            Response.Write("ok");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.lblList.Text = GetList();
        }
    }
}
