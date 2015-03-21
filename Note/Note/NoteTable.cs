using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace Note
{
    [Table]
    public class NoteTable : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // 记事本表独立增长ID，设置为主键
        private int _id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("ID");
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        // 定义记事的标题
        private string _title;

        [Column]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    NotifyPropertyChanging("Title");
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }


        // 定义记事的事件
        private string _time;

        [Column]
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time != value)
                {
                    NotifyPropertyChanging("Time");
                    _time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        //记事内容
        private string _content;

        [Column]
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (_content != value)
                {
                    NotifyPropertyChanging("Content");
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        //用来通知页面表的字段数据产生了改变
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // 用来通知数据上下文表的字段数据将要产生改变
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

}
