using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Note
{
    //用于跟页面的数据绑定
    public class NoteCollection : INotifyPropertyChanged
    {
        //定义ObservableCollection来绑定页面的数据
        private ObservableCollection<NoteTable> _noteTables;
        public ObservableCollection<NoteTable> NoteTables
        {
            get
            {
                return _noteTables;
            }
            set
            {
                if (_noteTables != value)
                {
                    _noteTables = value;
                    NotifyPropertyChanged("NoteTables");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        //用于通知属性的改变
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
