using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Windows.Storage;
using Windows.Storage.Search;
using Microsoft.Phone.Tasks;

namespace Recorder
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<Record> rcList = null;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
           
        }

        private void onPlay(object sender, RoutedEventArgs e)
        {
            Button playButton = e.OriginalSource as Button;
            if (playButton != null)
            {
                MemoryStream ms = new MemoryStream();
                string fileName = playButton.Tag.ToString();
                // 从独立存储区中读取文件
                try
                {

                    using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        // 如果文件不存在，跳出
                        if (!iso.FileExists(fileName))
                        {
                            return;
                        }
                        // 打开文件流
                        using (IsolatedStorageFileStream stream = iso.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[2048];
                            int n = 0;
                            // 读入到内存流
                            while ((n = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, n);
                            }
                        }
                    }
                }
                catch
                {
                    // 如果未能正确读取文件，就不往下执行
                    return;
                }
                // 如果文件长度为0，就不用播放了
                if (ms.Length == 0L)
                {
                    ms.Dispose();
                    return;
                }
                // 播放声音
                SoundEffect mySound = new SoundEffect(ms.ToArray(), Microphone.Default.SampleRate, AudioChannels.Mono);
                mySound.Play();
                ms.Dispose();
            }
        }

        private void onDelete(object sender, RoutedEventArgs e)
        {
            HyperlinkButton delLink = e.OriginalSource as HyperlinkButton;
            if (delLink != null)
            {
                string fileName = delLink.Tag.ToString();
                if (MessageBox.Show("确实要删除文件 " + fileName + " 吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (iso.FileExists(fileName))
                        {
                            try
                            {
                                iso.DeleteFile(fileName);
                            }
                            catch { return; }
                            // 重新获取文件列表
                            GetFileList();
                        }
                    }
                }
            }
        }

        private void onNew(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/RecPage.xaml", UriKind.Relative));
            
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        private void GetFileList()
        {
            try
            {

                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    string[] files = iso.GetFileNames();
                    rcList = new List<Record>();
                    foreach (string f in files)
                    {
                        Record rec = new Record()
                        {
                            FileName = f,
                            SaveTime = iso.GetCreationTime(f).DateTime
                        };
                        rcList.Add(rec);
                    }

                    this.fileList.ItemsSource = rcList;
                }
            }
            catch { }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GetFileList();
        }

        private void onAbout(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var SelectedItem = sender as MenuItem;           
            string fileName = SelectedItem.DataContext.ToString();
            if (MessageBox.Show("确实要删除文件 " + fileName + " 吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (iso.FileExists(fileName))
                    {
                        try
                        {
                            iso.DeleteFile(fileName);
                        }
                        catch { return; }
                        // 重新获取文件列表
                        GetFileList();
                    }
                }
            }
        }

        private void SMS_Share_Click(object sender, RoutedEventArgs e)
        {

            var SelectedItem = sender as MenuItem;
            var temp = rcList.Find(o => o.FileName == SelectedItem.DataContext.ToString());
            SmsComposeTask SmsCompose = new SmsComposeTask();
            SmsCompose.Body = "少年，快来和我一起玩简易手机录音机吧:http://www.windowsphone.com/zh-cn/store/app/%E7%AE%80%E6%98%93%E6%89%8B%E6%9C%BA%E5%BD%95%E9%9F%B3%E6%9C%BA/c86f47e3-6a65-41ab-92ad-462b671d2d0e";            
            SmsCompose.Show();
        }

        private void Email_Share_Click(object sender, RoutedEventArgs e)
        {
            var SelectedItem = sender as MenuItem;
            var temp = rcList.Find(o => o.FileName == SelectedItem.DataContext.ToString());
            EmailComposeTask EmailCompose = new EmailComposeTask();
            EmailCompose.Subject = temp.FileName;
            EmailCompose.Body = "少年，快来和我一起玩简易手机录音机吧:http://www.windowsphone.com/zh-cn/store/app/%E7%AE%80%E6%98%93%E6%89%8B%E6%9C%BA%E5%BD%95%E9%9F%B3%E6%9C%BA/c86f47e3-6a65-41ab-92ad-462b671d2d0e";
            EmailCompose.Show();
        }
    }

}