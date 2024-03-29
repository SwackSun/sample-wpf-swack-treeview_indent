﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace window_explorer.View
{
    #region HeaderToImageConverter
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance =
            new HeaderToImageConverter();
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if ((value as string).Contains(@":)"))
            {
                Uri uri = new Uri
                ("pack://application:,,,/images/diskdrive.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/images/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
    #endregion // HeaderToImageConverter
}
