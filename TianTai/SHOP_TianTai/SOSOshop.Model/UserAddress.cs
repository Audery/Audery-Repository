using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    [Serializable]
    public class UserAddress
    {
        public int ID { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        public string Borough { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; }

        public bool Stat { get; set; }

        public string ConstructionSigns { get; set; }

        public string Consignestime { get; set; }
    }
}
