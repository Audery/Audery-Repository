using System;
namespace DSWebService.Model.Data_Centre
{
    /// <summary>
    /// Link:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Link
    {
        public Link()
        { }
        #region Model
        private int _id;
        private string _spid;
        private string _t_id;
        private int _iden;
        private DateTime _created;
        private DateTime _updated;
        private bool _is_default = false;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string spid
        {
            set { _spid = value; }
            get { return _spid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string t_id
        {
            set { _t_id = value; }
            get { return _t_id; }
        }
        /// <summary>
        /// 10000-天奇,10001-蓉锦
        /// </summary>
        public int iden
        {
            set { _iden = value; }
            get { return _iden; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime updated
        {
            set { _updated = value; }
            get { return _updated; }
        }
        /// <summary>
        /// 是否默认供货
        /// </summary>
        public bool is_default
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        #endregion Model

    }
}

