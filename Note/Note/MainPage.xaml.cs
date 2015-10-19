using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;

namespace Note
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 创建DataContext实例用于用于操作本地的数据库
        private NoteDataContext noteDB;
        private NoteCollection noteCol = new NoteCollection();
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            bindList();
        }

        private void AppBar_Add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Note;component/Add.xaml", UriKind.Relative));
        }

        //private void noteLocation_Click(object sender, RoutedEventArgs e)
        //{
        //    HyperlinkButton clickedLink = (HyperlinkButton)sender;

        //    string uri = String.Format("/Note;component/ViewEdit.xaml?id={0}", clickedLink.Tag);

        //    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        //}


        private void bindList()
        {
            try
            {
                //连接数据库并初始化DataContext实例
                noteDB = new NoteDataContext(NoteDataContext.DBConnectionString);

                // 使用Linq查询语句查询EmployeeTable表的所有数据
                var notesInDB = from NoteTable note in noteDB.Notes
                                select note;

                // 将查询的结果返回到页面数据绑定的集合里面
                noteCol.NoteTables = new ObservableCollection<NoteTable>(notesInDB);

                //赋值给当前页面的DataContext用于数据绑定
                this.DataContext = noteCol;

                noteListBox.ItemsSource = noteCol.NoteTables;

            }
            catch
            {
                MessageBox.Show("数据库查询错误！");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var SelectedItem = sender as MenuItem;
            var temp = noteCol.NoteTables.ToList().Find(o => o.ID == (int)SelectedItem.DataContext);
            noteDB.Notes.DeleteOnSubmit(temp);
            noteDB.SubmitChanges();
            //再次绑定
            bindList();
            MessageBox.Show("删除成功");
        }

        /// <summary>
        /// 选择项改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = noteListBox.SelectedItem as NoteTable;
            if (SelectedItem != null)
            {
                NavigationService.Navigate(new Uri(string.Format("/ViewEdit.xaml?id={0}", SelectedItem.ID), UriKind.Relative));
            }

        }

        /// <summary>
        /// 短信分享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMS_Share_Click(object sender, RoutedEventArgs e)
        {

            var SelectedItem = sender as MenuItem;
            var temp = noteCol.NoteTables.ToList().Find(o => o.ID == (int)SelectedItem.DataContext);
            SmsComposeTask SmsCompose = new SmsComposeTask();
            SmsCompose.Body = temp.Content;
            SmsCompose.Show();

        }
        /// <summary>
        /// 邮件分享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Email_Share_Click(object sender, RoutedEventArgs e)
        {

            var SelectedItem = sender as MenuItem;
            var temp = noteCol.NoteTables.ToList().Find(o => o.ID == (int)SelectedItem.DataContext);
            EmailComposeTask EmailCompose = new EmailComposeTask();
            EmailCompose.Subject = temp.Title;
            EmailCompose.Body = temp.Content;
            EmailCompose.Show();
        }

        /// <summary>
        /// 评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarketplaceReview_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            int totle = NavigationService.BackStack.Count();

            for (int i = 0; i < totle; i++)
            {
                NavigationService.RemoveBackEntry();
            }


            base.OnBackKeyPress(e);
        }
    }
}