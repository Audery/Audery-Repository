using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace Note
{
    public partial class Add : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        // 创建DataContext实例用于用于操作本地的数据库
        private NoteDataContext noteDB;
        private NoteCollection noteCol = new NoteCollection();
        public Add()
        {
            InitializeComponent();
            noteCol.NoteTables = new ObservableCollection<NoteTable>();
        }


        private void AppBar_Cancel_Click(object sender, EventArgs e)
        {
            TxtTitle.Text = string.Empty;
            TxtContent.Text = string.Empty;
        }

        private void AppBar_Save_Click(object sender, EventArgs e)
        {
            
            if (this.TxtTitle.Text == "")
            {
                MessageBox.Show("请填写标题");
                return;
            }
            if (this.TxtTitle.Text.Length>10)
            {
                MessageBox.Show("标题不能超过10个字符");
                return;
            }
            try
            {
                //连接数据库并初始化DataContext实例
                noteDB = new NoteDataContext(NoteDataContext.DBConnectionString);
                //创建一条表的数据
                NoteTable newNote = new NoteTable { Title = TxtTitle.Text, Time = DateTime.Now.ToLongDateString(), Content = TxtContent.Text.ToString() };
                //添加绑定集合的数据
                noteCol.NoteTables.Add(newNote);
                //插入数据库
                noteDB.Notes.InsertOnSubmit(newNote);
                //保存数据库的改变
                noteDB.SubmitChanges();
            }
            catch(Exception)
            {
                MessageBox.Show("保存数据出错！");
            }

        
            //保存完后直接跳到查询页面
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml"), UriKind.Relative));
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml",uriKind:UriKind.Relative));
            //base.OnBackKeyPress(e);
        }
       
    }
}