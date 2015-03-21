using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Recorder
{
    public partial class test : PhoneApplicationPage
    {
        public test()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(()=> SBRecoder.Begin());
            DoubleAnimation DoubleAnimation = new DoubleAnimation();
            
        }
    }
}