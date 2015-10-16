using System;

namespace SOSOshop.Model
{
    [Serializable]
    public class AdminInfo
    {
        public AdminInfo()
        { }
        private int adminId;
        private string adminName;
        private string adminPowerType;
        private string adminRole;

        /// <summary>
        /// ID
        /// </summary>
        public int AdminId
        {
            get { return adminId; }
            set { adminId = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string AdminPowerType
        {
            get { return adminPowerType; }
            set { adminPowerType = value; }
        }

        public string AdminRole
        {
            get { return adminRole; }
            set { adminRole = value; }
        }
    }
}
