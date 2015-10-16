using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace _101shop.admin.v3.filehandle
{
    /// <summary>
    /// TagPathHtmlSelect 的摘要说明
    /// </summary>
    public class TagPathHtmlSelect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //拒绝访问
            if (context.Request.UrlReferrer != null && context.Request.UrlReferrer.Host != context.Request.Url.Host)
            {
                context.Response.Write("拒绝访问。"); context.Response.End(); return;
            }

            /* *
             * * 格式：
             * <select/>
             * */
            string select = context.Request.QueryString["select"] != null ? context.Request.QueryString["select"] : "select";
            string select_name = context.Request.QueryString["select_name"] != null ? context.Request.QueryString["select_name"] : "select_tag_path";
            string selected_value = context.Request.QueryString["selected_value"] != null ? context.Request.QueryString["selected_value"] : "0";
            string separater = context.Request.QueryString["separater"] != null ? context.Request.QueryString["separater"] : HttpUtility.HtmlEncode(" . ");
            string js = Get_Html(select, select_name, selected_value, separater);
            js = js.Replace("\\", "/").Replace("'", "\\'");
            if (context.Request.QueryString["f"] != null && context.Request.QueryString["f"] == "jsonp")
            {
                js = "TagPathHtmlSelect='" + js + "';";//jsonp
                context.Response.ContentType = "text/plain";
                context.Response.Write(js);
            }
            if (context.Request.QueryString["f"] != null && context.Request.QueryString["f"] == "html")
            {
                context.Response.ContentType = "text/html";
                context.Response.Write(js);
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(js);
            }
        }

        public static string Get_Html(string select, string select_name, string selected_value, string separater)
        {
            DataSet Category = new DataSet();
            string sqlString = "SELECT id, name, detail, website, usergroup, sumproduct, editor, createdate, lastupdate, status, path FROM Tag WHERE 1=1 order by id";
            Category = new SOSOshop.BLL.DbBase().ExecuteDataSet(sqlString);
            Func<DataRow, bool> where1 = delegate(DataRow dr) { return dr["path"].ToString().Split('/').Length == 1; };//一级分类
            Func<DataRow, bool> where2 = delegate(DataRow dr) { return dr["path"].ToString().Split('/').Length == 2; };//二级分类
            Func<DataRow, bool> where3 = delegate(DataRow dr) { return dr["path"].ToString().Split('/').Length == 3; };//三级分类
            Func<DataRow, bool> where4 = delegate(DataRow dr) { return dr["path"].ToString().Split('/').Length == 4; };//三级分类
            Func<DataRow, bool> where5 = delegate(DataRow dr) { return dr["path"].ToString().Split('/').Length == 5; };//三级分类
            DataRow[] dr1s = Category.Tables[0].Select().Where<DataRow>(where1).ToArray();
            DataRow[] dr2s = Category.Tables[0].Select().Where<DataRow>(where2).ToArray();
            DataRow[] dr3s = Category.Tables[0].Select().Where<DataRow>(where3).ToArray();
            if (dr1s.Length == 0 || dr2s.Length == 0 || dr3s.Length == 0)
            {
                if (dr1s.Length == 0)
                {
                    if (dr2s.Length == 0)
                    {
                        dr1s = dr3s;
                    }
                    else
                    {
                        dr1s = dr2s;
                        dr2s = dr3s;
                    }
                }
                else if (dr2s.Length == 0)
                {
                    DataRow[] dr4s = Category.Tables[0].Select().Where<DataRow>(where4).ToArray();
                    DataRow[] dr5s = Category.Tables[0].Select().Where<DataRow>(where5).ToArray();
                    if (dr3s.Length == 0)
                    {
                        if (dr4s.Length == 0)
                        {
                            dr2s = dr5s;
                        }
                        else
                        {
                            dr2s = dr4s;
                            dr3s = dr5s;
                        }
                    }
                    else
                    {
                        dr2s = dr3s;
                        if (dr4s.Length == 0)
                        {
                            dr3s = dr5s;
                        }
                        else
                        {
                            dr3s = dr4s;
                        }
                    }
                }
            }
            StringBuilder js = new StringBuilder();
            int level = 0;
            bool selected = (string.IsNullOrEmpty(selected_value) || selected_value.Trim() == "0");
            if (select == "select") js.Append("<select id=\"" + select_name + "\" name=\"tag_path\">");
            if (select == "select") js.Append("<option value=\"\"" + (selected ? " selected" : "") + ">未定义(默认)</option>");
            int count1 = dr1s.Length;
            DataRow dr1 = null;
            for (int i1 = 0; i1 < count1; i1++)
            {
                dr1 = dr1s[i1];
                //id, name, detail, website, usergroup, sumproduct, editor, createdate, lastupdate, status, path
                selected = GetClassOptionSelected(dr1, selected_value);
                level = 1;
                if (dr2s.Length > 0)
                {
                    DataRow[] _dr2s = dr2s.AsQueryable().Where<DataRow>(delegate(DataRow dr) { return dr["path"].ToString() == dr1["path"].ToString() + "/" + dr["id"].ToString(); }).ToArray();
                    int count2 = _dr2s.Length;
                    if (count2 > 0)
                    {
                        js.Append(GetClassOptionValueString(select, dr1, selected, level, separater));
                        DataRow dr2 = null;
                        for (int i2 = 0; i2 < count2; i2++)
                        {
                            dr2 = _dr2s[i2];
                            selected = GetClassOptionSelected(dr2, selected_value);
                            level = 2;
                            if (dr3s.Length > 0)
                            {
                                DataRow[] _dr3s = dr3s.AsQueryable().Where<DataRow>(delegate(DataRow dr) { return dr["path"].ToString() == dr2["path"].ToString() + "/" + dr["id"].ToString(); }).ToArray();
                                int count3 = _dr3s.Length;
                                if (count3 > 0)
                                {
                                    js.Append(GetClassOptionValueString(select, dr2, selected, level, separater));
                                    DataRow dr3 = null;
                                    for (int i3 = 0; i3 < count3; i3++)
                                    {
                                        dr3 = _dr3s[i3];
                                        selected = GetClassOptionSelected(dr3, selected_value);
                                        level = 3;
                                        js.Append(GetClassOptionValueString(select, dr3, selected, level, separater));
                                    }
                                }
                                else
                                {
                                    js.Append(GetClassOptionValueString(select, dr2, selected, level, separater));
                                }
                            }
                            else
                            {
                                js.Append(GetClassOptionValueString(select, dr2, selected, level, separater));
                            }
                        }
                    }
                    else
                    {
                        js.Append(GetClassOptionValueString(select, dr1, selected, level, separater));
                    }
                }
                else
                {
                    js.Append(GetClassOptionValueString(select, dr1, selected, level, separater));
                }
            }
            if (select == "select") js.Append("</select>");
            return js.ToString();
        }
        protected static bool GetClassOptionSelected(DataRow dr, string selected_value)
        {
            return selected_value == dr["id"].ToString() || selected_value == dr["name"].ToString() || selected_value == dr["path"].ToString();
        }
        protected static string GetClassOptionValueString(string select, DataRow dr, bool selected, int level, string separater)
        {
            //id, name, detail, website, usergroup, sumproduct, editor, createdate, lastupdate, status, path
            string s = "<" + (select == "select" ? "option" : select) + " value=\"" + dr["path"]
                + "\" title=\"" + dr["detail"]
                + "\" website=\"" + dr["website"]
                + "\" usergroup=\"" + dr["usergroup"]
                + "\" sumproduct=\"" + dr["sumproduct"]
                + "\" editor=\"" + dr["editor"]
                + "\" createdate=\"" + dr["createdate"]
                + "\" status=\"" + dr["status"]
                + "\" path=\"" + dr["path"]
                + "\" id=\"" + dr["id"]
                + "\" class=\"level" + Convert.ToString(dr["path"]).Split('/').Length
                + "\"" + (selected ? " selected" : "") + ">"
                + GetClassSelectSeparaterString(level, separater)
                + dr["name"]
                + "</" + (select == "select" ? "option" : select) + ">";
            return s;
        }
        protected static string GetClassSelectSeparaterString(int level, string separater)
        {
            string s = string.Empty; for (int i = 0; i < level; i++) s += separater; return s;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}