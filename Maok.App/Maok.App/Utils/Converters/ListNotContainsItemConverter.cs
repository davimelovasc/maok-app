using System;
using System.Collections;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class ListNotContainsItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var list = value as IList;
            return (list?.Count ?? 0) == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}