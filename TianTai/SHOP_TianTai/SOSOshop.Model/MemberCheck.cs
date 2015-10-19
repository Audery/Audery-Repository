using System;

namespace SOSOshop.Model
{
    [Serializable]
    public class MemberCheck
    {
        private int _uid;

        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private bool _checked;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        private string _checkType;

        public string CheckType
        {
            get { return _checkType; }
            set { _checkType = value; }
        }

    }
}
