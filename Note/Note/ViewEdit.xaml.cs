using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

namespace Note
{
    public partial class ViewEdit : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        // 创建DataContext实例用于用于操作本地的数据库
        private NoteDataContext noteDB;
        private NoteCollection noteCol = new NoteCollection();
        private string id = "";
        public ViewEdit()
        {
            InitializeComponent();
            noteCol.NoteTables = new ObservableCollection<NoteTable>();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {            
                bindView();
        }


        private void AppBar_Save_Click(object sender, EventArgs e)
        {
                     
            try
            {
                //获取编辑的NoteTable对象
                NoteTable note = (NoteTable)State["note"];
                if (string.IsNullOrEmpty(TxtTitle.Text))
                {
                    MessageBox.Show("标题不能为空");
                    return;
                }
                if (TxtTitle.Text.Length > 10)
                {
                    MessageBox.Show("标题不能超过10个字符");
                }
                else
                {
                    note.Title = TxtTitle.Text;
                }
                note.Content = TxtContent.Text;
                //保存数据库的改变
                noteDB.SubmitChanges();
                State["note"] = note;
                //保存完后直接跳到查询页面
                NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml"), UriKind.Relative));
            }
            catch
            {
                MessageBox.Show("编辑出错！");
            }


        }


        private void bindView()
        {
            id = NavigationContext.QueryString["id"];
            try
            {

                //连接数据库并初始化DataContext实例
                noteDB = new NoteDataContext(NoteDataContext.DBConnectionString);

                // 使用Linq查询语句查询NoteTable表的所有数据
                var notesInDB = from NoteTable note in noteDB.Notes
                                where note.ID == Int32.Parse(id)
                                select note;

                // 将查询的结果返回到页面数据绑定的集合里面
                noteCol.NoteTables = new ObservableCollection<NoteTable>(notesInDB);
                TxtTitle.Text = noteCol.NoteTables.First().Title;
                TxtContent.Text = noteCol.NoteTables.First().Content;
                //将要删除的数据放到State中
                State["note"] = noteCol.NoteTables.First();
            }
            catch
            {
                MessageBox.Show("记事已经删除了！");
            }


        }

        private void AppBar_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //删除的NoteTable实例
                NoteTable noteForDelete = State["note"] as NoteTable;
                // 移除数据库里面要删除的NoteTable记录
                noteDB.Notes.DeleteOnSubmit(noteForDelete);
                //保存数据库的改变
                noteDB.SubmitChanges();
                //保存完后直接跳到查询页面
                NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml"), UriKind.Relative));
            }
            catch
            {
                MessageBox.Show("删除失败！");
            }
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (!State.Keys.Contains("OldTime"))
            {
                 State["OldTime"] = DateTime.Now;
                 stbin.Begin();
            }
            if (State["OldTime"]==null)
            {
                State["OldTime"] = DateTime.Now;
                //弹出动画提示,再次点击会跳回主页面
                stbin.Begin();
            }
            else
            {
                int s= DateTime.Compare((DateTime)State["OldTime"], DateTime.Now);
                if (s<3)
                {
                     NavigationService.Navigate(new Uri("/MainPage.xaml", uriKind: UriKind.Relative));
                }
                else
                {
                    //弹出动画提示
                    State["OldTime"] = DateTime.Now;
                }
            }

            NavigationService.Navigate(new Uri("/MainPage.xaml", uriKind: UriKind.Relative));
            //base.OnBackKeyPress(e);
        }
    }
}