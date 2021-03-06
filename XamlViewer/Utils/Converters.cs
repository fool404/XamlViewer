﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using System.Windows;
using XamlUtil.Common;
using System.Linq;
using System.Text.RegularExpressions;
using XamlViewer.Models;

namespace XamlViewer.Utils
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public BoolToVisibilityConverter()
        {
            TrueVisibility = Visibility.Visible;
            FalseVisibility = Visibility.Collapsed;
        }

        public Visibility TrueVisibility { get; set; }
        public Visibility FalseVisibility { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTrue = (bool)value;
            return isTrue ? TrueVisibility : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnyBoolToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(null) || values.Contains(DependencyProperty.UnsetValue))
                return Visibility.Hidden;

            return values.Any(v => (bool)v) ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FlagEnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            var originalValue = (int)value;
            var compareValue = (int)System.Convert.ChangeType(parameter, typeof(int));

            return (originalValue & compareValue) == compareValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class XamlTabToolTipMultiConverter : IMultiValueConverter
    { 
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(null) || values.Contains(DependencyProperty.UnsetValue))
                return null;

            var fileName = (string)values[0];
            var status = (TabStatus)values[1];

            if ((status & TabStatus.Locked) == TabStatus.Locked)
                return fileName + " [Read Only]";

            return fileName;
        }  

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToAConverter : IValueConverter
    {
        private Color? _curColor = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _curColor = (Color)value;

            return _curColor.Value.A;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromArgb((byte)((double)value), _curColor.Value.R, _curColor.Value.G, _curColor.Value.B);
        }
    }

    public class ColorToRedConverter : IValueConverter
    {
        private Color? _curColor = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _curColor = (Color)value;

            return _curColor.Value.R;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromArgb(_curColor.Value.A, (byte)((double)value), _curColor.Value.G, _curColor.Value.B);
        }
    }

    public class ColorToGreenConverter : IValueConverter
    {
        private Color? _curColor = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _curColor = (Color)value;

            return _curColor.Value.G;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromArgb(_curColor.Value.A, _curColor.Value.R, (byte)((double)value), _curColor.Value.B);
        }
    }

    public class ColorToBlueConverter : IValueConverter
    {
        private Color? _curColor = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _curColor = (Color)value;

            return _curColor.Value.B;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromArgb(_curColor.Value.A, _curColor.Value.R, _curColor.Value.G, (byte)((double)value));
        }
    }

    public class ColorToStringConverter : IValueConverter
    {
        private Color? _curColor = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _curColor = (Color)value;

            return _curColor.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorStr = (string)value;

            if (!string.IsNullOrWhiteSpace(colorStr) && Regex.IsMatch(colorStr, @"^#[\da-fA-F]{6,8}$"))
                return ColorConverter.ConvertFromString(colorStr);

            return _curColor.Value;
        }
    }

    public class ScrollOffsetToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(null) || values.Contains(DependencyProperty.UnsetValue))
                return Visibility.Collapsed;
 
            var offset = (double)values[0];
            var Length = (double)values[1];

            if (DoubleUtil.IsNaN(Length) || DoubleUtil.IsZero(Length))
                return Visibility.Collapsed;

            var isLeftOrTop = parameter == null;

            if (isLeftOrTop)
                return DoubleUtil.IsZero(offset) ? Visibility.Collapsed : Visibility.Visible;
            
            return DoubleUtil.AreClose(offset, Length) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
