﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KelsonBall.LudumDare41.LevelEditor.Utils
{
    public class xToLeftConverter : IValueConverter
    {
        public static int HalfWidth;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int x = System.Convert.ToInt32(value);
            return x + HalfWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int x = System.Convert.ToInt32(value);
            return x - HalfWidth;
        }
    }

    public class yToTopConverter : IValueConverter
    {
        public static int HalfHeight;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int y = System.Convert.ToInt32(value);
            return y + HalfHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int y = System.Convert.ToInt32(value);
            return y - HalfHeight;
        }
    }

    public class HalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TypeToVisibilityConverter<T> : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType() == typeof(T) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RectangleConverter : TypeToVisibilityConverter<RectangleTerrainViewModel>
    {

    }

    public class CircleConverter : TypeToVisibilityConverter<CircleTerrainViewModel>
    {

    }
}
