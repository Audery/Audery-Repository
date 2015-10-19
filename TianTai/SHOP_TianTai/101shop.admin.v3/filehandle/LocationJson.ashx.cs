using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace _101shop.admin.v3.filehandle
{
    /// <summary>
    /// LocationJScript 的摘要说明
    /// </summary>
    public class LocationJson : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Expires = 7 * 24 * 69;//缓存一周
            context.Response.CacheControl = "Public";
            context.Response.ContentType = "text/plain";
            /* *
             * * 格式：
             * {
	'0':{1:'北京市',22:'天津市',44:'上海市',...,4825:'台湾省'},
	'0,1':{2:'北京市'},
	'0,1,2':{3:'东城区',4:'西城区',5:'崇文区',...,7:'朝阳区'},
             * }
             * */
            if (context.Request.QueryString["f"] != null && context.Request.QueryString["f"] == "jsonp")
            {
                StringBuilder js = new StringBuilder();
                js.Append("function Location() { this.items = " + Get_Json(0) + ";}");
                js.Append("Location.prototype.find = function(id) { if(typeof(this.items[id]) == \"undefined\")return false; return this.items[id];};");
                js.Append("Location.prototype.findProvince = function(id) { if(typeof(this.items[\"0\"][id]) == \"undefined\")return false; return this.items[\"0\"][id];};");
                js.Append("Location.prototype.findCity = function(id) { for(var i in this.items){ if(typeof(this.items[i]) == \"function\")continue; if(i.toString().split(\",\").length == 2){ if(typeof(this.items[i][id]) == \"undefined\")continue; return this.items[i][id];}}return false;};");
                js.Append("Location.prototype.findCounty = function(id) { for(var i in this.items){ if(typeof(this.items[i]) == \"function\")continue; if(i.toString().split(\",\").length == 3){ if(typeof(this.items[i][id]) == \"undefined\")continue; return this.items[i][id];}}return false;};");
                js.Append("Location.prototype.initOption = function(el_id, loc_id, selectedText, selected) { var el=el_id,option='<option value=\"'+loc_id+'\">'+selectedText+'</option>';el.prepend(option);if(selected&&(jQuery('option',el).length==1||el.val()==null||el.val()==''||el.val()=='0'))el.attr('selectedIndex',0);};");
                js.Append("Location.prototype.fillOption = function(el_id, loc_id, selectedText) { var el=el_id,json=this.find(loc_id),s=parseInt(selectedText),sb=isNaN(s);if(json){var index=0,selected_index=-1;jQuery.each(json,function(k,v){var option=jQuery('<option value=\"'+k+'\">'+v+'</option>'),b=(sb&&v===selectedText)||(!sb&&parseInt(k)===s);option.attr('selected',b).appendTo(el);if(b)selected_index=index;index++;});if(selected_index!=-1){el.trigger('change');}else{el.attr('selectedIndex',-1);}}};");
                context.Response.Write(js.ToString());
            }
            else
            {
                context.Response.Write(Get_Json(0));
            }
        }

        public static string Get_Json(int value)
        {
            string js = "";
            //查询数据库
            try
            {
                StringBuilder json = new StringBuilder();
                string ParentPath = "";
                string sql = "SELECT ParentPath FROM Region WHERE (ParentPath <> '') GROUP BY ParentPath ORDER BY ParentPath";
                SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(sql);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        ParentPath += reader[0].ToString() + ":";
                    }
                    if (ParentPath != "") ParentPath = ParentPath.Substring(0, ParentPath.Length - 1);
                    reader.Close();

                    string[] Paths = ParentPath.Split(":".ToCharArray());
                    foreach (string Path in Paths)
                    {
                        sql = "select Id,Name from Region where ParentPath='" + Path + "' ORDER BY OrderID";
                        reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(sql);
                        if (reader != null)
                        {
                            json.Append("'" + Path + "':{");
                            string option = "";
                            while (reader.Read())
                            {
                                option += reader["Id"].ToString() + ":'" + reader["Name"].ToString().Trim("\r\n '".ToCharArray()) + "',";
                            }
                            if (option != "") json.Append(option.Substring(0, option.Length - 1) + "},");
                            reader.Close();
                        }
                    }
                }
                js = json.ToString(); if (js != "") js = js.Substring(0, js.Length - 1);
            }
            catch { }
            return "{" + js + "}";
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