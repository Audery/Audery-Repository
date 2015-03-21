using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Recorder
{
    /// <summary>
    /// 存储与录音文件相关的信息
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }


    /// <summary>
    /// 转换器：将日期转换为需要的字符串格式
    /// </summary>
    public class DateTimeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dt = (DateTime)value;
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
