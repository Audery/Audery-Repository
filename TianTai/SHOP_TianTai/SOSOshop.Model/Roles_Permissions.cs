using System;
using System.Data;

namespace SOSOshop.Model
{
    public class Roles_Permissions
    {
        #region "member variant"
        private int id;
        private int operateCode;
        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造一个空的新的数据访问对象
        /// </summary>
        /// <remarks></remarks>
        public Roles_Permissions()
        {
        }
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary>
        /// <remarks></remarks>
        public Roles_Permissions(DataRow row)
        {
            LoadFromRow(row);
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
        /// 与数据库基本列OperateCode相对应的公共属性, Caption:角色编码
        /// </summary>
        public int OperateCode
        {
            get { return operateCode; }
            set { operateCode = value; }
        }

        #endregion

        #region
        /// <summary>
        /// 构造一个数据访问对象，并将DataRow列的数据提取到对象的属性里
        /// </summary>
        /// <remarks></remarks>
        protected void LoadFromRow(DataRow row)
        {
            if (!row["id"].Equals(DBNull.Value)) this.id = Convert.ToInt32(row["id"]);
            if (!row["operatecode"].Equals(DBNull.Value)) this.OperateCode = Convert.ToInt32(row["operatecode"]);
        }
        #endregion
    }
}
