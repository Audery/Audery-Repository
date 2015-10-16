using System;

namespace SOSOshop.Model
{
    public class Role
    {
        #region "member variant"
        private int id;
        private string name;
        private string description;
        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary>
        /// <remarks></remarks>
        public Role()
        {
        }
        #endregion

        #region "Property"
        /// <summary>
        /// 与数据库基本列Id相对应的公共属性, Caption:角色ID
        /// </summary>
        /// <remarks></remarks>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 与数据库基本列RoleName相对应的公共属性, Caption:角色名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 与数据库基本列Description相对应的公共属性, Caption:角色描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion
    }
}
