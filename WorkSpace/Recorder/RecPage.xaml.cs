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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Threading;
using Windows.Storage;

namespace Recorder
{
    public partial class RecPage : PhoneApplicationPage
    {
        Microphone myMicrophone; //引用当前设备默认麦克风的变量
        List<byte> btList; //存储从麦克风中读取到的字节
        byte[] msBuffer; //读取数据的缓冲区
        public RecPage()
        {
            InitializeComponent();
            // Microphone.Default静态属性获得默认麦克风的引用
            myMicrophone = Microphone.Default;
            myMicrophone.BufferDuration = TimeSpan.FromMilliseconds(1000);
            msBuffer = new byte[myMicrophone.GetSampleSizeInBytes(myMicrophone.BufferDuration)];
        }

        void myMicrophone_BufferReady(object sender, EventArgs e)
        {
            // 获取数据
            try
            {
                myMicrophone.GetData(msBuffer);
                btList.AddRange(msBuffer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("获取数据时：{0}", ex.Message));
            }
        }


        private async void btnOk(object sender, RoutedEventArgs e)
        {
            // 判断输入有效性
            if (string.IsNullOrWhiteSpace(this.txtFileName.Text))
            {
                MessageBox.Show("请输入文件名。");
                return;
            }
            // 保存录音到独立存储
            try
            {
                StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;

                //判断文件是否已经存在
                StorageFile storageFile = null;

                storageFile = await applicationFolder.CreateFileAsync(this.txtFileName.Text, CreationCollisionOption.ReplaceExisting);
                //将指定内容写入到文件
                using (Stream stream = await storageFile.OpenStreamForWriteAsync())
                {
                    await stream.WriteAsync(btList.ToArray(), 0, btList.Count);
                    stream.Flush();
                    stream.Close();
                }

                //using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                //{
                //    if (iso.FileExists(this.txtFileName.Text))
                //    {
                //        MessageBox.Show("此文件名已经存在，请重新输入。");
                //        return;
                //    }
                //    using (IsolatedStorageFileStream fs = iso.CreateFile(this.txtFileName.Text))
                //    {
                //        fs.Write(btList.ToArray(), 0, btList.Count);
                //        fs.Flush();
                //        fs.Close();
                //    }
                //}
            }
            catch
            {
                MessageBox.Show("保存失败。");
            }
            // 返回主页面
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void onCancel(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void onStopRec(object sender, RoutedEventArgs e)
        {
            this.myMicrophone.Stop();//停止录音
            this.SBRecoder.Stop();
            EPS.Opacity = 0;
            EP.Opacity = 0;
            this.txbRecording.Text = "录音已停止。";
            this.svPan.Visibility = System.Windows.Visibility.Visible;
            this.txtFileName.Text = string.Format("录音_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            this.txtFileName.Focus(); //使文本框成为焦点
            this.txtFileName.SelectAll(); //选定文本框中所有文本
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //App.InitTimer();
            btList = new List<byte>();
            btList.Clear();

            // 绑定事件
            myMicrophone.BufferReady += new EventHandler<EventArgs>(myMicrophone_BufferReady);
            Dispatcher.BeginInvoke(() => BtnStop.IsEnabled = false);
            Dispatcher.BeginInvoke(() => txbRecording.Text = "准备....");
            EPS.Opacity = 0;
            EP.Opacity = 0;
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (myMicrophone.State != MicrophoneState.Stopped)
            {
                myMicrophone.Stop();
            }
            // 离开页面时解除事件绑定
            myMicrophone.BufferReady -= this.myMicrophone_BufferReady;
            base.OnNavigatingFrom(e);
        }

        private void onSartRec(object sender, RoutedEventArgs e)
        {
            // 开始录制
            if (myMicrophone.State != MicrophoneState.Started)
            {
                myMicrophone.Start();
            }
            SBRecoder.Begin();
            BtnStart.IsEnabled = false;
            Dispatcher.BeginInvoke(() => BtnStop.IsEnabled = true);
            Dispatcher.BeginInvoke(() => txbRecording.Text = "录音....");
        }
    }
}