using System;
using System.Collections;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class ListContainsItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IList list)
                return list.Count > (parameter != null ? System.Convert.ToInt32(parameter) : 0);

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}