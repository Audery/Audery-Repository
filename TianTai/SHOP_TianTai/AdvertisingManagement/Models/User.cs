using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvertisingManagement.Models
{
    /// <summary>
    /// 站点配置表
    /// </summary>
    [Serializable]
    public class User : BLL.DataEntityBase
    {
		#region Model
		private string _netno;
		private string _netname;
		private string _netkey;

        public enum Names { NetNo=0, NetName=1, NetKey=2 };
        private static string[] names = { "NetNo", "NetName", "NetKey" };

        /// <summary>
        /// 返回user表的字段名称
        /// </summary>
        /// <param name="xh">由Names来指定</param>
        /// <returns></returns>
        public static string Fields(int xh)
        {
            return names[xh];
        }

		/// <summary>
		/// 站点编号
		/// </summary>
        [BLL.DataField("NetNo")]
        public string NetNo
		{
			set{ _netno=value;}
			get{return _netno;}
		}
		/// <summary>
		/// 站点名称
		/// </summary>
        [BLL.DataField("NetName")]
        public string NetName
		{
			set{ _netname=value;}
			get{return _netname;}
		}
		/// <summary>
		/// 设置的密钥
		/// </summary>
        [BLL.DataField("NetKey")]
        public string NetKey
		{
			set{ _netkey=value;}
			get{return _netkey;}
		}
        public override string ToString()
        {
            return string.Format("NetNo:{1}{0}NetName:{2}{0}NetKey:{3}", Environment.NewLine, NetNo,
                                 NetName, NetKey);
        }
		#endregion Model
    }

    /// <summary>
    /// 内容有101广告系统管理员创建。
    /// </summary>
    [Serializable]
    public class Ad_Config : BLL.DataEntityBase
    {
        #region Model

        /// <summary>
        /// 广告配置表主键ID
        /// </summary>
        [BLL.DataField("ConfigID")]
        public int ConfigID{ set;get;}
        /// <summary>
        /// 广告位置名称
        /// </summary>
        [BLL.DataField("AdName")]
        public string AdName { set; get; }
        /// <summary>
        /// 广告站点名称
        /// </summary>
        [BLL.DataField("NetName")]
        public string NetName { set; get; }
        /// <summary>
        /// 广告位置大小的宽度
        /// </summary>
        [BLL.DataField("Width")]
        public int Width { set; get; }
        /// <summary>
        /// 广告位置大小的高度
        /// </summary>
        [BLL.DataField("Height")]
        public int Height { set; get; }
        /// <summary>
        /// 广告资源的类型：jpg、flash
        /// </summary>
        [BLL.DataField("Resource")]
        public int Resource { set; get; }
        /// <summary>
        /// 频道类型：中药，基药，搜索等频道
        /// </summary>
        [BLL.DataField("Channel")]
        public string Channel { set; get; }
        /// <summary>
        /// 站点编号
        /// </summary>
        [BLL.DataField("NetNo")]
        public string NetNo { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BLL.DataField("CreatedTime")]
        public DateTime CreatedTime { set; get; }


        [BLL.DataField("ContentID")]
        public int ContentID { set; get; }

        [BLL.DataField("Picture")]
        public string Picture { set; get; }
        [BLL.DataField("Url")]
        public string Url { set; get; }
        #endregion Model
    }

    /// <summary>
    /// 各站点展示的内容表（大小和类型有配置表指定）
    /// </summary>
    [Serializable]
    public partial class Ad_Content
    {
        #region Model
        private int _contentid;
        private int? _configid;
        private string _picture;
        private string _url;
        private string _netno;
        private DateTime? _createdtime;
        private DateTime? _stoptime;
        /// <summary>
        /// 广告内容id
        /// </summary>
        [BLL.DataField("ContentID")]
        public int ContentID
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        /// <summary>
        /// 广告位id
        /// </summary>
        [BLL.DataField("ConfigID")]
        public int? ConfigID
        {
            set { _configid = value; }
            get { return _configid; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        [BLL.DataField("Picture")]
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        [BLL.DataField("Url")]
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 站点编号
        /// </summary>
        [BLL.DataField("NetNo")]
        public string NetNo
        {
            set { _netno = value; }
            get { return _netno; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        [BLL.DataField("Detials")]
        public string Detials { set; get; }

        /// <summary>
        /// 域名
        /// </summary>
        [BLL.DataField("Domain")]
        public string Domain { get; set; }
        /// <summary>
        /// 建立时间
        /// </summary>
        [BLL.DataField("CreatedTime")]
        public DateTime? CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 停止时间
        /// </summary>
        [BLL.DataField("StopTime")]
        public DateTime? StopTime
        {
            set { _stoptime = value; }
            get { return _stoptime; }
        }
        #endregion Model

    }

}